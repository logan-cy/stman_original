using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

public class Business
    {
        public int id { get; protected set; }
        public string businessname { get; protected set; }

        public Business()
        {
        }
        public Business(int BusinessID)
        {
            id = BusinessID;
        }
        public Business(string BusinessName)
        {
            id = 0;
            businessname = BusinessName;
        }

        public DataTable GetBusinessData()
        {
            DataTable dt = new DataTable();
            string cmdText = "Business_SelectByBusinessID";
            List<MySqlParameter> args = new List<MySqlParameter>();

            if (id > 0)
            {
                args.Add(new MySqlParameter("xbusinessid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;
            }
            else
            {
                cmdText = "Business_SelectAll";
            }

            using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
            {
                dt = db.GetDataTable();
            }

            return dt;
        }

        public string SaveBusiness()
        {
            try
            {
                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xbusinessname", MySqlDbType.VarChar));
                args[args.Count - 1].Value = businessname;

                string cmd = "Business_Insert";

                if (id != 0)
                {
                    args.Add(new MySqlParameter("xbusinessid", MySqlDbType.Int32));
                    args[args.Count - 1].Value = id;

                    cmd = "Business_Update";
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

        public string DeleteBusiness()
        {
            try
            {
                List<MySqlParameter> args = new List<MySqlParameter>();
                args.Add(new MySqlParameter("xbusinessid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                using (Database db = new Database("Business_Delete", CommandType.StoredProcedure, args))
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