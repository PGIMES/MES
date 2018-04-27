using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowArchives
{
    public partial class List : Common.BasePage
    {
        protected System.Data.DataTable Dt = new System.Data.DataTable();
        protected string query1 = string.Empty;
        protected string appid = string.Empty;
        protected string tabid = string.Empty;
        protected string typeid = string.Empty;
        RoadFlow.Platform.WorkFlowArchives BWFA = new RoadFlow.Platform.WorkFlowArchives();
        RoadFlow.Platform.WorkFlow BWF = new RoadFlow.Platform.WorkFlow();
        string s_Title1 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            appid = Request.QueryString["appid"];
            tabid = Request.QueryString["tabid"];
            typeid = Request.QueryString["typeid"];
            
            if (!IsPostBack)
            {
                s_Title1 = Request.QueryString["Title1"];
                this.Title1.Value = s_Title1;
                initData();
            }  
        }

        private void initData()
        {
            string query = string.Format("&appid={0}&tabid={1}&Title1={2}&typeid={3}&display=1",
                            appid,
                            tabid,
                            s_Title1.UrlEncode(), typeid
                            );
            query1 = string.Format("{0}&pagesize={1}&pagenumber={2}", query, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);
            string pager;
            Dt = BWFA.GetPagerData(out pager, query, s_Title1, BWF.GetFlowIDFromType(typeid.ToGuid()));
            this.Pager.Text = pager;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_Title1 = Request.Form["Title1"];

            initData();
        }
    }
}