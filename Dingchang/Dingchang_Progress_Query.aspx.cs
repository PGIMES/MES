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

public partial class dingchang_Dingchang_Progress_Query : System.Web.UI.Page
{
    BaseFun fun = new BaseFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strsql = "select distinct year(tjdate_sale)year from form2_Sale_DC_MainTable order by year(tjdate_sale)desc";
            DataSet dsYear = DbHelperSQL.Query(strsql);
            fun.initDropDownList(dropYear, dsYear.Tables[0], "year", "year");
            string struid = "select value+' '+CLASS_NAME  as uid from [dbo].[form1_Sale_YJ_BASE] where BASE_ID in ('11','12')union select distinct Userid+' '+UserName as uid from form2_Sale_DC_MainTable";
            DataSet dsuid = DbHelperSQL.Query(struid);
            fun.initDropDownList(dropUser, dsuid.Tables[0], "uid", "uid");
            this.dropUser.Items.Insert(0, new ListItem("", ""));
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GetGrid();
    }
    public void GetGrid()
    {
        if (txt_qadno.Text == "" && txt_yjno.Text == "")
        {
            DataSet ds = DbHelperSQL.Query("exec MES_DCReport_Query_1 '" + dropYear.SelectedValue + "','" + txt_hyno.Text + "','" + dropstatus.SelectedValue + "','" + txtfyrq.Text + "','" + txtthrq.Text + "','','" + Droptype.SelectedValue + "','" + dropUser.SelectedValue + "','" + txtfinish.Text + "'");
            DataTable dt = ds.Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            DataSet ds = DbHelperSQL.Query("exec MES_DCReport_Query_2 '" + dropYear.SelectedValue + "','" + txt_hyno.Text + "','" + dropstatus.SelectedValue + "','" + txtfyrq.Text + "','" + txtthrq.Text + "','','" + Droptype.SelectedValue + "','" + dropUser.SelectedValue + "','" + txtfinish.Text + "','" + txt_yjno.Text + "','" + txt_qadno.Text + "'");
            DataTable dt = ds.Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetGrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HyperLink)e.Row.Cells[0].FindControl("HyperLink1")).Text != "")
            {
                HyperLink HyperLink = e.Row.FindControl("HyperLink1") as HyperLink;
                HyperLink.Attributes.Add("OnClick", "if(window.open(encodeURI('DC_Apply.aspx?requestid=" + e.Row.Cells[0].Text.ToString() + "'))) return false; ");
            }
        }
    }
}