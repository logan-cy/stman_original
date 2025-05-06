using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

    public class Unit
    {
        #region "Properties"
            public int id { get; protected set; }
            public int complexid { get; protected set; }
            public string unitnumber { get; protected set; }
            public string owneraccount { get; protected set; }
            public string ownername { get; protected set; }
            public string ownernumber { get; protected set; }
            public string tenantaccount { get; protected set; }
            public string tenantname { get; protected set; }
            public string tenantnumber { get; protected set; }
            public string search { get; protected set; }
        #endregion

        #region "Constructor Logic"
            public Unit(int UnitID, int ComplexID, string UnitNumber, string OwnerAccount, string OwnerName, string OwnerNumber, string TenantAccount, string TenantName, string TenantNumber)
            {
                id = UnitID;
                complexid = ComplexID;
                unitnumber = UnitNumber;
                owneraccount = OwnerAccount;
                ownername = OwnerName;
                ownernumber = OwnerNumber;
                tenantaccount = TenantAccount;
                tenantname = TenantName;
                tenantnumber = TenantNumber;
            }
            public Unit(int ComplexID, string UnitNumber, string OwnerAccount, string OwnerName, string OwnerNumber, string TenantAccount, string TenantName, string TenantNumber)
            {
                id = 0;
                complexid = ComplexID;
                unitnumber = UnitNumber;
                owneraccount = OwnerAccount;
                ownername = OwnerName;
                ownernumber = OwnerNumber;
                tenantaccount = TenantAccount;
                tenantname = TenantName;
                tenantnumber = TenantNumber;
            }
            public Unit(string SearchTerm)
            {
                search = SearchTerm;
            }
            public Unit(int UnitID)
            {
                id = UnitID;
            }
            public Unit(int TargetID, bool IsUnitID)
            {
                if (IsUnitID)
                {
                    id = TargetID;
                }
                else
                {
                    complexid = TargetID;
                }
            }
            public Unit()
            {
            }
        #endregion

        public DataTable GetUnitData()
        {
            DataTable dt = new DataTable();
            string cmdText = "Unit_SelectByUnitID";
            List<MySqlParameter> args = new List<MySqlParameter>();

            if (id > 0 && complexid == 0)
            {
                args.Add(new MySqlParameter("xunitid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;
            }
            else if (id < 1 && complexid >= 1)
            {
                cmdText = "Unit_SelectByComplexID";

                args.Add(new MySqlParameter("xcomplexid", MySqlDbType.Int32));
                args[args.Count - 1].Value = complexid;
            }
            else
            {
                args.Add(new MySqlParameter("xcompanyid", MySqlDbType.Int32));
                args[args.Count - 1].Value = (int)HttpContext.Current.Session["companyid"];
                cmdText = "Unit_SelectByCompanyID";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string SaveUnit()
        {
            try
            {
                var args = new List<MySqlParameter>
                {
                    new MySqlParameter("xcomplexid", MySqlDbType.Int32)
                    {
                        Value = complexid
                    },
                    new MySqlParameter("xunitnumber", MySqlDbType.VarChar)
                    {
                        Value = unitnumber
                    },
                    new MySqlParameter("xowneraccount", MySqlDbType.VarChar)
                    {
                        Value = owneraccount
                    },
                    new MySqlParameter("xownername", MySqlDbType.VarChar)
                    {
                        Value = ownername
                    },
                    new MySqlParameter("xownernumber", MySqlDbType.VarChar)
                    {
                        Value = ownernumber
                    },
                    new MySqlParameter("xtenantaccount", MySqlDbType.VarChar)
                    {
                        Value = tenantaccount
                    },
                    new MySqlParameter("xtenantname", MySqlDbType.VarChar)
                    {
                        Value = tenantname
                    },
                    new MySqlParameter("xtenantnumber", MySqlDbType.VarChar)
                    {
                        Value = tenantnumber
                    }
                };

                string cmd = "Unit_Insert";

                if (id != 0)
                {
                    args.Add(new MySqlParameter("xunitid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = id;

                    cmd = "Unit_Update";
                }

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
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

        public string DeleteUnit()
        {
            try
            {
                Job job = new Job(id, false);
                DataTable dt = job.GetJobData();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int jobid = (int)row["jobid"];

                        Job j = new Job(jobid);
                        j.Delete();
                    }
                }

                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xunitid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Unit_Delete", CommandType.StoredProcedure, args))
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

        public DataTable Search()
        {
            DataTable dt = new DataTable();

            List<MySqlParameter> args = new List<MySqlParameter>();
            args.Add(new MySqlParameter("xcompanyid", MySqlDbType.Int32));
            args[args.Count - 1].Value = (int)HttpContext.Current.Session["companyid"];
            args.Add(new MySqlParameter("xsearch", MySqlDbType.Text));
            args[args.Count - 1].Value = String.Format("%{0}%", search);

            using (Database db = new Database("Unit_Search", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public DataTable Filter()
        {
            DataTable dt = new DataTable();

            List<MySqlParameter> args = new List<MySqlParameter>();
            args.Add(new MySqlParameter("xcomplexid", MySqlDbType.Int32));
            args[args.Count - 1].Value = complexid;

            using (Database db = new Database("Unit_SelectByComplexID", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

    }