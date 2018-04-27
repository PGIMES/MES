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
using Maticsoft.DBUtility;

public partial  class PrintDaimler_OutBoxLabel : System.Web.UI.Page
{  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ObjectDataSource1.SelectParameters[0].DefaultValue = Request["requestid"];
            //ObjectDataSource1.SelectParameters[1].DefaultValue = Request["type"];

        }       
    }
     
    protected void Page_Unload(object sender, EventArgs e)
    {
       // ReportDocument sReport = new ReportDocument();
     
    } 
}