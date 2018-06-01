using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class MaterialBase_ForProductDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv_pt.IsCallback)
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
        MaterialBase_CLASS mbl = new MaterialBase_CLASS();
        DataTable dt = mbl.ForproductsDay(txt_tr_part_start.Text.Trim(), ddl_comp.SelectedValue, ddl_isSchedule.SelectedValue);//, txt_tr_part_end.Text.Trim()
        this.gv_pt.DataSource = dt;
        gv_pt.DataBind();
    }

    protected void gv_pt_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        string isSchedule = (string)e.GetValue("isSchedule");
        if (isSchedule == "否")
        {
            int sydays = (int)e.GetValue("sydays");
            if (sydays < 0)
            {
                e.Row.Style.Add("background-color", "red");
            }

            string ppap_date2 = (string)e.GetValue("ppap_date2");
            if (ppap_date2 != "")
            {
                int pdays = Convert.ToInt32(e.GetValue("purchase_days").ToString()) + 7;
                if (Convert.ToDateTime(ppap_date2).AddDays(-pdays) <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))//今天超过了算下来的日期，就应该标红
                {
                    e.Row.Style.Add("background-color", "red");
                }
            }
        }
    }

    protected void gv_pt_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
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

    protected void gv_pt_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}