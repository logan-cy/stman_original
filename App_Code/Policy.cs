using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

    public class Policy
    {
        #region "Properties"
            public int id { get; protected set; }
            public int insurerid { get; protected set; }
            public string policynumber { get; protected set; }
            public string search { get; protected set; }
        #endregion

            #region "Constructor Logic"
                public Policy()
            {
            }
                public Policy(int ID, bool IsPolicyID)
                {
                    if (IsPolicyID)
                    {
                        id = ID;
                    }
                    else
                    {
                        insurerid = ID;
                    }
                }
                public Policy(int PolicyID)
                {
                    id = PolicyID;
                }
                public Policy(string SearchTerm)
                {
                    search = SearchTerm;
                }
                public Policy(int PolicyID, int InsurerID, string PolicyNumber)
        {
            id = PolicyID;
            insurerid = InsurerID;
            policynumber = PolicyNumber;
        }
            #endregion

        public DataTable GetPolicyData()
        {
            DataTable dt = new DataTable();
            string cmd = "Policy_SelectByPolicyID";
            List<MySqlParameter> args = new List<MySqlParameter>();

            if (id > 0)
            {
                args.Add(new MySqlParameter("xpolicyid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;
            }
            else
            {
                cmd = "Policy_SelectAll";
            }
            if (id == 0)
            {
                args.Add(new MySqlParameter("xinsurerid", MySqlDbType.Int32));
                args[args.Count - 1].Value = insurerid;

                cmd = "Policy_SelectByInsurerID";
            }

            using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string SavePolicy()
        {
            try
            {
                var args = new List<MySqlParameter>
                {
                    new MySqlParameter("xinsurerid", MySqlDbType.Int32)
                    {
                        Value = insurerid
                    },
                    new MySqlParameter("xpolicynumber", MySqlDbType.VarChar)
                    {
                        Value = policynumber
                    }
                };

                string cmd = "Policy_Insert";

                if (id != 0)
                {
                    args.Add(new MySqlParameter("xpolicyid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = id;

                    cmd = "Policy_Update";
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

        public string DeletePolicy()
        {
            try
            {
                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xpolicyid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Policy_Delete", CommandType.StoredProcedure, args))
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
            args.Add(new MySqlParameter("xsearch", MySqlDbType.Text));
            args[args.Count - 1].Value = String.Format("%{0}%", search);

            using (Database db = new Database("Policy_Search", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public DataTable Filter()
        {
            DataTable dt = new DataTable();

            List<MySqlParameter> args = new List<MySqlParameter>();
            args.Add(new MySqlParameter("xinsurerid", MySqlDbType.Int32));
            args[args.Count - 1].Value = insurerid;

            using (Database db = new Database("Policy_SelectByInsurerID", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }
    }