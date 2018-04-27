using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebForm.Platform.UserInfo
{
    public partial class EditUserHeader : Common.BasePage
    {
        protected RoadFlow.Platform.Users bui = new RoadFlow.Platform.Users();
        protected RoadFlow.Data.Model.Users ui = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.HeadImg.Src = "../../Files/UserHeads/default.jpg";
            ui = CurrentUser;
            if (!IsPostBack)
            {
                if (ui != null)
                {
                    if (!ui.HeadImg.IsNullOrEmpty() && File.Exists(Server.MapPath(ui.HeadImg)))
                    {
                        this.HeadImg.Src = ui.HeadImg;
                    }
                }
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}