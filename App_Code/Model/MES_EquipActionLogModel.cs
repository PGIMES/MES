using System;
using System.Collections.Generic;
namespace MES.Model
{
	/// <summary>
	/// MES_Equipment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MES_EquipActionLogModel
	{
        public MES_EquipActionLogModel()
		{
            createtime = System.DateTime.Now;
        }
		
		public int id{set;get;}		
        public string equip_no  {set;get; }
        public string logaction {   set; get;}
        public DateTime createtime { get; set; }
        public string actionmark   { set;  get; }
        public string actionreason { set; get; } 	
		
	}
    
}

