using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm
{
    /// <summary>
    /// Update 的摘要说明
    /// </summary>
    public class Update : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Guid userid = RoadFlow.Platform.Users.CurrentUserID;
            if (userid.IsEmptyGuid())
            {
                context.Response.Write("{}");
                context.Response.End();
                return;
            }
            var infoCount = new RoadFlow.Platform.ShortMessage().GetAllNoReadByUserID(userid).Count;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"infocount\":{0}", infoCount.ToString());
            sb.Append("}");
            context.Response.Write(sb.ToString());
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