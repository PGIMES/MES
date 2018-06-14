using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_product_d : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 20;
        
        if (!IsPostBack)
        {
            txtpgi_no.Text = Request["pgi_no"];
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

        string sql = @"select pt_part,pt_desc1,pt_desc2,pt_status,pt_prod_line
	                    ,pt_domain as make_factory,product_user
	                    ,(select dept_name  from HRM_EMP_MES where workcode=left(product_user,5)) as dept_name
	                    ,(select top 1 cailiao from [PGIHR].report.[dbo].[Track] where 'P0'+right(pgi_no,3)=pgino) as cailiao
	                    ,(select sum(quantity_year) from form3_Sale_ProductQuantity_DetailTable where requestId=form3_Sale_Product_MainTable.requestId) as nyl
	                    ,'A' as ver
                    from qad_pt_mstr a
	                    inner join form3_Sale_Product_MainTable on left(a.pt_part,5)=form3_Sale_Product_MainTable.pgino
                     where pt_prod_line<>'4010' and a.pt_domain='{0}' and a.pt_part like '%{1}%' and a.pt_desc1 like '%{2}%'
                    order by a.pt_part";
        sql = string.Format(sql, Request["domain"], this.txtpgi_no.Text.Trim(), this.txtpn.Text.Trim());
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
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
        string lspgino = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");

        string temp = @"<script>parent.setvalue_product_d('" + lspgino + "','"+Request["vi"]+ "','" + Request.QueryString["ty"] + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";


        Response.Write(temp.Trim());
    }
}