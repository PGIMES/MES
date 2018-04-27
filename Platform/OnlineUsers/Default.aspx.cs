using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.OnlineUsers
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.OnlineUsers> UserList = new List<RoadFlow.Data.Model.OnlineUsers>();
        RoadFlow.Platform.OnlineUsers bou = new RoadFlow.Platform.OnlineUsers();
        string s_Name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Name = Request.QueryString["Name"];
                this.Name.Value = s_Name;
                initData();
            }
        }

        private void initData()
        {
            UserList = bou.GetAll();
            this.Count.Text = UserList.Count.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_Name = Request.Form["Name"];
            initData();
            if (!s_Name.IsNullOrEmpty())
            {
                UserList = UserList.Where(p => p.UserName.IndexOf(s_Name) >= 0).ToList();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            bou.RemoveAll();
            initData();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string userids = Request.Form["checkbox_app"];
            if (!userids.IsNullOrEmpty())
            {
                foreach (string userid in userids.Split(','))
                {
                    Guid uid;
                    if (userid.IsGuid(out uid))
                    {
                        bou.Remove(uid);
                    }
                }
            }
            initData();
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}