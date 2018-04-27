using System;
using System.Collections.Generic;
namespace MES.Model
{
	/// <summary>
	/// MES_Equipment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MES_SB_WX
	{
        public MES_SB_WX()
		{            
            //List<MES_HeJin> list=new List<MES_HeJin>();
            //list.Add(new MES_HeJin{ id="A380", name="A380" });
            //list.Add(new MES_HeJin { id = "EN46000", name = "EN46000" });
            //list.Add(new MES_HeJin { id = "ADC12", name = "ADC12" });
            //list.Add(new MES_HeJin { id = "EN47100", name = "EN47100" });
            //return list;
        }
		
		public int id{set;get;}		
        public string wx_dh  {set;get; }
        public string wx_gonghao {   set; get;}
        public string wx_name   { set;  get; }
        public string wx_banzhu   { set; get; }
        public string wx_cs   { set; get;  }
        public string wx_banbie  { set; get;  }
        public string wx_result{  set;   get;   }
        public string mo_down_cs  { set; get;   }
        public DateTime wx_begin_date {   set;   get;  }
        public DateTime wx_end_date { get; set; }	
		

        ///viewModel params
        public string p_status { get; set; }

	}
    [Serializable]
    public partial class MES_SB_QR
    {
        public MES_SB_QR()
        {             
        }

        public int id { set; get; }
        public string dh { set; get; }
        public string qr_gh { set; get; }
        public string qr_name { set; get; }
        public string qr_banzhu { set; get; }
        public string qr_banbie { set; get; }
        public string qr_remark { set; get; }      
        public DateTime qr_date { set; get; }
        

        ///viewModel params
        public string p_status { get; set; }

    }
}

