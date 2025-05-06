using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Subcontractors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadSubcontractorData();
            loadBusinesses();
            gvItems.Visible = false;
        }
    }

    private void loadBusinesses()
    {
        Business bus = new Business();
        DataTable dt = bus.GetBusinessData();

        ddlFilter.DataSource = dt;
        ddlFilter.DataTextField = "businessname";
        ddlFilter.DataValueField = "businessid";
        ddlFilter.DataBind();

        ddlFilter.Items.Insert(0, new ListItem("Please select a business", "0"));

        ddlBusiness.DataSource = dt;
        ddlBusiness.DataTextField = "businessname";
        ddlBusiness.DataValueField = "businessid";
        ddlBusiness.DataBind();

        ddlBusiness.Items.Insert(0, new ListItem("Please select a business", "0"));
    }

    private void loadSubcontractorData()
    {
        Subcontractor sc = new Subcontractor();
        gvItems.DataSource = sc.GetSubcontractorData();
        gvItems.DataBind();
    }

    private void loadSubcontractorData(string SearchTerm)
    {
        Subcontractor sc = new Subcontractor(SearchTerm);
        gvItems.DataSource = sc.Search();
        gvItems.DataBind();
    }

    private void loadSubcontractorData(int SubcontractorID)
    {
        DataTable dt = new DataTable();

        Subcontractor sc = new Subcontractor(SubcontractorID, false);
        dt = sc.GetSubcontractorData();

        txtAddress.Text = (string)dt.Rows[0]["address"];
        txtContactNumber.Text = (string)dt.Rows[0]["contactnumber"];
        txtContactPerson.Text = (string)dt.Rows[0]["contactperson"];
        txtName.Text = (string)dt.Rows[0]["name"];

        string businessid = Convert.ToString(dt.Rows[0]["businessid"]);

        ddlBusiness.ClearSelection();
        ddlBusiness.Items.FindByValue(businessid).Selected = true;
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int businessid = Convert.ToInt32(ddlFilter.SelectedItem.Value);

        if (businessid == 0)
        {
            loadSubcontractorData();
        }
        else
        {
            filterSubcontractors(businessid);

            Session["subcontractorfilter"] = true;
        }

        gvItems.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveSubcontractor();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void saveSubcontractor()
    {
        int subContractorID = Convert.ToInt32(lblSubcontractorID.Text);

        Subcontractor sc = new Subcontractor(subContractorID, txtName.Text, txtAddress.Text, txtContactPerson.Text, txtContactNumber.Text, Convert.ToInt32(ddlBusiness.SelectedItem.Value), Convert.ToInt32(ddlRating.SelectedItem.Value));
        string result = sc.SaveSubcontractor();

        if (result == "success")
        {
            loadSubcontractorData();
            clearFields();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    private void clearFields()
    {
        ddlFilter.ClearSelection();
        txtSearch.Text = String.Empty;
        txtAddress.Text = String.Empty;
        txtContactNumber.Text = String.Empty;
        txtContactPerson.Text = String.Empty;
        txtName.Text = String.Empty;
        ddlBusiness.ClearSelection();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAddEdit.Visible = true;
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btnDelete.OnClientClick = "return confirm('Are you sure you would like to delete this subcontractor?');";
        }
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int subcontractorID = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["subcontractorid"];
        //lblSubcontractorID.Text = Convert.ToString(subcontractorID);

        loadSubcontractorData(subcontractorID);

        divView.Visible = false;
        divAddEdit.Visible = true;

        lblerr.Text = String.Empty;
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int subcontractorid = (int)gvItems.DataKeys[e.RowIndex].Values["subcontractorid"];
        deleteSubcontractor(subcontractorid);
        loadSubcontractorData();
    }

    private void deleteSubcontractor(int subcontractorid)
    {
        Subcontractor sc = new Subcontractor(subcontractorid, false);
        string result = sc.DeleteSubcontractor();

        if (result == "success")
        {
            loadSubcontractorData();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadSubcontractorData(txtSearch.Text);
        gvItems.Visible = true;
    }

    protected void fuSubcontractor_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        btnBulkAdd.Enabled = false;
        if (!fuSubcontractor.HasFile)
        {
            lblerr.Text = "Please select a file to upload.";
        }
        else
        {
            if (System.IO.Path.GetExtension(fuSubcontractor.FileName) == ".csv")
            {
                try
                {
                    BulkData bd = new BulkData(fuSubcontractor.FileBytes);

                    string result = bd.ImportSubcontractorList();

                    if (result == "success")
                    {
                        lblerr.Text = "Bulk data import succeeded.";
                        lblerr.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblerr.Text = result;
                    }
                }
                catch (Exception ex)
                {
                    lblerr.Text = ex.Message;
                }
            }

            btnBulkAdd.Enabled = true;
        }
    }

    protected void btnBulkAdd_Click(object sender, EventArgs e)
    {
        loadSubcontractorData();

        gvItems.Visible = true;
    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;

        bool filtered = (bool)Session["subcontractorfilter"];
        if (!filtered)
        {
            loadSubcontractorData(txtSearch.Text);
        }
        else
        {
            filterSubcontractors(Convert.ToInt32(ddlFilter.SelectedItem.Value));
        }
    }

    private void filterSubcontractors(int businessid)
    {
        Subcontractor sub = new Subcontractor(businessid, true);

        Session["subcontractordata"] = sub.Filter();

        gvItems.DataSource = (DataTable)Session["subcontractordata"];
        gvItems.DataBind();
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
        DataTable dt = Session["subcontractordata"] as DataTable;

        if (dt != null)
        {
            dt.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, getSortDirection(e.SortExpression));
            gvItems.DataSource = Session["subcontractordata"];
            gvItems.DataBind();
        }
    }
}