using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class PGIBasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
        //  base.OnInit(e);
        //   this.CheckUrl();
        // this.CheckLogin();
        //  this.CheckApp();
        //   this.InitInclude();


        object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
        Guid uid;
        string strUser = Request.ServerVariables["LOGON_USER"];
        LoginUser LogUserModel = null;
        if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
        {
        }
        LogUserModel = InitUser.GetLoginUserInfo("02069", strUser);

        Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
        // Response.Write(LogUserModel.UserGuid.ToString());

        // string strUser = Request.ServerVariables["LOGON_USER"];
        // LoginUser LogUserModel = InitUser.GetLoginUserInfo("02069", strUser);
        Session["LogUser"] = LogUserModel;

        //Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()] = LogUserModel.UserGuid;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId; ;
        Session["JobTitleName"] = LogUserModel.JobTitleName;

    }

        ///// <summary>
        ///// 初始化页面
        ///// </summary>
        //protected virtual void InitInclude()
        //{
        //    if (Page.Header != null)
        //    {
        //        Page.Header.Controls.AddAt(Page.Header.Controls.Count - 1, new System.Web.UI.WebControls.Literal() { Text = Tools.IncludeFiles });
        //    }
        //}

        ///// <summary>
        ///// 检查是否登录
        ///// </summary>
        ///// <returns></returns>
        //protected virtual bool CheckLogin(bool isRedirect = true)
        //{
        //    return Tools.CheckLogin(isRedirect);
        //}

        ///// <summary>
        ///// 检查访问地址
        ///// </summary>
        ///// <param name="isEnd"></param>
        ///// <returns></returns>
        //protected virtual bool CheckUrl(bool isEnd = true)
        //{
        //    return Tools.CheckReferrer(isEnd);
        //}

        ///// <summary>
        ///// 检查应用权限
        ///// </summary>
        ///// <param name="isEnd"></param>
        ///// <returns></returns>
        //protected virtual bool CheckApp()
        //{
        //    string msg;
        //    return Tools.CheckApp(out msg);
        //}

        ///// <summary>
        ///// 当前登录用户
        ///// </summary>
        //public static RoadFlow.Data.Model.Users CurrentUser
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUser;
        //    }
        //}
        ///// <summary>
        ///// 当前登录用户ID
        ///// </summary>
        //public static Guid CurrentUserID
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUserID;
        //    }
        //}
        ///// <summary>
        ///// 当前用户姓名
        ///// </summary>
        //public static string CurrentUserName
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUserName;
        //    }
        //}

        ///// <summary>
        ///// 当前用户部门
        ///// </summary>
        //public static RoadFlow.Data.Model.Organize CurrentUserDept
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentDept;
        //    }
        //}

        ///// <summary>
        ///// 当前用户部门ID
        ///// </summary>
        //public static Guid CurrentUserDeptID
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentDeptID;
        //    }
        //}

        ///// <summary>
        ///// 当前用户部门名称
        ///// </summary>
        //public static string CurrentUserDeptName
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentDeptName;
        //    }
        //}

        ///// <summary>
        ///// 当前用户单位
        ///// </summary>
        //public static RoadFlow.Data.Model.Organize CurrentUserUnit
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUnit;
        //    }
        //}

        ///// <summary>
        ///// 当前用户单位ID
        ///// </summary>
        //public static Guid CurrentUserUnitID
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUnitID;
        //    }
        //}

        ///// <summary>
        ///// 当前用户单位名称
        ///// </summary>
        //public static string CurrentUserUnitName
        //{
        //    get
        //    {
        //        return RoadFlow.Platform.Users.CurrentUnitName;
        //    }
        //}

        ///// <summary>
        ///// 当前日期时间
        ///// </summary>
        //public static DateTime CurrentDateTime
        //{
        //    get
        //    {
        //        return RoadFlow.Utility.DateTimeNew.Now;
        //    }
        //}

        ///// <summary>
        ///// 应用程序路径
        ///// </summary>
        //public static string SitePath
        //{
        //    get
        //    {
        //        return Tools.BaseUrl;
        //    }
        //}
    }
