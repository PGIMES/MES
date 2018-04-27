/**  版本信息模板在安装目录下，可自行修改。
* form4_Customer_mstr.cs
*
* 功 能： N/A
* 类 名： form4_Customer_mstr
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/17 16:25:26   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace MES.Model
{
	/// <summary>
	/// form4_Customer_mstr:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class form4_Customer_mstr
	{
		public form4_Customer_mstr()
		{}
        #region Model
        private int _id;
        private int _requestid;
		private string _code;
		private DateTime? _createdate;
		private string _userid;
		private string _username;
		private string _username_ad;
		private string _dept;
		private string _managerid;
		private string _manager;
		private string _manager_ad;
		private string _class;
		private string _cmclassid;
		private string _cmclassname;
		private string _cm_domain;
		private string _businessrelationcode;
		private string _businessrelationname1;
		private string _addresssearchname;
		private string _addresstypecode;
		private string _addressstreet1;
		private string _cm_lang;
		private string _ctry_country;
		private string _cm_region;
		private string _addresszip;
		private string _addresscity;
		private string _addressstate;
		private string _ad_fax;
		private string _ad_phone;
		private string _addressemail;
		private string _contactname;
		private string _contactgender;
		private string _contacttelephone;
		private string _contactemail;
		private string _addressistaxable;
		private string _addressistaxincluded;
		private string _addressistaxincity;
		private string _txztaxzone;
		private string _txcltaxcls;
		private string _ledgerdays;
		private string _cm_slspsn;
		private string _cm_pr_list;
		private string _cm_fix_pr;
		private string _banknumberisactive;
		private string _invcontrolglprofilecode;
		private string _cncontrolglprofilecode;
		private string _prepaycontrolglprofilecode;
		private string _salesaccountglprofilecode;
		private string _reasoncode;
		private string _normalpaymentconditioncode;
		private string _currencycode;
		private string _bankaccformatcode;
		private string _banknumberformatted;
		private string _ownbanknumber;
		private string _debtortypecode;
        private string _ExistsClass;
        private string _Status_id;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int requestid
		{
			set{ _requestid=value;}
			get{return _requestid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName_AD
		{
			set{ _username_ad=value;}
			get{return _username_ad;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dept
		{
			set{ _dept=value;}
			get{return _dept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string managerid
		{
			set{ _managerid=value;}
			get{return _managerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string manager
		{
			set{ _manager=value;}
			get{return _manager;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string manager_AD
		{
			set{ _manager_ad=value;}
			get{return _manager_ad;}
		}
		/// <summary>
		/// 类别：已有客户、新客户
		/// </summary>
		public string Class
		{
			set{ _class=value;}
			get{return _class;}
		}
		/// <summary>
		/// 客户大类代码
		/// </summary>
		public string CmClassID
		{
			set{ _cmclassid=value;}
			get{return _cmclassid;}
		}
		/// <summary>
		/// 客户大类名称
		/// </summary>
		public string cmClassName
		{
			set{ _cmclassname=value;}
			get{return _cmclassname;}
		}
		/// <summary>
		/// 申请工厂
		/// </summary>
		public string cm_domain
		{
			set{ _cm_domain=value;}
			get{return _cm_domain;}
		}
		/// <summary>
		/// 业务关系代码
		/// </summary>
		public string BusinessRelationCode
		{
			set{ _businessrelationcode=value;}
			get{return _businessrelationcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BusinessRelationName1
		{
			set{ _businessrelationname1=value;}
			get{return _businessrelationname1;}
		}
		/// <summary>
		/// 搜索名称
		/// </summary>
		public string AddressSearchName
		{
			set{ _addresssearchname=value;}
			get{return _addresssearchname;}
		}
		/// <summary>
		/// 地址类型
		/// </summary>
		public string AddressTypeCode
		{
			set{ _addresstypecode=value;}
			get{return _addresstypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddressStreet1
		{
			set{ _addressstreet1=value;}
			get{return _addressstreet1;}
		}
		/// <summary>
		/// 语言代码
		/// </summary>
		public string cm_lang
		{
			set{ _cm_lang=value;}
			get{return _cm_lang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ctry_country
		{
			set{ _ctry_country=value;}
			get{return _ctry_country;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cm_region
		{
			set{ _cm_region=value;}
			get{return _cm_region;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddressZip
		{
			set{ _addresszip=value;}
			get{return _addresszip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddressCity
		{
			set{ _addresscity=value;}
			get{return _addresscity;}
		}
		/// <summary>
		/// 省份/州
		/// </summary>
		public string AddressState
		{
			set{ _addressstate=value;}
			get{return _addressstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ad_fax
		{
			set{ _ad_fax=value;}
			get{return _ad_fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ad_phone
		{
			set{ _ad_phone=value;}
			get{return _ad_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddressEMail
		{
			set{ _addressemail=value;}
			get{return _addressemail;}
		}
		/// <summary>
		/// ContactName
		/// </summary>
		public string ContactName
		{
			set{ _contactname=value;}
			get{return _contactname;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string ContactGender
		{
			set{ _contactgender=value;}
			get{return _contactgender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactTelephone
		{
			set{ _contacttelephone=value;}
			get{return _contacttelephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactEmail
		{
			set{ _contactemail=value;}
			get{return _contactemail;}
		}
		/// <summary>
		/// 纳税地址？
		/// </summary>
		public string AddressIsTaxable
		{
			set{ _addressistaxable=value;}
			get{return _addressistaxable;}
		}
		/// <summary>
		/// 是否含税？
		/// </summary>
		public string AddressIsTaxIncluded
		{
			set{ _addressistaxincluded=value;}
			get{return _addressistaxincluded;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddressIsTaxInCity
		{
			set{ _addressistaxincity=value;}
			get{return _addressistaxincity;}
		}
		/// <summary>
		/// 税区
		/// </summary>
		public string TxzTaxZone
		{
			set{ _txztaxzone=value;}
			get{return _txztaxzone;}
		}
		/// <summary>
		/// 税种
		/// </summary>
		public string TxclTaxCls
		{
			set{ _txcltaxcls=value;}
			get{return _txcltaxcls;}
		}
		/// <summary>
		/// 账期
		/// </summary>
		public string LedgerDays
		{
			set{ _ledgerdays=value;}
			get{return _ledgerdays;}
		}
		/// <summary>
		/// 推销员
		/// </summary>
		public string cm_slspsn
		{
			set{ _cm_slspsn=value;}
			get{return _cm_slspsn;}
		}
		/// <summary>
		/// 折扣表
		/// </summary>
		public string cm_pr_list
		{
			set{ _cm_pr_list=value;}
			get{return _cm_pr_list;}
		}
		/// <summary>
		/// 是否固定价格？
		/// </summary>
		public string cm_fix_pr
		{
			set{ _cm_fix_pr=value;}
			get{return _cm_fix_pr;}
		}
		/// <summary>
		/// 是否启用？
		/// </summary>
		public string BankNumberIsActive
		{
			set{ _banknumberisactive=value;}
			get{return _banknumberisactive;}
		}
		/// <summary>
		/// 控制总帐配置文件(发票)
		/// </summary>
		public string InvControlGLProfileCode
		{
			set{ _invcontrolglprofilecode=value;}
			get{return _invcontrolglprofilecode;}
		}
		/// <summary>
		/// 控制总账配置文件(信用票据)
		/// </summary>
		public string CnControlGLProfileCode
		{
			set{ _cncontrolglprofilecode=value;}
			get{return _cncontrolglprofilecode;}
		}
		/// <summary>
		/// 控制总账配置文件(预付款)
		/// </summary>
		public string PrepayControlGLProfileCode
		{
			set{ _prepaycontrolglprofilecode=value;}
			get{return _prepaycontrolglprofilecode;}
		}
		/// <summary>
		/// 销售帐户总账配置文件
		/// </summary>
		public string SalesAccountGLProfileCode
		{
			set{ _salesaccountglprofilecode=value;}
			get{return _salesaccountglprofilecode;}
		}
		/// <summary>
		/// 发票状态
		/// </summary>
		public string ReasonCode
		{
			set{ _reasoncode=value;}
			get{return _reasoncode;}
		}
		/// <summary>
		/// 信贷期限
		/// </summary>
		public string NormalPaymentConditionCode
		{
			set{ _normalpaymentconditioncode=value;}
			get{return _normalpaymentconditioncode;}
		}
		/// <summary>
		/// 货币代码
		/// </summary>
		public string CurrencyCode
		{
			set{ _currencycode=value;}
			get{return _currencycode;}
		}
		/// <summary>
		/// 银行格式
		/// </summary>
		public string BankAccFormatCode
		{
			set{ _bankaccformatcode=value;}
			get{return _bankaccformatcode;}
		}
		/// <summary>
		/// 客户银行帐号
		/// </summary>
		public string BankNumberFormatted
		{
			set{ _banknumberformatted=value;}
			get{return _banknumberformatted;}
		}
		/// <summary>
		/// 自有银行账号
		/// </summary>
		public string OwnBankNumber
		{
			set{ _ownbanknumber=value;}
			get{return _ownbanknumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DebtorTypeCode
		{
			set{ _debtortypecode=value;}
			get{return _debtortypecode;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string ExistsClass
        {
            set { _ExistsClass = value; }
            get { return _ExistsClass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status_id
        {
            set { _Status_id = value; }
            get { return _Status_id; }
        }
        #endregion Model

    }
}

