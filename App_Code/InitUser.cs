using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Maticsoft.DBUtility;

using System.Web.UI;
using System.Web.UI.WebControls;
using MES.Model;
using System.Collections;

/// <summary>
///BaseFun 的摘要说明
/// </summary>
public abstract class InitUser
{
	public InitUser()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// V1.0获取指定人员详细信息：工号，职称（建议使用V2.0）
    /// </summary>
    /// <param name="pg"></param>
    /// <param name="LOGON_USER"></param>
    public static void  GetLoginUserInfo(Page pg,string LOGON_USER)
    {        
        
            string strUser = LOGON_USER;
            pg.Session["loginUser"] = strUser.Substring(strUser.IndexOf(@"\") + 1);
            string loginUser = "";
            if (pg.Session["loginUser"] != null)
            {
                loginUser = pg.Session["loginUser"].ToString();
            }
            //object empid = DbHelperSQL.GetSingle("select empid from AD_ACCOUNT where lower(ADAccount)=lower('" + loginUser + "') ");

            object empid = DbHelperSQL.GetSingle("SELECT workcode FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");
            object job = DbHelperSQL.GetSingle("SELECT jobtitlename FROM V_HRM_EMP_MES where lower(ADAccount)=lower('" + loginUser + "') ");

            pg.Session["empid"] = empid == null ? "" : empid.ToString();
            pg.Session["job"] = job == null ? "" : job.ToString();
        
    }
    /// <summary>
    /// V2.0获取指定人员详细信息：工号，姓名，包括部门，职称，直属主管，组别
    /// </summary>
    /// <param name="WorkCode">工号(2参数至少输入一个)</param>
    /// <param name="AdAccount">登入AD账号(2参数至少输入一个)</param>
    /// <returns>LoginUser</returns>
    public static LoginUser GetLoginUserInfo(string WorkCode,string AdAccount)
    {
         
        if (AdAccount.IndexOf(@"\") >= 0)
        {   //如果传入参数是完整 域名\AD,则格式化
            AdAccount = AdAccount.Substring(AdAccount.IndexOf(@"\") + 1);
        }

        LoginUser model = null;
        DataTable dt = DbHelperSQL.Query("SELECT  * FROM V_HRM_EMP_MES where workcode='" + WorkCode + "' or ADAccount='"+ AdAccount + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataRow dr= dt.Rows[0];
            model = new LoginUser();
            model.UserId = dr["workcode"].ToString();
            model.UserName = dr["lastname"].ToString();
            model.ADAccount = dr["ADAccount"].ToString();
           // model.JobTitleId = dr[""].ToString();
            model.JobTitleName = dr["JobTitleName"].ToString();
            model.SuperviserId = dr["manager_workcode"].ToString();
            model.SuperviserName = dr["manager_name"].ToString();
            model.ManagerId = dr["zg_workcode"].ToString();
            model.ManagerName = dr["zg_name"].ToString();
            model.ManagerADAccount = dr["manager_ad_account"].ToString();
            // model.DepartId = dr[""].ToString();
            model.DepartName = dr["dept_name"].ToString();
            model.GroupName = dr["departmentname"].ToString();
            model.Domain= dr["domain"].ToString();
            model.DomainName= dr["gc"].ToString();
            model.Telephone= dr["telephone"].ToString();
            model.Car = dr["Car"].ToString();
        }
        return model;
    }
    //是否存在该员工,且是该角色的人员
    public static bool IsRoleUser(string modelName,string RoleName,string loginUser)
    {
        bool result = false;//select * from z_UserRole a where a.modelName='' and EmpName=''
        object obj = DbHelperSQL.GetSingle("SELECT  Count(1) FROM  z_UserRole where enable=1 and modelName='"+ modelName + "' and rolename='"+ RoleName + "' and EmpName= lower('" + loginUser + "')  ");
        if(obj.ToString()=="1")
        {
            result = true;
        }
        return result;
    }




     /// <summary>
     /// 产生流程
     /// </summary>
     /// <returns></returns>
    public static int generateFlow()
    {

        return 1;
    }
    /// <summary>
    /// 签核
    /// </summary>
    /// <returns></returns>
    public static int SignFlow()
    {
        return 1;
    }

}