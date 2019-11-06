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

public partial class Forms_Finance_OES_Report_Query : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec  Report_Fin_OES '" + ddl_domain.SelectedValue + "','"+ LogUserModel.UserId + "','" + LogUserModel.DepartName + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse("费用报销" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    protected void gv_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        if (e.Column.Name == "FormNo" || e.Column.FieldName == "ApplyDate" || e.Column.FieldName == "ApplyName" || e.Column.FieldName == "ApplyDept" || e.Column.FieldName == "ApplyDomainName"
             || e.Column.FieldName == "GoDays" || e.Column.FieldName == "GoSatus" || e.Column.FieldName == "ApproveDate")
        {
            var formno1 = gv.GetRowValues(e.RowVisibleIndex1, "FormNo");
            var formno2 = gv.GetRowValues(e.RowVisibleIndex2, "FormNo");

            if (formno1.ToString() != formno2.ToString())
            {
                e.Handled = true;
            }

        }

    }


    public void ExportQuery()
    {
        string sql = string.Format("exec  Report_Fin_OES_xls '{0}','{1}','{2}','{3}','{4}'", ddl_domain.SelectedValue, LogUserModel.UserId, LogUserModel.DepartName,dateStart.Text,dateEnd.Text);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        dgexp.DataSource = dt;
        dgexp.DataBind();
    }
    protected void btnExport_ServerClick(object sender, EventArgs e)
    {
        ExportQuery();
        for(int i = 0; i < dgexp.Items.Count; i++)
        {
            dgexp.Items[i].Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
        Excel("excel", "报销记录"+DateTime.Now.ToString("yyMMddHHmm")+".xls");
    }
    /// <summary>
    /// 下载数据
    /// </summary>
    /// <param name="FileType">文件类型</param>
    /// <param name="FileName">Excel表名</param>
    private void Excel(string FileType, string FileName)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            //返回与指定代码页关联的数据
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //attachment表示作为附件下载，filename指定输出文件名称
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
            //指定文件类型 
            Response.ContentType = FileType;
            this.EnableViewState = false;
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            //定义一输入流
            System.IO.StringWriter tw = new System.IO.StringWriter(myCItrad);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            this.dgexp.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js", "alert('发生错误：" + err.Message.Replace("\r\n", "\\r\\n").Replace("'", "‘") + "')", true);
            return;
        }
    }
}