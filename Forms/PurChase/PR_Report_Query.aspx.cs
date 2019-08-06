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
       
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        if (!IsPostBack)
        {   //初始化日期
            ViewState["empname"] = LogUserModel.UserName;
            ViewState["empid"] = LogUserModel.UserId;
            ViewState["dept_ame"] = LogUserModel.DepartName;
            txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
           // QueryASPxGridView();
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
        DataTable dt = DbHelperSQL.Query("exec [Pur_PR_Query_New] '" + drop_type.SelectedValue + "','" + txtDateFrom.Text + "','" + txtDateTo.Text 
            + "','" + (string)ViewState["empid"] + "','" + (string)ViewState["dept_ame"] + "','" + txtUserFor.Text + "'").Tables[0];
       this.GV_PART.Columns.Clear();
        Pgi.Auto.Control.SetGrid("Pur_PR_Query", "Query", this.GV_PART, dt);
        this.GV_PART.Columns["del"].Caption = "申请人操作 ";

        for (int i = 20; i < this.GV_PART.DataColumns.Count-5; i++)
        {

            this.GV_PART.Columns[i].HeaderStyle.BackColor = Color.Khaki;

        }

    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (this.GV_PART.DataColumns["PRNo"] != null)
            {
                int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["PRNo"]).VisibleIndex;
                string PRNo = Convert.ToString(e.GetValue("PRNo"));
                string groupid = Convert.ToString(e.GetValue("GroupID"));
                string stepid = Convert.ToString(e.GetValue("StepID"));
                //e.Row.Cells[index].Text = "<a href='http://172.16.5.26:8030/Forms/MaterialBase/ToolKnife.aspx?instanceid=" + e.GetValue("wlh") + "&domain=" + site + "' target='_blank'>" + value.ToString() + "</a>";
                e.Row.Cells[index].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=62676129-f059-4c92-bd5c-86897f5b0d5&instanceid=" + e.GetValue("PRNo") + "&stepid=" + stepid + "&groupid=" + groupid + "&display=1' target='_blank'>" + PRNo.ToString() + "</a>";

                //  ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).PropertiesHyperLinkEdit.NavigateUrlFormatString = "Forproducts.aspx?wlh=" + e.GetValue("wlh") + "&ljh=" + ljh + "&site=" + site + "";
            }
        }
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType != GridViewRowType.Data) return;
        DevExpress.Web.GridViewDataColumn t = this.GV_PART.Columns[30] as DevExpress.Web.GridViewDataColumn;

        DevExpress.Web.ASPxButton tb1 = (DevExpress.Web.ASPxButton)this.GV_PART.FindRowCellTemplateControl(e.VisibleIndex, t, "del");
        string status = e.GetValue("Status").ToString();
        string IsDel = (string)e.GetValue("IsDel");

        if (status == "Undo" && IsDel == "0")
        { tb1.Visible = true; }
        else
        { tb1.Visible = false; }
        if (IsDel == "1")
        { e.Row.BackColor = Color.Gray; }

        //PO单号:PO单号<br>QAD单号
        string PoNo = e.GetValue("PoNo").ToString();
        e.Row.Cells[20].Text = PoNo;

    }
    protected void GV_PART_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        //object o = GV_PART.GetRow(e.VisibleIndex);//可以获取某行对应的数据源的object, 再把它强制转换成实体对象
        ViewState["prno"]= GV_PART.GetRowValues(e.VisibleIndex, "PRNo").ToString();//可以获取某行某列的值
        //ViewState["prno"] = ((HyperLink)GV_PART.FindRowCellTemplateControl(e.VisibleIndex, GV_PART.DataColumns[5], "PRNo")).Text;
        ViewState["rowid"] = GV_PART.GetRowValues(e.VisibleIndex, "rowid").ToString();//行号
        string CreateByName=GV_PART.GetRowValues(e.VisibleIndex, "CreateByName").ToString();//申请人
        if (CreateByName != (string)ViewState["empname"])
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('非请购人不可删除！')", true);
            return;
        }
        else
        {
        string str = "layer.confirm('若确认删除,则取消请购,请确认是否删除？', {  btn: ['是','否'] }, function(index){  $('#MainContent_btnNext').click();layer.close(index);}, function(){  });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
      
        System.Text.StringBuilder result = new StringBuilder();
        result.Append(string.Format("update [dbo].[PUR_PR_Dtl_Form] set [Status]='1' where [PRNo]='{0}' and [rowid]={1}  ", (string)ViewState["prno"], (string)ViewState["rowid"]));
        int rows = DbHelperSQL.ExecuteSql(result.ToString());
        QueryASPxGridView();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("请购单" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }
    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_ExportRenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Column.Caption == "PO单号")
            {
                if (e.Value != DBNull.Value)
                {
                    e.TextValue = Convert.ToString(e.Value).Replace("<br>", "\r\n");
                }
            }

        }
    }
}