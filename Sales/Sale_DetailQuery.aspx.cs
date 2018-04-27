using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;

public partial class Sale_Sale_DetailQuery : System.Web.UI.Page
{
    control control = new control();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    
            
            //string strsql = "select distinct year(ih_eff_date)as year from [qad].dbo.qad_ih_hist order by year(ih_eff_date) DESC";
            //DataSet dsYear = DbHelperSQL.Query(strsql);
            //fun.initDropDownList(txt_year, dsYear.Tables[0], "year", "year");
            //txt_year.Items.Add(new ListItem { Value = "", Text = "ALL" });

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 2;
            int chaYear = intYear - 2016;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (2016 + i).ToString();
            }
            txt_year.DataSource = Yearlist;
            txt_year.DataBind();
            txt_year.SelectedValue = Year;
            txt_year.Items.Add(new ListItem { Value = "", Text = "ALL" });

            string sql = "select distinct substring(pl_desc,dbo.fn_find('-',pl_desc,'1')+1,1) as material,substring(pl_desc,dbo.fn_find('-',pl_desc,'1')+1,len(pl_desc)-dbo.fn_find('-',pl_desc,'1')+1) as material_id from [qad].dbo.qad_pl_mstr   where (pl_prod_line like '3%' or pl_prod_line='1090')";
            DataSet product = DbHelperSQL.Query(sql);
            //control.Dropdownlist_Bind(ddl_productid, product.Tables[0], "material_id", "material", 1);
            fun.initDropDownList(ddl_productid, product.Tables[0], "material_id", "material_id");
            this.ddl_productid.Items.Insert(0, new ListItem("", ""));
            DataSet ds = DbHelperSQL.Query("exec MES_GetSale_Detail '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_ljh.Text + "','" + ddl_kh.SelectedValue + "','" + ddl_productid.SelectedValue + "'");
           // DataTable dt = ds.Tables[0];

         
            string khsql = " select * from Sale_CustID";
            DataSet kh = DbHelperSQL.Query(khsql);
            control.Dropdownlist_Bind(ddl_kh, kh.Tables[0], "Customer_DM", "Customer_MC", 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    DataColumn col = ds.Tables[0].Columns[i];
                    if (col.ColumnName == "ord")
                    {
                        ds.Tables[0].Columns.Remove(col);
                        i = 0;
                    }
                }
                ViewState["tbxj"] = ds.Tables[1];

                // GridView1.PageSize = 500;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GroupCol(GridView1, 1);
                GroupCol(GridView1, 2);
            }
            else
            {
                DataTable dt = new DataTable();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            
           


        }
    }
    private void GetGrid()
    {
        DataSet ds;
        if (txt_year.SelectedValue == "")
        {
            ds = DbHelperSQL.Query("exec MES_GetSale_Detail_ByYear '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_ljh.Text + "','" + ddl_kh.SelectedValue + "','" + ddl_productid.SelectedValue + "'");

        }
        else
        {
             ds = DbHelperSQL.Query("exec MES_GetSale_Detail '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_ljh.Text + "','" + ddl_kh.SelectedValue + "','" + ddl_productid.SelectedValue + "'");

        }


        if (ds != null && ds.Tables.Count > 0)
        {
            if (ddl_item.SelectedValue == "0" || ddl_item.SelectedValue == "1")
            {
                DataTable dt = ds.Tables[0];
                ViewState["tbxj"] = ds.Tables[1];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn col = dt.Columns[i];
                    if (col.ColumnName == "ord")
                    {
                        dt.Columns.Remove(col);

                    }
                }


                //GetSum((DataTable)ViewState["tbxj"]);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GroupCol(GridView1, 1);
                GroupCol(GridView1, 2);
            }
            else
            {
                DataTable dt = ds.Tables[0];
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GroupCol_dj(GridView1, 1);
            }
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('查无数据，请重新选择查询条件！')", true);
            //return;
            DataTable dt = new DataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetGrid();
    }
  
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "序号";
            e.Row.Cells[1].Text = "序号";
            e.Row.Cells[2].Text = "PGI零件号";
            e.Row.Cells[3].Text = "零件号";
            e.Row.Cells[4].Text = "零件名称";
            e.Row.Cells[5].Text = "客户名称";
            e.Row.Cells[6].Text = "材料";
            e.Row.Cells[7].Text = "产品类别";
            e.Row.Cells[8].Text = "客户代码";
        }
        else
        {
            for (int i = 9; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

            }

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int count = e.Row.Cells.Count;
        
        if (ddl_item.SelectedValue == "0" || ddl_item.SelectedValue == "1")
        {
            GridView1.ShowFooter = true;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[count - 1].Visible = false;
                int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[1].Text = e.Row.Cells[count - 1].Text;


                if (e.Row.Cells[3].Text == "小计")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;

                }
                for (int i = 1; i < e.Row.Cells.Count - 10; i++)
                {
                    if (e.Row.Cells[8 + i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        decimal sl = Math.Round(Convert.ToDecimal(e.Row.Cells[8 + i].Text.ToString()), 2);
                        
                        e.Row.Cells[8 + i].Text = sl.ToString("N0");
                        if (e.Row.Cells[count - 2].Text.Replace("&nbsp;", "") != "")
                        {
                            decimal zb = Math.Round(Convert.ToDecimal(e.Row.Cells[count - 2].Text.Replace("&nbsp;", "").ToString()), 2);
                            e.Row.Cells[count - 2].Text = zb.ToString("N2");
                        }
                       

                    }


                }

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[count - 1].Visible = false;
                // e.Row.Cells[0].Visible = false;
                e.Row.BackColor = System.Drawing.Color.BlanchedAlmond;
                DataTable dt = (DataTable)ViewState["tbxj"];
                List<string> list = new List<string>();
                e.Row.Cells[2].Text = "合计";

                for (int i = 0; i < dt.Columns.Count - 7; i++)
                {

                    list.Add(dt.Rows[0][7 + i].ToString());

                }
                for (int i = 0; i < list.Count; i++)
                {
                    decimal sl = 0;
                    if (list[i].ToString() != "")
                    {
                        sl = Math.Round(Convert.ToDecimal(list[i].ToString()), 2);
                        e.Row.Cells[8 + i].Text = sl.ToString("N0");
                    }

                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // CreatedHeader(e);
                e.Row.Cells[count - 1].Visible = false;
               e.Row.Cells[1].Visible = false;
                //e.Row.Cells[0].Visible = false;

            }
        }
        else
        {
            GridView1.ShowFooter = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Width = 130;
                e.Row.Cells[1].Visible = false;
                //if (e.Row.RowIndex != -1)
                //{
                int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();

                //}
                for (int i = 1; i < e.Row.Cells.Count - 8; i++)
                {
                    if (e.Row.Cells[8 + i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        decimal sl = Math.Round(Convert.ToDecimal(e.Row.Cells[8 + i].Text.ToString()), 2);
                        //decimal zb = Math.Round(Convert.ToDecimal(e.Row.Cells[count - 2].Text.Replace("&nbsp;", "").ToString()), 2);
                        e.Row.Cells[8 + i].Text = sl.ToString("N2");
                        e.Row.Cells[8 + i].Width = 65;

                    }
                    else
                    {
                        e.Row.Cells[8 + i].Width = 50;
                    }
                }
                for (int i = 1; i <= 9; i++)
                {
                    int col = e.Row.Cells.Count;

                    if ((e.Row.Cells[col - 2 - i].Text.Replace("&nbsp;", "")) != "" && (e.Row.Cells[col - 3 - i].Text.Replace("&nbsp;", "")) != "")
                    {
                        //if (Convert.ToDecimal(e.Row.Cells[col - 2 - i].Text) > Convert.ToDecimal(e.Row.Cells[col - 3 - i].Text))
                        //{
                        //    e.Row.Cells[col - 2 - i].BackColor = System.Drawing.Color.Green;
                        //}
                        //else 
                        if (Convert.ToDecimal(e.Row.Cells[col - 2 - i].Text) < Convert.ToDecimal(e.Row.Cells[col - 3 - i].Text))
                        {
                            e.Row.Cells[col - 2 - i].BackColor = System.Drawing.Color.Red;
                        }
                    }


                }

            }
            else if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
          
        }
        
    }


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void ddl_item_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_item.SelectedValue != "0")
        {
            ddl_type.CssClass = "form-control input-s-sm ";
            ddl_type.Enabled = false;  
        }
        else
        {
            ddl_type.Enabled = true; 
        }
       
    }

    public static void GroupCol(GridView gridView, int cols)
    {
        int index = 0;
        if (gridView.Rows.Count < 1 || cols > gridView.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc = new TableCell(); //gridView.Rows[0].Cells[cols];
        TableCell oldTc0 = new TableCell();// gridView.Rows[0].Cells[0];
        for (int i = 0; i < gridView.Rows.Count; i++)
        {
            TableCell tc = gridView.Rows[i].Cells[cols];
            TableCell tc0 = gridView.Rows[i].Cells[0];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                tc0.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                    oldTc0.RowSpan = 1;
                }
                oldTc.RowSpan++;
                oldTc0.RowSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
                oldTc0.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                index = index + 1;
                oldTc = tc;
                oldTc0 = tc0;

            }
            oldTc0.Text = index.ToString();
        }

    }

   

    public static void GroupCol_dj(GridView gridView, int cols)
    {
        int index = 0;
        if (gridView.Rows.Count < 1 || cols > gridView.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc =new TableCell(); //gridView.Rows[0].Cells[cols];
        TableCell oldxmh =new TableCell(); //gridView.Rows[0].Cells[1];
        TableCell oldTc0 = new TableCell(); //gridView.Rows[0].Cells[0];
        
        for (int i = 0; i < gridView.Rows.Count; i++)
        {
            TableCell tc = gridView.Rows[i].Cells[cols];
            TableCell xmh = gridView.Rows[i].Cells[2];
            TableCell tc0 = gridView.Rows[i].Cells[0];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                xmh.Visible = false;
                tc0.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                    oldxmh.RowSpan = 1;
                    oldTc0.RowSpan = 1;
                }

                oldTc.RowSpan++;
                oldxmh.RowSpan++;
                oldTc0.RowSpan++;

                oldTc.VerticalAlign = VerticalAlign.Middle;
                oldxmh.VerticalAlign = VerticalAlign.Middle;
                oldTc0.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                index = index + 1;
                oldTc = tc;
                oldxmh = xmh;
                oldTc0 = tc0;
            }
            oldTc0.Text = index.ToString();
        }
        //TableCell oldTc = gridView.Rows[0].Cells[cols];
        //TableCell oldxmh= gridView.Rows[0].Cells[1];
        //TableCell oldTc0 = gridView.Rows[0].Cells[0];
        //for (int i = 1; i < gridView.Rows.Count; i++)
        //{
        //    TableCell tc = gridView.Rows[i].Cells[cols];
        //    TableCell xmh = gridView.Rows[i].Cells[2];
        //    TableCell tc0 = gridView.Rows[i].Cells[0];
        //    if (oldTc.Text == tc.Text)
        //    {
        //        tc.Visible = false;
        //        xmh.Visible = false;
        //        tc0.Visible = false;
        //        if (oldTc.RowSpan == 0)
        //        {
        //            oldTc.RowSpan = 1;
        //            oldxmh.RowSpan = 1;
        //            oldTc0.RowSpan = 1;
        //        }
              
        //            oldTc.RowSpan++;
        //            oldxmh.RowSpan++;
        //            oldTc0.RowSpan++;
                
        //        oldTc.VerticalAlign = VerticalAlign.Middle;
        //        oldxmh.VerticalAlign = VerticalAlign.Middle;
        //        oldTc0.VerticalAlign = VerticalAlign.Middle;
        //    }
        //    else
        //    {
        //        index = index + 1;
        //        oldTc = tc;
        //        oldxmh = xmh;
        //        oldTc0 = tc0;
        //    }
        //    oldTc0.Text = index.ToString();
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetGrid();
    }
   
}