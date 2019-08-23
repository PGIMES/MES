using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Fin_Fin_idh_invoice_Report : System.Web.UI.Page
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


    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec Report_idh_invoice '{0}','{1}','{2}','{3}'";
        sql = string.Format(sql, ddl_comp.SelectedValue, StartDate.Text, EndDate.Text, ddl_status.SelectedValue);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        GV_PART.Visible = false;
        GV_PART_DK.Visible = false;
        GV_PART_YK.Visible = false;

        if (ddl_status.SelectedValue == "未开票")
        {
            GV_PART.Visible = true;

            GV_PART.DataSource = dt;
            GV_PART.DataBind();
        }
        if (ddl_status.SelectedValue == "待开票")
        {
            GV_PART_DK.Visible = true;

            GV_PART_DK.DataSource = dt;
            GV_PART_DK.DataBind();
        }
        if (ddl_status.SelectedValue == "已开票")
        {
            GV_PART_YK.Visible = true;

            GV_PART_YK.DataSource = dt;
            GV_PART_YK.DataBind();
        }

    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("国内客户开票通知_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_DK_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_YK_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_DK_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "ih_inv_nbr" || e.Column.FieldName == "ih_ship" || e.Column.FieldName == "idh_part"
            || e.Column.FieldName == "idh_custpart" || e.Column.FieldName == "cp_comment" || e.Column.FieldName == "idh_um" || e.Column.FieldName == "idh_list_pr"
            || e.Column.FieldName == "idh_taxc_new" || e.Column.FieldName == "ih_eff_date" || e.Column.FieldName == "ih_bill" || e.Column.FieldName == "ih_bill_name"
             || e.Column.FieldName == "ih_bol"
            || e.Column.FieldName == "idh_qty_inv" || e.Column.FieldName == "yksl_sum" || e.Column.FieldName == "wksl_sum")
        {
            var ih_inv_nbr1 = GV_PART_DK.GetRowValues(e.RowVisibleIndex1, "ih_inv_nbr");
            var ih_inv_nbr2 = GV_PART_DK.GetRowValues(e.RowVisibleIndex2, "ih_inv_nbr");

            var ih_ship1 = GV_PART_DK.GetRowValues(e.RowVisibleIndex1, "ih_ship");
            var ih_ship2 = GV_PART_DK.GetRowValues(e.RowVisibleIndex2, "ih_ship");

            var idh_part1 = GV_PART_DK.GetRowValues(e.RowVisibleIndex1, "idh_part");
            var idh_part2 = GV_PART_DK.GetRowValues(e.RowVisibleIndex2, "idh_part");

            if (ih_inv_nbr1.ToString() != ih_inv_nbr2.ToString() || ih_ship1.ToString() != ih_ship2.ToString() || idh_part1.ToString() != idh_part2.ToString())
            {
                e.Handled = true;
            }

        }
    }

    protected void GV_PART_YK_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "ih_inv_nbr" || e.Column.FieldName == "ih_ship" || e.Column.FieldName == "idh_part"
            || e.Column.FieldName == "idh_custpart" || e.Column.FieldName == "cp_comment" || e.Column.FieldName == "idh_um" || e.Column.FieldName == "idh_list_pr"
            || e.Column.FieldName == "idh_taxc_new" || e.Column.FieldName == "ih_eff_date" || e.Column.FieldName == "ih_bill" || e.Column.FieldName == "ih_bill_name"
             || e.Column.FieldName == "ih_bol"
            || e.Column.FieldName == "idh_qty_inv" || e.Column.FieldName == "yksl_sum" || e.Column.FieldName == "wksl_sum")
        {
            var ih_inv_nbr1 = GV_PART_YK.GetRowValues(e.RowVisibleIndex1, "ih_inv_nbr");
            var ih_inv_nbr2 = GV_PART_YK.GetRowValues(e.RowVisibleIndex2, "ih_inv_nbr");

            var ih_ship1 = GV_PART_YK.GetRowValues(e.RowVisibleIndex1, "ih_ship");
            var ih_ship2 = GV_PART_YK.GetRowValues(e.RowVisibleIndex2, "ih_ship");

            var idh_part1 = GV_PART_YK.GetRowValues(e.RowVisibleIndex1, "idh_part");
            var idh_part2 = GV_PART_YK.GetRowValues(e.RowVisibleIndex2, "idh_part");

            if (ih_inv_nbr1.ToString() != ih_inv_nbr2.ToString() || ih_ship1.ToString() != ih_ship2.ToString() || idh_part1.ToString() != idh_part2.ToString())
            {
                e.Handled = true;
            }

        }
    }

    [WebMethod]
    public static string deal(string ids)
    {
        string re_flag = "";

        string sql = @"update idh_invoice_upload set status='已开票',sure_date=getdate() where id in("+ids+")";
        int i = DbHelperSQL.ExecuteSql(sql);
        if (i > 0)
        {
            re_flag = "确认成功！";
        }
        else
        {
            re_flag = "确认失败！";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }



}