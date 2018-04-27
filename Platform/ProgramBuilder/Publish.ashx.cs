using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.ProgramBuilder
{
    /// <summary>
    /// Publish 的摘要说明
    /// </summary>
    public class Publish : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["pid"];
            if (!id.IsGuid()) return;
            if (new RoadFlow.Platform.ProgramBuilder().Publish(id.ToGuid()))
            {
                context.Response.Write("成功发布!");
                return;
            }
            else
            {
                context.Response.Write("成功失败!");
                return;
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