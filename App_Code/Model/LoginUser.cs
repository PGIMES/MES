using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// LoginUser 的摘要说明
/// </summary>
public class LoginUser
{
    public LoginUser()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //       

    }
    /// <summary>
    /// 工号
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 工号Guid
    /// </summary>
    public Guid UserGuid { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// AD Account
    /// </summary>
    public string ADAccount { get; set; }
    /// <summary>
    /// 职称编号
    /// </summary>
    public string JobTitleId { get; set; }
    /// <summary>
    /// 职称名称
    /// </summary>
    public string JobTitleName { get; set; }
    /// <summary>
    /// 部门编号
    /// </summary>
    public string DepartId { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartName { get; set; }

    /// <summary>
    ///部门组别
    /// </summary>
    public string GroupName { get; set; }
   
    /// <summary>
    ///直属主管工号
    /// </summary>
    public string SuperviserId { get; set; }
    /// <summary>
    ///直属主管姓名
    /// </summary>
    public string SuperviserName { get; set; }
    
    /// <summary>
    ///部门主管工号
    /// </summary>
    public string ManagerId { get; set; }
    /// <summary>
    /// 部门主管姓名
    /// </summary>
    public string ManagerName { get; set; }
    /// <summary>
    /// 部门主管域账号
    /// </summary>
    public string ManagerADAccount { get; set; }
    public string Domain { get; set; }
    public string DomainName { get; set; }
    public string Telephone { get; set; }
}