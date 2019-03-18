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
using DevExpress.XtraCharts;

public partial class Product_Chanpin_ForcastByMonth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            //for (int i = 0; i < 5; i++)
            //{
            //    dropYear.Items.Add(new ListItem((year + i).ToString(), (year + i).ToString()));
            //}

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 6;
            int chaYear = intYear - 2016;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (2016 + i).ToString();
            }
            dropYear.DataSource = Yearlist;
            dropYear.DataBind();
            dropYear.SelectedValue = Year;

            Query_year();


        }
    }

    public void Query_year()
    {
        ChartA.Series.Clear();
        DataSet ds;
        ds = DbHelperSQL.Query("exec rpt_Form3_Sale_Forcast_TJ_modify  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "'");

        DataTable dt = ds.Tables[0];
        //ViewState["footer"] = ds.Tables[3];
        // lblMstS.Text = dropYear.SelectedValue + "年";
        gv_month.DataSource = dt;
        gv_month.DataBind();
        //ViewState["detail"] = ds.Tables[2];


        // setGridLink_gv_year();
        // bindChartByYear(ds.Tables[1]);
        CreateChart(ds.Tables[1]);
        QueryGridView2();
    }
    public void QueryGridView2()
    {

        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_ForcastDetail_modify  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "'");
        this.GridView2.DataSource = ds.Tables[0];
        this.GridView2.DataBind();
        GridView2.Visible = true;

        int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        MergGridRow.MergeRow(GridView2, cols);

        int rowIndex = 1;


        int Index = 1;
        for (int j = 0; j < GridView2.Rows.Count - 1; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[15].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[2].Controls.Add(link);
                Index++;
            }
        }
    }

    private void CreateChart(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 1;
        for (int i = 1; i < dt.Columns.Count; i++)
        {
            list.Add(CreateSeries(dt.Columns[i].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
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
            string argument = dt.Rows[i][0].ToString();//参数名称 

            value = string.IsNullOrEmpty(dt.Rows[i][index].ToString()) == true ? 0 : Convert.ToDecimal(dt.Rows[i][index].ToString());

            series.Points.Add(new SeriesPoint(argument, value));


        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Query_year();
    }
    protected void gv_month_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int count = e.Row.Cells.Count;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indexID = this.gv_month.PageIndex * this.gv_month.PageSize + e.Row.RowIndex + 1;
            if (indexID >= 3)
            {
                e.Row.Cells[16].Text = "";
            }



            for (int i = 2; i <= e.Row.Cells.Count - 2; i++)
            {
                if (e.Row.Cells[i].Text.Replace("&nbsp;", "") != "")
                {
                    e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));

                }

            }
            if (e.Row.Cells[e.Row.Cells.Count - 1].Text.Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[e.Row.Cells.Count - 1].Text));
            }

        }

    }
    protected void gv_month_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "";
            e.Row.Cells[16].Wrap = false;
        }
        else
        {
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[16].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }



        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int CurrMnth = Convert.ToInt16(DateTime.Now.Month.ToString());
        int CurrYear = Convert.ToInt16(DateTime.Now.Year.ToString());
        double diff = 0;
        double qty_rf = 0;
        double qty_f = 0;
        double qty_A = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Wrap = false;
            e.Row.Cells[1].Wrap = false;
            DataRowView dr = e.Row.DataItem as DataRowView;

            //添加可点击Link 及前端识别属性：name
            for (int i = 13; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[13].Text.Replace(",", "") == "999999999")
                {
                    e.Row.Cells[13].Text = "小计";
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
                if (i == 13 && e.Row.Cells[i].Text != "小计")
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                GridViewRow hr = GridView2.HeaderRow;

                if (hr.Cells[i].Controls.Count > 1)
                {
                    string headValue = ((Label)hr.Cells[i].Controls[1]).Text.Replace("月", "");//月份
                   
                    qty_rf = Convert.ToDouble(((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Text.Replace(",", ""));
                    qty_A = Convert.ToDouble(((Label)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Text.Replace(",", ""));
                    qty_f = Convert.ToDouble(((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).Text.Replace(",", ""));
                    diff = (Math.Abs((qty_rf - qty_f) / (qty_f == 0 ? 1 : qty_f)));
                    
                    if (CurrYear == Convert.ToInt16(dropYear.SelectedValue))
                    {//大于当前月时实际值不显示
                        if (Convert.ToInt16(headValue) > CurrMnth)
                        {
                            ((Label)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("A" + headValue)).Visible = false;
                            ((Label)hr.Cells[i].FindControl("lb_A_" + headValue)).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)hr.Cells[i].Controls[1].FindControl("LA" + headValue)).Visible = false;
                           
                        }
                        if (CurrMnth >= 2 && CurrMnth < 11)
                        {
                            if (Convert.ToInt16(headValue) > CurrMnth + 2)
                            {
                                ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;
                                ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;

                            }
                            if (Convert.ToInt16(headValue) < CurrMnth - 1)
                            {
                                ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).Visible = false;
                                ((Label)hr.Cells[i].FindControl("lb_F_" + headValue)).Visible = false;
                                ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                                ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("F" + headValue)).Visible = false;

                            }
                        }
                        else if (CurrMnth >= 11 && Convert.ToInt16(headValue) < 9)
                        {
                            ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).Visible = false;
                            ((Label)hr.Cells[i].FindControl("lb_F_" + headValue)).Visible = false;
                            ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                            ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("F" + headValue)).Visible = false;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt16(headValue) > 4 && CurrMnth < 11)
                            {
                                ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                                ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;
                                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;
                            }
                        }

                    }
                    else if (Convert.ToInt16(dropYear.SelectedValue) > CurrYear)
                    {

                        ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                        ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;
                        ((Label)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Visible = false;
                        ((Label)hr.Cells[i].FindControl("lb_A_" + headValue)).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("A" + headValue)).Visible = false;

                    }
                    else if (Convert.ToInt16(dropYear.SelectedValue) < CurrYear)
                    {

                        ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).Visible = false;
                        ((Label)hr.Cells[i].FindControl("lb_F_" + headValue)).Visible = false;
                        ((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = false;
                        ((Label)hr.Cells[i].FindControl("lb_RF_" + headValue)).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("F" + headValue)).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).Visible = false;

                    }
                    if (((Label)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible ==true)
                    {
                        //if ((Math.Abs((qty_rf - qty_f) / (qty_f == 0 ? 1 : qty_f)) > 0.25 || Math.Abs((qty_f - qty_rf) / (qty_rf == 0 ? 1 : qty_rf)) > 0.25) && e.Row.Cells[1].Text != "合计(万)")
                        //{
                        //   // ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).BackColor = System.Drawing.Color.Red;
                        //    ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("F" + headValue)).BgColor = "red";
                        //}
                    }
                    if (((Label)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Visible == true)
                    {
                        if ((Math.Abs((qty_A - qty_f) / (qty_f == 0 ? 1 : qty_f)) > 0.25 || Math.Abs((qty_f - qty_A) / (qty_A == 0 ? 1 : qty_A)) > 0.25) && e.Row.Cells[1].Text != "合计(万)")
                        {
                            // ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).BackColor = System.Drawing.Color.Red;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("F" + headValue)).BgColor = "red";
                        }
                        if ((Math.Abs((qty_A - qty_rf) / (qty_rf == 0 ? 1 : qty_rf)) > 0.25 || Math.Abs((qty_rf - qty_A) / (qty_A == 0 ? 1 : qty_A)) > 0.25) && e.Row.Cells[1].Text != "合计(万)")
                        {
                            // ((Label)e.Row.Cells[i].FindControl("QTY_F_" + headValue)).BackColor = System.Drawing.Color.Red;
                            ((System.Web.UI.HtmlControls.HtmlTableCell)e.Row.Cells[i].FindControl("RF" + headValue)).BgColor = "red";
                        }
                    }

                }

            }

        }

        if (e.Row.Cells[1].Text == "合计(万)")
        {
            e.Row.BackColor = System.Drawing.Color.BlanchedAlmond;
            e.Row.Cells[13].Text = "";
            e.Row.Cells[2].Text = "";
            e.Row.Cells[0].Text = "合计(万)";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[14].Text = "";

        }

    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        string lsname = "月销售预测";
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_ForcastDetail_export '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "'");
        DataTableToExcel(ds.Tables[0], "xls", lsname, "1");
    }
    public void DataTableToExcel(DataTable dt, string FileType, string FileName, string b_head)
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Charset = "UTF-8";
        System.Web.HttpContext.Current.Response.Buffer = true;
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
        System.Web.HttpContext.Current.Response.ContentType = FileType;
        string colHeaders = string.Empty;



        string ls_item = string.Empty;
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        if (b_head == "1")
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ls_item += dt.Columns[j].ColumnName + "\t";
            }
        }
        ls_item += "\n";
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    ls_item += row[i].ToString() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString() + "\t";
                }
            }
            System.Web.HttpContext.Current.Response.Output.Write(ls_item);
            ls_item = string.Empty;
        }

        System.Web.HttpContext.Current.Response.Output.Flush();
        System.Web.HttpContext.Current.Response.End();
    }
}
