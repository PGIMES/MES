using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Menu
{
    public partial class Body : Common.BasePage
    {
        RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
        RoadFlow.Platform.Menu bMenu = new RoadFlow.Platform.Menu();
        RoadFlow.Data.Model.Menu menu = null;
        protected string AppTypesOptions = string.Empty;
        protected bool isRoot = false;
        protected string appid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Guid appID;
            if (id.IsGuid(out appID))
            {
                menu = bMenu.Get(appID);
            }
            string type = "";
            if (!IsPostBack)
            {
                if (menu != null)
                {
                    this.Name.Value = menu.Title;
                    this.Params.Value = menu.Params;
                    isRoot = menu.ParentID == Guid.Empty;
                    if (menu.AppLibraryID.HasValue)
                    {
                        var app = new RoadFlow.Platform.AppLibrary().Get(menu.AppLibraryID.Value);
                        if (app != null)
                        {
                            type = app.Type.ToString();
                        }
                    }
                    appid = menu.AppLibraryID.ToString();
                    
                }
            }
            AppTypesOptions = bappLibrary.GetTypeOptions(type);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (menu == null)
            {
                return;
            }
            string name = Request.Form["Name"];
            string type = Request.Form["Type"];
            string appid = Request.Form["AppID"];
            string params1 = Request.Form["Params"];
            string ico = Request.Form["Ico"];

            string oldXML = menu.Serialize();
            menu.Title = name.Trim();
            if (appid.IsGuid())
            {
                menu.AppLibraryID = appid.ToGuid();
            }
            else
            {
                menu.AppLibraryID = null;
            }
            menu.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
            if (!ico.IsNullOrEmpty())
            {
                menu.Ico = ico;
            }
            else
            {
                menu.Ico = null;
            }

            bMenu.Update(menu);
            bMenu.ClearAllDataTableCache();
            RoadFlow.Platform.Log.Add("修改了菜单", "", RoadFlow.Platform.Log.Types.菜单权限, oldXML, menu.Serialize());
            string refreshID = menu.ParentID == Guid.Empty ? menu.ID.ToString() : menu.ParentID.ToString();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');alert('保存成功!');", true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (menu == null)
            {
                return;
            }
            int i = bMenu.DeleteAndAllChilds(menu.ID);
            RoadFlow.Platform.Log.Add("删除了菜单及其所有下级共" + i.ToString() + "项", menu.Serialize(), RoadFlow.Platform.Log.Types.菜单权限);
            string refreshID = menu.ParentID == Guid.Empty ? menu.ID.ToString() : menu.ParentID.ToString();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');window.location='Body.aspx?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "';", true);
        }
    }
}