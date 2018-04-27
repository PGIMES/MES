using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.ProgramBuilder
{
    /// <summary>
    /// OutToExcel 的摘要说明
    /// </summary>
    public class OutToExcel : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!Common.Tools.CheckLogin(false))
            {
                context.Response.Write("登录失效,请重新登录再导出!");
                return;
            }
            string programID = context.Request.QueryString["programid"];
            if (!programID.IsGuid())
            {
                context.Response.Write("导出参数错误!");
                return;
            }
            RoadFlow.Platform.ProgramBuilder PB = new RoadFlow.Platform.ProgramBuilder();
            string exportTemplate, exportHeaderText, exportFileName;
            System.Data.DataTable dt = PB.GetExportDataTable(programID.ToGuid(), out exportTemplate, out exportHeaderText, out exportFileName);
            RoadFlow.Utility.NPOIHelper.ExportByWeb(dt, exportHeaderText, exportFileName, exportTemplate);
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