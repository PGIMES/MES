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
        if (this.gv_xx_wo_mstr.IsCallback == true || this.gv_tr_hist.IsCallback == true || this.gv_workorder.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        string sql = "";
        if (Request.QueryString["typedesc"].ToString().Contains("实际发货数量"))
        {
            string sql_date = @"select distinct DATEADD(dd,-1,week_startdate) week_startdate,DATEADD(dd,-1,week_enddate)  week_enddate
                               from[dbo].[Work_Calendar] where [year]='{0}' and  [week]='{1}'";
            sql_date = string.Format(sql_date, Request.QueryString["year"], Request.QueryString["week"]);
            DataTable dt_date = DbHelperSQL.Query(sql_date).Tables[0];

            sql = @"select *
                    from qad.dbo.qad_tr_hist a
                    where a.tr_type in('ISS-SO','ISS-TR') and tr_loc='9060' and a.tr_part like 'P0%'
	                     and a.tr_domain='{0}' and tr_date>='{1}' and tr_date<'{2}'
                    order by tr_trnbr";

            sql = string.Format(sql, Request.QueryString["dept_str"], dt_date.Rows[0]["week_startdate"], dt_date.Rows[0]["week_enddate"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_tr_hist.Visible = true;

            gv_tr_hist.DataSource = dt;
            gv_tr_hist.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "计划生产订单数量")
        {
            sql = @"select *
                        ,case scx_workshop when '二车间' then kaishi_qty/1.5 else kaishi_qty end as jihua_qty
                        ,kaishi_qty-touchan_qty as chayi_qty 
                        ,ROW_NUMBER() OVER (ORDER BY xxwo_nbr) rownum
                    from Planning_xx_wo_mstr
                    where years='{0}' and weeks='{1}' and scx_workshop='{2}'
                    order by xxwo_nbr,xxwo_part";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_xx_wo_mstr.Visible = true;

            gv_xx_wo_mstr.DataSource = dt;
            gv_xx_wo_mstr.DataBind();

            if (Request.QueryString["dept_str"].ToString() != "二车间")
            {
                gv_xx_wo_mstr.Columns["jihua_qty"].Visible = false;
            }
            else
            {
                gv_xx_wo_mstr.Columns["jihua_qty"].Visible = true;
            }
            if (Request.QueryString["dept_str"].ToString() != "三车间")
            {
                gv_xx_wo_mstr.Columns["ps_qty_per"].Visible = false;
                gv_xx_wo_mstr.Columns["ps_qty_per_qty"].Visible = false;
            }
            else
            {
                gv_xx_wo_mstr.Columns["ps_qty_per"].Visible = true;
                gv_xx_wo_mstr.Columns["ps_qty_per_qty"].Visible = true;
            }
        }
        if (Request.QueryString["typedesc"].ToString().Contains("未完成订单数量") || Request.QueryString["typedesc"].ToString().Contains("废品数量"))
        {
            string sql_date = @"select distinct DATEADD(dd,-1,week_startdate) week_startdate,DATEADD(dd,-1,week_enddate)  week_enddate
                               from[dbo].[Work_Calendar] where [year]='{0}' and  [week]='{1}'";
            sql_date = string.Format(sql_date, Request.QueryString["year"], Request.QueryString["week"]);
            DataTable dt_date = DbHelperSQL.Query(sql_date).Tables[0];

            sql = @"select *
                         ,case xxwo__chr01 when '0' then '量产' when '1' then '试制' when '2' then '隔离'  WHEN '3' THEN '挑选'  WHEN '4' THEN '重工' end xxwo__chr01_desc
                        ,ROW_NUMBER() OVER (ORDER BY workorder) rownum
                    from Planning_workorder
                    where years = '{0}' and weeks = '{1}' and workshop = '{2}'
                    order by workorder,pgino";

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
    protected void gv_workorder_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}