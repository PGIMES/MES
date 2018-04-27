using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Applications.Documents
{
    /// <summary>
    /// Tree1 的摘要说明
    /// </summary>
    public class Tree1 : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(new RoadFlow.Platform.DocumentDirectory().GetTreeJsonString());
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