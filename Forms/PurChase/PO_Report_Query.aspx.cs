using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using DevExpress.Web;
using System.Drawing;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;

public partial class Forms_PurChase_PO_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {   //初始化日期
           
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
           
        }
        QueryASPxGridView();
        //else
        //{
        //    DataTable dt = Pgi.Auto.Control.AgvToDt(this.GV_PART);
        //   this.GV_PART.Columns.Clear();
        //    Pgi.Auto.Control.SetGrid("PO_Query", "", this.GV_PART, dt);
           
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        //
        DataTable dt = DbHelperSQL.Query("exec Pur_PO_Query '" + drop_type.SelectedValue + "', '" + txtDateFrom.Text + "','" + txtDateTo.Text + "'").Tables[0];
        this.GV_PART.Columns.Clear();
        Pgi.Auto.Control.SetGrid("PO_Query", "", this.GV_PART, dt);
       


    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        ///Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&stepid={1}&instanceid={0}&groupid={2}&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e&display=1
        if (e.RowType == GridViewRowType.Data)
        {
            if (this.GV_PART.DataColumns["PoNo"] != null)
            {
                int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["PoNo"]).VisibleIndex;
                string PoNo = Convert.ToString(e.GetValue("PoNo"));
                string groupid = Convert.ToString(e.GetValue("GroupID"));
                string stepid = Convert.ToString(e.GetValue("StepID"));
                //e.Row.Cells[index].Text = "<a href='http://172.16.5.26:8030/Forms/MaterialBase/ToolKnife.aspx?instanceid=" + e.GetValue("wlh") + "&domain=" + site + "' target='_blank'>" + value.ToString() + "</a>";
                e.Row.Cells[index].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e&instanceid=" + e.GetValue("PoNo") + "&stepid=" + stepid + "&groupid=" + groupid + "&display=1' target='_blank'>" + PoNo.ToString() + "</a>";

            }
        }
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

       


    }
    protected void GV_PART_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
       
        
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {

        
    }
    protected void GV_PART_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "编号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
    }
}