using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDelegation
{
    public partial class Default : Common.BasePage
    {
        protected RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
        protected RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected RoadFlow.Platform.WorkFlowDelegation bworkFlowDelegation = new RoadFlow.Platform.WorkFlowDelegation();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowDelegation> workFlowDelegationList;
        protected string Query1 = string.Empty;
        protected bool isoneself = false;
        private string s_UserID = string.Empty;
        private string s_StartTime = string.Empty;
        private string s_EndTime = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            isoneself = "1" == Request.QueryString["isoneself"];
            if (isoneself)
            {
                this.UserID.Disabled = true;
                this.UserID.Value = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
            }
            if (!IsPostBack)
            {
                s_UserID = Request.QueryString["UserID"];
                s_StartTime = Request.QueryString["StartTime"];
                s_EndTime = Request.QueryString["EndTime"];
                this.UserID.Value = RoadFlow.Platform.Users.RemovePrefix(s_UserID);
                this.StartTime.Value = s_StartTime;
                this.EndTime.Value = s_EndTime;
                initData();
            }
        }

        private void initData()
        {
            Query1 = string.Format("&appid={0}&tabid={1}&isoneself={2}", Request.QueryString["appid"],
                    Request.QueryString["tabid"], Request.QueryString["isoneself"]);
            Query1 += "&StartTime=" + s_StartTime + "&EndTime=" + s_EndTime + "&UserID=" + s_UserID;
            string pager;
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, Query1, RoadFlow.Platform.Users.CurrentUserID.ToString(), s_StartTime, s_EndTime);
            }
            else
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out pager, Query1, RoadFlow.Platform.Users.RemovePrefix(s_UserID), s_StartTime, s_EndTime);
            }
            this.Pager.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["checkbox_app"];
            foreach (string id in ids.Split(','))
            {
                Guid bid;
                if (!id.IsGuid(out bid))
                {
                    continue;
                }
                var comment = bworkFlowDelegation.Get(bid);
                if (comment != null)
                {
                    bworkFlowDelegation.Delete(bid);
                    RoadFlow.Platform.Log.Add("删除了流程意见", comment.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
                }
            }
            bworkFlowDelegation.RefreshCache();
            initData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_UserID = Request.Form["UserID"];
            s_StartTime = Request.Form["StartTime"];
            s_EndTime = Request.Form["EndTime"];
            initData();
        }
    }
}