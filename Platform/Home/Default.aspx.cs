using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Home
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.HomeItems> ItemsList = new List<RoadFlow.Data.Model.HomeItems>();
        protected RoadFlow.Platform.HomeItems BHI = new RoadFlow.Platform.HomeItems();
        protected void Page_Load(object sender, EventArgs e)
        {
            ItemsList = BHI.GetAllByUserID(CurrentUserID);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}