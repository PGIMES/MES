using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebForm.Platform.Files
{
    public partial class List : Common.BasePage
    {
        protected string dir = string.Empty;
        protected string ParentDir = string.Empty;
        protected string RootDir = string.Empty;
        protected RoadFlow.Platform.Files BFiles = new RoadFlow.Platform.Files();
        protected List<RoadFlow.Data.Model.Files> FilesList = new List<RoadFlow.Data.Model.Files>();
        protected string Query = string.Empty;
        protected bool IsSelect = false;//是否是选择
        protected void Page_Load(object sender, EventArgs e)
        {
            IsSelect = "1" == Request.QueryString["isselect"];
            dir = (Request.QueryString["id"] ?? "").DesDecrypt();
            if (dir.IsNullOrEmpty() || !Directory.Exists(dir))
            {
                Response.Write("目录为空或不存在!");
                Response.End();
            }
            RootDir = BFiles.GetUserRootPath(CurrentUserID);
            if (!dir.Equals(RootDir, StringComparison.CurrentCultureIgnoreCase))
            {
                var parent = Directory.GetParent(dir);
                ParentDir = parent.FullName;
            }
            Query = "&appid=" + Request.QueryString["appid"] + "&isselect=" + Request.QueryString["isselect"] + "&eid=" + Request.QueryString["eid"] +
                "&files=" + Request.QueryString["files"] + "&filetype=" + Request.QueryString["filetype"] + "&iframeid=" + Request.QueryString["iframeid"];
            if (!IsPostBack)
            {
                InitList();
            }
        }

        protected void InitList()
        {
            FilesList = BFiles.GetList(dir);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string files = Request.Form["file"] ?? "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var file in files.Split(','))
            {
                sb.Append(BFiles.Delete(file.DesDecrypt()));
            }
            string refreshDir = ParentDir.IsNullOrEmpty() ? RootDir : ParentDir;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('" + (sb.Length == 0 ? "删除成功!" : sb.ToString()) + "');window.location='List.aspx?id=" + refreshDir.DesEncrypt() + Query + "';", true);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string refreshDir = ParentDir.IsNullOrEmpty() ? RootDir : ParentDir;
            string msg = BFiles.Delete(dir);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('" + (msg.IsNullOrEmpty() ? "删除成功!" : msg) + "');window.location='List.aspx?id=" + refreshDir.DesEncrypt() + Query + "';", true);
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}