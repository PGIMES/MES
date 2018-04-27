using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls.SelectDiv
{
    public partial class Default : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string applibaryid = Request.QueryString["applibaryid"];
            var app = new RoadFlow.Platform.AppLibrary().Get(applibaryid.ToGuid());
            if (app != null && !app.Address.IsNullOrEmpty())
            {
                string url = app.Address + (app.Address.IndexOf('?') >= 0 ? app.Params : "?" + app.Params) + "&" + Request.Url.Query.TrimStart('?');
                Response.Redirect(url);
            }
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}