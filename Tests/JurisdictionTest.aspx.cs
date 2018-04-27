using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Tests
{
    public partial class JurisdictionTest : Common.BasePage
    {
        protected string query = string.Empty;
        protected string s_title = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_title = Request.QueryString["s_title"];
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_title = this.Title1.Value;
        }


    }
}