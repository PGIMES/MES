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
using System.Drawing;


public partial class XM_XM_TJ_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();

            string strsql = "select distinct year(ppap_date)year from track order by year(ppap_date)  desc ";
            DataSet dsYear = Query(strsql);
            fun.initDropDownList(dropYear, dsYear.Tables[0], "year", "year");
            dropYear.SelectedValue = DateTime.Now.Year.ToString();
            Query_mnth();
            Query_customer();
            Query_DL();
            Query_Product();

            LinkBtn.Style.Add("display", "none");
            LinkSale.Style.Add("display", "none");
            LinkCustomer.Style.Add("display", "none");
            LinkDL.Style.Add("display", "none");




        }
    }

    public void Query_mnth()
    {
        DataSet ds = Query("exec TJ_XM_Repoart  '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue+ "'");
        DataTable dt = ds.Tables[0];
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
        setGridLink_gv();
        bindChartByMnth(ds.Tables[1]);
        bindChartPie(ds.Tables[2]);
       
    }

    public void bindChartByMnth(DataTable tbl)
    {

        ChartByMonth.DataSource = tbl;
        ChartByMonth.Series["本月新启动"].XValueMember = "no";
        ChartByMonth.Series["本月新启动"].YValueMembers = "start_xms";

        ChartByMonth.Series["本月PPAP"].XValueMember = "no";
        ChartByMonth.Series["本月PPAP"].YValueMembers = "ppap_xms";

        

    }
    public void bindChartPie(DataTable tbl)
    {

        this.C2.DataSource = tbl;
        this.C2.Series[0].XValueMember = "status";
        this.C2.Series[0].YValueMembers = "bs";
        this.C2.DataBind();
        this.C2.Series[0].ToolTip = "#VALX:#VALY";
        this.C2.Series[0].LegendToolTip = "#VALX:#VALY";
        //this.C2.Series[0].Label = "#VALX";
        C2.Series[0].ToolTip = "#AXISLABEL #VALY";
        C2.Series[0].PostBackValue = "#VALX";
        this.C2.Series["Series1"].Label = "#VALY";
        this.C2.Series["Series1"].LegendText = "#VALX:#VALY";


        this.C2.Series["Series1"].Points[0].Color = Color.DodgerBlue;
        this.C2.Series["Series1"].Points[1].Color = Color.DarkKhaki;
        this.C2.Series["Series1"].Points[2].Color = Color.IndianRed;
        this.C2.Series["Series1"].Points[3].Color = Color.LightSteelBlue;



    }
    public void Query_customer()
    {
        DataSet ds = Query("exec TJ_XM_Repoart  '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "'");
        DataTable dt = ds.Tables[3];
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
        if (dt.Rows.Count > 0)
        {
            setGridLink_gv_customer();
        }
        bindChartCustomer(ds.Tables[4]);
    }

    public void bindChartCustomer(DataTable tbl)
    {

        Chart1.DataSource = tbl;
        Chart1.Series["未启动工装"].XValueMember = "customer";
        Chart1.Series["未启动工装"].YValueMembers = "num1";
        Chart1.Series["开发"].XValueMember = "customer";
        Chart1.Series["开发"].YValueMembers = "num2";
        Chart1.Series["批产"].XValueMember = "customer";
        Chart1.Series["批产"].YValueMembers = "num3";
        Chart1.Series["爬坡"].XValueMember = "customer";
        Chart1.Series["爬坡"].YValueMembers = "num4";
        Chart1.DataBind();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {

            this.Chart1.Series["未启动工装"].Points[i].AxisLabel = tbl.Rows[i]["customer"].ToString();
            this.Chart1.Series["未启动工装"].Points[i].ToolTip = tbl.Rows[i]["customer"].ToString() + "未启动工装 " + "(" + tbl.Rows[i]["num1"].ToString() + ")";
            this.Chart1.Series["爬坡"].Points[i].ToolTip = tbl.Rows[i]["customer"].ToString() + "爬坡 " + "(" + tbl.Rows[i]["num4"].ToString() + ")";
            this.Chart1.Series["批产"].Points[i].ToolTip = tbl.Rows[i]["customer"].ToString() + "批产 " + "(" + tbl.Rows[i]["num3"].ToString() + ")";
            this.Chart1.Series["开发"].Points[i].ToolTip = tbl.Rows[i]["customer"].ToString() + "开发 " + "(" + tbl.Rows[i]["num2"].ToString() + ")";
          
           
          
          

        }

        this.Chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
     
    }

    public void Query_DL()
    {
        DataSet ds = Query("exec TJ_XM_Repoart  '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "'");
        DataTable dt = ds.Tables[5];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord")
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        gv_dl.DataSource = dt;
        gv_dl.DataBind();
        ViewState["tblDL"] = dt;
        if (dt.Rows.Count > 0)
        {
            setGridLink_gv_dl();
        }
        bindChartDL(ds.Tables[7]);
    }
    public void bindChartDL(DataTable tbl)
    {

        //Chart2.DataSource = tbl;
        //Chart2.Series["开发中"].XValueMember = "material_id";
        //Chart2.Series["开发中"].YValueMembers = "dev_num";
        //this.Chart3.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        Chart2.DataSource = tbl;
        Chart2.Series["未启动工装"].XValueMember = "product_dl";
        Chart2.Series["未启动工装"].YValueMembers = "num1";
        Chart2.Series["开发"].XValueMember = "product_dl";
        Chart2.Series["开发"].YValueMembers = "num2";
        Chart2.Series["批产"].XValueMember = "product_dl";
        Chart2.Series["批产"].YValueMembers = "num3";
        Chart2.Series["爬坡"].XValueMember = "product_dl";
        Chart2.Series["爬坡"].YValueMembers = "num4";
        Chart2.DataBind();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {

            this.Chart2.Series["未启动工装"].Points[i].AxisLabel = tbl.Rows[i]["product_dl"].ToString();

            this.Chart2.Series["未启动工装"].Points[i].ToolTip = tbl.Rows[i]["product_dl"].ToString() + "未启动工装 " + "(" + tbl.Rows[i]["num1"].ToString() + ")";
            this.Chart2.Series["开发"].Points[i].ToolTip = tbl.Rows[i]["product_dl"].ToString() + "开发 " + "(" + tbl.Rows[i]["num2"].ToString() + ")";
            this.Chart2.Series["批产"].Points[i].ToolTip = tbl.Rows[i]["product_dl"].ToString() + "批产 " + "(" + tbl.Rows[i]["num3"].ToString() + ")";
            this.Chart2.Series["爬坡"].Points[i].ToolTip = tbl.Rows[i]["product_dl"].ToString() + "爬坡 " + "(" + tbl.Rows[i]["num4"].ToString() + ")";

        }

       // this.Chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

    }

    public void Query_Product()
    {
        DataSet ds = Query("exec TJ_XM_Repoart  '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "'");
        DataTable dt = ds.Tables[8];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataColumn col = dt.Columns[i];
            if (col.ColumnName == "ord")
            {
                dt.Columns.Remove(col);
                i = 0;
            }
        }
        Gv_Product.DataSource = dt;
        Gv_Product.DataBind();
        ViewState["tblProduct"] = dt;
        if (dt.Rows.Count > 0)
        {
            setGridLink_gv_product();
        }
        bindChartProduct(ds.Tables[10]);
       
    }

    public void bindChartProduct(DataTable tbl)
    {

        //Chart3.DataSource = tbl;
        //Chart3.Series["工程师"].XValueMember = "suser";
        //Chart3.Series["工程师"].YValueMembers = "dev_num";
        //this.Chart3.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        Chart3.DataSource = tbl;
        Chart3.Series["未启动工装"].XValueMember = "suser";
        Chart3.Series["未启动工装"].YValueMembers = "num1";
        Chart3.Series["开发"].XValueMember = "suser";
        Chart3.Series["开发"].YValueMembers = "num2";
        Chart3.Series["批产"].XValueMember = "suser";
        Chart3.Series["批产"].YValueMembers = "num3";
        Chart3.Series["爬坡"].XValueMember = "suser";
        Chart3.Series["爬坡"].YValueMembers = "num4";
        Chart3.DataBind();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {

            this.Chart3.Series["未启动工装"].Points[i].AxisLabel = tbl.Rows[i]["suser"].ToString();

            this.Chart3.Series["未启动工装"].Points[i].ToolTip = tbl.Rows[i]["suser"].ToString() + "未启动工装 " + "(" + tbl.Rows[i]["num1"].ToString() + ")";
            this.Chart3.Series["开发"].Points[i].ToolTip = tbl.Rows[i]["suser"].ToString() + "开发 " + "(" + tbl.Rows[i]["num2"].ToString() + ")";
            this.Chart3.Series["批产"].Points[i].ToolTip = tbl.Rows[i]["suser"].ToString() + "批产 " + "(" + tbl.Rows[i]["num3"].ToString() + ")";
            this.Chart3.Series["爬坡"].Points[i].ToolTip = tbl.Rows[i]["suser"].ToString() + "爬坡 " + "(" + tbl.Rows[i]["num4"].ToString() + ")";

        }

    }

    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["connstringMoJu"];
        SqlConnection connection = new SqlConnection(connString);
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
            command.Fill(ds, "ds");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;


    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Query_mnth();
        Query_customer();
        Query_DL();
        Query_Product();
        AddLink();
        gvdetail.DataSource = null;
        gvdetail.DataBind();
        lblDays.Text = "明细";
    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "月份";

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }

    }

    public void setGridLink_gv()
    {
        GridViewRow dr = gv.HeaderRow;

        for (int i = 0; i < gv.Rows.Count; i++)
        {

            GridViewRow row = (GridViewRow)gv.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = gv.Rows[i].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "decrib");

                string strMon = dr.Cells[j].Text;
                string decrib = Server.HtmlEncode(row.Cells[0].Text);
                

                lbtn.Attributes.Add("mon", strMon);
                lbtn.Attributes.Add("decrib_mnth", decrib);

                gv.Rows[i].Cells[j].Controls.Add(lbtn);
                gv.Rows[i].Cells[j].Attributes.Add("allowClick", "true");

            }
        }
    }
    public void setGridLink_gv_customer()
    {
        GridViewRow dr = gv_customer.HeaderRow;

        for (int i = 0; i < 1; i++)
        {
            GridViewRow row = (GridViewRow)gv_customer.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = gv_customer.Rows[0].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkCustomer','')");
                lbtn.Attributes.Add("name", "Customer");
                
                string customer = dr.Cells[j].Text;
                lbtn.Attributes.Add("customerid", customer);
                gv_customer.Rows[i].Cells[j].Controls.Add(lbtn);
                gv_customer.Rows[i].Cells[j].Attributes.Add("allowClick", "true");
            }
        }
        
    }

    public void setGridLink_gv_dl()
    {
        GridViewRow dr = gv_dl.HeaderRow;

        for (int i = 0; i < 1; i++)
        {
            GridViewRow row = (GridViewRow)gv_dl.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = gv_dl.Rows[0].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDL','')");
                lbtn.Attributes.Add("name", "DL");

                string DL = dr.Cells[j].Text;
                lbtn.Attributes.Add("DL", DL);
                gv_dl.Rows[i].Cells[j].Controls.Add(lbtn);
                gv_dl.Rows[i].Cells[j].Attributes.Add("allowClick", "true");
            }
        }

    }

    public void setGridLink_gv_product()
    {
        GridViewRow dr = Gv_Product.HeaderRow;

        for (int i = 0; i < 1; i++)
        {
            GridViewRow row = (GridViewRow)Gv_Product.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = Gv_Product.Rows[0].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkSale','')");
                lbtn.Attributes.Add("name", "sale");

                string sale = dr.Cells[j].Text;
                lbtn.Attributes.Add("sale", sale);
                Gv_Product.Rows[i].Cells[j].Controls.Add(lbtn);
                Gv_Product.Rows[i].Cells[j].Attributes.Add("allowClick", "true");
            }
        }

    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        setGridLink_gv();
        string describ = txtbymnth.Text;
        string mnth = txtmonth.Text;
        
        //TJ_XM_Repoart_Detail '2017','','','1','开发中项目数量','','',''

        DataSet ds = Query("exec  TJ_XM_Repoart_Detail '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "','" + mnth + "','" + describ + "','','',''");
        gvdetail.DataSource = ds.Tables[0];
        gvdetail.DataBind();
        ViewState["gvdetail"] = ds.Tables[0];
        lblDays.Text = mnth +"月 "+ describ.Replace("本月","");
        AddLink();
    }

    protected void AddLink()
    {
        int rowIndex = 1;
        for (int j = 0; j <= gvdetail.Rows.Count - 1; j++)
        {
            if (gvdetail.Rows[j].Cells[0].Visible == true)
            {
                gvdetail.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                string pgi_no = gvdetail.Rows[j].Cells[1].Text;
                string mainid = gvdetail.Rows[j].Cells[12].Text;
            
                link.NavigateUrl = "http://172.16.5.6:8080/source1/Track.aspx?mainid=" + mainid;
                link.Text = gvdetail.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                gvdetail.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;
            }
            //gvdetail.Rows[j].Cells[19].Attributes.Add("onclick", "OpenMsg(this.firstElementChild),'');");
        }
    }

   
    protected void gv_customer_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_customer.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "客户";

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }

    }
 
    protected void LinkSale_Click(object sender, EventArgs e)
    {
        setGridLink_gv_product();
        string user = txtsale.Text;
        DataSet ds = Query("exec  TJ_XM_Repoart_Detail '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "','','','','','"+user+"'");
        gvdetail.DataSource = ds.Tables[0];
        gvdetail.DataBind();
        ViewState["gvdetail"] = ds.Tables[0];
        lblDays.Text = user +" 开发中项目数量";
        AddLink();
    }
    protected void LinkCustomer_Click(object sender, EventArgs e)
    {
        setGridLink_gv_customer();
        string customer = txtCustomer.Text;
        DataSet ds = Query("exec  TJ_XM_Repoart_Detail '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "','','','"+customer+"','',''");
        gvdetail.DataSource = ds.Tables[0];
        gvdetail.DataBind();
        ViewState["gvdetail"] = ds.Tables[0];
        lblDays.Text = customer + " 开发中项目数量";
        AddLink();
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = dropengineer.SelectedValue;

        }
    }
    


    protected void LinkDL_Click(object sender, EventArgs e)
    {
        setGridLink_gv_dl();
        string dl = txtDL.Text;
        

        //TJ_XM_Repoart_Detail '2017','','','1','开发中项目数量','','',''

        DataSet ds = Query("exec  TJ_XM_Repoart_Detail '" + dropYear.SelectedValue + "','" + dropengineer.SelectedValue + "','" + dropdept.SelectedValue + "','','','','"+dl+"',''");
        gvdetail.DataSource = ds.Tables[0];
        gvdetail.DataBind();
        ViewState["gvdetail"] = ds.Tables[0];
        lblDays.Text = dl + " 开发中项目数量";
        AddLink();
    }
    protected void gv_dl_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_dl.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "产品大类";

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }
    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void Gv_Product_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Gv_Product.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = dropengineer.SelectedValue;

        }
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }
    }

    protected void gv_customer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 1; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text.ToString() == "0")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }

    protected void gv_dl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 1; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text.ToString() == "0")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }

    protected void Gv_Product_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 1; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text.ToString() == "0")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }

    protected void gvdetail_Sorting(object sender, GridViewSortEventArgs e)
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
        DataTable ldt2 = (DataTable)ViewState["gvdetail"];
        DataView dv = ldt2.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }
        this.gvdetail.DataSource = dv;
        this.gvdetail.DataBind();
        AddLink();
    }
}