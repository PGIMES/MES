/**  版本信息模板在安装目录下，可自行修改。
* form4_Customer_mstr.cs
*
* 功 能： N/A
* 类 名： form4_Customer_mstr
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
	/// 数据访问类:form4_Customer_mstr
	/// </summary>
	public partial class form4_Customer_mstr
	{
		public form4_Customer_mstr()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("requestid", "form4_Customer_mstr"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int requestid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from form4_Customer_mstr");
			strSql.Append(" where requestid=@requestid ");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4)			};
			parameters[0].Value = requestid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MES.Model.form4_Customer_mstr model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into form4_Customer_mstr(");
			strSql.Append("requestid,Code,CreateDate,Userid,UserName,UserName_AD,dept,managerid,manager,manager_AD,Class,CmClassID,cmClassName,cm_domain,BusinessRelationCode,BusinessRelationName1,AddressSearchName,AddressTypeCode,AddressStreet1,cm_lang,ctry_country,cm_region,AddressZip,AddressCity,AddressState,ad_fax,ad_phone,AddressEMail,ContactName,ContactGender,ContactTelephone,ContactEmail,AddressIsTaxable,AddressIsTaxIncluded,AddressIsTaxInCity,TxzTaxZone,TxclTaxCls,LedgerDays,cm_slspsn,cm_pr_list,cm_fix_pr,BankNumberIsActive,InvControlGLProfileCode,CnControlGLProfileCode,PrepayControlGLProfileCode,SalesAccountGLProfileCode,ReasonCode,NormalPaymentConditionCode,CurrencyCode,BankAccFormatCode,BankNumberFormatted,OwnBankNumber,DebtorTypeCode,ExistsClass,Status_id)");
			strSql.Append(" values (");
			strSql.Append("@requestid,@Code,@CreateDate,@Userid,@UserName,@UserName_AD,@dept,@managerid,@manager,@manager_AD,@Class,@CmClassID,@cmClassName,@cm_domain,@BusinessRelationCode,@BusinessRelationName1,@AddressSearchName,@AddressTypeCode,@AddressStreet1,@cm_lang,@ctry_country,@cm_region,@AddressZip,@AddressCity,@AddressState,@ad_fax,@ad_phone,@AddressEMail,@ContactName,@ContactGender,@ContactTelephone,@ContactEmail,@AddressIsTaxable,@AddressIsTaxIncluded,@AddressIsTaxInCity,@TxzTaxZone,@TxclTaxCls,@LedgerDays,@cm_slspsn,@cm_pr_list,@cm_fix_pr,@BankNumberIsActive,@InvControlGLProfileCode,@CnControlGLProfileCode,@PrepayControlGLProfileCode,@SalesAccountGLProfileCode,@ReasonCode,@NormalPaymentConditionCode,@CurrencyCode,@BankAccFormatCode,@BankNumberFormatted,@OwnBankNumber,@DebtorTypeCode,@ExistsClass,@Status_id)");
			SqlParameter[] parameters = {
				//	new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@requestid", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Userid", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName_AD", SqlDbType.NVarChar,50),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@managerid", SqlDbType.NVarChar,50),
					new SqlParameter("@manager", SqlDbType.NVarChar,50),
					new SqlParameter("@manager_AD", SqlDbType.NVarChar,50),
					new SqlParameter("@Class", SqlDbType.NVarChar,10),
					new SqlParameter("@CmClassID", SqlDbType.NVarChar,20),
					new SqlParameter("@cmClassName", SqlDbType.NVarChar,100),
					new SqlParameter("@cm_domain", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessRelationCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessRelationName1", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressSearchName", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressStreet1", SqlDbType.NVarChar,100),
					new SqlParameter("@cm_lang", SqlDbType.NVarChar,50),
					new SqlParameter("@ctry_country", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_region", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressZip", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressCity", SqlDbType.VarChar,50),
					new SqlParameter("@AddressState", SqlDbType.NVarChar,50),
					new SqlParameter("@ad_fax", SqlDbType.NVarChar,50),
					new SqlParameter("@ad_phone", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressEMail", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactGender", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactTelephone", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressIsTaxable", SqlDbType.VarChar,150),
					new SqlParameter("@AddressIsTaxIncluded", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressIsTaxInCity", SqlDbType.NVarChar,50),
					new SqlParameter("@TxzTaxZone", SqlDbType.NVarChar,50),
					new SqlParameter("@TxclTaxCls", SqlDbType.NVarChar,50),
					new SqlParameter("@LedgerDays", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_slspsn", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_pr_list", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_fix_pr", SqlDbType.NVarChar,50),
					new SqlParameter("@BankNumberIsActive", SqlDbType.NVarChar,50),
					new SqlParameter("@InvControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CnControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PrepayControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SalesAccountGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReasonCode", SqlDbType.NVarChar,50),
					new SqlParameter("@NormalPaymentConditionCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CurrencyCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccFormatCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankNumberFormatted", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnBankNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DebtorTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExistsClass", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status_id", SqlDbType.NVarChar,50)

            };
			//parameters[0].Value = model.ID;
			parameters[0].Value = model.requestid;
			parameters[1].Value = model.Code;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.Userid;
			parameters[4].Value = model.UserName;
			parameters[5].Value = model.UserName_AD;
			parameters[6].Value = model.dept;
			parameters[7].Value = model.managerid;
			parameters[8].Value = model.manager;
			parameters[9].Value = model.manager_AD;
			parameters[10].Value = model.Class;
			parameters[11].Value = model.CmClassID;
			parameters[12].Value = model.cmClassName;
			parameters[13].Value = model.cm_domain;
			parameters[14].Value = model.BusinessRelationCode;
			parameters[15].Value = model.BusinessRelationName1;
			parameters[16].Value = model.AddressSearchName;
			parameters[17].Value = model.AddressTypeCode;
			parameters[18].Value = model.AddressStreet1;
			parameters[19].Value = model.cm_lang;
			parameters[20].Value = model.ctry_country;
			parameters[21].Value = model.cm_region;
			parameters[22].Value = model.AddressZip;
			parameters[23].Value = model.AddressCity;
			parameters[24].Value = model.AddressState;
			parameters[25].Value = model.ad_fax;
			parameters[26].Value = model.ad_phone;
			parameters[27].Value = model.AddressEMail;
			parameters[28].Value = model.ContactName;
			parameters[29].Value = model.ContactGender;
			parameters[30].Value = model.ContactTelephone;
			parameters[31].Value = model.ContactEmail;
			parameters[32].Value = model.AddressIsTaxable;
			parameters[33].Value = model.AddressIsTaxIncluded;
			parameters[34].Value = model.AddressIsTaxInCity;
			parameters[35].Value = model.TxzTaxZone;
			parameters[36].Value = model.TxclTaxCls;
			parameters[37].Value = model.LedgerDays;
			parameters[38].Value = model.cm_slspsn;
			parameters[39].Value = model.cm_pr_list;
			parameters[40].Value = model.cm_fix_pr;
			parameters[41].Value = model.BankNumberIsActive;
			parameters[42].Value = model.InvControlGLProfileCode;
			parameters[43].Value = model.CnControlGLProfileCode;
			parameters[44].Value = model.PrepayControlGLProfileCode;
			parameters[45].Value = model.SalesAccountGLProfileCode;
			parameters[46].Value = model.ReasonCode;
			parameters[47].Value = model.NormalPaymentConditionCode;
			parameters[48].Value = model.CurrencyCode;
			parameters[49].Value = model.BankAccFormatCode;
			parameters[50].Value = model.BankNumberFormatted;
			parameters[51].Value = model.OwnBankNumber;
			parameters[52].Value = model.DebtorTypeCode;
            parameters[53].Value = model.ExistsClass;
            parameters[54].Value = model.Status_id;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(MES.Model.form4_Customer_mstr model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update form4_Customer_mstr set ");
			strSql.Append("@ID=@ID,");
			strSql.Append("Code=@Code,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("Userid=@Userid,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserName_AD=@UserName_AD,");
			strSql.Append("dept=@dept,");
			strSql.Append("managerid=@managerid,");
			strSql.Append("manager=@manager,");
			strSql.Append("manager_AD=@manager_AD,");
			strSql.Append("Class=@Class,");
			strSql.Append("CmClassID=@CmClassID,");
			strSql.Append("cmClassName=@cmClassName,");
			strSql.Append("cm_domain=@cm_domain,");
			strSql.Append("BusinessRelationCode=@BusinessRelationCode,");
			strSql.Append("BusinessRelationName1=@BusinessRelationName1,");
			strSql.Append("AddressSearchName=@AddressSearchName,");
			strSql.Append("AddressTypeCode=@AddressTypeCode,");
			strSql.Append("AddressStreet1=@AddressStreet1,");
			strSql.Append("cm_lang=@cm_lang,");
			strSql.Append("ctry_country=@ctry_country,");
			strSql.Append("cm_region=@cm_region,");
			strSql.Append("AddressZip=@AddressZip,");
			strSql.Append("AddressCity=@AddressCity,");
			strSql.Append("AddressState=@AddressState,");
			strSql.Append("ad_fax=@ad_fax,");
			strSql.Append("ad_phone=@ad_phone,");
			strSql.Append("AddressEMail=@AddressEMail,");
			strSql.Append("ContactName=@ContactName,");
			strSql.Append("ContactGender=@ContactGender,");
			strSql.Append("ContactTelephone=@ContactTelephone,");
			strSql.Append("ContactEmail=@ContactEmail,");
			strSql.Append("AddressIsTaxable=@AddressIsTaxable,");
			strSql.Append("AddressIsTaxIncluded=@AddressIsTaxIncluded,");
			strSql.Append("AddressIsTaxInCity=@AddressIsTaxInCity,");
			strSql.Append("TxzTaxZone=@TxzTaxZone,");
			strSql.Append("TxclTaxCls=@TxclTaxCls,");
			strSql.Append("LedgerDays=@LedgerDays,");
			strSql.Append("cm_slspsn=@cm_slspsn,");
			strSql.Append("cm_pr_list=@cm_pr_list,");
			strSql.Append("cm_fix_pr=@cm_fix_pr,");
			strSql.Append("BankNumberIsActive=@BankNumberIsActive,");
			strSql.Append("InvControlGLProfileCode=@InvControlGLProfileCode,");
			strSql.Append("CnControlGLProfileCode=@CnControlGLProfileCode,");
			strSql.Append("PrepayControlGLProfileCode=@PrepayControlGLProfileCode,");
			strSql.Append("SalesAccountGLProfileCode=@SalesAccountGLProfileCode,");
			strSql.Append("ReasonCode=@ReasonCode,");
			strSql.Append("NormalPaymentConditionCode=@NormalPaymentConditionCode,");
			strSql.Append("CurrencyCode=@CurrencyCode,");
			strSql.Append("BankAccFormatCode=@BankAccFormatCode,");
			strSql.Append("BankNumberFormatted=@BankNumberFormatted,");
			strSql.Append("OwnBankNumber=@OwnBankNumber,");
			strSql.Append("DebtorTypeCode=@DebtorTypeCode,");
            strSql.Append("ExistsClass=@ExistsClass,");
            strSql.Append("Status_id=@Status_id");
            strSql.Append(" where requestid=@requestid ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Userid", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName_AD", SqlDbType.NVarChar,50),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@managerid", SqlDbType.NVarChar,50),
					new SqlParameter("@manager", SqlDbType.NVarChar,50),
					new SqlParameter("@manager_AD", SqlDbType.NVarChar,50),
					new SqlParameter("@Class", SqlDbType.NVarChar,10),
					new SqlParameter("@CmClassID", SqlDbType.NVarChar,20),
					new SqlParameter("@cmClassName", SqlDbType.NVarChar,100),
					new SqlParameter("@cm_domain", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessRelationCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessRelationName1", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressSearchName", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressStreet1", SqlDbType.NVarChar,100),
					new SqlParameter("@cm_lang", SqlDbType.NVarChar,50),
					new SqlParameter("@ctry_country", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_region", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressZip", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressCity", SqlDbType.VarChar,50),
					new SqlParameter("@AddressState", SqlDbType.NVarChar,50),
					new SqlParameter("@ad_fax", SqlDbType.NVarChar,50),
					new SqlParameter("@ad_phone", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressEMail", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactGender", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactTelephone", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressIsTaxable", SqlDbType.VarChar,150),
					new SqlParameter("@AddressIsTaxIncluded", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressIsTaxInCity", SqlDbType.NVarChar,50),
					new SqlParameter("@TxzTaxZone", SqlDbType.NVarChar,50),
					new SqlParameter("@TxclTaxCls", SqlDbType.NVarChar,50),
					new SqlParameter("@LedgerDays", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_slspsn", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_pr_list", SqlDbType.NVarChar,50),
					new SqlParameter("@cm_fix_pr", SqlDbType.NVarChar,50),
					new SqlParameter("@BankNumberIsActive", SqlDbType.NVarChar,50),
					new SqlParameter("@InvControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CnControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PrepayControlGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SalesAccountGLProfileCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReasonCode", SqlDbType.NVarChar,50),
					new SqlParameter("@NormalPaymentConditionCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CurrencyCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccFormatCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankNumberFormatted", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnBankNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DebtorTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExistsClass", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status_id", SqlDbType.NVarChar,50),
                    new SqlParameter("@requestid", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Code;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.Userid;
			parameters[4].Value = model.UserName;
			parameters[5].Value = model.UserName_AD;
			parameters[6].Value = model.dept;
			parameters[7].Value = model.managerid;
			parameters[8].Value = model.manager;
			parameters[9].Value = model.manager_AD;
			parameters[10].Value = model.Class;
			parameters[11].Value = model.CmClassID;
			parameters[12].Value = model.cmClassName;
			parameters[13].Value = model.cm_domain;
			parameters[14].Value = model.BusinessRelationCode;
			parameters[15].Value = model.BusinessRelationName1;
			parameters[16].Value = model.AddressSearchName;
			parameters[17].Value = model.AddressTypeCode;
			parameters[18].Value = model.AddressStreet1;
			parameters[19].Value = model.cm_lang;
			parameters[20].Value = model.ctry_country;
			parameters[21].Value = model.cm_region;
			parameters[22].Value = model.AddressZip;
			parameters[23].Value = model.AddressCity;
			parameters[24].Value = model.AddressState;
			parameters[25].Value = model.ad_fax;
			parameters[26].Value = model.ad_phone;
			parameters[27].Value = model.AddressEMail;
			parameters[28].Value = model.ContactName;
			parameters[29].Value = model.ContactGender;
			parameters[30].Value = model.ContactTelephone;
			parameters[31].Value = model.ContactEmail;
			parameters[32].Value = model.AddressIsTaxable;
			parameters[33].Value = model.AddressIsTaxIncluded;
			parameters[34].Value = model.AddressIsTaxInCity;
			parameters[35].Value = model.TxzTaxZone;
			parameters[36].Value = model.TxclTaxCls;
			parameters[37].Value = model.LedgerDays;
			parameters[38].Value = model.cm_slspsn;
			parameters[39].Value = model.cm_pr_list;
			parameters[40].Value = model.cm_fix_pr;
			parameters[41].Value = model.BankNumberIsActive;
			parameters[42].Value = model.InvControlGLProfileCode;
			parameters[43].Value = model.CnControlGLProfileCode;
			parameters[44].Value = model.PrepayControlGLProfileCode;
			parameters[45].Value = model.SalesAccountGLProfileCode;
			parameters[46].Value = model.ReasonCode;
			parameters[47].Value = model.NormalPaymentConditionCode;
			parameters[48].Value = model.CurrencyCode;
			parameters[49].Value = model.BankAccFormatCode;
			parameters[50].Value = model.BankNumberFormatted;
			parameters[51].Value = model.OwnBankNumber;
			parameters[52].Value = model.DebtorTypeCode;
            parameters[53].Value = model.ExistsClass;
            parameters[54].Value = model.Status_id;
            parameters[55].Value = model.requestid;
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
		public bool Delete(int requestid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from form4_Customer_mstr ");
			strSql.Append(" where requestid=@requestid ");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4)			};
			parameters[0].Value = requestid;

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
		public bool DeleteList(string requestidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from form4_Customer_mstr ");
			strSql.Append(" where requestid in ("+requestidlist + ")  ");
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
		public MES.Model.form4_Customer_mstr GetModel(int requestid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,requestid,Code,CreateDate,Userid,UserName,UserName_AD,dept,managerid,manager,manager_AD,Class,CmClassID,cmClassName,cm_domain,BusinessRelationCode,BusinessRelationName1,AddressSearchName,AddressTypeCode,AddressStreet1,cm_lang,ctry_country,cm_region,AddressZip,AddressCity,AddressState,ad_fax,ad_phone,AddressEMail,ContactName,ContactGender,ContactTelephone,ContactEmail,AddressIsTaxable,AddressIsTaxIncluded,AddressIsTaxInCity,TxzTaxZone,TxclTaxCls,LedgerDays,cm_slspsn,cm_pr_list,cm_fix_pr,BankNumberIsActive,InvControlGLProfileCode,CnControlGLProfileCode,PrepayControlGLProfileCode,SalesAccountGLProfileCode,ReasonCode,NormalPaymentConditionCode,CurrencyCode,BankAccFormatCode,BankNumberFormatted,OwnBankNumber,DebtorTypeCode,ExistsClass,Status_id from form4_Customer_mstr ");
			strSql.Append(" where requestid=@requestid ");
			SqlParameter[] parameters = {
					new SqlParameter("@requestid", SqlDbType.Int,4)			};
			parameters[0].Value = requestid;

			MES.Model.form4_Customer_mstr model=new MES.Model.form4_Customer_mstr();
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
		public MES.Model.form4_Customer_mstr DataRowToModel(DataRow row)
		{
			MES.Model.form4_Customer_mstr model=new MES.Model.form4_Customer_mstr();
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
				if(row["Code"]!=null)
				{
					model.Code=row["Code"].ToString();
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
				if(row["Userid"]!=null)
				{
					model.Userid=row["Userid"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserName_AD"]!=null)
				{
					model.UserName_AD=row["UserName_AD"].ToString();
				}
				if(row["dept"]!=null)
				{
					model.dept=row["dept"].ToString();
				}
				if(row["managerid"]!=null)
				{
					model.managerid=row["managerid"].ToString();
				}
				if(row["manager"]!=null)
				{
					model.manager=row["manager"].ToString();
				}
				if(row["manager_AD"]!=null)
				{
					model.manager_AD=row["manager_AD"].ToString();
				}
				if(row["Class"]!=null)
				{
					model.Class=row["Class"].ToString();
				}
				if(row["CmClassID"]!=null)
				{
					model.CmClassID=row["CmClassID"].ToString();
				}
				if(row["cmClassName"]!=null)
				{
					model.cmClassName=row["cmClassName"].ToString();
				}
				if(row["cm_domain"]!=null)
				{
					model.cm_domain=row["cm_domain"].ToString();
				}
				if(row["BusinessRelationCode"]!=null)
				{
					model.BusinessRelationCode=row["BusinessRelationCode"].ToString();
				}
				if(row["BusinessRelationName1"]!=null)
				{
					model.BusinessRelationName1=row["BusinessRelationName1"].ToString();
				}
				if(row["AddressSearchName"]!=null)
				{
					model.AddressSearchName=row["AddressSearchName"].ToString();
				}
				if(row["AddressTypeCode"]!=null)
				{
					model.AddressTypeCode=row["AddressTypeCode"].ToString();
				}
				if(row["AddressStreet1"]!=null)
				{
					model.AddressStreet1=row["AddressStreet1"].ToString();
				}
				if(row["cm_lang"]!=null)
				{
					model.cm_lang=row["cm_lang"].ToString();
				}
				if(row["ctry_country"]!=null)
				{
					model.ctry_country=row["ctry_country"].ToString();
				}
				if(row["cm_region"]!=null)
				{
					model.cm_region=row["cm_region"].ToString();
				}
				if(row["AddressZip"]!=null)
				{
					model.AddressZip=row["AddressZip"].ToString();
				}
				if(row["AddressCity"]!=null)
				{
					model.AddressCity=row["AddressCity"].ToString();
				}
				if(row["AddressState"]!=null)
				{
					model.AddressState=row["AddressState"].ToString();
				}
				if(row["ad_fax"]!=null)
				{
					model.ad_fax=row["ad_fax"].ToString();
				}
				if(row["ad_phone"]!=null)
				{
					model.ad_phone=row["ad_phone"].ToString();
				}
				if(row["AddressEMail"]!=null)
				{
					model.AddressEMail=row["AddressEMail"].ToString();
				}
				if(row["ContactName"]!=null)
				{
					model.ContactName=row["ContactName"].ToString();
				}
				if(row["ContactGender"]!=null)
				{
					model.ContactGender=row["ContactGender"].ToString();
				}
				if(row["ContactTelephone"]!=null)
				{
					model.ContactTelephone=row["ContactTelephone"].ToString();
				}
				if(row["ContactEmail"]!=null)
				{
					model.ContactEmail=row["ContactEmail"].ToString();
				}
				if(row["AddressIsTaxable"]!=null)
				{
					model.AddressIsTaxable=row["AddressIsTaxable"].ToString();
				}
				if(row["AddressIsTaxIncluded"]!=null)
				{
					model.AddressIsTaxIncluded=row["AddressIsTaxIncluded"].ToString();
				}
				if(row["AddressIsTaxInCity"]!=null)
				{
					model.AddressIsTaxInCity=row["AddressIsTaxInCity"].ToString();
				}
				if(row["TxzTaxZone"]!=null)
				{
					model.TxzTaxZone=row["TxzTaxZone"].ToString();
				}
				if(row["TxclTaxCls"]!=null)
				{
					model.TxclTaxCls=row["TxclTaxCls"].ToString();
				}
				if(row["LedgerDays"]!=null)
				{
					model.LedgerDays=row["LedgerDays"].ToString();
				}
				if(row["cm_slspsn"]!=null)
				{
					model.cm_slspsn=row["cm_slspsn"].ToString();
				}
				if(row["cm_pr_list"]!=null)
				{
					model.cm_pr_list=row["cm_pr_list"].ToString();
				}
				if(row["cm_fix_pr"]!=null)
				{
					model.cm_fix_pr=row["cm_fix_pr"].ToString();
				}
				if(row["BankNumberIsActive"]!=null)
				{
					model.BankNumberIsActive=row["BankNumberIsActive"].ToString();
				}
				if(row["InvControlGLProfileCode"]!=null)
				{
					model.InvControlGLProfileCode=row["InvControlGLProfileCode"].ToString();
				}
				if(row["CnControlGLProfileCode"]!=null)
				{
					model.CnControlGLProfileCode=row["CnControlGLProfileCode"].ToString();
				}
				if(row["PrepayControlGLProfileCode"]!=null)
				{
					model.PrepayControlGLProfileCode=row["PrepayControlGLProfileCode"].ToString();
				}
				if(row["SalesAccountGLProfileCode"]!=null)
				{
					model.SalesAccountGLProfileCode=row["SalesAccountGLProfileCode"].ToString();
				}
				if(row["ReasonCode"]!=null)
				{
					model.ReasonCode=row["ReasonCode"].ToString();
				}
				if(row["NormalPaymentConditionCode"]!=null)
				{
					model.NormalPaymentConditionCode=row["NormalPaymentConditionCode"].ToString();
				}
				if(row["CurrencyCode"]!=null)
				{
					model.CurrencyCode=row["CurrencyCode"].ToString();
				}
				if(row["BankAccFormatCode"]!=null)
				{
					model.BankAccFormatCode=row["BankAccFormatCode"].ToString();
				}
				if(row["BankNumberFormatted"]!=null)
				{
					model.BankNumberFormatted=row["BankNumberFormatted"].ToString();
				}
				if(row["OwnBankNumber"]!=null)
				{
					model.OwnBankNumber=row["OwnBankNumber"].ToString();
				}
				if(row["DebtorTypeCode"]!=null)
				{
					model.DebtorTypeCode=row["DebtorTypeCode"].ToString();
				}
                if (row["ExistsClass"] != null)
                {
                    model.ExistsClass = row["ExistsClass"].ToString();
                }
                if (row["Status_id"] != null)
                {
                    model.Status_id = row["Status_id"].ToString();
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
			strSql.Append("select ID,requestid,Code,CreateDate,Userid,UserName,UserName_AD,dept,managerid,manager,manager_AD,Class,CmClassID,cmClassName,cm_domain,BusinessRelationCode,BusinessRelationName1,AddressSearchName,AddressTypeCode,AddressStreet1,cm_lang,ctry_country,cm_region,AddressZip,AddressCity,AddressState,ad_fax,ad_phone,AddressEMail,ContactName,ContactGender,ContactTelephone,ContactEmail,AddressIsTaxable,AddressIsTaxIncluded,AddressIsTaxInCity,TxzTaxZone,TxclTaxCls,LedgerDays,cm_slspsn,cm_pr_list,cm_fix_pr,BankNumberIsActive,InvControlGLProfileCode,CnControlGLProfileCode,PrepayControlGLProfileCode,SalesAccountGLProfileCode,ReasonCode,NormalPaymentConditionCode,CurrencyCode,BankAccFormatCode,BankNumberFormatted,OwnBankNumber,DebtorTypeCode,ExistsClass,Status_id ");
			strSql.Append(" FROM form4_Customer_mstr ");
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
			strSql.Append(" ID,requestid,Code,CreateDate,Userid,UserName,UserName_AD,dept,managerid,manager,manager_AD,Class,CmClassID,cmClassName,cm_domain,BusinessRelationCode,BusinessRelationName1,AddressSearchName,AddressTypeCode,AddressStreet1,cm_lang,ctry_country,cm_region,AddressZip,AddressCity,AddressState,ad_fax,ad_phone,AddressEMail,ContactName,ContactGender,ContactTelephone,ContactEmail,AddressIsTaxable,AddressIsTaxIncluded,AddressIsTaxInCity,TxzTaxZone,TxclTaxCls,LedgerDays,cm_slspsn,cm_pr_list,cm_fix_pr,BankNumberIsActive,InvControlGLProfileCode,CnControlGLProfileCode,PrepayControlGLProfileCode,SalesAccountGLProfileCode,ReasonCode,NormalPaymentConditionCode,CurrencyCode,BankAccFormatCode,BankNumberFormatted,OwnBankNumber,DebtorTypeCode,ExistsClass,Status_id ");
			strSql.Append(" FROM form4_Customer_mstr ");
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
			strSql.Append("select count(1) FROM form4_Customer_mstr ");
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
				strSql.Append("order by T.requestid desc");
			}
			strSql.Append(")AS Row, T.*  from form4_Customer_mstr T ");
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
			parameters[0].Value = "form4_Customer_mstr";
			parameters[1].Value = "requestid";
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

