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

public partial class Select_select_shdz : System.Web.UI.Page
{
    YJ_CLASS YJ_CLASS = new YJ_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 20;
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
       if(txt_shdz.Text=="" && txt_shxx.Text=="")
        {

        }
       else
        { 
            DataTable dt = new DataTable();
           
            dt = YJ_CLASS.Getshdz(txt_shdz.Text,txt_shxx.Text);
          
            if (dt == null || dt.Rows.Count <= 0)
            {
                lb_msg.Text = "No Data Found!";
                //return;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
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
        //string xmh = GridView1.SelectedRow.Cells[0].Text.Trim();
        //string ljh = GridView1.SelectedRow.Cells[1].Text.Trim();
        //string gxh = GridView1.SelectedRow.Cells[3].Text.Trim();
        string requestid = GridView1.SelectedRow.Cells[0].Text.Trim();
        string shdz = GridView1.SelectedRow.Cells[1].Text.Trim();
        string shrxx = GridView1.SelectedRow.Cells[2].Text.Trim();

        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        string temp = "<script>window.opener.shdz_setvalue('" + shrxx + "','" + shdz + "','" + requestid + "');</script>";
        Response.Write(temp.Trim());
    }
}