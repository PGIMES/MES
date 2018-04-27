using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class List : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.ProgramBuilder> pbList = new List<RoadFlow.Data.Model.ProgramBuilder>();
        protected RoadFlow.Platform.ProgramBuilder PB = new RoadFlow.Platform.ProgramBuilder();
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected string typeid = string.Empty;
        protected string S_Name = string.Empty;
        protected string Query1 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            typeid = Request.QueryString["typeid"];
            if (!IsPostBack)
            {
                S_Name = Request.QueryString["Name1"];
                InitData();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            S_Name = Request.Form["Name1"];
            InitData();
        }

        protected void InitData()
        {
            Query1 = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + typeid + "&Name1=" + S_Name;
            string pager;
            pbList = PB.GetList(out pager, Query1, S_Name, typeid);
            this.PagerText.Text = pager;
            this.Name1.Value = S_Name;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string checkbox_app = Request.Form["checkbox_app"] ?? "";
            foreach (string id in checkbox_app.Split(','))
            { 
                if(!id.IsGuid()) continue;
                PB.DeleteAllSet(id.ToGuid());
                RoadFlow.Platform.Log.Add("删除的应用程序设计", id, RoadFlow.Platform.Log.Types.系统管理);
            }
            InitData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string checkbox_app = Request.Form["checkbox_app"] ?? "";
            int i = 0;
            foreach (string id in checkbox_app.Split(','))
            {
                if (!id.IsGuid()) continue;
                i += PB.Publish(id.ToGuid()) ? 1 : 0;
            }
            InitData();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('成功发布" + i.ToString() + "个应用!');", true);
        }
    }
}