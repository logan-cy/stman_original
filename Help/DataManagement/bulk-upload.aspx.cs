using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Help_DataManagement_bulk_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBusinesses();
        }
    }

    private void loadBusinesses()
    {
        Business bus = new Business(0);

        gvBusinesses.DataSource = bus.GetBusinessData();
        gvBusinesses.DataBind();
    }
}