using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.Files
{
    /// <summary>
    /// Tree1 的摘要说明
    /// </summary>
    public class Tree1 : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!Common.Tools.CheckLogin(false))
            {
                context.Response.Write("[]");
                return;
            }
            context.Response.Write("[" + new RoadFlow.Platform.Files().GetUserDirectoryJson(RoadFlow.Platform.Users.CurrentUserID, "", true) + "]");
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