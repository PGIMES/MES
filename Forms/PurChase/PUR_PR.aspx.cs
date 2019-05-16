using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using System.Linq;
using DevExpress.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using Pgi.Auto;
using System.IO;

public partial class PUR_PR : PGIBasePage
{
    string m_sid = "";
    public string fieldStatus;    
    public string DisplayModel;
    public string ValidScript="";
    public string IsRead = "N";

    public string SQ_StepID = "6F4C466E-6673-4C18-A541-333565FBF545";

    string FlowID = "A";
    string StepID = "A";

    /*
protected void Page_Load(object sender, EventArgs e)
{
    Page.MaintainScrollPositionOnPostBack = true;
    //      WebForm.Common.DataTransfer.PaoZiLiao                                        02128  00404      00076  01968  
    LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
    Session["LogUser"] = LogUserModel;
    Session["UserAD"] = LogUserModel.ADAccount;
    Session["UserId"] = LogUserModel.UserId;

    #region "IsPostBack"
    if (!IsPostBack)
    {   //文件上传在updatepanel需用此方法+enctype = "multipart/form-data" ，否则无法上传文件
        PostBackTrigger trigger = new PostBackTrigger();
        trigger.ControlID = btnflowSend.UniqueID;
        UpdatePanel1.Triggers.Add(trigger);
        PostBackTrigger trigger2 = new PostBackTrigger();
        trigger2.ControlID = btnSave.UniqueID;
        UpdatePanel1.Triggers.Add(trigger2);
        if (LogUserModel != null)
        {
            //当前登陆人员
            txt_LogUserId.Value = LogUserModel.UserId;
            txt_LogUserName.Value = LogUserModel.UserName;
           // txt_LogUserJob.Value = LogUserModel.JobTitleName;
            txt_LogUserDept.Value = LogUserModel.DepartName;

            if (Request["instanceid"] != null)//页面加载
            {                                           
                //日志加载                     
               // bindrz2_log(requestid, gv_rz2);
            }
            else//新建
            {                             
                CreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                CreateById.Text = LogUserModel.UserId;
                CreateByName.Text = LogUserModel.UserName;                    
                this.domain.Text = LogUserModel.DomainName;
                DeptName.Text = LogUserModel.DepartName;
                this.phone.Text = LogUserModel.Telephone;
                txt_LogUserId.Value = LogUserModel.UserId;
                txt_LogUserName.Value = LogUserModel.UserName;                                    
            }

            BaseFun fun = new BaseFun();
            //var tbltype = DbHelperSQL.Query("select '存货' as value ,'存货' as text").Tables[0];
            //20181107 add heguiqin 新增采购类别
            var tbltype = DbHelperSQL.Query("select '存货(刀具类)' as value ,'存货(刀具类)' as text union select '存货(其他辅料类)' as value ,'存货(其他辅料类)' as text").Tables[0];

            fun.initDropDownList(prtype, tbltype, "value", "text");
             //申请部门
            BaseFun.loadDepartment(applydept, domain.SelectedValue);
            applydept.Items.Insert(0,"");
        }

        //  this.bindData();


    }
    #endregion

    DataTable dtMst =new DataTable();
    var strType = "";
    string id = Request["instanceid"]; // get instanceid
    if(ViewState["PRNo"] == null) { ViewState["PRNo"] = ""; }

    //--==第1步：Get instance Data=======================================================================================================      
    if (id!=""&&id!=null ) 
    {   
        ViewState["PRNo"] = id==null?"":id;
        PRNo.Text = ViewState["PRNo"].ToString();                          
        dtMst = DbHelperSQL.Query("select * from pur_pr_main_form where prno='" + id.ToString() + "' ").Tables[0];                
    }
    if (dtMst.Rows.Count > 0)
    {
        PRNo.Text = dtMst.Rows[0]["PRNo"].ToString();
        strType = dtMst.Rows[0]["PRType"].ToString();
        var item = prtype.Items.FindByText(strType);
        if (item != null)
        {
            strType = item.Value;
        }

        CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
        CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
        CreateDate.Text = dtMst.Rows[0]["CreateDate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
       // applydept.SelectedValue = dtMst.Rows[0]["applydept"].ToString();
    }
    else
    {
      //  strType = prtype.SelectedItem.Value;
    }


    //--== 第 步:装载控件========================================================================================================
    loadControl(strType);
    //将表单主表值给页面
    if (dtMst!=null && dtMst.Rows.Count > 0)
    {   
        Pgi.Auto.Control.SetControlValue("PUR_PR_Main_Form", "main", this, dtMst.Rows[0]);
        var item = prtype.Items.FindByText(dtMst.Rows[0]["prtype"].ToString());
        if (item != null)
        {
            prtype.ClearSelection();
            item.Selected=true;
        }
        //显示文件
        ShowFile(dtMst.Rows[0]["files"]==null? "": dtMst.Rows[0]["files"].ToString(),'|');

    }


    DataTable dtscript =new DataTable();
    var sqlScript = "";

    //Load Check Script  （不需修改）      
    sqlScript ="select control_event as script from auto_form where form_type='" + prtype.SelectedValue + "' and isnull(control_id,'')<>'' and isnull(control_event,'')<>'' order by form_div, control_order;";
    dtscript = DbHelperSQL.Query(sqlScript).Tables[0];
    for (int i = 0; i < dtscript.Rows.Count; i++)
    {
        ValidScript = ValidScript + dtscript.Rows[i]["script"].ToString();
    }



    //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
    string FlowID = Request.QueryString["flowid"]; 
    string StepID = Request.QueryString["stepid"];
    DisplayModel = Request.QueryString["display"] ?? "0"; 
    RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
    fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    ViewState["fieldStatus"] = fieldStatus;

    //特殊处理
    if (Request["mode"] == null )
    {
        DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" + StepID + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + FlowID + "' as varchar(36)) and instanceid='" + this.PRNo.Text + "'").Tables[0];

        if (ldt_flow.Rows.Count >0)
        {
            for(int row = 0; row <gvdtl.VisibleRowCount;row++ )
            {
                if (ldt_flow.Rows[0]["stepname"].ToString() != "申请" )
                {                 
                    ((DevExpress.Web.ASPxDateEdit)this.gvdtl.FindRowCellTemplateControl(row, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["deliverydate"], "deliverydate")).Enabled = false;
                    ((DevExpress.Web.ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(row, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).Enabled = false;
                    ((DevExpress.Web.ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(row, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).Enabled = false;


                    //  ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$$recmdvendorname")).Enabled = false;
                    // ((DevExpress.Web.ASPxComboBox)this.FindControl("gvdtl$cell0_9$TC$usefor")).Enabled = false;
                    // ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).Enabled = false;
                    // ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).Enabled = false;


                }
                else
                {
                    btnflowSend.Text = "提交";
                }
            }        
        }
        else
        {
            btnflowSend.Text = "提交";
        }
    }

}
*/

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }
        if (Request.QueryString["flowid"] != null)
        {
            FlowID = Request.QueryString["flowid"].ToString();
        }
        if (Request.QueryString["stepid"] != null)
        {
            StepID = Request.QueryString["stepid"].ToString();
        }

        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        Session["LogUser"] = LogUserModel;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId;

        if (!IsPostBack)
        {   //文件上传在updatepanel需用此方法+enctype = "multipart/form-data" ，否则无法上传文件
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = btnflowSend.UniqueID;
            UpdatePanel1.Triggers.Add(trigger);
            PostBackTrigger trigger2 = new PostBackTrigger();
            trigger2.ControlID = btnSave.UniqueID;
            UpdatePanel1.Triggers.Add(trigger2);


            BaseFun fun = new BaseFun();

            //var tbltype = DbHelperSQL.Query("select '存货' as value ,'存货' as text").Tables[0];
            //20181107 add heguiqin 新增采购类别
            //string tbltype_sql = @"select '存货(刀具类)' as value ,'存货(刀具类)' as text 
            //                union select '存货(其他辅料类)' as value ,'存货(其他辅料类)' as text 
            //                union select '存货(原材料及前期样件)' as value ,'存货(原材料及前期样件)' as text";
            //string tbltype_sql = @"select '存货(刀具类)' as value ,'存货(刀具类)' as text 
            //                union select '存货(其他辅料类)' as value ,'存货(其他辅料类)' as text 
            //                union select '存货(原材料及前期样件)' as value ,'存货(原材料及前期样件)' as text
            //                union select '费用类' as value ,'费用类' as text
            //                union select '合同类' as value ,'合同类' as text";
            //var tbltype = DbHelperSQL.Query(tbltype_sql).Tables[0];
            //fun.initDropDownList(prtype, tbltype, "value", "text");

            //BaseFun.loadDepartment(applydept, domain.SelectedValue);//申请部门
            //applydept.Items.Insert(0, "");

            //if (LogUserModel != null)
            //{
            //    //当前登陆人员
            //    txt_LogUserId.Value = LogUserModel.UserId;
            //    txt_LogUserName.Value = LogUserModel.UserName;
            //    txt_LogUserDept.Value = LogUserModel.DepartName;
            //}

            DataTable ldt_detail = null;
            string lssql = @"select *,'查看' attachments_name from pur_pr_dtl_form";
            string prtype_value = "";

            if (this.m_sid == "")//新建
            {
                if (LogUserModel != null)
                {
                    CreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //CreateById.Text = "02433";
                    CreateById.Text = LogUserModel.UserId;
                    CreateByName.Text = LogUserModel.UserName;
                    this.domain.Text = LogUserModel.DomainName;
                    DeptName.Text = LogUserModel.DepartName;
                    this.phone.Text = LogUserModel.Telephone;
                    //txt_LogUserId.Value = LogUserModel.UserId;
                    //txt_LogUserName.Value = LogUserModel.UserName;

                    bind_deptname();
                    bind_prtype(CreateById.Text);
                }
                this.prtype.SelectedIndex = 0;

                lssql += " where 1=0";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            }
            else
            {
                DataTable dtMst = new DataTable();
                dtMst = DbHelperSQL.Query("select * from pur_pr_main_form where prno='" + m_sid + "' ").Tables[0];

                if (dtMst.Rows.Count > 0)
                {
                    PRNo.Text = dtMst.Rows[0]["PRNo"].ToString(); prtype_value = dtMst.Rows[0]["PRType"].ToString();

                    CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
                    CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
                    CreateDate.Text = dtMst.Rows[0]["CreateDate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
                    domain.SelectedValue= dtMst.Rows[0]["domain"].ToString();

                    bind_deptname();
                    bind_prtype(CreateById.Text);

                    //将表单主表值给页面
                    Pgi.Auto.Control.SetControlValue("PUR_PR_Main_Form", "main_new", this, dtMst.Rows[0]);

                    //显示文件
                    //ShowFile(dtMst.Rows[0]["files"] == null ? "" : dtMst.Rows[0]["files"].ToString(), '|');
                    if (dtMst.Rows[0]["files"].ToString() != "")
                    {
                        this.ip_filelist_db.Value = dtMst.Rows[0]["files"].ToString();
                        bindtab();
                    }
                }

                if (prtype_value != "刀具类" && prtype_value != "")
                {
                    lssql = @"select *,'无' attachments_name from pur_pr_dtl_form";
                }
                lssql += @" where prno = '" + m_sid + "' order by rowid asc";

                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];

            }          


            PRNo.Text = m_sid;
            //ViewState["PRNo"] = m_sid;
            //ViewState["dtl"] = ldt_detail;
            loadControl(ldt_detail);

            setGridIsRead(ldt_detail, prtype_value);

        }
        else
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gvdtl);
            loadControl(ldt);
            bindtab();
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
                                        + Request.QueryString["flowid"] + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='申请'").Tables[0];

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

            if (ls_files_2.Length == 3)
            {
                hl.Text = ls_files_2[0].ToString();
                hl.NavigateUrl = ls_files_2[1].ToString();
                hl.Target = "_blank";

                lb.Text = ls_files_2[2].ToString();
            }
            else//之前的文件，只有一个路径
            {

                string s = ls_files_2[0].ToString();
                string[] ss= s.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                hl.Text = ss[ss.Length - 1].ToString(); //"文件浏览";

                hl.NavigateUrl = ls_files_2[0].ToString();
                hl.Target = "_blank";

                lb.Text = "";
            }

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

    //绑定申请部门
    public void bind_deptname()
    {
        var tbldept = DbHelperSQL.Query("exec Pur_GetDeptByPerson '" + CreateById.Text + "','" + domain.SelectedValue + "'");
        applydept.DataSource = tbldept;
        applydept.DataTextField = "Dept_Name";
        applydept.DataValueField = "Dept_Name";
        applydept.DataBind();
    }

    //绑定采购类别
    public void bind_prtype(string CreateById)
    {
        BaseFun fun = new BaseFun();

        //string tbltype_sql = @"exec Pur_GetClassByPerson '" + CreateById + "','','PR'";
        string tbltype_sql = @"select * from (
                                select '刀具类' as type ,1 as sort
                                union select '非刀具辅料类' as type ,2 as sort
                                union select '原材料' as type  ,3 as sort
                                union select '费用服务类' as type  ,4 as sort
                                union select '合同类' as type ,5 as sort
                                            ) a order by sort";
        var tbltype = DbHelperSQL.Query(tbltype_sql).Tables[0];
        DataRow dr = tbltype.NewRow(); dr["type"] = ""; tbltype.Rows.InsertAt(dr, 0);
        fun.initDropDownList(prtype, tbltype, "type", "type");

    }

    public void setGridIsRead(DataTable ldt_detail,string formtype)
    {
        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (ldt_flow_pro.Rows.Count == 0 && (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID))
            {
                this.btnflowSend.Text = "批准";
            }
            if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
            {
                setread(i, formtype);
            }
            else
            {
                if (Request.QueryString["display"] != null)
                {
                    setread(i, formtype);
                }
            }
        }
    }

    public void setread(int i,string formtype)
    {
        IsRead = "Y";

        btnAddDetl.Visible = false;
        btnDelete.Visible = false;

        //phone.CssClass = "lineread";
        domain.CssClass = "lineread";
        applydept.CssClass = "lineread";
        prtype.CssClass = "lineread";
        prreason.CssClass = "lineread";


        domain.Enabled = false;
        applydept.Enabled = false;
        prtype.Enabled = false;

        //file.Visible = false;
        this.uploadcontrol.Visible = false;

        if (this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlh"], "wlh") is TextBox)
        {
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlh"], "wlh")).ReadOnly = true;
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlh"], "wlh")).BorderStyle = BorderStyle.None;
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlh"], "wlh")).Attributes.Remove("ondblclick");
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlh"], "wlh")).BackColor = System.Drawing.Color.Transparent;
        }
        if (this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice") is TextBox)
        {
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).Style.Add("text-align", "right");
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).BackColor = System.Drawing.Color.Transparent;
        }
        if (this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlsubtype"], "wlsubtype") is TextBox)
        {
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlsubtype"], "wlsubtype")).BackColor = System.Drawing.Color.Transparent;
        }
        if (this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype") is TextBox)
        {
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype")).BackColor = System.Drawing.Color.Transparent;
        }
        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit") is ASPxComboBox)
        {
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).Enabled = false;
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).Width = Unit.Pixel(30);
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;
        }

        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).Enabled = false;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).Width = Unit.Pixel(120);
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).Enabled = false;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).Width = Unit.Pixel(110);
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).Enabled = false;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).Width = Unit.Pixel(30);
        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlmc"], "wlmc")).ReadOnly = true;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlms"], "wlms")).ReadOnly = true;

        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlmc"], "wlmc")).BackColor = System.Drawing.Color.Transparent;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlms"], "wlms")).BackColor = System.Drawing.Color.Transparent;
                
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlmc"], "wlmc")).BorderStyle = BorderStyle.None;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["wlms"], "wlms")).BorderStyle = BorderStyle.None;

        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targetprice"], "notax_targetprice")).ReadOnly = true;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targetprice"], "notax_targetprice")).BorderStyle = BorderStyle.None;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targetprice"], "notax_targetprice")).Style.Add("text-align", "right");
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targetprice"], "notax_targetprice")).BackColor = System.Drawing.Color.Transparent;

        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).Style.Add("text-align", "right");
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).BackColor = System.Drawing.Color.Transparent;

        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["qty"], "qty")).ReadOnly = true;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["qty"], "qty")).BorderStyle = BorderStyle.None;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["qty"], "qty")).BackColor = System.Drawing.Color.Transparent;
        
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["Note"], "Note")).ReadOnly = true;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["Note"], "Note")).BorderStyle = BorderStyle.None;
        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gvdtl.Columns["Note"], "Note")).BackColor = System.Drawing.Color.Transparent;

        ((ASPxDateEdit)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["deliverydate"], "deliverydate")).Enabled = false;
        ((ASPxDateEdit)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["deliverydate"], "deliverydate")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
        ((ASPxDateEdit)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["deliverydate"], "deliverydate")).Width = Unit.Pixel(72);
        ((ASPxDateEdit)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["deliverydate"], "deliverydate")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc") is ASPxComboBox)
        {
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc")).Enabled = false;
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc")).Width = Unit.Pixel(130);
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;
        }
    }

    public  string GetDanHao()
    {
        string result = "";        
        var sql = string.Format(" select  'PR' + CONVERT(varchar(8), GETDATE(), 112) + right('000' + cast(isnull(right(max(PRNo), 3) + 1, '001') as varchar), 3)  from pur_pr_main_form where PrNo like 'PR' + CONVERT(varchar(8), GETDATE(), 112)+'%'");
        var value = DbHelperSQL.GetSingle(sql).ToString();
        result = value;
        return result;
    }

    #region "WebMethod"

    //获取指定物料最低历史单价
    [WebMethod] 
    public static string GetHistoryPrice(string P1, string P2)
    {
        /*string result = "";
        var strB = new StringBuilder();//历史采购最低价  含税
        strB.Append(" SELECT top 1  (1+(CASE WHEN pod_taxc='17' AND ISNULL([pod_start_eff[1]]],pod_due_date)>='2018-05-01' THEN 16 ");
        strB.Append("           WHEN pod_taxc = '17' AND ISNULL([pod_start_eff[1]]],pod_due_date)< '2018-05-01' THEN 17 ELSE pod_taxc END )/ 100.0) *[pod_pur_cost] ");
        strB.Append(" FROM[qad].[dbo].[qad_pod_det]  where pod_nbr<> '11801'  and pod_type<> 'M'   ");//and ISNULL([pod_start_eff[1]]], pod_due_date)<= dateadd(year, -1, getdate())
        strB.Append("   and pod_part = '{0}' and pod_domain = '{1}'   order by [pod_pur_cost] asc ");

        var sql = string.Format(strB.ToString(), P1, P2);
       //  var sql= string.Format(" select top 1 [pc_amt[1]]] as amt from qad.dbo.qad_pc_mstr where  pc_start between DATEADD(YEAR,-1,GETDATE()) and  GETDATE()  and   pc_part='{0}'   order by [pc_amt[1]]] ", P1); //取价格单
              
        var value = DbHelperSQL.GetSingle(sql) ;        
        result = value==null?"":value.ToString();
        return result;*/

        string result = "";//历史采购最低价  未税
        //string sql = @"select top 1 [pod_pur_cost]
        //            from[qad].[dbo].[qad_pod_det]  
        //            where pod_nbr<> '11801'  and pod_type<> 'm'   and pod_part = '{0}' and pod_domain = '{1}'   
        //            order by [pod_pur_cost] asc ";

        //有物料号取采购记录和价格单最低价
        string sql = @"select top 1 [pod_pur_cost]
                        from(
                            select[pod_pur_cost]
                            from[qad].[dbo].[qad_pod_det]--采购历史记录
                            where pod_nbr <> '11801'  and pod_type <> 'm'   and pod_part = '{0}' and pod_domain = '{1}'
                            union all
                            select[pc_amt[1]]] [pod_pur_cost]
                            from qad.dbo.qad_pc_mstr --价格单记录
                            where pc_part='{0}' and pc_domain='{1}'
                            ) t
                        order by [pod_pur_cost] asc ";

       sql = string.Format(sql, P1, P2);

        var value = DbHelperSQL.GetSingle(sql);
        result = value == null ? "0.0000" : value.ToString();
        return result;
    }
    [WebMethod]
    public static string getHisToryPrice_By_mc_ms(string mc, string ms,string p2)
    {
        string result = "";
        //开窗选择，根据名称及描述
        string sql = @"exec [z_SelectWindow] 'historyprice','{0},','{1}','','',''";

        sql = string.Format(sql, mc + "," + ms, p2);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        result = dt.Rows[0]["价格"].ToString();
        if (dt.Rows.Count >= 2)
        {
            result = dt.Rows[1]["价格"].ToString();
        }
        return result;
    }

    [WebMethod]
    public static string GetDaoJuMatInfo(string P1, string P2, string P3)
    {
        /*
        string result = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("  select wlh,wlmc,ms,class,type,upload, ");
        sb.Append(" (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.domain and [pod_sched]=1 and [pod_part]=a.wlh  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched ");
        sb.Append("  from dbo.PGI_BASE_PART_DATA a where  wlh='{0}' and domain='{1}' ");
        //var sql = string.Format(" select wlh,wlmc,ms,class,type,upload from dbo.PGI_BASE_PART_DATA where  wlh='{0}' and domain='{1}' ", P1,P2);
        var value = DbHelperSQL.Query(string.Format(sb.ToString(),P1,P2)).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
        */

        //20181108 modify heguiqin
        string result = "";
        string sql = "";
        if (P3 == "刀具类")
        {
            //暂时取pt_mstr表的名称、描述，不然历史价格开窗会有问题
            //sql = @"select a.wlh,a.wlmc,a.ms,a.class,a.type,a.upload,b.pt_status
            sql = @"select a.wlh,b.pt_desc1 wlmc,b.pt_desc2 ms,a.class,a.type,a.upload,b.pt_status
                    from dbo.PGI_BASE_PART_DATA a 
	                    left join dbo.qad_pt_mstr b on a.domain=b.pt_domain and a.wlh=b.pt_part
                    where a.wlh='{0}' and a.domain='{1}' and RIGHT(a.wlh,1)<>'X' and (b.pt_status<>'DEAD' and b.pt_status<>'OBS')";
        }

        if (P3 == "非刀具辅料类")
        {
            sql = @"select pt_part wlh,pt_desc1 wlmc,pt_desc2 ms,pt_prod_line+'-'+isnull(b.pt_prod_line_mc,'') class,'' type,'' upload
                    from dbo.qad_pt_mstr a
	                    left join(
		                    select distinct pl_domain, PL_PROD_LINE 
			                    ,case when len(pl_desc)-len(replace(pl_desc,'-',''))=2 then SUBSTRING(pl_desc,dbo.fn_find('-',pl_desc,2)+1 ,LEN(pl_desc)-dbo.fn_find('-',pl_desc,1))
				                    when   len(pl_desc)-len(replace(pl_desc,'-',''))=1 then substring(pl_desc,charindex('-',pl_desc)+1,len(pl_desc)-charindex('-',pl_desc)) else pl_desc
				                    end as pt_prod_line_mc 
		                    from qad.dbo.qad_pl_mstr
		                    where left(pl_prod_line,1) in ('4') and pl_prod_line<>'4010'
		                    ) b on a.pt_domain=b.pl_domain and a.pt_prod_line=b.pl_prod_line
                    where pt_pm_code = 'P' and (pt_status<>'DEAD' and pt_status<>'OBS') and pt_part like 'z%' and pt_prod_line<>'4010'
                        and pt_part='{0}' and pt_domain='{1}' ";
        }

        if (P3 == "原材料")
        {
            sql = @"select pt_part wlh,pt_desc1 wlmc,pt_desc2 ms,pt_prod_line+'-'+isnull(b.pt_prod_line_mc,'') class,'' type,'' upload
                    from dbo.qad_pt_mstr a
	                    left join(
		                    select distinct pl_domain, PL_PROD_LINE 
			                    ,case when len(pl_desc)-len(replace(pl_desc,'-',''))=2 then SUBSTRING(pl_desc,dbo.fn_find('-',pl_desc,2)+1 ,LEN(pl_desc)-dbo.fn_find('-',pl_desc,1))
				                    when   len(pl_desc)-len(replace(pl_desc,'-',''))=1 then substring(pl_desc,charindex('-',pl_desc)+1,len(pl_desc)-charindex('-',pl_desc)) else pl_desc
				                    end as pt_prod_line_mc 
		                    from qad.dbo.qad_pl_mstr
		                    where left(pl_prod_line,1) in ('1','2','3')
		                    ) b on a.pt_domain=b.pl_domain and a.pt_prod_line=b.pl_prod_line
                    where 
                        (
                            (pt_pm_code = 'P' and (pt_status<>'DEAD' and pt_status<>'OBS') and pt_part like 'P%' and pt_prod_line like '1%' ) 
                            or pt_part='P0170AA' or pt_part='P0739AA-01' or pt_part='P0738AA-01'
                        )
                        and pt_part='{0}' and pt_domain='{1}' ";
        }
        var value = DbHelperSQL.Query(string.Format(sql, P1, P2)).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }

    //获取申请部门
    [WebMethod]
    public static string GetDeptByDomain(string domain, string applyid)
    {
        string result = "";
        var sql = string.Format("exec Pur_GetDeptByPerson '{0}','{1}'", applyid, domain);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }

    //部门取部门主管
    [WebMethod]
    public static string getDeptLeaderByDept(string domain, string dept)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[V_HRM_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }

    #endregion


    /*
    protected void SaveData(string formtype,out bool flag)
    {   //保存数据是否成功标识
        flag = true;
       
        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        string instanceid = "0";
        var strprno = "";
        try
        {
            strprno = ViewState["PRNo"].ToString();
            //产生请购单号
            if (ViewState["PRNo"].ToString() == "")
            {   
                strprno = GetDanHao();
                PRNo.Text = strprno;
                ViewState["PRNo"] = strprno;
            }
            else
            {
                strprno = ViewState["PRNo"].ToString();
                PRNo.Text= ViewState["PRNo"].ToString();
            }
            //从Auto_Form 获取值 验证
            List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("pur_pr_main_form", "", this);
            var lsPrNo = ls.Where(r => r.Code == "prno").ToList();
            lsPrNo[0].Value = ViewState["PRNo"].ToString();
            for (int i = 0; i < ls.Count; i++)
            {
                Pgi.Auto.Common com = new Pgi.Auto.Common();
                com = ls[i];
                if (ls[i].Code == "")
                {
                    var msg = ls[i].Value + "不能为空!";
                    flag = false; //
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('" + msg + "');", true);//$(#'" + ls[i].Code + "').focus();
                    return;
                }
            }

            //更新dtl中的prno
            var dtl = Pgi.Auto.Control.AgvToDt(gvdtl);
            for (int i = dtl.Rows.Count - 1; i >= 0; i--)
            {
                var dr = dtl.Rows[i];
                dr["prno"] = strprno;
                //if (dr["wlh"].ToString()=="")
                //{
                //    dr.Delete();
                //}
            }
            

            List<Pgi.Auto.Common> lsdtl = Pgi.Auto.Control.GetList(dtl, "pur_pr_dtl_form", "id", "attachments_name,flag");
            //明细删除增加到list中
            if (Session["del"] != null)
            {
                DataTable ldt_del = (DataTable)Session["del"];
                for (int i = 0; i < ldt_del.Rows.Count; i++)
                {
                    Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
                    ls_del.Sql = "delete from PUR_Pr_Dtl_Form where id=" + ldt_del.Rows[i]["id"].ToString() + "";
                    lsdtl.Add(ls_del);
                }
                Session["del"] = null;
            }
            //CRUD main_form
            instanceid = Pgi.Auto.Control.UpdateValues(ls, "pur_pr_main_form").ToString();

            script += "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + strprno + "');$('#PRNo').val('" + strprno + "');};"; 
            
            
            //CRUD dtl_form
            Pgi.Auto.Control.UpdateListValues(lsdtl);
        }
        catch (Exception e)
        {
            string err = e.Message.Replace("'", "").Replace("\r\n", "").Replace("nvarchar", "<字符串>").Replace("varchar", "<字符串>").Replace("numeric", "<数字>").Replace("int", "<数字>");
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script+"layer.alert('保存表单数据失败，请确认。ErrorMessage:" +err + "');", true);
            flag = false;
            return ;
        }
        finally
        {

        }
        
        //如果是签核或修改 取传递过来instanceid值
        if((ViewState["PRNo"] != null&& ViewState["PRNo"].ToString() != "")|| Request.Form["txtInstanceID"] != "")
        {
            instanceid = Request.Form["txtInstanceID"]==""? (Request["instanceid"]): (txtInstanceID.Text);
        }
        //Save file
        var fileup = (FileUpload)this.FindControl("file");
        var filepath = "";
        if (fileup != null)
        {   if (fileup.HasFile)
            {
                var filename = fileup.FileName;
                SaveFile(fileup,  strprno , out filepath, filename, filename);
                //更新文件目录
                string sqlupdatefilecolum = string.Format("update pur_pr_main_form set files='{0}' where prno='{1}'", filepath, strprno);
                DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
                flag = true;
            }
        }
        //执行流程相关事宜
        if (instanceid  != "0" && instanceid !="")//0 影响行数; "" 没有prno
        {
            var titletype = "请购申请" ;
            string title = titletype + "["+ strprno + "][" + CreateByName.Text+"]"; //设定表单标题
            
            //将实例id,表单标题给流程script
            script += "$('#instanceid',parent.document).val('" + strprno.ToString() + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');" +
                 "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + strprno.ToString() + "');}";
          
        }
        else
        {           
            flag = false;
        }
    }
    */

    /// <summary>
    /// 保存Data
    /// </summary>
    /// <param name="formtype">表单对应的设定</param>
    /// <param name="flag">输出参数是否保存成功</param>

    protected void SaveData(string formtype, out bool flag)
    {
        flag = true;//保存数据是否成功标识
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();//定义总SQL LIST
        //flag = false;return;
        try
        {
            //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
            formtype = prtype.SelectedValue;
            string domain_value = domain.SelectedValue;
            //string applydept_value = applydept.SelectedValue;//签核的时候可以读取到，新申请界面 读取不到，这个控件绑定 是通过前台绑定的

            List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("pur_pr_main_form", "main_new", this);

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "prtype") { ls[i].Value = formtype; }
                if (ls[i].Code.ToLower() == "domain") { ls[i].Value = domain_value; }
                //if (ls[i].Code.ToLower() == "applydept") { ls[i].Value = applydept_value; }
            }

            //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
            DataTable dtl = Pgi.Auto.Control.AgvToDt(gvdtl);

            //获取单号
            //this.m_sid = ViewState["PRNo"].ToString();
            if (this.m_sid == "")//产生请购单号
            {
                this.m_sid = GetDanHao();
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i].Code.ToLower() == "prno")
                    {
                        ls[i].Value = this.m_sid;
                        PRNo.Text = this.m_sid;
                        //ViewState["PRNo"] = this.m_sid;
                    }
                }
                
            }

            //自定义，上传文件Save file
            //var fileup = (FileUpload)this.FindControl("file");
            
            //if (fileup != null)
            //{
            //    if (fileup.HasFile)
            //    {
            //        var filename = fileup.FileName;
            //        var filepath = "";

            //        SaveFile(fileup, this.m_sid, out filepath, filename, filename);

            //        //增加上传文件列
            //        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
            //        lcfile.Code = "files";
            //        lcfile.Key = "";
            //        lcfile.Value = filepath;
            //        ls.Add(lcfile);
            //    }
            //}

            string filepath = "";//string filepath=this.UploadFiles(this.uploadcontrol);
            string[] ls_files = ip_filelist.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ls_files)
            {
                string[] ls_files_2 = item.Split(',');
                if (ls_files_2.Length == 3)//挪动路径到po单号下面
                {
                    FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

                    var sorpath = @"\UploadFile\Purchase\";
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

                    filepath += item.Replace(@"\UploadFile\Purchase\", @"\UploadFile\Purchase\" + m_sid + @"\") + "|";
                }
                else
                {
                    filepath += item + "|";
                }
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


            //主表相关字段赋值到明细表
            string formno_main = "";
            for (int j = 0; j < ls.Count; j++)
            {
                if (ls[j].Code.ToLower() == "prno") { formno_main = ls[j].Value; }
            }
            for (int i = 0; i < dtl.Rows.Count; i++)
            {
                dtl.Rows[i]["prno"] = formno_main;

                if (formtype == "刀具类")//刀具类 隐藏了这两列
                {
                    dtl.Rows[i]["wltype"] = "4010-刀具类";
                    dtl.Rows[i]["unit"] = "EA";
                }
            }

            //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
            //获取的表头信息，自动生成SQL，增加到SUM中
            ls_sum.Add(Pgi.Auto.Control.GetList(ls, "pur_pr_main_form"));

            //明细删除增加到list中
            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            string dtl_ids = "";
            for (int i = 0; i < dtl.Rows.Count; i++)
            {
                if (dtl.Rows[i]["id"].ToString() != "") { dtl_ids = dtl_ids + dtl.Rows[i]["id"].ToString() + ","; }
            }
            if (dtl_ids != "")
            {
                dtl_ids = dtl_ids.Substring(0, dtl_ids.Length - 1);
                ls_del.Sql = "delete from PUR_Pr_Dtl_Form where prno='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PUR_Pr_Dtl_Form where prno='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);


            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> lsdtl = Pgi.Auto.Control.GetList(dtl, "pur_pr_dtl_form", "id", "attachments_name,flag");
            for (int i = 0; i < lsdtl.Count; i++)
            {
                ls_sum.Add(lsdtl[i]);
            }

            //批量提交
            int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

            if (ln > 0)
            {
                flag = true;

                var titletype = "请购申请";
                string title = titletype + "[" + this.m_sid + "][" + CreateByName.Text + "]"; //设定表单标题

                //将实例id,表单标题给流程script
                script += "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                     "$('#customformtitle',parent.document).val('" + title + "');" +
                     "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + this.m_sid + "');$('#PRNo').val('" + this.m_sid + "');}";
            }
            else
            {
                flag = false;
            }
            return;

        }
        catch (Exception e)
        {
            string err = e.Message.Replace("'", "").Replace("\r\n", "").Replace("nvarchar", "<字符串>").Replace("varchar", "<字符串>").Replace("numeric", "<数字>").Replace("int", "<数字>");
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + "layer.alert('保存表单数据失败，请确认。ErrorMessage:" + err + "');", true);
            flag = false;
            return;
        }
    }

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        SaveData(prtype.SelectedValue,out flag);

        //保存当前流程
        if(flag==true)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + " parent.flowSave(true);", true);
           
        }
        
    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        SaveData(prtype.SelectedValue,out flag);
        //发送
        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(Page,this.GetType(), "ok", script + " parent.flowSend(true);", true);           
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion
    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Purchase";
    //public void SaveFile(FileUpload fileupload,string subpath,out string filepath,string oldName,string newName )
    //{
    //    var path = MapPath("~") +  savepath + "\\" + subpath;
    //    filepath = "";
    //    //Create directory
    //    if (!System.IO.Directory.Exists(path))
    //    {
    //        System.IO.Directory.CreateDirectory(path);
    //    }
    //    //save file
    //    var filename = "";
    //    if (fileupload.HasFile)
    //    {
    //        var list = fileupload.PostedFiles;
    //        foreach (var item in list)
    //        {
    //            filename = item.FileName ;

    //            var Svrpath = path  +"\\" + filename;            
    //            item.SaveAs(Svrpath.Replace("&", "_").TrimStart(' '));

    //            filepath  = filepath + "\\" + savepath + "\\" + subpath + "\\" + filename.Replace("&", "_").TrimStart(' ')+"|";
    //        }

    //    }
    //    //return save path
    //    filepath = filepath.TrimEnd('|');
    //    //filepath ="\\"+ savepath + "\\" + subpath+ "\\"+filename.Replace("&", "_").TrimStart(' ');
    //}

    //public void ShowFile(string filestring,char splitchar)
    //{
    //    var arrFile = filestring.Split(splitchar);
    //    foreach(string file in arrFile)
    //    {
    //        var filename = file.Substring(file.LastIndexOf(@"\")+1);
    //        HyperLink link = new HyperLink() {
    //            ID = "lnk_" + file,
    //            NavigateUrl = file,
    //            Text = filename,
    //            Target = "_blank"

    //        };
    //        link.Attributes.Add("style","padding-left:10px;");
    //        filecontainer.Controls.AddAt(0,link);
    //    }
    //    if (arrFile.Length == 0) {
    //        Label lbl = new Label() { Text = "无附件" };
    //        filecontainer.Controls.AddAt(0,lbl ); }
    //}

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


    //public void loadControl()
    //{/*
    //    string formtype = prtype.SelectedValue;

    //    gvdtl.Columns.Clear();
    //    //var mode = Request["mode"] == null ? "" : "_" + Request["mode"].ToString();
    //    //Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form" + mode, "dtl", this.gvdtl, ViewState["dtl"] as DataTable, 2);
    //    //Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form", "dtl", this.gvdtl, ViewState["dtl"] as DataTable, 2);
    //    Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form", "dtl_new_1", this.gvdtl, ViewState["dtl"] as DataTable, 2);
    //    GetGrid(ViewState["dtl"] as DataTable, formtype);
    //    */

    //    string formtype = prtype.SelectedValue;
    //    gvdtl.Columns.Clear();

    //    string formdiv = "";
    //    if (formtype == "存货(刀具类)") { formdiv = "dtl_new_1"; }
    //    else if (formtype == "存货(其他辅料类)" || formtype == "存货(原材料及前期样件)") { formdiv = "dtl_new_2"; }
    //    else { formdiv = "dtl_new_3"; }
    //    Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form", formdiv, this.gvdtl, ViewState["dtl"] as DataTable, 2);
    //    GetGrid(ViewState["dtl"] as DataTable, formtype);

    //    for (int i = 0; i < ((DataTable)ViewState["dtl"]).Rows.Count; i++)
    //    {
    //        if (formtype == "存货(刀具类)" || formtype == "存货(其他辅料类)" || formtype == "存货(原材料及前期样件)")
    //        {
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlmc"], "wlmc")).Enabled = false;//.ReadOnly = true;
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlms"], "wlms")).Enabled = false;//.ReadOnly = true;

    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlmc"], "wlmc")).BackColor = System.Drawing.Color.Transparent;
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlms"], "wlms")).BackColor = System.Drawing.Color.Transparent;
    //        }

    //        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlsubtype"], "wlsubtype") is TextBox)
    //        {
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wlsubtype"], "wlsubtype")).BackColor = System.Drawing.Color.Transparent;
    //        }
    //        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype") is TextBox)
    //        {
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype")).BackColor = System.Drawing.Color.Transparent;
    //        }
    //        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype") is ASPxComboBox)
    //        {
    //            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["wltype"], "wltype")).Width = Unit.Pixel(160);
    //        }
    //        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit") is ASPxComboBox)
    //        {
    //            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).Width = Unit.Pixel(45);
    //        }
    //        if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice") is TextBox)
    //        {
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).Style.Add("text-align", "right");
    //            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).BackColor = System.Drawing.Color.Transparent;
    //        }

    //        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).Width = Unit.Pixel(120);
    //        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).Width = Unit.Pixel(120); 
    //        ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).Width = Unit.Pixel(50);            

    //        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).Style.Add("text-align", "right");
    //        ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).BackColor = System.Drawing.Color.Transparent;
    //    }

    //}

    public void loadControl(DataTable ldt)
    {
        string domain_code = domain.SelectedValue;
        string formtype = prtype.SelectedValue; 
        this.gvdtl.Columns.Clear();

        string formdiv = "dtl_daoju";
        if (formtype == "刀具类") { formdiv = "dtl_daoju"; }
        else if (formtype == "非刀具辅料类" || formtype == "原材料") { formdiv = "dtl_ndj_ycl"; }
        else if (formtype == "费用服务类") { formdiv = "dtl_fw"; }
        else if (formtype == "合同类") { formdiv = "dtl_ht"; }

        Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form", formdiv, this.gvdtl, ldt, 2);
        GetGrid(ldt, formtype, domain_code);

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit") is ASPxComboBox)
            {
                ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["unit"], "unit")).Width = Unit.Pixel(45);
            }
            if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice") is TextBox)
            {
                ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).Style.Add("text-align", "right");
                ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_historyprice"], "notax_historyprice")).BackColor = System.Drawing.Color.Transparent;
            }

            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["recmdvendorname"], "recmdvendorname")).Width = Unit.Pixel(120);
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor")).Width = Unit.Pixel(120);
            ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["currency"], "currency")).Width = Unit.Pixel(50);

            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).Style.Add("text-align", "right");
            ((TextBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["notax_targettotal"], "notax_targettotal")).BackColor = System.Drawing.Color.Transparent;

            if (this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc") is ASPxComboBox)
            {
                ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["assetattributedesc"], "assetattributedesc")).Width = Unit.Pixel(130);
            }
        }

    }

    //protected void prtype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string formtype = prtype.SelectedValue;

    //    //if (ViewState["PRNo"] != null && ViewState["PRNo"].ToString() != "")
    //    //{
    //    //    var dtl = DbHelperSQL.Query(@"select *,'查看' attachments_name from pur_pr_dtl_form 
    //    //                                where prno=(select prno from PUR_PR_Main_Form where prno='" + ViewState["PRNo"].ToString() + "' and PRType='" + formtype + @"') 
    //    //                                order by rowid asc").Tables[0];
    //    //    ViewState["dtl"] = dtl;
    //    //}
    //    //else
    //    //{
    //    //    var dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where 1=0 order by rowid asc").Tables[0];
    //    //    ViewState["dtl"] = dtl; ;
    //    //}
    //    //loadControl();

    //    var dtl = new DataTable();
    //    if (this.m_sid != null && this.m_sid != "")
    //    {
    //        dtl = DbHelperSQL.Query(@"select *,'查看' attachments_name from pur_pr_dtl_form 
    //                                    where prno=(select prno from PUR_PR_Main_Form where prno='" + ViewState["PRNo"].ToString() + "' and PRType='" + formtype + @"') 
    //                                    order by rowid asc").Tables[0];
    //    }
    //    else
    //    {
    //        dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where 1=0 order by rowid asc").Tables[0];
    //    }
    //    loadControl(dtl);
    //}

    protected void btnAddDetl_Click(object sender, EventArgs e)
    {
        DataTable dtl = Pgi.Auto.Control.AgvToDt(gvdtl);

        var dr = dtl.NewRow();
        dr["prno"] = PRNo.Text;

        object maxObject;
        maxObject = dtl.Compute("max(rowid)", "");
        if (maxObject.ToString() == "") { maxObject = 0; }
        dr["rowid"] = (Convert.ToInt16(maxObject) + 1).ToString();

        dr["attachments_name"] = prtype.SelectedValue == "刀具类" ? "查看" : "无";
        dtl.Rows.Add(dr);
        var sortrowid = 0;
        for (int row = 0; row < dtl.Rows.Count; row++)
        {

            if (dtl.Rows[row]["id"].ToString().Trim() != "")
            {
                sortrowid = Convert.ToInt16(dtl.Rows[row]["rowid"]);
            }
            else
            {
                dtl.Rows[row]["rowid"] = (sortrowid + 1).ToString();
                sortrowid = Convert.ToInt16(dtl.Rows[row]["rowid"]);
            }

        }

        //ViewState["dtl"] = dtl;
        loadControl(dtl);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gvdtl);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            ldt.Rows[i]["attachments_name"] = prtype.SelectedValue == "刀具类" ? "查看" : "无";
            if (ldt.Rows[i]["flag"].ToString() == "1")
            {
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        //ViewState["dtl"] = ldt;       
        loadControl(ldt);
        Pgi.Auto.Public.MsgBox(this.Page, "alert", "删除成功!");
    }

    /*
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gvdtl);
        DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        ViewState["dtl"] = ldt;
        if (ldt_del.Rows.Count > 0)
        {
            if (Session["del"] != null)
            {
                for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
                {
                    ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
                }

            }
            Session["del"] = ldt_del;
        }

        loadControl();
        Pgi.Auto.Public.MsgBox(this.Page, "alert", "删除成功!");
    }
    */

    protected void GetGrid(DataTable DT, string prtype_v,string domain_code)
    {
        /*
        DataTable ldt = DT;
        int index = gvdtl.VisibleRowCount;
        string sql = @"select ''  value";
        if (prtype_v != "存货(刀具类)")
        {
            sql += @" union all select '无'  value";
        }
        sql += @" union all SELECT replace(pgino+[version]+'/'+productcode,' ','')   FROM [dbo].[form3_Sale_Product_DetailTable]";

        if (prtype_v == "存货(其他辅料类)")
        {
            sql += @" union all select replace(XMBH+'/'+XMMS,' ','') from [dbo].[formtable_main_55_ZDHXM]";
        }
        DataTable ldt_usefor = DbHelperSQL.Query(sql).Tables[0];

        */

        //用于产品/项目
        string sql_usefor = @"select ''  value";
        if (prtype_v != "刀具类")
        {
            sql_usefor += @" union all select '无'  value";
        }
        sql_usefor += @" union all SELECT replace(pgino+[version]+'/'+productcode,' ','')   FROM [dbo].[form3_Sale_Product_DetailTable]";

        if (prtype_v == "非刀具辅料类")
        {
            sql_usefor += @" union all select replace(XMBH+'/'+XMMS,' ','') from [dbo].[formtable_main_55_ZDHXM]";
        }
        DataTable ldt_usefor = DbHelperSQL.Query(sql_usefor).Tables[0];

        for (int i = 0; i < gvdtl.VisibleRowCount; i++)
        {
            ASPxComboBox tb1 = ((ASPxComboBox)this.gvdtl.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gvdtl.Columns["usefor"], "usefor"));
            tb1.DataSource = ldt_usefor;
            tb1.TextField = "value";
            tb1.ValueField = "value";
            tb1.DataBind();
            tb1.Value = DT.Rows[i]["usefor"].ToString();
        }


    }


    protected void gvdtl_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        string formtype = param;

        var dtl = new DataTable();
        if (this.m_sid != null && this.m_sid != "")
        {
            dtl = DbHelperSQL.Query(@"select *,'查看' attachments_name from pur_pr_dtl_form 
                                        where prno=(select prno from PUR_PR_Main_Form where prno='" + this.m_sid + "' and PRType='" + formtype + @"') 
                                        order by rowid asc").Tables[0];
            //ViewState["dtl"] = dtl;
        }
        else
        {
            dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where 1=0 order by rowid asc").Tables[0];
            //ViewState["dtl"] = dtl; ;
        }

        loadControl(dtl);
    }


}



public class GridViewTemplate : ITemplate
{
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler eh;
    private DataControlRowType templateType;
    private string columnName;
    private string columnValue;
    private string controlID;
    public GridViewTemplate(DataControlRowType type, string colname,string colvalue)
    {
        templateType = type;
        columnName = colname;
        columnValue = colvalue;
    }
    public GridViewTemplate(DataControlRowType type, string controlID, string colname, string colvalue)
    {
        templateType = type;
        this.controlID = controlID;
        columnName = colname;
        columnValue = colvalue;
    }
    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                Literal lc = new Literal();
                lc.Text = columnName;
                container.Controls.Add(lc);
                break;
            case DataControlRowType.DataRow://可以定义自己想显示的控件以及绑定事件 
                TextBox txt = new TextBox();
                // txt.Text = columnValue;
              //  txt.DataBinding += new EventHandler(this.TextBoxDataBinding);
                container.Controls.Add(txt);
                break;
            default:
                break;
        }
    }
    private void TextBoxDataBinding(Object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txt.NamingContainer;
        txt.Text = System.Web.UI.DataBinder.Eval(row.DataItem, columnName).ToString();
    }
}
public class GridViewTextTemplate : System.Web.UI.ITemplate
{
    private DataControlRowType _templateType;
    private string _columnName;
    private string _dataType;
    private string Id;
    private string _controlType;
    private DataTable _items;
    public GridViewTextTemplate(DataControlRowType type, string colname, string dataType, string controlType,DataTable items=null)
    {
        _templateType = type;
        _columnName = colname;
        Id = colname;
        _dataType = dataType;
        _controlType = controlType.ToLower();
        _items = items;
    }
    
    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case DataControlRowType.Header:
                Literal myHeadLiteral = new Literal();
                myHeadLiteral.Text = "<b><u>" + _columnName + "</u></b>";
                container.Controls.Add(myHeadLiteral);
                break;
            case DataControlRowType.DataRow:
                switch (_controlType)
                {
                    case "label":
                        Label ctrl = new Label();
                        ctrl.ID = Id;
                        ctrl.DataBinding += new EventHandler(this.LabelDataBinding);
                        container.Controls.Add(ctrl);
                        break;
                    case "textbox":
                        TextBox txt = new TextBox();
                        txt.ID = Id;
                        txt.DataBinding += new EventHandler(this.TextBoxDataBinding);
                        container.Controls.Add(txt);
                        break;
                    case "dropdownlist":
                        DropDownList drop = new DropDownList();
                        drop.ID = Id;
                        drop.DataBinding += new EventHandler(this.DropDownListDataBinding);
                        container.Controls.Add(drop);                         
                        break;
                    case "HyperLink":

                        //HyperLink link = new HyperLink();
                        //link.ID = this.dr["list_fieldname"].ToString();
                        //link.Width = int.Parse(this.dr["list_width"].ToString());
                        //link.ForeColor = System.Drawing.Color.Black;
                        //link.Text = this.dr["list_caption"].ToString();
                        //link.DataBinding += new EventHandler(this.HyperLinkDataBinding);
                        //container.Controls.Add(link);
                        //if (dr["list_type_ref"].ToString() == "DROPDOWN")
                        //{
                        //    // ltxt.DropDownStyle = DevExpress.Web.DropDownStyle.DropDown;
                        //}
                        break;
                    default:
                        break;
                }
                
                break;
            default:
                break;
        }
    }
    private void LabelDataBinding(Object sender, EventArgs e)
    {
        Label ctl = (Label)sender;
        GridViewRow row = (GridViewRow)ctl.NamingContainer;
        ctl.Text = System.Web.UI.DataBinder.Eval(row.DataItem, _columnName).ToString();
        ctl.ID = _columnName;
    }
    private void TextBoxDataBinding(Object sender, EventArgs e)
    {
        TextBox ctl = (TextBox)sender;
        GridViewRow row = (GridViewRow)ctl.NamingContainer;
        ctl.Text = System.Web.UI.DataBinder.Eval(row.DataItem, _columnName).ToString();
    }
    private void DropDownListDataBinding(Object sender, EventArgs e)
    {
        DropDownList ctl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ctl.NamingContainer;
         
        foreach (DataRow irow in _items.Rows)
        {
            var item = new ListItem();
            item.Value = irow["value"].ToString();
            item.Text = irow["text"].ToString();
            ctl.Items.Add(item);
        }
        var items = ctl.Items.FindByValue(System.Web.UI.DataBinder.Eval(row.DataItem, _columnName).ToString());         
        if (items != null) { items.Selected = true; }
    }
    private void HyperLinkDataBinding(Object sender, EventArgs e)
    {
        HyperLink ctl = (HyperLink)sender;
        GridViewRow row = (GridViewRow)ctl.NamingContainer;
       // var _columnName = dr["list_fieldname"].ToString();
        ctl.Text = System.Web.UI.DataBinder.Eval(row.DataItem, _columnName).ToString();
        ctl.NavigateUrl = System.Web.UI.DataBinder.Eval(row.DataItem, _columnName).ToString();
        ctl.ID = _columnName;
    }
}