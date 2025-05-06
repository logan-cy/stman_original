using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

    public class Job
    {
        #region "Properties"
            public int id { get; protected set; }
            public int companyid { get; protected set; }
            public int unitid { get; protected set; }
            public string contactname { get; protected set; }
            public string contactnumber { get; protected set; }
            public string desc { get; protected set; }
            public double quotecost { get; protected set; }
            public double actualcost { get; protected set; }
            public string subcontractor  { get; protected set; }
            public DateTime startdate { get; protected set; }
            public DateTime duedate { get; protected set; }
            public string status { get; protected set; }
            public bool insuranceclaim { get; protected set; }
            public int insuranceid { get; protected set; }
            public bool comeback { get; protected set; }
            public int comebackjobid { get; protected set; }

            public enum DateType
            {
                Due = 0,
                Start = 1,
            }

            private string jobnumber { get; set; }
            private string jobprefix = "WH";
            private bool checkoverdue = false;
        #endregion

        #region "Constructor Logic"
            public Job()
            {
            }
            public Job(int JobID)
            {
                id = JobID;
            }
            public Job(int UnitID, bool IsJobID)
            {
                if (IsJobID)
                {
                    id = UnitID;
                }
                else
                {
                    unitid = UnitID;
                }
            }
            public Job(string Status, bool IsStatus)
            {
                if (IsStatus)
                {
                    status = Status;
                }
                else
                {
                    jobnumber = Status;
                }
            }
            public Job(DateTime Date, DateType DateType, bool ForOverDue)
            {
                switch (DateType)
                {
                    case DateType.Due:
                        duedate = Date.Date;
                        break;
                    case DateType.Start:
                        startdate = Date.Date;
                        break;
                }

                if (ForOverDue)
                {
                    checkoverdue = true;
                }
                        
            }
            public Job(int JobID, int UnitID, string ContactName, string ContactNumber, string JobDescription, double QuoteCost, double ActualCost, string SubContractor, DateTime StartDate, DateTime DueDate, string Status, bool IsInsuranceClaim, int InsuranceID, bool IsComeback, int ComebackJobID)
            {
                id = JobID;
                unitid = UnitID;
                contactname = ContactName;
                contactnumber = ContactNumber;
                desc = JobDescription;
                quotecost = QuoteCost;
                actualcost = ActualCost;
                subcontractor = SubContractor;
                startdate = StartDate;
                duedate = DueDate;
                status = Status;
                insuranceclaim = IsInsuranceClaim;
                insuranceid = InsuranceID;
                comeback = IsComeback;
                comebackjobid = ComebackJobID;
            }
        #endregion

        public DataTable GetJobData()
        {
            DataTable dt = new DataTable();
            List<MySqlParameter> args = new List<MySqlParameter>();

            string cmdText = "Jobs_SelectAll";

            if (id > 0)
            {
                args.Add(new MySqlParameter("xjobid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                cmdText = "Jobs_SelectByJobID";
            }
            else if (id <= 0 && unitid > 0)
            {
                args.Add(new MySqlParameter("xunitid", MySqlDbType.Int32));
                args[args.Count - 1].Value = unitid;

                cmdText = "Jobs_SelectByUnitID";
            }
            else if (id <= 0 && unitid <= 0 && status != null)
            {
                args.Add(new MySqlParameter("xstatus", MySqlDbType.VarChar));
                args[args.Count - 1].Value = status;

                cmdText = "Jobs_SelectByStatus";
            }
            else if (id <= 0 && unitid <= 0 && status == null && jobnumber != null)
            {
                args.Add(new MySqlParameter("xjobnumber", MySqlDbType.VarChar));
                args[args.Count - 1].Value = jobnumber;

                cmdText = "Jobs_SelectByJobNumber";
            }
            else if (duedate != null && checkoverdue == false)
            {
                args.Add(new MySqlParameter("xduedate", MySqlDbType.Date));
                args[args.Count - 1].Value = duedate.Date;
                
                cmdText = "Jobs_SelectByDueDate";
            }
            //else if (startdate != null)
            //{
            //    args.Add(new MySqlParameter("xstartdate", MySqlDbType.Date));
            //    args[args.Count - 1].Value = startdate.Date;

            //    cmdText = "Jobs_SelectByStartDate";
            //}
            else if (duedate != null && checkoverdue)
            {
                args.Add(new MySqlParameter("xselectiondate", MySqlDbType.Date));
                args[args.Count - 1].Value = duedate;

                cmdText = "Jobs_SelectOverdue";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public DataTable GetIncompleteJobs()
        {
            DataTable dt = new DataTable();

            List<MySqlParameter> args = new List<MySqlParameter>();
            args.Add(new MySqlParameter("xcompanyid", MySqlDbType.Int32));
            args[args.Count - 1].Value = (int)HttpContext.Current.Session["companyid"];

            using (Database db = new Database("Jobs_SelectNotComplete", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string Save()
        {
            try
            {
                var args = new List<MySqlParameter>()
            {
                new MySqlParameter("xjobid", MySqlDbType.Int32)
                {
                    Value = id,
                    Direction = ParameterDirection.InputOutput
                },
                new MySqlParameter("xunitid", MySqlDbType.Int32)
                {
                    Value = unitid
                },
                new MySqlParameter("xcapturedate", MySqlDbType.Date)
                {
                    Value = DateTime.Now.Date
                },
                new MySqlParameter("xcontactname", MySqlDbType.VarChar)
                {
                    Value = contactname
                },
                new MySqlParameter("xcontactnumber", MySqlDbType.VarChar)
                {
                    Value = contactnumber
                },
                new MySqlParameter("xdescription", MySqlDbType.VarChar)
                {
                    Value = desc
                },
                new MySqlParameter("xquote", MySqlDbType.Double)
                {
                    Value = quotecost
                },
                new MySqlParameter("xactual", MySqlDbType.Double)
                {
                    Value = actualcost
                },
                new MySqlParameter("xsubcontractors", MySqlDbType.VarChar)
                {
                    Value = subcontractor
                },
                new MySqlParameter("xstartdate", MySqlDbType.DateTime)
                {
                    Value = startdate
                },
                new MySqlParameter("xduedate", MySqlDbType.DateTime)
                {
                    Value = duedate
                },
                new MySqlParameter("xstatus", MySqlDbType.VarChar)
                {
                    Value = status
                },
                new MySqlParameter("xinsuranceclaim", MySqlDbType.Bit)
                {
                    Value = insuranceclaim
                },
                new MySqlParameter("xpolicyid", MySqlDbType.Int32)
                {
                    Value = insuranceid
                },
                new MySqlParameter("xcomeback", MySqlDbType.Bit)
                {
                    Value = comeback
                },
                new MySqlParameter("xcomebackjobid", MySqlDbType.Int32)
                {
                    Value = comebackjobid
                }
            };

                string cmd = "Jobs_Insert";
                string returnval = "xjobid";

                if (id != 0)
                {
                    cmd = "Jobs_Update";
                    using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                    {
                        db.ExecuteQuery();
                    }

                    return "success";
                }
                else
                {
                    int jobid = 0;
                    using (Database db = new Database(cmd, CommandType.StoredProcedure, args, returnval))
                    {
                        jobid = Convert.ToInt32(db.GetOutputParameter());
                    }

                    jobnumber = String.Format("{0}{1}", jobprefix, Convert.ToString(jobid).PadLeft(4, '0'));

                    args.Clear();
                    args.Add(new MySqlParameter("xjobnumber", MySqlDbType.VarChar));
                    args[args.Count - 1].Value = jobnumber;
                    args.Add(new MySqlParameter("xjobid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = jobid;
                    using (Database db = new Database("Jobs_UpdateJobNumber", CommandType.StoredProcedure, args))
                    {
                        db.ExecuteQuery();
                    }

                    return String.Format("success&{0}", jobnumber);
                }                
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
        }

        public string Delete()
        {
            try
            {
                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xjobid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Jobs_Delete", CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }

                return "success";
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
        }
    }