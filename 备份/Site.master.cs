using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strUser = Request.ServerVariables["LOGON_USER"];
        //Session["loginUser"] = strUser.Substring(strUser.IndexOf(@"\") + 1);
        //string loginUser = "";
        //if (Session["loginUser"] != null)
        //{
        //    loginUser = Session["loginUser"].ToString();
        //}
        ////object empid = DbHelperSQL.GetSingle("select empid from AD_ACCOUNT where lower(ADAccount)=lower('" + loginUser + "') ");

        //object empid = DbHelperSQL.GetSingle("SELECT workcode FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");
        //object job = DbHelperSQL.GetSingle("SELECT jobtitlename FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");

        //Session["empid"] = empid == null ? "" : empid.ToString();
        //Session["job"] = job == null ? "" : job.ToString();


        object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
        Guid uid;
        string strUser = Request.ServerVariables["LOGON_USER"];
        LoginUser LogUserModel = null;

        if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
        {
        }
        LogUserModel = InitUser.GetLoginUserInfo("", strUser);
        if (LogUserModel != null)
        {
            Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;

            Session["LogUser"] = LogUserModel;
            //Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
            Session["UserAD"] = LogUserModel.ADAccount;
            Session["UserId"] = LogUserModel.UserId; 
            Session["JobTitleName"] = LogUserModel.JobTitleName;
        }
        else//如果是公用账号
        {
            strUser = Request.ServerVariables["LOGON_USER"];
            Session["loginUser"] = strUser.Substring(strUser.IndexOf(@"\") + 1);
            string loginUser = "";
            if (Session["loginUser"] != null)
            {
                loginUser = Session["loginUser"].ToString();
            }
            object empid = DbHelperSQL.GetSingle("select empid from AD_ACCOUNT where lower(ADAccount)=lower('" + loginUser + "') ");
            Session["UserAD"] = loginUser;
            Session["UserId"] = loginUser;
           // object empid = DbHelperSQL.GetSingle("SELECT workcode FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");
            object job = DbHelperSQL.GetSingle("SELECT jobtitlename FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");
            Session["empid"] = empid == null ? "" : empid.ToString();
            Session["job"] = job == null ? "" : job.ToString();
        }






    }
}
