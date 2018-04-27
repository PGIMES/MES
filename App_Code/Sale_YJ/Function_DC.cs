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
using Maticsoft.DBUtility;

using System.Text;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
///Function_DC 的摘要说明
/// </summary>
public class Function_DC
{
	public Function_DC()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable DC_GetXMH(string fyrq,string requestid)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@fyrq",fyrq),
             // new SqlParameter("@shdz",dizhi),
               new SqlParameter("@requestid",requestid),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("MES_DC_XMQuery", param);
    }

    public int DC_Insert(int flag,int requestid,string code,string createdate,string userid,string username,string userad,string dept,string managerid,string managername,string managerad,
        int status_id,string dc_uid,string dc_uname,string fyrq,string shdz,string tjdesc_sale,string hyno,string yjthrq,string jjdhrq,string hydl,string tjdesc_wuliu,string qrch_ware,string tihuodate_ware,string tjdesc_ware,string status)
    {
        SqlParameter[] param = new SqlParameter[]
{              
                new SqlParameter("@flag",flag) ,
                new SqlParameter("@requestId",requestid) ,
                new SqlParameter("@code", code) ,
                new SqlParameter("@createdate", createdate) ,
                new SqlParameter("@userid", userid),
                new SqlParameter("@username",username),
                new SqlParameter("@userad",userad),
                new SqlParameter("@dept",dept),
                new SqlParameter("@managerid",managerid),
                new SqlParameter("@managername",managername),
                new SqlParameter("@managerad",managerad),
                new SqlParameter("@status_id",status_id),
                new SqlParameter("@dc_uid",dc_uid),
                new SqlParameter("@dc_uname",dc_uname),
                new SqlParameter("@fyrq_sale",fyrq),
                new SqlParameter("@shdz_sale",shdz),
                new SqlParameter("@tjdesc_sale",tjdesc_sale),
                new SqlParameter("@hyno_wuliu",hyno),
                new SqlParameter("@yjthrq_wuliu",yjthrq),
                new SqlParameter("@jjdhrq_wuliu",jjdhrq),
                new SqlParameter("@hydl_wuliu",hydl),
                new SqlParameter("@tjdesc_wuliu",tjdesc_wuliu),
                new SqlParameter("@qrch_ware",qrch_ware),
                new SqlParameter("@tihuodate_ware",tihuodate_ware),
                new SqlParameter("@tjdesc_ware",tjdesc_ware),
                new SqlParameter("@status",status)
      };
        return SQLHelper.ExecuteNonQuery("MES_DC_Insert", param);
    }


    public DataTable DC_GetSale_Detail(string requestid)
    {
        string sql = "select* from form2_Sale_DC_MainTable where requestid='" + requestid + "' ";
        return DbHelperSQL.Query(sql.ToString()).Tables[0];
    }
     public DataTable DC_Getddmx(string requestid)
    {
        string sql = "select m.code,qadso,d.xmh,customer_ddh as gkddh,customer_mc as gkmc,customer_ljh as ljh, d.yhsl,xzsl as box_quantity,zxfa, mz,ysdesc as ystk,cast(dhdate as date) as yqdh_date,requestId_YJ as requestId,domain from form2_Sale_DC_DetailTable d ";
        sql+=" join form1_Sale_YJ_MainTable m on m.requestId=d.requestId_YJ left join form1_Sale_YJ_dt_Sales_Assistant FSYSA on FSYSA.requestId=m.requestId  where d.requestid='" + requestid + "' and isok='Y' ";
        return DbHelperSQL.Query(sql.ToString()).Tables[0];
    }
    public DataTable GetWare()
    {
        string sql = "SELECT   VALUE FROM      form1_Sale_YJ_BASE WHERE   BASE_ID =12 ";
        return DbHelperSQL.Query(sql.ToString()).Tables[0];
    }

    public DataTable GetStatus(string requestid)
    {
        string sql = "SELECT   * FROM     form2_Sale_DC_MainTable  where requestid='" + requestid + "'";
        return DbHelperSQL.Query(sql.ToString()).Tables[0];
    }
    //获取当前User状态 （提交或修改状态）
    public DataTable DC_Getstatus(string requestid,int status)
    {
        string sql = "select* from form2_Sale_DC_MainTable where requestid='" + requestid + "'  and status_id='"+status+"'";
        return DbHelperSQL.Query(sql.ToString()).Tables[0];
    }

}