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
namespace MES.Model
{
	/// <summary>
	/// form4_Customer_DebtorShipTo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class form4_Customer_DebtorShipTo
	{
		public form4_Customer_DebtorShipTo()
		{}
		#region Model
		private int _id;
		private int _requestid;
		private string _businessrelationcode;
		private int? _shiptonum;
		private string _debtorshiptocode;
		private string _debtorshiptoname;
		private string _cm_addr;
		private string _addresstypecode;
		private string _addressstreet1;
		private string _addresszip;
		private string _addresscity;
		private string _cm_region;
		private string _ctry_country;
		private string _debtor_domain;
		private string _iseffective;
		private DateTime? _updatedate;
		private string _updatebyid;
		private string _updatebyname;
		private string _lngcode;
        private string _turns;
        /// <summary>
        /// 
        /// </summary>
        public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 客户代码
		/// </summary>
		public string BusinessRelationCode
		{
			set{ _businessrelationcode=value;}
			get{return _businessrelationcode;}
		}
		/// <summary>
		/// 发货终点流水码
		/// </summary>
		public int? ShipToNum
		{
			set{ _shiptonum=value;}
			get{return _shiptonum;}
		}
		/// <summary>
		/// 发货终点代码
		/// </summary>
		public string DebtorShipToCode
		{
			set{ _debtorshiptocode=value;}
			get{return _debtorshiptocode;}
		}
		/// <summary>
		/// 发货终点名称
		/// </summary>
		public string DebtorShipToName
		{
			set{ _debtorshiptoname=value;}
			get{return _debtorshiptoname;}
		}
		/// <summary>
		/// PO工厂代码
		/// </summary>
		public string cm_addr
		{
			set{ _cm_addr=value;}
			get{return _cm_addr;}
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
		/// 地址类型
		/// </summary>
		public string AddressStreet1
		{
			set{ _addressstreet1=value;}
			get{return _addressstreet1;}
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
		public string ctry_country
		{
			set{ _ctry_country=value;}
			get{return _ctry_country;}
		}
		/// <summary>
		/// 域
		/// </summary>
		public string Debtor_Domain
		{
			set{ _debtor_domain=value;}
			get{return _debtor_domain;}
		}
		/// <summary>
		/// 是否有效
		/// </summary>
		public string IsEffective
		{
			set{ _iseffective=value;}
			get{return _iseffective;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateById
		{
			set{ _updatebyid=value;}
			get{return _updatebyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateByName
		{
			set{ _updatebyname=value;}
			get{return _updatebyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LngCode
		{
			set{ _lngcode=value;}
			get{return _lngcode;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string turns
        {
            set { _turns = value; }
            get { return _turns; }
        }
        #endregion Model

    }
}

