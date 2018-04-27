using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Menu
{
    public partial class Sort : Common.BasePage
    {
        RoadFlow.Platform.Menu bMenu = new RoadFlow.Platform.Menu();
        protected List<RoadFlow.Data.Model.Menu> menuList = new List<RoadFlow.Data.Model.Menu>();
        RoadFlow.Data.Model.Menu menu = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            menu = bMenu.Get(id.ToGuid());
            if (!IsPostBack)
            {
                menuList = bMenu.GetChild(menu.ParentID);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(menu==null)
            {
                return;
            }
            string srots = Request.Form["sortapp"];
            string[] sortArray = srots.Split(new char[] { ',' });
            for (int i = 0; i < sortArray.Length; i++)
            {
                Guid guid;
                if (!sortArray[i].IsGuid(out guid))
                {
                    continue;
                }
                bMenu.UpdateSort(guid, i + 1);
            }

            string rid = menu.ParentID.ToString();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + rid + "');",true);
            menuList = bMenu.GetChild(menu.ParentID);
            bMenu.ClearAllDataTableCache();
        }
    }
}