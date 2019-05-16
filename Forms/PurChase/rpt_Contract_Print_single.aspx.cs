using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_rpt_Contract_Print_single : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02432", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("Contract_Print_single.frx"));//加载模板
        string lssql = "exec rpt_Contract_Print_single '{0}', '{1}', '{2}', '{3}'";

        string nbr = "";
        if (Request.QueryString["nbr"] != null) { nbr = Request.QueryString["nbr"]; }

        string line = "";
        if (Request.QueryString["line"] != null) { line = Request.QueryString["line"]; }

        string lssql_m = string.Format(lssql, nbr, line, LogUserModel.UserName, "main");
        DataTable ldt = DbHelperSQL.Query(lssql_m).Tables[0];

        string lssql_d = string.Format(lssql, nbr, line, LogUserModel.UserName, "dtl");
        DataTable ldt_d = DbHelperSQL.Query(lssql_d).Tables[0];

        report.RegisterData(ldt, "main");
        report.RegisterData(ldt_d, "dtl");


        FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
        report.Prepare();
        report.Export(pdf, Server.MapPath("contract_single.pdf"));
        Response.Redirect("contract_single.pdf");
    }
}