using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Add : Common.BasePage
    {
        protected string Query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"] + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"];
        }

    }
}