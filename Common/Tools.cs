using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Common
{
    public class Tools
    {
        /// <summary>
        /// 包含文件
        /// </summary>
        public static string IncludeFiles
        {
            get
            {
                return
                    string.Format(@"<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <link href=""{0}/Themes/Common.css"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}/Themes/{1}/Style/style.css"" id=""style_style"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}/Themes/{1}/Style/ui.css"" id=""style_ui"" rel=""stylesheet"" type=""text/css"" media=""screen""/> 
    <link href=""{0}/Scripts/font-awesome/css/font-awesome.min.css?v=1"" rel=""stylesheet"" type=""text/css""/>
    <!--[if lt IE 8]><link href=""{0}/Scripts/font-awesome/css/font-awesome-ie7.min.css"" rel=""stylesheet"" type=""text/css""/><![endif]-->
    <script type=""text/javascript"" src=""{0}/Scripts/My97DatePicker/WdatePicker.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/jquery-1.11.1.min.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/jquery.cookie.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/json.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.core.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.button.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.calendar.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.file.js?v=1""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.member.js?v=1""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.dict.js?v=1""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.menu.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.select.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.combox.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.tab.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.text.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.textarea.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.editor.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.tree.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.validate.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.window.js?ver=4""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.dragsort.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.selectico.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.selectdiv.js?v=1""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.accordion.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.grid.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.treetable.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.init.js""></script>"
    , BaseUrl, RoadFlow.Utility.Config.Theme);
            }
        }

        /// <summary>
        /// 系统所在目录
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                return "/RoadFlow"+RoadFlow.Utility.Config.BaseUrl;
            }
        }

        public static bool CheckLogin(out string msg)
        {
            msg = "";
            object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
            Guid uid;
            if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
            {
                return false;
            }

            //#if DEBUG
            return true; //正式使用时请注释掉这一行
            //#endif

            string uniqueIDSessionKey = RoadFlow.Utility.Keys.SessionKeys.UserUniqueID.ToString();
            var user = new RoadFlow.Platform.OnlineUsers().Get(uid);
            if (user == null)
            {
                return false;
            }
            else if (System.Web.HttpContext.Current.Session[uniqueIDSessionKey] == null)
            {
                return false;
            }
            else if (string.Compare(System.Web.HttpContext.Current.Session[uniqueIDSessionKey].ToString(), user.UniqueID.ToString(), true) != 0)
            {
                msg = string.Format("您的帐号在{0}登录,您被迫下线!", user.IP);
                return false;
            }
            return true;
        }

        public static bool CheckLogin(bool redirect = true)
        {
            string msg;
            if (!CheckLogin(out msg))
            {
                if (!redirect)
                {
                    System.Web.HttpContext.Current.Response.Write("登录验证失败!");
                    System.Web.HttpContext.Current.Response.End();
                    return false;
                }
                else
                {
                    string lastURL = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                    System.Web.HttpContext.Current.Response.Write("<script>top.lastURL='" + lastURL + "';top.currentWindow=window;top.login();</script>");
                    //System.Web.HttpContext.Current.Response.End();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查应用程序权限
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static bool CheckApp(out string msg, string appid = "")
        {
            msg = "";
            var userID = RoadFlow.Platform.Users.CurrentUserID;
            if (userID.IsEmptyGuid())
            {
                msg = "<script>top.login();</script>";
                System.Web.HttpContext.Current.Response.Write(msg);
                System.Web.HttpContext.Current.Response.End();
                return false;
            }
            appid = appid.IsNullOrEmpty() ? System.Web.HttpContext.Current.Request.QueryString["appid"] : appid;
            Guid appGuid;
            if (!appid.IsGuid(out appGuid))
            {
                System.Web.HttpContext.Current.Response.Write("权限检查错误!");
                System.Web.HttpContext.Current.Response.End();
                return false;
            }
            List<RoadFlow.Data.Model.MenuUser> menuusers = new RoadFlow.Platform.MenuUser().GetAll();
            string showSource;
            string params1;
            bool isUse = new RoadFlow.Platform.Menu().HasUse(appGuid, userID, menuusers, out showSource, out params1);
            if (!isUse)
            {
                System.Web.HttpContext.Current.Response.Write("权限检查错误!");
                System.Web.HttpContext.Current.Response.End();
                return false;
            }
            else
            {
                string url = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
                if (!url.IsNullOrEmpty())
                {
                    url = url.TrimStart('/');
                    if (!url.IsNullOrEmpty())
                    {
                        var subpageList = new RoadFlow.Platform.AppLibrarySubPages().GetAll().FindAll(p => p.Address.Contains(url, StringComparison.CurrentCultureIgnoreCase));
                        if (subpageList.Count > 0)
                        {
                            foreach (var sub in subpageList)
                            {
                                var menu = menuusers.Find(p => p.MenuID == appGuid && p.SubPageID == sub.ID
                                    && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                                if (menu != null)
                                {
                                    return true;
                                }
                            }
                            System.Web.HttpContext.Current.Response.Write("权限检查错误!");
                            System.Web.HttpContext.Current.Response.End();
                            return false;
                        }
                    }
                }
                return isUse;
            }
        }

        /// <summary>
        /// 检查访问地址
        /// </summary>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        public static bool CheckReferrer(bool isEnd = true)
        {
            var urlReferrer = HttpContext.Current.Request.UrlReferrer;
            if (urlReferrer == null)
            {
                if (isEnd)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Write("访问地址错误!");
                    HttpContext.Current.Response.End();
                }
                return false;
            }
            bool IsUri = HttpContext.Current.Request.Url.Host.Equals(urlReferrer.Host, StringComparison.CurrentCultureIgnoreCase);
            if (!IsUri && isEnd)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("访问地址错误!");
                HttpContext.Current.Response.End();
            }
            return IsUri;
        }

        /// <summary>
        /// 得到一个应用的按钮显示HTML
        /// </summary>
        /// <param name="showType"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetAppButtonHtml(int showType, string appid = "", string subpageid = "")
        {
            if (appid.IsNullOrEmpty())
            {
                appid = System.Web.HttpContext.Current.Request.QueryString["appid"];
            }
            var dicts = RoadFlow.Platform.MenuUser.getButtonsHtml(appid, subpageid);
            return dicts[showType];
        }

        /// <summary>
        /// 得到一个应用的按钮显示HTML
        /// </summary>
        /// <param name="showType"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetAppButtonHtml(string appid = "", string subpageid = "")
        {
            if (appid.IsNullOrEmpty())
            {
                appid = System.Web.HttpContext.Current.Request.QueryString["appid"];
            }
            var dicts = RoadFlow.Platform.MenuUser.getButtonsHtml(appid, subpageid, System.Web.HttpContext.Current.Request.QueryString["programid"]);
            return dicts;
        }
        
    }
}