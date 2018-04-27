using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class ChangeStatus : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowTask BTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Data.Model.WorkFlowTask task = null;
        protected string Status = string.Empty;
        protected string taskid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskid = Request.QueryString["taskid"];
            if (taskid.IsGuid())
            {
                task = BTask.Get(taskid.ToGuid());
            }
            if (task != null && !IsPostBack)
            {
                Status = task.Status.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Status = Request.Form["Status"];
            if (task != null && Status.IsInt())
            {
                string oldxml = task.Serialize();
                task.Status = Status.ToInt();
                BTask.Update(task);
                RoadFlow.Platform.Log.Add("改变了流程任务状态", "改变了流程任务状态", RoadFlow.Platform.Log.Types.流程相关, oldxml, task.Serialize());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('设置成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}