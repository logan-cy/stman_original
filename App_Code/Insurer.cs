using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

    public class Insurer
    {
        public int id { get; protected set; }
        public string name { get; protected set; }
        public string contactname { get; protected set; }
        public string contactnumber { get; protected set; }
        public string search { get; protected set; }

        public Insurer()
        {
        }

        public Insurer(int InsurerID, string InsurerName, string ContactPerson, string ContactNumber)
        {
            id = InsurerID;
            name = InsurerName;
            contactname = ContactPerson;
            contactnumber = ContactNumber;
        }

        public Insurer(int InsurerID)
        {
            id = InsurerID;
        }

        public Insurer(string SearchTerm)
        {
            search = SearchTerm;
        }

        public DataTable GetInsurerData()
        {
            DataTable dt = new DataTable();
            string cmdText = "Insurers_SelectByInsurerID";
            List<MySqlParameter> args = new List<MySqlParameter>();

            if (id > 0)
            {
                args.Add(new MySqlParameter("xinsurerid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;
            }
            else
            {
                cmdText = "Insurers_SelectAll";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string SaveInsurer()
        {
            try
            {
                var args = new List<MySqlParameter>
                {
                    new MySqlParameter("xname", MySqlDbType.VarChar)
                    {
                        Value = name
                    },
                    new MySqlParameter("xcontactname", MySqlDbType.VarChar)
                    {
                        Value = contactname
                    },
                    new MySqlParameter("xcontactnumber", MySqlDbType.VarChar)
                    {
                        Value = contactnumber
                    }
                };

                string cmd = "Insurers_Insert";

                if (id != 0)
                {
                    args.Add(new MySqlParameter("xinsurerid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = id;

                    cmd = "Insurers_Update";
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

        public string DeleteInsurer()
        {
            try
            {
                Policy pol = new Policy(id, false);
                DataTable dt = pol.GetPolicyData();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int policyid = (int)row["policyid"];

                        Policy p = new Policy(policyid);
                        p.DeletePolicy();
                    }
                }

                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xinsurerid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Insurers_Delete", CommandType.StoredProcedure, args))
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

            using (Database db = new Database("Insurers_Search", CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }
    }