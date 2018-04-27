using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    public partial class Open_List1 : Common.BasePage
    {
        protected string typeid = string.Empty;
        protected List<RoadFlow.Data.Model.WorkFlowForm> forms = new List<RoadFlow.Data.Model.WorkFlowForm>();
        protected RoadFlow.Platform.WorkFlowForm WFF = new RoadFlow.Platform.WorkFlowForm();
        protected string query = string.Empty;
        protected string formname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            typeid = Request.QueryString["typeid"];
            if (!IsPostBack)
            {
                formname = Request.QueryString["name"];
                initData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            formname = Request.Form["form_name"];
            initData();
        }

        protected void initData()
        {
            query = "&appid=" + Request.QueryString["appid"] + "&typeid=" + typeid + "&name=" + formname.UrlEncode();
            string pager;
            string typeids = "";
            if (typeid.IsGuid())
            {
                typeids = new RoadFlow.Platform.Dictionary().GetAllChildsIDString(typeid.ToGuid());
            }
            forms = WFF.GetPagerData(out pager, query, "", typeids, formname);
            this.PaerText.Text = pager;
        }
    }
}