using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class PW_PW_Clear : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    Function_PW PW = new Function_PW();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            ;
            this.txtShiJian.Value = DateTime.Now.ToString("HH:ss:mm");
            //初始话下拉登入此台设备人员
            //string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 ";
            string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
            txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
            txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
            txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            string sql = "select equip_name from MES_Equipment where gongwei='抛丸'and  equip_no='" + Request["deviceid"] + "' ";
            DataTable db = DbHelperSQL.Query(sql).Tables[0];
            sbname.Text = db.Rows[0][0].ToString();
            GetContent();

        }
    }

    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 and emp_no='" + dropGongHao.SelectedValue + "'";
        DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
        txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
        txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
        txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
    }

    private void GetContent()
    {
        DataTable dtcontent = Function_Jinglian.ZybClear_Content_Query("11");
        this.GridView1.DataSource = dtcontent;
        this.GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GetBase GetBase = new GetBase();
            //DataTable dt1 = GetBase.GetPwResult("checkitem='钢灰清扫(天)' and equip_no='" + Request["deviceid"] + "'").Tables[0];
            DataTable dt1 = GetBase.GetPwResult("checkitem='钢灰清扫(天)' and equip_no='" + Request["deviceid"] + "' and emp_banbie='" + txtBanBie.Value + "'").Tables[0];
            DataTable dt2 = GetBase.GetPwsxResult("checkitem='钢丸筛选(周)' and equip_no= '" + Request["deviceid"] + "'").Tables[0];

            if ( dt1.Rows.Count > 0)
            {
                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).DataSource =dt1;
                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).DataTextField = "checkresult";
                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).DataValueField = "checkresult";
                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).DataBind();

                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).CssClass = "form-control input-s-sm ";
                ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).Enabled = false;
                ((TextBox)this.GridView1.Rows[0].FindControl("txt_remark")).Text = dt1.Rows[0]["checkdemo"].ToString();
                ((TextBox)this.GridView1.Rows[0].FindControl("txt_remark")).CssClass = "form-control input-s-sm  disabled";
                ((TextBox)this.GridView1.Rows[0].FindControl("txt_remark")).Enabled = false;
                ((Button)this.GridView1.Rows[0].FindControl("btn_clear")).CssClass = "btn btn-large btn-primary  disabled";
                ((Button)this.GridView1.Rows[0].FindControl("btn_clear")).Enabled = false;
            }
            if (dt2.Rows.Count > 0)
            {
                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).DataSource = dt2;
                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).DataTextField = "checkresult";
                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).DataValueField = "checkresult";
                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).DataBind();

                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).CssClass = "form-control input-s-sm ";
                ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).Enabled = false;
                ((TextBox)this.GridView1.Rows[1].FindControl("txt_remark")).Text = dt2.Rows[0]["checkdemo"].ToString();
                ((TextBox)this.GridView1.Rows[1].FindControl("txt_remark")).CssClass = "form-control input-s-sm  disabled";
                ((TextBox)this.GridView1.Rows[1].FindControl("txt_remark")).Enabled = false;
                ((Button)this.GridView1.Rows[1].FindControl("btn_clear")).CssClass = "btn btn-large btn-primary  disabled";
                ((Button)this.GridView1.Rows[1].FindControl("btn_clear")).Enabled = false;
            }
            
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        string checkitem = "";
        string checkdemo = "";
        string checkresult = "";
        int result = 0;

        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;

        checkitem = ((Label)this.GridView1.Rows[lnindex].FindControl("lb_item")).Text;
        checkresult = ((DropDownList)this.GridView1.Rows[lnindex].FindControl("ddl_result")).SelectedValue;
        checkdemo = ((TextBox)this.GridView1.Rows[lnindex].FindControl("txt_remark")).Text;

        if (checkresult == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择清理结果！')", true);
            return;
        }
        //result = PW.PW_Clear_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, Request["deviceid"] , sbname.Text, checkitem, checkresult, checkdemo);
        result = PW.PW_Clear_Insert(dropGongHao.SelectedValue.Trim(), txtXingMing.Value.Trim(), txtBanBie.Value.Trim(), txtBanZu.Value.Trim(), Request["deviceid"].Trim(), sbname.Text.Trim(), checkitem.Trim(), checkresult.Trim(), checkdemo.Trim());


        if (result >= 1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('清理成功！')", true);
            ((DropDownList)this.GridView1.Rows[lnindex].FindControl("ddl_result")).CssClass = "form-control input-s-sm  disabled";
            ((DropDownList)this.GridView1.Rows[lnindex].FindControl("ddl_result")).Enabled = false;
            ((TextBox)this.GridView1.Rows[lnindex].FindControl("txt_remark")).CssClass = "form-control input-s-sm  disabled";
            ((TextBox)this.GridView1.Rows[lnindex].FindControl("txt_remark")).Enabled = false;
            ((Button)this.GridView1.Rows[lnindex].FindControl("btn_clear")).CssClass = "btn btn-large btn-primary  disabled";
            ((Button)this.GridView1.Rows[lnindex].FindControl("btn_clear")).Enabled = false;
        }
        
    }
}