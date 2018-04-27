using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.Documents
{
    public partial class DocAdd : Common.BasePage
    {
        protected string DirID = string.Empty;
        protected RoadFlow.Platform.DocumentDirectory DocDir = new RoadFlow.Platform.DocumentDirectory();
        protected RoadFlow.Data.Model.Documents doc = null;
        protected RoadFlow.Platform.Documents DOC = new RoadFlow.Platform.Documents();
        protected RoadFlow.Platform.DocumentsReadUsers DocReadUsers = new RoadFlow.Platform.DocumentsReadUsers();
        protected void Page_Load(object sender, EventArgs e)
        {
            DirID = Request.QueryString["DirID"];
            string DocID = Request.QueryString["DocID"];
            if (DocID.IsGuid())
            {
                doc = DOC.Get(DocID.ToGuid());
            }
            if (!IsPostBack)
            {
                if (doc != null)
                {
                    this.Title1.Value = doc.Title;
                    this.ReadUsers.Value = doc.ReadUsers;
                    this.Contents.Value = doc.Contents;
                    this.Files.Value = doc.Files;
                    this.Source.Value = doc.Source;
                }
                else
                {
                    this.Source.Value = CurrentUserDeptName;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string Title1 = this.Title1.Value;
            string ReadUsers = Request.Form["ReadUsers"];
            string Contents = Request.Form["Contents"];
            string Files = Request.Form["Files"];
            string Source = Request.Form["Source"];

            bool isAdd = false;
            string oldxml = string.Empty;
            if (doc == null)
            {
                doc = new RoadFlow.Data.Model.Documents();
                isAdd = true;
                doc.ID = Guid.NewGuid();
                doc.ReadCount = 0;
                doc.WriteTime = RoadFlow.Utility.DateTimeNew.Now;
                doc.WriteUserID = CurrentUserID;
                doc.WriteUserName = CurrentUserName;
            }
            else
            {
                oldxml = doc.Serialize();
                doc.EditTime = RoadFlow.Utility.DateTimeNew.Now;
                doc.EditUserID = CurrentUserID;
                doc.EditUserName = CurrentUserName;
            }
            doc.Contents = Contents;
            doc.DirectoryID = DirID.ToGuid();
            doc.Source = Source;
            doc.Files = Files;
            doc.ReadUsers = ReadUsers;
            doc.Title = Title1.Trim1();
            doc.DirectoryName = DocDir.GetName(doc.DirectoryID);

            if (isAdd)
            {
                DOC.Add(doc);
                RoadFlow.Platform.Log.Add("添加了文档", doc.Serialize(), RoadFlow.Platform.Log.Types.文档中心);
            }
            else
            {
                DOC.Update(doc);
                RoadFlow.Platform.Log.Add("修改了文档", doc.Serialize(), RoadFlow.Platform.Log.Types.文档中心, oldxml, doc.Serialize());
            }

            //更新阅读人员
            var users = doc.ReadUsers.IsNullOrEmpty() ? DocDir.GetReadUsers(doc.DirectoryID) : new RoadFlow.Platform.Organize().GetAllUsers(doc.ReadUsers);
            DocReadUsers.Delete(doc.ID);
            bool isWeiXin = RoadFlow.Platform.WeiXin.Config.IsUse;
            RoadFlow.Platform.WeiXin.Message weiXinMsg = new RoadFlow.Platform.WeiXin.Message();
            System.Text.StringBuilder wxUsers = new System.Text.StringBuilder();
            foreach (var user in users)
            {
                RoadFlow.Data.Model.DocumentsReadUsers docReadUsers = new RoadFlow.Data.Model.DocumentsReadUsers();
                docReadUsers.DocumentID = doc.ID;
                docReadUsers.IsRead = 0;
                docReadUsers.UserID = user.ID;
                DocReadUsers.Add(docReadUsers);
                if (isWeiXin)
                {
                    wxUsers.Append(user.Account);
                    wxUsers.Append('|');
                }
            }
            if (isWeiXin)
            { 
               weiXinMsg.SendText(doc.Title, wxUsers.ToString().TrimEnd('|'), agentid: new RoadFlow.Platform.WeiXin.Agents().GetAgentIDByCode("weixinagents_documents"), async: true);
            }
            string url = string.Empty;
            if (isAdd)
            {
                url = "'List.aspx" + Request.Url.Query+"'";
            }
            else
            {
                url = "'DocRead.aspx" + Request.Url.Query + "'";
            }

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location=" + url + ";", true);
        }
    }
}