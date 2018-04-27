using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.WeiXin.CompletedTasks
{
    public partial class Default : System.Web.UI.Page
    {
        protected Guid UserID;
        protected List<RoadFlow.Data.Model.WorkFlowTask> TasksList = new List<RoadFlow.Data.Model.WorkFlowTask>();
        protected string SearchTitle = string.Empty;
        protected RoadFlow.Platform.WorkFlowTask BWF = new RoadFlow.Platform.WorkFlowTask();
        protected long count;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WeiXin.Organize.CheckLogin();
            UserID = RoadFlow.Platform.WeiXin.Organize.CurrentUserID;
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SearchTitle = this.searchkey.Value;
            InitData();
        }

        protected void InitData()
        {
            TasksList = BWF.GetTasks(UserID, out count, 15, 1, title: SearchTitle, type: 1);
            this.searchkey.Attributes.Add("placeholder", "您共有" + count.ToString() + "条已办事项");
        }
    }
}