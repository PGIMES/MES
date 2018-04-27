using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

/// <summary>
///Function_PW 的摘要说明
/// </summary>
public class Function_PW
{
	public Function_PW()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    SQLHelper SQLHelper = new SQLHelper();
    public int PW_Clear_Insert(string emp_no,string emp_name,string emp_banbie,string emp_banzhu,string equip_no,string equip_name,string checkitem,string checkresult,string checkdemo)
    {


        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@checkitem",checkitem),
           new SqlParameter("@checkresult",checkresult),
           new SqlParameter("@checkdemo",checkdemo)
 
       };
        return SQLHelper.ExecuteNonQuery("MES_PW_Insert", param);

    }

    public int PW_Add_Insert(string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no, string equip_name, string wl,int bs)
    {


        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@wl",wl),
           new SqlParameter("@bs",bs)
 
       };
        return SQLHelper.ExecuteNonQuery("MES_PW_AddInsert", param);

    }
}