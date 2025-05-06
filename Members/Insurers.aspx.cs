using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Insurers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadInsurerData();
            gvItems.Visible = false;
        }
    }

    private void loadInsurerData()
    {
        Insurer ins = new Insurer();
        gvItems.DataSource = ins.GetInsurerData();
        gvItems.DataBind();
    }

    private void loadInsurerData(int insurerid)
    {
        Insurer ins = new Insurer(insurerid);
        DataTable dt = ins.GetInsurerData();

        txtContact.Text = (string)dt.Rows[0]["contactname"];
        txtName.Text = (string)dt.Rows[0]["name"];
        txtNumber.Text = (string)dt.Rows[0]["contactnumber"];

        lblInsurerID.Text = Convert.ToString(insurerid);
    }

    private void loadInsurerData(string SearchTerm)
    {
        Insurer ins = new Insurer(SearchTerm);

        Session["insurerdata"] = ins.Search();

        gvItems.DataSource = ins.Search();
        gvItems.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAddEdit.Visible = true;
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int insurerid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["insurerid"];

            loadInsurerData(insurerid);

            divView.Visible = false;
            divAddEdit.Visible = true;
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.Message;
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this insurer?');";
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int insurerid = (int)gvItems.DataKeys[e.RowIndex].Values["insurerid"];
        deleteInsurer(insurerid);
        loadInsurerData();
    }

    private void deleteInsurer(int insurerid)
    {
        Insurer ins = new Insurer(insurerid);
        string result = ins.DeleteInsurer();

        if (result == "success")
        {
            loadInsurerData();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInsurer();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void saveInsurer()
    {
        int insurerid = Convert.ToInt32(lblInsurerID.Text);

        Insurer ins = new Insurer(insurerid, txtName.Text, txtContact.Text, txtNumber.Text);
        string result = ins.SaveInsurer();

        if (result == "success")
        {
            loadInsurerData();
            clearFields();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void clearFields()
    {
        lblInsurerID.Text = "0";
        txtSearch.Text = String.Empty;
        txtContact.Text = String.Empty;
        txtName.Text = String.Empty;
        txtNumber.Text = String.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadInsurerData(txtSearch.Text);
        gvItems.Visible = true;
    }

    protected void fuInsurer_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        btnAddBulk.Enabled = false;
        if (!fuInsurer.HasFile)
        {
            lblerr.Text = "Please select a file to upload.";
        }
        else
        {
            if (System.IO.Path.GetExtension(fuInsurer.FileName) == ".csv")
            {
                try
                {
                    BulkData bd = new BulkData(fuInsurer.FileBytes);

                    string result = bd.ImportInsurerList();

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

            btnAddBulk.Enabled = true;
        }
    }

    protected void btnAddBulk_Click(object sender, EventArgs e)
    {
        loadInsurerData();
    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
        loadInsurerData(txtSearch.Text);
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
        DataTable dt = Session["insurerdata"] as DataTable;

        if (dt != null)
        {
            dt.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, getSortDirection(e.SortExpression));
            gvItems.DataSource = Session["insurerdata"];
            gvItems.DataBind();
        }
    }
}