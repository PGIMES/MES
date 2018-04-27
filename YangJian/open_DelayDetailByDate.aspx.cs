using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YangJian_open_DelayDetailByDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblName.Text = Request.QueryString["year"]+"-"+Request.QueryString["month"]+ (Request.QueryString["day"]==""?"":("-"+ Request.QueryString["day"]));
        ShowDelayDetailsByPerson(Request.QueryString["year"], Request.QueryString["month"], Request.QueryString["day"], Request.QueryString["fac"], Request.QueryString["type"]);
    }
    int rownum = 0;
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            rownum = rownum + 1;
            e.Row.Cells[0].Text = rownum.ToString();
            e.Row.Cells[9].Style.Add("display", "none");
            e.Row.Cells[13].Style.Add("display", "none");

            if (e.Row.Cells[11].Text.ToString() == "订舱申请")
            {
                if (e.Row.Cells[13].Text.ToString() == "" || e.Row.Cells[13].Text.ToString().Trim() == "&nbsp;")
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                }
                else
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[13].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                }

            }
            else if (e.Row.Cells[11].Text.ToString() == "订舱处理")
            {
                if (e.Row.Cells[13].Text.ToString() == "" || e.Row.Cells[13].Text.ToString().Trim() == "&nbsp;")
                {

                }
                else
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[13].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                }
            }
            else
            {
                e.Row.Cells[1].Text = "<a href='yangjian.aspx?requestid=" + e.Row.Cells[9].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";
            }
            e.Row.Cells[1].Width = Unit.Parse("100px");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Style.Add("display", "none");
            e.Row.Cells[13].Style.Add("display", "none");
        }
    }




    public void ShowDelayDetailsByPerson(string year, string month, string day,string fac,string type)
    {
        //[rpt_Form1_sale_YJ_Delay_TJ_CountByPerson]  '2017','4','','01715'
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ_CountBydate '" + year + "','" + month + "','" + day + "','" + fac + "','"+type+"'");
              
        gvDetail.DataSource = ds.Tables[0];
        gvDetail.DataBind();

    }
}