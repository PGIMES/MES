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


    public DataTable GYLX_query(string pgi_no, string pn, string ver,string ddl_typeno, string cpl,string pt_status, string applytype)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@pgi_no",pgi_no),
           new SqlParameter("@pn",pn),
           new SqlParameter("@ver",ver),
           new SqlParameter("@typeno",ddl_typeno),
           new SqlParameter("@cpl",cpl),
           new SqlParameter("@pt_status",pt_status),
           new SqlParameter("@applytype",applytype)
      };
        return SQLHelper.GetDataTable("Report_GYLX", param);

    }

    public DataTable GYLX_IsNeedCloseWork(DataTable dt, string lstypeno, string formno_main, string projectno_main, string pgi_no_t_main, string domain_main, string titlever,string containgp, string applytype)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@dt",dt),
            new SqlParameter("@formno",formno_main),
            new SqlParameter("@pgi_no",projectno_main),
            new SqlParameter("@pgi_no_t",pgi_no_t_main),
            new SqlParameter("@domain",domain_main),
            new SqlParameter("@ver",titlever),
            new SqlParameter("@containgp",containgp),
            new SqlParameter("@applytype",applytype)
      };

        string strsql = "";
        if (lstypeno == "机加")
        {
            strsql = "usp_GYLX_IsNeedCloseWork_product";
        }
        if (lstypeno == "压铸")
        {
            strsql = "usp_GYLX_IsNeedCloseWork_yz";
        }
        return SQLHelper.GetDataTable(strsql, param);

    }
}