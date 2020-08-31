using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_Fin_Base_QGSF_V1_Maintain : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        SetWlh(); Setimmunity();
    }

    //物料号
    private void SetWlh()
    {
        wlh.Columns.Clear();

        //新增没有维护税率的 物料号
        string lssql = @"exec usp_Fin_Base_QGSF_maintain_init '',''";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        wlh.ValueField = "V";
        wlh.Columns.Add("comd_pgino", "物料号", 80);
        wlh.Columns.Add("com_domain", "域", 50);
        wlh.TextFormatString = "{0}|{1}";
        wlh.DataSource = ldt;
        wlh.DataBind();
    }

    //物料号
    private void Setimmunity()
    {
        cmb_immunity.Columns.Clear();

        string lssql = @"select 'Y' YN union select 'N' YN";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        cmb_immunity.ValueField = "YN";
        cmb_immunity.Columns.Add("YN", "是否豁免", 80);
        cmb_immunity.DataSource = ldt;
        cmb_immunity.DataBind();
    }

    [WebMethod]
    public static string GetData_ByWlh(string wlh_domain)
    {
        string re_domain="", re_hscode = "", re_comdesc = "", baserate = "", qgcode = "", qgrate = "", immunity="";

        string[] wlh_domain_arr = wlh_domain.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string lssql = @"exec usp_Fin_Base_QGSF_maintain_init '" + wlhno + "','" + domain + "'";
        DataSet ds = DbHelperSQL.Query(lssql);

        DataTable dt = ds.Tables[0];
        re_domain = dt.Rows[0]["com_domain"].ToString();
        re_hscode = dt.Rows[0]["com_comm_code"].ToString();
        re_comdesc = dt.Rows[0]["com_desc"].ToString();

        if (ds.Tables[1].Rows.Count > 0)
        {
            DataTable dt_2 = ds.Tables[1];

            baserate = dt_2.Rows[0]["BaseRate"].ToString();
            qgcode = dt_2.Rows[0]["301code"].ToString();
            qgrate = dt_2.Rows[0]["301Rate"].ToString();
            immunity = dt_2.Rows[0]["immunity"].ToString();
        }

        string result = "[{\"domain\":\"" + re_domain + "\",\"hscode\":\"" + re_hscode + "\",\"comdesc\":\"" + re_comdesc 
            + "\",\"baserate\":\"" + baserate + "\",\"qgcode\":\"" + qgcode + "\",\"qgrate\":\"" + qgrate
            + "\",\"immunity\":\"" + immunity + "\"}]";
        return result;

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string sql = @"exec [usp_Fin_Base_QGSF_edit] '{0}','{1}','{2}',{3},{4},'{5}','{6}'";

        string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        sql = string.Format(sql, domain, wlhno, txt_301code.Text, Convert.ToSingle(txt_BaseRate.Text), Convert.ToSingle(txt_301Rate.Text), LogUserModel.UserId, LogUserModel.UserName);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        string msg = "";
        if (dt.Rows[0][0].ToString() == "N")
        {
            msg = "确认成功！";
        } else if (dt.Rows[0][0].ToString() == "Y")
        {
            msg = wlh.Value.ToString() + "已经存在，不可重复维护！";
        }
        else
        {
            msg = "确认失败！";
        }
        string lsstr = "layer.alert('" + msg + "',function(index) {layer.close(index);})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }




}