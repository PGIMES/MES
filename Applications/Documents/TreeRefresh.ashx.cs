using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Applications.Documents
{
    /// <summary>
    /// TreeRefresh 的摘要说明
    /// </summary>
    public class TreeRefresh : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string refreshid = context.Request["refreshid"];
            if (refreshid.IsGuid())
            {
                context.Response.Write(new RoadFlow.Platform.DocumentDirectory().GetTreeRefreshJsonString(refreshid.ToGuid()));
            }
            else
            {
                context.Response.Write("[]");
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