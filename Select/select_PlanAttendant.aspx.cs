using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Maticsoft.DBUtility;

public partial class select_PlanAttendant : System.Web.UI.Page
{
    Function_Base Function_Base = new Function_Base();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }

    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        GetData();
    }
   
    private void GetData()
    {
      
        DataTable dt = new DataTable();
        dt = DbHelperSQL.Query("select workcode,lastname,dept_name,ROW_NUMBER() OVER (ORDER BY workcode) numid  from [dbo].[HRM_EMP_MES] where workcode + lastname+ dept_name like '%" + this.txtKeywords.Text+"%' and workcode<>'"+ Request["ApplyId"] + "'").Tables[0];
        if (dt == null || dt.Rows.Count <= 0)
        {
            lb_msg.Text = "No Data Found!";
        }
        gv.DataSource = dt;
        gv.DataBind();

    }

    
    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string ctrl0 = Request["ctrl0"];
    //    string ctrl1 = Request["ctrl1"];
    //    string ctrl2 = Request["ctrl2"];
    //    string keyValue0 = GridView1.SelectedRow.Cells[0].Text.Trim();
    //    string keyValue1=  GridView1.SelectedRow.Cells[1].Text.Trim();
    //    string keyValue2 = GridView1.SelectedRow.Cells[2].Text.Trim();

    //    string temp = @"<script>parent.setvalue_PlanAttendant('" + ctrl0 + "','" + keyValue0 + "','" + ctrl1 + "','" + keyValue1 + "','" + ctrl2 + "','" 
    //        + keyValue2 + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";

    //    Response.Write(temp.Trim());
    //}
}