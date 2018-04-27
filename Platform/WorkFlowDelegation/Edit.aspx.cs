using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDelegation
{
    public partial class Edit : Common.BasePage
    {
        protected bool isOneSelf = false;
        protected RoadFlow.Platform.WorkFlowDelegation bworkFlowDelegation = new RoadFlow.Platform.WorkFlowDelegation();
        protected RoadFlow.Data.Model.WorkFlowDelegation workFlowDelegation = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                this.UserID1.Value = RoadFlow.Platform.Users.PREFIX + CurrentUserID.ToString();
                this.UserID1.Disabled = true;
            }
            Guid delegationID;
            string flowID = string.Empty;
            if (id.IsGuid(out delegationID))
            {
                workFlowDelegation = bworkFlowDelegation.Get(delegationID);
            }
            if (!IsPostBack)
            {
                if (workFlowDelegation != null)
                {
                    flowID = workFlowDelegation.FlowID.ToString();
                    this.UserID1.Value = RoadFlow.Platform.Users.PREFIX + workFlowDelegation.UserID.ToString();
                    this.ToUserID.Value = RoadFlow.Platform.Users.PREFIX + workFlowDelegation.ToUserID.ToString();
                    this.StartTime.Value = workFlowDelegation.StartTime.ToDateTimeString();
                    this.EndTime.Value = workFlowDelegation.EndTime.ToDateTimeString();
                    this.Note.Value = workFlowDelegation.Note;
                }
            }
            this.FlowOptions.Text = new RoadFlow.Platform.WorkFlow().GetOptions(flowID);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string UserID = Request.Form["UserID1"];
            string ToUserID = Request.Form["ToUserID"];
            string StartTime = Request.Form["StartTime"];
            string EndTime = Request.Form["EndTime"];
            string FlowID = Request.Form["FlowID"];
            string Note = Request.Form["Note"];

            string oldXML = workFlowDelegation.Serialize();
            bool isAdd = false;
            if (workFlowDelegation == null)
            {
                isAdd = true;
                workFlowDelegation = new RoadFlow.Data.Model.WorkFlowDelegation();
                workFlowDelegation.ID = Guid.NewGuid();
            }
            workFlowDelegation.UserID = isOneSelf ? RoadFlow.Platform.Users.CurrentUserID : RoadFlow.Platform.Users.RemovePrefix(UserID).ToGuid();
            workFlowDelegation.EndTime = EndTime.ToDateTime();
            if (FlowID.IsGuid())
            {
                workFlowDelegation.FlowID = FlowID.ToGuid();
            }
            workFlowDelegation.Note = Note.IsNullOrEmpty() ? null : Note;
            workFlowDelegation.StartTime = StartTime.ToDateTime();
            workFlowDelegation.ToUserID = RoadFlow.Platform.Users.RemovePrefix(ToUserID).ToGuid();
            workFlowDelegation.WriteTime = RoadFlow.Utility.DateTimeNew.Now;

            if (isAdd)
            {
                bworkFlowDelegation.Add(workFlowDelegation);
                RoadFlow.Platform.Log.Add("添加了工作委托", workFlowDelegation.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
            }
            else
            {
                bworkFlowDelegation.Update(workFlowDelegation);
                RoadFlow.Platform.Log.Add("修改了工作委托", "", RoadFlow.Platform.Log.Types.流程相关, oldXML, workFlowDelegation.Serialize());
            }
            bworkFlowDelegation.RefreshCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
        }
    }
}