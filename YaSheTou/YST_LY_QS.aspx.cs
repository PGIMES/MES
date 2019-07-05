using DevExpress.Utils;
using DevExpress.XtraCharts;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YaSheTou_YST_LY_QS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
        DataSet ds = DbHelperSQL.Query("exec [usp_MES_YaSheTou_LY_QS] '" + ddl_zj.SelectedValue + "','"+ txt_wlh.Text.Trim()  + "'");

        //grid A
        SetGrid(gv_tr_list_A, ds.Tables[0], 50, "typedesc");

        //图A
        List<Series> listA = CreateSeries(ds.Tables[0], 1);
        ChartA.Series.Clear();
        ChartA.Series.AddRange(listA.ToArray());
        ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;


        //grid B
        SetGrid(gv_tr_list_B, ds.Tables[1], 90, "typedesc");

        //图B
        List<Series> listB = CreateSeries(ds.Tables[1], 1);
        ChartB.Series.Clear();
        ChartB.Series.AddRange(listB.ToArray());
        ChartB.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid C
        SetGrid(gv_tr_list_C, ds.Tables[2], 70, "typedesc");

        //图C
        List<Series> listC = CreateSeries(ds.Tables[2], 1);
        ChartC.Series.Clear();
        ChartC.Series.AddRange(listC.ToArray());
        ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid D
        SetGrid(gv_tr_list_D, ds.Tables[3], 50, "typedesc");

        //图D
        List<Series> listD = CreateSeries(ds.Tables[3], 1);
        ChartD.Series.Clear();
        ChartD.Series.AddRange(listD.ToArray());
        ChartD.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

    }

    public List<Series> CreateSeries(DataTable dtChart,int rowindex)
    {
        List<Series> list = new List<Series>();
        Series series = new Series("金额(万)", DevExpress.XtraCharts.ViewType.Line);
        if (dtChart.Rows.Count >= 1)
        {
            for (int i = 1; i < dtChart.Columns.Count; i++)
            {
                string argument = dtChart.Columns[i].ColumnName;//参数名称 
                decimal value = Convert.ToDecimal(dtChart.Rows[rowindex][i].ToString() == "" ? "0" : dtChart.Rows[rowindex][i].ToString());//参数值
                series.Points.Add(new SeriesPoint(argument, value));
            }
        }

        series.ArgumentScaleType = ScaleType.Qualitative;
        series.View.Color = System.Drawing.Color.Green;

        list.Add(series);

        return list;
    }

    private static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw, string keyfieldname)
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
            if (ldt_data.Columns[i].ColumnName.ToString() == "typedesc") { lnwidth_emp = 80; lcolumn.Caption = " "; }

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
            lcolumn.PropertiesTextEdit.DisplayFormatString = "{0:N0}";
        }

        lgrid.Width = lnwidth;

        lgrid.KeyFieldName = keyfieldname;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

    }
}