using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Help_additional_support : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        bool result = sendMail();

        divIntro.Visible = false;
        divThankYou.Visible = true;

        if (result)
        {
            lblSuccess.Visible = true;
            lblError.Visible = false;
        }
        else
        {
            lblSuccess.Visible = false;
            lblError.Visible = true;
        }
    }

    private bool sendMail()
    {
        try
        {
            bool result = false;
            string companyName = String.Empty;

            using (MailMessage m = new MailMessage())
            {
                m.To.Add(new MailAddress("lyoung@loganyoung.za.net"));
                m.From = new MailAddress(txtEmail.Text);

                companyName = (string)Session["companyname"];

                switch (ddlSubject.SelectedItem.Value)
                {
                    case "support":
                        m.Subject = String.Format("{0}: SecMan Support Query", companyName);
                        break;
                    case "newFeature":
                        m.Subject = String.Format("{0}: New SecMan Feature Request", companyName);
                        break;
                    case "changeFeature":
                        m.Subject = String.Format("{0}: SecMan Feature Change Request", companyName);
                        break;
                    case "other":
                        m.Subject = String.Format("{0}: SecMan General Query", companyName);
                        break;
                }

                m.Body = txtMessage.Text;

                using (SmtpClient smtp = new SmtpClient("smtp.saix.co.za", 25))
                {
                    smtp.Send(m);
                    result = true;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtName.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtMessage.Text = String.Empty;
        ddlSubject.ClearSelection();
    }
}