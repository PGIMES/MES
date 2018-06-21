using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;

public partial class Select_select_product_m : System.Web.UI.Page
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
        //if(txt_ljh.Text=="" && txt_ljh.Text=="" && txt_sales_name.Text == "")
        // {
        //     lb_msg.Text = "请至少输入一个条件!";
        // }
        //else
        // { 

        //string sql = @"select b.pgino,b.productcode,a.productname,case a.make_factory when '上海工厂' then '100' when '昆山工厂' then '200' else '' end make_factory,b.version
        //                ,c.zl_user,c.yz_user,c.product_user 
        //            from form3_Sale_Product_MainTable a
        //                left join form3_Sale_Product_DetailTable b on a.pgino=b.pgino
        //                left join V_Track_product c on a.pgino=c.xmh
        //            where b.pgino like '%{0}%' and b.productcode like '%{1}%'
        //            union all select 'P0056','01336301771','奇瑞B12/B14轴承支架','200','A','01337-王雪斌','01968-孙娟','02432-何桂勤'
        //            union all select 'P0055','01336301772','奇瑞B12/B14轴承支架','200','A','01337-王雪斌','01968-孙娟','02432-何桂勤'
        //            order by b.pgino,b.version";
        //string sql = @"
        //            select aa.pgino, aa.productcode,aa.productname,aa.make_factory,aa.version,aa.zl_user,aa.yz_user,aa.product_user
        //            from(
        //                select b.pgino,b.productcode,a.productname,case a.make_factory when '上海工厂' then '100' when '昆山工厂' then '200' else '' end make_factory,b.version
        //                    ,a.zl_user,a.yz_user,a.product_user 
        //                from form3_Sale_Product_MainTable a
        //                    left join form3_Sale_Product_DetailTable b on a.pgino=b.pgino
        //                ) aa 
        //                    left join (                                
        //                        select projectno from [dbo].[PGI_GYGS_Main]
        //                        union
        //                        select b.projectno
        //                        from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID = 'a7ec8bec-1f81-4a81-81d2-a9c7385dedb7' and status in(0, 1))  a
        //                               inner join PGI_GYGS_Main_Form b on a.InstanceID = b.formno
        //                            ) bb on aa.pgino=bb.projectno
        //            where bb.projectno is null and aa.pgino like '%{0}%' and aa.productcode like '%{1}%'              
        //            order by aa.pgino,aa.version";
        string sql = @"
                        select b.pgino,b.productcode,a.productname,case a.make_factory when '上海工厂' then '100' when '昆山工厂' then '200' else '' end make_factory,b.version
                            ,a.zl_user,a.yz_user,a.product_user 
                        from form3_Sale_Product_MainTable a
                            left join form3_Sale_Product_DetailTable b on a.pgino=b.pgino                        
                    where b.pgino like '%{0}%' and b.productcode like '%{1}%'       
                    order by b.pgino,b.version";

        sql = string.Format(sql,this.txtpgi_no.Text.Trim(), this.txtpn.Text.Trim());
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
        string lsmake_factory = GridView1.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
        string lsver = GridView1.SelectedRow.Cells[4].Text.Trim().Replace("&nbsp;", "");
        string lsproduct_user = GridView1.SelectedRow.Cells[5].Text.Trim().Replace("&nbsp;", "");
        string lszl_user = GridView1.SelectedRow.Cells[6].Text.Trim().Replace("&nbsp;", "");
        string lsyz_user = GridView1.SelectedRow.Cells[7].Text.Trim().Replace("&nbsp;", "");

        string temp = @"<script>parent.setvalue_product('" + lspgino + "','" + lsproductcode + "','" + lsproductname + "','" + lsmake_factory + "','" + lsver + "','"
            + lszl_user + "','" + lsyz_user + "','" + lsproduct_user + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";


        Response.Write(temp.Trim());
    }

}