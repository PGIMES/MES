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

public partial class Forms_PgiOp_GYLX_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Setddl_p_leibie();

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true || this.gv_yz.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
        }
    }

    public void Setddl_p_leibie()
    {
        string strSQL = @"	SELECT CLASS_NAME as  status_id,CLASS_NAME as status  from form3_Sale_Product_BASE
                            where base_name='DDL_product_leibie'
                            order by CLASS_ID";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField= dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        GYLX GYLX = new GYLX();
        DataTable dt = GYLX.GYLX_query(txt_pgi_no.Text.Trim(), txt_pn.Text.Trim(), ddl_ver.SelectedValue, ddl_typeno.SelectedValue, ASPxDropDownEdit1.Text,ddl_pt_status.SelectedValue);

        if (ddl_typeno.SelectedValue=="机加")
        {
            gv.Visible = true;
            gv_yz.Visible = false;

            gv.DataSource = dt;
            gv.DataBind();
        }

        if (ddl_typeno.SelectedValue == "压铸")
        {
            gv.Visible = false;
            gv_yz.Visible = true;

            gv_yz.DataSource = dt;
            gv_yz.DataBind();
        }


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
        if (ddl_typeno.SelectedValue == "机加")
        {
            ASPxGridViewExporter1.WriteXlsToResponse("工艺路线" + System.DateTime.Now.ToShortDateString());//导出到Excel
        }

        if (ddl_typeno.SelectedValue == "压铸")
        {
            ASPxGridViewExporter2.WriteXlsToResponse("工艺路线" + System.DateTime.Now.ToShortDateString());//导出到Excel
        }
    }

    [WebMethod]
    public static string CheckData(string pgi_no,string pgi_no_t)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        string re_flag = "";

        string re_sql = @"select b.projectno,b.pgi_no_t,b.formno,b.createbyid,b.createbyname
                    from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID = 'ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0, 1))  a 
                        inner join PGI_GYLX_Main_Form b on a.InstanceID = b.formno
                    where b.projectno='" + pgi_no + "' and b.pgi_no_t='" + pgi_no_t + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        if (re_dt.Rows.Count > 0)
        {
            re_flag= pgi_no + "(" + pgi_no_t + ")项目正在申请中，不能修改(单号:" + re_dt.Rows[0]["formno"].ToString() + ",申请人:" 
                + re_dt.Rows[0]["createbyid"].ToString() + "-" + re_dt.Rows[0]["createbyname"].ToString() + ")!";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "pgi_no" || e.Column.FieldName == "pt_status" || e.Column.FieldName == "pgi_no_t"
            || e.Column.FieldName == "ver" || e.Column.FieldName == "pn" || e.Column.FieldName == "domain" || e.Column.FieldName == "product_user" || e.Column.FieldName == "formno")
        {
            var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "formno"); 
            var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "formno");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }

    }

    protected void gv_yz_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "pgi_no" || e.Column.FieldName == "pt_status" || e.Column.FieldName == "pgi_no_t"
            || e.Column.FieldName == "ver" || e.Column.FieldName == "pn" || e.Column.FieldName == "domain" || e.Column.FieldName == "product_user" || e.Column.FieldName == "formno")
        {
            var formno1 = gv_yz.GetRowValues(e.RowVisibleIndex1, "formno");
            var formno2 = gv_yz.GetRowValues(e.RowVisibleIndex2, "formno");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }
    }
}