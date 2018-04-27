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
public class Product_CLASS
{
    public Product_CLASS()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable emp(string workcode)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@workcode",workcode)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form1_Sale_YJ_BASE_emp", param);
    }
    public DataTable form3_Sale_V_Track(string XMH)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@XMH",XMH)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_Sale_V_Track", param);
    }
    public DataTable Product_version_new(string requestid)
    {
        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@requestid",requestid)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_version_new", param);
    }
    //  public int ProductImgUpdate(string lsid, string lsimg)
    //  {
    //      SqlParameter[] param = new SqlParameter[]
    //  { new SqlParameter("@lsid",lsid),
    //  new SqlParameter("lsimg",lsimg)


    //};
    //      return SQLHelper.ExecuteNonQuery("Baojia_YZ_JJ_DELETE", param);

    //  }
    public DataTable DDL_base(string type, string ddl_name, string update_user)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@type",type),
                new SqlParameter("@ddl_name",ddl_name),
                  new SqlParameter("@update_user",update_user),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_base_PRO", param);
    }
    public DataTable Form3_Product_ljh(string baojia_no, string ljh,string type)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@baojia_no",baojia_no),
                new SqlParameter("@ljh",ljh),
                new SqlParameter("@type",type),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_ljh", param);
    }
    public DataTable Form3_Product_ship_to(string khdm)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@khdm",khdm),
              

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_ship_to", param);
    }
    //public DataTable Form3_Product_xmh_ship_to(string pgino, string ship_to, string customer_project)
    //{

    //    SqlParameter[] param = new SqlParameter[]
    //   {

    //       new SqlParameter("@pgino",pgino),
    //       new SqlParameter("@ship_to",ship_to),
    //           new SqlParameter("@customer_project",customer_project),


    //   };
    //    DataTable dt = new DataTable();
    //    return SQLHelper.GetDataTable("Form3_Product_xmh_ship_to", param);
    //}
    public DataTable Form3_Product_xmh_ship_to(string pgino, string ship_to)
    {

        SqlParameter[] param = new SqlParameter[]
       {

           new SqlParameter("@pgino",pgino),
           new SqlParameter("@ship_to",ship_to),
     


       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_xmh_ship_to", param);
    }
    public DataTable form3_SUM_MAX(string pgino,string TYPE)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@pgino",pgino),
               new SqlParameter("@TYPE",TYPE),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_SUM_MAX", param);
    }
    public static System.Data.DataSet Getmax_hr()
    {

        string lssql = "select LEFT(MAX(pgino),1)+'0'+cast(SUBSTRING(MAX(pgino),2,LEN(MAX(pgino)))+1 as varchar(10)) as pgino  from ProductList ";
        using (DataSet ds = SQLHelper.reDs_HR( lssql))
        {
            return ds;
        }
    }
    public static System.Data.DataSet Getmax()
    {

        string lssql = "select LEFT(MAX(pgino),1)+'0'+cast(SUBSTRING(MAX(pgino),2,LEN(MAX(pgino)))+1 as varchar(10)) as pgino  from form3_Sale_Product_MainTable ";
        using (DataSet ds = SQLHelper.reDs(lssql))
        {
            return ds;
        }
    }
    public int insert_form3_Sale_Product_LOG(int requestId, string Update_Engineer, string Update_user,string Update_username, string Update_LB, string Update_content)
    {
        SqlParameter[] param = new SqlParameter[]
       { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_Engineer", Update_Engineer) ,
                        new SqlParameter("@Update_user", Update_user),
                        new SqlParameter("@Update_username", Update_username),
                        new SqlParameter("@Update_LB", Update_LB),
                        new SqlParameter("@Update_content", Update_content)
                         };
        return SQLHelper.ExecuteNonQuery("form3_Sale_insert_Sale_Product_LOG", param);
    }
    public int form3_Sale_update_baojia(string baojia_no, string ljh)
    {
        SqlParameter[] param = new SqlParameter[]
       { new SqlParameter("@baojia_no",baojia_no) ,
                        new SqlParameter("@ljh", ljh) 
                      
                     
                         };
        return SQLHelper.ExecuteNonQuery("form3_Sale_update_baojia", param);
    }
    public int BTN_product_update(int requestId,string product_leibie, string customer_name, string end_customer_name,
    string customer_project, string end_date,
    string update_User, string pc_date,string product_status,string delete_date,string Sales_engineers,string product_img,string dingdian_date,string customer_requestCN,string customer_requestSM,string productname)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@product_leibie", product_leibie),
                        new SqlParameter("@customer_name", customer_name),
                        new SqlParameter("@end_customer_name", end_customer_name),
                        new SqlParameter("@customer_project", customer_project),
                        new SqlParameter("@end_date", end_date),
                        new SqlParameter("@update_User", update_User),
                     new SqlParameter("@pc_date", pc_date),
                      new SqlParameter("@product_status", product_status),
                       new SqlParameter("@delete_date", delete_date),
                         new SqlParameter("@Sales_engineers", Sales_engineers),
                         new SqlParameter("@product_img", product_img),
                          new SqlParameter("@dingdian_date", dingdian_date),
                                 new SqlParameter("@customer_requestCN", customer_requestCN),
                          new SqlParameter("@customer_requestSM", customer_requestSM),
                           new SqlParameter("@productname", productname)
      };
        return SQLHelper.ExecuteNonQuery("form3_Sale_Product_Modify1", param);
    }
    public int BTN_fzr_update(int requestId, int type,string update_User, string project_user, string product_user,
      string moju_user, string yz_user, string jj_user, string zl_user, string bz_user, string wl_user, string zhiliangzhuguan_user,
      string sqe_user1, string sqe_user2, string caigou, string sale, string jiaju_egnieer, string daoju_egnieer, string jianju_egnieer,
      string mojugl_egnieer)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,

        new SqlParameter("@type",type),
        new SqlParameter("@update_User", update_User),
                        new SqlParameter("@project_user", project_user),
                        new SqlParameter("@product_user", product_user),

                         new SqlParameter("@moju_user", moju_user),
                        new SqlParameter("@yz_user", yz_user),
                        new SqlParameter("@jj_user", jj_user),
                        new SqlParameter("@zl_user", zl_user),
                        new SqlParameter("@bz_user", bz_user),
                        new SqlParameter("@wl_user", wl_user),
                        new SqlParameter("@zhiliangzhuguan_user", zhiliangzhuguan_user),

                        new SqlParameter("@sqe_user1", sqe_user1),
                        new SqlParameter("@sqe_user2", sqe_user2),
                        new SqlParameter("@caigou", caigou),
                        new SqlParameter("@sale", sale),
                        new SqlParameter("@jiaju_egnieer", jiaju_egnieer),
                        new SqlParameter("@daoju_egnieer", daoju_egnieer),
                        new SqlParameter("@jianju_egnieer", jianju_egnieer),

                        new SqlParameter("@mojugl_egnieer", mojugl_egnieer)
                        
      };
        return SQLHelper.ExecuteNonQuery("form3_Sale_Product_Modify2", param);
    }
    public int BTN_Sales_sub(int requestId, string Code,string CreateDate,string Userid,string UserName,string UserName_AD,
        string dept,string managerid,string manager,string manager_AD,string Sales_engineers,string baojia_no, string pgino,string productcode,
        string productname,string make_factory,string product_leibie,string customer_name,string end_customer_name,
        string customer_project,string end_date,string dingdian_date,string delete_date,string product_status,string product_img,
        string update_User,string pc_date,string customer_requestCN,string customer_requestSM,string project_user,string product_user,
        string moju_user,string yz_user,string jj_user,string zl_user,string bz_user,string wl_user,string zhiliangzhuguan_user,
        string sqe_user1,string sqe_user2,string caigou,string sale,string jiaju_egnieer,string daoju_egnieer,string jianju_egnieer,
        string mojugl_egnieer, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Code", Code) ,
                        new SqlParameter("@CreateDate", CreateDate) ,
                        new SqlParameter("@Userid", Userid),
                        new SqlParameter("@UserName", UserName),
                        new SqlParameter("@UserName_AD", UserName_AD),

                        new SqlParameter("@dept", dept),
                        new SqlParameter("@managerid", managerid),
                        new SqlParameter("@manager", manager),
                        new SqlParameter("@manager_AD", manager_AD),
                        new SqlParameter("@Sales_engineers", Sales_engineers),
                        new SqlParameter("@baojia_no", baojia_no),
                        new SqlParameter("@pgino", pgino),
                        new SqlParameter("@productcode", productcode),

                        new SqlParameter("@productname", productname),
                        new SqlParameter("@make_factory", make_factory),
                        new SqlParameter("@product_leibie", product_leibie),
                        new SqlParameter("@customer_name", customer_name),
                        new SqlParameter("@end_customer_name", end_customer_name),

                        new SqlParameter("@customer_project", customer_project),                     
                        new SqlParameter("@end_date", end_date),
                        new SqlParameter("@dingdian_date", dingdian_date),
                        new SqlParameter("@delete_date", delete_date),
                        new SqlParameter("@product_status", product_status),
                        new SqlParameter("@product_img", product_img),

                        //new SqlParameter("@update_date", update_date),
                        new SqlParameter("@update_User", update_User),
                        new SqlParameter("@pc_date", pc_date),
                         new SqlParameter("@customer_requestCN", customer_requestCN),
                          new SqlParameter("@customer_requestSM", customer_requestSM),
                        new SqlParameter("@project_user", project_user),
                        new SqlParameter("@product_user", product_user),

                         new SqlParameter("@moju_user", moju_user),
                        new SqlParameter("@yz_user", yz_user),
                        new SqlParameter("@jj_user", jj_user),
                        new SqlParameter("@zl_user", zl_user),
                        new SqlParameter("@bz_user", bz_user),
                        new SqlParameter("@wl_user", wl_user),
                        new SqlParameter("@zhiliangzhuguan_user", zhiliangzhuguan_user),

                        new SqlParameter("@sqe_user1", sqe_user1),
                        new SqlParameter("@sqe_user2", sqe_user2),
                        new SqlParameter("@caigou", caigou),
                        new SqlParameter("@sale", sale),
                        new SqlParameter("@jiaju_egnieer", jiaju_egnieer),
                        new SqlParameter("@daoju_egnieer", daoju_egnieer),
                        new SqlParameter("@jianju_egnieer", jianju_egnieer),

                        new SqlParameter("@mojugl_egnieer", mojugl_egnieer),
                        new SqlParameter("@Submit",Submit)
      };
        return SQLHelper.ExecuteNonQuery("form3_Sale_Product_Submit", param);
    }

    public DataTable BJ_base(string type,string classify)
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
    //public static SqlDataReader GetBase_Code(string lscode_type)
    //{

    //    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connstringMoJu"]);
    //    string sql = "select * from  base_code   where code_type='" + lscode_type + "'  order by req_no ";
    //    SqlCommand cmd = new SqlCommand(sql, conn);
    //    conn.Open();
    //    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    //    return dr;
    //}
    public DataTable Getljh(string ljh, string lj_name,string sales_name)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@lj_name",lj_name),
           new SqlParameter("@sales_name",sales_name),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_XZ_LJH", param);


    }
    public DataTable BJ_BASE(string lookup_type)
    {

        SqlParameter[] param = new SqlParameter[]
       {

               new SqlParameter("@lookup_type",lookup_type)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Baojia_lookup_table_PRO", param);
    }
    public DataTable Getgv(string requestid, string gv)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@requestid",requestid),
           new SqlParameter("@gv",gv),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_Sale_gv_modify", param);
    }
    public DataTable Getgv_colour(int ID)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@ID",ID),
      
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_Sale_gv_colour", param);
    }
    public DataTable Get_Sale_qad_pi_mstr(string xmh, string ad_id)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ad_id",ad_id),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("form3_Sale_qad_pi_mstr", param);
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
    public DataTable Form3_Product_qad_Debtor(string DebtorCode)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@DebtorCode ",DebtorCode )
           };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Form3_Product_qad_Debtor", param);
    }
    public int Baojia_YZ_JJ_DELETE(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId)


      };
        return SQLHelper.ExecuteNonQuery("Baojia_YZ_JJ_DELETE", param);
    }
    public void SendMail(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@requestId", requestId) ,
           
          };
        SQLHelper.ExecuteNonQuery("Baojia_Sendmail_Every_Ticket", param);

    }
    

    public int delete_gv(int requestid, string gv)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestid),

         new SqlParameter("@gv",gv),
      };
        return SQLHelper.ExecuteNonQuery("form3_Sale_gv_delete", param);
    }
}

