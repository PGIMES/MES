using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.Charts;
using System.Threading;
public partial class Sale_Customer_Query : System.Web.UI.Page
{
    
  
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
            //string strsql = "select distinct year(ih_eff_date)as year from [qad].dbo.qad_ih_hist order by year(ih_eff_date) DESC";
            //DataSet dsYear = DbHelperSQL.Query(strsql);
            //fun.initDropDownList(txt_year, dsYear.Tables[0], "year", "year");
            //int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
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



            for (int i = 1; i < 13; i++)
            {
                txt_mnth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            txt_mnth.SelectedValue = DateTime.Now.Month.ToString();
            

            //ddl_type.Visible = false;
            //lb_type.Visible = false;

            //add by lilian  绑定图表和数据源
            Query();
        }
    }
    public void ShowGridView1()
    {
        //DataSet ds = DbHelperSQL.Query("exec MES_GetSale '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_wd.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_mnth.SelectedValue + "'");
        //DataTable dt = ds.Tables[0];

        //for (int i = 0; i < dt.Columns.Count; i++)
        //{
        //    DataColumn col = dt.Columns[i];
        //    if (col.ColumnName == "ord" )
        //    {
        //        dt.Columns.Remove(col);
        //        i = 0;
        //    }
        //}
        //ViewState["tbxj"] = ds.Tables[1];

        //GridView1.DataSource = dt;
        //GridView1.DataBind();
        DataSet ds;
        if (txt_year.SelectedValue == "")
        {
            ds = DbHelperSQL.Query("exec MES_GetSale_ByYear '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_wd.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_mnth.SelectedValue + "'");
        }
        else
        {
            ds = DbHelperSQL.Query("exec MES_GetSale '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_wd.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + ddl_type.SelectedValue + "','" + txt_mnth.SelectedValue + "'");
        }
        if (ds != null && ds.Tables.Count > 0)
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
        }
        else
        {
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('查无数据，请重新选择查询条件！')", true);
        //    return;
           DataTable dt = new DataTable();
           GridView1.DataSource = dt;
           GridView1.DataBind();

        }

    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;
        ViewState["id"] = "id";
       
        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        DataSet ds = DbHelperSQL.Query("exec MES_GetSale '" + txt_year.SelectedValue + "','" + ddl_item.SelectedValue + "','" + ddl_wd.SelectedValue + "','" + ddl_comp.SelectedValue + "', '"+ddl_type.SelectedValue+"'");
        DataTable dt = ds.Tables[0];

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord" )
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null )
        {
            dv.Sort =  ViewState["id"].ToString() + " " + ViewState["sortdirection"].ToString();

        }
        
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }

    protected void CreatedHeader(GridViewRowEventArgs e)
    {
        if (ddl_wd.SelectedValue == "0")
        {
            //e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "客户大类";
                e.Row.Cells[1].Text = "客户代码";
                e.Row.Cells[2].Text = "客户名称";
            }
            else
            {
                for (int i = 3; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }
        else if (ddl_wd.SelectedValue == "1")
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "地区";
                e.Row.Cells[1].Text = "国家";

            }
            else
            {

                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }
        else if (ddl_wd.SelectedValue == "2")
        {
            //e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "材料";
                e.Row.Cells[1].Text = "材料类别";
            }
            else
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (ddl_wd.SelectedValue == "0")
        {
            //e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "客户大类";
                e.Row.Cells[1].Text = "客户代码";
                e.Row.Cells[2].Text = "客户名称";
            }
            else
            {
                for (int i = 3; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }
        else if (ddl_wd.SelectedValue == "1")
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "地区";
                e.Row.Cells[1].Text = "国家";

            }
            else
            {

                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }
        else if (ddl_wd.SelectedValue == "2")
        {
            //e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "材料";
                e.Row.Cells[1].Text = "材料类别";
            }
            else
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

                }

            }
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int count = e.Row.Cells.Count;
      

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[count - 1].Visible = false;
            e.Row.Cells[count - 2].Visible = false;
           
            if (ddl_wd.SelectedValue == "0")
            {
                if (e.Row.Cells[1].Text == "小计")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    
                }
                for (int i = 0; i < e.Row.Cells.Count - 6; i++)
                {
                    if (e.Row.Cells[3 + i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        decimal sl = Math.Round(Convert.ToDecimal(e.Row.Cells[3 + i].Text.ToString()), 2);
                        if (e.Row.Cells[count - 3].Text.ToString().Replace("&nbsp;", "") != "")
                        {
                            decimal zb = Math.Round(Convert.ToDecimal(e.Row.Cells[count - 3].Text.ToString()), 2);
                            e.Row.Cells[count - 3].Text = zb.ToString("N2");
                        }
                        e.Row.Cells[3 + i].Text = sl.ToString("N0");
                       
                       
                    }


                }
               
            }
           
            else
            {
                if (e.Row.Cells[1].Text == "小计")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                for (int i = 0; i < e.Row.Cells.Count - 5; i++)
                {
                    if (e.Row.Cells[2 + i].Text.ToString().Replace("&nbsp;", "") != "")
                    {

                        decimal sl = Math.Round(Convert.ToDecimal(e.Row.Cells[2 + i].Text.ToString()), 2);
                        if (e.Row.Cells[count - 3].Text.ToString().Replace("&nbsp;", "") != "")
                        {
                            decimal zb = Math.Round(Convert.ToDecimal(e.Row.Cells[count - 3].Text.ToString()), 2);
                            e.Row.Cells[count - 3].Text = zb.ToString("N2");
                        }
                        e.Row.Cells[2 + i].Text = sl.ToString("N0");


                    }


                }
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[count - 1].Visible = false;
            e.Row.Cells[count - 2].Visible = false;
            e.Row.BackColor = System.Drawing.Color.BlanchedAlmond;
            DataTable dt = (DataTable)ViewState["tbxj"];
            List<string> list = new List<string>();
            e.Row.Cells[1].Text = "合计";
            if (ddl_wd.SelectedValue == "0")
            {
                for (int i = 0; i < dt.Columns.Count - 3; i++)
                {

                    list.Add(dt.Rows[0][3 + i].ToString());

                }
                for (int i = 0; i < list.Count; i++)
                {
                    decimal sl = 0;
                    sl = Math.Round(Convert.ToDecimal(list[i].ToString()), 2);
                    e.Row.Cells[3 + i].Text = sl.ToString("N0");

                }
            }
            else
            {
                for (int i = 0; i < dt.Columns.Count - 2; i++)
                {

                    list.Add(dt.Rows[0][2 + i].ToString());

                }
                for (int i = 0; i < list.Count; i++)
                {
                    // e.Row.Cells[2 + i].Text = list[i].ToString();
                    decimal sl = 0;
                    sl = Math.Round(Convert.ToDecimal(list[i].ToString()), 2);
                    e.Row.Cells[2 + i].Text = sl.ToString("N0");
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
           // CreatedHeader(e);
            e.Row.Cells[count - 1].Visible = false;
            e.Row.Cells[count - 2].Visible = false;
           
        }
    }
    //protected void ddl_item_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_item.SelectedValue == "0")
    //    {
    //        ddl_type.Visible = false;
    //        lb_type.Visible = false;
    //    }
    //    else
    //    {
    //        ddl_type.Visible = true;
    //        lb_type.Visible = true;
    //        ddl_type.Items.Clear();
    //        string[] type = { "产品收入", "模具收入", "发票调整收入" };
    //        for (int i = 0; i < type.Length; i++)
    //        {
    //            ddl_type.Items.Insert(i, type[i]);
    //            ddl_type.Items[i].Value = i.ToString();
    //        }
    //    }
        

           
    //}
    //protected void ddl_wd_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_wd.SelectedValue == "0")
    //    {
    //        ddl_type.Items.Clear();
    //        string[] type = { "产品收入", "模具收入", "发票调整收入" };
    //        for (int i = 0; i < type.Length; i++)
    //        {
    //            ddl_type.Items.Insert(i, type[i]);
    //            ddl_type.Items[i].Value = i.ToString();
    //        }
    //    }
    //    else if (ddl_wd.SelectedValue == "1")
    //    {
    //        ddl_type.Items.Clear();
    //        string[] type = {  "产品收入", "模具收入" };
    //        for (int i = 0; i < type.Length; i++)
    //        {
    //            ddl_type.Items.Insert(i, type[i]);
    //            ddl_type.Items[i].Value = i.ToString();
    //        }

    //    }
    //    else
    //    {
    //       ddl_type.Items.Insert(0, "产品收入");
          
    //    }
    //}
    protected void ddl_item_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_item.SelectedValue == "0")
        {
            ddl_type.Visible = false;
            lb_type.Visible = false;
        }
        else
        {
            ddl_type.Visible = true;
            lb_type.Visible = true;
            //ddl_type.Items.Clear();
            //string[] type = { "产品收入", "模具收入", "发票调整收入" };
            //for (int i = 0; i < type.Length; i++)
            //{
            //    ddl_type.Items.Insert(i, type[i]);
            //    ddl_type.Items[i].Value = i.ToString();
            //}
        }
    }
    protected void ddl_wd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_wd.SelectedValue == "0" || ddl_wd.SelectedValue == "1")
        {
            ddl_type.Items.Clear();
            ddl_type.Items.Add(new ListItem("ALL", "3"));
            ddl_type.Items.Add(new ListItem("产品收入", "0"));
            ddl_type.Items.Add(new ListItem("模具收入", "1"));
            ddl_type.Items.Add(new ListItem("发票调整", "2"));
            ddl_type.Items.Add(new ListItem("其他收入", "4")); 
           //ddl_type.Items.Insert(0, "ALL");
           
            
        }
        else if (ddl_wd.SelectedValue == "2")
        {
            ddl_type.Items.Clear();
            ddl_type.Items.Add(new ListItem("ALL", "3"));
            ddl_type.Items.Add(new ListItem("产品收入", "0"));
            ddl_type.Items.Add(new ListItem("模具收入", "1"));
        }
        
    }
    /*Add by lilian 20170825 添加分析图表 */
    public void Query()
    {   //清空图表的数据源                                                     
        Thread th1;
        Thread th2;
        Thread th3;
        Thread th4;
        Thread th5;
        Thread th6;
        th1 = new Thread(new ThreadStart(ShowMonth));  //固定写法
        th2 = new Thread(new ThreadStart(ShowYear));
        th3 = new Thread(new ThreadStart(ShowCustomer));
        th4 = new Thread(new ThreadStart(ShowArea));  //固定写法
        th5 = new Thread(new ThreadStart(ShowMaterial));
        th6 = new Thread(new ThreadStart(ShowGridView1));

        //th1.Priority = ThreadPriority.Highest;  //设置优先级
        //th2.Priority = ThreadPriority.AboveNormal;
        //th3.Priority = ThreadPriority.Normal;
        th1.Name = "Ta";  //设置名字
        th2.Name = "Tb";
        th3.Name = "Tc";
        th4.Name = "Td";
        th5.Name = "Te";
        th6.Name = "Tg";

        th1.Start();  //启动线程
        th2.Start();
        th3.Start();
        th4.Start();
        th5.Start();
        th6.Start();

        th1.Join();
        th2.Join();
        th3.Join();
        th4.Join();
        th5.Join();
        th6.Join();

        th1.Abort();
        th2.Abort();
        th3.Abort();
        th4.Abort();
        th5.Abort();
        th6.Abort();
       
    }
  
    public void ShowMonth()
    {
        //加载 月
        ChartA.Series.Clear();
        DataSet ds_month;
        if (ddl_item.SelectedValue == "1")//金额
        {
            ds_month = DbHelperSQL.Query("exec [dbo].[MES_Sales_Fenxi_ByMonth_AddYear_XG] '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue + "'");
        }
        else//按数量
        {
            ds_month = DbHelperSQL.Query("exec [dbo].[MES_Sales_Fenxi_ByMonth_num_AddYear_XG] '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue + "'");
        }
        gv_month.DataSource = ds_month.Tables[0];
        gv_month.DataBind();
        CreateChart_month(ds_month.Tables[0]);
        string company_ddl = "ALL";
        string year_ddl = txt_year.SelectedValue;
        string month_ddl = txt_mnth.SelectedValue;
        if (ddl_comp.SelectedValue !="")
        {
            company_ddl = ddl_comp.SelectedValue;
        }
        if (year_ddl=="")
        {
            year_ddl = DateTime.Now.Year.ToString();
            month_ddl = DateTime.Now.Month.ToString();
        }
        this.lblMstMonth.Text = "按【月】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
    }

    public void ShowYear()
    {
        //年
        ChartB.Series.Clear();
        DataSet ds_year;
        if (ddl_item.SelectedValue == "1")//金额
        {
            ds_year = DbHelperSQL.Query("exec [dbo].[MES_Sales_Fenxi_ByYear_AddYear] '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue + "'");
        }
        else//按数量
        {
            ds_year = DbHelperSQL.Query("exec [dbo].[MES_Sales_Fenxi_ByYear_num_AddYear] '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue + "'");
        }
        gv_year.DataSource = ds_year.Tables[0];
        gv_year.DataBind();
        CreateChart_year(ds_year.Tables[0]);
        string company_ddl = "ALL";
        string year_ddl = txt_year.SelectedValue;
        string month_ddl = txt_mnth.SelectedValue;
        if (ddl_comp.SelectedValue != "")
        {
            company_ddl = ddl_comp.SelectedValue;
        }
        if (year_ddl == "")
        {
            year_ddl = DateTime.Now.Year.ToString();
            month_ddl = DateTime.Now.Month.ToString();
        }
        this.lblMstYear.Text = "按【年】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
    }
    public void ShowCustomer()
    {
        //客户
        ChartC.Series.Clear();
        DataSet ds_customer;
        if (ddl_item.SelectedValue == "1")//金额
        {
            ds_customer = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByCustomer] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
        else//按数量
        {
            ds_customer = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByCustomer_num] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
       // DataSet ds_customer = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByCustomer] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        gv_customer.DataSource = ds_customer.Tables[0];
        gv_customer.DataBind();
        CreateChart_customer(ds_customer.Tables[0]);
        string company_ddl = "ALL";
        string year_ddl = txt_year.SelectedValue;
        string month_ddl = txt_mnth.SelectedValue;
        if (ddl_comp.SelectedValue != "")
        {
            company_ddl = ddl_comp.SelectedValue;
        }
        if (year_ddl == "")
        {
            year_ddl = DateTime.Now.Year.ToString();
            month_ddl = DateTime.Now.Month.ToString();
        }
        this.lblCustomer.Text = "按【客户】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
    }
    public void ShowArea()
    {
        //地区
        ChartD.Series.Clear();
        DataSet ds_area;
        if (ddl_item.SelectedValue == "1")//金额
        {
            ds_area = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByArea] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
        else//按数量
        {
            ds_area = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByArea_num] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
       // DataSet ds_area = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByArea] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        gv_area.DataSource = ds_area.Tables[0];
        gv_area.DataBind();
        CreateChart_area(ds_area.Tables[0]);
        string company_ddl = "ALL";
        string year_ddl = txt_year.SelectedValue;
        string month_ddl = txt_mnth.SelectedValue;
        if (ddl_comp.SelectedValue != "")
        {
            company_ddl = ddl_comp.SelectedValue;
        }
        if (year_ddl == "")
        {
            year_ddl = DateTime.Now.Year.ToString();
            month_ddl = DateTime.Now.Month.ToString();
        }
        this.lblArea.Text = "按【地区】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
    }
    public void ShowMaterial()
    {
        ChartE.Series.Clear();
        ChartF.Series.Clear();
        //材料
        DataSet ds_material;
        if (ddl_item.SelectedValue == "1")//金额
        {
            ds_material = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByMaterial] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
        else//按数量
        {
            ds_material = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByMaterial_num] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        }
        //DataSet ds_material = DbHelperSQL.Query("exec [MES_Sales_Fenxi_ByMaterial] '" + txt_year.SelectedValue + "', '" + txt_mnth.SelectedValue + "', '" + ddl_comp.SelectedValue + "'");
        gv_material.DataSource = ds_material.Tables[0];
        gv_material.DataBind();
        CreateChart_material(ds_material.Tables[0]);
        string company_ddl = "ALL";
        string year_ddl = txt_year.SelectedValue;
        string month_ddl = txt_mnth.SelectedValue;
        if (ddl_comp.SelectedValue != "")
        {
            company_ddl = ddl_comp.SelectedValue;
        }
        if (year_ddl == "")
        {
            year_ddl = DateTime.Now.Year.ToString();
            month_ddl = DateTime.Now.Month.ToString();
        }
        this.lblMaterial.Text = "按【材料】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
        //产品类别       
        gv_cplb.DataSource = ds_material.Tables[1];
        gv_cplb.DataBind();
        CreateChart_cplb(ds_material.Tables[1]);

        this.lblCPLB.Text = "按【产品类别】统计: " + txt_year.SelectedValue + " 年 " + month_ddl + " 月;" + "公司别：" + company_ddl;
    }


    /// <summary>
    /// 显示Chart图形 月
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_month(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i < dt.Rows.Count-2; i++)
        {
            if (dt.Rows[i][1].ToString() != "差异")
            {
                list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Line, dt, j, 3));
                j++;
            }
            else
            {
                j++;
            }
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 显示Chart图形 年
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_year(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i < dt.Rows.Count-2; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Bar, dt, j,0));
            j++;
        }
        #endregion
        this.ChartB.Series.AddRange(list.ToArray());
        this.ChartB.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 显示Chart图形 客户
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_customer(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 2; i < dt.Rows.Count; i++)
        {
                list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Bar, dt, j));
                j++;
        }
        #endregion
        this.ChartC.Series.AddRange(list.ToArray());
        ((XYDiagram)ChartC.Diagram).AxisX.Label.Angle = -45;
        this.ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 显示Chart图形 地区
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_area(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 2; i < dt.Rows.Count; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Bar, dt, j,1));
            j++;
        }
        #endregion
        this.ChartD.Series.AddRange(list.ToArray());
        this.ChartD.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 显示Chart图形 材料
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_material(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 2; i < dt.Rows.Count; i++)
        {
          
                list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Bar, dt, j,1));
                j++;
            
        }
        #endregion
        this.ChartE.Series.AddRange(list.ToArray());
        this.ChartE.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 显示Chart图形 产品类别
    /// </summary>
    /// <param name="dt"></param>
    private void CreateChart_cplb(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 2;
        for (int i = 2; i < dt.Rows.Count; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Bar, dt, j));
            j++;
        }
        #endregion
        this.ChartF.Series.AddRange(list.ToArray());
        ((XYDiagram)ChartF.Diagram).AxisX.Label.Angle = -45;
        this.ChartF.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex,int notshow_qty)
    {
        Series series = new Series(caption, viewType);
        for (int i = 2; i < dt.Columns.Count - notshow_qty; i++)
        {
            string argument = dt.Columns[i].ColumnName;//参数名称 
            decimal value = 0;
            if (dt.Rows[rowIndex][i].ToString() != null && dt.Rows[rowIndex][i].ToString() != "")
            {
                value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
            }
                series.Points.Add(new SeriesPoint(argument, value));
            
        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }

    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
    {
        Series series = new Series(caption, viewType);
        for (int i = 2; i < dt.Columns.Count - 1; i++)
        {
            if (i < 10)
            {
                string argument = dt.Columns[i].ColumnName;//参数名称 
                decimal value = 0;
                if (dt.Rows[rowIndex][i].ToString() != null && dt.Rows[rowIndex][i].ToString() != "")
                {
                    value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
                }
                series.Points.Add(new SeriesPoint(argument, value));
            }
        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }
    protected void gv_month_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[15].Wrap = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex <=4)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if ( e.Row.RowIndex == 5)
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
    }

    protected void gv_year_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 1)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if (e.Row.RowIndex == 2 )
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
       
    }
    /// <summary>
    /// 格式化Gridview的数值为百分比
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_customer_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
           // e.Row.Cells[1].Wrap = false;
            gv_customer.Style.Add("word-break", "keep-all");
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 1)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if (e.Row.RowIndex == 2 || e.Row.RowIndex == 3)
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
    }

    protected void gv_area_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
     

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 1)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if (e.Row.RowIndex == 2 || e.Row.RowIndex == 3)
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
    }
    protected void gv_material_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 1)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if (e.Row.RowIndex == 2 || e.Row.RowIndex == 3)
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
    }
    protected void gv_cplb_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            gv_cplb.Style.Add("word-break", "keep-all");
        }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 1)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    }
                }
            }
            if (e.Row.RowIndex == 2 || e.Row.RowIndex == 3)
            {
                for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    if (e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text.ToString().Replace("&nbsp;", "") != "")
                    {
                        e.Row.Cells[i].Text = string.Format("{0:P0}", Convert.ToDecimal(e.Row.Cells[i].Text));
                    }
                }
            }
        }
    }


}
