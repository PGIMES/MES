using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YangJian_PintFordDomesticPacking_CAF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters[0].DefaultValue = Request["requestid"];
    }
}