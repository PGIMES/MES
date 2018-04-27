using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using System.Globalization;

public partial class MaterialBase_DJ_LY_QS : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMstS.Text = "按周统计刀具领用金额";
        QueryYear();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryYear();
        lblMstS.Text = "按" + drop_period.SelectedItem.Text + "统计刀具领用金额";
    }

    public void QueryYear()
    {
        
        DataSet ds = DbHelperSQL.Query("exec  usp_DJLY_QS '" + ddl_comp.SelectedValue + "','" + drop_period.SelectedValue + "','" + drop_dept.SelectedValue + "'");
        //bindChartByYear(ds.Tables[0]);
        ChartA.Series.Clear();
        CreateChartA(ds.Tables[0]);

        DataSet ds_djlx = DbHelperSQL.Query("exec  usp_DJLY_QS_ByDjlx_xg '" + ddl_comp.SelectedValue + "','" + drop_period.SelectedValue + "','" + drop_dept.SelectedValue + "'");
       // bindChartByDJLX(ds_djlx.Tables[0]);
        ChartB.Series.Clear();
        CreateChartB(ds_djlx.Tables[0]);

        DataSet ds_cpdl = DbHelperSQL.Query("exec  usp_DJLY_QS_ByCPDL_XG '" + ddl_comp.SelectedValue + "','" + drop_period.SelectedValue + "','" + drop_dept.SelectedValue + "'");
        ChartC.Series.Clear();
        CreateChartC(ds_cpdl.Tables[0]);


        DataSet ds_gys = DbHelperSQL.Query("exec  usp_DJLY_QS_ByGYS_XG '" + ddl_comp.SelectedValue + "','" + drop_period.SelectedValue + "','" + drop_dept.SelectedValue + "'");
        ChartD.Series.Clear();
        CreateChartD(ds_gys.Tables[0]);

    }
    //public void bindChartByDJLX(DataTable tbl)
    //{

    //    ChartDJLX.DataSource = tbl;
    //    ChartDJLX.Series["刀具类型"].XValueMember = "type";
    //    ChartDJLX.Series["刀具类型"].YValueMembers = "金额";

    //    ChartDJLX.DataBind();
    //    for (int i = 0; i < tbl.Rows.Count; i++)
    //    {
    //        this.ChartDJLX.Series["刀具类型"].Points[i].AxisLabel = tbl.Rows[i]["type"].ToString();
    //        this.ChartDJLX.Series["刀具类型"].Points[i].ToolTip = tbl.Rows[i]["type"].ToString() + "\n" +"金额:"+ string.Format("{0:N0}", Convert.ToDecimal(tbl.Rows[i][2].ToString()))+"\n" + "比例:" + tbl.Rows[i][4].ToString();

    //    }
    //}

    //public void bindChartByCPDL(DataTable tbl)
    //{

    //    Chartcpdl.DataSource = tbl;
    //    Chartcpdl.Series["产品大类"].XValueMember = "dl";
    //    Chartcpdl.Series["产品大类"].YValueMembers = "金额";

    //    Chartcpdl.DataBind();
    //    for (int i = 0; i < tbl.Rows.Count; i++)
    //    {
    //        this.Chartcpdl.Series["产品大类"].Points[i].AxisLabel = tbl.Rows[i]["dl"].ToString();
    //        this.Chartcpdl.Series["产品大类"].Points[i].ToolTip = tbl.Rows[i]["dl"].ToString() + "\n" + "金额:" + string.Format("{0:N0}", Convert.ToDecimal(tbl.Rows[i][2].ToString())) + "\n" + "比例:" + tbl.Rows[i][4].ToString();

    //    }
    //}

    //public void bindChartByGYS(DataTable tbl)
    //{

    //    ChartGYS.DataSource = tbl;
    //    ChartGYS.Series["供应商"].XValueMember = "gys";
    //    ChartGYS.Series["供应商"].YValueMembers = "金额";
    //    this.ChartGYS.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

    //    ChartGYS.DataBind();
    //    for (int i = 0; i < tbl.Rows.Count; i++)
    //    {
    //        this.ChartGYS.Series["供应商"].Points[i].AxisLabel = tbl.Rows[i]["gys"].ToString();
    //        this.ChartGYS.Series["供应商"].Points[i].ToolTip = tbl.Rows[i]["gys"].ToString() + "\n" + "金额:" + string.Format("{0:N0}", Convert.ToDecimal(tbl.Rows[i][2].ToString())) + "\n" + "比例:" + tbl.Rows[i][4].ToString();

    //    }
    //}

    

    private void CreateChartA(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 2; i < dt.Columns.Count; i++)
        {
            list.Add(CreateSeries(dt.Columns[i].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
       
    }

    private void CreateChartB(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 0; i < dt.Columns.Count-4; i++)
        {
            list.Add(CreateSeries(dt.Columns[j].ToString(), ViewType.Bar, dt, j));
            j++;
        }
        #endregion
        this.ChartB.Series.AddRange(list.ToArray());
        this.ChartB.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    private void CreateChartC(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 0; i < dt.Columns.Count - 4; i++)
        {
            list.Add(CreateSeries(dt.Columns[j].ToString(), ViewType.Bar, dt, j));
            j++;
        }
        #endregion
        this.ChartC.Series.AddRange(list.ToArray());
        this.ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    private void CreateChartD(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 0; i < dt.Columns.Count - 4; i++)
        {
            list.Add(CreateSeries(dt.Columns[j].ToString(), ViewType.Bar, dt, j));
            j++;
        }
        #endregion
        this.ChartD.Series.AddRange(list.ToArray());
        ((XYDiagram)ChartD.Diagram).AxisX.Label.Angle =350;
        
        this.ChartD.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int index)
    {
        Series series = new Series(caption, viewType);
        decimal value = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string argument = dt.Rows[i][1].ToString();//参数名称 

            value = string.IsNullOrEmpty(dt.Rows[i][index].ToString()) == true ? 0 : Convert.ToDecimal(dt.Rows[i][index].ToString());

            series.Points.Add(new SeriesPoint(argument, value));


        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }
  
}