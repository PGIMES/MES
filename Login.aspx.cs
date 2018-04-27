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
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BaseFun fun = new BaseFun();
           // fun.initHeJin(dropHeJin);
            string gongwei = DbHelperSQL.GetSingle("select distinct gongwei from [dbo].[MES_Equipment] where equip_station_desc='" + Request["quyu"] + "'").ToString();

            fun.initDropDownList(dropCheJian, DbHelperSQL.Query("select * from MES_BasType where type='chejian'").Tables[0], "value1", "value1");
            txtDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtTime.Text = DateTime.Now.ToString("HH:mm:ss");

            txtGangWei.Text = gongwei;

            string quyu = DbHelperSQL.GetSingle("select distinct equip_quyu from [dbo].[MES_Equipment] where gongwei='" + gongwei + "'").ToString();
            if (dropCheJian.Items.FindByValue(quyu) != null) {
                dropCheJian.SelectedValue = quyu;
            }

            fun.initEquipment(chkE, " equip_station_Desc='" + Request["quyu"] + "' and inuse='Y' order by equip_no");

            string banbie = "0";
            string time = DateTime.Now.ToString("HHmm");
            if (string.Compare(time, "0730") > 0 && string.Compare(time, "1730") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            dropBanBie.SelectedValue = banbie;//白，晚
            this.btnLogin.Attributes.Add("onclick", "return closelogin();");
            //init 交接班状态列表
            DataTable tbl = DbHelperSQL.Query("select * from [dbo].[MES_BasType] where type='jiaojie' order by num").Tables[0];
            fun.initDropDownList(dropLoginstatus, tbl, "num", "value1");

            string strDemo = DbHelperSQL.GetSingle("select  dbo.fn_GetJiaojieDemo( '" + Request["quyu"] + "')").ToString();
            lblDemo.Text = strDemo;

            txtBanZhu.CssClass = "form-control  input-sm";
            this.txtDate.CssClass = "form-control  input-sm";
            txtTime.CssClass = "form-control  input-sm";
            this.txtEmpName.CssClass = "form-control  input-sm";
            this.txtGangWei.CssClass = "form-control  input-sm";
            this.dropBanBie.CssClass = "form-control  input-sm";
            txtBanZhu.CssClass = "form-control  input-sm";
            dropCheJian.CssClass = "form-control  input-sm";

        }

        //if (Request["RequestType"] == "AjaxRequest")//没用到
        //{
        //    string id = Request["id"];

        //    string empid = ((System.Web.UI.WebControls.TextBox)this.Form.FindControl("ctl00$MainContent$txtEmpNo")).Text;
        //    string empname = ((System.Web.UI.WebControls.TextBox)this.Form.FindControl("ctl00$MainContent$txtEmpName")).Text;
        //    string banbie2 = ((System.Web.UI.WebControls.DropDownList)this.Form.FindControl("ctl00$MainContent$dropBanBie")).SelectedValue;
        //    string banzhu = ((System.Web.UI.WebControls.TextBox)this.Form.FindControl("ctl00$MainContent$txtBanZhu")).Text;
        //    string logindemo = ((System.Web.UI.WebControls.TextBox)this.Form.FindControl("ctl00$MainContent$txtDemo")).Text;
        //    string hejin = ((System.Web.UI.WebControls.DropDownList)this.Form.FindControl("ctl00$MainContent$dropHeJin")).SelectedValue;
        //    CheckBoxList chkE2 = ((System.Web.UI.WebControls.CheckBoxList)this.Form.FindControl("ctl00$MainContent$chkE"));
        //    if (empid.TrimEnd() == "")
        //    {
        //        Response.Clear();
        //        Response.Write("请输入工号！");
        //        // Response.Write("ID : " + id + " ," + DateTime.Now);
        //        Response.End();
        //        return;
        //    }
        //    MES.BLL.MES_EmpLogin bll = new MES.BLL.MES_EmpLogin();
        //    for (int i = 0; i < chkE2.Items.Count; i++)
        //    {
        //        MES.Model.MES_EmpLogin m = new MES.Model.MES_EmpLogin();
        //        m.emp_no = empid;
        //        m.emp_name = empname;
        //        m.emp_banbie = banbie2;
        //        m.emp_banzhu = banzhu;
        //        m.emp_gongwei = "";
        //        m.hejing = hejin;
        //        //m.id =Convert.ToInt64( DateTime.Now.ToString("yyyyMMddHHmmss"));
        //        m.logindate = DateTime.Now;
        //        m.logindemo = logindemo;
        //        m.Status = "1";//1登入，0登出
        //        m.emp_gongwei = Request["gongwei"];
        //        if (chkE.Items[i].Selected)
        //        {
        //            m.emp_shebei = chkE.Items[i].Value;
        //            // bll.Add(m);
        //        }


        //    }

        //    Response.Clear();
        //    Response.Write("");// Response.Write("ID : " + id + " ," + DateTime.Now)
        //    Response.End();
        //    return;
        //} 
    }
   
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtEmpNo.Text.TrimEnd() == "")
        {
            ScriptManager.RegisterStartupScript(btnLogin,this.GetType(), "", "layer.alert('请输入工号!');", true);
            return;
        }
        if (chkE.SelectedIndex == -1)
        {
            ScriptManager.RegisterStartupScript(btnLogin,this.GetType(), "", "layer.alert('请选择登入设备!');", true);
            return;
        }
        if (dropLoginstatus.SelectedValue != "1"&&txtDemo.Text.Trim()=="")// 有问题，需描述详细
        {
            ScriptManager.RegisterStartupScript(btnLogin, this.GetType(), "", "layer.alert('有问题或不能生产，请在【描述】栏中输入设备状态，交接班状态!');", true);
            return;
        }
        DateTime logindate = DateTime.Now;
        MES.BLL.MES_EmpLogin bll = new MES.BLL.MES_EmpLogin();
        for (int i = 0; i < chkE.Items.Count; i++)
        {
            MES.Model.MES_EmpLogin m = new MES.Model.MES_EmpLogin();
            m.emp_no = txtEmpNo.Text;
            m.emp_name = txtEmpName.Text;
            m.emp_banbie = dropBanBie.SelectedValue;
            m.emp_banzhu = txtBanZhu.Text;
            m.emp_gongwei = "";
            m.hejing = "";//dropHeJin.SelectedValue;
            //m.id =Convert.ToInt64( DateTime.Now.ToString("yyyyMMddHHmmss"));
            m.logindate = logindate;
            m.logindemo = txtDemo.Text;
            m.Status = "1";//1登入，0登出
            m.emp_gongwei = DbHelperSQL.GetSingle("select distinct gongwei from [dbo].[MES_Equipment] where equip_station_desc='" + Request["quyu"] + "'").ToString();
            m.loginstatus = dropLoginstatus.SelectedItem.Text;
            m.logoffstatus = "";
            m.emp_chejian = dropCheJian.SelectedValue;
            m.loginflag = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (chkE.Items[i].Selected)
            { 
                m.emp_shebei = chkE.Items[i].Value;
                bool cnt=false;
                cnt = DbHelperSQL.Exists("select * from MES_EmpLogin  where emp_no='"+m.emp_no+"' and emp_shebei='"+m.emp_shebei+"' and status=1 ");
                if(cnt==false)
                {
                    bll.Add(m);
                }
            }
            
            
        }
        //var scr = "<script>var index = parent.layer.getFrameIndex(window.name);alert('ss');</script> ";

        ScriptManager.RegisterStartupScript(btnLogin,this.GetType(), "clos", "layer.alert('登入成功. ');$('#btnBack').click()", true);
        //Response.Write("<script>"+scr+"</script>");
    }


    protected void txtEmpNo_TextChanged(object sender, EventArgs e)
    {   
        string empno=txtEmpNo.Text;
        string strSQL="select dept.UNITNAME as dept_name,PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME,ORGSTDSTRUCT.UNITNAME as zz" +
            " from [172.16.5.6].[ehr_db].dbo.psnaccount" +
            " left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID" +
            " left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0" +
            " where PSNACCOUNT.accessionstate in(1,2,6) and employeeid='"+empno.Replace("'","")+"'";

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
           
            txtBanZhu.Text = banzu;  
            txtEmpName.Text = dt.Rows[0]["TRUENAME"].ToString();            
                      
           

        } 
        else
        {
            txtEmpNo.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(),"alert","layer.open({title: '提示信息' ,content: '无此可操作账号，请确认是否输入完整后再登入.'}); ",true);
        }
    }
}