using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Wuliu_tr_hist 的摘要说明
/// </summary>
public class Capacity
{
    public Capacity()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    SQLHelper SQLHelper = new SQLHelper();

    /// <summary>
    /// 人员&产能核查报表
    /// </summary>
    ///  <param name="type">人员emp或产能cap</param>
    /// <param name="domain">公司别</param>
    /// <param name="op">工序</param>
    /// <returns></returns>
    public DataTable Report_Base_CapacityPlan(string type, string domain, string op)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@type",type),
           new SqlParameter("@domain",domain),
           new SqlParameter("@op",op)
      };
        return SQLHelper.GetDataTable("usp_Report_Base_CapacityPlan", param);

    }

    /// <summary>
    /// 人员&产能核查上传资料
    /// </summary>
    ///  <param name="pgi_no">工艺代码</param>
    /// <param name="start_date">起始日期</param>
    /// <param name="end_date">结束日期</param>
    /// <returns></returns>
    public DataTable Upload_Base_CapacityPlan(string pgi_no, string start_date, string end_date)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@pgi_no",pgi_no),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date)
      };
        return SQLHelper.GetDataTable("usp_Upload_Base_CapacityPlan", param);
    }

}