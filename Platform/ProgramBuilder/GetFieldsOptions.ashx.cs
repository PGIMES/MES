using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.ProgramBuilder
{
    /// <summary>
    /// GetFieldsOptions 的摘要说明
    /// </summary>
    public class GetFieldsOptions : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string appid = context.Request["applibaryid"];
            var app = new RoadFlow.Platform.AppLibrary().Get(appid.ToGuid());
            if (app == null || !app.Code.IsGuid())
            {
                context.Response.Write("");
                return;
            }
            var options = new RoadFlow.Platform.ProgramBuilder().GetFieldsOptions(app.Code.ToGuid(), "");
            context.Response.Write(options);
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