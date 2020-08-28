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

        SetWlh();
    }

    //物料号
    private void SetWlh()
    {
        wlh.Columns.Clear();

        //新增没有维护税率的 物料号
        string lssql = @"select com.com_domain,com.com_comm_code,com_desc,comd_part,comd_part+'|'+ com_domain as V
                        from qad.dbo.qad_com_mstr com 
	                        inner join qad.dbo.qad_comd_det comd on com.com_domain=comd.comd_domain and com.com_comm_code=comd.comd_comm_code 
	                        left join Fin_Base_QGSF QGSF on comd.comd_domain=QGSF.domain and comd.comd_part=QGSF.wlh
                        where QGSF.domain is null or QGSF.wlh is null
                        order by comd_part";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        wlh.ValueField = "V";
        wlh.Columns.Add("comd_part", "物料号", 80);
        wlh.Columns.Add("com_domain", "域", 50);
        wlh.TextFormatString = "{0}|{1}";
        wlh.DataSource = ldt;
        wlh.DataBind();
    }


    [WebMethod]
    public static string GetData_ByWlh(string wlh_domain)
    {
        string[] wlh_domain_arr = wlh_domain.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string lssql = @"select com.com_domain,com.com_comm_code,com_desc,comd_part 
                        from qad.dbo.qad_com_mstr com 
	                        inner join qad.dbo.qad_comd_det comd on com.com_domain=comd.comd_domain and com.com_comm_code=comd.comd_comm_code 
                        where comd_part='" + wlhno + "' and com_domain='" + domain + "'";
        DataTable dt = DbHelperSQL.Query(lssql).Tables[0];

        string result = "[{\"domain\":\"" + dt.Rows[0]["com_domain"].ToString() 
            + "\",\"hscode\":\"" + dt.Rows[0]["com_comm_code"].ToString() 
            + "\",\"comdesc\":\"" + dt.Rows[0]["com_desc"].ToString() + "\"}]";
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