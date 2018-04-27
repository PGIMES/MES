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

public partial class JingLian_Job_Monitor : System.Web.UI.Page
{
    API api = new API();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = api.GP_JobList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataTable dt = api.GP_JobList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string flag = e.Row.Cells[1].Text;
            string stopped = e.Row.Cells[7].Text;
            string lined = e.Row.Cells[8].Text;
            //if (flag == "FIN")
            //{
            //    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
            //}
            if (lined == "True")
            {

                ((Image)e.Row.FindControl("Image2")).ImageUrl = "~/images/停线.ico";
            }
            else if (stopped == "True")
            {
                ((Image)e.Row.FindControl("Image2")).ImageUrl = "~/images/停机.ico";
            }
            else
            {
                e.Row.Cells[4].Text = "";
            }


            //switch (e.Row.Cells[10].Text.Replace("&nbsp;", ""))
            //{
            //    case "":
            //        ((Image)e.Row.FindControl("Image1")).ImageUrl = "~/images/unstart.ico";
            //        //((Image)e.Row.Cells[1].Controls[0]).ImageUrl = "~/images/start.JPG";
            //        break;
            //    //case "IPG":
            //    //    //((Image)e.Row.Cells[1].Controls[0]).ImageUrl = "~/images/start.JPG";
            //    //    ((Image)e.Row.FindControl("Image1")).ImageUrl = "~/images/start.ico";
            //    //    break;
            //    //case "ORD":
            //    //    ((Image)e.Row.FindControl("Image1")).ImageUrl = "~/images/unstart.ico";
            //    //   // ((Image)e.Row.Cells[1].Controls[0]).ImageUrl = "~/images/unstart.jpg";
            //    //    break;
            //    //case "INI":
            //    //    //((Image)e.Row.Cells[1].Controls[0]).ImageUrl = "~/images/unstart.jpg";
            //    //    ((Image)e.Row.FindControl("Image1")).ImageUrl = "~/images/unstart.ico";
            //    //    break;
            //    default:
            //        ((Image)e.Row.FindControl("Image1")).ImageUrl = "~/images/start.ico";
            //        break;
            //}
        }
    }
}