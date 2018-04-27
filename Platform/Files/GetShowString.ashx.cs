using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Files
{
    /// <summary>
    /// GetShowString 的摘要说明
    /// </summary>
    public class GetShowString : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(RoadFlow.Platform.Files.GetFilesShowString(context.Request["files"], Common.Tools.BaseUrl + "/Platform/Files/Show.ashx"));
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