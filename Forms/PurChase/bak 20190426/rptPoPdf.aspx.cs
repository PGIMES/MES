using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class rptPoPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            ObjectDataSource1.SelectParameters[0].DefaultValue = Request["PoNo"];
            ObjectDataSource1.SelectParameters[1].DefaultValue = "main";
            ObjectDataSource2.SelectParameters[0].DefaultValue = Request["PoNo"];
            ObjectDataSource2.SelectParameters[1].DefaultValue = "dtl";
        }
    }
}
