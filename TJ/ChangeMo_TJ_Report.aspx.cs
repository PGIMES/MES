using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TJ_ChangeMo_TJ_Report : System.Web.UI.Page
{
    decimal heji = 0;
    Function_Jinglian jl = new Function_Jinglian();
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
            //init 合金

            this.txt_moju_type.Text = "压铸模";
            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.ToString("MM"));
            QueryDay( DateTime.Now.ToString("MM-dd"));
            txtday.Text = DateTime.Now.ToString("MM-dd");

            string area = Request["quyu"].ToString();
            string strSQL = "select * from MES_Equipment where equip_type='压铸机' and equip_station_desc='" + area + "'  ";
            DataTable dt_type = SQLHelper.reDs(strSQL).Tables[0];
            this.selsbname.DataSource = dt_type;
            this.selsbname.DataTextField = "equip_name";
            this.selsbname.DataValueField = "equip_no";
            this.selsbname.DataBind();
            this.selsbname.Items.Insert(0, new ListItem("", ""));
           
           
        }
    }
    public void QueryYear()
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_ChangeMo_TJ '年','" + this.dropYear.SelectedValue + "','','','" + selsbname.SelectedValue + "','"+txt_moju_type.Text+"','" + this.dropshift.SelectedValue + "',''",3000000);

        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue + "年";

        ViewState["tblYear_ys"] = ds.Tables[1];
        ViewState["tblYear_cs"] = ds.Tables[2];
        ViewState["tblYear_zys"] = ds.Tables[3];
        bindChartYear_ys(ds.Tables[1]);
        bindChartYear_cs(ds.Tables[2]);
        bindChartYear_zys(ds.Tables[3]);

    }
    public void bindChartMonth(DataTable tbl)
    {
        ChartMonth_cs.DataSource = tbl;
        
        ChartMonth_cs.Series["单机换模次数"].XValueMember = "change_date";
        ChartMonth_cs.Series["单机换模次数"].YValueMembers = "avgcs";
    }
    public void bindChartMonth_dcys(DataTable tbl)
    {
        ChartMonth.DataSource = tbl;

        ChartMonth.Series["单次换模用时"].XValueMember = "change_date";
        ChartMonth.Series["单次换模用时"].YValueMembers = "avgtime";
    }
    public void bindChartMonth_ys(DataTable tbl)
    {
        ChartMonth_ys.DataSource = tbl;

        ChartMonth_ys.Series["换模用时"].XValueMember = "change_date";
        ChartMonth_ys.Series["换模用时"].YValueMembers = "ys";
    }

    public void bindChartYear_ys(DataTable tbl)
    {
        ChartYear_ys.DataSource = tbl;
        ChartYear_ys.Series["单次换模用时"].XValueMember = "mon";
        ChartYear_ys.Series["单次换模用时"].YValueMembers = "avgtime";
    }

    public void bindChartYear_zys(DataTable tbl)
    {
        ChartYear_zys.DataSource = tbl;
        ChartYear_zys.Series["月总换模用时"].XValueMember = "mon";
        ChartYear_zys.Series["月总换模用时"].YValueMembers = "ys";
    }

    public void bindChartYear_cs(DataTable tbl)
    {
        
        ChartYear_cs.DataSource = tbl;
        ChartYear_cs.Series["单机换模次数"].XValueMember = "mon";
        ChartYear_cs.Series["单机换模次数"].YValueMembers = "ts";
      

    }

    public void QueryMonth(string month)
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_ChangeMo_TJ '月','" + this.dropYear.SelectedValue + "','" + month + "','','" + selsbname.SelectedValue + "','" + txt_moju_type.Text + "','" + this.dropshift.SelectedValue + "',''", 3000000);
      
        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";
        ViewState["tblMonth"] = ds.Tables[1];
        ViewState["tblMonth_ys"] = ds.Tables[2];
        ViewState["tblMonth_dcys"] = ds.Tables[3];
        bindChartMonth((DataTable)ViewState["tblMonth"]);
        bindChartMonth_ys((DataTable)ViewState["tblMonth_ys"]);
        bindChartMonth_dcys((DataTable)ViewState["tblMonth_dcys"]);
        bindChartYear_ys((DataTable)ViewState["tblYear_ys"]);
        bindChartYear_cs((DataTable)ViewState["tblYear_cs"]);
        bindChartYear_zys((DataTable)ViewState["tblYear_zys"]);

    }
    public void QueryDay(string day)
    {
        DataSet ds = DbHelperSQL.Query("exec MES_SP_ChangeMo_TJ '日','" + this.dropYear.SelectedValue + "','','" + day + "','" + selsbname.SelectedValue + "','" + txt_moju_type.Text + "','" + this.dropshift.SelectedValue + "',''");
        Getsum(ds.Tables[0]);
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
        //lblDays.Text = txtmonth.Text + txtday.Text.PadLeft(2, '0');
        lblDays.Text = day;
        bindChartMonth((DataTable)ViewState["tblMonth"]);
        bindChartMonth_ys((DataTable)ViewState["tblMonth_ys"]);
        bindChartMonth_dcys((DataTable)ViewState["tblMonth_dcys"]);
        bindChartYear_ys((DataTable)ViewState["tblYear_ys"]);
        bindChartYear_cs((DataTable)ViewState["tblYear_cs"]);
        bindChartYear_zys((DataTable)ViewState["tblYear_zys"]);
    }

    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txtmonth.Text.Substring(4));
        DataSet ds = DbHelperSQL.Query("exec MES_SP_ChangeMo_TJ '日','" + this.dropYear.SelectedValue + "','','" + txtday.Text + "','" + selsbname.SelectedValue + "','" + txt_moju_type.Text + "','" + this.dropshift.SelectedValue + "',''");
        Getsum(ds.Tables[0]);
        GridViewDay.DataSource = ds.Tables[0];
        GridViewDay.DataBind();
    }
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        //QueryDay(txtmonth.Text + txtday.Text.PadLeft(2, '0'), txtmonth.Text.Substring(4));
        QueryDay( txtday.Text);
    }
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 1; i < e.Row.Cells.Count-2; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDays','')");
                lbtn.Attributes.Add("name", "day");
                lbtn.Style.Add("text-align", "center");
                e.Row.Cells[i].Wrap = false;
                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
               
            }
            e.Row.Cells[0].Wrap = false;

        }
        
    }
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 1; i < e.Row.Cells.Count-2; i++)
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
        string day =  dropMonth.SelectedValue.PadLeft(2, '0')+"-"+ dropDay.SelectedValue.PadLeft(2, '0');
        QueryDay(day);
        lblDays.Text = day;
    }

    private void Getsum(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["timer"].ToString() != "")
            {
                this.heji += Convert.ToDecimal(ldt.Rows[i]["timer"].ToString());
            }


        }
    }
    protected void GridViewDay_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "合计";
            e.Row.Cells[5].Text = this.heji.ToString();
        }
    }
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        int year = int.Parse(dropYear.Text);
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
    protected void GridViewYear_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridViewYear.PageIndex * this.GridViewYear.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Wrap = false;
                if (indexID == 5 || indexID == 7 || indexID == 2)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
    protected void GridViewMonth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridViewMonth.PageIndex * this.GridViewMonth.PageSize + e.Row.RowIndex + 1;

                if (indexID == 6 || indexID == 8 || indexID == 5)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}