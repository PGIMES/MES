using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls.UploadFiles
{
    public partial class Default : Common.BasePage
    {
        protected string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = "&eid=" + Request.QueryString["eid"] + "&files=" + Request.QueryString["files"] + "&filetype=" + Request.QueryString["filetype"] + "&iframeid=" + Request.QueryString["iframeid"];
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}