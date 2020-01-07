﻿using System;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.UI;

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
        ASPxGridViewExporter1.WriteXlsToResponse("物流运费" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }
}