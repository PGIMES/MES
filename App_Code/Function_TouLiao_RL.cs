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
///Function_TouLiao_RL 的摘要说明
/// </summary>
public class Function_TouLiao_RL
{
	public Function_TouLiao_RL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();

    public DataTable GouLiao_RL_CUM_query(string material_type, string emp_banbie, string emp_banzhu,string equip_no)
    {
 //        @material_type nchar(10),
 //@emp_banbie nchar(10),
 // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@material_type",material_type),
           new SqlParameter("@emp_banbie",emp_banbie),
            new SqlParameter("@emp_banzhu",emp_banzhu),
               new SqlParameter("@equip_no",@equip_no)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_InputMaterial_RL_CUM_Query1", param);


    }


    public DataTable GouLiao_Hejin_query(string equip_no)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {
        
               new SqlParameter("@equip_no",@equip_no)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_InPutMaterial_Hejin", param);


    }




    public DataTable GouLiao_RL_query(string startdate, string enddate, string equip_name,string hejing,string banbie,string emp_name)
    {
//      @equip_name varchar(30),
//@hejin varchar(20),
//@start_date varchar(20),
//@end_date varchar(20),
//@emp_banbie varchar(10),
//@emp_name varchar(10)

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@hejing",hejing),
            new SqlParameter("@start_date",startdate ),

              new SqlParameter("@end_date",enddate ),
           new SqlParameter("@emp_banbie",banbie),
            new SqlParameter("@emp_name",emp_name)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_InputMaterial_Query", param);


    }



    public DataTable TouLiao_RL_NO_query( string emp_banbie, string emp_banzhu,string equip_no,string hejing)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)
 //       @emp_banbie nchar(10),
 //@emp_banzhu nchar(10),
 // @equip_no nchar(10),
 
 //@hejing nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@emp_banbie",emp_banbie),
            new SqlParameter("@emp_banzhu",emp_banzhu),
             new SqlParameter("@equip_no",equip_no),
              new SqlParameter("@hejing",hejing)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_GetTouLiaoNO", param);


    }

    public int touliao_rl_insert(string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no, string equip_name, string equip_type, string hejing, string material_type, string crossweight, string tareweight, string netweight,string material_lot)
    {

        SqlParameter[] param = new SqlParameter[]
       {

//           @emp_no nchar(10),
// @emp_name nchar(10),
// @emp_banbie nchar(10),
// @emp_banzhu nchar(10),
//  @equip_no nchar(10),
//  @equip_name nchar(10),
//  @equip_type nchar(10),
// @hejing nchar(10),
// @material_type nchar(10),
// @crossweight decimal(18,0),
//@tareweight decimal(18,0),
//  @netweight decimal(18,0)

           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@equip_type",equip_type),
           new SqlParameter("@hejing",hejing),
           new SqlParameter("@material_type",material_type),
           new SqlParameter("@crossweight",crossweight),
           new SqlParameter("@tareweight",tareweight),
           new SqlParameter("@netweight",netweight),
            new SqlParameter("@material_lot",material_lot),
          // new SqlParameter("@bf_time",bf_time),
          // new SqlParameter("@af_wd",af_wd),
           //new SqlParameter("@af_time",af_time)

       };
        return SQLHelper.ExecuteNonQuery("MES_SP_InPutMaterial_RL_Insert", param);

    }
    //投料统计--获取年,月，日数据
    public DataTable TouLiao_RL_TongJi(string Type, string hejin, string day, string year, string month)
    {
        //*  proc [dbo].[MES_SP_InputMaterial_TongJi]@Type varchar(10),--年，月，日@hejin varchar(20),@day varchar(20),@year varchar(20),@month varchar(20)

        SqlParameter[] param = new SqlParameter[]
       {          
           new SqlParameter("@Type",Type),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@day",day),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_InputMaterial_TongJi", param);


    }
}