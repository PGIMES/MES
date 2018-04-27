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

public partial class MoJu_MojuBX_Query : System.Web.UI.Page
{
    Moju Moju = new Moju();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                txt_year.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }
           txt_month.Items.Add(new ListItem { Value = "", Text = "" });
            for (int i = 1; i <= 12; i++)
            {
                this.txt_month.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            DataTable dt = Moju.Moju_BX_Query(1, "", txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_bxr.Text, txt_wxr.Text, txt_mojuno.Text, txt_moju_type.Text, txt_ljmc.Text, txt_sbjc.Text, txt_result.Text);
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
        DataTable dt = Moju.Moju_BX_Query(1, "", txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_bxr.Text, txt_wxr.Text, txt_mojuno.Text, txt_moju_type.Text, txt_ljmc.Text, txt_sbjc.Text, txt_result.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = Moju.Moju_BX_Query(1, "", txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_bxr.Text, txt_wxr.Text, txt_mojuno.Text, txt_moju_type.Text, txt_ljmc.Text, txt_sbjc.Text, txt_result.Text);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         int lnindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        string dh = ((LinkButton)this.GridView1.Rows[lnindex].FindControl("LinkButton1")).Text;
        string URL = "Moju_BX_Detail.aspx?dh=" + dh + "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>window.open('" + URL + "','_blank')</script>");
     //  Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>window.location.href='Moju_BX_Detail.aspx?dh="+dh+"';</script>");
      

    }
}