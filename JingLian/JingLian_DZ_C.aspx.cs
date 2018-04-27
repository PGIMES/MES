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
using Maticsoft.DBUtility;

public partial class JingLian_JingLian_DZ_C : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            txt_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txt_time.Text = DateTime.Now.ToString("HH:mm:ss");
            string devicename = DbHelperSQL.GetSingle("SELECT equip_name from MES_Equipment WHERE gongwei='熔炼' AND equip_no='" + Request["deviceid"] + "'").ToString();
            txt_luhao.Text = devicename;

            if (Function_Jinglian.Emplogin_query(6, "", Request["deviceid"]).Rows[0][0].ToString() == "1")
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(5, "", Request["deviceid"]);
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                txt_shift.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_banbie"].ToString();
                txt_name.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_name"].ToString();
                txt_banzu.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_banzhu"].ToString();


            }
            else
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(5, "", Request["deviceid"]);
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                this.txt_gh.Items.Insert(0, new ListItem("", ""));
                txt_gh.BackColor = System.Drawing.Color.Yellow;
            }
            btn_end.Enabled = false;
            btn_end.CssClass = "btn btn-large btn-primary disabled";
            ddl_check.Enabled = false;
            txt_before_wendu.ReadOnly = true;
            txt_jljuse.ReadOnly = true;
            txt_czjuse.ReadOnly = true;
            txt_after_wendu.ReadOnly = true;
            ddl_check.BackColor = System.Drawing.Color.WhiteSmoke;
            ddl_check.CssClass = "form-control input-s-sm ";


        }
    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_shift.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(5, txt_gh.Text, Request["deviceid"]).Rows[0]["emp_banzhu"].ToString();
    }
    protected void btn_begin_Click(object sender, EventArgs e)
    {
        if (txt_gh.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择员工工号！')", true);
            return;
        }
        txt_before_wendu.BackColor = System.Drawing.Color.Yellow;
        txt_jljuse.BackColor = System.Drawing.Color.Yellow;
        txt_czjuse.BackColor = System.Drawing.Color.Yellow;
        ddl_check.BackColor = System.Drawing.Color.Yellow;
        txt_after_wendu.BackColor = System.Drawing.Color.Yellow;
        btn_begin.Enabled = false;
        btn_end.Enabled = true;
        btn_begin.CssClass = "btn btn-large btn-primary disabled";
        btn_end.CssClass = "btn btn-large btn-primary ";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "setInterval('show_cur_times()', 100);", true);
        lb_start.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        div2.Style.Add("display", "block");
        txt_before_wendu.Text = "";
        txt_jljuse.Text = "";
        txt_czjuse.Text = "";
        ddl_check.Text = "";
        txt_after_wendu.Text = "";
        ddl_check.Enabled = true;
        txt_before_wendu.ReadOnly = false;
        txt_jljuse.ReadOnly = false;
        txt_czjuse.ReadOnly = false;
        txt_after_wendu.ReadOnly = false;
    }
    protected void btn_end_Click(object sender, EventArgs e)
    {
        if (txt_before_wendu.Text == "" || txt_jljuse.Text == "" || txt_czjuse.Text == "" || ddl_check.Text == "" || txt_after_wendu.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('有未填项，请确认！')", true);
            return;
        }
        int result = Function_Jinglian.jldz_insert(1, txt_gh.Text, txt_name.Text, txt_shift.Text, txt_banzu.Text, Request["deviceid"], txt_luhao.Text, txt_before_wendu.Text, txt_jljuse.Text, txt_czjuse.Text, ddl_check.Text, txt_after_wendu.Text, lb_start.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        if (result >= 1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('" + txt_luhao.Text + "精炼打渣完成！')", true);
        }
        btn_end.Enabled = false;
        btn_end.CssClass = "btn btn-large btn-primary disabled";
        btn_begin.Enabled = true;
        btn_begin.CssClass = "btn btn-large btn-primary ";
        div2.Style.Add("display", "none");
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void txt_jljuse_TextChanged(object sender, EventArgs e)
    {
        decimal f = 0;
        if (decimal.TryParse(txt_jljuse.Text, out f) == true && decimal.TryParse(txt_jljuse.Text, out f) == true)
        {
            if (Convert.ToDecimal(txt_jljuse.Text) < 6 || Convert.ToDecimal(txt_jljuse.Text) >8)
            {
                txt_jljuse.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                txt_jljuse.BackColor = System.Drawing.Color.Yellow;
            }
        }

    }
    protected void txt_czjuse_TextChanged(object sender, EventArgs e)
    {
        decimal f = 0;
        if (decimal.TryParse(txt_czjuse.Text, out f) == true && decimal.TryParse(txt_czjuse.Text, out f) == true)
        {
            if (Convert.ToDecimal(txt_czjuse.Text) < 10 || Convert.ToDecimal(txt_czjuse.Text) > 15)
            {
                txt_czjuse.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                txt_czjuse.BackColor = System.Drawing.Color.Yellow;
            }
        }

    }
}