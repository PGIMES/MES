using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class SortUsers : Common.BasePage
    {
        RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        protected List<RoadFlow.Data.Model.Users> Users = new List<RoadFlow.Data.Model.Users>();
        string parentID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            parentID = Request.QueryString["parentid"];
            if (!IsPostBack)
            {
                Users = new RoadFlow.Platform.Organize().GetAllUsers(parentID.ToGuid());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sort = Request.Form["sort"] ?? "";
            string[] sortArray = sort.Split(',');
            
            for (int i = 0; i < sortArray.Length; i++)
            {
                Guid gid;
                if (!sortArray[i].IsGuid(out gid))
                {
                    continue;
                }
                busers.UpdateSort(gid, i + 1);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + parentID + "');", true);
            Users = new RoadFlow.Platform.Organize().GetAllUsers(parentID.ToGuid());
        }
    }
}