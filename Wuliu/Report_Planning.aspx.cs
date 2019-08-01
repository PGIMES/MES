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
        //DataTable dt = DbHelperSQL.Query("exec [Report_Planning_Show] '" + ddl_year.SelectedValue + "','" + ddl_domain.SelectedValue + "'").Tables[0];
        DataTable dt = DbHelperSQL.Query("exec [Report_Planning_Show_New] '" + ddl_year.SelectedValue + "','" + ddl_dept.SelectedValue + "'").Tables[0];
        SetGrid(this.gv, dt, 90);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("Planning_"+ ddl_dept.SelectedItem.Text+"_" + System.DateTime.Now.ToShortDateString(), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
            if (ldt_data.Columns[i].ColumnName.ToString() == "typedesc") { lnwidth_emp = 200; lcolumn.Caption = "描述"; }

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

            if (lcolumn.FieldName == "dept")
            {
                lcolumn.Visible = false;
            }
            lcolumn.PropertiesTextEdit.DisplayFormatString = "{0:N0}";

            if (lcolumn.FieldName == "typedesc" || lcolumn.FieldName == "Total" || lcolumn.FieldName == "Average") 
            {
                lcolumn.FixedStyle = GridViewColumnFixedStyle.Left;
            }
        }
        lnwidth = lnwidth - 90 - 90;//减去 隐藏列typedesc_depta_all的宽度

        //lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

    }

    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("完成率"))
            {
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    string fieldname = ((DevExpress.Web.GridViewDataColumn)gv.Columns[i]).FieldName;
                    if (fieldname.StartsWith("W"))
                    {
                        if (e.GetValue(fieldname) != DBNull.Value)
                        {
                            e.Row.Cells[i - 1].Text = Convert.ToString(e.GetValue(fieldname)) + "%";
                        }
                    }
                    
                }

            }
            else
            {

                if (e.KeyValue.ToString().Contains("计划生产订单数量") || e.KeyValue.ToString().Contains("废品数量")
                     || e.KeyValue.ToString().Contains("未完成订单数量") || e.KeyValue.ToString().Contains("实际发货数量"))
                {
                    for (int i = 0; i < gv.Columns.Count; i++)
                    {
                        string fieldname = ((DevExpress.Web.GridViewDataColumn)gv.Columns[i]).FieldName;
                        if (fieldname.StartsWith("W"))
                        {
                            if (e.GetValue(fieldname) != DBNull.Value)
                            {
                                if (Convert.ToDouble(e.GetValue(fieldname)) != 0)
                                {
                                    string week = fieldname.Substring(1, fieldname.IndexOf('(') - 1);
                                    e.Row.Cells[i - 1].Style.Add("color", "blue");
                                    e.Row.Cells[i - 1].Attributes.Add("onclick", "show_detail('" + e.GetValue("dept").ToString() + "','" + e.GetValue("typedesc").ToString() + "','" + week + "')");
                                }

                            }
                        }
                        
                    }

                }

            }
        }
    }


    protected void gv_ExportRenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("完成率"))
            {
                if (e.Column.Caption.Substring(0, 1) == "W")
                {
                    if (e.Value != DBNull.Value)
                    {
                        e.TextValue = Convert.ToString(e.Value) + "%";
                    }
                }
            }
        }
    }
}