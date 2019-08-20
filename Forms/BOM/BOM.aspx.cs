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

public partial class BOM : PGIBasePage
{
    public string fieldStatus;    
    public string DisplayModel;
    public string ValidScript="";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.EnableViewState=true;
        Page.MaintainScrollPositionOnPostBack = true;
        //      WebForm.Common.DataTransfer.PaoZiLiao    测试账号   G1: 00020  G2:02045 00495 02104 00495 W1:00350   W2:00720                      
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
            Session["bomdtl"] = null;
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

                if (Request["instanceid"] != null)//页面加载
                {                                           
                    //日志加载                     
                   // bindrz2_log(requestid, gv_rz2);
                }
                else//新建
                {                             
                    CreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    CreateById.Text = LogUserModel.UserId;
                    CreateByName.Text = LogUserModel.UserName;
                    domain.Text = (Request["domain"] == null ? "" : Request["domain"].ToString());
                    DeptName.Text = LogUserModel.DepartName;
                    this.phone.Text = LogUserModel.Telephone;
                    txt_LogUserId.Value = LogUserModel.UserId;
                    txt_LogUserName.Value = LogUserModel.UserName;
                    //获取直属主管
                    deptm.Text = BOM.getSupervisorByEmp("", LogUserModel.UserId);
                   
                }

                
              
            }
             //----
             //--
             
            //此处可直接赋值，不需浪费资源
            Array values = Enum.GetValues(typeof(TreeListEditMode));
            foreach (object value in values)
            {
                cmbMode.Items.Add(Enum.GetName(typeof(TreeListEditMode), value), (int)value);
            }
            cmbMode.Value = treeList.SettingsEditing.Mode.ToString();

            ViewState["aplno"] = Request["instanceid"] == null ? "" : Request["instanceid"].ToString();


            ////--==第1步：Get instance Data=======================================================================================================      
            if (id != "" && id != null)
            {
                ViewState["aplno"] = id == null ? "" : id;
                aplno.Text = ViewState["aplno"].ToString();
                dtMst = DbHelperSQL.Query("select * from eng_bom_main_form where aplno='" + id.ToString() + "' ").Tables[0];
            }
            if (dtMst.Rows.Count > 0)
            {
                aplno.Text = dtMst.Rows[0]["aplno"].ToString();  
                CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
                CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
                CreateDate.Text = dtMst.Rows[0]["CreateDate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
                DeptName.Text = dtMst.Rows[0]["DeptName"].ToString();
                phone.Text = dtMst.Rows[0]["phone"].ToString();
                pgino.Value = dtMst.Rows[0]["pgino"].ToString();
                var dt = DbHelperSQL.Query("select top 1 * from eng_bom_dtl_form where pt_part='"+ pgino.Value + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txt_ptdesc1.Value=dt.Rows[0]["pt_desc1"].ToString();
                    txt_ptdesc2.Value = dt.Rows[0]["pt_desc2"].ToString();
                }
                bomver.Text = dtMst.Rows[0]["bomver"].ToString();
                domain.Text= dtMst.Rows[0]["domain"].ToString();
                deptm.Text = dtMst.Rows[0]["deptm"].ToString();
                projector.Text= dtMst.Rows[0]["projector"].ToString();
            }         
            //show version change Record
            bindRec();
            //如果没项目工厂师傅，重新加载
            if (domain.Text.Trim() !=""&& pgino.Value !="" && projector.Text=="")
            {
                SetProjectUser();
            }

        }
        // show treelist data
        bindData();
        
       //.ExpandToLevel(3);
       // treeList.StartEdit("2");
       // treeList.SettingsEditing.AllowNodeDragDrop = true;// chkDragging.Checked;
        if (Request["display"] != null)
        {
            treeList.SettingsEditing.AllowNodeDragDrop = false;
            //treeList.SettingsBehavior.AllowDragDrop = false;
        }                                          //url传参数
        if (Request["pgino"] != null && Request["pgino"].ToString() != "")
        {
            pgino.Value = Request["pgino"].ToString();//.Substring(0,5);
           // version.Value = Request["pgino"].ToString().Substring(5, 1);
            fuzhu();
            SetProjectUser();
            var newestOP = BOM.getPackingOP(pgino.Value.Trim(), domain.Text.Trim());
            txtOP.Text = newestOP == "" ? "未找到最新OP" : newestOP;
            //
            txtOP.ReadOnly = true;
        }

        #endregion



        //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
        string FlowID = Request.QueryString["flowid"]; 
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0"; 
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
        ViewState["fieldStatus"] = fieldStatus;       

    }
     
    
    
    protected void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        treeList.SettingsEditing.Mode = (TreeListEditMode)cmbMode.SelectedItem.Value;
    }
    void bindRec()
    {
        var aplno = ViewState["aplno"] as string;
        DataTable dtVerRec = null;
        var sqlVerRec = "";
        if (ViewState["aplno"] != null && ViewState["aplno"].ToString() != "")        
            sqlVerRec= "select * from eng_bom_verrec_form where 1=1 and  pgino = '{0}' and aplno<='"+ ViewState["aplno"].ToString() + "' order by aplno desc";
        else
            sqlVerRec = "select '"+aplno+ "' aplno,'' pgino,'"+ bomver.Text  +"'  version,NULL id,'' beforupdate,'' afterupdate,'' updatereason, ''drawver, getdate() createdate   "
                + " union all select * from eng_bom_verrec_form where 1=1 and  pgino = '{0}' and version< '{1}'  ";// order by aplno desc
        //select* from eng_bom_verrec_form where 1 = 1 and pgino = 'P0132AA' and version< 'D'
        dtVerRec = DbHelperSQL.Query(string.Format(sqlVerRec, pgino.Value,bomver.Text)).Tables[0];//"D"
        if (dtVerRec.Rows.Count == 0)
        {
            var drRec = dtVerRec.NewRow();
            drRec["id"] = 1;
            drRec["aplno"] = aplno;
            drRec["pgino"]= pgino.Value;
            dtVerRec.Rows.Add(drRec);
        }
        Session["bomverrec"] = dtVerRec;
        dtVerRec = Session["bomverrec"] as DataTable;
        gvVerRec.DataSource = dtVerRec;
        gvVerRec.DataBind();
        gvVerRec.EditIndex = 0;
    }
    void bindData(bool reload=false)
    { 
        var aplno = ViewState["aplno"] as string;
        DataTable dtBom = null;
        
        if (reload == true) { Session["bomdtl"] = null; }
        if (Session["bomdtl"] == null)
        {
            var sqlDtl = "select  d.aplno, d.id, d.pt_part, d.pt_desc1, d.pt_desc2, d.drawno, pt.pt_net_wt, d.ps_qty_per, d.unit, d.material, d.vendor, d.ps_op, d.note, d.pid "
                        + " from eng_bom_dtl_form d join eng_bom_main_form m on d.aplno=m.aplno left join [dbo].[qad_pt_mstr] pt on  d.pt_part=pt.pt_part and m.domain=pt.pt_domain where   d.aplno = '{0}' order by ps_op asc,pt_part asc";
            
            dtBom = DbHelperSQL.Query(string.Format(sqlDtl,aplno)).Tables[0];
            dtBom.PrimaryKey = new DataColumn[] { dtBom.Columns["id"] };
            //取历史最高版本变更
            if (dtBom.Rows.Count == 0)
            {

            }          

            Session["bomdtl"] = dtBom;            
        }
        
        dtBom = Session["bomdtl"] as DataTable;
       
        treeList.DataSource = dtBom;
        treeList.DataBind();

        
        //var dtMst = DbHelperSQL.Query("select top 20 * from PGI_BASE_PART_DATA_form  ").Tables[0];
        //string[] headers = {"key","物料号","物料名称","描述", "描述" };
        //string[] fields = {"id", "wlh", "wlmc", "ms","ms" };
        //string[] controltype = { "label", "textbox", "textbox", "dropdownlist" ,"label"};
        //initGridview(gv_dtl,headers, fields, controltype, dtMst);
    }
    void initGridview(GridView gv,string[] headers,string[] fields,string[] controltype, DataTable dt)
    {
         
        for (int i = 0; i < headers.Length; i++)
        {
            var tf = new TemplateField() { HeaderText = headers[i] };
           
            GridViewTextTemplate gvt=null;
            if (controltype[i] == "textbox" || controltype[i] == "label")
            {
                gvt = new GridViewTextTemplate(DataControlRowType.DataRow, fields[i], dt.Columns[1].DataType.Name, controltype[i]);
            }
            else if (  controltype[i] == "dropdownlist")
            {
                DataTable dtitems = DbHelperSQL.Query("select '-'  as value,'-'as text union select '825-87TC11-C6','825-87TC11-C6'  union select 'M6*1*100L*RH7 2P ' as value,'M6*1*100L*RH7 2P ' as text   ").Tables[0];
                gvt = new GridViewTextTemplate(DataControlRowType.DataRow, fields[i], dt.Columns[1].DataType.Name, controltype[i],dtitems);
            }


            tf.ItemTemplate = gvt;
            gv.Columns.Add(tf);

        }
        gv.AutoGenerateColumns = false;
        gv.DataSource = dt;
        gv.DataBind();

       // gv_dtl0.DataSource = dt;
       // gv_dtl0.DataBind();
       
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
        var mode = Request["mode"] == null ? "" : "_"+Request["mode"].ToString();
      //  Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form"+mode, "dtl", this.gvdtl, ViewState["dtl"] as DataTable,2);

    }
 
 
    public  string GetDanHao()
    {
        string result = "";        
        var sql = string.Format(" select  'BOM' + CONVERT(varchar(8), GETDATE(), 112) + right('000' + cast(isnull(right(max(aplno), 3) + 1, '001') as varchar), 3)  from eng_bom_main_form where aplno like 'BOM' + CONVERT(varchar(8), GETDATE(), 112)+'%'");
        var value = DbHelperSQL.GetSingle(sql).ToString();
        result = value;
        return result;
    }

    #region "WebMethod"   
  
    [System.Web.Services.WebMethod()]
    public static string GetDeptByDomain(string P1)
    {
        string result = "";
        var sql = string.Format(" select distinct dept_name as value,dept_name as text from  V_HRM_EMP_MES   where domain='{0}' or gc='{1}' ", P1, P1);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
    [System.Web.Services.WebMethod()]
    public static string DelFile(string P1,string P2)
    {
        string result = "";
        var sql = string.Format(" update   PUR_pr_main_Form set files=replace(files,'{0}','')  where prno='{1}' ", P1, P2);
        var value = DbHelperSQL.ExecuteSql(sql);
        if (value > 0)
        { result =value.ToString() ; }
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
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[V_HRM_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "' ) )");
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
        sb.Append("   (SELECT  distinct zg_workcode FROM [dbo].[V_HRM_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (workcode='" + emp + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取物料材料  P1：part  P2：domain
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getMaterial(string P1, string P2)
    {
        StringBuilder sb = new StringBuilder();

       // declare @part varchar(20),@domain varchar(20);set @part = 'P0122BA-01';set @domain = '200'


        sb.Append(" select * from( ");
        sb.Append("     select top 1 cailiao from[172.16.5.6].[report].dbo.track t where  'P' + right('0' + SUBSTRING(t.pgi_no, 2, 4), 4) = left('{0}', 5) ");//0:P1
        sb.Append("     union all ");
        sb.Append("     select(case when pt_part like 'R%' then pt_desc1 when pt_part like 'Z07%' then pt_drwg_loc end)  material  from qad.dbo.qad_pt_mstr where pt_part = '{1}' and pt_domain = '{2}' ");//1:P1,2:domain
        sb.Append("  ) t where cailiao is not null ");
        object obj = DbHelperSQL.GetSingle(string.Format(sb.ToString(),P1,P1, P2));
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取物包材消耗工序  P1：part  P2：domain
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getPackingOP(string P1, string P2 )
    {
        StringBuilder sb = new StringBuilder();
        // declare @part varchar(20),@domain varchar(20);set @part = 'P0122BA-01';set @domain = '200'
        
        var sqlnew = " select b.*  from PGI_GYLX_Main_Form a join PGI_GYLX_dtl_Form b on a.FormNo=b.GYGSNo  where col_for_bom='1' and isnull(iscomplete,'')='' and IsXh_op='是' ";
        sqlnew += "  and b.domain='"+P2+"'   and (  pgi_no='" + P1 + "') ";//and  (op='OP'+'" + ps_op + "' or '" + pt_part + "' like  'R%')
        var dt = DbHelperSQL.Query(string.Format(sqlnew, "", "")).Tables[0];
        if (dt.Rows.Count == 0)
        {
            var sql = "select top 1 * from [dbo].[PGI_GYLX_Dtl] where b_flag=1 and domain='"+P2+"' and  IsXh_op='是' and (/*pgi_no='{1}' or*/ pgi_no='" + P1 + "') order by id desc ";//and  (op='OP'+'" + ps_op + "' or '" + P1 + "' like  'R%')
            dt = DbHelperSQL.Query(string.Format(sql, "", "")).Tables[0];
        }

        object obj = dt.Rows.Count > 0 ? dt.Rows[0]["op"].ToString() : "";
        
        return  obj.ToString() =="" ?"":obj.ToString().Substring(2);//去除OP
    }
    
    /// <summary>
    /// 取项目负责人
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getProjectUserByProject(string domain, string pgino)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  left(project_user,5) FROM form3_Sale_Product_MainTable where pgino=left('" + pgino+"',5) and (replace(replace(make_factory,'上海','100'),'昆山','200') like '%" + domain + "%' or '" + domain + "'='')   )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    [System.Web.Services.WebMethod()]
    public static string ChrNext(string chr)
    {
        var Result = chr.ToString();
        if (chr.ToString() == "")
        { Result = 'A'.ToString(); }
        else
            for (char i = Convert.ToChar(chr); i <= 'z'; i++)
            {
                Result = ((char)(i + 1)).ToString();
                break;
            }

        return Result;
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
    protected void SaveData(string formtype,out bool flag)
    {   //保存数据是否成功标识
        flag = true;
        //验证
        Check(); 
 
        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        string instanceid = "0";
        var straplno = "";
        try
        {
            straplno = "";// ViewState["aplno"].ToString();
            //产生申请单号
            if (ViewState["aplno"].ToString() == "")
            {
                straplno = GetDanHao();
                aplno.Text = straplno;
                ViewState["aplno"] = straplno;
            }
            else
            {
                straplno = ViewState["aplno"].ToString();
                aplno.Text= ViewState["aplno"].ToString();
            }

            # region Main_form Dtl_Form             
            List<CommandInfo> cmdlist = new List<CommandInfo>();
            //Main_form
            CommandInfo cmdmain = new CommandInfo();
            if (DbHelperSQL.Exists("select count(1) from eng_bom_main_form where aplno='"+ straplno + "'")==true)
            {
                cmdmain.CommandText = "update eng_bom_main_form set   CreateDate=@CreateDate, CreateById=@CreateById, CreateByName=@CreateByName, deptName=@deptName, job=@job, phone=@phone, domain=@domain, pgino=@pgino,version=@version, bomver=@bomver, reason=@reason,  deptm=@deptm, deptmfg=@deptmfg,projector=@projector where  aplno=@aplno ";
            }
            else
            {            
                 cmdmain.CommandText = "insert into eng_bom_main_form(aplno, CreateDate, CreateById, CreateByName, deptName, job, phone, domain, pgino,version, bomver, reason,  deptm, deptmfg,projector)values(@aplno, @CreateDate, @CreateById, @CreateByName, @deptName, @job, @phone, @domain, @pgino,@version, @bomver, @reason, @deptm, @deptmfg,@projector)";
            }
            SqlParameter[] parammain = {
                          new SqlParameter() { ParameterName="@aplno",  SqlDbType=SqlDbType.VarChar, Value= straplno },
                          new SqlParameter() { ParameterName="@CreateDate",  SqlDbType=SqlDbType.VarChar, Value= CreateDate.Text },
                          new SqlParameter() { ParameterName="@CreateById",  SqlDbType=SqlDbType.VarChar, Value= CreateById.Text },
                          new SqlParameter() { ParameterName="@CreateByName",  SqlDbType=SqlDbType.VarChar, Value= CreateByName.Text },
                          new SqlParameter() { ParameterName="@deptName",  SqlDbType=SqlDbType.VarChar, Value= DeptName.Text },
                          new SqlParameter() { ParameterName="@job",  SqlDbType=SqlDbType.VarChar, Value= "" },
                          new SqlParameter() { ParameterName="@phone",  SqlDbType=SqlDbType.VarChar, Value= phone.Text },
                          new SqlParameter() { ParameterName="@domain",  SqlDbType=SqlDbType.VarChar, Value= domain.Text },
                          new SqlParameter() { ParameterName="@pgino",  SqlDbType=SqlDbType.VarChar, Value= pgino.Value },
                          new SqlParameter() { ParameterName="@version",  SqlDbType=SqlDbType.VarChar, Value= "" },
                          new SqlParameter() { ParameterName="@bomver",  SqlDbType=SqlDbType.VarChar, Value= bomver.Text },
                          new SqlParameter() { ParameterName="@reason",  SqlDbType=SqlDbType.VarChar, Value=""  },
                          new SqlParameter() { ParameterName="@deptm",  SqlDbType=SqlDbType.VarChar, Value= deptm.Text },
                          new SqlParameter() { ParameterName="@deptmfg",  SqlDbType=SqlDbType.VarChar, Value= deptmfg.Text },
                          new SqlParameter() { ParameterName="@projector",  SqlDbType=SqlDbType.VarChar, Value= projector.Text },
                    };
            cmdmain.Parameters = parammain;
            cmdlist.Add(cmdmain);
            //Dtl_Form
            DataTable dt = Session["bomdtl"] as DataTable;
            
            DataTable cdt = dt.GetChanges();
            if (cdt != null)
            {
                ////更新dtl中的aplno            
                //for (int i = cdt.Rows.Count - 1; i >= 0; i--)
                //{
                //    var dr = dt.Rows[i];
                //    if (dr.RowState != DataRowState.Deleted)//不可操控被删除的行
                //    {                       
                //        dr["aplno"] = straplno;
                //    }
                //}
                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    DataRow dr = cdt.Rows[i];
                    if (dr.RowState == DataRowState.Deleted)
                    {   //Console.WriteLine("删除的行索引{0}，原来数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Original]);                     
                        CommandInfo cmd = new CommandInfo( );
                        cmd.CommandText = "delete from eng_bom_dtl_form where id=@id";
                        SqlParameter[] listparam = {
                              new SqlParameter() { ParameterName="@id",  SqlDbType=SqlDbType.UniqueIdentifier, Value= dr["id",DataRowVersion.Original] },
                        };                    
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);                    
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {   //Console.WriteLine("修改的行索引{0}，原来数值是{1}，现在的新数值{2}", i, cdt.Rows[i][0, DataRowVersion.Original], cdt.Rows[i][0, DataRowVersion.Current]);
                        CommandInfo cmd = new CommandInfo();
                        cmd.CommandText = "update eng_bom_dtl_form set [pt_part]=@pt_part ,[pt_desc1]=@pt_desc1 ,[pt_desc2]=@pt_desc2 ,[drawno]=@drawno,[pt_net_wt]=@pt_net_wt,[ps_qty_per]=@ps_qty_per,[unit]=@unit,[material]=@material,[vendor]=@vendor,[ps_op]=@ps_op,[note]=@note,[pid]=@pid where id=@id";
                        SqlParameter[] listparam = new SqlParameter[] {
                            new SqlParameter() {  ParameterName="@id", SqlDbType=SqlDbType.UniqueIdentifier, Value=dr["id",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_part", Value=dr["pt_part",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_desc1", Value=dr["pt_desc1",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_desc2", Value=dr["pt_desc2",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@drawno", Value=dr["drawno",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_net_wt", Value=dr["pt_net_wt",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@ps_qty_per", Value=dr["ps_qty_per",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@unit", Value=dr["unit",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@material", Value=dr["material",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@vendor", Value=dr["vendor",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@ps_op", Value=dr["ps_op",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@note", Value=dr["note",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pid", Value=dr["pid",DataRowVersion.Current] },
                        };
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);
                    }
                    else if (dr.RowState == DataRowState.Added)
                    {   // Console.WriteLine("新添加行索引{0}，数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Current]);
                        CommandInfo cmd = new CommandInfo();
                        cmd.CommandText = "insert into eng_bom_dtl_form(id,pt_part,pt_desc1,pt_desc2,         drawno ,pt_net_wt ,ps_qty_per,unit ,material ,vendor,ps_op,note,pid,aplno)"
                                                              + " values(@id,@pt_part ,@pt_desc1 ,@pt_desc2 ,@drawno,@pt_net_wt,@ps_qty_per,@unit,@material,@vendor,@ps_op,@note,@pid,@aplno)";
                        SqlParameter[] listparam = new SqlParameter[] {
                            new SqlParameter() {  ParameterName="@id", SqlDbType=SqlDbType.UniqueIdentifier, Value=dr["id",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_part", Value=dr["pt_part",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_desc1", Value=dr["pt_desc1",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_desc2", Value=dr["pt_desc2",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@drawno", Value=dr["drawno",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pt_net_wt", Value=dr["pt_net_wt",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@ps_qty_per", Value=dr["ps_qty_per",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@unit", Value=dr["unit",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@material", Value=dr["material",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@vendor", Value=dr["vendor",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@ps_op", Value=dr["ps_op",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@note", Value=dr["note",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@pid", Value=dr["pid",DataRowVersion.Current] },
                            new SqlParameter() {  ParameterName="@aplno", Value=straplno },
                        };
                        cmd.Parameters = listparam;
                        cmdlist.Add(cmd);
                    }
                }
            }
            #endregion
            //bomverRecord
            CommandInfo cmdRec = GetCommandDtl2();             
            cmdlist.Add(cmdRec);
            //批处理 
            DbHelperSQL.ExecuteSqlTran(cmdlist);
            dt.AcceptChanges();
            script += "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + straplno + "');$('#aplno').val('" + straplno + "');};";
            
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
        if((ViewState["aplno"] != null&& ViewState["aplno"].ToString() != "")|| Request.Form["txtInstanceID"] != "")
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
                SaveFile(fileup,  straplno , out filepath, filename, filename);
                //更新文件目录
                string sqlupdatefilecolum = string.Format("update pur_pr_main_form set files='{0}' where prno='{1}'", filepath, straplno);
                DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
                flag = true;
            }
        }
        //执行流程相关事宜
        if (instanceid  != "0" && instanceid !="")//0 影响行数; "" 没有prno
        {
            var titletype = "BOM申请" ;
            string title = titletype + "["+ pgino.Value + "]["+ straplno + "][" + CreateByName.Text+"]"; //设定表单标题
            
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

    private CommandInfo GetCommandDtl2(List<CommandInfo> ListCmd=null)
    {
        CommandInfo cmdRec = new CommandInfo();
        if (DbHelperSQL.Exists("select count(1) from eng_bom_main_form where aplno='" + ViewState["aplno"] as string + "'") == true)
        {
            cmdRec.CommandText = "update eng_bom_verrec_form set pgino=@pgino,version=@version,beforupdate=@beforupdate, afterupdate=@afterupdate, updatereason=@updatereason, drawver=@drawver where  aplno=@aplno ";
        }
        else
        {
            cmdRec.CommandText = "insert into eng_bom_verrec_form(aplno, pgino, version, beforupdate, afterupdate, updatereason, drawver)values(@aplno, @pgino, @version, @beforupdate, @afterupdate, @updatereason, @drawver)";
        }
        GridViewRow gvr = (GridViewRow)gvVerRec.Rows[0]  ;
        var beforupdate = ((TextBox)gvr.FindControl("beforupdate")).Text;
        var afterupdate= ((TextBox)gvr.FindControl("afterupdate")).Text;
        var drawver= ((TextBox)gvr.FindControl("drawver")).Text;
        var updatereason = ((TextBox)gvr.FindControl("updatereason")).Text;
        SqlParameter[] paramRec = {
                          new SqlParameter() { ParameterName="@aplno",  SqlDbType=SqlDbType.VarChar, Value= ViewState["aplno"].ToString() },
                          new SqlParameter() { ParameterName="@pgino",  SqlDbType=SqlDbType.VarChar, Value= pgino.Value },
                          new SqlParameter() { ParameterName="@version",  SqlDbType=SqlDbType.VarChar, Value= bomver.Text },
                          new SqlParameter() { ParameterName="@beforupdate",  SqlDbType=SqlDbType.VarChar, Value= beforupdate },
                          new SqlParameter() { ParameterName="@afterupdate",  SqlDbType=SqlDbType.VarChar, Value= afterupdate },
                          new SqlParameter() { ParameterName="@drawver",  SqlDbType=SqlDbType.VarChar, Value= drawver },
                          new SqlParameter() { ParameterName="@updatereason",  SqlDbType=SqlDbType.VarChar, Value= updatereason },
                    };
        cmdRec.Parameters = paramRec;
        return cmdRec;
    }

    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Purchase";
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

    
    protected void treeList_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
    {
        var aplno = ViewState["aplno"] as string;
        var dragid = e.Node["id"].ToString();//DeptID
        var dragPIDP = e.Node["PID"].ToString();// Parent
        var dropNewPID = e.NewParentNode["id"].ToString();//DeptID
                                                               // var sql = "update ENG_BOM_dtl_form set PID= '" + dropNewPID.ToString() + "' where pt_part= '" + dragpt_part.ToString()+"'";
                                                               //if (DbHelperSQL.ExecuteSql(sql)>0)
                                                               //{
                                                               //    e.Handled = true;
                                                               //    bindData();
                                                               //    return;
                                                               //}
                                                               //else
                                                               //{
                                                               //    e.Handled = true;
                                                               //    bindData();
                                                               //}
        DataTable dt = Session["bomdtl"] as DataTable;
        DataRow[] dr = dt.Select("id = '" + dragid.ToString()+"' and aplno='"+aplno+"' and pid='"+ dragPIDP + "'");
        dr[0]["aplno"] = aplno;      
     
        dr[0]["PID"] = dropNewPID.ToString();       

        Session["bomdtl"] = dt;
        e.Cancel = true;
        bindData();
    }

    protected void treeList_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        var aplno = ViewState["aplno"] as string;
        //var pt_part = e.NewValues["pt_part"];
        //var pt_desc1= e.NewValues["pt_desc1"];
        //var pt_desc2 = e.NewValues["pt_desc2"];
        //var ps_qty_per = e.NewValues["ps_qty_per"];
        //var ps_op = e.NewValues["ps_op"];
        //var pid= e.NewValues["PID"];
        var pt_part = (treeList.FindEditFormTemplateControl("pt_part") as HtmlInputControl).Value.Trim(); //e.NewValues["pt_part"];
        var pt_desc1 = (treeList.FindEditFormTemplateControl("pt_desc1") as HtmlInputControl).Value.Trim(); // e.NewValues["pt_desc1"];
        var pt_desc2 = (treeList.FindEditFormTemplateControl("pt_desc2") as HtmlInputControl).Value.Trim(); //e.NewValues["pt_desc2"];
        var ps_qty_per = (treeList.FindEditFormTemplateControl("ps_qty_per") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_qty_per"];
        var ps_op = (treeList.FindEditFormTemplateControl("ps_op") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_op"];
        var pid = ((DataRowView)treeList.FocusedNode.DataItem).Row["PID"].ToString();
        var drawno = (treeList.FindEditFormTemplateControl("drawno") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_op"];
        var pt_net_wt = "0"; //(treeList.FindEditFormTemplateControl("pt_net_wt") as HtmlInputControl).Value.Trim();
        var unit = (treeList.FindEditFormTemplateControl("unit") as ASPxComboBox).SelectedItem.Value.ToString();
        var material = (treeList.FindEditFormTemplateControl("material") as HtmlInputControl).Value.Trim();
      //  var vendor = (treeList.FindEditFormTemplateControl("vendor") as HtmlInputControl).Value.Trim();
        var note = (treeList.FindEditFormTemplateControl("note") as HtmlInputControl).Value.Trim();
        
        DataTable dt = Session["bomdtl"] as DataTable;
        DataRow dr = dt.NewRow();
        dr["aplno"] = aplno;
        dr["pt_part"] = pt_part; 
        dr["pt_desc1"] = pt_desc1;
        dr["pt_desc2"] = pt_desc2;
        dr["ps_qty_per"] = ps_qty_per;
        dr["ps_op"] = ps_op;
        dr["PID"] = Session["pid"].ToString();// ((DataRowView)treeList.FocusedNode.DataItem).Row["pt_part"].ToString();
        dr["id"] = Guid.NewGuid();//  ((int)dt.Compute("Max(id)", "true")+1);
        dr["drawno"] = drawno;
       // dr["pt_net_wt"] = pt_net_wt;
        dr["unit"] = unit;
        dr["material"] = material;
       // dr["vendor"] = vendor;
        dr["note"] = note;
        dt.Rows.Add(dr);    

        Session["bomdtl"] = dt;

        treeList.CancelEdit();
        e.Cancel = true;
        bindData();
    }

    protected void treeList_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        var aplno = ViewState["aplno"] as string;
        var pt_part_old = (treeList.FindEditFormTemplateControl("pt_partold") as HtmlInputControl).Value.Trim();// e.OldValues["pt_part"];
        var pt_part = (treeList.FindEditFormTemplateControl("pt_part") as HtmlInputControl).Value .Trim(); //e.NewValues["pt_part"];
        var pt_desc1 = (treeList.FindEditFormTemplateControl("pt_desc1") as HtmlInputControl).Value.Trim(); // e.NewValues["pt_desc1"];
        var pt_desc2 = (treeList.FindEditFormTemplateControl("pt_desc2") as HtmlInputControl).Value.Trim(); //e.NewValues["pt_desc2"];
        var ps_qty_per = (treeList.FindEditFormTemplateControl("ps_qty_per") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_qty_per"];
        var ps_op =(treeList.FindEditFormTemplateControl("ps_op") as HtmlInputControl).Value.Trim(); //  (treeList.FindEditFormTemplateControl("ps_op") as ASPxComboBox).SelectedItem.Value.ToString().Trim();  // e.NewValues["ps_op"];
        var drawno = (treeList.FindEditFormTemplateControl("drawno") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_op"];
        var pt_net_wt = (treeList.FindEditFormTemplateControl("pt_net_wt") as HtmlInputControl).Value.Trim();
        var unit = (treeList.FindEditFormTemplateControl("unit") as ASPxComboBox).SelectedItem.Value.ToString();
        var material = (treeList.FindEditFormTemplateControl("material") as HtmlInputControl).Value.Trim();
       // var vendor = (treeList.FindEditFormTemplateControl("vendor") as HtmlInputControl).Value.Trim();
        var note = (treeList.FindEditFormTemplateControl("note") as HtmlInputControl).Value.Trim();
        //var pid = //((DataRowView)treeList.FocusedNode.DataItem).Row["PID"].ToString(); 
        var id= (treeList.FindEditFormTemplateControl("id") as HtmlInputControl).Value.Trim();
       

        DataTable dt = Session["bomdtl"] as DataTable;
        DataRow dr = dt.Select("id='"+id+"'")[0];//dt.Select("pt_part='" + pt_part_old + "' and isnull(pid,'')='" + pid + "'")[0];
        dr["aplno"] = aplno;
        dr["pt_part"] = pt_part;
        dr["pt_desc1"] = pt_desc1;
        dr["pt_desc2"] = pt_desc2;
        dr["ps_qty_per"] = ps_qty_per;
        dr["ps_op"] = ps_op;
        dr["drawno"] = drawno;
       // dr["pt_net_wt"] = pt_net_wt;
        dr["unit"] = unit;
        dr["material"] = material;
       // dr["vendor"] = vendor;
        dr["note"] = note;
        //dr["PID"] = pid;

        Session["bomdtl"] = dt;
        DataTable cdt = dt.GetChanges();
        treeList.CancelEdit();
        e.Cancel = true;
        bindData();
        

    }
    
    protected void treeList_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        var aplno = ViewState["aplno"] as string;
        var pt_part = e.Values["pt_part"];
        var pt_desc1 = e.Values["pt_desc1"];
        var pt_desc2 = e.Values["pt_desc2"];
        var ps_qty_per = e.Values["ps_qty_per"];
        var ps_op = e.Values["ps_op"];
        var pid = e.Values["PID"];
        var id = e.Values["id"];
        DataTable dt = Session["bomdtl"] as DataTable;

       // DataRow[] dr = dt.Select("pt_part='" + pt_part + "' and pid='" + pid + "' and aplno='" + aplno + "'");
       // dr[0].Delete();
        dt.Rows.Find(id).Delete();
       // dt.Rows.Remove(dr[0]);
       
        Session["bomdtl"] = dt;       
        e.Cancel = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "addclass", "alert(6)", true);
        bindData();
    }

    protected void treeList_InitNewNode(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
       
       var pid = ((DataRowView)treeList.FocusedNode.DataItem).Row["id"].ToString();
       Session["pid"] = pid;
      // (treeList.FindEditFormTemplateControl("pid") as HtmlInputControl).Value=pid;
    }
         
    protected void gvVerRec_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = (e.Row.RowIndex + 1).ToString();
             
        }
            
        
    }
     //
    protected void btnFuZhu_Click(object sender, EventArgs e)
    {
        fuzhu();//辅助带资料
        SetProjectUser();
        var newestOP = BOM.getPackingOP(pgino.Value.Trim(),domain.Text.Trim());
        txtOP.Text = newestOP==""?"未找到最新OP":newestOP;
        //
        txtOP.ReadOnly = true;    
        
    }
    protected void SetProjectUser()
    {
        projector.Text = BOM.getProjectUserByProject(domain.Text,pgino.Value );
        if(projector.Text=="")ScriptManager.RegisterStartupScript(Page, this.GetType(), "alertProjector",  " layer.alert('未从产品清单中获取到此产品项目负责人。 请确认.')", true);
    }
    ////辅助带资料
    public void fuzhu()
    {
        var _pgino = pgino.Value.Trim();
        var _domain = domain.Text;//-- domain.Text.Contains("上海")?"100":"200";
        if (_pgino == "") return;
        //是否改项目是否正在申请        
        var dtValid = DbHelperSQL.Query(string.Format(" select createbyname from eng_bom_main_form where pgino='{0}' and domain='{1}' and isnull(iscomplete,0)=0 or iscomplete=''", _pgino, _domain)).Tables[0];
        if (dtValid.Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "layer.alert(\"【" + _pgino + "】项目正被 [" + dtValid.Rows[0]["createbyname"] + "] 申请修改中，请找申请人确认状态。\")", true);
            return;
        }

        //取最新版本 bomver
        // var strSqlver = "select isnull(max(bomver),'') bomver from eng_bom_main_form ";
        var strSqlver = "select isnull(max(bomver),'') bomver from eng_bom_main_form where pgino='" + _pgino + "' and domain='"+_domain+"' ";
        var strver = DbHelperSQL.GetSingle(strSqlver).ToString();
        bomver.Text = BOM.ChrNext(strver);
        var dt = (Session["bomdtl"] as DataTable);

        var dtNew = DbHelperSQL.Query("exec Eng_Bom_GetBom '" + _pgino + "','" + _domain + "'").Tables[0];//新获取新选择的零件bom信息      
        DataRow[] drs = dtNew.Select("pid is null");
        if (drs.Count() > 0)
        {
            txt_ptdesc1.Value = drs[0]["pt_desc1"].ToString();
            txt_ptdesc2.Value = drs[0]["pt_desc2"].ToString();
        }
        if (dt.Rows.Count == 0)
        {
            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow _drNew in dtNew.Rows)
                {
                    var dr = dt.NewRow();
                    dr.ItemArray = _drNew.ItemArray;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                var dr = dt.NewRow();
                dr["pt_part"] = _pgino;
                dr["pt_desc1"] = txt_ptdesc1.Value.Trim();
                dr["pt_desc2"] = txt_ptdesc2.Value.Trim();
                dr["id"] = Guid.NewGuid();
                //dr["pt_net_wt"]=
                dt.Rows.Add(dr);
            }

            Session["bomdtl"] = dt;
            bindData(false);
        }
        else
        {
            if (dt.Select("isnull(pid,'')=''")[0]["pt_part"].ToString() != _pgino)//选择的零件和先前不一致，重新获取新选择的零件bom信息
            {
                //清除历史记录
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = dt.Rows[i];
                    if (dr.RowState != DataRowState.Deleted)
                        dr.Delete();
                }

                if (dtNew.Rows.Count > 0)
                {
                    foreach (DataRow _drNew in dtNew.Rows)
                    {
                        var dr = dt.NewRow();
                        dr.ItemArray = _drNew.ItemArray;
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    var dr = dt.NewRow();
                    dr["pt_part"] = pgino.Value;
                    dr["pt_desc1"] = txt_ptdesc1.Value.Trim();
                    dr["pt_desc2"] = txt_ptdesc2.Value.Trim();
                    dr["id"] = Guid.NewGuid();
                    dt.Rows.Add(dr);
                }
                Session["bomdtl"] = dt;
                bindData(false);
                
            }
        }
        treeList.ExpandAll();

        // gvVerRec.Rows[0].Cells[2].Text = bomver.Text;
        bindRec();
        var dtRec = (Session["bomverrec"] as DataTable);
        dtRec.Rows[0]["version"] = bomver.Text;
        dtRec.Rows[0]["pgino"] = pgino.Value;
        Session["bomverrec"] = dtRec;

        domain.Attributes.Add("readonly", "true");
    }
    
    protected void treeList_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
    {
        var ps_qty_per = (treeList.FindEditFormTemplateControl("ps_qty_per") as HtmlInputControl).Value.ToString(); //e.NewValues["ps_qty_per"];
        var ps_op =  (treeList.FindEditFormTemplateControl("ps_op") as HtmlInputControl).Value.Trim(); //(treeList.FindEditFormTemplateControl("ps_op") as ASPxComboBox).SelectedItem.Value.ToString();//    e.NewValues["ps_op"];
        var drawno = (treeList.FindEditFormTemplateControl("drawno") as HtmlInputControl).Value.Trim(); //e.NewValues["ps_op"];
       // var qty = (treeList.FindEditFormTemplateControl("qty") as HtmlInputControl).Value.Trim();
        var unit = (treeList.FindEditFormTemplateControl("unit") as ASPxComboBox).SelectedItem.Value.ToString();
        var material = (treeList.FindEditFormTemplateControl("material") as HtmlInputControl).Value.Trim();
       // var vendor = (treeList.FindEditFormTemplateControl("vendor") as HtmlInputControl).Value.Trim();
        var note = (treeList.FindEditFormTemplateControl("note") as HtmlInputControl).Value.Trim();
        var pt_part = (treeList.FindEditFormTemplateControl("pt_part") as HtmlInputControl).Value.Trim();
        decimal num;
        
        if (ps_qty_per == "" || Decimal.TryParse(ps_qty_per, out num) == false)
        {
            e.NodeError = "【单件用量】不能为空,且必须为数字。";
            return;
        }

        if (unit == "")
        {
            e.NodeError = "【单位】不能为空。 ";
            return;
        };

        if (material == "")
        {
            e.NodeError = "【材料】不能为空,且必须为整数格式。";
            return;
        }

        if (ps_op == "")
        {
            e.NodeError = "【消耗工序】不能为空,且必须为整数格式。";
            return;
        }

        else//判断是否在工艺工时中有设定此工序
        {
            var _domain = domain.Text.Trim();
            var _pgino = pgino.Value.Trim();
            var _pPart = "";
            object _pid;
            var dtT = (Session["bomdtl"] as System.Data.DataTable);
            if (!e.IsNewNode)
            { 
                var datarow = dtT.Select("id='" + new Guid(treeList.EditingNodeKey) + "'")[0];
                _pid = datarow.ItemArray[13];//太死了  pid
            }
            else
            {
                _pid = treeList.NewNodeParentKey;
            }
            var _pdatarow = dtT.Select("id='" + new Guid(_pid.ToString()) + "'")[0];
            _pPart= _pdatarow.ItemArray[2].ToString();//太死了 父级物料号
            var sqlnew = " select b.*  from PGI_GYLX_Main_Form a join PGI_GYLX_dtl_Form b on a.FormNo=b.GYGSNo  where col_for_bom='1' and isnull(iscomplete,'')='' ";
            sqlnew+= "     and (  pgi_no='" + _pPart + "') and  (op='OP'+'" + ps_op + "' or '" + pt_part + "' like  'R%')";
            var dt = DbHelperSQL.Query(string.Format(sqlnew, _domain, _pgino)).Tables[0];
            if (dt.Rows.Count == 0)
            {
                var sql = "select * from [dbo].[PGI_GYLX_Dtl] where b_flag=1 and domain='{0}' and (pgi_no='"+pt_part+"' or  pgi_no='"+ _pPart + "') and  (op='OP'+'"+ps_op+"' or '"+ pt_part + "' like  'R%')";
                dt = DbHelperSQL.Query(string.Format(sql, _domain, _pgino)).Tables[0];
            }

            if (dt.Rows.Count == 0)
            {
                e.NodeError = "此【消耗工序】不存在。";
                return;
            }
        }
        

    }


    protected void treeList_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if (Object.Equals(e.GetValue("ps_op"), "110"))
        {
           // e.Cell.Font.Bold = true;
        }
        
    }

    protected void treeList_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
    {

        if (e.NodeKey != "")
        {
            TreeListNode node = this.treeList.FindNodeByKeyValue(e.NodeKey.ToString());
            
            //if (node.ChildNodes.Count > 0)
            //{
            //    e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //}     
            e.Visible = DevExpress.Utils.DefaultBoolean.False;// 先置按钮不可用
            if (ViewState["bomright"]==null)
            {
                ViewState["bomright"] = GetRight(pgino.Value.Left(5), Session["LogUser"] as LoginUser);
            }                                  
            
            BomRight m = ViewState["bomright"] as BomRight;//GetRight(pgino.Value.Left(5), Session["LogUser"] as LoginUser);
            var pt_part = node["pt_part"] == null ? "" : node["pt_part"].ToString();

            if(pt_part!=""&&(node["pid"] == null|| node["pid"].ToString()=="")&&m.IsEditable==true)
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
                
            }
             if (m.IsEditable == true && m.IsGongCheng==true)  //   
            {
                if(Object.Equals(pt_part.Left(3), "Z07")==false) //--工程Z07包材不可更改;
                { 
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;

                }
                
            }
             if( m.IsEditable==true &&  m.IsWuLiu == true)  
            {
                if(Object.Equals(pt_part.Left(3), "Z07")){//--物流非 Z07  包材不可改
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                   
                }
                
            }         

            
        }
    }
    protected void treeList_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        var scriptul = new StringBuilder();
        if (e.RowKind == DevExpress.Web.ASPxTreeList.TreeListRowKind.EditForm)
        {            
            var unit = e.GetValue("unit");
            if (unit !=null && unit.ToString()!= "")
            {
                var Item = (treeList.FindEditFormTemplateControl("unit") as ASPxComboBox).Items.FindByValue(unit);
                if (Item != null) Item.Selected = true;
            }
            var _domain = domain.Text.Trim();
            var _pgino = pgino.Value.Trim();
            var _ps_op= e.GetValue("ps_op");

            var sql = "select ''op,''op_desc,''op_remark union select substring(op,3,7) op,substring(op,3,7)+'_'+ op_desc op_desc,op_remark  from [dbo].[PGI_GYLX_Dtl] d join (select pgi_no,max(ver)ver,domain from [dbo].[PGI_GYLX_Dtl] where pgi_no='{0}' and domain='{1}' group by pgi_no,domain  ) t on d.pgi_no=t.pgi_no and d.ver=t.ver and d.domain=t.domain";
            sql = string.Format(sql, _pgino, _domain);
            var dtGYSJ = DbHelperSQL.Query(sql).Tables[0];
            var dropps_op = (treeList.FindEditFormTemplateControl("ps_op_") as ASPxComboBox);
            dropps_op.DataSource = dtGYSJ;
            dropps_op.TextField = "op_desc";
            dropps_op.ValueField = "op";
            dropps_op.DataBind();
            
            if (_ps_op != null && _ps_op.ToString() != "")
            {
                var Itemps_op = dropps_op.Items.FindByValue(_ps_op.ToString());
                if (Itemps_op != null) Itemps_op.Selected = true;
            }

           
               
            for (var i = 1; i <= dtGYSJ.Rows.Count; i++)
            {
                scriptul.Append("alert(3);$('#ulPS_OP').append(\"<li  val='"+dtGYSJ.Rows[i-1]["op"].ToString()+"' onclick='$('#ps_op').val($(this).prop(\"val\"))'>"   + dtGYSJ.Rows[i - 1]["op_desc"].ToString() + "</li>");
            }
           
//ClientScript.RegisterStartupScript(this.GetType(), "registerscr", "alert(4)", true);
        }
        //设定根目录不可删除修改//父节点按钮设定
        if (e.Level == 1)
        {
            if (e.Row.Cells[11].Controls.Count > 2)
            {
                e.Row.Cells[11].Controls[0].Visible = false;//修改按钮禁止
                e.Row.Cells[11].Controls[2].Visible = false;//删除按钮禁止
            }            
        }
        // ScriptManager.RegisterStartupScript(treeList, this.GetType(), "registerscr", scriptul.ToString(), true);
        
    }

    
    protected BomRight GetRight(string pgino,LoginUser loguser)
    {
        BomRight m = new BomRight();
        StringBuilder str = new StringBuilder();
        str.Append("    select workcode,dept_name from V_HRM_EMP_MES a join   (select left(product_user,5)produser,left(bz_user,5)wluser from form3_Sale_Product_MainTable where pgino='"+pgino.Substring(0,5)+"')t ");
        str.Append("    on a.workcode=t.[produser] or a.workcode=t.wluser");
        DataTable dt = DbHelperSQL.Query(str.ToString()).Tables[0];
        if(dt.Select("dept_name='" + loguser.DepartName + "' ").Count() > 0 || loguser.DepartName.Contains("IT") == true)// 同部门人员可以发起修改申请 || IT人员可任意编辑
        {
            m.IsEditable = true;
        }
        if (loguser.DepartName.Contains("物流") == true || loguser.DepartName.Contains("IT")==true)
        {
            m.IsWuLiu = true;
        }
        if (loguser.DepartName.Contains("工程") == true || loguser.DepartName.Contains("IT") == true)
        {
            m.IsGongCheng = true;
        }

        if (m.IsEditable == true && m.IsWuLiu == true)
        {
            pnlPackOP.Visible = true;
        }
        else
        {
            pnlPackOP.Visible = false;
        }

        return m;
    }




    protected void treeList_CustomDataCallback(object sender, TreeListCustomDataCallbackEventArgs e)
    {
        //string key = e.Argument.ToString();
        //TreeListNode node = treeList.FindNodeByKeyValue(key);
        //e.Result = GetEntryText(node);
    }
    //protected string GetEntryText(TreeListNode node)
    //{
    //    //if (node != null)
    //    //{
    //    //    string text = node["pt_part"].ToString();
    //    //    return text.Trim().Replace("\r\n", "<br />");
    //    //}
    //    return string.Empty;
    //}

    protected void btnUpdatePackOp_Click(object sender, EventArgs e)
    {
        if (txtOP.Text.IndexOf("未找到")>=0)
        {
            return;
        }
        var dt = Session["bomdtl"] as DataTable;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["pt_part"].ToString().Left(3) == "Z07")
            {
                dr["ps_op"]=txtOP.Text.Trim();
            }            
        }
        
        bindData(false);
        treeList.ExpandAll();
    }
}
[Serializable]
public class BomRight
{
    public BomRight()
    {
        IsEditable = false;
        IsWuLiu = false;
        IsGongCheng = false;
    }
    public bool IsEditable { get; set; }
    public bool IsWuLiu { get; set; }
    public bool IsGongCheng { get; set; }

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