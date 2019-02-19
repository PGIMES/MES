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


            //if (ldt_data.Columns[i].MaxLength > 0)
            //{
            //    lnwidth += ldt_data.Columns[i].MaxLength;
            //}
            //else
            //{
            //    lnwidth += Convert.ToInt32(lnw);
            //}

        }

        //lgrid.Columns[0].Width = 40;
        //lgrid.Columns[0].ExportWidth = 40;

        //lgrid.Width = (lnwidth + 40);
        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

        //return lgrid;
    }

}