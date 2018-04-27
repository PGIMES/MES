using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Info.ShortMessage
{
    /// <summary>
    /// UpdateStatus 的摘要说明
    /// </summary>
    public class UpdateStatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["id"];
            if (id.IsGuid())
            {
                new RoadFlow.Platform.ShortMessage().UpdateStatus(id.ToGuid());
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