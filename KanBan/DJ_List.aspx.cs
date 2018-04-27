using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class KanBan_DJ_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Timer1.Enabled = true;

        Timer1.Interval = 20000;


        GetData();

    }
    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["connstringwt"];
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string status = e.Row.Cells[9].Text;

            string dh = e.Row.Cells[0].Text;
            // string djtime = e.Row.Cells[9].Text;
            if (status != "")
            {
                if (status == "red")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                else if (status == "yellow")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }

            }
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        GetData();
    }
    private void GetData()
    {
        DataSet ds = Query("exec KanBan_DJQuery ");
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (this.GridView1.PageCount > 1)
        {

            if (this.GridView1.PageIndex == this.GridView1.PageCount - 1)
            {

                this.GridView1.PageIndex = 0;

                GetData();

            }

            else
            {

                this.GridView1.PageIndex = this.GridView1.PageIndex + 1;

                GetData();

            }

        }
    }
}