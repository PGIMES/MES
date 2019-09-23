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
    public static string CheckData(string part, string domain, string formno)
    {
        //------------------------------------------------------------------------------验证申请中
        string re_flag = "";

        string re_sql = @"select * from PGI_PackScheme_Main_Form where isnull(iscomplete,'')='' and part='" + part + "' and domain='" + domain + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        if (re_dt.Rows.Count > 0)
        {
            re_flag = part + "(" + domain + ")正在<font color='red'>申请中</font>，不能修改(单号:" + re_dt.Rows[0]["formno"].ToString() + ",<font color='red'>申请人:"
                + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + "</font>)!";
        }

        //DataTable re_dt_2 = DbHelperSQL.Query(@"select * from  PGI_PackScheme_Main_Form where formno='" + formno + "' and ApplyType='删除工艺'").Tables[0];
        //if (re_dt_2.Rows.Count > 0)
        //{
        //    re_flag = re_flag + "<br />" + part + "(" + domain + ")已经失效，不能升版本！";
        //}
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }

    decimal sum = 0;    //和
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
                        sum = 0; key_list.Clear();
                        break;
                    case CustomSummaryProcess.Calculate:
                        key keys = new key();
                        keys.formno = view.GetRowValues(e.RowHandle, "FormNo").ToString();

                        //不存在 返回null
                        if (key_list.FirstOrDefault(x => x.formno == keys.formno) == null)
                        {
                            sum += Convert.ToDecimal(e.FieldValue.ToString().Replace("%",""));
                            key_list.Add(keys);
                        }
                        break;
                    case CustomSummaryProcess.Finalize:
                        e.TotalValue = sum.ToString() + "%";
                        break;
                }
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