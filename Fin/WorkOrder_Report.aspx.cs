using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_WorkOrder_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //StartDate.Text = System.DateTime.Now.ToShortDateString(); //System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
            //EndDate.Text = System.DateTime.Now.ToShortDateString();
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
        DataTable dt = null;
        if (StartDate.Text != "" && EndDate.Text != "")
        {
            dt = DbHelperSQL.Query("exec Report_WorkOrder '" + ddl_typeno.SelectedValue + "', '" + ddl_domain.SelectedValue + "','" + StartDate.Text + "','" + EndDate.Text + "'").Tables[0];
        }

        gv.Columns.Clear();
        Pgi.Auto.Control.SetGrid("Report_WorkOrder", ddl_typeno.SelectedValue, gv, dt);

    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv_export.WriteXlsxToResponse(ddl_typeno.SelectedItem.Text + "_" + ddl_domain.SelectedValue + "_" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

}