using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Attr : Common.BasePage
    {
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected string pid = string.Empty;
        protected RoadFlow.Data.Model.ProgramBuilder pb = null;
        protected RoadFlow.Platform.ProgramBuilder PB = new RoadFlow.Platform.ProgramBuilder();
        protected string query = string.Empty;
        protected string formid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            pid = Request.QueryString["pid"];
            if (pid.IsGuid())
            {
                pb = PB.Get(pid.ToGuid());
            }
            string typeid = Request.QueryString["typeid"];
            string isadd = "1";
            string ispager = "1";
            string connid="";
            string apptypes = "";
            int buttonOptionsIndex = 0;
            int tableStyleOptionsIndex = 0;
            if (!IsPostBack)
            {
                if (pb != null)
                {
                    this.Title1.Value = pb.Name;
                    this.sql.Value = pb.SQL;
                    this.ClientScript.Value = pb.ClientScript;
                    typeid = pb.Type.ToString();
                    isadd = pb.IsAdd.ToString();
                    ispager = pb.IsPager.ToString();
                    connid=pb.DBConnID.ToString();
                    buttonOptionsIndex = pb.ButtonLocation.HasValue ? pb.ButtonLocation.Value : 0;
                    tableStyleOptionsIndex = "reporttable" == pb.TableStyle ? 1 : 0;
                    if (pb.FormID.IsGuid())
                    {
                        formid = pb.FormID.ToString();
                        var app = new RoadFlow.Platform.AppLibrary().Get(pb.FormID.ToGuid());
                        if (app != null)
                        {
                            apptypes = app.Type.ToString();
                        }
                    }
                    this.form_editmodel.SelectedIndex = pb.EditModel.HasValue ? pb.EditModel.Value : 0;
                    this.form_editmodel_width.Value = pb.Width;
                    this.form_editmodel_height.Value = pb.Height;
                    this.ExportTemplate.Value = pb.ExportTemplate;
                    this.ExportHeaderText.Value = pb.ExportHeaderText;
                    this.ExportFileName.Value = pb.ExportFileName;
                    this.TableHead.Value = pb.TableHead;
                }
            }
            this.TypeOptions.Text = new RoadFlow.Platform.AppLibrary().GetTypeOptions(typeid);
            //this.IsAddOptions.Text = BDict.GetOptionsByCode("yesno", value: isadd);
            this.IsPagerOptions.Text = BDict.GetOptionsByCode("yesno", value: ispager);
            this.DbConnOptions.Text = new RoadFlow.Platform.DBConnection().GetAllOptions(connid);
            this.TypeOptions1.Text = new RoadFlow.Platform.AppLibrary().GetTypeOptions(apptypes);
            this.ButtonLocation.SelectedIndex = buttonOptionsIndex;
            this.TableStyle.SelectedIndex = tableStyleOptionsIndex;
            this.Button1.ClickDisabled();
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"] + "&Name1=" + Request.QueryString["Name1"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string title1 = this.Title1.Value;
            string sql = this.sql.Value;
            string typeid = Request.Form["Type"];
            string IsAdd = Request.Form["IsAdd"];
            string DBConnID = Request.Form["DBConnID"];
            string form_forms = Request.Form["form_forms"];
            string form_editmodel = Request.Form["form_editmodel"];
            string form_editmodel_width = Request.Form["form_editmodel_width"];
            string form_editmodel_height = Request.Form["form_editmodel_height"];
            string ButtonLocation = Request.Form["ButtonLocation"];
            string IsPager = Request.Form["IsPager"];
            string ClientScript = Request.Form["ClientScript"];
            string ExportTemplate = Request.Form["ExportTemplate"];
            string ExportHeaderText = Request.Form["ExportHeaderText"];
            string ExportFileName = Request.Form["ExportFileName"];
            string TableStyle = Request.Form["TableStyle"];
            string TableHead = Request.Form["TableHead"];

            bool isadd = false;
            if (pb == null)
            {
                isadd = true;
                pb = new RoadFlow.Data.Model.ProgramBuilder();
                pb.ID = Guid.NewGuid();
                pb.CreateTime = RoadFlow.Utility.DateTimeNew.Now;
                pb.CreateUser = CurrentUserID;
                pb.Status = 0;
            }
            pb.IsAdd = IsAdd.ToInt(0);
            pb.Name = title1.Trim();
            pb.SQL = sql;
            pb.DBConnID = DBConnID.ToGuid();
            pb.Type = typeid.ToGuid();
            pb.FormID = form_forms;
            pb.EditModel = form_editmodel.ToInt(0);
            pb.Width = form_editmodel_width;
            pb.Height = form_editmodel_height;
            pb.ButtonLocation = ButtonLocation.ToInt(0);
            pb.IsPager = IsPager.ToInt(1);
            pb.ClientScript = ClientScript;
            pb.ExportTemplate = ExportTemplate;
            pb.ExportHeaderText = ExportHeaderText.Trim1();
            pb.ExportFileName = ExportFileName.Trim1();
            pb.TableStyle = TableStyle;
            pb.TableHead = TableHead;
            if (isadd)
            {
                PB.Add(pb);
            }
            else
            {
                PB.Update(pb);
            }
            RoadFlow.Platform.Log.Add("保存了应用程序设计属性", pb.Serialize(), RoadFlow.Platform.Log.Types.其它分类);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功');parent.location='Add.aspx?pid=" + pb.ID + query + "';", true);
        }
    }
}