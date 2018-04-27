using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.ProgramBuilder
{
    public partial class Set_Validate : Common.BasePage
    {
        string pid = string.Empty;
        RoadFlow.Platform.ProgramBuilderValidates PBV = new RoadFlow.Platform.ProgramBuilderValidates();
        protected List<RoadFlow.Data.Model.ProgramBuilderValidates> validateList = new List<RoadFlow.Data.Model.ProgramBuilderValidates>();
        List<RoadFlow.Data.Model.ProgramBuilderValidates> validateList1 = new List<RoadFlow.Data.Model.ProgramBuilderValidates>();
        List<Tuple<string, string, string>> filedList = new List<Tuple<string, string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["pid"];
            #region 从表单设置中加载表字段
            if (pid.IsGuid())
            {
                var pro = new RoadFlow.Platform.ProgramBuilder().Get(pid.ToGuid());
                if (pro != null && !pro.FormID.IsNullOrEmpty() && pro.FormID.IsGuid())
                {

                    var applibary = new RoadFlow.Platform.AppLibrary().Get(pro.FormID.ToGuid());
                    if (applibary != null && applibary.Code.IsGuid())
                    {
                        var proform = new RoadFlow.Platform.WorkFlowForm().Get(applibary.Code.ToGuid());
                        if (proform != null)
                        {
                            LitJson.JsonData formAttr = LitJson.JsonMapper.ToObject(proform.Attribute);
                            string dbconn = formAttr.ContainsKey("dbconn") ? formAttr["dbconn"].ToString() : "";
                            string dbtable = formAttr.ContainsKey("dbtable") ? formAttr["dbtable"].ToString() : "";
                            if (dbconn.IsGuid() && !dbtable.IsNullOrEmpty())
                            {
                                var mainTableFields = new RoadFlow.Platform.DBConnection().GetFields(dbconn.ToGuid(), dbtable);
                                foreach (var field in mainTableFields)
                                {
                                    filedList.Add(new Tuple<string, string, string>(dbtable, field.Key, field.Value));
                                }
                            }
                            LitJson.JsonData subtables = LitJson.JsonMapper.ToObject(proform.SubTableJson);
                            if (subtables.IsArray)
                            {
                                foreach (LitJson.JsonData jd in subtables)
                                {
                                    string secondtable = jd.ContainsKey("secondtable") ? jd["secondtable"].ToString() : "";
                                    if (jd.ContainsKey("colnums"))
                                    {
                                        LitJson.JsonData colnums = jd["colnums"];
                                        if (colnums.IsArray)
                                        {
                                            foreach (LitJson.JsonData col in colnums)
                                            {
                                                string fieldname = col.ContainsKey("fieldname") ? col["fieldname"].ToString() : "";
                                                string showname = col.ContainsKey("showname") ? col["showname"].ToString() : "";
                                                if (fieldname.IsNullOrEmpty())
                                                {
                                                    continue;
                                                }
                                                filedList.Add(new Tuple<string, string, string>(secondtable, fieldname, showname));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            validateList1 = PBV.GetAll(pid.ToGuid());
            foreach (var filed in filedList)
            {
                var val = validateList1.Find(p => p.TableName.Equals(filed.Item1, StringComparison.CurrentCultureIgnoreCase) &&
                    p.FieldName.Equals(filed.Item2, StringComparison.CurrentCultureIgnoreCase));
                validateList.Add(new RoadFlow.Data.Model.ProgramBuilderValidates()
                {
                    ID = Guid.NewGuid(),
                    ProgramID = pid.ToGuid(),
                    TableName = filed.Item1,
                    FieldName = filed.Item2,
                    FieldNote = filed.Item3,
                    Validate = val != null ? val.Validate : 0
                });
            }
        }

        protected string getValidateOptions(int value)
        {
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(0, "不检查");
            dicts.Add(1, "允许为空,非空时检查");
            dicts.Add(2, "不允许为空,并检查");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var dict in dicts)
            {
                sb.Append("<option value='" + dict.Key + "'" + (value == dict.Key ? " selected='selected'" : "") + ">" + dict.Value + "</option>");
            }
            return sb.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                PBV.DeleteByProgramID(pid.ToGuid());
                foreach (var val in validateList)
                {
                    val.Validate = Request.Form["valdate_" + val.TableName + "_" + val.FieldName].ToInt(0);
                    PBV.Add(val);
                }
                scope.Complete();
            }
        }
    }
}