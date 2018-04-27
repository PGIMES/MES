using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class PW_PW_Add : System.Web.UI.Page
{
    Function_PW PW = new Function_PW();
    GetBase GetBase = new GetBase();
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
            string strSQL = "select distinct emp_no,emp_name,emp_banbie,emp_banzhu from MES_EmpLogin where emp_gongwei='抛丸' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
            txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
            txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
            txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            Button2.CssClass = "btn btn-large btn-primary  disabled";
            Button2.Enabled = false;

            txt_bs1.ReadOnly = true;
            txt_bs2.ReadOnly = true;
          

           

        }
    }

    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSQL = "select * from MES_EmpLogin where  emp_gongwei='抛丸' and status=1 and emp_no='" + dropGongHao.SelectedValue + "'";
        DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
        txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
        txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
        txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
    }
    
    protected void B_Click(object sender, EventArgs e)
    {
        btnB.CssClass = "btn btn-large btn-primary disabled ";
        btnB.Enabled = false;

        btnA.CssClass = "btn btn-large btn-primary  ";
        btnA.Enabled = true;

        btnC.CssClass = "btn btn-large btn-primary  ";
        btnC.Enabled = true;

        btnD.CssClass = "btn btn-large btn-primary  ";
        btnD.Enabled = true;

        btnE.CssClass = "btn btn-large btn-primary  ";
        btnE.Enabled = true;

        txt_bs1.Text = "";
        txt_bs2.Text = "";
        txt_bs1.ReadOnly = false;
        txt_bs1.Focus();
        txt_bs2.ReadOnly = true;
        txt_bs1.BackColor = System.Drawing.Color.Yellow;
        txt_bs2.BackColor = txt.BackColor;
        Button2.CssClass = "btn btn-large btn-primary  ";
        Button2.Enabled = true;
    }
    protected void C_Click(object sender, EventArgs e)
    {
        btnC.CssClass = "btn btn-large btn-primary disabled ";
        btnC.Enabled = false;

        btnB.CssClass = "btn btn-large btn-primary  ";
        btnB.Enabled = true;

        btnA.CssClass = "btn btn-large btn-primary  ";
        btnA.Enabled = true;

        btnD.CssClass = "btn btn-large btn-primary  ";
        btnD.Enabled = true;

        btnE.CssClass = "btn btn-large btn-primary  ";
        btnE.Enabled = true;

        txt_bs1.Text = "";
        txt_bs2.Text = "";
        txt_bs2.ReadOnly = false;
        txt_bs2.Focus();
        txt_bs1.ReadOnly = true;
        txt_bs2.BackColor = System.Drawing.Color.Yellow;
        txt_bs1.BackColor = txt.BackColor;
        Button2.CssClass = "btn btn-large btn-primary  ";
        Button2.Enabled = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        int result = 0;
        if (txt_bs1.Text != "")
        {
            
            result = PW.PW_Add_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value,txtSheBeiJianCheng.Value, txtSheBeiJianCheng.Value,"0.4mm",Convert.ToInt16( txt_bs1.Text));
            if (result >= 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('加料成功！')", true);
                
                Button2.CssClass = "btn btn-large  btn-primary disabled";
                
            }
        }
        if (txt_bs2.Text != "")
        {
            result = PW.PW_Add_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiJianCheng.Value, txtSheBeiJianCheng.Value, "0.6mm", Convert.ToInt16(txt_bs2.Text));
            if (result >= 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('加料成功！')", true);
                
                Button2.CssClass = "btn btn-large  btn-primary disabled";
            }
        }
        btnB.CssClass = "btn btn-large btn-primary  ";
        btnB.Enabled = true;

        btnA.CssClass = "btn btn-large btn-primary  ";
        btnA.Enabled = true;

        btnD.CssClass = "btn btn-large btn-primary  ";
        btnD.Enabled = true;

        btnE.CssClass = "btn btn-large btn-primary  ";
        btnE.Enabled = true;

        btnC.CssClass = "btn btn-large btn-primary  ";
        btnC.Enabled = true;
    }
    protected void btnA_Click(object sender, EventArgs e)
    {
        btnA.CssClass = "btn btn-large btn-primary disabled ";
        btnA.Enabled = false;

        btnB.CssClass = "btn btn-large btn-primary  ";
        btnB.Enabled = true;

        btnC.CssClass = "btn btn-large btn-primary  ";
        btnC.Enabled = true;

        btnD.CssClass = "btn btn-large btn-primary  ";
        btnD.Enabled = true;

        btnE.CssClass = "btn btn-large btn-primary  ";
        btnE.Enabled = true;

        txt_bs1.Text = "";
        txt_bs2.Text = "";
        txt_bs2.ReadOnly = false;
        txt_bs2.Focus();
        txt_bs1.ReadOnly = true;
        txt_bs2.BackColor = System.Drawing.Color.Yellow;
        txt_bs1.BackColor = txt.BackColor;
        Button2.CssClass = "btn btn-large btn-primary  ";
        Button2.Enabled = true;

    }
    protected void btnD_Click(object sender, EventArgs e)
    {
        btnD.CssClass = "btn btn-large btn-primary disabled ";
        btnD.Enabled = false;

        btnB.CssClass = "btn btn-large btn-primary  ";
        btnB.Enabled = true;

        btnC.CssClass = "btn btn-large btn-primary  ";
        btnC.Enabled = true;

        btnA.CssClass = "btn btn-large btn-primary  ";
        btnA.Enabled = true;

        btnE.CssClass = "btn btn-large btn-primary  ";
        btnE.Enabled = true;

        txt_bs1.Text = "";
        txt_bs2.Text = "";
        txt_bs1.ReadOnly = false;
        txt_bs1.Focus();
        txt_bs2.ReadOnly = true;
        txt_bs1.BackColor = System.Drawing.Color.Yellow;
        txt_bs2.BackColor = txt.BackColor;
        Button2.CssClass = "btn btn-large btn-primary  ";
        Button2.Enabled = true;
    }
    protected void btnE_Click(object sender, EventArgs e)
    {
        btnE.CssClass = "btn btn-large btn-primary disabled ";
        btnE.Enabled = false;

        btnB.CssClass = "btn btn-large btn-primary  ";
        btnB.Enabled = true;

        btnA.CssClass = "btn btn-large btn-primary  ";
        btnA.Enabled = true;

        btnD.CssClass = "btn btn-large btn-primary  ";
        btnD.Enabled = true;

        btnC.CssClass = "btn btn-large btn-primary  ";
        btnC.Enabled = true;

        txt_bs1.Text = "";
        txt_bs2.Text = "";
        txt_bs2.ReadOnly = false;
        txt_bs2.Focus();
        txt_bs1.ReadOnly = true;
        txt_bs2.BackColor = System.Drawing.Color.Yellow;
        txt_bs1.BackColor = txt.BackColor;
        Button2.CssClass = "btn btn-large btn-primary  ";
        Button2.Enabled = true;
    }
   
}