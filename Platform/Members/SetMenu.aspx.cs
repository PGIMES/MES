using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class SetMenu : Common.BasePage
    {
        protected string menuhtml = string.Empty;
        RoadFlow.Platform.Menu bmenu = new RoadFlow.Platform.Menu();
        RoadFlow.Platform.MenuUser bmenuuser = new RoadFlow.Platform.MenuUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];

            if (!IsPostBack)
            {
                menuhtml = bmenu.GetMenuTreeTableHtml(id);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string type = Request.QueryString["type"];
            string userid = ("4" == type ? RoadFlow.Platform.Users.PREFIX : "5" == type ? RoadFlow.Platform.WorkGroup.PREFIX : "") + id;

            string menus = Request.Form["menuid"] ?? "";
            RoadFlow.Platform.Organize borg = new RoadFlow.Platform.Organize();
            bmenuuser.ClearCache();
            string useridstring = borg.GetAllUsersIdList(userid).ToArray().Join1(",");
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                bmenuuser.DeleteByOrganizes(userid);
                foreach (string menu in menus.Split(','))
                {
                    if (!menu.IsGuid())
                    {
                        continue;
                    }
                    RoadFlow.Data.Model.MenuUser mu = new RoadFlow.Data.Model.MenuUser();
                    mu.Buttons = Request.Form["button_" + menu] ?? "";
                    mu.SubPageID = Guid.Empty;
                    mu.ID = Guid.NewGuid();
                    mu.MenuID = menu.ToGuid();
                    mu.Organizes = userid;
                    mu.Users = useridstring;
                    mu.Params = (Request.Form["params_" + menu] ?? "").Replace("\"","");
                    bmenuuser.Add(mu);
                    string subpage_ = Request.Form["subpage_" + menu] ?? "";
                    foreach (string subpage in subpage_.Split(','))
                    {
                        if (!subpage.IsGuid())
                        {
                            continue;
                        }
                        RoadFlow.Data.Model.MenuUser mu1 = new RoadFlow.Data.Model.MenuUser();
                        mu1.Buttons = Request.Form["button_" + menu + "_" + subpage] ?? "";
                        mu1.SubPageID = subpage.ToGuid();
                        mu1.ID = Guid.NewGuid();
                        mu1.MenuID = mu.MenuID;
                        mu1.Organizes = userid;
                        mu1.Users = useridstring;
                        bmenuuser.Add(mu1);
                    }
                }

                scope.Complete();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location=window.location;", true);
            }
            menuhtml = bmenu.GetMenuTreeTableHtml(id);
            bmenuuser.ClearCache();
        }

    }
}