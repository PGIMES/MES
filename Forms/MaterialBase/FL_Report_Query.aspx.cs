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

public partial class Forms_MaterialBase_FL_Report_Query : System.Web.UI.Page
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
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
        }
    }

    public void Setddl_p_leibie()
    {
        string strSQL = @"	select cp_line,name from  PGI_BASE_PART_ddl where type = '辅料类'";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = dt.Columns[1].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[1].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }
    public void QueryASPxGridView()
    {
        GYLX GYLX = new GYLX();
        DataTable dt = DbHelperSQL.Query("exec  Rpt_Fuliao_Query '" + ddl_comp.SelectedValue + "','" + txt_pgi_no.Text + "','" + txt_pn.Text + "','" + ASPxDropDownEdit1.Text + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();




    }

    [WebMethod]
    public static string CheckData(string part_no,string comp)
    {
        //------------------------------------------------------------------------------
        string re_flag = "";

        string re_sql = @" select top 1 a.InstanceID,b.createbyid,b.createbyname ,B.WLH,B.domain
                                        from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='9d591dd9-b615-4e8f-b2f8-d3a7161af952' and status in(0,1))  a
                                        inner join PGI_FLMstr_DATA_Form b on a.InstanceID=b.formno 
                                         where b.WLH='" + part_no + "' and b.domain='" + comp + "'";
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
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

        ASPxGridViewExporter1.WriteXlsToResponse("辅料" + System.DateTime.Now.ToShortDateString());//导出到Excel

    }

    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        //if (e.Column.FieldName == "wlh" || e.Column.FieldName == "comp" || e.Column.FieldName == "cpgroup" || e.Column.FieldName == "fyweight" || e.Column.FieldName == "gc_version" || e.Column.FieldName == "bz_version"
        //    || e.Column.FieldName == "site" || e.Column.FieldName == "wlmc" || e.Column.FieldName == "ms" || e.Column.FieldName == "fxcode" || e.Column.FieldName == "status"
        //    )
        //{
        //    var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "formno");
        //    var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "formno");

        //    if (formno1.ToString() != formno2.ToString())
        //    {
        //        e.Handled = true;
        //    }

        //}

    }

}