using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class BaoJia_FenXi_TJ : System.Web.UI.Page
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

            QueryA();//       
            QueryB();//
            QueryC();//
            QueryD();//

        }      

    }
    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {   
        QueryA();//Year        
        QueryB();//month
        QueryC();//
        QueryD();//
 
    }
    public void QueryA()
    {
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_"+ dropCondition.SelectedValue+ "  '" + this.dropYear.SelectedValue + "','','1'");
  
        GridViewA.DataSource = ds.Tables[1];
        GridViewA.DataBind();
        lblA.Text = dropYear.SelectedValue+"年";

        ViewState["tblA"] = ds.Tables[0];       
        bindChartA(ds.Tables[0]);
    }
    public void QueryB()
    {
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_" + dropCondition.SelectedValue + "  '" + this.dropYear.SelectedValue + "','','2'");

        GridViewB.DataSource = ds.Tables[1];
        GridViewB.DataBind();
        lblB.Text = dropYear.SelectedValue + "年";

        ViewState["tblB"] = ds.Tables[0];
        bindChartB(ds.Tables[0]);
    }
    public void QueryC()
    {          
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_" + dropCondition.SelectedValue + "  '" + this.dropYear.SelectedValue + "','','3'");
        
        GridViewC.DataSource = ds.Tables[1];
        GridViewC.DataBind();        
        lblC.Text = dropYear.SelectedValue + "年";
        ViewState["tblC"] = ds.Tables[0];
        bindChartC(ds.Tables[0]);
       
    }
    public void QueryD()
    {      
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_" + dropCondition.SelectedValue + "  '" + this.dropYear.SelectedValue + "','','4'");
        
        GridViewD.DataSource = ds.Tables[1];
        GridViewD.DataBind();
        lblD.Text = dropYear.SelectedValue + "年";        
        ViewState["tblD"] = ds.Tables[0];
        bindChartD(ds.Tables[0]);

    }
    public void bindChartA(DataTable tbl)
    {   
        ChartA.DataSource = tbl;
        //DataRow[] drs = tbl.Select(" mon='100'");
        //foreach(DataRow dr in drs) { dr.Delete(); }
        ChartA.Series["A1"].XValueMember = "MON";
        ChartA.Series["A1"].YValueMembers = "CNT";
        ChartA.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        
        //ChartA.Series["A2"].XValueMember = "MON";
        //ChartA.Series["A2"].YValueMembers = "BCNT";   
        setChartType(ChartA.Series["A1"], true);
    }
    public void bindChartB(DataTable tbl)
    {   //批次
        ChartB.DataSource = tbl;               
        ChartB.Series["B1"].XValueMember = "Mon";
        ChartB.Series["B1"].YValueMembers = "CNT";
        ChartB.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        // ChartB.ChartAreas[0].AxisX.Minimum = 1;
        // ChartB.ChartAreas[0].AxisX.Maximum = 31;
        setChartType(ChartB.Series["B1"], true);
    }
    public void bindChartC(DataTable tbl)
    {   //人员超时
        ChartC.DataSource = tbl;
        ChartC.Series["C1"].XValueMember = "Mon";
        ChartC.Series["C1"].YValueMembers = "CNT";
        ChartC.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        setChartType(ChartC.Series["C1"], true);
    }
    public void bindChartD(DataTable tbl)
    {   //人员超时
        ChartD.DataSource = tbl;       
        ChartD.Series["D1"].XValueMember = "Mon";
        ChartD.Series["D1"].YValueMembers = "CNT";
        ChartD.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        setChartType(ChartD.Series["D1"],true);
    }
    public void setChartType(System.Web.UI.DataVisualization.Charting.Series   chartSeries, Boolean isC )
    {
        Boolean bln = this.dropCondition.SelectedValue == "M" ? true:false; //按月份:显示折线图
        if (bln)
        {
            chartSeries.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        }
        else
        {
            chartSeries.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
        }
    }
    //设定Gridview Header
    protected void GridViewA_RowCreated(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = ""; 
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
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
        else
        {
            e.Row.Cells[0].Wrap = false;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }
    }
    /// <summary>
    /// C: 报价成功率
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewC_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {

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
        else
        {
                e.Row.Cells[0].Wrap = false;
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
                }
        }
    }

    /// <summary>
    /// 报价及时率
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewB_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridViewB.HeaderStyle.Wrap = false;
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
   
    protected void GridViewD_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Wrap=false;
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
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }
    }
    /// <summary>
    /// A:报价零件数 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
        {
            e.Row.Cells[1].Style.Add("padding-left", "20px");
        }
        
        if (this.dropCondition.SelectedValue == "M")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if ( e.Row.Cells[0].Text == "1")//报价次数
                {
                    //添加可点击Link 及前端识别属性：name
                    for (int i = 2; i < e.Row.Cells.Count-1; i++)
                    {
                        
                        if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim()!="")
                        {
                            LinkButton lbtn = new LinkButton();
                            lbtn.ID = "lbtnA1" + e.Row.Cells[i-1].Text;
                            lbtn.Text = e.Row.Cells[i].Text;
                            lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                            lbtn.Attributes.Add("name", "A");
                            lbtn.Attributes.Add("value", (i-1).ToString());//月分
                            lbtn.Attributes.Add("type", "A"+e.Row.Cells[0].Text); //1:报价次数-- 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
                            e.Row.Cells[i].Controls.Add(lbtn);
                            e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                        }
                   
                    }
                }
            }
        }
    }
    /// <summary>
    /// B:报价及时率
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
            {
                e.Row.Cells[1].Style.Add("padding-left", "20px");
            }
            if (this.dropCondition.SelectedValue == "M")//月份才加链接
            {
                if (e.Row.Cells[0].Text == "1")
                {
                    //添加可点击Link 及前端识别属性：name
                    for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                    {
                        if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                        {
                            LinkButton lbtn = new LinkButton();
                            lbtn.ID = "lbtnB1" + e.Row.Cells[i - 1].Text;
                            lbtn.Text = e.Row.Cells[i].Text;
                            lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                            lbtn.Attributes.Add("name", "B");
                            lbtn.Attributes.Add("value", (i - 1).ToString());//月
                            lbtn.Attributes.Add("type", "B" + e.Row.Cells[0].Text); //1:报出次数 2:及时报出次数 
                            e.Row.Cells[i].Controls.Add(lbtn);
                            e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击变色
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// C: 报价成功率 GridViewC_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (this.dropCondition.SelectedValue == "M")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "1"||e.Row.Cells[0].Text == "3")//1:报价项目数,3:定点项目数
                {
                    //添加可点击Link 及前端识别属性：name
                    for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                    {
                        if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                        {
                            LinkButton lbtn = new LinkButton();
                            lbtn.ID = "lbtnC1" + e.Row.Cells[i - 1].Text;
                            lbtn.Text = e.Row.Cells[i].Text;
                            lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                            lbtn.Attributes.Add("name", "C" );
                            lbtn.Attributes.Add("value", (i - 1).ToString());//月分
                            lbtn.Attributes.Add("type", "C"+e.Row.Cells[0].Text); //1:报价项目数;2:  ;3:定点项目数 
                            e.Row.Cells[i].Controls.Add(lbtn);
                            e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// D: 报价天数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
        {
            e.Row.Cells[1].Style.Add("padding-left", "20px");
        }

        if (this.dropCondition.SelectedValue == "M")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(e.Row.Cells[0].Text == "1" || e.Row.Cells[0].Text=="2"|| e.Row.Cells[0].Text == "3")
                {
             
                    for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                    {
                        if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                        {
                            LinkButton lbtn = new LinkButton();
                            lbtn.ID = "lbtnD1" + e.Row.Cells[i - 1].Text;
                            lbtn.Text = e.Row.Cells[i].Text;
                            lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                            lbtn.Attributes.Add("name", "D");
                            lbtn.Attributes.Add("value", (i - 1).ToString());//日

                            lbtn.Attributes.Add("type", "D"+e.Row.Cells[0].Text); //2:报价中(逾时) 3:报价中(未逾时)                  

                            e.Row.Cells[i].Controls.Add(lbtn);
                            e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击

                        }
                    }
                }


            }


        }
    }
}