using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebForm.Tests
{
    /// <summary>
    /// savedoc 的摘要说明
    /// </summary>
    public class savedoc : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["FileData"];
            string oldFilePath = context.Request.Form["FilePath"];
            Uri uri = new Uri(oldFilePath);
            string newFilePath = context.Server.MapPath(uri.AbsolutePath);
            try
            {
                file.SaveAs(newFilePath);
                context.Response.Write("保存成功!");
            }
            catch (Exception err)
            {
                context.Response.Write(err.Message);
            }
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