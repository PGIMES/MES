using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Menu
{
    public partial class ButtionsEdit : Common.BasePage
    {
        RoadFlow.Data.Model.AppLibraryButtons but = null;
        RoadFlow.Platform.AppLibraryButtons But = new RoadFlow.Platform.AppLibraryButtons();
        protected void Page_Load(object sender, EventArgs e)
        {
            string butid = Request.QueryString["butid"];
            if (butid.IsGuid())
            {
                but = But.Get(butid.ToGuid());
            }
            if (!IsPostBack)
            {
                if (but != null)
                {
                    this.Name.Value = but.Name;
                    this.Events.Value = but.Events;
                    this.Ico.Value = but.Ico;
                    this.Note.Value = but.Note;
                    this.Sort.Value = but.Sort.ToString();
                }
                else
                {
                    this.Sort.Value = But.GetMaxSort().ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Name = Request.Form["Name"];
            string Events = Request.Form["Events"];
            string Ico = Request.Form["Ico"];
            string Note = Request.Form["Note"];
            string Sort = Request.Form["Sort"];

            bool isAdd = false;
            string oldxml = string.Empty;
            if (but == null)
            {
                isAdd = true;
                but = new RoadFlow.Data.Model.AppLibraryButtons();
                but.ID = Guid.NewGuid();
            }
            else
            {
                oldxml = but.Serialize();
            }
            but.Name = Name.Trim1();
            but.Events = Events;
            but.Ico = Ico;
            but.Note = Note;
            but.Sort = Sort.ToInt();

            if (isAdd)
            {
                But.Add(but);
                RoadFlow.Platform.Log.Add("添加了按钮", but.Serialize(), RoadFlow.Platform.Log.Types.系统管理);
            }
            else
            {
                But.Update(but);
                RoadFlow.Platform.Log.Add("修改了按钮", but.Serialize(), RoadFlow.Platform.Log.Types.系统管理, oldxml);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
        }
    }
}