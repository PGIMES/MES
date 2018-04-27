using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.DBConnection
{
    public partial class Edit : Common.BasePage
    {
        RoadFlow.Platform.DBConnection bdbConn = new RoadFlow.Platform.DBConnection();
        RoadFlow.Data.Model.DBConnection dbconn = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string editid = Request.QueryString["id"];
            if (editid.IsGuid())
            {
                dbconn = bdbConn.Get(editid.ToGuid());
            }

            if (!IsPostBack)
            {
                string connType = string.Empty;
                if (dbconn != null)
                {
                    this.Name.Value = dbconn.Name;
                    this.ConnStr.Value = dbconn.ConnectionString;
                    this.Note.Value = dbconn.Note;
                    connType = dbconn.Type;
                }
                this.TypeOptions.Text = bdbConn.GetAllTypeOptions(connType);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Name = Request.Form["Name"];
            string LinkType = Request.Form["LinkType"];
            string ConnStr = Request.Form["ConnStr"];
            string Note = Request.Form["Note"];
            bool isAdd = false;
            string oldXML = string.Empty;
            if (dbconn == null)
            {
                isAdd = true;
                dbconn = new RoadFlow.Data.Model.DBConnection();
                dbconn.ID = Guid.NewGuid();
            }
            else
            {
                oldXML = dbconn.Serialize();
            }
            dbconn.Name = Name.Trim();
            dbconn.Type = LinkType;
            dbconn.ConnectionString = ConnStr;
            dbconn.Note = Note;
            
            if (isAdd)
            {
                bdbConn.Add(dbconn);
                RoadFlow.Platform.Log.Add("添加了数据连接", dbconn.Serialize(), RoadFlow.Platform.Log.Types.数据连接);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
            }
            else
            {
                bdbConn.Update(dbconn);
                RoadFlow.Platform.Log.Add("修改了数据连接", "", RoadFlow.Platform.Log.Types.数据连接, oldXML, dbconn.Serialize());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('修改成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
            }
            bdbConn.ClearCache();
        }
    }
}