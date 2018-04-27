using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Baojia_Report_sql 的摘要说明
/// </summary>
public class Baojia_Report_sql
{
    public Baojia_Report_sql()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper BaojiaSQLHelper = new SQLHelper();
    public DataTable Get_Baojia_Task_Query_Data(string year,string baojia_start_date_start,string baojia_start_date_end,
         string baojia_status,string sign_status,string RequireDate,string zzgc,string depart,string Update_name,
         string ISDelay)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@year",year),
           new SqlParameter("@baojia_start_date_start", baojia_start_date_start),
           new SqlParameter("@baojia_start_date_end", baojia_start_date_end),
           new SqlParameter("@baojia_status", baojia_status),
           new SqlParameter("@sign_status", sign_status),
           new SqlParameter("@RequireDate", RequireDate),
           new SqlParameter("@zzgc",zzgc),
           new SqlParameter("@depart", depart),
           new SqlParameter("@Update_name",Update_name),
           new SqlParameter("@ISDelay", ISDelay)
     };
      
     return  BaojiaSQLHelper.GetDataTable("Baojia_Task_Query", param);
 

    }
    public DataTable Get_Baojia_Process_Query_Data(string baojia_no,string customer_name,string update_user,string baojia_status)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@baojia_no",baojia_no),
           new SqlParameter("@customer_name", customer_name),
           new SqlParameter("@update_user", update_user),
           new SqlParameter("@baojia_status", baojia_status)
           
     };
        return BaojiaSQLHelper.GetDataTable("Baojia_Progress_Query", param);

    }
    public DataTable Get_Baojia_history_update_data(string baojia_no, string customer_name, string update_user)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@baojia_no",baojia_no),
           new SqlParameter("@customer_name", customer_name),
           new SqlParameter("@update_user", update_user)
     };
        return BaojiaSQLHelper.GetDataTable("Baojia_history_UpdateBaojia", param);

    }

    public DataSet Get_Baojia_FenXi_ZD_Project_mst(string condition,string project_level)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@condition",condition),
           new SqlParameter("@project_level", project_level)
     };

        return BaojiaSQLHelper.GetDataSet("Baojia_TJ_FenXi_ZD_Project", param);


    }
    public DataTable Get_Baojia_FenXi_ZD_Project_dtl(string condition, string project_level,string value)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@condition",condition),
           new SqlParameter("@project_level", project_level),
           new SqlParameter("@value", value)

     };
        return BaojiaSQLHelper.GetDataTable("Baojia_TJ_FenXi_ZD_Project_DTL", param);
    }

}