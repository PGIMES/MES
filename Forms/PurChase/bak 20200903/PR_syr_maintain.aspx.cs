using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_PR_syr_maintain : System.Web.UI.Page
{
    protected string PRNo = "";
    protected string rowid = "";

    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        PRNo = Request.QueryString["PRNo"].ToString();
        rowid = Request.QueryString["rowid"].ToString();

        if (!IsPostBack)
        {
            txt_PRNo.Text = PRNo;
            txt_rowid.Text = rowid;

            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query(@"select main.prtype,dtl.* from pur_pr_main_form main inner join pur_pr_dtl_form dtl on main.prno=dtl.prno 
                                        where dtl.prno='" + txt_PRNo.Text + "' and dtl.rowid='" + txt_rowid.Text + "'").Tables[0];
        if (dt.Rows.Count == 1)
        {
            txt_PRNo.Text = dt.Rows[0]["PRNo"].ToString();
            txt_rowid.Text = dt.Rows[0]["rowid"].ToString();
            txt_PRType.Text = dt.Rows[0]["prtype"].ToString();
            txt_wlh.Text = dt.Rows[0]["wlh"].ToString();
            txt_wlmc.Text = dt.Rows[0]["wlmc"].ToString();
            txt_wlms.Text = dt.Rows[0]["wlms"].ToString();
            txt_syr.Text = dt.Rows[0]["syr"].ToString();
        }
        else
        {
            //txt_PRNo.Text = "";
            //txt_rowid.Text = "";
            txt_PRType.Text = "";
            txt_wlh.Text = "";
            txt_wlmc.Text = "";
            txt_wlms.Text = "";
            txt_syr.Text = "";
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        Pgi.Auto.Common ls_update = new Pgi.Auto.Common();
        string sql_update = @"update PUR_PR_Dtl_Form set syr='{2}' where prno='{0}' and rowid='{1}'";
        sql_update = string.Format(sql_update, txt_PRNo.Text, txt_rowid.Text, txt_syr.Text);
        ls_update.Sql = sql_update;
        ls_sum.Add(ls_update);

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        string msg = "";
        if (ln > 0)
        {
            msg = "确认成功！";
        }
        else
        {
            msg = "确认失败！";
        }
        //string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        string lsstr = "layer.alert('" + msg + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }

    protected void txt_PRNo_TextChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}