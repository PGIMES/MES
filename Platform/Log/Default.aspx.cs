using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Log
{
    public partial class Default : Common.BasePage
    {
        protected string Query = string.Empty;
        protected System.Data.DataTable Dt = new System.Data.DataTable();
        string s_Title1 = string.Empty;
        string s_Type = string.Empty;
        string s_Date1 = string.Empty;
        string s_Date2 = string.Empty;
        string s_UserID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                s_Title1 = Request.QueryString["Title1"];
                s_Type = Request.QueryString["Type"];
                s_Date1 = Request.QueryString["Date1"];
                s_Date2 = Request.QueryString["Date2"];
                s_UserID = Request.QueryString["UserID"];

                this.Title1.Value = s_Title1;
                this.UserID.Value =  s_UserID;
                this.Date1.Value = s_Date1;
                this.Date2.Value = s_Date2;

                initData();
            }
        }

        private void initData()
        {
            RoadFlow.Platform.Log blog = new RoadFlow.Platform.Log();
            Query = string.Format("&appid={0}&tabid={1}&Title1={2}&Type={3}&Date1={4}&Date2={5}&UserID={6}",
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                s_Title1.UrlEncode(),
                s_Type.UrlEncode(),
                s_Date1,
                s_Date2,
                s_UserID
                );
            string pager;
            Dt = blog.GetPagerData(out pager, Query, s_Title1, s_Type, s_Date1, s_Date2, s_UserID);
            this.TypeOptions.Text = blog.GetTypeOptions(s_Type);
            this.Pager.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_Title1 = Request.Form["Title1"];
            s_Type = Request.Form["Type"];
            s_Date1 = Request.Form["Date1"];
            s_Date2 = Request.Form["Date2"];
            s_UserID = Request.Form["UserID"];
            initData();
        }
    }
}