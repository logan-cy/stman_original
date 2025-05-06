using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Help_Help : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void tvNavigation_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tvNavigation.SelectedNode.ChildNodes.Count > 0 && tvNavigation.SelectedNode.Expanded == false)
        {
            tvNavigation.SelectedNode.Expand();
            tvNavigation.SelectedNode.Expanded = true;
        }
        else if (tvNavigation.SelectedNode.Expanded == true)
        {
            tvNavigation.SelectedNode.Collapse();
            tvNavigation.SelectedNode.Expanded = false;
        }
    }
}
