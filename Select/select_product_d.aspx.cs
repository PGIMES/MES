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
        
        //string sql = @"select aa.pt_part,aa.pt_desc1,aa.pt_desc2,aa.pt_status,aa.pt_prod_line
        //            from qad_pt_mstr aa
        //                left join (                                
        //                        select pgi_no from [dbo].[PGI_GYLX_Dtl]
        //                        union
        //                        select b.pgi_no
        //                        from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID = 'ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0, 1))  a
        //                               inner join PGI_GYLX_Dtl_Form b on a.InstanceID = b.gygsno
        //                            ) bb on aa.pt_part=bb.pgi_no
        //            where bb.pgi_no is null 
        //                and (aa.pt_prod_line like '2%' or aa.pt_prod_line like '3%') and (aa.pt_status<>'DEAD' and aa.pt_status<>'OBS') and aa.pt_domain='{0}' 
        //                and aa.pt_part like '%{1}%' and aa.pt_desc1 like '%{2}%'
        //            order by aa.pt_part";

        string sql_go = @"select pgi_no from [dbo].[PGI_GYLX_Dtl]
                        union
                        select b.pgi_no
                        from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID = 'ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0, 1))  a
                                inner join PGI_GYLX_Dtl_Form b on a.InstanceID = b.gygsno";

        if (Request["formno"] != "") { sql_go = sql_go + " where a.InstanceID<>'" + Request["formno"] + "'"; }

        string sql = @"select aa.pt_part,aa.pt_desc1,aa.pt_desc2,aa.pt_status,aa.pt_prod_line
                    from qad_pt_mstr aa
                        left join ({0}) bb on aa.pt_part=bb.pgi_no
                    where bb.pgi_no is null 
                        and (aa.pt_prod_line like '2%' or aa.pt_prod_line like '3%') and (aa.pt_status<>'DEAD' and aa.pt_status<>'OBS') and aa.pt_domain='{1}' 
                        and aa.pt_part like '%{2}%' and aa.pt_desc1 like '%{3}%'
                    order by aa.pt_part";
        sql = string.Format(sql,sql_go, Request["domain"], this.txtpgi_no.Text.Trim(), this.txtpn.Text.Trim());

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