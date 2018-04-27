using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Menu
{
    public partial class AddApp : Common.BasePage
    {
        RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
        RoadFlow.Platform.Menu bMenu = new RoadFlow.Platform.Menu();
        RoadFlow.Data.Model.Menu menu = null;
        protected string AppTypesOptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AppTypesOptions = bappLibrary.GetTypeOptions("");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string name = Request.Form["Name"];
            string type = Request.Form["Type"];
            string appid = Request.Form["AppID"];
            string params1 = Request.Form["Params"];
            string ico = Request.Form["Ico"];

            RoadFlow.Data.Model.Menu menu1 = new RoadFlow.Data.Model.Menu();

            menu1.ID = Guid.NewGuid();
            menu1.ParentID = id.ToGuid();
            menu1.Title = name.Trim();
            menu1.Sort = bMenu.GetMaxSort(menu1.ParentID);

            if (appid.IsGuid())
            {
                menu1.AppLibraryID = appid.ToGuid();
            }
            else
            {
                menu1.AppLibraryID = null;
            }
            menu1.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
            if (!ico.IsNullOrEmpty())
            {
                menu1.Ico = ico;
            }

            bMenu.Add(menu1);
            RoadFlow.Platform.Log.Add("添加了菜单", menu1.Serialize(), RoadFlow.Platform.Log.Types.菜单权限);
            string refreshID = id;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功');parent.frames[0].reLoad('" + refreshID + "');window.location=window.location;", true);
        }
    }
}