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
using Newtonsoft.Json;
using System.Web.Services;



public partial class Product_chanpin_saleforecast : System.Web.UI.Page
{
    decimal ntotal_Q1 = 0;
    decimal ntotal_Q2 = 0;
    decimal ntotal_Q3 = 0;
    decimal ntotal_Q4 = 0;
    decimal ntotal_Q5 = 0;
    decimal ntotal_Q6 = 0;
    decimal ntotal_Q7 = 0;
    decimal ntotal_Q8 = 0;
    decimal ntotal_Q9 = 0;
    decimal ntotal_Q10 = 0;
    decimal ntotal_Q11 = 0;
    decimal ntotal_Q12 = 0;
    decimal ypj = 0;
    decimal hj = 0;
    decimal zb = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 1000;
        this.GridView2.PageSize = 1000;
        this.GridView3.PageSize = 1000;
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
             for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add(new ListItem((year + i).ToString(), (year +i).ToString()));
            }
             dropYear.Items.Insert(0, new ListItem("全部", "全部"));
             Setddl_make_factory();
            //dropYear.SelectedValue = DateTime.Now.Year.ToString();
            Query_year();
            Query_mnth();
            Query_KH();
            Query_DL();
            Query_Detail();
            //if (dropYear.SelectedValue != "全部")
            //{
            //    Label1.Text = "按" + dropYear.SelectedValue + "月统计";
            //}
            //else
            //{
            //    Label1.Text = "按" + System.DateTime.Now.Year.ToString() + "月统计";
            //}




        }
    }


    public void Setddl_make_factory()
    {

        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct make_factory  as  status_id,make_factory as status from form3_Sale_Product_MainTable ";
        DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];

        if (ship_from.Rows.Count > 0)
        {
            fun.initDropDownList(this.dropsite, ship_from, "status_id", "status");
        }
        dropsite.Items.Insert(0, new ListItem("全部", "-1"));
    }
    public void Query_year()
    {
        DataSet ds;
        if (droptype.SelectedIndex != 2)
        {
             ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','0'");
        }
        else
        {
             ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1','0'");
        }
        DataTable dt = ds.Tables[0];
       // lblMstS.Text = dropYear.SelectedValue + "年";
        gv_year.DataSource = dt;
        gv_year.DataBind();
        ViewState["tblyear"] = dt;
       // setGridLink_gv_year();
        bindChartByYear(ds.Tables[1]);
       

    }

    public void Query_Detail()
    {
        DataSet ds;
        DataTable tmp;
        if (droptype.SelectedIndex != 2)
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ_ByDetail  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "'");
            

        }
        else//占比
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ_ByDetail  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1'");
           
        }

       

        tmp = ds.Tables[0];
        //若年份默认为全部，则按月份显示，否则显示年
        if (dropYear.SelectedValue != "全部")
        {
            QueryGridView2(tmp);
        }
        else
        {
           // QueryGridView1(dt);
            if (droptype.SelectedValue != "0")//金额/占比
            {
                QueryGridView3(tmp);
                setHeader(GridView3);
            }
            else if (droptype.SelectedValue == "0")//数量
            {
                QueryGridView2_ALL(tmp);
                setHeader(GridView2);
            }
            
        }
     


    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        Query_Detail();
    }
    protected void gv2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (dropYear.SelectedValue != "全部")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[26].Style.Add("display", "none");
                e.Row.Cells[25].Wrap = false;
                e.Row.Cells[30].Style.Add("display", "none");
                e.Row.Cells[31].Style.Add("display", "none");
                e.Row.Cells[32].Style.Add("display", "none");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[26].Style.Add("display", "none");
                e.Row.Cells[30].Style.Add("display", "none");
                e.Row.Cells[31].Style.Add("display", "none");
                e.Row.Cells[32].Style.Add("display", "none");
                e.Row.Cells[25].Wrap = false;
                for (int i = 11; i < 25; i++)
                {
                    if (i != 11)
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                    else
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                        if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == -1)
                        {
                            e.Row.Cells[i].Text = "小计";
                            e.Row.BackColor = System.Drawing.Color.LightYellow;
                        }
                    }
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                e.Row.Cells[27].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[27].Text)));
                e.Row.Cells[28].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[28].Text)));
                e.Row.Cells[29].Text = (string.Format("{0:P2}", Convert.ToDecimal(e.Row.Cells[29].Text)));

            }
            ////添加合计功能
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[26].Style.Add("display", "none");
                e.Row.Cells[30].Style.Add("display", "none");
                e.Row.Cells[31].Style.Add("display", "none");
                e.Row.Cells[32].Style.Add("display", "none");
                e.Row.Cells[12].Text = "合计";
                e.Row.Cells[13].Text = this.ntotal_Q1.ToString("N0");
                e.Row.Cells[14].Text = this.ntotal_Q2.ToString("N0");
                e.Row.Cells[15].Text = this.ntotal_Q3.ToString("N0");
                e.Row.Cells[16].Text = this.ntotal_Q4.ToString("N0");
                e.Row.Cells[17].Text = this.ntotal_Q5.ToString("N0");
                e.Row.Cells[18].Text = this.ntotal_Q6.ToString("N0");
                e.Row.Cells[19].Text = this.ntotal_Q7.ToString("N0");
                e.Row.Cells[20].Text = this.ntotal_Q8.ToString("N0");
                e.Row.Cells[21].Text = this.ntotal_Q9.ToString("N0");
                e.Row.Cells[22].Text = this.ntotal_Q10.ToString("N0");
                e.Row.Cells[23].Text = this.ntotal_Q11.ToString("N0");
                e.Row.Cells[24].Text = this.ntotal_Q12.ToString("N0");
                e.Row.Cells[27].Text = this.ypj.ToString("N0");
                e.Row.Cells[28].Text = this.hj.ToString("N0");
                e.Row.Cells[29].Text = this.zb.ToString("P2");
            }
        }
       
        else{
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Style.Add("word-break", "break-all");//零件号
            e.Row.Cells[4].Style.Add("width", "140px");
            e.Row.Cells[10].Style.Add("word-break", "break-all");//ship_to
            e.Row.Cells[10].Style.Add("width", "140");//ship_to
            e.Row.Cells[8].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[8].Style.Add("width", "120px");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");

            e.Row.Cells[1].Width = Convert.ToInt16(80);
            e.Row.Cells[2].Width = Convert.ToInt16(80);
            e.Row.Cells[3].Width = Convert.ToInt16(60);
            e.Row.Cells[4].Width = Convert.ToInt16(110);
            e.Row.Cells[5].Width = Convert.ToInt16(100);
            e.Row.Cells[6].Width = Convert.ToInt16(80);
            e.Row.Cells[7].Width = Convert.ToInt16(100);
            e.Row.Cells[8].Width = Convert.ToInt16(120);
            e.Row.Cells[9].Width = Convert.ToInt16(100);
            e.Row.Cells[10].Width = Convert.ToInt16(100);
            e.Row.Cells[24].Width = Convert.ToInt16(40);//更新
            e.Row.Cells[25].Width = Convert.ToInt16(50);

            for (int i = 11; i < 24; i++)
            {
                if (i != 11)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == 99999999)
                    {
                        e.Row.Cells[i].Text = "小计";
                        e.Row.Cells[i - 3].Text = ""; //顾客项目栏位改为空
                        e.Row.Cells[i - 1].Text = "";
                        e.Row.Style.Add("font-weight", "bold");
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            //如果该订单是停产状态，则整行显示灰色
            if (e.Row.Cells[2].Text.ToString().Trim() == "停产" || e.Row.Cells[2].Text.ToString().Trim() == "项目取消")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[2].Text.ToString().Trim() == "开发中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            if (e.Row.Cells[22].Text.ToString().Trim() != "" && e.Row.Cells[23].Text.ToString().Trim() != "")
            {
                if (Convert.ToDecimal(e.Row.Cells[23].Text) - Convert.ToDecimal(e.Row.Cells[22].Text) < 0)
                {
                    e.Row.Cells[23].BackColor = System.Drawing.Color.Red;
                }

            }



        }
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");
            e.Row.Cells[11].Text = "合计";
            e.Row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[13].Text = this.ntotal_Q2.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q3.ToString("N0");
            e.Row.Cells[15].Text = this.ntotal_Q4.ToString("N0");
            e.Row.Cells[16].Text = this.ntotal_Q5.ToString("N0");
            e.Row.Cells[17].Text = this.ntotal_Q6.ToString("N0");
            e.Row.Cells[18].Text = this.ntotal_Q7.ToString("N0");
            e.Row.Cells[19].Text = this.ntotal_Q8.ToString("N0");
            e.Row.Cells[20].Text = this.ntotal_Q9.ToString("N0");
            e.Row.Cells[21].Text = this.ntotal_Q10.ToString("N0");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");
        }
            }
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[24].Style.Add("display", "none");
           
          
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[24].Style.Add("display", "none");
           
            for (int i = 11; i < 23; i++)
            {
                if (i != 11)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == -1)
                    {
                        e.Row.Cells[i].Text = "小计";
                        e.Row.BackColor = System.Drawing.Color.LightYellow;
                    }
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
           
        }
        ////添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[24].Style.Add("display", "none");
            e.Row.Cells[12].Text = "合计";
            e.Row.Cells[13].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q2.ToString("N0");
            e.Row.Cells[15].Text = this.ntotal_Q3.ToString("N0");
            e.Row.Cells[16].Text = this.ntotal_Q4.ToString("N0");
            e.Row.Cells[17].Text = this.ntotal_Q5.ToString("N0");
            e.Row.Cells[18].Text = this.ntotal_Q6.ToString("N0");
            e.Row.Cells[19].Text = this.ntotal_Q7.ToString("N0");
            e.Row.Cells[20].Text = this.ntotal_Q8.ToString("N0");
            e.Row.Cells[21].Text = this.ntotal_Q9.ToString("N0");
            e.Row.Cells[22].Text = this.ntotal_Q10.ToString("N0");
        }

    }

    protected void gv_year_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        string year = dropYear.SelectedValue;
        if (year == "全部")
        {
            year = DateTime.Now.ToString("yyyy").ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow hr = gv_year.HeaderRow;
            if (e.Row.RowIndex == 3)
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text.Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }
            else
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text.Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }

                
            
        }
           

    }


    public void Query_mnth()
    {
        DataSet ds;
        if (droptype.SelectedIndex != 2)
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','6'");
        }
        else
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1','7'");
        }
        DataTable dt = ds.Tables[0];
       // lblMstS.Text = dropYear.SelectedValue + "年";
        gv_month.DataSource = dt;
        gv_month.DataBind();
        if (droptype.SelectedValue == "0" || droptype.SelectedValue == "1")
        {
            ViewState["tblMnth"] = ds.Tables[2];
        }
       
        // setGridLink_gv_year();
        bindChartByMnth(ds.Tables[1]);


    }

    public void Query_KH()
    {
        DataSet ds;
        if (droptype.SelectedIndex != 2)
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','2'");
        }
        else
        {
             ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1','4'");
        }
        DataTable dt = ds.Tables[0];
        // lblMstS.Text = dropYear.SelectedValue + "年";
       
        gv_KH.DataSource = dt;
        gv_KH.DataBind();
        if (droptype.SelectedValue == "0" || droptype.SelectedValue == "1")
        {
            ViewState["tblKH"] = ds.Tables[2];
        }
        // setGridLink_gv_year();
        bindChartByKH(ds.Tables[1]);


    }

    public void Query_DL()
    {
        DataSet ds;
        if (droptype.SelectedIndex != 2)
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','3'");
        }
        else
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1','5'");
        }

      //  DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ_modify  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','3'");
        DataTable dt = ds.Tables[0];
        Gv_DL.DataSource = dt;
        Gv_DL.DataBind();
        if (droptype.SelectedValue == "0" || droptype.SelectedValue == "1")
        {
            ViewState["tblDL"] = ds.Tables[2];
        }
        // setGridLink_gv_year();
        bindChartByDL(ds.Tables[1]);


    }

    public void bindChartByKH(DataTable tbl)
    {

        Chart_KH.DataSource = tbl;
        Chart_KH.Series["T1"].XValueMember = "year";
        Chart_KH.Series["T1"].YValueMembers = tbl.Columns[1].ToString();
        Chart_KH.Series["T1"].LegendText = tbl.Columns[1].ToString();

        Chart_KH.Series["T2"].XValueMember = "year";
        Chart_KH.Series["T2"].YValueMembers = tbl.Columns[2].ToString();
        Chart_KH.Series["T2"].LegendText = tbl.Columns[2].ToString();

        Chart_KH.Series["T3"].XValueMember = "year";
        Chart_KH.Series["T3"].YValueMembers = tbl.Columns[3].ToString();
        Chart_KH.Series["T3"].LegendText = tbl.Columns[3].ToString();

        Chart_KH.Series["T4"].XValueMember = "year";
        Chart_KH.Series["T4"].YValueMembers = tbl.Columns[4].ToString();
        Chart_KH.Series["T4"].LegendText = tbl.Columns[4].ToString();

        Chart_KH.Series["T5"].XValueMember = "year";
        Chart_KH.Series["T5"].YValueMembers = tbl.Columns[5].ToString();
        Chart_KH.Series["T5"].LegendText = tbl.Columns[5].ToString();
        Chart_KH.DataBind();
        DataTable dt_kh = (DataTable)ViewState["tblKH"];
        string type = droptype.SelectedValue == "0" ? "(数量)" : "(金额)";
        for (int i = 0; i < dt_kh.Rows.Count; i++)
        {

            this.Chart_KH.Series["T1"].Points[i].AxisLabel = dt_kh.Rows[i]["year"].ToString();
            this.Chart_KH.Series["T1"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n"  + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n"  + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString();
            this.Chart_KH.Series["T2"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString();
            this.Chart_KH.Series["T3"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString();
            this.Chart_KH.Series["T4"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString();
            this.Chart_KH.Series["T5"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString();


        }
    }

    public void bindChartByDL(DataTable tbl)
    {

        Chartdl.DataSource = tbl;
        Chartdl.Series["T1"].XValueMember = "year";
        Chartdl.Series["T1"].YValueMembers = tbl.Columns[1].ToString();
        Chartdl.Series["T1"].LegendText = tbl.Columns[1].ToString();

        Chartdl.Series["T2"].XValueMember = "year";
        Chartdl.Series["T2"].YValueMembers = tbl.Columns[2].ToString();
        Chartdl.Series["T2"].LegendText = tbl.Columns[2].ToString();

        if (tbl.Columns.Count > 3)
        {
            Chartdl.Series["T3"].XValueMember = "year";
            Chartdl.Series["T3"].YValueMembers = tbl.Columns[3].ToString();
            Chartdl.Series["T3"].LegendText = tbl.Columns[3].ToString();
        }
        else
        {
            Chartdl.Series["T3"].IsVisibleInLegend = false;
        }
        if (tbl.Columns.Count > 4)
        {
            Chartdl.Series["T4"].XValueMember = "year";
            Chartdl.Series["T4"].YValueMembers = tbl.Columns[4].ToString();
            Chartdl.Series["T4"].LegendText = tbl.Columns[4].ToString();
        }
        else
        {
            Chartdl.Series["T4"].IsVisibleInLegend = false;
        }
        if (tbl.Columns.Count > 5)
        {
            Chartdl.Series["T5"].XValueMember = "year";
            Chartdl.Series["T5"].YValueMembers = tbl.Columns[5].ToString();
            Chartdl.Series["T5"].LegendText = tbl.Columns[5].ToString();
        }
        else
        {
            Chartdl.Series["T5"].IsVisibleInLegend = false;
        }

        Chartdl.DataBind();
        DataTable dt_DL = (DataTable)ViewState["tblDL"];
        string type = droptype.SelectedValue == "0" ? "(数量)" : "(金额)";
        int bs = dt_DL.Columns.Count;
        bs = bs > 5 ? 5 : bs;

        for (int i = 0; i < dt_DL.Rows.Count; i++)
        {
            this.Chartdl.Series["T1"].Points[i].AxisLabel = dt_DL.Rows[i]["year"].ToString();
            this.Chartdl.Series["T1"].Points[i].ToolTip = dt_DL.Rows[i]["year"].ToString() + "\n" + dt_DL.Columns[1].ToString() + "占比:" + dt_DL.Rows[i][1].ToString() + "\n" + dt_DL.Columns[2].ToString() + "占比:" + dt_DL.Rows[i][2].ToString() + "\n" ;
            this.Chartdl.Series["T2"].Points[i].ToolTip = dt_DL.Rows[i]["year"].ToString() + "\n" + dt_DL.Columns[1].ToString() + "占比:" + dt_DL.Rows[i][1].ToString() + "\n" + dt_DL.Columns[2].ToString() + "占比:" + dt_DL.Rows[i][2].ToString() + "\n" ;
            if (Chartdl.Series["T3"].IsVisibleInLegend == true)
            {
                this.Chartdl.Series["T1"].Points[i].ToolTip += dt_DL.Columns[3].ToString() + "占比:" + dt_DL.Rows[i][3].ToString() + "\n";
                this.Chartdl.Series["T2"].Points[i].ToolTip += dt_DL.Columns[3].ToString() + "占比:" + dt_DL.Rows[i][3].ToString() + "\n";
                this.Chartdl.Series["T3"].Points[i].ToolTip = dt_DL.Rows[i]["year"].ToString() + "\n" + dt_DL.Columns[1].ToString() + "占比:" + dt_DL.Rows[i][1].ToString() + "\n" + dt_DL.Columns[2].ToString() + "占比:" + dt_DL.Rows[i][2].ToString() + "\n" + dt_DL.Columns[3].ToString() + "占比:" + dt_DL.Rows[i][3].ToString() + "\n"; 
            }
            if (Chartdl.Series["T4"].IsVisibleInLegend == true)
            {
                this.Chartdl.Series["T1"].Points[i].ToolTip += dt_DL.Columns[4].ToString() + "占比:" + dt_DL.Rows[i][4].ToString() + "\n";
                this.Chartdl.Series["T2"].Points[i].ToolTip += dt_DL.Columns[4].ToString() + "占比:" + dt_DL.Rows[i][4].ToString() + "\n";
                this.Chartdl.Series["T3"].Points[i].ToolTip += dt_DL.Columns[4].ToString() + "占比:" + dt_DL.Rows[i][4].ToString() + "\n";
                this.Chartdl.Series["T4"].Points[i].ToolTip = dt_DL.Rows[i]["year"].ToString() + "\n" + dt_DL.Columns[1].ToString() + "占比:" + dt_DL.Rows[i][1].ToString() + "\n" + dt_DL.Columns[2].ToString() + "占比:" + dt_DL.Rows[i][2].ToString() + "\n" + dt_DL.Columns[3].ToString() + "占比:" + dt_DL.Rows[i][3].ToString() + "\n" + dt_DL.Columns[4].ToString() + "占比:" + dt_DL.Rows[i][4].ToString() + "\n"; 
            }
            if (Chartdl.Series["T5"].IsVisibleInLegend == true)
            {
                this.Chartdl.Series["T1"].Points[i].ToolTip += dt_DL.Columns[5].ToString() + "占比:" + dt_DL.Rows[i][5].ToString() + "\n";
                this.Chartdl.Series["T2"].Points[i].ToolTip += dt_DL.Columns[5].ToString() + "占比:" + dt_DL.Rows[i][5].ToString() + "\n";
                this.Chartdl.Series["T3"].Points[i].ToolTip += dt_DL.Columns[5].ToString() + "占比:" + dt_DL.Rows[i][5].ToString() + "\n";
                this.Chartdl.Series["T4"].Points[i].ToolTip += dt_DL.Columns[5].ToString() + "占比:" + dt_DL.Rows[i][5].ToString() + "\n";
                this.Chartdl.Series["T5"].Points[i].ToolTip = dt_DL.Rows[i]["year"].ToString() + "\n" + dt_DL.Columns[1].ToString() + "占比:" + dt_DL.Rows[i][1].ToString() + "\n" + dt_DL.Columns[2].ToString() + "占比:" + dt_DL.Rows[i][2].ToString() + "\n" + dt_DL.Columns[3].ToString() + "占比:" + dt_DL.Rows[i][3].ToString() + "\n" + dt_DL.Columns[4].ToString() + "占比:" + dt_DL.Rows[i][4].ToString() + "\n" + dt_DL.Columns[5].ToString() + "占比:" + dt_DL.Rows[i][5].ToString() + "\n"; 
            }
        }


      
       
    }


    public void bindChartByYear(DataTable tbl)
    {

        Chart_ByYear.DataSource = tbl;
        Chart_ByYear.Series["生产中"].XValueMember = "year";
        Chart_ByYear.Series["生产中"].YValueMembers = "pro_num";
        Chart_ByYear.Series["开发中"].XValueMember = "year";
        Chart_ByYear.Series["开发中"].YValueMembers = "dev_num";

        string type = droptype.SelectedValue == "0" ? "(数量)" : "(金额)";

        Chart_ByYear.DataBind();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            this.Chart_ByYear.Series["开发中"].Points[i].AxisLabel = tbl.Rows[i]["year"].ToString();
            this.Chart_ByYear.Series["开发中"].Points[i].ToolTip = tbl.Rows[i]["year"].ToString() + "\n" +"开发中" + "占比:" + tbl.Rows[i]["dev_zb"].ToString() + "\n" + "生产中"  + "占比:" + tbl.Rows[i]["pro_zb"].ToString();
            this.Chart_ByYear.Series["生产中"].Points[i].ToolTip = tbl.Rows[i]["year"].ToString() + "\n" + "开发中"  + "占比:" + tbl.Rows[i]["dev_zb"].ToString() + "\n" + "生产中"  + "占比:" + tbl.Rows[i]["pro_zb"].ToString();
         
        }
       

    }

    public void bindChartByMnth(DataTable tbl)
    {

        Chart_ByMonth.DataSource = tbl;
        Chart_ByMonth.Series["T1"].XValueMember = "year";
        Chart_ByMonth.Series["T1"].YValueMembers = tbl.Columns[1].ToString();
        Chart_ByMonth.Series["T1"].LegendText = tbl.Columns[1].ToString();

        Chart_ByMonth.Series["T2"].XValueMember = "year";
        Chart_ByMonth.Series["T2"].YValueMembers = tbl.Columns[2].ToString();
        Chart_ByMonth.Series["T2"].LegendText = tbl.Columns[2].ToString();

        Chart_ByMonth.Series["T3"].XValueMember = "year";
        Chart_ByMonth.Series["T3"].YValueMembers = tbl.Columns[3].ToString();
        Chart_ByMonth.Series["T3"].LegendText = tbl.Columns[3].ToString();

        Chart_ByMonth.Series["T4"].XValueMember = "year";
        Chart_ByMonth.Series["T4"].YValueMembers = tbl.Columns[4].ToString();
        Chart_ByMonth.Series["T4"].LegendText = tbl.Columns[4].ToString();

        Chart_ByMonth.Series["T5"].XValueMember = "year";
        Chart_ByMonth.Series["T5"].YValueMembers = tbl.Columns[5].ToString();
        Chart_ByMonth.Series["T5"].LegendText = tbl.Columns[5].ToString();
        Chart_ByMonth.DataBind();

        Chart_ByMonth.Series["T6"].XValueMember = "year";
        Chart_ByMonth.Series["T6"].YValueMembers = tbl.Columns[6].ToString();
        Chart_ByMonth.Series["T6"].LegendText = tbl.Columns[6].ToString();
        Chart_ByMonth.DataBind();
        DataTable dt_kh = (DataTable)ViewState["tblMnth"];
        string type = droptype.SelectedValue == "0" ? "(数量)" : "(金额)";
        for (int i = 0; i < dt_kh.Rows.Count; i++)
        {

            this.Chart_ByMonth.Series["T1"].Points[i].AxisLabel = dt_kh.Rows[i]["year"].ToString();
            this.Chart_ByMonth.Series["T1"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString() + "\n"+ dt_kh.Columns[6].ToString() + "占比:" + dt_kh.Rows[i][6].ToString();
            this.Chart_ByMonth.Series["T2"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString() + "\n" + dt_kh.Columns[6].ToString() + "占比:" + dt_kh.Rows[i][6].ToString(); ;
            this.Chart_ByMonth.Series["T3"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString() + "\n" + dt_kh.Columns[6].ToString() + "占比:" + dt_kh.Rows[i][6].ToString(); ;
            this.Chart_ByMonth.Series["T4"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString() + "\n" + dt_kh.Columns[6].ToString() + "占比:" + dt_kh.Rows[i][6].ToString(); ;
            this.Chart_ByMonth.Series["T5"].Points[i].ToolTip = dt_kh.Rows[i]["year"].ToString() + "\n" + dt_kh.Columns[1].ToString() + "占比:" + dt_kh.Rows[i][1].ToString() + "\n" + dt_kh.Columns[2].ToString() + "占比:" + dt_kh.Rows[i][2].ToString() + "\n" + dt_kh.Columns[3].ToString() + "占比:" + dt_kh.Rows[i][3].ToString() + "\n" + dt_kh.Columns[4].ToString() + "占比:" + dt_kh.Rows[i][4].ToString() + "\n" + dt_kh.Columns[5].ToString() + "占比:" + dt_kh.Rows[i][5].ToString() + "\n" + dt_kh.Columns[6].ToString() + "占比:" + dt_kh.Rows[i][6].ToString(); ;


        }


    }
    protected void gv_year_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
            //{
            //    LinkButton lbtn = new LinkButton();
            //    lbtn.ID = "lbtn" + i.ToString();
            //    lbtn.Text = e.Row.Cells[i].Text;
            //    lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
            //    lbtn.Attributes.Add("name", "month");
            //    lbtn.Style.Add("text-align", "center");

            //    e.Row.Cells[i].Controls.Add(lbtn);
            //}
        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }



        }
    }
    protected void gv_month_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
           // e.Row.Cells[16].Wrap = false;
        }
        else
        {
           // e.Row.Cells[1].Wrap = false;
           // e.Row.Cells[16].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }



        }
    }
    protected void Gv_DL_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }



        }
    }
    protected void gv_KH_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }



        }
    }
    protected void Gv_DL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow hr = Gv_DL.HeaderRow;
            string year = dropYear.SelectedValue;

            if (dropYear.SelectedValue == "全部")
            {
                year = System.DateTime.Now.Year.ToString();
            }
            if (droptype.SelectedValue != "2")
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            else
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }

        }
    }
    protected void gv_month_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //string month = DateTime.Now.Month.ToString();
        

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    GridViewRow hr = gv_month.HeaderRow;
        //    int indexID = this.gv_month.PageIndex * this.gv_month.PageSize + e.Row.RowIndex + 1;
        //    if (indexID == 3 || indexID == 4 || indexID == 5)
        //    {
        //        e.Row.Cells[16].Text = "";
        //    }



        //    for (int i = 2; i <= e.Row.Cells.Count - 2; i++)
        //    {
        //        if (e.Row.Cells[i].Text.Replace("&nbsp;", "") != "")
        //        {
        //            e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
        //        }
        //        if (hr.Cells[i].Text.Replace("月","") == month)
        //        {
        //            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
        //        }

        //    }
        //    if (e.Row.Cells[e.Row.Cells.Count-1].Text.Replace("&nbsp;", "") != "")
        //    {
        //        e.Row.Cells[e.Row.Cells.Count - 1].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[e.Row.Cells.Count-1].Text));
        //    }
            
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow hr = gv_month.HeaderRow;
            string year = dropYear.SelectedValue;

            if (dropYear.SelectedValue == "全部")
            {
                year = System.DateTime.Now.Year.ToString();
            }
            if (droptype.SelectedValue != "2")
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }
            else
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }

            }


        }
    }
    protected void gv_KH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow hr = gv_KH.HeaderRow;
            string year = dropYear.SelectedValue;

            if (dropYear.SelectedValue == "全部")
            {
                year = System.DateTime.Now.Year.ToString();
            }
            if (droptype.SelectedValue != "2")
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }
            else
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    if (hr.Cells[i].Text == year)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }

            }

         
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       
        Query_year();
        Query_mnth();
        Query_KH();
        Query_DL();
        Query_Detail();
        //if (dropYear.SelectedValue != "全部")
        //{
        //    Label1.Text = "按" + dropYear.SelectedValue + "月统计";
        //}
        //else
        //{
        //    Label1.Text = "按" + System.DateTime.Now.Year.ToString() + "月统计";
        //}
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        Query_Detail();
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {   //序号列不排序


        for (int i = 8; i < 25; i++)///‘从第一列开始设置
            try
            {
                LinkButton lb = (LinkButton)GridView3.HeaderRow.Cells[i].Controls[0];
                string txt = lb.Text;
                GridView3.HeaderRow.Cells[i].Text = txt;

            }
            catch (Exception ex)
            { }

    }
    protected void gv3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Style.Add("word-break", "break-all");//零件号
            e.Row.Cells[4].Style.Add("width", "140px");
            e.Row.Cells[8].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[8].Style.Add("width", "120px");
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");

            e.Row.Cells[1].Width = Convert.ToInt16(80);
            e.Row.Cells[2].Width = Convert.ToInt16(80);
            e.Row.Cells[3].Width = Convert.ToInt16(60);
            e.Row.Cells[4].Width = Convert.ToInt16(100);
            e.Row.Cells[5].Width = Convert.ToInt16(100);
            e.Row.Cells[6].Width = Convert.ToInt16(80);
            e.Row.Cells[7].Width = Convert.ToInt16(100);
            e.Row.Cells[8].Width = Convert.ToInt16(120);
            e.Row.Cells[9].Width = Convert.ToInt16(100);
            e.Row.Cells[10].Width = Convert.ToInt16(100);
            e.Row.Cells[23].Width = Convert.ToInt16(50);//更新
            e.Row.Cells[24].Width = Convert.ToInt16(100);

            for (int i = 11; i < 23; i++)
            {
                if (i != 11)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == 99999999)
                    {
                        e.Row.Cells[i].Text = "小计";
                        e.Row.Cells[i - 3].Text = ""; //顾客项目栏位改为空
                        e.Row.Cells[i - 1].Text = "";
                        e.Row.Style.Add("font-weight", "bold");
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            //如果该订单是停产状态，则整行显示灰色
            if (e.Row.Cells[2].Text.ToString().Trim() == "停产" || e.Row.Cells[2].Text.ToString().Trim() == "项目取消")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[2].Text.ToString().Trim() == "开发中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

        }
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");

            e.Row.Cells[11].Text = "合计";
            e.Row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[13].Text = this.ntotal_Q2.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q3.ToString("N0");
            e.Row.Cells[15].Text = this.ntotal_Q4.ToString("N0");
            e.Row.Cells[16].Text = this.ntotal_Q5.ToString("N0");
            e.Row.Cells[17].Text = this.ntotal_Q6.ToString("N0");
            e.Row.Cells[18].Text = this.ntotal_Q7.ToString("N0");
            e.Row.Cells[19].Text = this.ntotal_Q8.ToString("N0");
            e.Row.Cells[20].Text = this.ntotal_Q9.ToString("N0");
            e.Row.Cells[21].Text = this.ntotal_Q10.ToString("N0");
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
        }

    }
    /// <summary>
    /// 按全部查询时按金额显示
    /// </summary>
    /// <param name="ds"></param>
    public void QueryGridView3(DataTable dt)
    {
        GridView3.DataSource = null;
        GridView3.DataBind();
        GridView3.Visible = true;
        GridView1.Visible = false;
        GridView2.Visible = false;
        this.Getsum(dt);
        this.GridView3.DataSource = dt;
        this.GridView3.DataBind();
        int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 23, 8, 9, 10, 24 };
        MergGridRow.MergeRow(GridView3, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = rowIndex.ToString();
                //HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                //link.ImageUrl = this.GridView3.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView3.Rows[j].Cells[27].Text).Trim();
                GridView3.Rows[j].Cells[23].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                link.Text = GridView3.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView3.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }
        ////后台设定列宽
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {

            for (int j = 11; j < 22; j++)
            {
                GridView3.Rows[i].Cells[j].Width = Convert.ToInt16(60);
            }

        }
    }

    /// <summary>
    /// 按全部查询时按数量显示
    /// </summary>
    /// <param name="ds"></param>
    public void QueryGridView2_ALL(DataTable dt)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        this.Getsum(dt);
        this.GridView2.DataSource = dt;
        this.GridView2.DataBind();
        GridView2.Visible = true;
        GridView1.Visible = false;
        GridView3.Visible = false;

        int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 24, 8, 9, 10, 25 };
        MergGridRow.MergeRow(GridView2, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = rowIndex.ToString();
                //HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                //link.ImageUrl = this.GridView2.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView2.Rows[j].Cells[28].Text).Trim();
                GridView2.Rows[j].Cells[24].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }
        ////后台设定列宽
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            GridView2.Rows[i].Cells[1].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[2].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[3].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[4].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[5].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[6].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[7].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[8].Width = Convert.ToInt16(120);
            GridView2.Rows[i].Cells[8].Style.Add("word-break", "break-all");
            GridView2.Rows[i].Cells[9].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[10].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[25].Width = Convert.ToInt16(100);

            for (int j = 11; j < 24; j++)
            {
                GridView2.Rows[i].Cells[j].Width = Convert.ToInt16(60);
            }

        }

    }


    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (dropYear.SelectedValue == "全部")
        {
            for (int i = 8; i < 26; i++)///‘从第一列开始设置
                try
                {
                    LinkButton kk = (LinkButton)GridView2.HeaderRow.Cells[i].Controls[0];
                    string txt = kk.Text;
                    GridView2.HeaderRow.Cells[i].Text = txt;

                }
                catch (Exception ex)
                { }
        }


    }

    public void setHeader(GridView grid)
    {
        GridViewRow row = grid.HeaderRow;
        for (int i = 10; i < row.Cells.Count; i++)
        {
            switch (row.Cells[i].Text)
            {
                case "C1":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<span style='text-decoration:underline'>" + (DateTime.Now.Year - 2).ToString() + "</span><br>A";
                    break;
                case "C2":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<span style='text-decoration:underline'>" + (DateTime.Now.Year - 1).ToString() + "</span><br>A";
                    break;
                case "C3":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<table style='width:100%'><tr><td colspan=2 style='text-decoration:underline;text-align:center;' > " + (DateTime.Now.Year).ToString() + "</td></tr><tr><td>A</td><td alt='Forcast'>F</td></tr></table>";
                    row.Cells[i].ColumnSpan = 2;
                    row.Cells[i + 1].Visible = false;
                    i = i + 1;
                    break;
                default:
                    if (Maticsoft.Common.Assistant.IsNumberic(row.Cells[i].Text))
                    {
                        row.Cells[i].Style.Add("text-align", "center");
                        row.Cells[i].Text = "<span style='text-decoration:underline'>" + row.Cells[i].Text + "</span><br>F";
                    }
                    break;
            }


        }
    }

    /// <summary>
    /// 按月统计
    /// </summary>
    /// <param name="dt"></param>
    public void QueryGridView2(DataTable dt)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        this.Getsum_mnth(dt);
        this.GridView2.DataSource = dt;
        this.GridView2.DataBind();
        GridView2.Visible = true;
        GridView1.Visible = false;
        GridView3.Visible = false;
        int[] cols = { 0, 1, 2, 3, 4, 5, 6,7,8,9,10};
        MergGridRow.MergeRow(GridView2, cols);
      
        int rowIndex = 1;
       

        int Index = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[2].Controls.Add(link);
                Index++;
            }
        }
        
    }
    public void QueryGridView1(DataTable dt)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        this.Getsum(dt);
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView1.Visible = true;
        int[] cols = { 0, 1, 2, 3, 4, 5, 6,7,8,9,10 };
        MergGridRow.MergeRow(GridView1, cols);
        int rowIndex = 1;


        int Index = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[24].Text).Trim();
                link.Text = GridView1.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[2].Controls.Add(link);
                Index++;
            }
        }
        
    }

    private void Getsum(DataTable ldt)
    {
        this.ntotal_Q1 = 0;
        this.ntotal_Q2 = 0;
        this.ntotal_Q3 = 0;
        this.ntotal_Q4 = 0;
        this.ntotal_Q5 = 0;
        this.ntotal_Q6 = 0;
        this.ntotal_Q7 = 0;
        this.ntotal_Q8 = 0;
        this.ntotal_Q9 = 0;
        this.ntotal_Q10 = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (Convert.ToInt32(Convert.ToDecimal(ldt.Rows[i][11].ToString().Replace(",", ""))) != 99999999)
            {
                if (ldt.Rows[i][12].ToString() != "")
                {
                    this.ntotal_Q1 = this.ntotal_Q1 + Convert.ToDecimal(ldt.Rows[i][12].ToString());
                }
                if (ldt.Rows[i][13].ToString() != "")
                {
                    this.ntotal_Q2 = this.ntotal_Q2 + Convert.ToDecimal(ldt.Rows[i][13].ToString());
                }
                if (ldt.Rows[i][14].ToString() != "")
                {
                    this.ntotal_Q3 = this.ntotal_Q3 + Convert.ToDecimal(ldt.Rows[i][14].ToString());
                }
                if (ldt.Rows[i][15].ToString() != "")
                {
                    this.ntotal_Q4 = this.ntotal_Q4 + Convert.ToDecimal(ldt.Rows[i][15].ToString());
                }
                if (ldt.Rows[i][16].ToString() != "")
                {
                    this.ntotal_Q5 = this.ntotal_Q5 + Convert.ToDecimal(ldt.Rows[i][16].ToString());
                }
                if (ldt.Rows[i][17].ToString() != "")
                {
                    this.ntotal_Q6 = this.ntotal_Q6 + Convert.ToDecimal(ldt.Rows[i][17].ToString());
                }
                if (ldt.Rows[i][18].ToString() != "")
                {
                    this.ntotal_Q7 = this.ntotal_Q7 + Convert.ToDecimal(ldt.Rows[i][18].ToString());
                }
                if (ldt.Rows[i][19].ToString() != "")
                {
                    this.ntotal_Q8 = this.ntotal_Q8 + Convert.ToDecimal(ldt.Rows[i][19].ToString());
                }
                if (ldt.Rows[i][20].ToString() != "")
                {
                    this.ntotal_Q9 = this.ntotal_Q9 + Convert.ToDecimal(ldt.Rows[i][20].ToString());
                }
                if (ldt.Rows[i][21].ToString() != "")
                {
                    this.ntotal_Q10 = this.ntotal_Q10 + Convert.ToDecimal(ldt.Rows[i][21].ToString());
                }
            }
        }
    }


    private void Getsum_mnth(DataTable ldt)
    {
        this.ntotal_Q1 = 0;
        this.ntotal_Q2 = 0;
        this.ntotal_Q3 = 0;
        this.ntotal_Q4 = 0;
        this.ntotal_Q5 = 0;
        this.ntotal_Q6 = 0;
        this.ntotal_Q7 = 0;
        this.ntotal_Q8 = 0;
        this.ntotal_Q9 = 0;
        this.ntotal_Q10 = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i][11].ToString() != "-1")
            {
                if (ldt.Rows[i][13].ToString() != "")
                {
                    this.ntotal_Q1 = this.ntotal_Q1 + Convert.ToDecimal(ldt.Rows[i][13].ToString());
                }
                if (ldt.Rows[i][14].ToString() != "")
                {
                    this.ntotal_Q2 = this.ntotal_Q2 + Convert.ToDecimal(ldt.Rows[i][14].ToString());
                }
                if (ldt.Rows[i][15].ToString() != "")
                {
                    this.ntotal_Q3 = this.ntotal_Q3 + Convert.ToDecimal(ldt.Rows[i][15].ToString());
                }
                if (ldt.Rows[i][16].ToString() != "")
                {
                    this.ntotal_Q4 = this.ntotal_Q4 + Convert.ToDecimal(ldt.Rows[i][16].ToString());
                }
                if (ldt.Rows[i][17].ToString() != "")
                {
                    this.ntotal_Q5 = this.ntotal_Q5 + Convert.ToDecimal(ldt.Rows[i][17].ToString());
                }
                if (ldt.Rows[i][18].ToString() != "")
                {
                    this.ntotal_Q6 = this.ntotal_Q6 + Convert.ToDecimal(ldt.Rows[i][18].ToString());
                }
                if (ldt.Rows[i][19].ToString() != "")
                {
                    this.ntotal_Q7 = this.ntotal_Q7 + Convert.ToDecimal(ldt.Rows[i][19].ToString());
                }
                if (ldt.Rows[i][20].ToString() != "")
                {
                    this.ntotal_Q8 = this.ntotal_Q8 + Convert.ToDecimal(ldt.Rows[i][20].ToString());
                }
                if (ldt.Rows[i][21].ToString() != "")
                {
                    this.ntotal_Q9 = this.ntotal_Q9 + Convert.ToDecimal(ldt.Rows[i][21].ToString());
                }
                if (ldt.Rows[i][22].ToString() != "")
                {
                    this.ntotal_Q10 = this.ntotal_Q10 + Convert.ToDecimal(ldt.Rows[i][22].ToString());
                }
                if (ldt.Rows[i][23].ToString() != "")
                {
                    this.ntotal_Q11 = this.ntotal_Q11 + Convert.ToDecimal(ldt.Rows[i][23].ToString());
                }
                if (ldt.Rows[i][24].ToString() != "")
                {
                    this.ntotal_Q12 = this.ntotal_Q12 + Convert.ToDecimal(ldt.Rows[i][24].ToString());
                }
                if (ldt.Rows[i][27].ToString() != "")
                {
                    this.ypj = this.ypj + Convert.ToDecimal(ldt.Rows[i][27].ToString());
                }
                if (ldt.Rows[i][28].ToString() != "")
                {
                    this.hj = this.hj + Convert.ToDecimal(ldt.Rows[i][28].ToString());
                }
                if (ldt.Rows[i][29].ToString() != "")
                {
                    this.zb = this.zb + Convert.ToDecimal(ldt.Rows[i][29].ToString());
                }
            }

        }
    }

    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
       // DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 3 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 3 + ",'', '','','','" + this.dropsite.SelectedValue.ToString() + "','-1','-1','-1','','','-1','-1','-1','A'");
        DataView dv = ds.Tables[0].DefaultView;
        Getsum(ds.Tables[0]);
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView3.DataSource = dv;

        this.GridView3.DataBind();

        int[] cols = { 0, 1, 2, 3, 4, 23, 5, 6, 7, 8, 9, 10, 24 };
        MergGridRow.MergeRow(GridView3, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = rowIndex.ToString();
                // HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                //link.ImageUrl = this.GridView3.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView2.Rows[j].Cells[27].Text).Trim();

                GridView3.Rows[j].Cells[23].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                link.Text = GridView3.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView3.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }

        setHeader(GridView3);

    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataSet ds;
       
        if (droptype.SelectedIndex != 2)
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ_ByDetail_export  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "'");
        }
        else
        {
            ds = DbHelperSQL.Query("exec rpt_Form3_Sale_YC_TJ_ByDetail_export  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','1'");
        }
        string lsname = "销售预测明细";

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