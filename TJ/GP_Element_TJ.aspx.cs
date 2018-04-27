using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TJ_GP_Element_TJ : System.Web.UI.Page
{
    decimal avg_si = 0;
    decimal avg_fe = 0;
    decimal avg_cu = 0;
    decimal avg_mg = 0;
    decimal avg_sf = 0;
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
           // dropMonth.SelectedValue = DateTime.Now.ToString("MM");
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份
            dropDay.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 31; i++)
            {
                dropDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            //dropDay.SelectedValue = DateTime.Now.ToString("dd");
            dropDay.SelectedValue = DateTime.Now.Day.ToString();
            //init 合金
            fun.initHeJin(selHeJin);
            //selHeJin.Items.Insert(0, new ListItem("", ""));

            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.ToString("MM"));
            QueryDay(DateTime.Now.ToString("MM-dd"));
        }
    }
    public void QueryYear()
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_GPElement_TJ '年','" + this.dropYear.SelectedValue + "','','','" + dropshift.SelectedValue + "','" + this.dropsource.SelectedValue + "','" + selHeJin.SelectedValue+ "'");

        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue + "年";

        ViewState["tblYear"] = ds.Tables[1];
        bindChartYear((DataTable)ViewState["tblYear"]);
     

    }
     
    public void bindChartYear(DataTable tbl)
    {
        ChartYear_Si.DataSource = tbl;
        ChartYear_Fe.DataSource = tbl;
        ChartYear_Cu.DataSource = tbl;
        ChartYear_Mg.DataSource = tbl;
        ChartYear_Sf.DataSource = tbl;
        SetMap();
   
        ChartYear_Si.Series["Si"].XValueMember = "confirm_date";
        ChartYear_Si.Series["Si"].YValueMembers = "si";

        ChartYear_Fe.Series["Fe"].XValueMember = "confirm_date";
        ChartYear_Fe.Series["Fe"].YValueMembers = "Fe";

        ChartYear_Cu.Series["Cu"].XValueMember = "confirm_date";
        ChartYear_Cu.Series["Cu"].YValueMembers = "Cu";

        ChartYear_Mg.Series["Mg"].XValueMember = "confirm_date";
        ChartYear_Mg.Series["Mg"].YValueMembers = "Mg";

        ChartYear_Sf.Series["Sf"].XValueMember = "confirm_date";
        ChartYear_Sf.Series["Sf"].YValueMembers = "Sf";

    }

    public void bindChartMonth(DataTable tbl)
    {
        ChartMonth_Si.DataSource = tbl;
        ChartMonth_Fe.DataSource = tbl;
        ChartMonth_Cu.DataSource = tbl;
        ChartMonth_Mg.DataSource = tbl;
        ChartMonth_Sf.DataSource = tbl;
        SetMap_Month();

        ChartMonth_Si.Series["Si"].XValueMember = "confirm_date";
        ChartMonth_Si.Series["Si"].YValueMembers = "si";
        this.ChartMonth_Si.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        ChartMonth_Fe.Series["Fe"].XValueMember = "confirm_date";
        ChartMonth_Fe.Series["Fe"].YValueMembers = "Fe";
        this.ChartMonth_Fe.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        ChartMonth_Cu.Series["Cu"].XValueMember = "confirm_date";
        ChartMonth_Cu.Series["Cu"].YValueMembers = "Cu";
        this.ChartMonth_Cu.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        ChartMonth_Mg.Series["Mg"].XValueMember = "confirm_date";
        ChartMonth_Mg.Series["Mg"].YValueMembers = "Mg";
        this.ChartMonth_Mg.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        ChartMonth_Sf.Series["Sf"].XValueMember = "confirm_date";
        ChartMonth_Sf.Series["Sf"].YValueMembers = "Sf";
        this.ChartMonth_Sf.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

    }


    public void QueryMonth(string month)
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_GPElement_TJ '月','" + this.dropYear.SelectedValue + "','" + month+ "','','" + dropshift.SelectedValue + "','" + this.dropsource.SelectedValue + "','" + selHeJin.SelectedValue + "'");
        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";
        ViewState["tblMonth"] = ds.Tables[1];
        bindChartYear((DataTable)ViewState["tblYear"]);
        bindChartMonth((DataTable)ViewState["tblMonth"]);

    }

    public void QueryDay(string day)
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_GPElement_TJ '日','" + this.dropYear.SelectedValue + "','','" + day + "','" + dropshift.SelectedValue + "','" + this.dropsource.SelectedValue + "','" + selHeJin.SelectedValue + "'");
        GridViewDay.DataSource = ds.Tables[0];
        Getavg(ds.Tables[0]);
        GridViewDay.DataBind();
        //lblDays.Text = txtmonth.Text + txtday.Text.PadLeft(2, '0');
        lblDays.Text = day;
       
        bindChartYear((DataTable)ViewState["tblYear"]);
        bindChartMonth((DataTable)ViewState["tblMonth"]);
   
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txtmonth.Text.Substring(4));

    }
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        //QueryDay(txtmonth.Text + txtday.Text.PadLeft(2, '0'), txtmonth.Text.Substring(4));
        QueryDay(txtday.Text);
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "元素";
            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
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
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "元素";
            //e.Row.Cells[0].Width=200;
            for (int i = 2; i < e.Row.Cells.Count - 5; i++)
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
    protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        QueryYear();
        //month
        QueryMonth(dropMonth.SelectedValue.PadLeft(2, '0'));
        lblMonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        //Day
        string day = dropMonth.SelectedValue.PadLeft(2, '0') + "-" + dropDay.SelectedValue.PadLeft(2, '0');
        QueryDay(day);
        lblDays.Text = day;
    }

    private void Getavg(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            
                this.avg_si += Convert.ToDecimal(ldt.Rows[i]["si"].ToString());
                this.avg_fe += Convert.ToDecimal(ldt.Rows[i]["fe"].ToString());
                this.avg_cu += Convert.ToDecimal(ldt.Rows[i]["cu"].ToString());
                this.avg_mg += Convert.ToDecimal(ldt.Rows[i]["mg"].ToString());
                if (!string.IsNullOrEmpty(ldt.Rows[i]["sf"].ToString()))
                {
                    this.avg_sf += Convert.ToDecimal(ldt.Rows[i]["sf"].ToString());
                }
        }
        if (ldt.Rows.Count > 0)
        {
            avg_si = avg_si / ldt.Rows.Count;
            avg_fe = avg_fe / ldt.Rows.Count;
            avg_cu = avg_cu / ldt.Rows.Count;
            avg_mg = avg_mg / ldt.Rows.Count;
            avg_sf = avg_sf / ldt.Rows.Count;
        }
    }

    protected void GridViewDay_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "合计";
            e.Row.Cells[5].Text = this.avg_si.ToString("N2");
            e.Row.Cells[6].Text = this.avg_fe.ToString("N2");
            e.Row.Cells[7].Text = this.avg_cu.ToString("N2");
            e.Row.Cells[8].Text = this.avg_mg.ToString("N2");
            e.Row.Cells[9].Text = this.avg_sf.ToString("N2");

        }
    }
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        int year =int.Parse( dropYear.Text);
        int month = int.Parse(dropMonth.Text);
        int lastDayOfThisMonth = DateTime.DaysInMonth(year, month);
        dropDay.SelectedValue = lastDayOfThisMonth.ToString();

        if (dropMonth.Text == System.DateTime.Now.Month.ToString())
        {
            dropDay.SelectedValue = DateTime.Now.Day.ToString();
        }
        LinkBtn.Style.Add("display", "none");
        LinkBtnDays.Style.Add("display", "none");
        txtmonth.Style.Add("display", "none");
        txtday.Style.Add("display", "none");
    }

    private void SetMap()
    {



        if (this.selHeJin.Text == "A380")
        {
            this.ChartYear_Si.ChartAreas[0].AxisY.Minimum = 7.5;
            this.ChartYear_Si.ChartAreas[0].AxisY.Maximum = 10;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.001;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].Text = "8.001";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.001";



            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.199;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].Text = "8.199";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.199";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 9.3;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].Text = "9.31";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "9.31";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 9.499;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].Text = "9.499";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "9.499";

            this.ChartYear_Fe.ChartAreas[0].AxisY.Minimum = 0.7;
            this.ChartYear_Fe.ChartAreas[0].AxisY.Maximum = 1.1;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.801;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.801";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.801";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.809;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.809";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.809";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.951;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "0.951";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.951";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.999;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "0.999";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.999";

            this.ChartYear_Cu.ChartAreas[0].AxisY.Minimum = 3.1;
            this.ChartYear_Cu.ChartAreas[0].AxisY.Maximum = 4.1;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 3.251;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "3.251";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "3.251";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 3.299;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "3.299";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "3.299";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.9;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "3.9";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.9";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.999;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.999";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.999";

            this.ChartYear_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Mg.ChartAreas[0].AxisY.Maximum = 0.4;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.151;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.151";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.151";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.169;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.169";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.169";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.281;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.281";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.281";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.299;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.299";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.299";


            this.ChartYear_Sf.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Sf.ChartAreas[0].AxisY.Maximum = 2.5;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";

      

        }
        else if (this.selHeJin.Text == "EN46000")
        {
            this.ChartYear_Si.ChartAreas[0].AxisY.Minimum = 7;
            this.ChartYear_Si.ChartAreas[0].AxisY.Maximum = 11;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.001;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].Text = "8.001";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.001";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.749;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].Text = "8.749";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.749";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 10.26;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].Text = "10.26";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "10.26";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 10.999;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].Text = "10.999";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "10.999";

            this.ChartYear_Fe.ChartAreas[0].AxisY.Minimum = 0.4;
            this.ChartYear_Fe.ChartAreas[0].AxisY.Maximum = 1.4;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.3;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.3";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.3";


            this.ChartYear_Cu.ChartAreas[0].AxisY.Minimum = 1.5;
            this.ChartYear_Cu.ChartAreas[0].AxisY.Maximum = 4.5;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 2.001;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "2.001";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "2.001";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 2.499;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "2.499";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "2.499";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.51;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "3.51";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.51";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.999;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.999";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.999";

            this.ChartYear_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Mg.ChartAreas[0].AxisY.Maximum = 0.7;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.051;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.051";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.051";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.174;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.174";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.174";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.426;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.426";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.426";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.549;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.549";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.549";

            this.ChartYear_Sf.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Sf.ChartAreas[0].AxisY.Maximum = 2.5;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";


        }
        else if (this.selHeJin.Text == "EN47100")
        {
            this.ChartYear_Si.ChartAreas[0].AxisY.Minimum = 10.3;
            this.ChartYear_Si.ChartAreas[0].AxisY.Maximum = 13.8;


            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 10.501;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].Text = "10.501";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "10.501";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.799;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].Text = "10.799";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.799";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 13.201;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].Text = "13.201";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "13.201";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 13.5;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].Text = "13.5";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "13.5";


            this.ChartYear_Fe.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartYear_Fe.ChartAreas[0].AxisY.Maximum = 1.5;

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.651;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.651";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.651";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.2;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.2";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.2";

            this.ChartYear_Cu.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartYear_Cu.ChartAreas[0].AxisY.Maximum = 1.5;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.701;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "0.701";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.701";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.749;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "0.749";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.749";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.151;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "1.151";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.151";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.2;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "1.2";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.2";

            this.ChartYear_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Mg.ChartAreas[0].AxisY.Maximum = 0.7;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.001;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.001";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.001";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.049;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.049";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.049";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.29;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.29";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.29";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.35;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.35";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.35";

            this.ChartYear_Sf.Visible = false;


        }

        else if (this.selHeJin.Text == "ADC12")
        {
            this.ChartYear_Si.ChartAreas[0].AxisY.Minimum = 9.5;
            this.ChartYear_Si.ChartAreas[0].AxisY.Maximum = 13;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 9.601;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].Text = "9.601";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "9.601";


            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.399;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].Text = "10.399";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.399";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 11.2;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].Text = "11.2";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "11.2";

            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 11.999;
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].Text = "11.999";
            this.ChartYear_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "11.999";

            this.ChartYear_Fe.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartYear_Fe.ChartAreas[0].AxisY.Maximum = 1.5;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.3;
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.3";
            this.ChartYear_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.3";

            this.ChartYear_Cu.ChartAreas[0].AxisY.Minimum = 1.2;
            this.ChartYear_Cu.ChartAreas[0].AxisY.Maximum = 5;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 1.501;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "1.501";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "1.501";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 1.799;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "1.799";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "1.799";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 2.84;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "2.84";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "2.84";

            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.499;
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.499";
            this.ChartYear_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.499";

            this.ChartYear_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartYear_Mg.ChartAreas[0].AxisY.Maximum = 1;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.001;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.001";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.001";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.049;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.049";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.049";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.271;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.271";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.271";

            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.299;
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.299";
            this.ChartYear_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.299";


            this.ChartYear_Sf.ChartAreas[0].AxisY.Minimum = 0.2;
            this.ChartYear_Sf.ChartAreas[0].AxisY.Maximum = 2;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartYear_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";




        }


    }

    private void SetMap_Month()
    {
        if (this.selHeJin.Text == "A380")
        {
            this.ChartMonth_Si.ChartAreas[0].AxisY.Minimum = 7.5;
            this.ChartMonth_Si.ChartAreas[0].AxisY.Maximum = 10;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.001;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].Text = "8.001";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.001";



            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.199;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].Text = "8.199";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.199";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 9.3;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].Text = "9.31";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "9.31";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 9.499;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].Text = "9.499";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "9.499";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.Minimum = 0.7;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.Maximum = 1.1;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.801;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.801";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.801";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.809;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.809";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.809";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.951;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "0.951";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.951";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.999;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "0.999";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.999";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.Minimum = 3.1;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.Maximum = 4.1;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 3.251;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "3.251";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "3.251";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 3.299;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "3.299";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "3.299";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.9;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "3.9";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.9";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.999;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.999";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.999";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.Maximum = 0.4;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.151;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.151";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.151";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.169;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.169";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.169";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.281;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.281";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.281";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.299;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.299";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.299";


            this.ChartMonth_Sf.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.Maximum = 2.5;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";

      

        }
        else if (this.selHeJin.Text == "EN46000")
        {
            this.ChartMonth_Si.ChartAreas[0].AxisY.Minimum = 7;
            this.ChartMonth_Si.ChartAreas[0].AxisY.Maximum = 11;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.001;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].Text = "8.001";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.001";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.749;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].Text = "8.749";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.749";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 10.26;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].Text = "10.26";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "10.26";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 10.999;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].Text = "10.999";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "10.999";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.Minimum = 0.4;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.Maximum = 1.4;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.3;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.3";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.3";


            this.ChartMonth_Cu.ChartAreas[0].AxisY.Minimum = 1.5;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.Maximum = 4.5;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 2.001;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "2.001";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "2.001";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 2.499;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "2.499";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "2.499";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.51;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "3.51";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.51";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.999;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.999";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.999";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.Maximum = 0.7;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.051;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.051";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.051";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.174;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.174";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.174";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.426;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.426";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.426";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.549;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.549";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.549";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.Maximum = 2.5;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";


        }
        else if (this.selHeJin.Text == "EN47100")
        {
            this.ChartMonth_Si.ChartAreas[0].AxisY.Minimum = 10.3;
            this.ChartMonth_Si.ChartAreas[0].AxisY.Maximum = 13.8;


            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 10.501;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].Text = "10.501";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "10.501";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.799;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].Text = "10.799";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.799";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 13.201;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].Text = "13.201";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "13.201";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 13.5;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].Text = "13.5";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "13.5";


            this.ChartMonth_Fe.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.Maximum = 1.5;

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.651;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.651";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.651";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.2;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.2";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.2";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.Maximum = 1.5;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.701;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "0.701";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.701";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.749;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "0.749";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.749";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.151;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "1.151";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.151";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.2;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "1.2";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.2";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.Maximum = 0.7;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.001;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.001";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.001";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.049;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.049";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.049";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.29;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.29";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.29";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.35;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.35";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.35";

            this.ChartMonth_Sf.Visible = false;


        }

        else if (this.selHeJin.Text == "ADC12")
        {
            this.ChartMonth_Si.ChartAreas[0].AxisY.Minimum = 9.5;
            this.ChartMonth_Si.ChartAreas[0].AxisY.Maximum = 13;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 9.601;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].Text = "9.601";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[0].ToolTip = "9.601";


            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.399;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].Text = "10.399";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.399";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 11.2;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].Text = "11.2";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[2].ToolTip = "11.2";

            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 11.999;
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].Text = "11.999";
            this.ChartMonth_Si.ChartAreas[0].AxisY.StripLines[3].ToolTip = "11.999";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.Minimum = 0.5;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.Maximum = 1.5;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.3;
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].Text = "1.3";
            this.ChartMonth_Fe.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.3";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.Minimum = 1.2;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.Maximum = 5;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 1.501;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].Text = "1.501";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[0].ToolTip = "1.501";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 1.799;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].Text = "1.799";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[1].ToolTip = "1.799";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 2.84;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].Text = "2.84";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[2].ToolTip = "2.84";

            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.499;
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].Text = "3.499";
            this.ChartMonth_Cu.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.499";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.Minimum = 0;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.Maximum = 1;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.001;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].Text = "0.001";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.001";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.049;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].Text = "0.049";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.049";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.271;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].Text = "0.271";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.271";

            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.299;
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].Text = "0.299";
            this.ChartMonth_Mg.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.299";


            this.ChartMonth_Sf.ChartAreas[0].AxisY.Minimum = 0.2;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.Maximum = 2;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.401;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].Text = "0.401";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.401";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.599;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].Text = "0.599";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.599";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.999;
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].Text = "1.999";
            this.ChartMonth_Sf.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.999";



        }
       





    }
}