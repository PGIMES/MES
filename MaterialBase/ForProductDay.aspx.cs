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
        
        string ppap_date2 = (string)e.GetValue("ppap_date2");
        if (ppap_date2 != "")
        {
            int pdays = Convert.ToInt32(e.GetValue("purchase_days").ToString())+7;
            if (Convert.ToDateTime(ppap_date2).AddDays(-pdays) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                e.Row.Style.Add("background-color", "color");
            }
        }
        
    }
}