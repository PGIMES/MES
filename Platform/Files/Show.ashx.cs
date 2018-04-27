using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace WebForm.Platform.Files
{
    /// <summary>
    /// Show 的摘要说明
    /// </summary>
    public class Show : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.VaryByParams["SkipGlobalExpires"] = true;
            context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(10));
            string loginMsg;
            if (!Common.Tools.CheckLogin(out loginMsg) && !RoadFlow.Platform.WeiXin.Organize.CheckLogin())
            {
                context.Response.Write("您未登录,不能查看文件!");
                context.Response.End();
                return;
            }

            string file = context.Request.QueryString["id"].DesDecrypt();
            FileInfo tmpFile = new FileInfo(file);

            if (!tmpFile.Exists)
            {
                context.Response.Write("未找到要查看的文件!");
                return;
            }

            if (!("," + RoadFlow.Utility.Config.UploadFileType + ",").Contains("," + tmpFile.Extension.Replace(".", "") + ",", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write("该文件类型不允许查看!");
                return;
            }

            string fileName = tmpFile.Name;
            if (context.Request != null && context.Request.Browser != null && (context.Request.Browser.Type.StartsWith("IE", StringComparison.CurrentCultureIgnoreCase)
                || context.Request.Browser.Type.StartsWith("InternetExplorer", StringComparison.CurrentCultureIgnoreCase)))
            {
                fileName = fileName.UrlEncode();
            }
            
            context.Response.AppendHeader("Server-FileName", fileName);
            
            string tmpContentType = ",.zip,.rar,.7z,".Contains(","+tmpFile.Extension+",", StringComparison.CurrentCultureIgnoreCase) ? "" :  new RoadFlow.Platform.Files().GetFileContentType(tmpFile.Extension);
            if (string.IsNullOrEmpty(tmpContentType))
            {
                context.Response.ContentType = "application/octet-stream";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            }
            else
            {
                context.Response.ContentType = tmpContentType;
                context.Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
            }

            context.Response.AddHeader("Content-Length", tmpFile.Length.ToString());

            using (var tmpRead = tmpFile.OpenRead())
            {
                var tmpByte = new byte[2048];
                var i = tmpRead.Read(tmpByte, 0, tmpByte.Length);
                while (i > 0)
                {
                    if (context.Response.IsClientConnected)
                    {
                        context.Response.OutputStream.Write(tmpByte, 0, i);
                        context.Response.Flush();
                    }
                    else
                    {
                        break;
                    }
                    i = tmpRead.Read(tmpByte, 0, tmpByte.Length);
                }
            }
            context.Response.Flush();
            context.Response.OutputStream.Close();
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