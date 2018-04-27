using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;


public partial class BaoJia_DingDian_Report : System.Web.UI.Page
{
    decimal nyl = 0;
    decimal nxs = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    

            //for (int i = 0; i < 5; i++)
            //{
            //    txt_year.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            //}
            string strsql = "select distinct year(dingdian_date)year from Baojia_agreement_flow order by year(dingdian_date)  desc ";
            DataSet dsYear = DbHelperSQL.Query(strsql);
            fun.initDropDownList(dropYear, dsYear.Tables[0], "year", "year");
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            string mnth=DateTime.Now.Month.ToString();
                
            DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','0','','',''");

            DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','0','0','',''");

            DataTable dt = ds.Tables[0];
            gv.DataSource = dt;
           
            gv.DataBind();
            Getsum(dt_mx.Tables[0]);
            gvdetail.DataSource = dt_mx.Tables[0];
            gvdetail.DataBind();
            setGridLink();
            bindChartYear(ds.Tables[1]);
            LinkBtn.Style.Add("display", "none");
        }

    }

    public void bindChartYear(DataTable tbl)
    {
      
        //时长
        ChartByMonth.DataSource = tbl;
        ChartByMonth.Series["年销售额"].XValueMember = tbl.Columns[0].ColumnName;
        ChartByMonth.Series["年销售额"].YValueMembers = tbl.Columns[3].ColumnName;
        if (dropMonth.SelectedValue == "0")
        {
            ChartByMonth.Series["年销售额"].LegendText = "月份";
        }
        else if (dropMonth.SelectedValue == "1")
        {
            ChartByMonth.Series["年销售额"].LegendText = "销售人员";
        }
        else if (dropMonth.SelectedValue == "2")
        {
            ChartByMonth.Series["年销售额"].LegendText = "客户";
        }
        
    }


    public void setGridLink()
    {
         GridViewRow dr= gv.HeaderRow;

         for (int i = 0; i < gv.Rows.Count-1; i++)
         {

             GridViewRow row = (GridViewRow)gv.Rows[i];
            
                 LinkButton lbtn = new LinkButton();
                 lbtn.ID = "btn";
                 lbtn.Text = gv.Rows[i].Cells[3].Text;
                 // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                 lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                 lbtn.Attributes.Add("name", "month");

                 string strMonth = row.Cells[0].Text;
                 lbtn.Attributes.Add("month", strMonth);

                gv.Rows[i].Cells[3].Controls.Add(lbtn);
                gv.Rows[i].Cells[3].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
             
         }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','" + dropMonth.SelectedValue+ "','','',''");
        DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','"+dropMonth.SelectedValue+"','0','0','0'");
      

        DataTable dt = ds.Tables[0];
        gv.DataSource = dt;

        gv.DataBind();
        Getsum(dt_mx.Tables[0]);
        lblDays.Text = "定点项目";
        gvdetail.DataSource = dt_mx.Tables[0];
        gvdetail.DataBind();
        setGridLink();
        bindChartYear(ds.Tables[1]);
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        string type = txtmonth.Text;
        QueryDetail(dropYear.SelectedValue, type);
    }

    public void QueryDetail(string year, string type)
    {                                                               
        string mnth = "0";
        string sale = "0";
        string customer = "0";
            
        if (dropMonth.SelectedValue == "0")
        {
            mnth = type.Replace("月", "");

        }
        else if (dropMonth.SelectedValue == "1")
        {
            sale = type;

        }
        else if (dropMonth.SelectedValue == "2")
        {
            customer = type;

        }
        lblDays.Text = type + "--定点项目";
        DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','','',''");
        DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query  '" + dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','"+mnth+"','"+sale+"','"+customer+"'");

        DataTable dt = ds.Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
        Getsum(dt_mx.Tables[0]);
        gvdetail.DataSource = dt_mx.Tables[0];
        gvdetail.DataBind();
        bindChartYear(ds.Tables[1]);
        //setGridLink();
      


    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "零件个数";
            e.Row.Cells[2].Text = "年用量";
            e.Row.Cells[3].Text = "年销售额";
            e.Row.Cells[4].Text = "占比%";
           
        }
        else
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

            }

        }
    }
    protected void gvdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gvdetail.PageIndex * this.gvdetail.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();

            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise;
        }
    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = "合计";
            e.Row.Cells[8].Text = this.nyl.ToString();
            e.Row.Cells[10].Text = this.nxs.ToString();
           




        }
    }

    private void Getsum(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["quantity_year"].ToString() != "")
            {
                this.nyl += Convert.ToDecimal(ldt.Rows[i]["quantity_year"].ToString());
            }

            if (ldt.Rows[i]["price_year"].ToString() != "")
            {
                this.nxs += Convert.ToDecimal(ldt.Rows[i]["price_year"].ToString());
            }
           

        }
    }
}