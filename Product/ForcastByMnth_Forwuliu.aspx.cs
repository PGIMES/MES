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

public partial class Product_ForcastByMnth_Forwuliu : System.Web.UI.Page
{
    int sj1 = 0;
    int sj2 = 0;
    int sj3 = 0;
    int yc1 = 0;
    int yc2 = 0;
    int yc3 = 0;
    int rc1 = 0;
    int rc2 = 0;
    int rc3 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 2;
            int chaYear = intYear - 2016;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (2016 + i).ToString();
            }
            dropYear.DataSource = Yearlist;
            dropYear.DataBind();
            dropYear.SelectedValue = Year;


            string strSQL = @"select distinct ship_from   from form3_Sale_ProductQuantity_DetailTable  ";
            DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];

            if (ship_from.Rows.Count > 0)
            {
                fun.initDropDownList(this.dropfrom, ship_from, "ship_from", "ship_from");
            }
            dropfrom.Items.Insert(0, new ListItem("全部", ""));


            DataTable dt = GetDataTable();
            Getsum(dt);
            this.gv_month.DataSource = dt;
            this.gv_month.DataBind();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataTable();
        Getsum(dt);
        this.gv_month.DataSource = dt;
        this.gv_month.DataBind();
       
    }

    public DataTable GetDataTable()
    {

        DataSet ds;
        ds = DbHelperSQL.Query("exec rpt_Form3_Sale_ForcastDetail_Forwuliu_xg  '" + dropYear.SelectedValue + "','" + dropsite.SelectedValue + "','" + droptype.SelectedValue + "','" + dropfrom.SelectedValue + "'");
        DataTable dt = ds.Tables[0];
        ViewState["tbhj"] = dt;
        for (int i = 12; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            string name = col.ColumnName.Substring(0, 2);
            if (name != "实际" && name != "滚动" && name != "销售")
            {
                dt.Columns.Remove(col);
                i = 12;
            }
        }
        return dt;
    }
    protected void gv_month_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_month_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[5].Style.Add("width", "160px");
            e.Row.Cells[4].Style.Add("word-break", "break-all");//ship_to
            e.Row.Cells[4].Style.Add("width", "180px");
          
                for (int i = 13; i < e.Row.Cells.Count; i++)
                {
                    
                    if (e.Row.Cells[i].Text.Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            
            e.Row.Cells[1].Text = "合计";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Style.Add("text-align", "right");
            e.Row.Cells[13].Text = this.sj1.ToString("N0");
            e.Row.Cells[14].Text = this.sj2.ToString("N0");
            e.Row.Cells[15].Text = this.sj3.ToString("N0");
            e.Row.Cells[16].Text = this.rc1.ToString("N0");
            e.Row.Cells[17].Text = this.rc2.ToString("N0");
            e.Row.Cells[18].Text = this.rc3.ToString("N0");
            e.Row.Cells[19].Text = this.yc1.ToString("N0");
            e.Row.Cells[20].Text = this.yc2.ToString("N0");
            e.Row.Cells[21].Text = this.yc3.ToString("N0");

        }
        
    }
    protected void btnexport_Click(object sender, EventArgs e)
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        string lsname = "三个月滚动预测";
         DataTable dt = GetDataTable();;
        YangjianSQLHelp.DataTableToExcel(dt, "xls", lsname, "1");
    }

    private void Getsum(DataTable ldt)
    {
        this.sj1 = 0;
        this.sj2 = 0;
        this.sj3 = 0;
        this.yc1 = 0;
        this.yc2 = 0;
        this.yc3 = 0;
        this.rc1 = 0;
        this.rc2 = 0;
        this.rc3 = 0;
        DataTable dt = (DataTable)ViewState["tbhj"];
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            string x = ldt.Rows[i][13].ToString();
                if (ldt.Rows[i][13].ToString() != "")
                {
                    this.sj1 = this.sj1 + Convert.ToInt32(ldt.Rows[i][13].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][14].ToString() != "")
                {
                    this.sj2 = this.sj2 + Convert.ToInt32(ldt.Rows[i][14].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][15].ToString() != "")
                {
                    this.sj3 = this.sj3 + Convert.ToInt32(ldt.Rows[i][15].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][16].ToString() != "")
                {
                    this.rc1 = this.rc1 + Convert.ToInt32(ldt.Rows[i][16].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][17].ToString() != "")
                {
                    this.rc2 = this.rc2 + Convert.ToInt32(ldt.Rows[i][17].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][18].ToString() != "")
                {
                    this.rc3 = this.rc3 + Convert.ToInt32(ldt.Rows[i][18].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][19].ToString() != "")
                {
                    this.yc1 = this.yc1 + Convert.ToInt32(ldt.Rows[i][19].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][20].ToString() != "")
                {
                    this.yc2 = this.yc2 + Convert.ToInt32(ldt.Rows[i][20].ToString().Replace(",", ""));
                }
                if (ldt.Rows[i][21].ToString() != "")
                {
                    this.yc3 = this.yc3 + Convert.ToInt32(ldt.Rows[i][21].ToString().Replace(",", ""));
                }
            
        }
    }
}