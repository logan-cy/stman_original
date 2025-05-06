using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Members_Members : System.Web.UI.MasterPage
{
    //protected void OnPreInit(EventArgs e)
    //{
    //    base.Init += new EventHandler(Page_Init);
    //}

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    if (HttpContext.Current.User.Identity.IsAuthenticated)
    //    {
    //        if (Session["userid"] == null)
    //        {
    //            LoginContext log = new LoginContext();
    //            log.DoLogin(HttpContext.Current.User.Identity.Name);
    //        }
    //    }
    //    else
    //    {
    //        FormsAuthentication.SignOut();
    //        Response.Redirect("~/Login.aspx");
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (Database db = new Database())
            {
                lblCompanyName.Text = (string)Session["companyname"];
            }
        }

        if (Session["userid"] != null)
        {
            // valid login

            if (!IsPostBack)
            {
                lblWelcome.Text = String.Format("Welcome {0} {1}", Session["firstname"].ToString(), Session["lastname"].ToString());
            }
        }
        else
        {
            // no login data present
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }
}
