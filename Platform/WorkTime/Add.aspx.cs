using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkTime
{
    public partial class Add : Common.BasePage
    {
        protected RoadFlow.Platform.WorkTime BWorkTime = new RoadFlow.Platform.WorkTime();
        protected RoadFlow.Data.Model.WorkTime MWorkTime = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                MWorkTime = BWorkTime.Get(id.ToGuid());
            }
            if (!IsPostBack)
            {
                int am1_h = 0;
                int am1_m = 0;
                int am2_h = 0;
                int am2_m = 0;
                int pm1_h = 0;
                int pm1_m = 0;
                int pm2_h = 0;
                int pm2_m = 0;
                if (MWorkTime != null)
                {
                    this.Date1.Value = MWorkTime.Date1.ToDateString();
                    this.Date2.Value = MWorkTime.Date2.ToDateString();
                    string[] am1 = MWorkTime.AmTime1.Split(':');
                    if (am1.Length > 0)
                    {
                        am1_h = am1[0].ToInt();
                    }
                    if (am1.Length > 1)
                    {
                        am1_m = am1[1].ToInt();
                    }
                    string[] am2 = MWorkTime.AmTime2.Split(':');
                    if (am2.Length > 0)
                    {
                        am2_h = am2[0].ToInt();
                    }
                    if (am2.Length > 1)
                    {
                        am2_m = am2[1].ToInt();
                    }

                    string[] pm1 = MWorkTime.PmTime1.Split(':');
                    if (pm1.Length > 0)
                    {
                        pm1_h = pm1[0].ToInt();
                    }
                    if (pm1.Length > 1)
                    {
                        pm1_m = pm1[1].ToInt();
                    }
                    string[] pm2 = MWorkTime.PmTime2.Split(':');
                    if (pm2.Length > 0)
                    {
                        pm2_h = pm2[0].ToInt();
                    }
                    if (pm2.Length > 1)
                    {
                        pm2_m = pm2[1].ToInt();
                    }
                }

                this.AmTime1_H.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 12, am1_h));
                this.AmTime1_M.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 59, am1_m));
                this.AmTime2_H.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 12, am2_h));
                this.AmTime2_M.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 59, am2_m));

                this.PmTime1_H.Items.AddRange(BWorkTime.GetNumberOptionsItems(12, 24, pm1_h));
                this.PmTime1_M.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 59, pm1_m));
                this.PmTime2_H.Items.AddRange(BWorkTime.GetNumberOptionsItems(12, 24, pm2_h));
                this.PmTime2_M.Items.AddRange(BWorkTime.GetNumberOptionsItems(0, 59, pm2_m));

                this.Year1.Value = Request.QueryString["year"];
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string year = Request.Form["Year1"];
            string date1 = Request.Form["Date1"];
            string date2 = Request.Form["Date2"];
            string AmTime1 = Request.Form["AmTime1_H"] + ":" + Request.Form["AmTime1_M"];
            string AmTime2 = Request.Form["AmTime2_H"] + ":" + Request.Form["AmTime2_M"];
            string PmTime1 = Request.Form["PmTime1_H"] + ":" + Request.Form["PmTime1_M"];
            string PmTime2 = Request.Form["PmTime2_H"] + ":" + Request.Form["PmTime2_M"];

            bool isAdd = false;
            string oldXML = string.Empty;
            if (MWorkTime == null)
            {
                MWorkTime = new RoadFlow.Data.Model.WorkTime();
                MWorkTime.ID = Guid.NewGuid();
                MWorkTime.Year = year.ToInt();
                isAdd = true;
            }
            else
            {
                oldXML = MWorkTime.Serialize();
            }

            MWorkTime.AmTime1 = AmTime1;
            MWorkTime.AmTime2 = AmTime2;
            MWorkTime.Date1 = date1.ToDateTime();
            MWorkTime.Date2 = date2.ToDateTime();
            MWorkTime.PmTime1 = PmTime1;
            MWorkTime.PmTime2 = PmTime2;

            if (isAdd)
            {
                BWorkTime.Add(MWorkTime);
                RoadFlow.Platform.Log.Add("添加了工作时间", MWorkTime.Serialize(), RoadFlow.Platform.Log.Types.系统管理);
            }
            else
            {
                BWorkTime.Update(MWorkTime);
                RoadFlow.Platform.Log.Add("修改了工作时间", MWorkTime.Serialize(), RoadFlow.Platform.Log.Types.系统管理, oldXML);
            }
            BWorkTime.ClearYearCache(MWorkTime.Year);
            var refreshUrl = "Default.aspx?appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&DropDownList_Year=" + year.ToString();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功');new RoadUI.Window().reloadOpener('" + refreshUrl + "');new RoadUI.Window().close();", true);
        }
    }
}