using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.AppLibrary
{
    public partial class SubPages : Common.BasePage
    {
        protected string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = "&pagesize=" + Request.QueryString["pagesize"] + "&pagenumber=" + Request.QueryString["pagenumber"]
         + "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"]
         + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
         + "&iframeid=" + Request.QueryString["iframeid"] + "&openerid=" + Request.QueryString["openerid"];

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string subpagesbox = Request.Form["subpagesbox"] ?? "";
            RoadFlow.Platform.AppLibrarySubPages Sub = new RoadFlow.Platform.AppLibrarySubPages();
            RoadFlow.Platform.AppLibraryButtons1 But = new RoadFlow.Platform.AppLibraryButtons1();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var subindex in subpagesbox.Split(','))
                {
                    if (subindex.IsGuid())
                    {
                        Sub.Delete(subindex.ToGuid());
                        But.DeleteByAppID(subindex.ToGuid());
                    }
                }
                Sub.ClearCache();
                But.ClearCache();
                RoadFlow.Platform.Log.Add("删除了子页面", subpagesbox, RoadFlow.Platform.Log.Types.菜单权限);
                scope.Complete();
            }
        }
    }
}