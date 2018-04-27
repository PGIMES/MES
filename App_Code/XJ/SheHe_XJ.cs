using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
///SheHe_XJ 的摘要说明
/// </summary>
public class SheHe_XJ
{
	public SheHe_XJ()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

      SQLHelper SQLHelper = new SQLHelper();


      public DataTable GetXJList(int flag, string emp_no, string emp_name,string emp_banbie,string emp_banzhu,string equip_no,string equip_name,string ljh,string moju_no,string jclb,string date,string timer,string jc_value,int id)
      {

          SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@moju_no",moju_no),
           new SqlParameter("@jclb",jclb),
           new SqlParameter("@date",date),
           new SqlParameter("@timer",timer),
           new SqlParameter("@jc_value",jc_value),
            new SqlParameter("@id",id),
       };
          DataTable dt = new DataTable();
          return SQLHelper.GetDataTable("MES_YZ_XJ", param);
      }


}