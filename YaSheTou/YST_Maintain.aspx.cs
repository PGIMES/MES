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

public partial class YaSheTou_YST_Maintain : System.Web.UI.Page
{
    //public string UserId = "";
    //public string UserName = "";
    //LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        //UserId = LogUserModel.UserId;
        //UserName = LogUserModel.UserName;

        if (!IsPostBack)
        {
            //StartDate.Text = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
            //EndDate.Text = System.DateTime.Now.ToShortDateString();
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
    }

    protected void btn_search_ServerClick(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Base_Select] 1,'" + txt_code.Text + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }
    protected void btnimport_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("压射头明细" + System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }


    [WebMethod]
    public static string del_data(string code)
    {
        string re_flag = "";

        DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Base_Select] 3,'" + code + "'").Tables[0];
        if (dt.Rows[0][0].ToString() == "N")
        {
            re_flag = "删除成功！";
        }
        else
        {
            re_flag = "删除失败：压射头编号" + code + ",设备号"+ dt.Rows[0][0].ToString() + "已使用，不能删除！";
        }
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        if (Convert.ToInt32(e.GetValue("mc").ToString()) < Convert.ToInt32(e.GetValue("lj_mc").ToString()))
        {
            e.Row.Cells[6].Style.Add("background-color", "#EEEE00");
        }
        if (e.GetValue("yzt_status").ToString()=="报废")
        {
            e.Row.Cells[8].Style.Add("background-color", "#FF0000");
            e.Row.Cells[8].Style.Add("color", "#FFFFFF");
        }
    }
}