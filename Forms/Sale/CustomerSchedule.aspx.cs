using DevExpress.Web;
using Maticsoft.DBUtility;
using Pgi.Auto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Forms_Sale_CustomerSchedule : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    public string SQ_StepID = "12d2501c-d316-4edd-a43e-2c44d11d2ef6";
    public string HQ_StepID = "1d00f84b-3e89-4daf-8d69-34ffd6ecfdf7";
    public string SQ_QR_StepID = "dbcfb77e-897b-4bb7-b20e-4509c136f280";
    public string UserId = "";
    public string DeptName = "";

    string FlowID = "A";
    string StepID = "A";
    string state = "";
    string m_sid = "";

    string sql_TaxRate = @"select null TaxRate,''TaxRate_Code 
                        union SELECT distinct cast([tx2_tax_pct] as numeric(18,0)) TaxRate,[tx2_pt_taxc] TaxRate_Code FROM [qad].[dbo].[qad_tx2_mstr] 
                            where tx2_exp_date is null and tx2_tax_type='VAT'and tx2_domain in('100','200') and [tx2_pt_taxc]<>'13'";

    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["ApplyId_i"] == null) { ViewState["ApplyId_i"] = ""; }
        if (ViewState["qad_rq_i"] == null) { ViewState["qad_rq_i"] = ""; }

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
        UserId = LogUserModel.UserId;
        DeptName = LogUserModel.DepartName;
        //DeptName = "销售二部"; 

        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            string lssql = @"select a.* from [dbo].[PGI_CustomerSchedule_Dtl_Form] a";

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    ApplyDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    CreateId.Text = LogUserModel.UserId;
                    CreateName.Text = LogUserModel.UserName;
                    ApplyId.Text = LogUserModel.UserId;
                    ApplyName.Text = LogUserModel.UserName;
                    ApplyDeptName.Text = LogUserModel.DepartName;
                    ApplyTelephone.Text = LogUserModel.Telephone;
                }

                //修改申请
                if (state == "edit")
                {
                    //----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了
                    string domain = Request.QueryString["domain"];
                    string part = Request.QueryString["part"];
                    string cust_part = Server.UrlDecode(Request.QueryString["cust_part"]);
                    string ship = Request.QueryString["ship"];

                    string re_sql = @" exec [Report_CS_edit_check] '" + domain + "','" + part + "','" + cust_part + "'";
                    DataSet ds = DbHelperSQL.Query(re_sql);
                    string dt_flag = ds.Tables[0].Rows[0][0].ToString();
                    DataTable dt = ds.Tables[0];

                    if (dt_flag == "Y1")
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "【PGI_零件号】" + part + "【申请工厂】" + domain + "【客户物料号】" + cust_part
                            + "正在<font color='red'>申请中</font>，不能修改(单号:" + dt.Rows[0]["formno"].ToString() + ",申请人:"
                            + dt.Rows[0]["ApplyId"].ToString() + "-" + dt.Rows[0]["ApplyName"].ToString() + ")!");
                    }
                    else if (dt_flag == "Y2")
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "【PGI_零件号】" + part + "【申请工厂】" + domain + ",物料状态DEAD或OBS，不可修改!");
                    }
                    else
                    {
                        string sql_con = @"exec Report_CS_edit '" + domain + "','" + part + "','" + cust_part + "','" + ship + "','" + LogUserModel.UserId + "'";
                        DataSet ds_con = DbHelperSQL.Query(sql_con);
                        DataTable ldt_head = ds_con.Tables[0];
                        ldt_detail = ds_con.Tables[1];

                        SetControlValue("PGI_CustomerSchedule_Main_Form", "HEAD_NEW", this.Page, ldt_head.Rows[0], "ctl00$MainContent$");
                    }

                }
                else//新增申请
                {
                    lssql += " where 1=0";
                    ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
                }
            }
            else
            {
                //表头赋值
                DataTable ldt = DbHelperSQL.Query("select * from PGI_CustomerSchedule_Main_Form where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    SetControlValue("PGI_CustomerSchedule_Main_Form", "HEAD_NEW", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                    hd_domain.Value = ldt.Rows[0]["domain"].ToString();

                    if (ldt.Rows[0]["files"].ToString() != "")
                    {
                        this.ip_filelist_db.Value = ldt.Rows[0]["files"].ToString();
                        bindtab();
                    }
                    //bind_qad_qr(ldt.Rows[0]["IsSign_HQ"].ToString(), ldt.Rows[0]["sign_name_show"].ToString());
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql += " where CSNo='" + this.m_sid + "'";

                if ((StepID.ToUpper() != SQ_StepID.ToUpper() && StepID.ToUpper() != "A") || Request.QueryString["display"] != null)
                {
                    lssql += " order by a.isyn desc"; 
                }
                else//申请人
                {
                    lssql += " order by a.numid ";
                }

                if (StepID.ToUpper() == SQ_QR_StepID.ToUpper())//申请人确认
                {
                    lssql = @"exec [Report_CS_GetData_Dtl] '"+ hd_domain.Value + "','"+ this.m_sid + "'";
                }
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            }
            bind_grid(ldt_detail);
        }
        else
        {
            bindtab();
        }

        //签核界面show：取消了，不要每次加载的时候 都绑定
        if ((StepID.ToUpper() != SQ_StepID.ToUpper() && StepID.ToUpper() != "A") || Request.QueryString["display"] != null)
        {
            bind_qad_qr_load();
        }

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }
    

    void bindtab()
    {
        bool is_del = true;
        DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('"
                                            + StepID + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + FlowID + "' as varchar(36)) and instanceid='"
                                            + this.m_sid + "' and stepname='包装工程师申请'").Tables[0];

        if (ldt_flow.Rows.Count == 0)
        {
            is_del = false;
        }
        if (Request.QueryString["display"] != null)//未发送之前
        {
            is_del = false;
        }

        tab1.Rows.Clear();
        string[] ls_files = this.ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_files.Length; i++)
        {
            TableRow tempRow = new TableRow();
            string[] ls_files_oth = ls_files[i].Split(',');

            HyperLink hl = new HyperLink();
            hl.Text = ls_files_oth[0].ToString();
            hl.NavigateUrl = ls_files_oth[1].ToString();
            hl.Target = "_blank";

            Label lb = new Label();
            lb.Text = ls_files_oth[2].ToString();

            TableCell td1 = new TableCell(); td1.Controls.Add(hl); td1.Width = Unit.Pixel(400);
            tempRow.Cells.Add(td1);

            TableCell td2 = new TableCell(); td2.Controls.Add(lb); td2.Width = Unit.Pixel(60);
            tempRow.Cells.Add(td2);

            if (is_del)
            {
                LinkButton Btn = new LinkButton();
                Btn.Text = "删除"; Btn.ID = "btn_lj_" + i.ToString(); Btn.Click += new EventHandler(Btn_Click);

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
        int index = Convert.ToInt32(btn.ID.Substring(7));

        string filedb = ip_filelist_db.Value;
        string[] ls_files = filedb.Split(';');

        string files = "";
        for (int i = 0; i < ls_files.Length; i++)
        {
            if (i != index) { files += ls_files[i] + ";"; }
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        ip_filelist_db.Value = files;

        bindtab();
    }

    void bind_qad_qr_load()
    {
        string lspart = part.Text; string lsdomain = domain.Text;

        //DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        //CustomerSchedule cs = new CustomerSchedule();
        //DataTable dt_IsSign = cs.CS_IsModifyByBom(ldt, lspart, lsdomain);//, lstypeno, this.m_sid            

        DataTable dt_IsSign = DbHelperSQL.Query("exec usp_CS_IsSign_HQ_again '" + lspart + "','" + lsdomain + "','" + m_sid + "'").Tables[0];

        string IsSign_HQ = dt_IsSign.Rows[0]["IsSign_HQ"].ToString();
        string workcode = dt_IsSign.Rows[0]["workcode"].ToString();

        string[] IsSign_HQ_list = IsSign_HQ.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        bool part_yn = true, ship_yn = true, pr_list_yn = true, rf_yn = true, site_yn = true;
        foreach (var item in IsSign_HQ_list)
        {
            if (item == "Y_part") { part_yn = false; }
            if (item == "Y_ship") { ship_yn = false; }
            if (item == "Y_pr_list") { pr_list_yn = false; }
            if (item == "Y_rf") { rf_yn = false; }
            if (item == "Y_site") { site_yn = false; }
        }
        cb_part_qr.Checked = part_yn; cb_ship_qr.Checked = ship_yn; cb_pr_list_qr.Checked = pr_list_yn; cb_rf_qr.Checked = rf_yn; cb_site_qr.Checked = site_yn;

        string[] workcode_list = workcode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        lbl_par_qr.Text = "责任人【" + workcode_list[0] + "】"; lbl_ship_qr.Text = "责任人【" + workcode_list[1] + "】";
        lbl_pr_list_qr.Text = "责任人【" + workcode_list[2] + "】"; lbl_rf_qr.Text = "责任人【" + workcode_list[3] + "】";
        lbl_site_qr.Text = "责任人【" + workcode_list[4] + "】";
    }

    //void bind_qad_qr(string IsSign_HQ,string workcode)
    //{
    //    string[] IsSign_HQ_list = IsSign_HQ.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    //    bool part_yn = true, ship_yn = true, pr_list_yn = true, rf_yn = true;
    //    foreach (var item in IsSign_HQ_list)
    //    {
    //        if (item == "Y_part") { part_yn = false; }
    //        if (item == "Y_ship") { ship_yn = false; }
    //        if (item == "Y_pr_list") { pr_list_yn = false; }
    //        if (item == "Y_rf") { rf_yn = false; }
    //    }
    //    cb_part_qr.Checked = part_yn; cb_ship_qr.Checked = ship_yn; cb_pr_list_qr.Checked = pr_list_yn; cb_rf_qr.Checked = rf_yn;

    //    string[] workcode_list = workcode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    //    if (workcode_list.Length==4)
    //    {
    //        lbl_par_qr.Text = "责任人【" + workcode_list[0] + "】"; lbl_ship_qr.Text = "责任人【" + workcode_list[1] + "】";
    //        lbl_pr_list_qr.Text = "责任人【" + workcode_list[2] + "】"; lbl_rf_qr.Text = "责任人【" + workcode_list[3] + "】";
    //    }
    //}

    public void bind_grid(DataTable dt)
    {
        this.gv.DataSource = dt;
        this.gv.DataBind();
        GetGrid(dt);
        setGridIsRead(dt, typeno.Text);
    }

    public void setGridIsRead(DataTable ldt_detail, string typeno)
    {
        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "申请人");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (typeno == "新增")
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
            else if (typeno == "修改")
            {
                if (state == "edit")//修改申请的时候
                {
                    if (ldt_flow_pro.Rows.Count != 0)//申请的时候
                    {
                        setread_edit(i, ldt_detail.Rows[i]);
                    }
                    if (Request.QueryString["display"] == null)
                    {
                        setread_edit(i, ldt_detail.Rows[i]);
                    }
                }
                else
                {
                    if (ldt_flow_pro.Rows.Count != 0)//申请的时候
                    {
                        setread_edit(i, ldt_detail.Rows[i]);
                    }
                    if (Request.QueryString["display"] == null)
                    {
                        setread_edit(i, ldt_detail.Rows[i]);
                    }

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
        }
    }

    public void setread(int i)
    {
        part.CssClass = "lineread"; part.ReadOnly = true;//PGI零件号
        domain.CssClass = "lineread"; domain.ReadOnly = true;
        cust_part.CssClass = "lineread"; cust_part.ReadOnly = true;
        comment.CssClass = "lineread"; comment.ReadOnly = true;

        this.uploadcontrol.Visible = false;

        ViewState["ApplyId_i"] = "Y";
        ViewState["qad_rq_i"] = "Y";

        btnadd.Visible = false; btndel.Visible = false;

        cb_part_qr.Enabled = false;cb_ship_qr.Enabled = false;cb_pr_list_qr.Enabled = false;cb_rf_qr.Enabled = false; cb_site_qr.Enabled = false;

        setread_grid(i);
    }

    public void setread_grid(int i)
    {
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["delivery_mode"], "delivery_mode")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["site"], "site")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["ship"], "ship")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["curr"], "curr")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["taxable"], "taxable")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["taxc"], "taxc")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["loc"], "loc")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["consignment"], "consignment")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["consignment_loc"], "consignment_loc")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["modelyr"], "modelyr")).Enabled = false;
        ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["isyn"], "isyn")).Enabled = false;


        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["shipname"], "shipname")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["shipname"], "shipname")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["nbr"], "nbr")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["nbr"], "nbr")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["bill"], "bill")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["bill"], "bill")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["pr_list"], "pr_list")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["pr_list"], "pr_list")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["remark"], "remark")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["remark"], "remark")).BorderStyle = BorderStyle.None;
    }

    //编辑申请的时候
    public void setread_edit(int i,DataRow dr)
    {
        part.CssClass = "lineread"; part.ReadOnly = true;//PGI零件号
        domain.CssClass = "lineread"; domain.ReadOnly = true;
        cust_part.CssClass = "lineread"; cust_part.ReadOnly = true;
        comment.CssClass = "lineread"; comment.ReadOnly = true;

        ViewState["ApplyId_i"] = "Y";

        setread_grid_edit(i, dr);
    }

    public void setread_grid_edit(int i, DataRow dr)
    {
        if (dr["line"].ToString() != "")//qad的  行 不为空，不可以编辑 KEY
        {
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["delivery_mode"], "delivery_mode")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["site"], "site")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["ship"], "ship")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["consignment"], "consignment")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["consignment_loc"], "consignment_loc")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["modelyr"], "modelyr")).Enabled = false;

            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["shipname"], "shipname")).ReadOnly = true;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["shipname"], "shipname")).BorderStyle = BorderStyle.None;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["nbr"], "nbr")).ReadOnly = true;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["nbr"], "nbr")).BorderStyle = BorderStyle.None;

            if (dr["shipname"].ToString() != "中转库")
            {
                ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["bill"], "bill")).ReadOnly = false;
                ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["bill"], "bill")).Border.BorderWidth = Unit.Pixel(1);
                ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["pr_list"], "pr_list")).ReadOnly = false;
                ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["pr_list"], "pr_list")).Border.BorderWidth = Unit.Pixel(1);
            }
            else
            {
                ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["curr"], "curr")).Enabled = false;
                ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["taxable"], "taxable")).Enabled = false;
                ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["taxc"], "taxc")).Enabled = false;
            }
        }
    }

    //发货自
    protected void site_Callback(object sender, CallbackEventArgsBase e)
    {
        string[] para = e.Parameter.Split('|');
        FillsiteCombo(sender as ASPxComboBox, para[0], para[1]);
    }

    protected void FillsiteCombo(ASPxComboBox cmb, string domain_str, string delivery_mode)
    {
        if (string.IsNullOrEmpty(delivery_mode)) return;
        cmb.Items.Clear();

        string sql_site = @"select si_site as value from qad.dbo.qad_si_mstr_ISYX where is_yx=1 and si_domain='" + domain_str + "'";
        if (delivery_mode == "直发")
        {
            sql_site = sql_site + @" and si_domain=si_site";
        }
        sql_site = sql_site + @" order by si_site";
        DataTable ldt_site = DbHelperSQL.Query(sql_site).Tables[0];

        cmb.DataSource = ldt_site;
        cmb.TextField = "value";
        cmb.ValueField = "value";
        cmb.Columns.Add("value", "发货自", 15);
        cmb.TextFormatString = "{0}";
        cmb.DataBind();
    }

    //发货至
    protected void ship_Callback(object sender, CallbackEventArgsBase e)
    {
        string[] para = e.Parameter.Split('|');
        FillshipCombo(sender as ASPxComboBox, para[0], para[1], para[2]);
    }

    protected void FillshipCombo(ASPxComboBox cmb, string domain_str, string delivery_mode, string site)
    {
        if (string.IsNullOrEmpty(delivery_mode)) return;
        cmb.Items.Clear();

        string sql_ship = @"select DebtorShipToCode as value,BusinessRelationCode as bill from form4_Customer_DebtorShipTo where IsEffective='有效' and charindex('" + domain_str + "',Debtor_Domain)>0";
        if (delivery_mode == "中转库发" && site == domain_str)
        {
            sql_ship = @"select si_site as value from qad.dbo.qad_si_mstr_ISYX where is_yx=1 and si_domain='" + domain_str + "' and si_domain<>si_site";
        }
        DataTable ldt_ship = DbHelperSQL.Query(sql_ship).Tables[0];
        
        cmb.DataSource = ldt_ship;
        cmb.TextField = "value";
        cmb.ValueField = "value";
        cmb.Columns.Add("value", "发货至", 15);
        cmb.TextFormatString = "{0}";
        cmb.DataBind();
    }

    protected void GetGrid(DataTable DT)
    {
        string domain_str = domain.Text;
        DataTable ldt = DT;
        int index = gv.VisibleRowCount;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            ASPxComboBox tb_delivery_mode = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["delivery_mode"], "delivery_mode");
            ASPxComboBox tb_site = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["site"], "site");
            ASPxComboBox tb_ship = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["ship"], "ship");
            ASPxComboBox tb_curr = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["curr"], "curr");
            ASPxComboBox tb_modelyr = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["modelyr"], "modelyr");
            ASPxComboBox tb_loc = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["loc"], "loc");
            ASPxComboBox tb_taxable = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["taxable"], "taxable");
            ASPxComboBox tb_taxc = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["taxc"], "taxc"); 
            ASPxComboBox tb_consignment = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["consignment"], "consignment");
            ASPxComboBox tb_consignment_loc = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["consignment_loc"], "consignment_loc");
            ASPxComboBox tb_isyn = (ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["isyn"], "isyn");

            FillsiteCombo(tb_site, domain_str, ldt.Rows[i]["delivery_mode"].ToString());
            FillshipCombo(tb_ship, domain_str, ldt.Rows[i]["delivery_mode"].ToString(), ldt.Rows[i]["site"].ToString());

            //发货方式
            string sql_delivery_mode = @"select [value]
                                from (
	                                select '直发' [value],0 rownum
	                                union 
	                                select '中转库发' [value],1 rownum
	                                ) a
                                order by rownum";
            string sql_curr = @"select '' value union select 'CNY' value union select 'USD' value union select 'EUR' value union select 'JPY' value union select 'THB' value union select 'GBP' value";

            //模型年
            string sql_modelyr = @"select [value]
                                from (
	                                select '' [value],0 rownum
	                                union 
	                                select '2019' [value],1 rownum
	                                union 
	                                select '2020' [value],2 rownum
	                                ) a
                                order by rownum";
            string sql_loc = @"select [value]
                                from (
	                                select '' [value],0 rownum
	                                union 
	                                select '9060' [value],1 rownum
	                                union 
	                                select '9071' [value],1 rownum
	                                union 
	                                select '9080' [value],2 rownum
	                                ) a
                                order by rownum";

            string sql_YN = @"select [value]
                                from (
	                                select '' [value],0 rownum
	                                union 
	                                select 'yes' [value],1 rownum
	                                union 
	                                select 'no' [value],2 rownum
	                                ) a
                                order by rownum";

            string sql_consignment_loc = @"select '' value union select loc_loc as value from qad.dbo.qad_loc_mstr where loc_domain='" + domain_str + "' and loc_status='CUSTOMER'";

            DataTable ldt_delivery_mode = DbHelperSQL.Query(sql_delivery_mode).Tables[0];
            DataTable ldt_curr = DbHelperSQL.Query(sql_curr).Tables[0];
            DataTable ldt_modelyr = DbHelperSQL.Query(sql_modelyr).Tables[0];
            DataTable ldt_loc = DbHelperSQL.Query(sql_loc).Tables[0];
            DataTable ldt_YN = DbHelperSQL.Query(sql_YN).Tables[0];
            DataTable ldt_TaxRate = DbHelperSQL.Query(sql_TaxRate).Tables[0];
            DataTable ldt_consignment_loc = DbHelperSQL.Query(sql_consignment_loc).Tables[0];

            tb_delivery_mode.DataSource = ldt_delivery_mode;
            tb_delivery_mode.TextField = "value";
            tb_delivery_mode.ValueField = "value";
            tb_delivery_mode.Columns.Add("value", "发货方式", 25);
            tb_delivery_mode.TextFormatString = "{0}";
            tb_delivery_mode.DataBind();
            tb_delivery_mode.Value = ldt.Rows[i]["delivery_mode"].ToString();

            tb_site.Value = ldt.Rows[i]["site"].ToString();           
            tb_ship.Value = ldt.Rows[i]["ship"].ToString();

            tb_curr.DataSource = ldt_curr;
            tb_curr.TextField = "value";
            tb_curr.ValueField = "value";
            tb_curr.Columns.Add("value", "币别", 15);
            tb_curr.TextFormatString = "{0}";
            tb_curr.DataBind();
            tb_curr.Value = ldt.Rows[i]["curr"].ToString();

            tb_modelyr.DataSource = ldt_modelyr;
            tb_modelyr.TextField = "value";
            tb_modelyr.ValueField = "value";
            tb_modelyr.Columns.Add("value", "模型年", 15);
            tb_modelyr.TextFormatString = "{0}";
            tb_modelyr.DataBind();
            tb_modelyr.Value = ldt.Rows[i]["modelyr"].ToString();

            tb_loc.DataSource = ldt_loc;
            tb_loc.TextField = "value";
            tb_loc.ValueField = "value";
            tb_loc.Columns.Add("value", "库位", 15);
            tb_loc.TextFormatString = "{0}";
            tb_loc.DataBind();
            tb_loc.Value = ldt.Rows[i]["loc"].ToString();

            tb_taxable.DataSource = ldt_YN;
            tb_taxable.TextField = "value";
            tb_taxable.ValueField = "value";
            tb_taxable.Columns.Add("value", "应纳税", 15);
            tb_taxable.TextFormatString = "{0}";
            tb_taxable.DataBind();
            tb_taxable.Value = ldt.Rows[i]["taxable"].ToString();

            tb_taxc.DataSource = ldt_TaxRate;
            tb_taxc.TextField = "TaxRate_Code";
            tb_taxc.ValueField = "TaxRate_Code";
            tb_taxc.Columns.Add("TaxRate_Code", "税率", 15);
            tb_taxc.TextFormatString = "{0}";
            tb_taxc.DataBind();
            tb_taxc.Value = ldt.Rows[i]["taxc"].ToString();

            tb_consignment.DataSource = ldt_YN;
            tb_consignment.TextField = "value";
            tb_consignment.ValueField = "value";
            tb_consignment.Columns.Add("value", "寄售", 15);
            tb_consignment.TextFormatString = "{0}";
            tb_consignment.DataBind();
            tb_consignment.Value = ldt.Rows[i]["consignment"].ToString();

            tb_consignment_loc.DataSource = ldt_consignment_loc;
            tb_consignment_loc.TextField = "value";
            tb_consignment_loc.ValueField = "value";
            tb_consignment_loc.Columns.Add("value", "寄售地点", 25);
            tb_consignment_loc.TextFormatString = "{0}";
            tb_consignment_loc.DataBind();
            tb_consignment_loc.Value = ldt.Rows[i]["consignment_loc"].ToString();
            
            tb_isyn.DataSource = ldt_YN;
            tb_isyn.TextField = "value";
            tb_isyn.ValueField = "value";
            tb_isyn.Columns.Add("value", "有效", 15);
            tb_isyn.TextFormatString = "{0}";
            tb_isyn.DataBind();
            tb_isyn.Value = ldt.Rows[i]["isyn"].ToString();
        }
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
                else if (ldt.Columns[j].ColumnName == "rjzj")
                {
                    ldr[ldt.Columns[j].ColumnName] = "AR";
                }
                else if (ldt.Columns[j].ColumnName == "channel")
                {
                    ldr[ldt.Columns[j].ColumnName] = "101";
                }
                else if (ldt.Columns[j].ColumnName == "consignment")
                {
                    ldr[ldt.Columns[j].ColumnName] = "no";
                }
                else if (ldt.Columns[j].ColumnName == "isyn")
                {
                    ldr[ldt.Columns[j].ColumnName] = "yes";
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.Add(ldr);
        }

        ldt.AcceptChanges();
        //this.gv.DataSource = ldt;
        //this.gv.DataBind();
        //GetGrid(ldt);
        bind_grid(ldt);
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        string msg = "";
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            //if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            //{
            //    ldt.Rows[i].Delete();
            //}
            //else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            //{
            //    ldt.Rows[i].Delete();
            //}
            if (ldt.Rows[i]["flag"].ToString() == "1")
            {
                if (ldt.Rows[i]["line"].ToString() == "")
                {
                    ldt.Rows[i].Delete();
                }
                else
                {
                    msg += "第"+(i+1).ToString()+ "行，QAD订单行"+ ldt.Rows[i]["line"].ToString() + "已经存在，不可删除！<br />";
                }
               
            }
        }

        if (msg != "")
        {
            Pgi.Auto.Public.MsgBox(this, "alert", msg);
        }

        ldt.AcceptChanges();
        //gv.DataSource = ldt;
        //gv.DataBind();
        //GetGrid(ldt);
        bind_grid(ldt);
    }

    //protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    string param = e.Parameters.Trim();
    //    if (param == "clear")
    //    {
    //        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
    //        ldt.Rows.Clear();
    //        ldt.AcceptChanges();
    //        gv.DataSource = ldt;
    //        gv.DataBind();
    //    }
    //    if (param == "add")
    //    {
    //        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
    //        if (ldt.Rows.Count <= 0)
    //        {
    //            add_row(2);
    //        }
    //    }

    //}

    protected void gv_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }

        string CSNo = e.GetValue("CSNo").ToString();
        if (CSNo == "") { return; }

        // 停留在申请人步骤不加色彩，
        string stepname = DbHelperSQL.Query("select top 1 stepname from RoadFlowWebForm.dbo.WorkFlowTask where flowid='3e31a6c8-b80e-4179-bacd-ba6be7a2afe2' and InstanceID='" + CSNo + "' order by sort desc").Tables[0].Rows[0][0].ToString();
        if (stepname == "申请人") { return; }

        string backcolor = "";
        if (e.GetValue("modify_flag").ToString() == "add")//新增行
        {
            backcolor = "#FA8072";
        }
        if (e.GetValue("modify_flag").ToString() == "update")//修改行，包含修改为失效的
        {
            backcolor = "#EEEE00";
        }
        if (e.GetValue("isyn").ToString() == "no" && e.GetValue("modify_flag").ToString() == "")//无动作 ，本身就是失效的
        {
            backcolor = "#DCDCDC";
        }

        if (backcolor != "")
        {
            e.Row.Style.Add("background-color", backcolor);
            for (int i = 0; i < this.gv.DataColumns.Count; i++)
            {
                if (this.gv.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gv.Columns[gv.DataColumns[i].FieldName], gv.DataColumns[i].FieldName) is ASPxTextBox)
                {
                    ((ASPxTextBox)this.gv.FindRowCellTemplateControl(e.VisibleIndex
                       , (GridViewDataColumn)this.gv.Columns[gv.DataColumns[i].FieldName], gv.DataColumns[i].FieldName)).BackColor = System.Drawing.ColorTranslator.FromHtml(backcolor);
                }
            }
            return;
        }

    }

    [WebMethod]
    public static string GetDataByShip(string delivery_mode, string site, string ship, string domain, string part,string cust_part)
    {
        string shipname = ""; string nbr = ""; string bill = ""; string curr = ""; string pr_list = ""; string taxable = ""; string taxc = ""; string addresstype = "";

        if (delivery_mode == "中转库发" && site == domain)
        {
            //string sql_ship = @"select a.ad_bus_relation,a.shipname,b.cm_curr,b.cm_pr_list,case b.cm_taxable when 1 then 'yes' else 'no' end cm_taxable,b.cm_taxc
            string sql_ship = @"select a.ad_bus_relation,a.shipname,b.cm_curr,'Z'+a.ad_addr cm_pr_list,case b.cm_taxable when 1 then 'yes' else 'no' end cm_taxable,b.cm_taxc
                              from (select ad_addr,ad_bus_relation,ad_name as shipname from qad_ad_mstr where ad_domain='{0}' and ad_addr='{1}') a
                                inner join qad.dbo.qad_cm_mstr b on a.ad_bus_relation=b.cm_addr and b.cm_domain='{0}' ";
            sql_ship = string.Format(sql_ship, domain, ship);
            DataTable ldt_ship = DbHelperSQL.Query(sql_ship).Tables[0];
            if (ldt_ship.Rows.Count > 0)
            {
                shipname = ldt_ship.Rows[0]["shipname"].ToString();
                bill = ldt_ship.Rows[0]["ad_bus_relation"].ToString();
                curr = ldt_ship.Rows[0]["cm_curr"].ToString();
                pr_list = ldt_ship.Rows[0]["cm_pr_list"].ToString();
                taxable = ldt_ship.Rows[0]["cm_taxable"].ToString();
                taxc = ldt_ship.Rows[0]["cm_taxc"].ToString();
            }
        }
        else
        {
            string sql_ship = @"select top 1 a.BusinessRelationCode,a.shipname,a.AddressTypeCode,b.cm_curr
                                    ,b.cm_pr_list+case when a.AddressTypeCode like '%售后%' then 'SP' else '' end cm_pr_list
                                    ,case b.cm_taxable when 1 then 'yes' else 'no' end cm_taxable,b.cm_taxc
                            from (
                                select BusinessRelationCode,right(DebtorShipToName,len(DebtorShipToName)-CHARINDEX(' ',DebtorShipToName)) shipname,AddressTypeCode,updatedate 
                                from form4_Customer_DebtorShipTo where IsEffective='有效' and charindex('{0}',Debtor_Domain)>0 and DebtorShipToCode='{1}'
                                ) a
                                    inner join qad.dbo.qad_cm_mstr b on a.BusinessRelationCode=b.cm_addr and b.cm_domain='{0}'
                            order by updatedate desc";
            sql_ship = string.Format(sql_ship, domain, ship);
            DataTable ldt_ship = DbHelperSQL.Query(sql_ship).Tables[0];
            if (ldt_ship.Rows.Count > 0)
            {
                shipname = ldt_ship.Rows[0]["shipname"].ToString();
                bill = ldt_ship.Rows[0]["BusinessRelationCode"].ToString();
                curr = ldt_ship.Rows[0]["cm_curr"].ToString();
                pr_list = ldt_ship.Rows[0]["cm_pr_list"].ToString();
                taxable = ldt_ship.Rows[0]["cm_taxable"].ToString();
                taxc = ldt_ship.Rows[0]["cm_taxc"].ToString();
                addresstype = ldt_ship.Rows[0]["AddressTypeCode"].ToString();
            }
        }

        //销售订单产生规则，根据PGI零件号，按照发货至自增
        /*string sql_nbr = @"select so_domain,sod_part,so_ship,so_nbr
                        from qad.dbo.qad_so_mstr so
	                        inner join qad.dbo.qad_sod_det sod on so.so_nbr=sod.sod_nbr and so.so_domain=sod.sod_domain and so.so_site=sod.sod_site
                        where so.so_sched='1' and so_domain='{0}' and sod_part='{1}' and so_ship='{2}'";
        sql_nbr = string.Format(sql_nbr, domain, part, ship);*/
        /*string sql_nbr = @"select so_domain,so_ship,so_nbr
                        from qad.dbo.qad_so_mstr so
                        where so.so_sched='1' and so_domain='{0}' and so_ship='{1}'";*/
        string sql_nbr = @"select so_domain,sod_part,so_ship,so_nbr
                        from qad.dbo.qad_so_mstr so
	                        inner join qad.dbo.qad_sod_det sod on so.so_nbr=sod.sod_nbr and so.so_domain=sod.sod_domain and so.so_site=sod.sod_site
                        where so.so_sched='1' and so_domain='{0}' and (sod_part='{2}' or sod_custpart='{3}') and so_ship='{1}'";
        sql_nbr = string.Format(sql_nbr, domain, ship, part, cust_part);
        DataTable ldt_nbr = DbHelperSQL.Query(sql_nbr).Tables[0];

        string nbr_num = "";
        if (ldt_nbr.Rows.Count > 0)
        {
            nbr_num = DbHelperSQL.Query("select nchar(ascii('A')+" + (ldt_nbr.Rows.Count - 1).ToString() + ")").Tables[0].Rows[0][0].ToString();
        }
        nbr = ship + nbr_num;

        //add 2020/6/23 09:42
        string sql_nbr_a = @" select a.scx_order
                             from (select distinct scx_domain,scx_order,scx_shipfrom,scx_shipto from qad.dbo.qad_scx_ref ) a
	                            left join qad.dbo.qad_so_mstr b on a.scx_domain=b.so_domain and a.scx_order=b.so_nbr
                        where b.so_sched='1' and scx_domain='{0}'and scx_shipfrom='{1}' and scx_shipto='{2}'  and so_bill='{3}'  and so_curr='{4}' ";
        sql_nbr_a = string.Format(sql_nbr_a, domain, site, ship, bill, curr);
        DataTable ldt_nbr_a = DbHelperSQL.Query(sql_nbr_a).Tables[0];
        if (ldt_nbr_a.Rows.Count > 0)
        {
            nbr = ldt_nbr_a.Rows[0]["scx_order"].ToString();
        }
        else
        {
            string sql_nbr_a_n = @" select max(a.scx_order)scx_order
                             from (select distinct scx_domain,scx_order,scx_shipfrom,scx_shipto from qad.dbo.qad_scx_ref ) a
	                            left join qad.dbo.qad_so_mstr b on a.scx_domain=b.so_domain and a.scx_order=b.so_nbr
                        where so.so_sched='1' and scx_domain='{0}' and scx_shipto='{2}'  and so_bill='{3}'  and so_curr='{4}' ";
            sql_nbr_a_n = string.Format(sql_nbr_a_n, domain, site, ship, bill, curr);
            DataTable ldt_nbr_a_n = DbHelperSQL.Query(sql_nbr_a_n).Tables[0];
            if (ldt_nbr_a.Rows.Count > 0)
            {
                string scx_order = ldt_nbr_a.Rows[0]["scx_order"].ToString();
                string _last_str = scx_order.Right(1);
                if (Regex.Matches(_last_str, "[A-Z]").Count <= 0)
                {
                    nbr = ship + "A";
                }
                else if (_last_str == "Z")
                {
                    nbr = scx_order + "A";
                }
                else
                {
                    nbr_num = DbHelperSQL.Query("select nchar(ascii('" + _last_str + "')+1)").Tables[0].Rows[0][0].ToString();
                    nbr = scx_order.Left(scx_order.Length - 1)+ nbr_num;
                }
            }
            else
            {

                nbr = ship + "A";
            }
        }
        //end

        string result = "[{\"shipname\":\"" + shipname + "\",\"bill\":\"" + bill + "\",\"curr\":\"" + curr 
                + "\",\"pr_list\":\"" + pr_list + "\",\"taxable\":\"" + taxable + "\",\"taxc\":\"" + taxc 
                + "\",\"addresstype\":\"" + addresstype + "\",\"nbr\":\"" + nbr + "\"}]";
        return result;

    }

    [WebMethod]
    public static string CheckData(string applyid, string formno, string part, string domain, string cust_part, string typeno)
    {
        string manager_flag = ""; //string zg_id = "";
        //CheckData_manager(applyid, out manager_flag, out zg_id);

        string part_flag = CheckVer_data(part, domain, cust_part, typeno, formno);

        string result = "[{\"manager_flag\":\"" + manager_flag + "\",\"part_flag\":\"" + part_flag + "\"}]";
        return result;

    }

    //public static void CheckData_manager(string applyid, out string manager_flag, out string zg_id)
    //{
    //    //------------------------------------------------------------------------------验证工程师对应主管是否为空
    //    manager_flag = "";

    //    DataTable dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + applyid + "')").Tables[0];
    //    zg_id = dt_manager.Rows[0]["zg_id"].ToString();

    //    if (zg_id == "")
    //    {
    //        manager_flag += "工程师(" + applyid + ")的直属主管不存在，不能提交!<br />";
    //    }
    //}

    public static string CheckVer_data(string part, string domain, string cust_part, string typeno, string formno)
    {
        string flag = "";

        string sql = @"exec Report_CS_CheckData '{0}','{1}','{2}','{3}','{4}'";
        sql = string.Format(sql, part, domain, cust_part, formno, typeno);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        if (dt.Rows[0][0].ToString() == "Y0")
        {
            flag = "【PGI_零件号】" + part + "对应的项目不存在，不能申请!<br />";
        }

        if (dt.Rows[0][0].ToString() == "Y1")
        {
            flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + "【客户物料号】" + cust_part + "正在申请中，不能申请!<br />";
        }

        if (dt.Rows[0][0].ToString() == "Y2")
        {
            flag = "【PGI_零件号】" + part + "【申请工厂】" + domain + "【客户物料号】" + cust_part + "已经存在，不能新增申请!<br />";
        }

        return flag;
    }

    [WebMethod]
    public static string CheckData_dtl(string formno, string part, string domain, string cust_part, string typeno
                                    , string site, string ship, string bill, string curr
                                    , string pr_list, string modelyr, string nbr, string delivery_mode
                                    , string line)
    {
        string flag = "";

        string sql = @"exec Report_CS_CheckData_dtl '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}'";
        sql = string.Format(sql, formno, part, domain, cust_part, typeno, site, ship, bill, curr, pr_list, modelyr, nbr, delivery_mode, line);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        flag = dt.Rows[0][0].ToString();
        string result = "[{\"flag\":\"" + flag + "\"}]";
        return result;

    }

    private bool SaveData(string action)
    {
        bool bflag = false;

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = GetControlValue("PGI_CustomerSchedule_Main_Form", "HEAD_NEW", this, "ctl00$MainContent${0}");

        string applyid = ApplyId.Text; string applyname = ApplyName.Text;
        string lspart = part.Text; string lscust_part = cust_part.Text; string lsdomain = domain.Text;
        string lstypeno = typeno.Text;

        //string manager_flag = ""; string zg_id = "";
        //CheckData_manager(applyid, out manager_flag, out zg_id);

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "CS" + System.DateTime.Now.ToString("yyMMdd");
            this.m_sid = Pgi.Auto.Public.GetNo("CS", lsid, 0, 4);

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "formno")
                {
                    ls[i].Value = this.m_sid;
                    FormNo.Text = this.m_sid;
                    break;
                }

            }

        }

        //主管
        //Pgi.Auto.Common lczg_id = new Pgi.Auto.Common();
        //lczg_id.Code = "zg_id";
        //lczg_id.Key = "";
        //lczg_id.Value = "u_" + zg_id;
        //ls.Add(lczg_id);

        //自定义，上传文件
        string savepath_new = @"\" + savepath + @"\";
        var despath = MapPath("~") + savepath_new + m_sid + @"\";
        if (!System.IO.Directory.Exists(despath))
        {
            System.IO.Directory.CreateDirectory(despath);
        }

        #region 附件

        string files = "";
        string[] ls_files = ip_filelist.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_oth = item.Split(',');

            //目的地存在的话，先删除
            string tmp = despath + ls_files_oth[1].Replace(savepath_new, "");
            if (File.Exists(tmp)) { File.Delete(tmp); }

            //文件从临时目录转移到表单单号下面
            FileInfo fi = new FileInfo(MapPath("~") + ls_files_oth[1]);
            fi.MoveTo(tmp);

            files += item.Replace(savepath_new, savepath_new + m_sid + @"\") + ";";
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            files += item + ";";
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        #endregion

        // 增加上传文件列:附件
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "files";
        lcfile.Key = "";
        lcfile.Value = files;
        ls.Add(lcfile);

        //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        //主表相关字段赋值到明细表
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["CSNo"] = m_sid;
            ldt.Rows[i]["numid"] = (i + 1);
        }
        /*
        //---------------------------------------------------------表体数据，申请人步骤的时候才更新---------------------------------------------------------------------
        if (StepID.ToUpper() == "A" || StepID.ToUpper() == SQ_StepID.ToUpper())
        {
            string IsSign_HQ = "";//判定是否存在加签
            string SignEmp_id = "";//加签人员
            string sign_name_show = "";//不管是否会签，显示所有会签负责人
            if (action == "submit")
            {
                try
                {
                    CustomerSchedule cs = new CustomerSchedule();
                    DataTable dt_IsSign = cs.CS_IsModifyByBom(ldt, lspart, lsdomain);//, lstypeno, this.m_sid            
                    IsSign_HQ = dt_IsSign.Rows[0]["IsSign_HQ"].ToString();
                    SignEmp_id = dt_IsSign.Rows[0]["SignEmp_id"].ToString();
                    sign_name_show = dt_IsSign.Rows[0]["workcode"].ToString();
                }
                catch (Exception ex)
                {
                    IsSign_HQ = "e";
                }
            }

            Pgi.Auto.Common lcIsSign_HQ = new Pgi.Auto.Common();
            lcIsSign_HQ.Code = "IsSign_HQ";
            lcIsSign_HQ.Key = "";
            lcIsSign_HQ.Value = IsSign_HQ;
            ls.Add(lcIsSign_HQ);

            Pgi.Auto.Common lcSignEmp_id = new Pgi.Auto.Common();
            lcSignEmp_id.Code = "SignEmp_id";
            lcSignEmp_id.Key = "";
            lcSignEmp_id.Value = SignEmp_id;
            ls.Add(lcSignEmp_id);

            Pgi.Auto.Common lcsign_name_show = new Pgi.Auto.Common();
            lcsign_name_show.Code = "sign_name_show";
            lcsign_name_show.Key = "";
            lcsign_name_show.Value = sign_name_show;
            ls.Add(lcsign_name_show);
        }*/

        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_CustomerSchedule_Main_Form"));

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
                ls_del.Sql = "delete from PGI_CustomerSchedule_Dtl_Form where CSNo='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_CustomerSchedule_Dtl_Form where CSNo='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_CustomerSchedule_Dtl_Form", "id", "Column1,flag");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }

            if (action == "submit")
            {
                //更新修改标记字段modify_flag：与qad的数据作比较，新增行add，修改行update
                Pgi.Auto.Common ls_modify_flag_a = new Pgi.Auto.Common();
                ls_modify_flag_a.Sql = @"update PGI_CustomerSchedule_Dtl_Form set modify_flag='add' where CSNo='{0}' and isnull(line,'')=''";
                ls_modify_flag_a.Sql = string.Format(ls_modify_flag_a.Sql, m_sid);
                ls_sum.Add(ls_modify_flag_a);

                Pgi.Auto.Common ls_modify_flag_u = new Pgi.Auto.Common();
                ls_modify_flag_u.Sql = @"update PGI_CustomerSchedule_Dtl_Form set modify_flag='update' 
                                        from (select so_domain,so_nbr,sod_line,so_bill,so_curr,sod_pr_list
	                                            ,case sod_taxable when 1 then 'yes' else 'no' end sod_taxable,sod_taxc,sod_loc
	                                            ,case when [sod_end_eff[1]]] is null or [sod_end_eff[1]]] >= convert(datetime,CONVERT(VARCHAR(10), GETDATE(), 120)) then 'yes' else 'no' end sod_isyn	
                                            from qad.dbo.qad_so_mstr so
	                                            inner join qad.dbo.qad_sod_det sod on so.so_nbr=sod.sod_nbr and so.so_domain=sod.sod_domain and so.so_site=sod.sod_site
                                            where so.so_sched='1' and so.so_domain='{1}') a
                                        where CSNo='{0}' and isnull(line,'')<>'' and nbr=so_nbr and isnull(line,'')=sod_line
                                            and (bill<>so_bill or curr<>so_curr or pr_list<>sod_pr_list or taxable<>sod_taxable or taxc<>sod_taxc or loc<>sod_loc or isyn<>sod_isyn)";
                ls_modify_flag_u.Sql = string.Format(ls_modify_flag_u.Sql, m_sid, lsdomain);
                ls_sum.Add(ls_modify_flag_u);

                //更新税率值
                Pgi.Auto.Common ls_taxratecode = new Pgi.Auto.Common();
                ls_taxratecode.Sql = @"update PGI_CustomerSchedule_Dtl_Form set taxc_rate=a.TaxRate
                                    from ({1}) a
                                    where PGI_CustomerSchedule_Dtl_Form.CSNo='{0}' and PGI_CustomerSchedule_Dtl_Form.taxc=a.TaxRate_Code";
                ls_taxratecode.Sql = string.Format(ls_taxratecode.Sql, m_sid, sql_TaxRate);
                ls_sum.Add(ls_taxratecode);

            }
        }
        else
        {
            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            ls_del.Sql = "delete from PGI_CustomerSchedule_Dtl_Form where CSNo='" + m_sid + "'";
            ls_sum.Add(ls_del);
        }

        //-----------------------------------------------------------需要即时验证是否存在正在申请的或者保存着的项目号

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        //---------------------------------------------------------表体数据，申请人步骤的时候才更新:判断是否会签的时候，新增行，以及修改的行，所以需要在判断了哪些行修改了在做这个---------------------------
        if (StepID.ToUpper() == "A" || StepID.ToUpper() == SQ_StepID.ToUpper())
        {
            string IsSign_HQ = "";//判定是否存在加签
            string SignEmp_id = "";//加签人员
            string sign_name_show = "";//不管是否会签，显示所有会签负责人
            if (action == "submit")
            {
                try
                {
                    CustomerSchedule cs = new CustomerSchedule();
                    DataTable dt_IsSign = DbHelperSQL.Query("exec usp_CS_IsSign_HQ_again '" + lspart + "','" + lsdomain + "','" + m_sid + "'").Tables[0];
                    IsSign_HQ = dt_IsSign.Rows[0]["IsSign_HQ"].ToString();
                    SignEmp_id = dt_IsSign.Rows[0]["SignEmp_id"].ToString();
                    sign_name_show = dt_IsSign.Rows[0]["workcode"].ToString();
                }
                catch (Exception ex)
                {
                    IsSign_HQ = "e";
                }

                DbHelperSQL.ExecuteSql(@"update PGI_CustomerSchedule_Main_Form set IsSign_HQ='" + IsSign_HQ + "',SignEmp_id='" + SignEmp_id + "',sign_name_show='" + sign_name_show
                    + "' where formno='" + m_sid + "'");
            }
        }

        if (ln > 0)
        {
            bflag = true;

            var titletype = lstypeno == "新增" ? "客户日程申请" : "客户日程修改";
            string title = titletype + "[" + this.m_sid + "][" + applyname + "][" + lspart + "][" + lscust_part + "][" + lsdomain + "]";

            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";

        }
        else
        {
            bflag = false;
        }

        return bflag;
    }


    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Sale";
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
    #endregion

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID.ToUpper() && StepID.ToUpper() != SQ_QR_StepID.ToUpper())
        {
            flag = true;
        }
        else
        {
            flag = SaveData("save");
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
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID.ToUpper() && StepID.ToUpper() != SQ_QR_StepID.ToUpper())
        {
            flag = true;
        }
        else
        {
            flag = SaveData("submit");
        }
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
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

        List<Pgi.Auto.Common> ls = new List<Common>();
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            string lscontrol_id = ldt.Rows[i]["control_id"].ToString().ToLower();
            if (lscontrol_format != "")
            {
                lscontrol_id = lscontrol_format.Replace("{0}", lscontrol_id);
            }
            if (p.FindControl(lscontrol_id) != null)
            {



                Pgi.Auto.Common com = new Common();
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
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXTEXTBOX")
                {
                    com.Value = p.Request.Form[lscontrol_id].ToString();
                    ((ASPxTextBox)p.FindControl(lscontrol_id)).Text = p.Request.Form[lscontrol_id].ToString();
                    lstr = ((ASPxTextBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
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
                else if (row["control_type"].ToString() == "ASPXTEXTBOX")
                {
                    ((ASPxTextBox)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower())).Text = columnValue;
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