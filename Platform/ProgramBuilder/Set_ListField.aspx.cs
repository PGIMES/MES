using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_ListField : Common.BasePage
    {
        protected string query = string.Empty;
        protected List<RoadFlow.Data.Model.ProgramBuilderFields> fieldList = new List<RoadFlow.Data.Model.ProgramBuilderFields>();
        protected string pid = string.Empty;
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected string maxSort = string.Empty;
        protected RoadFlow.Platform.ProgramBuilderFields PBF = new RoadFlow.Platform.ProgramBuilderFields();
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["pid"];
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void InitData()
        {
            fieldList = PBF.GetAll(pid.ToGuid());
            maxSort = fieldList.Count > 0 ? fieldList.Max(p => p.Sort).ToString() : "0";
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] ids = (Request.Form["checkbox_app"] ?? "").Split(',');
            foreach (string id in ids)
            {
                PBF.Delete(id.ToGuid());
            }
            RoadFlow.Platform.Log.Add("删除了应用程序设计字段", pid, RoadFlow.Platform.Log.Types.其它分类);
            InitData();
        }
    }
}