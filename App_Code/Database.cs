using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Database
/// </summary>
public class Database : IDisposable
{

    #region "Properties"

    public string sql { get; protected set; }
    public CommandType cType { get; protected set; }
    public List<MySqlParameter> args { get; protected set; }
    public string rValue { get; protected set; }

    private MySqlConnection conn;

    private string server = "localhost";
    private string database = "stman";
    private string user = "root";
    //private string password = "root";
    private string password = "Mozart000";
    #endregion

    #region "Constructor Logic"
    public Database(string CommandText, CommandType CommandType, List<MySqlParameter> Parameters, string ReturnParameter = "")
    {
        buildConnection();

        sql = CommandText;
        cType = CommandType;
        args = Parameters;
        rValue = ReturnParameter;
    }

    public Database(string CommandText, CommandType CommandType)
    {
        buildConnection();

        args = new List<MySqlParameter>();
        sql = CommandText;
        cType = CommandType;
    }

    public Database()
    {
    }

    private void buildConnection()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(String.Format("data source={0}; initial catalog={1}; user id={2}; password={3};", server, database, user, password));

        conn = new MySqlConnection(sb.ToString());
    }
    #endregion

    public MySqlDataReader GetReader()
    {
        MySqlDataReader dr;

        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
        {

            if (args.Count > 0)
            {
                cmd.Parameters.AddRange(args.ToArray());
            }

            cmd.CommandType = cType;

            dr = cmd.ExecuteReader();
        }

        return dr;
    }

    public DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        MySqlDataAdapter da = new MySqlDataAdapter();

        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
        {
            if (args.Count > 0)
            {
                cmd.Parameters.AddRange(args.ToArray());
            }

            cmd.CommandType = cType;

            da.SelectCommand = cmd;
            da.Fill(dt);
        }

        if (dt.Rows.Count > 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    public object GetOutputParameter()
    {
        MySqlCommand cmd = new MySqlCommand(sql, conn);

        if (args.Count > 0)
        {
            cmd.Parameters.AddRange(args.ToArray());
        }

        cmd.CommandType = cType;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return cmd.Parameters[rValue].Value;
    }

    public void ExecuteQuery()
    {
        MySqlCommand cmd = new MySqlCommand(sql, conn);

        if (args.Count > 0)
        {
            cmd.Parameters.AddRange(args.ToArray());
        }

        cmd.CommandType = cType;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Dispose()
    {
        sql = String.Empty;
        cType = CommandType.StoredProcedure;

        if (args != null)
        {
            args.Clear();
        }

        rValue = String.Empty;

        if (conn != null)
        {
            conn.Close();
            conn = null;
        }
        GC.Collect();
    }
}