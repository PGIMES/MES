using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_Emp_LoginLog_Query : System.Web.UI.Page
{
    public string location = "";
    public string m_slocation = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //LoginUser LogUserModel = null;
        //if (Session["empid"] == null)
        //{
        //    LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        //}

        //Session["LogUser"] = LogUserModel;


        if (Request.QueryString["location"] != null)
        {
            this.location = Request.QueryString["location"].ToString();
        }
        if (Request.QueryString["order_id"] != null)
        {
            this.m_slocation = Request.QueryString["order_id"].ToString();
        }

        if (!IsPostBack)
        {
            this.txtdate1.Text = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
            this.txtdate2.Text = System.DateTime.Now.ToShortDateString();
            this.txt_order_id.Text = this.m_slocation;
            //this.txt_emp.Text = LogUserModel.UserId;

            QueryASPxGridView();
        }
        if (this.gv1.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    protected void gv1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "序号")
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        string lssql = "exec Emp_LoginLog_Query '" + this.txtdate1.Text + "','" + this.txtdate2.Text + "','" + this.txt_emp.Text.Trim() + "','" + this.txt_order_id.Text.Trim() + "'";
        DataTable ldt = DbHelperSQLProduction.Query(lssql).Tables[0];
        Pgi.Auto.Control.SetGrid("Emp_LoginLog", "SELECT", this.gv1, ldt);
    }

    protected void gv1_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gve1.WriteXlsToResponse("登录查询" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }



    protected void gv1_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }

        int remarkindex = 0; 
        for (int i = 0; i < this.gv1.DataColumns.Count; i++)
        {
            if (this.gv1.DataColumns[i].FieldName == "remark")
            {
                remarkindex = i;
            }
        }

        e.Row.Cells[remarkindex].Text= "登入留言:" + (string)e.GetValue("login_remark") + "<br />登出留言:" + (string)e.GetValue("logout_remark");

    }
}