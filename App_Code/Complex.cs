using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


public class Complex
{
    #region "Properties"
    public int id { get; protected set; }
    public int companyid { get; protected set; }
    public string name { get; protected set; }
    public string address { get; protected set; }
    public string cperson { get; protected set; }
    public string cnumber { get; protected set; }
    public string pnumber { get; protected set; }
    public string search { get; protected set; }
    #endregion

    #region "Constructor Logic"
    public Complex(int ComplexID, int CompanyID, string ComplexName, string ComplexAddress, string ContactPerson, string ContactNumber, string PolicyNumber)
    {
        id = ComplexID;
        name = ComplexName;
        address = ComplexAddress;
        cperson = ContactPerson;
        cnumber = ContactNumber;
        pnumber = PolicyNumber;
    }

    public Complex(int CompanyID, string ComplexName, string ComplexAddress, string ContactPerson, string ContactNumber, string PolicyNumber)
    {
        id = 0;
        companyid = CompanyID;
        name = ComplexName;
        address = ComplexAddress;
        cperson = ContactPerson;
        cnumber = ContactNumber;
        pnumber = PolicyNumber;
    }

    public Complex(int ComplexID)
    {
        id = ComplexID;
    }

    public Complex(int CompanyID, string SearchTerm)
    {
        search = SearchTerm;
    }

    public Complex()
    {
    }
    #endregion

    public DataTable GetComplexData()
    {
        DataTable dt = new DataTable();
        string cmdText = "Complex_SelectByComplexID";
        List<MySqlParameter> args = new List<MySqlParameter>();

        if (id > 0)
        {
            args.Add(new MySqlParameter("xcomplexid", MySqlDbType.Int32));
            args[args.Count - 1].Value = id;
        }
        else
        {
            args.Add(new MySqlParameter("xcompanyid", MySqlDbType.Int32));
            args[args.Count - 1].Value = companyid;

            cmdText = "Complex_SelectByCompanyID";
        }

        using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
        {
            dt = db.GetDataTable();
        }

        return dt;
    }

    public string SaveComplex()
    {
        try
        {
            var args = new List<MySqlParameter>
                {
                    new MySqlParameter("xcompanyid", MySqlDbType.Int32)
                    {
                        Value = companyid
                    },
                    new MySqlParameter("xcomplexname", MySqlDbType.VarChar)
                    {
                        Value = name
                    },
                    new MySqlParameter("xcomplexaddress", MySqlDbType.VarChar)
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
                    new MySqlParameter("xpolicynumber", MySqlDbType.VarChar)
                    {
                        Value = pnumber
                    }
                };

            string cmd = "Complex_Insert";

            if (id != 0)
            {
                args.Add(new MySqlParameter("xcomplexid", MySqlDbType.Int32));
                args[args.Count - 1].Value = id;

                cmd = "Complex_Update";
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

    public string DeleteComplex()
    {
        try
        {
            Unit unit = new Unit(id, false);
            DataTable dt = unit.GetUnitData();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int unitid = (int)row["unitid"];
                    Unit u = new Unit(unitid);
                    u.DeleteUnit();
                }
            }

            List<MySqlParameter> args = new List<MySqlParameter>();
            args.Add(new MySqlParameter("xcomplexid", MySqlDbType.Int32));
            args[args.Count - 1].Value = id;

            using (Database db = new Database("Complex_Delete", CommandType.StoredProcedure, args))
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
        args[args.Count - 1].Value = companyid;
        args.Add(new MySqlParameter("xsearch", MySqlDbType.Text));
        args[args.Count - 1].Value = String.Format("%{0}%", search);

        using (Database db = new Database("Complex_Search", CommandType.StoredProcedure, args))
        {
            dt = db.GetDataTable();
        }

        return dt;
    }
}