using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YangJian_open_DelayDetailByPerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblName.Text = Request.QueryString["name"];
        ShowDelayDetailsByPerson(Request.QueryString["year"], Request.QueryString["month"], Request.QueryString["fac"], Request.QueryString["personNo"]);
    }
    int rownum = 0;
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            rownum = rownum + 1;
            e.Row.Cells[0].Text = rownum.ToString();
            e.Row.Cells[9].Style.Add("display", "none");
            e.Row.Cells[12].Style.Add("display", "none");

            if (e.Row.Cells[10].Text.ToString() == "订舱申请")
            {
                if (e.Row.Cells[12].Text.ToString() == "" || e.Row.Cells[12].Text.ToString().Trim() == "&nbsp;")
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                }
                else
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[12].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                }

            }
            else if (e.Row.Cells[10].Text.ToString() == "订舱处理")
            {
                if (e.Row.Cells[12].Text.ToString() == "" || e.Row.Cells[12].Text.ToString().Trim() == "&nbsp;")
                {

                }
                else
                {
                    e.Row.Cells[1].Text = "<a href='../dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[12].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

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
            e.Row.Cells[12].Style.Add("display", "none");
        }
    }

    public void ShowDelayDetailsByPerson(string year, string month,string fac, string personNo)
    {
        //[rpt_Form1_sale_YJ_Delay_TJ_CountByPerson]  '2017','4','','01715'
        DataSet ds = DbHelperSQL.Query("exec rpt_Form1_sale_YJ_Delay_TJ_CountByPerson '" + year + "','" + month + "','" + fac + "','" + personNo + "'");

        gvDetail.DataSource = ds.Tables[0];
        gvDetail.DataBind();

      
    }
}