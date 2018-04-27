using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class Hasten : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowTask WFT = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Data.Model.WorkFlowTask task = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled("正在发送");
            string taskid = Request.QueryString["taskid"];
            task = WFT.Get(taskid.ToGuid());
            if (!IsPostBack)
            {
                this.HastenUsersText.Text = WFT.GetHastenUsersCheckboxString(taskid.ToGuid(), "HastenUsers");
                this.HastenTypeText.Text = RoadFlow.Platform.HastenLog.GetHastenTypeCheckboxString("HastenType", "");
                this.Contents.Value = "您有一个 " + (task != null ? task.Title : "") + " 待办事项，请尽快办理。";
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string users = Request.Form["HastenUsers"];
            string types = Request.Form["HastenType"];
            string contents = Request.Form["Contents"];

            RoadFlow.Platform.HastenLog.Hasten(types, users, contents, task);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('催办成功!');new RoadUI.Window().close()", true);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}