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
using Maticsoft.DBUtility;//Please add references

/// <summary>
///Function_Base 的摘要说明
/// </summary>
public class Customer_CLASS
{
    public Customer_CLASS()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable BJ_base(string type, string classify)
    {
        //        @material_type nchar(10),
        //@emp_banbie nchar(10),
        // @emp_banzhu nchar(10)

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@type",type),
               new SqlParameter("@classify",classify)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_base_PRO", param);
    }
    public DataTable emp(string workcode)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@workcode",workcode)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_emp", param);
    }
    public DataTable form4_Customer_base_PRO(string type)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@type",type)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form4_Customer_base_PRO", param);
    }
    public DataTable GetBTN(int status_id,int requestid, string empid, string turns)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@status_id",status_id),
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@empid",empid),
           new SqlParameter("@turns",turns),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form4_Customer_flow", param);


    }
    public int insert_form_LOG(int requestId, string Update_Engineer, string Update_user,string Update_username, string Update_LB, string Update_content)
    {
        SqlParameter[] param = new SqlParameter[]
       { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_Engineer", Update_Engineer) ,
                        new SqlParameter("@Update_user", Update_user),
                        new SqlParameter("@Update_username", Update_username),
                        new SqlParameter("@Update_LB", Update_LB),
                        new SqlParameter("@Update_content", Update_content)
                         };
        return SQLHelper.ExecuteNonQuery("form4_Sale_insert_LOG", param);
    }

    public int insert_ExistsClass(int ID, string ExistsClass, string CmClassID)
    {
        SqlParameter[] param = new SqlParameter[]
       { new SqlParameter("@ID",ID) ,
                        new SqlParameter("@ExistsClass", ExistsClass) ,
                        new SqlParameter("@CmClassID", CmClassID),
                         };
        return SQLHelper.ExecuteNonQuery("form4_Sale_insert_ExistsClass", param);
    }



    public DataTable Getgv(int requestid, string gv,int  turns)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@gv",gv),
           new SqlParameter("@turns",turns),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form4_Customer_gv", param);
    }

    public DataTable GetSPR(string ZG_empid, String ZG, string Userid, string UserName, string CreateDate, string turns)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@ZG_empid",ZG_empid),
               new SqlParameter("@ZG",ZG),
               new SqlParameter("@Userid",Userid),
               new SqlParameter("@UserName",UserName),
               new SqlParameter("@CreateDate",CreateDate),
               new SqlParameter("@turns",turns),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("Form4_Customer_GetSPR", param).Tables[0];
    }
    public DataTable Get_Customer_Update_list(string cmClassName, string BusinessRelationCode, string BusinessRelationName1,string UserName)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@cmClassName",cmClassName),
           new SqlParameter("@BusinessRelationCode", BusinessRelationCode),
           new SqlParameter("@BusinessRelationName1", BusinessRelationName1),
            new SqlParameter("@UserName", UserName)
     };
        return SQLHelper.GetDataTable("form4_Customer_Update_list", param);

    }
    public DataTable Get_Customer_work(string cmClassName, string BusinessRelationCode, string BusinessRelationName1, string UserName,string zt)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@cmClassName",cmClassName),
           new SqlParameter("@BusinessRelationCode", BusinessRelationCode),
           new SqlParameter("@BusinessRelationName1", BusinessRelationName1),
            new SqlParameter("@UserName", UserName),
            new SqlParameter("@zt", zt)
     };
        return SQLHelper.GetDataTable("form4_Customer_work", param);

    }
    public DataTable Get_Customer_report(string cmClassName, string BusinessRelationCode, string BusinessRelationName1, string UserName, string cm_region,string DebtorShipToCode,string DebtorShipToName,string IsEffective)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@cmClassName",cmClassName),
           new SqlParameter("@BusinessRelationCode", BusinessRelationCode),
           new SqlParameter("@BusinessRelationName1", BusinessRelationName1),
            new SqlParameter("@UserName", UserName),
             new SqlParameter("@cm_region", cm_region),
              new SqlParameter("@DebtorShipToCode", DebtorShipToCode),
               new SqlParameter("@DebtorShipToName", DebtorShipToName),
                      new SqlParameter("@IsEffective", IsEffective)
     };
        return SQLHelper.GetDataTable("form4_Customer_report", param);

    }
    public DataTable form3_Sale_Product_Query_PRO(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@requestId",requestId)
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_Sale_Product_Query_PRO", param);
    }

    public void SendMail(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@requestId", requestId) ,
           
          };
        SQLHelper.ExecuteNonQuery("Baojia_Sendmail_Every_Ticket", param);

    }

}

