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

public partial class DJ_DJ_JZ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();

            string strsql = "select distinct year  from MES_DJ_JZ order by year DESC";
            DataSet dsYear = Query(strsql);
            fun.initDropDownList(txt_year, dsYear.Tables[0], "year", "year");
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            txt_year.Items.Add(new ListItem { Value = year.ToString(), Text = year.ToString() });
            for (int i = 1; i < 13; i++)
            {
                txt_mnth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            txt_mnth.SelectedValue = DateTime.Now.Month.ToString();
        }
      
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = Query("exec  MES_DJ_JZ_Insert '2','" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue.Trim() + "','" + txt_mnth.SelectedValue + "','',''");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {//
            string str = "layer.confirm('所选月份已结转，请确认是否需重新结转？', {  btn: ['是','否'] }, function(){ $('#MainContent_btnNext').click(); }, function(){  });";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
        }
        else
        {
            btnNext_Click(sender, e);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        DataSet ds = Query("exec  MES_DJ_JZ_Insert '1','" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue + "','" + txt_mnth.SelectedValue + "','',''");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('结转成功！')", true);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
       

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

    protected void Button2_Click(object sender, EventArgs e)
    {
        DataSet ds = Query("exec  MES_DJ_JZ_Insert '2','" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue.Trim() + "','" + txt_mnth.SelectedValue + "','"+txt_xmh.Value+"','"+txt_part.Value+"'");

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = Query("exec  MES_DJ_JZ_Insert '2','" + ddl_comp.SelectedValue + "','" + txt_year.SelectedValue.Trim() + "','" + txt_mnth.SelectedValue + "','" + txt_xmh.Value + "','" + txt_part.Value + "'");

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }


   
}