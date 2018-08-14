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

public partial class Forms_MaterialBase_WuLiao_Report_Query : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         id = Request["formid"]; 
        Setddl_p_leibie();

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true )//页面搜索条件使用
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

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }
    public void QueryASPxGridView()
    {
        GYLX GYLX = new GYLX();
        DataTable dt = DbHelperSQL.Query("exec  Rpt_Wuliao_Query '" + ddl_comp.SelectedValue + "','" + txt_pgi_no.Text + "','" + txt_pn.Text + "','" + ASPxDropDownEdit1.Text + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();




    }

    [WebMethod]
    public static string CheckData(string wlh, string gc_version,string bz_version,string comp,string part_no)
    {
        //------------------------------------------------------------------------------
        string re_flag = "";

        string re_sql = @"select top 1 a.InstanceID,b.createbyid,b.createbyname 
                                                            from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='d9cb9476-13f9-48ec-a87e-5b84ca0790b0' and status in(0,1))  a
                                                                inner join PGI_PartMstr_DATA_Form b on a.InstanceID=b.formno 
                                                             where b.part_no='" + part_no + "' and b.comp='" + comp + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];


        if (re_dt.Rows.Count > 0)
        {
            re_flag = "物料号：" + part_no + ";正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString() + ",申请人:"
                + re_dt.Rows[0]["createbyid"].ToString() + "-" + re_dt.Rows[0]["createbyname"].ToString() + ")!";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "mergecells();setHeight();", true);
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

            ASPxGridViewExporter1.WriteXlsToResponse("生产性物料" + System.DateTime.Now.ToShortDateString());//导出到Excel
      
    }

    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.FieldName == "wlh" || e.Column.FieldName == "comp" || e.Column.FieldName == "cpgroup" || e.Column.FieldName == "fyweight" || e.Column.FieldName=="gc_version" || e.Column.FieldName=="bz_version"
            || e.Column.FieldName == "site" || e.Column.FieldName == "wlmc" || e.Column.FieldName == "ms" || e.Column.FieldName == "fxcode" || e.Column.FieldName == "status" 
            )
        {
            var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "formno");
            var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "formno");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }

    }

}