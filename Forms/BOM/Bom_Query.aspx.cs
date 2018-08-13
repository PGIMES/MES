using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web.ASPxTreeList;

public partial class Bom_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
            QueryASPxGridView();        
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(" select  d.aplno, id, (case when pid is null then d.pt_part+'【'+d.bomver+'】' else d.pt_part end) pt_part, d.pt_desc1, d.pt_desc2, drawno, pt.pt_net_wt, ps_qty_per, (case when isnull(unit,'')<>'' then unit when  cast(ps_qty_per as float)  in(1.0,2.0,3.0,4.0) then 'EA' else 'KG' end)unit, material, vendor, ps_op, note, pid,d.domain,pt_status,p.product_user,p.bz_user from eng_bom_dtl d     ");
        sb.Append(" left join form3_Sale_Product_MainTable p on left(d.pt_part,5)=p.pgino and d.pid is null ");
        sb.Append(" left join qad_pt_mstr pt on d.pt_part=pt.pt_part and d.domain=pt.pt_domain and d.pt_part not like 'R%'");
        sb.Append(" join  (select aplno,bomver from eng_bom_main_Form where (domain = '"+domain.SelectedValue+"' or '"+ domain.SelectedValue + "' = '') and(pgino like '%"+pgino.Text.Trim()+"%' or '"+pgino.Text.Trim()+ "' = ''))t on d.aplno = t.aplno order by d.pt_part ");

        //sb.Append("  select    d.aplno, id, (case when pid is null then d.pt_part+'【'+d.bomver+'】' else d.pt_part end) pt_part, pt_desc1, pt_desc2, drawno, pt_net_wt, ps_qty_per, unit, material, vendor, ps_op, note, pid from eng_bom_dtl d left join  ");
        //sb.Append("  (select aplno,bomver from eng_bom_main_Form where (domain = '" + domain.SelectedValue + "' or '" + domain.SelectedValue + "' = '') and(pgino like '%" + pgino.Text.Trim() + "%' or '" + pgino.Text.Trim() + "' = '')) t on d.aplno = t.aplno order by d.pt_part");


        DataTable dt = DbHelperSQL.Query(sb.ToString()).Tables[0];

        bomtree.DataSource = dt;
        bomtree.DataBind();

    }
   
    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
      //  ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }
    protected void bomtree_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        TreeListNode node = this.bomtree.FindNodeByKeyValue(e.NodeKey.ToString());
        if (Object.Equals(e.GetValue("pid"), ""))
        {
            // e.Cell.Font.Bold = true;
            e.Cell.Text= "";
        }

    }


    protected void bomtree_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        TreeListNode node = this.bomtree.FindNodeByKeyValue(e.NodeKey.ToString());
        if (Object.Equals(node["pid"].ToString(), ""))
        {
            // e.Cell.Font.Bold = true;
            e.Row.Cells[1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=4A901BC7-EA83-43B1-80B6-5B14708DEDE9&instanceid="+ node["aplno"].ToString() + "&display=1' target='_blank'>" + node["pt_part"].ToString() + "</a>";
        }
        //e.Row.Cells[1].Text = "<a href=''>" + node["pt_part"].ToString() + "</a>";
    }

  
}