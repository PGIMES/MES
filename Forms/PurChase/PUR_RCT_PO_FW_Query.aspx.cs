using System;
using System.Web.UI;
using System.Data;
using Maticsoft.DBUtility;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.Generic;

public partial class Forms_PurChase_PUR_RCT_PO_FW_Query : System.Web.UI.Page
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

        SetPoVendor(PoDomain.Text);
        if (!IsPostBack)
        {
            //初始化日期           
            //txtCheckDateFrom.Text = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            //txtCheckDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
        QueryASPxGridView();
    }

    //采购供应商
    private void SetPoVendor(string lsdomain)
    {
        PoVendorId.Columns.Clear();
        //string lssql = @"select distinct ad_addr,ad_name,ad_addr+'|'+ad_name as v   
        //                from qad_ad_mstr 
        //                    inner join qad_vd_mstr on ad_addr=vd_addr and ad_domain=vd_domain 
        //                where ad_type='supplier' and vd_taxc<>'' and ad_domain='" + lsdomain + "'";
        string lssql = @"select distinct PoVendorId
                            ,case when PoVendorId in(select PoVendorId from pur_po_Vendor) then WgVendor else PoVendorName end PoVendorName
                            ,PoVendorId+'|'+case when PoVendorId in(select PoVendorId from pur_po_Vendor) then WgVendor else PoVendorName end v 
                        from PUR_PO_Main_Form 
                        where isnull(iscomplete,'1')='1'
	                        and PoNo  in(select PoNo from PUR_RCT_Main_Form rct where type='' and isnull(iscomplete,'')='1')
                            and PoDomain='" + lsdomain + "'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        PoVendorId.ValueField = "v";
        PoVendorId.Columns.Add("PoVendorId", "代码", 20);
        PoVendorId.Columns.Add("PoVendorName", "名称", 80);
        PoVendorId.TextFormatString = "{0}|{1}";
        PoVendorId.DataSource = ldt;
        PoVendorId.DataBind();
    }
    public void PoDomain_TextChanged(object sender, EventArgs e)
    {
        SetPoVendor(((DropDownList)sender).SelectedValue);

    }
    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec PUR_RCT_PO_FW_Query '{0}','{1}','{2}','{3}','{4}','{5}','{6}'";

        string Vendor = PoVendorId.Value == null ? "" : PoVendorId.Value.ToString();
        sql = string.Format(sql, PoDomain.SelectedValue, Vendor, txtPoNo.Text.Trim(), txtRCTNo.Text.Trim(), txtCheckDateFrom.Text, txtCheckDateTo.Text, ddlType.SelectedValue);

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("未匹配收货报表_费用服务_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType != GridViewRowType.Data)
        //{
        //    return;
        //}

        //string SysContractNo = Convert.ToString(e.GetValue("SysContractNo"));
        //if (SysContractNo.Contains("_QAD"))
        //{
        //    return;
        //}

        //int pono_index = 0;
        //for (int i = 0; i < this.GV_PART.DataColumns.Count; i++)
        //{
        //    if (this.GV_PART.DataColumns[i].FieldName == "PoNo")
        //    {
        //        pono_index = i;
        //    }
        //}

        //string PoNo = Convert.ToString(e.GetValue("PoNo"));
        //string groupid = Convert.ToString(e.GetValue("GroupID"));
        //e.Row.Cells[pono_index + 1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&instanceid="
        //            + e.GetValue("PoNo") + "&groupid=" + groupid + "&display=1' target='_blank'>" + PoNo.ToString() + "</a>";
    }

    [WebMethod]
    public static string po_deal(string rctno)
    {
        string[] ls_rctno = rctno.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string re_flag = "";

        for (int i = 0; i < ls_rctno.Length; i++)
        {
            DataTable dt = DbHelperSQL.Query(@"select * from PUR_RCT_PO_FW where rctno='" + ls_rctno[i] + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                re_flag += "验收单" + ls_rctno[i] + dt.Rows[0]["OptionType"].ToString() + "，不能重复确认！";
            }
        }

        if (re_flag == "")
        {
            string sql_insert = @"insert into PUR_RCT_PO_FW(rctno,PO_tj_time,OptionType) 
                            select '{0}',getdate(),'已开票'";

            List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
            for (int i = 0; i < ls_rctno.Length; i++)
            {
                Pgi.Auto.Common ls_update = new Pgi.Auto.Common();
                ls_update.Sql = string.Format(sql_insert, ls_rctno[i]);
                ls_sum.Add(ls_update);
            }

            int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
            if (ln > 0)
            {
                re_flag = "确认成功！";
            }
            else
            {
                re_flag = "确认失败！";
            }
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
    [WebMethod]
    public static string fw_deal(string rctno,string domain)
    {
        string[] ls_rctno = rctno.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string re_flag = "";

        string rctnos = "";
        for (int i = 0; i < ls_rctno.Length; i++)
        {
            DataTable dt = DbHelperSQL.Query(@"select * from PUR_RCT_PO_FW where rctno='" + ls_rctno[i] + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["OptionType"].ToString() == "已匹配")
                {
                    re_flag += "验收单" + ls_rctno[i] + dt.Rows[0]["OptionType"].ToString() + "，不能重复确认！";
                }
            }
            else
            {
                re_flag += "验收单" + ls_rctno[i] + ",采购还未确认,不能确认不能重复确认！";
            }
            rctnos = rctnos + "'" + ls_rctno[i] + "',";
        }

        if (re_flag == "")//right(cast(year(GETDATE())as nvarchar(max)),2)
        {
            string code_f = domain == "200" ? "K" : "S";
            string sql = @"update PUR_RCT_PO_FW set OptionType='已匹配',fw_qr_time=getdate()
                                                    ,qad_fp_no='{1}'+CONVERT(nvarchar(4),getdate(),12)
                                                                +right('000'+cast(
																	                (select isnull(max(cast(right(qad_fp_no,3) as int)),0) from PUR_RCT_PO_FW 
                                                                                    where qad_fp_no like '{1}'+CONVERT(nvarchar(4),getdate(),12)+'%'
                                                                                    )
																                +1 as nvarchar(max)),3)
                            where rctno in({0})";
            sql = string.Format(sql, rctnos.Substring(0, rctnos.Length - 1), code_f + "F");

            int i = DbHelperSQL.ExecuteSql(sql);
            if (i > 0)
            {
                re_flag = "确认成功！";
            }
            else
            {
                re_flag = "确认失败！";
            }
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
}