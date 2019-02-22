using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Report_Planning : System.Web.UI.Page
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
        DataTable dt = DbHelperSQL.Query("exec [Report_Planning_Show] '" + ddl_year.SelectedValue + "','" + ddl_domain.SelectedValue + "'").Tables[0];
        SetGrid(this.gv, dt, 90);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("Planning_" + System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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

            lnwidth_emp = 0;
            if (ldt_data.Columns[i].ColumnName.ToString() == "描述") { lnwidth_emp = 170; }

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

            if (lcolumn.FieldName == "typedesc_depta_all" || lcolumn.FieldName == "dept")
            {
                lcolumn.Visible = false;
            }
            lcolumn.PropertiesTextEdit.DisplayFormatString = "{0:N0}";
        }
        lnwidth = lnwidth - 90 - 90;//减去 隐藏列typedesc_depta_all的宽度

        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

    }


    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //if (e.KeyValue.ToString().Contains("计划生产订单数量"))
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#EEEE00");
            //}
            if (e.KeyValue.ToString().Contains("完成率"))
            {
                for (int i = 1; i < gv.Columns.Count - 4; i++)
                {
                    if (e.GetValue("W" + i.ToString()) != DBNull.Value)
                    {
                        e.Row.Cells[i + 2].Text = Convert.ToString(e.GetValue("W" + i.ToString())) + "%";
                    }
                }

                //e.Row.Style.Add("background-color", "#BFEFFF");
            }
            else
            {
                if (e.KeyValue.ToString().Contains("生产2部") || e.KeyValue.ToString().Contains("生产1部") || e.KeyValue.ToString().Contains("生产4部") || e.KeyValue.ToString().Contains("压铸")
                    || e.KeyValue.ToString().Contains("实际发货数量"))
                {
                    for (int i = 1; i < gv.Columns.Count - 4; i++)
                    {
                        if (e.GetValue("W" + i.ToString()) != DBNull.Value)
                        {
                            if (Convert.ToDouble(e.GetValue("W" + i.ToString()))!=0)
                            {
                                e.Row.Cells[i + 2].Style.Add("color", "blue");
                                e.Row.Cells[i + 2].Attributes.Add("onclick", "show_detail('" + e.GetValue("dept").ToString() + "','"
                                    + e.GetValue("typedesc_depta_all").ToString().Replace("(" + e.GetValue("dept").ToString() + ")", "") + "','" + i.ToString() + "')");
                            }
                           
                        }
                    }

                }

            }


        }
    }
}