﻿using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_Qad_Report_tr_hist_new_query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_year.SelectedValue = DateTime.Now.AddMonths(-1).ToString("yyyy"); //获取上月年份
            ddl_month.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM"); //获取上月月份
            QueryASPxGridView();
        }
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataSet ds = DbHelperSQL.Query("exec [Report_tr_hist_new] '5','" + ddl_comp.SelectedValue + "','" + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "',''");

        //grid A
        gv_tr_list.DataSource = ds.Tables[0];
        gv_tr_list.DataBind();

        //图A
        DataTable dt_chartA = ds.Tables[1];
        ChartA.Series.Clear();

        dt_chartA.Columns["amount1"].ColumnName = "10以内金额"; dt_chartA.Columns["amount2"].ColumnName = "10-20金额";
        dt_chartA.Columns["amount3"].ColumnName = "20-30金额"; dt_chartA.Columns["amount4"].ColumnName = "30-60金额";
        dt_chartA.Columns["amount5"].ColumnName = "60-90金额"; dt_chartA.Columns["amount6"].ColumnName = "90-180金额";
        dt_chartA.Columns["amount7"].ColumnName = "180-360金额"; dt_chartA.Columns["amount8"].ColumnName = "360以上金额";

        List<Series> list = new List<Series>();
        Series series = new Series("昆山库存金额", ViewType.Pie);
        for (int i = 1; i <= 8; i++)
        {
            string argument = dt_chartA.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartA.Rows[0][i].ToString());//参数值
            series.Points.Add(new SeriesPoint(argument, value));
            series.LabelsVisibility = DefaultBoolean.True;
        }
        series.ArgumentScaleType = ScaleType.Qualitative;
        series.Label.TextPattern = "{A}:{VP:P2}";

        list.Add(series);
        ChartA.Series.AddRange(list.ToArray());
        ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid 2
        SetGrid(gv_tr_list_2, ds.Tables[2], 80, "typedesc");
        gv_tr_list_2.Columns["typedesc"].Caption = "月份/金额";

        //图B
        DataTable dt_chartB = ds.Tables[3];
        if (ChartB.Diagram != null)
        {
            ((XYDiagram)ChartB.Diagram).SecondaryAxesY.Clear();
        }
        ChartB.Series.Clear();

        List<Series> listB = new List<Series>();
        Series seriesB = new Series("金额", ViewType.Bar);
        Series seriesB_2 = new Series("金额占比", ViewType.Line); 
        for (int i = 1; i < dt_chartB.Columns.Count; i++)
        {
            string argument = dt_chartB.Columns[i].ColumnName;//参数名称 

            decimal value = Convert.ToDecimal(dt_chartB.Rows[0][i].ToString());//参数值
            seriesB.Points.Add(new SeriesPoint(argument, value));

            decimal value_2 = Convert.ToDecimal(dt_chartB.Rows[1][i].ToString());//参数值
            seriesB_2.Points.Add(new SeriesPoint(argument, value_2));

        }
        seriesB.ArgumentScaleType = ScaleType.Qualitative;
        seriesB_2.ArgumentScaleType = ScaleType.Qualitative;

        listB.Add(seriesB); listB.Add(seriesB_2);

        ChartB.Series.AddRange(listB.ToArray());
        ChartB.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        var diagram = ((XYDiagram)ChartB.Diagram);
        SecondaryAxisY secondaryYAxis = new SecondaryAxisY("Population Axis");
        secondaryYAxis.Label.TextPattern = "{VP:P0}";
        diagram.SecondaryAxesY.Add(secondaryYAxis);
        ((LineSeriesView)seriesB_2.View).AxisY = secondaryYAxis;

        //grid 4
        SetGrid(gv_tr_list_4, ds.Tables[4], 80, "typedesc");
        gv_tr_list_4.Columns["typedesc"].Caption = "月份/金额";

        //图D
        DataTable dt_chartD = ds.Tables[4];
        ChartD.Series.Clear(); 

        List<Series> listD = new List<Series>();
        Series seriesD = new Series("金额", ViewType.Line);
        for (int i = 1; i < dt_chartD.Columns.Count; i++)
        {
            string argument = dt_chartD.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartD.Rows[0][i].ToString());//参数值
            seriesD.Points.Add(new SeriesPoint(argument, value));
        }
        seriesD.ArgumentScaleType = ScaleType.Qualitative;
        listD.Add(seriesD);
        ChartD.Series.AddRange(listD.ToArray());
        ChartD.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid 3
        SetGrid(gv_tr_list_3, ds.Tables[5], 80, "typedesc;days");
        gv_tr_list_3.Columns["typedesc"].Caption = "分类";
        gv_tr_list_3.Columns["days"].Caption = "在库天数";

        //图C
        DataTable dt_chartC = ds.Tables[6];
        ChartC.Series.Clear();

        List<Series> listC = new List<Series>();
        Series seriesC = new Series("昆山库存30-180金额", ViewType.Pie);
        for (int i = 2; i < dt_chartC.Columns.Count - 1; i++)
        {
            string argument = dt_chartC.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartC.Rows[0][i].ToString());//参数值
            seriesC.Points.Add(new SeriesPoint(argument, value));

        }
        seriesC.ArgumentScaleType = ScaleType.Qualitative;
        seriesC.Label.TextPattern = "{A}:{VP:P2}";

        listC.Add(seriesC);
        ChartC.Series.AddRange(listC.ToArray());
        ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid 5
        SetGrid(gv_tr_list_5, ds.Tables[7], 80, "typedesc");
        gv_tr_list_5.Columns["typedesc"].Caption = "分类";

        //图E
        DataTable dt_chartE = ds.Tables[8];
        ChartE.Series.Clear();

        List<Series> listE = new List<Series>();
        Series seriesE = new Series("昆山库存超180金额", ViewType.Pie);
        for (int i = 1; i < dt_chartE.Columns.Count - 1; i++)
        {
            string argument = dt_chartE.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartE.Rows[0][i].ToString());//参数值
            seriesE.Points.Add(new SeriesPoint(argument, value));

        }
        seriesE.ArgumentScaleType = ScaleType.Qualitative;
        seriesE.Label.TextPattern = "{A}:{VP:P2}";

        listE.Add(seriesE);
        ChartE.Series.AddRange(listE.ToArray());
        ChartE.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    protected void gv_tr_list_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("rate"))
            {
                for (int i = 1; i < gv_tr_list.Columns.Count - 1; i++)
                {
                    if (e.GetValue("amount" + i.ToString()) != DBNull.Value)
                    {
                        e.Row.Cells[i - 1].Text = Convert.ToString(Convert.ToDouble(e.GetValue("amount" + i.ToString())) * 100) + "%";
                    }
                }

                if (e.GetValue("ld_qty_oh_amount") != DBNull.Value)
                {
                    e.Row.Cells[8].Text = Convert.ToString(e.GetValue("ld_qty_oh_amount")) + "00%";
                }
            }
        }
    }

    protected void gv_tr_list_2_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("金额占比"))
            {
                DataTable ldt = Pgi.Auto.Control.AgvToDt(gv_tr_list_2);
                for (int i = 1; i < ldt.Columns.Count; i++)
                {
                    if (ldt.Columns[i].ColumnName != "flag")
                    {
                        if (e.GetValue(ldt.Columns[i].ColumnName) != DBNull.Value)
                        {
                            e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(ldt.Columns[i].ColumnName)) * 100) + "%";
                        }
                    }
                }
            }
        }
    }
    protected void gv_tr_list_3_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("百分比"))
            {
                DataTable ldt = Pgi.Auto.Control.AgvToDt(gv_tr_list_3);
                for (int i = 2; i < ldt.Columns.Count; i++)
                {
                    if (ldt.Columns[i].ColumnName != "flag")
                    {
                        if (e.GetValue(ldt.Columns[i].ColumnName) != DBNull.Value)
                        {
                            e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(ldt.Columns[i].ColumnName)) * 100) + "%";
                        }
                    }
                }
            }
        }
    }

    protected void gv_tr_list_5_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString().Contains("百分比"))
            {
                DataTable ldt = Pgi.Auto.Control.AgvToDt(gv_tr_list_5);
                for (int i = 1; i < ldt.Columns.Count; i++)
                {
                    if (ldt.Columns[i].ColumnName != "flag")
                    {
                        if (e.GetValue(ldt.Columns[i].ColumnName) != DBNull.Value)
                        {
                            e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(ldt.Columns[i].ColumnName)) * 100) + "%";
                        }
                    }
                }
            }
        }
    }

    private static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw,string keyfieldname)
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

    //private void CreateChart(DataTable dt,WebChartControl chart, ViewType viewType)
    //{
    //    chart.Series.Clear();

    //    //动态创建多个Series 图形的对象
    //    List<Series> list = new List<Series>();
    //    int j = 0;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        list.Add(CreateSeries(dt.Rows[i][1].ToString(), viewType, dt, j));
    //        j++;
    //    }

    //    chart.Series.AddRange(list.ToArray());
    //    chart.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    //}



    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    //private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
    //{
    //    Series series = new Series(caption, viewType);
    //    for (int i = 1; i < dt.Columns.Count - 1; i++)
    //    {
    //        if (i < 10)
    //        {
    //            string argument = dt.Columns[i].ColumnName;//参数名称 
    //            decimal value = 0;
    //            if (dt.Rows[rowIndex][i].ToString() != null && dt.Rows[rowIndex][i].ToString() != "")
    //            {
    //                value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
    //            }
    //            series.Points.Add(new SeriesPoint(argument, value));
    //        }
    //    }
    //    //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
    //    //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
    //    series.ArgumentScaleType = ScaleType.Qualitative;
    //    return series;
    //}
}