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
using System.Security.Principal;
public partial class JingLian_JinLian_Query : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_startdate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_enddate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HHmm");
            string banbie = "0";

            if (string.Compare(time, "0800") > 0 && string.Compare(time, "2000") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            txt_banzu.SelectedValue = banbie;

            DataTable dt = Function_Jinglian.Hydrogen_Query_ByGW(1, txt_zybno.Text, txt_hejin.Text, txt_startdate.Text, txt_enddate.Text, txt_banzu.Text, txt_luhao.Text, txt_czg.Text,Request["gongwei"]);
           // DataTable dt = Function_Jinglian.Hydrogen_Query("", "", "", "", "", "", "");
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
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = Function_Jinglian.Hydrogen_Query_ByGW(1, txt_zybno.Text, txt_hejin.Text, txt_startdate.Text, txt_enddate.Text, txt_banzu.Text, txt_luhao.Text, txt_czg.Text, Request["gongwei"]);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string result = e.Row.Cells[17].Text.ToString();
            if (result == "NG")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
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
        DataTable ldt2 = Function_Jinglian.Hydrogen_Query_ByGW(1, txt_zybno.Text, txt_hejin.Text, txt_startdate.Text, txt_enddate.Text, txt_banzu.Text, txt_luhao.Text, txt_czg.Text, Request["gongwei"]);
        DataView dv = ldt2.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = Function_Jinglian.Hydrogen_Query_ByGW(1, txt_zybno.Text, txt_hejin.Text, txt_startdate.Text, txt_enddate.Text, txt_banzu.Text, txt_luhao.Text, txt_czg.Text, Request["gongwei"]);
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}