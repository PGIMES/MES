using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_rpt_Po_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("Po_Print.frx"));//加载模板
        string lssql = "exec rpt_Po_Print '{0}', '{1}'";

        string pono = "";
        if (Request.QueryString["pono"] != null) { pono = Request.QueryString["pono"]; }

        string lssql_m = string.Format(lssql, pono,"main");
        DataTable ldt = DbHelperSQL.Query(lssql_m).Tables[0];

        string lssql_d = string.Format(lssql, pono, "dtl");
        DataTable ldt_d = DbHelperSQL.Query(lssql_d).Tables[0];

        report.RegisterData(ldt, "main");
        report.RegisterData(ldt_d, "dtl");


        FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
        report.Prepare();
        report.Export(pdf, Server.MapPath("po.pdf"));
        Response.Redirect("po.pdf");

    }
}