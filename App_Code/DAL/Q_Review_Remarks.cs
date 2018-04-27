/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Remarks.cs
*
* 功 能： N/A
* 类 名： Q_Review_Remarks
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 10:31:15   N/A    初版
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
	/// 数据访问类:Q_Review_Remarks
	/// </summary>
	public partial class Q_Review_Remarks
	{
		public Q_Review_Remarks()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("SlnId", "Q_Review_Remarks"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SlnId,int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Q_Review_Remarks");
			strSql.Append(" where SlnId=@SlnId and Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SlnId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = SlnId;
			parameters[1].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MES.Model.Q_Review_Remarks model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Q_Review_Remarks(");
			strSql.Append("SlnId,Remarks,File_Path,File_Name,Assessments,Create_by_EmpId,Create_by_EmpName,Create_Date)");
			strSql.Append(" values (");
			strSql.Append("@SlnId,@Remarks,@File_Path,@File_Name,@Assessments,@Create_by_EmpId,@Create_by_EmpName,@Create_Date)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SlnId", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.VarChar,200),
					new SqlParameter("@File_Path", SqlDbType.VarChar,100),
					new SqlParameter("@File_Name", SqlDbType.VarChar,100),
					new SqlParameter("@Assessments", SqlDbType.VarChar,200),
					new SqlParameter("@Create_by_EmpId", SqlDbType.VarChar,10),
					new SqlParameter("@Create_by_EmpName", SqlDbType.VarChar,20),
					new SqlParameter("@Create_Date", SqlDbType.DateTime)};
			parameters[0].Value = model.SlnId;
			parameters[1].Value = model.Remarks;
			parameters[2].Value = model.File_Path;
			parameters[3].Value = model.File_Name;
			parameters[4].Value = model.Assessments;
			parameters[5].Value = model.Create_by_EmpId;
			parameters[6].Value = model.Create_by_EmpName;
			parameters[7].Value = model.Create_Date;

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
		public bool Update(MES.Model.Q_Review_Remarks model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Q_Review_Remarks set ");
			strSql.Append("Remarks=@Remarks,");
			strSql.Append("File_Path=@File_Path,");
			strSql.Append("File_Name=@File_Name,");
			strSql.Append("Assessments=@Assessments,");
			strSql.Append("Create_by_EmpId=@Create_by_EmpId,");
			strSql.Append("Create_by_EmpName=@Create_by_EmpName,");
			strSql.Append("Create_Date=@Create_Date");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Remarks", SqlDbType.VarChar,200),
					new SqlParameter("@File_Path", SqlDbType.VarChar,100),
					new SqlParameter("@File_Name", SqlDbType.VarChar,100),
					new SqlParameter("@Assessments", SqlDbType.VarChar,200),
					new SqlParameter("@Create_by_EmpId", SqlDbType.VarChar,10),
					new SqlParameter("@Create_by_EmpName", SqlDbType.VarChar,20),
					new SqlParameter("@Create_Date", SqlDbType.DateTime),
					new SqlParameter("@SlnId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Remarks;
			parameters[1].Value = model.File_Path;
			parameters[2].Value = model.File_Name;
			parameters[3].Value = model.Assessments;
			parameters[4].Value = model.Create_by_EmpId;
			parameters[5].Value = model.Create_by_EmpName;
			parameters[6].Value = model.Create_Date;
			parameters[7].Value = model.SlnId;
			parameters[8].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Remarks ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool Delete(int SlnId,int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Remarks ");
			strSql.Append(" where SlnId=@SlnId and Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SlnId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = SlnId;
			parameters[1].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Remarks ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public MES.Model.Q_Review_Remarks GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SlnId,Id,Remarks,File_Path,File_Name,Assessments,Create_by_EmpId,Create_by_EmpName,Create_Date from Q_Review_Remarks ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			MES.Model.Q_Review_Remarks model=new MES.Model.Q_Review_Remarks();
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
		public MES.Model.Q_Review_Remarks DataRowToModel(DataRow row)
		{
			MES.Model.Q_Review_Remarks model=new MES.Model.Q_Review_Remarks();
			if (row != null)
			{
				if(row["SlnId"]!=null && row["SlnId"].ToString()!="")
				{
					model.SlnId=int.Parse(row["SlnId"].ToString());
				}
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Remarks"]!=null)
				{
					model.Remarks=row["Remarks"].ToString();
				}
				if(row["File_Path"]!=null)
				{
					model.File_Path=row["File_Path"].ToString();
				}
				if(row["File_Name"]!=null)
				{
					model.File_Name=row["File_Name"].ToString();
				}
				if(row["Assessments"]!=null)
				{
					model.Assessments=row["Assessments"].ToString();
				}
				if(row["Create_by_EmpId"]!=null)
				{
					model.Create_by_EmpId=row["Create_by_EmpId"].ToString();
				}
				if(row["Create_by_EmpName"]!=null)
				{
					model.Create_by_EmpName=row["Create_by_EmpName"].ToString();
				}
				if(row["Create_Date"]!=null && row["Create_Date"].ToString()!="")
				{
					model.Create_Date=DateTime.Parse(row["Create_Date"].ToString());
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
			strSql.Append("select SlnId,Id,Remarks,File_Path,File_Name,Assessments,Create_by_EmpId,Create_by_EmpName,Create_Date ");
			strSql.Append(" FROM Q_Review_Remarks ");
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
			strSql.Append(" SlnId,Id,Remarks,File_Path,File_Name,Assessments,Create_by_EmpId,Create_by_EmpName,Create_Date ");
			strSql.Append(" FROM Q_Review_Remarks ");
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
			strSql.Append("select count(1) FROM Q_Review_Remarks ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from Q_Review_Remarks T ");
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
			parameters[0].Value = "Q_Review_Remarks";
			parameters[1].Value = "Id";
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

