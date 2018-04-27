using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.Home
{
    /// <summary>
    /// MenuRefresh 的摘要说明
    /// </summary>
    public class MenuRefresh : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string userid = context.Request.QueryString["userid"];
            string refreshID = context.Request.QueryString["refreshid"];
            bool showSource = "1" == context.Request.QueryString["showsource"];
            Guid refreshid;
            Guid uid = userid.IsGuid() ? userid.ToGuid() : RoadFlow.Platform.Users.CurrentUserID;
            if (!refreshID.IsGuid(out refreshid))
            {
                context.Response.Write("[]");
            }
            if (!refreshid.IsEmptyGuid())
            {
                context.Response.Write(new RoadFlow.Platform.Menu().GetUserMenuRefreshJsonString(uid, refreshid, Common.Tools.BaseUrl, showSource));
            }
            else
            {
                context.Response.Write("[]");
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