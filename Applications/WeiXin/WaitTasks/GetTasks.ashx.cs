using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;

namespace WebForm.Applications.WeiXin.WaitTasks
{
    /// <summary>
    /// GetTasks 的摘要说明
    /// </summary>
    public class GetTasks : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string pageNumber = context.Request.QueryString["pagenumber"];
            string pageSize = context.Request.QueryString["pagesize"];
            string searchTitle = context.Request.QueryString["SearchTitle"];
            long count;
            Guid userID = RoadFlow.Platform.WeiXin.Organize.CurrentUserID;
            var tasks = new RoadFlow.Platform.WorkFlowTask().GetTasks(userID, out count, pageSize.ToInt(15), pageNumber.ToInt(2), title: searchTitle);
            LitJson.JsonData jd = new LitJson.JsonData();
            if (tasks.Count == 0)
            {
                context.Response.Write("[]");
                context.Response.End();
            }
            foreach (var task in tasks)
            {
                LitJson.JsonData jd1 = new LitJson.JsonData();
                jd1["id"] = task.ID.ToString();
                jd1["title"] = task.Title;
                jd1["time"] = task.ReceiveTime.ToDateTimeString();
                jd1["sender"] = task.SenderName;
                jd1["flowid"] = task.FlowID.ToString();
                jd1["stepid"] = task.StepID.ToString();
                jd1["instanceid"] = task.InstanceID;
                jd1["groupid"] = task.GroupID.ToString();
                jd.Add(jd1);
            }
            context.Response.Write(jd.ToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}