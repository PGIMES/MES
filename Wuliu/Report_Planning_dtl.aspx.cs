using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Report_Planning_dtl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_year.Text = Request.QueryString["year"]; lbl_week.Text = 'W' + Request.QueryString["week"]; lbl_dept.Text = Request.QueryString["dept"];

            QueryASPxGridView();
        }
        if (this.gv_xx_wo_mstr.IsCallback == true || this.gv_workorder_touchan.IsCallback == true 
            || this.gv_workorder_ruku.IsCallback == true || this.gv_workorder_feipin.IsCallback == true
            || this.gv_workorder_shangque.IsCallback == true || this.gv_workorder_GP.IsCallback == true
            || this.gv_workorder_N_GP.IsCallback == true || this.gv_tr_hist.IsCallback == true)//页面搜索条件使用
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
            sql = @"select * from Planning_xx_wo_mstr_dept
                    where years='{0}' and weeks='{1}' and scx_workshop='{2}'
                    order by xxwo_nbr";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_xx_wo_mstr.Visible = true;

            gv_xx_wo_mstr.DataSource = dt;
            gv_xx_wo_mstr.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "投产数量")
        {
            sql = @"select * from Planning_workorder_touchan
                    where years='{0}' and weeks='{1}' and workshop='{2}'";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_touchan.Visible = true;

            gv_workorder_touchan.DataSource = dt;
            gv_workorder_touchan.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "入库数量")
        {
            sql = @"select * from Planning_workorder_ruku
                    where years='{0}' and weeks='{1}' and workshop='{2}'";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_ruku.Visible = true;

            gv_workorder_ruku.DataSource = dt;
            gv_workorder_ruku.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "废品数量")
        {
            sql = @"select * from Planning_workorder_feipin
                    where years='{0}' and weeks='{1}' and workshop='{2}'";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_feipin.Visible = true;

            gv_workorder_feipin.DataSource = dt;
            gv_workorder_feipin.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "未完成订单数量")
        {
            sql = @"select * from Planning_workorder_shangque
                    where years='{0}' and weeks='{1}' and workshop='{2}' order by workorder";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_shangque.Visible = true;

            gv_workorder_shangque.DataSource = dt;
            gv_workorder_shangque.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "待检验")
        {
            sql = @"select * from Planning_workorder_GP
                    where years='{0}' and weeks='{1}' and workshop='{2}' order by workorder";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_GP.Visible = true;

            gv_workorder_GP.DataSource = dt;
            gv_workorder_GP.DataBind();
        }
        if (Request.QueryString["typedesc"].ToString() == "生产-机加工" || Request.QueryString["typedesc"].ToString() == "生产-压铸")
        {
            sql = @"select * from Planning_workorder_N_GP
                    where years='{0}' and weeks='{1}' and workshop='{2}' order by workorder";

            sql = string.Format(sql, Request.QueryString["year"], Request.QueryString["week"], Request.QueryString["dept_str"]);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            gv_workorder_N_GP.Visible = true;

            gv_workorder_N_GP.DataSource = dt;
            gv_workorder_N_GP.DataBind();
        }
    }

    protected void gv_xx_wo_mstr_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_workorder_touchan_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_ruku_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_feipin_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_shangque_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_GP_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_workorder_N_GP_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_tr_hist_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}