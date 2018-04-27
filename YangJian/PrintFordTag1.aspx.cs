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

public partial  class PrintFordTag1 : System.Web.UI.Page
{  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = DbHelperSQL.Query("exec print_form1_Sale_YJ_FordTag1 '" + Request["requestid"] + "','"+ Request["PrintCount"] + "'");
           //     ObjectDataSource1.SelectParameters[0].DefaultValue = Request["requestid"];
           //     ObjectDataSource1.SelectParameters[1].DefaultValue = Request["PrintCount"];
           this.Repeater1.DataSource = ds;
            this.Repeater1.DataBind();
        }
       
       


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
       // ReportDocument sReport = new ReportDocument();
     
    } 
}