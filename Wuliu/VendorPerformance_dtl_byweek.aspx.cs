using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class VendorPerformance_dtl_byweek : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string vendor = Request["vendor"];
            string week = Request["week"];
            string domain = Request["domain"];
            string year = Request["year"];
            //欠交
            GetDelayQty( vendor,  domain,  week,  year);
            //超交
            GetOverQty( vendor,  domain,  week,  year);
            //周需求
            GetWeekRequest( vendor,  domain,  week,  year);
        }
    }
    //欠交
    protected void GetDelayQty(string vendor,string domain,string week,string year)
    {   //[dbo].[SUP_VendorPerformance_dtlByWeek]  
        //@ptype varchar(50)='',--1.周需求,2.超交，3.欠交
        //@domain varchar(20)='200',
        //@vendor varchar(50)='K30020',
        //@week varchar(20)='',
        //@year varchar(20)
        DataSet ds = DbHelperSQL.Query("exec QAD.dbo.SUP_VendorPerformance_dtlByWeek 3,'" + domain + "','" + vendor + "','" + week + "','" + year + "'");
        GridView1.DataSource= ds;
        GridView1.DataBind();
    }
    //超交
    protected void GetOverQty(string vendor, string domain, string week, string year)
    {
        DataSet ds = DbHelperSQL.Query("exec QAD.dbo.SUP_VendorPerformance_dtlByWeek 2,'" + domain + "','" + vendor + "','" + week + "','" + year + "'");
        GridView2.DataSource = ds;
        GridView2.DataBind();
    }

    //周需求
    protected void GetWeekRequest(string vendor, string domain, string week, string year)
    {
        DataSet ds = DbHelperSQL.Query("exec QAD.dbo.SUP_VendorPerformance_dtlByWeek 1,'" + domain + "','" + vendor + "','" + week + "','" + year + "'");
        GridView3.DataSource = ds;
        GridView3.DataBind();
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
                ls_item += dt.Columns[j].ColumnName + "\t";
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

    Single totalQ=0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            totalQ = totalQ + Convert.ToSingle(e.Row.Cells[11].Text.Replace(",", ""));
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = "Toal:" + string.Format("{0:N0}", totalQ);
            e.Row.HorizontalAlign = HorizontalAlign.Right;
        }
    }
    Single totalC = 0;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            totalC = totalC + Convert.ToSingle(e.Row.Cells[11].Text.Replace(",", ""));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = "Toal:" + string.Format("{0:N0}", totalC);
            e.Row.HorizontalAlign = HorizontalAlign.Right;
        }
    }
    Single totalZ = 0;
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            totalZ = totalZ + Convert.ToSingle(e.Row.Cells[6].Text.Replace(",", ""));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Toal:" + string.Format("{0:N0}", totalZ);
            e.Row.HorizontalAlign = HorizontalAlign.Right;
        }
    }
}