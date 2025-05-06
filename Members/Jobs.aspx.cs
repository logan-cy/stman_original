using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Jobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadJobs();
            gvItems.Visible = false;
            loadComplexes();
            loadBusinesses();
            loadSubcontractors();
            loadInsurers();

            pnlCallback.Visible = false;
            pnlInsurer.Visible = false;

            ddlFilterUnit.Items.Add(new ListItem("Please select a complex", "0"));
            ddlUnit.Items.Add(new ListItem("Please select a complex", "0"));
            ddlPolicy.Items.Add(new ListItem("Please select a policy", "0"));

            if (Session["jobid"] != null)
            {
                int jobid = Convert.ToInt32(Session["jobid"]);
                lblJobID.Text = Convert.ToString(Session["jobid"]);
                loadJobs(jobid);

                divView.Visible = false;
                divAddEdit.Visible = true;

                Session["jobid"] = null;
            }
        }
    }

    private void loadBusinesses()
    {
        Business bus = new Business();
        DataTable dt = bus.GetBusinessData();
        ddlFilterBusiness.DataSource = dt;
        ddlFilterBusiness.DataTextField = "businessname";
        ddlFilterBusiness.DataValueField = "businessid";
        ddlFilterBusiness.DataBind();

        ddlFilterBusiness.Items.Insert(0, new ListItem("Please select a business", "0"));
    }

    private void loadSubcontractors()
    {
        Subcontractor sc = new Subcontractor();
        ddlSubcontractor.DataSource = sc.GetSubcontractorData();
        ddlSubcontractor.DataTextField = "name";
        ddlSubcontractor.DataValueField = "subcontractorid";
        ddlSubcontractor.DataBind();

        ddlSubcontractor.Items.Insert(0, new ListItem("Please select a subcontractor", "0"));
    }

    private void loadComplexes()
    {
        Complex complex = new Complex();
        DataTable dt = complex.GetComplexData();

        ddlFilterComplex.DataSource = dt;
        ddlFilterComplex.DataTextField = "name";
        ddlFilterComplex.DataValueField = "complexid";
        ddlFilterComplex.DataBind();

        ddlFilterComplex.Items.Insert(0, new ListItem("Please select a complex", "0"));

        ddlComplex.DataSource = dt;
        ddlComplex.DataTextField = "name";
        ddlComplex.DataValueField = "complexid";
        ddlComplex.DataBind();

        ddlComplex.Items.Insert(0, new ListItem("Please select a complex", "0"));
    }

    private void loadJobs()
    {
        Job job = new Job();
        gvItems.DataSource = job.GetJobData();
        gvItems.DataBind();
    }

    private void loadJobs(int jobid)
    {
        Job job = new Job(jobid);
        DataTable dt = job.GetJobData();

        txtActual.Text = dt.Rows[0]["actualcost"].ToString();

        if (dt.Rows[0]["contactnumber"] == DBNull.Value)
        {
            txtContactNumber.Text = String.Empty;
        }
        else
        {
            txtContactNumber.Text = (string)dt.Rows[0]["contactnumber"];
        }
        if (dt.Rows[0]["contactname"] == DBNull.Value)
        {
            txtContactPerson.Text = String.Empty;
        }
        else
        {
            txtContactPerson.Text = (string)dt.Rows[0]["contactname"];
        }

        txtDescription.Text = (string)dt.Rows[0]["description"].ToString().Replace("|", "\r\n");
        txtEndDate.Text = dt.Rows[0]["duedate"].ToString().Substring(0, 10);
        txtQuote.Text = dt.Rows[0]["quotedcost"].ToString();
        txtStartDate.Text = dt.Rows[0]["startdate"].ToString().Substring(0, 10);

        ddlComplex.ClearSelection();
        ddlComplex.Items.FindByValue(Convert.ToString(dt.Rows[0]["complexid"])).Selected = true;
        loadUnits();

        ddlUnit.ClearSelection();
        ddlUnit.Items.FindByValue(Convert.ToString(dt.Rows[0]["unitid"])).Selected = true;

        ddlStatus.ClearSelection();
        ddlStatus.Items.FindByValue((string)dt.Rows[0]["status"]).Selected = true;

        if (Convert.ToBoolean(dt.Rows[0]["insuranceclaim"]))
        {
            try
            {
                loadInsurers();
                pnlInsurer.Visible = true;
                rblInsurance.Items.FindByValue("true").Selected = true;
                ddlInsurer.Items.FindByValue(Convert.ToString(dt.Rows[0]["insurerid"])).Selected = true;

                loadPolicies((int)dt.Rows[0]["insurerid"]);
                ddlPolicy.Items.FindByValue(Convert.ToString(dt.Rows[0]["policyid"])).Selected = true;
            }
            catch (NullReferenceException ex)
            {
                ddlInsurer.Items[0].Selected = true;
                ddlPolicy.Items[0].Selected = true;
            }
        }
        else
        {
            rblInsurance.Items.FindByValue("false").Selected = true;
        }

        if (Convert.ToBoolean(dt.Rows[0]["comeback"]))
        {
            rblCallback.Items[0].Selected = true; // Yes
            loadUnitJobs();

            ddlCallbackJobNumber.ClearSelection();
            ddlCallbackJobNumber.Items.FindByValue(Convert.ToString(dt.Rows[0]["comebackjobid"])).Selected = true;
            int callbackjobid = (int)dt.Rows[0]["comebackjobid"];
            loadCallbackJob(callbackjobid);

            pnlCallback.Visible = true;
        }
        else
        {
            pnlCallback.Visible = false;
            rblCallback.Items[1].Selected = true; // No
        }

        string subcontractor = (string)dt.Rows[0]["subcontractors"];
        if (subcontractor != String.Empty)
        {
            String[] subcontractors = subcontractor.Split(',');

            lstSubcontractors.Items.Clear();
            for (int i = 0; i <= subcontractors.Length - 1; i++)
            {
                int subcontractorid = Convert.ToInt32(subcontractors[i]);

                Subcontractor sc = new Subcontractor(subcontractorid);
                DataTable scdt = sc.GetSubcontractorData();

                string scname = (string)scdt.Rows[0]["name"];
                string scid = Convert.ToString(scdt.Rows[0]["subcontractorid"]);

                lstSubcontractors.Items.Add(new ListItem(scname, scid));
            }
        }
    }

    private void loadPolicies(int insurerid)
    {
        Policy pol = new Policy(insurerid, false);

        ddlPolicy.DataSource = pol.GetPolicyData();
        ddlPolicy.DataTextField = "policynumber";
        ddlPolicy.DataValueField = "policyid";
        ddlPolicy.DataBind();

        ddlPolicy.Items.Insert(0, new ListItem("Please select a policy", "0"));
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

    protected void btnAddSubcontractor_Click(object sender, EventArgs e)
    {
        string id = ddlSubcontractor.SelectedItem.Value;
        string name = ddlSubcontractor.SelectedItem.ToString();

        lstSubcontractors.Items.Add(new ListItem(name, id));
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        int unitid = Convert.ToInt32(ddlUnit.SelectedItem.Value);
        Unit unit = new Unit(unitid);
        DataTable dt = unit.GetUnitData();

        txtContactPerson.Text = (string)dt.Rows[0]["ownername"];
        txtContactNumber.Text = (string)dt.Rows[0]["ownernumber"];
    }

    protected void ddlComplex_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUnits();
    }

    private void loadUnits()
    {
        int complexid = Convert.ToInt32(ddlComplex.SelectedItem.Value);

        Unit unit = new Unit(complexid, false);
        DataTable dt = unit.GetUnitData();

        ddlUnit.Items.Clear();
        ddlUnit.ClearSelection();

        foreach (DataRow row in dt.Rows)
        {
            ddlUnit.Items.Add(new ListItem((string)row["unitnumber"], Convert.ToString(row["unitid"])));
        }

        ddlUnit.Items.Insert(0, new ListItem("Please select a unit", "0"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveJob();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    private void saveJob()
    {
        int jobid = Convert.ToInt32(lblJobID.Text);
        int unitid = Convert.ToInt32(ddlUnit.SelectedItem.Value);
        double quote = 0;
        if (txtQuote.Text != String.Empty)
        {
            quote = Convert.ToDouble(txtQuote.Text);
        }
        double actual = 0;
        if (txtActual.Text != String.Empty)
        {
            actual = Convert.ToDouble(txtActual.Text);
        }
        DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
        bool isinsurance = false;
        bool iscomeback = false;
        int comebackjobid = 0;
        string subcontractors = String.Empty;

        if (rblInsurance.SelectedItem.Value == "true")
        {
            isinsurance = true;
        }

        if (rblCallback.SelectedItem.Value == "true")
        {
            iscomeback = true;
            comebackjobid = Convert.ToInt32(ddlCallbackJobNumber.SelectedItem.Value);
        }

        if (lstSubcontractors.Items.Count > 0)
        {
            foreach (ListItem li in lstSubcontractors.Items)
            {
                subcontractors += String.Format("{0},", li.Value);
            }

            subcontractors = subcontractors.Substring(0, subcontractors.Length - 1);
        }
        else
        {
            subcontractors = String.Empty;
        }

        Job job = new Job(jobid, unitid, txtContactPerson.Text, txtContactNumber.Text, txtDescription.Text.Replace("\r\n", "|"), quote, actual, subcontractors, startdate, enddate, ddlStatus.SelectedItem.Value, isinsurance, Convert.ToInt32(ddlPolicy.SelectedItem.Value), iscomeback, comebackjobid);
        string res = job.Save();
        string result = res.Substring(0, 7);

        if (jobid == 0)
        {
            // create a new job number
            string jobnumber = res.Substring(8);
            res = String.Format("Job successfully captured. Job number is <b>{0}</b>", jobnumber);
        }
        else
        {
            res = "Job successfully saved";
        }

        if (result == "success")
        {
            loadJobs();
            clearFields();
            lblerr.Text = res;
            lblerr.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblerr.Text = res;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();

        divAddEdit.Visible = false;
        divView.Visible = true;
    }

    protected void ddlFilterComplex_SelectedIndexChanged(object sender, EventArgs e)
    {
        int complexid = Convert.ToInt32(ddlFilterComplex.SelectedItem.Value);

        Unit unit = new Unit(complexid, false);
        ddlFilterUnit.DataSource = unit.GetUnitData();
        ddlFilterUnit.DataTextField = "unitnumber";
        ddlFilterUnit.DataValueField = "unitid";
        ddlFilterUnit.DataBind();

        ddlFilterUnit.Items.Insert(0, new ListItem("Please select a unit", "0"));
    }

    protected void ddlFilterUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        int unitid = Convert.ToInt32(ddlFilterUnit.SelectedItem.Value);

        Job job = new Job(unitid, false);
        gvItems.DataSource = job.GetJobData();
        gvItems.DataBind();

        gvItems.Visible = true;
    }

    protected void ddlFilterSubcontractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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
            btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this job?');";

            // Process "comeback" data value
            if (e.Row.Cells[e.Row.Cells.Count - 4].Text == "0")
            {
                e.Row.Cells[e.Row.Cells.Count - 4].Text = "No";
            }
            else
            {
                e.Row.Cells[e.Row.Cells.Count - 4].Text = "Yes";
            }

            // Truncate visible description
            string desc = e.Row.Cells[e.Row.Cells.Count - 3].Text;
            if (desc.Length >= 30)
            {
                e.Row.Cells[e.Row.Cells.Count - 3].Text = desc.Substring(0, 30).PadRight(3, '.');
            }
        }
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int jobid = (int)gvItems.DataKeys[e.NewSelectedIndex].Values["jobid"];
            loadJobs(jobid);
            lblJobID.Text = Convert.ToString(jobid);

            divAddEdit.Visible = true;
            divView.Visible = false;
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.Message;
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int jobid = (int)gvItems.DataKeys[e.RowIndex].Values["jobid"];

        Job job = new Job(jobid);
        string result = job.Delete();

        if (result == "success")
        {
            loadJobs();
        }
        else
        {
            lblerr.Text = result;
        }
    }

    private void clearFields()
    {
        ddlFilterBusiness.ClearSelection();
        ddlFilterComplex.ClearSelection();
        ddlFilterUnit.ClearSelection();
        ddlComplex.ClearSelection();
        ddlInsurer.ClearSelection();
        ddlPolicy.ClearSelection();
        ddlStatus.ClearSelection();
        ddlUnit.ClearSelection();
        txtActual.Text = String.Empty;
        txtContactNumber.Text = String.Empty;
        txtContactPerson.Text = String.Empty;
        txtDescription.Text = String.Empty;
        txtEndDate.Text = String.Empty;
        txtQuote.Text = String.Empty;
        txtStartDate.Text = String.Empty;
        lstSubcontractors.Items.Clear();
        ddlSubcontractor.ClearSelection();
    }

    protected void rblInsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblInsurance.SelectedItem.Value == "true")
        {
            pnlInsurer.Visible = true;
            loadInsurers();
        }
        else
        {
            pnlInsurer.Visible = false;
        }
    }

    protected void ddlInsurer_SelectedIndexChanged(object sender, EventArgs e)
    {
        int insurerid = Convert.ToInt32(ddlInsurer.SelectedItem.Value);

        Policy pol = new Policy(insurerid, false);
        ddlPolicy.DataSource = pol.GetPolicyData();
        ddlPolicy.DataTextField = "policynumber";
        ddlPolicy.DataValueField = "policyid";
        ddlPolicy.DataBind();

        ddlPolicy.Items.Insert(0, new ListItem("Please select a policy", "0"));
    }

    protected void ddlFilterBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubcontractor.ClearSelection();
        ddlSubcontractor.Items.Clear();

        Subcontractor sc = new Subcontractor(Convert.ToInt32(ddlFilterBusiness.SelectedItem.Value), true);
        DataTable dt = sc.Filter();

        ddlSubcontractor.DataSource = dt;
        ddlSubcontractor.DataTextField = "name";
        ddlSubcontractor.DataValueField = "subcontractorid";
        ddlSubcontractor.DataBind();

        ddlSubcontractor.Items.Insert(0, new ListItem("Please select a subcontractor", "0"));
    }

    protected void btnDelSubcontractor_Click(object sender, EventArgs e)
    {
        if (lstSubcontractors.SelectedItem.Value != null)
        {
            lstSubcontractors.Items.RemoveAt(lstSubcontractors.SelectedIndex);
        }
    }

    protected void ddlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string status = ddlFilterStatus.SelectedItem.Value;

        Job job = new Job(status, true);
        gvItems.DataSource = job.GetJobData();
        gvItems.DataBind();

        gvItems.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string jobnumber = txtSearch.Text;

        Job job = new Job(jobnumber, false);
        gvItems.DataSource = job.GetJobData();
        gvItems.DataBind();

        gvItems.Visible = true;

        txtSearch.Text = String.Empty;
    }

    protected void rblCallback_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlCallback.Visible = true;

        loadUnitJobs();       
    }

    protected void ddlCallbackJobNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCallbackJob();
    }

    private void loadUnitJobs()
    {
        Job job = new Job(Convert.ToInt32(ddlUnit.SelectedItem.Value), false);

        ddlCallbackJobNumber.DataSource = job.GetJobData();
        ddlCallbackJobNumber.DataTextField = "jobnumber";
        ddlCallbackJobNumber.DataValueField = "jobid";
        ddlCallbackJobNumber.DataBind();

        ddlCallbackJobNumber.Items.Insert(0, new ListItem("Please select a job", "0"));
    }

    private void loadCallbackJob()
    {
        int jobid = Convert.ToInt32(ddlCallbackJobNumber.SelectedItem.Value);

        loadCallbackJob(jobid);
    }

    private void loadCallbackJob(int jobid)
    {
        Job job = new Job(jobid);
        DataTable dt = job.GetJobData();

        if (dt != null)
        {
            string subcontractors = "None";
            if (dt.Rows[0]["name"] != DBNull.Value)
            {
                subcontractors = (string)dt.Rows[0]["name"];
            }

            lblCallbackJobDescription.Text = String.Format("<p>Date Captured: {0}<br />Subcontractor: {1}<br />{2}", Convert.ToString(dt.Rows[0]["capturedate"]).Substring(0,10), subcontractors, (string)dt.Rows[0]["description"]);
        }
    }
}