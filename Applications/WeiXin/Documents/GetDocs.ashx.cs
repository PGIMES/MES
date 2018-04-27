using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Applications.WeiXin.Documents
{
    /// <summary>
    /// GetDocs 的摘要说明
    /// </summary>
    public class GetDocs : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string pageNumber = context.Request.QueryString["pagenumber"];
            string pageSize = context.Request.QueryString["pagesize"];
            string searchTitle = context.Request.QueryString["SearchTitle"];
            string dirID = context.Request.QueryString["dirid"];
            long count;
            Guid userID = RoadFlow.Platform.WeiXin.Organize.CurrentUserID;
            var docs = new RoadFlow.Platform.Documents().GetList(out count, pageSize.ToInt(), pageNumber.ToInt(), dirID, userID.ToString(), searchTitle);
            LitJson.JsonData jd = new LitJson.JsonData();
            if (docs.Rows.Count == 0)
            {
                context.Response.Write("[]");
                context.Response.End();
            }
            foreach(System.Data.DataRow dr in docs.Rows)
            {
                LitJson.JsonData jd1 = new LitJson.JsonData();
                jd1["id"] = dr["ID"].ToString();
                jd1["title"] = dr["Title"].ToString();
                jd1["writetime"] = dr["WriteTime"].ToString().ToDateTime().ToDateTimeString();
                jd1["writeuser"] = dr["WriteUserName"].ToString();
                jd.Add(jd1);
            }
            context.Response.Write(jd.ToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}