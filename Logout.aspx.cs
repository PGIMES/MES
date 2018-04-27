using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MES.Model;
using MES.BLL;
using MES.DAL;
using Maticsoft.DBUtility;
public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BaseFun fun = new BaseFun();

            txtDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            txtDate.CssClass = "form-control  input-sm disabled";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "css", "<script>$(\"input[id*='txtDate'].attr('class','form-control  input-sm disabled')\")</script>");
           // txtTime.Text = DateTime.Now.ToString("HH:mm:ss");
            //初始登入此台设备人员
           // string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "'";
            string strSQL = "select distinct emp_no,emp_name,emp_no+'_'+emp_name emp from MES_EmpLogin where emp_gongwei='" + Request["gongwei"] + "' and  status=1";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];

            fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp");
            dropGongHao.Items.Insert(0, new ListItem("-请选择登出人员-","0"));

            //init 交接班状态
            tbl = DbHelperSQL.Query("select * from [dbo].[MES_BasType] where type='jiaojie' order by num").Tables[0];
            fun.initDropDownList(dropLogoffstatus, tbl, "num", "value1");
           // txtXingMing.Text = tbl.Rows[0]["emp_name"].ToString();
            //初始化已登入设备
           // strSQL = "select * from MES_EmpLogin where emp_gongwei='"+Request["gongwei"]+"' and emp_no='"+this.dropGongHao.SelectedValue+"' and status=1";


           // txtBanBie.Value = tbl.Rows[0]["emp_name"].ToString();
           // txtBanZu.Value = tbl.Rows[0]["emp_name"].ToString();
            //string banbie = "0";
            //string time = DateTime.Now.ToString("HHmm");
            //if (string.Compare(time, "0730") > 0 && string.Compare(time, "1730") < 0)
            //{
            //    banbie = "白班";
            //}
            //else
            //{ banbie = "晚班"; }
           // dropBanBie.SelectedValue = banbie;//白，晚

        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (dropGongHao.SelectedValue  == "0")
        {
            ScriptManager.RegisterStartupScript(this.btnLogout, this.GetType(), "", "parent.layer.alert('请选择登出工号!');", true);
            return;
        }
        if (chkE.SelectedIndex == -1)
        {
            ScriptManager.RegisterStartupScript(this.btnLogout, this.GetType(), "", "parent.layer.alert('请选择登出设备!');", true);
            return;
        }
        if (dropLogoffstatus.SelectedValue != "1" && txtDemo.Text.Trim() == "")// 有问题，需描述详细
        {
            ScriptManager.RegisterStartupScript(btnLogout, this.GetType(), "", "layer.alert('有问题或不能生产，请在【描述】栏中输入设备状态，交接班状态!');", true);
            return;
        }
        MES.BLL.MES_EmpLogin bll = new MES.BLL.MES_EmpLogin();
        DateTime logoffdate = DateTime.Now;
        for (int i = 0; i < chkE.Items.Count; i++)
        {
            MES.Model.MES_EmpLogin m = new MES.Model.MES_EmpLogin();
            MES.DAL.MES_EquipActionLog dal = new MES.DAL.MES_EquipActionLog();

            m.emp_no = dropGongHao.SelectedValue;
           // m.emp_name = txtEmpName.Text;
          //  m.emp_banbie = dropBanBie.SelectedValue;
          //  m.emp_banzhu = txtBanZhu.Text;           
         //   m.hejing = dropHeJin.SelectedValue;
            //m.id =Convert.ToInt64( DateTime.Now.ToString("yyyyMMddHHmmss"));

            m.logoffdate = logoffdate;
            m.logoffdemo = txtDemo.Text;
            m.Status = "0";//1登入，0登出
            m.emp_gongwei = Request["gongwei"];
            m.logoffstatus = dropLogoffstatus.SelectedItem.Text;
            if (chkE.Items[i].Selected)
            {   
                m.emp_shebei = chkE.Items[i].Value;
                //判断登入设备人员数量，如果只有一人，则关机
                if(DbHelperSQL.Query("select count(1)cnt from MES_EmpLogin where  emp_shebei='"+m.emp_shebei+"'  and logoffdate is null").Tables[0].Rows[0][0].ToString()=="1"){
                    string result = dal.Add(new MES.Model.MES_EquipActionLogModel() { equip_no = m.emp_shebei, logaction = "关机", actionmark = "登出关机", actionreason = "登出关机" });
                };
                //登出                
                bll.Logout(m);
                
         
            }


        }
        //var scr = "<script>var index = parent.layer.getFrameIndex(window.name);alert('ss');</script> ";

      //  Page.ClientScript.RegisterStartupScript(this.GetType(), "clos","<script>layer.alert('登出成功。');</script>", false);
        ScriptManager.RegisterStartupScript(this.btnLogout, this.GetType(), "alertScript", "layer.alert('登出成功。');$('#btnBack').click()", true);
        //Response.Write("<script>"+scr+"</script>");
    }


    protected void txtEmpNo_TextChanged(object sender, EventArgs e)
    {
        string empno = "";// txtEmpNo.Text;
        string strSQL = "select dept.UNITNAME as dept_name,PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME,ORGSTDSTRUCT.UNITNAME as zz" +
            " from [172.16.5.6].[ehr_db].dbo.psnaccount" +
            " left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID" +
            " left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0" +
            " where PSNACCOUNT.accessionstate in(1,2,6) and employeeid='" + empno.Replace("'", "") + "'";

        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string banzu = "";
            string banbie = "";

            if (dt.Rows[0]["zz"].ToString().IndexOf("C") >= 0)
            {
                banzu = "C";
            }
            else if (dt.Rows[0]["zz"].ToString().IndexOf("D") >= 0)
            {
                banzu = "D";
            }
            else { banzu = "其他"; }

          //  txtBanZhu.Text = banzu;
          //  txtEmpName.Text = dt.Rows[0]["TRUENAME"].ToString();



        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.open({title: '提示信息' ,content: '无此可操作账号，请确认后再登入.'}); ", true);
        }
    }
    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        BaseFun fun = new BaseFun();
        //初始化已登入机台设备
        //string strSQL2 = "select a.equip_name,a.equip_no from [dbo].[MES_Equipment] a,[dbo].[MES_EmpLogin] b " +
        //            " where a.equip_no=b.emp_shebei and status=1  and emp_no='01238'";
        string strSQL2 = "select distinct emp_shebei from MES_EmpLogin where emp_gongwei='" + Request["gongwei"] + "' and  status=1 and emp_no='"+this.dropGongHao.SelectedValue+"'";
        DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
        fun.initCheckBoxList(chkE, tbl2, "emp_shebei", "emp_shebei");
        for (int i = 0; i < chkE.Items.Count; i++) {
            chkE.Items[i].Selected = true;
        }
        chkE.Enabled = false;
    }
}