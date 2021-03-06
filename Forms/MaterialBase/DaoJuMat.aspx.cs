﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using IBatisNet.Common.Transaction;
using System.Web.Script.Services;
using System.Web.Services;
using MES.Model;
using MES.DAL;
using Maticsoft.Common;
using System.Collections;
using System.Reflection;
using System.Linq;

public partial class DaoJuMat : PGIBasePage
{
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript="";
    public StringBuilder Edsm=new StringBuilder();
    public string formid = @"/Forms/MaterialBase/DaoJuMat.aspx"; public string stepid = "";
    AttachUpload AttachUpload = new AttachUpload();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        //      WebForm.Common.DataTransfer.PaoZiLiao                                        02128  00404      00076  01968  
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);//
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
                txt_LogUserJob.Value = LogUserModel.JobTitleName;
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
                    txt_CreateByAd.Value = LogUserModel.ADAccount;
                    txt_CreateByDept.Value = LogUserModel.DepartName; 
                    txt_managerid.Value = LogUserModel.ManagerId; 
                    txt_manager.Value = LogUserModel.ManagerName; 
                    txt_manager_AD.Value = LogUserModel.ManagerADAccount;
                    txt_LogUserId.Value = LogUserModel.UserId;
                    txt_LogUserName.Value = LogUserModel.UserName;                                    
                }

                BaseFun fun = new BaseFun();
                var tbldomain = DbHelperSQL.Query(" select cateid,catevalue from pgi_base_data where cate='domain' ").Tables[0];
                fun.initCheckBoxList(ddldomain, tbldomain, "cateid", "catevalue");
                var tblclass = DbHelperSQL.Query("select distinct [type] as value from PGI_BASE_PART_ddl").Tables[0];
                fun.initDropDownList((DropDownList)this.FindControl("class"), tblclass, "value", "value");
                var tbltype = DbHelperSQL.Query("select '' as value ,'-请选择-' as text,'' as val union all  select value as value, name as text,value as val from PGI_BASE_PART_ddl where type = '刀具类' order by val").Tables[0];
                fun.initDropDownList(type, tbltype, "value", "text");
            }
        }
        #endregion

        DataTable dtMst =new DataTable();
        var strType = "";
        string id = Request["instanceid"]; // get instanceid
       
        //--==第1步：Get instance Data=======================================================================================================      
        if (id.IsInt() ) 
        {                       
            dtMst = DbHelperSQL.Query("select * from PGI_BASE_PART_DATA_form where id='" + id.ToString() + "' ").Tables[0];                
        }
        if (dtMst.Rows.Count > 0)
        {
            strType = dtMst.Rows[0]["type"].ToString();
            var item = type.Items.FindByText(strType);
            if (item != null)
            {
                strType = item.Value;
            }

            setCheckBoxListSelectValue(ddldomain, dtMst.Rows[0]["domain"].ToString(), ';', true);
            CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
            CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
            CreateDate.Text = dtMst.Rows[0]["CreateDate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
            purchaser.Text= dtMst.Rows[0]["purchaser"].ToString();
        }
        else
        {
            strType = type.SelectedItem.Value;
        }

        //发起【修改申请】流程参数,初始化值给画面
        var wlh = Request["wlh"] ;
        var domain = Request["domain"] ;
        DataTable dtMstOld = new DataTable() ;
        if (wlh != ""&& Request["wlh"]!=null && domain != "" && Request["domain"]!=null)
        {
            //StringBuilder str = new StringBuilder();
            dtMstOld = DbHelperSQL.Query("select *,'是' as askprice,aqkc as newaqkc from PGI_BASE_PART_DATA where wlh='" + wlh.ToString() + "' and domain='" + domain.ToString() + "'").Tables[0];

        }
        if (dtMstOld != null && dtMstOld.Rows.Count > 0)
        {
            strType = dtMstOld.Rows[0]["type"].ToString();
            var item = type.Items.FindByText(strType);
            if (item != null)
            {
                strType = item.Value;
            }

            setCheckBoxListSelectValue(ddldomain, dtMstOld.Rows[0]["domain"].ToString(), ';', true);
            
        }
        //end 发起【修改申请】
        //--== 第 步:装载控件========================================================================================================
        loadControl(strType);
        //将表单主表值给页面
        if (dtMst!=null && dtMst.Rows.Count > 0)
        {   
            Pgi.Auto.Control.SetControlValue(strType, "", this, dtMst.Rows[0]);
            var item = type.Items.FindByText(dtMst.Rows[0]["type"].ToString());
            if (item != null)
            {
                type.ClearSelection();
                item.Selected=true;
            }
             
            formstate.Text = dtMst.Rows[0]["formstate"] == null ? "" : dtMst.Rows[0]["formstate"].ToString();
        }
        //将表单主表值给页面
        if (dtMstOld != null && dtMstOld.Rows.Count > 0)
        {
            // 没有purchaser,补上
            DataColumn colpurchaser = new DataColumn()
            {
                DefaultValue = "", ColumnName = "purchaser"
            };
            DataColumn createbyid = new DataColumn()
            {
                DefaultValue = CreateById.Text, ColumnName = "createbyid"
            };
            DataColumn createbyname = new DataColumn()
            {
                DefaultValue = CreateByName.Text, ColumnName = "createbyname"
            };
            DataColumn createdate = new DataColumn()
            {
                DefaultValue = DateTime.Now.ToString("yyyy-MM-dd"), ColumnName = "createdate"
            };
            dtMstOld.Columns.Add(colpurchaser);
            dtMstOld.Columns.Add(createbyid);
            dtMstOld.Columns.Add(createbyname);
            dtMstOld.Columns.Add(createdate);

            Pgi.Auto.Control.SetControlValue(strType, "", this, dtMstOld.Rows[0]);
             

            var item = type.Items.FindByText(dtMstOld.Rows[0]["type"].ToString());
            if (item != null)
            {
                type.ClearSelection();
                item.Selected = true;
            }
            //新请购人
            var newPurchaser = DaoJuMat.GetPurchaser(type.SelectedItem.Text, "");
            purchaser.Text = newPurchaser;           

        }

        DataTable dtscript =new DataTable();
        var sqlScript = "";
        //edsm Script  （自动带出额定寿命）
        StringBuilder strBSql = new StringBuilder();
        strBSql.Append(" declare @script varchar(max)='';");
        strBSql.Append(" select @script+=  'else if( '+ '$(\"#type\").find(\"option:selected\").text()==\"'+ type +'\" && ' +  ");
        strBSql.Append(" isnull('$(\"#'+replace(bjgcz,'=','\").val()=='),'1==1') +' && ' +isnull('$(\"#'+replace(var1,'=','\").val()=='),'1==1')  +' && ' +");
        strBSql.Append(" isnull('$(\"#'+replace(var2,'=','\").val()=='),'1==1')  +' && ' +isnull('$(\"#'+replace(var3,'=','\").val()=='),'1==1')  +' && ' +");
        strBSql.Append(" isnull('$(\"#'+replace(var4,'=','\").val()=='),'1==1')  +' && ' +");
        strBSql.Append(" (case when charindex('$(',var5 )=0 then '1==1' when charindex('$(',var5 )>0 then var5 when var5 is null then '1==1'	end )");
        strBSql.Append(" +'){ $(\"#edsm\").val(\"'+cast(edsm as varchar)+'\"); }'+ char(10)   from PGI_BASE_PART_DATA_lifetime where 1=1 and isYX='Y'");
        strBSql.Append(" ; select substring(@script,5,len(@script)-4)+'' as script");
        dtscript = DbHelperSQL.Query(strBSql.ToString()).Tables[0];

        for (int i = 0; i < dtscript.Rows.Count; i++)
        {
            Edsm .Append( dtscript.Rows[i]["script"].ToString());
        }
        //Load Check Script  （不需修改）      
        sqlScript ="select control_event+char(10) as script from auto_form where form_type='" + type.SelectedValue + "' and isnull(control_id,'')<>'' and isnull(control_event,'')<>'' order by form_div, control_order;";
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

        BindData();

    }
    public void setCheckBoxListSelectValue(CheckBoxList checkboxlist,string checkedValue,char splitChar,bool enabled)
    {
        var list = checkedValue.Split(splitChar);
        foreach(var value in list)
        {
            ListItem item = checkboxlist.Items.FindByValue(value);
            if (item != null)
            {
                item.Selected = true;
                item.Enabled = enabled;
            }
        }
        checkboxlist.Enabled = enabled;
    }
    public void loadControl(string formtype)
    {
        //--== 第一步:装载控件========================================================================================================
        //物料类别
        //List<TableRow> ls = Pgi.Auto.Control.ShowControl(formtype, formtype + "_1", 4, "rows", "column", "form-control");
        //for (int i = 0; i < ls.Count; i++)
        //{
        //    this.tblWLLeibie.Rows.Add(ls[i]);
        //}
        //物料属性
        tblWLShuXing.Rows.Clear();
        List<TableRow> ls2 = Pgi.Auto.Control.ShowControl(formtype, formtype + "_2", 4, "rows", "column", "form-control");
        for (int i = 0; i < ls2.Count; i++)
        {
            this.tblWLShuXing.Rows.Add(ls2[i]);
        }
        //物料数据、计划数据
        tblWLShuJu.Rows.Clear();
        List<TableRow> ls3 = Pgi.Auto.Control.ShowControl(formtype, formtype+"_3", 4, "rows", "column", "form-control");
        for (int i = 0; i < ls3.Count; i++)
        {
            this.tblWLShuJu.Rows.Add(ls3[i]);
        }
    }
    /// <summary>
    /// 获取物料编号
    /// </summary>
    /// <param name="id">流程实例id</param>
    /// <returns></returns>
   // [System.Web.Services.WebMethod()] 
    public static string GetWLH(string P1,string P2)
    {
        string result = "";
        var sqlwlh = string.Format(" select isnull(max(wlh),'')wlh from (select pt_part as wlh from qad_pt_mstr where left(pt_prod_line,4)=4010 and left(pt_part,1)='Z' and  len(pt_part)=9  union  select wlh from PGI_BASE_PART_DATA_FORM)t where left(wlh,5) = 'Z{0}{1}' and right(wlh,1)<>'X'", P1.Left(2), P2.Left(2));        
        var wlh = DbHelperSQL.GetSingle(sqlwlh).ToString();
        if (wlh.Length > 0)
        {
            var sn = wlh.Substring(5);
            sn = (Convert.ToInt16(sn) + 1).ToString().PadLeft(4, '0');
            wlh = wlh.Left(5) + sn;

        }
        else
        {
            wlh = string.Format("Z{0}{1}{2}" , P1.Left(2),P2.Left(2) , "0001");
        }
        //txtWlh.Text = wlh;

        result = wlh.ToString();
        return result;
    }
     /// <summary>
     /// 获取此类型刀具请购人
     /// </summary>
     /// <param name="P1">类型名称</param>
     /// <param name="P2">暂不使用，空即可</param>
     /// <returns></returns>
   public static string GetPurchaser(string P1, string P2)
    {
        string result = "";
        var sql= string.Format(" select 'u_'+cast(ID as varchar(100)) as purchaser_id   from RoadFlowWebForm.dbo.Users where Account=(  SELECT purchaser_id  FROM[MES].[dbo].[PGI_BASE_PART_DDL]  where name = '{0}')", P1);
        var value = DbHelperSQL.GetSingle(sql).ToString();        
        result = value;
        return result;
    }
    #region "日志 暂不使用"

    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM  [Q_ReView_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }
    public void bindrz2_log(string requestid, GridView gv_rz2)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM Q_ReView_LOG ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz2.DataSource = dt;
        gv_rz2.DataBind();
        gv_rz2.PageSize = 100;
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
        
        var txtWlh = ((TextBox)this.FindControl("wlh"));
        var oldwlh = txtWlh.Text;
        var newwlh = txtWlh.Text;
        //物料号重新获取，免同时提交重复

        if ((Request["state"] == null || Request["state"] == "")&&(Request["instanceid"]==null)&&formstate.Text=="")//
        {
            newwlh = DaoJuMat.GetWLH("01", type.SelectedValue.Left(2));
        }
         
        ((TextBox)this.FindControl("wlh")).Text = newwlh;
        //从Auto_Form 获取值 验证
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue(formtype, "", this);
        for (int i = 0; i < ls.Count; i++)
        {
            Pgi.Auto.Common com = new Pgi.Auto.Common();
            com = ls[i];
            if (ls[i].Code == "")
            {
                var msg=ls[i].Value + "不能为空!";
                flag = false; //
                ScriptManager.RegisterStartupScript(Page,this.GetType(), "", "layer.alert('" + msg + "');",true);//$(#'" + ls[i].Code + "').focus();
                return ;
            }
        }
        var common = ls.Where(r => r.Code == "type").ToList();
        common[0].Value = this.type.SelectedItem.Text;
        //设定主键：1.如果是变更资料
        var state = Request["state"];
        string sformsate = "";
        if(state!=null&&state.ToString()!="") //update
        {
            var lskey = ls.Where(r => r.Key == "1").ToList();
            foreach(var item in lskey)
            {
                item.Key = "0";
            }
            sformsate = formstate.Text == "" ? "edit" + DateTime.Now.ToString("yyyyMMddHHmmss"): formstate.Text;
            script += "$('#formstate').val('" + sformsate + "');";

            Pgi.Auto.Common comformstate = new Pgi.Auto.Common() {  Code="formstate", Key="1", Value= sformsate };
            ls.Add(comformstate);

            var upfile = ((HyperLink)this.FindControl("link_upload")).NavigateUrl;
            var lsupload = ls.Where(r => r.Code == "upload").ToList();
            lsupload[0].Value = upfile;
        }
        else //new
        {
            sformsate = formstate.Text == "" ? "new" + DateTime.Now.ToString("yyyyMMddHHmmss") : formstate.Text;
            script += "$('#formstate').val('" + sformsate + "');";

            Pgi.Auto.Common comformstate = new Pgi.Auto.Common() { Code = "formstate", Key = "1", Value = sformsate };
            ls.Add(comformstate);
            var lswlh = ls.Where(r => r.Code == "wlh").ToList();
            lswlh[0].Value = newwlh;
        }
        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        int instanceid = 0;
        try
        {
            instanceid = Pgi.Auto.Control.UpdateValues(ls, "PGI_BASE_PART_DATA_FORM");
        }
        catch (Exception e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok",  "layer.alert('保存表单数据失败，请确认。ErrorMessage:"+e.Message.Replace("'","").Replace("\r\n", "") + "');", true);
        }
        
        //如果是签核或修改 取传递过来instanceid值
        if((Request["instanceid"]!=null&& Request["instanceid"] != "")|| Request.Form["txtInstanceID"] != "")
        {
            instanceid = Request.Form["txtInstanceID"]==""? Convert.ToInt32(Request["instanceid"]): Convert.ToInt32(txtInstanceID.Text);
        }
        //Save file
        var fileup = (FileUpload)this.FindControl("UPLOAD");
        var filepath = ""; var originalname = "";
        if (fileup != null)
        {   if (fileup.HasFile)
            {
                //wlh = ((TextBox)this.FindControl("wlh")).Text.Trim();
                SaveFile(fileup, newwlh + "_" + getDomain(), out filepath,oldwlh,newwlh, out originalname);
                //更新文件目录
                string sqlupdatefilecolum = string.Format("update PGI_BASE_PART_DATA_FORM set upload='{0}' where id='{1}'", filepath, instanceid.ToString());
                DbHelperSQL.ExecuteSql(sqlupdatefilecolum);

                AttachUpload.AttachUpload_Edit("insert", formid, stepid
                                    , newwlh, originalname, filepath
                                    , originalname.Substring(originalname.LastIndexOf('.') + 1)
                                    , "fileupload", Session["UserId"].ToString());
                flag = true;
            }
        }
        BindData();
        //执行流程相关事宜
        if (instanceid > 0)
        {
            var titletype = sformsate.Left(4) == "edit" ? "刀具修改" : "刀具申请";
            string title = titletype + "["+newwlh+ "][" + Request.Form["wlmc"]+"]"+Request.Form["ms"]; //设定表单标题
            
            //将实例id,表单标题给流程script
            script += "$('#instanceid',parent.document).val('" + instanceid.ToString() + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');" +
                 "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + instanceid.ToString() + "');}";
            //保存自定义（保存...）
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok",
            //     "$('#instanceid',parent.document).val('" + instanceid + "');" +
            //     "$('#customformtitle',parent.document).val('" + title + ")');parent.flowSaveIframe(true);",
            //     true);
        }
        else
        {
            //Response.Write("保存失败!");
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
        SaveData(type.SelectedValue,out flag);

        //保存当前流程
        if(flag==true)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + " parent.flowSave(true);", true);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok",script+" parent.flowSave(true);",true); 

        }
        
    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        SaveData(type.SelectedValue,out flag);
        //发送
        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(Page,this.GetType(), "ok", script + " parent.flowSend(true);", true);           
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

    public string getDomain()
    {
        string value = "";
        foreach(ListItem item in ddldomain.Items)
        {
            if (item.Selected)
            {
                value = value + item.Value + ";";
            }
        }
        return value.TrimEnd(';');
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ViewState["type"] = type.SelectedValue;
       
        var wlh= DaoJuMat.GetWLH("01", type.SelectedValue.Left(2));
        var purchaser= DaoJuMat.GetPurchaser(type.SelectedItem.Text.Trim(),"");
        var txtWlh = (TextBox)this.FindControl("wlh");
        var txtPurchaser = (TextBox)this.FindControl("purchaser");
        if (txtWlh!= null)
        {
            txtWlh.Text = wlh;
        }
        if (txtPurchaser != null)
        {
            txtPurchaser.Text = purchaser;            
        }
        if (purchaser == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "nopurchaser", "layer.alert('尚未设定此申请类型请购人。请联络IT更新设定');");
        }
    }
    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\MaterialBase";
    public void SaveFile(FileUpload fileupload,string subpath,out string filepath,string oldWlh,string newWlh,out string originalname)
    {
        var path = MapPath("~") +  savepath + "\\" + subpath;
        //Create directory
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        //save file
        var filename = "";
        if (fileupload.HasFile)
        {
            filename = fileupload.FileName.Replace(oldWlh,newWlh);
            path = path + "\\" + filename;            
            fileupload.SaveAs(path.Replace("&", "_").TrimStart(' '));           
        }
        //return save path
        filepath ="\\"+ savepath + "\\" + subpath+ "\\"+filename.Replace("&", "_").TrimStart(' ');
        originalname = filename.Replace("&", "_").TrimStart(' ');
    }
    #endregion

    protected void class_SelectedIndexChanged(object sender, EventArgs e)
    {
        // var tbltype = DbHelperSQL.Query("select distinct [name] as value from PGI_BASE_PART_ddl where ").Tables[0];
        // fun.initDropDownList((DropDownList)this.FindControl("class"), tbltype, "value", "value");
        BaseFun fun = new BaseFun();
        var ddlclass = (DropDownList)this.FindControl("class");
        var tbl = DbHelperSQL.Query("select '' as value ,'-请选择-' as text union all  select value, name as text from PGI_BASE_PART_ddl where type = '"+ddlclass.SelectedItem.Text+"'").Tables[0];
        fun.initDropDownList(type, tbl, "value", "text");
    }

    #region gv_attachfile

    public void BindData()
    {
        string wlh_str = "";
        var wlh = (TextBox)this.FindControl("wlh");
        if (wlh != null)
        {
            wlh_str = wlh.Text;
        }

        DataSet ds = AttachUpload.AttachUpload_List("select", formid, stepid, wlh_str);
        gv_AttachList.DataSource = ds;
        gv_AttachList.Columns[0].Visible = true;
        gv_AttachList.DataBind();
        gv_AttachList.Columns[0].Visible = false;

        if (Request["instanceid"] != null && Request["instanceid"] != "")
        {
            gv_AttachList.Columns[3].Visible = false;
        }
        else
        {
            gv_AttachList.Columns[3].Visible = true;
        }
    }

    protected void gv_AttachList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_attach = ((HyperLink)gv_AttachList.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_attach;

        AttachUpload.AttachUpload_delete("delete", id);

        if (System.IO.File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
        BindData();
    }

    protected void gv_AttachList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_AttachList.SelectedIndex = -1;
        gv_AttachList.PageIndex = e.NewPageIndex;

        BindData();
    }
    #endregion

}


