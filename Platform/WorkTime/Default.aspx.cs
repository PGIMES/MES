using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkTime
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.WorkTime> List = new List<RoadFlow.Data.Model.WorkTime>();
        protected RoadFlow.Platform.WorkTime BWorkTime = new RoadFlow.Platform.WorkTime();
        protected int year = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DropDownList_Year.Items.Clear();
            if (!IsPostBack)
            {
                year = Request.QueryString["DropDownList_Year"].ToInt(0);
                initData();
            } 
        }

        protected void DropDownList_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Request.Form["DropDownList_Year"].ToInt(0);
            initData();
        }

        protected void initData()
        {
            if (year == 0)
            {
                year = RoadFlow.Utility.DateTimeNew.Now.Year;
            }
            this.DropDownList_Year.Items.AddRange(BWorkTime.GetAllYearOptionItems(year));
            List = BWorkTime.GetAll(year);
            this.Repeater1.DataSource = List.OrderBy(p=>p.Date1);
            this.Repeater1.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid delid = e.CommandArgument.ToString().ToGuid();
            if (!delid.IsEmptyGuid())
            {
                BWorkTime.Delete(delid);
                RoadFlow.Platform.Log.Add("删除了工作时间", delid.ToString(), RoadFlow.Platform.Log.Types.系统管理);
            }

            initData();
        }
    }
}