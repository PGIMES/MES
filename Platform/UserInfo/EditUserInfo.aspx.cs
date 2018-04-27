using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserInfo
{
    public partial class EditUserInfo : Common.BasePage
    {
        protected RoadFlow.Platform.Users bui = new RoadFlow.Platform.Users();
        protected RoadFlow.Data.Model.Users ui = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ui = bui.Get(CurrentUserID);
            if (!IsPostBack)
            {
                if (ui != null)
                {
                    this.Tel.Value = ui.Tel;
                    this.MobilePhone.Value = ui.Mobile;
                    this.Fax.Value = ui.Fax;
                    this.Email.Value = ui.Email;
                    this.QQ.Value = ui.QQ;
                    this.OtherTel.Value = ui.OtherTel;
                    this.Note.Value = ui.Note;
                    this.WeiXin.Value = ui.WeiXin;
                }
            }
        }

        protected override bool CheckApp()
        {
            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Tel = Request.Form["Tel"];
            string MobilePhone = Request.Form["MobilePhone"];
            string WeiXin = Request.Form["WeiXin"];
            string Email = Request.Form["Email"];
            string QQ = Request.Form["QQ"];
            string OtherTel = Request.Form["OtherTel"];
            string Note = Request.Form["Note"];

            string oldXML=ui.Serialize();
            ui.Tel = Tel;
            ui.Mobile = MobilePhone;
            ui.Email = Email;
            ui.QQ = QQ;
            ui.OtherTel = OtherTel;
            ui.WeiXin = WeiXin;
            ui.Note = Note;
            bui.Update(ui);
            RoadFlow.Platform.Log.Add("修改了个人信息-" + ui.Name, ui.Serialize(), RoadFlow.Platform.Log.Types.组织机构, oldXML);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ok", "alert('保存成功!');window.location=window.location;", true);
        }

    }
}