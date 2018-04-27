using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Run : Common.BasePage
    {
        protected RoadFlow.Data.Model.ProgramBuilderCache PBModel = null;
        protected RoadFlow.Platform.ProgramBuilder PB = new RoadFlow.Platform.ProgramBuilder();
        protected RoadFlow.Platform.ProgramBuilderQuerys PBQ = new RoadFlow.Platform.ProgramBuilderQuerys();
        protected string pid = string.Empty;
        protected System.Data.DataTable Dt = null;
        protected RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected RoadFlow.Platform.Organize BOrganize = new RoadFlow.Platform.Organize();
        protected string Query = string.Empty;
        protected string PrevUrl = string.Empty;
        protected Dictionary<int, string> buttonHtmlDicts = new Dictionary<int, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["programid"];
            buttonHtmlDicts = WebForm.Common.Tools.GetAppButtonHtml();
            Guid pguid;
            if (pid.IsGuid(out pguid))
            {
                PBModel = PB.GetSet(pguid);
            }
            if (PBModel == null)
            {
                Response.Write("未找到程序设置!");
                Response.End();
            }

            if (IsPostBack && !Request.Form["searchbutton"].IsNullOrEmpty())
            {
                foreach (var query in PBModel.Querys)
                {
                    if (query.InputType.In(3, 5))//日期时间范围
                    {
                        query.Value = Request.Form[query.ControlName + "_start"].Trim1() + "," + Request.Form[query.ControlName + "_end"].Trim1();
                    }
                    else
                    {
                        query.Value = Request.Form[query.ControlName].Trim1();
                    }
                }
            }
            else
            {
                foreach (var query in PBModel.Querys)
                {
                    if (query.InputType.In(3, 5))//日期时间范围
                    {
                        query.Value = Request.QueryString[query.ControlName + "_start"].Trim1() + "," + Request.QueryString[query.ControlName + "_end"].Trim1();
                    }
                    else
                    {
                        query.Value = Request.QueryString[query.ControlName].Trim1();
                    }
                }
            }

            InitQuery();
        }

        private void InitQuery()
        {
            Query = "&programid=" + pid + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];           
            foreach (var query in PBModel.Querys)
            {
                if (query.InputType.In(3, 5))//日期时间范围
                {
                    string[] val = query.Value.Split(',');
                    Query += "&" + query.ControlName + "_start=" + val[0] + "&" + query.ControlName + "_end=" + val[1];
                }
                else
                {
                    Query += "&" + query.ControlName + "=" + query.Value;
                }
            }
            PrevUrl = (Common.Tools.BaseUrl + "/Platform/ProgramBuilder/Run.aspx?1=1" + Query).UrlEncode();
            string pager;
            StringBuilder sql = new StringBuilder(PBModel.Program.SQL);
            List<IDbDataParameter> parList = new List<IDbDataParameter>();
            var dbconn = new RoadFlow.Platform.DBConnection().Get(PBModel.Program.DBConnID);
            if (dbconn == null)
            {
                Response.Write("未找到数据连接!");
                Response.End();
            }
            string parameter = string.Empty;
            switch (dbconn.Type)
            {
                case "SqlServer":
                    parameter = "@";
                    break;
                case "Oracle":
                    parameter = ":";
                    break;
            }
            foreach (var query in PBModel.Querys)
            {
                if (query.Value.IsNullOrEmpty())
                {
                    continue;
                }
                string queryValue = query.Value.ReplaceSelectSql();
                string operatiors = query.Operators;
                if (query.InputType == 7 && query.IsQueryUsers == 1)//查询时将组织组织机构转换为人员
                {
                    queryValue = new RoadFlow.Platform.Organize().GetAllUsersIdList(queryValue).ToArray().Join1();
                }
                switch (operatiors)
                { 
                    case "%LIKE%":
                        sql.AppendFormat(" AND {0} LIKE '%{1}%'", query.Field, queryValue);
                        break;
                    case "LIKE%":
                        sql.AppendFormat(" AND {0} LIKE '{1}%'", query.Field, queryValue);
                        break;
                    case "%LIKE":
                        sql.AppendFormat(" AND {0} LIKE '%{1}'", query.Field, queryValue);
                        break;
                    case "IN":
                        sql.AppendFormat(" AND {0} IN({1})", query.Field, RoadFlow.Utility.Tools.GetSqlInString(queryValue));
                        break;
                    case "NOT IN":
                        sql.AppendFormat(" AND {0} NOT IN({1})", query.Field, RoadFlow.Utility.Tools.GetSqlInString(queryValue));
                        break;
                    default:
                        if (query.InputType.In(3, 5))//日期时间范围
                        {
                            string[] val = queryValue.Split(',');
                            if (val[0].IsDateTime())
                            {
                                val[0] = query.InputType == 3 ? val[0].ToDateString() : val[0].ToDateTimeString();
                                sql.AppendFormat(" AND {0}{1}{2}{3}_start", query.Field, operatiors, parameter, query.ControlName);
                                if (dbconn.Type.Equals("sqlserver", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    parList.Add(new SqlParameter(parameter + query.ControlName + "_start", val[0]));
                                }
                                else if (dbconn.Type.Equals("oracle", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    parList.Add(new OracleParameter(parameter + query.ControlName + "_start", val[0]));
                                }

                            }
                            if (val[1].IsDateTime())
                            {
                                val[1] = query.InputType == 3 ? val[1].ToDateTime().AddDays(1).ToDateString() : val[1].ToDateTimeString();
                                sql.AppendFormat(" AND {0}{1}{2}{3}_end", query.Field, operatiors == ">" ? "<" : "<=", parameter, query.ControlName);
                                if (dbconn.Type.Equals("sqlserver", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    parList.Add(new SqlParameter(parameter + query.ControlName + "_end", val[1]));
                                }
                                else if (dbconn.Type.Equals("oracle", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    parList.Add(new OracleParameter(parameter + query.ControlName + "_end", val[1]));
                                }
                            }
                        }
                        else
                        {
                            sql.AppendFormat(" AND {0}{1}{2}{3}", query.Field, operatiors, parameter, query.ControlName);
                            if (dbconn.Type.Equals("sqlserver", StringComparison.CurrentCultureIgnoreCase))
                            {
                                parList.Add(new SqlParameter(parameter + query.ControlName, queryValue));
                            }
                            else if (dbconn.Type.Equals("oracle", StringComparison.CurrentCultureIgnoreCase))
                            {
                                parList.Add(new OracleParameter(parameter + query.ControlName, queryValue));
                            }
                        }
                        break;
                }
            }
            string querySql = sql.ToString().FilterWildcard(CurrentUserID.ToString());
            Dt = DBConn.GetDataTable(dbconn, querySql, out pager, Query, parList, PBModel.Program.IsPager.HasValue && PBModel.Program.IsPager.Value == 0 ? -1 : 0);
            PB.AddToExportCache(PBModel.Program.ID, dbconn.ID, querySql, parList);
            if (Dt == null)
            {
                Response.Write("查询错误!");
                Response.End();
            }
            this.PagerText.Text = pager;
        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}