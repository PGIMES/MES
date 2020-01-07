using System;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.UI;
using System.Web.Services;

public partial class Wuliu_WLYF : System.Web.UI.Page
{
    public string UserId = "";
    public string UserName = "";
    public string DeptName = "";
    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        UserId = LogUserModel.UserId;
        UserName = LogUserModel.UserName;
        DeptName = LogUserModel.DepartName;

        if (!IsPostBack)
        {   //初始化日期

            DateTime DateNow = DateTime.Now.AddMonths(-1);
            DateTime DateBegin = new DateTime(DateNow.Year, DateNow.Month, 1);
            DateTime DateEnd = DateBegin.AddMonths(1).AddDays(-1);

            txtDateFrom.Text = DateBegin.ToString("yyyy-MM-dd");
            txtDateTo.Text = DateEnd.ToString("yyyy-MM-dd");
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
        DataTable dt = DbHelperSQL.Query("exec qad.dbo.PGI_QAD_WLYF_NEW  '" + ddl_comp.SelectedValue + "','" + txtDateFrom.Text + "','" + txtDateTo.Text + "','" + ddl_status.SelectedValue + "'").Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("物流运费" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    [WebMethod]
    public static string deal(string ids)
    {
        string re_flag = "";

        string sql = @"update WLYF_upload set status='已付款',sure_date=getdate() where id in(" + ids + ")";
        int i = DbHelperSQL.ExecuteSql(sql);
        if (i > 0)
        {
            re_flag = "确认成功！";
        }
        else
        {
            re_flag = "确认失败！";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }

}