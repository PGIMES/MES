using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class ChanPin_TJ : System.Web.UI.Page
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
            QueryD();
            QueryE();
            QueryA();//       
            QueryB();//
            QueryC();//
           

        }      

    }
    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        QueryD();
        QueryE();
        QueryA();//     
        QueryB();//
        QueryC();//
     
 
    }
    public void QueryD()
    {
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ  '" + this.dropYear.SelectedValue + "','" + dropCondition.SelectedValue + "','4'");

        GridViewD.DataSource = ds.Tables[1];
        GridViewD.DataBind();
        lblD.Text = dropYear.SelectedValue + "年";

        ViewState["tblD"] = ds.Tables[0];
        bindChartD(ds.Tables[0]);
    }
    public void QueryE()
    {
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ  '" + this.dropYear.SelectedValue + "','" + dropCondition.SelectedValue + "','5'");

        GridViewE.DataSource = ds.Tables[1];
        GridViewE.DataBind();
        LblE.Text = "";//dropYear.SelectedValue + "年"

        ViewState["tblE"] = ds.Tables[0];
        bindChartE(ds.Tables[0]);
    }
    public void QueryA()
    {
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ  '" + this.dropYear.SelectedValue + "','" + dropCondition.SelectedValue+ "','1'");
  
        GridViewA.DataSource = ds.Tables[1];
        GridViewA.DataBind();
        lblA.Text = dropYear.SelectedValue+"年";

        ViewState["tblA"] = ds.Tables[0];       
        bindChartA(ds.Tables[0]);
    }
    public void QueryB()
    {
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ  '" + this.dropYear.SelectedValue + "','" + dropCondition.SelectedValue + "','2'");

        GridViewB.DataSource = ds.Tables[1];
        GridViewB.DataBind();
        lblB.Text = dropYear.SelectedValue + "年";

        ViewState["tblB"] = ds.Tables[0];
        bindChartB(ds.Tables[0]);
    }
    public void QueryC()
    {          
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ  '" + this.dropYear.SelectedValue + "','" + dropCondition.SelectedValue + "','3'");
        
        GridViewC.DataSource = ds.Tables[1];
        GridViewC.DataBind();        
        lblC.Text = dropYear.SelectedValue + "年";
        ViewState["tblC"] = ds.Tables[0];
        bindChartC(ds.Tables[0]);
       
    }
    public void bindChartD(DataTable tbl)
    {
        ChartD.DataSource = tbl;
        //DataRow[] drs = tbl.Select(" mon='100'");
        //foreach(DataRow dr in drs) { dr.Delete(); }
        ChartD.Series["A1"].XValueMember = "LB";
        ChartD.Series["A1"].YValueMembers = "ACNT";
         
        // ChartA.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        /// setChartType(ChartA.Series["A1"], true);
    }
    public void bindChartE(DataTable tbl)
    {
        ChartE.DataSource = tbl;
        //DataRow[] drs = tbl.Select(" mon='100'");
        //foreach(DataRow dr in drs) { dr.Delete(); }
        ChartE.Series["A1"].XValueMember = "MON";
        ChartE.Series["A1"].YValueMembers = "BCNT";
        ChartE.Series["A2"].XValueMember = "MON";
        ChartE.Series["A2"].YValueMembers = "ACNT";
        // ChartA.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        /// setChartType(ChartA.Series["A1"], true);
    }
    public void bindChartA(DataTable tbl)
    {   
        ChartA.DataSource = tbl;
        //DataRow[] drs = tbl.Select(" mon='100'");
        //foreach(DataRow dr in drs) { dr.Delete(); }
        ChartA.Series["A1"].XValueMember = "MON";
        ChartA.Series["A1"].YValueMembers = "BCNT";
        ChartA.Series["A2"].XValueMember = "MON";
        ChartA.Series["A2"].YValueMembers = "ACNT";
        // ChartA.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
       /// setChartType(ChartA.Series["A1"], true);
    }
    public void bindChartB(DataTable tbl)
    {   
        ChartB.DataSource = tbl;               
        ChartB.Series["B1"].XValueMember = "MON";
        ChartB.Series["B1"].YValueMembers = "ACNT";
        ChartB.Series["B2"].XValueMember = "MON";
        ChartB.Series["B2"].YValueMembers = "BCNT";
        ChartB.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        // ChartB.ChartAreas[0].AxisX.Minimum = 1;
        // ChartB.ChartAreas[0].AxisX.Maximum = 31;
        //setChartType(ChartB.Series["B1"], true);
    }
    public void bindChartC(DataTable tbl)
    {   
        ChartC.DataSource = tbl;
        ChartC.Series["C1"].XValueMember = "MON";
        ChartC.Series["C1"].YValueMembers = "ACNT";
        ChartC.Series["C2"].XValueMember = "MON";
        ChartC.Series["C2"].YValueMembers = "BCNT";
        ChartC.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        //setChartType(ChartC.Series["C1"], true);
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
    //当前数量统计 设定Gridview Header
    protected void GridViewD_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void GridViewE_RowCreated(object sender, GridViewRowEventArgs e)
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
    //月统计 设定Gridview Header
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
    /// 客户统计
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
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
            }
        }
    }

    /// <summary>
    /// C: 类别统计
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
    /// A:月统计 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "4")//左边留空表示子项
        {
            e.Row.Cells[1].Style.Add("padding-left", "20px");
        }
        
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {                
            if ( e.Row.Cells[0].Text == "1" || e.Row.Cells[0].Text == "3"|| e.Row.Cells[0].Text == "5") //1，3，5 添加链接
            {   //添加可点击Link 及前端识别属性：name
                for (int i = 2; i <= e.Row.Cells.Count-1; i++)
                {                        
                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim()!="")
                    {
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnA1" + e.Row.Cells[i-1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                       // lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                        lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                        lbtn.Attributes.Add("name", "A");
                        lbtn.Attributes.Add("value", (i-1).ToString());//月分
                        lbtn.Attributes.Add("type", "A"+e.Row.Cells[0].Text); //1:开发中数量  3:生产中数量   5:停产数量
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                    }                   
                }
            }            

        }
    }
    /// <summary>
    /// B:客户统计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "4")//1，3，6 添加链接
            {
                e.Row.Cells[1].Style.Add("padding-left", "20px");
            }
             
            if (e.Row.Cells[0].Text == "1" || e.Row.Cells[0].Text == "3" || e.Row.Cells[0].Text == "6")
            {
                //添加可点击Link 及前端识别属性：name
                for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                {
                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                    {
                        GridViewRow dr = GridViewB.HeaderRow;
                        string strValue = dr.Cells[i].Text;
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnB1" + e.Row.Cells[i - 1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                        // lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                        lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                        lbtn.Attributes.Add("name", "B");
                        lbtn.Attributes.Add("value", strValue);//月
                        lbtn.Attributes.Add("type", "B" + e.Row.Cells[0].Text); //1:开发中数量  3:生产中数量   6:合计数量
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击变色
                    }
                }
            }
            
        }
    }
    /// <summary>
    /// C: 类别统计 GridViewC_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "4")//左边留空表示子项
        {
            e.Row.Cells[1].Style.Add("padding-left", "20px");
        }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "1"||e.Row.Cells[0].Text == "3" || e.Row.Cells[0].Text == "5" || e.Row.Cells[0].Text == "6")//1:开发中数量  3:生产中数量 5:停产  6:合计数量
            {
                //添加可点击Link 及前端识别属性：name
                for (int i = 2; i < e.Row.Cells.Count - 1; i++)
                {
                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                    {
                        GridViewRow dr = GridViewC.HeaderRow;
                        string strValue = dr.Cells[i].Text;
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnC1" + e.Row.Cells[i - 1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                        // lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                        lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                        lbtn.Attributes.Add("name", "C" );
                        lbtn.Attributes.Add("value", strValue);//列值
                        lbtn.Attributes.Add("type", "C"+e.Row.Cells[0].Text); //1:开发中数量  3:生产中数量 5:停产  6:合计数量
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                    }

                }
            }
        }
        
    }

    //--=====Show Detail=====
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        this.lblName.Text = txtyear.Text + "-" + txtmonth.Text + txttitle.Text;
        ShowDetails(txtyear.Text, txtmonth.Text, txtcondition.Text, txttype.Text);

        List<int> list = new List<int>();
        for (int i = 0; i < ((DataTable)gvdetail.DataSource).Columns.Count; i++)
        {
            if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 14)//6:零件号  ;7:零件名称
            {
                list.Add(i);
            }
        }

        //  int[] cols = { 0, 1, 2, 3, 4,14 };

        MergGridRow.MergeRow(gvdetail, list.ToArray());
        int rowIndex = 1;
        for (int i = 0; i <= gvdetail.Rows.Count - 1; i++)
        {
            if (gvdetail.Rows[i].Cells[0].Visible == true)
            {
                gvdetail.Rows[i].Cells[0].Text = rowIndex.ToString();
                rowIndex++;
            }
        }
    }
    protected void gvdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string requestid=Server.HtmlDecode(e.Row.Cells[0].Text.ToString()).Trim();           
            //e.Row.Cells[1].Text = "<a href='baojia.aspx?requestid="+ requestid + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";
            //e.Row.Cells[5].Style.Add("word-break", "break-all");
            //e.Row.Cells[5].Style.Add("width", "100px");
            Image img = new Image();
            img.Width = 40;
            img.Height = 40;
            img.ImageUrl = e.Row.Cells[18].Text;
            e.Row.Cells[18].Controls.Add(img);
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "No.";
        }
    }

    public void ShowDetails(string year, string month, string condition, string type)
    {
        month = month.Length >= 2 ? month : ("0" + month);
        DataSet ds = DbHelperSQL.Query("exec rpt_Form3_Sale_PRD_TJ_ByCondition '" + year + "','" + month + "','" + condition + "','" + type + "'");
       
        gvdetail.DataSource = ds.Tables[0];
        gvdetail.DataBind();

    }


   
}