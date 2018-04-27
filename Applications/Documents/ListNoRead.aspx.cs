using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebForm.Applications.Documents
{
    public partial class ListNoRead : Common.BasePage
    {
        protected string DirID = string.Empty;
        protected string Query = string.Empty;
        protected string Query1 = string.Empty;
        protected RoadFlow.Platform.Documents Doc = new RoadFlow.Platform.Documents();
        protected RoadFlow.Platform.DocumentDirectory DocDir = new RoadFlow.Platform.DocumentDirectory();
        protected DataTable DocDt = new DataTable();
        string stitle = string.Empty;
        string sdate1 = string.Empty;
        string sdate2 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            DirID = Request.QueryString["dirid"];

            if (!IsPostBack)
            {
                stitle = Request.QueryString["title1"];
                sdate1 = Request.QueryString["date1"];
                sdate2 = Request.QueryString["date2"];
                this.Title1.Value = stitle;
                this.Date1.Value = sdate1;
                this.Date2.Value = sdate2;
                InitData();
            }
        }

        protected void InitData()
        {
            Query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&dirid=" + Request.QueryString["dirid"]
                + "&title1=" + stitle.UrlEncode() + "&date1=" + sdate1 + "&date2=" + sdate2;
            Query1 = Query + "&pagesize=" + Request.QueryString["pagesize"] + "&pagenumber=" + Request.QueryString["pagenumber"];
            string pager;
            string dirIdString = "";
            if (DirID.IsGuid())
            {
                dirIdString = new RoadFlow.Platform.DocumentDirectory().GetAllChildIdString(DirID.ToGuid(), CurrentUserID);
            }
            DocDt = Doc.GetList(out pager, dirIdString, CurrentUserID.ToString(), Query, stitle, sdate1, sdate2, true);
            this.PagerText.Text = pager;
        }

        protected void Button_Query_Click(object sender, EventArgs e)
        {
            stitle = Request.Form["Title1"];
            sdate1 = Request.Form["Date1"];
            sdate2 = Request.Form["Date2"];

            InitData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["docbox"] ?? "";
            foreach (string id in ids.Split(','))
            {
                Doc.Delete(id.ToGuid());
            }
            InitData();
        }

        protected override bool CheckApp()
        {
            return true;
        }
    }
}