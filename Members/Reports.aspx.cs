using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

public partial class Members_Reports : System.Web.UI.Page
{
    private enum FilterType
    {
        None = 0,
        Complex = 1,
        Subcontractor = 2,
        DateRange = 3,
        MultipleSubcontractors = 4
    }

    private FilterType filter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadComplexes();
            loadContractors();
        }
    }

    private void loadComplexes()
    {
        Complex complex = new Complex();

        ddlComplex.DataSource = complex.GetComplexData();
        ddlComplex.DataTextField = "name";
        ddlComplex.DataValueField = "complexid";
        ddlComplex.DataBind();

        ddlComplex.Items.Insert(0, new ListItem("Please select a complex", "0"));
    }

    private void loadContractors()
    {
        Subcontractor sub = new Subcontractor();

        ddlContractor.DataSource = sub.GetSubcontractorData();
        ddlContractor.DataTextField = "name";
        ddlContractor.DataValueField = "subcontractorid";
        ddlContractor.DataBind();

        ddlContractor.Items.Insert(0, new ListItem("please select a subcontractor", "0"));
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddlFilter.SelectedItem.Value)
        {
            case "complex":
                filter = FilterType.Complex;
                divComplexFilter.Visible = true;
                break;
            case "date range":
                filter = FilterType.DateRange;
                divComplexFilter.Visible = false;
                divContractorFilter.Visible = false;
                divDateFilter.Visible = true;
                divReport.Visible = true;
                break;
            case "contractor":
                filter = FilterType.Subcontractor;
                divComplexFilter.Visible = false;
                divDateFilter.Visible = false;
                divContractorFilter.Visible = true;
                break;
            case "multiple":
                divComplexFilter.Visible = false;
                divContractorFilter.Visible = false;
                divDateFilter.Visible = false;
                filter = FilterType.MultipleSubcontractors;
                break;
        }
    }

    protected void ddlComplex_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlComplex.SelectedItem.Value != "0")
        {
            divReport.Visible = true;
            filter = FilterType.Complex;
            loadReport();
        }
    }

    protected void ddlContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlContractor.SelectedItem.Value != "0")
        {
            divReport.Visible = true;
            filter = FilterType.Subcontractor;
            loadReport();
        }
    }

    private void loadReport()
    {
        string cmd = String.Empty;
        
        List<MySqlParameter> args = new List<MySqlParameter>();
        args.Add(new MySqlParameter("@status", MySqlDbType.VarChar));
        args[args.Count - 1].Value = ddlStatus.SelectedItem.Value;

        switch (filter)
        {
            case FilterType.Complex:
                // Report_SelectByComplexID(xcomplexid INT, xstatus VARCHAR(50))
                cmd = "select co.name as complex_name, un.unitnumber, jo.jobnumber, jo.capturedate, jo.description from jobs jo inner join units un on jo.unitid = un.unitid inner join complex co on un.complexid = co.complexid where co.complexid = @complexid and jo.status = @status";

                args.Add(new MySqlParameter("@complexid", MySqlDbType.Int32));
                args[args.Count - 1].Value = Convert.ToInt32(ddlComplex.SelectedItem.Value);
                break;

            case FilterType.DateRange:
                // Report_SelectByDateRange(xstartdate DATE, xenddate DATE, xstatus VARCHAR(50))
                cmd = "select co.name as complex_name, un.unitnumber, jo.jobnumber, jo.capturedate, jo.description from jobs jo inner join units un on jo.unitid = un.unitid inner join complex co on un.complexid = co.complexid where startdate between @startdate and @enddate and jo.status = @status";

                args.Add(new MySqlParameter("@startdate", MySqlDbType.Date));
                args[args.Count - 1].Value = Convert.ToDateTime(txtDateFrom.Text).Date;

                args.Add(new MySqlParameter("@enddate", MySqlDbType.Date));
                args[args.Count - 1].Value = Convert.ToDateTime(txtDateTo.Text).Date;
                break;

            case FilterType.Subcontractor:
                // Report_SelectBySubcontractor(xsubcontractor INT, xstatus VARCHAR(50))
                cmd = "select co.name as complex_name, un.unitnumber, jo.jobnumber, jo.capturedate, jo.description from jobs jo inner join units un on jo.unitid = jo.unitnumber inner join complex co on un.unitid = co.unitid inner join subcontractors sub on jo.subcontractors = sub.subcontractorid where jo.subcontractors like @subcontractor and jo.status = @status";

                args.Add(new MySqlParameter("@subcontractor", MySqlDbType.Int32));
                args[args.Count - 1].Value = Convert.ToInt32(ddlContractor.SelectedItem.Value);
                break;

            case FilterType.MultipleSubcontractors:
                // Report_SelectMultipleSubcontractors()
                cmd = "select co.name as complex_name, un.unitnumber, jo.jobnumber, jo.capturedate, jo.description from jobs jo inner join units un on jo.unitid = un.unitid inner join complex co on un.complexid = co.complexid where jo.subcontractors like '%,%' and jo.status = xstatus";
                break;
        }

        using (Database db = new Database(cmd, System.Data.CommandType.Text, args))
        {
            Session["reportData"] = db.GetDataTable();

            gvItems.DataSource = (DataTable)Session["reportData"];
            gvItems.DataBind();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (divDateFilter.Visible)
        {
            filter = FilterType.DateRange;
        }

        loadReport();
    }

    protected void imbExcel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = (DataTable)Session["reportData"];

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Complex Name,Unit Number,Job Number,Capture Date,Description");

        foreach (DataRow row in dt.Rows)
        {
            sb.AppendLine(String.Format("{0},{1},{2},{3},{4}", row["complex_name"], row["unitnumber"], row["jobnumber"], row["capturedate"], row["description"].ToString().Replace(",", " ").Replace("|", " ")));
        }

        Response.Clear();
        Response.ClearHeaders();
        Response.ContentType = "text/csv";
        Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", String.Format("SecMan Report - {0}.csv", DateTime.Now.Date.ToString("yyyy-MM-dd"))));
        Response.Write(sb.ToString());

        Response.End();
    }

}