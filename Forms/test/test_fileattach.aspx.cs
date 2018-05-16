using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_test_test_fileattach : System.Web.UI.Page
{
    string m_sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["lv"] = "SQXX";
        //string FlowID = "A";
        //string StepID = "A";


        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
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
                    txt_AppById.Value = LogUserModel.UserId;
                    txt_AppByName.Value = LogUserModel.UserName;

                    txt_CreateById.Value = LogUserModel.UserId;
                    txt_CreateByName.Value = LogUserModel.UserName;
                }
            }
            else//编辑
            {

            }
            

        }


    }

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        //bool flag = SaveData();
        //保存当前流程
        //if (flag == true)
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        //}

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
        //bool flag = SaveData();
        //发送
        //if (flag == true)
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        //}
    }
    #endregion

}