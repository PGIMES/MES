using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Qad_Report_tr_hist_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ddl_year.SelectedValue= DateTime.Now.AddMonths(-1).ToString("yyyy"); //获取上月年份
            ddl_month.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM"); //获取上月月份
            QueryASPxGridView();
        }
        if (this.gv_tr_list.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "set", "setHeight();", true);
        }
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        string curmonth = "";
        if (ddl_condition.SelectedValue == "his")
        {
            curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        }
        DataTable dt = DbHelperSQL.Query("exec [Report_tr_hist_new] '2','" + ddl_comp.SelectedValue 
            + "','" + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" 
            + curmonth + "'").Tables[0];

        gv_tr_list.DataSource = dt;
        gv_tr_list.DataBind();
    }

    protected void gv_tr_list_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_tr_list_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "序号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
    }


    protected void btnimport_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv_tr_list.ExportXlsxToResponse("库龄明细" + System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }
    
}