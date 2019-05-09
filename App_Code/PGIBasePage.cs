using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// 获取单号
    /// </summary>
    /// <param name="startTag"></param>
    /// <param name="column"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public string GetDanHao(string startTag, string column, string table)
    {
        string result = "";
        var sql = string.Format(" select  '" + startTag + "' + CONVERT(varchar(8), GETDATE(), 112) + right('000' + cast(isnull(right(max(" + column + "), 3) + 1, '001') as varchar), 3)  from " + table + " where " + column + " like '" + startTag + "' + CONVERT(varchar(8), GETDATE(), 112)+'%'");
        var value = DbHelperSQL.GetSingle(sql).ToString();
        result = value;
        return result;
    }

    #region "WebMethod"   
    /// <summary>
    /// 获取部门清单 返回Json string
    /// </summary>
    /// <param name="P1">域或工厂名称</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetDeptByDomain(string P1)
    {
        string result = "";
        var sql = string.Format(" select distinct dept_name as value,dept_name as text from  HR_EMP_MES   where domain='{0}' or gc='{1}' ", P1, P1);
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }

    //[System.Web.Services.WebMethod()]
    //public static string DelFile(string P1, string P2)
    //{
    //    string result = "";
    //    var sql = string.Format(" update   PUR_pr_main_Form set files=replace(files,'{0}','')  where prno='{1}' ", P1, P2);
    //    var value = DbHelperSQL.ExecuteSql(sql);
    //    if (value > 0)
    //    { result = value.ToString(); }
    //    return result;
    //}
    /// <summary>
    /// 根据部门取部门主管guid
    /// </summary>
    /// <returns>u_xxxxx-xxxx--xx--x-xxxx</returns>
    [System.Web.Services.WebMethod()]
    public static string getDeptLeaderByDept(string domain, string dept)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[HR_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (dept_name='" + dept + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 根据人员取部门主管guid
    /// </summary>
    /// <returns>u_xxxxx-xxxx--xx--x-xxxx</returns>
    [System.Web.Services.WebMethod()]
    public static string getDeptLeaderByEmp(string domain, string emp)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct Manager_workcode FROM [dbo].[HR_EMP_MES]  where 1=1 /* and (domain='" + domain + "' or '" + domain + "'='')*/ and  (workcode='" + emp + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取直属主管
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getSupervisorByEmp(string domain, string emp)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  distinct zg_workcode FROM [dbo].[HR_EMP_MES]  where (domain='" + domain + "' or '" + domain + "'='') and  (workcode='" + emp + "' ) )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取物料材料  P1：part  P2：domain
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getMaterial(string P1, string P2)
    {
        StringBuilder sb = new StringBuilder();

        // declare @part varchar(20),@domain varchar(20);set @part = 'P0122BA-01';set @domain = '200'


        sb.Append(" select * from( ");
        sb.Append("     select top 1 cailiao from[172.16.5.6].[report].dbo.track t where  'P' + right('0' + SUBSTRING(t.pgi_no, 2, 4), 4) = left('{0}', 5) ");//0:P1
        sb.Append("     union all ");
        sb.Append("     select(case when pt_part like 'R%' then pt_desc1 when pt_part like 'Z07%' then pt_drwg_loc end)  material  from qad.dbo.qad_pt_mstr where pt_part = '{1}' and pt_domain = '{2}' ");//1:P1,2:domain
        sb.Append("  ) t where cailiao is not null ");
        object obj = DbHelperSQL.GetSingle(string.Format(sb.ToString(), P1, P1, P2));
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 取项目负责人
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getProjectUserByProject(string domain, string pgino)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("  select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users where account=");
        sb.Append("   (SELECT  left(project_user,5) FROM form3_Sale_Product_MainTable where pgino=left('" + pgino + "',5) and (replace(replace(make_factory,'上海','100'),'昆山','200') like '%" + domain + "%' or '" + domain + "'='')   )");
        object obj = DbHelperSQL.GetSingle(sb.ToString());
        return obj == null ? "" : obj.ToString();
    }
    /// <summary>
    /// 字母晋级
    /// </summary>
    /// <param name="chr"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string ChrNext(string chr)
    {
        var Result = chr.ToString();
        if (chr.ToString() == "")
        { Result = 'A'.ToString(); }
        else
            for (char i = Convert.ToChar(chr); i <= 'z'; i++)
            {
                Result = ((char)(i + 1)).ToString();
                break;
            }

        return Result;
    }
    
    #endregion
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
