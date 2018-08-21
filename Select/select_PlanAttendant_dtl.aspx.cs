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

public partial class select_PlanAttendant_dtl : System.Web.UI.Page
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
        string pa =Request["PA"];
        string[] sArray = pa.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        DataTable dt = new DataTable();
        dt.Columns.Add("workcode", typeof(string)); dt.Columns.Add("workname", typeof(string));

        for (int i = 0; i < sArray.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr["workcode"] = sArray[i].Substring(0, sArray[i].IndexOf('('));
            dr["workname"] = sArray[i].Substring(sArray[i].IndexOf('(') + 1, sArray[i].Length - sArray[i].IndexOf('(') - 2);
            dt.Rows.Add(dr);
        }

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
        string workcode = GridView1.SelectedRow.Cells[0].Text.Trim();
        string workname =  GridView1.SelectedRow.Cells[1].Text.Trim();

        string temp = @"<script>parent.setvalue_Traveler('" + Request["vi"] + "','" + workcode + "','" + workname + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";

        Response.Write(temp.Trim());
    }
}