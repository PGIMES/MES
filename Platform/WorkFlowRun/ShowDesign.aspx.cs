using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class ShowDesign : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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