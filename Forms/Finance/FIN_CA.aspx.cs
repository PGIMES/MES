using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Finance_FIN_CA : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    string FlowID = "A";
    string StepID = "A";
    string m_sid = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["ApplyId_i"] == null) { ViewState["ApplyId_i"] = ""; }
        if (ViewState["Date_i"] == null) { ViewState["Date_i"] = ""; }
        if (ViewState["E_Date_i"] == null) { ViewState["E_Date_i"] = ""; }

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }

        if (Request.QueryString["flowid"] != null)
        {
            FlowID = Request.QueryString["flowid"];
        }

        if (Request.QueryString["stepid"] != null)
        {
            StepID = Request.QueryString["stepid"];
        }

        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        Session["LogUser"] = LogUserModel;

        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            string lssql = @"select a.id, a.FIN_CA_No, a.StartDate, a.StartTime, a.EndDate, a.EndTime, a.TravelRoute, a.Mileage, a.Remark
                                ,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            from[dbo].[Fin_CA_Dtl_Form] a "; 

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    CreateId.Text = LogUserModel.UserId; CreateName.Text = LogUserModel.UserName; CreateTelephone.Text = LogUserModel.Telephone;
                    CreateJobTitleName.Text = LogUserModel.JobTitleName;
                    CreateDomain.Text = LogUserModel.Domain; CreateDomainName.Text = LogUserModel.DomainName;
                    CreateDeptId.Text = ""; CreateDeptName.Text = LogUserModel.DepartName;

                    ApplyDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
                    ApplyId.Text = LogUserModel.UserId; ApplyName.Text = LogUserModel.UserName; ApplyTelephone.Text = LogUserModel.Telephone;
                    ApplyJobTitleName.Text = LogUserModel.JobTitleName;
                    ApplyDomain.Text = LogUserModel.Domain; ApplyDomainName.Text = LogUserModel.DomainName;
                    ApplyDeptId.Text = ""; ApplyDeptName.Text = LogUserModel.DepartName;
                    CarNo.Text = LogUserModel.Car;

                    DataTable dt_costcenter = DbHelperSQL.Query("select * from [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] where [EMPLOYEEID]='" + LogUserModel.UserId + "'").Tables[0];
                    if (dt_costcenter != null)
                    {
                        if (dt_costcenter.Rows.Count > 0)
                        {
                            CreateDeptId.Text = dt_costcenter.Rows[0]["ITEMVALUE"].ToString();
                            ApplyDeptId.Text = dt_costcenter.Rows[0]["ITEMVALUE"].ToString();
                        }
                    }

                }

                lssql += @" where 1=0";
            }
            else
            {
                DataTable ldt = DbHelperSQL.Query("select * from [Fin_CA_Main_Form] where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    //表头基本信息
                    Pgi.Auto.Control.SetControlValue("Fin_CA_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql += @" where a.FIN_CA_No='" + this.m_sid + "' order by a.id";
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            this.gv_d.DataSource = ldt_detail;
            this.gv_d.DataBind();

            setGridIsRead(ldt_detail);

        }
        else
        {

        }

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    public void setGridIsRead(DataTable ldt_detail)
    {
        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请人");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (ldt_flow_pro.Rows.Count == 0)
            {
                this.btnflowSend.Text = "批准";
            }
            if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
            {
                setread(i);
            }

        }
    }

    public void setread(int i)
    {
        ViewState["ApplyId_i"] = "Y"; ViewState["Date_i"] = "Y"; ViewState["E_Date_i"] = "Y";

        btnadd.Visible = false;
        btndel.Visible = false;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["TravelRoute"], "TravelRoute")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["TravelRoute"], "TravelRoute")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["Mileage"], "Mileage")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["Mileage"], "Mileage")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["Remark"], "Remark")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["Remark"], "Remark")).BorderStyle = BorderStyle.None;

    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);

        DataRow ldr = ldt.NewRow();
        for (int j = 0; j < ldt.Columns.Count; j++)
        {

            if (ldt.Columns[j].ColumnName == "numid")
            {
                ldr[ldt.Columns[j].ColumnName] = ldt.Rows.Count <= 0 ? 1 : (Convert.ToInt32(ldt.Rows[ldt.Rows.Count - 1]["numid"]) + 1);
            }
            else
            {
                ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
            }

        }
        ldt.Rows.Add(ldr);

        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();

    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        gv_d.DataSource = ldt;
        gv_d.DataBind();
    }

    
    private bool SaveData(string action)
    {
        bool flag = true;// 保存数据是否成功标识

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("Fin_CA_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "FIN_CA_" + System.DateTime.Now.ToString("yyyyMMdd");
            this.m_sid = Pgi.Auto.Public.GetNo("FIN_CA_", lsid, 0, 4);

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "formno")
                {
                    ls[i].Value = this.m_sid;
                    ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text = this.m_sid;
                    break;
                }

            }
        }

        //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
        DataTable ldt = new DataTable(); 
        ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);

        //主表相关字段值
        string formno_main = "";
        for (int j = 0; j < ls.Count; j++)
        {
            if (ls[j].Code.ToLower() == "formno") { formno_main = ls[j].Value; }
        }

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["FIN_CA_No"] = formno_main;
        }

        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "Fin_CA_Main_Form"));


        if (ldt.Rows.Count > 0)//出差预算明细
        {
            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            string dtl_ids = "";
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["id"].ToString() != "") { dtl_ids = dtl_ids + ldt.Rows[i]["id"].ToString() + ","; }
            }
            if (dtl_ids != "")
            {
                dtl_ids = dtl_ids.Substring(0, dtl_ids.Length - 1);
                ls_del.Sql = "delete from Fin_CA_Dtl_Form where FIN_CA_No='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from Fin_CA_Dtl_Form where FIN_CA_No='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "Fin_CA_Dtl_Form", "id", "Column1,numid,flag");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }
        }

        //-----------------------------------------------------------需要即时验证是否存在正在申请的或者保存着的项目号

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        if (ln > 0)
        {
            flag = true;

            string title = "私车公用申请--" + this.m_sid;

            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";

        }
        else
        {
            flag = false;
        }

        return flag;
    }



    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Pgi.Auto.Public.MsgBox(Page, "alert", "GP12调整中,请2018/7/11再申请，谢谢！");
        //保存数据
        bool flag = SaveData("save");
        //保存当前流程
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //Pgi.Auto.Public.MsgBox(Page, "alert", "GP12调整中,请2018/7/11再签核，谢谢！");
        //保存数据
        bool flag = SaveData("submit");
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion
}