using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using Maticsoft.DBUtility;

public partial class Baojia_ZhongdianZhengqu_TJ : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 200;
        if (!IsPostBack)
        {
            Query_Sale();
            Query_Customer();
            lbldetail.Text = "明细";
            GridView1.DataSource = null;
            GridView1.DataBind();
            LinkSale.Style.Add("display", "none");
            LinkCustomer.Style.Add("display", "none");
        }

    }
    /// <summary>
    /// 确认按钮 触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    { 
        Query_Sale();
        Query_Customer();
        lbldetail.Text = "明细";
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    public void Query_Sale()
    {
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_ZD_Project  'S','" + this.ddl_project_level.SelectedValue.ToString() + "'");
        gv_sale.DataSource = ds.Tables[0];
        gv_sale.DataBind();
        lblMstS.Text = "按【销售人员】统计；争取级别：" + this.ddl_project_level.SelectedItem.ToString();
        ViewState["tblSale"] = ds.Tables[0];
        bindChartBySale(ds.Tables[1]);
    }
    public void Query_Customer()
    {
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_ZD_Project  'C','" + this.ddl_project_level.SelectedValue.ToString() + "'");
        gv_customer.DataSource = ds.Tables[0];
        gv_customer.DataBind();
        lblMstC.Text = "按【直接顾客】统计；争取级别：" + this.ddl_project_level.SelectedItem.ToString();
        ViewState["tblCustomer"] = ds.Tables[0];
        bindChartByCustomer(ds.Tables[1]);
    }
    public void bindChartBySale(DataTable tbl)
    {
        ChartA.DataSource = tbl;
        ChartA.Series["A1"].XValueMember = tbl.Columns[1].ColumnName;
        ChartA.Series["A1"].YValueMembers = tbl.Columns[2].ColumnName;
    }
    public void bindChartByCustomer(DataTable tbl)
    {
        ChartB.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        ChartB.DataSource = tbl;
        ChartB.Series["B1"].XValueMember = tbl.Columns[1].ColumnName;
        ChartB.Series["B1"].YValueMembers = tbl.Columns[2].ColumnName;
    }
    protected void LinkSale_Click(object sender, EventArgs e)
    {
        string type = txtsale.Text;
        this.lbldetail.Text= "【"+type + "】--明细";
        BindGridviewDTL("S", this.ddl_project_level.SelectedValue.ToString(),type);
    }
    protected void LinkCustomer_Click(object sender, EventArgs e)
    {
        string type = txtCustomer.Text;
        this.lbldetail.Text = "【" + type + "】--明细";
        BindGridviewDTL("C", this.ddl_project_level.SelectedValue.ToString(), type);
    }
    protected void gv_sale_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkSale','')");
                lbtn.Attributes.Add("name", "sale");
                lbtn.Style.Add("text-align", "center");

                e.Row.Cells[i].Controls.Add(lbtn);
                e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
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
    protected void gv_customer_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkCustomer','')");
                lbtn.Attributes.Add("name", "Customer");
                lbtn.Style.Add("text-align", "center");

                e.Row.Cells[i].Controls.Add(lbtn);
                e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
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
    protected void gv_sale_RowDataBound(object sender, GridViewRowEventArgs e)
    {   

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[0].Text == "3"&& e.Row.Cells[i].Text!="&nbsp;")
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else if (e.Row.Cells[0].Text == "4" & e.Row.Cells[i].Text != "&nbsp;")
                {
                    e.Row.Cells[i].Text = (string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                
            }

        }
    }
    protected void gv_customer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            for (int i = 2; i < e.Row.Cells.Count ; i++)
            {
                if (e.Row.Cells[0].Text == "3" && e.Row.Cells[i].Text != "&nbsp;")
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else if (e.Row.Cells[0].Text == "4" && e.Row.Cells[i].Text != "&nbsp;")
                {
                    e.Row.Cells[i].Text = (string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }

            }

        }
    }

    /// <summary>
    /// 明细表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[20].Style.Add("display", "none");

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[20].Style.Add("display", "none");
            if(e.Row.Cells[18].Text.ToString()!="")
            {
                if (e.Row.Cells[18].Text.Replace("&nbsp;", "") != "")
                {
                    DateTime updatetime = Convert.ToDateTime(e.Row.Cells[18].Text.ToString());
                    DateTime nowtime = System.DateTime.Now;

                    TimeSpan ts = nowtime - updatetime;
                    if (ts.TotalDays < 7)
                    {
                        e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[19].BackColor = System.Drawing.Color.Yellow;
                    }
                }

            }
        
        }

     
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
 
    public void BindGridviewDTL(string condition, string project_level, string value)
    {
        Baojia_Report_sql BaojiaSQLHelp = new Baojia_Report_sql();
        DataTable ds = BaojiaSQLHelp.Get_Baojia_FenXi_ZD_Project_dtl(condition, project_level, value);
   
        this.GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView1.Rows[GridView1.Rows.Count - 1].Cells[17].Text = "";
        GridView1.Rows[GridView1.Rows.Count - 1].Cells[2].Text = "";
        GridView1.Rows[GridView1.Rows.Count - 1].Cells[3].Text = "";

        GridView1.Rows[GridView1.Rows.Count - 1].BackColor = System.Drawing.Color.LightYellow;
     
        int[] cols = { 0, 1, 2, 3, 4, 5,6, 15, 17, 18,19 };

        MergGridRow.MergeRow(GridView1, cols);
        int visbleRow = 0;
        for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                visbleRow = j;
            }
            else
            {
                GridView1.Rows[visbleRow].Cells[19].Text = GridView1.Rows[j].Cells[19].Text;
            }


        }

        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count-1; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Baojia.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[20].Text).Trim();
                link.Text = GridView1.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;

            }
            GridView1.Rows[j].Cells[17].Attributes.Add("onclick", "OpenMsg(this.firstElementChild,'" + condition + "');");
        }

    }

}