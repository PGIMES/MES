using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace WebForm.Platform.DBConnection
{
    public partial class TableEdit_SqlServer : Common.BasePage
    {
        protected string dbconnID = string.Empty;
        protected string tableName = string.Empty;
        protected RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
        protected DataTable SchemaDt = new DataTable();
        protected IDbConnection conn = null;
        protected List<string> PrimaryKeyList = new List<string>();
        protected RoadFlow.Data.Model.DBConnection dbconn = null;
        protected bool IsAddTable = false;
        protected List<string> SystemTables;
        protected bool hasIdentity = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            SystemTables = RoadFlow.Utility.Config.SystemDataTables;
            dbconnID = Request.QueryString["dbconnid"];
            tableName = Request.QueryString["tablename"];
            if (tableName.IsNullOrEmpty())
            {
                tableName = "NEWTABLE_" + RoadFlow.Utility.Tools.GetRandomString();
                IsAddTable = true;
            }
            if (dbconnID.IsGuid() && !tableName.IsNullOrEmpty())
            {
                dbconn = DBConn.Get(dbconnID.ToGuid());
                if (dbconn != null)
                {
                    conn = DBConn.GetConnection(dbconn);
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        SchemaDt = DBConn.GetTableSchema(conn, tableName, dbconn.Type);
                        PrimaryKeyList = DBConn.GetPrimaryKey(dbconn, tableName);
                    }
                }
            }
            if (IsAddTable)
            {
                tableName = "";
            }
            if (SchemaDt.Rows.Count == 0)
            {
                DataRow dr = SchemaDt.NewRow();
                dr["f_name"] = "ID";
                dr["t_name"] = "int";
                dr["is_null"] = 0;
                dr["isidentity"] = 1;
                SchemaDt.Rows.Add(dr);
                PrimaryKeyList.Add("ID");
            }
            this.LinkButton1.ClickDisabled();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (dbconn == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('未找到数据连接!');", true);
                return;
            }
            
            string f_name = Request.Form["f_name"] ?? "";
            string[] f_nameArray = f_name.Split(',');
            string tablename = Request.Form["tablename"].Trim1();
            string oldtablename = Request.Form["oldtablename"];
            string delfield = Request.Form["delfield"] ?? "";
            StringBuilder sql = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('您不能修改系统表!');", true);
                return;
            }
            if (IsAddTable)
            {
                sql.Append("CREATE TABLE [" + tablename + "] (");
                oldtablename = tablename;
            }
            else
            {
                List<string> constraints = DBConn.GetConstraints(dbconn, oldtablename);
                foreach (string constraint in constraints)
                {
                    sql.Append("ALTER TABLE [" + oldtablename + "] DROP CONSTRAINT [" + constraint + "];");
                }
                StringBuilder dropColnum = new StringBuilder();
                foreach (var del in delfield.Split(','))
                {
                    if (!del.IsNullOrEmpty() && SchemaDt.Select("f_name='" + del + "'").Length > 0)
                    {
                        dropColnum.Append("[" + del + "],");
                    }
                }
                if (dropColnum.Length > 0)
                {
                    sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN " + dropColnum.ToString().TrimEnd(',') + ";");
                }
            }
            
            List<string> pkList = new List<string>();
            foreach (var fname in f_nameArray)
            {
                string fieldName = Request.Form[fname + "_name1"];
                string fieldType = Request.Form[fname + "_type"];
                string fieldLength = Request.Form[fname + "_length"];
                string fieldIsNull = Request.Form[fname + "_isnull"];
                string fieldIsIdentity = Request.Form[fname + "_isidentity"];
                string fieldPrimarykey = Request.Form[fname + "_primarykey"];
                string fieldDefaultValue = Request.Form[fname + "_defaultvalue"];
                string fieldIsAdd = Request.Form[fname + "_isadd"];
               
                if (fieldName.IsNullOrEmpty() || fieldType.IsNullOrEmpty())
                {
                    continue;
                }
                string fieldType1 = string.Empty;
                switch (fieldType)
                { 
                    case "varchar":
                    case "nvarchar":
                        fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() == -1 ? "MAX" : fieldLength : "50") + ")";
                        break;
                    case "char":
                        fieldType1 =  fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "50") + ")";
                        break;
                    case "datetime":
                    case "text":
                    case "uniqueidentifier":
                    case "int":
                    case "money":
                    case "float":
                        fieldType1 = fieldType;
                        break;
                    case "decimal":
                        fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                        break;
                }
                string isNull = "1" == fieldIsNull ? " NULL" : " NOT NULL";
                string identity = "1" == fieldIsIdentity ? " IDENTITY(1,1)" : "";
                bool isNew = "1" == fieldIsAdd;
                if ("1" == fieldPrimarykey)
                {
                    pkList.Add(fieldName);
                }
                if (IsAddTable)
                {
                    sql.Append("[" + fieldName + "] ");
                    sql.Append(fieldType1);
                    sql.Append(" " + isNull);
                    sql.Append(" " + identity);
                    if (!fieldDefaultValue.IsNullOrEmpty())
                    {
                        sql.Append(" DEFAULT " + fieldDefaultValue);
                    }
                    if (!fname.Equals(f_nameArray.Last(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        sql.Append(",");
                    }
                }
                else
                {
                    //如果以前是自增，现在又取消了
                    if (!isNew && identity.IsNullOrEmpty() && SchemaDt.Select("f_name='" + fname + "' and isidentity=1").Length > 0)
                    {
                        sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN [" + fname + "];");
                        sql.Append("ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] " + fieldType1 + identity + isNull + ";");
                    }
                    else
                    {
                        if (!identity.IsNullOrEmpty() && !isNew)
                        {
                            sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN [" + fname + "];ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] int NOT NULL IDENTITY(1,1);");
                        }
                        else
                        {
                            if (isNew)
                            {
                                sql.Append("ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] " + fieldType1 + identity + isNull + ";");
                            }
                            else
                            {
                                sql.Append("ALTER TABLE [" + oldtablename + "] ALTER COLUMN [" + fname + "] " + fieldType1 + identity + isNull + ";");
                            }
                            if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                sql.Append("EXEC sp_rename N'[" + oldtablename + "].[" + fname + "]', N'" + fieldName + "', 'COLUMN';");
                            }
                        }
                    }

                    if (!fieldDefaultValue.IsNullOrEmpty())
                    {
                        sql.Append("ALTER TABLE [" + oldtablename + "] ADD CONSTRAINT [DF_" + tablename + "_" + fname + "] DEFAULT (" + fieldDefaultValue + ") FOR [" + fname + "];");
                    }
                }
            }
            if (IsAddTable)
            {
                if (pkList.Count > 0)
                {
                    sql.Append(", PRIMARY KEY (");
                    foreach (string pk in pkList)
                    {
                        sql.Append("[" + pk + "]");
                        if (!pk.Equals(pkList.Last()))
                        {
                            sql.Append(",");
                        }
                    }
                    sql.Append(")");
                }
                sql.Append(")");
            }
            else
            {
                if (pkList.Count > 0)
                {
                    sql2.Append("ALTER TABLE [" + tablename + "] ADD CONSTRAINT [PK_" + tablename + "] PRIMARY KEY (");
                    foreach (string pk in pkList)
                    {
                        sql2.Append("[" + pk + "]");
                        if (!pk.Equals(pkList.Last()))
                        {
                            sql2.Append(",");
                        }
                    }
                    sql2.Append(");");
                }
                if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.Append("EXEC sp_rename '" + oldtablename + "', '" + tablename + "';");
                }
            }
            string sql1 = sql.ToString();
            string errmsg;
            bool isSuccess = DBConn.TestSql(dbconn, sql1, out errmsg, false);
            if (isSuccess && sql2.Length > 0)
            {
                isSuccess = DBConn.TestSql(dbconn, sql2.ToString(), out errmsg, false);
            }
            string url = "TableEdit_SqlServer.aspx?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
            if (isSuccess)
            {
                RoadFlow.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存成功!');window.location='" + url + "';", true);
                return;
            }
            else
            {
                RoadFlow.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接, errmsg);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存失败-" + errmsg.RemoveHTML().Replace("'","") + "!');window.location='" + url + "';", true);
                return;
            }

        }
    }
}