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
    public string UserId = "";

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
                if (Request.QueryString["formno"] != null && state == "edit")
                {
                    //        //----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了
                    //        string sql_prd = @"select * from PGI_PackScheme_Main where formno='" + Request.QueryString["formno"] + "'";
                    //        DataTable dt_prd = DbHelperSQL.Query(sql_prd).Tables[0];

                    //        string part = dt_prd.Rows[0]["part"].ToString(); string domain = dt_prd.Rows[0]["domain"].ToString();
                    //        string site = dt_prd.Rows[0]["site"].ToString(); string ship = dt_prd.Rows[0]["ship"].ToString();

                    //        //附件：编辑初次加载时候，使用后台绑定的，也是可以删除的
                    //        string files_part = dt_prd.Rows[0]["files_part"].ToString();
                    //        string files_bzx_nb = dt_prd.Rows[0]["files_bzx_nb"].ToString();
                    //        string files_bzx_wg = dt_prd.Rows[0]["files_bzx_wg"].ToString();

                    //        //解析附件，挪到临时目录
                    //        string files_part_new = "";
                    //        if (files_part != "") { files_part_new = jxfiles(files_part); }

                    //        string files_bzx_nb_new = "";
                    //        if (files_bzx_nb != "") { files_bzx_nb_new = jxfiles(files_bzx_nb); }

                    //        string files_bzx_wg_new = "";
                    //        if (files_bzx_wg != "") { files_bzx_wg_new = jxfiles(files_bzx_wg); }

                    //        string re_sql = @"select * from PGI_PackScheme_Main_Form where isnull(iscomplete,'')='' and part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
                    //        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

                    //        if (re_dt.Rows.Count > 0)
                    //        {
                    //            Pgi.Auto.Public.MsgBox(this, "alert", "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship
                    //                + "正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString()
                    //                + ",申请人:" + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + ")!");
                    //        }
                    //        else
                    //        {
                    //            string sql_head_con = @"exec Report_Pack_edit '" + part + "','" + domain + "','" + site + "','" + ship + "','" + LogUserModel.UserId + "'";
                    //            DataSet ds_head_con = DbHelperSQL.Query(sql_head_con);
                    //            DataTable ldt_head = ds_head_con.Tables[0];
                    //            ldt_detail = ds_head_con.Tables[1];

                    //            SetControlValue("PGI_PackScheme_Main_Form", "HEAD", this.Page, ldt_head.Rows[0], "ctl00$MainContent$");
                    //            //绑定上一张的附件
                    //            this.ip_filelist.Value = files_part_new;
                    //            this.ip_filelist_2.Value = files_bzx_nb_new;
                    //            this.ip_filelist_3.Value = files_bzx_wg_new;
                    //        }

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
                    SetControlValue("PGI_CustomerSchedule_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                    hd_domain.Value = ldt.Rows[0]["domain"].ToString();

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

                lssql += " where CSNo='" + this.m_sid + "' order by a.numid";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            }
            bind_grid(ldt_detail);
        }
        else
        {
            bindtab();
        }

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }



    //setCheckBoxListSelectValue(ddlopinion, dtMst.Rows[0]["opinion"].ToString(), ';', true);
    //public void setCheckBoxListSelectValue(CheckBoxList checkboxlist, string checkedValue, char splitChar, bool enabled)
    //{
    //    var list = checkedValue.Split(splitChar);
    //    foreach (var value in list)
    //    {
    //        ListItem item = checkboxlist.Items.FindByValue(value);
    //        if (item != null)
    //        {
    //            item.Selected = true;
    //            item.Enabled = enabled;
    //        }
    //    }
    //    checkboxlist.Enabled = enabled;
    //}

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

    public void bind_grid(DataTable dt)
    {
        this.gv.DataSource = dt;
        this.gv.DataBind();
        GetGrid(dt);
        //setGridIsRead(dt, typeno.Text);
    }

    public void setGridIsRead(DataTable ldt_detail, string typeno)
    {
        ////特殊处理，签核界面，明细的框框拿掉
        //string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
        //                where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
        //                    and instanceid='{2}' and stepname='{3}'";
        //string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "包装工程师申请");
        //DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        //for (int i = 0; i < ldt_detail.Rows.Count; i++)
        //{
        //    if (state == "edit" || typeno != "新增")
        //    {
        //        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
        //        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
        //        //((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

        //        //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
        //        //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

        //        //((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

        //        if (state != "edit" && typeno != "新增")
        //        {
        //            if (ldt_flow_pro.Rows.Count == 0)
        //            {
        //                this.btnflowSend.Text = "批准";
        //            }
        //            if (ldt_flow_pro.Rows.Count == 0 || Request.QueryString["display"] != null)
        //            {
        //                setread(i);
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
        //            setread(i);
        //        }
        //    }
        //}
    }

    public void setread(int i)
    {
        //ver.Enabled = false; bzlb.Enabled = false;//包装类别
        //ljcc_l.Enabled = false; ljcc_w.Enabled = false; ljcc_h.Enabled = false;
        //gdsl_cp.Enabled = false; gdsl_bcp.Enabled = false; //klgx.Enabled = false;
        //bzx_cc.CssClass = "lineread"; bzx_cc.ReadOnly = true;//箱尺寸(L*W*H)
        //bzx_sl_c.Enabled = false; bzx_cs_x.Enabled = false;
        //bzx_xs_c.Enabled = false; bzx_c_t.Enabled = false;
        //bzx_dzcs.Enabled = false; bzx_jzcs.Enabled = false;
        //bzx_t_l.Enabled = false; bzx_t_w.Enabled = false; bzx_t_h.Enabled = false;
        //cbfx_mb_j.Enabled = false;

        //this.uploadcontrol.Visible = false;
        //this.uploadcontrol_2.Visible = false;
        //this.uploadcontrol_3.Visible = false;

        //ViewState["ApplyId_i"] = "Y";

        //btnadd.Visible = false; btndel.Visible = false;

        //if (i == 0)
        //{
        //    gv.Columns[gv.VisibleColumns.Count - 1].Visible = false;
        //    gv.Columns[0].Visible = false;
        //}

        setread_grid(i);
    }

    public void setread_grid(int i)
    {
        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["sl"], "sl")).ReadOnly = true;
        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["sl"], "sl")).BorderStyle = BorderStyle.None;
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
        GetGrid(ldt);
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
        GetGrid(ldt);
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

    protected void gv_DataBound(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, e.GetType(), "gridcolor", "gv_color();", true);
    }

    [WebMethod]
    public static string GetDataByShip(string delivery_mode, string site, string ship, string domain)
    {
        string shipname = ""; string bill = ""; string curr = ""; string pr_list = ""; string taxable = ""; string taxc = "";

        if (delivery_mode == "中转库发" && site == domain)
        {
            string sql_ship = @"select a.ad_bus_relation,a.shipname,b.cm_curr,b.cm_pr_list,case b.cm_taxable when 1 then 'yes' else 'no' end cm_taxable,b.cm_taxc
                              from (select ad_bus_relation,ad_name as shipname from qad_ad_mstr where ad_domain='{0}' and ad_addr='{1}') a
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
            string sql_ship = @"select a.BusinessRelationCode,a.shipname,b.cm_curr,b.cm_pr_list,case b.cm_taxable when 1 then 'yes' else 'no' end cm_taxable,b.cm_taxc
                        from (
                            select BusinessRelationCode,right(DebtorShipToName,len(DebtorShipToName)-CHARINDEX(' ',DebtorShipToName)) shipname 
                            from form4_Customer_DebtorShipTo where IsEffective='有效' and charindex('{0}',Debtor_Domain)>0 and DebtorShipToCode='{1}'
                            ) a
                                inner join qad.dbo.qad_cm_mstr b on a.BusinessRelationCode=b.cm_addr and b.cm_domain='{0}'";
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
            }
        }

        string result = "[{\"shipname\":\"" + shipname + "\",\"bill\":\"" + bill + "\",\"curr\":\"" + curr + "\",\"pr_list\":\"" + pr_list + "\",\"taxable\":\"" + taxable + "\",\"taxc\":\"" + taxc + "\"}]";
        return result;

    }

    [WebMethod]
    public static string CheckData(string applyid, string formno, string part, string domain, string cust_part, string typeno)
    {
        string manager_flag = ""; string zg_id = "";
        CheckData_manager(applyid, out manager_flag, out zg_id);

        string part_flag = CheckVer_data(part, domain, cust_part, typeno, formno);

        string result = "[{\"manager_flag\":\"" + manager_flag + "\",\"part_flag\":\"" + part_flag + "\"}]";
        return result;

    }

    public static void CheckData_manager(string applyid, out string manager_flag, out string zg_id)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        manager_flag = "";

        DataTable dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + applyid + "')").Tables[0];
        zg_id = dt_manager.Rows[0]["zg_id"].ToString();

        if (zg_id == "")
        {
            manager_flag += "工程师(" + applyid + ")的直属主管不存在，不能提交!<br />";
        }
    }

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
                                    , string pr_list, string modelyr, string nbr,string delivery_mode
                                    , string line, string index)
    {
        string flag = ""; 

        string sql = @"exec Report_CS_CheckData '{0}','{1}','{2}','{3}','{4}'";
        sql = string.Format(sql, part, domain, cust_part, formno, typeno);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        if (dt.Rows[0][0].ToString() == "Y1") //若是中转库发，且 发货自等于域的话，发货至必须存在在地点表里    
        {
            flag = "第" + index + "行【发货至】" + ship + "，地点不存在，不能申请!<br />";
        }
        if (dt.Rows[0][0].ToString() == "Y2")//模型年的check:相同的 客户物料号，发货自，发货至，不同的PGI零件号，必须要有模型年，否则导入不进去qad          
        {
            flag = "第" + index + "行【申请工厂】" + domain + "【客户物料号】" + cust_part + "【发货自】" + site + "【发货至】" + ship + "【模型年】" + modelyr + "必须唯一!<br />";
        }
        if (dt.Rows[0][0].ToString() == "Y3")//销售订单，发货自，发货至，票据开往，物料号，客户项目号，模型年 必须唯一
        {
            flag = "第" + index + "行【申请工厂】" + domain  + "【发货自】" + site + "【发货至】" + ship + "【销售订单】" + nbr 
                + "【票据开往】" + bill + "【物料号】" + part + "【客户物料号】" + cust_part + "【模型年】" + modelyr + "必须唯一!<br />";
        }

        string result = "[{\"flag\":\"" + flag + "\"}]";
        return result;

    }

    private bool SaveData(string action)
    {
        bool bflag = false;

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = GetControlValue("PGI_CustomerSchedule_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        string applyid = ApplyId.Text; string applyname = ApplyName.Text;
        string lspart = part.Text; string lsdomain = domain.Text;
        string lstypeno = typeno.Text;

        string manager_flag = ""; string zg_id = "";
        CheckData_manager(applyid, out manager_flag, out zg_id);

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
        Pgi.Auto.Common lczg_id = new Pgi.Auto.Common();
        lczg_id.Code = "zg_id";
        lczg_id.Key = "";
        lczg_id.Value = "u_" + zg_id;
        ls.Add(lczg_id);

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

        //---------------------------------------------------------表体数据，申请人步骤的时候才更新---------------------------------------------------------------------
        if (StepID.ToUpper() == "A" || StepID.ToUpper() == SQ_StepID.ToUpper())
        {
            string IsSign_HQ = "";//判定是否存在加签
            string SignEmp_id = "";
            if (action == "submit")
            {
                try
                {
                    CustomerSchedule cs = new CustomerSchedule();
                    DataTable dt_IsSign = cs.CS_IsModifyByBom(ldt, lspart, lsdomain);//, lstypeno, this.m_sid            
                    IsSign_HQ = dt_IsSign.Rows[0][0].ToString();
                    SignEmp_id = dt_IsSign.Rows[0][1].ToString();
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
        }
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

            //更新税率值
            Pgi.Auto.Common ls_taxratecode = new Pgi.Auto.Common();
            ls_taxratecode.Sql = @"update PGI_CustomerSchedule_Dtl_Form set taxc_rate=a.TaxRate
                                    from ({1}) a
                                    where PGI_CustomerSchedule_Dtl_Form.CSNo='{0}' and PGI_CustomerSchedule_Dtl_Form.taxc=a.TaxRate_Code";
            ls_taxratecode.Sql = string.Format(ls_taxratecode.Sql, m_sid, sql_TaxRate);
            ls_sum.Add(ls_taxratecode);
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

        if (ln > 0)
        {
            bflag = true;

            var titletype = lstypeno == "新增" ? "客户日程申请" : "客户日程修改";
            string title = titletype + "[" + this.m_sid + "][" + applyname + "][" + lspart + "][" + lsdomain + "]";

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
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID.ToUpper())
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
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID.ToUpper())
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