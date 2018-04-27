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

public partial class GongCheng_DJ_LY : System.Web.UI.Page
{
    string xmh = "";
    string part = "";
    string comp = "";
    string year = "";
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
   
        if (!IsPostBack)
        {
            DataSet ds = Query("exec  GetSL_DJ '" + comp + "','" + year_start + "','" + mnth_start + "','" + year_end + "','" + mnth_end + "','"+part.TrimEnd()+"','" + xmh.TrimEnd() + "'");

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

}

