using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Info.ShortMessage
{
    public partial class Show : Common.BasePage
    {
        protected RoadFlow.Data.Model.ShortMessage message = null;
        protected RoadFlow.Platform.ShortMessage MSG = new RoadFlow.Platform.ShortMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg;
            if (!Common.Tools.CheckLogin(out msg) && !RoadFlow.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.Write("未登录用户不能查看!");
                Response.End();
            }
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                message = MSG.Get(id.ToGuid());
            }
            if (message != null)
            {
                MSG.UpdateStatus(message.ID);
            }
            else
            {
                message = MSG.GetRead(id.ToGuid());
            }
            
            if (message == null)
            {
                Response.Write("未找到该消息!");
                Response.End();
            }
            
            
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;// base.CheckUrl(isEnd);
        }
    }
}