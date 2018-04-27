using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using DevExpress.Charts;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;
using System.Drawing;

public partial class GongCheng_Product_Byxmh : System.Web.UI.Page
{

    string xmh = "";
    string part = "";
    string comp = "";
    string year = "";
    string dj_group = "";
    int mnth_start = 0;
    int mnth_end = 0;
    int year_start = 0;
    int year_end = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["xmh"] != null)
        {
            xmh = Request["xmh"];
        }

        if (Request["part"] != null)
        {
            part = Request["part"];
        }
        if (Request["comp"] != null)
        {
            comp = Request["comp"];
        }
        if (Request["startmnth"] != null)
        {
            mnth_start = int.Parse(Request["startmnth"]);
        }
        if (Request["endmnth"] != null)
        {
            mnth_end = int.Parse(Request["endmnth"]);

        }
        if (Request["startyear"] != null)
        {
            year_start = int.Parse(Request["startyear"]);
        }
        if (Request["endyear"] != null)
        {
            year_end = int.Parse(Request["endyear"]);

        }
        if (Request["dj_group"] != null)
        {
            dj_group = Request["dj_group"];
        }
        DataSet ds;

        if (!IsPostBack)
        {


            ds = Query("exec  GetSL_DJ_xmh '" + comp + "','" + year_start + "','" + mnth_start + "','" + year_end + "','" + mnth_end + "','" + xmh.TrimEnd() + "'");
      
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();




        }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string lsname = xmh.TrimEnd()+"产品的刀具领用数明细";
       DataSet ds = Query("exec  GetSL_DJ_xmh '" + comp + "','" + year_start + "','" + mnth_start + "','" + year_end + "','" + mnth_end + "','" + xmh.TrimEnd() + "'");

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