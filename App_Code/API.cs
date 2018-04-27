using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
/// <summary>
///API 的摘要说明
/// </summary>
public class API
{
	public API()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    SQLHelper SQLHelper = new SQLHelper();


    public DataTable GP_JobList()
    {


        SqlParameter[] param = new SqlParameter[]
       { };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Monitor_query", param);


    }
}