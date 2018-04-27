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

public partial class select_product : System.Web.UI.Page
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
        //if(txt_ljh.Text=="" && txt_ljh.Text=="" && txt_sales_name.Text == "")
        // {
        //     lb_msg.Text = "请至少输入一个条件!";
        // }
        //else
        // { 

            DataTable dt = DbHelperSQL.Query("exec PGI_Base_Product_Select '"+this.txtpgi_no.Text.Trim()+"','"+this.txtpn.Text.Trim()+"','"+this.txtproduct_user.Text.Trim()+"'").Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();
         
        //}
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
        string lspgi_no = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        string lspn = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        string lspn_desc = GridView1.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
        string lsdomain = GridView1.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
        string lsproduct_user = GridView1.SelectedRow.Cells[4].Text.Trim().Replace("&nbsp;", "");
        string lsproduct_dept = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string lsstatus = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string lscailiao = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");
        string lsnyl = GridView1.SelectedRow.Cells[8].Text.Trim().Replace("&nbsp;", "");
        string lsline = GridView1.SelectedRow.Cells[9].Text.Trim().Replace("&nbsp;", "");
        string lsver = GridView1.SelectedRow.Cells[10].Text.Trim().Replace("&nbsp;", "");

        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        string temp = "<script>window.opener.setvalue_product('" + lspgi_no + "','" + lspn + "','" + lspn_desc + "','" + lsdomain + "','" + lsproduct_user + "','" + lsproduct_dept + "' ,'" + lsstatus + "','" + lscailiao + "','" + lsnyl + "','" + lsline + "','" + lsver + "');</script>";
        Response.Write(temp.Trim());
    }
}