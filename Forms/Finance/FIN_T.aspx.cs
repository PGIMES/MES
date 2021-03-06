﻿using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    public string is_hr_zy="";

    string FlowID = "A";
    string StepID = "A";
    string m_sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["ApplyId_i"] == null) { ViewState["ApplyId_i"] = ""; }
        if (ViewState["PlanAttendant_i"] == null) { ViewState["PlanAttendant_i"] = ""; }
        if (ViewState["IsHrReserve_i"] == null) { ViewState["IsHrReserve_i"] = ""; }
        if (ViewState["Traveler_i"] == null) { ViewState["Traveler_i"] = ""; }
        if (ViewState["Vehicle_i"] == null) { ViewState["Vehicle_i"] = ""; }

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
        

        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            string lssql = "";string str_IsHrReserve = "";

            DataTable ldt_detail_hr = null;
            string lssql_hr = @"";
            

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

                    DataTable dt_costcenter = DbHelperSQL.Query("select * from [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] where [EMPLOYEEID]='" + LogUserModel.UserId + "'").Tables[0];
                    if (dt_costcenter!=null)
                    {
                        if (dt_costcenter.Rows.Count>0)
                        {
                            CreateDeptId.Text = dt_costcenter.Rows[0]["ITEMVALUE"].ToString();
                            ApplyDeptId.Text = dt_costcenter.Rows[0]["ITEMVALUE"].ToString();
                        }
                    }

                }

                lssql = @"select null id, null FIN_T_No, a.Cost_Code CostCode, null BudgetTotalCost, case when a.Cost_Code='T001' or a.Cost_Code='T002' or a.Cost_Code='T000' then '否' else null end IsHrReserve, null BudgetRemark
                            , null BudgetStandardCost, null BudgetCount, null BudgetPersonCount,null BudgetOtherCost
	                        ,ROW_NUMBER() OVER (ORDER BY a.sort) numid   
                            ,a.cost_item as CostCodeDesc,Standardlimit,cost_unit,BudgetCount_1_unit,BudgetCount_2_unit
                        from [dbo].[Fin_Base_CostCate] a 
                        where cost_type='T' order by sort";
            }
            else
            {
                DataTable ldt = DbHelperSQL.Query("select * from [Fin_T_Main_Form] where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    //表头基本信息
                    Pgi.Auto.Control.SetControlValue("Fin_T_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                    str_IsHrReserve = ldt.Rows[0]["IsHrReserveByForm"].ToString();

                    //显示文件
                    if (ldt.Rows[0]["files"].ToString() != "")
                    {
                        this.ip_filelist_db.Value = ldt.Rows[0]["files"].ToString();
                        bindtab();
                    }
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql = @"select a.id, a.FIN_T_No, a.CostCode, a.BudgetTotalCost, a.IsHrReserve, a.BudgetRemark
                            , a.BudgetStandardCost, a.BudgetCount, a.BudgetPersonCount,a.BudgetOtherCost
                            ,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            ,b.cost_item as CostCodeDesc,b.Standardlimit,b.cost_unit,b.BudgetCount_1_unit,b.BudgetCount_2_unit
                        from[dbo].[Fin_T_Dtl_Form] a 
                            left join (select * from [dbo].[Fin_Base_CostCate] where cost_type='T') b on a.CostCode=b.cost_code
                        where FIN_T_No='" + this.m_sid + "' order by a.id"; 
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            this.gv_d.DataSource = ldt_detail;
            this.gv_d.DataBind();

             if (str_IsHrReserve == "是")//人事预定信息
            {
                lssql_hr = @"select a.id, a.FIN_T_No, a.TravelerId, a.TravelerName, a.StartFromPlace, a.EndToPlace
                                , a.StartDateTime, a.BudgetCost, a.Vehicle, a.Remark, a.ScheduledFlight, a.ActualCost 
                                ,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            from [dbo].[Fin_T_Dtl_HR_Form] a
                            where FIN_T_No='" + this.m_sid + "' order by a.id";
                ldt_detail_hr = DbHelperSQL.Query(lssql_hr).Tables[0];
                this.gv_d_hr.DataSource = ldt_detail_hr;
                this.gv_d_hr.DataBind();

                GetGrid(ldt_detail_hr);
            }


            setGridIsRead(ldt_detail, ldt_detail_hr);
           
        }
        else
        {

        }

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    void bindtab()
    {
        bool is_del = true;
        DataTable ldt_flow = DbHelperSQL.Query(@"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                                        where cast(stepid as varchar(36))=cast('" + Request.QueryString["stepid"] + "' as varchar(36)) and cast(flowid as varchar(36))=cast('"
                                        + Request.QueryString["flowid"] + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='申请人'").Tables[0];

        if (ldt_flow.Rows.Count == 0)
        {
            is_del = false;
        }
        if (Request.QueryString["display"] != null)//未发送之前
        {
            is_del = false;
        }

        tab1.Rows.Clear();
        string[] ls_files = this.ip_filelist_db.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_files.Length; i++)
        {
            TableRow tempRow = new TableRow();
            string[] ls_files_2 = ls_files[i].Split(',');

            HyperLink hl = new HyperLink();
            Label lb = new Label();

            hl.Text = ls_files_2[0].ToString();
            hl.NavigateUrl = ls_files_2[1].ToString();
            hl.Target = "_blank";

            lb.Text = ls_files_2[2].ToString();

            TableCell td1 = new TableCell(); td1.Controls.Add(hl); td1.Width = Unit.Pixel(400);
            tempRow.Cells.Add(td1);

            TableCell td2 = new TableCell(); td2.Controls.Add(lb); td2.Width = Unit.Pixel(60);
            tempRow.Cells.Add(td2);

            if (is_del)
            {
                //Button Btn = new Button(); 
                LinkButton Btn = new LinkButton();
                Btn.Text = "删除"; Btn.ID = "btn_" + i.ToString(); Btn.Click += new EventHandler(Btn_Click);

                TableCell td3 = new TableCell(); td3.Controls.Add(Btn);
                tempRow.Cells.Add(td3);
            }
            tab1.Rows.Add(tempRow);
        }
    }

    void Btn_Click(object sender, EventArgs e)
    {
        //var btn = sender as Button;
        var btn = sender as LinkButton;
        int index = Convert.ToInt32(btn.ID.Substring(4));

        string filedb = ip_filelist_db.Value;
        string[] ls_files = filedb.Split('|');

        string files = "";
        for (int i = 0; i < ls_files.Length; i++)
        {
            if (i != index) { files += ls_files[i] + "|"; }
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        ip_filelist_db.Value = files;

        bindtab();
    }

    protected void GetGrid(DataTable DT)
    {
        DataTable ldt = DT;
        int index = gv_d_hr.VisibleRowCount;
        for (int i = 0; i < gv_d_hr.VisibleRowCount; i++)
        {

            DevExpress.Web.ASPxDateEdit tb1 = (DevExpress.Web.ASPxDateEdit)gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv_d_hr.Columns["StartDateTime"], "StartDateTime");
            if (ldt.Rows[i]["StartDateTime"].ToString() != "")
            {
                tb1.Date = Convert.ToDateTime(ldt.Rows[i]["StartDateTime"].ToString());
            }
        }
    }

    public void setGridIsRead(DataTable ldt_detail, DataTable ldt_detail_hr)
    {
        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请人");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        //特殊处理，签核界面，明细的框框拿掉
        string lssql_hr = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname like '%{3}%'";
        string sql_pro_hr = string.Format(lssql_hr, StepID, FlowID, m_sid, "行政专员");
        DataTable ldt_flow_pro_hr = DbHelperSQL.Query(sql_pro_hr).Tables[0];


        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (ldt_flow_pro.Rows.Count == 0 && (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID))
            {
                this.btnflowSend.Text = "批准";
            }
            if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
            {
                ViewState["ApplyId_i"] = "Y"; ViewState["PlanAttendant_i"] = "Y";
                setread(i);
            }
            else
            {
                if (Request.QueryString["display"] != null)
                {
                    ViewState["ApplyId_i"] = "Y"; ViewState["PlanAttendant_i"] = "Y";
                    setread(i);
                }
            }

        }

        if (ldt_detail_hr == null)
        {

        }
        else
        {
            for (int i = 0; i < ldt_detail_hr.Rows.Count; i++)
            {
                if (ldt_flow_pro.Rows.Count == 0)
                {
                    this.btnflowSend.Text = "批准";
                }
                if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
                {
                    setread_hr(i, ldt_flow_pro_hr);
                }
            }
        }
    }

    public void setread(int i)
    {
        ViewState["IsHrReserve_i"] = "Y";

        this.uploadcontrol.Visible = false;

        ((TextBox)this.FindControl("ctl00$MainContent$PlanStartTime")).CssClass = "lineread";
        ((TextBox)this.FindControl("ctl00$MainContent$PlanStartTime")).Attributes.Remove("onclick");

        ((TextBox)this.FindControl("ctl00$MainContent$PlanEndTime")).CssClass = "lineread";
        ((TextBox)this.FindControl("ctl00$MainContent$PlanEndTime")).Attributes.Remove("onclick");

        ((TextBox)this.FindControl("ctl00$MainContent$PlanAttendant")).Width = Unit.Pixel(260);

        ((TextBox)this.FindControl("ctl00$MainContent$TravelPlace")).CssClass = "lineread";
        ((TextBox)this.FindControl("ctl00$MainContent$TravelPlace")).ReadOnly = true;

        ((TextBox)this.FindControl("ctl00$MainContent$TravelReason")).BackColor = System.Drawing.ColorTranslator.FromHtml("#fff"); 
        ((TextBox)this.FindControl("ctl00$MainContent$TravelReason")).ReadOnly = true;

        ((DropDownList)this.FindControl("ctl00$MainContent$TravelType")).Enabled = false;
        ((DropDownList)this.FindControl("ctl00$MainContent$TravelType")).CssClass = "lineread";

        ((DropDownList)this.FindControl("ctl00$MainContent$TravelClass")).Enabled = false;
        ((DropDownList)this.FindControl("ctl00$MainContent$TravelClass")).CssClass = "lineread";

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetStandardCost"], "BudgetStandardCost")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetStandardCost"], "BudgetStandardCost")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetCount"], "BudgetCount")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetCount"], "BudgetCount")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetPersonCount"], "BudgetPersonCount")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetPersonCount"], "BudgetPersonCount")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetOtherCost"], "BudgetOtherCost")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetOtherCost"], "BudgetOtherCost")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).ReadOnly = true;
        //((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetTotalCost"], "BudgetTotalCost")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetRemark"], "BudgetRemark")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["BudgetRemark"], "BudgetRemark")).BorderStyle = BorderStyle.None;

       
    }

    public void setread_hr(int i,DataTable ldt_flow_pro_hr)
    {
        ViewState["Traveler_i"] = "Y"; ViewState["Vehicle_i"] = "Y";

        btnadd.Visible = false;
        btndel.Visible = false;


        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["TravelerName"], "TravelerName")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["TravelerName"], "TravelerName")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["StartFromPlace"], "StartFromPlace")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["StartFromPlace"], "StartFromPlace")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["EndToPlace"], "EndToPlace")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["EndToPlace"], "EndToPlace")).BorderStyle = BorderStyle.None;

        ((DevExpress.Web.ASPxDateEdit)gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv_d_hr.Columns["StartDateTime"], "StartDateTime")).Enabled = false;

        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["BudgetCost"], "BudgetCost")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["BudgetCost"], "BudgetCost")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["Remark"], "Remark")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_hr.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["Remark"], "Remark")).BorderStyle = BorderStyle.None;


        if (ldt_flow_pro_hr.Rows.Count > 0 && Request.QueryString["display"] == null)//人事签核
        {
            is_hr_zy = "Y";
        }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        add_row(1);
    }

    protected void add_row(int lnadd_rows)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);

        for (int i = 0; i < lnadd_rows; i++)
        {
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
        }

        ldt.AcceptChanges();
        this.gv_d_hr.DataSource = ldt;
        this.gv_d_hr.DataBind();

        GetGrid(ldt);
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

        GetGrid(ldt);
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
            GetGrid(ldt);
        }
        if (param == "add")
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_hr);
            if (ldt.Rows.Count<=0)
            {
                add_row(2);
            }
        }

    }

    //protected void gv_d_hr_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    //{
    //    if (e.RowType != GridViewRowType.Data)
    //    {
    //        return;
    //    }
    //    DataTable dtl_hr = ((DataTable)ViewState["dtl_hr"]);
    //    ASPxComboBox tb1 = ((ASPxComboBox)this.gv_d_hr.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["Vehicle"], "Vehicle"));

    //    if (dtl_hr != null)
    //    {
    //        DataTable ldt_Vehicle = DbHelperSQL.Query(@"select '飞机' as Vehicle union all select '火车' as Vehicle ").Tables[0];
    //        tb1.DataSource = ldt_Vehicle;
    //        tb1.TextField = "Vehicle";
    //        tb1.ValueField = "Vehicle";
    //        tb1.DataBind();
    //        tb1.Value = dtl_hr.Rows.Count > 0 ? dtl_hr.Rows[e.VisibleIndex]["Vehicle"].ToString() : "";
    //    }



    //    ASPxComboBox tb1 = ((ASPxComboBox)this.gv_d_hr.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv_d_hr.Columns["TravelerName"], "TravelerName"));
    //    if (tb1 != null)
    //    {
    //        string pa = PlanAttendant.Text.Trim();
    //        string[] sArray = pa.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("workcode", typeof(string)); dt.Columns.Add("workname", typeof(string));

    //        for (int i = 0; i < sArray.Length; i++)
    //        {
    //            DataRow dr = dt.NewRow();
    //            dr["workcode"] = sArray[i].Substring(0, sArray[i].IndexOf('('));
    //            dr["workname"] = sArray[i].Substring(sArray[i].IndexOf('(') + 1, sArray[i].Length - sArray[i].IndexOf('(') - 2);
    //            dt.Rows.Add(dr);
    //        }

    //        tb1.DataSource = dt;
    //        tb1.TextField = "workname";
    //        tb1.ValueField = "workcode";
    //        tb1.DataBind();
    //        tb1.Value = dt.Rows.Count > 0 ? dt.Rows[e.VisibleIndex]["workcode"].ToString() : "";
    //    }
    //}

    [WebMethod]
    public static string CheckData(string appuserid)
    {
        DataTable dt_manager = null; string manager_flag = "";
        CheckData_manager(appuserid, out dt_manager, out manager_flag);

        string result = "[{\"manager_flag\":\"" + manager_flag+ "\"}]";
        return result;

    }

    public static void CheckData_manager(string appuserid, out DataTable dt_manager, out string manager_flag)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        manager_flag = "";

        dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + appuserid + "')").Tables[0];

        if (dt_manager.Rows[0]["manager_id"].ToString() == "")
        {
            manager_flag += "申请人(" + appuserid + ")的部门经理不存在，不能提交!<br />";
        }
        if (dt_manager.Rows[0]["fz_id"].ToString() == "")
        {
            manager_flag += "申请人(" + appuserid + ")的分管副总不存在，不能提交!<br />";
        }

    }

    private bool SaveData(string action)
    {
        bool flag = true;// 保存数据是否成功标识
        
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("Fin_T_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        string ApplyId = ((TextBox)this.FindControl("ctl00$MainContent$ApplyId")).Text.Trim();
        string ApplyName = ((TextBox)this.FindControl("ctl00$MainContent$ApplyName")).Text.Trim();
        string PlanStartTime = ((TextBox)this.FindControl("ctl00$MainContent$PlanStartTime")).Text.Trim();
        string IsHrReserveByForm = ((TextBox)this.FindControl("ctl00$MainContent$IsHrReserveByForm")).Text.Trim();

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "FIN_T_" + System.DateTime.Now.ToString("yyyyMMdd");
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

        //新增部门经理，分管副总签核人字段值
        DataTable dt_manager = null; string manager_flag = "";
        CheckData_manager(ApplyId, out dt_manager, out manager_flag);

        Pgi.Auto.Common lc_deptm = new Pgi.Auto.Common();
        lc_deptm.Code = "deptm";
        lc_deptm.Key = "";
        lc_deptm.Value = "u_" + dt_manager.Rows[0]["manager_id"].ToString();
        ls.Add(lc_deptm);

        Pgi.Auto.Common lc_deptmfg = new Pgi.Auto.Common();
        lc_deptmfg.Code = "deptmfg";
        lc_deptmfg.Key = "";
        lc_deptmfg.Value = "u_" + dt_manager.Rows[0]["fz_id"].ToString();
        ls.Add(lc_deptmfg);

        //新增附件字段值
        string filepath = "";
        string[] ls_files = ip_filelist.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_2 = item.Split(',');
            
            FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

            var sorpath = @"\"+ savepath + @"\";
            var despath = MapPath("~") + sorpath + @"\" + m_sid + @"\";
            if (!System.IO.Directory.Exists(despath))
            {
                System.IO.Directory.CreateDirectory(despath);
            }
            string tmp = despath + ls_files_2[1].Replace(sorpath, "");
            if (File.Exists(tmp))
            {
                File.Delete(tmp);
            }
            fi.MoveTo(tmp);

            filepath += item.Replace(sorpath, sorpath + m_sid + @"\") + "|";
           
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            filepath += item + "|";
        }
        if (filepath != "") { filepath = filepath.Substring(0, filepath.Length - 1); }

        // 增加上传文件列
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "files";
        lcfile.Key = "";
        lcfile.Value = filepath;
        ls.Add(lcfile);


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
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "Fin_T_Main_Form"));


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
                ls_del.Sql = "delete from Fin_T_Dtl_Form where FIN_T_No='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from Fin_T_Dtl_Form where FIN_T_No='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "Fin_T_Dtl_Form", "id", "Column1,numid,flag,CostCodeDesc,Standardlimit,cost_unit,BudgetCount_1_unit,BudgetCount_2_unit");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }
        }

        if (ldt_hr.Rows.Count > 0 && IsHrReserveByForm == "是")//人事预定明细
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
                ls_del_hr.Sql = "delete from Fin_T_Dtl_HR_Form where FIN_T_No='" + m_sid + "' and id not in(" + dtl_ids_hr + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del_hr.Sql = "delete from Fin_T_Dtl_HR_Form where FIN_T_No='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del_hr);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1_hr = Pgi.Auto.Control.GetList(ldt_hr, "Fin_T_Dtl_HR_Form", "id", "Column1,numid,flag");
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

            string title = "差旅申请【" + ApplyName + "(" + ApplyId + ")：" + PlanStartTime + "】--" + this.m_sid;

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
    ////终止按钮
    //protected void btnStopFlow_Click(object sender, EventArgs e)
    //{
    //    string taskid = Request.QueryString["taskid"];
    //    //string msg = taskid.IsGuid() ? new RoadFlow.Platform.WorkFlowTask().EndTask(taskid.ToGuid()) : "参数错误";
    //    string msg = taskid.IsGuid() ? EndTask(taskid.ToGuid()) : "参数错误";
    //    if ("1" != msg)
    //    {
    //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "err", "alert('" + msg + "')", true);
    //        return;
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "err", "alert('终止成功!'); try{top.mainDialog.close();}catch(e){} try{top.mainTab.closeTab();}catch(e){parent.close();}", true);
    //        return;
    //    }
    //}

    ///// <summary>
    ///// 终止一个任务所在的流程
    ///// </summary>
    ///// <param name="taskID"></param>
    //public string EndTask(Guid taskID)
    //{
    //    RoadFlow.Platform.WorkFlowTask wft = new RoadFlow.Platform.WorkFlowTask();
    //    RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = null;

    //    var task = wft.Get(taskID);
    //    if (task == null)
    //    {
    //        return "未找到当前任务";
    //    }
    //    var tasks = wft.GetTaskList(task.FlowID, task.GroupID);
    //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
    //    {
    //        try
    //        {
    //            foreach (var t in tasks)
    //            {
    //                if (t.Status < 2)
    //                {
    //                    t.Status = t.ID == task.ID ? 6 : 7;
    //                    t.CompletedTime1 = DateTime.Now;
    //                    wft.Update(t);
    //                }
    //            }
    //            #region 更新业务表标识字段的值为2
    //            if (wfInstalled == null)
    //            {
    //                wfInstalled = new RoadFlow.Platform.WorkFlow().GetWorkFlowRunModel(task.FlowID);
    //            }
    //            if (wfInstalled.TitleField != null && wfInstalled.TitleField.LinkID != Guid.Empty && !wfInstalled.TitleField.Table.IsNullOrEmpty()
    //                && !wfInstalled.TitleField.Field.IsNullOrEmpty() && wfInstalled.DataBases.Count() > 0)
    //            {
    //                var firstDB = wfInstalled.DataBases.First();

    //                string sql = string.Format("UPDATE {0} SET {1}='{2}' WHERE {3}", firstDB.LinkName+".dbo."+ wfInstalled.TitleField.Table, wfInstalled.TitleField.Field,
    //               "2", string.Format("{0}='{1}'", firstDB.PrimaryKey, task.InstanceID));
    //                RoadFlow.Data.Factory.Factory.GetDBHelper().Execute(sql);
    //            }
    //            #endregion
    //            scope.Complete();
    //        }
    //        catch (Exception err)
    //        {
    //            RoadFlow.Platform.Log.Add(err);
    //        }
    //    }
    //    return "1";
    //}
    #endregion

    #region "上传文件"

    //保存上传文件路径
    public static string savepath = @"UploadFile\Fin\travel";
    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "," + "\\" + savepath + "\\" + resultFileName + "," + sizeText;

    }

    #endregion


    protected void gv_d_hr_DataBound(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, e.GetType(), "gridcolor", "gv_d_hr_color(); RefreshRow_HR();", true);
    }
}