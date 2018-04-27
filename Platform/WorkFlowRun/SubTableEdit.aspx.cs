using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class SubTableEdit : Common.BasePage
    {
        protected string editmodel = string.Empty;
        protected string prevurl = string.Empty;
        protected bool isshow = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            editmodel = Request.QueryString["editmodel"];
            prevurl = Request.QueryString["prevurl"];
            isshow = "1" == Request.QueryString["display"];
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            RoadFlow.Data.Model.WorkFlowCustomEventParams parmas = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
            string instanceid1 = new RoadFlow.Platform.WorkFlow().SaveFromData(Request.QueryString["instanceid"], parmas);
            string opt = string.Empty;
            if ("1" == editmodel)
            {
                opt = "new RoadUI.Window().reloadOpener('" + prevurl + "');new RoadUI.Window().close();";
            }
            else
            {
                opt = "window.location='" + prevurl + "';";
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');" + opt, true);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}