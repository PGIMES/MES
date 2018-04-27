using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class JingLian_ZYB_Clear : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            txt_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txt_time.Text = DateTime.Now.ToString("HH:mm:ss");

            if (Function_Jinglian.Emplogin_query(4, "","").Rows[0][0].ToString() == "1")
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(3, "","");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                txt_shift.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banbie"].ToString();
                txt_name.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_name"].ToString();
                txt_banzu.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banzhu"].ToString();

                this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_zyno_Query("1", txt_date.Text, txt_shift.Text);
                this.ddl_zybh.DataTextField = "object";
                this.ddl_zybh.DataValueField = "object";
                this.ddl_zybh.DataBind();
                GetResult();
            }
            else
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(3, "","");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                this.txt_gh.Items.Insert(0, new ListItem("", ""));
                txt_gh.BackColor = System.Drawing.Color.Yellow;
            }
            DIV1.Style.Add("display", "none");
            txt_cz.BackColor = System.Drawing.Color.Yellow;

            GetContent();
            DataTable dt_result = Function_Jinglian.ZybClear_Query(5, "", "", "", "", txt_shift.Text, "");
            GridView2.DataSource = dt_result;
            GridView2.DataBind();
           // GetResult();
        }

    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        txt_shift.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banzhu"].ToString();
        this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_zyno_Query("1", txt_date.Text, txt_shift.Text);
        this.ddl_zybh.DataTextField = "object";
        this.ddl_zybh.DataValueField = "object";
        this.ddl_zybh.DataBind();
       // GetResult();
    }

    private void GetContent()
    {
        DataTable dtcontent = Function_Jinglian.ZybClear_Content_Query("1");
        this.GridView1.DataSource = dtcontent;
        this.GridView1.DataBind();
    }

    private void GetResult()
    {
        DIV1.Style.Add("display", "block");
        btn_zyb_1.Enabled = false;
        btn_zyb_2.Enabled = false;
        btn_zyb_3.Enabled = false;
        btn_zyb_4.Enabled = false;
        DataTable dt = Function_Jinglian.ZybClear_zyno_Query("2", txt_date.Text, txt_shift.Text);
        if (dt.Rows[0]["b1"].ToString() == "NG")
        {
            btn_zyb_1.Text = "1#";
            btn_zyb_1.CssClass = "btn btn-large btn-danger disabled";
        }
        else
        {
            btn_zyb_1.Text = "1#";
            btn_zyb_1.CssClass = "btn btn-large btn-success disabled";
        }
        if (dt.Rows[0]["b2"].ToString() == "NG")
        {
            btn_zyb_2.Text = "2#";
            btn_zyb_2.CssClass = "btn btn-large btn-danger disabled";
        }
        else
        {
            btn_zyb_2.Text = "2#";
            btn_zyb_2.CssClass = "btn btn-large btn-success disabled";
        }
        if (dt.Rows[0]["b3"].ToString() == "NG")
        {
            btn_zyb_3.Text = "3#";
            btn_zyb_3.CssClass = "btn btn-large btn-danger disabled";
        }
        else
        {
            btn_zyb_3.Text = "3#";
            btn_zyb_3.CssClass = "btn btn-large btn-success disabled";
        }
        if (dt.Rows[0]["b4"].ToString() == "NG")
        {
            btn_zyb_4.Text = "4#";
            btn_zyb_4.CssClass = "btn btn-large btn-danger disabled";
        }
        else
        {
            btn_zyb_4.Text = "4#";
            btn_zyb_4.CssClass = "btn btn-large btn-success disabled";
        }
        
    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((DropDownList)this.GridView1.Rows[i].FindControl("ddl_result")).Text == "")
            {
                string itemno = GridView1.Rows[i].Cells[0].Text.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('清理项" + itemno + "未确认！请选择')", true);
                return;
            }

            if (((DropDownList)this.GridView1.Rows[i].FindControl("ddl_result")).Text == "NG" && ((TextBox)this.GridView1.Rows[i].FindControl("txt_remark")).Text == "")
            {
                string itemno = GridView1.Rows[i].Cells[0].Text.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('清理项" + itemno + "确认结果为NG！请输入说明')", true);
                return;
            }
        }
        if (txt_cz.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写转运包称重重量！')", true);
            return;
        }
        if (Function_Jinglian.ZybClear_Query(4,"", "", "", "",txt_shift.Text ,ddl_zybh.Text.Trim()).Rows.Count > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('转运包"+ddl_zybh.Text+"已清理，请选择其他转运包！')", true);
            return;
        }
        int result = Function_Jinglian.ZYBClear_insert(1, txt_gh.Text, txt_name.Text, txt_shift.Text, txt_banzu.Text, ddl_zybh.Text, ddl_zybh.Text, "转运包", ((DropDownList)this.GridView1.Rows[0].FindControl("ddl_result")).Text, ((DropDownList)this.GridView1.Rows[1].FindControl("ddl_result")).Text,
            ((DropDownList)this.GridView1.Rows[2].FindControl("ddl_result")).Text, ((DropDownList)this.GridView1.Rows[3].FindControl("ddl_result")).Text, txt_cz.Text, ((TextBox)this.GridView1.Rows[0].FindControl("txt_remark")).Text.ToString(), ((TextBox)this.GridView1.Rows[1].FindControl("txt_remark")).Text.ToString(), ((TextBox)this.GridView1.Rows[2].FindControl("txt_remark")).Text.ToString(), ((TextBox)this.GridView1.Rows[3].FindControl("txt_remark")).Text.ToString());

        if (result >= 1)
        {   
            //GetResult();
            string zybh = ddl_zybh.Text;
            DataTable dt_result = Function_Jinglian.ZybClear_Query(5, "", "", "", "", txt_shift.Text, "");
            GridView2.DataSource = dt_result;
            GridView2.DataBind();
            string str = "layer.confirm('" + zybh + "转运包已清理完成，是否需要清理下一个转运包？', {  btn: ['是','否'] }, function(){  $('#MainContent_btnNext').click();}, function(){  });";
             Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
             
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {

        //GridView2.DataSource = null;
        //GridView2.DataBind();
            GetContent();
           // GetResult();
            this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_zyno_Query("1", txt_date.Text, txt_shift.Text);
            this.ddl_zybh.DataTextField = "object";
            this.ddl_zybh.DataValueField = "object";
            this.ddl_zybh.DataBind();
            txt_cz.Text = "";
    }
    protected void ddl_zybh_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((DropDownList)this.GridView1.Rows[i].FindControl("ddl_result")).Text = "";
           ((TextBox)this.GridView1.Rows[i].FindControl("txt_remark")).Text = "";
           txt_cz.Text = "";
        }
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}