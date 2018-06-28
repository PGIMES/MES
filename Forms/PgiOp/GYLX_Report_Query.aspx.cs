using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PgiOp_GYLX_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        GYLX GYLX = new GYLX();
        DataTable dt = GYLX.GYLX_query(txt_pgi_no.Text.Trim(), txt_pn.Text.Trim(), ddl_ver.SelectedValue, ddl_typeno.SelectedValue);

        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
    }

    //protected void gv_tr_list_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.Caption == "序号")
    //    {
    //        if (Convert.ToInt16(ViewState["i"]) == 0)
    //        {
    //            ViewState["i"] = 1;
    //        }
    //        int i = Convert.ToInt16(ViewState["i"]);
    //        e.Cell.Text = i.ToString();
    //        i++;
    //        ViewState["i"] = i;
    //    }
    //}

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse("工艺路线" + System.DateTime.Now.ToShortDateString());//导出到Excel

    }

    [WebMethod]
    public static string CheckData(string pgi_no)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        string re_flag = "";

        string re_sql = @"select top 1 a.InstanceID,c.createbyid,c.createbyname 
                                    from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0,1))  a
                                        inner join PGI_GYLX_Dtl_Form b on a.InstanceID=b.GYGSNo
                                        inner join PGI_GYLX_Main_Form c on a.InstanceID=c.formno
                                     where b.pgi_no='" + pgi_no + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        if (re_dt.Rows.Count > 0)
        {
            re_flag= "该项目正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString() + ",申请人:" + re_dt.Rows[0]["createbyid"].ToString() + "-" + re_dt.Rows[0]["createbyname"].ToString() + ")!";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "pgi_no" || e.Column.FieldName == "pgi_no_t"
            || e.Column.FieldName == "ver" || e.Column.FieldName == "pn" || e.Column.FieldName == "domain" || e.Column.FieldName == "formno")
        {
            var pgi_no1 = gv.GetRowValues(e.RowVisibleIndex1, "pgi_no");
            var pgi_no2 = gv.GetRowValues(e.RowVisibleIndex2, "pgi_no");

            if (pgi_no1.ToString() != pgi_no2.ToString())
            {
                e.Handled = true;
            }
        }
    }
}