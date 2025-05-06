using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Units : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadUnitData();
            loadComplexData();
            gvItems.Visible = false;
        }
    }

    private void loadUnitData()
    {
        Unit unit = new Unit();
        gvItems.DataSource = unit.GetUnitData();
        gvItems.DataBind();
    }

    private void loadUnitData(string SearchTerm)
    {
        Unit unit = new Unit(SearchTerm);

        Session["unitdata"] = unit.Search();

        gvItems.DataSource = (DataTable)Session["unitdata"];
        gvItems.DataBind();
    }

    private void loadUnitData(int UnitID)
    {
        Unit unit = new Unit(UnitID, true);
        DataTable dt = unit.GetUnitData();

        txtOwnerAccount.Text = (string)dt.Rows[0]["owneraccount"];
        txtTenantAccount.Text = (string)dt.Rows[0]["tenantaccount"];
        txtOwner.Text = (string)dt.Rows[0]["ownername"];
        txtOwnerNum.Text = (string)dt.Rows[0]["ownernumber"];
        txtTenant.Text = (string)dt.Rows[0]["tenantname"];
        txtTenantNum.Text = (string)dt.Rows[0]["tenantnumber"];
        txtUnitNum.Text = (string)dt.Rows[0]["unitnumber"];

        ddlComplex.ClearSelection();

        string complexid = Convert.ToString(dt.Rows[0]["complexid"]);
        ddlComplex.Items.FindByValue(complexid).Selected = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadUnitData(txtSearch.Text);
        gvItems.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAddEdit.Visible = true;
        lblerr.Text = String.Empty;
    }

    private void loadComplexData()
    {
        Complex complex = new Complex();
        DataTable dt = complex.GetComplexData();

        ddlComplex.DataSource = dt;
        ddlComplex.DataTextField = "name";
        ddlComplex.DataValueField = "complexid";
        ddlComplex.DataBind();

        ddlComplex.Items.Insert(0, new ListItem("Please select a complex", "0"));

        ddlFilter.DataSource = dt;
        ddlFilter.DataTextField = "name";
        ddlFilter.DataValueField = "complexid";
        ddlFilter.DataBind();

        ddlFilter.Items.Insert(0, new ListItem("Please select a complex", "0"));
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int unitid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["unitid"];
        lblUnitID.Text = Convert.ToString(unitid);

        loadUnitData(unitid);

        divView.Visible = false;
        divAddEdit.Visible = true;

        lblerr.Text = String.Empty;
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this unit?');";
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int unitid = (int)gvItems.DataKeys[e.RowIndex].Values["unitid"];
        deleteUnit(unitid);
        loadUnitData();
        lblerr.Text = String.Empty;
    }

    private void deleteUnit(int UnitID)
    {
        Unit unit = new Unit(UnitID, true);
        unit.DeleteUnit();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int complexid = Convert.ToInt32(ddlFilter.SelectedItem.Value);

        if (complexid == 0)
        {
            loadUnitData();
        }
        else
        {
            filterUnits(complexid);

            Session["unitfilter"] = true;
        }

        gvItems.Visible = true;
    }

    private void filterUnits(int complexid)
    {
        Unit unit = new Unit(complexid, false);

        Session["unitdata"] = unit.Filter();

        gvItems.DataSource = (DataTable)Session["unitdata"];
        gvItems.DataBind(); 	
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveUnit();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void saveUnit()
    {
        int unitid = Convert.ToInt32(lblUnitID.Text);
        int complexid = Convert.ToInt32(ddlComplex.SelectedItem.Value);

        Unit unit = new Unit(unitid, complexid, txtUnitNum.Text, txtOwnerAccount.Text, txtOwner.Text, txtOwnerNum.Text, txtTenantAccount.Text, txtTenant.Text, txtTenantNum.Text);
        string result = unit.SaveUnit();

        if (result == "success")
        {
            loadUnitData();
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
        ddlComplex.Items.FindByValue("0").Selected = true;
        txtOwnerAccount.Text = String.Empty;
        txtOwner.Text = String.Empty;
        txtOwnerNum.Text = String.Empty;
        txtTenantAccount.Text = String.Empty;
        txtTenant.Text = String.Empty;
        txtTenantNum.Text = String.Empty;
        txtUnitNum.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    protected void fuUnit_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        btnBulkAdd.Enabled = false;
        if (!fuUnit.HasFile)
        {
            lblerr.Text = "Please select a file to upload.";
        }
        else
        {
            if (System.IO.Path.GetExtension(fuUnit.FileName) == ".csv")
            {
                try
                {
                    BulkData bd = new BulkData(fuUnit.FileBytes);

                    string result = bd.ImportUnitsList();

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
        loadUnitData();
    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;

        bool filtered = (bool)Session["unitfilter"];
        if (!filtered)
        {
            loadUnitData(txtSearch.Text);
        }
        else
        {
            filterUnits(Convert.ToInt32(ddlFilter.SelectedItem.Value));
        }
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
        DataTable dt = Session["unitdata"] as DataTable;

        if (dt != null)
        {
            dt.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, getSortDirection(e.SortExpression));
            gvItems.DataSource = Session["unitdata"];
            gvItems.DataBind();
        }
    }
}