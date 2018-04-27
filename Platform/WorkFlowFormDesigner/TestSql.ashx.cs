using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// TestSql 的摘要说明
    /// </summary>
    public class TestSql : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!WebForm.Common.Tools.CheckLogin(false))
            {
                context.Response.Write("未登录");
                return;
            }
            string sql = context.Request["sql"];
            string dbconn = context.Request["dbconn"];

            if (sql.IsNullOrEmpty() || !dbconn.IsGuid())
            {
                context.Response.Write("SQL语句为空或未设置数据连接");
                return;
            }

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var dbconn1 = bdbconn.Get(dbconn.ToGuid());
            if (bdbconn.TestSql(dbconn1, sql))
            {
                context.Response.Write("SQL语句测试正确");
            }
            else
            {
                context.Response.Write("SQL语句测试错误");
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