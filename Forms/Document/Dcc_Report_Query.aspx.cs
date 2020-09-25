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


public partial class Forms_Document_Dcc_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ApplyId_i"] = "Y";
        Setddl_sel();
        QueryASPxGridView();
    }
   protected void gv_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string part = Convert.ToString(e.GetValue("formno"));
            string file_serialno = Convert.ToString(e.GetValue("File_Serialno"));
            string pdfFile = Convert.ToString(e.GetValue("pdfFile"));
            //if (GroupID != "")
            //{
            string FormNo = Convert.ToString(e.GetValue("formno"));
            string groupid = Convert.ToString(e.GetValue("GroupID"));
            e.Row.Cells[3].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=a46c47ad-1e1b-47c3-a7b2-6859ea45b7d7&instanceid="
                + e.GetValue("formno") + "&groupid=" + groupid + "&display=1' target='_blank'>" + part + "</a>";

            if (Convert.ToString(e.GetValue("type")) != "期初导入")
            {
                e.Row.Cells[11].Text = "<a href='/file/" + e.GetValue("File_Serialno") + "/" + e.GetValue("pdfFile") + "' target='_blank'>" + pdfFile + "</a>";
            }
            else
            {
                e.Row.Cells[11].Text = "<a href='" + Convert.ToString(e.GetValue("File_Path_Orig")) + "' target='_blank'>" + Convert.ToString(e.GetValue("File_Path_Orig")) + "</a>";
            }
        
        }
    }


   public void Setddl_sel()
   {
       string strSQL = @"	select distinct file_type from PGI_File_Transceiver_FileType ";
       DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

       ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = dt.Columns[0].ColumnName;
       ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[0].ColumnName;
       ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
       ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
   }



    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gv_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    {

    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("文件收发" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec DCC_Report_Query '{0}','{1}','{2}'";
        sql = string.Format(sql, txt_part.Text, txt_custpart.Text, ASPxDropDownEdit1.Text);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        gv.DataSource = dt;
        gv.DataBind();

    }


    [WebMethod]
    public static string CheckData(string part,string type,string filetype, string formno,string filename)
    {
        //------------------------------------------------------------------------------验证申请中
        string re_flag = "";

        string re_sql = @"select * from  PGI_File_Transceiver_Main_form  main join PGI_File_Transceiver_Dtl_form  dtl 
                    on main.formno=dtl.formno  where isnull(iscomplete,'')='' and part='" + part + "'  and filetype='"+filetype+"' and filename='"+filename+"' ";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        if (re_dt.Rows.Count > 0)
        {
            re_flag = "【PGI_项目号】" + part + "【申请类别】" + type + "【文件类型】" + filetype + "【文件名称】" + filename
                + "正在<font color='red'>申请中</font>，不能修改(单号:" + re_dt.Rows[0]["formno"].ToString() + ",申请人:"
                + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + ")!";
        }

     
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }
}