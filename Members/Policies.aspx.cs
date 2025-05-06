using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Policies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPolicyData();
            loadInsurerData();

            gvItems.Visible = false;
        }
    }

    private void loadInsurerData()
    {
        Insurer ins = new Insurer();
        DataTable dt = ins.GetInsurerData();

        ddlFilter.DataSource = dt;
        ddlFilter.DataTextField = "name";
        ddlFilter.DataValueField = "insurerid";
        ddlFilter.DataBind();

        ddlFilter.Items.Insert(0, new ListItem("Please select an insurer", "0"));

        ddlInsurer.DataSource = dt;
        ddlInsurer.DataTextField = "name";
        ddlInsurer.DataValueField = "insurerid";
        ddlInsurer.DataBind();

        ddlInsurer.Items.Insert(0, new ListItem("Please select an insurer", "0"));
    }

    private void loadPolicyData()
    {
        Policy pol = new Policy();
        gvItems.DataSource = pol.GetPolicyData();
        gvItems.DataBind();
    }

    private void loadPolicyData(int policyid)
    {
        Policy pol = new Policy(policyid);
        DataTable dt = pol.GetPolicyData();
        txtPolicyNumber.Text = (string)dt.Rows[0]["policynumber"];

        ddlInsurer.ClearSelection();

        string iid = Convert.ToString(dt.Rows[0]["insurerid"]);
        ddlInsurer.Items.FindByValue(iid).Selected = true;
    }

    private void loadPolicyData(string SearchTerm)
    {
        Policy pol = new Policy(SearchTerm);
        DataTable dt = pol.GetPolicyData();

        gvItems.DataSource = dt;
        gvItems.DataBind();

        gvItems.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadPolicyData(txtSearch.Text);
        gvItems.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAddEdit.Visible = true;
        lblerr.Text = String.Empty;
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this policy?');";
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int policyid = (int)gvItems.DataKeys[e.RowIndex].Values["policyid"];
        deletePolicy(policyid);
        loadPolicyData();
        lblerr.Text = String.Empty;
    }

    private void deletePolicy(int policyid)
    {
        Policy pol = new Policy(policyid);
        pol.DeletePolicy();
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int policyid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["policyid"];
        //lblPolicyID.Text = Convert.ToString(policyid);

        loadPolicyData(policyid);

        divView.Visible = false;
        divAddEdit.Visible = true;

        lblerr.Text = String.Empty;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        savePolicy();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void savePolicy()
    {
        int policyid = Convert.ToInt32(lblPolicyID.Text);
        int insurerid = Convert.ToInt32(ddlInsurer.SelectedItem.Value);

        Policy pol = new Policy(policyid, insurerid, txtPolicyNumber.Text);
        string result = pol.SavePolicy();

        if (result == "success")
        {
            loadPolicyData();
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
        txtPolicyNumber.Text = String.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int insurerid = Convert.ToInt32(ddlFilter.SelectedItem.Value);

        if (insurerid == 0)
        {
            loadPolicyData();
        }
        else
        {
            Policy pol = new Policy(insurerid, false);
            gvItems.DataSource = pol.GetPolicyData();
            gvItems.DataBind();
        }

        gvItems.Visible = true;
    }

    protected void fuPolicy_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        btnBulkAdd.Enabled = false;
        if (!fuPolicy.HasFile)
        {
            lblerr.Text = "Please select a file to upload.";
        }
        else
        {
            if (System.IO.Path.GetExtension(fuPolicy.FileName) == ".csv")
            {
                try
                {
                    BulkData db = new BulkData(fuPolicy.FileBytes);

                    string result = db.ImportPolicyList();

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
        loadPolicyData();
    }
}