using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using DevExpress.Charts;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;
using System.Drawing;
using DevExpress.Utils;
using System.Globalization;

public partial class Wuliu_Wuliu_Container_Rate_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //页面Loading 初始，默认显示当天，含价格的数据
            this.txt_date.Value = DateTime.Now.ToString();
            DateTime dt = DateTime.Now;
            int year = dt.Year;
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(Convert.ToDateTime(this.txt_date.Value.ToString()), CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            this.txt_week.Value = year.ToString()+"W"+weekOfYear.ToString();
            Query(this.txt_date.Value, "1");
            setGridLink_Month();
            setGridLink_Week();
            LinkBtnWeek.Style.Add("display", "none");
            LinkBtnMonth.Style.Add("display", "none");
        }
    }
    /// <summary>
    /// 根据日历挑选的日期 给界面的年周赋值
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]//或[WebMethod(true)]
    public static string GetYearWeek(string date)
    {
        GregorianCalendar gc = new GregorianCalendar();
        int weekOfYear = gc.GetWeekOfYear(Convert.ToDateTime(date), CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        int year = Convert.ToDateTime(date).Year;
        return year.ToString()+"W"+weekOfYear.ToString();
    }
    /// <summary>
    /// 查询按钮触发事件，显示周，月综合率  但是不显示明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {

        Query(this.txt_date.Value.ToString(),this.ddl_type.SelectedValue.ToString());
        setGridLink_Month();
        setGridLink_Week();
    }

    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {

    }

    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    /// <summary>
    /// 格式化Gridview的数值为百分比
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewMonth_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i <= 8; i++)
            {
                e.Row.Cells[i].Text =string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                if (e.Row.Cells[i].Text == "0%")
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("0%", "—");
                }
            }
        }
    }
    /// <summary>
    /// 格式化Gridview的数值为百分比
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewWeek_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i <= 7; i++)
            {
                e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                if (e.Row.Cells[i].Text == "0%")
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("0%", "—");
                }
            }
        }
    }

    /// <summary>
    /// 格式化Gridview的数值为百分比
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewSummarys_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i <= 6; i++)
            {
                if (i!=4)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
            }
        }
    }
    /// <summary>
    /// 给月综合利用率报表添加链接
    /// </summary>
    public void setGridLink_Month()
    {
        GridViewRow dr = GridViewMonth.HeaderRow;
        for (int i = 0; i < GridViewMonth.Rows.Count; i++)
        {
            GridViewRow row = (GridViewRow)GridViewMonth.Rows[i];
            for (int j = 3; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM"+j.ToString();
                lbtn.Text = GridViewMonth.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnMonth','')");
                lbtn.Attributes.Add("name", "mon");

                string strMon = dr.Cells[j].Text;//获取月份
                string strMonJzx = Server.HtmlEncode(row.Cells[0].Text); //获取集装箱

                lbtn.Attributes.Add("mon", strMon);
                lbtn.Attributes.Add("Jzx_month", strMonJzx);

                GridViewMonth.Rows[i].Cells[j].Controls.Add(lbtn);
                GridViewMonth.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }
    public void setGridLink_Week()
    {
        GridViewRow dr = GridViewWeek.HeaderRow;
        for (int i = 0; i < GridViewWeek.Rows.Count; i++)
        {
            GridViewRow row = (GridViewRow)GridViewWeek.Rows[i];
            for (int j = 3; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnW" + j.ToString();
                lbtn.Text = GridViewWeek.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnWeek','')");
                lbtn.Attributes.Add("name", "weeks");

                string strWeek = dr.Cells[j].Text;//获取周
                string strWeekJzx = Server.HtmlEncode(row.Cells[0].Text); //获取集装箱

                lbtn.Attributes.Add("weeks", strWeek);
                lbtn.Attributes.Add("Jzx_week", strWeekJzx);
                GridViewWeek.Rows[i].Cells[j].Controls.Add(lbtn);
                GridViewWeek.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }
    /// <summary>
    /// 显示Chart图形 周报
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_Week(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][0].ToString(), ViewType.Line, dt, j));
            j++;
        }
       #endregion
        this.WebChartControl1.Series.AddRange(list.ToArray());
        this.WebChartControl1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    /// <summary>
    /// 显示Chart图形 月报
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_Month(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            list.Add(CreateSeries_month(dt.Rows[i][0].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.WebChartControl2.Series.AddRange(list.ToArray());
        this.WebChartControl2.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
    {
        Series series = new Series(caption, viewType);
        for (int i = 3; i < dt.Columns.Count; i++)
        {
           string argument = dt.Columns[i].ColumnName;//参数名称 
           decimal value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
           series.Points.Add(new SeriesPoint(argument, value));
        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }
    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    private Series CreateSeries_month(string caption, ViewType viewType, DataTable dt, int rowIndex)
    {
        Series series = new Series(caption, viewType);
        for (int i = 3; i < dt.Columns.Count-1; i++)
        {
            string argument = dt.Columns[i].ColumnName;//参数名称 
            decimal value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
            series.Points.Add(new SeriesPoint(argument, value));
        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }
    public void Query(string datetimes, string js_type)
    {   //清空两个图表的数据源                                                     
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        //清空明细
        GridViewSummarys.DataSource = null;
        GridViewSummarys.DataBind();
        Wuliu_Container_Rate_sql WuliuSQLHelp = new Wuliu_Container_Rate_sql();
        DataSet ds = WuliuSQLHelp.Get_jzxlyl_rate_query(datetimes, js_type);
        //加载周综合率信息
        GridViewWeek.DataSource = ds.Tables[0];
        GridViewWeek.DataBind();
        //加载月综合率信息
        GridViewMonth.DataSource = ds.Tables[1];
        GridViewMonth.DataBind();
        //图表显示
        CreateChart_Week(ds.Tables[0]);
        CreateChart_Month(ds.Tables[1]);
    }

protected void LinkBtn_Click(object sender, EventArgs e)
    {

    }
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["connstringMoJu"];
        SqlConnection connection = new SqlConnection(connString);
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
            command.Fill(ds, "ds");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;


    }
    
    protected void LinkBtnWeek_Click(object sender, EventArgs e)
    {
        setGridLink_Week();
        setGridLink_Month();
        string jzx = txtweekjzx.Text; //获取集装箱
        string week = txtweeks.Text;//获取周份
        Wuliu_Container_Rate_sql WuliuSQLHelp = new Wuliu_Container_Rate_sql();
        DataSet ds = WuliuSQLHelp.Get_jzxlyl_rate_datail(week, jzx, "周", this.ddl_type.SelectedValue);
        this.GridViewSummarys.DataSource = ds.Tables[0];
        GridViewSummarys.DataBind();
        //将点击的该单元格颜色显示黄色

    }
    /// <summary>
    /// 点击月综合率报表 链接查看明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkBtnMonth_Click(object sender, EventArgs e)
    {
        setGridLink_Month();
        setGridLink_Week();
        string jzx = txtmonjzx.Text; //获取集装箱
        string month = txtmonth.Text;//获取月份
        Wuliu_Container_Rate_sql WuliuSQLHelp = new Wuliu_Container_Rate_sql();
        DataSet ds = WuliuSQLHelp.Get_jzxlyl_rate_datail(month, jzx,"月",this.ddl_type.SelectedValue);
        this.GridViewSummarys.DataSource= ds.Tables[0];
        GridViewSummarys.DataBind();
    }
   
    
}