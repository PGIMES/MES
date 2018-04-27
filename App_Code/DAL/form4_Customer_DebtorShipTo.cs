/**  版本信息模板在安装目录下，可自行修改。
* form4_Customer_DebtorShipTo.cs
*
* 功 能： N/A
* 类 名： form4_Customer_DebtorShipTo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/17 16:25:27   N/A    初版
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
	/// 数据访问类:form4_Customer_DebtorShipTo
	/// </summary>
	public partial class form4_Customer_DebtorShipTo
	{
		public form4_Customer_DebtorShipTo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("requestid", "form4_Customer_DebtorShipTo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int requestid,int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from form4_Customer_DebtorShipTo");
			strSql.Append(" where requestid=@requestid and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = requestid;
			parameters[1].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MES.Model.form4_Customer_DebtorShipTo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into form4_Customer_DebtorShipTo(");
			strSql.Append("requestid,BusinessRelationCode,ShipToNum,DebtorShipToCode,DebtorShipToName,cm_addr,AddressTypeCode,AddressStreet1,AddressZip,AddressCity,cm_region,ctry_country,Debtor_Domain,IsEffective,UpdateDate,UpdateById,UpdateByName,LngCode,turns)");
			strSql.Append(" values (");
			strSql.Append("@requestid,@BusinessRelationCode,@ShipToNum,@DebtorShipToCode,@DebtorShipToName,@cm_addr,@AddressTypeCode,@AddressStreet1,@AddressZip,@AddressCity,@cm_region,@ctry_country,@Debtor_Domain,@IsEffective,@UpdateDate,@UpdateById,@UpdateByName,@LngCode,@turns)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@BusinessRelationCode", SqlDbType.VarChar,10),
					new SqlParameter("@ShipToNum", SqlDbType.Int,4),
					new SqlParameter("@DebtorShipToCode", SqlDbType.VarChar,10),
					new SqlParameter("@DebtorShipToName", SqlDbType.VarChar,100),
					new SqlParameter("@cm_addr", SqlDbType.VarChar,50),
					new SqlParameter("@AddressTypeCode", SqlDbType.VarChar,50),
					new SqlParameter("@AddressStreet1", SqlDbType.VarChar,2000),
					new SqlParameter("@AddressZip", SqlDbType.VarChar,10),
					new SqlParameter("@AddressCity", SqlDbType.VarChar,50),
					new SqlParameter("@cm_region", SqlDbType.VarChar,20),
					new SqlParameter("@ctry_country", SqlDbType.VarChar,50),
					new SqlParameter("@Debtor_Domain", SqlDbType.VarChar,50),
					new SqlParameter("@IsEffective", SqlDbType.VarChar,10),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdateById", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateByName", SqlDbType.VarChar,20),
					new SqlParameter("@LngCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@turns", SqlDbType.NVarChar,50)

            };

			parameters[0].Value = model.requestid;
			parameters[1].Value = model.BusinessRelationCode;
			parameters[2].Value = model.ShipToNum;
			parameters[3].Value = model.DebtorShipToCode;
			parameters[4].Value = model.DebtorShipToName;
			parameters[5].Value = model.cm_addr;
			parameters[6].Value = model.AddressTypeCode;
			parameters[7].Value = model.AddressStreet1;
			parameters[8].Value = model.AddressZip;
			parameters[9].Value = model.AddressCity;
			parameters[10].Value = model.cm_region;
			parameters[11].Value = model.ctry_country;
			parameters[12].Value = model.Debtor_Domain;
			parameters[13].Value = model.IsEffective;
			parameters[14].Value = model.UpdateDate;
			parameters[15].Value = model.UpdateById;
			parameters[16].Value = model.UpdateByName;
			parameters[17].Value = model.LngCode;
            parameters[18].Value = model.turns;


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
		public bool Update(MES.Model.form4_Customer_DebtorShipTo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update form4_Customer_DebtorShipTo set ");
			strSql.Append("BusinessRelationCode=@BusinessRelationCode,");
			strSql.Append("ShipToNum=@ShipToNum,");
			strSql.Append("DebtorShipToCode=@DebtorShipToCode,");
			strSql.Append("DebtorShipToName=@DebtorShipToName,");
			strSql.Append("cm_addr=@cm_addr,");
			strSql.Append("AddressTypeCode=@AddressTypeCode,");
			strSql.Append("AddressStreet1=@AddressStreet1,");
			strSql.Append("AddressZip=@AddressZip,");
			strSql.Append("AddressCity=@AddressCity,");
			strSql.Append("cm_region=@cm_region,");
			strSql.Append("ctry_country=@ctry_country,");
			strSql.Append("Debtor_Domain=@Debtor_Domain,");
			strSql.Append("IsEffective=@IsEffective,");
			strSql.Append("UpdateDate=@UpdateDate,");
			strSql.Append("UpdateById=@UpdateById,");
			strSql.Append("UpdateByName=@UpdateByName,");
			strSql.Append("LngCode=@LngCode");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@BusinessRelationCode", SqlDbType.VarChar,10),
					new SqlParameter("@ShipToNum", SqlDbType.Int,4),
					new SqlParameter("@DebtorShipToCode", SqlDbType.VarChar,10),
					new SqlParameter("@DebtorShipToName", SqlDbType.VarChar,50),
					new SqlParameter("@cm_addr", SqlDbType.VarChar,50),
					new SqlParameter("@AddressTypeCode", SqlDbType.VarChar,50),
					new SqlParameter("@AddressStreet1", SqlDbType.VarChar,50),
					new SqlParameter("@AddressZip", SqlDbType.VarChar,10),
					new SqlParameter("@AddressCity", SqlDbType.VarChar,50),
					new SqlParameter("@cm_region", SqlDbType.VarChar,20),
					new SqlParameter("@ctry_country", SqlDbType.VarChar,50),
					new SqlParameter("@Debtor_Domain", SqlDbType.VarChar,50),
					new SqlParameter("@IsEffective", SqlDbType.VarChar,10),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdateById", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateByName", SqlDbType.VarChar,20),
					new SqlParameter("@LngCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@requestid", SqlDbType.Int,4)};
			parameters[0].Value = model.BusinessRelationCode;
			parameters[1].Value = model.ShipToNum;
			parameters[2].Value = model.DebtorShipToCode;
			parameters[3].Value = model.DebtorShipToName;
			parameters[4].Value = model.cm_addr;
			parameters[5].Value = model.AddressTypeCode;
			parameters[6].Value = model.AddressStreet1;
			parameters[7].Value = model.AddressZip;
			parameters[8].Value = model.AddressCity;
			parameters[9].Value = model.cm_region;
			parameters[10].Value = model.ctry_country;
			parameters[11].Value = model.Debtor_Domain;
			parameters[12].Value = model.IsEffective;
			parameters[13].Value = model.UpdateDate;
			parameters[14].Value = model.UpdateById;
			parameters[15].Value = model.UpdateByName;
			parameters[16].Value = model.LngCode;
			parameters[17].Value = model.ID;
			parameters[18].Value = model.requestid;

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
			strSql.Append("delete from form4_Customer_DebtorShipTo ");
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int requestid,int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from form4_Customer_DebtorShipTo ");
			strSql.Append(" where requestid=@requestid and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = requestid;
			parameters[1].Value = ID;

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
			strSql.Append("delete from form4_Customer_DebtorShipTo ");
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
		public MES.Model.form4_Customer_DebtorShipTo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,requestid,BusinessRelationCode,ShipToNum,DebtorShipToCode,DebtorShipToName,cm_addr,AddressTypeCode,AddressStreet1,AddressZip,AddressCity,cm_region,ctry_country,Debtor_Domain,IsEffective,UpdateDate,UpdateById,UpdateByName,LngCode,turns from form4_Customer_DebtorShipTo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			MES.Model.form4_Customer_DebtorShipTo model=new MES.Model.form4_Customer_DebtorShipTo();
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
		public MES.Model.form4_Customer_DebtorShipTo DataRowToModel(DataRow row)
		{
			MES.Model.form4_Customer_DebtorShipTo model=new MES.Model.form4_Customer_DebtorShipTo();
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
				if(row["BusinessRelationCode"]!=null)
				{
					model.BusinessRelationCode=row["BusinessRelationCode"].ToString();
				}
				if(row["ShipToNum"]!=null && row["ShipToNum"].ToString()!="")
				{
					model.ShipToNum=int.Parse(row["ShipToNum"].ToString());
				}
				if(row["DebtorShipToCode"]!=null)
				{
					model.DebtorShipToCode=row["DebtorShipToCode"].ToString();
				}
				if(row["DebtorShipToName"]!=null)
				{
					model.DebtorShipToName=row["DebtorShipToName"].ToString();
				}
				if(row["cm_addr"]!=null)
				{
					model.cm_addr=row["cm_addr"].ToString();
				}
				if(row["AddressTypeCode"]!=null)
				{
					model.AddressTypeCode=row["AddressTypeCode"].ToString();
				}
				if(row["AddressStreet1"]!=null)
				{
					model.AddressStreet1=row["AddressStreet1"].ToString();
				}
				if(row["AddressZip"]!=null)
				{
					model.AddressZip=row["AddressZip"].ToString();
				}
				if(row["AddressCity"]!=null)
				{
					model.AddressCity=row["AddressCity"].ToString();
				}
				if(row["cm_region"]!=null)
				{
					model.cm_region=row["cm_region"].ToString();
				}
				if(row["ctry_country"]!=null)
				{
					model.ctry_country=row["ctry_country"].ToString();
				}
				if(row["Debtor_Domain"]!=null)
				{
					model.Debtor_Domain=row["Debtor_Domain"].ToString();
				}
				if(row["IsEffective"]!=null)
				{
					model.IsEffective=row["IsEffective"].ToString();
				}
				if(row["UpdateDate"]!=null && row["UpdateDate"].ToString()!="")
				{
					model.UpdateDate=DateTime.Parse(row["UpdateDate"].ToString());
				}
				if(row["UpdateById"]!=null)
				{
					model.UpdateById=row["UpdateById"].ToString();
				}
				if(row["UpdateByName"]!=null)
				{
					model.UpdateByName=row["UpdateByName"].ToString();
				}
				if(row["LngCode"]!=null)
				{
					model.LngCode=row["LngCode"].ToString();
				}
                if (row["turns"] != null)
                {
                    model.turns = row["turns"].ToString();
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
			strSql.Append("select ID,requestid,BusinessRelationCode,ShipToNum,DebtorShipToCode,DebtorShipToName,cm_addr,AddressTypeCode,AddressStreet1,AddressZip,AddressCity,cm_region,ctry_country,Debtor_Domain,IsEffective,UpdateDate,UpdateById,UpdateByName,LngCode,turns ");
			strSql.Append(" FROM form4_Customer_DebtorShipTo ");
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
			strSql.Append(" ID,requestid,BusinessRelationCode,ShipToNum,DebtorShipToCode,DebtorShipToName,cm_addr,AddressTypeCode,AddressStreet1,AddressZip,AddressCity,cm_region,ctry_country,Debtor_Domain,IsEffective,UpdateDate,UpdateById,UpdateByName,LngCode,turns ");
			strSql.Append(" FROM form4_Customer_DebtorShipTo ");
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
			strSql.Append("select count(1) FROM form4_Customer_DebtorShipTo ");
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
			strSql.Append(")AS Row, T.*  from form4_Customer_DebtorShipTo T ");
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
			parameters[0].Value = "form4_Customer_DebtorShipTo";
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

