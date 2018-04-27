using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class Default : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
            Guid uid;
            if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
            {
                 string strUser = Request.ServerVariables["LOGON_USER"];
                 LoginUser LogUserModel = InitUser.GetLoginUserInfo("", strUser);
                 
                 Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
                // Response.Write(LogUserModel.UserGuid.ToString());
            }          
             
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;// base.CheckUrl(isEnd);
        }
    }
}