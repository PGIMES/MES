using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.AppLibrary
{
    public partial class SubPageEdit : Common.BasePage
    {
        RoadFlow.Platform.AppLibrarySubPages Sub = new RoadFlow.Platform.AppLibrarySubPages();
        protected RoadFlow.Data.Model.AppLibrarySubPages sub = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string subid = Request.QueryString["subid"];
            if (subid.IsGuid())
            {
                sub = Sub.Get(subid.ToGuid());
            }
            if (!IsPostBack)
            {
                if (sub != null)
                {
                    this.Title1.Value = sub.Name;
                    this.Address.Value = sub.Address;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Title = Request.Form["Title1"];
            string Address = Request.Form["Address"];

            bool isAdd = false;
            if (sub == null)
            {
                sub = new RoadFlow.Data.Model.AppLibrarySubPages();
                isAdd = true;
                sub.ID = Guid.NewGuid();
                sub.AppLibraryID = Request.QueryString["id"].ToGuid();
            }
            sub.Name = Title.Trim1();
            sub.Address = Address.Trim1();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (isAdd)
                {
                    Sub.Add(sub);
                    RoadFlow.Platform.Log.Add("添加了子页面", sub.Serialize(), RoadFlow.Platform.Log.Types.菜单权限);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');window.location='SubPages.aspx" + Request.Url.Query + "';", true);
                }
                else
                {
                    Sub.Update(sub);
                    RoadFlow.Platform.Log.Add("修改了子页面", sub.Serialize(), RoadFlow.Platform.Log.Types.菜单权限);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='SubPages.aspx" + Request.Url.Query + "';", true);
                }

                RoadFlow.Platform.AppLibraryButtons1 Sub1 = new RoadFlow.Platform.AppLibraryButtons1();
                string buttonindex = Request.Form["buttonindex"] ?? "";
                var buttons = Sub1.GetAllByAppID(sub.ID);
                List<RoadFlow.Data.Model.AppLibraryButtons1> buttons1 = new List<RoadFlow.Data.Model.AppLibraryButtons1>();
                foreach (var index in buttonindex.Split(','))
                {
                    string button_ = Request.Form["button_" + index];
                    string buttonname_ = Request.Form["buttonname_" + index];
                    string buttonevents_ = Request.Form["buttonevents_" + index];
                    string buttonico_ = Request.Form["buttonico_" + index];
                    string showtype_ = Request.Form["showtype_" + index];
                    string buttonsort_ = Request.Form["buttonsort_" + index];
                    if (buttonname_.IsNullOrEmpty() || buttonevents_.IsNullOrEmpty())
                    {
                        continue;
                    }
                    RoadFlow.Data.Model.AppLibraryButtons1 sub1 = buttons.Find(p => p.ID == index.ToGuid());
                    bool isAdd1 = false;
                    if (sub1 == null)
                    {
                        isAdd1 = true;
                        sub1 = new RoadFlow.Data.Model.AppLibraryButtons1();
                        sub1.ID = Guid.NewGuid();
                    }
                    else
                    {
                        buttons1.Add(sub1);
                    }
                    sub1.AppLibraryID = sub.ID;
                    if (button_.IsGuid())
                    {
                        sub1.ButtonID = button_.ToGuid();
                    }
                    sub1.Events = buttonevents_;
                    sub1.Ico = buttonico_;
                    sub1.Name = buttonname_.Trim1();
                    sub1.Sort = buttonsort_.ToInt(0);
                    sub1.ShowType = showtype_.ToInt(0);
                    sub1.Type = 0;
                    if (isAdd1)
                    {
                        Sub1.Add(sub1);
                    }
                    else
                    {
                        Sub1.Update(sub1);
                    }
                }
                foreach (var sub1 in buttons)
                {
                    if (buttons1.Find(p => p.ID == sub1.ID) == null)
                    {
                        Sub1.Delete(sub1.ID);
                    }
                }
                scope.Complete();
                Sub1.ClearCache();
                Sub.ClearCache();
            }
        }
    }
}