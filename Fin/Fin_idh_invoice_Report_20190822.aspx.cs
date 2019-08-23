using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Fin_Fin_idh_invoice_Report_20190822 : System.Web.UI.Page
{
    //public string UserId = "";
    //public string UserName = "";
    //public string DeptName = "";
    //LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        //UserId = LogUserModel.UserId;
        //UserName = LogUserModel.UserName;
        //DeptName = LogUserModel.DepartName;

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
        string sql = @"exec Report_idh_invoice '{0}','{1}','{2}','{3}'";
        sql = string.Format(sql, ddl_comp.SelectedValue, StartDate.Text,EndDate.Text, ddl_status.SelectedValue);

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("国内客户开票通知_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }



}