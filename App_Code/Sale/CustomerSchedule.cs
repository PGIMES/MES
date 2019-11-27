using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// GYGS 的摘要说明
/// </summary>
public class CustomerSchedule
{
    public CustomerSchedule()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();

    public DataTable CS_IsModifyByBom(DataTable dt, string part, string domain)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@dt",dt),
            new SqlParameter("@part",part),
            new SqlParameter("@domain",domain)
      };
        return SQLHelper.GetDataTable("usp_CS_IsSign_HQ", param);

    }
}