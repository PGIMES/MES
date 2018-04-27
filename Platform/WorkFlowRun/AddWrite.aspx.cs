using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class AddWrite : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled();
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string addtype = Request.Form["addtype"];
            string writetype = Request.Form["writetype"];
            string writeuser = Request.Form["writeuser"];
            string note = Request.Form["note"];
            string taskid = Request.QueryString["taskid"];

            string msg;
            bool isSuccess = new RoadFlow.Platform.WorkFlowTask().AddWrite(taskid.ToGuid(), addtype.ToInt(), writetype.ToInt(), writeuser, note, out msg);
            string script = "alert('" + (isSuccess ? "加签成功!" : msg) + "');";
            if (addtype.ToInt() == 1)
            {
                script += "try{if(top.refreshPage){top.refreshPage();}if(top.mainTab){top.mainTab.closeTab();}else{top.close();}}catch(e){}";
            }
            script += "try{new RoadUI.Window().close();}catch(e){}";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script, true);
        }
    }
}