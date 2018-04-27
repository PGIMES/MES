using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
/// <summary>
///Function_Change_MoJu 的摘要说明
/// </summary>
public class Function_Change_MoJu
{
	public Function_Change_MoJu()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();
    public int MoJu_Change_Insert(string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no, string equip_name, string change_type, string chang_reason, string reason_type, string moju_no_up, string mo_no_up, string part_up, string moju_no_down, string mo_no_down, string part_down, string moju_status_down, string moju_status_demo_down, string up_product_status, string change_startdate, string change_enddate, string moju_type_up, string moju_type_down,string moju_kw_up,string moju_kw_down)
    {
        //@emp_no nchar(10)
        //   ,@emp_name nchar(10)
        //   ,@emp_banbie nchar(10)
        //   ,@emp_banzhu nchar(10)
        //   ,@equip_no nchar(10)
        //   ,@equip_name nchar(10)
        //   ,@change_type nchar(10)
        //   ,@chang_reason nvarchar(50)
        //   ,@reason_type nvarchar(50)
        //   ,@moju_no nchar(10)
        //   ,@mo_no nchar(10)
        //   ,@part nvarchar(50)
        //   ,@moju_status nchar(10)
        //   ,@moju_status_demo nvarchar(50)
        //   ,@up_product_status nchar(10)
        //   ,@change_startdate datetime
        //   ,@change_enddate datetime
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@change_type",change_type),
           new SqlParameter("@chang_reason",chang_reason),
           new SqlParameter("@reason_type",reason_type),
           new SqlParameter("@moju_no_up",moju_no_up),
           new SqlParameter("@mo_no_up",mo_no_up),
           new SqlParameter("@part_up",part_up),

            new SqlParameter("@moju_no_down",moju_no_down),
           new SqlParameter("@mo_no_down",mo_no_down),
           new SqlParameter("@part_down",part_down),


           new SqlParameter("@moju_status_down",moju_status_down),
             new SqlParameter("@moju_status_demo_down",moju_status_demo_down),
                           // @moju_status_demo_down
           new SqlParameter("@product_status_up",up_product_status),
           new SqlParameter("@change_startdate",change_startdate),
           new SqlParameter("@change_enddate",change_enddate),
             new SqlParameter("@moju_type_up",moju_type_up),
           new SqlParameter("@moju_type_down",moju_type_down),
            new SqlParameter("@moju_kw_up",moju_kw_up),
             new SqlParameter("@moju_kw_down",moju_kw_down),
       };
        return SQLHelper.ExecuteNonQuery("MES_SP_MoJu_Change_Insert", param);

    }

    public int MoJu_Change_Update(string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no, string equip_name, string change_type, string chang_reason, string reason_type, string moju_no_up, string mo_no_up, string part_up, string moju_no_down, string mo_no_down, string part_down, string moju_status_down, string moju_status_demo_down, string up_product_status, string change_startdate, string change_enddate, string moju_type_up, string moju_type_down)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@change_type",change_type),
           new SqlParameter("@chang_reason",chang_reason),
           new SqlParameter("@reason_type",reason_type),
           new SqlParameter("@moju_no_up",moju_no_up),
           new SqlParameter("@mo_no_up",mo_no_up),
           new SqlParameter("@part_up",part_up),

            new SqlParameter("@moju_no_down",moju_no_down),
           new SqlParameter("@mo_no_down",mo_no_down),
           new SqlParameter("@part_down",part_down),


           new SqlParameter("@moju_status_down",moju_status_down),
             new SqlParameter("@moju_status_demo_down",moju_status_demo_down),
                           // @moju_status_demo_down
           new SqlParameter("@product_status_up",up_product_status),
           new SqlParameter("@change_startdate",change_startdate),
           new SqlParameter("@change_enddate",change_enddate),
             new SqlParameter("@moju_type_up",moju_type_up),
           new SqlParameter("@moju_type_down",moju_type_down),
       };
        return SQLHelper.ExecuteNonQuery("MES_SP_MoJu_Change_Update", param);

    }


    public DataTable GP_MoJu_query(string mojuno)
    {


        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@mojuno",mojuno),
            //new SqlParameter("@emp_banzhu",emp_banzhu),
            // new SqlParameter("@equip_no",equip_no),
            //  new SqlParameter("@hejing",hejing)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_GetMoJuNo", param);


    }

    public DataTable MoJu_Down_query(string equip_no,string moju_type)
    {


        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@moju_type_up",moju_type),
            // new SqlParameter("@equip_no",equip_no),
            //  new SqlParameter("@hejing",hejing)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_MOJuDown_Query", param);


    }

}