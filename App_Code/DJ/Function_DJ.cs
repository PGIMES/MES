using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
/// <summary>
///Function_DJ 的摘要说明
/// </summary>
public class Function_DJ
{
	public Function_DJ()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    SQLHelper SQLHelper = new SQLHelper();
    public DataTable GetDJ_Group_XM(int flag,string comp,string type,string xmh)
    {


        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@comp",comp),
            new SqlParameter("@type",type),
             new SqlParameter("@xmh",xmh)
            
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Mes_DJ_Group_xm_Query", param);


    }

    public int DJ_Group_XM_Insert(int flag, string comp, string type, string xmh,string uid,int id,string bzdj)
    {


        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@comp",comp),
           new SqlParameter("@type",type),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@uid",uid),
           new SqlParameter("@id",id),
           new SqlParameter("@bzdj",bzdj)
            
       };
        return SQLHelper.ExecuteNonQuery("Mes_DJ_Group_xm_Insert", param);


    }
}