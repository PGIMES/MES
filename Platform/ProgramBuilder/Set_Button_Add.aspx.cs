using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Button_Add : Common.BasePage
    {
        protected RoadFlow.Platform.ProgramBuilderButtons PBB = new RoadFlow.Platform.ProgramBuilderButtons();
        protected RoadFlow.Data.Model.ProgramBuilderButtons pbb = null;
        protected string bid = string.Empty;
        protected string pid = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            bid = Request.QueryString["bid"];
            pid = Request.QueryString["pid"];
            string maxSort = Request.QueryString["maxSort"];
            this.Sort.Value = (maxSort.ToInt() + 5).ToString();
            if (bid.IsGuid())
            {
                pbb = PBB.Get(bid.ToGuid());
            }
            if (!IsPostBack)
            {
                if (pbb != null)
                {
                    this.buttonname.Value = pbb.ButtonName;
                    this.ClientScript.Value = pbb.ClientScript;
                    this.Sort.Value = pbb.Sort.ToString();
                    this.Ico.Value = pbb.Ico;
                    
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string buttonname = Request.Form["buttonname"];
            string ClientScript = Request.Form["ClientScript"];
            string Sort = Request.Form["Sort"];
            string Ico = Request.Form["Ico"];
            string showtype = Request.Form["showtype"];
            string buttonid = Request.Form["buttonid"];
            string IsValidateShow = Request.Form["IsValidateShow"];

            bool isAdd = false;
            if (pbb == null)
            {
                isAdd = true;
                pbb = new RoadFlow.Data.Model.ProgramBuilderButtons();
                pbb.ID = Guid.NewGuid();
                pbb.ProgramID = pid.ToGuid();
            }
            pbb.ButtonName = buttonname;
            pbb.ClientScript = ClientScript;
            pbb.Sort = Sort.ToInt(0);
            pbb.IsValidateShow = IsValidateShow.ToInt(1);
            if (Ico.IsNullOrEmpty())
            {
                pbb.Ico = null;
            }
            else
            {
                pbb.Ico = Ico;
            }
            if (buttonid.IsGuid())
            {
                pbb.ButtonID = buttonid.ToGuid();
            }
            else
            {
                pbb.ButtonID = null;
            }
            pbb.ShowType = showtype.ToInt();

            if (isAdd)
            {
                PBB.Add(pbb);
            }
            else
            {
                PBB.Update(pbb);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='Set_Button.aspx" + Request.Url.Query + "';", true);
        }
    }
}