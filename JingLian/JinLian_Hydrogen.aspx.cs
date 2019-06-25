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

public partial class JingLian_JinLian_Hydrogen : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    MES_JingLian_DAL jl_dal = new MES_JingLian_DAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //txt_mz.Value = "1400";
       // lb_jlno.Text = "精炼机1#";
        lb_jlno.Text = Request["gongwei"];
        if (!IsPostBack)
        {
            //div2.Style.Add("display", "none");

            txt_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txt_time.Text = DateTime.Now.ToString("HH:mm:ss");
          
            txt_zyno.CssClass = "form-control input-s-sm  ";


            if (Function_Jinglian.Emplogin_query(1, "","").Rows[0][0].ToString() == "1")
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(2, "","");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                txt_shift.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_banbie"].ToString();
                txt_name.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_name"].ToString();
                txt_banzu.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_banzhu"].ToString();
                

            }
            else
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(2, "","");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                this.txt_gh.Items.Insert(0, new ListItem("", ""));
                txt_gh.BackColor = System.Drawing.Color.Yellow;
            }

            this.txt_zybselect.DataSource = Function_Jinglian.zybno_query(2, "", "", "", "", "", "");
            this.txt_zybselect.DataTextField = "HD_zybno";
            this.txt_zybselect.DataValueField = "HD_zybno";
            this.txt_zybselect.DataBind();
            this.txt_zybselect.Items.Insert(0, new ListItem("", ""));

            this.txt_zybno.DataSource = Function_Jinglian.zybno_query(3, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text);
            this.txt_zybno.DataTextField = "zybno";
            this.txt_zybno.DataValueField = "zybno";
            this.txt_zybno.DataBind();


            this.txt_zyno.DataSource = Function_Jinglian.ZybClear_Content_Query("3");
            this.txt_zyno.DataTextField = "equip_no";
            this.txt_zyno.DataValueField = "equip_no";
            this.txt_zyno.DataBind();
            this.txt_zyno.Items.Insert(0, new ListItem("", ""));

            btn_bf_time.Enabled = false;
            btn_af_time.Enabled = false;
            //btn_zl_confirm.Enabled = false;
            btn_again.Enabled = false;
            btn_end.Enabled = false;
            btn_gpcs.Enabled = false;
            txt_luhao.BackColor = System.Drawing.Color.Yellow;
            txt_hejin.BackColor = System.Drawing.Color.Yellow;
            txt_zybselect.BackColor = System.Drawing.Color.Yellow;
            txt_kq.BackColor = System.Drawing.Color.Yellow;
            txt_water.BackColor = System.Drawing.Color.Yellow;
            txt_bmzt.BackColor = System.Drawing.Color.Yellow;
            btn_zl_confirm.Enabled = false;
            SetMap();
        }
      
       
    }
    protected void btn_begin_Click(object sender, EventArgs e)
    {
        SetMap();
        lb_af_time.Text = "";
        lb_bf_time.Text = "";
        btn_af_time.Enabled = false;
        txt_before_wd.Text = "";
        txt_before_wd_2.Text = "";
        txt_after_wd.Text = "";        
        btn_begin.Enabled = false;
        if (txt_luhao.Text == "" || txt_hejin.Text == "" || txt_gh.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择员工，熔炼炉号和合金！')", true);
            btn_begin.Enabled = true;
            return;
        }
        txt_luhao.BackColor = txt_date.BackColor;
        txt_hejin.BackColor = txt_date.BackColor;
        btn_again.Enabled = false;
        txt_zyno.BackColor = txt_date.BackColor;




        lb_start.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        div2.Style.Add("display", "block");

        //lb_again.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        txt_before_wd.BackColor = System.Drawing.Color.Yellow;
        btn_bf_time.Enabled = true;
        txt_before_wd.ReadOnly = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "dtime='"+ lb_start.Text + "';setInterval('show_cur_times()', 1000);", true);

        string date = txt_date.Text.Replace("/", "");
        this.txt_zybno.DataSource = Function_Jinglian.zybno_query(1, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text);
        this.txt_zybno.DataTextField = "zybno";
        this.txt_zybno.DataValueField = "zybno";
        this.txt_zybno.DataBind();
      
         string no=txt_zybno.Text.ToString().Substring(0,11);
        string hejin=txt_hejin.Text;
        string luhao = txt_luhao.SelectedValue;
        int count=int.Parse(jl_dal.Get_BS(no, hejin).Tables[0].Rows[0][0].ToString());
        //if ((count + 1) % 4 == 0 || hejin == "EN47100")
        if ((count + 1) % 4 == 0 || luhao == "E" || luhao == "A" || luhao == "F" || chk_cy_yn.Checked == true)
        {
            gp_flag.Value = "Y";
            btn_gpcs.BackColor = System.Drawing.Color.Yellow;
        }

    }


    public void ini_default()
    {
        this.txt_zybno.DataSource = Function_Jinglian.zybno_query(3, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text);
        this.txt_zybno.DataTextField = "zybno";
        this.txt_zybno.DataValueField = "zybno";
        this.txt_zybno.DataBind();

        this.txt_luhao.DataSource = Function_Jinglian.Hydrogen_Query(2, txt_zybno.Text, "", "", "", "", "", "");
        this.txt_luhao.DataTextField = "HD_luhao";
        this.txt_luhao.DataValueField = "HD_luhao";
        this.txt_luhao.DataBind();
        this.txt_luhao.Items.Insert(0, new ListItem("", ""));


        this.txt_hejin.DataSource = Function_Jinglian.Hydrogen_Query(2, txt_zybno.Text, "", "", "", "", "", "");
        this.txt_hejin.DataTextField = "HD_hejin";
        this.txt_hejin.DataValueField = "HD_hejin";
        this.txt_hejin.DataBind();
        this.txt_hejin.Items.Insert(0, new ListItem("", ""));


        txt_luhao.BackColor = System.Drawing.Color.Yellow;
        txt_hejin.BackColor = System.Drawing.Color.Yellow;
        this.txt_zybselect.DataSource = Function_Jinglian.zybno_query(2, "", "", "", "", "", "");
        this.txt_zybselect.DataTextField = "HD_zybno";
        this.txt_zybselect.DataValueField = "HD_zybno";
        this.txt_zybselect.DataBind();
        this.txt_zybselect.Items.Insert(0, new ListItem("", ""));
        btn_begin.Enabled = true;
        lb_start.Text = "";
        btn_end.Enabled = false;
        lb_af_time.Text = "";
        lb_bf_time.Text = "";
        txt_before_wd.Text = "";
        txt_before_wd_2.Text = "";
        txt_after_wd.Text = "";
        txt_zybno.BackColor = txt_date.BackColor;
        lb_again.Text = "";
        Label3.Text = "";
        this.txt_luhao.DataSource = Function_Jinglian.Hydrogen_Query(3, "", "", "", "", "", "", "");
        this.txt_luhao.DataTextField = "base_value";
        this.txt_luhao.DataValueField = "base_value";
        this.txt_luhao.DataBind();
        this.txt_luhao.Items.Insert(0, new ListItem("", ""));
        gp_flag.Value = "";


        this.txt_hejin.DataSource = Function_Jinglian.Hydrogen_Query(4, "", "", "", "", "", "", "");
        this.txt_hejin.DataTextField = "base_value";
        this.txt_hejin.DataValueField = "base_value";
        this.txt_hejin.DataBind();
        this.txt_hejin.Items.Insert(0, new ListItem("", ""));

        this.txt_zyno.DataSource = Function_Jinglian.ZybClear_Content_Query("3");
        this.txt_zyno.DataTextField = "equip_no";
        this.txt_zyno.DataValueField = "equip_no";
        this.txt_zyno.DataBind();
        this.txt_zyno.Items.Insert(0, new ListItem("", ""));

        txt_zyno.BackColor = System.Drawing.Color.Yellow;
        txt_pz.Text = "";
        txt_mz.Value = "";
        txt_jz.Value = "";
        //modify 0113
        //txt_zyno.CssClass = "form-control input-s-sm  ";
        //txt_zyno.Enabled = false;
    }

    protected void btn_bf_time_Click(object sender, EventArgs e)
    {
        SetMap();
        decimal f = 0;
        btn_bf_time.Enabled = true;
     

        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        if (txt_before_wd.Text == "")
        {
            strAlert = strAlert + "layer.alert('请输入出料时炉膛铝液温度！');";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }
       // decimal xx = decimal.Parse(txt_before_wd.Text);
        if (decimal.TryParse(txt_before_wd.Text, out f) == false)
        {
            strAlert = strAlert + "layer.alert('出料时炉膛铝液温度必须为数值型！');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }
        else
        {
            if (Convert.ToDecimal(txt_before_wd.Text) < 720 || Convert.ToDecimal(txt_before_wd.Text) >770)
            {
                strAlert = strAlert + "layer.alert('出料时炉膛铝液温度要求大于720小于770！！');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

                return;
            }
            else
            {
                txt_before_wd.BackColor = txt_hejin.BackColor;
                txt_before_wd.ReadOnly = true;
            }

        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

        if (gp_flag.Value=="Y")
        {
            btn_gpcs.Enabled = true;
            btn_gpcs.BackColor = txt_hejin.BackColor;
           // btn_gpcs.CssClass = "btn btn-primary";
            btn_af_time.Enabled = false;
            txt_before_wd_2.ReadOnly = true;
            txt_after_wd.ReadOnly = true;
        }
        else
        {
            btn_af_time.Enabled = true;
            txt_before_wd_2.ReadOnly = false;
            txt_before_wd_2.BackColor = System.Drawing.Color.Yellow;
            txt_after_wd.ReadOnly = false;
            txt_after_wd.BackColor = System.Drawing.Color.Yellow;
            btn_af_time.Enabled = true;
        }
        
       
        btn_bf_time.Enabled = false;
        div9.Style.Add("display", "block");
        lb_bf_time.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        btn_again.Enabled = false;

    }
    protected void btn_af_time_Click(object sender, EventArgs e)
    {
        
        SetMap();
        decimal f = 0;
        btn_af_time.Enabled = true;

        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";

        //add by 20190624
        if (txt_before_wd_2.Text == "")
        {
            strAlert = strAlert + "layer.alert('请输入精炼前温度！');";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }

        if (decimal.TryParse(txt_before_wd_2.Text, out f) == false)
        {
            strAlert = strAlert + "layer.alert('精炼前温度必须为数值型！');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }
        else
        {
            if (Convert.ToDecimal(txt_before_wd_2.Text) <= 700)
            {
                strAlert = strAlert + "layer.alert('精炼前温度要求大于700！！');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

                return;
            }
            else
            {
                txt_before_wd_2.BackColor = txt_hejin.BackColor;
                txt_before_wd_2.ReadOnly = true;
            }

        }
        //end

        if (txt_after_wd.Text == "")
        {
            strAlert = strAlert + "layer.alert('请输入精炼后温度！');";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }

        if (decimal.TryParse(txt_after_wd.Text, out f) == false)
        {
            strAlert = strAlert + "layer.alert('精炼后温度必须为数值型！');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
            return;
        }
        else
        {
            if (Convert.ToDecimal(txt_after_wd.Text) < 670 || Convert.ToDecimal(txt_after_wd.Text) >730)
            {
                strAlert = strAlert + "layer.alert('精炼后温度要求大于670小于730！！');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

                return;
            }
            else
            {
                txt_after_wd.BackColor = txt_hejin.BackColor;
                txt_after_wd.ReadOnly = true;
            }

        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
        btn_af_time.Enabled = false;
        div11.Style.Add("display", "block");
        lb_af_time.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
       // btn_end.Enabled = true;
        btn_again.Enabled = false;
        btn_zl_confirm.Enabled = true;
       

    }
    protected void btn_again_Click(object sender, EventArgs e)
    {
        
        //btn_end.CssClass = "btn btn-large btn-primary disabled";
        SetMap();
        btn_end.Enabled = false;
        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
        this.txt_zybno.DataSource = Function_Jinglian.zybno_query(2, "", "", "", "", "", "");
        this.txt_zybno.DataTextField = "HD_zybno";
        this.txt_zybno.DataValueField = "HD_zybno";
        this.txt_zybno.DataBind();
        this.txt_zybno.Items.Insert(0, new ListItem("", ""));
        txt_zybno.BackColor = System.Drawing.Color.Yellow;
        txt_luhao.BackColor = txt_date.BackColor;
        txt_hejin.BackColor = txt_date.BackColor;
        btn_again.Enabled = false;
        btn_begin.Enabled = false;
        txt_before_wd.ReadOnly = false;
        div2.Style.Add("display", "none");
        lb_again.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        //lb_start.Text = lb_again.Text;
        this.txt_luhao.DataSource = Function_Jinglian.Hydrogen_Query(3, "", "", "", "", "", "", "");
        this.txt_luhao.DataTextField = "base_value";
        this.txt_luhao.DataValueField = "base_value";
        this.txt_luhao.DataBind();
        this.txt_luhao.Items.Insert(0, new ListItem("", ""));


        this.txt_hejin.DataSource = Function_Jinglian.Hydrogen_Query(4, "", "", "", "", "", "", "");
        this.txt_hejin.DataTextField = "base_value";
        this.txt_hejin.DataValueField = "base_value";
        this.txt_hejin.DataBind();
        this.txt_hejin.Items.Insert(0, new ListItem("", ""));
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

       
    }
    protected void btn_end_Click(object sender, EventArgs e)
    {
        if (lb_start.Text != "")
        {
            string zybno = Function_Jinglian.zybno_query(1, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text).Rows[0][0].ToString();
            if (Function_Jinglian.hydrogen_check(1, zybno).Rows.Count != 0)
            {
                string luhao = Function_Jinglian.hydrogen_check(1, zybno).Rows[0][0].ToString();
                string hejin = Function_Jinglian.hydrogen_check(1, zybno).Rows[0][1].ToString();
                if (luhao == txt_luhao.Text && hejin != txt_hejin.Text)
                {

                    string str = "layer.confirm('此笔合金号与上笔合金号" + hejin + "不一致，是否重新选择？', {  btn: ['否','是'] }, function(){  $('#MainContent_btnNext').click();}, function(){  });";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
                }
                else
                {

                    btnNext_Click(sender, e);
                }
            }
            else
            {
                btnNext_Click(sender, e);
            }
            
          
        }
        else
        {
            string zybno = txt_zybno.Text;
            int result = Function_Jinglian.jinglian_insert_2(1, txt_date.Text, lb_again.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), txt_shift.Text, txt_gh.Text, txt_name.Text,
           txt_banzu.Text, lb_jlno.Text, zybno, txt_luhao.Text, txt_hejin.Text, decimal.Parse(txt_before_wd.Text).ToString(), lb_bf_time.Text, decimal.Parse(txt_after_wd.Text).ToString(),
           lb_af_time.Text, txt_zyno.Text, txt_pz.Text, lb_mz.Text, lb_jz.Text, Label3.Text, gp_flag.Value, decimal.Parse(txt_before_wd_2.Text).ToString(),
           chk_cy_yn.Checked == true ? "是" : "");
            ini_default();
        }
       // string zybno = Function_Jinglian.zybno_query(1, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text).Rows[0][0].ToString();
        btn_again.Enabled = true;
        SetMap();



    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        if (txt_gh.Text == "")
        {
            ScriptManager.RegisterStartupScript(Button4, this.GetType(), "alert", "layer.alert('请选择工号！');" + strAlert, true);
            return;
        }

        if (txt_zybselect.Text == "")
        {

            ScriptManager.RegisterStartupScript(Button4, this.GetType(), "alert", "layer.alert('请选择转运包序列号！');" + strAlert, true);
            return;
        }
        if (txt_midu.Text == "" )
        {
            ScriptManager.RegisterStartupScript(Button4,this.GetType(), "alert", "layer.alert('请输入空气中重量和水中重量！');" + strAlert, true);
            return;
        }
        if (txt_bmzt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Button4,this.GetType(), "alert", "layer.alert('请选择测氢样块表面状态！');" + strAlert, true);
            return;
        }
        if (txt_temperature.Text == "")
        {
            ScriptManager.RegisterStartupScript(Button4, this.GetType(), "alert", "layer.alert('请填写当前水温！');" + strAlert, true);
            return;
        }
        int result = Function_Jinglian.Hydrogen_insert_Bytemperature(1, txt_zybselect.Text, txt_date.Text, txt_shift.Text, txt_banzu.Text, txt_gh.Text, txt_name.Text, txt_water.Text, txt_kq.Text, txt_midu.Text, txt_bmzt.Text,txt_temperature.Text,lb_ms.Text);
        if (result >= 1)
        {
            ScriptManager.RegisterStartupScript(Button4,this.GetType(), "alert", "layer.alert('转运包" + txt_zybselect.Text + "！测氢完成！');" + strAlert, true);
            //txt_zybselect.SelectedValue = "";
            txt_water.Text = "";
            txt_kq.Text = "";
            txt_midu.Text = "";
            txt_bmzt.Text = "";
            txt_midu.BackColor = txt_date.BackColor;

        }
        this.txt_zybselect.DataSource = Function_Jinglian.zybno_query(2, "", "", "", "", "", "");
        this.txt_zybselect.DataTextField = "HD_zybno";
        this.txt_zybselect.DataValueField = "HD_zybno";
        this.txt_zybselect.DataBind();
        this.txt_zybselect.Items.Insert(0, new ListItem("", ""));
        SetMap();

       
    }
    protected void txt_water_TextChanged(object sender, EventArgs e)
    {
        SetMap();
        decimal midu_value = 0;
        decimal f = 0;
        string sql = string.Format("   select density  from mes_water_density  where  temperature='{0}'  ", txt_temperature.Text);
        DataTable dt_md = DbHelperSQL.Query(sql).Tables[0];
        if (dt_md.Rows.Count > 0)
        {
            lb_ms.Text ="ρ="+ DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString();
        }
        if (txt_kq.Text != "" && txt_water.Text != "")
        {

            if (decimal.TryParse(txt_kq.Text, out f) == true && decimal.TryParse(txt_water.Text, out f) == true)
            {
                //midu_value = Math.Round(decimal.Parse(txt_kq.Text) *decimal.Parse( lb_ms.Text.Replace("ρ=", "")) / (decimal.Parse(txt_kq.Text) - decimal.Parse(txt_water.Text)), 2);
                //modify by 20190329:数值的区别就在小数点第三位，只取两位不够精确
                midu_value = Math.Round(decimal.Parse(txt_kq.Text) * decimal.Parse(lb_ms.Text.Replace("ρ=", "")) / (decimal.Parse(txt_kq.Text) - decimal.Parse(txt_water.Text)), 3);

                txt_midu.Text = midu_value.ToString();
                if (Convert.ToDouble(midu_value) >= 2.65)
                {
                    txt_midu.Text = "ρ=" + midu_value + "" + "  " + "OK";
                    txt_midu.BackColor = System.Drawing.Color.Green;
                   
                }
                else
                {
                    txt_midu.Text = "ρ=" + midu_value + "" + "  " + "NG";
                    txt_midu.BackColor = System.Drawing.Color.Red;
                    
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('必须为数值型，请重新输入！')</script>");
                txt_water.Text = "";
                txt_kq.Text = "";
                return;
            }
        }
    }


    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_shift.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text,"").Rows[0]["emp_banzhu"].ToString();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void txt_zybno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMap();
        this.txt_luhao.DataSource = Function_Jinglian.Hydrogen_Query(2, txt_zybno.Text, "", "", "", "", "", "");
        this.txt_luhao.DataTextField = "HD_luhao";
        this.txt_luhao.DataValueField = "HD_luhao";
        this.txt_luhao.DataBind();
       


        this.txt_hejin.DataSource = Function_Jinglian.Hydrogen_Query(2, txt_zybno.Text, "", "", "", "", "", "");
        this.txt_hejin.DataTextField = "HD_hejin";
        this.txt_hejin.DataValueField = "HD_hejin";
        this.txt_hejin.DataBind();

        txt_before_wd.BackColor = System.Drawing.Color.Yellow;
        btn_bf_time.Enabled = true;
      
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        string zybno = Function_Jinglian.zybno_query(1, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm:ss"), txt_shift.Text, txt_banzu.Text, txt_luhao.Text, txt_hejin.Text).Rows[0][0].ToString();
        int result = Function_Jinglian.jinglian_insert_2(1, txt_date.Text, lb_start.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), txt_shift.Text, txt_gh.Text, txt_name.Text,
         txt_banzu.Text, lb_jlno.Text, zybno, txt_luhao.Text, txt_hejin.Text, decimal.Parse(txt_before_wd.Text).ToString(), lb_bf_time.Text, decimal.Parse(txt_after_wd.Text).ToString(),
         lb_af_time.Text, txt_zyno.Text, txt_pz.Text, lb_mz.Text, lb_jz.Text, Label3.Text, gp_flag.Value, decimal.Parse(txt_before_wd_2.Text).ToString(),
         chk_cy_yn.Checked == true ? "是" : "");
        //int result = Function_Jinglian.jinglian_insert(1, txt_date.Text, lb_again.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), txt_shift.Text, txt_gh.Text, txt_name.Text,
        // txt_banzu.Text, lb_jlno.Text, zybno, txt_luhao.Text, txt_hejin.Text, txt_before_wd.Text, lb_bf_time.Text, txt_after_wd.Text, lb_af_time.Text, txt_zyno.Text, txt_pz.Text, txt_mz.Value, txt_jz.Value);

        ini_default();
        btn_again.Enabled = true;
        SetMap();

    }

    protected void txt_zyno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMap();
        string baohao = txt_zyno.Text;
        string strsql = "select weight from [MES_YZ_ZYBClear] where checkdate=(select max(checkdate) from [dbo].[MES_YZ_ZYBClear] where equip_no='" + baohao + "')";
        DataTable dt = SQLHelper.reDs(strsql).Tables[0];
        if (dt.Rows.Count == 0)
        {
            txt_pz.Text = dt.Rows[0][0].ToString();
        }
        else
        {
            txt_pz.Text = dt.Rows[0][0].ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "set", "setWeight('');", true);
        }
    }
    protected void btn_zl_confirm_Click(object sender, EventArgs e)
    {
        SetMap();
        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        if (txt_mz.Value == "" || Convert.ToDecimal(txt_mz.Value) <= 100 )
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('读入毛重数据不正确，请等待数据刷新！');"+strAlert, true);
            return;
        }
        txt_jz.Value = (Convert.ToDecimal(txt_mz.Value) - Convert.ToDecimal(txt_pz.Text)).ToString();
        if (Convert.ToDecimal(txt_jz.Value) < 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('净重数据为负数，不正确！');"+strAlert, true);
            return;
        }
        lb_mz.Text = txt_mz.Value;
        lb_jz.Text = txt_jz.Value;
        Label3.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss"); ;
        btn_zl_confirm.Enabled = false;
        btn_end.Enabled = true;
        //txt_jz.Value = (Convert.ToDecimal(txt_mz.Value) - Convert.ToDecimal(txt_pz.Text)).ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);

    }

    protected void SetMap()
    {
        this.C3.ChartAreas[0].AxisY.Minimum = 2.5;
        this.C3.ChartAreas[0].AxisY.Maximum = 2.8;

        this.C3.Series["密度"].XValueMember = "mon";
        this.C3.Series["密度"].YValueMembers = "midu";
        string curr_year=DateTime.Now.Year.ToString();
       DataTable  dt = Function_Jinglian.Jinglian_TJ_Report(4, curr_year, "", "", "").Tables[0];
       C3.DataSource = dt;
        this.C3.DataBind();//绑定数据
       
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            this.C3.Series["密度"].Points[i].AxisLabel = dt.Rows[i]["mon"].ToString();

            this.C3.Series["密度"].Points[i].ToolTip = "[密度]" + dt.Rows[i]["mon"].ToString() + "(" + dt.Rows[i]["midu"].ToString() + ")";

        }
        //string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
       
    }
    protected void btn_gpcs_Click(object sender, EventArgs e)
    {
        btn_gpcs.Enabled = false;
        txt_before_wd_2.ReadOnly = false;
        txt_before_wd_2.BackColor = System.Drawing.Color.Yellow;
        txt_after_wd.ReadOnly = false;
        btn_af_time.Enabled = true;
        txt_after_wd.BackColor = System.Drawing.Color.Yellow;
        //gp_flag.Value = "Y";
        string strAlert = "dtime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "';setInterval('show_cur_times()', 1000);";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", strAlert, true);
        
    }
    protected void txt_luhao_SelectedIndexChanged(object sender, EventArgs e)
    {
        string zyno = txt_zyno.SelectedValue;
        if (txt_luhao.SelectedValue == "A" || txt_luhao.SelectedValue == "E" || txt_luhao.SelectedValue == "F")
        {
            Label1.Text = "1次/包";
        }
        else
        {
            Label1.Text = "1次/4包";
        }
        if (txt_zyno.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择转运包号')", true);
            return;
        }
        string luhao = jl_dal.Get_luhao(zyno);
        if (luhao == txt_luhao.SelectedValue)
        {
            txt_hejin.SelectedValue = jl_dal.Get_hejin(zyno, luhao);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择的转运包号和炉号与上一笔不一致或不存在，请自行选择合金号！')", true);
            txt_hejin.SelectedValue = "";
            return;
        }
       
       
    }



    //获取当前水温对应的水密度
    [System.Web.Services.WebMethod()]
    public static string GetMD(string temperature)
    {
        string result = "";
        var sql = "";
        sql = string.Format("  select density  from mes_water_density  where  temperature='{0}'  ", temperature);

        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.Rows[0][0].ToString(); }
        result = "[{\"temperature\":\"" + result + "\"}]";
        return result;
    }
}