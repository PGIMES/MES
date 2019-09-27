using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// GYGS 的摘要说明
/// </summary>
public class PackSheme
{
    public PackSheme()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();

    public DataTable PackSheme_IsModifyByBom(DataTable dt, string lstypeno, string formno, string part, string domain, string site, string ship)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@dt",dt),
            new SqlParameter("@typeno",lstypeno),
            new SqlParameter("@formno",formno),
            new SqlParameter("@part",part),
            new SqlParameter("@domain",domain),
            new SqlParameter("@site",site),
            new SqlParameter("@ship",ship)
      };
        return SQLHelper.GetDataTable("usp_PackSheme_IsModifyByBom", param);

    }
}