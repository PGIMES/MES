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

public partial class JiaJu_JiaJu_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            //txtDateTo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {

        DataTable dt = DbHelperSQL.Query("Rpt_JiaJu_LYQuery '"+Drop_status.SelectedValue+"','"+txt_jiaju_no.Text+"','"+txt_pn.Text+"','"+txt_lyuid.Text+"','"+Drop_comp.SelectedValue+"','"+txtDateFrom.Text+"','"+txtDateTo.Text+"'").Tables[0];
        Pgi.Auto.Control.SetGrid(this.ASPxGridView1, dt, 100);

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight();", true);
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

        ASPxGridViewExporter1.WriteXlsToResponse("夹具出入库查询" + System.DateTime.Now.ToShortDateString());//导出到Excel

    }

    protected void gv_CustomCellMerge(object sender, ASPxGridViewCustomCellMergeEventArgs e)
    {

    }
    protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight();", true);
    }
}