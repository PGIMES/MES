using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Applications.WeiXin.Documents
{
    public partial class Default : System.Web.UI.Page
    {
        protected RoadFlow.Platform.DocumentDirectory docDir = new RoadFlow.Platform.DocumentDirectory();
        protected RoadFlow.Platform.Documents bdoc = new RoadFlow.Platform.Documents();
        protected string searchTitle = string.Empty;
        protected Dictionary<Guid, string> dicts = new Dictionary<Guid, string>();
        protected Guid userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WeiXin.Organize.CheckLogin();
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            InitData();
        }

        protected void InitData()
        {
            userID = RoadFlow.Platform.Users.CurrentUserID;
            dicts = docDir.GetDirs(userID);
        }
    }
}