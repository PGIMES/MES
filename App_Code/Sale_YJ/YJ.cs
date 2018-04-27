using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
///Function_Base 的摘要说明
/// </summary>
public class YJ_CLASS
{
	public YJ_CLASS()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable YJ_BASE(string BASE_ID)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@BASE_ID",BASE_ID)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_PRO", param);
    }
    public DataTable YJ_BASE2(string BASE_ID,string CLASS_NAME)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@BASE_ID",BASE_ID),
               new SqlParameter("@CLASS_NAME",CLASS_NAME)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_PRO", param);
    }
    public DataTable YJ_BASE3(string ID)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@ID",ID)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_PRO_ID", param);
    }
    public DataTable YJ_emp(string workcode)
    {
     

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@workcode",workcode)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_emp", param);
    }
    public DataTable Getxmh(string pt_part,string cp_cust_part,string cp_cust, string CP_ID)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@pt_part",pt_part),
           new SqlParameter("@cp_cust_part",cp_cust_part),
           new SqlParameter("@cp_cust",cp_cust),
           new SqlParameter("@CP_ID",CP_ID)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_V_form1_ljh_select", param);


    }
    public DataTable Getjg(string pi_part_code, string ad_id,string pi_domain)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@pi_part_code",pi_part_code),
           new SqlParameter("@ad_id",ad_id),
          new SqlParameter("@pi_domain",pi_domain),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_V_form1_pi_mstr_new", param);


    }
    public DataTable Getkcl(string ld_part, string ld_part2, string ld_domain,int lb,string ld_ref)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@ld_part",ld_part),
           new SqlParameter("@ld_part2",ld_part2),
          new SqlParameter("@ld_domain",ld_domain),
          new SqlParameter("@lb",lb),
          new SqlParameter("@ld_ref",ld_ref),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_V_qad_ld_det", param);


    }

    public DataTable Getshdz(string shdz,string shrxx)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@shdz",shdz),
           new SqlParameter("@shrxx",shrxx),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_V_form1_shdz_select", param);


    }
    public DataTable Getlog(string requestid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_LOG_PRO", param);


    }
    public DataTable Getlog_require(string requestid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_LOG_require", param);


    }
   
    public DataTable Getlog_group(string requestid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_LOG_group_PRO", param);


    }
    public DataTable GetCommit_time(string requestid)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_Commit_time", param);


    }
    public DataTable GetBTN(string requestid, string cp_status, string status_id, string emp_id)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@cp_status",cp_status),
           new SqlParameter("@status_id",status_id),
           new SqlParameter("@emp_id",emp_id),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_V_form1_BTN", param);


    }

    public DataTable GetBTN2(string requestid,string status_id, string emp_id)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@status_id",status_id),
           new SqlParameter("@emp_id",emp_id),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form1_Sale_YJ_LOG_1", param);


    }
    public DataTable GetJCApply_Master( string xmh, string dh)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@dh",dh),
           new SqlParameter("@xmh",xmh),
           
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_JCApply_Master", param);


    }
    public DataTable qr_date(int requestId, string engineer,int update_status)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
          new SqlParameter("@engineer", engineer) ,
           new SqlParameter("@update_status", update_status) ,
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_QR", param);
       
    }
    public DataTable qr_date_yq(int requestId, string engineer, int update_status)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
          new SqlParameter("@engineer", engineer) ,
           new SqlParameter("@update_status", update_status) ,
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_QRyq", param);

    }
    public DataTable DC_PRO(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
      
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form2_DC_PRO", param);

    }
    public int Process_intervention(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        return SQLHelper.ExecuteNonQuery("Form1_sale_YJ_Process_intervention", param);
    }
    public int Process_intervention_delete(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        return SQLHelper.ExecuteNonQuery("Form1_sale_YJ_Process_intervention_delete", param);
    }
    public int iserp(int requestId,string QADSO)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
          new SqlParameter("@QADSO", QADSO) ,
            //new SqlParameter("@Process_intervention", Process_intervention) ,
               
          };
        return SQLHelper.ExecuteNonQuery("Form1_sale_YJ_ERP", param);
    }
  
    public void SendMail(int requestId, int flow_id,string Code)
    {
        SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@requestId", requestId) ,
              new SqlParameter("@flow_id", flow_id) ,
              new SqlParameter("@Code", Code) ,
          };
         SQLHelper.ExecuteNonQuery("YJ_Sendmail_Every_Ticket_Submit", param);

    }
}