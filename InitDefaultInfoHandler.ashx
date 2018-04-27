<%@ WebHandler Language="C#" Class="InitDefaultInfoHandler" %>

using System;
using System.Web;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public class InitDefaultInfoHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string flag = context.Request.QueryString["flag"];
        string shebei=context.Request.QueryString["shebei"];
        
        if (flag == "RL")//RL -熔炼信息
        {
            SqlParameter[] paras={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds=DbHelperSQL.Query("MES_SP_GetDefaultInfo @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0) {
                strInfo = ds.Tables[0].Rows[0][0].ToString();
            
            }
            
            context.Response.Write(strInfo);
        }
        if (flag == "BULE")//BuLe -布勒信息
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_BuLe @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }
        if (flag.ToLower() == "yazhu")//压铸机信息
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_YaZhu @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }
        //获取设备维修状态
        if (flag.ToLower() == "equipstatus")//压铸机信息
        {
            context.Response.ContentType = "text/json";
            string strInfo=GetEquipStatus(context.Request.QueryString["shebei"]);
            context.Response.Write(strInfo);
        }
        if (flag.ToLower() == "eqcls")//Equator测量室信息
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_Equator @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }
        //
        if (flag.ToLower() == "jjcls")//机加测量室 信息
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_Equator @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }
        
    }
//获取设备维修状态context.Request.QueryString["flag"] 
    [System.Web.Services.WebMethod()]//或[WebMethod(true)]
    public static string GetEquipStatus(string equipno)
    {
        string result = "";
        DataSet ds = DbHelperSQL.Query("exec  MES_Get_Status  1,'" + equipno + "'");
        if(ds.Tables[0].Rows.Count>0)
        {
            result = "{\"color\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"msg\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",\"action\":\"" + ds.Tables[0].Rows[0][2].ToString() + "\"}";
        }
        return result;
    } 
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}