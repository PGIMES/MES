using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDesigner
{
    public partial class Export : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WorkFlow WF = new RoadFlow.Platform.WorkFlow();
            var file = WF.Export(Request.QueryString["flowid"].ToGuid());
            if (!file.IsNullOrEmpty())
            {
                Response.Redirect("../Files/Show.ashx?id=" + file.DesEncrypt() + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"]);
            }
            else
            {
                Response.Write("导出失败");
                Response.End();
            }
        }
    }
}