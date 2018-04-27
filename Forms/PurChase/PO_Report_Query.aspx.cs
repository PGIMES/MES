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
            QueryASPxGridView();
        }
        else
        {
            DataTable dt = Pgi.Auto.Control.AgvToDt(this.GV_PART);
            this.GV_PART.Columns.Clear();
            Pgi.Auto.Control.SetGrid("Pur_PO_Query", "", this.GV_PART, dt);
           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        //
        DataTable dt = DbHelperSQL.Query("exec Pur_PO_Query  '','','',''").Tables[0];
        this.GV_PART.Columns.Clear();
        Pgi.Auto.Control.SetGrid("PO_Query", "", this.GV_PART, dt);
       


    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {


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