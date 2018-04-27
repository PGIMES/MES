using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class User : Common.BasePage
    {
        RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
        RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        RoadFlow.Platform.UsersRelation buserRelation = new RoadFlow.Platform.UsersRelation();
        RoadFlow.Data.Model.Users user = null;
        RoadFlow.Data.Model.Organize organize = null;
        string id = string.Empty;
        string parentID = string.Empty;
        protected string query = string.Empty;
        Guid userID, organizeID;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            parentID = Request.QueryString["parentid"];
            string parentString = string.Empty;
            this.Account.Attributes.Add("validate_url", "CheckAccount.ashx?id=" + id);
            if (id.IsGuid(out userID))
            {
                user = busers.Get(userID);
                if (user != null)
                {
                    //所在组织字符串
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    var userRelations = buserRelation.GetAllByUserID(user.ID).OrderByDescending(p => p.IsMain);
                    foreach (var userRelation in userRelations)
                    {
                        sb.Append("<div style='margin:3px 0;'>");
                        sb.Append(borganize.GetAllParentNames(userRelation.OrganizeID, true));
                        if (userRelation.IsMain == 0)
                        {
                            sb.Append("<span style='color:#999'> [兼任]</span>");
                        }
                        sb.Append("</div>");

                    }
                    this.ParentString.Text = sb.ToString();

                    //所属角色
                    this.RoleString.Text = new RoadFlow.Platform.WorkGroup().GetAllNamesByUserID(user.ID);
                }
            }
            if (parentID.IsGuid(out organizeID))
            {
                organize = borganize.Get(organizeID);
            }
            if (user != null)
            {
                this.Name.Value = user.Name;
                this.Account.Value = user.Account;
                this.Note.Value = user.Note;
                this.Tel.Value = user.Tel;
                this.Mobile.Value = user.Mobile;
                this.WeiXin.Value = user.WeiXin;
                this.Email.Value = user.Email;
                this.Fax.Value = user.Fax;
                this.QQ.Value = user.QQ;
                this.OtherTel.Value = user.OtherTel;

            }
            this.StatusRadios.Text = borganize.GetStatusRadio("Status", user != null ? user.Status.ToString() : "", "validate=\"radio\"");
            this.SexRadios.Text = borganize.GetSexRadio("Sex", user != null ? user.Sex.ToString() : "", "validate=\"radio\"");

            query = "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"]
            + "&parentid=" + Request.QueryString["parentid"] + "&type=" + Request.QueryString["type"];
            this.Button1.ClickDisabled();
            this.Button2.ClickDisabled();
            this.Button3.ClickDisabled();
            this.Button4.ClickDisabled();
        }

        protected void Button1_Click(object sender, EventArgs e)
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

            string oldXML = user.Serialize();
            user.Name = name.Trim();
            user.Account = account.Trim();
            user.Status = status.ToInt(1);
            user.Note = note.IsNullOrEmpty() ? null : note.Trim();
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

            busers.Update(user);
            //更新微信
            if (RoadFlow.Platform.WeiXin.Config.IsUse)
            {
                new RoadFlow.Platform.WeiXin.Organize().EditUserAsync(user);
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            RoadFlow.Platform.Log.Add("修改了用户", "", RoadFlow.Platform.Log.Types.组织机构, oldXML, user.Serialize());
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');parent.frames[0].reLoad('" + parentID + "');window.location=window.location;", true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                var urs = buserRelation.GetAllByUserID(user.ID);
                busers.Delete(user.ID);
                buserRelation.DeleteByUserID(user.ID);
                //更新父级[ChildsLength]字段
                foreach (var ur in urs)
                {
                    borganize.UpdateChildsLength(ur.OrganizeID);
                }
                //删除微信
                if (RoadFlow.Platform.WeiXin.Config.IsUse)
                {
                    new RoadFlow.Platform.WeiXin.Organize().DeleteUserAsync(user.Account);
                }

                scope.Complete();
            }

            string refreshID = parentID;
            string url = string.Empty;
            var users = borganize.GetAllUsers(refreshID);
            if (users.Count > 0)
            {
                url = "User.aspx?id=" + users.Last().ID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + parentID;
            }
            else if (organize != null)
            {
                refreshID = organize.ParentID == Guid.Empty ? organize.ID.ToString() : organize.ParentID.ToString();
                url = "Body.aspx?id=" + parentID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + organize.ParentID;
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            RoadFlow.Platform.Log.Add("删除了用户", user.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除成功');parent.frames[0].reLoad('" + refreshID + "');window.location='" + url + "'", true);
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string moveto = Request.Form["movetostation"];
            string movetostationjz = Request.Form["movetostationjz"];
            Guid moveToID;
            if (moveto.IsGuid(out moveToID))
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    var us = buserRelation.GetAllByUserID(user.ID);
                    if ("1" != movetostationjz)
                    {
                        buserRelation.DeleteByUserID(user.ID);
                    }
                    RoadFlow.Data.Model.UsersRelation ur = new RoadFlow.Data.Model.UsersRelation();
                    ur.UserID = user.ID;
                    ur.OrganizeID = moveToID;
                    ur.IsMain = "1" == movetostationjz ? 0 : 1;
                    ur.Sort = buserRelation.GetMaxSort(moveToID);
                    buserRelation.Add(ur);

                    foreach (var u in us)
                    {
                        borganize.UpdateChildsLength(u.OrganizeID);
                    }

                    borganize.UpdateChildsLength(organizeID);
                    borganize.UpdateChildsLength(moveToID);
                    //更新微信
                    if (RoadFlow.Platform.WeiXin.Config.IsUse)
                    {
                        new RoadFlow.Platform.WeiXin.Organize().EditUserAsync(user);
                    }
                    scope.Complete();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('调动成功!');parent.frames[0].reLoad('" + parentID + "');parent.frames[0].reLoad('" + moveto + "');window.location=window.location;", true);
                }
                new RoadFlow.Platform.MenuUser().ClearCache();
                new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
                RoadFlow.Platform.Log.Add(("1" == movetostationjz ? "兼职" : "全职") + "调动了人员的岗位", "将人员调往岗位(" + moveto + ")", RoadFlow.Platform.Log.Types.组织机构);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string initpass = busers.GetInitPassword();
            busers.InitPassword(user.ID);
            RoadFlow.Platform.Log.Add("初始化了用户密码", user.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('密码已初始化为：" + initpass + "');window.location=window.location;", true);
        }
    }
}