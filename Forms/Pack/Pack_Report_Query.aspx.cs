using DevExpress.Data;
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

public partial class Forms_Pack_Pack_Report_Query : System.Web.UI.Page
{
    public string UserId = "";
    public string UserName = "";
    public string DeptName = "";
    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        UserId = LogUserModel.UserId;
        UserName = LogUserModel.UserName;
        DeptName = LogUserModel.DepartName;

        if (!IsPostBack)
        {
            //ddl_year.SelectedValue = DateTime.Now.AddMonths(-1).ToString("yyyy"); //获取上月年份
            //ddl_month.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM"); //获取上月月份
        }
        QueryASPxGridView();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec Report_Pack '{0}','{1}','{2}'";
        sql = string.Format(sql, txt_part.Text, txt_custpart.Text, ddl_ver.SelectedValue);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        gv.DataSource = dt;
        gv.DataBind();

    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("包装方案" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    [WebMethod]
    public static string CheckData(string part, string domain, string site, string ship, string domain_code, string formno)
    {
        //------------------------------------------------------------------------------验证申请中
        string re_flag = "";

        string re_sql = @"select * from PGI_PackScheme_Main_Form where isnull(iscomplete,'')='' and part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        if (re_dt.Rows.Count > 0)
        {
            re_flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + "【发自】" + site + "【发至】" + ship
                + "正在<font color='red'>申请中</font>，不能修改(单号:" + re_dt.Rows[0]["formno"].ToString() + ",申请人:"
                + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + ")!";
        }

        if (re_flag == "")
        {
            //注释 生效的日程，历史数据也可以.
            /*string re_sql_2 = @"select sod.sod_domain, sod.sod_custpart, sod.sod_end_eff, sod.sod_site, sod.sod_part, so.so_ship,so.so_bill, sod.sod_pr_list 
	                            , so.ad_sort, so.ad_name
                            from (
	                            select sod_domain, sod_custpart, [sod_end_eff[1]]] AS sod_end_eff, sod_site, sod_part, sod_nbr, sod_pr_list
	                            from qad.dbo.qad_sod_det
	                            --WHERE ([sod_end_eff[1]]] IS NULL OR [sod_end_eff[1]]] >= CONVERT(VARCHAR(10), GETDATE(), 120)) AND (sod_pr_list <> '')
	                            ) sod
	                            left join (
		                            select so.*--,ad.ad_sort, ad.ad_name 
			                            ,ad.cm_addr ad_sort,ad.DebtorShipToName ad_name  
		                            from qad.dbo.qad_so_mstr so 
			                            --INNER JOIN dbo.qad_ad_mstr ad ON ad.ad_domain = so.so_domain AND ad.ad_addr = so.so_ship AND ad.ad_bus_relation = so.so_bill
			                            inner join [form4_Customer_DebtorShipTo] ad on charindex(so.so_domain,ad.Debtor_Domain)>0 and ad.DebtorShipToCode=so.so_ship and ad.BusinessRelationCode=so.so_bill
		                            ) so ON so.so_domain = sod.sod_domain AND so.so_nbr = sod.sod_nbr AND so.so_site = sod.sod_site
                            where sod.sod_domain='{0}' and sod.sod_part='{1}' and sod.sod_site='{2}' and so.so_ship='{3}'
                            ";*/
            string re_sql_2 = @"exec Report_CS_edit_report_check '{0}','{1}','{2}','{3}'";
            re_sql_2 = string.Format(re_sql_2, domain_code, part, site, ship);
            DataTable re_dt_2 = DbHelperSQL.Query(re_sql_2).Tables[0];
            if (re_dt_2.Rows.Count <= 0)
            {
                re_flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + "【发自】" + site + "【发至】" + ship + "不存在，不能编辑！";
            }
        }
        
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }

    decimal sum_nyl = 0;    //和
    decimal sum_nzj = 0;    //和
    List<key> key_list = new List<key>();
    protected void gv_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    {
        ASPxGridView view = sender as ASPxGridView;

        if (e.Item != null)
        {
            if (e.IsTotalSummary)
            {
                switch (e.SummaryProcess)
                {
                    case CustomSummaryProcess.Start:
                        sum_nyl = 0; sum_nzj = 0; key_list.Clear();
                        break;
                    case CustomSummaryProcess.Calculate:
                        key keys = new key();
                        keys.formno = view.GetRowValues(e.RowHandle, "FormNo").ToString();

                        //不存在 返回null
                        if (key_list.FirstOrDefault(x => x.formno == keys.formno) == null)
                        {
                            sum_nyl += Convert.ToDecimal(view.GetRowValues(e.RowHandle, "ncb").ToString());
                            sum_nzj += Convert.ToDecimal(view.GetRowValues(e.RowHandle, "nxsjg").ToString());
                            key_list.Add(keys);
                        }
                        break;
                    case CustomSummaryProcess.Finalize:
                        if (sum_nzj==0)
                        {
                            e.TotalValue = "0.0%";
                        }
                        else
                        {
                            e.TotalValue = (sum_nyl / sum_nzj * 100).ToString("0.0") + "%";
                        }                        
                        break;
                }
            }
        }
    }

    protected void gv_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string part = Convert.ToString(e.GetValue("part"));
            string GroupID = Convert.ToString(e.GetValue("GroupID"));
            if (GroupID != "")
            {
                string FormNo = Convert.ToString(e.GetValue("FormNo"));
                string groupid = Convert.ToString(e.GetValue("GroupID"));
                e.Row.Cells[2].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=6fe4a501-d522-458b-a46c-0baa6162d8d3&instanceid=" 
                    + e.GetValue("FormNo") + "&groupid=" + groupid + "&display=1' target='_blank'>" + part + "</a>";

            }
            else
            {
                e.Row.Cells[2].Text = part;
            }
        }
    }
}

public class key
{
    public key()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //       

    }
    /// <summary>
    /// 单号
    /// </summary>
    public string formno { get; set; }
}