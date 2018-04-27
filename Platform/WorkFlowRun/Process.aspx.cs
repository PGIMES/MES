using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class Process : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true;// base.CheckLogin(isRedirect);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;// base.CheckUrl(isEnd);
        }
    }
}