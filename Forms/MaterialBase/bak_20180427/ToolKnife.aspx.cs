using System;
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

public partial class ToolKnife : PGIBasePage
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
        { 
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
        if (id!=""&& Request["domain"]!=null&& Request["domain"].ToString()!="") //id.IsInt()
        {
            var key = "declare @id varchar(100);select @id=id from PGI_BASE_PART_DATA_form where wlh='" + id.ToString() + "' and domain='" + Request["domain"].ToString() + "';";
            dtMst = DbHelperSQL.Query(key+";select * from PGI_BASE_PART_DATA_form where id=@id").Tables[0];      //id=              
        }

        if (dtMst.Rows.Count > 0)
        {
            strType = dtMst.Rows[0]["type"].ToString();
            //txtid.Value= dtMst.Rows[0]["id"].ToString();
            var item = type.Items.FindByText(strType);
            if (item != null)
            {
                strType = item.Value;
            }

            setCheckBoxListSelectValue(ddldomain, dtMst.Rows[0]["domain"].ToString(), ';', false);
            CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
            CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
            CreateDate.Text = dtMst.Rows[0]["CreateDate"].DateFormat("yyyy-MM-dd").ToString().Left(10);
            purchaser.Text= dtMst.Rows[0]["purchaser"].ToString();

            type.Attributes.Add("disabled", "disabled");
            if (this.FindControl("class")!=null)
            {
                ((DropDownList)this.FindControl("class")).Attributes.Add("disabled", "disabled");
            }
            
            
        }
        else
        {
            strType = type.SelectedItem.Value;
        }
         
        //--== 第 步:装载控件========================================================================================================
        loadControl(strType);
        if (this.FindControl("wlh")!=null)
        {
            ((TextBox)this.FindControl("wlh")).Attributes.Add("readonly","readonly");
        }
        

        if (dtMst.Rows.Count > 0)
        {   
            Pgi.Auto.Control.SetControlValue(strType, "", this, dtMst.Rows[0]);
            var item = type.Items.FindByText(dtMst.Rows[0]["type"].ToString());
            if (item != null)
            {
                item.Selected=true;
            }
        }
        
        //Load Check Script        
        var sqlScript = "select control_event as script from auto_form where form_type='"+ type.SelectedValue + "' and isnull(control_id,'')<>'' and isnull(control_event,'')<>'' order by control_order;";
        var dtscript = DbHelperSQL.Query(sqlScript).Tables[0];
        for (int i = 0; i < dtscript.Rows.Count; i++)
        {
            ValidScript = ValidScript + dtscript.Rows[i]["script"].ToString();
        }

        //获取每步骤栏位状态设定值，方便前端控制其可编辑性 keep
        string FlowID = Request.QueryString["flowid"]; 
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0"; 
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);

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
        var sqlwlh = string.Format(" select isnull(max(wlh),'')wlh from PGI_BASE_PART_DATA_FORM where left(wlh,5) = 'Z{0}{1}' and right(wlh,1)<>'X'", P1.Left(2), P2.Left(2));        
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
        var sql= string.Format(" SELECT purchaser_id  FROM[MES].[dbo].[PGI_BASE_PART_DDL]    where name = '{0}'", P1);
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "layer.alert('" + msg + "');",true);//$(#'" + ls[i].Code + "').focus();
                return ;
            }
        }
        var common = ls.Where(r => r.Code == "type").ToList();
        common[0].Value = this.type.SelectedItem.Text;
        //加入主键值
        var domain = ls.Where(r => r.Code == "domain").ToList().Single();
        domain.Key = "1";
         

        //var lscom = new Pgi.Auto.Common();
        //lscom.Code = "id";
        //lscom.Key = "1";
        //lscom.Value = txtid.Value;
        //ls.Add(lscom);


        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        int instanceid = Pgi.Auto.Control.UpdateValues(ls, "PGI_BASE_PART_DATA_FORM");
        //如果是签核或修改 取传递过来instanceid值
        if(Request["instanceid"]!=null&& Request["instanceid"] != "")
        {
          //  instanceid = Convert.ToInt32(Request["instanceid"]);
        }
        //Save file
        var fileup = (FileUpload)this.FindControl("UPLOAD");
        var filepath = "";
        
        if (fileup != null)
        {   if (fileup.HasFile)
            {
                SaveFile(fileup, ((TextBox)this.FindControl("wlh")).Text.Trim(), out filepath);
                //更新文件目录
                //string sqlupdatefilecolum = string.Format("update PGI_BASE_PART_DATA_FORM set upload='{0}' where id='{1}'", filepath, txtid.Value);
                //DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
                flag = true;
            }
        }
        //执行流程相关事宜
        if (instanceid > 0)
        {
            //string title = "刀具申请"+((TextBox)this.FindControl("wlh")).Text; //设定表单标题

            //将实例id,表单标题给流程script
            //script = "$('#instanceid',parent.document).val('" + instanceid.ToString() + "');" +
            //     "$('#customformtitle',parent.document).val('" + title + "');";
            StringBuilder sb = new StringBuilder();
            sb.Append(" update PGI_BASE_PART_DATA ");
            sb.Append(" set  class='4010-'+b.class,  coping_lb=b.coping_lb,   typeDesc=b.typeDesc,   djlx=b.djlx,  pp=b.pp, pt_status=b.pt_status, wlh=b.wlh, wlmc=b.wlmc, ms=b.ms,   ");
            sb.Append("   gys =b.gys, jg=b.jg, edsm=b.edsm, edxmcs=b.edxmcs, lycs=b.lycs, lycs_week=b.lycs_week, dwcb=b.dwcb, zpjyl=b.zpjyl, xyph=b.xyph, djjs=b.djjs, zongchang=b.zongchang ");
            sb.Append(" ,  cpl=b.cpl, aqkc=b.aqkc,  ");
            sb.Append("  UPLOAD =b.UPLOAD, dhzq=b.dhzq, quantity_min=b.quantity_min, quantity_max=b.quantity_max, ddbs=b.ddbs, buyer_planner=b.buyer_planner, purchase_make=b.purchase_make ");
            sb.Append("  , make_days=b.make_days, purchase_days=b.purchase_days, lycs_fweek=b.lycs_fweek, lycs_tweek=b.lycs_tweek,  ");
            sb.Append("  var1 =b.var1, var2=b.var2, var3=b.var3, var4=b.var4, var5=b.var5, var6=b.var6, var7=b.var7, var8=b.var8, var9=b.var9, var10=b.var10, var11=b.var11 ");
            sb.Append("  , var12=b.var12, var13=b.var13,var14=b.var14, float1=b.float1, float2=b.float2, float3=b.float3, float4=b.float4, float5=b.float5 ");
            sb.Append("      , otherms=b.otherms ");
            sb.Append("  from PGI_BASE_PART_DATA a, PGI_BASE_PART_DATA_FORM b ");
            sb.Append("  where  a.wlh=b.wlh and a.wlh='"+Request["instanceid"]+"' and a.Domain='"+Request["domain"]+"'  ");
            DbHelperSQL.ExecuteSql(sb.ToString());
            //是否修磨刀
            if (this.FindControl("coping_lb")!=null)
            {
                var strEx = "select count(1) from PGI_BASE_PART_DATA where wlh='" + Request["instanceid"] + "'+'X'";
                if(!DbHelperSQL.Exists(strEx))
                {
                    sb.Clear();
                    //PGI_BASE_PART_DATA_FORM
                    sb.Append(" insert into PGI_BASE_PART_DATA_FORM ");
                    sb.Append(" (class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh, wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5) ");
                    sb.Append(" select ");
                    sb.Append("  class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh+'X', wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5 ");
                    sb.Append(" from PGI_BASE_PART_DATA_FORM ");
                    sb.Append(" where wlh = '" + Request["instanceid"] + "' and a.Domain='" + Request["domain"] + "'; ");
                    //insert into PGI_BASE_PART_DATA
                    sb.Append(" insert into PGI_BASE_PART_DATA ");
                    sb.Append(" ('4010-'+class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh, wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5) ");
                    sb.Append(" select ");
                    sb.Append("  class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh+'X', wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5 ");
                    sb.Append(" from PGI_BASE_PART_DATA_FORM ");
                    sb.Append(" where wlh = '" + Request["instanceid"] + "'+'X' and a.Domain='" + Request["domain"] + "'; ");
                  //  DbHelperSQL.ExecuteSql(sb.ToString());
                }
            }
            flag = true;
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
        if (flag == true)
        {
            // Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok",script+" parent.flowSave(true);",true); 
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " alert('保存成功');", true);
        }
        else
        { Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " alert('保存失败');", true); }
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
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
        else
        {

        }
    }
    #endregion



    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ViewState["type"] = type.SelectedValue;
        var wlh=  ToolKnife.GetWLH("01", type.SelectedValue.Left(2));
        var purchaser= ToolKnife.GetPurchaser(type.SelectedItem.Text.Trim(),"");
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
    public void SaveFile(FileUpload fileupload,string subpath,out string filepath)
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
            filename = fileupload.FileName;
            path = path + "\\" + filename;            
            fileupload.SaveAs(path.Replace("&", "_"));           
        }
        //return save path
        filepath ="\\"+ savepath + "\\" + subpath+ "\\"+filename.Replace("&","_");
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
}


