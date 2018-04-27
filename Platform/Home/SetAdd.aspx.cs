using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Home
{
    public partial class SetAdd : Common.BasePage
    {
        protected RoadFlow.Platform.HomeItems BHI = new RoadFlow.Platform.HomeItems();
        protected RoadFlow.Data.Model.HomeItems hi = null;
        protected string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                hi = BHI.Get(id.ToGuid());
            }
            string type = string.Empty;
            string ds = string.Empty;
            string dbconnid = string.Empty;
            if (!IsPostBack)
            {
                if (hi != null)
                {
                    this.Name1.Value = hi.Name;
                    this.Title1.Value = hi.Title;
                    type = hi.Type.ToString();
                    ds = hi.DataSourceType.ToString();
                    this.DataSource.Value = hi.DataSource;
                    this.BgColor.Value = hi.BgColor;
                    this.Ico.Value = hi.Ico;
                    this.UseOrganizes.Value = hi.UseOrganizes;
                    dbconnid = hi.DBConnID.ToString();
                    this.LinkURL.Value = hi.LinkURL;
                    this.Note.Value = hi.Note;
                    this.Sort.Value = hi.Sort.ToString();
                    if (!hi.Sort.HasValue)
                    {
                        this.Sort.Value = BHI.GetMaxSort(hi.Type).ToString();
                    }
                }
                this.TypeOptions.Text = BHI.getTypeOptions(type);
                this.DataSourceTypeOptions.Text = BHI.getDataSourceOptions(ds);
                this.DBConnIDOptions.Text = new RoadFlow.Platform.DBConnection().GetAllOptions(dbconnid);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Name1 = Request.Form["Name1"];
            string Title1 = Request.Form["Title1"];
            string Type = Request.Form["Type"];
            string DataSourceType = Request.Form["DataSourceType"];
            string DataSource = Request.Form["DataSource"];
            string Ico = Request.Form["Ico"];
            string BgColor = Request.Form["BgColor"];
            string UseOrganizes = Request.Form["UseOrganizes"];
            string DBConnID = Request.Form["DBConnID"];
            string LinkURL = Request.Form["LinkURL"];
            string Note = Request.Form["Note"];
            string Sort = Request.Form["Sort"];

            bool isAdd = false;
            if (hi == null)
            {
                hi = new RoadFlow.Data.Model.HomeItems();
                hi.ID = Guid.NewGuid();
                isAdd = true;
            }
            hi.Title = Title1;
            hi.Name = Name1;
            hi.Type = Type.ToInt();
            hi.DataSourceType = DataSourceType.ToInt();
            hi.DataSource = DataSource;
            hi.Ico = Ico;
            hi.BgColor = BgColor;
            hi.UseOrganizes = UseOrganizes;
            hi.Sort = Sort.IsInt() ? Sort.ToInt() : BHI.GetMaxSort(hi.Type);
            if (DBConnID.IsGuid())
            {
                hi.DBConnID = DBConnID.ToGuid();
            }
            else
            {
                hi.DBConnID = null;
            }
            hi.LinkURL = LinkURL;
            hi.Note = Note;
            if (isAdd)
            {
                BHI.Add(hi);
            }
            else
            {
                BHI.Update(hi);
            }
            BHI.ClearCache();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');window.location='SetList.aspx" + Request.Url.Query + "';", true);
        }
    }
}