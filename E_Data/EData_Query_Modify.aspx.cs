using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class E_Data_EData_Query_Modify : System.Web.UI.Page
{
   
        protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            MES.DAL.MES_E_Data dal=new MES.DAL.MES_E_Data();
            DataTable dt = dal.EData_Query_Modify(Request["deviceid"], txt_startdate.Text, txt_enddate.Text);
            GridView1.DataSource = dt;
            GridView1.DataBind();           
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        MES.DAL.MES_E_Data dal = new MES.DAL.MES_E_Data();
        DataTable dt = dal.EData_Query_Modify(Request["deviceid"], txt_startdate.Text, txt_enddate.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MES.DAL.MES_E_Data dal = new MES.DAL.MES_E_Data();
        DataTable dt = dal.EData_Query_Modify(Request["deviceid"], txt_startdate.Text, txt_enddate.Text); 
        GridView1.DataSource = dt;
        GridView1.DataBind();
       
    }
    
   
}