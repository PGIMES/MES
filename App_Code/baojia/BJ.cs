using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
///Function_Base 的摘要说明
/// </summary>
public class BJ_CLASS
{
    public BJ_CLASS()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable BJ_BASE(string lookup_type)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@lookup_type",lookup_type)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_lookup_table_PRO", param);
    }

    public DataTable BJ_ljh(string baojia_no,string lb)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@baojia_no",baojia_no),
               new SqlParameter("@lb",lb)
       };
        //DataTable dt = new DataTable();
        //dt = SQLHelper.GetDataTable("BJ_ljh", param);
        //return dt;
        //DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("BJ_ljh", param);
    }
    public DataTable BJ_ljh_old_LJH(string baojia_no, string ljh)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@baojia_no",baojia_no),
               new SqlParameter("@ljh",ljh)
       };
        //DataTable dt = new DataTable();
        //dt = SQLHelper.GetDataTable("BJ_ljh", param);
        //return dt;
        //DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("BJ_ljh_old_LJH", param);
    }
    public DataTable baojia_ljh_ship(string baojia_no, string ljh)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@baojia_no",baojia_no),
               new SqlParameter("@ljh",ljh)
       };
        //DataTable dt = new DataTable();
        //dt = SQLHelper.GetDataTable("BJ_ljh", param);
        //return dt;
        //DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("baojia_ljh_ship", param);
    }
    public DataTable BJ_dept(string lookup_code)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@lookup_code",lookup_code)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_dept", param);
    }
    public DataTable BJ_dept_step(string lookup_code, string step)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@lookup_code",lookup_code),
               new SqlParameter("@step",step)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_dept", param);
    }
    public DataTable BJ_base(string type,string classify)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@type",type),
                new SqlParameter("@classify",classify)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_base_PRO", param);
    }
    public DataTable Getgv(string requestid, string gv)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@gv",gv),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_gv", param);
    }
    //public DataTable Getgv_update(string requestid, string gv)
    //{
    //    SqlParameter[] param = new SqlParameter[]
    //   {
    //       new SqlParameter("@requestid",requestid),
    //       new SqlParameter("@gv",gv),
    //   };
    //    DataTable dt = new DataTable();
    //    return SQLHelper.GetDataTable("Baojia_gv_update_test", param);
    //}
    public DataTable BJ_GetSPR(string project_size, String salesempid, string domain,string sfxy_bjfx,string customer_name,string wl_tk,string sfxj_cg,string bz_tk,string jijia_tk,string yz_tk,string ZG_empid)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@project_size",project_size),
               new SqlParameter("@salesempid",salesempid),
               new SqlParameter("@domain",domain),
               new SqlParameter("@sfxy_bjfx",sfxy_bjfx),
               new SqlParameter("@customer_name",customer_name),
               new SqlParameter("@wl_tk",wl_tk),
               new SqlParameter("@sfxj_cg",sfxj_cg),
               new SqlParameter("@bz_tk",bz_tk),
               new SqlParameter("@jijia_tk",jijia_tk),
               new SqlParameter("@yz_tk",yz_tk),
               new SqlParameter("@ZG_empid",ZG_empid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("Baojia_Get_Sign_Routing", param).Tables[1];
    }
    public DataTable GetBaojia_Get_Agreement_data(string requestid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_Get_Agreement_data", param);


    }
    public DataTable emp(string workcode)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@workcode",workcode)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_emp", param);
    }
    public DataTable BJ_Query_PRO(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@requestId",requestId)
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("BJ_Query_PRO", param);
    }

    public DataTable baojia_flow_select(int requestId,string flow,string stepid)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@requestId",requestId),
               new SqlParameter("@flow",flow),
               new SqlParameter("@stepid",stepid)
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("baojia_flow_select", param);
    }

    public DataTable BJ_baojia_no(string year)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@year",year)
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("BJ_baojia_no", param);
    }
    public DataTable BJ_baojia_no_all(string baojia_no, string turns)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@baojia_no",baojia_no),
               new SqlParameter("@turns",turns)
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("BJ_baojia_no_all", param);
    }
    public DataTable Get_year_heji(string baojia_no, string lb)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@baojia_no",baojia_no),
           new SqlParameter("@lb",lb),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_year_heji", param);


    }
    public DataTable GetBTN(string requestid, string empid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@empid",empid),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("baojia_flow", param);


    }
    public void SendMail(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@requestId", requestId) ,
           
          };
        SQLHelper.ExecuteNonQuery("Baojia_Sendmail_Every_Ticket", param);

    }
    public int BTN_Sales_sub(int requestId, string baojia_no, int turns, string sales_empid, string sales_name, string sales_ad, string customer_name, string end_customer_name, string customer_project, string project_size, string project_level, string is_stop, string create_by_empid, string create_by_name, string create_by_ad, string create_by_dept, string managerid, string manager, string manager_AD, string baojia_file_path, string baojia_desc, string domain,string baojia_start_date,string sfxy_bjfx,string create_date, string wl_tk,string bz_tk,string jijia_tk,string yz_tk,string sfxj_cg,string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@baojia_no", baojia_no) ,
                        new SqlParameter("@turns", turns) ,
                        new SqlParameter("@sales_empid", sales_empid),
                        new SqlParameter("@sales_name", sales_name),
                        new SqlParameter("@sales_ad", sales_ad),
                        new SqlParameter("@customer_name", customer_name),
                        new SqlParameter("@end_customer_name", end_customer_name),
                        new SqlParameter("@customer_project", customer_project),
                        new SqlParameter("@project_size", project_size),
                        new SqlParameter("@project_level", project_level),
                        new SqlParameter("@is_stop", is_stop),
                        new SqlParameter("@create_by_empid", create_by_empid),
                        new SqlParameter("@create_by_name", create_by_name),
                        new SqlParameter("@create_by_ad", create_by_ad),
                        new SqlParameter("@create_by_dept", create_by_dept),
                        new SqlParameter("@managerid", managerid),
                        new SqlParameter("@manager", manager),
                        new SqlParameter("@manager_AD", manager_AD),
                        new SqlParameter("@baojia_file_path", baojia_file_path),
                        new SqlParameter("@baojia_desc", baojia_desc),
                        new SqlParameter("@domain", domain),
                        new SqlParameter("@baojia_start_date", baojia_start_date),
                        new SqlParameter("@sfxy_bjfx", sfxy_bjfx),
                        new SqlParameter("@create_date", create_date),
                         new SqlParameter("@wl_tk", wl_tk),
                          new SqlParameter("@bz_tk", bz_tk),
                           new SqlParameter("@jijia_tk", jijia_tk),
                            new SqlParameter("@yz_tk", yz_tk),
                          new SqlParameter("@sfxj_cg", sfxj_cg),
                        new SqlParameter("@Submit",Submit)
      };
        return SQLHelper.ExecuteNonQuery("Baojia_bjxmxx_commit", param);
    }
    public int BTN_Sales_project_level(int requestId, string project_level, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
        new SqlParameter("@project_level",project_level) ,
                        new SqlParameter("@Submit",Submit)
      };
        return SQLHelper.ExecuteNonQuery("Baojia_update_project_level", param);
    }
    public int Baojia_Reback_Modify_Flow(int requestId, string type)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
 
       new SqlParameter("@type", type) ,

      };
        return SQLHelper.ExecuteNonQuery("Baojia_Reback_Modify_Flow", param);
    }
 
    public int Baojia_Dtl2Agreement(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@requestId",requestId) ,     
      };
        return SQLHelper.ExecuteNonQuery("Baojia_Dtl2Agreement", param);
    }
    public int Baojia_Sign_Submit(int requestId, int id, string sign_desc)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@id", id) ,
                        new SqlParameter("@sign_desc", sign_desc) ,

      };
        return SQLHelper.ExecuteNonQuery("Baojia_Sign_Submit", param);
    }
    public int Baojia_YZ_JJ_DELETE(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) 
                      

      };
        return SQLHelper.ExecuteNonQuery("Baojia_YZ_JJ_DELETE", param);
    }
}

