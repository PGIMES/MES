using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;

public partial class YangJian_Yangjian_DelayShip_WASFH_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder(Request["type"]);
        string title = builder.Replace("1", "计划发货批次").Replace("2", "计划发货数量").Replace("3", "按时发货批次").Replace("4", "未按时发货批次").Replace("5", "发货及时率") + "明细";

        this.lblName.Text = Request.QueryString["year"] + "-" + Request.QueryString["month"] + (Request.QueryString["day"] == "" ? "" : ("-" + Request.QueryString["day"]))+title;
        ShowDelayShipDetails(Request["year"], Request["month"], Request["day"], Request["type"]);

           
    }
    public void ShowDelayShipDetails(string year,string month,string day,string type)
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_DelayShip_Details(year,month,day,type);
        this.GridViewDelayShip.DataSource = dt;
        GridViewDelayShip.DataBind();
    }
    protected void GridViewDelayShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = "<a href='yangjian.aspx?requestid=" + e.Row.Cells[14].Text + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";
            e.Row.Cells[3].Text = string.Format("{0:d}",Convert.ToDateTime(e.Row.Cells[3].Text));
            e.Row.Cells[9].Text = string.Format("{0:d}", Convert.ToDateTime(e.Row.Cells[9].Text));
            e.Row.Cells[10].Text = string.Format("{0:d}", Convert.ToDateTime(e.Row.Cells[10].Text));
            e.Row.Cells[4].Width = Unit.Parse("100px");
        }
        //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[14].Visible = false;
        }
    }
    }