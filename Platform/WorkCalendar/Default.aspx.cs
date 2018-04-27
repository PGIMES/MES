using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WebForm.Platform.WorkCalendar
{
    public partial class Default : Common.BasePage
    {
        protected RoadFlow.Platform.WorkCalendar BCal = new RoadFlow.Platform.WorkCalendar();
        protected int Year1 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Year1 = Request.Form["DropDownList1"].IsNullOrEmpty() ? RoadFlow.Utility.DateTimeNew.Now.Year : Request.Form["DropDownList1"].ToInt();
            if (!IsPostBack)
            {
                for (int i = 2016; i < 2099; i++)
                {
                    this.DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()) { Selected = i == Year1 });
                }
            }
            else
            {
                for (int i = 2016; i < 2099; i++)
                {
                    this.DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()) { Selected = i == Year1 });
                }
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string workdate = Request.Form["workdate"] ?? "";
            string year1 = Request.Form["year1"];
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                BCal.Delete(year1.ToInt());
                foreach (string wk in workdate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                {
                    if (wk.IsDateTime())
                    {
                        var cal = new RoadFlow.Data.Model.WorkCalendar();
                        cal.WorkDate = wk.ToDateTime();
                        BCal.Add(cal);
                    }
                }
                scope.Complete();
            }
            RoadFlow.Platform.Log.Add("设置了工作日历", workdate, RoadFlow.Platform.Log.Types.系统管理);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!')", true);
        }


    }
}