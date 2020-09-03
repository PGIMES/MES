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
        string pono = "";
        if (Request.QueryString["pono"] != null) { pono = Request.QueryString["pono"]; }
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        //----------------------------------------------------打印记录----------------------------------------------------------------------
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        Pgi.Auto.Common ls_update = new Pgi.Auto.Common();
        string sql_update = @"update PUR_PO_Main_Form set IsPrint=1 where PoNo='{0}' and isnull(IsPrint,'')=''";
        sql_update = string.Format(sql_update, pono);
        ls_update.Sql = sql_update;
        ls_sum.Add(ls_update);

        Pgi.Auto.Common ls_insert = new Pgi.Auto.Common();
        string sql_insert = @"insert into PUR_PO_Print_His(PoNo, PrintById, PrintByName)
                            select '{0}','{1}','{2}'";
        sql_insert = string.Format(sql_insert, pono, LogUserModel.UserId, LogUserModel.UserName);
        ls_insert.Sql = sql_insert;
        ls_sum.Add(ls_insert);

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        //string msg = "";
        //if (ln > 0)
        //{
        //    msg = "确认成功！";
        //}
        //else
        //{
        //    msg = "确认失败！";
        //}
        //string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
        //--------------------------------------------------------------------------------------------------------------------------

        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("Po_Print.frx"));//加载模板
        string lssql = "exec rpt_Po_Print '{0}', '{1}'";


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