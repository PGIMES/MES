using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraCharts;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_Fin_xxwms_det_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_year.SelectedValue = DateTime.Now.Year.ToString(); //获取上月年份
        }

        QueryASPxGridView();
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec Report_xxwms_det {0},'{1}'";
        sql = string.Format(sql, ddl_year.SelectedValue, ddl_comp.SelectedValue);
        DataSet ds = DbHelperSQL.Query(sql);

        DataTable dt = ds.Tables[0]; 

        SetGrid(gv, dt, 80, "si_desc");


        if (Chart.Diagram != null)
        {
            ((XYDiagram)Chart.Diagram).SecondaryAxesY.Clear();
        }
        Chart.Series.Clear();

        List<Series> list = new List<Series>();

        if (dt.Rows.Count >= 1)
        {
            for (int row = 0; row < dt.Rows.Count - 1; row++)//最后一行合计排除
            {
                Series seriesA = new Series(dt.Rows[row]["si_desc"].ToString(), DevExpress.XtraCharts.ViewType.StackedBar);
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    string argument = dt.Columns[i].ColumnName;//参数名称 
                    decimal value = Convert.ToDecimal(dt.Rows[row][i].ToString() == "" ? "0" : dt.Rows[row][i].ToString());//参数值
                    seriesA.Points.Add(new SeriesPoint(argument, value));
                }
                seriesA.ArgumentScaleType = ScaleType.Qualitative;

                BarSeriesView sv1 = (BarSeriesView)seriesA.View;
                sv1.FillStyle.FillMode = FillMode.Solid;//    solid 实线，gradient 渐变，hatch 斜形
                list.Add(seriesA);
            }
        }

        string title = "合计";
        int rows_hj = dt.Rows.Count - 1;
        Series seriesA_1 = new Series(title, DevExpress.XtraCharts.ViewType.Line);
        for (int i = 0; i < DateTime.Now.Month; i++)
        {
            string argument = dt.Columns[i + 1].ColumnName;//参数名称 

            decimal value_2 = Convert.ToDecimal(dt.Rows[rows_hj][i + 1].ToString() == "" ? "0" : dt.Rows[rows_hj][i + 1].ToString());//参数值
            seriesA_1.Points.Add(new SeriesPoint(argument, value_2));

        }
        seriesA_1.Label.TextPattern = "{V:N0}";
        seriesA_1.LabelsVisibility = DefaultBoolean.True;
        seriesA_1.View.Color = System.Drawing.Color.Green;
        seriesA_1.ArgumentScaleType = ScaleType.Qualitative;
        list.Add(seriesA_1);

        Chart.Series.AddRange(list.ToArray());
        Chart.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        var diagramA_1 = ((XYDiagram)Chart.Diagram);
        SecondaryAxisY secondaryYAxisA_1 = new SecondaryAxisY("Population Axis");
        diagramA_1.SecondaryAxesY.Add(secondaryYAxisA_1);
        ((LineSeriesView)seriesA_1.View).AxisY = secondaryYAxisA_1;
        ((LineSeriesView)seriesA_1.View).AxisY.Label.TextPattern = "{V:#,0}";

        ((XYDiagram)Chart.Diagram).AxisY.Label.TextPattern = "{V:#,0}";


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

            if (lcolumn.FieldName == "si_desc") { lcolumn.Caption = "仓库描述"; lnwidth_emp = 110; }

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

        //lgrid.Width = lnwidth;

        lgrid.KeyFieldName = keyfieldname;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

    }

    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }

        string si_desc = Convert.ToString(e.GetValue("si_desc"));
        if (si_desc == "合计")
        {
            e.Row.Style.Add("background-color", "LightGrey");
            //e.Row.Style.Add("color", "red");
        }
    }
}