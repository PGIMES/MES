using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDesigner
{
    public partial class Open_List : Common.BasePage
    {
        protected RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        protected RoadFlow.Platform.Organize borg = new RoadFlow.Platform.Organize();
        protected RoadFlow.Platform.WorkFlow bwf = new RoadFlow.Platform.WorkFlow();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlow> flows;
        protected string type = string.Empty;
        protected string query = string.Empty;
        protected string flowname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["typeid"];
            if (!IsPostBack)
            {
                flowname = Request.QueryString["name"];
                initData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            flowname = Request.Form["flow_name"];
            initData();
        }

        protected void initData()
        {
            query = "&appid=" + Request.QueryString["appid"] + "&typeid=" + type + "&name=" + flowname.UrlEncode() + "&iframeid=" + Request.QueryString["iframeid"] + "&openerid=" + Request.QueryString["openerid"];
            string typeids = "";
            if (type.IsGuid())
            {
                typeids = new RoadFlow.Platform.Dictionary().GetAllChildsIDString(type.ToGuid());
            }
            string pager;
            flows = bwf.GetPagerData(out pager, query, CurrentUserID.ToString(), typeids, flowname, 10);
            this.PaerText.Text = pager;
        }
    }
}