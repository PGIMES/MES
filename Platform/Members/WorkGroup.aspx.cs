using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class WorkGroup : Common.BasePage
    {
        protected RoadFlow.Platform.WorkGroup bwg = new RoadFlow.Platform.WorkGroup();
        protected RoadFlow.Data.Model.WorkGroup wg = null;
        protected string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Guid wid;

            if (id.IsGuid(out wid))
            {
                wg = bwg.Get(wid);
            }
            if (wid.IsEmptyGuid())
            {
                Response.End();
            }
            if (!IsPostBack)
            {
                if (wg != null)
                {
                    this.Name.Value = wg.Name;
                    this.Members.Value = wg.Members;
                    this.Note.Value = wg.Note;
                    this.UsersText.Text = bwg.GetUsersNames(wg.Members, '、');
                }
            }

            query = "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"] + "&parentid=" + Request.QueryString["parentid"]+
                "&type=" + Request.QueryString["type"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (wg != null)
            {
                string oldmembers = wg.Members;
                string oldxml = wg.Serialize();
                string name = Request.Form["Name"];
                string members = Request.Form["Members"];
                string note = Request.Form["Note"];
                wg.Name = name.Trim();
                wg.Members = members;
                if (!note.IsNullOrEmpty())
                {
                    wg.Note = note;
                }
                else
                {
                    wg.Note = null;
                }
                wg.IntID = wg.ID.ToInt32();
                bwg.Update(wg);

                //更新微信标签
                if (RoadFlow.Platform.WeiXin.Config.IsUse)
                {
                    RoadFlow.Platform.WeiXin.Organize worg = new RoadFlow.Platform.WeiXin.Organize();
                    worg.EditGroupAsync(wg);
                    if (!oldmembers.Equals(wg.Members, StringComparison.CurrentCultureIgnoreCase))
                    {
                        worg.AddGroupUserAsync(wg);
                    }
                }
                
                new RoadFlow.Platform.MenuUser().ClearCache();
                new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
                string query = Request.Url.Query;
                RoadFlow.Platform.Log.Add("修改了工作组", "修改了工作组", RoadFlow.Platform.Log.Types.组织机构, oldxml, wg.Serialize());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "OK", "alert('保存成功!');window.location=window.location;", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (wg != null)
            {
                string oldxml = wg.Serialize();
                bwg.Delete(wg.ID);
                //同步微信删除
                if (RoadFlow.Platform.WeiXin.Config.IsUse)
                {
                    RoadFlow.Platform.WeiXin.Organize worg = new RoadFlow.Platform.WeiXin.Organize();
                    worg.DeleteGroupAsync(wg);
                }
                string query = Request.Url.Query;
                new RoadFlow.Platform.MenuUser().ClearCache();
                new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
                RoadFlow.Platform.Log.Add("删除了工作组", oldxml, RoadFlow.Platform.Log.Types.组织机构);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "OK", "window.location = 'Empty.aspx' + '" + query + "';alert('删除成功!');parent.frames[0].treecng('1');", true);
            }
        }
    }
}