using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class JingLian_ZYB_Query : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_year.DataSource = Function_Jinglian.ZybClear_Query(2,"","","","","","");
            this.txt_year.DataTextField = "year";
            this.txt_year.DataValueField = "year";
            this.txt_year.DataBind();

            string time = DateTime.Now.ToString("HHmm");
            string banbie = "0";

            if (string.Compare(time, "0800") > 0 && string.Compare(time, "2000") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            txt_banbie.SelectedValue = banbie;
             txt_month.Items.Add(new ListItem { Value = "", Text = "" });
            for(int i = 1; i <= 12; i++)
           {      
                this.txt_month.Items.Add(new ListItem(i.ToString()));  
            }
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            DataTable dt = Function_Jinglian.ZybClear_Query(1, txt_year.Text,txt_month.Text,txt_startdate.Text,txt_enddate.Text,txt_banbie.Text,"");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt == null || dt.Rows.Count <= 0)
            {
                DIV1.Style.Add("display", "none");
            }
            else
            {
                DIV1.Style.Add("display", "block");

            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = Function_Jinglian.ZybClear_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_banbie.Text,"");
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[4].Text.ToString().Trim() == "NG" || e.Row.Cells[6].Text.ToString().Trim() == "NG" || e.Row.Cells[8].Text.ToString().Trim() == "NG" || e.Row.Cells[10].Text.ToString().Trim() == "NG")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            else if ((e.Row.Cells[4].Text.ToString().Trim() == "OK" && e.Row.Cells[5].Text.ToString().Replace("&nbsp;", "") != "") || (e.Row.Cells[6].Text.ToString().Trim() == "OK" && e.Row.Cells[7].Text.ToString().Replace("&nbsp;", "") != "") || (e.Row.Cells[8].Text.ToString().Trim() == "OK" && e.Row.Cells[9].Text.ToString().Replace("&nbsp;", "") != "") || (e.Row.Cells[10].Text.ToString().Replace("&nbsp;", "") == "OK" && e.Row.Cells[11].Text.ToString().Replace("&nbsp;", "") != ""))
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        DataTable dt = Function_Jinglian.ZybClear_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_banbie.Text,"");

        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = Function_Jinglian.ZybClear_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_banbie.Text,"");
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (dt == null || dt.Rows.Count <= 0)
        {
            DIV1.Style.Add("display", "none");

        }
        else
        {
            DIV1.Style.Add("display", "block");

        }
    }
}