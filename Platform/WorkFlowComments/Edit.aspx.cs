using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowComments
{
    public partial class Edit : Common.BasePage
    {
        RoadFlow.Platform.WorkFlowComment bworkFlowComment = new RoadFlow.Platform.WorkFlowComment();
        RoadFlow.Data.Model.WorkFlowComment workFlowComment = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                this.usermemberid.Visible = false;
            }
            Guid commentID;
            if (id.IsGuid(out commentID))
            {
                workFlowComment = bworkFlowComment.Get(commentID);
            }
            
            if (!IsPostBack)
            {
                if (workFlowComment != null)
                {
                    this.Comment.Value = workFlowComment.Comment;
                    this.Member.Value = workFlowComment.MemberID;
                    this.Sort.Value = workFlowComment.Sort.ToString();
                }
                else
                {
                    this.Sort.Value = (bworkFlowComment.GetManagerMaxSort() + 5).ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string oldXML = workFlowComment.Serialize();
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            string  member = isOneSelf ? RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString() : Request.Form["Member"];
            string comment = Request.Form["Comment"];
            string sort = Request.Form["Sort"];

            bool isAdd = false;
            if (workFlowComment == null)
            {
                isAdd = true;
                workFlowComment = new RoadFlow.Data.Model.WorkFlowComment();
                workFlowComment.ID = Guid.NewGuid();
                workFlowComment.Type = isOneSelf ? 1 : 0;
            }
            else
            {
                oldXML = workFlowComment.Serialize();
            }
            workFlowComment.MemberID = member.IsNullOrEmpty() ? "" : member.Trim();
            workFlowComment.Comment = comment.IsNullOrEmpty() ? "" : comment.Trim();
            workFlowComment.Sort = sort.IsInt() ? sort.ToInt() : bworkFlowComment.GetManagerMaxSort() + 5;
            if (isAdd)
            {
                bworkFlowComment.Add(workFlowComment);
                RoadFlow.Platform.Log.Add("添加了流程意见", workFlowComment.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
            }
            else
            {
                bworkFlowComment.Update(workFlowComment);
                RoadFlow.Platform.Log.Add("修改了流程意见", "", RoadFlow.Platform.Log.Types.流程相关, oldXML, workFlowComment.Serialize());
            }
            bworkFlowComment.RefreshCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "new RoadUI.Window().reloadOpener();alert('保存成功!'); new RoadUI.Window().close();", true);
        }
    }
}