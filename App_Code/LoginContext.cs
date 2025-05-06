using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for LoginContext
/// </summary>
public class LoginContext
{
    public string email { get; protected set; }
    public string password { get; protected set; }

    public LoginContext()
    {
    }

    public void SetUser(string EmailAddress, string Password)
    {
        email = EmailAddress;
        password = Password;
    }

    public bool DoLogin()
    {
        bool result = false;
        string cmdText = "Users_SelectByEmailAndPassword";

        var args = new List<MySqlParameter>
            {
                new MySqlParameter("xemail", MySqlDbType.VarChar)
                {
                    Value = email
                },
                new MySqlParameter("xpassword", MySqlDbType.VarChar)
                {
                    Value = password
                }
            };

        DataTable dt;
        using (Database db = new Database(cmdText, CommandType.StoredProcedure, args))
        {
            dt = db.GetDataTable();
        }

        if (dt != null)
        {
            populateSessionVars(dt.Rows[0]);
            result = true;
        }

        return result;
    }

    public void DoLogin(string EmailAddress)
    {
        string cmdText = "select * from users where email = @email";

        List<MySqlParameter> args = new List<MySqlParameter>();
        args.Add(new MySqlParameter("@email", MySqlDbType.VarChar));
        args[args.Count - 1].Value = EmailAddress;

        DataTable dt;
        using (Database db = new Database(cmdText, CommandType.Text, args))
        {
            dt = db.GetDataTable();
        }

        if (dt != null)
        {
            populateSessionVars(dt.Rows[0]);
        }
    }

    private void populateSessionVars(DataRow row)
    {
        HttpContext.Current.Session["userid"] = row["userid"];
        HttpContext.Current.Session["companyid"] = row["companyid"];
        HttpContext.Current.Session["companyname"] = row["companyname"];
        HttpContext.Current.Session["emailaddress"] = row["email"];
        HttpContext.Current.Session["firstname"] = row["firstname"];
        HttpContext.Current.Session["lastname"] = row["lastname"];
    }
}