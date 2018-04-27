using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class Wuliu_FH_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        
        }
    }
    protected DataSet GetDS()
    {
        DataSet ds = DbHelperSQL.Query("exec usp_qad_fhrecored '" + txt_id.Text + "','" + txt_startdate.Text + "','" + txt_enddate.Text + "','" + txt_shipfrom.Text + "','" + txt_shipto.Text + "','" + dropcomp.SelectedValue + "','"+txt_xmh.Text+"'");
        return ds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = GetDS();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = GetDS();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string lsname = "发运记录";
        DataSet ds = GetDS();
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
}