using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TouLiaoTongJi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            BaseFun fun = new BaseFun();         
            //初始化年份    
            int year=Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for(int i=0;i<5;i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }         

            //初始化月份
            dropMonth.Items.Add(new ListItem{ Value="", Text="" });
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem( i.ToString() , i.ToString()  ));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份
            dropDay.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 31; i++)
            {
                dropDay.Items.Add(new ListItem(i.ToString() , i.ToString()));
            }
            dropDay.SelectedValue=DateTime.Now.Day.ToString();
            //init 合金
            fun.initHeJin(selHeJin);
            selHeJin.Items.Insert(0,new ListItem( "", ""));

            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.ToString("MM"));
            QueryDay(DateTime.Now.ToString("yyyyMMdd"),DateTime.Now.ToString("MM"));
        }
        
    }
    //年Gridview
    protected void btnQuery_Click(object sender, EventArgs e)
    {   //Year
        QueryYear();
        //month
        QueryMonth(dropMonth.SelectedValue.PadLeft(2, '0'));
        lblMonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0')+"月";
        this.txtmonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0');
        //Day
        string day = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + dropDay.SelectedValue.PadLeft(2, '0');
        QueryDay(day, ""); 
        lblDays.Text = day;
    }
    public void QueryYear()
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_InputMaterial_TongJi '年','" + selHeJin.SelectedValue + "','','" + this.dropYear.SelectedValue + "',''");
  
        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue+"年";

        ViewState["tblYear"] = ds.Tables[1];       
        bindChartYear(ds.Tables[1]);    
    
    }
    public void bindChartYear(DataTable tbl)
    {   //铝锭重量
        DataRow[] dr = tbl.Select("mon=33");
        if (dr.Length > 0)
        {
            tbl.Rows.Remove(dr[0]);
        }
        ChartYear.DataSource = tbl;
        ChartYear.Series["铝锭重量"].XValueMember = "mon";
        ChartYear.Series["铝锭重量"].YValueMembers = "weight";
        //铝锭占比
        ChartYearRate.DataSource = tbl;
        ChartYearRate.Series["铝锭占比"].XValueMember = "mon";
        ChartYearRate.Series["铝锭占比"].YValueMembers = "rate";
        ChartYearRate.ChartAreas[0].AxisY.Maximum = 100;
        ChartYearRate.ChartAreas[0].AxisY.Minimum = 30;
      
    }
    public void bindChartMonth(DataTable tbl)
    {
        
        //剔除合计栏位，免图形异常
        DataRow[] dr = tbl.Select("Days=33");
        if (dr.Length>0)
        {
            tbl.Rows.Remove(dr[0]);
        }
        ChartMonth.DataSource = tbl;
        ChartMonth.Series["铝锭重量"].XValueMember = "Days";
        ChartMonth.Series["铝锭重量"].YValueMembers = "weight";

        //铝锭占比
        ChartMonthRate.DataSource = tbl;
        ChartMonthRate.Series["铝锭占比"].XValueMember = "Days";
        ChartMonthRate.Series["铝锭占比"].YValueMembers = "rate";
        ChartMonthRate.ChartAreas[0].AxisY.Maximum = 100;
        ChartMonthRate.ChartAreas[0].AxisY.Minimum = 30;
       // ChartMonth.Titles["TitleMonth"].Text = this.txtmonth.Text + "月";
    }
     //设定年Gridview Header
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {   if (e.Row.Cells[i].Text == "33")
                {
                    e.Row.Cells[i].Text = "合计";
                }     
                else
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
    }
    //Binding月 明细资料
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txtmonth.Text.Substring(4));
    }
    public void QueryMonth(string month)
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_InputMaterial_TongJi '月','" + selHeJin.SelectedValue + "','','" + this.dropYear.SelectedValue + "','" + month + "'");
        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";
        ViewState["tblMonth"] = ds.Tables[1];
        bindChartMonth(ds.Tables[1]);
        bindChartYear((DataTable)ViewState["tblYear"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
   
    }
    //Binding 日明细资料
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        QueryDay(txtmonth.Text + txtday.Text.PadLeft(2, '0'), txtmonth.Text.Substring(4));
    }
    public void QueryDay(string day,string month)
    {        
        DataSet ds = DbHelperSQL.Query("exec MES_SP_InputMaterial_TongJi '日','" + selHeJin.SelectedValue + "','" + day + "','" + this.dropYear.SelectedValue + "','" + month + "'");
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        lblDays.Text =txtmonth.Text+ txtday.Text.PadLeft(2,'0');

        bindChartMonth((DataTable)ViewState["tblMonth"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
        bindChartYear((DataTable)ViewState["tblYear"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认    
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDays','')");
                lbtn.Attributes.Add("name", "day");
                lbtn.Style.Add("text-align","center");
                
                if (e.Row.Cells[i].Text == "33")
                {
                    e.Row.Cells[i].Text = "合计";                     
                }
                else
                    e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {           
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {               
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;;
            }
            
        }
    }

    private int a_weight = 0; private Decimal a_rate = 0;
    private int b_weight = 0; private Decimal b_rate = 0;
    private int c_weight = 0; private Decimal c_rate = 0;
    private int d_weight = 0; private Decimal d_rate = 0;
    private float tweight = 0; private int rowsCount = 0;    
    protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            a_weight += Convert.ToInt32(dr.Row["a_weight"]);
            b_weight += Convert.ToInt32(dr.Row["b_weight"]);
            c_weight += Convert.ToInt32(dr.Row["c_weight"]);
            d_weight += Convert.ToInt32(dr.Row["d_weight"]);
            a_rate += Convert.ToDecimal(dr.Row["a_rate"]);
            b_rate += Convert.ToDecimal(dr.Row["b_rate"]);
            c_rate += Convert.ToDecimal(dr.Row["c_rate"]);
            d_rate += Convert.ToDecimal(dr.Row["d_rate"]);
            tweight += Convert.ToInt32(dr.Row["tweight"]);
            rowsCount += 1;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align","right");
            e.Row.Style.Add("background-color", "lightyellow");            
            e.Row.Cells[2].Text = "合计：";          
            e.Row.Cells[3].Text = a_weight.ToString();
            e.Row.Cells[4].Text = (a_weight / tweight).ToString("0.0%");            
            e.Row.Cells[5].Text = b_weight.ToString();
            e.Row.Cells[6].Text = (b_weight / tweight).ToString("0.0%");
            e.Row.Cells[7].Text = c_weight.ToString();
            e.Row.Cells[8].Text = (c_weight / tweight).ToString("0.0%");
            e.Row.Cells[9].Text = d_weight.ToString();
            e.Row.Cells[10].Text = (d_weight / tweight).ToString("0.0%");
            e.Row.Cells[11].Text = tweight.ToString();
        }
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DateTime.Now.Month != Convert.ToInt16(dropMonth.SelectedValue))
        {
            string day = Convert.ToDateTime(dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue)).ToString().PadLeft(2, '0') + "-01").AddMonths(1).AddDays(-1).Day.ToString();
            //string date = dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue) + 1).ToString().PadLeft(2, '0') + "-01";
            //date = Convert.ToDateTime(date).AddDays(-1).Day.ToString();
            dropDay.SelectedValue = day;
        }
    }
}