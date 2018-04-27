using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Query_Add : Common.BasePage
    {
        protected RoadFlow.Platform.ProgramBuilder ProgramBuilder = new RoadFlow.Platform.ProgramBuilder();
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected string pid = string.Empty;
        protected string queryid = string.Empty;
        protected RoadFlow.Data.Model.ProgramBuilderQuerys pbq = null;
        protected RoadFlow.Platform.ProgramBuilderQuerys PBQ = new RoadFlow.Platform.ProgramBuilderQuerys();
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["pid"];
            queryid = Request.QueryString["queryid"];
            string maxSort = Request.QueryString["maxSort"];
            if (queryid.IsGuid())
            {
                pbq = PBQ.Get(queryid.ToGuid());
            }

            string field = "";
            string operators = "";
            string inputtype = "";
            string datasource = "";
            string linkid = "";
            this.Sort.Value = (maxSort.ToInt() + 5).ToString();
            if (!IsPostBack)
            {
                if (pbq != null)
                {
                    field = pbq.Field;
                    this.ShowTitle.Value = pbq.ShowTitle;
                    operators = pbq.Operators;
                    inputtype = pbq.InputType.ToString();
                    this.Width.Value = pbq.Width;
                    this.Sort.Value = pbq.Sort.ToString();
                    this.ControlName.Value = pbq.ControlName;
                    datasource = pbq.DataSource.ToString();
                    linkid = pbq.DataLinkID;
                    if (pbq.InputType == 6 || pbq.InputType == 8)
                    {
                        this.DataSource_Dict.Value = pbq.DataSourceString;
                        this.DataSource_String.Value = pbq.DataSourceString;
                        this.DataSource_Dict_Value.Value = pbq.DataSourceString;
                    }
                    else if (pbq.InputType == 7)
                    {
                        string[] dstring = (pbq.DataSourceString ?? "").Split('|');
                        if (dstring.Length > 0)
                        {
                            this.DataSource_Organize_Range.Value = dstring[0];
                        }
                        if (dstring.Length > 1)
                        {
                            this.DataSource_Organize_Type_Unit.Checked = "1" == dstring[1];
                        }
                        if (dstring.Length > 2)
                        {
                            this.DataSource_Organize_Type_Dept.Checked = "1" == dstring[2];
                        }
                        if (dstring.Length > 3)
                        {
                            this.DataSource_Organize_Type_Station.Checked = "1" == dstring[3];
                        }
                        if (dstring.Length > 4)
                        {
                            this.DataSource_Organize_Type_WorkGroup.Checked = "1" == dstring[4];
                        }
                        if (dstring.Length > 5)
                        {
                            this.DataSource_Organize_Type_Role.Checked = "1" == dstring[5];
                        }
                        if (dstring.Length > 6)
                        {
                            this.DataSource_Organize_Type_User.Checked = "1" == dstring[6];
                        }
                        if (dstring.Length > 7)
                        {
                            this.DataSource_Organize_Type_More.Checked = "1" == dstring[7];
                        }
                        this.DataSource_Organize_Type_QueryUsers.Checked = 1 == pbq.IsQueryUsers;
                    }
                }
            }

            this.FieldOptions.Text = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);
            this.OperatorsOptions.Text = BDict.GetOptionsByCode("programbuilder_operators", value: operators);
            this.InputTypeOptions.Text = BDict.GetOptionsByCode("programbuilder_inputtype", value: inputtype);
            this.ControlHidden.Value = RoadFlow.Utility.Tools.GetRandomString(6);
            this.DataSource.Text = BDict.GetRadiosByCode("programbuilder_datasource", "DataSource", value: datasource, attr: "onclick=\"dataSourceChange(this.value)\";");
            this.DataSource_String_SQL_LinkOptions.Text = new RoadFlow.Platform.DBConnection().GetAllOptions(linkid);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Field = Request.Form["Field"];
            string ShowTitle = Request.Form["ShowTitle"];
            string ControlName = Request.Form["ControlName"];
            string Operators = Request.Form["Operators"];
            string InputType = Request.Form["InputType"];
            string Width = Request.Form["Width"];
            string Sort = Request.Form["Sort"];
            string DataSource = Request.Form["DataSource"];
            string DataSource_String_SQL_Link = Request.Form["DataSource_String_SQL_Link"];

            bool isadd = false;
            if (pbq == null)
            {
                isadd = true;
                pbq = new RoadFlow.Data.Model.ProgramBuilderQuerys();
                pbq.ID = Guid.NewGuid();
                pbq.ProgramID = pid.ToGuid();
            }
            pbq.ControlName = ControlName;
            pbq.Field = Field;
            pbq.InputType = InputType.ToInt();
            pbq.Operators = Operators;
            pbq.ShowTitle = ShowTitle;
            pbq.Sort = Sort.ToInt();
            pbq.Width = Width;
            pbq.DataLinkID = DataSource_String_SQL_Link;
            
            if (pbq.InputType == 6)
            {
                if (DataSource.IsInt())
                {
                    pbq.DataSource = DataSource.ToInt();
                    if (pbq.DataSource == 1 || pbq.DataSource == 3)
                    {
                        pbq.DataSourceString = Request.Form["DataSource_String"];
                    }
                    else if (pbq.DataSource == 2)
                    {
                        pbq.DataSourceString = Request.Form["DataSource_Dict"];
                    }
                }
                else
                {
                    pbq.DataSource = null;
                }
            }
            else if (pbq.InputType == 8)
            {
                pbq.DataSourceString = Request.Form["DataSource_Dict_Value"];
            }
            else if (pbq.InputType == 7)
            {
                string DataSource_Organize_Range = Request.Form["DataSource_Organize_Range"];
                string DataSource_Organize_Type_Unit = Request.Form["DataSource_Organize_Type_Unit"];
                string DataSource_Organize_Type_Dept = Request.Form["DataSource_Organize_Type_Dept"];
                string DataSource_Organize_Type_Station = Request.Form["DataSource_Organize_Type_Station"];
                string DataSource_Organize_Type_WorkGroup = Request.Form["DataSource_Organize_Type_WorkGroup"];
                string DataSource_Organize_Type_Role = Request.Form["DataSource_Organize_Type_Role"];
                string DataSource_Organize_Type_User = Request.Form["DataSource_Organize_Type_User"];
                string DataSource_Organize_Type_More = Request.Form["DataSource_Organize_Type_More"];
                string DataSource_Organize_Type_QueryUsers = Request.Form["DataSource_Organize_Type_QueryUsers"];
                pbq.DataSourceString = DataSource_Organize_Range + "|" +
                    DataSource_Organize_Type_Unit + "|" +
                    DataSource_Organize_Type_Dept + "|" +
                    DataSource_Organize_Type_Station + "|" +
                    DataSource_Organize_Type_WorkGroup + "|" +
                    DataSource_Organize_Type_Role + "|" +
                    DataSource_Organize_Type_User + "|" +
                    DataSource_Organize_Type_More;
                pbq.IsQueryUsers = DataSource_Organize_Type_QueryUsers.ToInt(0);
            }
            

            if (isadd)
            {
                PBQ.Add(pbq);
            }
            else
            {
                PBQ.Update(pbq);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='Set_Query.aspx" + Request.Url.Query + "';", true);
        }
    }
}