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

public partial class XM_APQP_Rate_TJ : System.Web.UI.Page
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

            string lssql = "select distinct pgino from ProductList ";
            this.txtpgi_no.DataSource = Query(lssql);
            this.txtpgi_no.DataTextField = "pgino";
            this.txtpgi_no.DataValueField = "pgino";
            this.txtpgi_no.DataBind();
            this.txtpgi_no.Items.Insert(0, new ListItem("", ""));

            DataTable tbl = GetBase.Getzzr_Dept();
            fun.initDropDownList(ddl_dept, tbl, "dept_name", "dept_name");
            ddl_dept.Items.Insert(0, new ListItem("", "")); 

            QueryYear();
            ChartServices.SetChartTitle(this.WebChartControl1, true, "统计图(年)", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);
           

            txtmonth.Text = DateTime.Now.ToString("yyyy");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
            ChartServices.SetChartTitle(this.WebChartControl2, true, "统计图(月)", true, 1, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 10, FontStyle.Bold), Color.Red, 0);

            
        }
    }
    public void QueryYear()
    {
        DataSet ds = Query("exec  usp_XM_RPT_JSLTJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','年','"+ddl_dept.SelectedValue+"'");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewYear.DataSource = ds.Tables[0];
            GridViewYear.DataBind();
            lblYear.Text = dropYear.SelectedValue+"年";

            ViewState["tblYear"] = ds.Tables[0];
            ViewState["tblYearTop"] = ds.Tables[1];

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
        setGridLink();
    }
    public void QueryMonth(string year,string month)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();


        DataSet ds = Query("exec  usp_XM_RPT_JSLTJ '" + year + "','" + month + "','" + txtpgi_no.Text + "','" + txt_zrr.Value + "','月','"+ddl_dept.SelectedValue+"'");
        if (ds != null && ds.Tables.Count > 0)
        {
            GridViewMonth.DataSource = ds.Tables[0];
            GridViewMonth.DataBind();
            lblMonth.Text = dropYear.SelectedValue + "年"+month+"月";
            ViewState["tblMonth"] = ds.Tables[0];
            ViewState["tblMonthTop"] = ds.Tables[1];

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

        ChartServices.DrawChart(this.WebChartControl1, "完成率", ViewType.Bar, (DataTable)ViewState["tblYearTop"], "file_zzr", "wcl");
        ChartServices.DrawChart(this.WebChartControl1, "及时率", ViewType.Bar, (DataTable)ViewState["tblYearTop"], "file_zzr", "jsl");
        ChartServices.SetAxisX(this.WebChartControl1, true, StringAlignment.Center, "责任人", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl1, true, StringAlignment.Center, "完成率/及时率", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
    }




    public void CreatePointMonth()
    {

        ChartServices.DrawChart(this.WebChartControl2, "完成率", ViewType.Bar, (DataTable)ViewState["tblMonthTop"], "file_zzr", "wcl");
        ChartServices.DrawChart(this.WebChartControl2, "及时率", ViewType.Bar, (DataTable)ViewState["tblMonthTop"], "file_zzr", "jsl");
        ChartServices.SetAxisX(this.WebChartControl2, true, StringAlignment.Center, "责任人", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));
        ChartServices.SetAxisY(this.WebChartControl2, true, StringAlignment.Center, "完成率/及时率", Color.Red, true, new Font("宋体", 10, FontStyle.Bold));


      
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
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();
  
        QueryYear();
        //month
        QueryMonth(dropYear.SelectedValue,dropMonth.SelectedValue.PadLeft(2, '0'));
        lblMonth.Text = dropYear.SelectedValue + "年"+dropMonth.SelectedValue+"月";
        this.txtmonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0');
        if (txt_zrr.Value != "" || ddl_dept.SelectedValue!="")
        {
            QueryDay(dropYear.SelectedValue, dropMonth.SelectedValue, txt_zrr.Value);
        }
        else
        {
            GridViewDay.DataSource = null;
            GridViewDay.DataBind();
            lblDays.Text = "明细";
        }
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {

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
            if (e.Row.Cells[4].Text.Replace("&nbsp;","") != "")
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
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
       
    }
    public void QueryDay(string year, string month,string zrr)
    {                                                                   // '2016','12','2016/12/24','','','日'
        WebChartControl1.Series.Clear();
        WebChartControl2.Series.Clear();

        DataSet ds = Query("exec  usp_XM_RPT_JSLTJ '" + year + "','" + month + "','" + txtpgi_no.Text + "','" + zrr + "','日','"+ddl_dept.SelectedValue+"'");
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        lblDays.Text = zrr;
        CreatePointYear();
        CreatePointMonth();


    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        
        setGridLink();
        string zrr = txtmonth.Text;
        QueryDay(dropYear.SelectedValue, dropMonth.SelectedValue, zrr);
    }
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {

    }
    protected void GridViewYear_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    public void setGridLink()
    {
        for (int i = 0; i < GridViewYear.Rows.Count; i++)
        {
            LinkButton lbtn = new LinkButton();
            lbtn.ID = "btn";
            lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
            // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
            lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
            lbtn.Attributes.Add("name", "mon");

            GridViewYear.Rows[i].Cells[0].Controls.Add(lbtn);
        }
    }
}