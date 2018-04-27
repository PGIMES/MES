using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class FlowCopyFor : Common.BasePage
    {
        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = null;
        RoadFlow.Data.Model.WorkFlowTask currentTask = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg;
            if (!Common.Tools.CheckLogin(out msg) && !RoadFlow.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.Write("登录验证错误!");
                Response.End();
            }

            string flowid = Request.QueryString["flowid"];
            string stepid = Request.QueryString["stepid"];
            string groupid = Request.QueryString["groupid"];
            string instanceid = Request.QueryString["instanceid"];
            wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
            if (wfInstalled == null)
            {
                Response.Write("未找到流程运行实体");
                Response.End();
            }

            var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
            if (steps.Count() == 0)
            {
                Response.Write("未找到当前步骤");
                Response.End();
            }

            currentTask = btask.Get(Request.QueryString["taskid"].ToGuid());
            if (currentTask == null)
            {
                Response.Write("当前任务为空,请先保存再抄送!");
                Response.End();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var tasks = btask.GetTaskList(currentTask.ID);
            var users = new RoadFlow.Platform.Organize().GetAllUsers(Request.Form["user"] ?? "");
            System.Text.StringBuilder names = new System.Text.StringBuilder();
            foreach (var user in users)
            {
                if (tasks.Find(p => p.ReceiveID == user.ID) != null)
                {
                    continue;
                }
                var nextStep = wfInstalled.Steps.Where(p => p.ID == Request.QueryString["stepid"].ToGuid()).First();
                RoadFlow.Data.Model.WorkFlowTask task = new RoadFlow.Data.Model.WorkFlowTask();
                if (nextStep.WorkTime > 0)
                {
                    task.CompletedTime = RoadFlow.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                }
                task.FlowID = currentTask.FlowID;
                task.GroupID = currentTask.GroupID;
                task.ID = Guid.NewGuid();
                task.Type = 5;
                task.InstanceID = currentTask.InstanceID;
                task.Note = "抄送任务";
                task.PrevID = currentTask.PrevID;
                task.PrevStepID = currentTask.PrevStepID;
                task.ReceiveID = user.ID;
                task.ReceiveName = user.Name;
                task.ReceiveTime = RoadFlow.Utility.DateTimeNew.Now;
                task.SenderID = currentTask.ReceiveID;
                task.SenderName = currentTask.ReceiveName;
                task.SenderTime = task.ReceiveTime;
                task.Status = 0;
                task.StepID = currentTask.StepID;
                task.StepName = currentTask.StepName;
                task.Sort = currentTask.Sort;
                task.Title = currentTask.Title;
                btask.Add(task);
                names.Append(task.ReceiveName);
                names.Append(",");
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('成功抄送给：" + names.ToString().TrimEnd(',') + "');new RoadUI.Window().close();", true);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true;// base.CheckLogin(isRedirect);
        }
    }
}