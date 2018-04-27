using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Menu
{
    /// <summary>
    /// TreeRefresh 的摘要说明
    /// </summary>
    public class TreeRefresh : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["refreshid"];
            if (!id.IsGuid())
            {
                context.Response.Write("[]");
                return;
            }
            RoadFlow.Platform.Menu BMenu = new RoadFlow.Platform.Menu();
            var childs = BMenu.GetAllDataTable().Select("ParentID='" + id + "'");
            
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", childs.Length * 50);
            int count = childs.Length;
            int i = 0;
            foreach (var child in childs)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child["ID"]);
                json.AppendFormat("\"title\":\"{0}\",", child["Title"]);
                json.AppendFormat("\"ico\":\"{0}\",", child["AppIco"].ToString().IsNullOrEmpty() ? "" : child["AppIco"].ToString());
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", "");
                json.AppendFormat("\"width\":\"{0}\",", "");
                json.AppendFormat("\"height\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", BMenu.HasChild(child["ID"].ToString().ToGuid()) ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            context.Response.Write(json.ToString());
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