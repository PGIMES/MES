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

public partial class Forms_Finance_FIN_T : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    public string SQ_StepID = "F882B5B3-78BE-4804-BB42-72C0D6B680AB";

    string FlowID = "A";
    string StepID = "A";
    string m_sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["Traveler_i"] == null) { ViewState["Traveler_i"] = ""; }

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
        Session["LogUser_CurPage"] = LogUserModel;

        ////加载表头控件
        //List<TableRow> ls = ShowControl("PGI_GYLX_Main_Form", "HEAD_NEW", 4, "", "", "lineread", "linewrite");//Pgi.Auto.Control.form-control input-s-sm
        //for (int i = 0; i < ls.Count; i++)
        //{
        //    this.tblCPXX.Rows.Add(ls[i]);
        //}


        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            string lssql = "";string IsHrReserve = "";

            DataTable ldt_detail_hr = null;
            string lssql_hr = @"";

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    ApplyDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
                    ApplyId.Text = LogUserModel.UserId; ApplyName.Text = LogUserModel.UserName; ApplyTelephone.Text = LogUserModel.Telephone;
                    ApplyJobTitleName.Text = LogUserModel.JobTitleName;
                    ApplyDomain.Text = LogUserModel.Domain; ApplyDomainName.Text = LogUserModel.DomainName;
                    ApplyDeptId.Text = "0000"; ApplyDeptName.Text = LogUserModel.DepartName;
                }

                lssql = @"select null id, null FIN_T_No, a.Cost_Code CostCode, null BudgetTotalCost, null IsHrReserve, null BudgetRemark
	                        ,ROW_NUMBER() OVER (ORDER BY a.sort) numid   
	                        ,a.cost_category+'/'+a.cost_item+'['+a.cost_code+']' as CostCodeDesc
                        from [dbo].[PGI_BASE_FINANCE_CostCategory] a 
                        where cost_type='T' order by sort";
            }
            else
            {
                DataTable ldt = DbHelperSQL.Query("select * from [PGI_FIN_T_Main_Form] where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    //表头基本信息
                    //txt_CreateById.Value = ldt.Rows[0]["CreateById"].ToString();
                    //txt_CreateByName.Value = ldt.Rows[0]["CreateByName"].ToString();
                    //txt_CreateByDept.Value = ldt.Rows[0]["CreateByDept"].ToString();
                    //txt_CreateDate.Value = ldt.Rows[0]["CreateDate"].ToString();
                    Pgi.Auto.Control.SetControlValue("PGI_FIN_T_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                    IsHrReserve = ldt.Rows[0]["IsHrReserve"].ToString();
                    //txt_domain.Text = ldt.Rows[0]["domain"].ToString(); txt_pn.Text = ldt.Rows[0]["pn"].ToString();
                    //modifyremark.Text = ldt.Rows[0]["modifyremark"].ToString();
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql = @"select a.id, a.FIN_T_No, a.CostCode, a.BudgetTotalCost, a.IsHrReserve, a.BudgetRemark
                            ,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            ,b.cost_category+'/'+b.cost_item+'['+b.cost_code+']' as CostCodeDesc
                        from[dbo].[PGI_FIN_T_Dtl_Form] a 
                            left join [dbo].[PGI_BASE_FINANCE_CostCategory] b
                        where FIN_T_No='" + this.m_sid + "' order by a.id"; 
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            this.gv_d.DataSource = ldt_detail;
            this.gv_d.DataBind();

            if (IsHrReserve == "Y")//人事预定信息
            {
                lssql_hr = @"select a.id, a.FIN_T_No, a.TravelerId, a.TravelerName, a.StartFromPlace, a.EndToPlace
                                , a.StartDate, a.StartTime, a.BudgetCost, a.Vehicle, a.Remark, a.ScheduledFlight, a.ActualCost 
                                ,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            from [dbo].[PGI_FIN_T_Dtl_HR_Form]";
                ldt_detail_hr = DbHelperSQL.Query(lssql_hr).Tables[0];
                this.gv_d_hr.DataSource = ldt_detail_hr;
                this.gv_d_hr.DataBind();
            }


            setGridIsRead(ldt_detail, IsHrReserve, ldt_detail_hr);

        }
        else
        {
            //暂时不需要
            //DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
            //IsGrid_pro = "Y";
            //this.gv_d.DataSource = ldt;
            //this.gv_d.DataBind();

            //DataTable ldt_yz = Pgi.Auto.Control.AgvToDt(this.gv_d_yz);
            //IsGrid_yz = "Y";
            //this.gv_d_yz.DataSource = ldt_yz;
            //this.gv_d_yz.DataBind();

        }

        //if ((StepID.ToUpper() != SQ_StepID && StepID != "A") || (this.m_sid != "" && txt_CreateById.Value == ""))
        //{
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        //    ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
        //    ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;


        //    if (((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).SelectedValue == "Y")
        //    {
        //        ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;
        //    }
        //    else
        //    {
        //        ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = true;
        //    }

        //}

        //if (StepID.ToUpper() != SQ_StepID && StepID != "A")
        //{
        //    modifyremark.ReadOnly = true;
        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;
        //}

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    public void setGridIsRead(DataTable ldt_detail,string IsHrReserve, DataTable ldt_detail_hr)
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
            if ((ldt_detail.Rows[i]["CostCode"].ToString() == "T001" || ldt_detail.Rows[i]["CostCode"].ToString() == "T002") && ldt_detail.Rows[i]["IsHrReserve"].ToString() == "是")
            {//差旅/机票费 差旅/火车票 人事预定选择“是”的情况下，预算金额只读
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).BorderStyle = BorderStyle.None;
            }
            if (ldt_detail.Rows[i]["CostCode"].ToString() == "T007")//差旅/补贴
            {
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).BorderStyle = BorderStyle.None;
            }
            if (ldt_detail.Rows[i]["CostCode"].ToString() != "T001" && ldt_detail.Rows[i]["CostCode"].ToString() != "T002")
            {
                ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["IsHrReserve"], "IsHrReserve")).Visible = false;
            }
            if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
            {
                setread(i);
            }

        }
    }

    public void setread(int i)
    {
        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
        //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

        //((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;
        //((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;

        //modifyremark.ReadOnly = true;

        ViewState["Traveler_i"] = "Y";

        //btndel.Visible = false;

        //if (i == 0)
        //{
        //    gv_d.Columns[gv_d.VisibleColumns.Count - 1].Visible = false;
        //    gv_d.Columns[0].Visible = false;
        //}

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op"], "op")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op"], "op")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_desc"], "op_desc")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_desc"], "op_desc")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_remark"], "op_remark")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_remark"], "op_remark")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgNum"], "JgNum")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgNum"], "JgNum")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgSec"], "JgSec")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgSec"], "JgSec")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["WaitSec"], "WaitSec")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["WaitSec"], "WaitSec")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ZjSecc"], "ZjSecc")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ZjSecc"], "ZjSecc")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JtNum"], "JtNum")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JtNum"], "JtNum")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col1"], "col1")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col1"], "col1")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col2"], "col2")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col2"], "col2")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["EquipmentRate"], "EquipmentRate")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["EquipmentRate"], "EquipmentRate")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col6"], "col6")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col6"], "col6")).BorderStyle = BorderStyle.None;
    }


    protected void gv_d_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
        {
            return;
        }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);

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
        this.gv_d_hr.DataSource = ldt;
        this.gv_d_hr.DataBind();

    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);
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
        gv_d_hr.DataSource = ldt;
        gv_d_hr.DataBind();
    }

    protected void gv_d_hr_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        if (param == "clear")
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);
            ldt.Rows.Clear();
            ldt.AcceptChanges();
            gv_d_hr.DataSource = ldt;
            gv_d_hr.DataBind();
        }

    }

    private bool SaveData(string action)
    {
        bool flag = true;// 保存数据是否成功标识

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("PGI_FIN_T_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        string ApplyId = ((TextBox)this.FindControl("ctl00$MainContent$ApplyId")).Text.Trim();
        string ApplyName = ((TextBox)this.FindControl("ctl00$MainContent$ApplyName")).Text.Trim();
        string PlanStartTime = ((TextBox)this.FindControl("ctl00$MainContent$PlanStartTime")).Text.Trim();
        string IsHrReserveByForm = ((TextBox)this.FindControl("ctl00$MainContent$IsHrReserveByForm")).Text.Trim();

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "FIN_T_" + System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
            this.m_sid = Pgi.Auto.Public.GetNo("FIN_T_", lsid, 0, 4);

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
        DataTable ldt = new DataTable(); DataTable ldt_hr = new DataTable();
        ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        if (IsHrReserveByForm == "是")
        {
            ldt_hr = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);
        }

        //主表相关字段值
        string formno_main = "";
        for (int j = 0; j < ls.Count; j++)
        {
            if (ls[j].Code.ToLower() == "formno") { formno_main = ls[j].Value; }
        }

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["FIN_T_No"] = formno_main;
        }
        if (ldt_hr != null)
        {
            for (int i = 0; i < ldt_hr.Rows.Count; i++)
            {
                ldt_hr.Rows[i]["FIN_T_No"] = formno_main;
            }
        }

        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_FIN_T_Main_Form"));


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
                ls_del.Sql = "delete from PGI_FIN_T_Dtl_Form where FIN_T_No='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_FIN_T_Dtl_Form where FIN_T_No='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_FIN_T_Dtl_Form", "id", "Column1,numid,flag");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }
        }

        if (ldt_hr.Rows.Count > 0)//人事预定明细
        {
            Pgi.Auto.Common ls_del_hr = new Pgi.Auto.Common();
            string dtl_ids_hr = "";
            for (int i = 0; i < ldt_hr.Rows.Count; i++)
            {
                if (ldt_hr.Rows[i]["id"].ToString() != "") { dtl_ids_hr = dtl_ids_hr + ldt_hr.Rows[i]["id"].ToString() + ","; }
            }
            if (dtl_ids_hr != "")
            {
                dtl_ids_hr = dtl_ids_hr.Substring(0, dtl_ids_hr.Length - 1);
                ls_del_hr.Sql = "delete from PGI_FIN_T_Dtl_Form where FIN_T_No='" + m_sid + "' and id not in(" + dtl_ids_hr + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del_hr.Sql = "delete from PGI_FIN_T_Dtl_Form where FIN_T_No='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del_hr);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1_hr = Pgi.Auto.Control.GetList(ldt, "PGI_FIN_T_Dtl_HR_Form", "id", "Column1,numid,flag");
            for (int i = 0; i < ls1_hr.Count; i++)
            {
                ls_sum.Add(ls1_hr[i]);
            }
        }

        //-----------------------------------------------------------需要即时验证是否存在正在申请的或者保存着的项目号

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        if (ln > 0)
        {
            flag = true;

            string title = "差旅申请-" + ApplyName + "(" + ApplyId + ")-预计出发时间" + PlanStartTime + "--" + this.m_sid;

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