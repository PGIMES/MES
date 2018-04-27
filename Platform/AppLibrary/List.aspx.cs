using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.AppLibrary
{
    public partial class List : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.AppLibrary> AppList = new List<RoadFlow.Data.Model.AppLibrary>();
        protected RoadFlow.Platform.Dictionary bdict = new RoadFlow.Platform.Dictionary();
        protected RoadFlow.Platform.AppLibrary bapp = new RoadFlow.Platform.AppLibrary();
        protected string Query = string.Empty;
        protected string Query1 = string.Empty;
        string s_Title1 = string.Empty;
        string s_Address = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Title1 = Request.QueryString["Title1"];
                s_Address = Request.QueryString["Address"];
                this.Title1.Value = s_Title1;
                this.Address.Value = s_Address;
                initData();
            }
        }

        private void initData()
        {
            string appid = Request.QueryString["appid"];
            string tabid = Request.QueryString["tabid"];
            string typeid = Request.QueryString["typeid"];
            string pager;
            string typeidstring = typeid.IsGuid() ? bapp.GetAllChildsIDString(typeid.ToGuid()) : "";
            Query = string.Format("&appid={0}&tabid={1}&title1={2}&typeid={3}&address={4}",
                        Request.QueryString["appid"],
                        Request.QueryString["tabid"],
                        s_Title1.UrlEncode(), typeid, s_Address.UrlEncode()
                        );
            Query1 = string.Format("{0}&pagesize={1}&pagenumber={2}", Query, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);
            AppList = bapp.GetPagerData(out pager, Query, s_Title1, typeidstring, s_Address);
            this.Pager.Text = pager;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_Title1 = Request.Form["Title1"];
            s_Address = Request.Form["Address"];

            initData();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string deleteID = Request.Form["checkbox_app"];
            System.Text.StringBuilder delxml = new System.Text.StringBuilder();
            RoadFlow.Platform.Menu bMenu = new RoadFlow.Platform.Menu();
            RoadFlow.Platform.MenuUser bMenuUser = new RoadFlow.Platform.MenuUser();
            RoadFlow.Platform.UserShortcut bUserShortcut = new RoadFlow.Platform.UserShortcut();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (string id in deleteID.Split(','))
                {
                    Guid gid;
                    if (id.IsGuid(out gid))
                    {
                        var app = bapp.Get(gid);
                        if (app != null)
                        {
                            var menus = bMenu.GetAllByApplibaryID(app.ID);
                            foreach (var menu in menus)
                            {
                                bMenuUser.DeleteByMenuID(menu.ID);
                                bUserShortcut.DeleteByMenuID(menu.ID);
                            }
                            delxml.Append(app.Serialize());
                            bapp.Delete(gid);
                        }
                    }
                }
                RoadFlow.Platform.Log.Add("删除了一批应用程序库", delxml.ToString(), RoadFlow.Platform.Log.Types.菜单权限);
                scope.Complete();
            }

            bMenu.ClearAllDataTableCache();
            bMenuUser.ClearCache();
            bUserShortcut.ClearCache();
            bapp.ClearCache();
           
            initData();
        }
    }
}