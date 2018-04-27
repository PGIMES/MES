using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.WeiXin.Agents
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.WeiXinAgent> AgentList = new List<RoadFlow.Data.Model.WeiXinAgent>();
        protected RoadFlow.Platform.WeiXin.Agents BAgents = new RoadFlow.Platform.WeiXin.Agents();
        protected string s_name = string.Empty;
        protected string Query1 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_name = Request.QueryString["title1"];
                InitData();
            }
        }


        protected void InitData()
        {
            Query1 = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&title1=" + s_name.UrlEncode();
            AgentList = BAgents.GetAgents();
            if (!s_name.IsNullOrEmpty())
            {
                AgentList = AgentList.FindAll(p => p.name.Contains(s_name, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_name = this.Title1.Value;
            InitData();
        }
    }
}