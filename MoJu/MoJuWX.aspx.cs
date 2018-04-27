using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
public partial class MoJuWX : System.Web.UI.Page
{   

    Function_TouLiao_RL Function_TouLiaoRL = new Function_TouLiao_RL();
    protected void Page_Load(object sender, EventArgs e)
    {      

        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            //初始话下拉登入此台设备人员           
            string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            fun.initDropDownList(dropQr_gh, tbl, "emp_no", "emp_no");
            dropQr_gh.Items.Insert(0, new ListItem("-请选择工号-",""));
            //init MoXiu person
            tbl = fun.getMoXiuEmpInfo("");
            fun.initDropDownList(dropGongHao, tbl, "EMPLOYEEID", "EMPLOYEEID");
            dropGongHao.Items.Insert(0, new ListItem("-请选择工号-", ""));
           // txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
            txtBanBie.Value= fun.GetBanBie();//tbl.Rows[0]["emp_banbie"].ToString()
           // txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();

            BindData(" status<>'确认完成'");
            enableBtn(false,false,false);
           
        }


      
    }
   
    public void BindData(string strWhere)
    {
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        DataSet ds = dal.GetListBX(strWhere);
        GridView1.DataSource=ds;
        GridView1.DataBind();
        
    }


    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Function_Jinglian jl = new Function_Jinglian();
        BaseFun fun = new BaseFun();
        DataTable tbl = fun.getMoXiuEmpInfo(dropGongHao.SelectedValue);
        txtXingMing.Value = tbl.Rows[0]["TRUENAME"].ToString();
        txtBanBie.Value = fun.GetBanBie();
        txtBanZu.Value = tbl.Rows[0]["zz"].ToString();
        
    }
    
     
  
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {    
          BaseFun fun = new BaseFun();
        foreach (GridViewRow row in GridView1.Rows)
        {          
            row.BackColor = System.Drawing.Color.White;
        }
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Cyan;
        GridViewRow gv=GridView1.SelectedRow;

        if (gv.Cells[11].Text.Trim() == "维修中")
        {
            enableBtn(false,true,false );
        }
        else if (gv.Cells[11].Text.Trim() == "维修完成")
        {
            DataTable dt = fun.Get_WX_Record(gv.Cells[1].Text);
            txtWX_CS.Value = dt.Rows[0]["wx_cs"].ToString();
            dropResult.Text = dt.Rows[0]["wx_result"].ToString();
            txtMo_Down_cs.Value = dt.Rows[0]["mo_down_cs"].ToString();
            enableBtn(false, false, true);
        }
        else //报修
        {
            enableBtn(true, false, false);
        }
       
    }
    private void enableBtn(Boolean btnStart, Boolean btnEnd, Boolean btnConfirm) 
    {
        this.btnStart.Enabled = btnStart;  this.btnStart.CssClass="btn btn-primary "+(btnStart==true ? "":"true");
        this.btnEnd.Enabled = btnEnd; this.btnEnd.CssClass = "btn btn-primary " + (btnStart == true ? "" : "true");
        this.btnConfirm.Enabled = btnConfirm; this.btnConfirm.CssClass = "btn btn-primary " + (btnStart == true ? "" : "true");          
    }
    private void setbackgroundEnd()
    { 
        
    }
    //开始维修
    protected void btnStart_Click(object sender, EventArgs e)
    {
        MES.Model.MES_SB_WX m = new MES.Model.MES_SB_WX();
        string wx_dh=GridView1.SelectedRow.Cells[1].Text.Trim();
        txtHidden.Value = wx_dh;
        m.wx_dh = wx_dh;        
        m.wx_begin_date = DateTime.Now;
        m.wx_banbie = txtBanBie.Value;
        m.wx_banzhu = txtBanZu.Value;
        m.wx_gonghao = dropGongHao.SelectedValue;
        m.wx_name = txtXingMing.Value;
        m.p_status = "维修中";
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        dal.Add(m);
        BindData(" status<>'确认完成'");
        enableBtn(false, true, false);
    }
    //维修完成
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        MES.Model.MES_SB_WX m = new MES.Model.MES_SB_WX();
        string wx_dh = GridView1.SelectedRow.Cells[1].Text.Trim();
       
        m.wx_dh = wx_dh;
        m.wx_end_date = DateTime.Now;       
        m.wx_cs = txtWX_CS.Value;
        m.wx_result = dropResult.SelectedItem.Value;
        m.mo_down_cs = txtMo_Down_cs.Value;
        m.p_status = "维修完成";
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        dal.Update(m);
        BindData(" status<>'确认完成'");
    }
    protected void dropResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(dropResult.SelectedValue!="恢复正常")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),"set",@"$('#"+txtMo_Down_cs.ClientID +"').css('background-color','yellow').focus();",true);
        }
    }
    //确认完成
    protected void btnConfirm_Click(object sender, EventArgs e)
    {        
        //-------
        MES.Model.MES_SB_QR m = new MES.Model.MES_SB_QR();
        string wx_dh = GridView1.SelectedRow.Cells[1].Text.Trim();
        m.dh = wx_dh;
        m.qr_date = DateTime.Now;
        m.qr_banbie = txtBanBie.Value;
        m.qr_banzhu = txtBanZu.Value;
        m.qr_gh = dropQr_gh.SelectedValue ;
        m.qr_name = this.txtQr_Name.Value;
        m.qr_remark = this.txtQr_Remark.Value;
        m.p_status = "确认完成";
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        dal.Add(m);
        BindData(" status<>'确认完成'");
        enableBtn(false, false, false);
        string strSQL = "SELECT wx_result FROM MES_SB_WX WHERE wx_dh='" + wx_dh + "'  ";
        DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
        if (tbl.Rows[0][0].ToString() == "需下模维修")
        {
            string str = "layer.confirm('维修结果为：需下模维修，是否直接跳转至换模页面？', {  btn: ['是','否'] }, function(){  $('#MainContent_btnNext').click();}, function(){  });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
           
        }
    }
  
   
    protected void dropQr_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        BaseFun fun = new BaseFun();
        string name = fun.getEmpNameById(this.dropQr_gh.SelectedValue.Trim());
        if(name=="")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"layer.alert('无此工号，请确认！');$('#" + this.dropQr_gh.ClientID + "').select();", true);
            return;
        }
        txtQr_Name.Value = name;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"$('#" + txtQr_Remark.ClientID + "').css('background-color','yellow').focus();", true);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[1].Text==txtHidden.Value)
        {
            e.Row.BackColor = System.Drawing.Color.Cyan;
        }
         
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>window.location.href='/BuLeHuanMo/BuLeHuanMo.aspx?deviceid=" + Request["deviceid"] + "';</script>");
    }
}