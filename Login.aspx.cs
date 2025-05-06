using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.Params["action"]) && Request.Params["action"] == "logout")
        {
            Session.Clear();
            //FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            Page.Validate("Login");

            if (!Page.IsValid)
            {
                return;
            }

            processLogin();
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.ToString();
        }
    }

    private void processLogin()
    {
        try
        {
            LoginContext log = new LoginContext();
            log.SetUser(txtEmail.Text, txtPassword.Text);

            if (log.DoLogin())
            {
                if (chkCookie.Checked)
                {
                    HttpCookie cookie = createPersistantCookie(txtEmail.Text, 7);
                    Response.Cookies.Clear();
                    Response.Cookies.Add(cookie);
                }

                //FormsAuthentication.SetAuthCookie(txtEmail.Text, true);
                //FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
                Response.Redirect("~/Members/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            lblerr.Text = ex.ToString();
        }

    }

    private HttpCookie createPersistantCookie(string Username, int PersistDays = 0)
    {
        HttpCookie cookie = new HttpCookie("stman");
        if (PersistDays != 0)
        {
            cookie.Expires = DateTime.Now.AddDays(PersistDays);
        }

        cookie["user"] = Username;

        return cookie;
    }
}