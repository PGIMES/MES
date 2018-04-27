using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class GongCheng_DJ_analyse_byproduct : System.Web.UI.Page
{
    int tcl = 0;
    decimal zbzcb = 0;
    decimal ztxcb = 0;
    decimal zsjcb = 0;
    decimal diff = 0;
    decimal bzly = 0;
    int sjly = 0;
    decimal dwbzcb = 0;
    decimal dwtxcb = 0;
    decimal dwsjcb = 0;
    decimal diff2 = 0;

    decimal gv1_tcsl = 0;
    decimal gv1_zbzcb = 0;
    decimal gv1_ztxcb = 0;
    decimal gv1_zsjcb = 0;
    decimal diff_sj_bz = 0;
    decimal cyl_tx_bz = 0;
    decimal cyl_sj_bz = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            string sqlstr = "select distinct rtrim(year)year from MES_DJ_JZ";
            DataSet dsyear = Query(sqlstr);
            fun.initDropDownList(txt_syear, dsyear.Tables[0], "year", "year");
            fun.initDropDownList(txt_eyear, dsyear.Tables[0], "year", "year");
            //初始化月份
            txt_smnth.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i < 13; i++)
            {
                txt_smnth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                txt_emnth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            txt_smnth.SelectedValue = "1";
            txt_emnth.SelectedValue = DateTime.Now.Month.ToString();
            txt_eyear.SelectedValue = DateTime.Now.ToString("yyyy");
            txt_syear.SelectedValue = "2017";
            //产品工程师
            string sqlgcs = "select distinct substring(product_user,charindex('-',product_user)+1,len(product_user))product_user from track where product_user<>''";
            DataSet dsgcs = Query(sqlgcs);
            fun.initDropDownList(txt_gcs, dsgcs.Tables[0], "product_user", "product_user");
            this.txt_gcs.Items.Insert(0, new ListItem("ALL", ""));
            //项目状态
            string sqlzt = "select * from  base_code   where code_type='TRACK_STATUS'  order by req_no";
            DataSet dszt = Query(sqlzt);
            fun.initDropDownList(status, dszt.Tables[0], "code_name", "code_name");
            this.status.Items.Insert(0, new ListItem("ALL", ""));


            string sql = "select distinct substring(pl_desc,dbo.fn_find('-',pl_desc,'1')+1,1) as material,substring(pl_desc,dbo.fn_find('-',pl_desc,'1')+1,len(pl_desc)-dbo.fn_find('-',pl_desc,'1')+1) as material_id from [qad].dbo.qad_pl_mstr   where (pl_prod_line like '3%')";
            DataSet product = DbHelperSQL.Query(sql);
            //control.Dropdownlist_Bind(ddl_productid, product.Tables[0], "material_id", "material", 1);
            fun.initDropDownList(txt_type, product.Tables[0], "material_id", "material_id");
            this.txt_type.Items.Insert(0, new ListItem("", ""));

            //
            string sqqz = "select distinct DJ_Group from dbo.MES_DJ_XMGroup ";
            DataSet dsqz = DbHelperSQL.Query(sqqz);
            fun.initDropDownList(txt_cpz, dsqz.Tables[0], "DJ_Group", "DJ_Group");
            this.txt_cpz.Items.Insert(0, new ListItem("", ""));

            Setddl_p_status();
            //初始化月份
            Init();
        }
    }

    protected void Init()
    {

        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','','','" + txt_zuzhi.SelectedValue + "'");
            ViewState["tblYear"] = ds.Tables[0];
            ViewState["tblGrid"] = ds.Tables[1];
            
            if (txt_lb.SelectedValue == "产品")
            {
                GridView4.Visible = false;
                GridView1.Visible = true;
                bindChartYear(ds.Tables[0]);
                GridView1.PageSize = int.Parse(txt_bs.SelectedValue);
                Getsum_gv1(ds.Tables[1]);
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
            }
            else
            {
                GridView1.Visible = false;
                GridView4.Visible = true;
                bindChartYear_cpz(ds.Tables[0]);
                GridView4.PageSize = int.Parse(txt_bs.SelectedValue);
                Getsum_gv1(ds.Tables[1]);
                GridView4.DataSource = ds.Tables[1];
                GridView4.DataBind();
            }
           
            zbz.Style.Add("display", "none");
            dwbz.Style.Add("display", "none");
            qs.Style.Add("display", "none");
            xmh.Style.Add("display", "none");
            
        
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


    protected void Button2_Click(object sender, EventArgs e)
    {
        Init();
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


    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("background-color", "lightyellow");
            e.Row.Cells[3].Text = "合计";
            e.Row.Cells[4].Text = this.gv1_tcsl.ToString("N0");//投产量
            e.Row.Cells[5].Text = this.gv1_zbzcb.ToString("N0");//总标准成本
            e.Row.Cells[6].Text = this.gv1_ztxcb.ToString("N0");//总弹性标准成本
            e.Row.Cells[7].Text = this.gv1_zsjcb.ToString("N0");//总实际成本
            e.Row.Cells[8].Text = this.diff_sj_bz.ToString("N0");//实际vs标准
            e.Row.Cells[14].Text = this.cyl_tx_bz.ToString("P1");//差异率_弹性VS标准
            e.Row.Cells[16].Text = this.cyl_sj_bz.ToString("P1");//差异率_实际VS标准
            //e.Row.Cells[13].Text = this.dwbzcb.ToString("f3");
            //e.Row.Cells[14].Text = this.dwtxcb.ToString("f3");
            //e.Row.Cells[15].Text = this.dwsjcb.ToString("f3");
            //e.Row.Cells[17].Text = this.diff2.ToString("f3");


        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        DataTable dt = (DataTable)ViewState["tblYear"];
        DataTable dtGrid = (DataTable)ViewState["tblGrid"];

        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string part = this.GridView1.Rows[lnindex].Cells[1].Text.ToString();
        this.GridView1.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;

        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','" + part + "','true','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','','','" + txt_zuzhi.SelectedValue + "'");

        bindChartByProduct(ds.Tables[3]);
        ViewState["Chartcb"]=ds.Tables[3];
        ViewState["tblzcb"] = ds.Tables[2];
        Getsum_Detail(ds.Tables[2]);
        Getsum_gv1(ds.Tables[1]);
        GridView1.DataSource = ds.Tables[1];
        GridView1.DataBind();
        GridView3.DataSource = ds.Tables[2];
        GridView3.DataBind();
        GridView5.DataSource = null;
        GridView5.DataBind();
        bindChartYear((DataTable)ViewState["tblYear"]);
        Label1.Text = part + " 总标准成本";
        Label2.Text = part + " 单位标准成本";
        zbz.Style.Add("display", "block");
        dwbz.Style.Add("display", "block");
        qs.Style.Add("display", "none");
        xmh.Style.Add("display", "block");
    }

    public void bindChartYear(DataTable tbl)
    {   //批次
        Chartzcb.DataSource = tbl;
        Chartzcb.Series["总标准成本"].XValueMember = "xmh";
        Chartzcb.Series["总标准成本"].YValueMembers = "zbzcb";
        Chartzcb.Series["总弹性成本"].XValueMember = "xmh";
        Chartzcb.Series["总弹性成本"].YValueMembers = "ztxcb";
        Chartzcb.Series["总实际成本"].XValueMember = "xmh";
        Chartzcb.Series["总实际成本"].YValueMembers = "zsjcb";

        Chartdwcb.DataSource = tbl;
        Chartdwcb.Series["单位成本"].XValueMember = "xmh";
        Chartdwcb.Series["单位成本"].YValueMembers = "dwbzcb";
        Chartdwcb.Series["弹性单位成本"].XValueMember = "xmh";
        Chartdwcb.Series["弹性单位成本"].YValueMembers = "dwtxcb";
        Chartdwcb.Series["单位实际成本"].XValueMember = "xmh";
        Chartdwcb.Series["单位实际成本"].YValueMembers = "dwsjcb";
    }



    public void bindChartByProduct(DataTable tbl)
    {   //批次
        ChartByProduct_zcb.DataSource = tbl;
        ChartByProduct_zcb.Series["总标准成本"].XValueMember = "part";
        ChartByProduct_zcb.Series["总标准成本"].YValueMembers = "zbzcb";
        ChartByProduct_zcb.Series["总弹性成本"].XValueMember = "part";
        ChartByProduct_zcb.Series["总弹性成本"].YValueMembers = "ztxcb";
        ChartByProduct_zcb.Series["总实际成本"].XValueMember = "part";
        ChartByProduct_zcb.Series["总实际成本"].YValueMembers = "zsjcb";

        ChartByProduct_dwcb.DataSource = tbl;
        ChartByProduct_dwcb.Series["单位成本"].XValueMember = "part";
        ChartByProduct_dwcb.Series["单位成本"].YValueMembers = "dwbzcb";
        ChartByProduct_dwcb.Series["弹性单位成本"].XValueMember = "part";
        ChartByProduct_dwcb.Series["弹性单位成本"].YValueMembers = "dwtxcb";
        ChartByProduct_dwcb.Series["单位实际成本"].XValueMember = "part";
        ChartByProduct_dwcb.Series["单位实际成本"].YValueMembers = "dwsjcb";
    }

    public void bindChartYear_cpz(DataTable tbl)
    {   //批次
        Chartzcb.DataSource = tbl;
        Chartzcb.Series["总标准成本"].XValueMember = "dj_group";
        Chartzcb.Series["总标准成本"].YValueMembers = "zbzcb";
        Chartzcb.Series["总弹性成本"].XValueMember = "dj_group";
        Chartzcb.Series["总弹性成本"].YValueMembers = "ztxcb";
        Chartzcb.Series["总实际成本"].XValueMember = "dj_group";
        Chartzcb.Series["总实际成本"].YValueMembers = "zsjcb";

        Chartdwcb.DataSource = tbl;
        Chartdwcb.Series["单位成本"].XValueMember = "dj_group";
        Chartdwcb.Series["单位成本"].YValueMembers = "dwbzcb";
        Chartdwcb.Series["弹性单位成本"].XValueMember = "dj_group";
        Chartdwcb.Series["弹性单位成本"].YValueMembers = "dwtxcb";
        Chartdwcb.Series["单位实际成本"].XValueMember = "dj_group";
        Chartdwcb.Series["单位实际成本"].YValueMembers = "dwsjcb";
    }
    public void bindChartByDJ(DataTable tbl)
    {   //批次
        ChartByDJ.DataSource = tbl;

        ChartByDJ.Series["单位标准成本"].XValueMember = "mnth";
        ChartByDJ.Series["单位标准成本"].YValueMembers = "dwbzcb";

        ChartByDJ.Series["弹性单位标准成本"].XValueMember = "mnth";
        ChartByDJ.Series["弹性单位标准成本"].YValueMembers = "dwtxcb";

        ChartByDJ.Series["单位实际成本"].XValueMember = "mnth";
        ChartByDJ.Series["单位实际成本"].YValueMembers = "dwsjcb";

        ChartByDJ.DataBind();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            this.ChartByDJ.Series["单位标准成本"].Points[i].ToolTip = tbl.Rows[i]["dwbzcb"].ToString();
            this.ChartByDJ.Series["弹性单位标准成本"].Points[i].ToolTip = tbl.Rows[i]["dwtxcb"].ToString();
            this.ChartByDJ.Series["单位实际成本"].Points[i].ToolTip = tbl.Rows[i]["dwsjcb"].ToString();
        }

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string part = this.GridView1.Rows[lnindex].Cells[1].Text.ToString();
        this.GridView1.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        //DataSet ds = Query("exec DJ_analyse_Report_Product '" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','','" + txt_bs.SelectedValue + "','" + part + "','','true','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "'");
        //DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','" + part + "','','true','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "'");
        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','" + part + "','','true','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','','','" + txt_zuzhi.SelectedValue + "'");

        bindChartByDJ(ds.Tables[2]);
        Getsum_gv1(ds.Tables[1]);
        GridView1.DataSource = ds.Tables[1];
        GridView1.DataBind();
        bindChartYear((DataTable)ViewState["tblYear"]);
        GridView2.DataSource = ds.Tables[2];
        GridView2.DataBind();
        zbz.Style.Add("display", "none");
        dwbz.Style.Add("display", "none");
        qs.Style.Add("display", "block");
        xmh.Style.Add("display", "none");
        Label3.Text = part + " 趋势图";
    }


    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        Init();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridView3.PageIndex * this.GridView3.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
   {
        ViewState["sortexpression"] = e.SortExpression;
        // ViewState["id"] = "id";

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
       // DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','0','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "'");
        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','','','" + txt_zuzhi.SelectedValue + "'");

        DataTable dt = ds.Tables[1];

        // bindChartYear(ds.Tables[1]);

        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"] + " " + ViewState["sortdirection"].ToString();

        }
        Getsum_gv1(ds.Tables[1]);
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
        DataTable NewTable = dv.ToTable().Clone();
        DataRow[] rows = dv.ToTable().Select("1=1");
        int tops = dv.ToTable().Rows.Count;
        if (tops > 10) tops = 10;
        for (int i = 0; i < tops; i++)
        {

            NewTable.ImportRow((DataRow)rows[i]);

        }
        bindChartYear(NewTable);
        ViewState["tblYear"] = NewTable;
        zbz.Style.Add("display", "none");
        dwbz.Style.Add("display", "none");
        qs.Style.Add("display", "none");
        xmh.Style.Add("display", "none");

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView3.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }

        int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        //string xmh = GridView3.Rows[lnindex].Cells[1].Text.ToString();
        string xmh = ((LinkButton)this.GridView3.Rows[lnindex].FindControl("LinkButton4")).Text.TrimEnd();
        string part = ((LinkButton)sender).Text;
        string comp = ddl_comp.SelectedValue.TrimEnd();
        int startmnth = int.Parse(txt_smnth.SelectedValue);
        int endmnth = int.Parse(txt_emnth.SelectedValue);
        int startyear = int.Parse(txt_syear.SelectedValue);
        int endyear = int.Parse(txt_eyear.SelectedValue);
        string year = txt_syear.SelectedValue;

        string URL = "Base.aspx?xmh=" + xmh + " &part=" + part + "&comp=" + comp + " &startmnth=" + startmnth + "&endmnth=" + endmnth + "&startyear=" + startyear + "&endyear=" + endyear + "&year=" + year + "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.open('" + URL + "','_blank','height=800,width=1000,scrollbars=no')", true);
       // bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        bindChartByProduct((DataTable)ViewState["Chartcb"]);
        Getsum_Detail((DataTable)ViewState["tblzcb"]);
        GridView3.DataSource = ((DataTable)ViewState["tblzcb"]);
        GridView3.DataBind();
        Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView1.DataSource = (DataTable)ViewState["tblGrid"];
        GridView1.DataBind();
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
    }


    private void Getsum_gv1(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["tcsl"].ToString() != "")
            {
                this.gv1_tcsl += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["tcsl"].ToString()), 0));
            }

            if (ldt.Rows[i]["zbzcb"].ToString() != "")
            {
                this.gv1_zbzcb += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["zbzcb"].ToString()), 0));
            }
            if (ldt.Rows[i]["ztxcb"].ToString() != "")
            {
                this.gv1_ztxcb += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["ztxcb"].ToString()), 0));
            }
            if (ldt.Rows[i]["zsjcb"].ToString() != "")
            {
                this.gv1_zsjcb += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["zsjcb"].ToString()), 0));
            }
            if (ldt.Rows[i]["diff1"].ToString() != "")
            {
                this.diff_sj_bz += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["diff1"].ToString()), 0));
            }
           
        }
        cyl_tx_bz = gv1_zbzcb==0?0:(gv1_ztxcb - gv1_zbzcb) / gv1_zbzcb;
        cyl_sj_bz = gv1_zbzcb==0?0:(gv1_zsjcb - gv1_zbzcb) / gv1_zbzcb;
    }

   


    private void Getsum_Detail(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["tcsl"].ToString() != "")
            {
                this.tcl += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["tcsl"].ToString()), 0));
            }

            if (ldt.Rows[i]["zbzcb"].ToString() != "")
            {
                this.zbzcb +=Convert.ToDecimal(ldt.Rows[i]["zbzcb"].ToString());
            }
            if (ldt.Rows[i]["ztxcb"].ToString() != "")
            {
                this.ztxcb += Convert.ToDecimal(ldt.Rows[i]["ztxcb"].ToString());
            }
            if (ldt.Rows[i]["zsjcb"].ToString() != "")
            {
                this.zsjcb += Convert.ToDecimal(ldt.Rows[i]["zsjcb"].ToString());
            }
            if (ldt.Rows[i]["diff1"].ToString() != "")
            {
                this.diff +=Convert.ToDecimal(ldt.Rows[i]["diff1"].ToString());
            }
            if (ldt.Rows[i]["bzlys"].ToString() != "")
            {
                this.bzly += Convert.ToDecimal(ldt.Rows[i]["bzlys"].ToString());
            }
            if (ldt.Rows[i]["sjlys"].ToString() != "")
            {
                this.sjly += Convert.ToInt32(Math.Round(Convert.ToDecimal(ldt.Rows[i]["sjlys"].ToString()), 0));
            }
            if (ldt.Rows[i]["dwbzcb"].ToString() != "")
            {
                this.dwbzcb += Convert.ToDecimal(ldt.Rows[i]["dwbzcb"].ToString());
            }
            if (ldt.Rows[i]["dwtxcb"].ToString() != "")
            {
                this.dwtxcb += Convert.ToDecimal(ldt.Rows[i]["dwtxcb"].ToString());
            }
            if (ldt.Rows[i]["dwsjcb"].ToString() != "")
            {
                this.dwsjcb += Convert.ToDecimal(ldt.Rows[i]["dwsjcb"].ToString());
            }
            if (ldt.Rows[i]["diff2"].ToString() != "")
            {
                this.diff2 += Convert.ToDecimal(ldt.Rows[i]["diff2"].ToString());
            }
        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("background-color", "lightyellow");
            e.Row.Cells[5].Text = "合计";
            e.Row.Cells[6].Text = this.tcl.ToString("N0");
            e.Row.Cells[7].Text = this.zbzcb.ToString("N0");
            e.Row.Cells[8].Text = this.ztxcb.ToString("N0");
            e.Row.Cells[9].Text = this.zsjcb.ToString("N0");
            e.Row.Cells[10].Text = this.diff.ToString("N0");
            e.Row.Cells[11].Text = this.bzly.ToString("f2");
            e.Row.Cells[12].Text = this.sjly.ToString("N0");
            e.Row.Cells[13].Text = this.dwbzcb.ToString("f3");
            e.Row.Cells[14].Text = this.dwtxcb.ToString("f3");
            e.Row.Cells[15].Text = this.dwsjcb.ToString("f3");
            e.Row.Cells[17].Text = this.diff2.ToString("f3");


        }
    }
    protected void GridView5_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("background-color", "lightyellow");
            e.Row.Cells[3].Text = "合计";
            e.Row.Cells[4].Text = this.tcl.ToString("N0");
            e.Row.Cells[5].Text = this.zbzcb.ToString("N0");
            e.Row.Cells[6].Text = this.ztxcb.ToString("N0");
            e.Row.Cells[7].Text = this.zsjcb.ToString("N0");
            e.Row.Cells[8].Text = this.diff.ToString("N0");
            e.Row.Cells[9].Text = this.bzly.ToString("f2");
            e.Row.Cells[10].Text = this.sjly.ToString("N0");
            //e.Row.Cells[11].Text = this.dwbzcb.ToString("f3");
            //e.Row.Cells[12].Text = this.dwtxcb.ToString("f3");
            //e.Row.Cells[13].Text = this.dwsjcb.ToString("f3");
      


        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView3.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        string xmh = ((LinkButton)this.GridView3.Rows[lnindex].FindControl("LinkButton4")).Text.TrimEnd(); //GridView3.Rows[lnindex].Cells[1].Text.ToString().TrimEnd();
        string part =((LinkButton)this.GridView3.Rows[lnindex].FindControl("LinkButton1")).Text.TrimEnd();
        string comp = ddl_comp.SelectedValue.TrimEnd();
        int startmnth = int.Parse(txt_smnth.SelectedValue);
        int endmnth = int.Parse(txt_emnth.SelectedValue);
        int startyear = int.Parse(txt_syear.SelectedValue);
        int endyear = int.Parse(txt_eyear.SelectedValue);
        string year = txt_syear.SelectedValue;

        string URL = "Product_LY.aspx?xmh=" + xmh + " &part=" + part + " &comp=" + comp + " &startmnth=" + startmnth + "&endmnth=" + endmnth + "&startyear=" + startyear + "&endyear=" + endyear + "&year=" + year + "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.open('" + URL + "','_blank','height=800,width=1000,scrollbars=yes')", true);
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        bindChartByProduct((DataTable)ViewState["Chartcb"]);
        Getsum_Detail((DataTable)ViewState["tblzcb"]);
        GridView3.DataSource = ((DataTable)ViewState["tblzcb"]);
        GridView3.DataBind();
        Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView1.DataSource = (DataTable)ViewState["tblGrid"];
        GridView1.DataBind();
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
    }
    protected void qz_ljcb_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView4.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        DataTable dt = (DataTable)ViewState["tblYear"];
        DataTable dtGrid = (DataTable)ViewState["tblGrid"];

        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string qzm = this.GridView4.Rows[lnindex].Cells[1].Text.ToString();
        this.GridView4.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','true','','" + qzm + "','" + txt_zuzhi.SelectedValue + "'");

        bindChartByProduct(ds.Tables[3]);
        ViewState["tblzcb"] = ds.Tables[2];
        ViewState["Chartcb"] = ds.Tables[3];
        Getsum_Detail(ds.Tables[2]);
        Getsum_gv1(ds.Tables[1]);
        GridView4.DataSource = ds.Tables[1];
        GridView4.DataBind();
        GridView3.DataSource = null;
        GridView3.DataBind();
        GridView5.DataSource = ds.Tables[2];
        GridView5.DataBind();
        bindChartYear_cpz((DataTable)ViewState["tblYear"]);
        Label1.Text = qzm + " 总标准成本";
        Label2.Text = qzm + " 单位标准成本";
        zbz.Style.Add("display", "block");
        dwbz.Style.Add("display", "block");
        qs.Style.Add("display", "none");
        xmh.Style.Add("display", "block");
       
    }
    protected void qz_cbqs_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView4.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string qzm = this.GridView4.Rows[lnindex].Cells[1].Text.ToString();
        this.GridView4.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        //DataSet ds = Query("exec DJ_analyse_Report_Product '" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','','" + txt_bs.SelectedValue + "','" + part + "','','true','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "'");
       // DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','true','" + qzm + "'");
        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" +txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','true','" + qzm + "','" + txt_zuzhi.SelectedValue + "'");

        bindChartByDJ(ds.Tables[2]);
        bindChartYear_cpz((DataTable)ViewState["tblYear"]);
        GridView2.DataSource = ds.Tables[2];
        GridView2.DataBind();
        Getsum_gv1(ds.Tables[1]);
        GridView4.DataSource = ds.Tables[1];
        GridView4.DataBind();
        zbz.Style.Add("display", "none");
        dwbz.Style.Add("display", "none");
        qs.Style.Add("display", "block");
        xmh.Style.Add("display", "none");
        Label3.Text = qzm + " 趋势图";
    }
    protected void GridView4_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;
        // ViewState["id"] = "id";

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
        // DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','0','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + status.SelectedValue + "','" + txt_gcs.SelectedValue + "'");
        DataSet ds = Query("exec DJ_analyse_Report_Product_Query_temp'" + ddl_comp.SelectedValue + "','" + txt_syear.SelectedValue.Trim() + "','" + txt_smnth.SelectedValue + "','" + txt_eyear.SelectedValue + "','" + txt_emnth.SelectedValue + "','" + txt_part.Value + "','" + txt_bs.SelectedValue + "','','','','" + txt_xmh.Value + "','" + txt_type.SelectedValue + "','" + txt_djlx.SelectedValue + "','" + txt_cpdl.SelectedValue + "','" + txt_product_status.Text + "','" + txt_gcs.SelectedValue + "','" + txt_lb.SelectedValue + "','" + txt_cpz.SelectedValue + "','','','','" + txt_zuzhi.SelectedValue + "'");

        DataTable dt = ds.Tables[1];

        // bindChartYear(ds.Tables[1]);

        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"] + " " + ViewState["sortdirection"].ToString();

        }

        Getsum_gv1(ds.Tables[1]);
        this.GridView4.DataSource = dv;
        this.GridView4.DataBind();
        DataTable NewTable = dv.ToTable().Clone();
        DataRow[] rows = dv.ToTable().Select("1=1");
        int tops = dv.ToTable().Rows.Count;
        if (tops > 10) tops = 10;
        for (int i = 0; i < tops; i++)
        {

            NewTable.ImportRow((DataRow)rows[i]);

        }
        bindChartYear_cpz(NewTable);
        ViewState["tblYear"] = NewTable;
        zbz.Style.Add("display", "none");
        dwbz.Style.Add("display", "none");
        qs.Style.Add("display", "none");
        xmh.Style.Add("display", "none");
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridView5.PageIndex * this.GridView5.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }
        }
    }
    protected void lbtn_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView3.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }

        int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        this.GridView5.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        string dj_group = GridView5.Rows[lnindex].Cells[1].Text.ToString();
        string part = ((LinkButton)sender).Text;
        string comp = ddl_comp.SelectedValue.TrimEnd();
        int startmnth = int.Parse(txt_smnth.SelectedValue);
        int endmnth = int.Parse(txt_emnth.SelectedValue);
        int startyear = int.Parse(txt_syear.SelectedValue);
        int endyear = int.Parse(txt_eyear.SelectedValue);
        string year = txt_syear.SelectedValue;
        string xmh = "0";


        string URL = "Base.aspx?xmh=" + xmh + " &part=" + part + "&comp=" + comp + " &startmnth=" + startmnth + "&endmnth=" + endmnth + "&startyear=" + startyear + "&endyear=" + endyear + "&year=" + year + "&dj_group=" + dj_group + "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.open('" + URL + "','_blank','height=800,width=1000,scrollbars=no')", true);
        // bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        bindChartByProduct((DataTable)ViewState["Chartcb"]);
        Getsum_Detail((DataTable)ViewState["tblzcb"]);
        GridView5.DataSource = ((DataTable)ViewState["tblzcb"]);
        GridView5.DataBind();
         Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView4.DataSource = (DataTable)ViewState["tblGrid"];
        GridView4.DataBind();
        this.GridView5.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
    }
    protected void lbtn2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView5.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        this.GridView5.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        string dj_group = GridView5.Rows[lnindex].Cells[1].Text.ToString().TrimEnd();
        string part = ((LinkButton)this.GridView5.Rows[lnindex].FindControl("lbtn")).Text.TrimEnd();
        string comp = ddl_comp.SelectedValue.TrimEnd();
        int startmnth = int.Parse(txt_smnth.SelectedValue);
        int endmnth = int.Parse(txt_emnth.SelectedValue);
        int startyear = int.Parse(txt_syear.SelectedValue);
        int endyear = int.Parse(txt_eyear.SelectedValue);
        string year = txt_syear.SelectedValue;
        string xmh = "0";

        string URL = "Product_LY.aspx?xmh=" + xmh + " &part=" + part + " &comp=" + comp + " &startmnth=" + startmnth + "&endmnth=" + endmnth + "&startyear=" + startyear + "&endyear=" + endyear + "&year=" + year + "&dj_group="+@dj_group+"";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.open('" + URL + "','_blank','height=800,width=1000,scrollbars=yes')", true);
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        bindChartByProduct((DataTable)ViewState["Chartcb"]);
        Getsum_Detail((DataTable)ViewState["tblzcb"]);
        GridView5.DataSource = ((DataTable)ViewState["tblzcb"]);
        GridView5.DataBind();
        Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView4.DataSource = (DataTable)ViewState["tblGrid"];
        GridView4.DataBind();
        this.GridView5.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView3.Rows)
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
        int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
        string xmh = ((LinkButton)this.GridView3.Rows[lnindex].FindControl("LinkButton4")).Text.TrimEnd();
        string comp = ddl_comp.SelectedValue.TrimEnd();
        int startmnth = int.Parse(txt_smnth.SelectedValue);
        int endmnth = int.Parse(txt_emnth.SelectedValue);
        int startyear = int.Parse(txt_syear.SelectedValue);
        int endyear = int.Parse(txt_eyear.SelectedValue);
        string year = txt_syear.SelectedValue;

        string URL = "Product_Byxmh.aspx?xmh=" + xmh + "&comp=" + comp + " &startmnth=" + startmnth + "&endmnth=" + endmnth + "&startyear=" + startyear + "&endyear=" + endyear + "&year=" + year + "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.open('" + URL + "','_blank','height=800,width=1000,scrollbars=yes')", true);
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        bindChartByProduct((DataTable)ViewState["Chartcb"]);
        Getsum_Detail((DataTable)ViewState["tblzcb"]);
        GridView3.DataSource = ((DataTable)ViewState["tblzcb"]);
        GridView3.DataBind();
        Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView1.DataSource = (DataTable)ViewState["tblGrid"];
        GridView1.DataBind();
        this.GridView3.Rows[lnindex].BackColor = System.Drawing.Color.PaleTurquoise;
    }

    protected void txt_zuzhi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_zuzhi.SelectedValue != "")
        {
            DataSet ds = Query("exec usp_ByGroup_Select  '0','" + txt_zuzhi.SelectedValue + "'");
            DataTable dt = ds.Tables[0];
            txt_gcs.DataTextField = dt.Columns[1].ColumnName;
            txt_gcs.DataValueField = dt.Columns[1].ColumnName;
            this.txt_gcs.DataSource = dt;
            this.txt_gcs.DataBind();
            this.txt_gcs.Items.Insert(0, new ListItem("", ""));
        }
        if (txt_lb.SelectedValue == "产品")
        {
            bindChartYear((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
           
        }
        else
        {
            bindChartYear_cpz((DataTable)ViewState["tblYear"]);//ViewState["tblzcb"]
        }
        Getsum_gv1((DataTable)ViewState["tblGrid"]);
        GridView1.DataSource = (DataTable)ViewState["tblGrid"];
        GridView1.DataBind();
        zbz.Style.Add("display", "none");
        dwbz.Style.Add("display", "none");
        qs.Style.Add("display", "none");
        xmh.Style.Add("display", "none");
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Style.Add("background-color", "lightyellow");
            e.Row.Cells[1].Text = "合计";
            e.Row.Cells[2].Text = this.gv1_tcsl.ToString("N0");//投产量
            e.Row.Cells[3].Text = this.gv1_zbzcb.ToString("N0");//总标准成本
            e.Row.Cells[4].Text = this.gv1_ztxcb.ToString("N0");//总弹性标准成本
            e.Row.Cells[5].Text = this.gv1_zsjcb.ToString("N0");//总实际成本
            e.Row.Cells[6].Text = this.diff_sj_bz.ToString("N0");//实际vs标准
            e.Row.Cells[12].Text = this.cyl_tx_bz.ToString("P1");//差异率_弹性VS标准
            e.Row.Cells[14].Text = this.cyl_sj_bz.ToString("P1");//差异率_实际VS标准
            


        }
    }
}