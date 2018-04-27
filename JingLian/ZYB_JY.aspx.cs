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

public partial class JingLian_ZYB_JY : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    DataTable dt_jy;
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

                this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_zyno_Query("3", txt_date.Text, txt_shift.Text);
                this.ddl_zybh.DataTextField = "HD_zybno";
                this.ddl_zybh.DataValueField = "HD_zybno";
                this.ddl_zybh.DataBind();
                this.ddl_zybh.Items.Insert(0, new ListItem("", ""));
                
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

            this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_Content_Query("4");
            this.ddl_zybh.DataTextField = "HD_zybno";
            this.ddl_zybh.DataValueField = "HD_zybno";
            this.ddl_zybh.DataBind();
            this.ddl_zybh.Items.Insert(0, new ListItem("", ""));

            this.ddl_zyb.DataSource = Function_Jinglian.ZybClear_Content_Query("3");
            this.ddl_zyb.DataTextField = "equip_no";
            this.ddl_zyb.DataValueField = "equip_no";
            this.ddl_zyb.DataBind();
            this.ddl_zyb.Items.Insert(0, new ListItem("", ""));

            this.ddl_luhao.DataSource = Function_Jinglian.ZybClear_Content_Query("5");
            this.ddl_luhao.DataTextField = "equip_no";
            this.ddl_luhao.DataValueField = "equip_no";
            this.ddl_luhao.DataBind();
            this.ddl_luhao.Items.Insert(0, new ListItem("", ""));

            this.ddl_hejin.DataSource = Function_Jinglian.Hydrogen_Query(4, "", "", "", "", "", "", "");
            this.ddl_hejin.DataTextField = "base_value";
            this.ddl_hejin.DataValueField = "base_value";
            this.ddl_hejin.DataBind();
            this.ddl_hejin.Items.Insert(0, new ListItem("", ""));

            ddl_zybh.BackColor = System.Drawing.Color.Yellow;
            ddl_zyb.BackColor = System.Drawing.Color.Yellow;
            //ddl_hejin.BackColor = System.Drawing.Color.Yellow;
            ddl_luhao.BackColor = System.Drawing.Color.Yellow;
            
        }
    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_shift.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text,"").Rows[0]["emp_banzhu"].ToString();
    }
    
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (ddl_zybh.Text == "" || ddl_zyb.Text == "" || ddl_luhao.Text == "" || txt_gh.Text=="")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择工号、转运包序列号、转运包号、保温炉号！')", true);
            return;
        }

        int result = Function_Jinglian.ZYB_JY_Insert(1, txt_gh.Text.Trim(), txt_name.Text.Trim(), txt_banzu.Text.Trim(), txt_shift.Text.Trim(), ddl_luhao.Text.Trim(), ddl_zybh.Text.Trim(), ddl_hejin.Text.Trim(), ddl_zyb.Text.Trim());
        if (result >= 1)
        {
            DataTable dt_result = Function_Jinglian.ZYB_JY_query(1, "", "", ddl_zybh.Text, "", "","","","","");
            GridView1.DataSource = dt_result;
            GridView1.DataBind();

 
        }

    }

    protected void btn_finish_Click(object sender, EventArgs e)
    {
        string zybno=ddl_zybh.Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('转运包序列号" + zybno + "已加液完成！')", true);
        int result = Function_Jinglian.ZYB_JY_Insert(2, "", "", "","", "", ddl_zybh.Text.Trim(), "", "");
        this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_zyno_Query("3", txt_date.Text, txt_shift.Text);
        this.ddl_zybh.DataTextField = "HD_zybno";
        this.ddl_zybh.DataValueField = "HD_zybno";
        this.ddl_zybh.DataBind();
        this.ddl_zybh.Items.Insert(0, new ListItem("", ""));

        this.ddl_hejin.DataSource = Function_Jinglian.Hydrogen_Query(4, "", "", "", "", "", "", "");
        this.ddl_hejin.DataTextField = "base_value";
        this.ddl_hejin.DataValueField = "base_value";
        this.ddl_hejin.DataBind();
        this.ddl_hejin.Items.Insert(0, new ListItem("", ""));

        this.ddl_luhao.DataSource = Function_Jinglian.ZybClear_Content_Query("5");
        this.ddl_luhao.DataTextField = "equip_no";
        this.ddl_luhao.DataValueField = "equip_no";
        this.ddl_luhao.DataBind();
        this.ddl_luhao.Items.Insert(0, new ListItem("", ""));

        this.ddl_zyb.DataSource = Function_Jinglian.ZybClear_Content_Query("3");
        this.ddl_zyb.DataTextField = "equip_no";
        this.ddl_zyb.DataValueField = "equip_no";
        this.ddl_zyb.DataBind();
        this.ddl_zyb.Items.Insert(0, new ListItem("", ""));

        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void ddl_zybh_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_hejin.DataSource = Function_Jinglian.Hydrogen_Query(2, ddl_zybh.Text, "", "", "", "", "", "");
        this.ddl_hejin.DataTextField = "HD_hejin";
        this.ddl_hejin.DataValueField = "HD_hejin";
        this.ddl_hejin.DataBind();

        
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        this.ddl_zybh.DataSource = Function_Jinglian.ZybClear_Content_Query("4");
        this.ddl_zybh.DataTextField = "HD_zybno";
        this.ddl_zybh.DataValueField = "HD_zybno";
        this.ddl_zybh.DataBind();
        this.ddl_zybh.Items.Insert(0, new ListItem("", ""));
    }
}