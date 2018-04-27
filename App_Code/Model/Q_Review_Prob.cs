/**  版本信息模板在安装目录下，可自行修改。
* Q_Review_Prob.cs
*
* 功 能： N/A
* 类 名： Q_Review_Prob
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 13:48:24   N/A    初版
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
    /// Q_Review_Prob; Q_Review_Log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
	public partial class Q_Review_Prob
	{
		public Q_Review_Prob()
		{}
		#region Model
		private int _requestid;
		private string _dh;
		private string _empid;
		private string _empname;
		private string _dept;
		private DateTime? _probdate;
		private string _probemp;
		private string _probstatus;
		private string _probfrom;
		private string _domain;
		private string _custclass;
		private string _prodproject;
		private string _ljh;
		private string _ljname;
		private string _proddesc;
		private string _probfile;
		private DateTime? _reqslndate;
		private DateTime? _reqclosedate;
		private DateTime? _actualclosedate;
        private string _actualstate;
        private DateTime? _createdate;
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
		public string DH
		{
			set{ _dh=value;}
			get{return _dh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpId
		{
			set{ _empid=value;}
			get{return _empid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmpName
		{
			set{ _empname=value;}
			get{return _empname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dept
		{
			set{ _dept=value;}
			get{return _dept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProbDate
		{
			set{ _probdate=value;}
			get{return _probdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProbEmp
		{
			set{ _probemp=value;}
			get{return _probemp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProbStatus
		{
			set{ _probstatus=value;}
			get{return _probstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProbFrom
		{
			set{ _probfrom=value;}
			get{return _probfrom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustClass
		{
			set{ _custclass=value;}
			get{return _custclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProdProject
		{
			set{ _prodproject=value;}
			get{return _prodproject;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LJH
		{
			set{ _ljh=value;}
			get{return _ljh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LJName
		{
			set{ _ljname=value;}
			get{return _ljname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProdDesc
		{
			set{ _proddesc=value;}
			get{return _proddesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProbFile
		{
			set{ _probfile=value;}
			get{return _probfile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReqSlnDate
		{
			set{ _reqslndate=value;}
			get{return _reqslndate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReqCloseDate
		{
			set{ _reqclosedate=value;}
			get{return _reqclosedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ActualCloseDate
		{
			set{ _actualclosedate=value;}
			get{return _actualclosedate;}
		}
        /// <summary>
		/// 问题是否解决：已解决、未解决
		/// </summary>
		public string ActualState
        {
            set { _actualstate = value; }
            get { return _actualstate; }
        }

        public DateTime? CreateDate
        {
            set;
            get;
        }
        /// <summary>
        /// 级别 5，4，3，2，1
        /// </summary>
        public string Rank { set; get; }
        public string RankDesc { set; get; }
        #endregion Model

    }
    public partial class Q_Review_Log
    {
        public Q_Review_Log()
        {
            Commit_time = DateTime.Now;
        }
        #region Model
        
        public int ID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int RequestId
        {
            set;
            get;
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string Update_Engineer
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Update_user
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Update_username
        {
            set;
            get;
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string Update_LB
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Commit_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Update_content
        {
            set;
            get;
        }
        
        #endregion Model

    }
}

