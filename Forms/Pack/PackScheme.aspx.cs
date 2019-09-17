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

public partial class Forms_Pack_PackScheme : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    public string SQ_StepID = "F882B5B3-78BE-4804-BB42-72C0D6B680AB";
    public string is_hr_zy = "";

    string FlowID = "A";
    string StepID = "A";
    string state = "";
    string m_sid = "";

    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["ApplyId_i"] == null) { ViewState["ApplyId_i"] = ""; }

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }

        //获取每步骤栏位状态设定值，方便前端控制其可编辑性

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
            string lssql = @"select a.*,ROW_NUMBER() OVER (ORDER BY a.id) numid 
                            from [dbo].[PGI_PackScheme_Dtl_Form] a";

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    ApplyDate.Text = System.DateTime.Now.ToString();
                    CreateId.Text = LogUserModel.UserId;
                    CreateName.Text = LogUserModel.UserName;
                    ApplyId.Text = LogUserModel.UserId;
                    ApplyName.Text = LogUserModel.UserName;
                    ApplyDeptName.Text = LogUserModel.DepartName;
                    ApplyTelephone.Text = LogUserModel.Telephone;
                }

                //修改申请
                if (Request.QueryString["formno"] != null && state == "edit")
                {
                    ////----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了

                    //string re_sql = @"select top 1 a.InstanceID,b.createbyid,b.createbyname 
                    //                from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='ee59e0b3-d6a1-4a30-a3b4-65d188323134' and status in(0,1))  a
                    //                    inner join PGI_GYLX_Main_Form b on a.InstanceID=b.formno 
                    //                 where b.projectno='" + Request.QueryString["pgi_no"] + "' and b.pgi_no_t='" + Request.QueryString["pgi_no_t"] + "'";
                    //DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

                    //if (re_dt.Rows.Count > 0)
                    //{
                    //    Pgi.Auto.Public.MsgBox(this, "alert", Request.QueryString["pgi_no"] + "(" + Request.QueryString["pgi_no_t"]
                    //        + ")项目正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString() + ",申请人:"
                    //        + re_dt.Rows[0]["createbyid"].ToString() + "-" + re_dt.Rows[0]["createbyname"].ToString() + ")!");
                    //}
                    //else
                    //{
                    //    string sql_head = @"select a.id, a.FormNo, a.projectno,d.pt_desc1 pn,d.pt_desc2 pn_desc, a.domain, a.typeno, a.state
                    //                        , a.containgp, nchar(ascii(isnull(ver,'A'))+1) ver, a.pgi_no_t
                    //                        , a.CreateById, a.CreateByName, a.CreateByDept, a.CreateDate, a.ModifyGP 
                    //                        ,c.product_user,c.zl_user,c.yz_user,c.bz_user 
                    //                from PGI_GYLX_Main a 
                    //                    left join form3_Sale_Product_MainTable c on left(a.projectno,5)=c.pgino 
                    //                    left join qad_pt_mstr d on a.projectno=d.pt_part and a.domain=d.pt_domain
                    //                where formno='" + Request.QueryString["formno"] + "'";
                    //    DataTable ldt = DbHelperSQL.Query(sql_head).Tables[0];

                    //    SetControlValue("PGI_GYLX_Main_Form", "HEAD_NEW_3", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                    //    txt_domain.Text = ldt.Rows[0]["domain"].ToString(); txt_pn.Text = ldt.Rows[0]["pn"].ToString();


                    //    ((TextBox)this.FindControl("ctl00$MainContent$formno")).Text = "";

                    //    lssql = @"select null id, '' GYGSNo, typeno, pgi_no, pgi_no_t, op, op_desc, op_remark, gzzx, gzzx_desc, IsBg, JgNum, JgSec, WaitSec, ZjSecc, JtNum, TjOpSec, JSec, JHour
                    //            , col1, col2, EquipmentRate, col3, col4, col5, col6, col7, weights, acupoints, capacity, UpdateById, UpdateByName, UpdateDate, domain, nchar(ascii(isnull(ver,'A'))+1) ver
                    //            , '" + ldt.Rows[0]["pn"].ToString() + @"' pn, isnull(IsXh_op,'') IsXh_op,ROW_NUMBER() OVER(ORDER BY UpdateDate) numid
                    //       from PGI_GYLX_Dtl a 
                    //       where GYGSNo='" + Request.QueryString["formno"] + "' and pgi_no='" + Request.QueryString["pgi_no"] + "'  order by a.typeno, pgi_no, pgi_no_t,cast(right(op,len(op)-2) as int)";
                    //}

                }
                else//新增申请
                {
                    lssql += " where 1=0";
                }
            }
            else
            {
                //DataTable ldt = DbHelperSQL.Query(@"select a.*,d.pt_desc1,d.pt_desc2 
                //                                    from PGI_GYLX_Main_Form a 
                //                                        left join qad_pt_mstr d on a.projectno=d.pt_part and a.domain=d.pt_domain 
                //                                    where formno='" + this.m_sid + "'").Tables[0];
                //if (ldt.Rows.Count > 0)
                //{
                //    ldt.Rows[0]["pn"] = ldt.Rows[0]["pt_desc1"]; ldt.Rows[0]["pn_desc"] = ldt.Rows[0]["pt_desc2"];
                //    //表头基本信息
                //    txt_CreateById.Value = ldt.Rows[0]["CreateById"].ToString();
                //    txt_CreateByName.Value = ldt.Rows[0]["CreateByName"].ToString();
                //    txt_CreateByDept.Value = ldt.Rows[0]["CreateByDept"].ToString();
                //    txt_CreateDate.Value = ldt.Rows[0]["CreateDate"].ToString();
                //    SetControlValue("PGI_GYLX_Main_Form", "HEAD_NEW_3", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                //    txt_domain.Text = ldt.Rows[0]["domain"].ToString(); txt_pn.Text = ldt.Rows[0]["pn"].ToString();
                //    applytype.SelectedValue = ldt.Rows[0]["applytype"].ToString();
                //    modifyremark.Text = ldt.Rows[0]["modifyremark"].ToString();
                //}
                //else
                //{
                //    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                //}

                //lssql += " where GYGSNo='" + this.m_sid + "' order by a.typeno, pgi_no, pgi_no_t,cast(right(op,len(op)-2) as int)"; //order by a.typeno,op
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            bind_grid(ldt_detail);
        }

        // //if ((StepID.ToUpper() != SQ_StepID && StepID != "A") || state == "edit")
        //if ((StepID.ToUpper() != SQ_StepID && StepID != "A") || state == "edit" || (this.m_sid != "" && txt_CreateById.Value == "")) 
        //{
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        //    ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        //    ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
        //    ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;
            
        //    //20190320 add 注释 释放GP12可以修改
        //    //if (((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).SelectedValue == "Y")
        //    //{
        //    //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;
        //    //}
        //    //else
        //    //{
        //    //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = true;
        //    //}
            
        //}

        //if (StepID.ToUpper() != SQ_StepID && StepID != "A")
        //{
        //    applytype.CssClass = "lineread";
        //    applytype.Enabled = false;

        //    modifyremark.ReadOnly = true;
        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;
        //}

        //if (applytype.SelectedValue == "")
        //{
        //    if (((TextBox)this.FindControl("ctl00$MainContent$ver")).Text == "A" || ((TextBox)this.FindControl("ctl00$MainContent$ver")).Text == "")
        //    {
        //        applytype.SelectedValue = "新增工艺";

        //        applytype.CssClass = "lineread";
        //        applytype.Enabled = false;
        //    }
        //}

        Setbzlb();//绑定申请类别
        Settypeno();//绑定包装类别
        //Setfilestype();//绑定附件类别

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }
    public void bind_grid(DataTable dt)
    {
        this.gv.DataSource = dt;
        this.gv.DataBind();
        setread_By_wkzx();
    }

    //根据工作中心决定  行  修改的 权限
    private void setread_By_wkzx()
    {
        //bool bf_modify_read = false;

        //if (Request.QueryString["display"] == null && ((TextBox)this.FindControl("ctl00$MainContent$ver")).Text != "A" && ((TextBox)this.FindControl("ctl00$MainContent$ver")).Text != "")//修改申请  且 在申请步骤
        //{
        //    if (this.m_sid != "")
        //    {
        //        string stepname_gp = DbHelperSQL.Query("select top 1 stepname from RoadFlowWebForm.dbo.WorkFlowTask where flowid='EE59E0B3-D6A1-4A30-A3B4-65D188323134' and InstanceID='"
        //              + this.m_sid + "' order by sort desc").Tables[0].Rows[0][0].ToString();
        //        if (stepname_gp == "申请人")//申请步骤
        //        {
        //            bf_modify_read = true;
        //        }
        //    }
        //    else
        //    {
        //        bf_modify_read = true;
        //    }
        //}

        //if (this.m_sid != "")
        //{
        //    string stepname_gp = DbHelperSQL.Query("select top 1 stepname from RoadFlowWebForm.dbo.WorkFlowTask where flowid='EE59E0B3-D6A1-4A30-A3B4-65D188323134' and InstanceID='"
        //            + this.m_sid + "' order by sort desc").Tables[0].Rows[0][0].ToString();
        //    if (stepname_gp == "检验工时申请")//步骤：检验工时申请
        //    {
        //        bf_modify_read = true;
        //    }
        //}
        //else
        //{
        //    bf_modify_read = true;
        //}


        //DataTable dt = Get_wkzx(txt_domain.Text, LogUserModel.UserId);

        //if (bf_modify_read == true)//修改申请 且 在申请步骤、检验工时申请
        //{

        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = true;

        //    if (((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue == "机加")
        //    {
        //        DataTable dt_jj = (DataTable)gv_d.DataSource;
        //        if (dt_jj != null)
        //        {
        //            for (int i = 0; i < dt_jj.Rows.Count; i++)
        //            {
        //                if (dt_jj.Rows[i]["gzzx"].ToString() != "" && dt.Select("wc_wkctr='" + dt_jj.Rows[i]["gzzx"].ToString() + "'").Length <= 0)
        //                {
        //                    setread_grid(i);
        //                }
        //            }
        //        }

        //    }
        //    if (((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue == "压铸")
        //    {
        //        DataTable dt_yz = (DataTable)gv_d_yz.DataSource;
        //        if (dt_yz != null)
        //        {
        //            for (int i = 0; i < dt_yz.Rows.Count; i++)
        //            {
        //                if (dt_yz.Rows[i]["gzzx"].ToString() != "" && dt.Select("wc_wkctr='" + dt_yz.Rows[i]["gzzx"].ToString() + "'").Length <= 0)
        //                {
        //                    setread_grid_yz(i);
        //                }
        //            }
        //        }
        //    }

        //}
    }

    public void setGridIsRead(DataTable ldt_detail)
    {
        //string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).SelectedValue;

        ////特殊处理，签核界面，明细的框框拿掉
        //string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
        //                where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
        //                    and instanceid='{2}' and (stepname='{3}' or stepname='{4}')";
        ////and instanceid = '{2}' and stepname = '{3}'";
        //string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请人", "检验工时申请");
        //DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];


        //for (int i = 0; i < ldt_detail.Rows.Count; i++)
        //{

        //    if (state == "edit" || ldt_detail.Rows[i]["ver"].ToString() != "A")
        //    {
        //        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        //        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        //        ((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        //        ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
        //        ((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

        //        ((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

        //        //20190320 add 注释 释放GP12可以修改
        //        //if (((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).SelectedValue == "Y")
        //        //{
        //        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = false;
        //        //}
        //        //else
        //        //{
        //        //    ((RadioButtonList)this.FindControl("ctl00$MainContent$containgp")).Enabled = true;
        //        //}


        //        if (state != "edit" && ldt_detail.Rows[i]["ver"].ToString() != "A")
        //        {
        //            if (ldt_flow_pro.Rows.Count == 0)
        //            {
        //                this.btnflowSend.Text = "批准";
        //            }
        //            if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
        //            {
        //                if (lstypeno == "机加")
        //                {
        //                    setread(i);
        //                }
        //                if (lstypeno == "压铸")
        //                {
        //                    setread_yz(i);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (ldt_flow_pro.Rows.Count == 0)
        //        {
        //            this.btnflowSend.Text = "批准";
        //        }
        //        if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
        //        {
        //            if (lstypeno == "机加")
        //            {
        //                setread(i);
        //            }
        //            if (lstypeno == "压铸")
        //            {
        //                setread_yz(i);
        //            }
        //        }
        //    }
        //}
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        add_row(1);
    }

    protected void add_row(int lnadd_rows)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

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
        this.gv.DataSource = ldt;
        this.gv.DataBind();
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
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
        gv.DataSource = ldt;
        gv.DataBind();
    }
    protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        if (param == "clear")
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            ldt.Rows.Clear();
            ldt.AcceptChanges();
            gv.DataSource = ldt;
            gv.DataBind();
        }
        if (param == "add")
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            if (ldt.Rows.Count <= 0)
            {
                add_row(2);
            }
        }

    }
    protected void gv_DataBound(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, e.GetType(), "gridcolor", "gv_color(); RefreshRow();", true);
    }

    //绑定申请类别
    public void Settypeno()
    {
        typeno.Columns.Clear();
        string lssql = @"select [Code],[Name]
                        from (
	                        select '新增' [Code],'新增' [Name],0 rownum
	                        union 
	                        select '零件信息修改' [Code],'零件信息修改' [Name],1 rownum
	                        union 
	                        select '装箱数据修改' [Code],'装箱数据修改' [Name],2 rownum
	                        union 
	                        select '包装明细修改' [Code],'包装明细修改' [Name],3 rownum
	                        ) a
                        order by rownum";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
        typeno.ValueField = "Name";
        typeno.Columns.Add("Name", "描述", 80);
        typeno.DataSource = ldt;
        typeno.DataBind();
    }

    //绑定包装类别
    public void Setbzlb()
    {
        bzlb.Columns.Clear();
        string lssql = @"select [Code],[Name]
                        from (
	                        select '成品可回用' [Code],'成品可回用' [Name],0 rownum
	                        union 
	                        select '成品一次性' [Code],'成品一次性' [Name],1 rownum
	                        union 
	                        select '原材料包装' [Code],'原材料包装' [Name],2 rownum
	                        union 
	                        select '内包装' [Code],'内包装' [Name],3 rownum
	                        ) a
                        order by rownum";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
        bzlb.ValueField = "Name";
        bzlb.Columns.Add("Name", "描述", 80);
        bzlb.DataSource = ldt;
        bzlb.DataBind();
    }

    ////绑定附件类别
    //public void Setfilestype()
    //{
    //    bzlb.Columns.Clear();
    //    string lssql = @"select [Code],[Name]
    //                    from (
    //                     select '零件图片' [Code],'零件图片' [Name],0 rownum
    //                     union 
    //                     select '包装箱内部' [Code],'包装箱内部' [Name],1 rownum
    //                     union 
    //                     select '包装箱外观' [Code],'包装箱外观' [Name],2 rownum
    //                     ) a
    //                    order by rownum";
    //    DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
    //    files_type.ValueField = "Name";
    //    files_type.Columns.Add("Name", "描述", 80);
    //    files_type.DataSource = ldt;
    //    files_type.DataBind();
    //}


    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Pack";
    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }

    protected void uploadcontrol_2_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }
    protected void uploadcontrol_3_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }
    #endregion

    private bool SaveData()
    {
        bool bflag = false;

        return bflag;
    }

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
        {
            flag = true;
        }
        else
        {
            flag = SaveData();
        }
        //保存当前流程
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
        {
            flag = true;
        }
        else
        {
            flag = SaveData();
        }
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion
}