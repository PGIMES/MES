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
    public partial class TableEdit_Oracle : Common.BasePage
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
            string tablename = Request.Form["tablename"];
            string oldtablename = Request.Form["oldtablename"];
            string delfield = Request.Form["delfield"] ?? "";
            StringBuilder sql = new StringBuilder();
            List<string> sqlList = new List<string>();
            
            if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('您不能修改系统表!');", true);
                return;
            }
            if (IsAddTable)
            {
                sql.Append("CREATE TABLE \"" + tablename + "\" (");
                oldtablename = tablename;
            }
            else
            {
                if (PrimaryKeyList.Count > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" DROP PRIMARY KEY");
                }
                StringBuilder dropColnum = new StringBuilder();
                foreach (var del in delfield.Split(','))
                {
                    if (!del.IsNullOrEmpty())
                    {
                        dropColnum.Append("\"" + del + "\",");
                    }
                }
                if (dropColnum.Length > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" DROP (" + dropColnum.ToString().TrimEnd(',') + ")");
                }
            }
            StringBuilder pks = new StringBuilder();
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
                switch (fieldType.ToLower())
                {
                    case "varchar2":
                    case "nvarchar2":
                        fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() == -1 ? "50" : fieldLength : "50") + ")";
                        break;
                    case "char":
                        fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "50") + ")";
                        break;
                    case "date":
                    case "clog":
                    case "nclog":
                    case "int":
                    case "float":
                        fieldType1 = fieldType;
                        break;
                    case "number":
                        fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                        break;
                }

                int fieldIsNull1 = SchemaDt.Select("F_Name='" + fname + "'").Length > 0 ? SchemaDt.Select("F_Name='" + fname + "'")[0]["IS_NULL"].ToString().ToInt() : -1;
                string isNull = "";
                if ("1" == fieldIsNull)
                {
                    if (fieldIsNull1 == 0)
                    {
                        isNull = " NULL";
                    }
                }
                else
                {
                    if (fieldIsNull1 == 1)
                    {
                        isNull = " NOT NULL";
                    }
                }
                string identity = "1" == fieldIsIdentity ? " " : "";
                string defaultvalue = !fieldDefaultValue.IsNullOrEmpty() ? " DEFAULT " + fieldDefaultValue : "";
                bool isNew = "1" == fieldIsAdd;
                if ("1" == fieldPrimarykey)
                {
                    pks.Append("\"" + fieldName + "\",");
                }
                if (IsAddTable)
                {
                    sql.Append("\"" + fieldName + "\" ");
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
                    if (isNew)
                    {
                        sqlList.Add("ALTER TABLE \"" + oldtablename + "\" ADD (\"" + fieldName + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                    }
                    else
                    {
                        if (!fieldIsIdentity.IsNullOrEmpty())
                        {
                            sqlList.Add("ALTER TABLE \"" + oldtablename + "\" MODIFY (\"" + fieldName + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                        }
                        else
                        {
                            sqlList.Add("ALTER TABLE \"" + oldtablename + "\" MODIFY (\"" + fname + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                        }
                    }

                    if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        sqlList.Add("ALTER TABLE \"" + oldtablename + "\" RENAME COLUMN \"" + fname + "\" TO \"" + fieldName + "\"");
                    }
                }
                if (pks.Length > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" ADD CONSTRAINT \"" + tablename + "_PK\" PRIMARY KEY (" + pks.ToString().TrimEnd(',') + ")");
                }
                if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" RENAME TO \"" + tablename + "\"");
                }
            }
            string sql1 = string.Empty;
            bool isSuccess = true;
            string errmsg = string.Empty;
            if (IsAddTable)
            {
                if (pks.Length > 0)
                {
                    sql.Append(", PRIMARY KEY (");
                    sql.Append(pks.ToString().TrimEnd(','));
                    sql.Append(")");
                }
                sql.Append(")");
                sql1 = sql.ToString();
                isSuccess = DBConn.TestSql(dbconn, sql1, out errmsg, false);
            }
            else
            {
                sql1 = sqlList.ToString(";");
                foreach (var s in sqlList)
                {
                    if (!DBConn.TestSql(dbconn, s, out errmsg, false) && isSuccess)
                    {
                        isSuccess = false;
                    }
                }
            }
            string url = "TableEdit_Oracle.aspx?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
            if (isSuccess)
            {
                RoadFlow.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存成功!');window.location='" + url + "';", true);
                return;
            }
            else
            {
                RoadFlow.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, RoadFlow.Platform.Log.Types.数据连接, errmsg);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", "alert('保存失败-" + errmsg.RemoveHTML().Replace("'", "") + "!');window.location='" + url + "';", true);
                return;
            }

        }
    }
}