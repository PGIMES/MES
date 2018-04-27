using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class StartFlow : Common.BasePage
    {
        public List<RoadFlow.Data.Model.WorkFlowStart> StartFlows;
        public RoadFlow.Platform.Users BUsers = new RoadFlow.Platform.Users();
        public string s_FlowName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_FlowName = Request.QueryString["FlowName"];
                InitFlows();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_FlowName = Request.Form["FlowName"];
            InitFlows();
        }

        protected void InitFlows()
        {
            StartFlows = new RoadFlow.Platform.WorkFlow().GetUserStartFlows(CurrentUserID);
            if (!s_FlowName.IsNullOrEmpty())
            {
                StartFlows = StartFlows.FindAll(p => p.Name.Contains(s_FlowName.Trim1(), StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}