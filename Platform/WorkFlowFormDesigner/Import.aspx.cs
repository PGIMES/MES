using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    public partial class Import : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.FileName.IsNullOrEmpty())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('请选择要导入的文件!');", true);
                return;
            }
            string filePath = RoadFlow.Utility.Config.FilePath + "WorkFlowFormImportFiles\\" + Guid.NewGuid().ToString("N");
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string zipFile = filePath + "\\" + FileUpload1.FileName;
            FileUpload1.SaveAs(zipFile);
            string msg = new RoadFlow.Platform.WorkFlowForm().Import(zipFile, 0);
            if ("1" == msg)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('导入成功!');new RoadUI.Window().reloadOpener();", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('" + msg + "');new RoadUI.Window().reloadOpener();", true);
            }
        }
    }
}