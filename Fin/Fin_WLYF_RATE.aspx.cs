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

public partial class Fin_Fin_WLYF_RATE : System.Web.UI.Page
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
        string year = ddl_year.SelectedValue;
        string sql = @"exec [Report_WLYF_RATE] '{0}'";
        sql = string.Format(sql, year);
        DataSet ds = DbHelperSQL.Query(sql);

        DataTable dt = ds.Tables[0];
        DataTable dt_1 = ds.Tables[1];

        string year_r = year.Right(2);
        dt.Columns["col1"].ColumnName = "Jan/" + year_r; dt.Columns["col2"].ColumnName = "Feb/" + year_r;
        dt.Columns["col3"].ColumnName = "Mar/" + year_r; dt.Columns["col4"].ColumnName = "Apr/" + year_r;
        dt.Columns["col5"].ColumnName = "May/" + year_r; dt.Columns["col6"].ColumnName = "Jun/" + year_r;
        dt.Columns["col7"].ColumnName = "Jul/" + year_r; dt.Columns["col8"].ColumnName = "Aug/" + year_r;
        dt.Columns["col9"].ColumnName = "Sep/" + year_r; dt.Columns["col10"].ColumnName = "Oct/" + year_r;
        dt.Columns["col11"].ColumnName = "Nov/" + year_r; dt.Columns["col12"].ColumnName = "Dec/" + year_r;

        dt_1.Columns["col1"].ColumnName = "Jan/" + year_r; dt_1.Columns["col2"].ColumnName = "Feb/" + year_r;
        dt_1.Columns["col3"].ColumnName = "Mar/" + year_r; dt_1.Columns["col4"].ColumnName = "Apr/" + year_r;
        dt_1.Columns["col5"].ColumnName = "May/" + year_r; dt_1.Columns["col6"].ColumnName = "Jun/" + year_r;
        dt_1.Columns["col7"].ColumnName = "Jul/" + year_r; dt_1.Columns["col8"].ColumnName = "Aug/" + year_r;
        dt_1.Columns["col9"].ColumnName = "Sep/" + year_r; dt_1.Columns["col10"].ColumnName = "Oct/" + year_r;
        dt_1.Columns["col11"].ColumnName = "Nov/" + year_r; dt_1.Columns["col12"].ColumnName = "Dec/" + year_r;

        SetGrid(gv, dt, 80, "id");

        Chart.Series.Clear();
        List<Series> list = new List<Series>();
        if (dt_1.Rows.Count >= 1)
        {
            for (int row = 0; row < dt_1.Rows.Count - 1; row++)//最后一行合计排除
            {
                Series seriesA = new Series(dt_1.Rows[row]["part_hx"].ToString(), DevExpress.XtraCharts.ViewType.Bar);
                for (int i = 7; i < dt_1.Columns.Count; i++)
                {
                    string argument = dt_1.Columns[i].ColumnName;//参数名称 
                    decimal value = Convert.ToDecimal(dt_1.Rows[row][i].ToString() == "" ? "0" : dt_1.Rows[row][i].ToString());//参数值
                    seriesA.Points.Add(new SeriesPoint(argument, value));
                }
                seriesA.ArgumentScaleType = ScaleType.Qualitative;

                BarSeriesView sv1 = (BarSeriesView)seriesA.View;
                sv1.FillStyle.FillMode = FillMode.Solid;//    solid 实线，gradient 渐变，hatch 斜形
                switch (dt_1.Rows[row]["part_hx"].ToString())
                {
                    case "墨西哥":
                        sv1.Color = System.Drawing.Color.OrangeRed;
                        break;
                    case "欧洲线":
                        sv1.Color = System.Drawing.Color.DarkBlue;
                        break;
                    case "美西":
                        sv1.Color = System.Drawing.Color.DarkRed;
                        break;
                    case "Overall":
                        sv1.Color = System.Drawing.Color.White;
                        break;
                    default:
                        break;
                }
                list.Add(seriesA);
            }
        }
        

        Chart.Series.AddRange(list.ToArray());
        Chart.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

       
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

            if (lcolumn.FieldName == "part_hx") { lcolumn.Caption = "航线"; lnwidth_emp = 60; }
            if (lcolumn.FieldName == "part") { lcolumn.Caption = "集装箱"; lnwidth_emp = 80; }
            if (lcolumn.FieldName == "part_ms") { lcolumn.Caption = "集装箱描述"; lnwidth_emp = 110; }
            if (lcolumn.FieldName == "part_gg") { lcolumn.Caption = "规格"; lnwidth_emp = 40; }

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
            lcolumn.PropertiesTextEdit.DisplayFormatString = "{0:N2}";
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

        string tp = Convert.ToString(e.GetValue("tp"));
        string domain = Convert.ToString(e.GetValue("domain"));
        string part_hx = Convert.ToString(e.GetValue("part_hx"));

        if (part_hx == "合计")
        {
            e.Row.Style.Add("background-color", "LightGrey");
            e.Row.Style.Add("font-weight", "800");
        }

        if (domain == "" && tp != "比例" && tp != "Overall")
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                string fieldname = ((DevExpress.Web.GridViewDataColumn)gv.Columns[i]).FieldName;
                if (fieldname.Contains("/"))
                {
                    if (e.GetValue(fieldname) != DBNull.Value)
                    {
                        e.Row.Cells[i].Text = (Convert.ToSingle(e.GetValue(fieldname))).ToString("N0");
                    }
                }

            }
        }

        if (tp == "比例" || tp == "Overall")
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                string fieldname = ((DevExpress.Web.GridViewDataColumn)gv.Columns[i]).FieldName;
                if (fieldname.Contains("/"))
                {
                    if (e.GetValue(fieldname) != DBNull.Value)
                    {
                        e.Row.Cells[i].Text = Convert.ToString(Convert.ToSingle(e.GetValue(fieldname))) + "%";
                    }
                }

            }
        }
    }

}