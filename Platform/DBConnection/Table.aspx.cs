using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.DBConnection
{
    public partial class Table : Common.BasePage
    {
        protected string dbconnID = string.Empty;
        protected List<Tuple<string, int>> List = new List<Tuple<string, int>>();
        protected RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
        protected string Query = string.Empty;
        protected string s_Name = string.Empty;
        protected string DBType = string.Empty;
        protected List<string> SystemTables;
        protected void Page_Load(object sender, EventArgs e)
        {
            SystemTables = RoadFlow.Utility.Config.SystemDataTables;
            dbconnID = Request.QueryString["connid"];
            if (!IsPostBack)
            {
                s_Name = Request.QueryString["s_Name"];
                this.Name1.Value = s_Name;
                InitList();
            }
        }

        private void InitList()
        {
            Query = "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + s_Name;
            if (!dbconnID.IsGuid())
            {
                Response.Write("数据连接ID错误");
                return;
            }
            var dbconn = DBConn.Get(dbconnID.ToGuid());
            if (dbconn == null)
            {
                Response.Write("未找到数据连接");
                return;
            }
            DBType = dbconn.Type;
            foreach (var table in DBConn.GetTables(dbconn.ID, 1))
            {
                List.Add(new Tuple<string, int>(table, 0));
            }
            foreach (var view in DBConn.GetTables(dbconn.ID, 2))
            {
                List.Add(new Tuple<string, int>(view, 1));
            }
            if (!s_Name.IsNullOrEmpty())
            {
                List = List.FindAll(p => p.Item1.Contains(s_Name.Trim1(), StringComparison.CurrentCultureIgnoreCase));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            s_Name = this.Name1.Value;
            InitList();
        }
    }
}