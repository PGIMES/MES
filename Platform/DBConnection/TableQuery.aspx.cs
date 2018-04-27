using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebForm.Platform.DBConnection
{
    public partial class TableQuery : Common.BasePage
    {
        protected RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
        protected string TableName = string.Empty;
        protected string ConnID = string.Empty;
        protected RoadFlow.Data.Model.DBConnection MDBConn = null;
        protected string SqlString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TableName = Request.QueryString["tablename"];
            ConnID = Request.QueryString["dbconnid"];
            MDBConn = DBConn.Get(ConnID.ToGuid());
            if (MDBConn == null)
            {
                this.LiteralResult.Text = "未找到数据连接";
                this.LiteralResultCount.Text = "";
                this.resultdiv.Visible = true;
            }
            if (!IsPostBack)
            {
                if (!TableName.IsNullOrEmpty())
                {
                    SqlString = DBConn.GetDefaultQuerySql(MDBConn, TableName);
                    this.sqltext.Text = SqlString;
                    ExecuteSQL();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlString = this.sqltext.Text;
            ExecuteSQL();
        }

        protected void ExecuteSQL()
        {
            if (SqlString.IsNullOrEmpty())
            {
                this.LiteralResult.Text = "SQL为空！";
                this.LiteralResultCount.Text = "";
                this.resultdiv.Visible = true;
                return;
            }
            if (!DBConn.CheckSql(SqlString))
            {
                this.LiteralResult.Text = "SQL含有破坏系统表的语句，禁止执行！";
                this.LiteralResultCount.Text = "";
                this.resultdiv.Visible = true;
                RoadFlow.Platform.Log.Add("尝试执行有破坏系统表的SQL语句", SqlString, RoadFlow.Platform.Log.Types.数据连接);
                return;
            }
            DataTable dt = DBConn.GetDataTable(MDBConn, SqlString);
            RoadFlow.Platform.Log.Add("执行了SQL", SqlString, RoadFlow.Platform.Log.Types.数据连接, dt.ToJsonString());
            this.LiteralResult.Text = RoadFlow.Utility.Tools.DataTableToHtml(dt);
            this.LiteralResultCount.Text = "(共" + dt.Rows.Count + "行)";
            this.resultdiv.Visible = true;
        }
    }
}