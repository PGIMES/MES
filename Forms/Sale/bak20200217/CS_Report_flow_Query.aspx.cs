using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Sale_CS_Report_flow_Query : System.Web.UI.Page
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
        string sql = @"exec Report_CS_flow '{0}','{1}','{2}'";
        sql = string.Format(sql, txt_wlh.Text, txt_cust_part.Text, ddl_ver.SelectedValue);

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("客户日程单流程状态查询_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }
}