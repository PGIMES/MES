/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Remarks.cs
*
* 功 能： N/A
* 类 名： Q_Review_Remarks
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
namespace MES.Model
{
	/// <summary>
	/// Q_Review_Remarks:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Q_Review_Remarks
	{
		public Q_Review_Remarks()
		{}
		#region Model
		private int _slnid;
		private int _id;
		private string _remarks;
		private string _file_path;
		private string _file_name;
		private string _assessments;
		private string _create_by_empid;
		private string _create_by_empname;
		private DateTime? _create_date;
		/// <summary>
		/// 
		/// </summary>
		public int SlnId
		{
			set{ _slnid=value;}
			get{return _slnid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string File_Path
		{
			set{ _file_path=value;}
			get{return _file_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string File_Name
		{
			set{ _file_name=value;}
			get{return _file_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Assessments
		{
			set{ _assessments=value;}
			get{return _assessments;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Create_by_EmpId
		{
			set{ _create_by_empid=value;}
			get{return _create_by_empid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Create_by_EmpName
		{
			set{ _create_by_empname=value;}
			get{return _create_by_empname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Create_Date
		{
			set{ _create_date=value;}
			get{return _create_date;}
		}
		#endregion Model

	}
}

