using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;

public partial class Select_select_pgino : System.Web.UI.Page
{
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
       
        string sql = @"
                            select b.pgino,b.productcode,a.productname,case a.make_factory when '上海工厂' then '100' when '昆山工厂' then '200' else '' end make_factory,b.version
                            ,a.bz_user,a.product_leibie,caigou   from form3_Sale_Product_MainTable a
                            left join form3_Sale_Product_DetailTable b on a.pgino=b.pgino                       
                    where b.pgino like '%{0}%' and b.productcode like '%{1}%'       
                    order by b.pgino,b.version";

        sql = string.Format(sql, this.txtpgi_no.Text.Trim(), this.txtpn.Text.Trim());
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
        string lsproductcode = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        string lsproductname = GridView1.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
        string lsdl = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string bz = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string caigou = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");
        string temp = @"<script>parent.setvalue_product('" + lspgino + "','" + lsproductcode + "','" + lsproductname + "','"+lsdl+"','"+bz+"','"+caigou+"'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";


        Response.Write(temp.Trim());
    }
}