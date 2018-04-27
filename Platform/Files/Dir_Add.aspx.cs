using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebForm.Platform.Files
{
    public partial class Dir_Add : Common.BasePage
    {
        protected string dir = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            dir = (Request.QueryString["id"] ?? "").DesDecrypt();
            if (dir.IsNullOrEmpty() || !Directory.Exists(dir))
            {
                Response.Write("目录为空或不存在!");
                Response.End();
            }
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string dirName = this.DirName.Value.Trim1();
            bool isAdd = new RoadFlow.Platform.Files().CreateDirectory(dir, dirName);
            if (isAdd)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');parent.frames[0].reLoad('" + Request.QueryString["id"] + "');window.location='List.aspx" + Request.Url.Query + "';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加失败!')", true);
            }
        }
    }
}