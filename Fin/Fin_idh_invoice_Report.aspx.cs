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
        if (ddl_status.SelectedValue == "未开票")
        {
            GV_PART.ExportXlsxToResponse("国内客户开票通知_未开票_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
        }
        if (ddl_status.SelectedValue == "待开票")
        {
            GV_PART_DK.ExportXlsxToResponse("国内客户开票通知_待开票_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
        }
        if (ddl_status.SelectedValue == "已开票")
        {
            GV_PART_YK.ExportXlsxToResponse("国内客户开票通知_已开票_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
        }
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
            || e.Column.FieldName == "idh_taxc_pr" || e.Column.FieldName == "ih_eff_date" || e.Column.FieldName == "ih_bill" || e.Column.FieldName == "ih_bill_name"
            || e.Column.FieldName == "ih_bol"
            || e.Column.FieldName == "idh_qty_inv" || e.Column.FieldName == "yksl_sum" || e.Column.FieldName == "wksl_sum" || e.Column.FieldName == "wksl_sum_fpe")
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
            || e.Column.FieldName == "idh_taxc_pr" || e.Column.FieldName == "ih_eff_date" || e.Column.FieldName == "ih_bill" || e.Column.FieldName == "ih_bill_name"
            || e.Column.FieldName == "ih_bol"
            || e.Column.FieldName == "idh_qty_inv" || e.Column.FieldName == "yksl_sum" || e.Column.FieldName == "wksl_sum" || e.Column.FieldName == "wksl_sum_fpe")
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



    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        //10122 > 60天显示黄，> 75天显示红色
        //其他 > 30显示黄，> 45天显示红色
        DateTime ih_eff_date = Convert.ToDateTime(e.GetValue("ih_eff_date").ToString());//生效日期
        string ih_bill = e.GetValue("ih_bill").ToString();//票据开往
        if (ih_bill == "10122")
        {
            if (ih_eff_date <= DateTime.Today.AddDays(-75))
            {
                e.Row.Style.Add("background-color", "#FF0000");
                e.Row.Style.Add("color", "#FFFFFF");
            }
            else if (ih_eff_date <= DateTime.Today.AddDays(-60))
            {
                e.Row.Style.Add("background-color", "#EEEE00");
            }
        }
        else
        {
            if (ih_eff_date <= DateTime.Today.AddDays(-45))
            {
                e.Row.Style.Add("background-color", "#FF0000");
                e.Row.Style.Add("color", "#FFFFFF");
            }
            else if (ih_eff_date <= DateTime.Today.AddDays(-30))
            {
                e.Row.Style.Add("background-color", "#EEEE00");
            }
        }

    }


    protected void GV_PART_DK_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        double idh_taxc_pr = Convert.ToDouble(e.GetValue("idh_taxc_pr").ToString());
        double idh_taxc_new = Convert.ToDouble(e.GetValue("idh_taxc_new").ToString());
        if (idh_taxc_pr != idh_taxc_new)//税率(开票) 黄色背景色 
        {
            if (e.Row.Cells.Count < 21)
            {
                e.Row.Cells[4].Style.Add("background-color", "#EEEE00");
            }
            else
            {
                e.Row.Cells[20].Style.Add("background-color", "#EEEE00");
            }
        }

    }

    protected void GV_PART_YK_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        double idh_taxc_pr = Convert.ToDouble(e.GetValue("idh_taxc_pr").ToString());
        double idh_taxc_new = Convert.ToDouble(e.GetValue("idh_taxc_new").ToString());
        double chae = Convert.ToDouble(e.GetValue("chae").ToString());
        if (idh_taxc_pr != idh_taxc_new)//税率(开票) 黄色背景色
        {
            e.Row.Cells[21].Style.Add("background-color", "#EEEE00");
        }
        if (chae != 0)//差额(未税) 红色背景 白色字体
        {
            e.Row.Cells[20].Style.Add("background-color", "#FF0000");
            e.Row.Cells[20].Style.Add("color", "#FFFFFF");
        }
    }

    decimal sum_dk = 0;    //和
    List<idh_key> idh_key_list_dk = new List<idh_key>();

    protected void GV_PART_DK_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
    {
        ASPxGridView view_dk = sender as ASPxGridView;

        if (e.Item != null)
        {
            if (e.IsTotalSummary)
            {
                switch (e.SummaryProcess)
                {
                    case CustomSummaryProcess.Start:
                        sum_dk = 0; idh_key_list_dk.Clear();
                        break;
                    case CustomSummaryProcess.Calculate:
                        //string[] fieldnames = new string[] { "ih_inv_nbr", "ih_ship", "idh_part" };
                        //object oj = view.GetRowValues(e.RowHandle, fieldnames);

                        idh_key idh_keys_dk = new idh_key();
                        idh_keys_dk.ih_inv_nbr = view_dk.GetRowValues(e.RowHandle, "ih_inv_nbr").ToString();
                        idh_keys_dk.ih_ship = view_dk.GetRowValues(e.RowHandle, "ih_ship").ToString();
                        idh_keys_dk.idh_part = view_dk.GetRowValues(e.RowHandle, "idh_part").ToString();

                        //不存在 返回null
                        if (idh_key_list_dk.FirstOrDefault(x => x.idh_part == idh_keys_dk.idh_part && x.ih_ship == idh_keys_dk.ih_ship && x.ih_inv_nbr == idh_keys_dk.ih_inv_nbr) == null)
                        {
                            sum_dk += Convert.ToDecimal(e.FieldValue);
                            idh_key_list_dk.Add(idh_keys_dk);
                        }
                        break;
                    case CustomSummaryProcess.Finalize:
                        e.TotalValue = sum_dk;
                        break;
                }
            }
        }
    }

    decimal sum = 0;    //和
    List<idh_key> idh_key_list = new List<idh_key>();

    protected void GV_PART_YK_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    {
        ASPxGridView view = sender as ASPxGridView;

        if (e.Item != null)
        {
            if (e.IsTotalSummary)
            {
                switch (e.SummaryProcess)
                {
                    case CustomSummaryProcess.Start:
                        sum = 0; idh_key_list.Clear();
                        break;
                    case CustomSummaryProcess.Calculate:
                        //string[] fieldnames = new string[] { "ih_inv_nbr", "ih_ship", "idh_part" };
                        //object oj = view.GetRowValues(e.RowHandle, fieldnames);

                        idh_key idh_keys = new idh_key();
                        idh_keys.ih_inv_nbr = view.GetRowValues(e.RowHandle, "ih_inv_nbr").ToString();
                        idh_keys.ih_ship = view.GetRowValues(e.RowHandle, "ih_ship").ToString();
                        idh_keys.idh_part = view.GetRowValues(e.RowHandle, "idh_part").ToString();

                        //不存在 返回null
                        if(idh_key_list.FirstOrDefault(x => x.idh_part == idh_keys.idh_part && x.ih_ship == idh_keys.ih_ship && x.ih_inv_nbr == idh_keys.ih_inv_nbr) == null)
                        {
                            sum += Convert.ToDecimal(e.FieldValue);
                            idh_key_list.Add(idh_keys);
                        }
                        break;
                    case CustomSummaryProcess.Finalize:
                        e.TotalValue = sum;
                        break;
                }
            }
        }
    }

}

public class idh_key
{
    public idh_key()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //       

    }
    /// <summary>
    /// 发票号
    /// </summary>
    public string ih_inv_nbr { get; set; }
    /// <summary>
    /// 发货至
    /// </summary>
    public string ih_ship { get; set; }
    /// <summary>
    /// 物料号
    /// </summary>
    public string idh_part { get; set; }
}