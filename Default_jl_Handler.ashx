<%@ WebHandler Language="C#" Class="Default_jl_Handler" %>

using System;
using System.Web;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public class Default_jl_Handler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
       

        string flag = context.Request.QueryString["flag"];
        //string shebei = context.Request.QueryString["shebei"];
        string shebei = "精炼机1#";
        //string shebei_jl = HttpUtility.UrlDecode(context.Request.QueryString["shebei"]);// "精炼机1#";
        

        if (flag == "JL")//精炼查询
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = shebei;
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_jl @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }

        if (flag == "ZY")//转运
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = "";
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_zy @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }

        if (flag == "GP")//转运
        {
            SqlParameter[] paras ={
                new SqlParameter("@shebei",SqlDbType.VarChar,20),new SqlParameter("@html",SqlDbType.VarChar,2000)
                
            };
            paras[0].Value = "";
            paras[1].Direction = ParameterDirection.Output;
            DataSet ds = DbHelperSQL.Query("MES_SP_GetDefaultInfo_GP @shebei,@html", paras);
            string strInfo = paras[1].Value.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strInfo = ds.Tables[0].Rows[0][0].ToString();

            }

            context.Response.Write(strInfo);
        }



    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}