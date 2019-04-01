using DevExpress.Utils;
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
using Aspose.Cells;

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
        Series series = new Series("昆山库存金额", DevExpress.XtraCharts.ViewType.Pie);
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
        Series seriesB = new Series("金额", DevExpress.XtraCharts.ViewType.Bar);
        Series seriesB_2 = new Series("金额占比", DevExpress.XtraCharts.ViewType.Line); 
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
        Series seriesD = new Series("金额", DevExpress.XtraCharts.ViewType.Line);
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
        Series seriesC = new Series("昆山库存30-180金额", DevExpress.XtraCharts.ViewType.Pie);
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
        Series seriesE = new Series("昆山库存超180金额", DevExpress.XtraCharts.ViewType.Pie);
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


    /// <summary>
    /// 将DataTable生成Excel
    /// </summary>
    /// <param name="dtList">DataTable</param>
    /// <param name="fileName">文件名</param>
    /// <returns>返回文件路径名</returns>
    #region DataTable生成Excel
    public string ExportToExcel(DataTable dtList, string fileName)
    {
        string ExportFilesPath = MapPath("~") + "ExportFile\\Kulin" + "\\";

        //这里是利用Aspose.Cells.dll 生成excel文件的
        string pathToFiles = System.Web.HttpContext.Current.Server.MapPath(ExportFilesPath);
        string etsName = ".xls";
        //获取保存路径
        string path = pathToFiles + fileName + etsName;
        Workbook wb = new Workbook();
        Worksheet ws = wb.Worksheets[0];
        Cells cell = ws.Cells;

        //设置行高
        //cell.SetRowHeight(0, 20);

        //表头样式
        Aspose.Cells.Style stHeadLeft = wb.Styles[wb.Styles.Add()];
        stHeadLeft.HorizontalAlignment = TextAlignmentType.Left;       //文字居中
        stHeadLeft.Font.Name = "宋体";
        stHeadLeft.Font.IsBold = true;                                 //设置粗体
        stHeadLeft.Font.Size = 14;                                     //设置字体大小
        Aspose.Cells.Style stHeadRight = wb.Styles[wb.Styles.Add()];
        stHeadRight.HorizontalAlignment = TextAlignmentType.Right;       //文字居中
        stHeadRight.Font.Name = "宋体";
        stHeadRight.Font.IsBold = true;                                  //设置粗体
        stHeadRight.Font.Size = 14;                                      //设置字体大小

        //内容样式
        Aspose.Cells.Style stContentLeft = wb.Styles[wb.Styles.Add()];
        stContentLeft.HorizontalAlignment = TextAlignmentType.Left;
        stContentLeft.Font.Size = 10;
        Aspose.Cells.Style stContentRight = wb.Styles[wb.Styles.Add()];
        stContentRight.HorizontalAlignment = TextAlignmentType.Right;
        stContentRight.Font.Size = 10;

        //赋值给Excel内容
        for (int col = 0; col < dtList.Columns.Count; col++)
        {
            //Style stHead = null;
            ////Style stContent = null;
            ////设置表头
            //string columnType = dtList.Columns[col].DataType.ToString();
            //switch (columnType.ToLower())
            //{
            //    //如果类型是string，则靠左对齐(对齐方式看项目需求修改)
            //    case "system.string":
            //        stHead = stHeadLeft;
            //        //stContent = stContentLeft;
            //        break;
            //    default:
            //        stHead = stHeadRight;
            //        //stContent = stContentRight;
            //        break;
            //}
            putValue(cell, dtList.Columns[col].ColumnName, 0, col);

            for (int row = 0; row < dtList.Rows.Count; row++)
            {
                putValue(cell, dtList.Rows[row][col], row + 1, col);
            }
        }
        wb.Save(path);

        return ExportFilesPath + fileName + etsName;
    }
    #endregion

    private static void putValue(Cells cell, object value, int row, int column)
    {
        //填充数据到excel中
        cell[row, column].PutValue(value);
        // cell[row, column].SetStyle(st);
    }


    protected void btn_export_ServerClick(object sender, EventArgs e)
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataTable dt = DbHelperSQL.Query("exec [Report_tr_hist_new] '6','" + ddl_comp.SelectedValue + "','" + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "',''").Tables[0];

        string filename = ExportToExcel(dt, "30-180天库存清单");
    }

    protected void btn_export2_ServerClick(object sender, EventArgs e)
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataTable dt = DbHelperSQL.Query("exec [Report_tr_hist_new] '6','" + ddl_comp.SelectedValue + "','" + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "',''").Tables[0];

        string filename = ExportToExcel(dt, "超180天库存清单");
    }
}