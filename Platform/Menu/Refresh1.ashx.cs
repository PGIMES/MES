using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Menu
{
    /// <summary>
    /// Refresh1 的摘要说明
    /// </summary>
    public class Refresh1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            new RoadFlow.Platform.MenuUser().RefreshUsers();
            context.Response.Write("更新完成!");
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