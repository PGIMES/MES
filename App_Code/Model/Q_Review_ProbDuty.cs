/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_ProbDuty.cs
*
* 功 能： N/A
* 类 名： Q_Review_ProbDuty
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
	/// Q_Review_ProbDuty:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Q_Review_ProbDuty
	{
		public Q_Review_ProbDuty()
		{}
		#region Model
		private int _requestid;
		private int _id;
		private string _improvetarget;
		private string _dutydept;
		private string _dutyemp;
         
        private DateTime? _reqfinishdate;
		/// <summary>
		/// 
		/// </summary>
		public int RequestId
		{
			set{ _requestid=value;}
			get{return _requestid;}
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
		public string ImproveTarget
		{
			set{ _improvetarget=value;}
			get{return _improvetarget;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DutyDept
		{
			set{ _dutydept=value;}
			get{return _dutydept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DutyEmp
		{
			set{ _dutyemp=value;}
			get{return _dutyemp;}
		}
        public string DutyEmpName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReqFinishDate
		{
			set{ _reqfinishdate=value;}
			get{return _reqfinishdate;}
		}

        public int Mark{ get; set; }

		#endregion Model

	}
}

