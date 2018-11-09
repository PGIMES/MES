﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using System.Linq;

public partial class PUR_PR : PGIBasePage
{
    public string fieldStatus;    
    public string DisplayModel;
    public string ValidScript="";
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

    void bindData()
    {
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
            var dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where prno='" + ViewState["PRNo"].ToString() + "' order by rowid asc").Tables[0];
            ViewState["dtl"] = dtl;
        }
        gvdtl.Columns.Clear();
        var mode = Request["mode"] == null ? "" : "_"+Request["mode"].ToString();
        Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form"+mode, "dtl", this.gvdtl, ViewState["dtl"] as DataTable,2);
        GetGrid(ViewState["dtl"] as DataTable, formtype);
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
   /// <summary>
    /// 获取指定物料最低历史单价
    /// </summary>
    /// <param name="P1">物料号</param>
    /// <param name="P2">暂不使用，空即可</param>
    /// <returns></returns>
     [System.Web.Services.WebMethod()] 
    public static string GetHistoryPrice(string P1, string P2)
    {
        string result = "";
        var strB = new StringBuilder();//历史采购最低价
        strB.Append(" SELECT top 1  (1+(CASE WHEN pod_taxc='17' AND ISNULL([pod_start_eff[1]]],pod_due_date)>='2018-05-01' THEN 16 ");
        strB.Append("           WHEN pod_taxc = '17' AND ISNULL([pod_start_eff[1]]],pod_due_date)< '2018-05-01' THEN 17 ELSE pod_taxc END )/ 100.0) *[pod_pur_cost] ");
        strB.Append(" FROM[qad].[dbo].[qad_pod_det]  where pod_nbr<> '11801'  and pod_type<> 'M'   ");//and ISNULL([pod_start_eff[1]]], pod_due_date)<= dateadd(year, -1, getdate())
        strB.Append("   and pod_part = '{0}' and pod_domain = '{1}'   order by [pod_pur_cost] asc ");

        var sql = string.Format(strB.ToString(), P1, P2);
       //  var sql= string.Format(" select top 1 [pc_amt[1]]] as amt from qad.dbo.qad_pc_mstr where  pc_start between DATEADD(YEAR,-1,GETDATE()) and  GETDATE()  and   pc_part='{0}'   order by [pc_amt[1]]] ", P1); //取价格单
              
        var value = DbHelperSQL.GetSingle(sql) ;        
        result = value==null?"":value.ToString();
        return result;
    }
    [System.Web.Services.WebMethod()]
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
        if (P3 == "存货(刀具类)")
        {
            sql = @"select wlh,wlmc,ms,class,type,upload
	                    , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.domain and [pod_sched]=1 and [pod_part]=a.wlh  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                    from dbo.PGI_BASE_PART_DATA a 
                    where wlh='{0}' and domain='{1}' ";
        }
        else
        {
            sql = @"select pt_part wlh,pt_desc1 wlmc,pt_desc2 ms,pt_prod_line+'-'+isnull(b.pt_prod_line_mc,'') class,'' type,'' upload
	                    , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.pt_domain and [pod_sched]=1 and [pod_part]=a.pt_part  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                    from dbo.qad_pt_mstr a
	                    left join(
		                    select distinct pl_domain, PL_PROD_LINE 
			                    ,case when len(pl_desc)-len(replace(pl_desc,'-',''))=2 then SUBSTRING(pl_desc,dbo.fn_find('-',pl_desc,2)+1 ,LEN(pl_desc)-dbo.fn_find('-',pl_desc,1))
				                    when   len(pl_desc)-len(replace(pl_desc,'-',''))=1 then substring(pl_desc,charindex('-',pl_desc)+1,len(pl_desc)-charindex('-',pl_desc)) else pl_desc
				                    end as pt_prod_line_mc 
		                    from qad.dbo.qad_pl_mstr
		                    where left(pl_prod_line,1) in ('4') and pl_prod_line<>'4010'
		                    ) b on a.pt_domain=b.pl_domain and a.pt_prod_line=b.pl_prod_line
                    where pt_part like 'z%' and pt_prod_line<>'4010' and (pt_status<>'DEAD' and pt_status<>'OBS') 
                        and pt_part='{0}' and pt_domain='{1}' ";
        }
        
        var value = DbHelperSQL.Query(string.Format(sql, P1, P2)).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
    [System.Web.Services.WebMethod()]
    public static string GetDeptByDomain(string P1)
    {
        string result = "";
        var sql = string.Format(" select distinct dept_name as value,dept_name as text from  HR_EMP_MES   where domain='{0}' or gc='{1}' ", P1,P1);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
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
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[HR_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
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
    protected void SaveData(string formtype,out bool flag)
    {   //保存数据是否成功标识
        flag = true;
        //验证
        Check(); 
        //string sformsate = "";        
        //{                   
        //    Pgi.Auto.Common comformstate = new Pgi.Auto.Common() { Code = "formstate", Key = "1", Value = sformsate };
        //    ls.Add(comformstate);           
        //}
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
            var filename = file.Substring(file.LastIndexOf(@"\")+1);
            HyperLink link = new HyperLink() {
                ID = "lnk_" + file,
                NavigateUrl = file,
                Text = filename,
                Target = "_blank"
                
            };
            link.Attributes.Add("style","padding-left:10px;");
            filecontainer.Controls.AddAt(0,link);
        }
        if (arrFile.Length == 0) {
            Label lbl = new Label() { Text = "无附件" };
            filecontainer.Controls.AddAt(0,lbl ); }
    }
    #endregion



    protected void btnAddDetl_Click(object sender, EventArgs e)
    {
        DataTable dtl;//= ViewState["dtl"] as DataTable;
        dtl=Pgi.Auto.Control.AgvToDt(gvdtl);
        var dr= dtl.NewRow();
        dr["prno"] = PRNo.Text;

        object maxObject;
        maxObject = dtl.Compute("max(rowid)", "");
        if (maxObject.ToString() == "") { maxObject = 0; }
        dr["rowid"] = (Convert.ToInt16(maxObject) + 1).ToString();
        //dr["rowid"] = dtl.Rows.Count + 1;
        dr["attachments_name"] = "查看";
        dtl.Rows.Add(dr);
        var sortrowid = 0;
        for(int  row=0;row<dtl.Rows.Count;row++)
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

        ViewState["dtl"] = dtl;
        loadControl( "");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //DataTable dtl;//= ViewState["dtl"] as DataTable;
        //dtl = Pgi.Auto.Control.AgvToDt(gvdtl);
        //var dr = dtl.NewRow();        
        //dr["rowid"] = gvdtl.;
        //dtl.Rows.Add(dr);
        //ViewState["dtl"] = dtl;
        //loadControl("");

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
         
        loadControl("");
        Pgi.Auto.Public.MsgBox(this.Page, "alert", "删除成功!");

    }

    //protected void domain_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var P1 = domain.SelectedValue;
    //    var sql = string.Format(" select distinct dept_name as value,dept_name as text from  HR_EMP_MES   where domain='{0}' or gc='{1}' ", P1, P1);
    //    var value = DbHelperSQL.Query(sql).Tables[0];
    //    this.applydept.DataSource = value;
    //    applydept.DataValueField = "value";
    //    applydept.DataTextField = "text";
    //    applydept.DataBind();
    //    applydept.Items.Insert(0, "");
    //}


    //20181108 add by heguiqin
    protected void prtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["PRNo"] != null && ViewState["PRNo"].ToString() != "")
        {
            var dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where prno='" + ViewState["PRNo"].ToString() + "' order by rowid asc").Tables[0];
            ViewState["dtl"] = dtl;
        }
        else
        {
            var dtl = DbHelperSQL.Query("select *,'查看' attachments_name from pur_pr_dtl_form where 1=2 order by rowid asc").Tables[0];
            ViewState["dtl"] = dtl; ;
        }
        gvdtl.Columns.Clear();
        var mode = Request["mode"] == null ? "" : "_" + Request["mode"].ToString();
        Pgi.Auto.Control.SetGrid("PUR_PR_Dtl_Form" + mode, "dtl", this.gvdtl, ViewState["dtl"] as DataTable, 2);
        GetGrid(ViewState["dtl"] as DataTable, prtype.SelectedValue);
    }


    protected void GetGrid(DataTable DT, string prtype_v)
    {
        DataTable ldt = DT;
        int index = gvdtl.VisibleRowCount;
        string sql = @"select ''  value";
        if (prtype_v!="存货(刀具类)")
        {
            sql += @" union all select '无'  value";
        }
        sql += @" union all SELECT replace(pgino+[version]+'/'+productcode,' ','')   FROM [dbo].[form3_Sale_Product_DetailTable]";

        DataTable ldt_usefor = DbHelperSQL.Query(sql).Tables[0];

        for (int i = 0; i < gvdtl.VisibleRowCount; i++)
        {

            DevExpress.Web.ASPxComboBox tb1 = (DevExpress.Web.ASPxComboBox)gvdtl.FindRowCellTemplateControl(i, gvdtl.DataColumns[8], gvdtl.DataColumns[8].FieldName);
            tb1.DataSource = ldt_usefor;
            tb1.TextField = "value";
            tb1.ValueField = "value";
            tb1.DataBind();
            tb1.Value = ldt.Rows[i]["usefor"].ToString();

        }
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