using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Forms_Sale_CS_Report_Query : System.Web.UI.Page
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
        string sql = @"exec Report_CS '{0}','{1}'";
        sql = string.Format(sql, ddl_comp.SelectedValue, txt_wlh.Text);

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("客户日程单浏览_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    [WebMethod]
    public static string CheckData(string domain, string part, string cust_part)
    {
        //------------------------------------------------------------------------------验证申请中
        string re_flag = "";

        string re_sql = @" exec [Report_CS_edit_check] '"+domain+ "','" + part + "','" + cust_part + "'";
        DataSet ds = DbHelperSQL.Query(re_sql);
        string dt_flag = ds.Tables[0].Rows[0][0].ToString();
        DataTable dt = ds.Tables[0];

        if (dt_flag == "Y1")
        {
            re_flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + "【客户物料号】" + cust_part
                + "正在<font color='red'>申请中</font>，不能修改(单号:" + dt.Rows[0]["formno"].ToString() + ",申请人:"
                + dt.Rows[0]["ApplyId"].ToString() + "-" + dt.Rows[0]["ApplyName"].ToString() + ")!";
        }
        if (dt_flag == "Y2")
        {
            re_flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + ",物料状态已经DEAD，不可修改!";
        }
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
}