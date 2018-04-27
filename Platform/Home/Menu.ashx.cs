using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.Home
{
    /// <summary>
    /// Menu 的摘要说明
    /// </summary>
    public class Menu : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string userid = context.Request.QueryString["userid"];
            Guid uid = userid.IsGuid() ? userid.ToGuid() : RoadFlow.Platform.Users.CurrentUserID;
            bool showSource = "1" == context.Request.QueryString["showsource"];
            if (uid.IsEmptyGuid())
            {
                context.Response.Write("[]");
            }
            else
            {
                context.Response.Write(new RoadFlow.Platform.Menu().GetUserMenuJsonString(uid, Common.Tools.BaseUrl, showSource));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}