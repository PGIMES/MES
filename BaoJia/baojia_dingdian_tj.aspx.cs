using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;



public partial class baojia_dingdian_tj : System.Web.UI.Page
{
    decimal nyl = 0;
    decimal nxs = 0;
    decimal mjprice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
           
            string strsql = "select distinct year(dingdian_date)year from Baojia_agreement_flow where dingdian_date is not null  order by year(dingdian_date)  desc ";
            DataSet dsYear = DbHelperSQL.Query(strsql);
            fun.initDropDownList(dropYear, dsYear.Tables[0], "year", "year");
            DataRow myDr = dsYear.Tables[0].Rows[0];
            if (myDr[0].ToString() != DateTime.Now.Year.ToString())
            {
                dropYear.Items.Insert(0,new ListItem(DateTime.Now.Year.ToString(),DateTime.Now.Year.ToString()));
            }

            Query_mnth();
            Query_Sale();
            Query_Customer();       
           
            LinkBtn.Style.Add("display", "none");
            LinkSale.Style.Add("display", "none");
            LinkCustomer.Style.Add("display", "none");
          
            
            
           
        }

    }

    public void Query_mnth()
    {
        DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','0','','',''");
        DataTable dt = ds.Tables[1];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord")
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        gv.DataSource = dt;
        gv.DataBind();
        ViewState["tblMnth"] = dt;
        bindChartByMnth(ds.Tables[0]);
    }
    public void Query_Sale()
    {
        DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','1','','',''");
        DataTable dt = ds.Tables[1];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord")
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        gv_sale.DataSource =dt;
        gv_sale.DataBind();
        ViewState["tblSale"] = dt;
        bindChartBySale(ds.Tables[0]);
    }
    public void Query_Customer()
    {
        DataSet ds = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','2','','',''");
        DataTable dt = ds.Tables[1];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord")
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        gv_customer.DataSource = dt;
        gv_customer.DataBind();
        ViewState["tblCustomer"] = dt;
        bindChartByCustomer(ds.Tables[0]);
    }
    public void Getrowspan(string condition)
    {
        int[] cols = { 0, 1, 2, 3, 4,5,6,14,16,17,18,19,20,21,22,23};
        MergGridRow.MergeRow(gvdetail, cols);
        int visbleRow = 0;
        decimal zj = 0;
        for (int j = 0; j <= gvdetail.Rows.Count - 1; j++)
        {
            if (gvdetail.Rows[j].Cells[0].Visible == true)
            {
                 zj = 0;
                visbleRow = j;
                zj =Convert.ToDecimal( gvdetail.Rows[j].Cells[13].Text);
                gvdetail.Rows[visbleRow].Cells[14].Text = zj.ToString("N0");
            }
            else
            {
                gvdetail.Rows[visbleRow].Cells[20].Text = gvdetail.Rows[j].Cells[20].Text;
                zj +=Convert.ToDecimal( gvdetail.Rows[j].Cells[13].Text);
                gvdetail.Rows[visbleRow].Cells[14].Text = zj.ToString("N0");
            }


        }
        int rowIndex = 1;
        for (int j = 0; j <= gvdetail.Rows.Count - 1; j++)
        {
            if (gvdetail.Rows[j].Cells[0].Visible == true)
            {
                gvdetail.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Baojia.aspx?requestid=" + gvdetail.Rows[j].Cells[20].Text;
                link.Text = gvdetail.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                gvdetail.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;
            }
            //gvdetail.Rows[j].Cells[19].Attributes.Add("onclick", "OpenMsg(this.firstElementChild),'');");
            gvdetail.Rows[j].Cells[19].Attributes.Add("onclick", "OpenMsg(this.firstElementChild,'" + condition + "');");
        }
       // setGridLink();
       
       
    }
    public void bindChartByMnth(DataTable tbl)
    {
        ChartByMonth.DataSource = tbl;
        ChartByMonth.Series["年销售额"].XValueMember = tbl.Columns[0].ColumnName;
        ChartByMonth.Series["年销售额"].YValueMembers = tbl.Columns[2].ColumnName;
        ChartByMonth.Series["年销售额"].Name = "";
       // this.ChartByMonth.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
    }
    public void bindChartBySale(DataTable tbl)
    {
        Chart1.DataSource = tbl;
        Chart1.Series["年销售额"].XValueMember = tbl.Columns[0].ColumnName;
        Chart1.Series["年销售额"].YValueMembers = tbl.Columns[2].ColumnName;
        Chart1.Series["年销售额"].Name = "";
      //  this.Chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
    }
    public void bindChartByCustomer(DataTable tbl)
    {
        Chart2.DataSource = tbl;
        Chart2.Series["年销售额"].XValueMember = tbl.Columns[0].ColumnName;
        Chart2.Series["年销售额"].YValueMembers = tbl.Columns[2].ColumnName;
        Chart2.Series["年销售额"].Name = "";
        this.Chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
    }


    //public void setGridLink()
    //{
    //     GridViewRow dr= gv.HeaderRow;

    //     for (int i = 0; i < gv.Rows.Count; i++)
    //     {

    //         GridViewRow row = (GridViewRow)gv.Rows[i];
            
    //             LinkButton lbtn = new LinkButton();
    //             lbtn.ID = "btn";
    //             lbtn.Text = gv.Rows[i].Cells[3].Text;
    //             // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
    //             lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
    //             lbtn.Attributes.Add("name", "month");

    //             string strMonth = row.Cells[0].Text;
    //             lbtn.Attributes.Add("month", strMonth);

    //            gv.Rows[i].Cells[3].Controls.Add(lbtn);
    //            gv.Rows[i].Cells[3].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
             
    //     }
    //}


    protected void btnQuery_Click(object sender, EventArgs e)
    {

        Query_mnth();
        Query_Sale();
        Query_Customer();
        lblDays.Text = "定点项目";
        gvdetail.DataSource = null;
        gvdetail.DataBind();
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        string type = txtmonth.Text;
        QueryDetail(dropYear.SelectedValue, type,"M");
    }

    public void QueryDetail(string year, string type,string condition)
    {                                                               

        lblDays.Text = type + "--定点项目";
        int drop = 0;
        if (type == "合计")
        {
            drop = 3;
        }

        DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','" + drop + "','" + type.Replace("月", "") + "','0','0'");
        Getsum(dt_mx.Tables[0]);
        gvdetail.DataSource = dt_mx.Tables[0];
        gvdetail.DataBind();
        Getrowspan(condition);

    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 1; i <= e.Row.Cells.Count-1; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "month");
                lbtn.Style.Add("text-align", "center");

                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {
            e.Row.Cells[1].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }



        }
        
    }
    protected void gvdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise;
            e.Row.Cells[10].Text = "合计";
            e.Row.Cells[11].Text = this.nyl.ToString("N0");
            e.Row.Cells[14].Text = this.nxs.ToString("N0");
            e.Row.Cells[12].Text =(nxs==0)? "0": string.Format("{0:F2}", (Convert.ToDecimal(nxs) * 10000 / nyl));
            e.Row.Cells[15].Text = this.mjprice.ToString("N0");
           
        }

       

    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
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
            if (ldt.Rows[i]["mj_price"].ToString() != "")
            {
                this.mjprice += Convert.ToDecimal(ldt.Rows[i]["mj_price"].ToString());
            }


        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        gv.Style.Add("word-break", "keep-all");

        if (e.Row.RowType == DataControlRowType.DataRow )
        {
            e.Row.Cells[0].Wrap = false;
            DataRowView drv = (DataRowView)e.Row.DataItem; 
            for (int i = 1; i <e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() != "占比%")
                {
                    decimal nysl = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                    e.Row.Cells[i].Text = nysl.ToString("N0");
                }
                else if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() == "占比%")
                {
                    decimal zb = Convert.ToDecimal(e.Row.Cells[i].Text.ToString());
                    e.Row.Cells[i].Text = zb.ToString("N0")+'%';
                }
            }


            
        }
        

        
        
    }

   
    protected void gvdetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvdetail.PageIndex = e.NewPageIndex;
        
    }

    protected void LinkSale_Click(object sender, EventArgs e)
    {
        string type = txtsale.Text;
        lblDays.Text = type + "--定点项目";
        int drop = 1;
        if (type == "合计")
        {
            drop = 3;
        }
        DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','"+drop+"','0','" + type + "','0'");
        Getsum(dt_mx.Tables[0]);
        gvdetail.DataSource = dt_mx.Tables[0];
        gvdetail.DataBind();
        Getrowspan("S");
    }
    protected void LinkCustomer_Click(object sender, EventArgs e)
    {
        string type = txtCustomer.Text;
        lblDays.Text = type + "--定点项目";
        int drop = 2;
        if (type == "合计")
        {
            drop = 3;
        }
        DataSet dt_mx = DbHelperSQL.Query("exec BaoJia_Dingdian_Query_modify  '" + dropYear.SelectedValue + "','"+drop+"','0','0','" + type + "'");
        Getsum(dt_mx.Tables[0]);
        gvdetail.DataSource = dt_mx.Tables[0];
        gvdetail.DataBind();
        Getrowspan("C");
    }
    protected void gv_sale_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkSale','')");
                lbtn.Attributes.Add("name", "sale");
                lbtn.Style.Add("text-align", "center");

                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {
            e.Row.Cells[1].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
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
            for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkCustomer','')");
                lbtn.Attributes.Add("name", "Customer");
                lbtn.Style.Add("text-align", "center");

                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else
        {
            e.Row.Cells[1].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
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
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() != "占比%")
                {
                    decimal nysl = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                    e.Row.Cells[i].Text = nysl.ToString("N0");
                }
                else if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() == "占比%")
                {
                    decimal zb = Convert.ToDecimal(e.Row.Cells[i].Text.ToString());
                    e.Row.Cells[i].Text = zb.ToString("N0") + '%';
                }
            }



        }
    }
    protected void gv_customer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() != "占比%")
                {
                    decimal nysl = Math.Round(Convert.ToDecimal(e.Row.Cells[i].Text.ToString()), 0);
                    e.Row.Cells[i].Text = nysl.ToString("N0");
                }
                else if (e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[0].Text.ToString() == "占比%")
                {
                    decimal zb = Convert.ToDecimal(e.Row.Cells[i].Text.ToString());
                    e.Row.Cells[i].Text = zb.ToString("N0") + '%';
                }
            }



        }
    }
}