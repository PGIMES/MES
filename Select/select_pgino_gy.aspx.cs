using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_pgino_gy : System.Web.UI.Page
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
        string sql = @"select aa.pt_part,aa.pt_desc1,aa.pt_desc2,aa.pt_status,aa.pt_prod_line,aa.pt_domain
                        ,fpm.zl_user,fpm.yz_user,fpm.product_user 
                    from qad_pt_mstr aa
                        left join form3_Sale_Product_MainTable fpm on left(aa.pt_part,5)=fpm.pgino
                    where (aa.pt_prod_line like '2%' or aa.pt_prod_line like '3%') and (aa.pt_status<>'DEAD' and aa.pt_status<>'OBS')
                        and aa.pt_part like '%{0}%' and aa.pt_desc1 like '%{1}%'
                    order by aa.pt_part";
        sql = string.Format(sql, this.txtpgi_no.Text.Trim(), this.txtpn.Text.Trim());

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
        string lspgino = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        string lsproductcode = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        string lsproductname = GridView1.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
        string lsmake_factory = GridView1.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
        string lsproduct_user = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string lszl_user = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string lsyz_user = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");
        string lsver = "A";

        string temp = @"<script>parent.setvalue_product('" + lspgino + "','" + lsproductcode + "','" + lsproductname + "','" + lsmake_factory + "','" + lsver + "','"
            + lszl_user + "','" + lsyz_user + "','" + lsproduct_user + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);parent.CheckVer();</script>";

        Response.Write(temp.Trim());
    }
}