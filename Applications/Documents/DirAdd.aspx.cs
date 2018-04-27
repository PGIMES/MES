using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.Documents
{
    public partial class DirAdd : Common.BasePage
    {
        protected string DirID = string.Empty;
        protected RoadFlow.Platform.DocumentDirectory DocDir = new RoadFlow.Platform.DocumentDirectory();
        protected RoadFlow.Data.Model.DocumentDirectory docDir = null;
        string currentDirID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            DirID = Request.QueryString["DirID"];
            currentDirID = Request.QueryString["currentDirID"];
            if (currentDirID.IsGuid())
            {
                docDir = DocDir.Get(currentDirID.ToGuid());
            }
            if (!IsPostBack)
            {
                if (docDir != null)
                {
                    this.Name.Value = docDir.Name;
                    this.ReadUsers.Value = docDir.ReadUsers;
                    this.PublishUsers.Value = docDir.PublishUsers;
                    this.ManageUsers.Value = docDir.ManageUsers;
                    this.Sort.Value = docDir.Sort.ToString();
                    DirID = docDir.ParentID.ToString();
                }
                else
                {
                    this.Sort.Value = DocDir.GetMaxSort(DirID.ToGuid()).ToString();
                    //this.PublishUsers.Value = "r_" + RoadFlow.Utility.Config.AdminRoleID;
                    //this.ManageUsers.Value = "r_" + RoadFlow.Utility.Config.AdminRoleID;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string Name = Request.Form["Name"];
            string ReadUsers = Request.Form["ReadUsers"];
            string PublishUsers = Request.Form["PublishUsers"];
            string ManageUsers = Request.Form["ManageUsers"];
            string Sort = Request.Form["Sort"];
            
            bool isAdd = false;
            string oldxml = string.Empty;
            if (docDir == null)
            {
                isAdd = true;
                docDir = new RoadFlow.Data.Model.DocumentDirectory();
                docDir.ID = Guid.NewGuid();
                docDir.ParentID = DirID.ToGuid();
            }
            else
            {
                oldxml = docDir.Serialize();
            }
            docDir.ManageUsers = ManageUsers;
            docDir.Name = Name.Trim1();
            docDir.PublishUsers = PublishUsers;
            docDir.ReadUsers = ReadUsers;
            docDir.Sort = Sort.ToInt();

            if (isAdd)
            {
                DocDir.Add(docDir);
                RoadFlow.Platform.Log.Add("添加了栏目", docDir.Serialize(), RoadFlow.Platform.Log.Types.文档中心);
            }
            else
            {
                DocDir.Update(docDir);
                RoadFlow.Platform.Log.Add("修改了栏目", docDir.Serialize(), RoadFlow.Platform.Log.Types.文档中心, oldxml, docDir.Serialize());
            }
            DocDir.ClearDirUsersCache(docDir.ID);
            DocDir.ClearCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + DirID + "');alert('保存成功!');window.location='List.aspx" + Request.Url.Query + "';", true);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (docDir == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('栏目为空!');window.location='List.aspx" + Request.Url.Query + "';", true);
                return;
            }
            if (docDir.ParentID == Guid.Empty)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('不能删除根栏目!');window.location=window.location;", true);
                return;
            }
            var childIdString = DocDir.GetAllChildIdString(docDir.ID);
            RoadFlow.Platform.Documents Doc = new RoadFlow.Platform.Documents();
            foreach (string id in childIdString.Split(','))
            {
                DocDir.Delete(id.ToGuid());
                Doc.DeleteByDirectoryID(id.ToGuid());
                DocDir.ClearDirUsersCache(id.ToGuid());
            }
            DocDir.ClearCache();
            RoadFlow.Platform.Log.Add("删除的文档栏目及其所有下级栏目", childIdString, RoadFlow.Platform.Log.Types.文档中心);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + docDir.ParentID + "');alert('删除成功!');window.location='List.aspx" + Request.Url.Query + "';", true);
        }
    }
}