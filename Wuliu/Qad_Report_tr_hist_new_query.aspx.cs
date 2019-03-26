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
        string curmonth = "";
        if (ddl_condition.SelectedValue == "his")
        {
            curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        }
        DataSet ds = DbHelperSQL.Query("exec [Report_tr_hist_new] '5','" + ddl_comp.SelectedValue + "','" + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "'");

        //grid A
        gv_tr_list.DataSource = ds.Tables[0];
        gv_tr_list.DataBind();

        //图A
        DataTable dt_chartA = ds.Tables[1];
        ChartA.Series.Clear();

        List<Series> list = new List<Series>();
        Series series = new Series("昆山库存金额", ViewType.Pie);
        for (int i = 1; i <= 8; i++)
        {
            string argument = dt_chartA.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartA.Rows[0][i].ToString());//参数值
            series.Points.Add(new SeriesPoint(argument, value));

        }
        series.ArgumentScaleType = ScaleType.Qualitative;
        list.Add(series);
        ChartA.Series.AddRange(list.ToArray());
        ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        //grid 3
        gv_tr_list_3.Columns.Clear();
        gv_tr_list_3.AutoGenerateColumns = true;
        gv_tr_list_3.KeyFieldName = "typedesc;days";
        gv_tr_list_3.DataSource = ds.Tables[2];
        gv_tr_list_3.DataBind();
        gv_tr_list_3.Columns["tr_domain"].Visible = false;
        gv_tr_list_3.Columns["sort"].Visible = false;
        gv_tr_list_3.Columns["typedesc"].Caption = "分类";
        gv_tr_list_3.Columns["days"].Caption = "在库天数";

        //图C
        DataTable dt_chartC = ds.Tables[3];
        ChartC.Series.Clear();

        List<Series> listC = new List<Series>();
        Series seriesC = new Series("昆山库存金额", ViewType.Pie);
        for (int i = 4; i < dt_chartC.Columns.Count; i++)
        {
            string argument = dt_chartC.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt_chartC.Rows[0][i].ToString());//参数值
            seriesC.Points.Add(new SeriesPoint(argument, value));

        }
        seriesC.ArgumentScaleType = ScaleType.Qualitative;
        listC.Add(seriesC);
        ChartC.Series.AddRange(listC.ToArray());
        ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
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
                        e.Row.Cells[i - 1].Text = Convert.ToString(e.GetValue("amount" + i.ToString())) + "%";
                    }
                }

                if (e.GetValue("ld_qty_oh_amount") != DBNull.Value)
                {
                    e.Row.Cells[8].Text = Convert.ToString(e.GetValue("ld_qty_oh_amount")) + "00%";
                }
            }
        }
    }
    protected void gv_tr_list_3_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //if (e.KeyValue.ToString().Contains("rate"))
            //{
            //    for (int i = 1; i < gv_tr_list.Columns.Count - 1; i++)
            //    {
            //        if (e.GetValue("amount" + i.ToString()) != DBNull.Value)
            //        {
            //            e.Row.Cells[i - 1].Text = Convert.ToString(e.GetValue("amount" + i.ToString())) + "%";
            //        }
            //    }

            //    if (e.GetValue("ld_qty_oh_amount") != DBNull.Value)
            //    {
            //        e.Row.Cells[8].Text = Convert.ToString(e.GetValue("ld_qty_oh_amount")) + "00%";
            //    }
            //}

            if (e.KeyValue.ToString().Contains("百分比"))
            {
                DataTable ldt = Pgi.Auto.Control.AgvToDt(gv_tr_list_3);
                for (int i = 4; i < ldt.Columns.Count; i++)
                {
                    if (ldt.Columns[i].ColumnName != "flag")
                    {
                        if (e.GetValue(ldt.Columns[i].ColumnName) != DBNull.Value)
                        {
                            //e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(ldt.Columns[i].ColumnName)) * 100) + "%";
                        }
                    }
                }
            }
        }
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