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
///Moju 的摘要说明
/// </summary>
public class Moju
{
	public Moju()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable Moju_BX_Query(int flag,string dh,string year,string month,string start_date,string end_date,string bxr,string wxr,string moju_no,string moju_type,string moju_part,string sbjc,string wx_result)
    {


        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@bxr",bxr),
           new SqlParameter("@wxr",wxr),
           new SqlParameter("@moju_no",moju_no),
           new SqlParameter("@moju_type",moju_type),
           new SqlParameter("@moju_part",moju_part),
           new SqlParameter("@sbjc",sbjc),
           new SqlParameter("@wx_result",wx_result)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Mes_MoJu_BX_Query", param);


    }
    public int Moju_BX_Insert(int flag, string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string moju_no, string equip_no, string equip_name, string moju_type, string moju_part, string mo_no, string gz_type, string gz_ms,string bx_date,string stopped)
    {


        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@moju_no",moju_no),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@moju_type",moju_type),
           new SqlParameter("@moju_part",moju_part),
           new SqlParameter("@mo_no",mo_no),
           new SqlParameter("@gz_type",gz_type),
           new SqlParameter("@gz_ms",gz_ms),
           new SqlParameter("@bx_date",bx_date),
           new SqlParameter("@stopped",stopped)

       };
        return SQLHelper.ExecuteNonQuery("MES_MoJu_BX_Insert", param);


    }


    public DataTable MoJu_no_query(string equip_no, string moju_no)
    {


        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@moju_no",moju_no)
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_MOJuno_Query", param);


    }

    public bool IsInUse(string mojuno)
    {
        string sqlStr = "select iif(COUNT(1)>0,0,1) as IsInUse  from MES_SB_BX where   status<>'确认完成' and bx_moju_no='"+ mojuno + "'";
        string value = DbHelperSQL.GetSingle(sqlStr).ToString(); //SQLHelper.GetDataTable(sqlStr, null).Rows[0][0].ToString();
        return Convert.ToBoolean(Convert.ToInt16(value));
    }
}