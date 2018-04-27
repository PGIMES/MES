using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.DBConnection
{
    public partial class TableDelete : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tableName = Request.QueryString["tablename"];
            string connid = Request.QueryString["dbconnid"];

            string prevUrl = "Table.aspx" + Request.Url.Query; ;
            RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
            var conn = DBConn.Get(connid.ToGuid());
            if (conn == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('未找到数据连接!');window.location='" + prevUrl + "';", true);
                return;
            }
            string sql = string.Empty;
            switch (conn.Type.ToLower())
            { 
                case "sqlserver":
                    sql = "DROP TABLE [" + tableName + "]";
                    break;
                case "oracle":
                    sql = "DROP TABLE " + tableName + " purge";
                    break;
                case "mysql":
                    sql = "DROP TABLE `" + tableName + "`";
                    break;
            }
             
            if (RoadFlow.Utility.Config.SystemDataTables.Find(p => p.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('不能删除系统表!');window.location='" + prevUrl + "';", true);
                RoadFlow.Platform.Log.Add("删除表-不能删除系统表-" + tableName, sql, RoadFlow.Platform.Log.Types.数据连接);
                return;
            }
            
            if (conn != null)
            {
                if (DBConn.TestSql(conn, sql, false))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除成功!');window.location='" + prevUrl + "';", true);
                    RoadFlow.Platform.Log.Add("删除表-成功-" + tableName, sql, RoadFlow.Platform.Log.Types.数据连接);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除失败!');window.location='" + prevUrl + "';", true);
                    RoadFlow.Platform.Log.Add("删除表-失败-" + tableName, sql, RoadFlow.Platform.Log.Types.数据连接);
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('未找到数据连接!');window.location='" + prevUrl + "';", true);
                RoadFlow.Platform.Log.Add("删除表-未找到连接-" + tableName, sql, RoadFlow.Platform.Log.Types.数据连接);
                return;
            }
        }
    }
}