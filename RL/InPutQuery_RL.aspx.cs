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

public partial class RL_InPutQuery_RL : System.Web.UI.Page
{
    Function_TouLiao_RL rl = new Function_TouLiao_RL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_startdate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_enddate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");

            txt_equip_name.SelectedValue = Request["deviceid"].ToString();
            string time = DateTime.Now.ToString("HHmm");

             string banbie = "0";

            if (string.Compare(time, "0800") > 0 && string.Compare(time, "2000") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            txt_banbie.SelectedValue  = banbie;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = rl.GouLiao_RL_query(txt_startdate.Text, txt_enddate.Text, txt_equip_name.SelectedValue, txt_hejin.SelectedValue, txt_banbie.SelectedValue, "");
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = rl.GouLiao_RL_query(txt_startdate.Text, txt_enddate.Text, txt_equip_name.SelectedValue, txt_hejin.SelectedValue, txt_banbie.SelectedValue, "");
      
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal a =decimal.Parse(e.Row.Cells[17].Text.Replace("%",""));
            decimal b = decimal.Parse(e.Row.Cells[18].Text.Replace("%", ""));
            decimal c = decimal.Parse(e.Row.Cells[19].Text.Replace("%", ""));

            decimal d = decimal.Parse(e.Row.Cells[16].Text.Replace("%", ""));

            if (d >=45&&d<=50 )
            {
                e.Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
            }
            else if (d<45)
            {
                 e.Row.Cells[16].BackColor = System.Drawing.Color.Red ;

            }



            //if ( (a >= 30&&a<=35) || (b <=25 &&b>=25)||( c <5&&c>0))
            //{
            //    e.Row.BackColor = System.Drawing.Color.Yellow;
            //}

            //if (a > 35 || b > 25 || c > 5)
            //{
            //    e.Row.BackColor = System.Drawing.Color.Red;
            //}


            //if (a <= 30)
            //{
            //    e.Row.Cells[16].BackColor = System.Drawing.Color.Green;
            //}
             if (a > 30 && a <= 35)
            {
                e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
            }
            else if (a > 35)
            {
                e.Row.Cells[17].BackColor = System.Drawing.Color.Red;
            }

            //if (b <= 20)
            //{
            //    e.Row.Cells[17].BackColor = System.Drawing.Color.Green;
            //}
             if (b > 20 && b <= 25)
            {
                e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow;
            }
            else if (b > 25)
            {
                e.Row.Cells[18].BackColor = System.Drawing.Color.Red;
            }

            if (c > 0 && c <= 5)
            {
                e.Row.Cells[19].BackColor = System.Drawing.Color.Yellow;
            }
            else if (c>5)
            {
                e.Row.Cells[19].BackColor = System.Drawing.Color.Red;
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
        DataTable ldt2 = rl.GouLiao_RL_query(txt_startdate.Text, txt_enddate.Text, txt_equip_name.SelectedValue, txt_hejin.SelectedValue, txt_banbie.SelectedValue, "");
        DataView dv = ldt2.DefaultView;
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
}