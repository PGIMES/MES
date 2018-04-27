using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class YangJianDelay_TJ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            BaseFun fun = new BaseFun();         
            //初始化年份    
            int year=Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for(int i=0;i<5;i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }         

            //初始化月份            
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem( i.ToString() , i.ToString()  ));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //部门
            
            string sql = "select distinct dept_name value,dept_name text from [dbo].[HRM_EMP_MES] where 1=1 and dept_name not like'%总经理%' ";
            DataSet ds = DbHelperSQL.Query(sql);
            selectDepart.DataSource = ds;
            selectDepart.DataTextField = "text";
            selectDepart.DataValueField = "value";
            selectDepart.DataBind();

            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
           
            QueryMonth(DateTime.Now.ToString("MM"));
            QueryMonthCS(DateTime.Now.ToString("MM"));
            //完成率
            //初始化完成率默认排序栏位及顺序
            ViewState["yearcol"] = "逾时未完成";
            ViewState["yearorder"] = "asc";
            ViewState["monthcol"] = "逾时未完成";
            ViewState["monthorder"] = "asc";

            QueryYearFinish(true);
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            QueryMonthFinish(DateTime.Now.ToString("MM"),true);

        }
       
      //封存  bindChartYearByRen((DataTable)ViewState["tblYearByRen"]);
    }
    //年Gridview
    protected void btnQuery_Click(object sender, EventArgs e)
    {   //Year
        QueryYear();
        //month
        QueryMonth(dropMonth.SelectedValue.PadLeft(2, '0'));
        QueryMonthCS(dropMonth.SelectedValue.PadLeft(2, '0'));
        lblMonth.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0')+"月";
        lblMonthCS.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') ;
        this.txtmonth.Text =  dropMonth.SelectedValue.PadLeft(2, '0');//dropYear.SelectedValue.PadLeft(2, '0') +

        //===完成率=======
        //Year
        QueryYearFinish(false);
        //month
        QueryMonthFinish(dropMonth.SelectedValue.PadLeft(2, '0'),false);
        lblMonthFinish.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        this.txtmonth.Text = dropMonth.SelectedValue.PadLeft(2, '0');//dropYear.SelectedValue.PadLeft(2, '0') +


    }
    public void QueryYear()
    {                                 
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ '" + this.dropYear.SelectedValue + "','','" + dropFac.SelectedValue + "'");
  
        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue+"年";

        ViewState["tblYear"] = ds.Tables[1];       
        bindChartYear(ds.Tables[1]);
        //统计人员超时次数
        DataSet dsCS = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ_Count '" + this.dropYear.SelectedValue + "','','" + dropFac.SelectedValue + "'");
        //年逾时次数统计(月) 
        GridViewYearCS.DataSource = dsCS.Tables[0];
        GridViewYearCS.DataBind();
        lblYear.Text = dropYear.SelectedValue + "年";
        lblYearCS.Text = dropYear.SelectedValue + "年";
        ViewState["tblYearCS"] = dsCS.Tables[1];
        bindChartYearCS(dsCS.Tables[1]);
       
    }
    
    public void bindChartYear(DataTable tbl)
    {   //批次
        ChartYearTimes.DataSource = tbl;
        //DataRow[] drs = tbl.Select(" mon='100'");
        //foreach(DataRow dr in drs) { dr.Delete(); }
        ChartYearTimes.Series["计划发货批次"].XValueMember = "MON";
        ChartYearTimes.Series["计划发货批次"].YValueMembers = "ACNT";           
        ChartYearTimes.Series["未按时发货批次"].XValueMember = "MON";
        ChartYearTimes.Series["未按时发货批次"].YValueMembers = "BCNT";    
    }
    public void bindChartYearCS(DataTable tbl)
    {   //人员超时
        ChartYearCS.DataSource = tbl;
        ChartYearCS.Series["超时次数"].XValueMember = "MON";
        ChartYearCS.Series["超时次数"].YValueMembers = "CNT";
    }
    public void bindChartMonth(DataTable tbl)
    {   //批次
        ChartMonth.DataSource = tbl;               
        ChartMonth.Series["计划发货批次"].XValueMember = "MON";
        ChartMonth.Series["计划发货批次"].YValueMembers = "ACNT";
        ChartMonth.Series["未按时发货批次"].XValueMember = "MON";
        ChartMonth.Series["未按时发货批次"].YValueMembers = "BCNT";
        ChartMonth.ChartAreas[0].AxisX.Minimum = 1;
        ChartMonth.ChartAreas[0].AxisX.Maximum = 31;
    }
    public void bindChartMonthCS(DataTable tbl)
    {   //人员超时
        ChartMonthCS.DataSource = tbl;       
        ChartMonthCS.Series["超时次数"].XValueMember = "Engineer";
        ChartMonthCS.Series["超时次数"].YValueMembers = "CNT";       

    }
    //设定年Gridview Header
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";             
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {               
                //LinkButton lbtn = new LinkButton();
                //lbtn.ID = "btn" + i.ToString();
                //lbtn.Text = e.Row.Cells[i].Text;                             
                //lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");               
                //lbtn.Attributes.Add("name", "mon");
                if (e.Row.Cells[i].Text.ToUpper() == "100")
                {
                    e.Row.Cells[i].Text = "合计";
                }
                else if (e.Row.Cells[i].Text == "AVG")
                {
                    e.Row.Cells[i].Text = "平均";
                }
                //else
                //    e.Row.Cells[i].Controls.Add(lbtn);
                
            }
            
        }        
    }
    /// <summary>
    /// 年逾时次数统计(月) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewYearCS_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                //LinkButton lbtn = new LinkButton();
                //lbtn.ID = "btn" + i.ToString();
                //lbtn.Text = e.Row.Cells[i].Text;
                //lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDays','')");
                //lbtn.Attributes.Add("name", "mon");
                if (e.Row.Cells[i].Text.ToUpper() == "100")
                {
                    e.Row.Cells[i].Text = "合计";
                }
                else if (e.Row.Cells[i].Text == "AVG")
                {
                    e.Row.Cells[i].Text = "平均";
                }
                //else
                //    e.Row.Cells[i].Controls.Add(lbtn);

            }

        }
    }
    //Binding月 明细资料
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonth(txtmonth.Text);
        lblMonth.Text = txtmonth.Text + "月";//dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        this.txtmonth.Text = dropMonth.SelectedValue.PadLeft(2, '0');

        this.bindChartYear((DataTable)ViewState["tblYear"]);
        this.bindChartYearCS((DataTable)ViewState["tblYearCS"]);
        this.bindChartMonthCS((DataTable)ViewState["tblMonthCS"]);
        //完成率
        QueryMonthFinish(txtmonth.Text,false);
        lblMonthFinish.Text = txtmonth.Text + "月";//dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        this.txtmonth.Text = dropMonth.SelectedValue.PadLeft(2, '0');

        this.bindChartYearFinish((DataTable)ViewState["tblYearFinish"]);
        setRowLink(GridViewYearFinish, "LinkBtnPersonYear");
      
    }
    public void QueryMonth(string month)
    {                                                       
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ '" + this.dropYear.SelectedValue + "','" + month + "','" + dropFac.SelectedValue + "'");
        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";
        ViewState["tblMonth"] = ds.Tables[1]; 
             
        bindChartYear((DataTable)ViewState["tblYear"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
        bindChartMonth(ds.Tables[1]);
    }
    /// <summary>
    /// 月逾时次数统计(日) 
    /// </summary>
    /// <param name="month"></param>
    public void QueryMonthCS(string month)
    {
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ_count '" + this.dropYear.SelectedValue + "','" + month + "','" + dropFac.SelectedValue + "'");
        GridViewMonthCS.DataSource = ds.Tables[0];
        GridViewMonthCS.DataBind();
        lblMonthCS.Text = month ;
        
        ViewState["tblMonthCS"] = ds.Tables[1];
        bindChartMonthCS(ds.Tables[1]);
       // bindChartYear((DataTable)ViewState["tblYear"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
       // bindChartMonth((DataTable)ViewState["tblMonth"]);
    }
    //Binding 日明细资料
    protected void LinkBtnDays_Click(object sender, EventArgs e)
    {
        QueryMonthCS(txtmonth.Text);
        lblMonthCS.Text = dropYear.SelectedValue.PadLeft(2, '0') + txtmonth.Text.PadLeft(2, '0') ;
        this.txtmonth.Text = dropMonth.SelectedValue.PadLeft(2, '0');

        this.bindChartYear((DataTable)ViewState["tblYear"]);
        this.bindChartYearCS((DataTable)ViewState["tblYearCS"]);
        this.bindChartMonth((DataTable)ViewState["tblMonth"]);
        setRowLink(GridViewYearFinish, "LinkBtnPersonYear");
        setRowLink(GridViewMonthFinish, "LinkBtnPerson");
    }
    /// <summary>
    /// 月发货统计(日) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridViewMonth.HeaderStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {            
            e.Row.Cells[0].Text = "";
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "100")
                    e.Row.Cells[i].Text = "合计";
            }            
        }
        else
        {           
            e.Row.Cells[0].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {               
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;;
            }            
        }
    }
    
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if(DateTime.Now.Month!=Convert.ToInt16(dropMonth.SelectedValue) )
        {
            string day = Convert.ToDateTime(dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue)).ToString().PadLeft(2, '0') + "-01").AddMonths(1).AddDays(-1).Day.ToString();

        }
        this.btnQuery_Click(sender,e);//查询
    }
 
    protected void GridViewMonthCS_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Wrap=false;
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "99")
                    e.Row.Cells[i].Text = "合计";
            }
            
        }
    }
    //--暂不用-点击完成率- 月-链接人员超时明细--暂不用
    protected void LinkBtnPerson_Click(object sender, EventArgs e)
    {
        ShowDelayDetailsByPerson(this.dropYear.SelectedValue, txtmonth.Text, txtPerson.Text);

    }
    //--暂不用-点击完成率 -年-链接人员超时明细--暂不用
    protected void LinkBtnPersonYear_Click(object sender, EventArgs e)
    {
        ShowDelayDetailsByPerson(this.dropYear.SelectedValue, "", txtPerson.Text);

    }
    //--暂不用
    public void ShowDelayDetailsByPerson( string year,string month,string personNo)
    {
        //[rpt_Form1_sale_YJ_Delay_TJ_CountByPerson]  '2017','4','','01715'
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ_CountByPerson '" + year + "','"+month +"','" + dropFac.SelectedValue + "','"+ personNo + "'");

        gvDetail.DataSource = ds.Tables[0];
        gvDetail.DataBind();

        this.bindChartYear((DataTable)ViewState["tblYear"]);
        this.bindChartYearCS((DataTable)ViewState["tblYearCS"]);
        this.bindChartMonth((DataTable)ViewState["tblMonth"]);
        this.bindChartMonthCS((DataTable)ViewState["tblMonthCS"]);
        //完成率月绑定链接点击
        setRowLink(GridViewYearFinish, "LinkBtnPersonYear");
        setRowLink(GridViewMonthFinish, "LinkBtnPerson");
    }


    #region 完成率统计
    //完成率年查询
    public void QueryYearFinish(bool isPageLoad)
    {   //  [rpt_Form1_sale_YJ_FinishRate_TJ]--年--月--产品--负责人 --部门
        //抓年资料 月不传值
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','','','','"+txtDepart.Text.Trim()+"','" + txtYearOrder.Text + "','"+this.dropFac.SelectedValue+"'");
        // DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','','" + txtPart.Text + "','" + txtWho.Text + "','" + dropDept.SelectedValue + "','" + txtYearOrder.Text + "'");
        if (isPageLoad)//如果是页面第一次加载,判断是否有逾时未完成的记录,
        {
            ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','','','','" + txtDepart.Text.Trim() + "','','" + this.dropFac.SelectedValue + "'");
            if (ds.Tables[1].Rows[0]["逾时未完成"].ToString() == "0")//若无则初始排序方式为[未完成数未逾时]
            {
                txtYearOrder.Text = "未逾时未完成 desc";
               // ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','','','','',''");
                ViewState["yearcol"] = "未逾时未完成";
                ViewState["yearorder"] = "desc";

            }
        }
        ViewState["tblYearFinish"] = ds.Tables[0];
        ViewState["tblYearFinishFooter"] = ds.Tables[1];//年统计Footer数据
        
        GridViewYearFinish.DataSource = ds.Tables[0];
        GridViewYearFinish.DataBind();
       
        lblYearFinish.Text = dropYear.SelectedValue + "年";
        
        bindChartYearFinish(ds.Tables[0]);
    }
    //绑定年图
    public void bindChartYearFinish(DataTable tbl)
    {  
        tbl.DefaultView.Sort = txtYearOrder.Text;//  //排序
       
        ChartYearTimesFinish.DataSource = tbl.Select().Take(10);//前10行
        ChartYearTimesFinish.Series["完成率"].XValueMember = "责任人";
        ChartYearTimesFinish.Series["完成率"].YValueMembers = ViewState["yearcol"].ToString();//txtYearOrder.Text;//设定Y轴对应的数据列   "完成率";
        ChartYearTimesFinish.Series["完成率"].LegendText = ViewState["yearcol"].ToString().Replace("及时完成数", "完成数(及时)").Replace("未逾时未完成", "未完成数(未逾时)").Replace("逾时未完成", "未完成数(逾时)").Replace("逾时完成", "完成数(逾时)");//更改图例名称   
    }
    //月图
    public void bindChartMonthFinish(DataTable tbl)
    {   
        tbl.DefaultView.Sort = txtOrder.Text;//排序       
        ChartMonthFinish.DataSource = tbl.Select().Take(10);//前几行
        ChartMonthFinish.Series["完成率"].XValueMember = "责任人";
        ChartMonthFinish.Series["完成率"].YValueMembers = ViewState["monthcol"].ToString(); // "完成率"
        ChartMonthFinish.Series["完成率"].LegendText = ViewState["monthcol"].ToString().Replace("及时完成数", "完成数(及时)").Replace("未逾时未完成", "未完成数(未逾时)").Replace("逾时未完成", "未完成数(逾时)").Replace("逾时完成", "完成数(逾时)");//更改图例名称
    }

    //设定年Gridview Header
    protected void GridViewYearFinish_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {   //添加可点击Link 及前端识别属性：name
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnYearOrder','')");
                lbtn.Attributes.Add("name", "yearorder");
                lbtn.Attributes.Add("yearcolumnname", ((BoundField)this.GridViewYearFinish.Columns[i]).DataField);
                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            DataTable tbl =((DataTable)ViewState["tblYearFinishFooter"]);
            if(tbl.Rows.Count>0)
            {
                e.Row.BackColor = System.Drawing.Color.Orange;
                for(int j = 0; j < e.Row.Cells.Count; j++)
                {
                    e.Row.Cells[j].Text = tbl.Rows[0][j].ToString(); //string.Format("{0:P0}", tbl.Rows[0][j].ToString());
                }
            }
        }
    }
    public void QueryMonthFinish(string month,bool isPageLoad)
    {
        //DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','" + txtPart.Text + "','" + txtWho.Text + "','" + dropDept.SelectedValue + "','" + txtOrder.Text + "'");
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','','','" + txtDepart.Text.Trim() + "','" + txtOrder.Text + "','" + this.dropFac.SelectedValue + "'");
        
        if (isPageLoad)//如果是页面第一次加载,判断是否有逾时未完成的记录,
        {
            ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','','','" + txtDepart.Text.Trim() + "','','" + this.dropFac.SelectedValue + "'");
            if (ds.Tables[1].Rows[0]["逾时未完成"].ToString() == "0")//若无则初始排序方式为[未完成数未逾时]
            {
                txtOrder.Text = "未逾时未完成 desc";
              //  ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','','','',''");
                ViewState["monthcol"] = "未逾时未完成";
                ViewState["order"] = "desc";

            }
        }
        ViewState["tblMonthFinish"] = ds.Tables[0];
        ViewState["tblMonthFinishFooter"] = ds.Tables[1];
        
        GridViewMonthFinish.DataSource = ds.Tables[0];
        GridViewMonthFinish.DataBind();
        
        lblMonthFinish.Text = txtmonth.Text + "月";

        bindChartYearFinish((DataTable)ViewState["tblYearFinish"]);//因为页面刷新会消失，所以重新绑定？为什么，待确认
        bindChartMonthFinish(ds.Tables[0]);
    }

    protected void GridViewMonthFinish_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {//添加可点击Link 及前端识别属性：name
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnMonthOrder','')");
                lbtn.Attributes.Add("name", "order");
                lbtn.Attributes.Add("monthcolumnname", ((BoundField)this.GridViewMonthFinish.Columns[i]).DataField);
                e.Row.Cells[i].Controls.Add(lbtn);
            }            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            DataTable tbl = ((DataTable)ViewState["tblMonthFinishFooter"]);
            if (tbl.Rows.Count > 0)
            {
                e.Row.BackColor = System.Drawing.Color.Orange;
                for (int j = 0; j < e.Row.Cells.Count; j++)
                {
                    e.Row.Cells[j].Text = tbl.Rows[0][j].ToString(); //string.Format("{0:P0}", tbl.Rows[0][j].ToString());
                }
            }
        }
    }   

    //年排序
    protected void LinkBtnYearOrder_Click(object sender, EventArgs e)
    {
        txtYearOrder.Text = txtYearOrder.Text.Replace("完成数(及时)", "及时完成数").Replace("未完成数(逾时)", "逾时未完成").Replace("未完成数(未逾时)", "未逾时未完成").Replace("完成数(逾时)", "逾时完成");
        string col = txtYearOrder.Text.Replace("desc", "").Replace("asc", "").Trim();//取新的排序栏位
        if (ViewState["yearcol"].ToString() == col)
        {
            if (ViewState["yearorder"].ToString() == "desc")
            {
                ViewState["yearorder"] = "asc";
            }
            else { ViewState["yearorder"] = "desc"; }
        }
        else {
            if (txtYearOrder.Text.Contains("率") == true) //如果是及时率或是完成率 则顺序排列
            {
                ViewState["yearorder"] = "asc";
            }
            else
            { ViewState["yearorder"] = "desc"; }
        };
        ViewState["yearcol"] = col;
        txtYearOrder.Text = ViewState["yearcol"].ToString() + " " + ViewState["yearorder"].ToString();

        GridViewYearFinish.PageIndex = 0;
        btnQuery_Click(sender, e);
        
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$(\"a[yearcolumnname='" + ViewState["yearcol"].ToString() + "']\").css('color','orange');$(\"a[monthcolumnname='" + ViewState["monthcol"].ToString() + "']\").css('color','orange');", true);
    }
    //月排序
    protected void LinkBtnMonthOrder_Click(object sender, EventArgs e)
    {
        txtOrder.Text = txtOrder.Text.Replace("完成数(及时)", "及时完成数").Replace("未完成数(逾时)", "逾时未完成").Replace("未完成数(未逾时)", "未逾时未完成").Replace("完成数(逾时)", "逾时完成");
        string col = txtOrder.Text.Replace("desc", "").Replace("asc", "").Trim();//取新的排序栏位;

        if (ViewState["monthcol"].ToString() == col)
        {
            if (ViewState["monthorder"].ToString() == "desc")
            {
                ViewState["monthorder"] = "asc";
            }
            else { ViewState["monthorder"] = "desc"; }
        }
        else {
            if (txtOrder.Text.Contains("率") == true) //如果是及时率或是完成率 则顺序排列
            {
                ViewState["monthorder"] = "asc";
            }
            else
            { ViewState["monthorder"] = "desc";  }
            
        };
        ViewState["monthcol"] = col;
        txtOrder.Text = ViewState["monthcol"].ToString() + " " + ViewState["monthorder"].ToString();

        GridViewMonthFinish.PageIndex = 0;
        // LinkBtn_Click(sender, e);
        btnQuery_Click(sender, e);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$(\"a[yearcolumnname='" + ViewState["yearcol"].ToString() + "']\").css('color','orange');$(\"a[monthcolumnname='" + ViewState["monthcol"].ToString() + "']\").css('color','orange');", true);
    }
    protected void GridViewMonthFinish_RowDataBound(object sender, GridViewRowEventArgs e)
    {  
        if (e.Row.RowType == DataControlRowType.DataRow)
        {   e.Row.Cells[11].Style.Add("display", "none");//Hide 工号         
            if (e.Row.Cells[0].Text.Trim() != "合计")//第一列添加链接
            { //添加可点击Link 及前端识别属性：name
                LinkButton lbtn = (LinkButton)e.Row.Cells[0].FindControl("lbtnMonth");
                // lbtn.ID = "btn" + e.Row.Cells[0].Text;
                // lbtn.Text = e.Row.Cells[0].Text;
                // lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnPerson','')");
                lbtn.Attributes.Add("href", @"javascript:void(0)");//remove link event
                lbtn.Attributes.Add("name", "Person");
                lbtn.Attributes.Add("value", e.Row.Cells[11].Text);//绑定工号
               // e.Row.Cells[0].Controls.Add(lbtn);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Style.Add("display", "none");//Hide 工号            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Style.Add("display", "none");//Hide 工号栏           
        }
    }
    protected void GridViewYearFinish_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Style.Add("display", "none");//Hide 工号
            if (e.Row.Cells[0].Text.Trim() != "合计")//第一列添加链接
            { //添加可点击Link 及前端识别属性：name
                LinkButton lbtn = (LinkButton)e.Row.Cells[0].FindControl("lbtnYear");
                // lbtn.ID = "btn" + e.Row.Cells[0].Text;
                // lbtn.Text = e.Row.Cells[0].Text;
                // lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnPersonYear','')");
                lbtn.Attributes.Add("href", @"javascript:void(0)");//remove link event
                lbtn.Attributes.Add("name", "Person");
                lbtn.Attributes.Add("value", e.Row.Cells[11].Text);//绑定工号
               // e.Row.Cells[0].Controls.Add(lbtn);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Style.Add("display", "none");//Hide 工号            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Style.Add("display", "none");//Hide 工号栏           
        }
    }
    protected void GridViewYearFinish_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewYearFinish.PageIndex = e.NewPageIndex;
        GridViewYearFinish.DataSource = ViewState["tblYearFinish"];
        GridViewYearFinish.DataBind();
               
        bindChartYearFinish((DataTable)ViewState["tblYearFinish"]);
       
    }
    protected void GridViewMonthFinish_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewMonthFinish.PageIndex = e.NewPageIndex;
        GridViewMonthFinish.DataSource = ViewState["tblMonthFinish"];
        GridViewMonthFinish.DataBind();       
        
        bindChartMonthFinish((DataTable)ViewState["tblMonthFinish"]);
    }
    public void setRowLink(GridView gv,string linkBtn)
    {
        for(int i = 0; i < gv.Rows.Count; i++)
        {
            gv.Rows[i].Cells[11].Style.Add("display", "none");//Hide 工号
            if (gv.Rows[i].Cells[0].Text.Trim() != "合计")//第一列添加链接
            { //添加可点击Link 及前端识别属性：name
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + gv.Rows[i].Cells[0].Text;
                lbtn.Text = gv.Rows[i].Cells[0].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$"+ linkBtn + "','')");
                lbtn.Attributes.Add("name", "Person");
                lbtn.Attributes.Add("value", gv.Rows[i].Cells[11].Text);//绑定工号
                gv.Rows[i].Cells[0].Controls.Add(lbtn);
            }
        }
        bindChartMonthFinish((DataTable)ViewState["tblMonthFinish"]);
        bindChartYearFinish((DataTable)ViewState["tblYearFinish"]);
    }

    #endregion
        
    //--暂不用
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Style.Add("display", "none");
            e.Row.Cells[0].Text = "<a href='yangjian.aspx?requestid=" + e.Row.Cells[10].Text + "' target='_blank'>" + e.Row.Cells[0].Text + "</a>";
        }
        else if (e.Row.RowType == DataControlRowType.Header) {
            e.Row.Cells[10].Style.Add("display", "none");
        }
    }
    /// <summary>
    /// 年发货统计(月) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewYear_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "4"|| e.Row.Cells[0].Text == "1")
            {
                //添加可点击Link 及前端识别属性：name
                for (int i = 2; i < e.Row.Cells.Count-1; i++)
                {
                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim()!="")
                    {
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnLotYear" + e.Row.Cells[i-1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                        lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                        lbtn.Attributes.Add("name", "Lot");
                        lbtn.Attributes.Add("value", (i-1).ToString());//月分
                        lbtn.Attributes.Add("type", e.Row.Cells[0].Text); //1:计划发货批次 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                    }
                   
                }
            }
        }
    }


    /// <summary>
    ///月发货统计(日) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewMonth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "4" || e.Row.Cells[0].Text == "1")
            {
                //添加可点击Link 及前端识别属性：name
                for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                {
                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                    {
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnLotMonth" + e.Row.Cells[i - 1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                        lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                        lbtn.Attributes.Add("name", "Lot");
                        lbtn.Attributes.Add("value", (i - 1).ToString());//日
                        lbtn.Attributes.Add("type", e.Row.Cells[0].Text); //1:计划发货批次 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick","true");//添加特殊属性表示可以点击
                    }

                }
            }
        }
    }
    /// <summary>
    /// 年逾时次数统计(月) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewYearCS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            //添加可点击Link 及前端识别属性：name
            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
            {
                if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnOvertimeMonth" + e.Row.Cells[i - 1].Text;
                    lbtn.Text = e.Row.Cells[i].Text;
                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                    lbtn.Attributes.Add("name", "OverTime");
                    lbtn.Attributes.Add("value", (i - 1).ToString());//月
                    lbtn.Attributes.Add("type", e.Row.Cells[0].Text); //1:完成(逾时) 2:未完成(逾时) 3:未完成(未逾时)
                    e.Row.Cells[i].Controls.Add(lbtn);
                    e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                }

            }
        }
    }
    /// <summary>
    /// 月逾时次数统计(日) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewMonthCS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
            {
                if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnOverTimeMonth" + e.Row.Cells[i - 1].Text;
                    lbtn.Text = e.Row.Cells[i].Text;
                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                    lbtn.Attributes.Add("name", "OverTime");
                    lbtn.Attributes.Add("value", (i - 1).ToString());//日
                   
                    lbtn.Attributes.Add("type", e.Row.Cells[0].Text); //1:完成(逾时) 2:未完成(逾时) 3:未完成(未逾时)                 
                                               
                    e.Row.Cells[i].Controls.Add(lbtn);
                    e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                   
                }
            }


            
            
        }
    }
}