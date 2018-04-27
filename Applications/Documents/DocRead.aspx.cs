using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.Documents
{
    public partial class DocRead : Common.BasePage
    {
        protected string DocID = string.Empty;
        protected RoadFlow.Data.Model.Documents doc = null;
        protected RoadFlow.Platform.Documents Doc = new RoadFlow.Platform.Documents();
        protected RoadFlow.Platform.DocumentsReadUsers DocReadUsers = new RoadFlow.Platform.DocumentsReadUsers();
        protected bool IsEdit = false;//是否可以编辑文档
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginMsg;
            if (!WebForm.Common.Tools.CheckLogin(out loginMsg) && !RoadFlow.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.End();
            }

            DocID = Request.QueryString["docid"];

            if (DocID.IsGuid())
            {
                doc = Doc.Get(DocID.ToGuid());
                if (doc != null)
                {
                    var readusers = DocReadUsers.Get(doc.ID, CurrentUserID);
                    if (readusers == null)
                    {
                        Response.Write("您无权查看该文档!");
                        Response.End();
                    }
                    IsEdit = new RoadFlow.Platform.DocumentDirectory().HasPublish(doc.DirectoryID, CurrentUserID) ||
                        new RoadFlow.Platform.DocumentDirectory().HasManage(doc.DirectoryID, CurrentUserID);
                    if (!IsPostBack)
                    {
                        this.Title1.Text = doc.Title;
                        this.WriteTime.Text = doc.WriteTime.ToDateTimeString();
                        this.WriteUserName.Text = doc.WriteUserName;
                        this.Contents.Text = doc.Contents;
                        this.DirectoryID.Text = new RoadFlow.Platform.DocumentDirectory().GetName(doc.DirectoryID);
                        this.Source.Text = doc.Source;
                        if (!doc.Files.IsNullOrEmpty())
                        {
                            this.Files.Text = "附件：" + RoadFlow.Platform.Files.GetFilesShowString(doc.Files, "/Platform/Files/Show.ashx");
                        }
                        this.ReadCount.Text = (doc.ReadCount + 1).ToString();
                        if (doc.EditTime.HasValue)
                        {
                            this.EditTime.Text = "修改时间：" + doc.EditTime.Value.ToDateTimeString();
                        }
                        if (!doc.EditUserName.IsNullOrEmpty())
                        {
                            this.EditUserName.Text = "修改人：" + doc.EditUserName;
                        }

                        Doc.UpdateReadCount(doc.ID);
                        DocReadUsers.UpdateRead(doc.ID, CurrentUserID);
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                Doc.Delete(doc.ID);
                new RoadFlow.Platform.DocumentsReadUsers().Delete(doc.ID);
                RoadFlow.Platform.Log.Add("删除了文档", doc.Serialize(), RoadFlow.Platform.Log.Types.文档中心);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除成功');window.location='List.aspx" + Request.Url.Query + "'", true);
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;// base.CheckUrl(isEnd);
        }

        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true;// base.CheckLogin(isRedirect);
        }
    }
}