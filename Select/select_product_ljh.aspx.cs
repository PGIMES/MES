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

public partial class select_product_ljh : System.Web.UI.Page
{
    Product_CLASS Product_CLASS = new Product_CLASS();
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
       if(txt_ljh.Text=="" && txt_ljh.Text=="" && txt_sales_name.Text == "")
        {
            lb_msg.Text = "请至少输入一个条件!";
        }
       else
        { 
        
            DataTable dt = Product_CLASS.Getljh(txt_ljh.Text,txt_lj_name.Text,txt_sales_name.Text);
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
        string ljh = GridView1.SelectedRow.Cells[1].Text.Trim();
        string baojia_no = GridView1.SelectedRow.Cells[0].Text.Trim();

        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        string temp = "<script>window.opener.XMH_setvalue('form1','" + ljh + "','" + baojia_no + "');</script>";
        Response.Write(temp.Trim());
    }
}