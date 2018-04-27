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


public partial class PW_PW_Add_TJReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }

            //初始化月份
            dropMonth.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份
            dropDay.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 31; i++)
            {
                dropDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropDay.SelectedValue = DateTime.Now.Day.ToString();

            string sql = "select equip_name from MES_Equipment where gongwei='抛丸'";
            DataTable db = DbHelperSQL.Query(sql).Tables[0];
            fun.initDropDownList(selsb, db, "equip_name", "equip_name");
            selsb.Items.Insert(0, new ListItem("", ""));

            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            txtday.Text = DateTime.Now.ToString("dd");
            int mnth = Convert.ToInt16(DateTime.Now.ToString("MM"));
            int day = Convert.ToInt16(DateTime.Now.ToString("dd"));
            QueryMonth(mnth);
            QueryDay(mnth,day);
        }

    }
    //年Gridview
    protected void btnQuery_Click(object sender, EventArgs e)
    {   //Year

        
        QueryYear();
        //month
       QueryMonth(Convert.ToInt16(dropMonth.SelectedValue));
        lblMonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        this.txtmonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0');
        //Day
        string day = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + dropDay.SelectedValue.PadLeft(2, '0');
        QueryDay(Convert.ToInt16(dropMonth.SelectedValue), Convert.ToInt16(dropDay.SelectedValue));
        lblDays.Text = day;
    }

    public void bindChartYear()
    {

        DataTable tbl = (DataTable)ViewState["tblYear"];
        DataTable dt_ZL = tbl.Clone();         //复制数据源的表结构
        DataRow[] dr = tbl.Select("type='钢丸重量(Kg)'");  //strWhere条件筛选出需要的数据！
        for (int i = 0; i < dr.Length; i++)
        {
            dt_ZL.Rows.Add(dr[i].ItemArray);  // 将DataRow添加到DataTable中
        }


        DataTable dt_RATE = tbl.Clone();         //复制数据源的表结构
        DataRow[] dr_rate = tbl.Select("type='0.6mm丸粒占比(%)'");
        for (int i = 0; i < dr_rate.Length; i++)
        {
            dt_RATE.Rows.Add(dr_rate[i].ItemArray);  // 将DataRow添加到DataTable中
        }
        
        //重量
        ChartYear.DataSource = dt_ZL;
        ChartYear.Series["重量(Kg)"].XValueMember = "add_date";
        ChartYear.Series["重量(Kg)"].YValueMembers = "zl";
        ChartYear.DataBind();
  
        for (int i = 0; i < dt_ZL.Rows.Count; i++)
        {
            this.ChartYear.Series["重量(Kg)"].Points[i].AxisLabel = dt_ZL.Rows[i]["add_date"].ToString();
            this.ChartYear.Series["重量(Kg)"].Points[i].ToolTip = dt_ZL.Rows[i]["zl"].ToString();
        }

         
        //占比
        ChartYearRate.DataSource = dt_RATE;
        ChartYearRate.Series["0.6mm占比(%)"].XValueMember = "add_date";
        ChartYearRate.Series["0.6mm占比(%)"].YValueMembers = "zl";
        ChartYearRate.DataBind();

        for (int i = 0; i < dt_RATE.Rows.Count; i++)
        {
            this.ChartYearRate.Series["0.6mm占比(%)"].Points[i].AxisLabel = dt_RATE.Rows[i]["add_date"].ToString();
            this.ChartYearRate.Series["0.6mm占比(%)"].Points[i].ToolTip = dt_RATE.Rows[i]["zl"].ToString();
        }

    }


    public void bindChartMonth()
    {

        DataTable tbl = (DataTable)ViewState["tblMonth"];
        DataTable dt_ZL = tbl.Clone();
        DataRow[] dr = tbl.Select("type='钢丸重量(Kg)'");
        for (int i = 0; i < dr.Length; i++)
        {
            dt_ZL.Rows.Add(dr[i].ItemArray);
        }

        DataTable dt_RATE = tbl.Clone();
        DataRow[] dr_rate = tbl.Select("type='0.6mm丸粒占比(%)'");
        for (int i = 0; i < dr_rate.Length; i++)
        {
            dt_RATE.Rows.Add(dr_rate[i].ItemArray);
        }


        ChartMonth.DataSource = dt_ZL;
        ChartMonth.Series["重量(Kg)"].XValueMember = "add_date";
        ChartMonth.Series["重量(Kg)"].YValueMembers = "zl";
        ChartMonth.DataBind();
        for (int i = 0; i < dt_ZL.Rows.Count; i++)
        {
            this.ChartMonth.Series["重量(Kg)"].Points[i].AxisLabel = dt_ZL.Rows[i]["add_date"].ToString();
            this.ChartMonth.Series["重量(Kg)"].Points[i].ToolTip = dt_ZL.Rows[i]["zl"].ToString();
        }

        //占比
        ChartMonthRate.DataSource = dt_RATE;
        ChartMonthRate.Series["0.6mm占比(%)"].XValueMember = "add_date";
        ChartMonthRate.Series["0.6mm占比(%)"].YValueMembers = "zl";
        ChartMonthRate.DataBind();

        for (int i = 0; i < dt_RATE.Rows.Count; i++)
        {
            this.ChartMonthRate.Series["0.6mm占比(%)"].Points[i].AxisLabel = dt_RATE.Rows[i]["add_date"].ToString();
            this.ChartMonthRate.Series["0.6mm占比(%)"].Points[i].ToolTip = dt_RATE.Rows[i]["zl"].ToString();
        }
    
    }

    public void QueryYear()
    {

        //MES_PWADD_TJ '2017','4','','','','年'

        DataSet ds = DbHelperSQL.Query("exec MES_PWADD_TJ '" + this.dropYear.SelectedValue + "','','','" + selsb.SelectedValue + "','','年'");

        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue + "年";

        ViewState["tblYear"] = ds.Tables[1];
        bindChartYear();

    }

  

    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "描述";
            //e.Row.Cells[0].Width=200;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "mon");
                e.Row.Cells[i].Controls.Add(lbtn);
            }

        }
    }
    //Binding月 明细资料
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(Convert.ToInt16(txtmonth.Text.Substring(4)));
    }
    public void QueryMonth(int month)
    {
        //MES_PWADD_TJ '2017','4','','','','年'

       
        DataSet ds = DbHelperSQL.Query("exec MES_PWADD_TJ '" + this.dropYear.SelectedValue + "','" + month + "','','" + selsb.SelectedValue + "','','月'");

        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";;

        ViewState["tblMonth"] = ds.Tables[1];
        bindChartMonth();
        

    }
    //Binding 日明细资料
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        QueryDay(Convert.ToInt16( txtmonth.Text.Substring(4)),Convert.ToInt16(txtday.Text.Substring(3)));
    }
    public void QueryDay(int month, int day)
    {
      
        DataSet ds = DbHelperSQL.Query("exec MES_PWADD_TJ '" + this.dropYear.SelectedValue + "','" + month + "','" + day + "','" + selsb.SelectedValue + "','','日'");
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        lblDays.Text = txtmonth.Text + day.ToString().PadLeft(2,'0');
        bindChartYear();
        bindChartMonth();

        
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "描述";
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDays','')");
                lbtn.Attributes.Add("name", "day");
                lbtn.Style.Add("text-align", "center");
                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }

        }
    }

    private int bs = 0; private int zl = 0;
   
    protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            bs += Convert.ToInt32(dr.Row["bs"]);
            zl += Convert.ToInt32(dr.Row["zl"]);
        }
        
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");
            e.Row.Style.Add("background-color", "lightyellow");
            e.Row.Cells[2].Text = "合计：";
            e.Row.Cells[4].Text = bs.ToString();
            e.Row.Cells[5].Text = zl.ToString();
           
        }
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DateTime.Now.Month != Convert.ToInt16(dropMonth.SelectedValue))
        {
            string day = Convert.ToDateTime(dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue)).ToString().PadLeft(2, '0') + "-01").AddMonths(1).AddDays(-1).Day.ToString();
            dropDay.SelectedValue = day;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}