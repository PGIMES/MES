using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class WaitList : Common.BasePage
    {
        protected string query = string.Empty;
        protected RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected RoadFlow.Platform.AppLibrary bapplibary = new RoadFlow.Platform.AppLibrary();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowTask> taskList;
        private string s_Title1 = string.Empty;
        private string s_FlowID = string.Empty;
        private string s_SenderID = string.Empty;
        private string s_Date1 = string.Empty;
        private string s_Date2 = string.Empty;
        protected void Page_Load(object sender1, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Title1 = Request.QueryString["Title1"];
                s_FlowID = Request.QueryString["FlowID"];
                s_SenderID = Request.QueryString["SenderID"];
                s_Date1 = Request.QueryString["Date1"];
                s_Date2 = Request.QueryString["Date2"];
                this.Title1.Value = s_Title1;
                this.SenderID.Value = RoadFlow.Platform.Users.RemovePrefix(s_SenderID);
                this.Date1.Value = s_Date1;
                this.Date2.Value = s_Date2;
                initData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_Title1 = Request.Form["Title1"];
            s_FlowID = Request.Form["FlowID"];
            s_SenderID = Request.Form["SenderID"];
            s_Date1 = Request.Form["Date1"];
            s_Date2 = Request.Form["Date2"];
            initData();
        }

        private void initData()
        {
            query = string.Format("&appid={0}&tabid={1}&Title1={2}&FlowID={3}&SenderID={4}&Date1={5}&Date2={6}",
                Request.QueryString["appid"], Request.QueryString["tabid"], s_Title1.UrlEncode(), s_FlowID, s_SenderID, s_Date1, s_Date2);
            string pager;
            taskList = bworkFlowTask.GetTasks(RoadFlow.Platform.Users.CurrentUserID,
               out pager, query, s_Title1, s_FlowID, s_SenderID, s_Date1, s_Date2);

            this.flowOptions.Text = bworkFlow.GetOptions(s_FlowID);
            this.Pager.Text = pager;
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}