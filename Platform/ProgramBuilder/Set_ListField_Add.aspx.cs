using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_ListField_Add : Common.BasePage
    {
        protected RoadFlow.Platform.ProgramBuilder ProgramBuilder = new RoadFlow.Platform.ProgramBuilder();
        protected string pid = string.Empty;
        protected string fid = string.Empty;
        protected RoadFlow.Data.Model.ProgramBuilderFields pbf = null;
        protected RoadFlow.Platform.ProgramBuilderFields PBF = new RoadFlow.Platform.ProgramBuilderFields();
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled();
            pid = Request.QueryString["pid"];
            fid = Request.QueryString["fid"];
            string maxSort = Request.QueryString["maxSort"];
            if (fid.IsGuid())
            {
                pbf = PBF.Get(fid.ToGuid());
            }
            this.Sort.Value = (maxSort.ToInt() + 5).ToString();
            string field = "";
            string showType="";
            string align="";
            if (!IsPostBack)
            {
                if (pbf != null)
                {
                    field = pbf.Field;
                    this.ShowTitle.Value = pbf.ShowTitle;
                    this.ShowFormat.Value = pbf.ShowFormat;
                    this.Width.Value = pbf.Width;
                    this.CustomString.Value = pbf.CustomString;
                    this.Sort.Value = pbf.Sort.ToString();
                    showType=pbf.ShowType.ToString();
                    align=pbf.Align;
                }
            }
            this.ShowTypeOptions.Text = BDict.GetOptionsByCode("programbuilder_showtype", value: showType);
            this.AlignOptions.Text = BDict.GetOptionsByCode("programbuilder_align", value: align);
            this.FieldOptions.Text = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Field = Request.Form["Field"];
            string ShowTitle = Request.Form["ShowTitle"];
            string ShowType = Request.Form["ShowType"];
            string ShowFormat = Request.Form["ShowFormat"];
            string Align = Request.Form["Align"];
            string Width = Request.Form["Width"];
            string CustomString = Request.Form["CustomString"];
            string Sort = Request.Form["Sort"];

            bool isadd = false;
            if (pbf == null)
            {
                isadd = true;
                pbf = new RoadFlow.Data.Model.ProgramBuilderFields();
                pbf.ID = Guid.NewGuid();
                pbf.ProgramID = pid.ToGuid();
            }

            pbf.Align = Align;
            pbf.CustomString = CustomString;
            pbf.Field = Field;
            pbf.ShowFormat = ShowFormat;
            pbf.ShowTitle = ShowTitle;
            pbf.ShowType = ShowType.ToInt();
            pbf.Sort = Sort.ToInt();
            pbf.Width = Width;

            if (isadd)
            {
                PBF.Add(pbf);
            }
            else
            {
                PBF.Update(pbf);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='Set_ListField.aspx"+Request.Url.Query+"';", true);
        }
    }
}