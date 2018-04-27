using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Menu
{
    public partial class Buttons : Common.BasePage
    {
        protected string Query1 = string.Empty;
        protected string title = string.Empty;
        protected List<RoadFlow.Data.Model.AppLibraryButtons> ButtonList = new List<RoadFlow.Data.Model.AppLibraryButtons>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                title = Request.QueryString["title"];
                InitData();
            }
        }

        protected void InitData()
        {
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&title=" + title.UrlEncode();
            Query1 = query + "&pagesize=" + Request.QueryString["pagesize"] + "&pagenumber=" + Request.QueryString["pagenumber"];
            string pager;
            ButtonList = new RoadFlow.Platform.AppLibraryButtons().GetPagerData(out pager, query, title).OrderBy(p => p.Sort).ToList();
            this.PagerText.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            title = this.Title1.Value;
            InitData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RoadFlow.Platform.AppLibraryButtons But = new RoadFlow.Platform.AppLibraryButtons();
            string[] idarray = (Request.Form["checkbox_app"] ?? "").Split(',');
            foreach (var id in idarray)
            {
                var but = But.Get(id.ToGuid());
                if (but != null)
                {
                    But.Delete(but.ID);
                    RoadFlow.Platform.Log.Add("删除了按钮", but.Serialize(), RoadFlow.Platform.Log.Types.系统管理);
                }
            }
            InitData();
        }
    }
}