using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class InputMat : System.Web.UI.Page
{
    static string  type = "";
    static string equip_no = "";
    Function_TouLiao_RL Function_TouLiaoRL = new Function_TouLiao_RL();
    protected void Page_Load(object sender, EventArgs e)
    {              

        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            //初始话下拉登入此台设备人员
            string strSQL = "select * from MES_EmpLogin where emp_shebei='"+Request["deviceid"]+"' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
            txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
            txtBanBie.Value= tbl.Rows[0]["emp_banbie"].ToString();
            txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            ddlhejin.DataSource = Function_TouLiaoRL.GouLiao_Hejin_query("");
            ddlhejin.DataValueField = "value";
            ddlhejin.DataTextField = "value";
            ddlhejin.DataBind();
            string a = Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();

            ddlhejin.BackColor = System.Drawing.Color.Yellow;

            ddlhejin.SelectedValue = a.Trim ();

            hejing.Text = ddlhejin.SelectedValue;

            //div1 .
            //初始设备信息
           // div1.Attributes.Add("class", "btn-padding-s btn-yellow");
            div3.Attributes.Add("class", "btn-padding-s btn-yellow");

            string strSQL2 = "select * from MES_Equipment where equip_no='" + Request["deviceid"] + "'";
            DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
            if (tbl2.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl2.Rows)
                {
                    txtSheBeiHao.Value = dr["equip_no"].ToString();//.Field["equip_no"];
                    txtSheBeiJianCheng.Value = dr["equip_name"].ToString();
                    txtSheBeiGuiGe.Value = dr["equip_name_api"].ToString();
                    
                }
            }
            DataTable dtpihao = new DataTable();
            dtpihao = Function_TouLiaoRL.TouLiao_RL_NO_query(txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, ddlhejin.SelectedValue );

            txtPiHao.Value = dtpihao.Rows[0]["touliaono"].ToString(); // Function_TouLiaoRL.TouLiao_RL_NO_query(txtBanBie.Value, txtBanZu.Value,txtSheBeiHao.Value, selHeJin.Value).Rows [0]["touliaono"].ToString ();
               type = "";
               txttype.Value  = "";
              equip_no = "";
              txtmaterial_lot.Enabled = false;
            ini_button();
            //初始化机台
            if (Request["deviceid"] == "A" )
            {
                A.Enabled = true;
                A.CssClass = "btn btn-large btn-primary";
                B.Enabled = false;
                B.CssClass = "btn btn-large btn-primary disabled";
                C.Enabled = false;
                C.CssClass = "btn btn-large btn-primary disabled";
                D.Enabled = false;
                D.CssClass = "btn btn-large btn-primary disabled";
                E.Enabled = false;
                E.CssClass = "btn btn-large btn-primary disabled";
               
            }

            else if (Request["deviceid"] == "B" )
            {
                A.Enabled = false;
                A.CssClass = "btn btn-large btn-primary disabled";
                B.Enabled = true;
                B.CssClass = "btn btn-large btn-primary";
                C.Enabled = false;
                C.CssClass = "btn btn-large btn-primary disabled";
                D.Enabled = false;
                D.CssClass = "btn btn-large btn-primary disabled";
                E.Enabled = false;
                E.CssClass = "btn btn-large btn-primary disabled";

            }



            else if (Request["deviceid"] == "C" )

            {
                C.Enabled = true;
                C.CssClass = "btn btn-large btn-primary";
                D.Enabled = false;
                D.CssClass = "btn btn-large btn-primary disabled";
                A.Enabled = false;
                A.CssClass = "btn btn-large btn-primary disabled";
                B.Enabled = false;
                B.CssClass = "btn btn-large btn-primary disabled";
                E.Enabled = false;
                E.CssClass = "btn btn-large btn-primary disabled";
            }


            else if ( Request["deviceid"] == "D")
            {
                C.Enabled = false;
                C.CssClass = "btn btn-large btn-primary disabled";
                D.Enabled = true;
                D.CssClass = "btn btn-large btn-primary";
                A.Enabled = false;
                A.CssClass = "btn btn-large btn-primary disabled";
                B.Enabled = false;
                B.CssClass = "btn btn-large btn-primary disabled";
                E.Enabled = false;
                E.CssClass = "btn btn-large btn-primary disabled";
            }
            else if (Request["deviceid"] == "E")
            {
                C.Enabled = false;
                C.CssClass = "btn btn-large btn-primary disabled";
                D.Enabled = false;
                D.CssClass = "btn btn-large btn-primary disabled";
                A.Enabled = false;
                A.CssClass = "btn btn-large btn-primary disabled";
                B.Enabled = false;
                B.CssClass = "btn btn-large btn-primary disabled";

                E.Enabled = true;
                E.CssClass = "btn btn-large btn-primary";

            }
           // E.Enabled = false;
           // E.CssClass = "btn btn-large btn-primary disabled";
          //  E.Enabled = false;
          //  E.CssClass = "btn btn-large btn-primary disabled";

            //A.Enabled = false;
            //A.CssClass = "btn btn-large btn-primary disabled";
            
            //C.Enabled = false;
            //C.CssClass = "btn btn-large btn-primary disabled";

            ///////////////


            ////////初始化加料信息///



            btn_jialiao1.Enabled = false  ;
            btnMaoZhong_confirm_1.Enabled = false;
            btnPiZhong_confirm_1.Enabled = false;
            btnTouLiao_confirm_1.Enabled = false;

            btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary disabled";
            btnPiZhong_confirm_1.CssClass = "btn btn-large btn-primary disabled";
            btnTouLiao_confirm_1.CssClass = "btn btn-large btn-primary disabled";
            btn_jialiao2.Enabled = false;

            btn_jialiao3.Enabled = false;

            btn_jialiao4.Enabled = false;


            txt_maozhong_1.Enabled = false;txt_maozhong_1.CssClass="input form-control";
            txt_pizhong_1.Enabled = false;txt_pizhong_1.CssClass="input form-control";
            txt_touliao_1.Enabled = false; txt_touliao_1.CssClass = "input form-control";

            //////////////

            //初始化按钮
          
          
            //btnPiZhong_confirm_1.Enabled = true;
            //txt_pizhong_1.Enabled = true;
            //txt_pizhong_1.BackColor = System.Drawing.Color.Yellow;

            //txt_touliao_1.Enabled = true;
            //txt_touliao_1.BackColor = System.Drawing.Color.Yellow;
        }


        A.Enabled = false;
        A.CssClass = "btn btn-large btn-primary disabled";

       // C.Enabled = false;
       // C.CssClass = "btn btn-large btn-primary disabled";
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{

    //}
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    //txtMaoZhong.Value = "aaa";
    //}

    public void ini_button()
    {
        /////// 初始化机台

        A.Enabled = false;
        A.CssClass = "btn btn-large btn-primary disabled";
            B.Enabled = false;
            B.CssClass = "btn btn-large btn-primary disabled";
            C.Enabled = false;
            C.CssClass = "btn btn-large btn-primary disabled";
            D.Enabled = false;
            D.CssClass = "btn btn-large btn-primary disabled";




            C.Enabled = false;
            C.CssClass = "btn btn-large btn-primary disabled";
            D.Enabled = false;
            D.CssClass = "btn btn-large btn-primary disabled";
            A.Enabled = false;
            A.CssClass = "btn btn-large btn-primary disabled";
            B.Enabled = false;
            B.CssClass = "btn btn-large btn-primary disabled";

     
        ///////
        btn_jialiao1.Enabled = true; ;
        btnMaoZhong_confirm_1.Enabled = false;
        btnPiZhong_confirm_1.Enabled = false;
        btnTouLiao_confirm_1.Enabled = false;

        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary disabled";
        btnPiZhong_confirm_1.CssClass = "btn btn-large btn-primary disabled";
        btnTouLiao_confirm_1.CssClass = "btn btn-large btn-primary disabled";
        btn_jialiao2.Enabled = false;
        
        btn_jialiao3.Enabled = false;

        btn_jialiao4.Enabled = false;


        txt_maozhong_1.Enabled = false;
        txt_pizhong_1.Enabled = false;
        txt_touliao_1.Enabled = false;

        DataTable dt1 = new DataTable();
           dt1= Function_TouLiaoRL.GouLiao_RL_CUM_query("铝锭", txtBanBie.Value, txtBanZu.Value,equip_no );
        if (dt1.Rows.Count > 0)
        {
            //if (float.Parse(dt1.Rows[0]["total"].ToString()) < 50)
            //{
                btn_jialiao1.Enabled = true;
                lbl_0BiLi.InnerText = dt1.Rows[0]["total"].ToString();
                lbl_0Zhong.InnerText = dt1.Rows[0]["total1"].ToString();
           // }


        }
        DataTable dt2 = new DataTable();
        dt2 = Function_TouLiaoRL.GouLiao_RL_CUM_query("一级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt2.Rows.Count > 0)
        {
            if (float.Parse(dt2.Rows[0]["total"].ToString()) <30)
            {
                btn_jialiao2.Enabled = true;
                div1.Attributes.Add("class", "btn-padding-s btn-success");
                
            }
            if (float.Parse(dt2.Rows[0]["total"].ToString()) >= 30 && float.Parse(dt2.Rows[0]["total"].ToString()) <= 35)
            {
                btn_jialiao2.Enabled = true;
              div1.Attributes.Add("class","btn-padding-s btn-yellow");
            }
            if (float.Parse(dt2.Rows[0]["total"].ToString()) >35)
            {
               // btn_jialiao2.Enabled = true;
                div1.Attributes.Add("class", "btn-padding-s btn-gray");
            }

            lbl_1BiLi.InnerText = dt2.Rows[0]["total"].ToString();
            lbl_1Zhong.InnerText = dt2.Rows[0]["total1"].ToString();
         }
        DataTable dt3 = new DataTable();
        dt3 = Function_TouLiaoRL.GouLiao_RL_CUM_query("二级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt3.Rows.Count > 0)
        {
            if (float.Parse(dt3.Rows[0]["total"].ToString()) <15)
            {
                btn_jialiao3.Enabled = true;
                div2.Attributes.Add("class", "btn-padding-s btn-success");
               
            }
            if (float.Parse(dt3.Rows[0]["total"].ToString()) >= 15 && float.Parse(dt3.Rows[0]["total"].ToString()) <=20)
            {
                btn_jialiao3.Enabled = true;
                div2.Attributes.Add("class", "btn-padding-s btn-yellow");
            }
            if (float.Parse(dt3.Rows[0]["total"].ToString()) >20)
            {
                //btn_jialiao3.Enabled = true;
                div2.Attributes.Add("class", "btn-padding-s btn-gray");
            }


            lbl_2BiLi.InnerText = dt3.Rows[0]["total"].ToString();
            lbl_2Zhong.InnerText = dt3.Rows[0]["total1"].ToString();
        }
        DataTable dt4 = new DataTable();
        dt4 = Function_TouLiaoRL.GouLiao_RL_CUM_query("三级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt4.Rows.Count > 0)
        {
            if (float.Parse(dt4.Rows[0]["total"].ToString()) <5)
            {
                btn_jialiao4.Enabled = true;
                div3.Attributes.Add("class", "btn-padding-s btn-yellow");
              
            }
            //if (float.Parse(dt2.Rows[0]["total"].ToString()) >= 10)
            //{
            //    // btn_jialiao2.Enabled = true;
            //    div3.Attributes.Add("class", "btn-padding-s btn-yellow");
            //}

            lbl_3BiLi.InnerText = dt4.Rows[0]["total"].ToString();
            lbl_3Zhong.InnerText = dt4.Rows[0]["total1"].ToString();
           
        }

    }


    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        Function_Jinglian jl = new Function_Jinglian();

        txtXingMing.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"" ).Rows[0]["emp_name"].ToString();
        txtBanBie.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"").Rows[0]["emp_banbie"].ToString();
        txtBanZu.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"").Rows[0]["emp_banzhu"].ToString();

        DataTable dtpihao = new DataTable();
        dtpihao = Function_TouLiaoRL.TouLiao_RL_NO_query(txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, ddlhejin.SelectedValue );

        txtPiHao.Value = dtpihao.Rows[0]["touliaono"].ToString(); // Function_TouLiaoRL.TouLiao_RL_NO_query(txtBanBie.Value, txtBanZu.Value,txtSheBeiHao.Value, selHeJin.Value).Rows [0]["touliaono"].ToString ();

        ////////////////////////////

        DataTable dt1 = new DataTable();
        dt1 = Function_TouLiaoRL.GouLiao_RL_CUM_query("铝锭", txtBanBie.Value, txtBanZu.Value,equip_no);
        if (dt1.Rows.Count > 0)
        {
            //if (float.Parse(dt1.Rows[0]["total"].ToString()) < 50)
            //{
            //  btn_jialiao1.Enabled = true;
            lbl_0BiLi.InnerText = dt1.Rows[0]["total"].ToString();
            lbl_0Zhong.InnerText = dt1.Rows[0]["total1"].ToString();
            // }


        }
        DataTable dt2 = new DataTable();
        dt2 = Function_TouLiaoRL.GouLiao_RL_CUM_query("一级回炉料", txtBanBie.Value, txtBanZu.Value,equip_no);
        if (dt2.Rows.Count > 0)
        {
            //if (float.Parse(dt2.Rows[0]["total"].ToString()) < 40)
            //{
            //    btn_jialiao2.Enabled = true;

            //}
            lbl_1BiLi.InnerText = dt2.Rows[0]["total"].ToString();
            lbl_1Zhong.InnerText = dt2.Rows[0]["total1"].ToString();
        }
        DataTable dt3 = new DataTable();
        dt3 = Function_TouLiaoRL.GouLiao_RL_CUM_query("二级回炉料", txtBanBie.Value, txtBanZu.Value,equip_no);
        if (dt3.Rows.Count > 0)
        {
            //if (float.Parse(dt3.Rows[0]["total"].ToString()) < 15)
            //{
            //    btn_jialiao3.Enabled = true;

            //}
            lbl_2BiLi.InnerText = dt3.Rows[0]["total"].ToString();
            lbl_2Zhong.InnerText = dt3.Rows[0]["total1"].ToString();
        }
        DataTable dt4 = new DataTable();
        dt4 = Function_TouLiaoRL.GouLiao_RL_CUM_query("三级回炉料", txtBanBie.Value, txtBanZu.Value,equip_no);
        if (dt4.Rows.Count > 0)
        {
            //if (float.Parse(dt4.Rows[0]["total"].ToString()) < 5)
            //{
            //    btn_jialiao4.Enabled = true;

            //}
            lbl_3BiLi.InnerText = dt4.Rows[0]["total"].ToString();
            lbl_3Zhong.InnerText = dt4.Rows[0]["total1"].ToString();
        }

        ///////////////////////

        //txt_shift.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text).Rows[0]["emp_banbie"].ToString();
        //txt_name.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text).Rows[0]["emp_name"].ToString();
        //txt_banzu.Text = Function_Jinglian.Emplogin_query(2, txt_gh.Text).Rows[0]["emp_banzhu"].ToString();
    }
    //三级回炉料加料
    protected void btn_jialiao4_Click(object sender, EventArgs e)
    {

        if (ddlhejin.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合金不能为空！')", true);
            return;
        }


        btn_jialiao1.Enabled = false;
        btn_jialiao2.Enabled = false;
        btn_jialiao3.Enabled = false;
        btn_jialiao4.Enabled = false;
        type ="三级回炉料";
        txttype.Value = "三级回炉料";
        txtmaterial_lot.Enabled = false; 
        txt_maozhong_1.Enabled = true;
        txt_maozhong_1.BackColor = System.Drawing.Color.Yellow;
        btnMaoZhong_confirm_1.Enabled = true;
        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary";
        

    }
    //铝锭加料
    protected void btn_jialiao1_Click(object sender, EventArgs e)
    {
        if (ddlhejin.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合金不能为空！')", true);
            return;
        }

        btn_jialiao1.Enabled = false;
        btn_jialiao2.Enabled = false;
        btn_jialiao3.Enabled = false;
        btn_jialiao4.Enabled = false;
        type = "铝锭";
        txttype.Value = "铝锭";
        txtmaterial_lot.Enabled = true; 
        txt_maozhong_1.Enabled = true;
        txt_maozhong_1.BackColor = System.Drawing.Color.Yellow;
        btnMaoZhong_confirm_1.Enabled = true;
        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary";
    }
    //一级回炉料加料
    protected void btn_jialiao2_Click(object sender, EventArgs e)
    {
        if (ddlhejin.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合金不能为空！')", true);
            return;
        }

        btn_jialiao1.Enabled = false;
        btn_jialiao2.Enabled = false;
        btn_jialiao3.Enabled = false;
        btn_jialiao4.Enabled = false;
        type = "一级回炉料";
        txttype.Value = "一级回炉料";
        txtmaterial_lot.Enabled = false; 

        txt_maozhong_1.Enabled = true;
        txt_maozhong_1.BackColor = System.Drawing.Color.Yellow;
        btnMaoZhong_confirm_1.Enabled = true;
        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary";
        //btnPiZhong_confirm_1.Enabled = true;
        //txt_pizhong_1.Enabled = true;
        //txt_pizhong_1.BackColor = System.Drawing.Color.Yellow;
        
    }
    //二级回炉料加料
    protected void btn_jialiao3_Click(object sender, EventArgs e)
    {
        if (ddlhejin.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合金不能为空！')", true);
            return;
        }
        btn_jialiao1.Enabled = false;
        btn_jialiao2.Enabled = false;
        btn_jialiao3.Enabled = false;
        btn_jialiao4.Enabled = false;
        type = "二级回炉料";;
        txttype.Value = "二级回炉料";
        txtmaterial_lot.Enabled = false; 
        txt_maozhong_1.Enabled = true;
        txt_maozhong_1.BackColor = System.Drawing.Color.Yellow;
        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary";
        btnMaoZhong_confirm_1.Enabled = true;
       
    }
    //确认投料
    protected void btnTouLiao_confirm_1_Click(object sender, EventArgs e)
    {

        if (txttype.Value == "铝锭")
        {
            if (txtmaterial_lot.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('铝锭投料必须填写原材料批号！')", true);
                return;
            }
        }
        Function_TouLiaoRL.touliao_rl_insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, "", ddlhejin.SelectedValue , txttype.Value , txt_maozhong_1.Text, txt_pizhong_1.Text, txt_touliao_1.Text, txtmaterial_lot.Text);
        // txt_maozhong_1 .b
           //  <asp:TextBox ID=txt_maozhong_1 class="form-control input-s-sm" runat="server" ></asp:TextBox>
       ////初始化几台
        if (Request["deviceid"] == "A" || Request["deviceid"] == "B")
        {
            A.Enabled = true;
            A.CssClass = "btn btn-large btn-primary";
            B.Enabled = true;
            B.CssClass = "btn btn-large btn-primary";
            C.Enabled = false;
            C.CssClass = "btn btn-large btn-primary disabled";
            D.Enabled = false;
            D.CssClass = "btn btn-large btn-primary disabled";
        }


        else if (Request["deviceid"] == "C" || Request["deviceid"] == "D")
        {
            C.Enabled = true;
            C.CssClass = "btn btn-large btn-primary";
            D.Enabled = true;
            D.CssClass = "btn btn-large btn-primary";
            A.Enabled = false;
            A.CssClass = "btn btn-large btn-primary disabled";
            B.Enabled = false;
            B.CssClass = "btn btn-large btn-primary disabled";

        }

        E.Enabled = false;
        E.CssClass = "btn btn-large btn-primary disabled";
        E.Enabled = false;
        E.CssClass = "btn btn-large btn-primary disabled";

        A.Enabled = false;
        A.CssClass = "btn btn-large btn-primary disabled";
        //C.Enabled = false;
        //C.CssClass = "btn btn-large btn-primary disabled";
        ///////////
        
       // ini_button();
        ////////////////////刷新加料数据//////////


        DataTable dt1 = new DataTable();
        dt1 = Function_TouLiaoRL.GouLiao_RL_CUM_query("铝锭", txtBanBie.Value, txtBanZu.Value,equip_no);
        if (dt1.Rows.Count > 0)
        {
            //if (float.Parse(dt1.Rows[0]["total"].ToString()) < 50)
            //{
          //  btn_jialiao1.Enabled = true;
            lbl_0BiLi.InnerText = dt1.Rows[0]["total"].ToString();
            lbl_0Zhong.InnerText = dt1.Rows[0]["total1"].ToString();
            // }


        }
        DataTable dt2 = new DataTable();
        dt2 = Function_TouLiaoRL.GouLiao_RL_CUM_query("一级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt2.Rows.Count > 0)
        {
            //if (float.Parse(dt2.Rows[0]["total"].ToString()) < 40)
            //{
            //    btn_jialiao2.Enabled = true;

            //}
            lbl_1BiLi.InnerText = dt2.Rows[0]["total"].ToString();
            lbl_1Zhong.InnerText = dt2.Rows[0]["total1"].ToString();
        }
        DataTable dt3 = new DataTable();
        dt3 = Function_TouLiaoRL.GouLiao_RL_CUM_query("二级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt3.Rows.Count > 0)
        {
            //if (float.Parse(dt3.Rows[0]["total"].ToString()) < 15)
            //{
            //    btn_jialiao3.Enabled = true;

            //}
            lbl_2BiLi.InnerText = dt3.Rows[0]["total"].ToString();
            lbl_2Zhong.InnerText = dt3.Rows[0]["total1"].ToString();
        }
        DataTable dt4 = new DataTable();
        dt4 = Function_TouLiaoRL.GouLiao_RL_CUM_query("三级回炉料", txtBanBie.Value, txtBanZu.Value, equip_no);
        if (dt4.Rows.Count > 0)
        {
            //if (float.Parse(dt4.Rows[0]["total"].ToString()) < 5)
            //{
            //    btn_jialiao4.Enabled = true;

            //}
            lbl_3BiLi.InnerText = dt4.Rows[0]["total"].ToString();
            lbl_3Zhong.InnerText = dt4.Rows[0]["total1"].ToString();
        }

        ////////////////////////////////////
       // txt_touliao_1.Enabled = false;
       // btnTouLiao_confirm_1.Enabled = false;
       // btnTouLiao_confirm_1. CssClass = "btn btn-large btn-primary disabled";
        txt_maozhong_1.Text = "";
        txt_pizhong_1.Text = "";
        txt_touliao_1.Text = "";
        txtmaterial_lot.Text = "";
        txt_touliao_1.BackColor = System.Drawing.Color.Gray;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('投料成功！')", true);
        txtPiHao.Value = Function_TouLiaoRL.TouLiao_RL_NO_query(txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, ddlhejin.SelectedValue ).Rows[0]["touliaono"].ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('投料成功！')", true);



        
        
    }

    //毛重确认
    protected void btnMaoZhong_confirm_1_Click(object sender, EventArgs e)
    {
        if (txt_maozhong_1.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写毛重重量！')", true);
            return;
        }
        //else
        //{
        //    if (Convert.ToInt16(txt_maozhong_1.Text) < 50)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('毛重重量小于50KG！')", true);
        //        return;
        //    }
        //}
       

        btnMaoZhong_confirm_1.Enabled = false;
        txt_maozhong_1.Enabled = false;
        txt_maozhong_1.BackColor = System.Drawing.Color.Gray;
        btnMaoZhong_confirm_1.CssClass = "btn btn-large btn-primary disabled";

        btnPiZhong_confirm_1.Enabled = true;
        btnPiZhong_confirm_1.CssClass = "btn btn-large btn-primary";
        txt_pizhong_1.Enabled = true;
        txt_pizhong_1.BackColor = System.Drawing.Color.Yellow;
      //  txt_pizhong_1.CssClass = "btn btn-large btn-primary disabled";
       

        
    }
    //皮重确认
    protected void btnPiZhong_confirm_1_Click(object sender, EventArgs e)
    {
        if (txt_pizhong_1.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写皮重重量！')", true);
            return;
        }
        //else
        //{
        //    if (Convert.ToInt16(txt_pizhong_1.Text) < 50)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('皮重重量小于50KG！')", true);
        //        return;
        //    }
        //}
        btnTouLiao_confirm_1.Enabled = true;
        btnTouLiao_confirm_1.CssClass = "btn btn-large btn-primary";
       btnPiZhong_confirm_1.Enabled = false;
       btnPiZhong_confirm_1. CssClass = "btn btn-large btn-primary disabled";
       txt_pizhong_1.Enabled = false;
       txt_pizhong_1.BackColor = System.Drawing.Color.Gray;

        //btnTouLiao_confirm_1.Enabled = true;
        txt_touliao_1.Enabled = true;
        txt_touliao_1.BackColor = System.Drawing.Color.Yellow;
        txt_touliao_1 .Text =(float.Parse(txt_maozhong_1.Text )-float.Parse (txt_pizhong_1 .Text )).ToString();
    }
    protected void txt_pizhong_1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void A_Click(object sender, EventArgs e)
    {
        equip_no = "A";
        ini_button();

        string hejing=Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();
        if (ddlhejin.SelectedValue.Trim() != hejing.Trim())
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请确认你选的合金和上次投料不一样！')", true);
        }


    }
    protected void B_Click(object sender, EventArgs e)
    {
        equip_no = "B";

        ini_button();
        string hejing = Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();
        
        if (ddlhejin.SelectedValue.Trim() != hejing.Trim())
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请确认你选的合金和上次投料不一样！')", true);
        }

    }
    protected void C_Click(object sender, EventArgs e)
    {
        equip_no = "C";
        ini_button();
        string hejing = Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();
        
        if (ddlhejin.SelectedValue.Trim() != hejing.Trim())
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请确认你选的合金和上次投料不一样！')", true);
        }
    }
    protected void D_Click(object sender, EventArgs e)
    {
        equip_no = "D";
        ini_button();
        string hejing = Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();
        
        if (ddlhejin.SelectedValue.Trim() != hejing.Trim())
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请确认你选的合金和上次投料不一样！')", true);
        }
    }
    protected void E_Click(object sender, EventArgs e)
    {
        equip_no = "E";
        ini_button();
        string hejing = Function_TouLiaoRL.GouLiao_Hejin_query(Request["deviceid"]).Rows[0]["hejing"].ToString();
        
        if (ddlhejin.SelectedValue.Trim() != hejing.Trim())
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请确认你选的合金和上次投料不一样！')", true);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void ddlhejin_SelectedIndexChanged(object sender, EventArgs e)
    {
        hejing.Text = ddlhejin.SelectedValue;
    }
}