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
using System.Configuration;


public partial class ProductionData_BMW_CELOU_Query : System.Web.UI.Page
{
    decimal sc_cs = 0;
    decimal sc_sl = 0;
    decimal hg_sl = 0;
    decimal fp_sl = 0;
    public string sbno = "";
    public string location = "";
    public string op = "";
    public string m_sorder_id = "";
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["sbno"] != null)
        {
            this.sbno = Request.QueryString["sbno"].ToString();
        }
        if (Request.QueryString["location"] != null)
        {
            this.location = Request.QueryString["location"].ToString();
        }

        if (Request.QueryString["order_id"] != null)
        {
            this.m_sorder_id = Request.QueryString["order_id"].ToString();
        }
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //this.DDL_OP.Items.Insert(0, m_sorder_id);
            //this.DDL_loc.Items.Insert(0, location);

            string sql = " select DISTINCT op from Data_Setting  where use_to='HEAD' AND SB_NO <>'1' AND SB_NO<>''";
            DataSet product = Query(sql);
            fun.initDropDownList(DDL_OP, product.Tables[0], "op", "op");
            this.DDL_OP.Items.Insert(0, new ListItem("全部", ""));
            this.DDL_OP.SelectedValue = location;

            string sql_loc = "select  DISTINCT LOCATION  from Data_Setting  where use_to='HEAD' AND SB_NO <>'1' AND SB_NO<>'' and op='"+location+"'";
            DataSet ds_loc = Query(sql_loc);
            fun.initDropDownList(DDL_loc, ds_loc.Tables[0], "LOCATION", "LOCATION");
            this.DDL_loc.Items.Insert(0, new ListItem("全部", ""));
            DDL_loc.SelectedValue = m_sorder_id;

            DDL_Line.SelectedValue = "BMW";

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year);
            int chaYear = intYear - 2018 + 4;
            //int chaYear = 4;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            Yearlist[0] = "近12日";
            Yearlist[1] = "近12周";
            Yearlist[2] = "近12月";
            //Yearlist[3] = Year;
            for (int i = 3; i < chaYear; i++)
            {
                Yearlist[i] = (intYear - i + 3).ToString();
            }
            txt_year.DataSource = Yearlist;
            txt_year.DataBind();
            txt_year.SelectedValue = Yearlist[0].ToString();

            GetDetail();
            LinkBtn.Style.Add("display", "none");
        }
    }
   
    protected void  Button1_Click(object sender, EventArgs e)
    {
        GetDetail();

    }

    [System.Web.Services.WebMethod()]
    public static string GetLocByOP(string P1)
    {
        string result = "";//domain='"+domain+"' or ''=''
        var sql = string.Format(" select DISTINCT LOCATION AS value,LOCATION  as text from Data_Setting  where use_to='HEAD' AND SB_NO <>'1' AND SB_NO<>'' AND( op='{0}' or ''='{0}')", P1);
        var value = Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }


    protected void GetDetail()
    {
        ChartA.Series.Clear();
        ChartB.Series.Clear();
        ChartC.Series.Clear();

        GridView1.DataSource = null;
        GridView1.DataBind();

        gvdetail.DataSource = null;
        gvdetail.DataBind();
        DataSet ds = Query("exec BMW_Product_Query_BySbName  '" + txt_year.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + DDL_Line.SelectedValue + "','" + DDL_OP.SelectedValue + "','" + DDL_loc.SelectedValue + "','" + txt_emp.Text + "','" + DDL_workshop.SelectedValue + "','" + DDL_Shift.SelectedValue + "','" + sbno + "'");

        if (ds.Tables[0].Rows.Count > 0)
        { 
           // Pgi.Auto.Control.SetGrid(this.gv_month, ds.Tables[0], 100);      
            //this.gv_month.Columns[1].Visible = false;
            gvdetail.DataSource = ds.Tables[0];
            gvdetail.DataBind();
            //this.gvdetail.Columns[0].HeaderText = " ";
            DataView dw = new DataView();
            dw.Table = ds.Tables[0];
            dw.RowFilter = "element='生产数量'";
            CreateChart_product(dw.ToTable());


            DataView dw_fp = new DataView();
            dw_fp.Table = ds.Tables[0];
            dw_fp.RowFilter = "element='废品率%'";
            CreateChart_fp(dw_fp.ToTable());

            DataView dw_xl = new DataView();
            dw_xl.Table = ds.Tables[0];
            dw_xl.RowFilter = "element in ('生产效率%','OEE')";
            CreateChart_XL(dw_xl.ToTable());
            if (ds.Tables[0].Rows.Count > 0)
            {
                setGridLink_gv_dl();
            }
        }

    }
    private void CreateChart_product(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;

        for (int i = 0; i < 1; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][0].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    private void CreateChart_fp(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;

        for (int i = 0; i < 1; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][0].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartB.Series.AddRange(list.ToArray());
        this.ChartB.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }

    private void CreateChart_XL(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;

        for (int i = 0; i <= 1; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][0].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartC.Series.AddRange(list.ToArray());
        this.ChartC.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
    {
        Series series = new Series(caption, viewType);

        for (int i = 1; i < dt.Columns.Count - 2; i++)
        {
            int length = dt.Columns[i].ColumnName.IndexOf("<");
            string argument = length > 0 ? dt.Columns[i].ColumnName.Substring(0, length) : dt.Columns[i].ColumnName;
            //string argument = dt.Columns[i].ColumnName.Replace("<BR>","");//参数名称 
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
    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["ConnectionStringProductionData"];
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
    protected void gvdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TableCellCollection cells = e.Row.Cells;
        foreach (TableCell cell in cells)
        {
            cell.Text = Server.HtmlDecode(cell.Text);
        }
    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gvdetail.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";

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
    public void setGridLink_gv_dl()
    {
        GridViewRow dr = gvdetail.HeaderRow;

        for (int i = 1; i < 2; i++)
        {
            GridViewRow row = (GridViewRow)gvdetail.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 3; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn";
                lbtn.Text = gvdetail.Rows[1].Cells[j].Text;
                // lbtn.Text = GridViewYear.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "DL");

                int length = dr.Cells[j].Text.IndexOf("<");
                string DL = length > 0 ? dr.Cells[j].Text.Substring(0, length) : dr.Cells[j].Text;
                //string DL = dr.Cells[j].Text;
                lbtn.Attributes.Add("DL", DL);
                gvdetail.Rows[i].Cells[j].Controls.Add(lbtn);
                gvdetail.Rows[i].Cells[j].Attributes.Add("allowClick", "true");
            }
        }

    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        setGridLink_gv_dl();
        string dl = txtDL.Text;
        //txtDL.Visible = false;


        DataSet ds = Query("exec BMW_Product_DetailQuery_BySbName  '" + txt_year.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + DDL_Line.SelectedValue + "','" + DDL_OP.SelectedValue + "','" + DDL_loc.SelectedValue + "','" + txt_emp.Text + "','" + DDL_workshop.SelectedValue + "','" + DDL_Shift.SelectedValue + "','" + dl + "','"+sbno+"'");
        Getsum(ds.Tables[0]);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        txtDL.Style.Add("display","none");
       
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[11].Text = this.sc_cs.ToString()=="0" ?"":this.sc_cs.ToString();
            e.Row.Cells[12].Text = this.sc_sl.ToString() == "0" ? "" : this.sc_sl.ToString();
            e.Row.Cells[13].Text = this.hg_sl.ToString()== "0" ? "": this.hg_sl.ToString();
            e.Row.Cells[14].Text = this.fp_sl.ToString() == "0" ? "" : this.fp_sl.ToString();

        }
    }
    private void Getsum(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(ldt.Rows[i]["sc_cs"].ToString()))
            {
                this.sc_cs += Convert.ToDecimal(ldt.Rows[i]["sc_cs"].ToString());
            }

            if (!string.IsNullOrEmpty(ldt.Rows[i]["product_sl"].ToString()))
            {
                this.sc_sl += Convert.ToDecimal(ldt.Rows[i]["product_sl"].ToString());
            }
            if (!string.IsNullOrEmpty(ldt.Rows[i]["qty_hg"].ToString()))
            {
                this.hg_sl += Convert.ToDecimal(ldt.Rows[i]["qty_hg"].ToString());
            }
            if (!string.IsNullOrEmpty(ldt.Rows[i]["qty_fp"].ToString()))
            {
                this.fp_sl += Convert.ToDecimal(ldt.Rows[i]["qty_fp"].ToString());
            }

        }
    }
}