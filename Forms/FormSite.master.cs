using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
public partial class FormSiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
        Guid uid;
        string strUser = Request.ServerVariables["LOGON_USER"];
        LoginUser LogUserModel = null;
        if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
        {
}
            LogUserModel = InitUser.GetLoginUserInfo("02069", strUser);

            Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
            // Response.Write(LogUserModel.UserGuid.ToString());
        
       // string strUser = Request.ServerVariables["LOGON_USER"];
       // LoginUser LogUserModel = InitUser.GetLoginUserInfo("02069", strUser);
        Session["LogUser"] = LogUserModel;
                
        //Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId; ;
        Session["JobTitleName"] = LogUserModel.JobTitleName;
    }
}
