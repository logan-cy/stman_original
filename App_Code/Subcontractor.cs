using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

    public class Subcontractor
    {
        #region "Properties"
        public int id { get; protected set; }
        public string name { get; protected set; }
        public string address { get; protected set; }
        public string cperson { get; protected set; }
        public string cnumber { get; protected set; }
        public int businessid { get; protected set; }
        public int rating { get; protected set; }
        public string search { get; protected set; }
        #endregion

        #region "Constructor Logic"
        public Subcontractor()
        {
        }
        public Subcontractor(int SubContractorID)
        {
            id = SubContractorID;
        }
        public Subcontractor(int SubContractorID, bool IsBusinessID)
        {
            if (IsBusinessID)
            {
                businessid = SubContractorID;
            }
            else
            {
                id = SubContractorID;
            }
        }
        public Subcontractor(string SearchTerm)
        {
            search = SearchTerm;
        }
        public Subcontractor(string SubContractorName, string SubContractAddress, string ContactPerson, string ContactNumber, int BusinessID, int Rating)
        {
            id = 0;
            name = SubContractorName;
            address = SubContractAddress;
            cperson = ContactPerson;
            cnumber = ContactNumber;
            businessid = BusinessID;
            rating = Rating;
        }
        public Subcontractor(int SubContractorID, string SubContractorName, string SubContractAddress, string ContactPerson, string ContactNumber, int BusinessID, int Rating)
        {
            id = SubContractorID;
            name = SubContractorName;
            address = SubContractAddress;
            cperson = ContactPerson;
            cnumber = ContactNumber;
            businessid = BusinessID;
            rating = Rating;
        }
        #endregion

        public DataTable GetSubcontractorData()
        {
            DataTable dt = new DataTable();
            string cmdText = "Subcontractors_SelectBySubcontractorID";
            List<MySqlParameter> args = new List<MySqlParameter>();

            if (id > 0)
            {
                args.Add(new MySqlParameter("xsubcontractorid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;
            }
            else
            {
                cmdText = "Subcontractors_SelectAll";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string SaveSubcontractor()
        {
            try
            {
                var args = new List<MySqlParameter>
                {
                    new MySqlParameter("xsubcontractorname", MySqlDbType.VarChar)
                    {
                        Value = name
                    },
                    new MySqlParameter("xsubcontractoraddress", MySqlDbType.VarChar)
                    {
                        Value = address
                    },
                    new MySqlParameter("xcontactperson", MySqlDbType.VarChar)
                    {
                        Value = cperson
                    },
                    new MySqlParameter("xcontactnumber", MySqlDbType.VarChar)
                    {
                        Value = cnumber
                    },
                    new MySqlParameter("xbusinessid", MySqlDbType.VarChar)
                    {
                        Value = businessid
                    },
                    new MySqlParameter("xrating", MySqlDbType.VarChar)
                    {
                        Value = rating
                    }
                };

                string cmd = "Subcontractors_Insert";

                if (id != 0)
                {
                    args.Add(new MySqlParameter("xsubcontractorid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = id;

                    cmd = "Subcontractors_Update";
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

        public string DeleteSubcontractor()
        {
            try
            {
                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xsubcontractorid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Subcontractors_Delete", CommandType.StoredProcedure, args))
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
            args.Add(new MySqlParameter("search", MySqlDbType.Text));
            args[args.Count - 1].Value = String.Format("%{0}%", search);

            using (Database db = new Database("Subcontractors_Search", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public DataTable Filter()
        {
            DataTable dt = new DataTable();


            List<MySqlParameter> args = new List<MySqlParameter>();
            string cmdText = "Subcontractors_SelectAll";

            if (businessid > 0)
            {
                args.Add(new MySqlParameter("xbusinessid", MySqlDbType.Int32));
                args[args.Count - 1].Value = businessid;

                cmdText = "Subcontractors_SelectByBusinessID";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }
    }