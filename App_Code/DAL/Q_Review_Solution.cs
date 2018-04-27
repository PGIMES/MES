/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Solution.cs
*
* 功 能： N/A
* 类 名： Q_Review_Solution
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 10:31:16   N/A    初版
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
	/// 数据访问类:Q_Review_Solution
	/// </summary>
	public partial class Q_Review_Solution
	{
		public Q_Review_Solution()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RequestId", "Q_Review_Solution"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RequestId,int SlnId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Q_Review_Solution");
			strSql.Append(" where RequestId=@RequestId and SlnId=@SlnId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@SlnId", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;
			parameters[1].Value = SlnId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MES.Model.Q_Review_Solution model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Q_Review_Solution(");
			strSql.Append("RequestId,ActionPlan,PlanDate,ActionEmp,ActionFile,SlnDate,SlnEmp,DisagreeState,DisagreeDesc,ResultDate,ResultEmp,ConfirmStatus,ConfirmDate,ConfirmEmpId,ConfirmEmpName,SlnState,DutyId,Cause)");
			strSql.Append(" values (");
			strSql.Append("@RequestId,@ActionPlan,@PlanDate,@ActionEmp,@ActionFile,@SlnDate,@SlnEmp,@DisagreeState,@DisagreeDesc,@ResultDate,@ResultEmp,@ConfirmStatus,@ConfirmDate,@ConfirmEmpId,@ConfirmEmpName,@SlnState,@DutyId,@Cause)");
            strSql.Append(";update Q_Review_ProbDuty set ReqFinishDate=isnull(ReqFinishDate,@PlanDate)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@ActionPlan", SqlDbType.VarChar,500),
					new SqlParameter("@PlanDate", SqlDbType.DateTime),
					new SqlParameter("@ActionEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ActionFile", SqlDbType.VarChar,100),
					new SqlParameter("@SlnDate", SqlDbType.DateTime),
					new SqlParameter("@SlnEmp", SqlDbType.VarChar,20),
					new SqlParameter("@DisagreeState", SqlDbType.VarChar,10),
					new SqlParameter("@DisagreeDesc", SqlDbType.VarChar,100),
					new SqlParameter("@ResultDate", SqlDbType.DateTime),
					new SqlParameter("@ResultEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmStatus", SqlDbType.VarChar,100),
					new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@ConfirmEmpId", SqlDbType.VarChar,10),
					new SqlParameter("@ConfirmEmpName", SqlDbType.VarChar,20),
					new SqlParameter("@SlnState", SqlDbType.VarChar,20),
                    new SqlParameter("@DutyId", SqlDbType.Int),
                    new SqlParameter("@Cause", SqlDbType.VarChar,500),

            };
			parameters[0].Value = model.RequestId;
			parameters[1].Value = model.ActionPlan;
			parameters[2].Value = model.PlanDate;
			parameters[3].Value = model.ActionEmp;
			parameters[4].Value = model.ActionFile;
			parameters[5].Value = model.SlnDate;
			parameters[6].Value = model.SlnEmp;
			parameters[7].Value = model.DisagreeState;
			parameters[8].Value = model.DisagreeDesc;
			parameters[9].Value = model.ResultDate;
			parameters[10].Value = model.ResultEmp;
			parameters[11].Value = model.ConfirmStatus;
			parameters[12].Value = model.ConfirmDate;
			parameters[13].Value = model.ConfirmEmpId;
			parameters[14].Value = model.ConfirmEmpName;
            parameters[15].Value = model.SlnState;
            parameters[16].Value = model.DutyId;
            parameters[17].Value = model.Cause;
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
        /// 插入日志记录
        /// </summary>
        /// <param name="slnid"></param>
        /// <returns></returns>
        public int AddLog(string slnid)
        {
            string sql = " insert into Q_Review_Solution_log ";
                sql = sql + " select * from Q_Review_Solution where slnid='" + slnid.ToString() + "' and ActionPlan is not null";
            int result = DbHelperSQL.ExecuteSql(sql);
            return result;

        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MES.Model.Q_Review_Solution model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Q_Review_Solution set ");
			strSql.Append("ActionPlan=@ActionPlan,");
			strSql.Append("PlanDate=@PlanDate,");
			strSql.Append("ActionEmp=@ActionEmp,");
			strSql.Append("ActionFile=@ActionFile,");
			strSql.Append("SlnDate=@SlnDate,");
			strSql.Append("SlnEmp=@SlnEmp,");
			strSql.Append("DisagreeState=@DisagreeState,");
			strSql.Append("DisagreeDesc=@DisagreeDesc,");
			strSql.Append("ResultDate=@ResultDate,");
			strSql.Append("ResultEmp=@ResultEmp,");
			strSql.Append("ConfirmStatus=@ConfirmStatus,");
			strSql.Append("ConfirmDate=@ConfirmDate,");
			strSql.Append("ConfirmEmpId=@ConfirmEmpId,");
			strSql.Append("ConfirmEmpName=@ConfirmEmpName,");            
            strSql.Append("SlnState=@SlnState,");
            strSql.Append("Cause=@Cause,");
            strSql.Append("ConfirmDesc=@ConfirmDesc, ");
            strSql.Append("DisagreeEmp=@DisagreeEmp, ");
            strSql.Append("DisagreeDate=@DisagreeDate ");
            strSql.Append(" where SlnId=@SlnId");
            strSql.Append(" ; ");

            SqlParameter[] parameters = {
					new SqlParameter("@ActionPlan", SqlDbType.VarChar,300),
					new SqlParameter("@PlanDate", SqlDbType.DateTime),
					new SqlParameter("@ActionEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ActionFile", SqlDbType.VarChar,100),
					new SqlParameter("@SlnDate", SqlDbType.DateTime),
					new SqlParameter("@SlnEmp", SqlDbType.VarChar,20),
					new SqlParameter("@DisagreeState", SqlDbType.VarChar,10),
					new SqlParameter("@DisagreeDesc", SqlDbType.VarChar,100),
					new SqlParameter("@ResultDate", SqlDbType.DateTime),
					new SqlParameter("@ResultEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmStatus", SqlDbType.VarChar,100),
					new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@ConfirmEmpId", SqlDbType.VarChar,10),
					new SqlParameter("@ConfirmEmpName", SqlDbType.VarChar,20),
                    new SqlParameter("@SlnState", SqlDbType.VarChar,20),
                    new SqlParameter("@Cause", SqlDbType.VarChar,300),
                    new SqlParameter("@ConfirmDesc", SqlDbType.VarChar,300),
                    new SqlParameter("@DisagreeEmp", SqlDbType.VarChar,20),
                    new SqlParameter("@DisagreeDate", SqlDbType.DateTime),
                    new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@SlnId", SqlDbType.Int,4)};
			parameters[0].Value = model.ActionPlan;
			parameters[1].Value = model.PlanDate;
			parameters[2].Value = model.ActionEmp;
			parameters[3].Value = model.ActionFile;
			parameters[4].Value = model.SlnDate;
			parameters[5].Value = model.SlnEmp;
			parameters[6].Value = model.DisagreeState;
			parameters[7].Value = model.DisagreeDesc;
			parameters[8].Value = model.ResultDate;
			parameters[9].Value = model.ResultEmp;
			parameters[10].Value = model.ConfirmStatus;
			parameters[11].Value = model.ConfirmDate;
			parameters[12].Value = model.ConfirmEmpId;
			parameters[13].Value = model.ConfirmEmpName;
            parameters[14].Value = model.SlnState;
            parameters[15].Value = model.Cause;
            parameters[16].Value = model.ConfirmDesc;
            parameters[17].Value = model.DisagreeEmp;
            parameters[18].Value =   model.DisagreeDate;
            parameters[19].Value = model.RequestId;
			parameters[20].Value = model.SlnId;

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
        /// 更新否决状态
        /// </summary>
        public bool UpdateReject(MES.Model.Q_Review_Solution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Q_Review_Solution set ");
            strSql.Append("DisagreeState=@DisagreeState,");
            strSql.Append("DisagreeDesc=@DisagreeDesc");
            strSql.Append("DisagreeEmp=@DisagreeEmp,");
            strSql.Append("DisagreeDate=@DisagreeDate");
            strSql.Append(" where SlnId=@SlnId ");
            SqlParameter[] parameters = {

                    new SqlParameter("@DisagreeState", SqlDbType.VarChar,20),
                    new SqlParameter("@DisagreeDesc", SqlDbType.VarChar),
                    new SqlParameter("@DisagreeEmp", SqlDbType.VarChar,20),
                    new SqlParameter("@DisagreeDate", SqlDbType.DateTime),
                    new SqlParameter("@SlnId", SqlDbType.Int)};
            parameters[0].Value = model.DisagreeState;
            parameters[1].Value = model.DisagreeDesc;
            parameters[2].Value = model.DisagreeEmp;
            parameters[3].Value = model.DisagreeDate;

            parameters[4].Value = model.SlnId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新确认状态
        /// </summary>
        public bool UpdateConfirm(MES.Model.Q_Review_Solution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Q_Review_Solution set ");
            strSql.Append("ConfirmStatus=@ConfirmStatus,");
            strSql.Append("ConfirmDate=@ConfirmDate,");
            strSql.Append("ConfirmEmpId=@ConfirmEmpId,");
            strSql.Append("ConfirmEmpName=@ConfirmEmpName,");
            
            strSql.Append("SlnState=@SlnState,");
            strSql.Append("ConfirmDesc=@ConfirmDesc");
            strSql.Append(" where SlnId=@SlnId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ConfirmStatus", SqlDbType.VarChar,100),
                    new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
                    new SqlParameter("@ConfirmEmpId", SqlDbType.VarChar,10),
                    new SqlParameter("@ConfirmEmpName", SqlDbType.VarChar,20),
                    new SqlParameter("@SlnState", SqlDbType.VarChar,20),
                    new SqlParameter("@ConfirmDesc", SqlDbType.VarChar,100),
                    new SqlParameter("@SlnId", SqlDbType.Int)};
            parameters[0].Value = model.ConfirmStatus;
            parameters[1].Value = model.ConfirmDate;
            parameters[2].Value = model.ConfirmEmpId;
            parameters[3].Value = model.ConfirmEmpName;
            parameters[4].Value = model.SlnState;
            parameters[5].Value = model.ConfirmDesc;
            parameters[6].Value = model.SlnId;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int SlnId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Solution ");
			strSql.Append(" where SlnId=@SlnId");
			SqlParameter[] parameters = {
					new SqlParameter("@SlnId", SqlDbType.Int,4)
			};
			parameters[0].Value = SlnId;

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
		public bool Delete(int RequestId,int SlnId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Solution ");
			strSql.Append(" where RequestId=@RequestId and SlnId=@SlnId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@SlnId", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;
			parameters[1].Value = SlnId;

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
		public bool DeleteList(string SlnIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Solution ");
			strSql.Append(" where SlnId in ("+SlnIdlist + ")  ");
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
		public MES.Model.Q_Review_Solution GetModel(int SlnId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 RequestId,SlnId,DutyId,ActionPlan,PlanDate,ActionEmp,ActionFile,SlnDate,SlnEmp,DisagreeState,DisagreeDesc,ResultDate,ResultEmp,ConfirmStatus,ConfirmDate,ConfirmEmpId,ConfirmEmpName,ConfirmDesc,SlnState,Cause from Q_Review_Solution ");
			strSql.Append(" where SlnId=@SlnId");
			SqlParameter[] parameters = {
					new SqlParameter("@SlnId", SqlDbType.Int,4)
			};
			parameters[0].Value = SlnId;

			MES.Model.Q_Review_Solution model=new MES.Model.Q_Review_Solution();
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
		public MES.Model.Q_Review_Solution DataRowToModel(DataRow row)
		{
			MES.Model.Q_Review_Solution model=new MES.Model.Q_Review_Solution();
			if (row != null)
			{
				if(row["RequestId"]!=null && row["RequestId"].ToString()!="")
				{
					model.RequestId=int.Parse(row["RequestId"].ToString());
				}
				if(row["SlnId"]!=null && row["SlnId"].ToString()!="")
				{
					model.SlnId=int.Parse(row["SlnId"].ToString());
				}
                if (row["DutyId"] != null && row["DutyId"].ToString() != "")
                {
                    model.DutyId = int.Parse(row["DutyId"].ToString());
                }
                if (row["ActionPlan"]!=null)
				{
					model.ActionPlan=row["ActionPlan"].ToString();
				}
				if(row["PlanDate"]!=null && row["PlanDate"].ToString()!="")
				{
					model.PlanDate=DateTime.Parse(row["PlanDate"].ToString());
				}
				if(row["ActionEmp"]!=null)
				{
					model.ActionEmp=row["ActionEmp"].ToString();
				}
				if(row["ActionFile"]!=null)
				{
					model.ActionFile=row["ActionFile"].ToString();
				}
				if(row["SlnDate"]!=null && row["SlnDate"].ToString()!="")
				{
					model.SlnDate=DateTime.Parse(row["SlnDate"].ToString());
				}
				if(row["SlnEmp"]!=null)
				{
					model.SlnEmp=row["SlnEmp"].ToString();
				}
				if(row["DisagreeState"]!=null)
				{
					model.DisagreeState=row["DisagreeState"].ToString();
				}
				if(row["DisagreeDesc"]!=null)
				{
					model.DisagreeDesc=row["DisagreeDesc"].ToString();
				}
				if(row["ResultDate"]!=null && row["ResultDate"].ToString()!="")
				{
					model.ResultDate=DateTime.Parse(row["ResultDate"].ToString());
				}
				if(row["ResultEmp"]!=null )
				{
					model.ResultEmp=row["ResultEmp"].ToString();
				}
				if(row["ConfirmStatus"]!=null)
				{
					model.ConfirmStatus=row["ConfirmStatus"].ToString();
				}
                if (row["ConfirmDesc"] != null)
                {
                    model.ConfirmDesc = row["ConfirmDesc"].ToString();
                }
                if (row["ConfirmDate"]!=null && row["ConfirmDate"].ToString()!="")
				{
					model.ConfirmDate=DateTime.Parse(row["ConfirmDate"].ToString());
				}
				if(row["ConfirmEmpId"]!=null)
				{
					model.ConfirmEmpId=row["ConfirmEmpId"].ToString();
				}
				if(row["ConfirmEmpName"]!=null)
				{
					model.ConfirmEmpName=row["ConfirmEmpName"].ToString();
				}
                model.SlnState = row["SlnState"].ToString();
                model.Cause = row["Cause"].ToString();
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select a.RequestId,SlnId,ActionPlan,PlanDate,b.lastname as ActionEmp,ActionEmp as ActionEmpId,ActionFile,SlnDate,c.lastname as SlnEmp,DisagreeState,DisagreeDesc,DisagreeEmp,DisagreeDate,ResultDesc,ResultDate,d.lastname as ResultEmp,ConfirmStatus,ConfirmDate,ConfirmEmpId,ConfirmEmpName ,ConfirmDesc,SlnState,Cause,e.ImproveTarget");
            strSql.Append(" FROM Q_Review_Solution a left join HRM_EMP_MES b on a.ActionEmp=b.workcode ");
            strSql.Append("                          left join Q_Review_ProbDuty e on a.dutyid=e.id ");

            strSql.Append("   left join   HRM_EMP_MES c on a.SlnEmp=c.workcode ");
            strSql.Append("   left join   HRM_EMP_MES d on a.ResultEmp=d.workcode ");
            if (strWhere.Trim()!="")
			{
				strSql.Append(" where a."+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
		/// 获得history记录
		/// </summary>
		public static DataSet GetLogList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.RequestId,SlnId,ActionPlan,PlanDate,b.lastname as ActionEmp,ActionEmp as ActionEmpId,ActionFile,SlnDate,c.lastname as SlnEmp,DisagreeState,DisagreeDesc,DisagreeEmp,DisagreeDate,ResultDesc,ResultDate,d.lastname as ResultEmp,ConfirmStatus,ConfirmDate,ConfirmEmpId,ConfirmEmpName ,ConfirmDesc,SlnState,Cause,e.ImproveTarget");
            strSql.Append(" FROM Q_Review_Solution_log a left join HRM_EMP_MES b on a.ActionEmp=b.workcode ");
            strSql.Append("                          left join Q_Review_ProbDuty e on a.dutyid=e.id ");

            strSql.Append("   left join   HRM_EMP_MES c on a.SlnEmp=c.workcode ");
            strSql.Append("   left join   HRM_EMP_MES d on a.ResultEmp=d.workcode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere );
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
			strSql.Append(" RequestId,SlnId,ActionPlan,PlanDate,a.lastname as ActionEmp,ActionFile,SlnDate,c.lastname as SlnEmp,DisagreeState,DisagreeDesc,DisagreeEmp,DisagreeDate,ResultDesc,ResultDate,d.lastname as ResultEmp,ConfirmStatus,ConfirmDate,ConfirmEmpId,ConfirmEmpName,ConfirmDesc,SlnState,Cause ");
			strSql.Append(" FROM Q_Review_Solution a left join HRM_EMP_MES b on a.ActionEmp=b.workcode ");
            strSql.Append("   left join   HRM_EMP_MES c on a.SlnEmp=c.workcode ");
            strSql.Append("   left join   HRM_EMP_MES d on a.ResultEmp=d.workcode ");
            if (strWhere.Trim()!="")
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
			strSql.Append("select count(1) FROM Q_Review_Solution ");
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
				strSql.Append("order by T.SlnId desc");
			}
			strSql.Append(")AS Row, T.*  from Q_Review_Solution T ");
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
			parameters[0].Value = "Q_Review_Solution";
			parameters[1].Value = "SlnId";
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

