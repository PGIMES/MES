using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class TJ_JingLian_TJ_Report : System.Web.UI.Page
{
    Function_Jinglian jl = new Function_Jinglian();
   
    string xx = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                txt_year.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }
            for (int i = 1; i <= 12; i++)
            {
                this.txt_month.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            txt_date.Text = date;
            Query_WD(txt_month.SelectedValue);
            

        }
    }
    public void Query_WD(string mnth)
    {
        
        lblYear.Text = txt_year.SelectedValue + "年";
        lblMonth.Text = mnth + "月";
        string month = Convert.ToDateTime(txt_date.Text).ToString("MM-dd");
        if (txtmonth.Text == "" && txt_date.Text != "")
        {
            Label1.Text = Convert.ToDateTime(txt_date.Text).ToString("MM-dd");
        }
        else
        {
            Label1.Text = txtmonth.Text;
        }
        DataTable dt_month = jl.Jinglian_TJ_Report(1, txt_year.Text, txt_month.Text, ddl_banzhu.Text, "").Tables[0];
        gv_bymonth.DataSource = dt_month;
        gv_bymonth.DataBind();

        DataTable dt = jl.Jinglian_TJ_Report(5, txt_year.Text, mnth, ddl_banzhu.Text, txt_date.Text).Tables[0];
        gv1.DataSource = dt;
        gv1.DataBind();


        DataTable dt_hj = jl.Jinglian_TJ_Report(3, txt_year.Text, mnth, ddl_banzhu.Text, month).Tables[0];
        gv_hj.DataSource = dt_hj;
        gv_hj.DataBind();

        ViewState["tblMD"] = jl.Jinglian_TJ_Report(1, txt_year.Text, txt_month.Text, ddl_banzhu.Text, "").Tables[1];
        ViewState["tblWD"] = jl.Jinglian_TJ_Report(1, txt_year.Text, txt_month.Text, ddl_banzhu.Text, "").Tables[2];
        bindChart_WD((DataTable)ViewState["tblWD"]);
        bindChart_MD((DataTable)ViewState["tblMD"]);
        bindChart_MD_Mnth(mnth);
        bindChart_WD_Mnth(mnth);
    }


    public void bindChart_WD(DataTable tbl)
    {
       
        C1.DataSource = tbl;
        C1.Series["Series1"].XValueMember = "mon";
        C1.Series["Series1"].YValueMembers = "value";
        this.C1.ChartAreas[0].AxisY.Minimum = 650;
        this.C1.ChartAreas[0].AxisY.Maximum = 750;
      
    }

    public void bindChart_MD(DataTable tbl)
    {
        C2.DataSource = tbl;
        C2.Series["Series2"].XValueMember = "mon";
        C2.Series["Series2"].YValueMembers = "value";
        this.C2.ChartAreas[0].AxisY.Minimum = 2.5;
        this.C2.ChartAreas[0].AxisY.Maximum = 2.8;
    }
    public void bindChart_MD_Mnth(string mnth)
    {
        DataTable md = jl.Jinglian_TJ_Report(2, txt_year.Text, mnth, ddl_banzhu.Text, "").Tables[0];

        C3.DataSource = md;
        C3.Series["密度"].XValueMember = "mon";
        C3.Series["密度"].YValueMembers = "midu";
        this.C3.ChartAreas[0].AxisY.Minimum = 2.5;
        this.C3.ChartAreas[0].AxisY.Maximum = 2.8;

    }

    public void bindChart_WD_Mnth(string mnth)
    {
        DataTable md = jl.Jinglian_TJ_Report(21, txt_year.Text, mnth, ddl_banzhu.Text, "").Tables[0];

        C4.DataSource = md;
        C4.Series["温度"].XValueMember = "mon";
        C4.Series["温度"].YValueMembers = "wd";
        this.C4.ChartAreas[0].AxisY.Minimum = 650;
        this.C4.ChartAreas[0].AxisY.Maximum = 750;

    }
   private void SetMap( string date)
    {



        this.C1.ChartAreas[0].AxisY.Minimum = 500;
        this.C1.ChartAreas[0].AxisY.Maximum = 1000;
        //this.C2.ChartAreas[0].AxisY.Minimum = 2.5;
        //this.C2.ChartAreas[0].AxisY.Maximum = 2.8;
   

    //        this.C1.DataSource = ldv_wd;
    //        this.C2.DataSource = ldv_md;
    //        this.C1.Series["Series1"].XValueMember = "t";
    //        this.C1.Series["Series1"].YValueMembers = "Hd_af_wd";
    //        this.C2.Series["Series2"].XValueMember = "t";
    //        this.C2.Series["Series2"].YValueMembers = "midu";

    //        this.C1.DataBind();//绑定数据
    //        this.C2.DataBind();
    //        for (int i = 0; i < dt2_wd.Rows.Count; i++)
    //        {

    //            this.C1.Series["Series1"].Points[i].AxisLabel = dt2_wd.Rows[i]["t"].ToString();

    //            this.C1.Series["Series1"].Points[i].ToolTip = "[温度]" + dt2_wd.Rows[i]["t"].ToString() + "(" + dt2_wd.Rows[i]["Hd_af_wd"].ToString() + ")";

    //        }
    //        for (int i = 0; i < dt2_md.Rows.Count; i++)
    //        {

    //            this.C2.Series["Series2"].Points[i].AxisLabel = dt2_md.Rows[i]["t"].ToString();

    //            this.C2.Series["Series2"].Points[i].ToolTip = "[温度]" + dt2_md.Rows[i]["t"].ToString() + "(" + dt2_md.Rows[i]["midu"].ToString() + ")";

    //        }
    //        this.C1.Series["Series1"].LegendText = "温度";

    //        this.C1.Series["Series1"].LegendToolTip = "温度";

    //        this.C2.Series["Series2"].LegendText = "密度";

    //        this.C2.Series["Series2"].LegendToolTip = "密度";


    //       // this.C1.Series["Series1"].Enabled = false;

    }
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        Query_WD(txt_month.SelectedValue);
        Label1.Text = Convert.ToDateTime(txt_date.Text).ToString("MM-dd");

       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void gv1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "描述";
            e.Row.Cells[0].Width = 70;
           // e.Row.Cells[0].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                //  lbtn.Click = Query_Click;        
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                // lbtn.OnClientClick = "return getMonth();";
                lbtn.Attributes.Add("name", "mon");
                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else 
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                
            }        
        }

        foreach (GridViewRow row in gv1.Rows)
        {
            for (int i = 0; i < gv1.Controls.Count; i++)
            {
                gv1.Rows[row.RowIndex].Cells[0].Wrap = false;
            }
        }
    }

    protected void LinkBtn_Click(object sender, EventArgs e)
    {

        
        //Query_WD(txtmonth.Text);
        int s = txtmonth.Text.Trim().IndexOf("-");
        string mnth = txtmonth.Text.Trim().Substring(0,2);
        DataTable dt_hj = jl.Jinglian_TJ_Report(3, txt_year.Text, mnth, ddl_banzhu.Text, txtmonth.Text.Trim()).Tables[0];
        gv_hj.DataSource = dt_hj;
        gv_hj.DataBind();
        DataTable dt_month = jl.Jinglian_TJ_Report(1, txt_year.Text, txt_month.Text, ddl_banzhu.Text, "").Tables[0];
        gv_bymonth.DataSource = dt_month;
        gv_bymonth.DataBind();
        Label1.Text = txtmonth.Text;
        bindChart_WD((DataTable)ViewState["tblWD"]);
        bindChart_MD((DataTable)ViewState["tblMD"]);
        bindChart_WD_Mnth(mnth);
        bindChart_MD_Mnth(mnth);
      
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gv1.PageIndex * this.gv1.PageSize + e.Row.RowIndex + 1;
                if (indexID == 1)
                {
                    for( int i=3;i<e.Row.Cells.Count;i++)
                    {
                        decimal bs = 0;
                        bs =Math.Round( Convert.ToDecimal(e.Row.Cells[i].Text.ToString()),0);
                        e.Row.Cells[i].Text = bs.ToString();
                    }
                }
                if (indexID == 2)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                        {
                            decimal zl = 0;
                            zl = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                            e.Row.Cells[i].Text = zl.ToString("N0");
                        }
                    }
                }
                if (indexID == 3)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                        {
                            decimal dbzl = 0;
                            dbzl = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                            e.Row.Cells[i].Text = dbzl.ToString("F0");
                        }
                    }
                }
                if (indexID == 5 || indexID==6)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                        {
                            decimal wd = 0;
                            wd = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                            e.Row.Cells[i].Text = wd.ToString("F0");
                        }
                    }
                }
            }
        }
    }

    protected void gv_bymonth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void LinkBtnMonth_Click(object sender, EventArgs e)
    {
        DataTable dt = jl.Jinglian_TJ_Report(5, txt_year.Text, txt_bymonth.Text, ddl_banzhu.Text, "").Tables[0];
        gv1.DataSource = dt;
        gv1.DataBind();
        string currmonth = txt_bymonth.Text.Substring(txt_bymonth.Text.Length-2, 2);
        if (currmonth != txt_month.Text)
        {
           string mn = Convert.ToDateTime(txt_year.Text + '-' + currmonth + "-01").AddMonths(1).AddDays(-1).ToString("MM-dd");
          if (Convert.ToDateTime(txt_year.Text + '-' + txt_month.Text + "-01").AddMonths(1).AddDays(-1).ToShortDateString() == System.DateTime.Now.Month.ToString())
            {
                mn = DateTime.Now.ToString("MM-dd");
            }
            DataTable dt_hj = jl.Jinglian_TJ_Report(3, txt_year.Text, currmonth, ddl_banzhu.Text, mn).Tables[0];
            gv_hj.DataSource = dt_hj;
            gv_hj.DataBind();
            Label1.Text = mn;
        }
        else
        {
            DataTable dt_hj = jl.Jinglian_TJ_Report(3, txt_year.Text, currmonth, ddl_banzhu.Text, DateTime.Now.ToString("MM-dd")).Tables[0];
            gv_hj.DataSource = dt_hj;
            gv_hj.DataBind();
            Label1.Text = DateTime.Now.ToString("MM-dd");
        }
        bindChart_WD((DataTable)ViewState["tblWD"]);
        bindChart_MD((DataTable)ViewState["tblMD"]);
        bindChart_WD_Mnth(txt_bymonth.Text);
        bindChart_MD_Mnth(txt_bymonth.Text);
      
     
    }
    protected void txt_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_date.Text = Convert.ToDateTime(txt_year.Text + '-' + txt_month.Text + "-01").AddMonths(1).AddDays(-1).ToShortDateString();
        if (txt_month.Text == System.DateTime.Now.Month.ToString())
        {
            txt_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        LinkBtn.Style.Add("display", "none");
        LinkBtnMonth.Style.Add("display", "none");
        txt_bymonth.Style.Add("display", "none");
    }

    protected void gv_bymonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "描述";
            e.Row.Cells[0].Width = 150;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;//ctl00$MainContent$LinkBtnMonth
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnMonth','')");

                lbtn.Attributes.Add("name", "mnth");
                lbtn.Style.Add("text-align", "center");
                e.Row.Cells[i].Controls.Add(lbtn);

            }
        }
        else
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }

        }
    }
}