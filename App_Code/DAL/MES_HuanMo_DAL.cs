using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;

/// <summary>
///MES_HuanMo_DAL 的摘要说明
/// </summary>
public class MES_HuanMo_DAL
{
	public MES_HuanMo_DAL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();

    public static DataSet Query(string SQLString)
    {
        string connString = ConfigurationManager.AppSettings["connstringMoJu"];
        SqlConnection connection = new SqlConnection(connString);
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
            command.Fill(ds, "ds");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;


    }

    public static int ExecuteNonQuery(string strSql)
    {
        string connString = ConfigurationManager.AppSettings["connstringMoJu"];
        SqlConnection connection = new SqlConnection(connString);
        try
        {
            int retcount = -1;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                   // cmd.Parameters.AddRange(Parms);
                    conn.Open();
                    retcount = cmd.ExecuteNonQuery();
                    conn.Close();
                 //   cmd.Parameters.Clear();

                }
            }

            return retcount;
        }
        catch
        {

            return -1;
        }

    }

    public DataSet GetMoju_statsu(string mojuno)
    {
        string strSql = "select  top 1 * from moju where mojuno='"+mojuno+"' AND weizhi='生产'";
        return Query(strSql.ToString());
    }

    public DataSet GetMoJuJS_User()
    {
        string sql = "select distinct   js_user from  moju_rk  ";
        return Query(sql);
    }

    public DataSet GetMoJuRK_User()
    {
        string sql = "select distinct   rk_user from  moju_rk  where rk_user  in('刘思华','王道胜','石鸢翔','汪新平','史猛')";
        return Query(sql);
    }
    public DataSet GetMoci(string mojuno)
    {
        string lssql = "select moci,weizhi,id from moju where mojuno='"+mojuno+"'";
       
        return Query(lssql);
    }

       public static string GetNo(string lstype, string lsNo)
    {
        string lsreturnNO = lsNo;
       
        string sql = "select * from  AllId  where type='" + lstype + "'";
        DataSet lds = Query(sql);
        int ln = 1;
        if (lds.Tables[0].Rows[0]["update_date"].ToString() == "")
        {
            ln = 1;
        }
        else if (Convert.ToDateTime(lds.Tables[0].Rows[0]["update_date"].ToString()).ToShortDateString() != System.DateTime.Now.ToShortDateString())
        {
            ln = 1;
        }
        else
        {
            ln=Convert.ToInt32(lds.Tables[0].Rows[0]["allid"]);
            
        }
        lsreturnNO += DateTime.Now.ToString("yyyyMMdd") + ln.ToString("000");
        int ln1 = ln + 1;
        string sql1 = "update allid set allid="+ln1+",update_date=getdate() where type='" + lstype + "'";
        ExecuteNonQuery(sql1);
        return lsreturnNO;
    }
       private static SqlParameter[] GetMoJuRKQueryParameters()
       {
           SqlParameter[] parms = new SqlParameter[] 
           {   
			new SqlParameter("@mojuno", SqlDbType.VarChar )
			};


           return parms;

       }

       public static System.Data.DataSet MoJuQuery(string lsmojuno)
       {


           SqlParameter[] parms = GetMoJuRKQueryParameters();
           parms[0].Value = "%" + lsmojuno + "%";

           string connstring = ConfigurationSettings.AppSettings["connstringMoJu"];
           string lssql = "select moju.*,(select top 1 moci from moju_ly where moju_id=moju.id order by moju_id desc) as ly_moci ";
           lssql += ",(select top 1 ly_date from MoJu_LY where moju_id=moju.id order by id desc) as ly_date";
           lssql += ",(select top 1 DATEDIFF(day,ly_date,GETDATE()) from MoJu_LY where moju_id=moju.id order by id desc) as ly_day";
           lssql += " from moju  where flag=1";
           if (lsmojuno != "")
           {
               lssql += " and mojuno like @mojuno";
           }
           DataSet ds = Query(lssql);
           return ds;
      

       }


       public static int GetALLID(string lstype)
       {
          
           string sql = "select allid from  AllId  where type='" + lstype + "'";
           int lnid = Convert.ToInt32(Query(sql).Tables[0].Rows[0][0].ToString());
           string sql1 = "update allid set allid=allid+1 where type='" + lstype + "'";
           ExecuteNonQuery(sql1);

           return lnid;

       }

       public static int MoJuRKInsert(string lsmoju_id, string lsrk_user, string lsrk_type, string lsjs_user, string lsrk_moci, string lsmoci_sum, string lsrk_date, string lsrk_remark, string lscreate_by, string lsrk_weizhi, string lsby_type, string lsrk_no, string lsyzj_no)
       {
           int ln = 0;
           
           if (lsrk_moci == "")
           {
               lsrk_moci = "0";
           }
          
           string lssql1 = "";


           lssql1 = "update moju set weizhi='保养', moci='" + lsmoci_sum + "' where id='" + lsmoju_id + "' and weizhi='生产'";
            string lsdate = Query("select top 1 ly_date from moju_ly where moju_id='" + lsmoju_id + "' ").Tables[0].Rows[0][0].ToString();

           string lsrk_id =GetALLID("ALL").ToString();
           string lssql = "insert into moju_rk(rk_id,moju_id,rk_user,rk_type,js_user,rk_moci,moci_sum,rk_date,rk_remark,create_by,create_date,rk_weizhi,by_type,rk_no,yzj_no,ly_date) values(";
           lssql += "" + lsrk_id + ",'" + lsmoju_id + "','" + lsrk_user + "','" + lsrk_type + "','" + lsjs_user + "','" + lsrk_moci + "','" + lsmoci_sum + "',getdate(),'" + lsrk_remark + "','" + lscreate_by + "',getdate(),'" + lsrk_weizhi + "','" + lsby_type + "','" + lsrk_no + "','" + lsyzj_no + "','" + lsdate + "')";

           //增加一条维修记录
           string lskw = Query("select kw from moju where id='"+lsmoju_id+"'").Tables[0].Rows[0][0].ToString();
           string lsno = GetNo("MOJUBY_NO", "BY");
          // string lssql4 = "insert into MoJu_BY(moju_id,by_no,by_type,by_date,by_moci,sum_moci,create_by,create_date,by_kw,rk_id) values (";
          // lssql4 += "" + lsmoju_id + ",'" + lsno + "','" + lsby_type + "',getdate()," + lsrk_moci + "," + lsmoci_sum + ",'" + lscreate_by + "',getdate(),'" + lskw + "'," + lsrk_id + ")";
           ExecuteNonQuery(lssql1);
           ExecuteNonQuery(lssql);
          //  ExecuteNonQuery(lssql4);
 
           return ln;
       }
}