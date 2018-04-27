using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class GoTo : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.WorkFlowInstalledSub.Step> nextSteps = new List<RoadFlow.Data.Model.WorkFlowInstalledSub.Step>();
        protected string TaskID = string.Empty;
        protected RoadFlow.Data.Model.WorkFlowTask Task = null;
        protected RoadFlow.Platform.WorkFlowTask BTask = new RoadFlow.Platform.WorkFlowTask();
        protected void Page_Load(object sender, EventArgs e)
        {
            TaskID = Request.QueryString["taskid"];
            Task = BTask.Get(TaskID.ToGuid());
            if (Task == null)
            {
                Response.Write("未找到任务");
                Response.End();
            }
            var wfins = new RoadFlow.Platform.WorkFlow().GetWorkFlowRunModel(Task.FlowID);
            if (wfins == null)
            {
                Response.Write("未找到流程运行时");
                Response.End();
            }
            nextSteps = wfins.Steps.ToList();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] stepids = (Request.Form["step"] ?? "").Split(',');
            Dictionary<Guid, string> steps = new Dictionary<Guid, string>();
            foreach (string step in stepids)
            {
                if (!step.IsGuid()) continue;
                string member = Request.Form["member_" + step];
                if (member.IsNullOrEmpty()) continue;
                steps.Add(step.ToGuid(), member);
            }
            bool isgoto = BTask.GoToTask(Task, steps);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('跳转" + (isgoto ? "成功" : "失败") + "');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}