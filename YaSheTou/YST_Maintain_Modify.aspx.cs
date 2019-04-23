using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
        DataTable dt = DbHelperSQL.Query("exec [usp_MES_YaSheTou_Base_Select] 2,'','" + lbl_zj.Text + txt_zj.Text + "',"
            + txt_mc.Text + ",'" + ddl_gys.SelectedValue + "','" + UserId + "','" + UserName + "'").Tables[0];

        string msg = "";
        if (dt.Rows[0][0].ToString() == "Y")
        {
            msg = "保存失败：直径" + txt_zj.Text + "，供应商" + ddl_gys.SelectedValue + "已经存在，不能新增！";
        }
        if (dt.Rows[0][0].ToString() == "N")
        {
            msg = "保存成功！";
        }
        string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }
}