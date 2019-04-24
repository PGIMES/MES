using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YaSheTou_YST_Maintain_Modify : System.Web.UI.Page
{
    public string UserId = "";
    public string UserName = "";
    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        UserId = LogUserModel.UserId;
        UserName = LogUserModel.UserName;
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        string re_flag = "";

        DataTable dt_flag = DbHelperSQL.Query("select * from [qad].[dbo].[qad_pt_mstr] where pt_part like 'Z%' and pt_part='" + txt_part.Text + "'").Tables[0];
        if (dt_flag == null)
        {
            re_flag = "物料号" + txt_part.Text + "不存在，不能新增！";
        }
        else
        {
            if (dt_flag.Rows.Count <= 0)
            {
                re_flag = "物料号" + txt_part.Text + "不存在，不能新增！";
            }
        }
        if (re_flag!="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('" + re_flag + "',function(index) {layer.close(index);$(\"select[id *= 'ddl_zj']\").trigger('change');})", true);
            return;
        }


        //DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Base_Select] 2,'','" + lbl_zj.Text + txt_zj.Text + "',"
        //    + txt_mc.Text + ",'" + ddl_gys.SelectedValue + "','" + UserId + "','" + UserName + "'").Tables[0];
        DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Base_Select] 2,'','" + ddl_zj.SelectedValue + "',"
           + txt_mc.Text + ",'" + ddl_gys.SelectedValue + "','" + txt_part.Text + "','" + UserId + "','" + UserName + "'").Tables[0];

        string msg = ""; string lsstr = "";
        if (dt.Rows[0][0].ToString() == "Y")
        {
            msg = "保存失败：直径" + ddl_zj.SelectedValue + "，供应商" + ddl_gys.SelectedValue + "，物料号" + txt_part.Text + "已经存在，不能新增！";
            lsstr = "layer.alert('" + msg + "',function(index) {layer.close(index);})";
        }
        if (dt.Rows[0][0].ToString() == "N")
        {
            msg = "保存成功！";
            lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }

}