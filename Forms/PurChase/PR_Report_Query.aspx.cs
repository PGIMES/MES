using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using DevExpress.Web;
using System.Drawing;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;

public partial class Forms_PurChase_PR_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["empid"] == null )
        //{   // 给Session["empid"] & Session["job"] 初始化
        //    InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        //}
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        if (!IsPostBack)
        {   //初始化日期
            ViewState["empname"] = LogUserModel.UserName;
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            QueryASPxGridView();
        }
        //else
        //{
        //    DataTable dt = Pgi.Auto.Control.AgvToDt(this.GV_PART);
        //    this.GV_PART.Columns.Clear();
        //    Pgi.Auto.Control.SetGrid("PRQuery", "", this.GV_PART, dt);
        //    this.GV_PART.Columns[27].Caption = "申请人操作 ";
        //}
        QueryASPxGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        //
        DataTable dt = DbHelperSQL.Query("exec Pur_PR_Query  '','','',''").Tables[0];
        this.GV_PART.Columns.Clear();
        Pgi.Auto.Control.SetGrid("PRQuery", "", this.GV_PART, dt);
        this.GV_PART.Columns["del"].Caption = "申请人操作 ";


    }
    //protected void GV_PART_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{

    //}
    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        
       
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType != GridViewRowType.Data) return;
        DevExpress.Web.GridViewDataColumn t = this.GV_PART.Columns[27] as DevExpress.Web.GridViewDataColumn;

        DevExpress.Web.ASPxButton tb1 = (DevExpress.Web.ASPxButton)this.GV_PART.FindRowCellTemplateControl(e.VisibleIndex, t, "del");
        string status = e.GetValue("Status").ToString();

        if (status == "Done")
        { tb1.Visible = true; }
       
      
    }
    protected void GV_PART_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        //object o = GV_PART.GetRow(e.VisibleIndex);//可以获取某行对应的数据源的object, 再把它强制转换成实体对象
        ViewState["prno"]= GV_PART.GetRowValues(e.VisibleIndex, "PRNo").ToString();//可以获取某行某列的值
        ViewState["rowid"] = GV_PART.GetRowValues(e.VisibleIndex, "rowid").ToString();//行号
        string CreateByName=GV_PART.GetRowValues(e.VisibleIndex, "CreateByName").ToString();//申请人
        //if (CreateByName != (string)ViewState["empname"])
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('非请购人不可删除！')", true);
        //    return;
        //}
        //else
        //{
        string str = "layer.confirm('若确认删除,则取消请购,请确认是否删除？', {  btn: ['是','否'] }, function(index){  $('#MainContent_btnNext').click();layer.close(index);}, function(){  });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
        //}
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
      
        System.Text.StringBuilder result = new StringBuilder();
        result.Append(string.Format("update [dbo].[PUR_PR_Dtl_Form] set [Status]='1' where [PRNo]='{0}' and [rowid]={1}  ", (string)ViewState["prno"], (string)ViewState["rowid"]));
        int rows = DbHelperSQL.ExecuteSql(result.ToString());
        QueryASPxGridView();
    }
}