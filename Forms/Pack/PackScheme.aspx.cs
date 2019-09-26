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

public partial class Forms_Pack_PackScheme : System.Web.UI.Page
{
    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    public string SQ_StepID = "F7AEA12F-90B3-4C99-997C-FB06333F1312";
    public string UserId = "";

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
        UserId = LogUserModel.UserId;

        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            string lssql = @"select a.* from [dbo].[PGI_PackScheme_Dtl_Form] a";

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
                    //----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了
                    string sql_prd = @"select * from PGI_PackScheme_Main where formno='" + Request.QueryString["formno"] + "'";
                    DataTable dt_prd = DbHelperSQL.Query(sql_prd).Tables[0];

                    string part = dt_prd.Rows[0]["part"].ToString(); string domain = dt_prd.Rows[0]["domain"].ToString();
                    string site = dt_prd.Rows[0]["site"].ToString(); string ship = dt_prd.Rows[0]["ship"].ToString();

                    string re_sql = @"select * from PGI_PackScheme_Main_Form where isnull(iscomplete,'')='' and part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
                    DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

                    if (re_dt.Rows.Count > 0)
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship 
                            + "正在申请中，不能修改(单号:" + re_dt.Rows[0]["InstanceID"].ToString() 
                            + ",申请人:" + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + ")!");
                    }
                    else
                    {
                        string sql_head_con = @"exec Report_Pack_edit '" + part + "','" + domain + "','" + site + "','" + ship + "','" + LogUserModel.UserId + "'";
                        DataSet ds_head_con = DbHelperSQL.Query(sql_head_con);
                        DataTable ldt_head = ds_head_con.Tables[0];
                        ldt_detail = ds_head_con.Tables[1];

                        SetControlValue("PGI_PackScheme_Main_Form", "HEAD", this.Page, ldt_head.Rows[0], "ctl00$MainContent$");
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
                DataTable ldt = DbHelperSQL.Query("select * from PGI_PackScheme_Main_Form where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    SetControlValue("PGI_PackScheme_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");

                    if (StepID.ToUpper() == SQ_StepID || StepID == "A")//申请步骤是，为0的字段置空
                    {
                        if (Convert.ToDecimal(ldt.Rows[0]["ljcc_l"].ToString()) == 0) { ljcc_l.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["ljcc_w"].ToString()) == 0) { ljcc_w.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["ljcc_h"].ToString()) == 0) { ljcc_h.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["ljzl"].ToString()) == 0) { ljzl.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["nyl"].ToString()) == 0) { nyl.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["nzj"].ToString()) == 0) { nzj.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["gdsl_cp"].ToString()) == 0) { gdsl_cp.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["gdsl_bcp"].ToString()) == 0) { gdsl_bcp.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["klgx"].ToString()) == 0) { klgx.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_w"].ToString()) == 0) { bzx_w.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_sl_c"].ToString()) == 0) { bzx_sl_c.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_cs_x"].ToString()) == 0) { bzx_cs_x.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_sl_x"].ToString()) == 0) { bzx_sl_x.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_jz_x"].ToString()) == 0) { bzx_jz_x.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_mz_x"].ToString()) == 0) { bzx_mz_x.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_xs_c"].ToString()) == 0) { bzx_xs_c.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_c_t"].ToString()) == 0) { bzx_c_t.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_xs_t"].ToString()) == 0) { bzx_xs_t.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_sl_t"].ToString()) == 0) { bzx_sl_t.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_dzcs"].ToString()) == 0) { bzx_dzcs.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_jzcs"].ToString()) == 0) { bzx_jzcs.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_jz_t"].ToString()) == 0) { bzx_jz_t.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_mz_t"].ToString()) == 0) { bzx_mz_t.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_t_h"].ToString()) == 0) { bzx_t_h.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_ljfyzl"].ToString()) == 0) { bzx_ljfyzl.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_t_l"].ToString()) == 0) { bzx_t_l.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["bzx_t_w"].ToString()) == 0) { bzx_t_w.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_sj_j"].ToString()) == 0) { cbfx_sj_j.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_mb_j"].ToString()) == 0) { cbfx_mb_j.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_xs_price"].ToString()) == 0) { cbfx_xs_price.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_bc_w_total"].ToString()) == 0) { cbfx_bc_w_total.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_cb_t_total"].ToString()) == 0) { cbfx_cb_t_total.Text = ""; }
                        if (Convert.ToDecimal(ldt.Rows[0]["cbfx_cb_rate"].ToString()) == 0) { cbfx_cb_rate.Text = ""; }
                    }

                    if (Convert.ToDecimal(ldt.Rows[0]["cbfx_cb_rate"].ToString()) != 0) 
                    {
                        cbfx_cb_rate.Text = (Convert.ToSingle(ldt.Rows[0]["cbfx_cb_rate"].ToString()) * 100).ToString() + "%";
                    }

                    if (ldt.Rows[0]["files_part"].ToString() != "")
                    {
                        this.ip_filelist_db.Value = ldt.Rows[0]["files_part"].ToString();
                        bindtab();
                    }
                    if (ldt.Rows[0]["files_bzx_nb"].ToString() != "")
                    {
                        this.ip_filelist_db_2.Value = ldt.Rows[0]["files_bzx_nb"].ToString();
                        bindtab_2();
                    }
                    if (ldt.Rows[0]["files_bzx_wg"].ToString() != "")
                    {
                        this.ip_filelist_db_3.Value = ldt.Rows[0]["files_bzx_wg"].ToString();
                        bindtab_3();
                    }
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql += " where PackNo='" + this.m_sid + "' order by a.numid";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            }
            bind_grid(ldt_detail);
        }
        else
        {
            bindtab(); bindtab_2(); bindtab_3();
        }

        if (ver.Text == "A0" || ver.Text == "")
        {
            typeno.Value = "新增";
            typeno.Enabled = false;
        }


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



        Setbzlb();//绑定包装类别
        Settypeno(ver.Text);//绑定申请类别
        //Setfilestype();//绑定附件类别

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    #region 零件图片
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

    #endregion

    #region 包装箱内部

    void bindtab_2()
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

        tab1_2.Rows.Clear();
        string[] ls_files_2 = this.ip_filelist_db_2.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_files_2.Length; i++)
        {
            TableRow tempRow = new TableRow();
            string[] ls_files_oth = ls_files_2[i].Split(',');

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
                Btn.Text = "删除"; Btn.ID = "btn_nb_" + i.ToString(); Btn.Click += new EventHandler(Btn_2_Click);

                TableCell td3 = new TableCell(); td3.Controls.Add(Btn);
                tempRow.Cells.Add(td3);
            }
            tab1_2.Rows.Add(tempRow);
        }
    }

    #endregion

    #region 包装箱外观

    void bindtab_3()
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

        tab1_3.Rows.Clear();
        string[] ls_files_3 = this.ip_filelist_db_3.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_files_3.Length; i++)
        {
            TableRow tempRow = new TableRow();
            string[] ls_files_oth = ls_files_3[i].Split(',');

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
                Btn.Text = "删除"; Btn.ID = "btn_wg_" + i.ToString(); Btn.Click += new EventHandler(Btn_3_Click);

                TableCell td3 = new TableCell(); td3.Controls.Add(Btn);
                tempRow.Cells.Add(td3);
            }
            tab1_3.Rows.Add(tempRow);
        }
    }

    #endregion

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

    void Btn_2_Click(object sender, EventArgs e)
    {
        //var btn = sender as Button;
        var btn = sender as LinkButton;
        int index = Convert.ToInt32(btn.ID.Substring(7));

        string filedb = ip_filelist_db_2.Value;
        string[] ls_files = filedb.Split(';');

        string files = "";
        for (int i = 0; i < ls_files.Length; i++)
        {
            if (i != index) { files += ls_files[i] + ";"; }
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        ip_filelist_db_2.Value = files;

        bindtab_2();
    }

    void Btn_3_Click(object sender, EventArgs e)
    {
        //var btn = sender as Button;
        var btn = sender as LinkButton;
        int index = Convert.ToInt32(btn.ID.Substring(7));

        string filedb = ip_filelist_db_3.Value;
        string[] ls_files = filedb.Split(';');

        string files = "";
        for (int i = 0; i < ls_files.Length; i++)
        {
            if (i != index) { files += ls_files[i] + ";"; }
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        ip_filelist_db_3.Value = files;

        bindtab_3();
    }

    public void bind_grid(DataTable dt)
    {
        this.gv.DataSource = dt;
        this.gv.DataBind();
        setGridIsRead(dt, ver.Text);
    }

    public void setGridIsRead(DataTable ldt_detail,string ver)
    {
        //特殊处理，签核界面，明细的框框拿掉
        string lssql = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                        where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                            and instanceid='{2}' and stepname='{3}'";
        string sql_pro = string.Format(lssql, StepID, FlowID, m_sid, "包装工程师申请");
        DataTable ldt_flow_pro = DbHelperSQL.Query(sql_pro).Tables[0];

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (state == "edit" || ver != "A0")
            {
                //((TextBox)this.FindControl("ctl00$MainContent$projectno")).CssClass = "lineread";
                //((TextBox)this.FindControl("ctl00$MainContent$projectno")).Attributes.Remove("ondblclick");
                //((TextBox)this.FindControl("ctl00$MainContent$projectno")).ReadOnly = true;

                //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).CssClass = "lineread";
                //((TextBox)this.FindControl("ctl00$MainContent$pgi_no_t")).ReadOnly = true;

                //((RadioButtonList)this.FindControl("ctl00$MainContent$typeno")).Enabled = false;

                if (state != "edit" && ver != "A0")
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
            else
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
    }

    public void setread(int i)
    {
        bzlb.Enabled = false;//包装类别
        ljcc_l.Enabled = false; ljcc_w.Enabled = false; ljcc_h.Enabled = false;
        gdsl_cp.Enabled = false; gdsl_bcp.Enabled = false; //klgx.Enabled = false;
        bzx_cc.CssClass = "lineread"; bzx_cc.ReadOnly = true;//箱尺寸(L*W*H)
        bzx_sl_c.Enabled = false; bzx_cs_x.Enabled = false;
        bzx_xs_c.Enabled = false; bzx_c_t.Enabled = false;
        bzx_dzcs.Enabled = false; bzx_jzcs.Enabled = false;
        bzx_t_l.Enabled = false; bzx_t_w.Enabled = false; bzx_t_h.Enabled = false;
        cbfx_mb_j.Enabled = false;

        this.uploadcontrol.Visible = false;
        this.uploadcontrol_2.Visible = false;
        this.uploadcontrol_3.Visible = false;

        ViewState["ApplyId_i"] = "Y";

        btnadd.Visible = false; btndel.Visible = false;

        if (i == 0)
        {
            gv.Columns[gv.VisibleColumns.Count - 1].Visible = false;
            gv.Columns[0].Visible = false;
        }

        setread_grid(i);
    }
    public void setread_grid(int i)
    {
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["sl"], "sl")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["sl"], "sl")).BorderStyle = BorderStyle.None;
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
        ScriptManager.RegisterStartupScript(this, e.GetType(), "gridcolor", "gv_color();", true);//RefreshRow();
    }

    //绑定申请类别
    public void Settypeno(string ver)
    {
        typeno.Columns.Clear();
        string lssql = "";
        if (ver == "A0" || ver == "")
        {
            lssql = @"select [Code],[Name]
                        from (
	                        select '新增' [Code],'新增' [Name],0 rownum
	                        ) a
                        order by rownum";
        }
        else
        {
            lssql = @"select [Code],[Name]
                        from (
	                        select '零件信息修改' [Code],'零件信息修改' [Name],1 rownum
	                        union 
	                        select '装箱数据修改' [Code],'装箱数据修改' [Name],2 rownum
	                        union 
	                        select '包装明细修改' [Code],'包装明细修改' [Name],3 rownum
	                        ) a
                        order by rownum";
        }
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
    //    files_type.Columns.Clear();
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

    public static string CheckVer_data(string part, string domain, string site, string ship, string ver, string formno)
    {
        string flag = "";
        string sql = @"select * from PGI_PackScheme_Main_Form where isnull(iscomplete,'')='' and part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
        if (formno != "") { sql = sql + " and formno<>'" + formno + "'"; }

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            flag = "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship + "正在申请中，不能申请!<br />";
        }

        if (flag == "")
        {
            if (ver == "A0")
            {
                sql = @"select * from [dbo].[PGI_PackScheme_Main] where part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
                DataTable dt_A0 = DbHelperSQL.Query(sql).Tables[0];

                if (dt_A0.Rows.Count > 0)
                {
                    flag = "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship + "已经存在，不能新增申请!<br />";
                }
            }
            else
            {
                //获取最新版本的版本号
                sql = @"select a.part,a.domain,a.ver,b.sort 
                    from [dbo].[PGI_PackScheme_Main] a 
                        left join [dbo].[PGI_PackScheme_Ver] b on a.ver=b.ver
                    where a.b_flag=1 and a.part='" + part + "' and domain='" + domain + "' and site='" + site + "' and ship='" + ship + "'";
                DataTable dt_other = DbHelperSQL.Query(sql).Tables[0];

                if (dt_other.Rows.Count > 0)
                {
                    DataTable dt_ver = DbHelperSQL.Query("select top 1 ver,sort from [dbo].[PGI_PackScheme_Ver] where sort>"+ Convert.ToInt32(dt_other.Rows[0]["sort"].ToString())+" order by sort").Tables[0];
                    if (dt_ver.Rows[0]["ver"].ToString() != ver)
                    {
                        flag = "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship + "修改申请版本" + ver + "，不正确，不能申请!<br />";
                    }
                }
                else
                {
                    flag = "PGI_零件号" + part + "申请工厂" + domain + "发自" + site + "发至" + ship + "没有申请过，不能修改申请!<br />";
                }
            }

        }
        return flag;
    }


    [WebMethod]
    public static string CheckData(string applyid, string formno, string part, string domain, string site, string ship, string ver)
    {
        string manager_flag = ""; string zg_id = "", manager_id = "";
        CheckData_manager(applyid, out manager_flag, out zg_id, out manager_id);

        string part_flag = CheckVer_data(part, domain, site, ship, ver, formno);

        string result = "[{\"manager_flag\":\"" + manager_flag + "\",\"part_flag\":\"" + part_flag + "\"}]";
        return result;

    }

    public static void CheckData_manager(string applyid, out string manager_flag, out string zg_id, out string manager_id)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        manager_flag = ""; 

        DataTable dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + applyid + "')").Tables[0];
        zg_id = dt_manager.Rows[0]["zg_id"].ToString();
        manager_id = dt_manager.Rows[0]["manager_id"].ToString();

        if (zg_id == "")
        {
            manager_flag += "工程师(" + applyid + ")的直属主管不存在，不能提交!<br />";
        }
        if (manager_id == "")
        {
            manager_flag += "工程师(" + applyid + ")的部门经理不存在，不能提交!<br />";
        }

    }


    [WebMethod]
    public static string GetData(string part, string domain, string site, string ship,string UserId)
    {
        //string manager_flag = "";
        string sql_head_con = @"exec Report_Pack_edit '" + part + "','" + domain + "','" + site + "','" + ship + "','" + UserId + "'";
        DataSet ds_head_con = DbHelperSQL.Query(sql_head_con);
        DataTable ldt_head = ds_head_con.Tables[0];
        DataTable ldt_detail = ds_head_con.Tables[1];

        string result = ldt_head.ToJsonString(); //"[{\"manager_flag\":\"" + manager_flag + "\",\"part_flag\":\"" + part_flag + "\"}]";
        return result;

    }

    private bool SaveData()
    {
        bool bflag = false;

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = GetControlValue("PGI_PackScheme_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        string applyid = ApplyId.Text;
        string applyname = ApplyName.Text;
        string lspart = part.Text;
        string lsver = ver.Text;
        string lstypeno = typeno.Value.ToString();//typeno.Value == null ? "" : typeno.Value.ToString();
        string lsbzlb = bzlb.Value == null ? "" : bzlb.Value.ToString();//bzlb.SelectedValue;

        string manager_flag = ""; string zg_id = "", manager_id = "";
        CheckData_manager(applyid, out manager_flag, out zg_id, out manager_id);

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "WLBZFA" + System.DateTime.Now.ToString("yyMMdd");
            this.m_sid = Pgi.Auto.Public.GetNo("WLBZFA", lsid, 0, 4);

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

        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code.ToLower() == "typeno") { ls[i].Value = lstypeno; }//申请类型
            if (ls[i].Code.ToLower() == "bzlb") { ls[i].Value = lsbzlb; }//包装类别
            if (ls[i].Code.ToLower() == "cbfx_cb_rate")
            {//包装成本比列

                if (ls[i].Value == "")
                {
                    ls[i].Value = (Convert.ToDecimal("0") / 100).ToString();
                }
                else
                {
                    ls[i].Value = (Convert.ToDecimal(ls[i].Value.Left(ls[i].Value.Length - 1)) / 100).ToString();
                }
                
            }

            if (ls[i].Value == "")
            {
                if (ls[i].Code.ToLower() == "ljcc_l" || ls[i].Code.ToLower() == "ljcc_w" || ls[i].Code.ToLower() == "ljcc_h"
                    || ls[i].Code.ToLower() == "ljzl" || ls[i].Code.ToLower() == "nyl" || ls[i].Code.ToLower() == "nzj"
                    || ls[i].Code.ToLower() == "gdsl_cp" || ls[i].Code.ToLower() == "gdsl_bcp" || ls[i].Code.ToLower() == "klgx"
                    || ls[i].Code.ToLower() == "bzx_w"
                    || ls[i].Code.ToLower() == "bzx_sl_c" || ls[i].Code.ToLower() == "bzx_cs_x" || ls[i].Code.ToLower() == "bzx_sl_x"
                    || ls[i].Code.ToLower() == "bzx_jz_x" || ls[i].Code.ToLower() == "bzx_mz_x" || ls[i].Code.ToLower() == "bzx_xs_c"
                    || ls[i].Code.ToLower() == "bzx_c_t" || ls[i].Code.ToLower() == "bzx_xs_t" || ls[i].Code.ToLower() == "bzx_sl_t"
                    || ls[i].Code.ToLower() == "bzx_dzcs" || ls[i].Code.ToLower() == "bzx_jzcs" || ls[i].Code.ToLower() == "bzx_jz_t"
                    || ls[i].Code.ToLower() == "bzx_mz_t" || ls[i].Code.ToLower() == "bzx_ljfyzl" || ls[i].Code.ToLower() == "bzx_t_l"
                    || ls[i].Code.ToLower() == "bzx_t_w" || ls[i].Code.ToLower() == "bzx_t_h" || ls[i].Code.ToLower() == "cbfx_sj_j"
                    || ls[i].Code.ToLower() == "cbfx_mb_j" || ls[i].Code.ToLower() == "cbfx_xs_price" || ls[i].Code.ToLower() == "cbfx_bc_w_total"
                    || ls[i].Code.ToLower() == "cbfx_cb_t_total")
                {
                    ls[i].Value = "0";
                }
            }

        }

        //主管
        Pgi.Auto.Common lczg_id = new Pgi.Auto.Common();
        lczg_id.Code = "zg_id";
        lczg_id.Key = "";
        lczg_id.Value = "u_" + zg_id;
        ls.Add(lczg_id);

        //经理
        Pgi.Auto.Common lcmanager_id = new Pgi.Auto.Common();
        lcmanager_id.Code = "manager_id";
        lcmanager_id.Key = "";
        lcmanager_id.Value = "u_" + manager_id;
        ls.Add(lcmanager_id);

        //自定义，上传文件
        string savepath_new = @"\" + savepath + @"\";
        var despath = MapPath("~") + savepath_new + m_sid + @"\";
        if (!System.IO.Directory.Exists(despath))
        {
            System.IO.Directory.CreateDirectory(despath);
        }

        #region 零件图片

        string files_part = "";
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

            files_part += item.Replace(savepath_new, savepath_new + m_sid + @"\") + ";";
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            files_part += item + ";";
        }
        if (files_part != "") { files_part = files_part.Substring(0, files_part.Length - 1); }

        #endregion

        #region 包装箱内部

        string files_bzx_nb = "";
        string[] ls_files_2 = ip_filelist_2.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_2)
        {
            string[] ls_files_oth = item.Split(',');

            //目的地存在的话，先删除
            string tmp = despath + ls_files_oth[1].Replace(savepath_new, "");
            if (File.Exists(tmp)) { File.Delete(tmp); }

            //文件从临时目录转移到表单单号下面
            FileInfo fi = new FileInfo(MapPath("~") + ls_files_oth[1]);
            fi.MoveTo(tmp);

            files_bzx_nb += item.Replace(savepath_new, savepath_new + m_sid + @"\") + ";";
        }

        string[] ls_files_db_2 = ip_filelist_db_2.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db_2)
        {
            files_bzx_nb += item + ";";
        }
        if (files_bzx_nb != "") { files_bzx_nb = files_bzx_nb.Substring(0, files_bzx_nb.Length - 1); }

        #endregion

        #region 包装箱外观

        string files_bzx_wg = "";
        string[] ls_files_3 = ip_filelist_3.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_3)
        {
            string[] ls_files_oth = item.Split(',');

            //目的地存在的话，先删除
            string tmp = despath + ls_files_oth[1].Replace(savepath_new, "");
            if (File.Exists(tmp)) { File.Delete(tmp); }

            //文件从临时目录转移到表单单号下面
            FileInfo fi = new FileInfo(MapPath("~") + ls_files_oth[1]);
            fi.MoveTo(tmp);

            files_bzx_wg += item.Replace(savepath_new, savepath_new + m_sid + @"\") + ";";
        }

        string[] ls_files_db_3 = ip_filelist_db_3.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db_3)
        {
            files_bzx_wg += item + ";";
        }
        if (files_bzx_wg != "") { files_bzx_wg = files_bzx_wg.Substring(0, files_bzx_wg.Length - 1); }

        #endregion

        // 增加上传文件列:零件图片
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "files_part";
        lcfile.Key = "";
        lcfile.Value = files_part;
        ls.Add(lcfile);

        // 增加上传文件列:包装箱内部
        Pgi.Auto.Common lcfiles_bzx_nb = new Pgi.Auto.Common();
        lcfiles_bzx_nb.Code = "files_bzx_nb";
        lcfiles_bzx_nb.Key = "";
        lcfiles_bzx_nb.Value = files_bzx_nb;
        ls.Add(lcfiles_bzx_nb);

        // 增加上传文件列:包装箱外观
        Pgi.Auto.Common lcfiles_bzx_wg = new Pgi.Auto.Common();
        lcfiles_bzx_wg.Code = "files_bzx_wg";
        lcfiles_bzx_wg.Key = "";
        lcfiles_bzx_wg.Value = files_bzx_wg;
        ls.Add(lcfiles_bzx_wg);

        //bom 修改
        string IsModifyByBom = "N";
        if (lstypeno == "包装明细修改")
        {
            IsModifyByBom = "Y";
        }
        Pgi.Auto.Common lcIsModifyByBom = new Pgi.Auto.Common();
        lcIsModifyByBom.Code = "IsModifyByBom";
        lcIsModifyByBom.Key = "";
        lcIsModifyByBom.Value = IsModifyByBom;
        ls.Add(lcIsModifyByBom);

        //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        //主表相关字段赋值到明细表
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["PackNo"] = m_sid;
            ldt.Rows[i]["numid"] = (i + 1);
        }

        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_PackScheme_Main_Form"));


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
                ls_del.Sql = "delete from PGI_PackScheme_Dtl_Form where PackNo='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_PackScheme_Dtl_Form where PackNo='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_PackScheme_Dtl_Form", "id", "Column1,flag");
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
            bflag = true;

            var titletype = lstypeno == "新增" ? "包装方案申请" : "包装方案修改";
            string title = titletype + "[" + this.m_sid + "][" + applyname + "][" + lspart + "][" + lsver + "]";

            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";

        }
        else
        {
            bflag = false;
        }

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
                }else if (ldt.Rows[i]["control_type"].ToString() == "ASPXTEXTBOX")
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
                } else if (row["control_type"].ToString() == "ASPXTEXTBOX")
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