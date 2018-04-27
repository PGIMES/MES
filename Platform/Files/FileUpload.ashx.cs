using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.Files
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUpload : IHttpHandler, IRequiresSessionState
    {
        private RoadFlow.Platform.Files bFiles = new RoadFlow.Platform.Files();
        private string uploadDir = string.Empty;
        private string allowFiles = string.Empty; //允许上传的文件扩展名
        private string userID = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            uploadDir = context.Request.Form["dir"].DesDecrypt();
            userID = context.Request.Form["userid"];
            if (!userID.IsGuid())
            {
                context.Response.End();
                return;
            }
            if (uploadDir.IsNullOrEmpty())
            {
                context.Response.End();
                return;
            }
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
                return;
            }
            allowFiles = RoadFlow.Utility.Config.UploadFileType;
            SaveFile();  
        }

        private void SaveFile()
        {
            string basePath = uploadDir.EndsWith("\\") ? uploadDir : uploadDir + "\\";
            string name;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            var suffix = files[0].ContentType.Split('/');
            //获取文件格式  
            //var _suffix = suffix[1].Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];  
            var _suffix = suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];
            //如果不修改文件名，则创建随机文件名  
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            //文件保存  
            var full = basePath + name;
            //如果文件存在则要重命名
            if (System.IO.File.Exists(full))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(full);
                string fileExtName = System.IO.Path.GetExtension(full);
                full = basePath + fileName + "(" + RoadFlow.Utility.Tools.GetRandomString() + ")" + fileExtName;
            }
            string extName = System.IO.Path.GetExtension(full).Replace(".","");
            if (!("," + allowFiles + ",").Contains("," + extName + ",", StringComparison.CurrentCultureIgnoreCase))
            {
                System.Web.HttpContext.Current.Response.Write("{\"jsonrpc\":\"2.0\",\"error\":\"不允许的文件\",\"message\":\"\",\"id\":\"" + name + "\"}");
                System.Web.HttpContext.Current.Response.End();
                return;
            }
            files[0].SaveAs(full);
            string id1 = full.Replace1(new RoadFlow.Platform.Files().GetRootPath(), "").DesEncrypt();
            decimal size = decimal.Round((files[0].ContentLength / 1024), 0);
            string fileSize = size == 0 ? files[0].ContentLength > 0 ? "1" : "0" : size.ToFormatString();
            System.Web.HttpContext.Current.Response.Write("{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + full.DesEncrypt() + "\", \"id1\":\"" + id1 + "\",\"size\":\"" + fileSize + "KB\"}");
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