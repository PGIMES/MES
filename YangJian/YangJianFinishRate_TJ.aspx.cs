using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class YangJianFinishRate_TJ : System.Web.UI.Page
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
            string str = "select distinct b.dept_name from[dbo].[form1_Sale_YJ_LOG] a join HRM_EMP_MES b on a.Update_user=b.workcode";
            DataSet ds = DbHelperSQL.Query(str);
            fun.initDropDownList(dropDept, ds.Tables[0], "dept_name", "dept_name");
            dropDept.Items.Insert(0,"");
            //初始化完成率默认排序栏位及顺序
            ViewState["yearcol"] = "及时率";
            ViewState["yearorder"] = "desc";
            ViewState["monthcol"] = "及时率";
            ViewState["monthorder"] = "desc";

            QueryYearFinish();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            QueryMonthFinish(DateTime.Now.ToString("MM"));
   

        }
        
    }
    //年Gridview
    protected void btnQuery_Click(object sender, EventArgs e)
    {   //Year
        QueryYearFinish();
        //month
        QueryMonthFinish(dropMonth.SelectedValue.PadLeft(2, '0'));
        lblMonthFinish.Text = dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0')+"月";        
        this.txtmonth.Text =  dropMonth.SelectedValue.PadLeft(2, '0');//dropYear.SelectedValue.PadLeft(2, '0') +
       
    }
    //完成率年查询
    public void QueryYearFinish()
    {   //  [rpt_Form1_sale_YJ_FinishRate_TJ]--年--月--产品--负责人 --部门
        //抓年资料 月不传值
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','','" + txtPart.Text + "','"+txtWho.Text+"','"+dropDept.SelectedValue+"','"+txtYearOrder.Text+"'");

        GridViewYearFinish.DataSource = ds.Tables[0];
        GridViewYearFinish.DataBind();
        GridViewYearFinish.Rows[GridViewYearFinish.Rows.Count - 1].BackColor = System.Drawing.Color.Orange;//设定合计背景色
        lblYearFinish.Text = dropYear.SelectedValue+"年";

        ViewState["tblYearFinish"] = ds.Tables[0];
        bindChartYearFinish(ds.Tables[0]);
    }
    //绑定年图
    public void bindChartYearFinish(DataTable tbl)
    {   //过滤掉合计栏位 不被Chart显示
        tbl.DefaultView.Sort = txtYearOrder.Text;// + " asc"; //排序
        DataRow[] drs = tbl.Select("责任人 = '合计'");
        foreach(DataRow dr in drs)
        {
            tbl.Rows.Remove(dr);
        }
        ChartYearTimesFinish.DataSource = tbl.Select().Take(20);//前20行
        ChartYearTimesFinish.Series["完成率"].XValueMember = "责任人";
        ChartYearTimesFinish.Series["完成率"].YValueMembers = ViewState["yearcol"].ToString();//txtYearOrder.Text;//设定Y轴对应的数据列   "完成率";
        ChartYearTimesFinish.Series["完成率"].LegendText = ViewState["yearcol"].ToString();//更改图例名称   
    }
   //月图
    public void bindChartMonthFinish(DataTable tbl)
    {   //过滤掉合计栏位 不被Chart显示
        tbl.DefaultView.Sort = txtOrder.Text ;//排序
        DataRow[] drs = tbl.Select("责任人 = '合计'");
        foreach (DataRow dr in drs)
        {
            tbl.Rows.Remove(dr);
        }
        ChartMonthFinish.DataSource = tbl.Select().Take(20);//前20行
        ChartMonthFinish.Series["完成率"].XValueMember = "责任人";
        ChartMonthFinish.Series["完成率"].YValueMembers = ViewState["monthcol"].ToString(); // "完成率"
        ChartMonthFinish.Series["完成率"].LegendText = ViewState["monthcol"].ToString();//更改图例名称
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
    }
 
    //Binding月 明细资料
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        QueryMonthFinish(txtmonth.Text);
        lblMonthFinish.Text = txtmonth.Text + "月";//dropYear.SelectedValue.PadLeft(2, '0') + dropMonth.SelectedValue.PadLeft(2, '0') + "月";
        this.txtmonth.Text = dropMonth.SelectedValue.PadLeft(2, '0');

        this.bindChartYearFinish((DataTable)ViewState["tblYearFinish"]);

    }
    public void QueryMonthFinish(string month)
    {                                                        
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_FinishRate_TJ '" + this.dropYear.SelectedValue + "','"+dropMonth.SelectedValue+"','" + txtPart.Text + "','" + txtWho.Text + "','" + dropDept.SelectedValue + "','"+txtOrder.Text+"'");
        GridViewMonthFinish.DataSource = ds.Tables[0];
        GridViewMonthFinish.DataBind();
        GridViewMonthFinish.Rows[GridViewMonthFinish.Rows.Count-1].BackColor=System.Drawing.Color.Orange;//设定合计背景色

        lblMonthFinish.Text = txtmonth.Text + "月";
        ViewState["tblMonthFinish"] = ds.Tables[0];

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
       
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if(DateTime.Now.Month!=Convert.ToInt16(dropMonth.SelectedValue) )
        {
            string day = Convert.ToDateTime(dropYear.SelectedValue + "-" + (Convert.ToInt16(dropMonth.SelectedValue)).ToString().PadLeft(2, '0') + "-01").AddMonths(1).AddDays(-1).Day.ToString();
        }
    }
    //年排序
    protected void LinkBtnYearOrder_Click(object sender, EventArgs e)
    {        
        string yearorder = txtYearOrder.Text.Replace("完成数(及时)", "及时完成数").Replace("未完成数(逾时)", "逾时未完成").Replace("未完成数(未逾时)", "未逾时未完成").Replace("完成数(逾时)", "逾时完成");
        txtYearOrder.Text = yearorder;
        ViewState["yearcol"] = yearorder;
        if (ViewState["yearcol"].ToString() == txtYearOrder.Text)
        {
            if (ViewState["yearorder"].ToString() == "asc")
            {
                ViewState["yearorder"] = "desc";
            }
            else { ViewState["yearorder"] = "asc"; }
        }
        else {
            ViewState["yearorder"] = "asc";
        };
        txtYearOrder.Text = ViewState["yearcol"].ToString() + " " + ViewState["yearorder"].ToString();
        btnQuery_Click(sender, e);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$(\"a[yearcolumnname='"+ViewState["yearcol"].ToString()+ "']\").css('color','orange');$(\"a[monthcolumnname='" + ViewState["monthcol"].ToString() + "']\").css('color','orange');", true); 
    }
    //月排序
    protected void LinkBtnMonthOrder_Click(object sender, EventArgs e)
    {
        string order = txtOrder.Text.Replace("完成数(及时)", "及时完成数").Replace("未完成数(逾时)", "逾时未完成").Replace("未完成数(未逾时)", "未逾时未完成").Replace("完成数(逾时)", "逾时完成");
        txtOrder.Text = order;
        ViewState["monthcol"] = order;
        if (ViewState["monthcol"].ToString() == txtOrder.Text)
        {
            if (ViewState["monthorder"].ToString() == "asc")
            {
                ViewState["monthorder"] = "desc";
            }
            else { ViewState["monthorder"] = "asc"; }
        }
        else {
            ViewState["monthorder"] = "asc";
        };
        txtOrder.Text = ViewState["monthcol"].ToString() + " " + ViewState["monthorder"].ToString();
        LinkBtn_Click(sender, e);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$(\"a[yearcolumnname='" + ViewState["yearcol"].ToString() + "']\").css('color','orange');$(\"a[monthcolumnname='" + ViewState["monthcol"].ToString() + "']\").css('color','orange');", true);
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string Code = "YJ12345";
    //    string FLR = "荷花";
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "openwind('"+Code+"', '"+FLR+"')", true);
    //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "layer.open({shade: [0.5, '#000', false],type: 2,offset: '100px',area: ['600px', '750px'], fix: false,maxmin: false,title: ['打印', false],closeBtn: 1,"
    //    //        + "content: 'PrintFenJianDan.aspx?Code=" + "YJ12345" + "&FLR=" + "荷花" + ", end: function() {}});", true);
    //}

}