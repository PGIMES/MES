using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class ShowForm : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskID = Request.QueryString["taskid"];
            if (taskID.IsGuid())
            {
                RoadFlow.Platform.WorkFlowTask workFlowTask = new RoadFlow.Platform.WorkFlowTask();
                var task = workFlowTask.Get(taskID.ToGuid());
                if (task != null)
                {
                    var mainTasks = workFlowTask.GetBySubFlowGroupID(task.GroupID);
                    if (mainTasks.Count > 0)
                    {
                        var mainTask = mainTasks.OrderByDescending(p => p.Sort).FirstOrDefault();
                        string url = ("1" == Request.QueryString["ismobile"] ? "Default_App.aspx" : "Default.aspx") + "?flowid=" + mainTask.FlowID + "&stepid=" + mainTask.StepID +
                            "&instanceid=" + mainTask.InstanceID + "&taskid=" + mainTask.ID + "&groupid=" + mainTask.GroupID +
                            "&appid=" + Request.QueryString["appid"] + "&display=1&tabid=" + Request.QueryString["tabid"];
                        Server.Transfer(url);
                    }
                }
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