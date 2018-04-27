using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// GetFormGridHtml 的摘要说明
    /// </summary>
    public class GetFormGridHtml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dbconnID=context.Request.Form["dbconnid"];
            string dataFormat=context.Request.Form["dataformat"];
            string dataSource=context.Request.Form["datasource"];
            string dataSource1 = context.Request.Form["datasource1"] ?? "";
            string params1 = context.Request.Form["params"];

            context.Response.Write(new RoadFlow.Platform.WorkFlowForm().GetFormGridHtml(dbconnID, dataFormat, dataSource, dataSource1.FilterWildcard(), params1));
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