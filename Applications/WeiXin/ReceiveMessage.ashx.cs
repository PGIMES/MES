using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace WebForm.Applications.WeiXin
{
    /// <summary>
    /// ReceiveMessage 的摘要说明
    /// </summary>
    public class ReceiveMessage : IHttpHandler
    {
        string token = "iTak7rMl7ItStvRFbDqA6wDdTeDOOoqX";
        string encodingAESKey = "J6B1ZF1bAx77hVYHhd6aNs6Yyha2BsNxtZq1dprOX2v";
        string corpId = RoadFlow.Platform.WeiXin.Config.CorpID;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "GET")
            {
                Auth();
            }
            else if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                string signature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
                string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
                string nonce = HttpContext.Current.Request.QueryString["nonce"];
                RoadFlow.Platform.WeiXin.WXBizMsgCrypt wxcpt = new RoadFlow.Platform.WeiXin.WXBizMsgCrypt(token, encodingAESKey, corpId);
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string msgBody = Encoding.UTF8.GetString(b);
                string sMsg = "";
                int flag = wxcpt.DecryptMsg(signature, timestamp, nonce, msgBody, ref sMsg);
                if (flag == 0)
                {
                    new RoadFlow.Platform.WeiXin.Message().Receive(sMsg);
                }
                else
                {
                    RoadFlow.Platform.Log.Add("消息解密失败", msgBody, RoadFlow.Platform.Log.Types.微信企业号);
                }
            }
        }

        /// <summary>
        /// 验证相应服务器的数据
        /// </summary>
        private void Auth()
        {
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            string decryptEchoString = "";
            if (CheckSignature(token, signature, timestamp, nonce, corpId, encodingAESKey, echoString, ref decryptEchoString))
            {
                if (!string.IsNullOrEmpty(decryptEchoString))
                {
                    HttpContext.Current.Response.Write(decryptEchoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 验证企业号签名
        /// </summary>
        /// <param name="token">企业号配置的Token</param>
        /// <param name="signature">签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">nonce参数</param>
        /// <param name="corpId">企业号ID标识</param>
        /// <param name="encodingAESKey">加密键</param>
        /// <param name="echostr">内容字符串</param>
        /// <param name="retEchostr">返回的字符串</param>
        /// <returns></returns>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce, string corpId, string encodingAESKey, string echostr, ref string retEchostr)
        {
            RoadFlow.Platform.WeiXin.WXBizMsgCrypt wxcpt = new RoadFlow.Platform.WeiXin.WXBizMsgCrypt(token, encodingAESKey, corpId);
            int result = wxcpt.VerifyURL(signature, timestamp, nonce, echostr, ref retEchostr);
            if (result != 0)
            {
                return false;
            }
            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}