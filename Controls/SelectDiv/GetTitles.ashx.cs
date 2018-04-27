using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;

namespace WebForm.Controls.SelectDiv
{
    /// <summary>
    /// GetTitles 的摘要说明
    /// </summary>
    public class GetTitles : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!WebForm.Common.Tools.CheckLogin(false))
            {
                context.Response.Write("");
                context.Response.End();
                return;
            }

            string values = context.Request.QueryString["values"];
            string titlefield = context.Request.QueryString["titlefield"];
            string pkfield = context.Request.QueryString["pkfield"];
            string applibaryid = context.Request.QueryString["applibaryid"];
            var applibary = new RoadFlow.Platform.AppLibrary().Get(applibaryid.ToGuid());
            if (applibary == null)
            {
                context.Response.Write(values);
                return;
            }
            var program = new RoadFlow.Platform.ProgramBuilder().Get(applibary.Code.ToGuid());
            if (program == null)
            {
                context.Response.Write(values);
                return;
            }
            var dbconn = new RoadFlow.Platform.DBConnection().Get(program.DBConnID);
            if (dbconn == null)
            {
                context.Response.Write(values);
                return;
            }
            string sql = "select " + titlefield + " from (" + program.SQL.ReplaceSelectSql().FilterWildcard(RoadFlow.Platform.Users.CurrentUserID.ToString()) + ") gettitletemptable where " + pkfield + " in(" + RoadFlow.Utility.Tools.GetSqlInString(values) + ")";
            DataTable dt = new RoadFlow.Platform.DBConnection().GetDataTable(dbconn, sql);
            StringBuilder titles = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                titles.Append(dr[0]);
                titles.Append(",");
            }
            context.Response.Write(titles.ToString().TrimEnd(','));

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