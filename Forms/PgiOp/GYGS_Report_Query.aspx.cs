using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PgiOp_GYGS_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }


    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

    }

    public void QueryASPxGridView()
    {
        GYGS GYGS = new GYGS();
        DataTable dt = GYGS.GYGS_query(txt_pgi_no.Text.Trim(), txt_pn.Text.Trim(), ddl_ver.SelectedValue, ddl_typeno.SelectedValue);
        //Pgi.Auto.Control.SetGrid("GYGS_Query", "", this.gv, dt);

        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();", true);
    }

    //protected void gv_tr_list_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.Caption == "序号")
    //    {
    //        if (Convert.ToInt16(ViewState["i"]) == 0)
    //        {
    //            ViewState["i"] = 1;
    //        }
    //        int i = Convert.ToInt16(ViewState["i"]);
    //        e.Cell.Text = i.ToString();
    //        i++;
    //        ViewState["i"] = i;
    //    }
    //}

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse("库龄" + System.DateTime.Now.ToShortDateString());//导出到Excel

    }

}