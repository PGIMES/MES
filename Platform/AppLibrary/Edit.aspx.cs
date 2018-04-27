using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.AppLibrary
{
    public partial class Edit : Common.BasePage
    {
        RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
        protected RoadFlow.Data.Model.AppLibrary appLibrary = null;
        RoadFlow.Platform.AppLibraryButtons AppButton = new RoadFlow.Platform.AppLibraryButtons();
        protected string buttonOptions = string.Empty;
        protected string buttonJson = string.Empty;
        protected string buttonShowTypeOptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            buttonOptions = AppButton.GetOptions();
            buttonJson = AppButton.GetAllJson();
            buttonShowTypeOptions = AppButton.GetShowTypeOptions();
            string editID = Request.QueryString["id"];
            string type = Request.QueryString["typeid"];
            if (editID.IsGuid())
            {
                appLibrary = bappLibrary.Get(editID.ToGuid());
            }
            if (!IsPostBack)
            {
                if (appLibrary != null)
                {
                    this.Title1.Value = appLibrary.Title;
                    this.Address.Value = appLibrary.Address;
                    this.TypeOptions.Text = new RoadFlow.Platform.AppLibrary().GetTypeOptions(appLibrary.Type.ToString());
                    this.OpenModelOptions.Text = new RoadFlow.Platform.Dictionary().GetOptionsByCode("appopenmodel", value: appLibrary.OpenMode.ToString());
                    this.Params.Value = appLibrary.Params;
                    this.Width.Value = appLibrary.Width.ToString();
                    this.Height.Value = appLibrary.Height.ToString();
                    this.Note.Value = appLibrary.Note;
                    this.Ico.Value = appLibrary.Ico;
                }
                else
                {
                    this.TypeOptions.Text = new RoadFlow.Platform.AppLibrary().GetTypeOptions(Request.QueryString["typeid"]);
                    this.OpenModelOptions.Text = new RoadFlow.Platform.Dictionary().GetOptionsByCode("appopenmodel");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string title = Request.Form["title1"];
            string address = Request.Form["address"];
            string openModel = Request.Form["openModel"];
            string width = Request.Form["width"];
            string height = Request.Form["height"];
            string params1 = Request.Form["Params"];
            string note = Request.Form["note"];
            string Ico = Request.Form["Ico"];
            string type = Request.Form["type"];

            bool isAdd = false;
            string oldXML = string.Empty;
            if (appLibrary == null)
            {
                isAdd = true;
                appLibrary = new RoadFlow.Data.Model.AppLibrary();
                appLibrary.ID = Guid.NewGuid();
            }
            else
            {
                oldXML = appLibrary.Serialize();
            }
            appLibrary.Address = address.Trim();
            appLibrary.Height = height.ToIntOrNull();
            appLibrary.Note = note;
            appLibrary.OpenMode = openModel.ToInt();
            appLibrary.Params = params1;
            appLibrary.Title = title;
            appLibrary.Type = type.ToGuid();
            appLibrary.Width = width.ToIntOrNull();
            if (Ico.IsNullOrEmpty())
            {
                appLibrary.Ico = null;
            }
            else
            {
                appLibrary.Ico = Ico;
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (isAdd)
                {
                    bappLibrary.Add(appLibrary);
                    RoadFlow.Platform.Log.Add("添加了应用程序库", appLibrary.Serialize(), RoadFlow.Platform.Log.Types.菜单权限);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
                else
                {
                    bappLibrary.Update(appLibrary);
                    RoadFlow.Platform.Log.Add("修改了应用程序库", "", RoadFlow.Platform.Log.Types.菜单权限, oldXML, appLibrary.Serialize());
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('修改成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }

                RoadFlow.Platform.AppLibraryButtons1 Sub1 = new RoadFlow.Platform.AppLibraryButtons1();
                string buttonindex = Request.Form["buttonindex"] ?? "";
                var buttons = Sub1.GetAllByAppID(appLibrary.ID);
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
                    sub1.AppLibraryID = appLibrary.ID;
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
                foreach (var sub in buttons)
                {
                    if (buttons1.Find(p => p.ID == sub.ID) == null)
                    {
                        Sub1.Delete(sub.ID);
                    }
                }
                scope.Complete();
                Sub1.ClearCache();
            }
            new RoadFlow.Platform.Menu().ClearAllDataTableCache();
            new RoadFlow.Platform.WorkFlow().ClearStartFlowsCache();
            bappLibrary.ClearCache();
        }
    }
}