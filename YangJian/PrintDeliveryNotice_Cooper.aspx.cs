using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

public partial  class PrintDeliveryNotice_Cooper : System.Web.UI.Page
{  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Request["requestid"]!="")
            {
                ObjectDataSource1.SelectParameters[0].DefaultValue = Request["requestid"];
              
            }
        }
      

    }

}