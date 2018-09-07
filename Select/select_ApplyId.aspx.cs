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

public partial class select_ApplyId : System.Web.UI.Page
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

        if (Request.QueryString["para"].ToString() == "travel")
        {
            GridView1.Columns[8].Visible = false;
        }

        if (Request.QueryString["para"].ToString() == "car")
        {
            GridView1.Columns[8].Visible = true;
        }

        DataTable dt = new DataTable();
        //dt = DbHelperSQL.Query("select  workcode,lastname,dept_name from [dbo].[HRM_EMP_MES] where workcode + lastname+ dept_name like '%" + this.txtKeywords.Text + "%'").Tables[0];

        string sql = @"select a.workcode,a.lastname,a.telephone,a.jobtitlename,a.domain,a.gc,a.dept_name,a.car,b.ITEMVALUE 
                    from V_HRM_EMP_MES a 
	                    left join [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] b on a.workcode=b.employeeid
                    where workcode + lastname+ dept_name like '%" + this.txtKeywords.Text + "%'";
        if (Request.QueryString["para"].ToString() == "car")
        {
            sql = sql + " and isnull(a.car,'')<>''";
        }

        dt = DbHelperSQL.Query(sql).Tables[0];

        if (dt == null || dt.Rows.Count <= 0)
        {
            lb_msg.Text = "No Data Found!";
            //return;
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DFE7DF';");
            e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor=currentcolor;");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string workcode = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        string lastname = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        string ITEMVALUE = GridView1.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
        string dept_name = GridView1.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
        string domain = GridView1.SelectedRow.Cells[4].Text.Trim().Replace("&nbsp;", "");
        string gc = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string jobtitlename = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string telephone = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");
        string car = "";

        if (Request.QueryString["para"].ToString() == "travel")
        {
            string temp = @"<script>parent.setvalue_ApplyId('" + workcode + "','" + lastname + "','" + ITEMVALUE + "','"
               + dept_name + "','" + domain + "','" + gc + "','" + jobtitlename + "','"
               + telephone + "','" + car + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";

            Response.Write(temp.Trim());
        }

        if (Request.QueryString["para"].ToString() == "car")
        {
            car = GridView1.SelectedRow.Cells[8].Text.Trim().Replace("&nbsp;", "");

            string temp = @"<script>parent.setvalue_ApplyId('" + workcode + "','" + lastname + "','" + ITEMVALUE + "','"
               + dept_name + "','" + domain + "','" + gc + "','" + jobtitlename + "','"
               + telephone + "','" + car + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";

            Response.Write(temp.Trim());
        }

       
    }
}