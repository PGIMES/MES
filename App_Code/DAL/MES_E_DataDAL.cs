/**  版本信息模板在安装目录下，可自行修改。
* MES_SB_WX.cs
*
* 功 能： N/A
* 类 名： MES_SB_WX
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/12/9 10:34:49   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace MES.DAL
{
	/// <summary>
	/// 数据访问类:MES_EquipActionLog
	/// </summary>
	public partial class MES_E_Data

	{
        public MES_E_Data()
		{}
		#region  BasicMethod


        SQLHelperMachine SQLHelper = new SQLHelperMachine();
        public DataTable EData_Query(string deviceid,  string start_date, string end_date,int isErrData )
        {

           SqlParameter[] param = new SqlParameter[]
           {          
               new SqlParameter("@deviceid",deviceid),         
               new SqlParameter("@start_date",start_date),
               new SqlParameter("@end_date",end_date) ,
               new SqlParameter("@isErrData",isErrData)
           };
            DataTable dt = new DataTable();
            return SQLHelper.GetDataTable("MES_EData_Query ", param);


        }

        public DataTable EData_Query_Modify(string deviceid, string start_date, string end_date)
        {

            SqlParameter[] param = new SqlParameter[]
           {          
               new SqlParameter("@sbno",deviceid),         
               new SqlParameter("@start_date",start_date),
               new SqlParameter("@end_date",end_date) 
           };
            DataTable dt = new DataTable();
            return SQLHelper.GetDataTable("MES_EData_Query_Modify ", param);


        }
		

		/// <summary>
		/// 获得数据列表
		/// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select id,wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date,wx_end_date,status ");
        //    strSql.Append(" FROM MES_SB_WX ");
        //    if(strWhere.Trim()!="")
        //    {
        //        strSql.Append(" where "+strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
        
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        //public DataSet GetList(int Top,string strWhere,string filedOrder)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select ");
        //    if(Top>0)
        //    {
        //        strSql.Append(" top "+Top.ToString());
        //    }
        //    strSql.Append(" id,wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date,wx_end_date ");
        //    strSql.Append(" FROM MES_SB_WX ");
        //    if(strWhere.Trim()!="")
        //    {
        //        strSql.Append(" where "+strWhere);
        //    }
        //    strSql.Append(" order by " + filedOrder);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from MES_SB_WX T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "MES_SB_WX";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

