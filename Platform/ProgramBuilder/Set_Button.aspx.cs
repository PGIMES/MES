using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Button : Common.BasePage
    {
        protected string query = string.Empty;
        protected string maxSort = string.Empty;
        protected List<RoadFlow.Data.Model.ProgramBuilderButtons> buttons = new List<RoadFlow.Data.Model.ProgramBuilderButtons>();
        protected RoadFlow.Platform.ProgramBuilderButtons PBB = new RoadFlow.Platform.ProgramBuilderButtons();
        string pid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["pid"];
            InitData();
        }

        protected void InitData()
        {
            buttons = PBB.GetAll(pid.ToGuid()).OrderBy(p => p.Sort).ToList();
            maxSort = buttons.Count > 0 ? buttons.Max(p => p.Sort).ToString() : "0";
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string checkbox_app = Request.Form["checkbox_app"] ?? "";
            foreach (string bid in checkbox_app.Split(','))
            {
                if (bid.IsGuid())
                {
                    PBB.Delete(bid.ToGuid());
                }
            }
            InitData();
        }
    }
}