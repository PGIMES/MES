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
public partial class select_XMLJ : System.Web.UI.Page
{
    YJ_CLASS YJ_CLASS = new YJ_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // GetData();
        }

    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        GetData();
    }
   
    private void GetData()
    {
       // string sql = "SELECT * FROM [MES].[dbo].[V_form1_ljh] WHERE DebtorCode IS not null and pt_part+cp_cust_part+ljmc like '%"+txtKeyWords.Text+"%'";
       string sql= "select pt_part XMH,pt_desc1 LJH,pt_desc2 ljmc  from 		qad_pt_mstr  where left(pt_part,1)='P' and pt_prod_line<>4010   and  pt_part+pt_desc1+pt_desc2 like '%"+txtKeyWords.Text+"%'";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            
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
        string keyValue1 = GridView1.SelectedRow.Cells[0].Text.Trim();
        string keyValue2 = GridView1.SelectedRow.Cells[1].Text.Trim();
        string keyValue3 = GridView1.SelectedRow.Cells[2].Text.Trim();
        
        string temp = "<script>window.opener.setvalue('ProdProject','" + keyValue1 + "','LJH','" + keyValue2 + "','LJName','" + keyValue3 + "');</script>";       
       
        Response.Write(temp.Trim());
    }
}