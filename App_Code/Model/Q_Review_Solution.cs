/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Solution.cs
*
* 功 能： N/A
* 类 名： Q_Review_Solution
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 10:31:16   N/A    初版
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
	/// Q_Review_Solution:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Q_Review_Solution
	{
		public Q_Review_Solution()
		{}
		#region Model
		private int _requestid;
		private int _slnid;
        private int _dutyid;
        private string _actionplan;
		private DateTime? _plandate;
		private string _actionemp;
		private string _actionfile;
		private DateTime? _slndate;
		private string _slnemp;
		private string _disagreestate;
		private string _disagreedesc;
		private DateTime? _resultdate;
		private string _resultemp;
		private string _confirmstatus;
		private DateTime? _confirmdate;
		private string _confirmempid;
		private string _confirmempname;
        private string _confirmdesc;
        private string _slnstate;
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
		public int SlnId
		{
			set{ _slnid=value;}
			get{return _slnid;}
		}
        /// <summary>
		/// 
		/// </summary>
		public int DutyId
        {
            set { _dutyid = value; }
            get { return _dutyid; }
        }
        /// <summary>
        /// 计划采取的行动
        /// </summary>
        public string ActionPlan
		{
			set{ _actionplan=value;}
			get{return _actionplan;}
		}
        /// <summary>
        /// 发生原因
        /// </summary>
        public string Cause { get; set; }
		/// <summary>
		/// 计划完成日期
		/// </summary>
		public DateTime? PlanDate
		{
			set{ _plandate=value;}
			get{return _plandate;}
		}
		/// <summary>
		/// 行动责任人
		/// </summary>
		public string ActionEmp
		{
			set{ _actionemp=value;}
			get{return _actionemp;}
		}
		/// <summary>
		/// 行动计划文件
		/// </summary>
		public string ActionFile
		{
			set{ _actionfile=value;}
			get{return _actionfile;}
		}
		/// <summary>
		/// 措施提交日期
		/// </summary>
		public DateTime? SlnDate
		{
			set{ _slndate=value;}
			get{return _slndate;}
		}
		/// <summary>
		/// 措施提交人
		/// </summary>
		public string SlnEmp
		{
			set{ _slnemp=value;}
			get{return _slnemp;}
		}
		/// <summary>
		/// 否决状态：0：正常，1：否决
		/// </summary>
		public string DisagreeState
		{
			set{ _disagreestate=value;}
			get{return _disagreestate;}
		}
		/// <summary>
		/// 否决原因描述
		/// </summary>
		public string DisagreeDesc
		{
			set{ _disagreedesc=value;}
			get{return _disagreedesc;}
		}
        public string DisagreeEmp { get; set; }
        public DateTime? DisagreeDate { get; set; }
		/// <summary>
		/// 提交改善结果日期
		/// </summary>
		public DateTime? ResultDate
		{
			set{ _resultdate=value;}
			get{return _resultdate;}
		}
		/// <summary>
		/// 改善结果提交人
		/// </summary>
		public string ResultEmp
		{
			set{ _resultemp=value;}
			get{return _resultemp;}
		}
		/// <summary>
		/// 确认状态:1.改善有效2.改善无效，继续改善
		/// </summary>
		public string ConfirmStatus
		{
			set{ _confirmstatus=value;}
			get{return _confirmstatus;}
		}
		/// <summary>
		/// 结果确认日期
		/// </summary>
		public DateTime? ConfirmDate
		{
			set{ _confirmdate=value;}
			get{return _confirmdate;}
		}
		/// <summary>
		/// 结果确认人
		/// </summary>
		public string ConfirmEmpId
		{
			set{ _confirmempid=value;}
			get{return _confirmempid;}
		}
		/// <summary>
		/// 结果确认人
		/// </summary>
		public string ConfirmEmpName
		{
			set{ _confirmempname=value;}
			get{return _confirmempname;}
		}
        /// <summary>
		/// 结果确认意见
		/// </summary>
		public string ConfirmDesc
        {
            set { _confirmdesc= value; }
            get { return _confirmdesc; }
        }
        /// <summary>
        /// 方案状态
        /// </summary>
        public string SlnState
        {
            get; set;
        }

        #endregion Model

    }
}

