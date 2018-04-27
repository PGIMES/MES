/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_ProbDuty.cs
*
* 功 能： N/A
* 类 名： Q_Review_ProbDuty
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
	/// 数据访问类:Q_Review_ProbDuty
	/// </summary>
	public partial class Q_Review_ProbDuty
	{
		public Q_Review_ProbDuty()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RequestId", "Q_Review_ProbDuty"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RequestId,int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Q_Review_ProbDuty");
			strSql.Append(" where RequestId=@RequestId and Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;
			parameters[1].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据 & 增加一条Solution
		/// </summary>
		public int Add(MES.Model.Q_Review_ProbDuty model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("declare @dutyid int");
            strSql.Append(";insert into Q_Review_ProbDuty(");
			strSql.Append("RequestId,ImproveTarget,DutyDept,DutyEmp,ReqFinishDate)");
			strSql.Append(" values (");
			strSql.Append("@RequestId,@ImproveTarget,@DutyDept,@DutyEmp,@ReqFinishDate)");
			strSql.Append(";select @dutyid= @@IDENTITY;");
            strSql.Append("insert into Q_Review_Solution(");
            strSql.Append("RequestId,  DutyId,  ActionEmp, DisagreeState , SlnState)");
            strSql.Append(" values (");
            strSql.Append("@RequestId,@dutyid,@ActionEmp,'未提交','未提交措施')");
            strSql.Append(";select @dutyid;");
            SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,8),
					new SqlParameter("@ImproveTarget", SqlDbType.VarChar,100),
					new SqlParameter("@DutyDept", SqlDbType.VarChar,20),
					new SqlParameter("@DutyEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ReqFinishDate", SqlDbType.DateTime),
                    
                    new SqlParameter("@ActionEmp", SqlDbType.VarChar,20)

            };
			parameters[0].Value = model.RequestId;
			parameters[1].Value = model.ImproveTarget;
			parameters[2].Value = model.DutyDept;
			parameters[3].Value = model.DutyEmp;
			parameters[4].Value = model.ReqFinishDate;
            parameters[5].Value = model.DutyEmp;
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
		public bool Update(MES.Model.Q_Review_ProbDuty model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Q_Review_ProbDuty set ");
			strSql.Append("ImproveTarget=@ImproveTarget,");
			strSql.Append("DutyDept=@DutyDept,");
			strSql.Append("DutyEmp=@DutyEmp,");
			strSql.Append("ReqFinishDate=@ReqFinishDate");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@ImproveTarget", SqlDbType.VarChar,100),
					new SqlParameter("@DutyDept", SqlDbType.VarChar,20),
					new SqlParameter("@DutyEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ReqFinishDate", SqlDbType.DateTime),
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.ImproveTarget;
			parameters[1].Value = model.DutyDept;
			parameters[2].Value = model.DutyEmp;
			parameters[3].Value = model.ReqFinishDate;
			parameters[4].Value = model.RequestId;
			parameters[5].Value = model.Id;

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
			strSql.Append("delete from Q_Review_ProbDuty ");
			strSql.Append(" where Id=@Id;");
            strSql.Append("delete from Q_Review_Solution ");
            strSql.Append(" where DutyId=@Id;");
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
		public bool Delete(int RequestId,int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_ProbDuty ");
			strSql.Append(" where RequestId=@RequestId and Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;
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
			strSql.Append("delete from Q_Review_ProbDuty ");
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
		public MES.Model.Q_Review_ProbDuty GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 RequestId,Id,ImproveTarget,DutyDept,DutyEmp,ReqFinishDate,(select count(1) from Q_Review_Solution where DutyId=Id and isnull(DisagreeState,'')<>'NG')Mark from Q_Review_ProbDuty ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			MES.Model.Q_Review_ProbDuty model=new MES.Model.Q_Review_ProbDuty();
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
		public MES.Model.Q_Review_ProbDuty DataRowToModel(DataRow row)
		{
			MES.Model.Q_Review_ProbDuty model=new MES.Model.Q_Review_ProbDuty();
			if (row != null)
			{
				if(row["RequestId"]!=null && row["RequestId"].ToString()!="")
				{
					model.RequestId=int.Parse(row["RequestId"].ToString());
				}
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["ImproveTarget"]!=null)
				{
					model.ImproveTarget=row["ImproveTarget"].ToString();
				}
				if(row["DutyDept"]!=null)
				{
					model.DutyDept=row["DutyDept"].ToString();
				}
				if(row["DutyEmp"]!=null)
				{
					model.DutyEmp=row["DutyEmp"].ToString();
				}                
                if (row["ReqFinishDate"]!=null && row["ReqFinishDate"].ToString()!="")
				{
					model.ReqFinishDate=DateTime.Parse(row["ReqFinishDate"].ToString());
				}
                if (row["Mark"] != null)
                {
                    model.Mark = Convert.ToInt16(row["Mark"]);
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
			strSql.Append("select RequestId,Id,ImproveTarget,DutyDept, DutyEmp,b.lastname as DutyEmpName,ReqFinishDate,(select count(1) from Q_Review_Solution where DutyId=Id and isnull(DisagreeState,'')<>'NG')Mark ");
			strSql.Append(" FROM Q_Review_ProbDuty a");
            strSql.Append("     left join HRM_EMP_MES b on a.DutyEmp=b.workcode");
            if (strWhere.Trim()!="")
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
			strSql.Append(" RequestId,Id,ImproveTarget,DutyDept,DutyEmp,ReqFinishDate ");
			strSql.Append(" FROM Q_Review_ProbDuty ");
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
			strSql.Append("select count(1) FROM Q_Review_ProbDuty ");
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
			strSql.Append(")AS Row, T.*  from Q_Review_ProbDuty T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public static int  GetUnDoCount(string EmpId)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(" select count(1)cnt from(");
            strb.Append(" select(select count(1) from Q_Review_Solution where DutyId = Id and isnull(DisagreeState, '') <> 'NG')Mark");
            strb.Append(" FROM Q_Review_ProbDuty a    where DutyEmp = '"+ EmpId + "')t where mark = 0");
            int qty = Convert.ToInt16(DbHelperSQL.GetSingle(strb.ToString()));
            return qty;
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
			parameters[0].Value = "Q_Review_ProbDuty";
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

