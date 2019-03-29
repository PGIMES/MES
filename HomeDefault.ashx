<%@ WebHandler Language="C#" Class="HomeDefault" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
public class HomeDefault : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        var username = context.Request.QueryString["flag"];//"陈红卫";
        if (context.Request.QueryString["flag"].Equals("yangjianrenwu"))
        {
            //context.Response.Write(GetResult(5,Int32.Parse(context.Request.QueryString["index"])));
            string[] cols = new string[] {"requestid","xmh","Update_Engineer_ms","Receive_time","Sign_status" };

            context.Response.Write( GetResult( username ));
        }
        if (context.Request.QueryString["flag"].Equals("count"))
        {
            context.Response.Write(getCount());
        }
    }
    private string GetResult(string username )
    {
        DataTable dt = new DataTable();

        dt = DbHelperSQL.Query(" rpt_Form1_sale_YJ_Tracking_Progress '','','','','','','"+username+"','','0','-1','','','-1','ALL'").Tables[0];

        int rLen = dt.Rows.Count; rLen= rLen>5? rLen = 5:rLen=rLen;

        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"homelistdiv\"><div style=\"height:35px; font-size:14px;\">"
                    +"<div class=\"fa fa-pencil-square-o\" style=\"font-size:18px;float:left;margin-right:6px;padding-top:12px;\"></div><div style=\"padding-top:10px; float:left; font-weight:bold;\">样件待办事项</div>"
                    +"<div style=\"padding-top:10px; float:right; margin-right:10px;\">"
                    +"    <a href=\"/yangjian/YJ_Tracking_Process_Report.aspx\"  target='_blank'>"
                    + "       <i class=\"fa fa-angle-double-right\"></i><span style=\"font-size:12px; margin-left:3px;\">更多...</span>"
                    + "   </a> </div> </div><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"hometable\"><th></th>");
        sb.Append("<thead><tr><th>项目号</th><th>步骤</th><th>接收时间</th><th>要求完成日</th><th>状态</th></tr></thead>");
        for (int j = 0; j < rLen; j++)
        {
            sb.Append("<tr>");
            sb.Append("<td><a href='/yangjian/yangjian.aspx?requestid="+dt.Rows[j]["requestid"].ToString()+"' target='_blank'>"+dt.Rows[j]["xmh"].ToString()+"</a></td>");
            sb.Append("<td>"+dt.Rows[j]["Update_Engineer_ms"].ToString()+"</td>");
            sb.Append("<td>"+(string.IsNullOrEmpty(dt.Rows[j]["Receive_time"].ToString())==true?"":Convert.ToDateTime(dt.Rows[j]["Receive_time"]).ToString("yyyy-MM-dd"))+"</td>");
            sb.Append("<td>"+Convert.ToDateTime(dt.Rows[j]["Requiredate"]).ToString("yyyy-MM-dd")+"</td>");
            sb.Append("<td>"+dt.Rows[j]["Sign_status"].ToString()+"</td>");

            sb.Append("</tr>");
        }
        sb.Append("</table></div>");
        return sb.ToString();
    }

    private string GetResult()
    {
        DataTable dt = new DataTable();

        dt = DbHelperSQL.Query(" rpt_Form1_sale_YJ_Tracking_Progress '','','','','','','陈红卫','','0','-1','','','-1','ALL'").Tables[0];

        int rLen = dt.Rows.Count;
        int cLen = dt.Columns.Count;

        StringBuilder sb = new StringBuilder();

        for (int j = 0; j < rLen; j++)
        {
            sb.Append("<tr>");
            for (int i = 0; i < cLen; i++)
            {
                sb.Append("<td>");
                sb.Append(dt.Rows[j][i].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        return sb.ToString();
    }
    private string GetResult(int pagecount,int currentpage)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["conn"]))
        {
            conn.Open();
            string sql = "SELECT TOP "+pagecount+" * FROM TestBlog tb WHERE ydid NOT IN (SELECT TOP "+pagecount*(currentpage-1)+" ydid FROM TestBlog tb2) ";
            SqlDataAdapter sda = new SqlDataAdapter(sql,conn);
            sda.Fill(dt);
        }

        int rLen = dt.Rows.Count;
        int cLen = dt.Columns.Count;

        StringBuilder sb = new StringBuilder();


        for (int j = 0; j < rLen; j++)
        {
            sb.Append("<tr id=add"+j+">");
            for (int i = 0; i < cLen; i++)
            {
                sb.Append("<td>");
                sb.Append(dt.Rows[j][i].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }

        return sb.ToString();
    }

    private string getCount()
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["conn"]))
        {
            conn.Open();
            string sql = "select count(*) from testblog";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            sda.Fill(dt);
        }
        return dt.Rows[0][0].ToString();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}