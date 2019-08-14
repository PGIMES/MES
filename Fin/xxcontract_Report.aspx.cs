using DevExpress.Export;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_xxcontract_Report : System.Web.UI.Page
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
        {
            StartDate.Text = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
            EndDate.Text = System.DateTime.Now.ToShortDateString();
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
        DataTable dt = DbHelperSQL.Query("exec Report_xxcontract_mstr_det_new '" + ddl_domain.SelectedValue + "','"+ StartDate.Text + "','" + EndDate.Text + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }
    protected void gv_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
        {
            return;
        }
        if (e.GetValue("contractstatus").ToString() == "作废")
        {
            e.Row.Style.Add("background-color", "#FFFFFF");
            e.Row.Style.Add("font-style", "italic");
            e.Row.Style.Add("color", "#969696");
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("应付类合同执行进度查询_" + ddl_domain.SelectedItem.Text + "_" + System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void gv_ExportRenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Column.Caption == "实际到货日期")
            {
                if (e.Value != DBNull.Value)
                {
                    e.TextValue = Convert.ToString(e.Value).Replace("<font color=red>", "").Replace("</font>", "").Replace("<br>", "\r\n");
                }
            }
        }
    }

    [WebMethod]
    public static string deal_data(string action, string nbr,string domain,string UserId,string UserName)
    {
        string re_flag = "";

        DataTable dt = DbHelperSQL.Query(@"select * from PUR_PO_Contract_Status where domain='"+domain+ "' and SysContractNo='"+ nbr + "'").Tables[0];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                re_flag = "合同号" + nbr + "已" + dt.Rows[0]["Status"].ToString() + "，不能再" + action + "！";
            }
        }

        if (re_flag == "")
        {
            string sql = @"insert into PUR_PO_Contract_Status(domain, SysContractNo, Status, CreateId, CreateName, CreateTime)
                        select '{0}','{1}','{2}','{3}','{4}',getdate()";
            sql = string.Format(sql, domain, nbr, action, UserId, UserName);

            int i = DbHelperSQL.ExecuteSql(sql);
            if (i > 0)
            {
                re_flag = action + "成功！";
            }
            else
            {
                re_flag = action + "失败！";
            }
        }
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
    [WebMethod]
    public static string check_data(string nbr, string domain)
    {
        string re_flag = "";

        DataTable dt = DbHelperSQL.Query(@"select * from PUR_PO_Contract_Status where domain='" + domain + "' and SysContractNo='" + nbr + "'").Tables[0];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                re_flag = dt.Rows[0]["Status"].ToString();
            }
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
}