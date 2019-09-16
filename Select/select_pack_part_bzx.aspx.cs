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

public partial class select_pack_part_bzx : System.Web.UI.Page
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
        string sql = @"exec [z_select_pack_part_bzx] '" + txtPart .Text+ "'"; 
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
        string domain = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        string part = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        string gg = GridView1.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
        string cc = GridView1.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
        string zl = GridView1.SelectedRow.Cells[4].Text.Trim().Replace("&nbsp;", "");
        string XC = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string CT = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string XT = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");

        string temp = @"<script>parent.setvalue_bzx_part('" + domain + "','" + part + "','" + gg + "','"
           + cc + "','" + zl + "','" + XC + "','" + CT + "','"
           + XT + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";

        Response.Write(temp.Trim());
    }
}