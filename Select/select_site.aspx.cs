using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_site : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 20;
        if (!IsPostBack)
        {
            GetData();
        }
    }

    //protected void BtnStartSearch_Click(object sender, EventArgs e)
    //{
    //    lb_msg.Text = "";
    //    GetData();
    //}

    private void GetData()
    {
        string sql = @"select * from qad_si_mstr where si_domain='" + Request.QueryString["domain"] + "' order by si_site";// and si_site like '%" + txt_si_site.Text.Trim() + "%' 
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
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
        string ls_site = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        
        string temp = @"<script>parent.setvalue_site('" + ls_site + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";
        Response.Write(temp.Trim());
    }
}