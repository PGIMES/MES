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

public partial class JingLian_ChangeMo_Query : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    decimal heji = 0;
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
          
          
            
            if (Function_Jinglian.Emplogin_query(10, "", Request["deviceid"]).Rows.Count > 0)
            {
                txt_sbjc.Text = Function_Jinglian.Emplogin_query(10, "", Request["deviceid"]).Rows[0]["equip_name"].ToString();
            }
            else
            {
                txt_sbjc.Text = "";
            }
            if (Function_Jinglian.Emplogin_query(11, "", Request["deviceid"]).Rows.Count > 0)
            {
                txt_ljmc.Text = Function_Jinglian.Emplogin_query(11, "", Request["deviceid"]).Rows[0]["part"].ToString();
            }
            else
            {
                txt_ljmc.Text = "";
            }

            DataTable dt = Function_Jinglian.ChangeMo_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_sbjc.Text, txt_ljmc.Text, ddl_reason.Text, txt_moju_type.Text);
            Getsum3(dt);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = Function_Jinglian.ChangeMo_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_sbjc.Text, txt_ljmc.Text, ddl_reason.Text,txt_moju_type.Text);
        Getsum3(dt);
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = Function_Jinglian.ChangeMo_Query(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_sbjc.Text, txt_ljmc.Text, ddl_reason.Text, txt_moju_type.Text);
        Getsum3(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    private void Getsum3(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["timer"].ToString() != "")
            {
                this.heji += Convert.ToDecimal(ldt.Rows[i]["timer"].ToString());
            }

            
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "合计";
            e.Row.Cells[7].Text = this.heji.ToString();
        }
    }
}