/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Prob.cs
*
* 功 能： N/A
* 类 名： Q_Review_Prob
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 13:48:24   N/A    初版
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
	/// 数据访问类:Q_Review_Prob
	/// </summary>
	public partial class Q_Review_Prob
	{
		public Q_Review_Prob()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		    return DbHelperSQL.GetMaxID("RequestId", "Q_Review_Prob"); 
		}
        /// <summary>
		/// 得到最大单号
		/// </summary>
		public string GetMaxDH()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select isnull(Max(DH)+1,convert(varchar(6),getdate(),112)+'001') from Q_Review_Prob");
            strSql.Append(" where left(DH,6)=convert(varchar(6),getdate(),112)       ");           

            return DbHelperSQL.GetSingle(strSql.ToString()).ToString();           

        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RequestId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Q_Review_Prob");
			strSql.Append(" where RequestId=@RequestId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public MES.Model.Q_Review_Prob Add(MES.Model.Q_Review_Prob model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Q_Review_Prob(");
			strSql.Append("RequestId,DH,EmpId,EmpName,Dept,ProbDate,ProbEmp,ProbStatus,ProbFrom,Domain,CustClass,ProdProject,LJH,LJName,ProdDesc,ProbFile,ReqSlnDate,ReqCloseDate,ActualCloseDate,Rank)");
			strSql.Append(" values (");
			strSql.Append("@RequestId,@DH,@EmpId,@EmpName,@Dept,@ProbDate,@ProbEmp,@ProbStatus,@ProbFrom,@Domain,@CustClass,@ProdProject,@LJH,@LJName,@ProdDesc,@ProbFile,@ReqSlnDate,@ReqCloseDate,@ActualCloseDate,@Rank);");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4),
					new SqlParameter("@DH", SqlDbType.VarChar,20),
					new SqlParameter("@EmpId", SqlDbType.VarChar,20),
					new SqlParameter("@EmpName", SqlDbType.VarChar,20),
					new SqlParameter("@Dept", SqlDbType.VarChar,20),
					new SqlParameter("@ProbDate", SqlDbType.DateTime),
					new SqlParameter("@ProbEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ProbStatus", SqlDbType.VarChar,20),
					new SqlParameter("@ProbFrom", SqlDbType.VarChar,20),
					new SqlParameter("@Domain", SqlDbType.VarChar,10),
					new SqlParameter("@CustClass", SqlDbType.VarChar,20),
					new SqlParameter("@ProdProject", SqlDbType.VarChar,50),
					new SqlParameter("@LJH", SqlDbType.VarChar,30),
					new SqlParameter("@LJName", SqlDbType.VarChar,50),
					new SqlParameter("@ProdDesc", SqlDbType.VarChar,500),
					new SqlParameter("@ProbFile", SqlDbType.VarChar,200),
					new SqlParameter("@ReqSlnDate", SqlDbType.DateTime),
					new SqlParameter("@ReqCloseDate", SqlDbType.DateTime),
					new SqlParameter("@ActualCloseDate", SqlDbType.DateTime),
                    new SqlParameter("@Rank", SqlDbType.VarChar,10)
            };
            model.RequestId = GetMaxId();
            model.DH=GetMaxDH();//
            parameters[0].Value = model.RequestId;
            parameters[1].Value = model.DH;
			parameters[2].Value = model.EmpId;
			parameters[3].Value = model.EmpName;
			parameters[4].Value = model.Dept;
			parameters[5].Value = model.ProbDate;
			parameters[6].Value = model.ProbEmp;
			parameters[7].Value = model.ProbStatus;
			parameters[8].Value = model.ProbFrom;
			parameters[9].Value = model.Domain;
			parameters[10].Value = model.CustClass;
			parameters[11].Value = model.ProdProject;
			parameters[12].Value = model.LJH;
			parameters[13].Value = model.LJName;
			parameters[14].Value = model.ProdDesc;
			parameters[15].Value = model.ProbFile;
			parameters[16].Value = model.ReqSlnDate;
			parameters[17].Value = model.ReqCloseDate;
			parameters[18].Value = model.ActualCloseDate;
            parameters[19].Value = model.Rank;
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return model;
			}
			else
			{
				return null;
			}

		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MES.Model.Q_Review_Prob model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Q_Review_Prob set ");
			strSql.Append("@DH=@DH,");//不更新
            strSql.Append("@EmpId=@EmpId,");//不更新
            strSql.Append("@EmpName=@EmpName,");//不更新
            strSql.Append("@Dept=@Dept,");//不更新
            strSql.Append("ProbDate=@ProbDate,");
			strSql.Append("ProbEmp=@ProbEmp,");
			strSql.Append("ProbStatus=@ProbStatus,");
			strSql.Append("ProbFrom=@ProbFrom,");
			strSql.Append("Domain=@Domain,");
			strSql.Append("CustClass=@CustClass,");
			strSql.Append("ProdProject=@ProdProject,");
			strSql.Append("LJH=@LJH,");
			strSql.Append("LJName=@LJName,");
			strSql.Append("ProdDesc=@ProdDesc,");
			strSql.Append("ProbFile=@ProbFile,");
			strSql.Append("ReqSlnDate=@ReqSlnDate,");
			strSql.Append("ReqCloseDate=@ReqCloseDate,");
			strSql.Append("@ActualCloseDate=@ActualCloseDate,");//不更新
            strSql.Append("Rank=@Rank");
            strSql.Append(" where RequestId=@RequestId ");
			SqlParameter[] parameters = {
					new SqlParameter("@DH", SqlDbType.VarChar,20),
					new SqlParameter("@EmpId", SqlDbType.VarChar,20),
					new SqlParameter("@EmpName", SqlDbType.VarChar,20),
					new SqlParameter("@Dept", SqlDbType.VarChar,20),
					new SqlParameter("@ProbDate", SqlDbType.DateTime),
					new SqlParameter("@ProbEmp", SqlDbType.VarChar,20),
					new SqlParameter("@ProbStatus", SqlDbType.VarChar,20),
					new SqlParameter("@ProbFrom", SqlDbType.VarChar,20),
					new SqlParameter("@Domain", SqlDbType.VarChar,10),
					new SqlParameter("@CustClass", SqlDbType.VarChar,20),
					new SqlParameter("@ProdProject", SqlDbType.VarChar,50),
					new SqlParameter("@LJH", SqlDbType.VarChar,30),
					new SqlParameter("@LJName", SqlDbType.VarChar,50),
					new SqlParameter("@ProdDesc", SqlDbType.VarChar,500),
					new SqlParameter("@ProbFile", SqlDbType.VarChar,200),
					new SqlParameter("@ReqSlnDate", SqlDbType.DateTime),
					new SqlParameter("@ReqCloseDate", SqlDbType.DateTime),
					new SqlParameter("@ActualCloseDate", SqlDbType.DateTime),
                    new SqlParameter("@Rank", SqlDbType.VarChar,10),
					new SqlParameter("@RequestId", SqlDbType.Int,4)
                    
            };
			parameters[0].Value = model.DH;
			parameters[1].Value = model.EmpId;
			parameters[2].Value = model.EmpName;
			parameters[3].Value = model.Dept;
			parameters[4].Value = model.ProbDate;
			parameters[5].Value = model.ProbEmp;
			parameters[6].Value = model.ProbStatus;
			parameters[7].Value = model.ProbFrom;
			parameters[8].Value = model.Domain;
			parameters[9].Value = model.CustClass;
			parameters[10].Value = model.ProdProject;
			parameters[11].Value = model.LJH;
			parameters[12].Value = model.LJName;
			parameters[13].Value = model.ProdDesc;
			parameters[14].Value = model.ProbFile;
			parameters[15].Value = model.ReqSlnDate;
			parameters[16].Value = model.ReqCloseDate;
			parameters[17].Value = model.ActualCloseDate;
            parameters[18].Value = model.Rank;
			parameters[19].Value = model.RequestId;
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
        /// 更新解决状态
        /// </summary>
        public bool UpdateActualState(MES.Model.Q_Review_Prob model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Q_Review_Prob set ");
            strSql.Append("ActualCloseDate=@ActualCloseDate,");
            strSql.Append("ActualState=@ActualState,");
            strSql.Append("ProbStatus=@ProbStatus");             
            strSql.Append(" where RequestId=@RequestId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ActualCloseDate", SqlDbType.DateTime),
                    new SqlParameter("@ActualState", SqlDbType.VarChar,20),
                    new SqlParameter("@ProbStatus", SqlDbType.VarChar),                    
                    new SqlParameter("@RequestId", SqlDbType.Int)};
            parameters[0].Value = model.ActualCloseDate;
            parameters[1].Value = model.ActualState;
            parameters[2].Value = model.ProbStatus;
            parameters[3].Value = model.RequestId;        

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
        public bool Delete(int RequestId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Prob ");
			strSql.Append(" where RequestId=@RequestId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;

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
		public bool DeleteList(string RequestIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Q_Review_Prob ");
			strSql.Append(" where RequestId in ("+RequestIdlist + ")  ");
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
		public MES.Model.Q_Review_Prob GetModel(int RequestId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 RequestId,DH,EmpId,EmpName,Dept,ProbDate,ProbEmp,ProbStatus,ProbFrom,Domain,CustClass,ProdProject,LJH,LJName,ProdDesc,ProbFile,ReqSlnDate,ReqCloseDate,ActualCloseDate,ActualState,CreateDate,Rank from Q_Review_Prob ");
			strSql.Append(" where RequestId=@RequestId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RequestId", SqlDbType.Int,4)			};
			parameters[0].Value = RequestId;

			MES.Model.Q_Review_Prob model=new MES.Model.Q_Review_Prob();
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
		public MES.Model.Q_Review_Prob DataRowToModel(DataRow row)
		{
			MES.Model.Q_Review_Prob model=new MES.Model.Q_Review_Prob();
			if (row != null)
			{
				if(row["RequestId"]!=null && row["RequestId"].ToString()!="")
				{
					model.RequestId=int.Parse(row["RequestId"].ToString());
				}
				if(row["DH"]!=null)
				{
					model.DH=row["DH"].ToString();
				}
				if(row["EmpId"]!=null)
				{
					model.EmpId=row["EmpId"].ToString();
				}
				if(row["EmpName"]!=null)
				{
					model.EmpName=row["EmpName"].ToString();
				}
				if(row["Dept"]!=null)
				{
					model.Dept=row["Dept"].ToString();
				}
				if(row["ProbDate"]!=null && row["ProbDate"].ToString()!="")
				{
					model.ProbDate=DateTime.Parse(row["ProbDate"].ToString());
				}
				if(row["ProbEmp"]!=null)
				{
					model.ProbEmp=row["ProbEmp"].ToString();
				}
				if(row["ProbStatus"]!=null)
				{
					model.ProbStatus=row["ProbStatus"].ToString();
				}
				if(row["ProbFrom"]!=null)
				{
					model.ProbFrom=row["ProbFrom"].ToString();
				}
				if(row["Domain"]!=null)
				{
					model.Domain=row["Domain"].ToString();
				}
				if(row["CustClass"]!=null)
				{
					model.CustClass=row["CustClass"].ToString();
				}
				if(row["ProdProject"]!=null)
				{
					model.ProdProject=row["ProdProject"].ToString();
				}
				if(row["LJH"]!=null)
				{
					model.LJH=row["LJH"].ToString();
				}
				if(row["LJName"]!=null)
				{
					model.LJName=row["LJName"].ToString();
				}
				if(row["ProdDesc"]!=null)
				{
					model.ProdDesc=row["ProdDesc"].ToString();
				}
				if(row["ProbFile"]!=null)
				{
					model.ProbFile=row["ProbFile"].ToString();
				}
				if(row["ReqSlnDate"]!=null && row["ReqSlnDate"].ToString()!="")
				{
					model.ReqSlnDate=DateTime.Parse(row["ReqSlnDate"].ToString());
				}
				if(row["ReqCloseDate"]!=null && row["ReqCloseDate"].ToString()!="")
				{
					model.ReqCloseDate=DateTime.Parse(row["ReqCloseDate"].ToString());
				}
				if(row["ActualCloseDate"]!=null && row["ActualCloseDate"].ToString()!="")
				{
					model.ActualCloseDate=DateTime.Parse(row["ActualCloseDate"].ToString());
				}
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["ActualState"] != null  )
                {
                    model.ActualState =  row["ActualState"].ToString();
                }
                if (row["Rank"] != null)
                {
                    model.Rank = row["Rank"].ToString();
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
			strSql.Append("select RequestId,DH,EmpId,EmpName,Dept,ProbDate,ProbEmp,ProbStatus,ProbFrom,Domain,CustClass,ProdProject,LJH,LJName,ProdDesc,ProbFile,ReqSlnDate,ReqCloseDate,ActualCloseDate,ActualState,Rank ");
			strSql.Append(" FROM Q_Review_Prob ");
            
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
			strSql.Append(" RequestId,DH,EmpId,EmpName,Dept,ProbDate,ProbEmp,ProbStatus,ProbFrom,Domain,CustClass,ProdProject,LJH,LJName,ProdDesc,ProbFile,ReqSlnDate,ReqCloseDate,ActualCloseDate,ActualState,Rank ");
			strSql.Append(" FROM Q_Review_Prob ");
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
			strSql.Append("select count(1) FROM Q_Review_Prob ");
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
				strSql.Append("order by T.RequestId desc");
			}
			strSql.Append(")AS Row, T.*  from Q_Review_Prob T ");
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
			parameters[0].Value = "Q_Review_Prob";
			parameters[1].Value = "RequestId";
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

