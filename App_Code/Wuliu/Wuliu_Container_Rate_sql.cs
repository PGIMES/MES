using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using Maticsoft.DBUtility;

/// <summary>
/// Wuliu_Container_Rate_sql 的摘要说明
/// </summary>
public class Wuliu_Container_Rate_sql
{
    public Wuliu_Container_Rate_sql()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    Wuliu_SF_SQLHelp SQLHelper = new Wuliu_SF_SQLHelp();
    public DataSet Get_jzxlyl_rate_query(string datetime, string flag)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@datetime",datetime),
           new SqlParameter("@flag",flag)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("jzxlyl_rate_query", param);
    }
    public DataSet Get_jzxlyl_rate_datail(string datetime, string jzx_type,string query_type,string flag)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@datetime",datetime),
           new SqlParameter("@jzx_type",jzx_type),
           new SqlParameter("@query_type",query_type),
           new SqlParameter("@flag",flag)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("jzxlyl_rate_query_details", param);
    }
}