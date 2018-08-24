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

public partial class Forms_Finance_T_CA_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true || this.gv_sj.IsCallback == true)//页面搜索条件使用
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
        DataTable dt = DbHelperSQL.Query("exec  Report_Fin_T_CA '" + ddl_domain.SelectedValue + "','" + ddl_typeno.SelectedValue + "'").Tables[0];
        if (ddl_typeno.SelectedValue == "差旅")
        {
            gv.Visible = true;
            gv_sj.Visible = false;

            gv.DataSource = dt;
            gv.DataBind();
        }

        if (ddl_typeno.SelectedValue == "私车公用")
        {
            gv.Visible = false;
            gv_sj.Visible = true;

            gv_sj.DataSource = dt;
            gv_sj.DataBind();
        }

    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }
    protected void gv_sj_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        if (ddl_typeno.SelectedValue == "差旅")
        {
            ASPxGridViewExporter1.WriteXlsToResponse("差旅" + System.DateTime.Now.ToShortDateString());//导出到Excel
        }

        if (ddl_typeno.SelectedValue == "私车公用")
        {
            ASPxGridViewExporter2.WriteXlsToResponse("私车公用" + System.DateTime.Now.ToShortDateString());//导出到Excel
        }
    }
}