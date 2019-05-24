using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_PO_Print_his : System.Web.UI.Page
{
    protected string PoNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PoNo = Request.QueryString["PoNo"].ToString();
        if (!IsPostBack)
        {

            QueryASPxGridView();
        }
        if (this.gv_his.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        DataTable dt_his = DbHelperSQL.Query(" select * from PUR_PO_Print_His where PoNo='" + PoNo + "' order by id").Tables[0];
        gv_his.DataSource = dt_his;
        gv_his.DataBind();
    }
    protected void gv_his_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}