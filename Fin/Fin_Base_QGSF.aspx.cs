using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_Fin_Base_QGSF : System.Web.UI.Page
{//public string UserId = "";
    //public string UserName = "";
    //public string DeptName = "";
    //LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        //UserId = LogUserModel.UserId;
        //UserName = LogUserModel.UserName;
        //DeptName = LogUserModel.DepartName;

        if (!IsPostBack)
        {
           

        }
        QueryASPxGridView();
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"select QGSF.domain,QGSF.wlh,QGSF.[301code], QGSF.BaseRate, QGSF.[301Rate],com.com_comm_code,com.com_desc 
                    from Fin_Base_QGSF QGSF
                        inner join qad.dbo.qad_comd_det comd on QGSF.domain=comd.comd_domain and QGSF.wlh=comd.comd_part 
                        inner join qad.dbo.qad_com_mstr com on com.com_domain=comd.comd_domain and com.com_comm_code=comd.comd_comm_code 
                    order by QGSF.wlh";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("产品税率_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    [WebMethod]
    public static string del_data(string keys)
    {
        string re_flag = "";

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        string[] ls_key = keys.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_key.Length; i++)
        {
            string[] wlh_domain_arr = ls_key[i].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            string sql = @"delete Fin_Base_QGSF where wlh='{1}' and domain='{0}'";
            sql = string.Format(sql, domain, wlhno);

            ls_del.Sql = sql;
            ls_sum.Add(ls_del);
        }

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        if (ln > 0)
        {
            re_flag = "删除成功！";
        }
        else
        {
            re_flag = "删除失败！";
        }
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;
    }



}