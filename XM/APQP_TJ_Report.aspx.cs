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

public partial class XM_APQP_TJ_Report : System.Web.UI.Page
{
    GetBase GetBase = new GetBase();
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
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份

            string lssql = "select distinct pgino from ProductList ";
            this.txtpgi_no.DataSource = Query(lssql);
            this.txtpgi_no.DataTextField = "pgino";
            this.txtpgi_no.DataValueField = "pgino";
            this.txtpgi_no.DataBind();
            this.txtpgi_no.Items.Insert(0, new ListItem("", ""));

            string sql = "select * from  base_code   where code_type='TRACK_PROJECT_USER_ADD'  order by req_no";
            this.ddl_projectuser.DataSource = Query(sql).Tables[0];
            this.ddl_projectuser.DataTextField = "code_name";
            this.ddl_projectuser.DataValueField = "code_name";
            this.ddl_projectuser.DataBind();
            this.ddl_projectuser.Items.Insert(0, new ListItem("ALL", ""));

            DataTable tbl = GetBase.Getzzr_Dept();
            fun.initDropDownList(ddl_dept, tbl, "dept_name", "dept_name");
            ddl_dept.Items.Insert(0, new ListItem("", "")); 

            QueryYear();
            ChartServices.SetChartTitle(this.WebChartControl1, true, "项目节点任务及时完成年度统计", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);
            ChartServices.SetChartTitle(this.WebChartControl2, true, "延迟状况年度分布", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);

            txtmonth.Text = DateTime.Now.ToString("yyyy");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.ToString("yyyy"));

            ChartServices.SetChartTitle(this.WebChartControl3, true, "项目节点任务及时完成月份统计", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);
            ChartServices.SetChartTitle(this.WebChartControl4, true, "延迟状况月份分布", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);
        }
    }
    public void QueryYear()
    {
        DataSet ds = Query("exec  usp_XM_RPT_TJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','年','" + ddl_dept.SelectedValue + "','" + ddl_projectuser.SelectedValue + "',''");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewYear.DataSource = ds.Tables[0];
            GridViewYear.DataBind();
            lblYear.Text = "年";

            ViewState["tblYear"] = ds.Tables[1];
            ViewState["tblcs"] = ds.Tables[2];
            CreatePointYear();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('查无数据，请重新选择查询条件！')", true);
            GridViewYear.DataSource = null;
            GridViewYear.DataBind();
            GridViewDay.DataSource = null;
            GridViewDay.DataBind();
            return;
        }

    }
    public void QueryMonth(string year)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        WebChartControl3.Series.Clear();
        WebChartControl4.Series.Clear();

        DataSet ds = Query("exec  usp_XM_RPT_TJ '" + year + "','" + dropMonth.SelectedValue + "','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','月','" + ddl_dept.SelectedValue + "','" + ddl_projectuser.SelectedValue + "',''");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewMonth.DataSource = ds.Tables[0];
            GridViewMonth.DataBind();
            lblMonth.Text = dropYear.SelectedValue + "年";
            ViewState["tblMonth"] = ds.Tables[1];
            ViewState["tblMonth_yq"] = ds.Tables[2];
            //ViewState["tblMonth_yq4"] = ds.Tables[3];
            //ViewState["tblMonth_yq7"] = ds.Tables[4];
            setGridLink();
            CreatePointMonth();
            CreatePointYear();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('查无数据，请重新选择查询条件！')", true);
            GridViewMonth.DataSource = null;
            GridViewMonth.DataBind();
            return;
        }
     

    }
  
   

    public void CreatePointYear()
    {
        DataTable dt = (DataTable)ViewState["tblYear"];
        ChartServices.DrawChart(this.WebChartControl1, "任务完成率目标值", ViewType.Line, (DataTable)ViewState["tblYear"], "period", "globs");
        ChartServices.DrawChart(this.WebChartControl1, "任务完成率实际值", ViewType.Line, (DataTable)ViewState["tblYear"], "period", "wcbs");
        ChartServices.SetAxisX(this.WebChartControl1, true, StringAlignment.Center, "年份", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl1, true, StringAlignment.Center, "完成率", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));

       
        ChartServices.DrawChart(this.WebChartControl2, "延迟次数：≤3天", ViewType.Line, (DataTable)ViewState["tblcs"], "period", "yq3bs");
        ChartServices.DrawChart(this.WebChartControl2, "延迟次数:4~7天", ViewType.Line, (DataTable)ViewState["tblcs"], "period", "yq4bs");
        ChartServices.DrawChart(this.WebChartControl2, "延迟次数:≥7天", ViewType.Line, (DataTable)ViewState["tblcs"], "period", "yq7bs");
        ChartServices.SetAxisX(this.WebChartControl2, true, StringAlignment.Center, "年份", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl2, true, StringAlignment.Center, "延迟数", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
    }
    



    public void CreatePointMonth()
    {
        DataTable dt = (DataTable)ViewState["tblMonth"];
        ChartServices.DrawChart(this.WebChartControl3, "任务完成率目标值", ViewType.Line, (DataTable)ViewState["tblMonth"], "period", "globs");
        ChartServices.DrawChart(this.WebChartControl3, "任务完成率实际值", ViewType.Line, (DataTable)ViewState["tblMonth"], "period", "wcbs");
        ChartServices.SetAxisX(this.WebChartControl3, true, StringAlignment.Center, "月份", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl3, true, StringAlignment.Center, "完成率", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));

       
        ChartServices.DrawChart(this.WebChartControl4, "延迟次数：≤3天", ViewType.Line, (DataTable)ViewState["tblMonth_yq"], "period", "yq3bs");
        ChartServices.DrawChart(this.WebChartControl4, "延迟次数:4~7天", ViewType.Line, (DataTable)ViewState["tblMonth_yq"], "period", "yq4bs");
        ChartServices.DrawChart(this.WebChartControl4, "延迟次数:≥7天", ViewType.Line, (DataTable)ViewState["tblMonth_yq"], "period", "yq7bs");
        ChartServices.SetAxisX(this.WebChartControl4, true, StringAlignment.Center, "月份", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl4, true, StringAlignment.Center, "延迟数", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
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
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        WebChartControl3.Series.Clear();
        WebChartControl4.Series.Clear();
        QueryYear();
        //month
        QueryMonth(dropYear.SelectedValue);
        lblMonth.Text = dropYear.SelectedValue+ "年";
        this.txtmonth.Text = dropYear.SelectedValue;
        if (txt_zrr.Value != "" || ddl_dept.SelectedValue!="" || ddl_projectuser.SelectedValue!="")
        {
            QueryDay(dropYear.SelectedValue, "", txt_zrr.Value);
        }
        else
        {
            GridViewDay.DataSource = null;
            GridViewDay.DataBind();
            lblDays.Text = "明细";
        }
   
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        setGridLink();
        QueryMonth(txtmonth.Text);
        lblMonth.Text = txtmonth.Text + "年";
    }

    public void QueryDay(string year,string month,string zrr)
    {                                                                   // '2016','12','2016/12/24','','','日'
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        WebChartControl3.Series.Clear();
        WebChartControl4.Series.Clear();
        DataSet ds = Query("exec  usp_XM_RPT_TJ '" + year + "','" + month + "','" + txtpgi_no.Text + "','" + zrr + "','日','" + ddl_dept.SelectedValue + "','" + ddl_projectuser.SelectedValue + "',''");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewDay.DataSource = ds.Tables[0];
            GridViewDay.DataBind();
            lblDays.Text = "明细";
            CreatePointYear();
            CreatePointMonth();
        }
        else
        {
            DataTable dt = new DataTable();
            GridViewDay.DataSource = dt;
            GridViewDay.DataBind();
        }

 
    }
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        setGridLink();
        QueryDay(txtmonth.Text,txtday.Text,txt_zrr.Value);
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "序号";
            e.Row.Cells[1].Text = "统计明细(月)";
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
            e.Row.Cells[1].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }

           

        }

    }
    protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i <= 4; i++)
            {
                if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                {
                    DateTime dt;
                    dt = Convert.ToDateTime(e.Row.Cells[i].Text.ToString());
                    e.Row.Cells[i].Text = dt.ToString("yyyy-MM-dd");
                }
            }
            if (e.Row.Cells[4].Text.Replace("&nbsp;", "") != "")
            {
                if (Convert.ToDateTime(e.Row.Cells[4].Text) > Convert.ToDateTime(e.Row.Cells[3].Text))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[4].Text == "1900-01-01")
                {
                    e.Row.Cells[4].Text = "";
                }
            }
        }
        
    }

    public void setGridLink()
    {
        GridViewRow dr= GridViewMonth.HeaderRow;
        
        for (int i = 7; i < GridViewMonth.Rows.Count; i++)
        {
           
            GridViewRow row = (GridViewRow)GridViewMonth.Rows[i];
            for (int j = 2; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = GridViewMonth.Rows[i].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDelay','')");
                lbtn.Attributes.Add("name", "delay");
                
                string strMon = dr.Cells[j].Text;
                string strDelayDays = Server.HtmlEncode(row.Cells[1].Text);
                if(strDelayDays.IndexOf("3天")>0)
                {
                    strDelayDays = "延迟次数：<=3";
                }
                else if(strDelayDays.IndexOf("4~7天")>0)
                {
                    strDelayDays = "延迟次数：4~7天";
                }
                else if (strDelayDays.IndexOf("7天") > 0)
                {
                    strDelayDays = "延迟次数：>7";
                }

                lbtn.Attributes.Add("mon",strMon);
                lbtn.Attributes.Add("DelayDays", strDelayDays);
               
                GridViewMonth.Rows[i].Cells[j].Controls.Add(lbtn);
            }
        }
    }


    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "序号";
            e.Row.Cells[1].Width = 150;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[1].Text = "统计明细(年)";
          
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
        else
        {
           // e.Row.Cells[0].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
            e.Row.Cells[1].Wrap = false;
        }
        
    }
    protected void LinkBtnDelay_Click(object sender, EventArgs e)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        WebChartControl3.Series.Clear();
        WebChartControl4.Series.Clear();
        setGridLink();
        string resaon = txtdelayreason.Text;
        string mnth = txtdelaymonth.Text;
        string year = txtmonth.Text;

        DataSet ds = Query("exec  usp_XM_RPT_TJ '" + year + "','" + mnth + "','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','delay','" + ddl_dept.SelectedValue + "','" + ddl_projectuser.SelectedValue + "','" + txtdelayreason.Text + "'");
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        lblDays.Text = resaon;
        CreatePointYear();
        CreatePointMonth();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
        WebChartControl3.Series.Clear();
        WebChartControl4.Series.Clear();
        QueryYear();
        //month
        QueryMonth(dropYear.SelectedValue);

        DataSet ds = Query("exec  usp_XM_RPT_TJ '','','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','undo','" + ddl_dept.SelectedValue + "','" + ddl_projectuser.SelectedValue + "',''");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewDay.DataSource = ds.Tables[0];
            GridViewDay.DataBind();
            lblDays.Text = "明细";
            CreatePointYear();
            CreatePointMonth();
        }
        else
        {
            DataTable dt = new DataTable();
            GridViewDay.DataSource = dt;
            GridViewDay.DataBind();
        }

     
    }
}