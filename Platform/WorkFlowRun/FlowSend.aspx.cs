using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class FlowSend : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg;
            if (!Common.Tools.CheckLogin(out msg) && !RoadFlow.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.Write("登录验证错误!");
                Response.End();
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true;// base.CheckLogin(isRedirect);
        }
    }
}