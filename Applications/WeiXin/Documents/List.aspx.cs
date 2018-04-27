using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.WeiXin.Documents
{
    public partial class List : System.Web.UI.Page
    {
        protected string searchText = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                searchText = Request.QueryString["searchkey"];
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            searchText = Request.Form["searchkey"];
        }
    }
}