using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class Body : Common.BasePage
    {
        RoadFlow.Data.Model.Organize org = null;
        RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
        string id = string.Empty;
        protected string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled();
            this.Button2.ClickDisabled();
            this.Button3.ClickDisabled();
            this.Button4.ClickDisabled();
            id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                org = borganize.Get(id.ToGuid());
                
            }

            if (!RoadFlow.Platform.WeiXin.Config.IsUse || id.ToGuid() != borganize.GetRoot().ID)
            {
                this.Button4.Visible = false;
            }

            if (!IsPostBack)
            {
                initData();
            }
            query = "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"]
            + "&parentid=" + Request.QueryString["parentid"] + "&type=" + Request.QueryString["type"];
        }

        private void initData()
        {
            if (org != null)
            {
                this.Name.Value = org.Name;
                this.TypeRadios.Text = borganize.GetTypeRadio("Type", org.Type.ToString(), "validate=\"radio\"");
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", org.Status.ToString(), "validate=\"radio\"");
                this.ChargeLeader.Value = org.ChargeLeader;
                this.Leader.Value = org.Leader;
                this.Note.Value = org.Note;
            }
            else
            {
                this.TypeRadios.Text = borganize.GetTypeRadio("Type", "", "validate=\"radio\"");
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", "", "validate=\"radio\"");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = Request.Form["Name"];
            string type = Request.Form["Type"];
            string status = Request.Form["Status"];
            string chargeLeader = Request.Form["ChargeLeader"];
            string leader = Request.Form["Leader"];
            string note = Request.Form["note"];
            string oldXML = org.Serialize();
            org.Name = name.Trim();
            org.Type = type.ToInt(1);
            org.Status = status.ToInt(0);
            org.ChargeLeader = chargeLeader;
            org.Leader = leader;
            org.Note = note.IsNullOrEmpty() ? null : note.Trim();

            borganize.Update(org);
            //同步修改微信企业号
            if (RoadFlow.Platform.WeiXin.Config.IsUse)
            {
                new RoadFlow.Platform.WeiXin.Organize().EditDeptAsync(org);
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            RoadFlow.Platform.Log.Add("修改了组织机构", "", RoadFlow.Platform.Log.Types.组织机构, oldXML, org.Serialize());
            string rid = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');parent.frames[0].reLoad('" + rid + "');", true);
            initData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (org != null)
            {
                int i = borganize.DeleteAndAllChilds(org.ID);
                RoadFlow.Platform.Log.Add("删除了组织机构及其所有下级共" + i.ToString() + "项", org.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
                string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('共删除了" + i.ToString() + "项!');parent.frames[0].reLoad('" + refreshID + "');"
                    + "window.location='Body.aspx?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&parentid=" + Request.QueryString["parentid"] + "&type="
                    + Request.QueryString["type"] + "';", true);
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
            initData();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string toOrgID = Request.Form["deptmove"];
            Guid toID;
            if (toOrgID.IsGuid(out toID) && borganize.Move(org.ID, toID))
            {
                new RoadFlow.Platform.MenuUser().ClearCache();
                new RoadFlow.Platform.HomeItems().ClearCache();//清除首页缓存
                RoadFlow.Platform.Log.Add("移动了组织机构", "将机构：" + org.ID + "移动到了：" + toID, RoadFlow.Platform.Log.Types.组织机构);
                string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('移动成功!');parent.frames[0].reLoad('" + refreshID + "');parent.frames[0].reLoad('" + toOrgID + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('移动失败!');", true);
            }
            initData();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            RoadFlow.Platform.WeiXin.Organize worg = new RoadFlow.Platform.WeiXin.Organize();
            worg.UpdateAllOrganize();
            worg.UpdateAllUsers();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('同步完成!');window.location=window.location;", true);
        }

    }
}