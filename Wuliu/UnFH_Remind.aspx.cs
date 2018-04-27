using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;


public partial class Wuliu_UnFH_Remind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   //初始化日期
            //this.txtCreate_dateT.Text = DateTime.Now.Year.ToString() + "/01/01";
            this.txtCreate_dateT.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected DataSet GetDS()
    {
        DataSet ds = Query("exec UnFaHuo_Remin '" +dropcomp.SelectedValue + "','" + txtCreate_dateT.Text + "'");
        return ds;
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[1].Style.Add("width", "140px");
        //}
    }
}