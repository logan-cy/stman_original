using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadJobs();
        }
    }

    private void loadJobs()
    {
        Job job = new Job();
        Session["jobdata"] = job.GetIncompleteJobs();

        gvItems.DataSource = (DataTable)Session["jobdata"];
        gvItems.DataBind();
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int jobid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["jobid"];

            Session["jobid"] = jobid;
            Response.Redirect("Jobs.aspx");
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.ToString();
        }
    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
        loadJobs();
    }

    private string getSortDirection(string column)
    {
        string sortDirection = "ASC";

        string sortExpression = ViewState["sortExpression"] as string;

        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        ViewState["SortDirection"] = sortDirection;
        ViewState["sortExpression"] = column;

        return sortDirection;
    }

    protected void gvItems_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["jobdata"] as DataTable;

        if (dt != null)
        {
            dt.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, getSortDirection(e.SortExpression));
            gvItems.DataSource = Session["jobdata"];
            gvItems.DataBind();
        }
    }
}