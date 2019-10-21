using DevExpress.Data;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Report_so : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddl_year.SelectedValue = DateTime.Now.AddMonths(-1).ToString("yyyy"); //获取上月年份
            //ddl_month.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM"); //获取上月月份

        }
        QueryASPxGridView();
    }
    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec [Report_so] 0,'{0}'";
        sql = string.Format(sql, ddl_comp.SelectedValue);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }

    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("客户日程单异常报表_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
       
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }


}