using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Forms_PurChase_PUR_RCT_PO_FW_Sure_fw : System.Web.UI.Page
{
    protected string rctno = "";
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        rctno = Request.QueryString["rctno"].ToString();

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        DataSet ds = DbHelperSQL.Query(" exec [Pur_RCT_PO_FW_Sure_fw] '" + rctno + "'");
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        GetGrid(ds.Tables[0]);

    }

    protected void GetGrid(DataTable DT)
    {
        DataTable ldt = DT;
        int index = gv.VisibleRowCount;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            DevExpress.Web.ASPxComboBox tb_kjkm_code = (DevExpress.Web.ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["kjkm_code"], "kjkm_code");
            DataTable ldt_kjkm_code = DbHelperSQL.Query(@"SELECT [GL_ID],[GLCode],[GLDescription] FROM [qad].[dbo].[qad_gl_all]").Tables[0];

            tb_kjkm_code.DataSource = ldt_kjkm_code;
            tb_kjkm_code.ValueField = "GLCode";
            tb_kjkm_code.Columns.Add("GLCode", "会计科目", 60);
            tb_kjkm_code.Columns.Add("GLDescription", "会计描述", 140);
            tb_kjkm_code.TextFormatString = "{0}|{1}";
            tb_kjkm_code.DataBind();
            tb_kjkm_code.Value = ldt.Rows[i]["kjkm_code"].ToString();
        }
    }

    protected void FPAmount_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt_2 = Pgi.Auto.Control.AgvToDt(this.gv);

        for (int i = 0; i < ldt_2.Rows.Count; i++)
        {
            decimal po_notax_TotalPrice = ldt_2.Rows[i]["po_notax_TotalPrice"].ToString() == "" ? 0 : Convert.ToDecimal(ldt_2.Rows[i]["po_notax_TotalPrice"].ToString());
            decimal FPAmount = ldt_2.Rows[i]["FPAmount"].ToString() == "" ? 0 : Convert.ToDecimal(ldt_2.Rows[i]["FPAmount"].ToString());
            ldt_2.Rows[i]["chayi"] = (FPAmount - po_notax_TotalPrice).ToString("0.00");
        }
        ldt_2.AcceptChanges();
        gv.DataSource = ldt_2;
        gv.DataBind();
        GetGrid(ldt_2);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        if (ldt.Rows.Count <= 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "财务确认信息不可为空!");
            return;
        }

        string msg = "";
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["FPAmount"].ToString() == "")
            {
                msg += "行号" + ldt.Rows[i]["FPAmount"].ToString() + "，发票金额不可为空!<br />";
            }
            if (ldt.Rows[i]["kjkm_code"].ToString() == "")
            {
                msg += "行号" + ldt.Rows[i]["kjkm_code"].ToString() + "，总账账户不可为空!<br />";
            }
        }

        if (msg != "")
        {
            Pgi.Auto.Public.MsgBox(this, "alert", msg);
            return;
        }

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        string sql_insert = @"update PUR_RCT_PO_FW set OptionType='已匹配',fw_qr_time=getdate()
                                                    ,qad_fp_no='{1}'+CONVERT(nvarchar(4),getdate(),12)
                                                                +right('000'+cast(
																	                (select isnull(max(cast(right(qad_fp_no,3) as int)),0) from PUR_RCT_PO_FW 
                                                                                    where qad_fp_no like '{1}'+CONVERT(nvarchar(4),getdate(),12)+'%'
                                                                                    )
																                +1 as nvarchar(max)),3)
                                                    ,FPAmount='{2}',kjkm_code='{3}',kjkm_name='{4}'
                            where rctno='{0}'";

        //明细数据自动生成SQL，并增入SUM
        foreach (DataRow item in ldt.Rows)
        {
            Pgi.Auto.Common ls_insert = new Pgi.Auto.Common();
            string sql = "";

            string code_f = item["domain"].ToString() == "200" ? "K" : "S";
            string kjkm_code = item["kjkm_code"].ToString().Substring(0, item["kjkm_code"].ToString().IndexOf("|"));
            string kjkm_name = item["kjkm_code"].ToString().Substring(item["kjkm_code"].ToString().IndexOf("|") + 1);
            sql = string.Format(sql_insert, item["rctno"], code_f + "F", item["FPAmount"], kjkm_code, kjkm_name);

            ls_insert.Sql = sql;
            ls_sum.Add(ls_insert);
        }

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        string remsg = "";
        if (ln > 0)
        {
            remsg = "确认成功！";
        }
        else
        {
            remsg = "确认失败！";
        }
        //string lsstr = "layer.alert('" + remsg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        string lsstr = "layer.alert('" + remsg + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }


}