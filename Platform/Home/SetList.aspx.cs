using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Home
{
    public partial class SetList : Common.BasePage
    {
        protected RoadFlow.Platform.HomeItems BHI = new RoadFlow.Platform.HomeItems();
        protected RoadFlow.Platform.Organize BORG = new RoadFlow.Platform.Organize();
        protected List<RoadFlow.Data.Model.HomeItems> HIList = new List<RoadFlow.Data.Model.HomeItems>();
        private string s_Name1 = string.Empty;
        private string s_Title1 = string.Empty;
        private string s_Type = string.Empty;
        protected string Query = string.Empty;
        protected string Query1 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Name1 = Request.QueryString["Name1"];
                s_Title1 = Request.QueryString["Title1"];
                s_Type = Request.QueryString["Type"];
                
                initData();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_Name1 = Request.Form["Name1"];
            s_Title1 = Request.Form["Title1"];
            s_Type = Request.Form["Type"];
            initData();
        }

        private void initData()
        {
            Query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&Name1=" + s_Name1.UrlEncode() +
                "&Title1=" + s_Title1.UrlEncode() + "&Type=" + s_Type;
            Query1 = Query + "&pagesize=" + Request.QueryString["pagesize"] + "&pagenumber=" + Request.QueryString["pagenumber"];

            this.Name1.Value = s_Name1;
            this.Title1.Value = s_Title1;
            this.TypeOptions.Text = BHI.getTypeOptions(s_Type);
            string pager;
            HIList = BHI.GetList(out pager, Query, s_Name1, s_Title1, s_Type);
            this.PagerText.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["checkbox_app"] ?? "";
            foreach (string id in ids.Split(','))
            {
                BHI.Delete(id.ToGuid());
            }
            initData();
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "window.location=window.location;", true);
        }
    }
}