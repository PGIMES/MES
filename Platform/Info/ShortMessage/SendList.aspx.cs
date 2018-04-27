using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Info.ShortMessage
{
    public partial class SendList : Common.BasePage
    {
        string s_Title1 = string.Empty;
        string s_Contents = string.Empty;
        string s_SenderID = string.Empty;
        string s_Date1 = string.Empty;
        string s_Date2 = string.Empty;
        protected List<RoadFlow.Data.Model.ShortMessage> MsgList = new List<RoadFlow.Data.Model.ShortMessage>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Title1 = Request.QueryString["s_Title1"];
                s_Contents = Request.QueryString["s_Contents"];
                s_SenderID = Request.QueryString["s_SenderID"];
                s_Date1 = Request.QueryString["s_Date1"];
                s_Date2 = Request.QueryString["s_Date2"];
                initData();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_Title1 = Request.Form["Title1"];
            s_Contents = Request.Form["Contents"];
            s_SenderID = Request.Form["SenderID"];
            s_Date1 = Request.Form["Date1"];
            s_Date2 = Request.Form["Date2"];
            initData();
        }

        private void initData()
        {
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Title1=" + s_Title1.UrlEncode() +
                    "&s_Contents=" + s_Contents.UrlEncode() + "&s_SenderID=" + s_SenderID + "&s_Date1=" + s_Date1 + "&s_Date2=" + s_Date2;
            string query1 = query + "&pagenumber=" + RoadFlow.Utility.Tools.GetPageNumber() + "&pagesize=" + RoadFlow.Utility.Tools.GetPageSize();
            string pager;
            s_SenderID = "u_" + CurrentUserID.ToString();
            MsgList = new RoadFlow.Platform.ShortMessage().GetList(out pager, new int[] { 0,1 }, query, s_Title1, s_Contents, s_SenderID, s_Date1, s_Date2);
            this.PagerText.Text = pager;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string checkbox_apps = Request.Form["checkbox_app"] ?? "";
            RoadFlow.Platform.ShortMessage SM = new RoadFlow.Platform.ShortMessage();
            foreach (string app in checkbox_apps.Split(','))
            {
                if (!app.IsGuid()) continue;
                var msg = SM.Get(app.ToGuid());
                if (msg == null) continue;
                SM.Delete(msg.ID);
                RoadFlow.Platform.Log.Add("删除了站内消息", msg.Serialize(), RoadFlow.Platform.Log.Types.信息管理);
            }
            //initData();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('操作成功!');window.location=window.location;", true);
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}