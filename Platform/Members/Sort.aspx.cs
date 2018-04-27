using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class Sort : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.Organize> Orgs = new List<RoadFlow.Data.Model.Organize>();
        string parentid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            parentid = Request.QueryString["parentid"];
            if (!IsPostBack)
            {
                Orgs = new RoadFlow.Platform.Organize().GetChilds(parentid.ToGuid());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sort = Request.Form["sort"] ?? "";
            string[] sortArray = sort.Split(',');
            RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
            for (int i = 0; i < sortArray.Length; i++)
            {
                Guid gid;
                if (!sortArray[i].IsGuid(out gid))
                {
                    continue;
                }
                borganize.UpdateSort(gid, i + 1);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + parentid + "');", true);
            Orgs = new RoadFlow.Platform.Organize().GetChilds(parentid.ToGuid());
        }
    }
}