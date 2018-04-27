using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class UserAdd : Common.BasePage
    {
        RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
        RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        string id = string.Empty;
        Guid parentID;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Account.Attributes.Add("validate_url", "CheckAccount.ashx");
            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", "0", "validate=\"radio\"");
                this.SexRadios.Text = borganize.GetSexRadio("Sex", "", "validate=\"radio\"");
            }
            this.Button1.ClickDisabled();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (id.IsGuid(out parentID))
            {
                string name = Request.Form["Name"];
                string account = Request.Form["Account"];
                string status = Request.Form["Status"];
                string note = Request.Form["Note"];
                string Tel = Request.Form["Tel"];
                string Mobile = Request.Form["Mobile"];
                string WeiXin = Request.Form["WeiXin"];
                string Email = Request.Form["Email"];
                string Fax = Request.Form["Fax"];
                string QQ = Request.Form["QQ"];
                string OtherTel = Request.Form["OtherTel"];
                string Sex = Request.Form["Sex"];

                Guid userID = Guid.NewGuid();
                string userXML = string.Empty;
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //添加人员
                    RoadFlow.Data.Model.Users user = new RoadFlow.Data.Model.Users();
                    user.Account = account.Trim();
                    user.Name = name.Trim();
                    user.Note = note.IsNullOrEmpty() ? null : note;
                    user.Password = busers.GetUserEncryptionPassword(userID.ToString(), busers.GetInitPassword());
                    user.Sort = 1;
                    user.Status = status.IsInt() ? status.ToInt() : 0;
                    user.ID = userID;
                    user.Tel = Tel.Trim1(); 
                    user.Mobile = Mobile.Trim1(); 
                    user.WeiXin = WeiXin.Trim1(); 
                    user.WeiXin = WeiXin.Trim1(); 
                    user.Email = Email.Trim1(); 
                    user.Fax = Fax.Trim1(); 
                    user.QQ = QQ.Trim1(); 
                    user.OtherTel = OtherTel.Trim1();
                    if (Sex.IsInt())
                    {
                        user.Sex = Sex.ToInt();
                    }
                    busers.Add(user);

                    //添加关系
                    RoadFlow.Data.Model.UsersRelation userRelation = new RoadFlow.Data.Model.UsersRelation();
                    userRelation.IsMain = 1;
                    userRelation.OrganizeID = parentID;
                    userRelation.Sort = new RoadFlow.Platform.UsersRelation().GetMaxSort(parentID);
                    userRelation.UserID = userID;
                    new RoadFlow.Platform.UsersRelation().Add(userRelation);

                    //更新父级[ChildsLength]字段
                    borganize.UpdateChildsLength(parentID);

                    //添加微信
                    if (RoadFlow.Platform.WeiXin.Config.IsUse)
                    {
                        new RoadFlow.Platform.WeiXin.Organize().AddUserAsync(user);
                    }

                    userXML = user.Serialize();
                    scope.Complete();
                }
                new RoadFlow.Platform.MenuUser().ClearCache();
                new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
                RoadFlow.Platform.Log.Add("添加了人员", userXML, RoadFlow.Platform.Log.Types.组织机构);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');parent.frames[0].reLoad('" + id + "');window.location=window.location;", true);
            }
        }
    }
}