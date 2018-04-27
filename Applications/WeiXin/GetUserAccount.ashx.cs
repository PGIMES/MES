using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Applications.WeiXin
{
    /// <summary>
    /// GetUserAccount 的摘要说明
    /// </summary>
    public class GetUserAccount : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string code = context.Request.QueryString["code"];
            if (code.IsNullOrEmpty())
            {
                context.Response.Write("身份验证失败");
                context.Response.End();
                return;
            }
            string account = new RoadFlow.Platform.WeiXin.Organize().GetUserAccountByCode(code);
            if (account.IsNullOrEmpty())
            {
                context.Response.Write("身份验证失败-");
                context.Response.End();
                return;
            }
            var user = new RoadFlow.Platform.Users().GetByAccount(account);
            if (user == null)
            {
                context.Response.Write("未找到帐号对应的人员");
                context.Response.End();
                return;
            }
            context.Response.Cookies.Add(new HttpCookie("weixin_userid", user.ID.ToString()));
            context.Session.Add(RoadFlow.Utility.Keys.SessionKeys.UserID.ToString(), user.ID.ToString());
            var lastURLCookie = context.Request.Cookies.Get("LastURL");
            var lastURL = lastURLCookie == null ? "" : lastURLCookie.Value;
            if (!lastURL.IsNullOrEmpty())
            {
                context.Response.Redirect(lastURL);
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