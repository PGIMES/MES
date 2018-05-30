using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_fileattach : System.Web.UI.Page
{
    string m_sid = ""; string m_sid_ori = "";
    string formid = @"/forms/test/test_fileattach", stepid_att = @"apply";

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["lv"] = "SQXX";
        //string FlowID = "A";
        //string StepID = "A";


        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
            m_sid_ori = m_sid;
        }

        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        Session["LogUser"] = LogUserModel;

        if (!IsPostBack)
        {
            //更新文件heguiqin begin
            DataTable dt_o = (DataTable)Session["attach_forms"];
            if (dt_o != null)
            {
                DataRow[] drs = dt_o.Select("formid='" + formid + "' and stepid='" + stepid_att + "' and wlh='" + m_sid + "'");
                if (drs.Length > 0)
                {
                    foreach (DataRow item in drs)
                    {
                        dt_o.Rows.Remove(item);
                    }
                    Session["attach_forms"] = dt_o;
                }
            }
            //end

            //获取每步骤栏位状态设定值，方便前端控制其可编辑性

            //if (Request.QueryString["flowid"] != null)
            //{
            //    FlowID = Request.QueryString["flowid"];
            //}

            //if (Request.QueryString["stepid"] != null)
            //{
            //    StepID = Request.QueryString["stepid"];
            //}

            if (m_sid=="")//新增
            {                
                if (LogUserModel != null)//表头基本信息赋值
                {
                    
                    txt_CreateDate.Value = System.DateTime.Now.ToString();
                    txt_CreateById.Value = LogUserModel.UserId;
                    txt_CreateByName.Value = LogUserModel.UserName;
                    txt_DomainName.Value = LogUserModel.DomainName;
                    txt_DepartName.Value = LogUserModel.DepartName;

                    txt_AppById.Value = LogUserModel.UserId;
                    txt_AppByName.Value = LogUserModel.UserName;
                }
            }
            else//编辑
            {
                //表头赋值
                DataTable ldt = DbHelperSQL.Query("select * from Test_Main_Form where TestNo='" + this.m_sid + "'").Tables[0];
                txt_testno.Value = this.m_sid;

                txt_CreateDate.Value = ldt.Rows[0]["CreateDate"].ToString();
                txt_CreateById.Value = ldt.Rows[0]["CreateById"].ToString();
                txt_CreateByName.Value = ldt.Rows[0]["CreateByName"].ToString();
                txt_DomainName.Value = ldt.Rows[0]["CreateDomain"].ToString();
                txt_DepartName.Value = ldt.Rows[0]["CreateDepartName"].ToString();

                txt_AppById.Value = ldt.Rows[0]["AppById"].ToString();
                txt_AppByName.Value = ldt.Rows[0]["AppByName"].ToString();
                rbl_domain.SelectedValue= ldt.Rows[0]["AppDomain"].ToString();

            }
            

        }


    }

    private bool SaveData()
    {
        bool bflag = false; string sql = "";
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "CS" + System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
            this.m_sid = Pgi.Auto.Public.GetNo("TEST", lsid, 0, 4);

            txt_testno.Value = this.m_sid;

            sql = @"INSERT INTO [dbo].[Test_Main_Form]
                               ([TestNo],[CreateDate],[CreateById],[CreateByName],[CreateDomain],[CreateDepartName]
                                ,[AppById],[AppByName],[AppDomain])
                         VALUES
                               ('{0}','{1}','{2}','{3}','{4}','{5}'
                            ,'{6}','{7}','{8}')";
            sql = string.Format(sql, m_sid, txt_CreateDate.Value, txt_CreateById.Value, txt_CreateByName.Value, txt_DomainName.Value, txt_DepartName.Value
                            , txt_AppById.Value, txt_AppByName.Value, rbl_domain.SelectedValue);
        }
        else
        {
            sql = @"UPDATE [dbo].[Test_Main_Form] SET [AppById]='{1}',[AppByName]='{2}'  WHERE [TestNo]='{0}'";
            sql = string.Format(sql, m_sid, txt_AppById.Value, txt_AppByName.Value);
        }

        Pgi.Auto.Common ls_main = new Pgi.Auto.Common();
        ls_main.Sql = sql;
        ls_sum.Add(ls_main);

        //更新文件heguiqin begin
        AttachUpload au = new AttachUpload();
        DataTable dt_o = (DataTable)Session["attach_forms"]; DataRow[] drs;
        List<Pgi.Auto.Common> ls_att_sum = au.GetAttachSql(dt_o, MapPath("~"), formid, stepid_att, m_sid, m_sid_ori, true, out drs);
        foreach (Pgi.Auto.Common item in ls_att_sum)
        {
            ls_sum.Add(item);
        }
        //end

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        if (ln > 0)
        {
            //更新文件heguiqin begin
            au.DealAttachFile(drs, MapPath("~"), formid, stepid_att, m_sid, true);            
            if (drs.Length > 0)
            {
                foreach (DataRow item in drs)
                {
                    dt_o.Rows.Remove(item);
                }
                Session["attach_forms"] = dt_o;
            }
            //end

            bflag = true;

            
            //不知道下面的语句是干嘛用的
            string title = "Test测试申请--" + this.m_sid;
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
        bool flag = SaveData();
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
        bool flag = SaveData();
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion


    protected void btn_attach_Click(object sender, EventArgs e)
    {
        string option = "edit";

        DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" + Request.QueryString["stepid"] + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" 
            + Request.QueryString["flowid"] + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='申请'").Tables[0];

        if (ldt_flow.Rows.Count == 0)
        {
            option = "view";
        }

        if (m_sid == "") { option = "edit"; }

        if (Request.QueryString["display"] != null)
        {
            option = "view";
        }
        
        
        
        string url = @"/FileAttach/Attach_Forms.aspx?lpname=test&formid="+ formid + "&stepid="+ stepid_att + "&formno="+ m_sid+ "&option="+ option;
        string lsstr = "layer.open({title: '<b>相关联文件</b>',closeBtn: 1,type: 2,area: ['600px', '500px'],fixed: false, maxmin: true,content: '" + url + "'});";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", lsstr, true);
    }

}