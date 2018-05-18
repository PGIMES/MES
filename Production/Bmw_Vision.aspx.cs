using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Web.Services;

public partial class Production_Bmw_Vision : System.Web.UI.Page
{

    public string m_sorder_id="";
    public string m_stable_name = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["order_id"] != null)
        {
            this.m_sorder_id = Request.QueryString["order_id"].ToString();
        }
        if (Request.QueryString["table_name"] != null)
        {
            this.m_stable_name = Request.QueryString["table_name"].ToString();
        }

        if (!IsPostBack)
        {
           
            //DataTable ldt = DbHelperSQL.Query("select distinct  substring(product_user, charindex('-',product_user)+1,LEN(product_user)-charindex('-',product_user)) as product_user from form3_Sale_Product_MainTable where product_user<>''").Tables[0];
            //this.txtproduct_user.DataValueField = "product_user";
            //this.txtproduct_user.DataTextField = "product_user";
            //this.txtproduct_user.DataSource = ldt;
            //this.txtproduct_user.DataBind();
            //this.txtproduct_user.Items.Insert(0, new ListItem("ALL", ""));
            this.txtdate1.Text = System.DateTime.Now.AddDays(-1).ToShortDateString();
            this.txtdate2.Text = System.DateTime.Now.ToShortDateString();
        }

        
	  
            this.SetData();
       
    }

    private void SetData()
    {
        if (this.m_stable_name!="" && this.m_sorder_id!="")
        {
            string lssql = "exec Bmw_Vision_Query '" + this.txtdate1.Text + "','" + this.txtdate2.Text + "','" + this.m_stable_name + "','" + this.m_sorder_id + "'";
            DataTable ldt = DbHelperSQLProductionData.Query(lssql).Tables[0];

            Pgi.Auto.Control.SetGrid("Bmw_Vision", "HEAD", this.gv1, ldt);

        }
    }

    protected void btnimport_Click(object sender, EventArgs e)
    {


      //  this.SetData();
        this.gve1.WriteXlsToResponse(System.DateTime.Now.ToShortDateString());//导出到Excel
       
    }

    protected void gv1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption.Trim() == "序号")
        {
            e.Cell.Text = ((e.VisibleIndex) + 1).ToString();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {

    }
}