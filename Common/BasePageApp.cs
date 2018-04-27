using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Common
{
    public class BasePageApp : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CheckLogin();
            this.InitInclude();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        protected virtual void InitInclude()
        {
            if (Page.Header != null)
            {
                Page.Header.Controls.AddAt(Page.Header.Controls.Count - 1, new System.Web.UI.WebControls.Literal() { Text = Tools.IncludeFiles });
            }
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckLogin()
        {
            return RoadFlow.Platform.WeiXin.Organize.CheckLogin();
        }

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                return RoadFlow.Platform.WeiXin.Organize.CurrentUserID;
            }
        }

        /// <summary>
        /// 当前登录用户姓名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                return RoadFlow.Platform.WeiXin.Organize.CurrentUserName;
            }
        }

        /// <summary>
        /// 当前日期时间
        /// </summary>
        public static DateTime CurrentDateTime
        {
            get
            {
                return RoadFlow.Utility.DateTimeNew.Now;
            }
        }

        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string SitePath
        {
            get
            {
                return Tools.BaseUrl;
            }
        }
    }
}