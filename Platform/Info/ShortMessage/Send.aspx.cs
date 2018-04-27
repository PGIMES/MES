using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.SignalR;

namespace WebForm.Platform.Info.ShortMessage
{
    public partial class Send : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled("正在发送");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Title1 = this.Title1.Value;
            string Contents = this.Contents.Value;
            string ReceiveUserID = Request.Form["ReceiveUserID"];
            string Files = Request.Form["Files"];
            string sendtoseixin = Request.Form["sendtoseixin"];

            if (Title1.IsNullOrEmpty() || Contents.IsNullOrEmpty() || ReceiveUserID.IsNullOrEmpty())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('数据验证错误!')", true);
                return;
            }
            var users = new RoadFlow.Platform.Organize().GetAllUsers(ReceiveUserID);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder userAccounts = new System.Text.StringBuilder();
            RoadFlow.Data.Model.ShortMessage sm1 = null;
            RoadFlow.Platform.ShortMessage SM = new RoadFlow.Platform.ShortMessage();
            foreach (var user in users)
            {
                RoadFlow.Data.Model.ShortMessage sm = new RoadFlow.Data.Model.ShortMessage();
                sm.Contents = Contents;
                sm.ID = Guid.NewGuid();
                sm.ReceiveUserID = user.ID;
                sm.ReceiveUserName = user.Name;
                sm.SendTime = RoadFlow.Utility.DateTimeNew.Now;
                sm.SendUserID = CurrentUserID;
                sm.SendUserName = CurrentUserName;
                sm.Status = 0;
                sm.Title = Title1;
                sm.Type = 0;
                sm.Files = Files;
                SM.Add(sm);
                RoadFlow.Platform.ShortMessage.SiganalR(user.ID, sm.ID.ToString(), Title1, Contents.RemoveHTML());
                sb.Append(user.Name);
                sb.Append(",");
                userAccounts.Append(user.Account);
                userAccounts.Append('|');
                if(sm1 == null)
                {
                    sm1 = sm;
                }
            }

            if ("1" == sendtoseixin && sm1 != null && userAccounts.Length > 0)//发送到微信
            {
                SM.SendToWeiXin(sm1, userAccounts.ToString().TrimEnd('|'));
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", string.Format("alert('成功将消息发送给了：{0}!');window.location=window.location;", sb.ToString().TrimEnd(',')), true);
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}