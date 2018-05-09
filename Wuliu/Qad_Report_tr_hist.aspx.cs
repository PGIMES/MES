using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Qad_Report_tr_hist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv_tr_list.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        Wuliu_tr_hist trlist_query = new Wuliu_tr_hist();
        DataTable dt = trlist_query.Get_tr_list_query(ddl_comp.SelectedValue, txt_tr_part_start.Text.Trim());//, txt_tr_part_end.Text.Trim()
        //Pgi.Auto.Control.SetGrid(this.gv_tr_list, dt, 100);
        gv_tr_list.DataSource = dt;
        gv_tr_list.DataBind();
    }

    protected void gv_tr_list_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_tr_list_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "序号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
    }


    protected void btnimport_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse("库龄"+System.DateTime.Now.ToShortDateString());//导出到Excel
    }
}