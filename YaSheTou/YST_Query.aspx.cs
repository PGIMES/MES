using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class YaSheTou_YST_Query : System.Web.UI.Page
{
    //public string UserId = "";
    //public string UserName = "";
    //LoginUser LogUserModel = null;
    Function_Jinglian Function_Jinglian = new Function_Jinglian();

    protected void Page_Load(object sender, EventArgs e)
    {
        //LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        //UserId = LogUserModel.UserId;
        //UserName = LogUserModel.UserName;

        if (!IsPostBack)
        {
            txt_startdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (Function_Jinglian.Emplogin_query(10, "", Request["deviceid"]).Rows.Count > 0)
            {
                txt_sbjc.Text = Function_Jinglian.Emplogin_query(10, "", Request["deviceid"]).Rows[0]["equip_name"].ToString().Trim();
            }
            else
            {
                txt_sbjc.Text = "";
            }

            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
    }

    protected void btn_search_ServerClick(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Record_Query] '" + txt_startdate.Text + "','" + txt_enddate.Text + "','"+ txt_sbjc.Text + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }
    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        int end_mc = Convert.ToInt32(e.GetValue("end_mc").ToString() == "" ? "0" : e.GetValue("end_mc").ToString());
        if (e.GetValue("yzt_status").ToString() == "报废")
        {
            if (end_mc < 18000)
            {
                e.Row.Cells[5].Style.Add("background-color", "#FF0000");
                e.Row.Cells[5].Style.Add("color", "#FFFFFF");
            }
        }
        else
        {
            if (end_mc > 25000)
            {
                e.Row.Cells[5].Style.Add("background-color", "#EEEE00");
            }
        }

    }
}