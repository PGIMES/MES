using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CapacityPlan_Report_Base_CapacityPlan : System.Web.UI.Page
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

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        Capacity Capacity = new Capacity();
        DataTable dt = Capacity.Report_Base_CapacityPlan(ddl_type.SelectedValue, ddl_comp.SelectedValue, txt_op.Text.Trim());
        SetGrid(this.gv, dt, 90);

        if (ddl_type.SelectedValue == "emp")
        {
            this.gv.DataColumns[1].Settings.AllowCellMerge = DevExpress.Utils.DefaultBoolean.True;
        }
    }
        

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
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


    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse(System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    private static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw)
    {
        if (ldt_data == null)
        {
            return;
        }

        lgrid.AutoGenerateColumns = false;
        int lnwidth = 0; int lnwidth_emp = 0;
        lgrid.Columns.Clear();
        for (int i = 0; i < ldt_data.Columns.Count; i++)
        {
            DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
            lcolumn.Name = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.Caption = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.FieldName = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

            //if (ldt_data.Columns[i].MaxLength > 0)
            //{
            //    lcolumn.Width = ldt_data.Columns[i].MaxLength;
            //    lcolumn.ExportWidth = ldt_data.Columns[i].MaxLength;
            //}
            //else
            //{
            //    lcolumn.Width = lnw;
            //    lcolumn.ExportWidth = lnw;
            //}

            lnwidth_emp = 0;
            if (ldt_data.Columns[i].ColumnName.ToString() == "工厂") { lnwidth_emp = 80; }
            if (ldt_data.Columns[i].ColumnName.ToString() == "工序") { lnwidth_emp = 50; }
            if (ldt_data.Columns[i].ColumnName.ToString() == "周数") { lnwidth_emp = 120; }

            if (lnwidth_emp > 0)
            {
                lcolumn.Width = lnwidth_emp;
                lcolumn.ExportWidth = lnwidth_emp;
                lnwidth += Convert.ToInt32(lnwidth_emp);
            }
            else
            {
                lcolumn.Width = lnw;
                lcolumn.ExportWidth = lnw;
                lnwidth += Convert.ToInt32(lnw);
            }

            //设置查询
            lcolumn.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;

            lgrid.Columns.Add(lcolumn);

            //if (ldt_data.Columns[i].MaxLength > 0)
            //{
            //    lnwidth += ldt_data.Columns[i].MaxLength;
            //}
            //else
            //{
            //    lnwidth += Convert.ToInt32(lnw);
            //}

        }

        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

        //return lgrid;
    }
}