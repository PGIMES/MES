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
using System.IO;

public partial class Wuliu_Qad_Report_tr_hist_new_query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Setddl_p_status();

        if (!IsPostBack)
        {
            ddl_year.SelectedValue = DateTime.Now.AddMonths(-1).ToString("yyyy"); //获取上月年份
            ddl_month.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM"); //获取上月月份
            QueryASPxGridView();
        }
    }
    public void Setddl_p_status()
    {
        string strSQL = @" select distinct case when isnull(pt_status,'')='' then '当前无状态' else pt_status end pt_status 
                        from qad.dbo.qad_pt_mstr where pt_domain='" + ddl_comp.SelectedValue + "'  order by pt_status";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).TextField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataBind();
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void ddl_comp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxDropDownEdit2.Text = "";
        Setddl_p_status();
    }

    public void QueryASPxGridView()
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataSet ds = DbHelperSQL.Query("exec [Report_tr_hist_new] '5','" + ddl_comp.SelectedValue + "','" 
            + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "','" + ASPxDropDownEdit2.Value + "'");

        //grid A
        gv_tr_list.DataSource = ds.Tables[0];
        gv_tr_list.DataBind();

        //图A
        ds.Tables[1].Columns["amount1"].ColumnName = "10以内金额"; ds.Tables[1].Columns["amount2"].ColumnName = "10-20金额";
        ds.Tables[1].Columns["amount3"].ColumnName = "20-30金额"; ds.Tables[1].Columns["amount4"].ColumnName = "30-60金额";
        ds.Tables[1].Columns["amount5"].ColumnName = "60-90金额"; ds.Tables[1].Columns["amount6"].ColumnName = "90-180金额";
        ds.Tables[1].Columns["amount7"].ColumnName = "180-360金额"; ds.Tables[1].Columns["amount8"].ColumnName = "360以上金额";

        DataTable dt_chartA = ds.Tables[1];
        ChartA.Series.Clear();

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

        //图A_1
        ds.Tables[0].Columns["amount1"].ColumnName = "10以内金额"; ds.Tables[0].Columns["amount2"].ColumnName = "10-20金额";
        ds.Tables[0].Columns["amount3"].ColumnName = "20-30金额"; ds.Tables[0].Columns["amount4"].ColumnName = "30-60金额";
        ds.Tables[0].Columns["amount5"].ColumnName = "60-90金额"; ds.Tables[0].Columns["amount6"].ColumnName = "90-180金额";
        ds.Tables[0].Columns["amount7"].ColumnName = "180-360金额"; ds.Tables[0].Columns["amount8"].ColumnName = "360以上金额";

        DataTable dt_chartA_1 = ds.Tables[0];
        if (ChartA_1.Diagram != null)
        {
            ((XYDiagram)ChartA_1.Diagram).SecondaryAxesY.Clear();
        }
        ChartA_1.Series.Clear();

        List<Series> listA_1 = new List<Series>();
        Series seriesA_1_1 = new Series("金额", DevExpress.XtraCharts.ViewType.Bar);
        Series seriesA_1_2 = new Series("金额占比", DevExpress.XtraCharts.ViewType.Line);
        for (int i = 1; i <=8; i++)
        {
            string argument = dt_chartA_1.Columns[i].ColumnName;//参数名称 

            decimal value = Convert.ToDecimal(dt_chartA_1.Rows[0][i].ToString());//参数值
            seriesA_1_1.Points.Add(new SeriesPoint(argument, value));

            decimal value_2 = Convert.ToDecimal(dt_chartA_1.Rows[1][i].ToString());//参数值
            seriesA_1_2.Points.Add(new SeriesPoint(argument, value_2));

        }
        seriesA_1_1.ArgumentScaleType = ScaleType.Qualitative;
        seriesA_1_2.ArgumentScaleType = ScaleType.Qualitative;

        listA_1.Add(seriesA_1_1); listA_1.Add(seriesA_1_2);

        ChartA_1.Series.AddRange(listA_1.ToArray());
        ChartA_1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

        var diagramA_1 = ((XYDiagram)ChartA_1.Diagram);
        SecondaryAxisY secondaryYAxisA_1 = new SecondaryAxisY("Population Axis");
        secondaryYAxisA_1.Label.TextPattern = "{VP:P0}";
        diagramA_1.SecondaryAxesY.Add(secondaryYAxisA_1);
        ((LineSeriesView)seriesA_1_2.View).AxisY = secondaryYAxisA_1;


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


    
    public void ExportToExcel(DataTable dtList, string fileName)
    {
        string pathToFiles = MapPath("~") + @"ExportFile\Kulin" + @"\";
        string etsName = ".xls";
        string path = pathToFiles + fileName + etsName; //获取保存路径

        Workbook wb = new Workbook();
        Worksheet ws = wb.Worksheets[0];
        Cells cell = ws.Cells;

        //设置行高
        cell.SetRowHeight(0, 20);

        Aspose.Cells.Style style_head = wb.Styles[wb.Styles.Add()];
        style_head.ForegroundColor = System.Drawing.Color.FromArgb(0, 176, 240);
        style_head.Pattern = BackgroundType.Solid;
        style_head.Font.IsBold = true;
        style_head.Font.Name = "宋体";
        style_head.Font.Size = 11;
        style_head.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style_head.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style_head.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style_head.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

        Aspose.Cells.Style style_con = wb.Styles[wb.Styles.Add()];
        style_con.Font.Name = "宋体";
        style_con.Font.Size = 11;
        style_con.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style_con.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style_con.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style_con.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

        //赋值给Excel表头
        for (int col = 0; col < dtList.Columns.Count; col++)
        {
            cell[0, col].PutValue(dtList.Columns[col].ColumnName);
            cell[0, col].SetStyle(style_head);
            cell.SetColumnWidth(col, 15);

            if (dtList.Columns[col].ColumnName.Contains("原因"))
            {
                var s = cell[0, col].GetStyle();
                s.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 0);
                cell[0, col].SetStyle(s);
            }
        }

        //赋值给Excel内容
        for (int row = 0; row < dtList.Rows.Count; row++)
        {
            for (int col = 0; col < dtList.Columns.Count; col++)
            {
                cell[row + 1, col].PutValue(dtList.Rows[row][col]);
                cell[row + 1, col].SetStyle(style_con);
            }
        }

        wb.Save(path);

        //以字符流的形式下载文件
        FileStream fs = new FileStream(path, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        Response.ContentType = "application/octet-stream";
        //通知浏览器下载文件而不是打开
        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName + etsName, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
   


    protected void btn_export_ServerClick(object sender, EventArgs e)
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataTable dtList = DbHelperSQL.Query("exec [Report_tr_hist_new] '6','" + ddl_comp.SelectedValue + "','" 
            + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "','" + ASPxDropDownEdit2.Value + "'").Tables[0];

        ExportToExcel(dtList, "30-180天库存清单_"+ curmonth);
    }

    protected void btn_export2_ServerClick(object sender, EventArgs e)
    {
        string curmonth = ddl_year.SelectedValue + ddl_month.SelectedValue;
        DataTable dt = DbHelperSQL.Query("exec [Report_tr_hist_new] '7','" + ddl_comp.SelectedValue + "','" 
            + txt_site.Text.Trim() + "','" + txt_tr_part_start.Text.Trim() + "','" + curmonth + "','" + ASPxDropDownEdit2.Value + "'").Tables[0];

        ExportToExcel(dt, "超180天库存清单_" + curmonth);
    }
   
}