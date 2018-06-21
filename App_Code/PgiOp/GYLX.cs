using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// GYGS 的摘要说明
/// </summary>
public class GYLX
{
    public GYLX()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();


    public DataTable GYLX_query(string pgi_no, string pn, string ver,string ddl_typeno)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@pgi_no",pgi_no),
           new SqlParameter("@pn",pn),
           new SqlParameter("@ver",ver),
           new SqlParameter("@typeno",ddl_typeno)
      };
        return SQLHelper.GetDataTable("Report_GYLX", param);

    }
}