using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class Default : WebForm.Common.BasePage
    {
        protected int RoleLength;
        protected string DefaultRoleID;
        protected string HeadImg = "Files/UserHeads/default.jpg";
        protected string NoReadMsgJson = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginMsg = string.Empty;
            if (!Common.Tools.CheckLogin(out loginMsg))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            this.UserName.Text = CurrentUserName;
            this.CurrentTime.Text = RoadFlow.Utility.DateTimeNew.Now.ToDateWeekString();
            
            //得到未读消息
            var noReadMsg = new RoadFlow.Platform.ShortMessage().GetAllNoReadByUserID(CurrentUserID);
            if (noReadMsg.Count > 0)
            {
                LitJson.JsonData jd = new LitJson.JsonData();
                string msgContents = string.Empty;
                var noReadMsg1 = noReadMsg.OrderByDescending(p => p.SendTime).FirstOrDefault();
                if (!noReadMsg1.LinkUrl.IsNullOrEmpty())
                {
                    msgContents = "<a class=\"blue1\" href=\"" + noReadMsg1.LinkUrl + "\">" + noReadMsg1.Contents.RemoveHTML() + "</a>";
                }
                else
                {
                    msgContents = noReadMsg1.Contents.RemoveHTML();
                }
                jd["title"] = noReadMsg1.Title;
                jd["contents"] = msgContents;
                jd["count"] = noReadMsg.Count;
                NoReadMsgJson = jd.ToJson();
            }

            //得到头像
            var user = CurrentUser;
            if (!user.HeadImg.IsNullOrEmpty() && System.IO.File.Exists(Server.MapPath(user.HeadImg)))
            {
                
                HeadImg = user.HeadImg;
            }
            this.UserHeadImg.Src = HeadImg;
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}