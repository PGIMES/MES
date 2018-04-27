using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowButtons
{
    public partial class Default : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowButtons bworkFlowButtons = new RoadFlow.Platform.WorkFlowButtons();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowButtons> workFlowButtonsList;
        protected string Query1 = string.Empty;
        private string s_Name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initData();
            }
        }

        private void initData()
        {
            Query1 = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            workFlowButtonsList = bworkFlowButtons.GetAll();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            initData();
            s_Name = Request.Form["Name"];
            if (!s_Name.IsNullOrEmpty())
            {
                workFlowButtonsList = workFlowButtonsList.Where(p => p.Title.IndexOf(s_Name) >= 0);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["checkbox_app"];
            foreach (string id in ids.Split(','))
            {
                Guid bid;
                if (!id.IsGuid(out bid))
                {
                    continue;
                }
                var but = bworkFlowButtons.Get(bid);
                if (but != null)
                {
                    bworkFlowButtons.Delete(bid);
                    RoadFlow.Platform.Log.Add("删除了流程按钮", but.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
                }
            }
            bworkFlowButtons.ClearCache();
            initData();
        }
    }
}