using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Complex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadComplexData();
            loadInsurers();
            loadPolicies();
            gvItems.Visible = false;
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this complex?');";
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int complexid = (int)gvItems.DataKeys[e.RowIndex].Values["complexid"];
        deleteComplex(complexid);
        loadComplexData();
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int complexid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["complexid"];

            loadComplexData(complexid);
            Session["complexid"] = complexid;

            divView.Visible = false;
            divAddEdit.Visible = true;
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.Message;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadComplexData(txtSearch.Text);
        gvItems.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void loadInsurers()
    {
        Insurer ins = new Insurer();
        ddlInsurer.DataSource = ins.GetInsurerData();
        ddlInsurer.DataTextField = "name";
        ddlInsurer.DataValueField = "insurerid";
        ddlInsurer.DataBind();

        ddlInsurer.Items.Insert(0, new ListItem("Please select an insurer", "0"));
    }

    protected void ddlInsurer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPolicies(Convert.ToInt32(ddlInsurer.SelectedItem.Value));
    }

    private void loadComplexData()
    {
        Complex complex = new Complex((int)Session["companyid"]);

        Session["complexdata"] = complex.GetComplexData();

        gvItems.DataSource = (DataTable)Session["complexdata"];
        gvItems.DataBind();

    }

    private void loadComplexData(int complexID)
    {
        Complex complex = new Complex(complexID);
        Session["complexdata"] = complex.GetComplexData();

        DataTable dt = (DataTable)Session["complexdata"];

        txtComplexName.Text = (string)dt.Rows[0]["name"];
        txtComplexAddress.Text = (string)dt.Rows[0]["address"];
        txtContactPerson.Text = (string)dt.Rows[0]["contactperson"];
        txtContactNumber.Text = (string)dt.Rows[0]["contactnumber"];

        ddlInsurer.ClearSelection();
        ddlPolicy.ClearSelection();

        if (dt.Rows[0]["insurerid"] == DBNull.Value)
        {
            ddlInsurer.Items[0].Selected = true;
        }
        else
        {
            ddlInsurer.Items.FindByValue(Convert.ToString(dt.Rows[0]["insurerid"])).Selected = true;
        }

        loadPolicies(Convert.ToInt32(ddlInsurer.SelectedItem.Value));
        
        if (dt.Rows[0]["policynumber"] == DBNull.Value)
        {
            ddlPolicy.Items[0].Selected = true;
        }
        else
        {
            ddlPolicy.Items.FindByValue(Convert.ToString(dt.Rows[0]["policynumber"])).Selected = true;
            //txtPolicyNumber.Text = (string)dt.Rows[0]["policyid"];
        }

        lblComplexID.Text = Convert.ToString(complexID);
    }

    private void loadPolicies()
    {
        Policy pol = new Policy();

        ddlPolicy.DataSource = pol.GetPolicyData();
        ddlPolicy.DataTextField = "policynumber";
        ddlPolicy.DataValueField = "policyid";
        ddlPolicy.DataBind();

        ddlPolicy.Items.Insert(0, new ListItem("Please select a policy", "0"));
    }

    private void loadPolicies(int insurerid)
    {
        ddlPolicy.Items.Clear();

        Policy pol = new Policy(insurerid, false);

        ddlPolicy.DataSource = pol.GetPolicyData();
        ddlPolicy.DataTextField = "policynumber";
        ddlPolicy.DataValueField = "policyid";
        ddlPolicy.DataBind();

        ddlPolicy.Items.Insert(0, new ListItem("Please select a policy", "0"));
    }

    private void loadComplexData(string SearchTerm)
    {
        Complex complex = new Complex((int)Session["companyid"], SearchTerm);

        Session["complexdata"] = complex.Search();

        gvItems.DataSource = (DataTable)Session["complexdata"];
        gvItems.DataBind();
    }

    private void deleteComplex(int ComplexID)
    {
        Complex complex = new Complex(ComplexID);
        string result = complex.DeleteComplex();

        if (result == "success")
        {
            loadComplexData();
            gvItems.Visible = false;
        }
        else
        {
            lblerr.Text = result;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAddEdit.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveComplex();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void saveComplex()
    {
        int complexID = Convert.ToInt32(lblComplexID.Text);

        Complex complex = new Complex(complexID, txtComplexName.Text, txtComplexAddress.Text, txtContactPerson.Text, txtContactNumber.Text, ddlPolicy.SelectedItem.Value);
        string result = complex.SaveComplex();

        if (result == "success")
        {
            loadComplexData();
            clearFields();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    private void clearFields()
    {
        txtSearch.Text = String.Empty;
        txtComplexAddress.Text = String.Empty;
        txtComplexName.Text = String.Empty;
        txtContactNumber.Text = String.Empty;
        txtContactPerson.Text = String.Empty;
        ddlInsurer.ClearSelection();
        ddlPolicy.Items.Clear();
    }

    protected void fuComplex_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        btnAddBulk.Enabled = false;
        if (!fuComplex.HasFile)
        {
            lblerr.Text = "Please select a file to upload.";
        }
        else
        {
            if (System.IO.Path.GetExtension(fuComplex.FileName) == ".csv")
            {
                try
                {
                    BulkData bd = new BulkData(fuComplex.FileBytes);

                    string result = bd.ImportComplexList();

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
        loadComplexData();
    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
        loadComplexData(txtSearch.Text);
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
        DataTable dt = Session["complexdata"] as DataTable;

        if (dt != null)
        {
            dt.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, getSortDirection(e.SortExpression));
            gvItems.DataSource = Session["complexdata"];
            gvItems.DataBind();
        }
    }
}