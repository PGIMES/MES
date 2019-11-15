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

    public string SQ_StepID = "";
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



        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }
}