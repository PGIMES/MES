using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Pgi.Auto
{
    public class Public
    {

        public static string GetNo(string lstype, string lsNo,int isshowdate=1,int lnrandom=0)
        {
            string lsreturnNO = lsNo;
            SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionMES"]);
            string sql = "select * from  AllId  where type='" + lstype + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;
            DataSet lds = new DataSet();
            ad.Fill(lds);
            int ln =Convert.ToInt32(lds.Tables[0].Rows[0]["allid"].ToString());
            string lupdate_date = System.DateTime.Now.ToShortDateString();
            if (lsNo!="" && isshowdate==1)
            {
                //按天
                if (lds.Tables[0].Rows[0]["update_date"].ToString() == "")
                {
                    ln = 1;
                }
                else if (Convert.ToDateTime(lds.Tables[0].Rows[0]["update_date"].ToString()).ToShortDateString() != lupdate_date)
                {
                    ln = 1;
                }
                else
                {
                    ln = Convert.ToInt32(lds.Tables[0].Rows[0]["allid"]);

                }
                lsreturnNO += DateTime.Now.ToString("yyyyMMdd");
                string ls0 = "000";
                if (lnrandom>0)
                {
                    ls0 = "";
                    for (int i = 0; i < lnrandom; i++)
                    {
                        ls0 += "0";
                    }
                }
                lsreturnNO +=  ln.ToString(ls0);
            }
            else if (lsNo != "" && isshowdate == 0)
            {
                //按月
                lupdate_date = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("00") + "-01";
                if (lds.Tables[0].Rows[0]["update_date"].ToString() == "")
                {
                    ln = 1;
                }
                else if (Convert.ToDateTime(lds.Tables[0].Rows[0]["update_date"].ToString()).ToString("yyyy-MM-dd") != lupdate_date)
                {
                    ln = 1;
                }
                else
                {
                    ln = Convert.ToInt32(lds.Tables[0].Rows[0]["allid"]);

                }
                string ls0 = "000";
                if (lnrandom > 0)
                {
                    ls0 = "";
                    for (int i = 0; i < lnrandom; i++)
                    {
                        ls0 += "0";
                    }
                }
                lsreturnNO += ln.ToString(ls0);
            }
            else
            {
                lsreturnNO = ln.ToString();
            }
            int ln1 = ln + 1;
            string sql1 = "update allid set allid=" + ln1 + ",update_date='"+ lupdate_date + "' where type='" + lstype + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            cmd1.ExecuteNonQuery();
            conn.Close();

            return lsreturnNO;
        }

        public static void MsgBox(System.Web.UI.Page p,string lsmsg_type,string lsmsg,int b_reload=0)
        {
            string lsstr = "layer."+lsmsg_type+"('"+lsmsg+"'";
            if (b_reload==1)
            {
                lsstr += ",function(index) {layer.close(index);location.reload();})";
            }
            else
            {
                lsstr += ")";
            }
            if (lsmsg_type == "msg")
            {
                lsmsg_type = "message";
            }
           
            //string lsstr = "layer.alert('"+lsmsg+ "',function(index) {layer.close(index);location.reload();})";
            //if (lsmsg_type== "message")
            //{
            //    lsstr = "layer.msg('" + lsmsg + "')";
            //}
          
            ScriptManager.RegisterStartupScript(p, p.GetType(), lsmsg_type,lsstr, true);
            return;

        }
    }
}
