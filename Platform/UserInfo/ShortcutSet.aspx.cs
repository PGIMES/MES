using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserInfo
{
    public partial class ShortcutSet : Common.BasePage
    {
        protected string menuhtml = string.Empty;
        protected RoadFlow.Platform.Menu bmenu = new RoadFlow.Platform.Menu();
        protected RoadFlow.Platform.UserShortcut bus = new RoadFlow.Platform.UserShortcut();
        protected System.Data.DataTable busDt = new System.Data.DataTable();
        protected string json = string.Empty;
        protected Guid userID = Guid.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            userID = CurrentUserID;
            busDt = bus.GetDataTableByUserID(userID);
            json = busDt.ToJsonString();
            if (!IsPostBack)
            {
                menuhtml = bmenu.GetMenuTreeTableHtml(id, CurrentUserID);
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string menuids = Request.Form["menuid"]??"";
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                bus.DeleteByUserID(userID);
                int i = 0;
                foreach (string menuid in menuids.Split(','))
                {
                    if (menuid.IsGuid())
                    {
                        RoadFlow.Data.Model.UserShortcut us = new RoadFlow.Data.Model.UserShortcut();
                        us.ID = Guid.NewGuid();
                        us.MenuID = menuid.ToGuid();
                        us.Sort = i++;
                        us.UserID = userID;
                        bus.Add(us);
                    }
                }
                scope.Complete();
            }
            bus.ClearCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location=window.location;", true);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string[] sorts = (Request.Form["sort"]??"").Split(',');
            for (int i = 0; i < sorts.Length; i++)
            {
                var us = bus.Get(sorts[i].ToGuid());
                if (us != null)
                {
                    us.Sort = i;
                    bus.Update(us);
                }
            }
            bus.ClearCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('排序保存成功!');window.location=window.location;", true);
        }
    }
}