using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
public class Ship
{
    public int status_id { get; set; }
    public string status { get; set; }
}
public partial class Product_Chanpin_Query : System.Web.UI.Page
{
    decimal ntotal_Q1 = 0;
    decimal ntotal_Q2 = 0;
    decimal ntotal_Q3= 0;
    decimal ntotal_Q4 = 0;
    decimal ntotal_Q5 = 0;
    decimal ntotal_Q6 = 0;
    decimal ntotal_Q7 = 0;
    decimal ntotal_Q8 = 0;
    decimal ntotal_Q9 = 0;
    decimal ntotal_Q10 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 1000;
        this.GridView2.PageSize = 1000;
        this.GridView3.PageSize = 1000;

        if (!IsPostBack)
        {
            //绑定DDL
            Setddl_make_factory();//给生产地点ddl_make_factory  Dropdownlist 绑定数据源
            Setddl_ship_from("");//给发运地点  ddl_ship_from 绑定数据源
            Setddl_ship_to(""); //给发往地点 ddl_ship_to  Dropdownlist 绑定数据源
            Setddl_dept();
            Setddl_cpfzr();// 给发往地点 ddl_cpfzr Dropdownlist 绑定数据源
            Setddl_p_leibie();
            Setddl_p_status();
            bindCust();
            Setddl_dingdian_year();
            Setddl_pc_year();
            Setddl_end_year();
            Query("1");
            
        }

    }
    public void Setddl_make_factory()
    {
  
        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct make_factory  as  status_id,make_factory as status from form3_Sale_Product_MainTable ";
        DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];

        if (ship_from.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_make_factory, ship_from, "status_id", "status");
        }
        ddl_make_factory.Items.Insert(0, new ListItem("全部", "-1"));
    }
    public void Setddl_ship_from(string cust)
    {       
        //BaseFun fun = new BaseFun();
        //string strSQL = @"select distinct ship_from as  status_id,ship_from as status   from form3_Sale_ProductQuantity_DetailTable ";
        //DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];
        var arr = cust.Split(',');
        string val = "";
        if (cust == "-1" || cust == "")
        {
            val = "";
        }
        else if (arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                val = val + "'" + arr[i] + "',";
            }
            val = val.TrimEnd(',');
        }
        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct ship_from as  status_id,ship_from as status   from form3_Sale_ProductQuantity_DetailTable a
                            join [dbo].[form3_Sale_Product_MainTable] b on a.pgino=b.pgino ";
        if (cust != "-1" && cust != "" && cust != "null")
        {
            strSQL = strSQL + " where b.customer_name in(" + val + ") "; ;
        }


        DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];
        if (ship_from.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_ship_from, ship_from, "status_id", "status");
        }
        ddl_ship_from.Items.Insert(0, new ListItem("全部", "-1"));
    }


    [WebMethod]
    public static string ship_fromBy(string cust)
    {
        var arr = cust.Split(',');
        string val = "";
        if (cust == "-1"||cust=="")
        {
            val = "";
        }
       else if(arr.Length>0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                val = val +"'"+ arr[i]+"',";
            }
            val = val.TrimEnd(',');
        }
        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct ship_from as  status_id,ship_from as status   from form3_Sale_ProductQuantity_DetailTable a
                            join [dbo].[form3_Sale_Product_MainTable] b on a.pgino=b.pgino ";
        if (cust != "-1"&&cust!=""&& cust!="null")
        {
            strSQL = strSQL+ " where b.customer_name in(" + val + ") "; ;
        }
        
        DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];
        Ship stu = new Ship();
        string stuJsonString = JsonConvert.SerializeObject(ship_from);
       
        return stuJsonString;
    }
    public void Setddl_ship_to(string cust)
    {       
        var arr = cust.Split(',');
        string val = "";
        if (cust == "-1" || cust == "")
        {
            val = "";
        }
        else if (arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                val = val + "'" + arr[i] + "',";
            }
            val = val.TrimEnd(',');
        }

        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct ship_to as  status_id,ship_to as status   from form3_Sale_ProductQuantity_DetailTable a
                            join [dbo].[form3_Sale_Product_MainTable] b on a.pgino=b.pgino ";
        if (cust != "-1" && cust != "" && cust != "null")
        {
            strSQL = strSQL + " where b.customer_name in(" + val + ") "; ;
        }

        DataTable ship_to = DbHelperSQL.Query(strSQL).Tables[0];

        if (ship_to.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_ship_to, ship_to, "status_id", "status");
        }
        ddl_ship_to.Items.Insert(0, new ListItem("全部", "-1"));
    }
    [WebMethod]
    public static string ship_toBy(string cust)
    {
        var arr = cust.Split(',');
        string val = "";
        if (cust == "-1" || cust == "")
        {
            val = "";
        }
        else if (arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                val = val + "'" + arr[i] + "',";
            }
            val = val.TrimEnd(',');
        }

        BaseFun fun = new BaseFun();
        string strSQL = @"select distinct ship_to as  status_id,ship_to as status   from form3_Sale_ProductQuantity_DetailTable a
                            join [dbo].[form3_Sale_Product_MainTable] b on a.pgino=b.pgino ";
        if (cust != "-1" && cust != "" && cust != "null")
        {
            strSQL = strSQL + " where b.customer_name in(" + val + ") "; ;
        }

        DataTable ship_to = DbHelperSQL.Query(strSQL).Tables[0];

        string jsonstring = JsonConvert.SerializeObject(ship_to);
        return jsonstring;
    }


    public void Setddl_dept()
    {
        BaseFun fun = new BaseFun();
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Get_CPFZR_List] '" + this.ddl_dept.SelectedValue.ToString() + "'");
        DataTable dept = ds.Tables[1];
        if (dept.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_dept, dept, "status_id", "status");
        }
        ddl_dept.Items.Insert(0, new ListItem("全部", "全部"));
    }
    public void Setddl_p_leibie()
    {
        string strSQL = @"	SELECT CLASS_NAME as  status_id,CLASS_NAME as status  from form3_Sale_Product_BASE
                            where base_name='DDL_product_leibie'
                            order by CLASS_ID";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectPLeibie.DataTextField = dt.Columns[0].ColumnName;
        selectPLeibie.DataValueField = dt.Columns[0].ColumnName;
        this.selectPLeibie.DataSource = dt;
        this.selectPLeibie.DataBind();
    }
    public void Setddl_p_status()
    {
        string strSQL = @"	select distinct product_status as  status_id,product_status as status from form3_Sale_Product_MainTable ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectPStatus.DataTextField = dt.Columns[0].ColumnName;
        selectPStatus.DataValueField = dt.Columns[0].ColumnName;
        this.selectPStatus.DataSource = dt;
        this.selectPStatus.DataBind();
    }
    protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       // Setddl_cpfzr();
    }
    public void Setddl_cpfzr()
    {
        BaseFun fun = new BaseFun();
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Get_CPFZR_List] '" + this.ddl_dept.SelectedValue.ToString() + "'");
        DataTable CPFZR_List = ds.Tables[0];
        if (CPFZR_List.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_cpfzr, CPFZR_List, "status_id", "status");
        }
        ddl_cpfzr.Items.Insert(0, new ListItem("全部", "-1"));
    }
    [WebMethod]
    public static string Setddl_cpfzr(string dept)
    {
        BaseFun fun = new BaseFun();
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Get_CPFZR_List] '" + dept + "'");
        DataTable CPFZR_List = ds.Tables[0];
        //if (CPFZR_List.Rows.Count > 0)
        //{
        //    fun.initDropDownList(this.ddl_cpfzr, CPFZR_List, "status_id", "status");
        //}
        //ddl_cpfzr.Items.Insert(0, new ListItem("全部", "-1"));
        string jsonstring = JsonConvert.SerializeObject(CPFZR_List);
        return jsonstring;
    }
    //顾客状态下拉列表
    public void bindCust()
    {
        string strSQL = @"select distinct customer_name from [dbo].[form3_Sale_Product_MainTable]";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectCust.DataTextField = dt.Columns[0].ColumnName;
        selectCust.DataValueField = dt.Columns[0].ColumnName;
        this.selectCust.DataSource = dt;
        this.selectCust.DataBind();
    }

    public void Setddl_dingdian_year()
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select  distinct year(dingdian_date) as status_id,year(dingdian_date) as status from form3_Sale_Product_MainTable where dingdian_date is not null ";
        DataTable dingdian_year = DbHelperSQL.Query(strSQL).Tables[0];

        if (dingdian_year.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_dingdian_year, dingdian_year, "status_id", "status");
        }
        ddl_dingdian_year.Items.Insert(0, new ListItem("全部", "-1"));
    }
    public void Setddl_pc_year()
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select  distinct year(pc_date) as status_id,year(pc_date) as status from form3_Sale_Product_MainTable where pc_date is not null ";
        DataTable pc_year = DbHelperSQL.Query(strSQL).Tables[0];

        if (pc_year.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_pc_year, pc_year, "status_id", "status");
        }
        ddl_pc_year.Items.Insert(0, new ListItem("全部", "-1"));
    }
    public void Setddl_end_year()
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select  distinct year(end_date) as status_id ,year(end_date) as status from form3_Sale_Product_MainTable where end_date is not null ";
        DataTable end_year = DbHelperSQL.Query(strSQL).Tables[0];

        if (end_year.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_end_year, end_year, "status_id", "status");
        }
        ddl_end_year.Items.Insert(0, new ListItem("全部", "-1"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.ddl_type.SelectedValue.ToString()== "1")
        {
            this.GridView1.Visible = true;
            this.GridView1.DataSource = null;
            GridView1.DataBind();
            Query("1");
            GridView2.DataSource = null;
            GridView2.DataBind();
            this.GridView2.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            this.GridView3.Visible = false;
        }
        if (this.ddl_type.SelectedValue.ToString() == "2")
        {
            this.GridView2.Visible = true;
            this.GridView2.DataSource = null;
            GridView2.DataBind();
            Query("2");
            GridView1.DataSource = null;
            GridView1.DataBind();
            this.GridView1.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            this.GridView3.Visible = false;
        }
        if (this.ddl_type.SelectedValue.ToString() == "3")
        {
            this.GridView3.Visible = true;
            this.GridView3.DataSource = null;
            GridView3.DataBind();
            Query("3");
            GridView1.DataSource = null;
            GridView1.DataBind();
            this.GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            this.GridView2.Visible = false;
        }
    }
    public void QueryGridView1(DataSet ds)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        this.GetsumJiBen(ds.Tables[0]);
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
        
        int[] cols = {0,1,2,15,20,3,4,5,23, 24, 6,7,8,9};
        MergGridRow.MergeRow(GridView1, cols);
        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[21].Text).Trim();
                link.Text = GridView1.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[2].Controls.Add(link);
                rowIndex++;
            }
        }
    }
    public void QueryGridView2(DataSet ds)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        this.Getsum(ds.Tables[0]);
        this.GridView2.DataSource = ds.Tables[0];
        this.GridView2.DataBind();
        
        int[] cols = { 0, 1,2, 3, 4, 5, 6, 7,24,8,9,10 , 25 };
        MergGridRow.MergeRow(GridView2, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = rowIndex.ToString();
                //HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                //link.ImageUrl = this.GridView2.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                Image img = new Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView2.Rows[j].Cells[28].Text).Trim();                
                GridView2.Rows[j].Cells[24].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }
        ////后台设定列宽
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            GridView2.Rows[i].Cells[1].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[2].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[3].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[4].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[5].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[6].Width = Convert.ToInt16(80);
            GridView2.Rows[i].Cells[7].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[8].Width = Convert.ToInt16(120);
            GridView2.Rows[i].Cells[8].Style.Add("word-break", "break-all");
            GridView2.Rows[i].Cells[9].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[10].Width = Convert.ToInt16(100);
            GridView2.Rows[i].Cells[25].Width = Convert.ToInt16(100);

            for (int j = 11; j < 24; j++)
            {
                GridView2.Rows[i].Cells[j].Width = Convert.ToInt16(60);
            }

        }

    }

    public void QueryGridView3(DataSet ds)
    {
        GridView3.DataSource = null;
        GridView3.DataBind();
        this.Getsum(ds.Tables[0]);
        this.GridView3.DataSource = ds.Tables[0];
        this.GridView3.DataBind();
        int[] cols = { 0, 1,2, 3, 4, 5, 6, 7, 23, 8,9,10, 24 };
        MergGridRow.MergeRow(GridView3, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = rowIndex.ToString();
                //HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                //link.ImageUrl = this.GridView3.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                Image img = new Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView3.Rows[j].Cells[27].Text).Trim();
                GridView3.Rows[j].Cells[23].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                link.Text = GridView3.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView3.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }
        ////后台设定列宽
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {          

            for (int j = 11; j < 22; j++)
            {
                GridView3.Rows[i].Cells[j].Width = Convert.ToInt16(60);
            }

        }
    }
    public void Query(string type)
    { 
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] '"+type+"','" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','"+this.txt_productcode.Text.Trim().ToString()+"','"+this.txt_customer_project.Text.ToString()+"','"+this.ddl_make_factory.SelectedValue.ToString()+"','"+this.ddl_ship_from.SelectedValue.ToString()+"','"+this.ddl_ship_to.SelectedValue.ToString()+"','"+this.ddl_cpfzr.SelectedValue.ToString()+ "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','"+this.ddl_dingdian_year.SelectedValue.ToString()+"','"+this.ddl_pc_year.SelectedValue.ToString()+"','"+this.ddl_end_year.SelectedValue.ToString()+"','"+this.ddlUpdate_date_type.SelectedValue+"'");
        if (type=="1")
        {
            QueryGridView1(ds);

        }
        if (type == "2")
        {
            QueryGridView2(ds);
            setHeader(GridView2);
        }
        if (type == "3")
        {
            QueryGridView3(ds);
            setHeader(GridView3);
        }

    }
    public void setHeader(GridView grid)
    {
        GridViewRow row = grid.HeaderRow;
        for (int i = 10; i < row.Cells.Count; i++)
        {
            switch (row.Cells[i].Text)
            {
                case "C1":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<span style='text-decoration:underline'>" + (DateTime.Now.Year - 2).ToString() + "</span><br>A";
                    break;
                case "C2":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<span style='text-decoration:underline'>" + (DateTime.Now.Year - 1).ToString() + "</span><br>A";
                    break;
                case "C3":
                    row.Cells[i].Style.Add("text-align", "center");
                    row.Cells[i].Text = "<table style='width:100%'><tr><td colspan=2 style='text-decoration:underline;text-align:center;' > " + (DateTime.Now.Year ).ToString() + "</td></tr><tr><td>A</td><td alt='Forcast'>F</td></tr></table>";
                    row.Cells[i].ColumnSpan = 2;
                    row.Cells[i + 1].Visible = false;
                    i = i + 1;
                    break;
                default:
                    if (Maticsoft.Common.Assistant.IsNumberic(row.Cells[i].Text))
                    {
                        row.Cells[i].Style.Add("text-align", "center");
                        row.Cells[i].Text = "<span style='text-decoration:underline'>" + row.Cells[i].Text + "</span><br>F";
                    }
                    break;
            }

            
        }
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[21].Style.Add("display", "none");
            e.Row.Cells[22].Style.Add("display", "none");
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Style.Add("word-break", "break-all");//零件号
            e.Row.Cells[4].Style.Add("width", "140px");
            e.Row.Cells[8].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[8].Style.Add("width", "120px");
            e.Row.Cells[21].Style.Add("display", "none");
            e.Row.Cells[22].Style.Add("display", "none");

            e.Row.Cells[1].Width = Convert.ToInt16(80);
            e.Row.Cells[2].Width = Convert.ToInt16(60);
            e.Row.Cells[3].Width = Convert.ToInt16(60);
            e.Row.Cells[4].Width = Convert.ToInt16(140);//零件号
            e.Row.Cells[5].Width = Convert.ToInt16(100);
            e.Row.Cells[6].Width = Convert.ToInt16(80);
            e.Row.Cells[7].Width = Convert.ToInt16(80);//最终顾客            
            e.Row.Cells[9].Width = Convert.ToInt16(50);//生产地点
            e.Row.Cells[10].Width = Convert.ToInt16(100);
            e.Row.Cells[19].Width = Convert.ToInt16(60);//产品状态
            e.Row.Cells[17].Width = Convert.ToInt16(90);
            e.Row.Cells[18].Width = Convert.ToInt16(90);
            e.Row.Cells[23].Width = Convert.ToInt16(90);
            e.Row.Cells[24].Width = Convert.ToInt16(100);//更新内容

            Image img = new Image();
            img.Width = 40;
            img.Height = 40;
            img.ImageUrl = e.Row.Cells[20].Text;
            e.Row.Cells[20].Controls.Add(img);

            DataRowView drv = (DataRowView)e.Row.DataItem;
            for (int i = 12; i < 16; i++)
            {
                if (i != 13)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; 
            }

            //如果该订单是停产状态，则整行显示灰色
            if ( (e.Row.Cells[22].Text.ToString().Trim() == "停产" || e.Row.Cells[22].Text.ToString().Trim() == "项目取消"))
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[19].Text.ToString().Trim() == "停产" || e.Row.Cells[19].Text.ToString().Trim() == "项目取消")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[19].Text.ToString().Trim() == "开发中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

        }
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");
            e.Row.Cells[11].Text = "合计";
            //this.GetsumJiBen();
            e.Row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q2.ToString("N0");

            e.Row.Cells[21].Style.Add("display", "none");
            e.Row.Cells[22].Style.Add("display", "none");

        }

    }
    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");
            
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Style.Add("word-break", "break-all");//零件号
            e.Row.Cells[4].Style.Add("width", "140px");
            e.Row.Cells[8].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[8].Style.Add("width", "120px");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");

            e.Row.Cells[1].Width = Convert.ToInt16(80);
            e.Row.Cells[2].Width = Convert.ToInt16(80);
            e.Row.Cells[3].Width = Convert.ToInt16(60);
            e.Row.Cells[4].Width = Convert.ToInt16(110);
            e.Row.Cells[5].Width = Convert.ToInt16(100);
            e.Row.Cells[6].Width = Convert.ToInt16(80);
            e.Row.Cells[7].Width = Convert.ToInt16(100);
            e.Row.Cells[8].Width = Convert.ToInt16(120);
            e.Row.Cells[9].Width = Convert.ToInt16(100);
            e.Row.Cells[10].Width = Convert.ToInt16(100);
            e.Row.Cells[24].Width = Convert.ToInt16(40);//更新
            e.Row.Cells[25].Width = Convert.ToInt16(50);

            for (int i = 11; i < 24; i++)
            {
                if (i != 11)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));


                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == 99999999)
                    {
                        e.Row.Cells[i].Text = "小计";
                        e.Row.Cells[i - 3].Text = ""; //顾客项目栏位改为空
                        e.Row.Cells[i - 1].Text = "";
                        e.Row.Style.Add("font-weight", "bold");
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            //如果该订单是停产状态，则整行显示灰色
            if (e.Row.Cells[2].Text.ToString().Trim() == "停产" || e.Row.Cells[2].Text.ToString().Trim() == "项目取消")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[2].Text.ToString().Trim() == "开发中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            if (e.Row.Cells[22].Text.ToString().Trim() != "" && e.Row.Cells[23].Text.ToString().Trim() != "")
            {
                if (Convert.ToDecimal(e.Row.Cells[23].Text) - Convert.ToDecimal(e.Row.Cells[22].Text) < 0)
                {
                    e.Row.Cells[23].BackColor = System.Drawing.Color.Red;
                }

            }
            //增加预测和实际数值比较
            double Act = Convert.ToDouble(e.Row.Cells[14].Text.ToString().Replace(",", ""));//实际
            double last_Act = Convert.ToDouble(e.Row.Cells[13].Text.ToString().Replace(",", ""));//前一年的实际

            double yc_curryear = Convert.ToDouble(e.Row.Cells[15].Text.ToString().Replace(",", "")); //当年预测数

            double next_curryear = Convert.ToDouble(e.Row.Cells[16].Text.ToString().Replace(",", "")); //第二年预测数

            int month = Convert.ToInt32(DateTime.Now.Month.ToString());

            double will_yc = 0;

            if (e.Row.Cells[11].Text != "小计")
            {

                if (month == 1)//当前月为第一个月时，拿当前年预测数和去年的实际数比较
                {
                    if ((Math.Abs((last_Act - yc_curryear) / (yc_curryear == 0 ? 1 : yc_curryear)) > 0.25) || (Math.Abs(yc_curryear - last_Act) / (last_Act == 0 ? 1 : last_Act)) > 0.25)
                    {
                        e.Row.Cells[15].Style.Add("color", "red");
                    }
                    if (Math.Abs((last_Act - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25 || Math.Abs((next_curryear - last_Act) / (last_Act == 0 ? 1 : last_Act)) > 0.25)
                    {
                        e.Row.Cells[16].Style.Add("color", "red");
                    }
                }
                else  //当前月为第一个月时，拿当前年预测数和当前年的实际数比较
                {
                    if ((Math.Abs(((Act / month) * 12 - yc_curryear) / (yc_curryear == 0 ? 1 : yc_curryear)) > 0.25) || (Math.Abs(yc_curryear - (Act / month) * 12) / (Act == 0 ? 1 : Act)) > 0.25)
                    {
                        e.Row.Cells[15].Style.Add("color", "red");
                    }
                    if (Math.Abs(((Act / month) * 12 - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25 || Math.Abs((next_curryear - (Act / month) * 12) / (Act == 0 ? 1 : Act)) > 0.25)
                    {
                        e.Row.Cells[16].Style.Add("color", "red");
                    }
                }
               

                for (int i = 16; i < 21; i++)
                {
                    next_curryear = Convert.ToDouble(e.Row.Cells[i].Text.ToString().Replace(",", ""));//从2018预测数开始比较
                    will_yc = Convert.ToDouble(e.Row.Cells[i+1].Text.ToString().Replace(",", ""));
                    if (Math.Abs((next_curryear - will_yc) / (will_yc == 0 ? 1 : will_yc)) > 0.25 || Math.Abs((will_yc - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25)
                    {
                        e.Row.Cells[i + 1].Style.Add("color", "red");
                    }
                   
                }

               
            }
        }



       
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");
            e.Row.Cells[11].Text = "合计";
            e.Row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[13].Text = this.ntotal_Q2.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q3.ToString("N0");
            e.Row.Cells[15].Text = this.ntotal_Q4.ToString("N0");
            e.Row.Cells[16].Text = this.ntotal_Q5.ToString("N0");
            e.Row.Cells[17].Text = this.ntotal_Q6.ToString("N0");
            e.Row.Cells[18].Text = this.ntotal_Q7.ToString("N0");
            e.Row.Cells[19].Text = this.ntotal_Q8.ToString("N0");
            e.Row.Cells[20].Text = this.ntotal_Q9.ToString("N0");
            e.Row.Cells[21].Text = this.ntotal_Q10.ToString("N0");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[28].Style.Add("display", "none");
        }

    }
    /// <summary>
    /// 收入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Style.Add("word-break", "break-all");//零件号
            e.Row.Cells[4].Style.Add("width", "140px");
            e.Row.Cells[8].Style.Add("word-break", "break-all");//顾客项目
            e.Row.Cells[8].Style.Add("width", "120px");
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");

            e.Row.Cells[1].Width = Convert.ToInt16(80);
            e.Row.Cells[2].Width = Convert.ToInt16(80);
            e.Row.Cells[3].Width = Convert.ToInt16(60);
            e.Row.Cells[4].Width = Convert.ToInt16(100);
            e.Row.Cells[5].Width = Convert.ToInt16(100);
            e.Row.Cells[6].Width = Convert.ToInt16(80);
            e.Row.Cells[7].Width = Convert.ToInt16(100);
            e.Row.Cells[8].Width = Convert.ToInt16(120);
            e.Row.Cells[9].Width = Convert.ToInt16(100);
            e.Row.Cells[10].Width = Convert.ToInt16(100);
            e.Row.Cells[23].Width = Convert.ToInt16(50);//更新
            e.Row.Cells[24].Width = Convert.ToInt16(100);

            for (int i = 11; i < 23; i++)
            {
                if (i != 11)
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                }
                else
                {
                    e.Row.Cells[i].Text = (string.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells[i].Text)));
                    if (Convert.ToInt32(Convert.ToDecimal(e.Row.Cells[i].Text.Replace(",", ""))) == 99999999)
                    {
                        e.Row.Cells[i].Text = "小计";
                        e.Row.Cells[i - 3].Text = ""; //顾客项目栏位改为空
                        e.Row.Cells[i - 1].Text = "";
                        e.Row.Style.Add("font-weight","bold");
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            //如果该订单是停产状态，则整行显示灰色
            if (e.Row.Cells[2].Text.ToString().Trim() == "停产" || e.Row.Cells[2].Text.ToString().Trim() == "项目取消")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            if (e.Row.Cells[2].Text.ToString().Trim() == "开发中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }



            //增加预测和实际数值比较
            double Act = Convert.ToDouble(e.Row.Cells[14].Text.ToString().Replace(",", ""));//实际
            double last_Act = Convert.ToDouble(e.Row.Cells[13].Text.ToString().Replace(",", ""));//前一年的实际

            double yc_curryear = Convert.ToDouble(e.Row.Cells[15].Text.ToString().Replace(",", "")); //当年预测数

            double next_curryear = Convert.ToDouble(e.Row.Cells[16].Text.ToString().Replace(",", "")); //第二年预测数

            int month = Convert.ToInt32(DateTime.Now.Month.ToString());

            double will_yc = 0;

            if (e.Row.Cells[11].Text != "小计")
            {

                if (month == 1)//当前月为第一个月时，拿当前年预测数和去年的实际数比较
                {
                    if ((Math.Abs((last_Act - yc_curryear) / (yc_curryear == 0 ? 1 : yc_curryear)) > 0.25) || (Math.Abs(yc_curryear - last_Act) / (last_Act == 0 ? 1 : last_Act)) > 0.25)
                    {
                        e.Row.Cells[15].Style.Add("color", "red");
                    }
                    if (Math.Abs((last_Act - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25 || Math.Abs((next_curryear - last_Act) / (last_Act == 0 ? 1 : last_Act)) > 0.25)
                    {
                        e.Row.Cells[16].Style.Add("color", "red");
                    }
                }
                else  //当前月为第一个月时，拿当前年预测数和当前年的实际数比较
                {
                    if ((Math.Abs(((Act / month) * 12 - yc_curryear) / (yc_curryear == 0 ? 1 : yc_curryear)) > 0.25) || (Math.Abs(yc_curryear - (Act / month) * 12) / (Act == 0 ? 1 : Act)) > 0.25)
                    {
                        e.Row.Cells[15].Style.Add("color", "red");
                    }
                    if (Math.Abs(((Act / month) * 12 - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25 || Math.Abs((next_curryear - (Act / month) * 12) / (Act == 0 ? 1 : Act)) > 0.25)
                    {
                        e.Row.Cells[16].Style.Add("color", "red");
                    }
                }

                for (int i = 16; i < 21; i++)
                {
                    next_curryear = Convert.ToDouble(e.Row.Cells[i].Text.ToString().Replace(",", ""));//从2018预测数开始比较
                    will_yc = Convert.ToDouble(e.Row.Cells[i + 1].Text.ToString().Replace(",", ""));
                    if (Math.Abs((next_curryear - will_yc) / (will_yc == 0 ? 1 : will_yc)) > 0.25 || Math.Abs((will_yc - next_curryear) / (next_curryear == 0 ? 1 : next_curryear)) > 0.25)
                    {
                        e.Row.Cells[i + 1].Style.Add("color", "red");
                    }

                }


            }

        }
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("text-align", "right");
            
            e.Row.Cells[11].Text = "合计";
            e.Row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
            e.Row.Cells[13].Text = this.ntotal_Q2.ToString("N0");
            e.Row.Cells[14].Text = this.ntotal_Q3.ToString("N0");
            e.Row.Cells[15].Text = this.ntotal_Q4.ToString("N0");
            e.Row.Cells[16].Text = this.ntotal_Q5.ToString("N0");
            e.Row.Cells[17].Text = this.ntotal_Q6.ToString("N0");
            e.Row.Cells[18].Text = this.ntotal_Q7.ToString("N0");
            e.Row.Cells[19].Text = this.ntotal_Q8.ToString("N0");
            e.Row.Cells[20].Text = this.ntotal_Q9.ToString("N0");
            e.Row.Cells[21].Text = this.ntotal_Q10.ToString("N0");
            e.Row.Cells[25].Style.Add("display", "none");
            e.Row.Cells[26].Style.Add("display", "none");
            e.Row.Cells[27].Style.Add("display", "none");
        }

    }
    /// <summary>
    /// 计算每年数量总和
    /// </summary>
    /// <param name="ldt"></param>
    private void Getsum(DataTable ldt)
    {
        this.ntotal_Q1 = 0;
        this.ntotal_Q2 = 0;
        this.ntotal_Q3 = 0;
        this.ntotal_Q4 = 0;
        this.ntotal_Q5 = 0;
        this.ntotal_Q6 = 0;
        this.ntotal_Q7 = 0;
        this.ntotal_Q8 = 0;
        this.ntotal_Q9 = 0;
        this.ntotal_Q10 = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (Convert.ToInt32(Convert.ToDecimal(ldt.Rows[i][11].ToString().Replace(",", ""))) != 99999999)
            {
                if (ldt.Rows[i][12].ToString() != "")
                {
                    this.ntotal_Q1 = this.ntotal_Q1 + Convert.ToDecimal(ldt.Rows[i][12].ToString());
                }
                if (ldt.Rows[i][13].ToString() != "")
                {
                    this.ntotal_Q2 = this.ntotal_Q2 + Convert.ToDecimal(ldt.Rows[i][13].ToString());
                }
                if (ldt.Rows[i][14].ToString() != "")
                {
                    this.ntotal_Q3 = this.ntotal_Q3 + Convert.ToDecimal(ldt.Rows[i][14].ToString());
                }
                if (ldt.Rows[i][15].ToString() != "")
                {
                    this.ntotal_Q4 = this.ntotal_Q4 + Convert.ToDecimal(ldt.Rows[i][15].ToString());
                }
                if (ldt.Rows[i][16].ToString() != "")
                {
                    this.ntotal_Q5 = this.ntotal_Q5 + Convert.ToDecimal(ldt.Rows[i][16].ToString());
                }
                if (ldt.Rows[i][17].ToString() != "")
                {
                    this.ntotal_Q6 = this.ntotal_Q6 + Convert.ToDecimal(ldt.Rows[i][17].ToString());
                }
                if (ldt.Rows[i][18].ToString() != "")
                {
                    this.ntotal_Q7 = this.ntotal_Q7 + Convert.ToDecimal(ldt.Rows[i][18].ToString());
                }
                if (ldt.Rows[i][19].ToString() != "")
                {
                    this.ntotal_Q8 = this.ntotal_Q8 + Convert.ToDecimal(ldt.Rows[i][19].ToString());
                }
                if (ldt.Rows[i][20].ToString() != "")
                {
                    this.ntotal_Q9 = this.ntotal_Q9 + Convert.ToDecimal(ldt.Rows[i][20].ToString());
                }
                if (ldt.Rows[i][21].ToString() != "")
                {
                    this.ntotal_Q10 = this.ntotal_Q10 + Convert.ToDecimal(ldt.Rows[i][21].ToString());
                }
            }
        }
    }
    //基本信息合计
    private void GetsumJiBen(DataTable ldt)
    {
        this.ntotal_Q1 = 0;
        this.ntotal_Q2 = 0;    
      
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            
            if (ldt.Rows[i][12].ToString() != "")
            {
                this.ntotal_Q1 = this.ntotal_Q1 + Convert.ToDecimal(ldt.Rows[i][12].ToString());
            }
          
            if (ldt.Rows[i][14].ToString() != "")
            {
                this.ntotal_Q2 = this.ntotal_Q2 + Convert.ToDecimal(ldt.Rows[i][14].ToString());
            }
                
            
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 1 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        QueryGridView1(ds);
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 2 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','"+this.txt_p_leibie.ToString()+ "','" + this.txtCustomer_name.Text+ "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        QueryGridView2(ds);
        setHeader(GridView2);
        Getsum(ds.Tables[0]);
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 3 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text+ "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        QueryGridView3(ds);
        setHeader(GridView3);
        Getsum(ds.Tables[0]);
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "desc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "desc")
            {
                ViewState["sortdirection"] = "asc";
            }
            else
            {
                ViewState["sortdirection"] = "desc";
            }
        }
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 1 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        DataView dv = ds.Tables[0].DefaultView;

        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
        this.GetsumJiBen(ds.Tables[0]);

        GridViewRow row = GridView1.FooterRow;
        row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
        row.Cells[14].Text = this.ntotal_Q2.ToString("N0");

        int[] cols = { 0, 1, 2, 3, 20, 4, 5,23, 6, 7, 8, 9, 15 };
        MergGridRow.MergeRow(GridView1, cols);
        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[21].Text).Trim();
                link.Text = GridView1.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[2].Controls.Add(link);
                rowIndex++;
            }
        }
    }

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

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
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 2 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        DataView dv = ds.Tables[0].DefaultView;
        Getsum(ds.Tables[0]);
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView2.DataSource = dv;
        this.GridView2.DataBind();
  
        int[] cols = { 0, 1,2,  3, 4,24, 5, 6, 7, 8, 22 };
        MergGridRow.MergeRow(GridView2, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = rowIndex.ToString();
                //HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                //link.ImageUrl = this.GridView2.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                Image img = new Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView2.Rows[j].Cells[28].Text).Trim();
                GridView2.Rows[j].Cells[24].Controls.Add(img);
                rowIndex++;
            }
        }

        int Index = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[26].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }

        setHeader(GridView2);

    }
    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

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
        DataSet ds = DbHelperSQL.Query("exec [dbo].[form3_Sale_Product_Query] " + 3 + ",'" + this.txt_product_status.Text.ToString() + "', '" + this.txt_pgino.Text.Trim().ToString() + "','" + this.txt_productcode.Text.Trim().ToString() + "','" + this.txt_customer_project.Text.ToString() + "','" + this.ddl_make_factory.SelectedValue.ToString() + "','" + this.ddl_ship_from.SelectedValue.ToString() + "','" + this.ddl_ship_to.SelectedValue.ToString() + "','" + this.ddl_cpfzr.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + this.ddl_dingdian_year.SelectedValue.ToString() + "','" + this.ddl_pc_year.SelectedValue.ToString() + "','" + this.ddl_end_year.SelectedValue.ToString() + "','" + this.ddlUpdate_date_type.SelectedValue + "'");
        DataView dv = ds.Tables[0].DefaultView;
        Getsum(ds.Tables[0]);
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView3.DataSource = dv;
    
        this.GridView3.DataBind();
    
        int[] cols = { 0, 1,2, 3, 4,23, 5, 6, 7, 8,9,10,  24 };
        MergGridRow.MergeRow(GridView3, cols);
        //给更新销量 添加图片和链接
        int rowIndex = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = rowIndex.ToString();
               // HyperLink link = new HyperLink();
                //link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                //link.ImageUrl = this.GridView3.ResolveClientUrl("~/Images/fdj.gif");
                //link.Target = "_blank";
                Image img = new Image();
                img.Width = 40;
                img.Height = 40;
                img.ImageUrl = Server.HtmlDecode(GridView2.Rows[j].Cells[27].Text).Trim();
                 
                GridView3.Rows[j].Cells[23].Controls.Add(img);
                rowIndex++;
            }
        }
  
        int Index = 1;
        for (int j = 0; j < GridView3.Rows.Count; j++)
        {
            if (GridView3.Rows[j].Cells[0].Visible == true)
            {
                GridView3.Rows[j].Cells[0].Text = Index.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView3.Rows[j].Cells[25].Text).Trim();
                link.Text = GridView3.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                GridView3.Rows[j].Cells[3].Controls.Add(link);
                Index++;
            }
        }

        setHeader(GridView3);

    }
    //设定排序栏位
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {   

        for (int i = 8; i < 26; i++)///‘从第一列开始设置
            try
            {
               LinkButton kk = (LinkButton)GridView2.HeaderRow.Cells[i].Controls[0];
               string txt = kk.Text;
                GridView2.HeaderRow.Cells[i].Text = txt;

            }
            catch (Exception ex)
            { }


    }



    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {   //序号列不排序
        

        for (int i = 8; i < 25; i++)///‘从第一列开始设置
            try
            {
                LinkButton lb = (LinkButton)GridView3.HeaderRow.Cells[i].Controls[0];
                string txt = lb.Text;
                GridView3.HeaderRow.Cells[i].Text = txt;

            }
            catch (Exception ex)
            { }

    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {   //序号列不排序        
        for (int i = 8; i < 25; i++)///‘
            try
            {
                LinkButton lb = (LinkButton)GridView1.HeaderRow.Cells[i].Controls[0];
                string txt = lb.Text;
                GridView1.HeaderRow.Cells[i].Text = txt;

            }
            catch (Exception ex)
            { }

    }


  
}