using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace MES.DAL
{
    /// <summary>
    /// 数据访问类:Q_Review_Log
    /// </summary>
    public partial class Q_Review_Log
    {
        public Q_Review_Log()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MES.Model.Q_Review_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Q_Review_Log(");
            strSql.Append("RequestId,Update_Engineer,Update_user,Update_username,Update_LB,Commit_time,Update_content)");
            strSql.Append(" values (");
            strSql.Append("@RequestId,@Update_Engineer,@Update_user,@Update_username,@Update_LB,@Commit_time,@Update_content)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RequestId", SqlDbType.Int,4),
                    new SqlParameter("@Update_Engineer", SqlDbType.NVarChar,50),
                    new SqlParameter("@Update_user", SqlDbType.NVarChar,20),
                    new SqlParameter("@Update_username", SqlDbType.NVarChar,20),
                    new SqlParameter("@Update_LB", SqlDbType.NVarChar,50),
                    new SqlParameter("@Commit_time", SqlDbType.DateTime),
                    new SqlParameter("@Update_content", SqlDbType.VarChar,100)};
            parameters[0].Value = model.RequestId;
            parameters[1].Value = model.Update_Engineer;
            parameters[2].Value = model.Update_user;
            parameters[3].Value = model.Update_username;
            parameters[4].Value = model.Update_LB;
            parameters[5].Value = model.Commit_time;
            parameters[6].Value = model.Update_content;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(MES.Model.Q_Review_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Q_Review_Log set ");
            strSql.Append("RequestId=@RequestId,");
            strSql.Append("Update_Engineer=@Update_Engineer,");
            strSql.Append("Update_user=@Update_user,");
            strSql.Append("Update_username=@Update_username,");
            strSql.Append("Update_LB=@Update_LB,");
            strSql.Append("Commit_time=@Commit_time,");
            strSql.Append("Update_content=@Update_content");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@RequestId", SqlDbType.Int,4),
                    new SqlParameter("@Update_Engineer", SqlDbType.NVarChar,50),
                    new SqlParameter("@Update_user", SqlDbType.NVarChar,20),
                    new SqlParameter("@Update_username", SqlDbType.NVarChar,20),
                    new SqlParameter("@Update_LB", SqlDbType.NVarChar,50),
                    new SqlParameter("@Commit_time", SqlDbType.DateTime),
                    new SqlParameter("@Update_content", SqlDbType.VarChar,100),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.RequestId;
            parameters[1].Value = model.Update_Engineer;
            parameters[2].Value = model.Update_user;
            parameters[3].Value = model.Update_username;
            parameters[4].Value = model.Update_LB;
            parameters[5].Value = model.Commit_time;
            parameters[6].Value = model.Update_content;
            parameters[7].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Q_Review_Log ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Q_Review_Log ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public MES.Model.Q_Review_Log GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,RequestId,Update_Engineer,Update_user,Update_username,Update_LB,Commit_time,Update_content from Q_Review_Log ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            MES.Model.Q_Review_Log model = new MES.Model.Q_Review_Log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public MES.Model.Q_Review_Log DataRowToModel(DataRow row)
        {
            MES.Model.Q_Review_Log model = new MES.Model.Q_Review_Log();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["requestid"] != null && row["requestid"].ToString() != "")
                {
                    model.RequestId = int.Parse(row["requestid"].ToString());
                }
                if (row["Update_Engineer"] != null)
                {
                    model.Update_Engineer = row["Update_Engineer"].ToString();
                }
                if (row["Update_user"] != null)
                {
                    model.Update_user = row["Update_user"].ToString();
                }
                if (row["Update_username"] != null)
                {
                    model.Update_username = row["Update_username"].ToString();
                }
                if (row["Update_LB"] != null)
                {
                    model.Update_LB = row["Update_LB"].ToString();
                }
                if (row["Commit_time"] != null && row["Commit_time"].ToString() != "")
                {
                    model.Commit_time = DateTime.Parse(row["Commit_time"].ToString());
                }
                if (row["Update_content"] != null)
                {
                    model.Update_content = row["Update_content"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RequestId,Update_Engineer,Update_user,Update_username,Update_LB,Commit_time,Update_content ");
            strSql.Append(" FROM Q_Review_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,RequestId,Update_Engineer,Update_user,Update_username,Update_LB,Commit_time,Update_content ");
            strSql.Append(" FROM Q_Review_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Q_Review_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Q_Review_Log T ");
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
			parameters[0].Value = "Q_Review_Log";
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

