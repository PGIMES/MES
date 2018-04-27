using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class BodyAdd : Common.BasePage
    {
        RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
        RoadFlow.Data.Model.Organize org = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Guid orgID;
            if (id.IsGuid(out orgID))
            {
                org = borganize.Get(orgID);
            }

            if (!IsPostBack)
            {
                this.TypeRadios.Text = borganize.GetTypeRadio("Type", "", "validate=\"radio\"");
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", "0", "validate=\"radio\"");
            }
            this.Button1.ClickDisabled();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = Request.Form["Name"];
            string type = Request.Form["Type"];
            string status = Request.Form["Status"];
            string note = Request.Form["note"];

            RoadFlow.Data.Model.Organize org1 = new RoadFlow.Data.Model.Organize();
            Guid org1ID = Guid.NewGuid();
            org1.ID = org1ID;
            org1.Name = name.Trim();
            org1.Note = note.IsNullOrEmpty() ? null : note.Trim();
            org1.Number = org.Number + "," + org1ID.ToString().ToLower();
            org1.ParentID = org.ID;
            org1.Sort = borganize.GetMaxSort(org.ID);
            org1.Status = status.IsInt() ? status.ToInt() : 0;
            org1.Type = type.ToInt();
            org1.Depth = org.Depth + 1;
            org1.IntID = org1ID.ToInt32();

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                borganize.Add(org1);
                //更新父级[ChildsLength]字段
                borganize.UpdateChildsLength(org.ID);
                scope.Complete();
            }
            //同步修改微信企业号
            if (RoadFlow.Platform.WeiXin.Config.IsUse)
            {
                new RoadFlow.Platform.WeiXin.Organize().AddDeptAsync(org1);
            }
            new RoadFlow.Platform.MenuUser().ClearCache();
            RoadFlow.Platform.Log.Add("添加了组织机构", org1.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');parent.frames[0].reLoad('" + Request.QueryString["id"] + "');window.location=window.location;", true);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}