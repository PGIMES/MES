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

public partial class Wuliu_WLYF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   //初始化日期

            DateTime DateNow = DateTime.Now.AddMonths(-1);
            DateTime DateBegin = new DateTime(DateNow.Year, DateNow.Month, 1);
            DateTime DateEnd = DateBegin.AddMonths(1).AddDays(-1);


            txtDateFrom.Text = DateBegin.ToString("yyyy-MM-dd");// DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            txtDateTo.Text = DateEnd.ToString("yyyy-MM-dd");
            QueryASPxGridView();
        }
        if (this.GV_PART.IsCallback)
        {
            QueryASPxGridView();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

        ASPxGridViewExporter1.WriteXlsToResponse("物流运费" + System.DateTime.Now.ToShortDateString());//导出到Excel

    }

    public void QueryASPxGridView()
    {
        //

        DataTable dt = DbHelperSQL.Query("exec qad.dbo.PGI_QAD_WLYF  '" + ddl_comp.SelectedValue + "','" + txtDateFrom.Text + "','" + txtDateTo.Text + "'").Tables[0];
        //Pgi.Auto.Control.SetGrid("DJLY", "", this.GV_PART, dt);
        Pgi.Auto.Control.SetGrid(this.GV_PART, dt,100);
        this.GV_PART.Columns[3].Width = 150;
        this.GV_PART.Columns[4].Width = 150;
        this.GV_PART.Columns[8].Width = 110;
        GV_PART.Width=1100;
    }




}