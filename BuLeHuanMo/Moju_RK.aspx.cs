using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;



public partial class BuLeHuanMo_Moju_RK : System.Web.UI.Page
{
    MES_HuanMo_DAL HuanMo_DAL = new MES_HuanMo_DAL();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string mojuno = Request["mojuno"];
         string sbno = Request["sbno"];
        if (!IsPostBack)
        {

            this.txtjs_user.DataSource = HuanMo_DAL.GetMoJuJS_User();
            this.txtjs_user.DataTextField = "js_user";
            this.txtjs_user.DataValueField = "js_user";
            this.txtjs_user.DataBind();
            this.txtjs_user.Items.Insert(0, new ListItem("", ""));

            this.txtrk_user1.DataSource = HuanMo_DAL.GetMoJuRK_User();
            this.txtrk_user1.DataTextField = "rk_user";
            this.txtrk_user1.DataValueField = "rk_user";
            this.txtrk_user1.DataBind();
            this.txtrk_user1.Items.Insert(0, new ListItem("", ""));
            txt_Mojuno.Text = mojuno;
            txt_rkDate.Text = System.DateTime.Now.ToShortDateString();
            txt_rktype.Text = "生产归还入库";
            txt_sbno.Text = sbno;
            txt_SumMoci.Text = HuanMo_DAL.GetMoci(mojuno).Tables[0].Rows[0]["moci"].ToString();
            string moju_weizhi = HuanMo_DAL.GetMoci(mojuno).Tables[0].Rows[0]["weizhi"].ToString();
            this.txtmoju_id.Text = HuanMo_DAL.GetMoci(mojuno).Tables[0].Rows[0]["id"].ToString();
        }
      
        
    }
   
 
 
    protected void btn_rk_Click(object sender, EventArgs e)
    {
        if (this.txtrk_user1.Text.ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('入库人不能为空！')", true);
            return;
        }
        if (this.txtjs_user.Text.ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('经手人不能为空！')", true);
            return;
        }
        if (this.txt_moci.Text.ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('本次生产模次不能为空！')", true);
            return;
        }
        if (this.txtDemo.Text.ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('入库状态说明不能为空！')", true);
            return;
        }
        if (HuanMo_DAL.GetMoci(Request["mojuno"]).Tables[0].Rows[0]["weizhi"].ToString() != "生产")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('该模具位置不为生产，无需入库！')", true);
            return;
        }

        string lsno =MES_HuanMo_DAL.GetNo("MOJURK_NO", "RK");
         int ln = MES_HuanMo_DAL.MoJuRKInsert(this.txtmoju_id.Text, this.txtrk_user1.Text, this.txt_rktype.Text, this.txtjs_user.Text, this.txt_moci.Text, this.txt_SumMoci.Text, this.txt_rkDate.Text, this.txtDemo.Text, "", this.txtrk_weizhi.Text, "", lsno,txt_sbno.Text);
            if (ln >= 0)
            {
                ScriptManager.RegisterStartupScript(btn_rk, this.GetType(), "clos", "layer.alert('入库成功. ');document.getElementById('btnBack').click()", true);
            }
        }

    protected void txt_moci_TextChanged(object sender, EventArgs e)
    {
        if (this.txt_SumMoci.Text != "" && this.txt_moci.Text != "")
        {

            this.txt_SumMoci.Text = (Convert.ToInt32(this.txt_moci.Text) + Convert.ToInt32(this.txt_SumMoci.Text)).ToString();


        }
    }
}