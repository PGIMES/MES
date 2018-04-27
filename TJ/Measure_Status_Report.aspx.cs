using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TJ_Measure_Status_Report : System.Web.UI.Page
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
                txt_year.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }

            //初始化月份
            txt_month.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i < 13; i++)
            {
                txt_month.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            // dropMonth.SelectedValue = DateTime.Now.ToString("MM");
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份
            string sql_sjlb = "select id,time from dbo.JC_Category";
            DataSet ds = Query(sql_sjlb);
            fun.initDropDownList(ddl_sjlb, ds.Tables[0], "id", "id");
            this.ddl_sjlb.Items.Insert(0, new ListItem("", ""));

            string sql_xmh = "select distinct xmh from JCApply_Master ";
            DataSet xmh = Query(sql_xmh);
            fun.initDropDownList(ddl_xmh, xmh.Tables[0], "xmh", "xmh");
            this.ddl_xmh.Items.Insert(0, new ListItem("", ""));

            string sql_sb = "SELECT   * FROM      Machine_Sys ";
            DataSet sb = Query(sql_sb);
            fun.initDropDownList(ddl_sb, sb.Tables[0], "machine_name", "machine_name");
            this.ddl_sb.Items.Insert(0, new ListItem("", ""));

            QueryYear();
            txt_bymonth.Text = DateTime.Now.ToString("yyyyMM");
           
            QueryMonth(DateTime.Now.Month.ToString());

        }
    }

    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["connstringwt"];
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

    public void QueryYear()
    {
        DataSet ds = Query("exec usp_Measure_Status_Report '1','" + this.txt_year.SelectedValue + "','" + txt_month.Text + "','" + ddl_lab.SelectedValue + "','" + ddl_sjlb.SelectedValue + "','" + txt_ljh.Text + "','" + txt_gxh.Text + "','" + txt_csr.Text + "','" + ddl_sb.SelectedValue + "','" + ddl_xmh.SelectedValue+ "'");

        gv_bymonth.DataSource = ds.Tables[0];
        gv_bymonth.DataBind();
        GroupCol(gv_bymonth, 0);
        lblYear.Text = txt_year.SelectedValue + "年";
       
            ViewState["tblYear"] = ds.Tables[1];
            ViewState["tblYear_rate"] = ds.Tables[2];
            bindChartYear((DataTable)ViewState["tblYear"]);
            bindChartYear_rate((DataTable)ViewState["tblYear_rate"]);
       


    }
    public void bindChartYear_rate(DataTable tbl)
    {
        //零件合格率
       
      
        Chart_ljhgl.DataSource = tbl;
        Chart_ljhgl.Series["Series1"].XValueMember = "mnth";
        Chart_ljhgl.Series["Series1"].YValueMembers = "ljhgl";
        Chart_ljhgl.Series["Series2"].XValueMember = "mnth";
        Chart_ljhgl.Series["Series2"].YValueMembers = "pchgl";
    }
    public void bindChartDay_rate(DataTable tbl)
    {
        //零件合格率


        Chart_ljhgl_day.DataSource = tbl;
        Chart_ljhgl_day.Series["Series1"].XValueMember = "mnth";
        Chart_ljhgl_day.Series["Series1"].YValueMembers = "ljhgl";
        Chart_ljhgl_day.Series["Series2"].XValueMember = "mnth";
        Chart_ljhgl_day.Series["Series2"].YValueMembers = "pchgl";
    }
    public void bindChartYear(DataTable tbl)
    {
        DataView ldv = tbl.DefaultView;
        ldv.RowFilter = "type='测量及时完成率%'";
        DataTable db_jsl = ldv.ToTable();
       
        //及时率
        Chart_jsl.DataSource = db_jsl;
        Chart_jsl.Series["Series2"].XValueMember = "mnth";
        Chart_jsl.Series["Series2"].YValueMembers = "sl";
        //测量效率
        DataView ldv_xl = tbl.DefaultView;
        ldv_xl.RowFilter = "type='测量效率(人)%'";
        DataTable db_xl = ldv_xl.ToTable();
        Chart_xl.DataSource = db_xl;
        Chart_xl.Series["Series1"].XValueMember = "mnth";
        Chart_xl.Series["Series1"].YValueMembers = "sl";
        
     

    }
    public void bindChartDay(DataTable tbl)
    {
        DataView ldv = tbl.DefaultView;
        ldv.RowFilter = "type='测量及时完成率%'";
        DataTable db_jsl = ldv.ToTable();

        //及时率
        Chart_jsl_day.DataSource = db_jsl;
        Chart_jsl_day.Series["Series2"].XValueMember = "mnth";
        Chart_jsl_day.Series["Series2"].YValueMembers = "sl";
        //测量效率
        DataView ldv_xl = tbl.DefaultView;
        ldv_xl.RowFilter = "type='测量效率(人)%'";
        DataTable db_xl = ldv_xl.ToTable();
        Chart_xl_day.DataSource = db_xl;
        Chart_xl_day.Series["Series1"].XValueMember = "mnth";
        Chart_xl_day.Series["Series1"].YValueMembers = "sl";



    }


    public void QueryMonth(string month)
    {
        DataSet ds = Query("exec usp_Measure_Status_Report '2','" + this.txt_year.SelectedValue + "','" + month + "','" + ddl_lab.SelectedValue + "','" + ddl_sjlb.SelectedValue + "','" + txt_ljh.Text + "','" + txt_gxh.Text + "','" + txt_csr.Text + "','" + ddl_sb.SelectedValue + "','" + ddl_xmh.SelectedValue + "'");

        gv1.DataSource = ds.Tables[0];
        gv1.DataBind();
        GroupCol(gv1, 0);
        lblMonth.Text = txt_bymonth.Text + "月";
        ViewState["tblMonth"] = ds.Tables[1];
        ViewState["tblMonth_rate"] = ds.Tables[2];
        bindChartDay((DataTable)ViewState["tblMonth"]);
        bindChartDay_rate((DataTable)ViewState["tblMonth_rate"]);
        bindChartYear((DataTable)ViewState["tblYear"]);
        bindChartYear_rate((DataTable)ViewState["tblYear_rate"]);
    }

    protected void txt_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkBtn.Style.Add("display", "none");
        LinkBtnMonth.Style.Add("display", "none");
        txt_bymonth.Style.Add("display", "none");
    }
    protected void gv_bymonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                e.Row.Cells[0].Wrap = false;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "mnth");
                e.Row.Cells[i].Controls.Add(lbtn);
            }

        }
        else
        {
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

            }
        }
    }
    protected void gv_bymonth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Replace("&nbsp;", "") == "")
            {
                e.Row.Cells[1].ColumnSpan = 2;
                e.Row.Cells[0].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].ColumnSpan = 2;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Text = "日期(月)";
        }
    }
    protected void gv1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Wrap = false;
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;//隐藏排序ord栏位
        e.Row.Cells[1].Wrap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Replace("&nbsp;", "") == "")
            {
                e.Row.Cells[1].ColumnSpan = 2;
                e.Row.Cells[0].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].ColumnSpan = 2;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Text = "日期(月/日)";
           // e.Row.Cells[1].Wrap = false;
        }
        
       
        
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txt_bymonth.Text);
    }
    protected void LinkBtnMonth_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryYear();
        //month
        QueryMonth(txt_month.SelectedValue.PadLeft(2, '0'));
        lblMonth.Text = txt_year.SelectedValue.PadLeft(2, '0') + txt_month.SelectedValue.PadLeft(2, '0') + "月";
  
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://172.16.5.26:8060/home/home.aspx");
    }

    public static void GroupCol(GridView gridView, int cols)
    {
        if (gridView.Rows.Count < 1 || cols > gridView.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc = gridView.Rows[0].Cells[cols];
        for (int i = 1; i < gridView.Rows.Count; i++)
        {
            TableCell tc = gridView.Rows[i].Cells[cols];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                }
                oldTc.RowSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                oldTc = tc;
            }
        }
    }
}