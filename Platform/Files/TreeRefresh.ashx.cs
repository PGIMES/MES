using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.Files
{
    /// <summary>
    /// TreeRefresh 的摘要说明
    /// </summary>
    public class TreeRefresh : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!Common.Tools.CheckLogin(false))
            {
                context.Response.Write("[]");
                return;
            }
            string dir = context.Request.QueryString["refreshid"];
            if (dir.IsNullOrEmpty())
            {
                context.Response.Write("[]");
                return;
            }
            
            context.Response.Write(new RoadFlow.Platform.Files().GetUserDirectoryJson(RoadFlow.Platform.Users.CurrentUserID, dir.DesDecrypt()));
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