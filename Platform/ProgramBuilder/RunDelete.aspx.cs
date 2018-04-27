using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class RunDelete : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formid = Request.QueryString["secondtableeditform"];
            string instanceid = Request.QueryString["instanceid"];
            var app = new RoadFlow.Platform.AppLibrary().Get(formid.ToGuid());
            if (app != null)
            {
                var wform = new RoadFlow.Platform.WorkFlowForm().Get(app.Code.ToGuid());
                if (wform != null)
                {
                    RoadFlow.Platform.DBConnection dbconn = new RoadFlow.Platform.DBConnection();
                    LitJson.JsonData json = LitJson.JsonMapper.ToObject(wform.Attribute);
                    var dbconnid = json.ContainsKey("dbconn") ? json["dbconn"].ToString() : "";
                    var dbtable = json.ContainsKey("dbtable") ? json["dbtable"].ToString() : "";
                    var dbtablepk = json.ContainsKey("dbtablepk") ? json["dbtablepk"].ToString() : "";

                    if (dbconnid.IsGuid() && !dbtable.IsNullOrEmpty() && !dbtablepk.IsNullOrEmpty())
                    {
                        foreach (string delid in (instanceid ?? "").Split(','))
                        {
                            var dt = dbconn.GetDataTable(dbconnid, dbtable, dbtablepk, delid);
                            if (dt.Rows.Count > 0)
                            {
                                dbconn.DeleteData(dbconnid.ToGuid(), dbtable, dbtablepk, delid);
                                RoadFlow.Platform.Log.Add("删除了数据(生成程序)(" + dbtable + ")", "连接ID:" + dbconnid + ",表名:" + dbtable + ",主键:" + delid, RoadFlow.Platform.Log.Types.其它分类, dt.ToJsonString());
                            }
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "del", "alert('删除成功!');window.location='" + Request.QueryString["prevurl"] + "'", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "del", "alert('删除失败,未找到表单数据连接信息!');window.location='" + Request.QueryString["prevurl"] + "'", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "del", "alert('删除失败,未找到对应的表单!');window.location='" + Request.QueryString["prevurl"] + "'", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "del", "alert('删除失败,未找到应用程序库!');window.location='" + Request.QueryString["prevurl"] + "'", true);
            }
            
        }
    }
}