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

public partial class JingLian_ZYB_JY_Query : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_year.DataSource = Function_Jinglian.ZYB_JY_query(3, "", "", "", "", "", "", "", "", "");
            this.txt_year.DataValueField = "year";
            this.txt_year.DataBind();

            this.txt_czr.DataSource = Function_Jinglian.ZYB_JY_query(4, "", "", "", "", "", "", "", "", "");
            this.txt_czr.DataValueField = "emp_name";
            this.txt_czr.DataTextField = "emp_name";
            this.txt_czr.DataBind();
            this.txt_czr.Items.Insert(0, new ListItem("", ""));

           

            this.ddl_zyb.DataSource = Function_Jinglian.ZybClear_Content_Query("3");
            this.ddl_zyb.DataTextField = "equip_no";
            this.ddl_zyb.DataValueField = "equip_no";
            this.ddl_zyb.DataBind();
            this.ddl_zyb.Items.Insert(0, new ListItem("", ""));

            this.ddl_luhao.DataSource = Function_Jinglian.ZybClear_Content_Query("5");
            this.ddl_luhao.DataTextField = "equip_no";
            this.ddl_luhao.DataValueField = "equip_no";
            this.ddl_luhao.DataBind();
            this.ddl_luhao.Items.Insert(0, new ListItem("", ""));
            string time = DateTime.Now.ToString("HHmm");
            string banbie = "0";

            if (string.Compare(time, "0800") > 0 && string.Compare(time, "2000") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            ddl_hejin.SelectedValue = banbie;
            txt_month.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 12; i++)
            {
                this.txt_month.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            DataTable dt = Function_Jinglian.ZYB_JY_query(2, txt_czr.Text, ddl_luhao.Text,txt_zyb.Text, ddl_hejin.Text, ddl_zyb.Text, txt_startdate.Text, txt_enddate.Text, txt_year.Text, txt_month.Text);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt == null || dt.Rows.Count <= 0)
            {
                DIV1.Style.Add("display", "none");
                DIV2.Style.Add("display", "none");
            }
            else
            {
                DIV1.Style.Add("display", "block");
                DIV2.Style.Add("display", "block");

            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = Function_Jinglian.ZYB_JY_query(2, txt_czr.Text, ddl_luhao.Text, txt_zyb.Text, ddl_hejin.Text, ddl_zyb.Text, txt_startdate.Text, txt_enddate.Text, txt_year.Text, txt_month.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text.ToString().Replace("&nbsp;", "") != "")
            {
                decimal jg = decimal.Parse(e.Row.Cells[8].Text);
                if (jg > 4)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                else if (jg > 2 && jg <= 4)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
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
        DataTable dt = Function_Jinglian.ZYB_JY_query(2, txt_czr.Text, ddl_luhao.Text, txt_zyb.Text, ddl_hejin.Text, ddl_zyb.Text, txt_startdate.Text, txt_enddate.Text, txt_year.Text, txt_month.Text);

        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = Function_Jinglian.ZYB_JY_query(2, txt_czr.Text, ddl_luhao.Text, txt_zyb.Text, ddl_hejin.Text, ddl_zyb.Text, txt_startdate.Text, txt_enddate.Text, txt_year.Text, txt_month.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (dt == null || dt.Rows.Count <= 0)
        {
            DIV1.Style.Add("display", "none");
            DIV2.Style.Add("display", "none");

        }
        else
        {
            DIV1.Style.Add("display", "block");
            DIV2.Style.Add("display", "block");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}