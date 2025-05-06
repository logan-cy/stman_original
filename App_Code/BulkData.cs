using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for BulkData
/// </summary>
public class BulkData
{
    private enum DataType
    {
        Complex = 0,
        Insurer = 1,
        Policy = 2,
        Subcontractor = 3,
        Unit = 4
    }

    public byte[] src { get; protected set; }

    public BulkData(byte[] Source)
    {
        src = Source;
    }

    public string ImportComplexList()
    {
        try
        {
            DataTable dt = getExistingData(DataType.Complex);
            string result = String.Empty;
            string cmd = "Complex_Insert";

            string content = System.Text.Encoding.ASCII.GetString(src);
            string[] data = content.Replace("\r\n", "|").Split('|');

            List<MySqlParameter> args = new List<MySqlParameter>();

            foreach (string row in data)
            {
                args.Clear();
                string[] cols = row.Split(',');
                string final = String.Empty;

                bool exists = false;

                if (cols[0] == "Complex Name")
                {
                    continue;
                }

                foreach (string col in cols)
                {
                    if (col == "^N")
                    {
                        final += String.Format("{0},", DBNull.Value);
                    }
                }

                cols = final.Split(',');

                foreach (DataRow r in dt.Rows)
                {
                    string name = (string)r["name"];

                    if (name == cols[0])
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    continue;
                }

                args.Add(new MySqlParameter("xcomplexname", cols[0]));
                args.Add(new MySqlParameter("xcomplexaddress", cols[1]));
                args.Add(new MySqlParameter("xcontactperson", cols[2]));
                args.Add(new MySqlParameter("xcontactnumber", cols[3]));
                args.Add(new MySqlParameter("xpolicynumber", cols[4]));

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }
            }

            return "success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ImportInsurerList()
    {
        try
        {
            DataTable dt = getExistingData(DataType.Insurer);
            string result = String.Empty;
            string cmd = "Insurers_Insert";

            string content = System.Text.Encoding.ASCII.GetString(src);
            string[] data = content.Replace("\r\n", "|").Split('|');

            List<MySqlParameter> args = new List<MySqlParameter>();

            foreach (string row in data)
            {
                args.Clear();
                string[] cols = row.Split(',');
                string final = String.Empty;

                bool exists = false;

                if (cols[0] == "Insurer Name")
                {
                    continue;
                }

                foreach (string col in cols)
                {
                    if (col == "^N")
                    {
                        final += String.Format("{0},", DBNull.Value);
                    }
                }

                cols = final.Split(',');

                foreach (DataRow r in dt.Rows)
                {
                    string insname = (string)r["name"];

                    if (insname == cols[0])
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    continue;
                }

                args.Add(new MySqlParameter("xname", cols[0]));
                args.Add(new MySqlParameter("xcontactname", cols[1]));
                args.Add(new MySqlParameter("xcontactnumber", cols[2]));

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }
            }

            return "success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ImportPolicyList()
    {
        try
        {
            DataTable dt = getExistingData(DataType.Policy);
            string result = String.Empty;
            string cmd = "Policy_Insert";

            string content = System.Text.Encoding.ASCII.GetString(src);
            string[] data = content.Replace("\r\n", "|").Split('|');

            List<MySqlParameter> args = new List<MySqlParameter>();

            foreach (string row in data)
            {
                args.Clear();
                string[] cols = row.Split(',');
                string final = String.Empty;

                bool exists = false;

                if (cols[0] == "Insurer ID")
                {
                    continue;
                }

                foreach (string col in cols)
                {
                    if (col == "^N")
                    {
                        final += String.Format("{0},", DBNull.Value);
                    }
                }

                cols = final.Split(',');

                foreach (DataRow r in dt.Rows)
                {
                    string policynumber = Convert.ToString(r["policynumber"]);


                    if (policynumber == cols[1])
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    continue;
                }

                args.Add(new MySqlParameter("xinsurerid", cols[0]));
                args.Add(new MySqlParameter("xpolicynumber", cols[1]));

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }
            }

            return "success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ImportSubcontractorList()
    {
        try
        {
            DataTable dt = getExistingData(DataType.Subcontractor);
            string result = String.Empty;
            string cmd = "Subcontractors_Insert";

            string content = System.Text.Encoding.ASCII.GetString(src);
            string[] data = content.Replace("\r\n", "|").Split('|');

            List<MySqlParameter> args = new List<MySqlParameter>();

            foreach (string row in data)
            {
                args.Clear();

                string[] cols = row.Split(',');
                string final = String.Empty;

                bool exists = false;

                if (cols[0] == "Business ID")
                {
                    continue;
                }

                foreach (string col in cols)
                {
                    if (col == "^N")
                    {
                        final += String.Format("{0},", DBNull.Value);
                    }
                }

                cols = final.Split(',');

                foreach (DataRow r in dt.Rows)
                {
                    string subname = (string)r["name"];

                    if (subname == cols[1])
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    continue;
                }

                args.Add(new MySqlParameter("xbusinessid", cols[0]));
                args.Add(new MySqlParameter("xsubcontractorname", cols[1]));
                args.Add(new MySqlParameter("xsubcontractoraddress", cols[2]));
                args.Add(new MySqlParameter("xcontactperson", cols[3]));
                args.Add(new MySqlParameter("xcontactnumber", cols[4]));
                args.Add(new MySqlParameter("xrating", cols[5]));

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ImportUnitsList()
    {
        try
        {
            DataTable dt = getExistingData(DataType.Unit);
            string result = String.Empty;
            string cmd = "Unit_Insert";

            string content = System.Text.Encoding.ASCII.GetString(src);
            string[] data = content.Replace("\r\n", "|").Split('|');

            List<MySqlParameter> args = new List<MySqlParameter>();

            foreach (string row in data)
            {
                args.Clear();
                string[] cols = row.Split(',');
                string final = String.Empty;

                bool exists = false;

                if (cols[0] == "Complex ID")
                {
                    continue;
                }

                foreach (string col in cols)
                {
                    if (col == "^N")
                    {
                        final += String.Format("{0},", DBNull.Value);
                    }
                    else
                    {
                        final += String.Format("{0},", col);
                    }
                }

                cols = final.Split(',');

                foreach (DataRow r in dt.Rows)
                {
                    string complexid = Convert.ToString(r["complexid"]);
                    string unitnumber = (string)r["unitnumber"];


                    if (complexid == cols[0] && unitnumber == cols[1])
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    continue;
                }

                args.Add(new MySqlParameter("xcomplexid", cols[0]));
                args.Add(new MySqlParameter("xunitnumber", cols[1]));
                args.Add(new MySqlParameter("xowneraccount", cols[2]));
                args.Add(new MySqlParameter("xownername", cols[3]));
                args.Add(new MySqlParameter("xownernumber", cols[4]));
                args.Add(new MySqlParameter("xtenantaccount", cols[5]));
                args.Add(new MySqlParameter("xtenantname", cols[6]));
                args.Add(new MySqlParameter("xtenantnumber", cols[7]));

                using (Database db = new Database(cmd, CommandType.StoredProcedure, args))
                {
                    db.ExecuteQuery();
                }
            }

            return "success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private DataTable getExistingData(DataType dataType)
    {
        string cmd = String.Empty;
        DataTable dt = new DataTable();
        switch (dataType)
        {
            case DataType.Complex:
                cmd = "Complex_SelectAll";
                break;
            case DataType.Insurer:
                cmd = "Insurers_SelectAll";
                break;
            case DataType.Policy:
                cmd = "Policy_SelectAll";
                break;
            case DataType.Subcontractor:
                cmd = "Subcontractors_SelectAll";
                break;
            case DataType.Unit:
                cmd = "Unit_SelectAll";
                break;
        }

        using (Database db = new Database(cmd, CommandType.StoredProcedure))
        {
            dt = db.GetDataTable();
        }

        return dt;
    }
}