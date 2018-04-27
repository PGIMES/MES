using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TJ_Guzhang_Rate_Report : System.Web.UI.Page
{
    decimal heji = 0;
    BaseFun fun = new BaseFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }

            //初始化月份
            dropMonth.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化月份
            dropDay.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 31; i++)
            {
                dropDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropDay.SelectedValue = DateTime.Now.Day.ToString();
            //init 合金
            DataSet ds = Query("select   Pos_key,Pos_key+'-'+Pos_name as Pos_name  from [dbo].[position] where  charindex('-',pos_key)=0  and (Pos_key like '%K1C%' or Pos_key like '%K1M%' OR Pos_key like '%S1M%')");
            fun.initDropDownList(ddl_area, ds.Tables[0], "Pos_key", "Pos_name");
            this.ddl_area.Items.Insert(0, new ListItem("", ""));
            QueryYear();
            txtmonth.Text = DateTime.Now.ToString("yyyyMM");
            txtday.Text = DateTime.Now.ToString("dd");
            QueryMonth(DateTime.Now.Month.ToString());
            QueryDay( DateTime.Now.ToString("MMdd"));
           // QueryMonth(DateTime.Now.Day.ToString());

        }
    }


    //GuZhang_Rate_Query '年','2017','1','comp','area'
    public void QueryYear()
    {
        DataSet ds = Query("exec GuZhang_Rate_Query '年','" + this.dropYear.SelectedValue + "','','"+dropcomp.SelectedValue+"','','"+ddl_area.SelectedValue+"'");

        GridViewYear.DataSource = ds.Tables[0];
        GridViewYear.DataBind();
        lblYear.Text = dropYear.SelectedValue + "年";

        ViewState["tblYear_stopped"] = ds.Tables[1];
        ViewState["tblYear_Nostopped"] = ds.Tables[2];
        bindChartYear_Stopped(ds.Tables[1]);
        bindChartYear_NoStopped(ds.Tables[2]);
       

    }
    public void bindChartYear_Stopped(DataTable tbl)
    {
        Chart_ztjsc.DataSource = tbl;
       
        
            Chart_ztjsc.Series["Series2"].XValueMember = "mnth_day";
            Chart_ztjsc.Series["Series2"].YValueMembers = "gz_sc";
          
       
        
    }
    public void bindChartYear_NoStopped(DataTable tbl)
    {
        Chart_zntjsc.DataSource = tbl;
        
            Chart_zntjsc.Series["Series2"].XValueMember = "mnth_day";
            Chart_zntjsc.Series["Series2"].YValueMembers = "gz_sc";
           
      
       
    }

    public void bindChartMonth_Stopped(DataTable tbl)
    {
        Chart_ztjsc_mnth.DataSource = tbl;
         
            Chart_ztjsc_mnth.Series["Series2"].XValueMember = "mnth_day";
            Chart_ztjsc_mnth.Series["Series2"].YValueMembers = "gz_sc";
           
       
      
        
    }
    public void bindChartMonth_NoStopped(DataTable tbl)
    {
        Chart_zntjsc_mnth.DataSource = tbl;
         
            Chart_zntjsc_mnth.Series["Series2"].XValueMember = "mnth_day";
            Chart_zntjsc_mnth.Series["Series2"].YValueMembers = "gz_sc";
           
       
       
    }
     public void QueryMonth(string month)
    {
        DataSet ds = Query("exec GuZhang_Rate_Query '月','" + this.dropYear.SelectedValue + "','" +month + "','" + dropcomp.SelectedValue + "','','" + ddl_area.SelectedValue + "'");
        GridViewMonth.DataSource = ds.Tables[0];
        GridViewMonth.DataBind();
        lblMonth.Text = txtmonth.Text + "月";
        ViewState["tblMonth_Stopped"] = ds.Tables[1];
        ViewState["tblMonth_Nostopped"] = ds.Tables[2];

        bindChartMonth_Stopped((DataTable)ViewState["tblMonth_Stopped"]);
        bindChartMonth_NoStopped((DataTable)ViewState["tblMonth_Nostopped"]);
        bindChartYear_Stopped((DataTable)ViewState["tblYear_stopped"]);
        bindChartYear_NoStopped((DataTable)ViewState["tblYear_Nostopped"]);
       

    }
protected void  LinkBtn_Click(object sender, EventArgs e)
{
     QueryMonth(txtmonth.Text);
}
protected void  LinkBtnDays_Click(object sender, EventArgs e)
{
    QueryDay(txtday.Text);
}
public void QueryDay(string day)
{
    DataSet ds = Query("exec GuZhang_Rate_Query '日','" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','" + dropcomp.SelectedValue + "','" + day + "','" + ddl_area.SelectedValue + "'");
    Getsum(ds.Tables[0]);
    GridViewDay.DataSource = ds.Tables[0];
    GridViewDay.DataBind();
    //lblDays.Text = txtmonth.Text + txtday.Text.PadLeft(2, '0');
    lblDays.Text = day;
    bindChartMonth_Stopped((DataTable)ViewState["tblMonth_Stopped"]);
    bindChartMonth_NoStopped((DataTable)ViewState["tblMonth_Nostopped"]);
    bindChartYear_Stopped((DataTable)ViewState["tblYear_stopped"]);
    bindChartYear_NoStopped((DataTable)ViewState["tblYear_Nostopped"]);
}
private void Getsum(DataTable ldt)
{
    for (int i = 0; i < ldt.Rows.Count; i++)
    {
        if (ldt.Rows[i][6].ToString() != "")
        {
            this.heji += Convert.ToDecimal(ldt.Rows[i][6].ToString());
        }


    }
}
protected void  GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
{
    e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            //e.Row.Cells[0].Width=200;
            for (int i = 1; i < e.Row.Cells.Count-2; i++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
                lbtn.Attributes.Add("name", "mon");
                e.Row.Cells[i].Controls.Add(lbtn);
            }

        }
        else
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

            }
            e.Row.Cells[0].Wrap = false;
        }
}
protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
{
    int year = int.Parse(dropYear.Text);
    int month = int.Parse(dropMonth.Text);
    int lastDayOfThisMonth = DateTime.DaysInMonth(year, month);
    dropDay.SelectedValue = lastDayOfThisMonth.ToString();

    if (dropMonth.Text == System.DateTime.Now.Month.ToString())
    {
        dropDay.SelectedValue = DateTime.Now.Day.ToString();
    }
    LinkBtn.Style.Add("display", "none");
    LinkBtnDays.Style.Add("display", "none");
    txtmonth.Style.Add("display", "none");
    txtday.Style.Add("display", "none");
}
protected void btnQuery_Click(object sender, EventArgs e)
{
    QueryYear();
    //month
    QueryMonth(dropMonth.SelectedValue);
    lblMonth.Text = dropYear.SelectedValue + dropMonth.SelectedValue + "月";
    //Day
    string day = dropMonth.SelectedValue.PadLeft(2, '0') + dropDay.SelectedValue.PadLeft(2, '0');
    QueryDay(day);
    lblDays.Text = day;
}
protected void GridViewMonth_RowCreated(object sender, GridViewRowEventArgs e)
{
    e.Row.Cells[1].Visible = false;//隐藏排序ord栏位
    if (e.Row.RowType == DataControlRowType.Header)
    {
        e.Row.Cells[0].Text = "";
        for (int i = 1; i < e.Row.Cells.Count-2; i++)
        {
            LinkButton lbtn = new LinkButton();
            lbtn.ID = "lbtn" + i.ToString();
            lbtn.Text = e.Row.Cells[i].Text;
            lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtnDays','')");
            lbtn.Attributes.Add("name", "day");
            lbtn.Style.Add("text-align", "center");
            e.Row.Cells[i].Wrap = false;
            e.Row.Cells[i].Controls.Add(lbtn);
        }
    }
    else
    {
        for (int i = 1; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;

        }
        e.Row.Cells[0].Wrap = false;
    }
    
}
public static DataSet Query(string SQLString)
{
    string connString = ConfigurationManager.AppSettings["connstringAPI"];
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
protected void dropcomp_SelectedIndexChanged(object sender, EventArgs e)
{
    if(dropcomp.SelectedValue!="")
    {
        DataSet ds = Query(" select   Pos_key,Pos_key+'-'+Pos_name as Pos_name  from [dbo].[position] where  charindex('-',pos_key)=0  and (Pos_key like '%K1C%' or Pos_key like '%K1M%' OR Pos_key like '%S1M%') and left(Pos_key,1)='" + dropcomp.SelectedValue + "'");
        fun.initDropDownList(ddl_area, ds.Tables[0], "Pos_key", "Pos_name");
        this.ddl_area.Items.Insert(0, new ListItem("", ""));
        LinkBtn.Style.Add("display", "none");
        LinkBtnDays.Style.Add("display", "none");
        txtmonth.Style.Add("display", "none");
        txtday.Style.Add("display", "none");
    }
}


protected void GridViewDay_RowCreated(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.Footer)
    {
        e.Row.Cells[5].Text = "合计";
        e.Row.Cells[6].Text = this.heji.ToString();
    }
}
protected void GridViewMonth_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
    {

        if (e.Row.RowIndex != -1)
        {
            int indexID = this.GridViewMonth.PageIndex * this.GridViewMonth.PageSize + e.Row.RowIndex + 1;

            if (indexID == 1 || indexID == 6)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
protected void GridViewYear_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
    {

        if (e.Row.RowIndex != -1)
        {
            int indexID = this.GridViewMonth.PageIndex * this.GridViewMonth.PageSize + e.Row.RowIndex + 1;

            if (indexID == 1 || indexID == 6)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
protected void GridViewDay_RowDataBound(object sender, GridViewRowEventArgs e)
{

}
protected void btn_export_Click(object sender, EventArgs e)
{
    string lsname = "设备报修统计报表(月)";
    DataSet ds = Query("exec GuZhang_Rate_Import '月','" + this.dropYear.SelectedValue + "','" + dropMonth.SelectedValue + "','" + dropcomp.SelectedValue + "','','" + ddl_area.SelectedValue + "'");
    DataTableToExcel(ds.Tables[0], "xls", lsname, "1");
}

public void DataTableToExcel(DataTable dt, string FileType, string FileName, string b_head)
{
    System.Web.HttpContext.Current.Response.Clear();
    System.Web.HttpContext.Current.Response.Charset = "UTF-8";
    System.Web.HttpContext.Current.Response.Buffer = true;
    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
    System.Web.HttpContext.Current.Response.ContentType = FileType;
    string colHeaders = string.Empty;
   
    
  
    string ls_item = string.Empty;
    DataRow[] myRow = dt.Select();
    int i = 0;
    int cl = dt.Columns.Count;
    if (b_head == "1")
    {
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            ls_item +=  dt.Columns[j].ColumnName + "\t";
        }
    }
    ls_item += "\n";
    foreach (DataRow row in myRow)
    {
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))
            {
                ls_item += row[i].ToString() + "\n";
            }
            else
            {
                ls_item += row[i].ToString() + "\t";
            }
        }
        System.Web.HttpContext.Current.Response.Output.Write(ls_item);
        ls_item = string.Empty;
    }

    System.Web.HttpContext.Current.Response.Output.Flush();
    System.Web.HttpContext.Current.Response.End();
}
}