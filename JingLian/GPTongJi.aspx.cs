using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class GPTongJi : System.Web.UI.Page
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
    {                                   //[usp_GP_TongJi] '2016','12','2016/12/24','','','日'
        DataSet ds = DbHelperSQL.Query("exec usp_GP_TongJi '" + this.dropYear.SelectedValue + "','','','" + dropBanbie.SelectedValue + "','"+dropSource.SelectedValue+"','年'");
  
        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue+"年";

        ViewState["tblYear"] = ds.Tables[1];       
        bindChartYear(ds.Tables[1]);    
    
    }
    public void bindChartYear(DataTable tbl)
    {
        DataRow[] drs = tbl.Select("mon='TJ13' or mon='TJ14'");
        foreach (DataRow dr in drs)
        { tbl.Rows.Remove(dr); }
        
        //次数
        ChartYear.DataSource = tbl;
        ChartYear.Series["铝锭重量"].XValueMember = "mon";
        ChartYear.Series["铝锭重量"].YValueMembers = "cnt";
        //时长
        ChartYearRate.DataSource = tbl;
        ChartYearRate.Series["铝锭占比"].XValueMember = "mon";
        ChartYearRate.Series["铝锭占比"].YValueMembers = "hour";
        //ChartYearRate.ChartAreas[0].AxisY.Maximum = 100;
        //ChartYearRate.ChartAreas[0].AxisY.Minimum = 30;
      
    }
    public void bindChartMonth(DataTable tbl)
    {        
        //剔除合计栏位，免图形异常
        DataRow[] drs = tbl.Select("Days=33 or Days=34");
        foreach (DataRow dr in drs)
        {   tbl.Rows.Remove(dr);   }

        ChartMonth.DataSource = tbl;
        ChartMonth.Series["铝锭重量"].XValueMember = "Days";
        ChartMonth.Series["铝锭重量"].YValueMembers = "cnt";

        ChartMonth2.DataSource = tbl;
        ChartMonth2.Series["平均测量时长"].XValueMember = "Days";
        ChartMonth2.Series["平均测量时长"].YValueMembers = "Hour";

       // ChartMonth.Titles["TitleMonth"].Text = this.txtmonth.Text + "月";
    }
     //设定年Gridview Header
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {               
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;                             
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");               
                lbtn.Attributes.Add("name", "mon");
                if (e.Row.Cells[i].Text == "TJ13")
                {
                    e.Row.Cells[i].Text = "合计";
                }
                else if (e.Row.Cells[i].Text == "TJ14")
                {
                    e.Row.Cells[i].Text = "平均";
                }
                else
                    e.Row.Cells[i].Controls.Add(lbtn);
                
            }
            
        }        
    }
    //Binding月 明细资料
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txtmonth.Text.Substring(4));
    }
    public void QueryMonth(string month)
    {                                                        // '2016','12','2016/12/24','','','日'
        DataSet ds = DbHelperSQL.Query("exec usp_GP_TongJi '" + this.dropYear.SelectedValue + "','" + month + "','','" + dropBanbie.SelectedValue + "','"+dropSource.SelectedValue+"','月'");
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
    {                                                                   // '2016','12','2016/12/24','','','日'
        DataSet ds = DbHelperSQL.Query("exec usp_GP_TongJi '" + this.dropYear.SelectedValue + "','" + month + "','" + day + "','" + dropBanbie.SelectedValue + "','"+dropSource.SelectedValue+"','日'");
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        lblDays.Text =txtmonth.Text+ txtday.Text.PadLeft(2,'0');

        bindChartMonth((DataTable)ViewState["tblMonth"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
        bindChartYear((DataTable)ViewState["tblYear"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认    
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
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
                else if (e.Row.Cells[i].Text == "34")
                {
                    e.Row.Cells[i].Text = "平均";
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

   
    protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
        }
        //else if (e.Row.RowType == DataControlRowType.Footer)
        //{
       
        //}
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if(DateTime.Now.Month!=Convert.ToInt16(dropMonth.SelectedValue) )
        {
            string day = Convert.ToDateTime(dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue)).ToString().PadLeft(2, '0') + "-01").AddMonths(1).AddDays(-1).Day.ToString();
            //string date = dropYear.SelectedValue +"-"+ (Convert.ToInt16(dropMonth.SelectedValue) + 1).ToString().PadLeft(2, '0') + "-01";
            //date = Convert.ToDateTime(date).AddDays(-1).Day.ToString();
            dropDay.SelectedValue = day;
        }
    }
}