using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
    /// SQLHelper 的摘要描述
    /// </summary>
public class SQLHelperMachine
{

    private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServicesMachine"].ConnectionString;

  
    #region ExecuteReader():執行并返回一個數據集


    /// <summary>
    /// 執行一段SqlCommand并返回一個結果集
    /// </summary>
    /// <param name="strSql">sql語句</param>
    /// <param name="Parms">參數</param>
    /// <returns></returns>
    public SqlDataReader ExecuteReader(string strSql, SqlParameter[] Parms)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(Parms);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                    conn.Close();
                }
            }
          
        }
        catch 
        {
           

            return (SqlDataReader)null;
        }

        
    }
    #endregion

   
    #region ExecuteNonQuery():執行并返回一個結果，1代表成功，-1代表失敗
    
    /// <summary>
    /// 執行并返回一個結果，1代表成功，-1代表失敗
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="Parms"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(string strSql, SqlParameter[] Parms)
    {
        try
        {
            int retcount = -1;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.AddRange(Parms);
                    conn.Open();
                    retcount = cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();

                }
            }

            return retcount;
        }
        catch 
        {
            
            return -1;
        }
   
    }
    #endregion

    #region ExecuteScalar():返回數據集第一行第一列的數據
    
    /// <summary>
    /// 返回數據集第一行第一列的數據
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="Parms">參數</param>
    /// <returns></returns>
    public object ExecuteScalar(string strSql, SqlParameter[] Parms)
    {
        object retobject = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(Parms);
                    conn.Open();
                    retobject = cmd.ExecuteScalar();
                    conn.Close();
                    cmd.Parameters.Clear();

                }
            }
        }
        catch 
        {
           
            
        }


        return retobject;
    }
    #endregion

    #region GetDataTable():根據sql返回一個數據表

    /// <summary>
    /// 根據sql返回一個數據表
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="Parms"></param>
    /// <returns></returns>
    public DataTable GetDataTable(string strSql, SqlParameter[] Parms)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandTimeout = 300000;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(Parms);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    conn.Close();

                    cmd.Parameters.Clear();

                }
            }
            return dt;

        }
        catch 
        {
            
            return (DataTable)null;
        }
        
    }
    #endregion

    #region GetDataSet():根據sql返回一個數據集

    /// <summary>
    /// 根據sql返回一個數據集
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="Parms"></param>
    /// <returns></returns>
    public DataSet GetDataSet(string strSql, SqlParameter[] Parms)
    {
        
        try
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(Parms);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);
                    conn.Close();
                    cmd.Parameters.Clear();
                }
            }
            return ds;
        }
        catch
        {
          
            return (DataSet)null;
        }
        
    }
    #endregion

    public static SqlConnection GetCon()
    {
        SqlConnection conn = new SqlConnection(ConnectionString);
        return conn;
    }

    public static bool ExSql(string sql)
    {
        SqlConnection con = SQLHelper.GetCon();
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            con.Dispose();
        }
    }
    public static DataSet reDs(string sql)
    {
        SqlConnection con = SQLHelper.GetCon();//连接上数据库
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;//返回DataSet对象
    }
}