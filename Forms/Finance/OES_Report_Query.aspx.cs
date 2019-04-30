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

public partial class Forms_Finance_OES_Report_Query : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);

        if (!IsPostBack)
        {
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
        DataTable dt = DbHelperSQL.Query("exec  Report_Fin_OES '" + ddl_domain.SelectedValue + "','"+ LogUserModel.UserId + "','" + LogUserModel.DepartName + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse("费用报销" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.Name == "FormNo" || e.Column.FieldName == "ApplyDate" || e.Column.FieldName == "ApplyName" || e.Column.FieldName == "ApplyDept" || e.Column.FieldName == "ApplyDomainName"
             || e.Column.FieldName == "GoDays" || e.Column.FieldName == "GoSatus" || e.Column.FieldName == "ApproveDate")
        {
            var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "FormNo");
            var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "FormNo");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }

    }
}