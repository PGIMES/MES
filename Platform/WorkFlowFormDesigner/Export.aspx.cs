using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    public partial class Export : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WorkFlowForm WFF = new RoadFlow.Platform.WorkFlowForm();
            var file = WFF.Export(Request.QueryString["formid"].ToGuid());
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