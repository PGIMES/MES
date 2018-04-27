using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using DevExpress.Utils;
using DevExpress.XtraCharts;


public partial class Product_Chanpin_Fenxi_ByCPFZR : System.Web.UI.Page
{
    decimal ntotal_Q1 = 0;
    decimal ntotal_Q2 = 0;
    decimal ntotal_Q3 = 0;
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
      
        if (!IsPostBack)
        {
            //Setddl_dept();
            Setddl_p_leibie();
            bindType();
            bindCust();
            ShowMst();
            lbldetail.Text = "明细";
            GridView1.DataSource = null;
            GridView1.DataBind();
            LinkDtl.Style.Add("display", "none");
        }
    }
    public void Setddl_dept()
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"	select distinct dept_name as  status_id,dept_name as status from HRM_EMP_MES
                            where jobtitlename in ('产品工程师','压铸工程师','模具工程师','质量工程师','项目工程师','包装工程师','销售工程师','采购工程师','生产计划')
	                        union select distinct departmentname as  status_id,departmentname as status from HRM_EMP_MES
                            where jobtitlename ='产品工程师' ";
        DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];

        if (ship_from.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_dept, ship_from, "status_id", "status");
        }
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
    public void bindType()
    {
        string strSQL = @"select '开发中' as customer_name
                         union select '生产中'
                         union select '停产'
                         union select '项目取消'   ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectPStatus.DataTextField = dt.Columns[0].ColumnName;
        selectPStatus.DataValueField = dt.Columns[0].ColumnName;
        this.selectPStatus.DataSource = dt;
        this.selectPStatus.DataBind();
    }

    public void bindCust()
    {
        string strSQL = @"select distinct customer_name from [dbo].[form3_Sale_Product_MainTable]";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectCust.DataTextField = dt.Columns[0].ColumnName;
        selectCust.DataValueField = dt.Columns[0].ColumnName;
        this.selectCust.DataSource = dt;
        this.selectCust.DataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowMst();
        this.lbldetail.Text = "";
        GridView1.Visible = false;
        GridView2.Visible = false;
    }
    public void ShowMst()
    {
        ChartA.Series.Clear();
        DataSet ds_Mst;
        if (ddl_type.SelectedValue == "0")
        {
            ds_Mst = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_QUERY_1] '" + this.txt_product_status.Text.ToString() + "', '" + this.ddl_GCS.SelectedValue.ToString() + "','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "', '0'");
        }
        else
        {
            ds_Mst = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_QUERY_1] '" + this.txt_product_status.Text.ToString() + "', '','全部','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','"+this.ddl_type.SelectedValue+"'");

        }
        gv_CPFZR.DataSource = ds_Mst.Tables[0];
        gv_CPFZR.DataBind();
        CreateChart(ds_Mst.Tables[0]);
        string product_status = this.txtCustomer_name.Text;
        string gcs = this.ddl_GCS.SelectedItem.ToString();
        string type = ddl_type.SelectedItem.Text;
        this.lblMstC.Text = "按【" + type + "】统计: 产品状态：" + product_status + " ；组织： " + this.ddl_dept.SelectedValue;
        setGridLink_Mst();
    }

    private void CreateChart(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i < dt.Rows.Count - 1; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.StackedBar, dt, j,1));
            j++;
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }
    /// <summary>
    /// 根据数据创建一个图形展现
    /// </summary>
    /// <param name="caption">图形标题</param>
    /// <param name="viewType">图形类型</param>
    /// <param name="dt">数据DataTable</param>
    /// <param name="rowIndex">图形数据的行序号</param>
    /// <returns></returns>
    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex, int notshow_qty)
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
    protected void gv_mst_RowDataBound(object sender, GridViewRowEventArgs e)
    { //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "—";
                    }
            }
        }
    }

    /// <summary>
    /// 给报表添加链接
    /// </summary>
    public void setGridLink_Mst()
    {
        GridViewRow dr = gv_CPFZR.HeaderRow;
        for (int i = 0; i < gv_CPFZR.Rows.Count; i++)
        {
            GridViewRow row = (GridViewRow)gv_CPFZR.Rows[i];
            for (int j = 2; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM" + j.ToString();
                lbtn.Text = gv_CPFZR.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
                lbtn.Attributes.Add("name", "sale");
                string strName = dr.Cells[j].Text;//获取人名
                string strType = Server.HtmlEncode(row.Cells[1].Text); //获取类别
                lbtn.Attributes.Add("names", strName);
                lbtn.Attributes.Add("types", strType);
                gv_CPFZR.Rows[i].Cells[j].Controls.Add(lbtn);
                gv_CPFZR.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }

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
    public void QueryGridView2(DataSet ds)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        this.GetsumJiBen(ds.Tables[0]);
        this.GridView2.DataSource = ds.Tables[0];
        this.GridView2.DataBind();

        int[] cols = { 0, 1, 2, 15, 20, 3, 4, 5, 23, 24, 6, 7, 8, 9 };
        MergGridRow.MergeRow(GridView2, cols);
        int rowIndex = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[21].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[2].Controls.Add(link);
                rowIndex++;
            }
        }
    }

    protected DataSet GetGrid2()
    {
        DataSet ds_dtl;
        string type = txtType.Text=="合计"?"":txtType.Text;
        string name = txtName.Text;

        
        if (ddl_type.SelectedValue == "0")
        {

            ds_dtl = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_Query_DTL_1] '" + txtType.Text + "', '" + name + "','" + this.ddl_GCS.SelectedValue.ToString() + "','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_product_status.Text + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + ddl_type.SelectedValue + "','',''");
        }
        else if (ddl_type.SelectedValue == "1") //客户
        {
            name = name == "合计" ? "" : name;
            if ( name == "")
            {
                name = txtCustomer_name.Text;
            }

            ds_dtl = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_Query_DTL_1] '" + type + "', '','','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_product_status.Text + "','" + this.txt_p_leibie.Text.ToString() + "','" + name + "','" + ddl_type.SelectedValue + "','-1','-1'");

        }
        else if (ddl_type.SelectedValue == "2") //产品大类
        {
            name = name == "合计" ? "" : name;
            if (name=="")
            {
                name = txt_p_leibie.Text;
            }
            ds_dtl = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_Query_DTL_1] '" + type + "', '','','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_product_status.Text + "','" + name + "','" + this.txtCustomer_name.Text + "','" + ddl_type.SelectedValue + "','-1','-1'");

        }
        else if (ddl_type.SelectedValue == "3") //生产地点
        {
            name = name == "合计" ? "-1" : name;
            ds_dtl = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_Query_DTL_1] '" + type + "', '','','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_product_status.Text + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + ddl_type.SelectedValue + "','"+name+"','-1'");

        }
        else  //ship_from
        {
            name = name == "合计" ? "-1" : name;
            ds_dtl = DbHelperSQL.Query("exec [dbo].[form3_Sale_Fenxi_Query_DTL_1] '" + type + "', '','','" + this.ddl_dept.SelectedValue.ToString() + "','" + this.txt_product_status.Text + "','" + this.txt_p_leibie.Text.ToString() + "','" + this.txtCustomer_name.Text + "','" + ddl_type.SelectedValue + "','-1','" + name + "'");

        }
        return ds_dtl;
    }

    protected void LinkDtl_Click(object sender, EventArgs e)
    {
        string type = txtType.Text;
        string name = txtName.Text; //获取人名
        if (ddl_type.SelectedValue == "0")
        {
            GridView2.Visible = false;
            GridView1.Visible = true;
        }
        else
        {
            GridView2.Visible = true;
            GridView1.Visible = false;
        }
        this.lbldetail.Text = "【" + type + name +"】--明细";
        DataSet ds_dtl;
        ds_dtl = GetGrid2();
       
        if (ddl_type.SelectedValue == "0")
        {

            this.GridView1.DataSource = ds_dtl.Tables[0];
            GridView1.DataBind();
            int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 20 };
            MergGridRow.MergeRow(GridView1, cols);
            //给更新销量 添加图片和链接
            int rowIndex = 1;
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                if (GridView1.Rows[j].Cells[0].Visible == true)
                {
                    GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                    HyperLink link = new HyperLink();
                    link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[21].Text).Trim();
                    link.ImageUrl = this.GridView1.ResolveClientUrl("~/Images/fdj.gif");
                    link.Target = "_blank";
                    GridView1.Rows[j].Cells[20].Controls.Add(link);
                    rowIndex++;
                }
            }


            int Index = 1;
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                if (GridView1.Rows[j].Cells[0].Visible == true)
                {
                    GridView1.Rows[j].Cells[0].Text = Index.ToString();
                    HyperLink link = new HyperLink();
                    link.ID = "link" + rowIndex.ToString();
                    link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[21].Text).Trim();
                    link.Text = GridView1.Rows[j].Cells[3].Text;
                    link.Target = "_blank";
                    GridView1.Rows[j].Cells[3].Controls.Add(link);
                    Index++;
                }
            }
            setGridLink_Mst();
        }
        else
        {
            QueryGridView2(ds_dtl);
        }

    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[21].Style.Add("display", "none");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[21].Style.Add("display", "none");
            for (int i = 9; i <= 19; i++)
            {
                if (e.Row.Cells[i].Text.ToString() == "空白")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
        

    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedValue!= "0")
        {
            ddl_GCS.CssClass = "form-control input-s-sm ";
            ddl_GCS.Enabled =false ;
            ddl_dept.CssClass = "form-control input-s-sm ";
            ddl_dept.Enabled = false;
        }
        else
        {
            ddl_GCS.Enabled = true;
            ddl_dept.Enabled = true;
        }
        setGridLink_Mst();

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string type = txtType.Text;
        string name = txtName.Text; //获取人名
        GridView2.PageIndex = e.NewPageIndex;
        DataSet ds;
        ds = GetGrid2();
        QueryGridView2(ds);
    }

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        string type = txtType.Text;
        string name = txtName.Text; //获取人名
        DataSet ds;
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
        ds =GetGrid2();
        DataView dv = ds.Tables[0].DefaultView;

        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView2.DataSource = dv;
        this.GridView2.DataBind();
        this.GetsumJiBen(ds.Tables[0]);

        GridViewRow row = GridView2.FooterRow;
        row.Cells[12].Text = this.ntotal_Q1.ToString("N0");
        row.Cells[14].Text = this.ntotal_Q2.ToString("N0");

        int[] cols = { 0, 1, 2, 3, 20, 4, 5, 23, 6, 7, 8, 9, 15 };
        MergGridRow.MergeRow(GridView2, cols);
        int rowIndex = 1;
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            if (GridView2.Rows[j].Cells[0].Visible == true)
            {
                GridView2.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "product.aspx?requestid=" + Server.HtmlDecode(GridView2.Rows[j].Cells[21].Text).Trim();
                link.Text = GridView2.Rows[j].Cells[2].Text;
                link.Target = "_blank";
                GridView2.Rows[j].Cells[2].Controls.Add(link);
                rowIndex++;
            }
        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //序号列不排序        
        for (int i = 8; i < 25; i++)///‘
            try
            {
                LinkButton lb = (LinkButton)GridView2.HeaderRow.Cells[i].Controls[0];
                string txt = lb.Text;
                GridView2.HeaderRow.Cells[i].Text = txt;

            }
            catch (Exception ex)
            { }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            if ((e.Row.Cells[22].Text.ToString().Trim() == "停产" || e.Row.Cells[22].Text.ToString().Trim() == "项目取消"))
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


    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSQL = @"select *,1 from [dbo].[form3_Sale_Product_position]  where zuzhi='"+ddl_dept.SelectedValue+"'union select '"+ddl_dept.SelectedValue+"','全部',2   ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        ddl_GCS.DataTextField = dt.Columns[1].ColumnName;
        ddl_GCS.DataValueField = dt.Columns[1].ColumnName;
        this.ddl_GCS.DataSource = dt;
        this.ddl_GCS.DataBind();

        setGridLink_Mst();
    }
}