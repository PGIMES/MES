/**  版本信息模板在安装目录下，可自行修改。
* form4_Sale_Customer_LOG.cs
*
* 功 能： N/A
* 类 名： form4_Sale_Customer_LOG
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/22 13:57:01   N/A    初版
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
	/// 数据访问类:form4_Sale_Customer_LOG
	/// </summary>
	public partial class form4_Sale_Customer_LOG
	{
		public form4_Sale_Customer_LOG()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "form4_Sale_Customer_LOG"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from form4_Sale_Customer_LOG");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MES.Model.form4_Sale_Customer_LOG model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into form4_Sale_Customer_LOG(");
			strSql.Append("requestid,status_id,status_ms,dept,Update_Engineer,Update_user,Update_username,Receive_time,Commit_time,Update_content,Update_LB)");
			strSql.Append(" values (");
			strSql.Append("@requestid,@status_id,@status_ms,@dept,@Update_Engineer,@Update_user,@Update_username,@Receive_time,@Commit_time,@Update_content,@Update_LB)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@status_id", SqlDbType.NVarChar,50),
					new SqlParameter("@status_ms", SqlDbType.NVarChar,50),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_Engineer", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_user", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_username", SqlDbType.NVarChar,50),
					new SqlParameter("@Receive_time", SqlDbType.DateTime),
					new SqlParameter("@Commit_time", SqlDbType.DateTime),
					new SqlParameter("@Update_content", SqlDbType.NVarChar,-1),
					new SqlParameter("@Update_LB", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.requestid;
			parameters[1].Value = model.status_id;
			parameters[2].Value = model.status_ms;
			parameters[3].Value = model.dept;
			parameters[4].Value = model.Update_Engineer;
			parameters[5].Value = model.Update_user;
			parameters[6].Value = model.Update_username;
			parameters[7].Value = model.Receive_time;
			parameters[8].Value = model.Commit_time;
			parameters[9].Value = model.Update_content;
			parameters[10].Value = model.Update_LB;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MES.Model.form4_Sale_Customer_LOG model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update form4_Sale_Customer_LOG set ");
			strSql.Append("requestid=@requestid,");
			strSql.Append("status_id=@status_id,");
			strSql.Append("status_ms=@status_ms,");
			strSql.Append("dept=@dept,");
			strSql.Append("Update_Engineer=@Update_Engineer,");
			strSql.Append("Update_user=@Update_user,");
			strSql.Append("Update_username=@Update_username,");
			strSql.Append("Receive_time=@Receive_time,");
			strSql.Append("Commit_time=@Commit_time,");
			strSql.Append("Update_content=@Update_content,");
			strSql.Append("Update_LB=@Update_LB");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@status_id", SqlDbType.NVarChar,50),
					new SqlParameter("@status_ms", SqlDbType.NVarChar,50),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_Engineer", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_user", SqlDbType.NVarChar,50),
					new SqlParameter("@Update_username", SqlDbType.NVarChar,50),
					new SqlParameter("@Receive_time", SqlDbType.DateTime),
					new SqlParameter("@Commit_time", SqlDbType.DateTime),
					new SqlParameter("@Update_content", SqlDbType.NVarChar,-1),
					new SqlParameter("@Update_LB", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.requestid;
			parameters[1].Value = model.status_id;
			parameters[2].Value = model.status_ms;
			parameters[3].Value = model.dept;
			parameters[4].Value = model.Update_Engineer;
			parameters[5].Value = model.Update_user;
			parameters[6].Value = model.Update_username;
			parameters[7].Value = model.Receive_time;
			parameters[8].Value = model.Commit_time;
			parameters[9].Value = model.Update_content;
			parameters[10].Value = model.Update_LB;
			parameters[11].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from form4_Sale_Customer_LOG ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from form4_Sale_Customer_LOG ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MES.Model.form4_Sale_Customer_LOG GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,requestid,status_id,status_ms,dept,Update_Engineer,Update_user,Update_username,Receive_time,Commit_time,Update_content,Update_LB from form4_Sale_Customer_LOG ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			MES.Model.form4_Sale_Customer_LOG model=new MES.Model.form4_Sale_Customer_LOG();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MES.Model.form4_Sale_Customer_LOG DataRowToModel(DataRow row)
		{
			MES.Model.form4_Sale_Customer_LOG model=new MES.Model.form4_Sale_Customer_LOG();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["requestid"]!=null && row["requestid"].ToString()!="")
				{
					model.requestid=int.Parse(row["requestid"].ToString());
				}
				if(row["status_id"]!=null)
				{
					model.status_id=row["status_id"].ToString();
				}
				if(row["status_ms"]!=null)
				{
					model.status_ms=row["status_ms"].ToString();
				}
				if(row["dept"]!=null)
				{
					model.dept=row["dept"].ToString();
				}
				if(row["Update_Engineer"]!=null)
				{
					model.Update_Engineer=row["Update_Engineer"].ToString();
				}
				if(row["Update_user"]!=null)
				{
					model.Update_user=row["Update_user"].ToString();
				}
				if(row["Update_username"]!=null)
				{
					model.Update_username=row["Update_username"].ToString();
				}
				if(row["Receive_time"]!=null && row["Receive_time"].ToString()!="")
				{
					model.Receive_time=DateTime.Parse(row["Receive_time"].ToString());
				}
				if(row["Commit_time"]!=null && row["Commit_time"].ToString()!="")
				{
					model.Commit_time=DateTime.Parse(row["Commit_time"].ToString());
				}
				if(row["Update_content"]!=null)
				{
					model.Update_content=row["Update_content"].ToString();
				}
				if(row["Update_LB"]!=null)
				{
					model.Update_LB=row["Update_LB"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,requestid,status_id,status_ms,dept,Update_Engineer,Update_user,Update_username,Receive_time,Commit_time,Update_content,Update_LB ");
			strSql.Append(" FROM form4_Sale_Customer_LOG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,requestid,status_id,status_ms,dept,Update_Engineer,Update_user,Update_username,Receive_time,Commit_time,Update_content,Update_LB ");
			strSql.Append(" FROM form4_Sale_Customer_LOG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM form4_Sale_Customer_LOG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from form4_Sale_Customer_LOG T ");
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
			parameters[0].Value = "form4_Sale_Customer_LOG";
			parameters[1].Value = "ID";
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

