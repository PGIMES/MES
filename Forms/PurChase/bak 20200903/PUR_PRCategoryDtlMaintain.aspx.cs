using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
public partial class Forms_PurChase_PUR_PRCategoryDtlMaintain : System.Web.UI.Page
{
    public string isIT="false";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
            Session["LogUser"] = LogUserModel;
            Session["UserAD"] = LogUserModel.ADAccount;
            Session["UserId"] = LogUserModel.UserId;
            if (!LogUserModel.DepartName.Contains("IT"))
            {
                isIT = "false";
                GridView1.Columns[8].Visible = false;
                setshow(false);
            }
            else
            {
                isIT = "true";
                GridView1.Columns[8].Visible = true;
                setshow(true);
            }


        }

    }

    private void setshow(bool bln)
    {
        for(int i = 0; i < GridView1.Rows.Count; i++)
        {
            (GridView1.Rows[i].FindControl("txtdeptcode") as TextBox).Visible=bln;
            (GridView1.Rows[i].FindControl("lbldeptcode") as Label).Visible = !bln;
            (GridView1.Rows[i].FindControl("txtdeptname") as TextBox).Visible = bln;
            (GridView1.Rows[i].FindControl("lbldeptname") as Label).Visible =! bln;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
            

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
        GridViewRow row = null;
        switch (e.CommandName)
        {
            case "save": // 模板列
                // 对于模板列内的按钮，我们需要显示绑定行索引到按钮的 CommandArgument 属性
                // 以获取触发事件的行信息
                rowIndex = Convert.ToInt32(e.CommandArgument);
                row = GridView1.Rows[rowIndex];
                var id= row.Cells[0].Text.Trim();
                var deptname= row.FindControl("txtdeptname") as TextBox;
                var deptcode= row.FindControl("txtdeptcode") as TextBox;
                var UpdateCommand = "UPDATE PUR_PR_Category_dtl SET deptcode = '{0}', deptname = '{1}' WHERE (ID = '{2}')";
                DbHelperSQL.ExecuteSql(string.Format(UpdateCommand, deptcode.Text, deptname.Text, id));
                Response.Write("<script>alert('ok');</script>");
                break;
             
             
        }

         
    }
}