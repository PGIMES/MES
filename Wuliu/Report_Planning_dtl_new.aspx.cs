using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Report_Planning_dtl_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_year.Text = Request.QueryString["year"]; lbl_week.Text = 'W' + Request.QueryString["week"]; lbl_dept.Text = Request.QueryString["dept"];

            QueryASPxGridView();
        }
        if (this.gv_xx_wo_mstr.IsCallback == true || this.gv_tr_hist.IsCallback == true || this.gv_workorder.IsCallback == true || this.gv_main.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        string sql = "";
        if (Request.QueryString["typedesc"].ToString().Contains("实际发货数量"))
        {
            /*string sql_date = @"select distinct DATEADD(dd,-1,week_startdate) week_startdate,DATEADD(dd,-1,week_enddate)  week_enddate
                               from[dbo].[Work_Calendar] where [year]='{0}' and  [week]='{1}'";
            sql_date = string.Format(sql_date, Request.QueryString["year"], Request.QueryString["week"]);
            DataTable dt_date = DbHelperSQL.Query(sql_date).Tables[0];

            sql = @"
                    select *
                    from qad.dbo.qad_tr_hist a
                        inner join qad.dbo.qad_pt_mstr b on a.tr_part=b.pt_part and a.tr_domain=b.pt_domain
                    where a.tr_type in('ISS-SO','ISS-TR') and tr_loc='9060' and a.tr_part like 'P0%'
	                        and tr_date>='{1}' and tr_date<'{2}' and b.scx_workshop='{0}'                       
                    order by tr_trnbr";

            sql = string.Format(sql, Request.QueryString["dept_str"], dt_date.Rows[0]["week_startdate"], dt_date.Rows[0]["week_enddate"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            */
            sql = "exec [Report_Planning_dtl_show_New] 0,'{0}','{1}','{2}'";
            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_tr_hist.Visible = true;

            gv_tr_hist.DataSource = dt;
            gv_tr_hist.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString().Contains("计划生产订单数量"))//Request.QueryString["typedesc"].ToString() == "计划生产订单数量"
        {
            /*sql = @"select *
                        ,case scx_workshop when '二车间' then kaishi_qty/1.5 else kaishi_qty end as jihua_qty
                        ,case scx_workshop when '二车间' then kaishi_qty/1.5-chaifei_qty else kaishi_qty-touchan_qty end as chayi_qty 
                        ,ROW_NUMBER() OVER (ORDER BY xxwo_nbr) rownum
                    from Planning_xx_wo_mstr
                    where years='{0}' and weeks='{1}' and scx_workshop='{2}'
                    order by xxwo_nbr,xxwo_part";

                sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            */
            sql = "exec [Report_Planning_dtl_show_New] 1,'{0}','{1}','{2}'";
            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            if (Request.QueryString["dept_str"].ToString() == "三车间" || (Request.QueryString["year"].ToString() == "2019" && Convert.ToInt32(Request.QueryString["week"]) <= 30))
            {

                gv_xx_wo_mstr.Visible = true;

                gv_xx_wo_mstr.DataSource = dt;
                gv_xx_wo_mstr.DataBind();

                if (Request.QueryString["dept_str"].ToString() == "二车间" || Request.QueryString["dept_str"].ToString() == "四车间")
                {
                    gv_xx_wo_mstr.Columns["jihua_qty"].Visible = true;
                }
                else
                {
                    gv_xx_wo_mstr.Columns["jihua_qty"].Visible = false;
                }
                if (Request.QueryString["dept_str"].ToString() != "三车间")
                {
                    gv_xx_wo_mstr.Columns["ps_qty_per"].Visible = false;
                    gv_xx_wo_mstr.Columns["ps_qty_per_qty"].Visible = false;
                    gv_xx_wo_mstr.Columns["sydept"].Visible = false;
                }
                else
                {
                    gv_xx_wo_mstr.Columns["ps_qty_per"].Visible = true;
                    gv_xx_wo_mstr.Columns["ps_qty_per_qty"].Visible = true;
                    gv_xx_wo_mstr.Columns["sydept"].Visible = true;
                }
            }
            else
            {
                gv_main.Visible = true;

                gv_main.DataSource = dt;
                gv_main.DataBind();
            }

        }
        if (Request.QueryString["typedesc"].ToString().Contains("未完成订单数量") || Request.QueryString["typedesc"].ToString().Contains("废品数量"))
        {
            /*string sql_date = @"select distinct DATEADD(dd,-1,week_startdate) week_startdate,DATEADD(dd,-1,week_enddate)  week_enddate
                               from[dbo].[Work_Calendar] where [year]='{0}' and  [week]='{1}'";
            sql_date = string.Format(sql_date, Request.QueryString["year"], Request.QueryString["week"]);
            DataTable dt_date = DbHelperSQL.Query(sql_date).Tables[0];

            sql = @"select *
                         ,case xxwo__chr01 when '0' then '量产' when '1' then '试制' when '2' then '隔离'  WHEN '3' THEN '挑选'  WHEN '4' THEN '重工' end xxwo__chr01_desc
                        ,ROW_NUMBER() OVER (ORDER BY workorder) rownum
                        ,case when touchan_qty=0 then 0 
                            when touchan_qty>0 then case workshop when '三车间' then touchan_qty-yiwan_qty else dingdan_qty-yiwan_qty end
                            end  as sque_qty2
                    from Planning_workorder
                    where years = '{0}' and weeks = '{1}' and workshop = '{2}'";
            if (Request.QueryString["typedesc"].ToString().Contains("未完成订单数量"))
            {
                sql = sql + " and xxwo_status='P'";
            }
            sql = sql + " order by workorder,pgino";
            */
            if (Request.QueryString["typedesc"].ToString().Contains("废品数量"))
            {
                sql = "exec [Report_Planning_dtl_show_New] 2,'{0}','{1}','{2}'";
            }
            if (Request.QueryString["typedesc"].ToString().Contains("未完成订单数量"))
            {
                sql = "exec [Report_Planning_dtl_show_New] 3,'{0}','{1}','{2}'";
            }
            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder.Visible = true;

            gv_workorder.DataSource = dt;
            gv_workorder.DataBind();
        }
    }


    protected void gv_tr_hist_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_xx_wo_mstr_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_main_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void btnimport_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        if (Request.QueryString["typedesc"].ToString().Contains("实际发货数量"))
        {
            gv_tr_hist.ExportXlsxToResponse(System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        if (Request.QueryString["typedesc"].ToString().Contains("计划生产订单数量"))
        {
            if (Request.QueryString["dept_str"].ToString() == "三车间" || (Request.QueryString["year"].ToString() == "2019" && Convert.ToInt32(Request.QueryString["week"]) <= 30))
            {
                gv_xx_wo_mstr.ExportXlsxToResponse(System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            }
            else
            {
                gv_main.ExportXlsxToResponse(System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
            }
            
        }
        if (Request.QueryString["typedesc"].ToString().Contains("未完成订单数量") || Request.QueryString["typedesc"].ToString().Contains("废品数量"))
        {
            gv_workorder.ExportXlsxToResponse(System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
    }
}