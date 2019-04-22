using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
public partial class BuLeHuanMo : System.Web.UI.Page
{
    Function_Change_MoJu moju = new Function_Change_MoJu();
    protected void Page_Load(object sender, EventArgs e)
    {      

        if (!Page.IsPostBack)
        {


           // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('换模原因不能为空！')", true);

            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            //初始话下拉登入此台设备人员
            string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 and emp_no in (select empid from Emp_Tiaoshi) ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            if (tbl.Rows.Count > 0)
            {
                fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
                txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
                txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
                txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            }
            //初始设备信息
            string strSQL2 = "select * from MES_Equipment where equip_no='" + Request["deviceid"] + "' and equip_type='压铸机'";
            DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
            if (tbl2.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl2.Rows)
                {
                    txtSheBeiHao.Value = dr["equip_no"].ToString();//.Field["equip_no"];
                    txtSheBeiJianCheng.Value = dr["equip_name"].ToString();
                     
                    
                }
            }

            ini_default();

         
                    
           
        }


      
    }


   

    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        Function_Jinglian jl = new Function_Jinglian();

        txtXingMing.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue,"" ).Rows[0]["emp_name"].ToString();
        txtBanBie.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue,"").Rows[0]["emp_banbie"].ToString();
        txtBanZu.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue,"").Rows[0]["emp_banzhu"].ToString();

        DataTable dtpihao = new DataTable();
       

    }
    
    
   
    //确认投料
    protected void btnTouLiao_confirm_1_Click(object sender, EventArgs e)
    {
         Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('投料成功！')", true);
    
    }

     
    
    protected void txt_pizhong_1_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void selLeiBie_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (selLeiBie.SelectedValue == "上模")
        {
            //divShangMo.Attributes.Add("class", "btn btn-large btn-primary disabled");
            //divShangMo.Attributes.Add("class", "btn-padding-s btn-yellow");
            divXiaMo.Visible = false;
            divShangMo.Visible = true;


        }
        else if (selLeiBie.SelectedValue == "下模")
        {
            //divShangMo.Attributes.Add("class", "btn btn-large btn-primary disabled");
            //divShangMo.Attributes.Add("class", "btn-padding-s btn-yellow");
            divShangMo.Visible = false;
            divXiaMo.Visible = true;
           DataTable dt=moju .MoJu_Down_query (Request["deviceid"],"");
           ddlmoju_down.DataSource = dt;
           ddlmoju_down.DataValueField = "moju_no";
           ddlmoju_down.DataTextField = "moju_no";
           ddlmoju_down.DataBind();

           DataTable dt_kw = moju.GP_MoJu_query(ddlmoju_down.SelectedValue);
            
           if (dt.Rows.Count > 0)
           {
               txtMoHao.Text = dt.Rows[0]["mo_no"].ToString();
               txtLingJianMing.Text = dt.Rows[0]["part"].ToString();
               txtmojutype_down.Text = dt.Rows[0]["moju_type"].ToString();
               txtKuWei.Text = dt_kw.Rows[0]["kw"].ToString();
              // txtMoJuHao.Text = dt.Rows[0]["moju_no"].ToString();
           }
            
            }
        else if (selLeiBie.SelectedValue == "先卸模再上模")
        {
            //divShangMo.Attributes.Add("class", "btn btn-large btn-primary disabled");
            //divShangMo.Attributes.Add("class", "btn-padding-s btn-yellow");
            divXiaMo.Visible = true;
            divShangMo.Visible = true;
            DataTable dt = moju.MoJu_Down_query(Request["deviceid"],"");
            ddlmoju_down.DataSource = dt;
            ddlmoju_down.DataValueField = "moju_no";
            ddlmoju_down.DataTextField = "moju_no";
            ddlmoju_down.DataBind();
            DataTable dt_kw = moju.GP_MoJu_query(ddlmoju_down.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                txtMoHao.Text = dt.Rows[0]["mo_no"].ToString();
                txtLingJianMing.Text = dt.Rows[0]["part"].ToString();
                txtmojutype_down.Text = dt.Rows[0]["moju_type"].ToString();
              //  txtMoJuHao.Text = dt.Rows[0]["moju_no"].ToString();
            }
                


        }
    }
    protected void btn_Start_Click(object sender, EventArgs e)
    {
        MES_HuanMo_DAL HuanMo_DAL = new MES_HuanMo_DAL();
        if (dropGongHao.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择操作人员工号！')", true);
            return;
        }
        if (selLeiBie.SelectedValue == "上模" || selLeiBie.SelectedValue == "先卸模再上模")
        {
            if (HuanMo_DAL.GetMoju_statsu(txtMoJuHaoS.Text).Tables[0].Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('此模具未领用，请先领用！')", true);
                return;
            }
        }
        
        if (selYuanYin.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('换模原因不能为空！')", true);
            return;
        }
        if (selLeiBie.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('换模类别不能为空！')", true);
            return;
        }
        if (selYuanYin.SelectedIndex != 3 && selYuanYin.SelectedIndex != 4 && selYuanYin.SelectedIndex != 5)
        {
            if (txtLingJianMingS.Text == "" || txtMoHaoS.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('模具号不存在！')", true);
                return;
            }
        }
        if (selLeiBie.SelectedValue == "上模" )
        {
            DataTable dt = moju.MoJu_Down_query(Request["deviceid"], txtmojutype_up.Text);
            if (dt.Rows.Count > 0)
            {
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('已经有模具在设备上！')", true);
                return;

            }
        }
        else if (selLeiBie.SelectedValue == "下模")
        {
            if (dropZhuangTai.SelectedValue == "异常")
            {
                if (txtShuoMing.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('异常必须填写！')", true);
                    return;
                }
            }

            //txtShuoMing

            DataTable dt = moju.MoJu_Down_query(Request["deviceid"], txtmojutype_down.Text);
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('该设备上无模具可下模！')", true);
                return;
            }
         
        }

        else 
        {

            if (dropZhuangTai.SelectedValue == "异常")
            {
                if (txtShuoMing.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('异常必须填写！')", true);
                    return;
                }
            }


            DataTable dt = moju.MoJu_Down_query(Request["deviceid"], txtmojutype_down.Text);
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('该设备上无模具可下模！')", true);
                return;
            }

            string str1 = "压铸模";
            string str2 = "切边模";

            if ((txtmojutype_up.Text.Contains(str1) && !txtmojutype_down.Text.Contains(str1)) || (txtmojutype_up.Text.Contains(str2) && !txtmojutype_down.Text.Contains(str2)))
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('模具类型必须一致！')", true);
                return;
            }

          
        }
       
        btn_Start.Enabled = false;
        btn_End.Enabled = true;
        btn_Start.CssClass = "btn btn-large btn-primary disabled";
        btn_End.CssClass = "btn btn-large btn-primary ";
        lblStart_time.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");

        if (selYuanYin.SelectedIndex == 1 || selYuanYin.SelectedIndex == 2)
        {
            moju.MoJu_Change_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, selLeiBie.SelectedValue, selYuanYin.SelectedValue, selYuanYinLeiBie.Text, txtMoJuHaoS.Text, txtMoHaoS.Text, txtLingJianMingS.Text, "", "", "", "", "", dropShengChang.SelectedValue, lblStart_time.Text, labendtime.Text, txtmojutype_up.Text, "", txtKuWeiS.Text, "");

        }
        else if (selYuanYin.SelectedIndex == 3 || selYuanYin.SelectedIndex == 4 || selYuanYin.SelectedIndex == 5)
        {

            moju.MoJu_Change_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, selLeiBie.SelectedValue, selYuanYin.SelectedValue, selYuanYinLeiBie.Text, "", "", "", ddlmoju_down.SelectedValue, txtMoHao.Text, txtLingJianMing.Text, dropZhuangTai.SelectedValue, txtShuoMing.Text, "", lblStart_time.Text, labendtime.Text, "", txtmojutype_down.Text,"",txtKuWei.Text);
        }
        else
        {
            moju.MoJu_Change_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, selLeiBie.SelectedValue, selYuanYin.SelectedValue, selYuanYinLeiBie.Text, txtMoJuHaoS.Text, txtMoHaoS.Text, txtLingJianMingS.Text, ddlmoju_down.SelectedValue, txtMoHao.Text, txtLingJianMing.Text, dropZhuangTai.SelectedValue, txtShuoMing.Text, dropShengChang.SelectedValue, lblStart_time.Text, labendtime.Text, txtmojutype_up.Text, txtmojutype_down.Text,txtKuWeiS.Text,txtKuWei.Text);
        }



       // moju.MoJu_Change_Insert(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, selLeiBie.SelectedValue, selYuanYin.SelectedValue, selYuanYinLeiBie.Text, "", "", "", ddlmoju_down.SelectedValue, txtMoHao.Text, txtLingJianMing.Text, dropZhuangTai.SelectedValue, txtShuoMing.Text, dropShengChang.SelectedValue, lblStart_time.Text, labendtime.Text, txtmojutype_up.Text, txtmojutype_down.Text);

    }
    protected void btn_End_Click(object sender, EventArgs e)
    {
            if (dropZhuangTai.SelectedValue == "异常")
            {
                if (txtShuoMing.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('异常必须填写！')", true);
                    return;
                }
            }

            if ((divShangMo.Visible == true && txtMoJuHaoS.Text.Trim() == "") || (divXiaMo.Visible == true && ddlmoju_down.Text.Trim() == ""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('模具号不存在！')", true);
                return;
            }

            labendtime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
            moju.MoJu_Change_Update(dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, selLeiBie.SelectedValue, selYuanYin.SelectedValue, selYuanYinLeiBie.Text, txtMoJuHaoS.Text, txtMoHaoS.Text, txtLingJianMingS.Text, ddlmoju_down.SelectedValue, txtMoHao.Text, txtLingJianMing.Text, dropZhuangTai.SelectedValue, txtShuoMing.Text, dropShengChang.SelectedValue, lblStart_time.Text, labendtime.Text, txtmojutype_up.Text, txtmojutype_down.Text);



            if (selLeiBie.SelectedValue == "先卸模再上模" || selLeiBie.SelectedValue == "下模")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('换模成功！'); layer.open({    type: 2,    title: '模具入库',    shadeClose: true,    shade: 0.8,    area: ['600px', '450px']," +
                                                                                                                  "  content: 'Moju_RK.aspx?mojuno=" + ddlmoju_down.SelectedValue + "&sbno=" + txtSheBeiHao.Value + "'" +
                                                                                                                "})", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('换模成功！')", true);
            }
               
         

        labendtime.Text = "";
        lblStart_time.Text = "";
        btn_Start.Enabled = true;
        btn_End.Enabled = false;
        btn_End.CssClass = "btn btn-large btn-primary disabled";
        btn_Start.CssClass = "btn btn-large btn-primary ";
        SetEmpty();//清空栏位

       
    }
    public void SetEmpty()
    {
        DataTable dt = moju.MoJu_Down_query(Request["deviceid"], "");
        ddlmoju_down.DataSource = dt;
        ddlmoju_down.DataValueField = "moju_no";
        ddlmoju_down.DataTextField = "moju_no";
        ddlmoju_down.DataBind();
        this.ddlmoju_down.Items.Insert(0, new ListItem("", ""));
        txtmojutype_down.Text = "";
        txtLingJianMing.Text = "";
        txtMoHao.Text = "";
        txtKuWei.Text = "";
        txtShuoMing.Text = "";
        txtMoJuHaoS.Text = "";
        txtmojutype_up.Text = "";
        txtLingJianMingS.Text = "";
        txtMoHaoS.Text = "";
        txtKuWeiS.Text = "";

    }

    public void ini_default()
    {
        BaseFun fun = new BaseFun();
        string strSQL = "select * from MES_MoJu_Change where equip_no='" + Request["deviceid"] + "' and change_enddate  is null";
        DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
        if (tbl.Rows.Count > 0)
        {
            lblStart_time.Text = tbl.Rows[0]["change_startdate"].ToString();
            btn_Start.Enabled = false;
            btn_End.Enabled = true;
            btn_End.CssClass = "btn btn-large btn-primary ";
            btn_Start.CssClass = "btn btn-large btn-primary  disabled";
            txtleibie.Text=tbl.Rows[0]["change_type"].ToString();//换模类别
            selYuanYinLeiBie.Text = tbl.Rows[0]["reason_type"].ToString();//原因类别
            selYuanYin.Text = tbl.Rows[0]["chang_reason"].ToString();//换模原因
            if (selYuanYin.SelectedIndex == 3 || selYuanYin.SelectedIndex == 4 || selYuanYin.SelectedIndex == 5 || selYuanYin.SelectedIndex == 6 || selYuanYin.SelectedIndex == 7 || selYuanYin.SelectedIndex == 8 || selYuanYin.SelectedIndex == 9)
            {
                divXiaMo.Visible = true;
                divShangMo.Visible = false;
                fun.initDropDownList(ddlmoju_down, tbl, "moju_no_down", "moju_no_down");
               // ddlmoju_down.Text = tbl.Rows[0]["moju_no_down"].ToString().Trim();//模具号（下）
                txtmojutype_down.Text = tbl.Rows[0]["moju_type_down"].ToString();//模具类型（下）
                txtLingJianMing.Text = tbl.Rows[0]["part_down"].ToString();//零件名称（下）
                txtMoHao.Text = tbl.Rows[0]["mo_no_down"].ToString();//模号（下）
                dropZhuangTai.Text = tbl.Rows[0]["moju_status_down"].ToString().Trim();//模具状态（下）
                txtShuoMing.Text = tbl.Rows[0]["moju_status_demo_down"].ToString();//说明（下）
            }
            if (selYuanYin.SelectedIndex == 1 || selYuanYin.SelectedIndex == 2 || selYuanYin.SelectedIndex == 6 || selYuanYin.SelectedIndex == 7 || selYuanYin.SelectedIndex == 8 || selYuanYin.SelectedIndex == 9)
            {
                divXiaMo.Visible = false;
                divShangMo.Visible = true;
                txtMoJuHaoS.Text = tbl.Rows[0]["moju_no_up"].ToString();//模具号（上）
                txtmojutype_up.Text = tbl.Rows[0]["moju_type_up"].ToString();//模具类型（上）
                txtLingJianMingS.Text = tbl.Rows[0]["part_up"].ToString();//零件名称（上）
                txtMoHaoS.Text = tbl.Rows[0]["mo_no_up"].ToString();//模号（上）
                dropShengChang.Text = tbl.Rows[0]["product_status_up"].ToString();
                
            }
            if (selYuanYin.SelectedIndex == 6 || selYuanYin.SelectedIndex == 7 || selYuanYin.SelectedIndex == 8 || selYuanYin.SelectedIndex == 9)
            {
                divXiaMo.Visible = true;
                divShangMo.Visible = true;
            }
        }
       

    //  btn_Start,btn_End
        else
        {
            btn_Start.Enabled = true;
            btn_End.Enabled = false;
            btn_Start.CssClass = "btn btn-large btn-primary ";
            btn_End.CssClass = "btn btn-large btn-primary  disabled";
        }

    }
    protected void txtMoJuHaoS_TextChanged(object sender, EventArgs e)
    {   

        DataTable dt=moju .GP_MoJu_query(txtMoJuHaoS .Text  );
        if (dt.Rows.Count > 0)
        {
            txtLingJianMingS.Text = dt.Rows[0]["pn"].ToString();
            txtMoHaoS.Text = dt.Rows[0]["mojuno_no"].ToString();
            txtmojutype_up.Text = dt.Rows[0]["type"].ToString();
            txtKuWeiS.Text = dt.Rows[0]["kw"].ToString();
        }

        else
        {
            txtLingJianMingS.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('模具号不存在，请联系模具管理员及时维护该模具号！')", true);
            return;
        }
    }
    protected void selYuanYin_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (selYuanYin.SelectedIndex == 1 || selYuanYin.SelectedIndex == 3 || selYuanYin.SelectedIndex == 6)
        {
            selYuanYinLeiBie.Text ="生产";
        }
        else if (selYuanYin.SelectedIndex == 2 || selYuanYin.SelectedIndex == 4 || selYuanYin.SelectedIndex == 7 || selYuanYin.SelectedIndex == 8)
        {
            selYuanYinLeiBie.Text ="调试";
        }
        else if ( selYuanYin.SelectedIndex == 5 || selYuanYin.SelectedIndex == 9)
        {
            selYuanYinLeiBie.Text = "故障";
        }


        if (selYuanYin.SelectedIndex == 1 || selYuanYin.SelectedIndex == 2 )
        {
           // selYuanYinLeiBie.Text = "生产";
            selLeiBie.SelectedValue = "上模";
            txtleibie.Text = "上模";
            divXiaMo.Visible = false;
            divShangMo.Visible = true;
            //清空下模数据
            ddlmoju_down.Items.Clear();
            txtmojutype_down.Text = "";
            txtLingJianMing.Text = "";
            txtMoHao.Text = "";
            txtKuWei.Text = "";
           // dropZhuangTai.Items.Clear();
            txtShuoMing.Text = "";
        }
        else if (selYuanYin.SelectedIndex == 3 || selYuanYin.SelectedIndex == 4 || selYuanYin.SelectedIndex == 5 )
        {
           // selYuanYinLeiBie.Text = "调试";
            selLeiBie.SelectedValue = "下模";
            txtleibie.Text = "下模";
            divShangMo.Visible = false;
            divXiaMo.Visible = true;
            //清空上模数据
            txtMoJuHaoS.Text = "";
            txtmojutype_up.Text = "";
            txtLingJianMingS.Text = "";
            txtMoHaoS.Text = "";
            txtKuWeiS.Text = "";

            DataTable dt = moju.MoJu_Down_query(Request["deviceid"], "");

            ddlmoju_down.DataSource = dt;
            ddlmoju_down.DataValueField = "moju_no";
            ddlmoju_down.DataTextField = "moju_no";
            ddlmoju_down.DataBind();

            DataTable dt_kw = moju.GP_MoJu_query(ddlmoju_down.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                txtMoHao.Text = dt.Rows[0]["mo_no"].ToString();
                txtLingJianMing.Text = dt.Rows[0]["part"].ToString();
                txtmojutype_down.Text = dt.Rows[0]["moju_type"].ToString();
                txtKuWei.Text = dt_kw.Rows[0]["kw"].ToString();
                // txtMoJuHao.Text = dt.Rows[0]["moju_no"].ToString();
            }

        }
        else if (selYuanYin.SelectedIndex == 6 || selYuanYin.SelectedIndex == 7 || selYuanYin.SelectedIndex == 8 || selYuanYin.SelectedIndex == 9)
        {
            //selYuanYinLeiBie.Text = "故障";
            selLeiBie.SelectedValue = "先卸模再上模";
            txtleibie.Text = "先卸模再上模";
            divXiaMo.Visible = true;
            divShangMo.Visible = true;
            DataTable dt = moju.MoJu_Down_query(Request["deviceid"], "");
            ddlmoju_down.DataSource = dt;
            ddlmoju_down.DataValueField = "moju_no";
            ddlmoju_down.DataTextField = "moju_no";
            ddlmoju_down.DataBind();

            DataTable dt_kw = moju.GP_MoJu_query(ddlmoju_down.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                txtMoHao.Text = dt.Rows[0]["mo_no"].ToString();
                txtLingJianMing.Text = dt.Rows[0]["part"].ToString();
                txtmojutype_down.Text = dt.Rows[0]["moju_type"].ToString();
                txtKuWei.Text = dt_kw.Rows[0]["kw"].ToString();
                //  txtMoJuHao.Text = dt.Rows[0]["moju_no"].ToString();
            }

        }


    }
    protected void ddlmoju_down_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = moju.GP_MoJu_query(ddlmoju_down .SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtLingJianMing.Text = dt.Rows[0]["pn"].ToString();
            txtMoHao.Text = dt.Rows[0]["mojuno_no"].ToString();
            txtmojutype_down.Text = dt.Rows[0]["type"].ToString();
            txtKuWei.Text = dt.Rows[0]["kw"].ToString();

            //txtMoHao.Text = dt.Rows[0]["mo_no"].ToString();
            //txtLingJianMing.Text = dt.Rows[0]["part"].ToString();
        }

        else
        {
            txtLingJianMing.Text = "";
            txtMoHao.Text = "";
            txtmojutype_down.Text = "";
            txtKuWei.Text = "";
        }
    }
}