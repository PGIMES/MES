using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
/// <summary>
///Function_GP 的摘要说明
/// </summary>
public class Function_GP
{
	public Function_GP()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    SQLHelper SQLHelper = new SQLHelper();


    public int GP_Temp_insert(string dh, string hejin, string si, string fe, string cu, string mg, string mn, string cr, string ni, string zn, string ti, string pb, string sn, string al, string sr, string sf)
    {
//        (@dh varchar(20),
//@hejin varchar(20),
//@si varchar(18),
//@fe varchar(18),
//@cu varchar(18),
//@mg varchar(18),
//@mn varchar(18),
//@cr varchar(18),
//@ni varchar(18),
//@zn varchar(18),
//@ti varchar(18),
//@pb varchar(18),
//@sn varchar(18),
//@al varchar(18),
//@sr varchar(18),
//@sf varchar(18)

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@dh",dh),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@si",si),
           new SqlParameter("@fe",fe),
           new SqlParameter("@cu",cu),
           new SqlParameter("@mg",mg),
           new SqlParameter("@mn",mn),
           new SqlParameter("@cr",cr),
           new SqlParameter("@ni",ni),
           new SqlParameter("@zn",zn),
           new SqlParameter("@ti",ti),
           new SqlParameter("@pb",pb),
           new SqlParameter("@sn",sn),
           new SqlParameter("@al",al),
           new SqlParameter("@sr",sr),
           new SqlParameter("@sf",sf)

       };
        return SQLHelper.ExecuteNonQuery("MES_SP_GP_InsertTemp", param);

    }



    public DataTable GP_Temp_query(string dh)
    {
        

        SqlParameter[] param = new SqlParameter[]
       {
          
           new SqlParameter("@dh",dh),
            //new SqlParameter("@emp_banzhu",emp_banzhu),
            // new SqlParameter("@equip_no",equip_no),
            //  new SqlParameter("@hejing",hejing)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_SP_GetGP_Temp", param);


    }

    public int GP_Detail_insert(int flag,string source,string dh, string hejin, string luhao,string sbno,string create_time,
        string confirm_time,string filename,string emp_gh,string emp_name,string emp_banbie,string emp_banzhu,string gys,string bihao)
    {
       

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@source",source),
           new SqlParameter("@dh",dh),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@create_time",create_time),
           new SqlParameter("@confirm_time",confirm_time),
           new SqlParameter("@filename",filename),
           new SqlParameter("@emp_gh",emp_gh),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@gys",gys),
           new SqlParameter("@bihao",bihao)
       };
        return SQLHelper.ExecuteNonQuery("usp_GPDetail_Insert", param);

    }


}