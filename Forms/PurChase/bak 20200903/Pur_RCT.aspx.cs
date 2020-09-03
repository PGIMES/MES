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

public partial class Pur_RCT : PGIBasePage
{
    public string fieldStatus;    
    public string DisplayModel;
    public string ValidScript="";
    private string mainTable = "PUR_RCT_Main_Form";
    private string dtlTable = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.EnableViewState=true;
        Page.MaintainScrollPositionOnPostBack = true;
        //      WebForm.Common.DataTransfer.PaoZiLiao    测试账号   G1: 00020  G2:02045 00495 02104 00495 W1:00350   W2:00720                      
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        Session["LogUser"] = LogUserModel;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId;
        ViewState["DepartName"] = LogUserModel.DepartName;

  //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
        string FlowID = Request.QueryString["flowid"]; 
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0"; 
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        
        #region "IsPostBack"
        if (!IsPostBack)
        {


            //文件上传在updatepanel需用此方法+enctype = "multipart/form-data" ，否则无法上传文件
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = btnflowSend.UniqueID;
            UpdatePanel1.Triggers.Add(trigger);
            PostBackTrigger trigger2 = new PostBackTrigger();
            trigger2.ControlID = btnSave.UniqueID;
            UpdatePanel1.Triggers.Add(trigger2);
            //Session["bomdtl"] = null;
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
                   
                }
                else//新建
                {                             
                    CreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    CreateById.Text = LogUserModel.UserId;
                    CreateByName.Text = LogUserModel.UserName;
                    
                    DeptName.Text = LogUserModel.DepartName;
                    this.phone.Text = LogUserModel.Telephone;
                    txt_LogUserId.Value = LogUserModel.UserId;
                    txt_LogUserName.Value = LogUserModel.UserName;

                    checkdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    //获取直属主管
                   // deptm.Text = BOM.getSupervisorByEmp("", LogUserModel.UserId);
                   
                }

                
              
            }
             bindObj();
                 

            ViewState["aplno"] = Request["instanceid"] == null ? "" : Request["instanceid"].ToString();

            ////--==第1步：Get instance Data=======================================================================================================      
            if (id != "" && id != null)
            {
                ViewState["aplno"] = id == null ? "" : id;
                aplno.Text = ViewState["aplno"].ToString();
                dtMst = DbHelperSQL.Query(string.Format("select * from {0} where aplno='" + id.ToString() + "' ",mainTable)).Tables[0];
            }
            if (dtMst.Rows.Count > 0)
            {             
                               
                LoadControlValue(dtMst);
                
            }         
            
            
            ////如果是项目，且版本是A直接显示完成按钮，隐藏发送按钮
            //var stepname = BWorkFlow.GetStepName(StepID.ToGuid(), FlowID.ToGuid());
            //if (stepname.Contains("项目"))
            //{
            //    string scriptInitBtn = "$(\"input[id*=btnflowSend]\").css('display','none');$(\"input[id*=btnflowCompleted]\").css('display','');";
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", scriptInitBtn, true);
            //}
            //else
            //{
            //    string scriptInitBtn = "$(\"input[id*=btnflowSend]\").css('display','');$(\"input[id*=btnflowCompleted]\").css('display','none');";
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", scriptInitBtn, true);
            //}

        }
        
        
        if (Request["display"] != null)
        {
            
        }                                         
        

        #endregion              
       
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
        ViewState["fieldStatus"] = fieldStatus;

    }
    public void bindObj()
    {
        var dt = DbHelperSQL.Query("select '' as LB union all select LB from PUR_PO_CLASS where MS_CODE in (2,3)");
        obj.DataSource = dt;
        obj.DataTextField = "LB";
        obj.DataValueField = "LB";
        obj.DataBind();
    }
    public void LoadControlValue(DataTable dt)
    {
        for(int i = 0; i < dt.Columns.Count; i++)
        {
            var field = dt.Columns[i].ColumnName;
            Control ctl = Page.FindControl(field);
            if (ctl != null) {

                if (  ctl.GetType().Name=="HtmlSelect"   )
                {
                    var select = (ctl as HtmlSelect);
                    if (select.Items.FindByValue(dt.Rows[0][i].ToString()) != null) {
                        select.Value = dt.Rows[0][i].ToString();
                    }
                    else
                    {
                        select.SelectedIndex = -1;
                    }
                        
                }
                else if (ctl.GetType().Name == "HtmlInputText")
                {
                    (ctl as HtmlInputText).Value = dt.Rows[0][i].ToString();
                }
                else if ( ctl.GetType().Name.ToLower() == "htmltextarea")
                {
                    (ctl as HtmlTextArea).Value = dt.Rows[0][i].ToString();
                }
                else if (ctl.GetType().Name == "TextBox")
                {
                    (ctl as TextBox).Text = dt.Rows[0][i].ToString();
                }
                else if (ctl.GetType().Name == "DropDownList")
                {
                    var select = (ctl as DropDownList);
                    if (select.Items.FindByValue(dt.Rows[0][i].ToString()) != null)
                    {
                        select.SelectedValue = dt.Rows[0][i].ToString();
                    }
                    else
                    {
                        select.SelectedIndex = -1;
                    }
                }
                else if (ctl.GetType().Name == "FileUpload")
                {
                    var file = (ctl as FileUpload);
                    var filename = dt.Rows[0][i].ToString();
                    if (file!=null && filename != "")
                    {
                        var link = (HyperLink)Page.FindControl( "link_file" );
                        if (link != null )
                        {
                            link.NavigateUrl = filename;
                            var name = filename.Substring(filename.LastIndexOf(@"\") + 1);
                            link.Text = name;
                            link.Target = "_blank";
                        }

                    }
                }
            }
            
        }
    }

    void bindData(bool reload=false)
    { 
        var aplno = ViewState["aplno"] as string;
        DataTable dtBom = null;
        
        //if (reload == true) { Session["bomdtl"] = null; }
        //if (Session["bomdtl"] == null)
        //{
        //    var sqlDtl = "select  d.aplno, d.id, d.pt_part, d.pt_desc1, d.pt_desc2, d.drawno, pt.pt_net_wt, d.ps_qty_per, d.unit, d.material, d.vendor, d.ps_op, d.note, d.pid "
        //                + " from eng_bom_dtl_form d join eng_bom_main_form m on d.aplno=m.aplno left join [dbo].[qad_pt_mstr] pt on  d.pt_part=pt.pt_part and m.domain=pt.pt_domain where   d.aplno = '{0}' order by ps_op asc,pt_part asc";
            
        //    dtBom = DbHelperSQL.Query(string.Format(sqlDtl,aplno)).Tables[0];
        //    dtBom.PrimaryKey = new DataColumn[] { dtBom.Columns["id"] };
        //    //取历史最高版本变更
        //    if (dtBom.Rows.Count == 0)
        //    {

        //    }          

        //   // Session["bomdtl"] = dtBom;            
        //}
        
       // dtBom = Session["bomdtl"] as DataTable;
       
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
 
 
    public  string GetDanHao(string FlagString,string table)
    {
        string result = "";
             
        var sql = string.Format(" select  '{0}' + right(CONVERT(varchar(8), GETDATE(), 112),6) + right('000' + cast(isnull(right(max(aplno), 3) + 1, '001') as varchar), 3)  from  {1} where aplno like '{2}' + right(CONVERT(varchar(8), GETDATE(), 112),6)+'%'",FlagString,table,FlagString);
        var value = DbHelperSQL.GetSingle(sql).ToString();
        result = value;
        return result;
    }

    #region "===========WebMethod===============WebMethod======================WebMethod============================================"   

    /// <summary>
    /// 财务签核人
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getFinSignPerson(string type,string mscode)
    {
        //ver.190415  type: 2.服务(总账会计服务： 吴燕蓝, 王晶晶)   3.到货验收-资产会计：01897 王晶晶   
        //ver.190523  3.设备 模/检/夹具，签核 01977 吴燕蓝，其他的签核 01897王晶晶   2.服务类： 李珊英	00789
        var account = "";
        if (mscode == "2")
            account = "'00789'"; //李珊英    00789
        else
            account = type == "模/检/夹具" ? "'01977'" : "'01897'"; 
         
        string sql= @"declare @ss varchar(500)='';
                    select @ss+= ',u_' + cast(id as varchar(100))  from RoadFlowWebForm.dbo.users where account in("+ account + @");
                    select right(@ss, len(@ss) - 1) finp";

        object obj = DbHelperSQL.GetSingle(sql);
        return obj == null ? "" : obj.ToString();
    }

    [System.Web.Services.WebMethod()]
    public static string GetDeptByDomain(string P1)
    {
        string result = "";
        var sql = string.Format(" select distinct dept_name as value,dept_name as text from  V_HRM_EMP_MES   where domain='{0}' or gc='{1}' ", P1,P1);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
    [System.Web.Services.WebMethod()]
    public static string Complete(string table,string aplno)
    {
        string result = "";
        var sql = string.Format(" update {0} table set iscomplete='1'  where aplno='{1}' ",table, aplno);
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
        sb.Append("   (SELECT  distinct Manager_workcode FROM V_HRM_EMP_MES  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "' ) )");
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
        sb.Append("   (SELECT  distinct zg_workcode FROM V_HRM_EMP_MES  where (domain='" + domain + "' or '" + domain + "'='') and  (workcode='" + emp + "' ) )");
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

    //判断该类别验收是否已验收
    [System.Web.Services.WebMethod()]
    public static string isExistsRct(string pono, string porowno,string contractno,string type)
    {
        string  sb = @"SELECT   isnull(iscomplete,'0')iscomplete  FROM [MES].[dbo].[PUR_RCT_Main_Form] A where isnull(pono,'') ='{0}'  and isnull(porowno,'')='{1}' and isnull(contractno,'')='{2}'  and  isnull(type,'')='{3}'";
                
        var result = DbHelperSQL.GetSingle(string.Format(sb, pono, porowno, contractno, type));

        return result==null?"":result.ToString();
    }
    /// <summary>
    /// 取采购合同信息  P1：id  P2：不使用
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    //未使用
    public static string getRCTInfo(string P1, string P2 )
    {
        StringBuilder sb = new StringBuilder();
        // declare @part varchar(20),@domain varchar(20);set @part = 'P0122BA-01';set @domain = '200'
        sb.Append(" select  r.id,r.syscontractno,r.pono,r.rowid,r.actualreceivedate,r.qty, pr.wlmc,pr.wlms");
        sb.Append(" from PUR_PO_dtl_form m left join PUR_PO_CLASS c on m.po_wltype=c.LB ");
        sb.Append(" left join PUR_PO_Contract_ActaulReceive r on r.pono=m.pono and r.rowid=m.rowid");
        sb.Append(" join PUR_PR_Dtl_Form pr on m.PRNo=pr.prno and m.PRRowId=pr.rowid");
        sb.Append(" where flag_qad='否'  and r.id='{0}'");
        var dt = DbHelperSQL.Query(string.Format(sb.ToString(), P1, "")).Tables[0];
          
        
        return DataTableToJson(dt); 
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
                straplno = GetDanHao("RCT",mainTable);
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
            CommandInfo cmdmain =  GetCommandMain();            
            cmdlist.Add(cmdmain);             
             
            #endregion
             
            //批处理 
            DbHelperSQL.ExecuteSqlTran(cmdlist);
            
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
        var fileup = (FileUpload)this.FindControl("files");
        var filepath = "";
        if (fileup != null)
        {   if (fileup.HasFile)
            {
                var filename = fileup.FileName;
                SaveFile(fileup,  straplno , out filepath, filename, filename);
                //更新文件目录
                string sqlupdatefilecolum = string.Format("update {0} set files='{1}' where aplno='{2}'",mainTable, filepath, straplno);
                DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
                flag = true;
            }
        }
        //执行流程相关事宜
        if (instanceid  != "0" && instanceid !="")//0 影响行数; "" 没有prno
        {
            var titletype = "验收申请" ;
            string title = titletype + "["+ ms.Value + "]["+ straplno + "][" + CreateByName.Text+"]"; //设定表单标题
            
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
    private CommandInfo GetCommandMain(List<CommandInfo> ListCmd = null)
    {
        
        CommandInfo cmdmain = new CommandInfo();
        if (DbHelperSQL.Exists("select count(1) from "+mainTable+" where aplno='" + ViewState["aplno"] as string + "'") == true)
        {
            //id, aplno, createdate, createbyid, createbyname, deptname, phone, obj, type, pono, porowno, ms, description, assetno, sno, equipmentno, usedept, position, receivedate, isreceiveform, files, checkqty, checkdate, isuseful, rctempId, rctempname, iscomplete
            cmdmain.CommandText =string.Format("update {0} set   createDate=@createDate, createbyid=@createbyid, createbyname=@createbyname, deptname=@deptname, phone=@phone"
                + ", obj = @obj, type = @type, pono = @pono, porowno = @porowno, ms = @ms, description = @description, assetno = @assetno"
                + ", sno = @sno, equipmentno = @equipmentno, usedept = @usedept, position = @position, receivedate = @receivedate, isreceiveform = @isreceiveform"
                + ", checkqty = @checkqty, checkdate = @checkdate, isuseful = @isuseful,finsignperson=@finsignperson,contractno=@contractno,domain=@domain,rid=@rid,lb=@lb,costcentre=@costcentre,costcentrename=@costcentrename,deptm=@deptm,deptmfg=@deptmfg  where  aplno=@aplno ", mainTable);
        }
        else
        {
            cmdmain.CommandText = string.Format("insert into {0}( aplno, createdate, createbyid, createbyname, deptname, phone, obj, type, pono, porowno, ms, description, assetno, sno, equipmentno, usedept, position, receivedate, isreceiveform,checkqty, checkdate, isuseful,finsignperson,contractno,domain,rid,lb,costcentre,costcentrename,deptm,deptmfg)"
                + "values( @aplno, @createdate, @createbyid, @createbyname, @deptname, @phone, @obj, @type, @pono, @porowno, @ms, @description, @assetno, @sno, @equipmentno, @usedept, @position, @receivedate, @isreceiveform,  @checkqty, @checkdate,@isuseful,@finsignperson,@contractno,@domain,@rid,@lb,@costcentre,@costcentrename,@deptm,@deptmfg)", mainTable);
        }
        SqlParameter[] parammain = {
                          new SqlParameter() { ParameterName="@aplno",  SqlDbType=SqlDbType.VarChar, Value= ViewState["aplno"] },
                          new SqlParameter() { ParameterName="@createdate",  SqlDbType=SqlDbType.VarChar, Value= CreateDate.Text },
                          new SqlParameter() { ParameterName="@createbyid",  SqlDbType=SqlDbType.VarChar, Value= CreateById.Text },
                          new SqlParameter() { ParameterName="@createbyname",  SqlDbType=SqlDbType.VarChar, Value= CreateByName.Text },
                          new SqlParameter() { ParameterName="@deptName",  SqlDbType=SqlDbType.VarChar, Value= DeptName.Text },                          
                          new SqlParameter() { ParameterName="@phone",  SqlDbType=SqlDbType.VarChar, Value= phone.Text },
                          new SqlParameter() { ParameterName="@obj",  SqlDbType=SqlDbType.VarChar, Value= obj.Value },
                          new SqlParameter() { ParameterName="@type",  SqlDbType=SqlDbType.VarChar, Value= type.Value },
                          new SqlParameter() { ParameterName="@pono",  SqlDbType=SqlDbType.VarChar, Value=pono.Value  },
                          new SqlParameter() { ParameterName="@porowno",  SqlDbType=SqlDbType.VarChar, Value= porowno.Value },
                          new SqlParameter() { ParameterName="@ms",  SqlDbType=SqlDbType.VarChar, Value= ms.Value },
                          new SqlParameter() { ParameterName="@description",  SqlDbType=SqlDbType.VarChar, Value= description.Value },
                          new SqlParameter() { ParameterName="@assetno",  SqlDbType=SqlDbType.VarChar, Value= assetno.Value },
                          new SqlParameter() { ParameterName="@sno",  SqlDbType=SqlDbType.VarChar, Value= sno.Text },
                          new SqlParameter() { ParameterName="@equipmentno",  SqlDbType=SqlDbType.VarChar, Value= equipmentno.Value },
                          new SqlParameter() { ParameterName="@usedept",  SqlDbType=SqlDbType.VarChar, Value= usedept.Value },
                          new SqlParameter() { ParameterName="@position",  SqlDbType=SqlDbType.VarChar, Value= position.Value },
                          new SqlParameter() { ParameterName="@receivedate",  SqlDbType=SqlDbType.VarChar, Value= receivedate.Value },
                          new SqlParameter() { ParameterName="@isreceiveform",  SqlDbType=SqlDbType.VarChar, Value= isreceiveform.Value },
                          new SqlParameter() { ParameterName="@checkqty",  SqlDbType=SqlDbType.Decimal, Value= checkqty.Value },
                          new SqlParameter() { ParameterName="@checkdate",  SqlDbType=SqlDbType.VarChar, Value= checkdate.Value },
                          new SqlParameter() { ParameterName="@isuseful",  SqlDbType=SqlDbType.VarChar, Value= isuseful.Value },
                          new SqlParameter() { ParameterName="@finsignperson",  SqlDbType=SqlDbType.VarChar, Value= finsignperson.Value },
                          new SqlParameter() { ParameterName="@contractno",  SqlDbType=SqlDbType.VarChar, Value= contractno.Value },
                          new SqlParameter() { ParameterName="@domain",  SqlDbType=SqlDbType.VarChar, Value= domain.Value },
                          new SqlParameter() { ParameterName="@rid",  SqlDbType=SqlDbType.VarChar, Value= rid.Value },
                          new SqlParameter() { ParameterName="@lb",  SqlDbType=SqlDbType.VarChar, Value= lb.Value },
                          new SqlParameter() { ParameterName="@costcentre",  SqlDbType=SqlDbType.VarChar, Value= costcentre.Value },
                          new SqlParameter() { ParameterName="@costcentrename",  SqlDbType=SqlDbType.VarChar, Value= costcentrename.Value },
                          new SqlParameter() { ParameterName="@deptm",  SqlDbType=SqlDbType.VarChar, Value= deptm.Value },
                          new SqlParameter() { ParameterName="@deptmfg",  SqlDbType=SqlDbType.VarChar, Value= deptmfg.Value },
                    };
        cmdmain.Parameters = parammain;
        return cmdmain;
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
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);// 防止获取的文件名为完整路径错误 截取最后真filename
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

              //  filecontainer.Controls.AddAt(0,link);
              //  filecontainer.Controls.AddAt(1, btndel);
            }
        }
        if (filestring == "") {
            Label lbl = new Label() {ID="lblnofile", Text = "无附件" };
           // filecontainer.Controls.AddAt(0,lbl );
            
        }
    }
    #endregion

   
  
    public static string DataTableToJson(DataTable table)
    {
        var JsonString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                   + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                     + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
        }
        return JsonString.ToString();
    } 


}
