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
    public partial class TableEdit_MySql : Common.BasePage
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
                        if (!IsAddTable)
                        {
                            SchemaDt = DBConn.GetTableSchema(conn, tableName, dbconn.Type);
                            PrimaryKeyList = DBConn.GetPrimaryKey(dbconn, tableName);
                        }
                        else
                        {
                            SchemaDt = DBConn.GetTableSchema(conn, "Log", dbconn.Type);
                            SchemaDt.Rows.Clear();
                        }
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
            string tablename = Request.Form["tablename"];
            string oldtablename = Request.Form["oldtablename"];
            string delfield = Request.Form["delfield"] ?? "";
            StringBuilder sql = new StringBuilder();
            string tempColumn = "temp_" + Guid.NewGuid().ToString("N");
            if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('您不能修改系统表!');", true);
                return;
            }
            if (IsAddTable)
            {
                sql.Append("CREATE TABLE `" + tablename + "` (`" + tempColumn + "` varchar(255) PRIMARY KEY NOT NULL);");
                oldtablename = tablename;
            }
            
            sql.Append("ALTER TABLE `" + oldtablename + "` ");
            if (PrimaryKeyList.Count > 0)
            {
                sql.Append("DROP PRIMARY KEY,");
            }
           
            foreach (var del in delfield.Split(','))
            {
                if (!del.IsNullOrEmpty())
                {
                    sql.Append("DROP COLUMN `" + del + "`,");
                }
            }

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
                        fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() <= -1 ? "255" : fieldLength : "255") + ")";
                        break;
                    case "char":
                        fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "255") + ")";
                        break;
                    case "datetime":
                    case "text":
                    case "longtext":
                    case "int":
                    case "float":
                        fieldType1 = fieldType;
                        break;
                    case "decimal":
                        fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                        break;
                }
                string isNull = "1" == fieldIsNull ? " NULL" : " NOT NULL";
                string identity = "1" == fieldIsIdentity ? " AUTO_INCREMENT" : "";
                string defaultValue = fieldDefaultValue.IsNullOrEmpty() ? "" : " DEFAULT " + fieldDefaultValue;
                bool isNew = "1" == fieldIsAdd;
                if (isNew)
                {
                    sql.Append("ADD COLUMN `" + fieldName + "` " + fieldType1 + identity + isNull + ",");
                }
                else
                {
                    if (!fieldIsIdentity.IsNullOrEmpty())
                    {
                        sql.Append("MODIFY COLUMN `" + fieldName + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                    }
                    else
                    {
                        if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            sql.Append("CHANGE COLUMN `" + fname + "` `" + fieldName + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                        }
                        else
                        {
                            sql.Append("MODIFY COLUMN `" + fname + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                        }
                    }
                }
                if ("1" == fieldPrimarykey)
                {
                    sql.Append("ADD PRIMARY KEY (`" + fname + "`),");
                }
               
                
            }
            if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
            {
                sql.Append("RENAME TABLE `" + oldtablename + "` TO `" + tablename + "`,");
            }
            if (IsAddTable)
            {
                sql.Append("DROP COLUMN `" + tempColumn + "`,");
            }
            string sql1 = sql.ToString().TrimEnd(',') + ";";
            bool isSuccess = DBConn.TestSql(dbconn, sql1, false);
            string url = "TableEdit_MySql.aspx?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
            if (isSuccess)
            {
                RoadFlow.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存成功!');window.location='" + url + "';", true);
                return;
            }
            else
            {
                RoadFlow.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存失败!');window.location='" + url + "';", true);
                return;
            }

        }
    }
}