﻿using DevExpress.Web;
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

public partial class Forms_PgiOp_GYLX : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;
    public string IsRead = "N"; public string IsRead_yz = "N"; public string IsGrid_pro = "N"; public string IsGrid_yz = "N";
    public string SQ_StepID ="B29B6624-485B-40EB-9481-87375BEE0C1F";

    string FlowID = "A";
    string StepID = "A";
    string state = "";
    string m_sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["lv"] = "";
        
        if (ViewState["pgi_no_i"] == null) { ViewState["pgi_no_i"] = ""; }
        if (ViewState["gzzx_i"] == null) { ViewState["gzzx_i"] = ""; }

        if (ViewState["pgi_no_i_yz"] == null) { ViewState["pgi_no_i_yz"] = ""; }
        if (ViewState["gzzx_i_yz"] == null) { ViewState["gzzx_i_yz"] = ""; }

        if (ViewState["IsBg_i"] == null) { ViewState["IsBg_i"] = ""; }
        if (ViewState["IsBg_i_yz"] == null) { ViewState["IsBg_i_yz"] = ""; }

        //string FlowID = "A";
        //string StepID = "A";
        //string state = "";

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

        if (Request.QueryString["state"] != null)
        {
            state = Request.QueryString["state"];
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

        //加载表头控件
        List<TableRow> ls = ShowControl("PGI_GYLX_Main_Form", "HEAD", 4, "", "", "lineread", "linewrite");//Pgi.Auto.Control.form-control input-s-sm
        for (int i = 0; i < ls.Count; i++)
        {
            this.tblCPXX.Rows.Add(ls[i]);
        }


        if (!IsPostBack)
        {
            //获取每步骤栏位状态设定值，方便前端控制其可编辑性

            //if (Request.QueryString["flowid"] != null)
            //{
            //    FlowID = Request.QueryString["flowid"];
            //}

            //if (Request.QueryString["stepid"] != null)
            //{
            //    StepID = Request.QueryString["stepid"];
            //}

            DataTable ldt_detail = null;
            string lssql = @"select a.id, GYGSNo, typeno, pgi_no, pgi_no_t, op, op_desc, op_remark, gzzx, gzzx_desc, IsBg, JgNum, JgSec, WaitSec, ZjSecc, JtNum, TjOpSec, JSec, JHour
                                , col1, col2, EquipmentRate, col3, col4, col5, col6, col7, weights, acupoints, capacity, UpdateById, UpdateByName, UpdateDate, domain, ver, pn                                
                                ,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid 
                            from [dbo].[PGI_GYLX_Dtl_Form] a";

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    txt_CreateById.Value = LogUserModel.UserId;
                    txt_CreateByName.Value = LogUserModel.UserName;
                    txt_CreateByDept.Value = LogUserModel.DepartName;
                    txt_CreateDate.Value = System.DateTime.Now.ToString();
                }

                //修改申请
                if (Request.QueryString["formno"] != null && state == "edit")
                {
                    //----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了

                    string re_sql = @"select top 1 a.InstanceID,c.createbyid,c.createbyname 
                                    from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0,1))  a
                                        inner join PGI_GYLX_Dtl_Form b on a.InstanceID=b.GYGSNo
                                        inner join PGI_GYLX_Main_Form c on a.InstanceID=c.formno
                                     where b.pgi_no='" + Request.QueryString["pgi_no"] + "'";
                    //if (m_sid != "") { re_sql += " and InstanceID<>'" + m_sid + "'"; }
                    DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

                    if (re_dt.Rows.Count > 0)
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "该项目正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString() + ",申请人:" + re_dt.Rows[0]["createbyid"].ToString() + "-" + re_dt.Rows[0]["createbyname"].ToString() + ")!");
                    }
                    else
                    {
                        string sql_head = @"select a.id, a.FormNo, a.projectno, a.pn, a.pn_desc, a.domain, a.typeno, a.state
                                            , a.CreateById, a.CreateByName, a.CreateByDept, a.CreateDate 
                                            ,c.product_user,c.zl_user,c.yz_user
                                    from PGI_GYLX_Main a 
                                        left join form3_Sale_Product_MainTable c on a.projectno=c.pgino 
                                    where formno='" + Request.QueryString["formno"] + "'";
                        DataTable ldt = DbHelperSQL.Query(sql_head).Tables[0];

                        //该条件仅作为测试使用
                        if (txt_CreateByDept.Value == "IT部")
                        {
                            string test_product_user = txt_CreateById.Value + "-" + txt_CreateByName.Value;
                            ldt.Rows[0]["product_user"] = test_product_user;
                            ldt.Rows[0]["yz_user"] = test_product_user;
                        }
                        //end

                        SetControlValue("PGI_GYLX_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                        txt_domain.Text = ldt.Rows[0]["domain"].ToString(); txt_pn.Text = ldt.Rows[0]["pn"].ToString();


                        ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text = "";

                        lssql = @"select null id, GYGSNo, typeno, pgi_no, pgi_no_t, op, op_desc, op_remark, gzzx, gzzx_desc, IsBg, JgNum, JgSec, WaitSec, ZjSecc, JtNum, TjOpSec, JSec, JHour
                                , col1, col2, EquipmentRate, col3, col4, col5, col6, col7, weights, acupoints, capacity, UpdateById, UpdateByName, UpdateDate, domain, nchar(ascii(isnull(ver,'A'))+1) ver, pn
                                ,ROW_NUMBER() OVER(ORDER BY UpdateDate) numid
                           from PGI_GYLX_Dtl a 
                           where GYGSNo='" + Request.QueryString["formno"] + "' and pgi_no='" + Request.QueryString["pgi_no"] + "'  order by a.typeno, pgi_no, pgi_no_t,op";
                    }
                    
                }
                else//新增申请
                {
                    lssql += " where 1=0";
                }

            }
            else
            {
                DataTable ldt = DbHelperSQL.Query("select * from PGI_GYLX_Main_Form where formno='" + this.m_sid + "'").Tables[0];
                //表头基本信息
                txt_CreateById.Value = ldt.Rows[0]["CreateById"].ToString();
                txt_CreateByName.Value = ldt.Rows[0]["CreateByName"].ToString();
                txt_CreateByDept.Value = ldt.Rows[0]["CreateByDept"].ToString();
                txt_CreateDate.Value = ldt.Rows[0]["CreateDate"].ToString();
                SetControlValue("PGI_GYLX_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                txt_domain.Text = ldt.Rows[0]["domain"].ToString(); txt_pn.Text = ldt.Rows[0]["pn"].ToString();
                modifyremark.Text = ldt.Rows[0]["modifyremark"].ToString();

                lssql += " where GYGSNo='" + this.m_sid + "'  order by a.typeno,op";
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];

            string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;
            if (lstypeno == "机加")
            {
                IsGrid_pro = "Y";
                this.gv_d.DataSource = ldt_detail;
                this.gv_d.DataBind();
            }
            if (lstypeno == "压铸")
            {
                IsGrid_yz = "Y";
                this.gv_d_yz.DataSource = ldt_detail;
                this.gv_d_yz.DataBind();
            }

            setGridIsRead(ldt_detail);

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


        if ((StepID.ToUpper() != SQ_StepID && StepID != "A") || state == "edit")
        {
            ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
            ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
            ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

            ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;
        }

        if (StepID.ToUpper() != SQ_StepID && StepID != "A")
        {
            modifyremark.ReadOnly = true;
        }

        /*JgNum_ValueChanged(sender, e);*/

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    public void setGridIsRead(DataTable ldt_detail)
    {
        string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;

        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请人");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];


        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {

            if (state == "edit" || ldt_detail.Rows[i]["ver"].ToString() != "A")
            {
                ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
                ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
                ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

                ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

                if (lstypeno == "机加")
                {
                    ViewState["pgi_no_i"] = "Y";
                    ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
                    ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;
                }
                if (lstypeno == "压铸")
                {
                    ViewState["pgi_no_i_yz"] = "Y";
                    ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
                    ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;
                }

                if (state != "edit" && ldt_detail.Rows[i]["ver"].ToString() != "A")
                {
                    if (ldt_flow_pro.Rows.Count == 0)
                    {
                        this.btnflowSend.Text = "批准";
                    }
                    if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
                    {
                        if (lstypeno == "机加")
                        {
                            setread(i);
                        }
                        if (lstypeno == "压铸")
                        {
                            setread_yz(i);
                        }
                    }
                }
            }            
            else
            {
                if (ldt_flow_pro.Rows.Count == 0)
                {
                    this.btnflowSend.Text = "批准";
                }
                if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
                {
                    if (lstypeno == "机加")
                    {
                        setread(i);
                    }
                    if (lstypeno == "压铸")
                    {
                        setread_yz(i);
                    }
                }
            }
        }
    }


    public void setpgino_read(DataTable dt)
    {
        if (ViewState["pgi_no_i"].ToString() == "Y")
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;
            }
        }

        if (ViewState["pgi_no_i_yz"].ToString() == "Y")
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;
            }
        }
    }

    #region 机加

    public void setread(int i)
    {
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

        modifyremark.ReadOnly = true; 

        ViewState["pgi_no_i"] = "Y"; ViewState["gzzx_i"] = "Y"; ViewState["IsBg_i"] = "Y"; 

        btndel.Visible = false;

        if (i == 0)
        {
            gv_d.Columns[gv_d.VisibleColumns.Count - 1].Visible = false;
            gv_d.Columns[0].Visible = false;
        }

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no_t"], "pgi_no_t")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["pgi_no_t"], "pgi_no_t")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op"], "op")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op"], "op")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_desc"], "op_desc")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_desc"], "op_desc")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_remark"], "op_remark")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["op_remark"], "op_remark")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgNum"], "JgNum")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgNum"], "JgNum")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgSec"], "JgSec")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JgSec"], "JgSec")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["WaitSec"], "WaitSec")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["WaitSec"], "WaitSec")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ZjSecc"], "ZjSecc")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ZjSecc"], "ZjSecc")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JtNum"], "JtNum")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["JtNum"], "JtNum")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col1"], "col1")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col1"], "col1")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col2"], "col2")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col2"], "col2")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["EquipmentRate"], "EquipmentRate")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["EquipmentRate"], "EquipmentRate")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col6"], "col6")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["col6"], "col6")).BorderStyle = BorderStyle.None;
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        //DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                //ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        //if (ldt_del.Rows.Count > 0)
        //{
        //    if (Session["del"] != null)
        //    {
        //        for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
        //        {
        //            ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
        //        }

        //    }
        //    Session["del"] = ldt_del;
        //}
        gv_d.DataSource = ldt;
        gv_d.DataBind();
        setpgino_read(ldt);
    }

    private void GvAddRows(int lnadd_rows, int lnindex)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);

        DataRow[] drs = ldt.Select("", "numid desc");
        DataTable dt_o = ldt.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt_o.Rows.Add(row.ItemArray);
        }

        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            for (int j = 0; j < ldt.Columns.Count; j++)
            {
                if (ldt.Columns[j].ColumnName == "typeno" || ldt.Columns[j].ColumnName == "pgi_no" || ldt.Columns[j].ColumnName == "ver"
                    || ldt.Columns[j].ColumnName == "pgi_no_t" || ldt.Columns[j].ColumnName == "domain" || ldt.Columns[j].ColumnName == "pn")
                {
                    ldr[ldt.Columns[j].ColumnName] = ldt.Rows[lnindex][ldt.Columns[j].ColumnName];
                }
                else if (ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = dt_o.Rows.Count <= 0 ? 0 : (Convert.ToInt32(dt_o.Rows[0]["numid"]) + 1);
                }
                else if (ldt.Columns[j].ColumnName.ToLower() == "isbg")
                {
                    ldr[ldt.Columns[j].ColumnName] = "Y";
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.InsertAt(ldr, lnindex + 1);

        }
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();

        setpgino_read(ldt);
    }

    protected void gv_d_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Add")
        {
            GvAddRows(1, e.VisibleIndex);
        }
    }

    protected void gv_d_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        SetGvRow();
    }

    public void SetGvRow()
    {
        //if (Request.QueryString["state"] == "edit")
        //{

        //}
        //else
        //{

        string lspgi_no = ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Text;
        string lsdomain = txt_domain.Text; //((TextBox)this.FindControl("ctl00$MainContent$domain")).Text;
        string lspn = txt_pn.Text; //((TextBox)this.FindControl("ctl00$MainContent$pn")).Text;

        string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;

        //if (lspgi_no == "" || lstypeno=="")
        //{
        //    return;
        //}

        //先查询数据库时候有数据
        string lsformno = ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text;
        string lssql = @"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYLX_Dtl_Form] a 
                        where GYGSNo='" + lsformno + "' and left(pgi_no,5)='" + lspgi_no + "'  order by a.typeno,op";
        DataTable ldt_db = DbHelperSQL.Query(lssql).Tables[0];
        if (ldt_db != null)
        {
            if (ldt_db.Rows.Count > 0)
            {
                gv_d.DataSource = ldt_db;
                gv_d.DataBind();
                return;
            }
        }

        //首次
        DataTable ldt = DbHelperSQL.Query(@"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYLX_Dtl_Form] a where 1=0").Tables[0];

        for (int i = 1; i <= 7; i++)
        {
            DataRow ldr = ldt.NewRow();
            ldr["typeno"] = "机加";
            ldr["ver"] = "A";
            ldr["op"] = "OP1" + i.ToString() + "0";
            ldr["isbg"] = "Y";
            ldr["domain"] = lsdomain; ldr["pn"] = lspn;
            ldr["numid"] = i;

            ldt.Rows.Add(ldr);
        }

        DataRow ldr_z1 = ldt.NewRow();
        ldr_z1["typeno"] = "机加";
        ldr_z1["ver"] = "A";
        ldr_z1["op"] = "OP600";
        ldr_z1["isbg"] = "Y";
        ldr_z1["domain"] = lsdomain; ldr_z1["pn"] = lspn;
        ldr_z1["numid"] = 8;
        ldt.Rows.Add(ldr_z1);

        DataRow ldr_z2 = ldt.NewRow();
        ldr_z2["typeno"] = "机加";//质量
        ldr_z2["ver"] = "A";
        ldr_z2["op"] = "OP700";
        ldr_z2["isbg"] = "Y";
        ldr_z2["domain"] = txt_domain.Text; ldr_z2["pn"] = txt_pn.Text;
        ldr_z2["numid"] = 9;
        ldt.Rows.Add(ldr_z2);

        gv_d.DataSource = ldt;
        gv_d.DataBind();
        //}

    }

    #endregion


    #region 压铸

    public void setread_yz(int i)
    {
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

        modifyremark.ReadOnly = true;

        ViewState["pgi_no_i_yz"] = "Y"; ViewState["gzzx_i_yz"] = "Y"; ViewState["IsBg_i_yz"] = "Y";

        btn_del_yz.Visible = false;

        if (i == 0)
        {
            gv_d_yz.Columns[gv_d_yz.VisibleColumns.Count - 1].Visible = false;
            gv_d_yz.Columns[0].Visible = false;
        }

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no"], "pgi_no")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no"], "pgi_no")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no_t"], "pgi_no_t")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["pgi_no_t"], "pgi_no_t")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op"], "op")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op"], "op")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op_desc"], "op_desc")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op_desc"], "op_desc")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op_remark"], "op_remark")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["op_remark"], "op_remark")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["weights"], "weights")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["weights"], "weights")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["acupoints"], "acupoints")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["acupoints"], "acupoints")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["capacity"], "capacity")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["capacity"], "capacity")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JgNum"], "JgNum")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JgNum"], "JgNum")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JgSec"], "JgSec")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JgSec"], "JgSec")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["WaitSec"], "WaitSec")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["WaitSec"], "WaitSec")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["ZjSecc"], "ZjSecc")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["ZjSecc"], "ZjSecc")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JtNum"], "JtNum")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["JtNum"], "JtNum")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col1"], "col1")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col1"], "col1")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col2"], "col2")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col2"], "col2")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["EquipmentRate"], "EquipmentRate")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["EquipmentRate"], "EquipmentRate")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col6"], "col6")).ReadOnly = true;
        ((ASPxTextBox)this.gv_d_yz.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d_yz.Columns["col6"], "col6")).BorderStyle = BorderStyle.None;
    }

    protected void btn_del_yz_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_yz);
        //DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                //ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        //if (ldt_del.Rows.Count > 0)
        //{
        //    if (Session["del"] != null)
        //    {
        //        for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
        //        {
        //            ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
        //        }

        //    }
        //    Session["del"] = ldt_del;
        //}
        gv_d_yz.DataSource = ldt;
        gv_d_yz.DataBind();

        setpgino_read(ldt);
    }

    private void GvAddRows_yz(int lnadd_rows, int lnindex)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_yz);

        DataRow[] drs = ldt.Select("", "numid desc");
        DataTable dt_o = ldt.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt_o.Rows.Add(row.ItemArray);
        }

        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            for (int j = 0; j < ldt.Columns.Count; j++)
            {
                if (ldt.Columns[j].ColumnName == "typeno" || ldt.Columns[j].ColumnName == "pgi_no" || ldt.Columns[j].ColumnName == "ver"
                    || ldt.Columns[j].ColumnName == "pgi_no_t" || ldt.Columns[j].ColumnName == "domain" || ldt.Columns[j].ColumnName == "pn")
                {
                    ldr[ldt.Columns[j].ColumnName] = ldt.Rows[lnindex][ldt.Columns[j].ColumnName];
                }
                else if (ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = dt_o.Rows.Count <= 0 ? 0 : (Convert.ToInt32(dt_o.Rows[0]["numid"]) + 1);
                }
                else if (ldt.Columns[j].ColumnName.ToLower() == "isbg")
                {
                    ldr[ldt.Columns[j].ColumnName] = "Y";
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.InsertAt(ldr, lnindex + 1);

        }
        ldt.AcceptChanges();
        this.gv_d_yz.DataSource = ldt;
        this.gv_d_yz.DataBind();


        setpgino_read(ldt);

    }

    protected void gv_d_yz_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Add")
        {
            GvAddRows_yz(1, e.VisibleIndex);
        }
    }

    protected void gv_d_yz_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        SetGvRow_yz();
    }

    public void SetGvRow_yz()
    {
        //if (Request.QueryString["state"] == "edit")
        //{

        //}
        //else
        //{

        string lspgi_no = ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Text;
        string lsdomain = txt_domain.Text; //((TextBox)this.FindControl("ctl00$MainContent$domain")).Text;
        string lspn = txt_pn.Text; //((TextBox)this.FindControl("ctl00$MainContent$pn")).Text;

        string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;

        //if (lspgi_no == "" || lstypeno=="")
        //{
        //    return;
        //}

        //先查询数据库时候有数据
        string lsformno = ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text;
        string lssql = @"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYLX_Dtl_Form] a 
                        where GYGSNo='" + lsformno + "' and left(pgi_no,5)='" + lspgi_no + "'  order by a.typeno,op";
        DataTable ldt_db = DbHelperSQL.Query(lssql).Tables[0];
        if (ldt_db != null)
        {
            if (ldt_db.Rows.Count > 0)
            {
                gv_d.DataSource = ldt_db;
                gv_d.DataBind();
                return;
            }
        }

        //首次
        DataTable ldt = DbHelperSQL.Query(@"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYLX_Dtl_Form] a where 1=0").Tables[0];
        for (int i = 1; i <= 5; i++)
        {
            DataRow ldr = ldt.NewRow();
            ldr["typeno"] = "压铸";
            ldr["ver"] = "A";
            ldr["op"] = "OP" + i.ToString() + "0";
            if (i == 1 || i == 2)
            {
                ldr["isbg"] = "N";
            }
            else
            {
                ldr["isbg"] = "Y";
            }            
            ldr["domain"] = lsdomain; ldr["pn"] = lspn;
            ldr["numid"] = i;

            ldt.Rows.Add(ldr);
        }

        gv_d_yz.DataSource = ldt;
        gv_d_yz.DataBind();

    }

    #endregion

    //[WebMethod]
    //public static string CheckData(string typeno, string product_user, string yz_user)
    //{
    //    //------------------------------------------------------------------------------验证工程师对应主管是否为空
    //    string manager_flag = "";

    //    string appuserid = "";
    //    if (typeno == "机加")
    //    {
    //        appuserid = product_user.Length >= 5 ? product_user.Substring(0, 5) : product_user;
    //    }
    //    if (typeno == "压铸")
    //    {
    //        appuserid = yz_user.Length >= 5 ? yz_user.Substring(0, 5) : yz_user;
    //    }

    //    DataTable dt_manager = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select Manager_workcode from HR_EMP_MES where workcode='" + appuserid + "')").Tables[0];
    //    if (dt_manager == null)
    //    {
    //        manager_flag = typeno + "工程师工号(" + appuserid + ")的主管不存在，不能提交!";
    //    }
    //    if (dt_manager.Rows.Count <= 0)
    //    {
    //        manager_flag = typeno + "工程师工号(" + appuserid + ")的主管不存在，不能提交!";
    //    }

    //    string result = "[{\"manager_flag\":\"" + manager_flag + "\"}]";
    //    return result;

    //}


    private bool SaveData()
    {
        bool flag = true;// 保存数据是否成功标识

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = GetControlValue("PGI_GYLX_Main_Form", "HEAD", this, "ctl00$MainContent${0}");


        /*
        //数据库字段设置不能为空，需要验证，利用ToolTip 设置的，
        //此处head部分，已经使用前端脚本处理，只是不是根据数据库动态判断的,效能会好。。
        for (int i = 0; i < ls.Count; i++)
        {
            Pgi.Auto.Common com = new Pgi.Auto.Common();
            com = ls[i];
            if (ls[i].Code == "")
            {
                Pgi.Auto.Public.MsgBox(this, "alert", ls[i].Value + " 不能为空!");
                return false;
            }

        }*/


        //------------------------------------------------------------------------------工程师对应主管

        string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;
        
        string appuserid = "";
        if (lstypeno == "机加")
        {
            string product_user = ((TextBox)this.FindControl("ctl00$MainContent$product_user")).Text.Trim();
            appuserid = product_user.Length >= 5 ? product_user.Substring(0, 5) : product_user;
        }
        if (lstypeno == "压铸")
        {
            string yz_user = ((TextBox)this.FindControl("ctl00$MainContent$yz_user")).Text.Trim();
            appuserid = yz_user.Length >= 5 ? yz_user.Substring(0, 5) : yz_user;
        }

        DataTable dt_manager = DbHelperSQL.Query(@"select a.id from RoadFlowWebForm.dbo.Users a where account=(select Manager_workcode from HR_EMP_MES where workcode='" + appuserid + "')").Tables[0];
        if (dt_manager == null)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "工程师工号(" + appuserid + ")的主管不存在，不能提交!");
            return false;
        }
        if (dt_manager.Rows.Count <= 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "工程师工号(" + appuserid + ")的主管不存在，不能提交!");
            return false;
        }

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "R" + System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
            this.m_sid = Pgi.Auto.Public.GetNo("R", lsid, 0, 4);

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "formno")
                {
                    ls[i].Value = this.m_sid;
                    ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text = this.m_sid;
                    break;
                }

            }
            //新增时增加创建人ID
            Pgi.Auto.Common lccreate_byid = new Pgi.Auto.Common();
            lccreate_byid.Code = "CreateById";
            lccreate_byid.Key = "";
            lccreate_byid.Value = txt_CreateById.Value;
            ls.Add(lccreate_byid);

            Pgi.Auto.Common lccreate_byname = new Pgi.Auto.Common();
            lccreate_byname.Code = "CreateByName";
            lccreate_byname.Key = "";
            lccreate_byname.Value = txt_CreateByName.Value;
            ls.Add(lccreate_byname);

            Pgi.Auto.Common lccreate_bydept = new Pgi.Auto.Common();
            lccreate_bydept.Code = "CreateByDept";
            lccreate_bydept.Key = "";
            lccreate_bydept.Value = txt_CreateByDept.Value;
            ls.Add(lccreate_bydept);

            Pgi.Auto.Common lccreate_date = new Pgi.Auto.Common();
            lccreate_date.Code = "CreateDate";
            lccreate_date.Key = "";
            lccreate_date.Value = txt_CreateDate.Value;
            ls.Add(lccreate_date);

        }
       
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code.ToLower() == "typeno")
            {
                ls[i].Value = lstypeno;
                break;
            }
        }
       

        Pgi.Auto.Common lcmanager_id = new Pgi.Auto.Common();
        lcmanager_id.Code = "manager_id";
        lcmanager_id.Key = "";
        lcmanager_id.Value = "u_" + dt_manager.Rows[0][0].ToString();
        ls.Add(lcmanager_id);

        Pgi.Auto.Common lcModifyRemark = new Pgi.Auto.Common();
        lcModifyRemark.Code = "ModifyRemark";
        lcModifyRemark.Key = "";
        lcModifyRemark.Value = modifyremark.Text.Trim();//modifyremark.Value.Trim();
        ls.Add(lcModifyRemark);

        //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
        DataTable ldt = new DataTable(); 

        if (lstypeno == "机加")
        {
            ldt = Pgi.Auto.Control.AgvToDt(this.gv_d); 
        }
        if (lstypeno == "压铸")
        {
            ldt = Pgi.Auto.Control.AgvToDt(this.gv_d_yz); 
        }

        //主表相关字段赋值到明细表
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            for (int j = 0; j < ls.Count; j++)
            {
                if (ls[j].Code.ToLower() == "formno")
                {
                    ldt.Rows[i]["GYGSNo"] = ls[j].Value;
                    break;
                }
            }
        }


        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_GYLX_Main_Form"));


        if (ldt.Rows.Count > 0)
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
                ls_del.Sql = "delete from PGI_GYLX_Dtl_Form where GYGSNo='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_GYLX_Dtl_Form where GYGSNo='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_GYLX_Dtl_Form", "id", "Column1,numid,flag");
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

            var titletype = ldt.Rows[0]["ver"].ToString() == "A" ? "工艺路线申请" : "工艺路线修改";
            string title = titletype + "--" + this.m_sid;
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
        //保存数据
        bool flag = SaveData();
        //保存当前流程
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //Pgi.Auto.Public.MsgBox(Page, "alert", "流程开发中。。");
        //保存数据
        bool flag = SaveData();
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

    #region 在页面中显示要显示的字段
    /// <summary>
    /// 在页面中显示要显示的字段
    /// </summary>
    /// <param name="lsform_type">要显示字段大类</param>
    /// <param name="lsform_div">要显示字段小类</param>
    /// <param name="lncolumn">设置每行显示列的数量</param>
    /// <param name="lsrow_style">设置行样式</param>
    /// <param name="lscolumn_style">设置列样式</param>
    /// <param name="lscontrol_style">设置显示控件样式（默认统一）</param>
    /// <param name="ldt_head">赋值数据源（可选参数，默认抓取Datatable中第一行）</param>
    /// <returns></returns>
    public static List<TableRow> ShowControl(string lsform_type, string lsform_div, int lncolumn, string lsrow_style, string lscolumn_style, string lscontrol_style, string lscontrol_style2, DataTable ldt_head = null)
    {
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where (control_id<>'' or control_id is null) " + lswhere + " order by control_order",
            new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

        List<TableRow> ls = new List<TableRow>();
        int ln = 1;
        TableRow lrow = null;


        int k = lncolumn;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (k == lncolumn)//(i % lncolumn) == 0
            {
                lrow = new TableRow();
                if (lsrow_style != "")
                {
                    lrow.CssClass = lsrow_style;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {

                ls.Add(lrow); k = lncolumn;
                lrow = new TableRow();
                //行样式
                if (lsrow_style != "")
                {
                    lrow.CssClass = lsrow_style;
                }
            }

            //列样式
            TableCell lcellHead = new TableCell();
            if (lscolumn_style != "")
            {
                lcellHead.CssClass = lscolumn_style;
            }
            TableCell lcellContent = new TableCell();
            if (lscolumn_style != "")
            {
                lcellContent.CssClass = lscolumn_style;
            }
            Label lbl = new Label();
            lbl.ID = "lbl_" + lsform_type + "_" + lsform_div + "_" + ln.ToString();
            lbl.Text = ldt.Rows[i]["control_dest"].ToString();

            //-----------------------------------控件判断开始----------------------------------------
            //文本控件
            if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
            {
                #region "TextBox"
                TextBox ltxt = new TextBox();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                {
                    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                }
                else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    //设置只读 刷新数据会丢掉数据
                    ltxt.ReadOnly = true;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                if (lscontrol_style != "")
                {
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    else
                    {

                        ltxt.CssClass = lscontrol_style2;
                    }

                }
                if (ldt_head != null)
                {
                    ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                }



                lcellContent.Controls.Add(ltxt);
                #endregion
            }
            //下拉控件
            else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
            {
                #region "DropDownList"
                DropDownList ltxt = new DropDownList();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;
                    //ltxt.CssClass = "form-control input-s-sm";
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    else
                    {

                        ltxt.CssClass = lscontrol_style2;
                    }
                }
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        if (ls1.Length == ls2.Length)
                        {
                            for (int j = 0; j < ls1.Length; j++)
                            {
                                ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                            }
                        }

                    }
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    for (int j = 0; j < ldt_source.Rows.Count; j++)
                    {
                        ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    }
                }

                if (ldt_head != null)
                {
                    if (ltxt.Items.Count > 0)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }

                }

                lcellContent.Controls.Add(ltxt);
                #endregion

            }

            //CheckBoxList控件
            else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOXLIST")
            {
                #region "CheckBoxList"
                CheckBoxList ltxt = new CheckBoxList();
                HiddenField htxt = new HiddenField();
                htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower(); ;// "chk" + ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //ltxt.RepeatLayout = RepeatLayout.Flow;
                ltxt.RepeatColumns = 3;
                //事件
                var script = "var val='';$(\"input[id*='" + ldt.Rows[i]["control_id"].ToString().ToLower() + "']\").each(function(){   val+=$(this).val();   }); ";
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onclick", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }
                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;
                }
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        if (ls1.Length == ls2.Length)
                        {
                            for (int j = 0; j < ls1.Length; j++)
                            {
                                ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                            }
                        }

                    }
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    for (int j = 0; j < ldt_source.Rows.Count; j++)
                    {
                        ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    }
                }

                if (ldt_head != null)
                {
                    if (ltxt.Items.Count > 0)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }

                }

                lcellContent.Controls.Add(ltxt);
                #endregion

            }

            //CheckBox控件
            else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOX")
            {
                #region CHECKBOX
                CheckBox ltxt = new CheckBox();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                {
                    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                }
                else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    //设置只读 刷新数据会丢掉数据
                    ltxt.Enabled = false;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                //    {
                //        ltxt.CssClass = lscontrol_style;
                //    }
                //    else
                //    {

                //        ltxt.CssClass = lscontrol_style2;
                //    }

                //}
                if (ldt_head != null)
                {
                    ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                }



                lcellContent.Controls.Add(ltxt);
                #endregion
            }

            //RadioButtonList控件
            else if (ldt.Rows[i]["control_type"].ToString() == "RadioButtonList")
            {
                #region "RadioButtonList"
                RadioButtonList ltxt = new RadioButtonList();
                HiddenField htxt = new HiddenField();
                htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //ltxt.RepeatLayout = RepeatLayout.Flow;
                ltxt.RepeatDirection = RepeatDirection.Horizontal;
                ltxt.RepeatColumns = 5;
                //事件
                var script = "var val='';$(\"input[id*='" + ldt.Rows[i]["control_id"].ToString().ToLower() + "']\").each(function(){   val+=$(this).val();   }); ";
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onclick", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }
                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;

                }
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        if (ls1.Length == ls2.Length)
                        {
                            for (int j = 0; j < ls1.Length; j++)
                            {
                                ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                            }
                        }

                    }
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    for (int j = 0; j < ldt_source.Rows.Count; j++)
                    {
                        ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    }
                }

                if (ldt_head != null)
                {
                    if (ltxt.Items.Count > 0)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }

                }

                lcellContent.Controls.Add(ltxt);
                #endregion

            }
            //-----------------------------------控件判断结束----------------------------------------
            //判断下个字段是否独立
            if (i + 1 < ldt.Rows.Count)
            {
                if (ldt.Rows[i + 1]["control_onlyrow"].ToString() == "1")
                {
                    int lnspan = i % lncolumn + 1;
                    lcellContent.ColumnSpan = lnspan * 2;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {
                lcellContent.ColumnSpan = lncolumn * 2 - 1;
            }
            lcellHead.Controls.Add(lbl);
            lrow.Cells.Add(lcellHead);
            lrow.Cells.Add(lcellContent);

            k--;

            if (k == 0 || i == ldt.Rows.Count - 1 || ldt.Rows[i]["control_onlyrow"].ToString() == "1")//(i % lncolumn) == 0
            {
                ls.Add(lrow); k = lncolumn;
            }
            ln += 1;
        }


        return ls;
    }
    #endregion

    #region 将界面中控件值统计到List中

    /// <summary>
    /// 将界面中控件值统计到List中
    /// </summary>
    /// <param name="lsform_type">要显示字段大类</param>
    /// <param name="lsform_div">要显示字段小类</param>
    /// <param name="p">要统计的界面Page</param>
    /// <param name="lscontrol_format">界面控件中要套用的控件ID格式</param>
    /// <returns></returns>
    public static List<Pgi.Auto.Common> GetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, string lscontrol_format = "")
    {
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 " + lswhere + "",
            new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

        List<Pgi.Auto.Common> ls = new List<Pgi.Auto.Common>();
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            string lscontrol_id = ldt.Rows[i]["control_id"].ToString().ToLower();
            if (lscontrol_format != "")
            {
                lscontrol_id = lscontrol_format.Replace("{0}", lscontrol_id);
            }
            if (p.FindControl(lscontrol_id) != null)
            {



                Pgi.Auto.Common com = new Pgi.Auto.Common();
                com.Code = ldt.Rows[i]["control_id"].ToString().ToLower();
                string lstr = "0|0";
                string initlstr = "0|0";//初始lstr字符，以便忘记配置值而报错
                                        //-----------------------------------控件判断开始----------------------------------
                if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
                {
                    // ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Enabled = true;
                    // com.Value = ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Text;
                    com.Value = p.Request.Form[lscontrol_id].ToString();
                    ((TextBox)p.FindControl(lscontrol_id)).Text = p.Request.Form[lscontrol_id].ToString();
                    lstr = ((TextBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
                {
                    com.Value = ((DropDownList)p.FindControl(lscontrol_id)).SelectedValue;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DropDownList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXCOMBOBOX")
                {
                    //com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                    com.Value = "";
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "RadioButtonList")
                {
                    //com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                    com.Value = "";
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((RadioButtonList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOXLIST")
                {
                    com.Value = "";
                    if (com.Value == "")
                    {
                        for (int chk = 0; chk < 2; chk++)
                        {
                            if (p.Request.Form[lscontrol_id + "$" + chk.ToString()] != null)
                            {
                                com.Value += p.Request.Form[lscontrol_id + "$" + chk.ToString()].ToString(); //无法获取
                            }

                            com.Value += ";";

                        }

                        if (com.Value != "") { com.Value = com.Value.Left(com.Value.Length - 1); }

                    }
                    lstr = ((CheckBoxList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOX")
                {
                    com.Value = "";
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString() == "on" ? "Y" : "N"; //无法获取
                        }
                    }
                    lstr = ((CheckBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDATEEDIT")
                {
                    com.Value = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).Text;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDROPDOWNEDIT")
                {
                    com.Value = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).Text;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                //-----------------------------------控件判断结束----------------------------------

                string[] ls1 = lstr.Split('|');
                com.Key = ls1[0];
                if (ls1[1] == "1" && com.Value == "")
                {
                    com.Value = ldt.Rows[i]["control_dest"].ToString();
                    com.Code = "";
                }
                ls.Add(com);
            }
        }
        return ls;
    }
    #endregion


    #region 将TableRow栏位值赋值给页面中的控件

    //把表中值初始化给控件 added by fish:
    /// <summary>
    /// 将TableRow栏位值赋值给页面中的控件
    /// </summary>
    /// <param name="lsform_type">页面识别参数</param>
    /// <param name="lsform_div">div显示范围识别参数</param>
    /// <param name="p">page</param>
    /// <param name="dr">a DataRow</param>
    public static void SetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, DataRow dr, string lscontrolformat = "")
    {
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 and isnull(control_id,'')<>''" + lswhere + "",
            new SqlParameter[]{
                                    new SqlParameter("@form_type",lsform_type)
                                    ,new SqlParameter("@form_div",lsform_div)}).Tables[0];


        foreach (DataRow row in ldt.Rows)
        {
            if (p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower()) != null)
            {
                var columnValue = dr[row["control_id"].ToString().ToLower()].ToString();
                if (row["control_type"].ToString() == "TEXTBOX")
                {
                    ((TextBox)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower())).Text = columnValue;
                }
                else if (row["control_type"].ToString() == "DROPDOWNLIST")
                {
                    var drop = (DropDownList)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {
                        ListItem item = drop.Items.FindByValue(columnValue);
                        if (item != null)
                        {
                            // drop.ClearSelection();
                            //  item.Selected = true;
                            drop.SelectedValue = columnValue;
                        }
                    }
                }
                else if (row["control_type"].ToString() == "RadioButtonList")
                {
                    var drop = (RadioButtonList)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {
                        ListItem item = drop.Items.FindByValue(columnValue);
                        if (item != null)
                        {
                            // drop.ClearSelection();
                            //  item.Selected = true;
                            drop.SelectedValue = columnValue;
                        }
                    }
                }
                else if (row["control_type"].ToString() == "CHECKBOXLIST")
                {
                    var chk = (CheckBoxList)p.FindControl(lscontrolformat + row["control_id"].ToString());

                    string[] columnValue_arr = columnValue.Split(';');
                    for (int k = 0; k < columnValue_arr.Length; k++)
                    {
                        chk.Items[k].Selected = columnValue_arr[k] == "" ? false : true;
                    }
                }
                else if (row["control_type"].ToString() == "CHECKBOX")
                {
                    var chk = (CheckBox)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (chk != null)
                    {
                        chk.Checked = columnValue == "Y" ? true : false;
                    }
                }
                else if (row["control_type"].ToString() == "ASPXCOMBOBOX")
                {
                    var drop = (DevExpress.Web.ASPxComboBox)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {

                        drop.Value = columnValue;

                    }
                }
                else if (row["control_type"].ToString() == "FILEUPLOAD")
                {
                    var upload = (FileUpload)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower());
                    var link = (HyperLink)p.FindControl(lscontrolformat + "link_" + row["control_id"].ToString().ToLower());
                    if (link != null && columnValue != "")
                    {
                        link.NavigateUrl = columnValue;
                        var name = columnValue.Substring(columnValue.LastIndexOf(@"\") + 1);
                        link.Text = name;
                        link.Target = "_blank";
                    }
                }

            }
        }
        // return ls;
    }

    #endregion

}