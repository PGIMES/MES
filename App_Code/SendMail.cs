using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Reflection;
using System.Web;
using System.Net.Mail;
using System.Data.SqlClient;
public class Mail
{
    SQLHelper SQLHelper = new SQLHelper();
    public static void SendMail(string lsmail, string lstitle, string LINKCode, string LINKName, string work, string job, string username, string Receive_time, string code, string name,string body)
    {
        if (lsmail != "")
        {
            string body1 = "Dear all:";
            //string body2 = " �ͻ�����ϵͳ��δ��������뾡�촦��!";
            string body2 = body;
            string body3 = "<table border=1>" +
   "<tr >" +
       "<td>" + code + "</td>" +
       "<td>" + name + "</td>" +
         "<td>��������</td>" +
          "<td>ǩ�˽�ɫ</td>" +
           "<td>��ǩ��������</td>" +
            "<td>����ʱ��</td>" +
   "</tr>" +
   "<tr>" +
       "<td>" + LINKCode + "</td>" +
       "<td>" + LINKName + "</td>" +
         "<td>" + work + "</td>" +
          "<td>" + job + "</td>" +
           "<td>" + username + "</td>" +
            "<td>" + Receive_time + "</td>" +
   "</tr>" +
"</table>";
            string Body = body1 + "<br>" + body2 + "<br>" + body3;
            SmtpClient mail = new SmtpClient();
            //���ͷ�ʽ
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp������
            mail.Host = "mail.pgi.cn";
            //mail.Host = "222.73.248.86";

            //�û���ƾ֤               
            mail.Credentials = new System.Net.NetworkCredential("oa@pgi.cn", "pgi_1234");

            //�ʼ���Ϣ
            MailMessage message = new MailMessage();
            //������
            message.From = new MailAddress("oa@pgi.cn");
            //�ռ���


            if (lsmail != "")
            {
                string[] lsmails = lsmail.Trim().Split(';');
                for (int i = 0; i < lsmails.Length; i++)
                {
                    if (lsmails[i].ToString().Trim() != "")
                    {
                        message.To.Add(new MailAddress(lsmails[i].ToString().Trim().Replace("\r", "").Replace("\n", "")));
                    }

                }

            }


            //����
            message.Subject = lstitle;
            //����
            message.Body = Body;
            //���ı���
            message.BodyEncoding = System.Text.Encoding.UTF8;
            //����ΪHTML��ʽ
            message.IsBodyHtml = true;
            //���ȼ�
            message.Priority = MailPriority.High;

            ////�����ռ���
            //message.CC.Add(Address);


            try
            {
                mail.Send(message);
                //Response.Write("���ͳɹ�");
            }
            catch (Exception ex)
            {

            }
        }


    }

    public int insert_A_MES_Send_mail_log(int requestId, string code, string send_empid, string send_name,string send_date, string work, string job,string type)
    {
        SqlParameter[] param = new SqlParameter[]
       { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@code", code) ,
                        new SqlParameter("@send_empid", send_empid),
                        new SqlParameter("@send_name", send_name),
                        new SqlParameter("@send_date", send_date),
                        new SqlParameter("@work", work),
                        new SqlParameter("@job", job),
                        new SqlParameter("@type", type)
                         };
        return SQLHelper.ExecuteNonQuery("A_MES_Send_mail_log_insert", param);
    }

}


