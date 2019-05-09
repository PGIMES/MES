using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using System.Linq;
using DevExpress.Web.ASPxTreeList;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using RoadFlow.Platform;
using System.IO;

public partial class Fin_FeiYongBX : PGIBasePage
{
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";
    public string StepName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.EnableViewState = true;
        Page.MaintainScrollPositionOnPostBack = true;
        //      WebForm.Common.DataTransfer.PaoZiLiao    测试账号   G1: 00020  G2:02045 00495 02104 W1:00350   W2:00720                      
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        Session["LogUser"] = LogUserModel;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId;
        ViewState["DepartName"] = LogUserModel.DepartName;



        #region "IsPostBack"
        if (!IsPostBack)
        {


            //文件上传在updatepanel需用此方法+enctype = "multipart/form-data" ，否则无法上传文件
            //PostBackTrigger trigger = new PostBackTrigger();
            //trigger.ControlID = btnflowSend.UniqueID;
            //UpdatePanel1.Triggers.Add(trigger);
            //PostBackTrigger trigger2 = new PostBackTrigger();
            //trigger2.ControlID = btnSave.UniqueID;
            //UpdatePanel1.Triggers.Add(trigger2);
            ViewState["dtl"] = null;
            ViewState["aplno"] = "";
            string id = Request["instanceid"];
            DataTable dtMst = new DataTable();
            if (LogUserModel != null)
            {
                //当前登陆人员
                txt_LogUserId.Value = LogUserModel.UserId;
                txt_LogUserName.Value = LogUserModel.UserName;
                // txt_LogUserJob.Value = LogUserModel.JobTitleName;
                txt_LogUserDept.Value = LogUserModel.DepartName;
                aplid.Attributes.Add("onclick", "selectemp();");

                if (Request["instanceid"] != null)//页面加载
                {

                }
                else//新建
                {
                    createdate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    createid.Text = LogUserModel.UserId;
                    createname.Text = LogUserModel.UserName;
                    apldomain.Text = (Request["domain"] == null ? "" : Request["domain"].ToString());
                    apldept.Text = LogUserModel.DepartName;
                    aplphone.Text = LogUserModel.Telephone;

                    aplid.Text = LogUserModel.UserId;
                    aplname.Text = LogUserModel.UserName;
                    apldept.Text = LogUserModel.DepartName;
                    apljob.Text = LogUserModel.JobTitleName;
                    apldomain.Text = LogUserModel.Domain;

                    //var dtEmpinfo = DbHelperSQL.Query(" SELECT  [ITEMVALUE] costcenter   ,[deptcode]  FROM [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] where employeeid='" + LogUserModel.UserId + "'");
                    //costcenter.Text = "";
                    //deptcode.Text = "";

                    txt_LogUserId.Value = LogUserModel.UserId;
                    txt_LogUserName.Value = LogUserModel.UserName;

                    //获取直属主管
                    deptm.Text = Fin_FeiYongBX.getDeptLeaderByDept("", LogUserModel.UserId);

                }

            }


            ViewState["aplno"] = Request["instanceid"] == null ? "" : Request["instanceid"].ToString();


            ////--==第1步：Get instance Data=======================================================================================================      
            if (id != "" && id != null)
            {
                ViewState["aplno"] = id == null ? "" : id;
                aplno.Text = ViewState["aplno"].ToString();
                dtMst = DbHelperSQL.Query("select * from Fin_FeiYongBX_main_form where aplno='" + id.ToString() + "' ").Tables[0];
            }
            if (dtMst.Rows.Count > 0)
            {
                aplno.Text = dtMst.Rows[0]["aplno"].ToString();
                createid.Text = dtMst.Rows[0]["createid"].ToString();
                createname.Text = dtMst.Rows[0]["createname"].ToString();
                createdate.Text = dtMst.Rows[0]["createdate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
                aplid.Text = dtMst.Rows[0]["aplid"].ToString();
                aplname.Text = dtMst.Rows[0]["aplname"].ToString();
                apljob.Text = dtMst.Rows[0]["apljob"].ToString();
                apldept.Text = dtMst.Rows[0]["apldept"].ToString();
                aplphone.Text = dtMst.Rows[0]["aplphone"].ToString();
                apldomain.Text = dtMst.Rows[0]["apldomain"].ToString();
                costcenter.Text = dtMst.Rows[0]["costcenter"].ToString();
                deptcode.Text = dtMst.Rows[0]["deptcode"].ToString();
                totalfee.Text = dtMst.Rows[0]["totalfee"].ToString();

                supervisor.Text = dtMst.Rows[0]["supervisor"].ToString();
                deptm.Text = dtMst.Rows[0]["deptm"].ToString();
                deptmfg.Text = dtMst.Rows[0]["deptmfg"].ToString();
                chuna.Value = dtMst.Rows[0]["chuna"].ToString();
                if (dtMst.Rows[0]["files"].ToString() != "")
                {
                    this.ip_filelist_db.Value = dtMst.Rows[0]["files"].ToString();
                    bindtab();
                }
            }
            // show grid data
            bindData();
            ShowHZ();
        }
        else
        {
            bindtab();
        }

        #endregion



        //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
        string FlowID = Request.QueryString["flowid"];
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
        StepName = BWorkFlow.GetStepName(StepID.ToGuid(), FlowID.ToGuid());
        ViewState["fieldStatus"] = fieldStatus;

    }



    void bindData(bool reload = false)
    {
        var aplno = ViewState["aplno"] as string;
        DataTable dtDtl = null;

        if (reload == true) { ViewState["dtl"] = null; }
        if (ViewState["dtl"] == null)
        {
            var sqlDtl = "select  id2, d.aplno, rowno, costcateid, costcatename,instanceid, budgetsour, feedate, feenote, limit, amount "
                        + " from fin_FeiyongBX_dtl_form d join fin_FeiyongBX_main_form m on d.aplno=m.aplno   where   d.aplno = '{0}' order by rowno ";

            dtDtl = DbHelperSQL.Query(string.Format(sqlDtl, aplno)).Tables[0];
            dtDtl.PrimaryKey = new DataColumn[] { dtDtl.Columns["id2"] };
            //如果没记录，则增加空行 5 行
            if (dtDtl.Rows.Count == 0)
            {
                for (int row = 0; row < 5; row++)
                {
                    var dr = dtDtl.NewRow();
                    dr["id2"] = Guid.NewGuid();
                    dr["rowno"] = GetMaxRowno(dtDtl, "rowno");
                    dtDtl.Rows.Add(dr);
                }
            }

            ViewState["dtl"] = dtDtl;
        }

        dtDtl = ViewState["dtl"] as DataTable;

        grid.DataSource = dtDtl;
        grid.DataBind();

    }

    void bindtab()
    {
        bool is_del = true;
        DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" 
            + Request.QueryString["stepid"] + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + Request.QueryString["flowid"] 
            + "' as varchar(36)) and instanceid='" + ViewState["aplno"] + "' and stepname='申请人'").Tables[0];

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

                hl.Text = "文件浏览";
                hl.NavigateUrl = ls_files_2[0].ToString();
                hl.Target = "_blank";

                lb.Text = "";
            }

            TableCell td1 = new TableCell(); td1.Controls.Add(hl); td1.Width = Unit.Pixel(400);
            tempRow.Cells.Add(td1);

            TableCell td2 = new TableCell(); td2.Controls.Add(lb); td2.Width = Unit.Pixel(60);
            tempRow.Cells.Add(td2);
            td2.CssClass = "hidden";

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


    public void loadControl(string formtype)
    {
        //--== 第一步:装载控件========================================================================================================
        //==Detail
        if (ViewState["dtl"] == null)
        {
            //var dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where prno='" + ViewState["PRNo"].ToString() + "' order by rowid asc").Tables[0];
            //ViewState["dtl"] = dtl;
        }
        //  gvdtl.Columns.Clear();
        var mode = Request["mode"] == null ? "" : "_" + Request["mode"].ToString();
        //  Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form"+mode, "dtl", this.gvdtl, ViewState["dtl"] as DataTable,2);

    }


    #region "WebMethod"  
    /// <summary>
    /// 获取时间区间
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="costcateid"></param>
    /// <param name="aplid"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getFindate(string domain, string costcateid, string aplid)
    {
        string result = "";
        var sql = string.Format("exec fin_getDate '{0}','{1}','{2}' ", domain, costcateid, aplid);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
    /// <summary>
    /// 机票费，火车票如果人事预定则不可报销
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="costcateid"></param>
    /// <param name="aplid"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string ValidTicket(string aplno, string costcateid)
    {
        string result = "";
        var sql = string.Format(" select count(1) from Fin_T_Dtl_hr_Form where FIN_T_No='{0}' and (case when Vehicle='飞机' then 'T001' when Vehicle='火车' then 'T002' end)='{1}'  ", aplno, costcateid);
        var value = DbHelperSQL.GetSingle(sql);
        if (value != null)
        { result = value.ToString(); }
        return result;
    }
    /// <summary>
    /// 获取人员成本中心，部门代码
    /// </summary>
    /// <param name="aplid"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getEmpCost(string empid)
    {
        string result = "";
        var sql = string.Format("SELECT  [ITEMVALUE] costcenter   ,[deptcode]  FROM [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] where employeeid='{0}'", empid);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }


    [System.Web.Services.WebMethod()]
    public static string getLimit(string domain, string costcateid, string aplid, string feedate, string aplno = "")
    {
        string result = "";
        var sql = string.Format("exec fin_getLimit '{0}','{1}','{2}','{3}','{4}' ", domain, costcateid, aplid, aplno, feedate);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.Rows[0][0].ToString(); }
        return result;
    }
    [System.Web.Services.WebMethod()]
    public static string GetDeptByDomain(string P1)
    {
        string result = "";
        var sql = string.Format(" select distinct dept_name as value,dept_name as text from  HR_EMP_MES   where domain='{0}' or gc='{1}' ", P1, P1);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
    [System.Web.Services.WebMethod()]
    public static string DelFile(string P1, string P2)
    {
        string result = "";
       //// var sql = string.Format(" update   PUR_pr_main_Form set files=replace(files,'{0}','')  where prno='{1}' ", P1, P2);
       // var value = DbHelperSQL.ExecuteSql(sql);
       // if (value > 0)
       // { result = value.ToString(); }
        return result;
    }
    /// <summary>
    /// 根据部门取部门主管
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getDeptLeaderByDept(string domain, string dept)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[HRM_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "'  or workcode='"+dept+"' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取直属主管
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getSupervisorByEmp(string domain, string emp)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct zg_workcode FROM [dbo].[HR_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (workcode='" + emp + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 根据人员取分管领导
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getChargeLeaderByUser(string userid)
    {
        // RoadFlow.Platform.Users users = new Users();
        // var M = users.GetByAccount(userid);
        // string obj = users.GetChargeLeader(M.ID);
        string obj = "";
        if ("财务二部，采购二部,副总经理室,人事二部,IT部,销售二部,总经理室,质量二部".IndexOf(userid) >= 0)
        {//徐总
            obj = "u_1459D7BC-C19C-41AD-8221-191600BF14B6";
        }
        else if("工程二部,工程三部,工程四部,技术副总经理室,项目管理部,压铸技术部,".IndexOf(userid) >= 0)
        { //施总
            obj = "u_49F04DEE-3969-46E5-94EF-A98000C6A917";
        }
        else if ("设备二部,生产三部（压铸）,生产四部,物流二部,运营副总经理室1".IndexOf(userid) >= 0)
        {//沈总
            obj = "u_AE087A29-F74D-469E-A5D6-24CDFCC0C7EC";
        }
        else if("财务一部,人事一部,设备一部,生产一部,物流一部,质量一部,工程一部,生产二部,".IndexOf(userid) >= 0)
        { //汤总
            obj = "u_66690388-4673-4A7E-A619-8361B5E3278F";
        }
        
                
        return obj == null ? "" : obj.ToString();
    }

    #endregion

    public bool Check()
    {
        bool result = true;

        // if()

        return result;
    }
    /// <summary>
    /// 保存Data
    /// </summary>
    /// <param name="formtype">表单对应的设定</param>
    /// <param name="flag">输出参数是否保存成功</param>
    protected void SaveData(string formtype, out bool flag)
    {   //保存数据是否成功标识
        flag = true;
        //验证
        Check();
        updateGridData();
        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        string instanceid = "0";
        var straplno = "";
        try
        {
            straplno = "";// ViewState["aplno"].ToString();
            //产生申请单号
            if (ViewState["aplno"].ToString() == "")
            {
                straplno = GetDanHao("OES", "aplno", "fin_feiyongBX_main_form");
                aplno.Text = straplno;
                ViewState["aplno"] = straplno;
            }
            else
            {
                straplno = ViewState["aplno"].ToString();
                aplno.Text = ViewState["aplno"].ToString();
            }

            # region Main_form  & Dtl_Form             
            List<CommandInfo> cmdlist = new List<CommandInfo>();
            //Main_form
            CommandInfo cmdmain = new CommandInfo();
            if (DbHelperSQL.Exists("select count(1) from fin_feiyongBX_main_form where aplno='" + straplno + "'") == true)
            {
                cmdmain.CommandText = "update Fin_FeiYongBX_main_form set aplno=@aplno, createdate=@createdate, createid=@createid, createname=@createname, createdept=@createdept, aplid=@aplid, aplname=@aplname, apldept=@apldept"
                    + ", apljob=@apljob, aplphone=@aplphone, apldomain=@apldomain, totalfee=@totalfee, supervisor=@supervisor, deptm=@deptm, deptmfg=@deptmfg, onlyflag=@onlyflag ,costcenter=@costcenter,deptcode=@deptcode,chuna=@chuna   where  aplno=@aplno ";
            }
            else
            {
                cmdmain.CommandText = "insert into fin_feiyongBX_main_form( aplno, createdate, createid, createname, createdept, aplid, aplname, apldept, apljob, aplphone, apldomain, totalfee, supervisor, deptm, deptmfg,  onlyflag, costcenter, deptcode,chuna)values"
                                                                       + "(@aplno, @createdate,@createid,@createname,@createdept,@aplid,@aplname,@apldept,@apljob,@aplphone,@apldomain,@totalfee,@supervisor,@deptm,@deptmfg,@onlyflag,@costcenter,@deptcode,@chuna )";
            }
            SqlParameter[] parammain = {
                          new SqlParameter() { ParameterName="@aplno",  SqlDbType=SqlDbType.VarChar, Value= straplno },
                          new SqlParameter() { ParameterName="@createdate",  SqlDbType=SqlDbType.VarChar, Value= Request["createdate"] },
                          new SqlParameter() { ParameterName="@createid",  SqlDbType=SqlDbType.VarChar, Value=  Request["createid"] },
                          new SqlParameter() { ParameterName="@createname",  SqlDbType=SqlDbType.VarChar, Value=  Request["createname"] },
                          new SqlParameter() { ParameterName="@createdept",  SqlDbType=SqlDbType.VarChar, Value=""  },//createdept.Text
                          new SqlParameter() { ParameterName="@aplid",  SqlDbType=SqlDbType.VarChar, Value=  Request["aplid"] },
                          new SqlParameter() { ParameterName="@aplname",  SqlDbType=SqlDbType.VarChar, Value=  Request["aplname"] },
                          new SqlParameter() { ParameterName="@apldept",  SqlDbType=SqlDbType.VarChar, Value=  Request["apldept"] },
                          new SqlParameter() { ParameterName="@apljob",  SqlDbType=SqlDbType.VarChar, Value=  Request["apljob"] },
                          new SqlParameter() { ParameterName="@aplphone",  SqlDbType=SqlDbType.VarChar, Value=  Request["aplphone"] },
                          new SqlParameter() { ParameterName="@apldomain",  SqlDbType=SqlDbType.VarChar, Value=  Request["apldomain"] },
                          new SqlParameter() { ParameterName="@totalfee",  SqlDbType=SqlDbType.VarChar, Value=  Request["totalfee"] },
                          new SqlParameter() { ParameterName="@supervisor",  SqlDbType=SqlDbType.VarChar, Value=  Request["supervisor"] },
                          new SqlParameter() { ParameterName="@deptm",  SqlDbType=SqlDbType.VarChar, Value=  Request["deptm"] },
                          new SqlParameter() { ParameterName="@deptmfg",  SqlDbType=SqlDbType.VarChar, Value=  Request["deptmfg"] },
                          new SqlParameter() { ParameterName="@onlyflag",  SqlDbType=SqlDbType.VarChar, Value=  onlyflag.Text },
                          new SqlParameter() { ParameterName="@costcenter",  SqlDbType=SqlDbType.VarChar, Value=  Request["costcenter"] },
                          new SqlParameter() { ParameterName="@deptcode",  SqlDbType=SqlDbType.VarChar, Value=  deptcode.Text },
                          new SqlParameter() { ParameterName="@chuna",  SqlDbType=SqlDbType.VarChar, Value=  chuna.Value },
                    };
            cmdmain.Parameters = parammain;
            cmdlist.Add(cmdmain);
            //Dtl_Form
            DataTable dt = ViewState["dtl"] as DataTable;

            DataTable cdt = dt.GetChanges();
            if (cdt != null)
            {
                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    DataRow dr = cdt.Rows[i];
                    if (dr.RowState == DataRowState.Deleted)
                    {   //Console.WriteLine("删除的行索引{0}，原来数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Original]);                     
                        CommandInfo cmd = new CommandInfo();
                        cmd.CommandText = "delete from Fin_feiyongBX_dtl_form where id2=@id2";
                        SqlParameter[] listparam = {
                              new SqlParameter() { ParameterName="@id2",  SqlDbType=SqlDbType.UniqueIdentifier, Value= dr["id2",DataRowVersion.Original] },
                        };
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {   //Console.WriteLine("修改的行索引{0}，原来数值是{1}，现在的新数值{2}", i, cdt.Rows[i][0, DataRowVersion.Original], cdt.Rows[i][0, DataRowVersion.Current]);
                        CommandInfo cmd = new CommandInfo();
                        cmd.CommandText = "update Fin_feiyongBX_dtl_form set    aplno=@aplno, rowno=@rowno, costcateid=@costcateid, costcatename=@costcatename," +
                            "instanceid=@instanceid, budgetsour=@budgetsour, feedate=@feedate, feenote=@feenote, limit=@limit, amount=@amount where id2=@id2";
                        SqlParameter[] listparam = new SqlParameter[] {
                            new SqlParameter() {  ParameterName="@id2", SqlDbType=SqlDbType.UniqueIdentifier, Value=dr["id2",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@aplno", Value=straplno },
                            new SqlParameter() {  ParameterName="@rowno", Value=dr["rowno",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@costcateid", Value=dr["costcateid",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@costcatename", Value=dr["costcatename",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@instanceid", Value=dr["instanceid",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@budgetsour", Value=dr["budgetsour",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@feedate", Value=dr["feedate",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@feenote", Value=dr["feenote",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@limit", Value=dr["limit",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@amount", Value=dr["amount",DataRowVersion.Current] },

                        };
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);
                    }
                    else if (dr.RowState == DataRowState.Added && dr["costcateid", DataRowVersion.Current].ToString()!="")
                    {   // Console.WriteLine("新添加行索引{0}，数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Current]);
                        CommandInfo cmd = new CommandInfo();
                        cmd.CommandText = "insert into fin_feiyongBX_dtl_form(id2, aplno, rowno, costcateid, costcatename, instanceid, budgetsour, feedate, feenote, limit, amount)"
                                                                 + " values( @id2,@aplno,@rowno,@costcateid,@costcatename,@instanceid,@budgetsour,@feedate,@feenote,@limit,@amount)";
                        SqlParameter[] listparam = new SqlParameter[] {
                            new SqlParameter() {  ParameterName="@id2", SqlDbType=SqlDbType.UniqueIdentifier,Value=dr["id2",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@aplno", Value=straplno },
                            new SqlParameter() {  ParameterName="@rowno", Value=dr["rowno",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@costcateid", Value=dr["costcateid",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@costcatename", Value=dr["costcatename",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@instanceid", Value=dr["instanceid",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@budgetsour", Value=dr["budgetsour",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@feedate", Value=dr["feedate",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@feenote", Value=dr["feenote",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@limit", Value=dr["limit",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@amount", Value=dr["amount",DataRowVersion.Current] },

                        };
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);
                    }
                }
            }
            #endregion

            //批处理 
            DbHelperSQL.ExecuteSqlTran(cmdlist);
            dt.AcceptChanges();
            script += "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + straplno + "');$('#aplno').val('" + straplno + "');};";
            //Validate 是否重复
            //var sql = "select costcateid,feedate from Fin_FeiYongBX_dtl_form where aplno='" + straplno + "' and costcateid in('P001','P002')	group by costcateid,feedate having count(1)>1";
            //var dtValid = DbHelperSQL.Query(sql).Tables[0];
            //if (dtValid.Rows.Count > 0)
            //{
            //    script += "layer.alert('"+ dtValid.Rows[0][0].ToString() + "费用区间重复.');return false;";
            //}
        }
        catch (Exception e)
        {
            string err = e.Message.Replace("'", "").Replace("\r\n", "").Replace("nvarchar", "<字符串>").Replace("varchar", "<字符串>").Replace("numeric", "<数字>").Replace("int", "<数字>");
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + "layer.alert('保存表单数据失败，请确认。ErrorMessage:" + err + "');", true);
            flag = false;
            return;
        }
        finally
        {

        }

        //如果是签核或修改 取传递过来instanceid值
        if ((ViewState["aplno"] != null && ViewState["aplno"].ToString() != "") || Request.Form["txtInstanceID"] != "")
        {
            instanceid = Request.Form["txtInstanceID"] == "" ? (Request["instanceid"]) : (txtInstanceID.Text);
        }
        //////Save file
        ////var fileup = (FileUpload)this.FindControl("file");
        ////var filepath = "";
        ////if (fileup != null)
        ////{ if (fileup.HasFile)
        ////    {
        ////        var filename = fileup.FileName;
        ////        SaveFile(fileup, straplno, out filepath, filename, filename);
        ////        //更新文件目录
        ////        string sqlupdatefilecolum = string.Format("update fin_feiyongbx_main_form set files='{0}' where prno='{1}'", filepath, straplno);
        ////        DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
        ////        flag = true;
        ////    }
        ////}


        //===========================

        string filepath = "";//string filepath=this.UploadFiles(this.uploadcontrol);
        string[] ls_files = ip_filelist.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_2 = item.Split(',');
            if (ls_files_2.Length == 3)//挪动路径到po单号下面
            {
                FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

                var sorpath = @"\UploadFile\FIN_BX\";
                var despath = MapPath("~") + sorpath + @"\" + straplno + @"\";
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

                filepath += item.Replace(@"\UploadFile\FIN_BX\", @"\UploadFile\FIN_BX\" + straplno + @"\") + ";";
            }
            else
            {
                filepath += item + ";";
            }
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            filepath += item + ";";
        }
        if (filepath != "") { filepath = filepath.Substring(0, filepath.Length - 1); }

        string sqlupdatefilecolum = string.Format("update fin_feiyongbx_main_form set files='{0}' where aplno='{1}'", filepath, straplno);
        DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
        flag = true;
        //==============================





        //执行流程相关事宜
        if (instanceid != "0" && instanceid != "")//0 影响行数; "" 没有prno
        {
            var titletype = "费用报销申请";
            string title = titletype + "[" + straplno + "][" + aplname.Text + "]"; //设定表单标题

            //将实例id,表单标题给流程script
            script += "$('#instanceid',parent.document).val('" + straplno.ToString() + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');" +
                 "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + straplno.ToString() + "');}";

        }
        else
        {
            flag = false;
        }
    }
    //行编辑更新
    protected void updateGridData()
    {
        var dtl = ViewState["dtl"] as DataTable;
        int count = 0;
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            var dr = dtl.Rows.Find((grid.Rows[i].FindControl("id2") as Label).Text);
            dr["costcateid"] = Request[(grid.Rows[i].FindControl("costcateid") as TextBox).UniqueID];
            dr["costcatename"] = Request[(grid.Rows[i].FindControl("costcatename") as TextBox).UniqueID]; 
            dr["instanceid"] = Request[(grid.Rows[i].FindControl("instanceid") as TextBox).UniqueID]; 
            dr["budgetsour"] = Request[(grid.Rows[i].FindControl("budgetsour") as TextBox).UniqueID];
            dr["feedate"] = Request[(grid.Rows[i].FindControl("feedate") as TextBox).UniqueID];
            dr["feenote"] = Request[(grid.Rows[i].FindControl("feenote") as TextBox).UniqueID];
            dr["limit"] = Request[(grid.Rows[i].FindControl("limit") as TextBox).UniqueID].Replace(",","");
            var _amount = (grid.Rows[i].FindControl("amount") as TextBox).Text == "" ? "0" : (grid.Rows[i].FindControl("amount") as TextBox).Text.Replace(",", "");
            if ((grid.Rows[i].FindControl("amount") as TextBox).Text == "")
            {
                dr["amount"] = DBNull.Value;
            }
            else
            {
                dr["amount"] = _amount;
            }
            //判断是否只有日常报销 且 在预算范围内
            if (("P001,P002".IndexOf(dr["costcateid"].ToString()) >= 0 && Convert.ToSingle(dr["limit"].ToString() == "" ? "0" : dr["limit"]) >= Convert.ToSingle(_amount)))
            {
                count = count + 1;//统计日常报销 且 在预算范围内行数
            }
        }
        if (count == grid.Rows.Count)
        {
            onlyflag.Text = "1";//1:标识日常报销 且 在预算范围内
        }
        else
            onlyflag.Text = "0";//0:标识含非日常报销（手机，油费）以外费用
        ;
    }
    //show汇总
    protected void ShowHZ()
    {
        var dtl = ViewState["dtl"] as DataTable;
        DataTable dthz = dtl.Clone();
        DataTable cdt = dtl.GetChanges();
        if (cdt != null)
        {
            for (int i = 0; i < cdt.Rows.Count; i++)
            {
                DataRow dr = cdt.Rows[i];
                if (dr.RowState != DataRowState.Deleted)
                {
                    DataRow drNew = dthz.NewRow();
                    drNew.ItemArray = dr.ItemArray;
                    dthz.Rows.Add(drNew);
                }
            }
        }
        else
        {
            dthz = dtl;
        }


        SQLHelper SQLHelper = new SQLHelper();
        SqlParameter[] param = new SqlParameter[]
        {
           new SqlParameter("@tmptable",dthz),
        };
        var s = SQLHelper.GetDataTable("Fin_FeiYongValid", param);

        HZgrid.DataSource = s;
        HZgrid.DataBind();

        if (HZgrid.Rows.Count > 0)
        {
            MergeRow(HZgrid, 0, 1, HZgrid.Rows.Count - 1);
        };
        HZgrid.Visible = (HZgrid.Rows.Count > 0);
    }
    //show 未报销
    protected void ShowHZ_Un()
    {
        var s = DbHelperSQL.Query("exec [fin_get_Unreimbursed] '" + Request[this.aplid.UniqueID] + "'");

        HZ_UnGrid.DataSource = s;
        HZ_UnGrid.DataBind();
        int[] cols = { 0 };
        if (HZ_UnGrid.Rows.Count > 0)
        {
            MergGridRow.MergeRow(HZ_UnGrid, cols);
        };
       // HZ_UnGrid.Visible = (HZ_UnGrid.Rows.Count > 0);
    }
    private static void MergeRow(GridView gv, int currentCol, int startRow, int endRow)
    {
        GridViewRow prevRow = gv.Rows[0];
        prevRow.Cells[currentCol].RowSpan = 1;
        var pretext = "...."; 
        for (int rowIndex = 0; rowIndex <= endRow; rowIndex++)
        {
            GridViewRow currentRow = gv.Rows[rowIndex];
            
            if ((currentRow.Cells[currentCol].FindControl("budgetsour") as HyperLink).Text == pretext)
            {
                prevRow.Cells[currentCol].RowSpan = prevRow.Cells[currentCol].RowSpan <= 1 ? 2 : prevRow.Cells[currentCol].RowSpan + 1;
                currentRow.Cells[currentCol].Visible = false;
                
            }
            else
            {
                prevRow = gv.Rows[rowIndex];
                pretext =(prevRow.Cells[currentCol].FindControl("budgetsour") as HyperLink).Text;
                

            }
        }
    }

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        SaveData("",out flag);

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
        SaveData("",out flag);
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
    public static string savepath = "UploadFile\\FIN_BX";
    public void SaveFile(FileUpload fileupload,string subpath,out string filepath,string oldName,string newName )
    {
        var path = MapPath("~") +  savepath + "\\" + subpath;
        filepath = "";
        //Create directory
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        //save file
        var filename = "";
        if (fileupload.HasFile)
        {
            var list = fileupload.PostedFiles;
            foreach (var item in list)
            {
                filename = item.FileName ;
                 
                var Svrpath = path  +"\\" + filename;            
                item.SaveAs(Svrpath.Replace("&", "_").TrimStart(' '));

                filepath  = filepath + "\\" + savepath + "\\" + subpath + "\\" + filename.Replace("&", "_").TrimStart(' ')+"|";
            }
                      
        }
        //return save path
        filepath = filepath.TrimEnd('|');
        //filepath ="\\"+ savepath + "\\" + subpath+ "\\"+filename.Replace("&", "_").TrimStart(' ');
    }

    public void ShowFile(string filestring,char splitchar)
    {
        var arrFile = filestring.Split(splitchar);
        foreach(string file in arrFile)
        {
            if (file!="") { 
                var filename = file.Substring(file.LastIndexOf(@"\")+1);
                HyperLink link = new HyperLink() {
                    ID = "lnk_" + file,
                    NavigateUrl = file,
                    Text = filename,
                    Target = "_blank"
                
                };
                link.Attributes.Add("style", "padding-left:10px;padding-right:5px");

                Image btndel = new Image()
                {
                    ID = "imgbtn_" + file,                  
                    AlternateText = "删除"         
                };
                btndel.Attributes.Add("file",file);
                btndel.Attributes.Add("onclick", "delfile(this)");

                filecontainer.Controls.AddAt(0,link);
              //  filecontainer.Controls.AddAt(1, btndel);
            }
        }
        if (filestring == "") {
            Label lbl = new Label() {ID="lblnofile", Text = "无附件" };
            filecontainer.Controls.AddAt(0,lbl );
            
        }
    }
    #endregion

  
    protected void HZgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if((e.Row.DataItem as System.Data.DataRowView)["costcatename"].ToString() == "合计")
            {
                e.Row.Style.Value=("background-color:silver");
                e.Row.Font.Bold = true;
            }
            //超支亮红色
            if (Convert.ToSingle((e.Row.DataItem as System.Data.DataRowView)["chayi"])>0)
            {
                e.Row.ForeColor=System.Drawing.Color.Tomato;
                //e.Row.Font.Bold = true;
            }
        }

    }
     
    
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        updateGridData();
        var dtl = ViewState["dtl"] as DataTable;
        
        if (e.CommandName.ToLower() == "add")
        {                            
            object maxObject;
            maxObject = dtl.Compute("max(rowno)", "");
            if (maxObject.ToString() == "") { maxObject = 0; }              
            var dr = dtl.NewRow();             
            dr["id2"] = Guid.NewGuid();
            dr["rowno"] = (Convert.ToInt16(maxObject) + 1).ToString();
            dtl.Rows.Add(dr);           
            //ViewState["dtl"] = dtl;
        }
        else if (e.CommandName.ToLower() == "del")
        {   var _rowindex= ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent)).RowIndex;
            var _id = (grid.Rows[_rowindex].FindControl("id2") as Label).Text;
          //  dtl.Select("id2='"+_id+"'").
            dtl.Rows.Find(_id).Delete();
            
        }

        bindData(false);
        ShowHZ();
    }
    protected int GetMaxRowno(DataTable dt,string column)
    {
        object maxObject;
        maxObject = dt.Compute("max("+ column + ")", "");
        if (maxObject.ToString() == "")
        {
            return 1;
        }
        else
            return Convert.ToInt16(maxObject) + 1;
    }
    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType ==DataControlRowType.DataRow)
        {
            var limit = (e.Row.FindControl("limit") as TextBox).Text;
            var amount =(e.Row.FindControl("amount") as TextBox).Text;
            
            if (limit!=""&& amount!="" && Convert.ToDecimal(limit) < Convert.ToDecimal(amount))
            {
                (e.Row.FindControl("amount") as TextBox).ForeColor = System.Drawing.Color.Red;
            }
            var budgetsour = (e.Row.FindControl("budgetsour") as TextBox).Text;
            if (budgetsour=="")
            {
                (e.Row.FindControl("lnkbudgetsour") as HyperLink).Style.Add("display", "none");
            }
        }
    }
    //刷新grid数据
    protected void refresh_Click(object sender, EventArgs e)
    {
        updateGridData();
        bindData(false);
        ShowHZ();
    }
    //带入私车公用单下所有明细入grid
    protected void btnCar_Click(object sender, EventArgs e)
    {
        updateGridData();
        var fin_ca_no = txtFin_CA_No.Value;
        string sql = "select  '3f8de2dd-9229-4517-90a6-c13cb10a5c07' as flowno, FormNo 申请单号, ApplyDate 申请日期,ApplyName 申请人, ApplyDomainName 申请工厂,ApplyDeptName 申请部门, TravelRoute 地点, Remark 事由,Mileage 总预算,startdatetime "
                    + " from Fin_CA_Main_Form m join Fin_CA_Dtl_Form d on m.FormNo=d.FIN_CA_No where m.FormNo='"+ fin_ca_no + "'";
        DataTable dtCar = DbHelperSQL.Query(sql).Tables[0];
        if (dtCar.Rows.Count > 0)
        {
            var dtl = ViewState["dtl"] as DataTable;
            //判断现有空行数和私车行数，少了补齐行，不足略过
            var drs = dtl.Select(" isnull(costcateid ,'')='' or instanceid='"+ fin_ca_no + "' ","rowno asc");
            //添加不足行
            for(int i = 0; i < dtCar.Rows.Count-drs.Length ; i++)
            {
                var drNew = dtl.NewRow();
                drNew["id2"] = Guid.NewGuid();
                drNew["rowno"] = GetMaxRowno(dtl, "rowno");
                dtl.Rows.Add(drNew);
            }

            //从后 清除多余T009行记录，否则会乱，重复
            var rowscnt = drs.Length;
            for (int i = rowscnt - 1; i >= rowscnt - dtCar.Rows.Count-1  ; i--)
            {                
              //  drs[i].Delete();
            }
            //

            drs = dtl.Select("isnull(costcateid ,'')='' or instanceid='" + fin_ca_no + "' ", "rowno asc"); ;//重新获取新空行

            for ( int j=0;j<dtCar.Rows.Count;j++)
            {  
                var drNew = drs[j];
                var drCar = dtCar.Rows[j];               
                drNew["costcateid"] = "T009";
                drNew["costcatename"] = "私车公用/私车公用";
                drNew["instanceid"] = fin_ca_no;
                drNew["budgetsour"] =  "/Platform/WorkFlowRun/Default.aspx?flowid=" + drCar["flowno"] + "&instanceid=" + fin_ca_no + "&display=1";// ;                
                drNew["limit"] = drCar["总预算"];
                drNew["feedate"] = drCar["startdatetime"];
                drNew["feenote"] = drCar["地点"]+"," +drCar["事由"]+";停车过路费:";               
            }

            bindData(false);
        }

    }

    

    protected void btnSearchReImbursed_Click(object sender, EventArgs e)
    {
        ShowHZ_Un();
    }

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
}

