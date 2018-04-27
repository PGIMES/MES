using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace WebForm.Platform.UserInfo
{
    /// <summary>
    /// SaveUserHead 的摘要说明
    /// </summary>
    public class SaveUserHead : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            RoadFlow.Platform.Users bui = new RoadFlow.Platform.Users();
            var ui = RoadFlow.Platform.Users.CurrentUser;
            if (ui == null)
            {
                context.Response.Write("未找到用户!");
                return;
            }
            string x = context.Request.Form["x"];
            string y = context.Request.Form["y"];
            string x2 = context.Request.Form["x2"];
            string y2 = context.Request.Form["y2"];
            string w = context.Request.Form["w"];
            string h = context.Request.Form["h"];
            string img = (context.Request.Form["img"] ?? "").DesDecrypt();
            if (img.IsNullOrEmpty() || !File.Exists(img))
            {
                context.Response.Write("文件不存在!");
                return;
            }
            try
            {
                string newfile = RoadFlow.Utility.ImgHelper.CutAvatar(img, Common.Tools.BaseUrl + "/Files/UserHeads/" + ui.ID.ToString() + ".jpg", x.ToInt(), y.ToInt(), w.ToInt(), h.ToInt());
                if (!newfile.IsNullOrEmpty())
                {
                    if (ui != null)
                    {
                        ui.HeadImg = newfile;
                        bui.Update(ui);
                    }
                    context.Response.Write("保存成功!");
                }
                else
                {
                    context.Response.Write("保存失败!");
                }
            }
            catch(Exception err)
            {
                context.Response.Write("保存失败!" + err.Message);
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