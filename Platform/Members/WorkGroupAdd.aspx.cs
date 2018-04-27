using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class WorkGroupAdd : Common.BasePage
    {
        RoadFlow.Platform.WorkGroup bwg = new RoadFlow.Platform.WorkGroup();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = Request.Form["Name"];
            string members = Request.Form["Members"];
            string note = Request.Form["Note"];

            RoadFlow.Data.Model.WorkGroup wg = new RoadFlow.Data.Model.WorkGroup();
            wg.ID = Guid.NewGuid();
            wg.Name = name.Trim();
            wg.Members = members;
            wg.IntID = wg.ID.ToInt32();
            if (!note.IsNullOrEmpty())
            {
                wg.Note = note;
            }

            bwg.Add(wg);
            //更新微信标签
            if (RoadFlow.Platform.WeiXin.Config.IsUse)
            {
                new RoadFlow.Platform.WeiXin.Organize().AddGroupAsync(wg);
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            string query = "WorkGroup.aspx?id=" + wg.ID.ToString() + "&appid=" + Request.QueryString["appid"] + "&parentid=" + Request.QueryString["parentid"] + "&type=" + Request.QueryString["type"];
            RoadFlow.Platform.Log.Add("添加了工作组", wg.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].treecng('1');alert('添加成功!');window.location = '" + query + "';", true);
        }
    }
}