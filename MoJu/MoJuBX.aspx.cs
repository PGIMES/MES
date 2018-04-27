using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
public partial class MoJuBX : System.Web.UI.Page
{
    Function_Change_MoJu moju = new Function_Change_MoJu();
    Moju Mj = new Moju();
    protected void Page_Load(object sender, EventArgs e)
    {      

        if (!Page.IsPostBack)
        {	
            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            //初始话下拉登入此台设备人员
            string strSQL = "select * from MES_EmpLogin where  emp_shebei='"+Request["deviceid"]+"' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
            //--确认工号
            fun.initDropDownList(dropQr_gh, tbl, "emp_no", "emp_no");
            dropQr_gh.Items.Insert(0, new ListItem("-请选择工号-", ""));

            txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
            txtBanBie.Value= tbl.Rows[0]["emp_banbie"].ToString();
            txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            string sql = "select base_value from MES_Base_code where base_code='MOJU_BX_TYPE'";
            DataTable dt_type = SQLHelper.reDs(sql).Tables[0];
            this.ddl_gz.DataSource = dt_type;
            this.ddl_gz.DataTextField = "base_value";
            this.ddl_gz.DataValueField = "base_value";
            this.ddl_gz.DataBind();
            this.ddl_gz.Items.Insert(0, new ListItem("", ""));
            DataTable dt = Mj.MoJu_no_query(Request["deviceid"], "");
            ddlMoJuHao.DataSource = dt;
            ddlMoJuHao.DataValueField = "moju_no";
            ddlMoJuHao.DataTextField = "moju_no";
            ddlMoJuHao.DataBind();
            this.ddlMoJuHao.Items.Insert(0, new ListItem("", ""));
            string strSQL2 = "select * from MES_Equipment where equip_no='" + Request["deviceid"] + "'";
            DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
            if (tbl2.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl2.Rows)
                {
                 
                    txtSbname.Text = dr["equip_name"].ToString();
                }
            }

            //init MoXiu 维修 person
            tbl = fun.getMoXiuEmpInfo("");
            fun.initDropDownList(dropWXGongHao, tbl, "EMPLOYEEID",  "EMPLOYEENAME");
            dropWXGongHao.Items.Insert(0, new ListItem("-请选择工号-", ""));
            dropWXGongHao.Items.Add(new ListItem("01713_汪朋", "01713"));
            dropWXGongHao.Items.Add(new ListItem("WXG1", "WXG1"));  
            dropWXGongHao.Items.Add(new ListItem("weixiugong1", "weixiugong1"));
            dropWXGongHao.Items.Add(new ListItem("mujuweixiu", "mujuweixiu")); 
            txtWXBanZu.Value = fun.GetBanBie();

           // GetGrid();
            BindData(" status<>'确认完成'");
            enableBtn(false, false, false);        
           
        }


      
    }
    //暂时不用
    //protected void GetGrid()
    //{
    //    DataTable dt = Mj.Moju_BX_Query(2, "", "", "", "", "", "", "", "", "", "", "", "");
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //    if (dt == null || dt.Rows.Count <= 0)
    //    {
    //        Div_Undo.Style.Add("display", "none");
    //    }
    //    else
    //    {
    //        Div_Undo.Style.Add("display", "block");

    //    }
    //}


    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Function_Jinglian jl = new Function_Jinglian();

        //txtXingMing.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"" ).Rows[0]["emp_name"].ToString();
        //txtBanBie.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"").Rows[0]["emp_banbie"].ToString();
        //txtBanZu.Value = jl.Emplogin_query(5, dropGongHao.SelectedValue,"").Rows[0]["emp_banzhu"].ToString();

        //DataTable dtpihao = new DataTable();
        BaseFun fun = new BaseFun();
        string name = fun.getEmpNameById(this.dropGongHao.SelectedValue.Trim());
        if (name == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"layer.alert('无此工号，请确认！');$('#" + this.dropGongHao.ClientID + "').select();", true);
            return;
        }
        txtXingMing.Value = name;
        //同步刷新确认人信息
        this.dropQr_gh.SelectedValue = this.dropGongHao.SelectedValue;
        this.txtQr_Name.Value = name;
    }

   
   
    protected void btn_Start_Click(object sender, EventArgs e)
    {
        if (ddlMoJuHao.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('模具号不能为空！')", true);
            return;
        }
     
        if (ddlstop.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择是否停机！')", true);
            return;
        }
        if (ddl_gz.Text == "" || ddl_gz_ms.Text=="")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('故障类型和故障描述必须填写！')", true);
            return;
        }
        if ( Mj.IsInUse(ddlMoJuHao.Text.Trim()) ==false)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('此模具正在维修确认中,请勿重复报修！')", true);
            return;
        }


        lblStart_time.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");


        int result = Mj.Moju_BX_Insert(1, dropGongHao.Text.Trim(), txtXingMing.Value.Trim().Trim(), txtBanBie.Value.Trim(), txtBanZu.Value.Trim(), ddlMoJuHao.Text.Trim(), Request["deviceid"], txtSbname.Text.Trim(), txtMoJutype.Text.Trim(), txtMoJuljh.Text.Trim(), txtMono.Text.Trim(), ddl_gz.Text.Trim(), ddl_gz_ms.Text.Trim(), lblStart_time.Text.Trim(),ddlstop.SelectedValue);
        if (result >= 1)
       {
           Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('报修成功！')", true);
           BindData(" status<>'确认完成'");
           lblStart_time.Text = "";
           txtMoJutype.Text = "";
         
           txtMoJuljh.Text = "";
           txtMono.Text = "";
           ddl_gz.Text = "";
           ddl_gz_ms.Text = "";
           ddlMoJuHao.Text = "";
       }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        BindData(" status<>'确认完成'");
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        if (e.Row.RowIndex != -1)
    //        {
    //            int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
    //            e.Row.Cells[0].Text = indexID.ToString();
    //        }

    //        if (Convert.ToDecimal(e.Row.Cells[10].Text) > 1)
    //        {
    //            e.Row.BackColor = System.Drawing.Color.Yellow;
    //        }
          
    //    }
    //}
    
    protected void ddlMoJuHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt =Mj.MoJu_no_query(Request["deviceid"], ddlMoJuHao.Text);
        //ddlMoJuHao.DataSource = dt;
        //ddlMoJuHao.DataValueField = "moju_no";
        //ddlMoJuHao.DataTextField = "moju_no";
        //ddlMoJuHao.DataBind();

        if (dt.Rows.Count > 0)
        {
            txtMono.Text = dt.Rows[0]["mo_no"].ToString();
            txtMoJuljh.Text = dt.Rows[0]["part"].ToString();
            txtMoJutype.Text = dt.Rows[0]["moju_type"].ToString();

        }
    }

    //======================维修，确认======  updated by fish=============
    
    public void BindData(string strWhere)
    {
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        DataSet ds = dal.GetListBX(strWhere);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }
    private void enableBtn(Boolean btnStart, Boolean btnEnd, Boolean btnConfirm)
    { 
        this.btnStart.Enabled = btnStart; this.btnStart.CssClass = "btn btn-primary " + (btnStart == true ? "" : "true");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "setcss", @"setenableWXStart(" + btnStart.ToString().ToLower() + ");setenableWXFinish(" + btnEnd.ToString().ToLower() + ");setenableWXConfirm(" + btnConfirm.ToString().ToLower() + ");", true);
            
       
        this.btnEnd.Enabled = btnEnd; this.btnEnd.CssClass = "btn btn-primary " + (btnStart == true ? "" : "true");

        this.btnConfirm.Enabled = btnConfirm; this.btnConfirm.CssClass = "btn btn-primary " + (btnStart == true ? "" : "true");
    }
    //开始维修
    protected void btnStart_Click(object sender, EventArgs e)
    {
        MES.Model.MES_SB_WX m = new MES.Model.MES_SB_WX();
        string wx_dh = GridView1.SelectedRow.Cells[1].Text.Trim();
        txtHidden.Value = wx_dh;
        m.wx_dh = wx_dh;
        m.wx_begin_date = DateTime.Now;
        m.wx_banbie = txtBanBie.Value;
        m.wx_banzhu = txtBanZu.Value;
        m.wx_gonghao = dropWXGongHao.SelectedValue;
        m.wx_name = txtWXXingMing.Value;
        m.p_status = "维修中";
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        try
        {
            if (DbHelperSQL.GetSingle("select count(1) from [dbo].[MES_SB_wx] where wx_dh='" + wx_dh + "'").ToString() == "0")
            {
               int result= dal.Add(m);//因经常出现资料未插入事件
                if (result >= 1)
                {
                    enableBtn(false, true, false);
                    BindData(" status<>'确认完成'");
                }
                else if (result == 0)
                {                    
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"layer.alert('插入维修资料失败，请重试【开始维修】'); ", true);
                }

            }
           
        }
        catch(Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"layer.alert('失败，请重试.[ex:"+ ex.Message + "]'); ", true);
        }
        finally { }
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
        enableBtn(false, false, false);
    }
    protected void dropResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropResult.SelectedValue != "恢复正常")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"$('#" + txtMo_Down_cs.ClientID + "').css('background-color','yellow').focus();", true);
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
        m.qr_gh = dropQr_gh.SelectedValue;
        m.qr_name = this.txtQr_Name.Value;
        m.qr_remark = this.txtQr_Remark.Value;
        m.p_status = "确认完成";
        MES.DAL.MES_SB_WX dal = new MES.DAL.MES_SB_WX();
        if (dal.IsExitsQR(m.dh) == 0)//--确认是否重复确认
        { 
            dal.Add(m);
        }

        string strSQL = "update MES_SB_BX set [status]='" + m.p_status + "' where bx_dh='"+ m.dh + "';";
        DbHelperSQL.ExecuteSql(strSQL);//因经常会无法更新到状态，固在此重复更新一次


        BindData(" status<>'确认完成'");
        enableBtn(false, false, false);
        strSQL = "SELECT wx_result FROM MES_SB_WX WHERE wx_dh='" + wx_dh + "'  ";
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
        if (name == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"layer.alert('无此工号，请确认！');$('#" + this.dropQr_gh.ClientID + "').select();", true);
            return;
        }
        txtQr_Name.Value = name;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "set", @"$('#" + txtQr_Remark.ClientID + "').addClass('input-edit').focus();", true);
    }

   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {   if(e.Row.RowType!=DataControlRowType.EmptyDataRow)
        {   if (e.Row.Cells[1].Text == txtHidden.Value)
            {
                e.Row.BackColor = System.Drawing.Color.Cyan;
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool bln = false;

        BaseFun fun = new BaseFun();
        foreach (GridViewRow row in GridView1.Rows)
        {
            row.BackColor = System.Drawing.Color.White;
        }
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Cyan;
        GridViewRow gv = GridView1.SelectedRow;

        if (gv.Cells[12].Text.Trim() == "维修中")
        {
            //Session["empid"] = "";
            if ((this.dropWXGongHao.Items.FindByValue(Session["empid"].ToString().ToLower())!=null&& Session["empid"].ToString()!="")||(this.dropWXGongHao.Items.FindByValue(Session["loginUser"].ToString().ToLower())!=null&& Session["loginUser"].ToString()!=""))
            {
                bln = true;
            }
            //非模具维修人员，不可点击维修完成
            if (bln == true)
            {
                enableBtn(false, true, false);
            }
            else
            {
                enableBtn(false, false, false);
            }

            

        }
        else if (gv.Cells[12].Text.Trim() == "维修完成" )
        {
            DataTable dt = fun.Get_WX_Record(gv.Cells[1].Text);
            txtWX_CS.Value = dt.Rows[0]["wx_cs"].ToString();
            dropResult.Text = dt.Rows[0]["wx_result"].ToString();
            txtMo_Down_cs.Value = dt.Rows[0]["mo_down_cs"].ToString();            
            enableBtn(false, false, true);          

        }
        else //报修完成
        {
            enableBtn(true, false, false);
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>window.location.href='/BuLeHuanMo/BuLeHuanMo.aspx?deviceid=" + Request["deviceid"] + "';</script>");
    }

    protected void dropWXGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {  
       // string str=dropWXGongHao.SelectedItem.Text.Trim();
       // txtWXXingMing.Value = str.Substring(str.IndexOf('_')+1);
        BaseFun fun = new BaseFun();
        DataTable tbl = fun.getMoXiuEmpInfo(dropWXGongHao.SelectedValue);
        if (tbl.Rows.Count > 0)
        {
            txtWXXingMing.Value = tbl.Rows[0]["TRUENAME"].ToString(); 
            // txtWXBanZu.Value = tbl.Rows[0]["zz"].ToString();
        }
        else
        {
            txtWXXingMing.Value = "";
        }
       // txtWXBanBie.Value = fun.GetBanBie();
      
        
    }
    protected void lbtn_Click(object sender, EventArgs e)
    {
        BindData(" status<>'确认完成'");
    }
}