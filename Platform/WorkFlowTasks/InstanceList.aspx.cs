using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class InstanceList : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected DataTable Dt = new DataTable();
        protected string query = string.Empty;
        private string s_Title = string.Empty;
        private string s_FlowID = string.Empty;
        private string s_SenderID = string.Empty;
        private string s_Date1 = string.Empty;
        private string s_Date2 = string.Empty;
        private string s_Status = string.Empty;
        private string typeid = string.Empty;
        protected void Page_Load(object sender1, EventArgs e)
        {
            
            typeid = Request.QueryString["typeid"];
            if (!IsPostBack)
            {
                s_Title = Request.QueryString["Title1"];
                s_FlowID = Request.QueryString["FlowID"];
                s_SenderID = Request.QueryString["SenderID"];
                s_Date1 = Request.QueryString["Date1"];
                s_Date2 = Request.QueryString["Date2"];
                s_Status = Request.QueryString["Status"];
                this.Title1.Value = s_Title;
                this.SenderID.Value = s_SenderID;
                this.Date1.Value = s_Date1;
                this.Date2.Value = s_Date2;
                InitData();
            }
        }

        private void InitData()
        {

            string query1 = string.Format("&appid={0}&tabid={1}&Title1={2}&FlowID={3}&SenderID={4}&Date1={5}&Date2={6}&Status={7}&typeid={8}",
                    Request.QueryString["appid"], Request.QueryString["tabid"], s_Title.UrlEncode(), s_FlowID, s_SenderID, s_Date1, s_Date2, s_Status, typeid);
            query = string.Format("{0}&pagesize={1}&pagenumber={2}", query1, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);
            string pager;

            List<System.Web.UI.WebControls.ListItem> statusItems = new List<System.Web.UI.WebControls.ListItem>();
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "", Value = "0", Selected = "0" == s_Status });
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "未完成", Value = "1", Selected = "1" == s_Status });
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "已完成", Value = "2", Selected = "2" == s_Status });
            this.Status.Items.AddRange(statusItems.ToArray());

            //可管理的流程ID数组
            var flows = bworkFlow.GetInstanceManageFlowIDList(RoadFlow.Platform.Users.CurrentUserID, typeid);
            List<Guid> flowids = new List<Guid>();
            foreach (var flow in flows.OrderBy(p => p.Value))
            {
                flowids.Add(flow.Key);
            }
            Guid[] manageFlows = flowids.ToArray();
            this.FlowOptions.Text = bworkFlow.GetOptions(flows, typeid, s_FlowID);

            Dt = bworkFlowTask.GetInstances1(manageFlows, new Guid[] { },
                s_SenderID.IsNullOrEmpty() ? new Guid[] { } : new Guid[] { s_SenderID.Replace(RoadFlow.Platform.Users.PREFIX, "").ToGuid() },
                out pager, query1, s_Title, s_FlowID, s_Date1, s_Date2, s_Status.ToInt());
            this.Pager.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_Title = Request.Form["Title1"];
            s_FlowID = Request.Form["FlowID"];
            s_SenderID = Request.Form["SenderID"];
            s_Date1 = Request.Form["Date1"];
            s_Date2 = Request.Form["Date2"];
            s_Status = Request.Form["Status"];
            InitData();
        }
    }
}