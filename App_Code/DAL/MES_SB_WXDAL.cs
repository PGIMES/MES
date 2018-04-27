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
	/// 数据访问类:MES_SB_WX
	/// </summary>
	public partial class MES_SB_WX
	{
		public MES_SB_WX()
		{}
		#region  BasicMethod



		/// <summary>
		/// 开始维修-增加一条数据
		/// </summary>
		public int Add(MES.Model.MES_SB_WX model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append(" BEGIN TRY ");
            strSql.Append("     BEGIN TRAN ");
            strSql.Append(" insert into MES_SB_WX(");
			strSql.Append("  wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date)");
			strSql.Append(" values (");
			strSql.Append("  @wx_dh,@wx_gonghao,@wx_name,@wx_banzhu,@wx_banbie,@wx_cs,@wx_result,@mo_down_cs,@wx_begin_date);");
			strSql.Append("if @@ROWCOUNT>0 ");
            //更新状态
            strSql.Append(" update MES_SB_BX set [status]=@status where bx_dh=@wx_dh; ");
            strSql.Append(" COMMIT TRAN  ");
            strSql.Append(" END TRY ");
            strSql.Append(" BEGIN CATCH ");
            strSql.Append(" ROLLBACK TRAN ");
            strSql.Append(" END CATCH ");
            SqlParameter[] parameters = {
					new SqlParameter("@wx_dh", SqlDbType.VarChar,20),
					new SqlParameter("@wx_gonghao", SqlDbType.VarChar,20),
					new SqlParameter("@wx_name", SqlDbType.VarChar,20),
					new SqlParameter("@wx_banzhu", SqlDbType.VarChar,10),
					new SqlParameter("@wx_banbie", SqlDbType.VarChar,10),
					new SqlParameter("@wx_cs", SqlDbType.VarChar,100),
					new SqlParameter("@wx_result", SqlDbType.VarChar,20),
					new SqlParameter("@mo_down_cs", SqlDbType.VarChar,50),
					new SqlParameter("@wx_begin_date", SqlDbType.DateTime),
					//new SqlParameter("@wx_end_date", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.VarChar,50)                   
                                        };
			parameters[0].Value = model.wx_dh;
			parameters[1].Value = model.wx_gonghao;
			parameters[2].Value = model.wx_name;
			parameters[3].Value = model.wx_banzhu;
			parameters[4].Value = model.wx_banbie;
			parameters[5].Value = model.wx_cs;
			parameters[6].Value = model.wx_result;
			parameters[7].Value = model.mo_down_cs;
			parameters[8].Value = model.wx_begin_date;
			//parameters[9].Value = model.wx_end_date;
            parameters[9].Value = model.p_status;
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
        /// 确认-增加一条数据
        /// </summary>
        public int Add(MES.Model.MES_SB_QR model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" BEGIN TRY ");
            strSql.Append("     BEGIN TRAN");
            strSql.Append(" insert into MES_SB_QR(");
            strSql.Append("dh, qr_gh, qr_name, qr_banzhu, qr_banbie, qr_remark, qr_date)");
            strSql.Append(" values (");
            strSql.Append("@dh,@qr_gh,@qr_name,@qr_banzhu,@qr_banbie,@qr_remark,@qr_date)");
            strSql.Append(";select @@IDENTITY; ");
            //更新状态
            strSql.Append("update MES_SB_BX set [status]=@status where bx_dh=@dh; ");
            strSql.Append("update MES_Equipment set equip_repair_status='' where equip_no=(select bx_sbno from MES_SB_BX where bx_dh=@dh) and equip_type='压铸机'");
            strSql.Append(" COMMIT TRAN  ");
            strSql.Append(" END TRY ");
            strSql.Append(" BEGIN CATCH ");
            strSql.Append(" ROLLBACK TRAN ");
            strSql.Append(" END CATCH ");
            SqlParameter[] parameters = {
					new SqlParameter("@dh", SqlDbType.VarChar,20),
					new SqlParameter("@qr_gh", SqlDbType.VarChar,20),
					new SqlParameter("@qr_name", SqlDbType.VarChar,20),
					new SqlParameter("@qr_banzhu", SqlDbType.VarChar,10),
					new SqlParameter("@qr_banbie", SqlDbType.VarChar,10),
					new SqlParameter("@qr_remark", SqlDbType.VarChar,100),			
					new SqlParameter("@qr_date", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.VarChar,50)                   
                                        };
            parameters[0].Value = model.dh;
            parameters[1].Value = model.qr_gh;
            parameters[2].Value = model.qr_name;
            parameters[3].Value = model.qr_banzhu;
            parameters[4].Value = model.qr_banbie;
            parameters[5].Value = model.qr_remark;
            parameters[6].Value = model.qr_date;           
            parameters[7].Value = model.p_status;
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
		/// 维修完成-更新一条数据
		/// </summary>
		public bool Update(MES.Model.MES_SB_WX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MES_SB_WX set ");
			//strSql.Append("wx_dh=@wx_dh,");			
			strSql.Append("wx_cs=@wx_cs,");
			strSql.Append("wx_result=@wx_result,");
			strSql.Append("mo_down_cs=@mo_down_cs,");
            strSql.Append("wx_end_date=@wx_end_date where wx_dh=@wx_dh ");
            //更新状态
            strSql.Append("update MES_SB_BX set [status]=@status where bx_dh=@wx_dh;");
			SqlParameter[] parameters = {
					new SqlParameter("@wx_dh", SqlDbType.VarChar,20),					
					new SqlParameter("@wx_cs", SqlDbType.VarChar,100),
					new SqlParameter("@wx_result", SqlDbType.VarChar,20),
					new SqlParameter("@mo_down_cs", SqlDbType.VarChar,50),					
					new SqlParameter("@wx_end_date", SqlDbType.DateTime),

                    new SqlParameter("@status", SqlDbType.VarChar,50)
					};
			parameters[0].Value = model.wx_dh;			
			parameters[1].Value = model.wx_cs;
			parameters[2].Value = model.wx_result;
			parameters[3].Value = model.mo_down_cs;		
			parameters[4].Value = model.wx_end_date;
            parameters[5].Value = model.p_status;

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
        /// 更新保修单状态
        /// </summary>
        //public bool Update(MES.Model.MES_SB_WX model,int  intValue)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update MES_SB_BX set ");
        //    strSql.Append("wx_dh=@wx_dh,");
        //    strSql.Append("wx_gonghao=@wx_gonghao,");
        //    strSql.Append("wx_name=@wx_name,");
        //    strSql.Append("wx_banzhu=@wx_banzhu,");
        //    strSql.Append("wx_banbie=@wx_banbie,");
        //    strSql.Append("wx_cs=@wx_cs,");
        //    strSql.Append("wx_result=@wx_result,");
        //    strSql.Append("mo_down_cs=@mo_down_cs,");
        //    strSql.Append("wx_begin_date=@wx_begin_date,");
        //    strSql.Append("wx_end_date=@wx_end_date");
        //    strSql.Append(" where ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@wx_dh", SqlDbType.VarChar,20),
        //            new SqlParameter("@wx_gonghao", SqlDbType.VarChar,20),
        //            new SqlParameter("@wx_name", SqlDbType.VarChar,20),
        //            new SqlParameter("@wx_banzhu", SqlDbType.VarChar,10),
        //            new SqlParameter("@wx_banbie", SqlDbType.VarChar,10),
        //            new SqlParameter("@wx_cs", SqlDbType.VarChar,100),
        //            new SqlParameter("@wx_result", SqlDbType.VarChar,20),
        //            new SqlParameter("@mo_down_cs", SqlDbType.VarChar,50),
        //            new SqlParameter("@wx_begin_date", SqlDbType.DateTime),
        //            new SqlParameter("@wx_end_date", SqlDbType.DateTime),
        //            new SqlParameter("@id", SqlDbType.Int,4)};
        //    parameters[0].Value = model.wx_dh;
        //    parameters[1].Value = model.wx_gonghao;
        //    parameters[2].Value = model.wx_name;
        //    parameters[3].Value = model.wx_banzhu;
        //    parameters[4].Value = model.wx_banbie;
        //    parameters[5].Value = model.wx_cs;
        //    parameters[6].Value = model.wx_result;
        //    parameters[7].Value = model.mo_down_cs;
        //    parameters[8].Value = model.wx_begin_date;
        //    parameters[9].Value = model.wx_end_date;
        //    parameters[10].Value = model.id;

        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MES_SB_WX ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MES_SB_WX ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public MES.Model.MES_SB_WX GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date,wx_end_date from MES_SB_WX ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			MES.Model.MES_SB_WX model=new MES.Model.MES_SB_WX();
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
		public MES.Model.MES_SB_WX DataRowToModel(DataRow row)
		{
			MES.Model.MES_SB_WX model=new MES.Model.MES_SB_WX();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
                    model.id = int.Parse(row["id"].ToString());
				}
				if(row["wx_dh"]!=null)
				{
					model.wx_dh=row["wx_dh"].ToString();
				}
				if(row["wx_gonghao"]!=null)
				{
					model.wx_gonghao=row["wx_gonghao"].ToString();
				}
				if(row["wx_name"]!=null)
				{
					model.wx_name=row["wx_name"].ToString();
				}
				if(row["wx_banzhu"]!=null)
				{
					model.wx_banzhu=row["wx_banzhu"].ToString();
				}
				if(row["wx_banbie"]!=null)
				{
					model.wx_banbie=row["wx_banbie"].ToString();
				}
				if(row["wx_cs"]!=null)
				{
					model.wx_cs=row["wx_cs"].ToString();
				}
				if(row["wx_result"]!=null)
				{
					model.wx_result=row["wx_result"].ToString();
				}
				if(row["mo_down_cs"]!=null)
				{
					model.mo_down_cs=row["mo_down_cs"].ToString();
				}
				if(row["wx_begin_date"]!=null && row["wx_begin_date"].ToString()!="")
				{
					model.wx_begin_date=DateTime.Parse(row["wx_begin_date"].ToString());
				}
				if(row["wx_end_date"]!=null && row["wx_end_date"].ToString()!="")
				{
					model.wx_end_date=DateTime.Parse(row["wx_end_date"].ToString());
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
            strSql.Append("select id,wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date,wx_end_date,status ");
			strSql.Append(" FROM MES_SB_WX ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        public DataSet GetListBX(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 'true' as ckbox, id, bx_date, bx_banbie, bx_gonghao, bx_name, bx_banzhu, bx_dh, bx_moju_no, bx_moju_type, bx_part, bx_mo_no, bx_gz_type, bx_gz_desc, bx_sbno, bx_sbname, status,format(datediff(mi,bx_date,getdate())/60.0,'0.0')bx_shichang ");
            strSql.Append(" FROM MES_SB_BX ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			strSql.Append(" id,wx_dh,wx_gonghao,wx_name,wx_banzhu,wx_banbie,wx_cs,wx_result,mo_down_cs,wx_begin_date,wx_end_date ");
			strSql.Append(" FROM MES_SB_WX ");
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
			strSql.Append("select count(1) FROM MES_SB_WX ");
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
        /// 是否有确认过
        /// </summary>
        /// <param name="dh"></param>
        /// <returns></returns>
        public int IsExitsQR(string dh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select iif(COUNT(1)>0,1,0) as IsInUse from MES_SB_QR where dh='"+dh+"' ");
          
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

