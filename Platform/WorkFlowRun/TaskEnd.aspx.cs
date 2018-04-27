using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class TaskEnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskid = Request.QueryString["taskid"];
            string msg = taskid.IsGuid() ? new RoadFlow.Platform.WorkFlowTask().EndTask(taskid.ToGuid()) : "参数错误";
            RoadFlow.Platform.Log.Add("终止的流程任务", taskid, RoadFlow.Platform.Log.Types.流程相关);
            if ("1" != msg)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "err", "alert('" + msg + "')", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "err", "alert('终止成功!'); try{top.mainDialog.close();}catch(e){} try{top.mainTab.closeTab();}catch(e){parent.close();}", true);
                return;
            }
        }

    }
}