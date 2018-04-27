using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Maticsoft.DBUtility;


public partial class Select_select_XMH : System.Web.UI.Page
{
    Function_Base Function_Base = new Function_Base();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }

    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        GetData();
    }
   
    private void GetData()
    {
      
        DataTable dt = new DataTable();
        string strsql = "select * from Qad_pt_mstr where left(pt_part,1)='P' AND pt_part like '%" + txt_xmh.Text + "%'";
         dt = Query(strsql).Tables[0];
       // dt = Function_Base.Getxmh(1, txt_xmh.Text);
        if (dt == null || dt.Rows.Count <= 0)
        {
            lb_msg.Text = "No Data Found!";
            //return;
        }
        GridView1.DataSource = dt;
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DFE7DF';");
            e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor=currentcolor;");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string xmh = GridView1.SelectedRow.Cells[0].Text.Trim();
        if (Request.QueryString["id"] != null)
        {
            string temp = "<script>window.opener.zy_setvalue('form1','" + xmh + "');</script>";
            Response.Write(temp.Trim());
        }
        else
        {
            string temp = "<script>window.opener.bz_setvalue('form1','" + xmh + "');</script>";
            Response.Write(temp.Trim());
        }
       
    }
}