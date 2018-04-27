using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Export_Add : Common.BasePage
    {
        protected RoadFlow.Platform.ProgramBuilder ProgramBuilder = new RoadFlow.Platform.ProgramBuilder();
        protected string pid = string.Empty;
        protected string fid = string.Empty;
        protected RoadFlow.Data.Model.ProgramBuilderExport pbe = null;
        protected RoadFlow.Platform.ProgramBuilderExport PBE = new RoadFlow.Platform.ProgramBuilderExport();
        protected RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.ClickDisabled();
            pid = Request.QueryString["pid"];
            fid = Request.QueryString["fid"];
            string maxSort = Request.QueryString["maxSort"];
            if (fid.IsGuid())
            {
                pbe = PBE.Get(fid.ToGuid());
            }
            this.Sort.Value = (maxSort.ToInt() + 5).ToString();
            string field = "";
            string showType = "";
            string align = "";
            string dataType = "";
            if (!IsPostBack)
            {
                if (pbe != null)
                {
                    field = pbe.Field;
                    this.ShowTitle.Value = pbe.ShowTitle;
                    this.ShowFormat.Value = pbe.ShowFormat;
                    this.Width.Value = pbe.Width.ToString();
                    this.CustomString.Value = pbe.CustomString;
                    this.Sort.Value = pbe.Sort.ToString();
                    showType = pbe.ShowType.ToString();
                    align = pbe.Align.ToString();
                    dataType = pbe.DataType.ToString();
                }
            }
            this.ShowTypeOptions.Text = BDict.GetOptionsByCode("programbuilder_showtype", value: showType);
            this.AlignOptions.Text = BDict.GetOptionsByCode("programbuilder_align", value: align);
            this.FieldOptions.Text = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);
            this.DataTypeOptions.Text = PBE.GetDataTypeOptions(dataType);
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
            string DataType = Request.Form["DataType"];

            bool isadd = false;
            if (pbe == null)
            {
                isadd = true;
                pbe = new RoadFlow.Data.Model.ProgramBuilderExport();
                pbe.ID = Guid.NewGuid();
                pbe.ProgramID = pid.ToGuid();
            }
            pbe.Align = Align.ToInt(0);
            pbe.CustomString = CustomString;
            pbe.Field = Field;
            pbe.ShowFormat = ShowFormat;
            pbe.ShowTitle = ShowTitle;
            pbe.ShowType = ShowType.ToInt();
            pbe.Sort = Sort.ToInt();
            if (Width.IsInt())
            {
                pbe.Width = Width.ToInt();
            }
            else
            {
                pbe.Width = null;
            }
            pbe.DataType = DataType.ToInt(0);

            if (isadd)
            {
                PBE.Add(pbe);
            }
            else
            {
                PBE.Update(pbe);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='Set_Export.aspx" + Request.Url.Query + "';", true);
        }
    }
}