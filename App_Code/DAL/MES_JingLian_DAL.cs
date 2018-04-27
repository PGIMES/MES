using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references

/// <summary>
///MES_JingLian_DAL 的摘要说明
/// </summary>
public class MES_JingLian_DAL
{
	public MES_JingLian_DAL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public DataSet Get_BS(string dh,string hejin)
    {
        //where HD_zybno like '20170105-JL%' AND HD_hejin='A380'
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select COUNT(*) from MES_Jinglian where 1=1 ");
        //if (dh.Trim() != "")
        //{
        //    strSql.Append(" and HD_zybno like '"+dh+"%'");
        //}
        if (hejin.Trim() != "")
        {
            strSql.Append(" and HD_hejin ='"+hejin+"' "  );
        }
        return DbHelperSQL.Query(strSql.ToString());
    }

    public string Get_luhao(string zyno)
    {
        string luhao = "";
        string strSql = "SELECT HD_luhao FROM MES_Jinglian WHERE HD_time=(SELECT MAX(HD_time)  FROM MES_Jinglian where  HD_baohao='"+zyno+"')";

       DataSet  ds = DbHelperSQL.Query(strSql.ToString());
       if (ds.Tables[0].Rows.Count > 0)
       {
           luhao = ds.Tables[0].Rows[0][0].ToString();
       }
       return luhao;  
    }

    public string Get_hejin(string zyno,string luhao)
    {
        string hejin = "";
        string strSql = "SELECT HD_hejin FROM MES_Jinglian WHERE HD_time=(SELECT MAX(HD_time)  FROM MES_Jinglian where HD_luhao='"+luhao+"' AND HD_baohao='"+zyno+"')";

        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            hejin = ds.Tables[0].Rows[0][0].ToString();
        }
        return hejin;
    }
   
}