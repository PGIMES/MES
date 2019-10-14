using DevExpress.Export;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pur_AcceptPool : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // StartDate.Text = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
           // EndDate.Text = System.DateTime.Now.ToShortDateString();
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec Pur_RCT_AcceptPool '" + this.keyword.Text.Trim() + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();

         
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("验收池查询_" +  System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        //ASPxGridViewExporter1.WriteXlsToResponse("应付类合同执行进度浏览_" + ddl_domain.SelectedItem.Text + "_" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    protected void gv_CustomCellMerge(object sender, ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "域"  || e.Column.Name == "请购单号" || e.Column.FieldName == "请购行号" //|| e.Column.FieldName == "ApplyDept" || e.Column.FieldName == "ApplyDomainName"
                                                                                                  // || e.Column.FieldName == "GoDays" || e.Column.FieldName == "GoSatus" || e.Column.FieldName == "ApproveDate"
             )
        {
            var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "请购单号");
            var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "请购单号");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }
    }
}