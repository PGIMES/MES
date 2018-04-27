using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.WeiXin.StartFlows
{
    public partial class Default : System.Web.UI.Page
    {
        public List<RoadFlow.Data.Model.WorkFlowStart> StartFlows;
        public RoadFlow.Platform.Users BUsers = new RoadFlow.Platform.Users();
        public string s_searchkey = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WeiXin.Organize.CheckLogin();
            if (!IsPostBack)
            {
                s_searchkey = Request.QueryString["searchkey"];
                InitFlows();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_searchkey = Request.Form["searchkey"];
            InitFlows();
        }

        protected void InitFlows()
        {
            StartFlows = new RoadFlow.Platform.WorkFlow().GetUserStartFlows(RoadFlow.Platform.WeiXin.Organize.CurrentUserID);
            if (!s_searchkey.IsNullOrEmpty())
            {
                StartFlows = StartFlows.FindAll(p => p.Name.Contains(s_searchkey.Trim1(), StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}