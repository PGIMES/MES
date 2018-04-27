using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.Services;

public partial class BaoJia_Query : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 500;         
        if (!IsPostBack)
        {   //初始化日期
            this.txtCreate_dateF.Text = DateTime.Now.Year.ToString() + "/01/01";
            this.txtCreate_dateT.Text = DateTime.Now.ToShortDateString();
          //  this.txtBaojia_end_dateFrom.Text = DateTime.Now.ToString("yyyy/MM")+"/01";
          //  this.txtBaojia_end_dateTo.Text = DateTime.Now.ToString("yyyy/MM/dd"); //Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy/MM")+"/01").AddDays(-1).ToString();
            //初始化项目大小
            ini_Project_Size();
            ///项目状态 合同状态下拉
            ini_HeTong_Status();
            //顾客
            bindCust();
            //销售人员
            bindSales();
            //var ListItem = ddlHetong_status.Items.FindByText("争取");
            //if (ListItem != null) {
            //     ListItem.Selected=true;
            //}
            
        }
    }
 
    /// 项目大小下拉
    public void ini_Project_Size()
    { 
        string strSQL = @"select null as lookup_code,'' as lookup_desc  union all SELECT [lookup_code] ,[lookup_desc]  FROM [MES].[dbo].[Baojia_lookup_table] where lookup_type='project_size' and status='Y'";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        BaseFun fun = new BaseFun();
        fun.initDropDownList(this.ddlProject_size, dt, "lookup_code", "lookup_desc");
    }


    /// 零件状态下拉
    public void ini_HeTong_Status()
    {
        string strSQL = @" SELECT [lookup_code] ,[lookup_desc]  FROM [MES].[dbo].[Baojia_lookup_table] where lookup_type='lj_status_q' and status='Y' order by lookup_code";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        BaseFun fun = new BaseFun();
        fun.initDropDownList(this.ddlHetong_status, dt, "lookup_code", "lookup_desc");
    }
    //顾客状态下拉列表
    public void bindCust()
    {
        string strSQL = @"select distinct customer_name from [Baojia_mst]";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        selectCust.DataTextField = dt.Columns[0].ColumnName;
        selectCust.DataValueField = dt.Columns[0].ColumnName;
        this.selectCust.DataSource = dt;
        this.selectCust.DataBind();

        //var rows = dt.AsEnumerable()
        //    .OrderByDescending(t=>t.Field<string>("customer_name")).Select(p=>new { customer_name=p.Field<string>("customer_name") });

        //selectCust.DataTextField = "customer_name";
        //selectCust.DataValueField = "customer_name";
        //this.selectCust.DataSource = rows;
        //this.selectCust.DataBind();

        //var q = from t in dt.AsEnumerable()
        //        select t;
         
        //foreach(var row in q)
        //{  }
    }
    //销售人员下拉列表
    public void bindSales()
    {
        string strSQL = @"select distinct sales_name from [Baojia_mst]";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        this.dropSales_Name.DataTextField = dt.Columns[0].ColumnName;
        this.dropSales_Name.DataValueField = dt.Columns[0].ColumnName;
        this.dropSales_Name.DataSource = dt;
        this.dropSales_Name.DataBind();
        this.dropSales_Name.Items.Insert(0,"");
    }
    ///<summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();
        sbSQL.Append("exec [Baojia_Query] ");
        sbSQL.Append("'" + this.txtBaojia_no.Text.Trim() + "',");
        sbSQL.Append("'" + this.ddlTurns.SelectedValue + "',");
        sbSQL.Append("'" + this.txtCustomer_name.Text+"',");
        sbSQL.Append("'" + this.txtCustomer_name.Text + "',");
        sbSQL.Append("'" + this.txtCustomer_project.Text + "',");
        sbSQL.Append("'" + this.ddlProject_size.SelectedValue+"',");
        sbSQL.Append("'" + this.ddlProject_size.SelectedItem.Text + "',");
        sbSQL.Append("'" + this.ddlProject_level.SelectedItem.Text+"',");
        sbSQL.Append("'" + this.ddlbaojia_status.SelectedItem.Text+"',");
        sbSQL.Append("'" + this.ddlHetong_status.SelectedItem.Text+"',");
        sbSQL.Append("'" + this.txtBaojia_end_dateFrom.Text.Trim()+"',");
        sbSQL.Append("'" + this.txtBaojia_end_dateTo.Text.Trim() + "',");
        sbSQL.Append("'" + ""+"',"); //--姓名
        sbSQL.Append("'" + this.txtCreate_dateF.Text.Trim()+"',");
        sbSQL.Append("'" + this.txtCreate_dateT.Text.Trim() + "',");
        sbSQL.Append("'" + this.txtLjh.Text.Trim()+"',");
        sbSQL.Append("'" + this.txtLj_Name.Text.Trim() + "',");
        sbSQL.Append("'" + this.dropSales_Name.Text.Trim() + "'");

        DataTable dt=DbHelperSQL.Query(sbSQL.ToString()).Tables[0];

        //处理查无资料结果显示效果：添加一行显示友好一点
        bool isEmpty = false;
        if (dt.Rows.Count == 0)
        {
           isEmpty = true;
           dt.Rows.Add(dt.NewRow());
        }        
        
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        
        if (isEmpty == true)
        {
            int columnCount = dt.Columns.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "没有记录";
            GridView1.Rows[0].Cells[0].Style.Add("text-align", "center");
        };

        if (isEmpty ==false)
        {            
            int[] cols= {0,1,2,3,4,5,6,25,7,20,21,22,23,24,26,18,19, };
            MergGridRow.MergeRow(GridView1, cols);

           //设定合并单元格后要显示连接的requestid
            int visbleRow = 0;
            for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
            {
                if (GridView1.Rows[j].Cells[0].Visible == true)
                {
                    visbleRow = j;
                }
                else
                {
                    GridView1.Rows[visbleRow].Cells[27].Text = GridView1.Rows[j].Cells[27].Text;
                }

            }

            int rowIndex = 1;
            for(int j = 0; j <= GridView1.Rows.Count - 1; j++)
            {              
                if (GridView1.Rows[j].Cells[0].Visible ==true)
                {                    
                    GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();   
                    HyperLink link = new HyperLink();
                    link.ID = "link" + rowIndex.ToString();
                    link.NavigateUrl = "Baojia.aspx?requestid=" + GridView1.Rows[j].Cells[27].Text;
                    link.Text = GridView1.Rows[j].Cells[1].Text;
                    link.Target = "_blank";
                    GridView1.Rows[j].Cells[1].Controls.Add(link);                 
                    rowIndex++;
                }
                 
                
            }
        }
       
    }
    

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //重新绑定行号   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[27].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[27].Style.Add("display", "none");
            e.Row.Cells[6].Style.Add("word-break","break-all");//会有连续不自动换行的数据

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Button1_Click(sender, e);
    }
    ////获取顾客下拉列表
    //[System.Web.Services.WebMethod()]//或[WebMethod(true)]
    //public static string getCustomerList()
    //{
    //    string result = "[";
    //    DataSet ds = DbHelperSQL.Query("select distinct customer_name from [Baojia_mst]");
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            result = result+ "{\"value\":\"" + ds.Tables[0].Rows[i][0].ToString() + "\",\"text\":\"" + ds.Tables[0].Rows[i][0].ToString() + "\"},";
    //        }
    //    }
    //    result = result.TrimEnd(',') + "]";
    //    return result;
    //}

    //[WebMethod(true)]//或[WebMethod(true)]
    //public static string InsertEquipLogStatus(string equipno, string logaction, string actionmark, string actionreason)
    //{
    //    MES.DAL.MES_EquipActionLog dal = new MES.DAL.MES_EquipActionLog();
    //    string result = dal.Add(new MES.Model.MES_EquipActionLogModel() { equip_no = equipno, logaction = logaction, actionmark = actionmark, actionreason = actionreason });

    //    return result;
    //}

}

