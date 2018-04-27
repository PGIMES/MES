/**  版本信息模板在安装目录下，可自行修改。
* form4_Sale_Customer_LOG.cs
*
* 功 能： N/A
* 类 名： form4_Sale_Customer_LOG
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/22 13:57:01   N/A    初版
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
	/// form4_Sale_Customer_LOG:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class form4_Sale_Customer_LOG
	{
		public form4_Sale_Customer_LOG()
		{}
		#region Model
		private int _id;
		private int _requestid;
		private string _status_id;
		private string _status_ms;
		private string _dept;
		private string _update_engineer;
		private string _update_user;
		private string _update_username;
		private DateTime? _receive_time;
        private DateTime? _commit_time;
		private string _update_content;
		private string _update_lb;
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
		/// 
		/// </summary>
		public string status_id
		{
			set{ _status_id=value;}
			get{return _status_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string status_ms
		{
			set{ _status_ms=value;}
			get{return _status_ms;}
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
		public string Update_Engineer
		{
			set{ _update_engineer=value;}
			get{return _update_engineer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Update_user
		{
			set{ _update_user=value;}
			get{return _update_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Update_username
		{
			set{ _update_username=value;}
			get{return _update_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Receive_time
		{
			set{ _receive_time=value;}
			get{return _receive_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Commit_time
		{
			set{ _commit_time=value;}
			get{return _commit_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Update_content
		{
			set{ _update_content=value;}
			get{return _update_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Update_LB
		{
			set{ _update_lb=value;}
			get{return _update_lb;}
		}
		#endregion Model

	}
}

