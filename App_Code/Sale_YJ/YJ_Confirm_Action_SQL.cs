using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// YJ_Confirm_Action_SQL 的摘要说明
/// </summary>
public class YJ_Confirm_Action_SQL
{
    public YJ_Confirm_Action_SQL()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper YangjianSQLHelper = new SQLHelper();
    /// <summary>
    ///  销售助理【订单输入】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Code"></param>
    /// <param name="CreateDate"></param>
    /// <param name="Userid"></param>
    /// <param name="UserName"></param>
    /// <param name="UserName_AD"></param>
    /// <param name="dept"></param>
    /// <param name="managerid"></param>
    /// <param name="manager"></param>
    /// <param name="manager_AD"></param>
    /// <param name="Sales_engineer_id"></param>
    /// <param name="Sales_engineer"></param>
    /// <param name="Sales_engineer_AD"></param>
    /// <param name="Sales_engineer_job"></param>
    /// <param name="product_engineer_id"></param>
    /// <param name="product_engineer"></param>
    /// <param name="product_engineer_AD"></param>
    /// <param name="product_engineer_job"></param>
    /// <param name="quality_engineer_id"></param>
    /// <param name="quality_engineer"></param>
    /// <param name="quality_engineer_AD"></param>
    /// <param name="quality_engineer_job"></param>
    /// <param name="project_engineer_id"></param>
    /// <param name="project_engineer"></param>
    /// <param name="project_engineer_AD"></param>
    /// <param name="project_engineer_job"></param>
    /// <param name="Packaging_engineer_id"></param>
    /// <param name="Packaging_engineer"></param>
    /// <param name="Packaging_engineer_AD"></param>
    /// <param name="Packaging_engineer_job"></param>
    /// <param name="planning_engineer_id"></param>
    /// <param name="planning_engineer"></param>
    /// <param name="planning_engineer_AD"></param>
    /// <param name="planning_engineer_job"></param>
    /// <param name="warehouse_keeper_id"></param>
    /// <param name="warehouse_keeper"></param>
    /// <param name="warehouse_keeper_AD"></param>
    /// <param name="warehouse_keeper_job"></param>
    /// <param name="checker_monitor_id"></param>
    /// <param name="checker_monitor"></param>
    /// <param name="checker_monitor_AD"></param>
    /// <param name="checker_monitor_job"></param>
    /// <param name="status_id"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="sqgc"></param>
    /// <param name="domain"></param>
    /// <param name="gkddh"></param>
    /// <param name="sddd_date"></param>
    /// <param name="xmh"></param>
    /// <param name="ljh"></param>
    /// <param name="ljmc"></param>
    /// <param name="ljzl"></param>
    /// <param name="ljdj"></param>
    /// <param name="fhz"></param>
    /// <param name="hb"></param>
    /// <param name="gkdm"></param>
    /// <param name="gkmc"></param>
    /// <param name="ljzj"></param>
    /// <param name="yhsl"></param>
    /// <param name="kcl"></param>
    /// <param name="kc_date"></param>
    /// <param name="tz_date"></param>
    /// <param name="yqdh_date"></param>
    /// <param name="yqfy_date"></param>
    /// <param name="sktj"></param>
    /// <param name="ystk"></param>
    /// <param name="ysfs"></param>
    /// <param name="yfzffs"></param>
    /// <param name="yfje"></param>
    /// <param name="shrxx"></param>
    /// <param name="shdz"></param>
    /// <param name="yq"></param>
    /// <param name="bqyq"></param>
    /// <param name="shwjyq"></param>
    /// <param name="other_yq"></param>
    /// <param name="gysdm"></param>
    /// <param name="xmjd"></param>
    /// <returns></returns>
    public int YJ_Sales_Assistant_PRO1(int requestId, string Code, DateTime CreateDate, string Userid,
string UserName, string UserName_AD, string dept, string managerid, string manager, string manager_AD, string Sales_engineer_id,
string Sales_engineer, string Sales_engineer_AD, string Sales_engineer_job, string product_engineer_id, string product_engineer,
string product_engineer_AD, string product_engineer_job, string quality_engineer_id, string quality_engineer, string quality_engineer_AD,
string quality_engineer_job, string project_engineer_id, string project_engineer, string project_engineer_AD, string project_engineer_job,
string Packaging_engineer_id, string Packaging_engineer, string Packaging_engineer_AD, string Packaging_engineer_job, string planning_engineer_id,
string planning_engineer, string planning_engineer_AD, string planning_engineer_job, string warehouse_keeper_id, string warehouse_keeper,
string warehouse_keeper_AD, string warehouse_keeper_job, string checker_monitor_id, string checker_monitor, string checker_monitor_AD,
string checker_monitor_job, string Update_user, string Update_content, string sqgc, int domain, string gkddh, DateTime sddd_date, string xmh,
string ljh, string ljmc, decimal ljzl, decimal ljdj, string fhz, string hb, string gkdm, string gkmc, decimal ljzj, int yhsl, int kcl,
DateTime kc_date, DateTime tz_date, DateTime yqdh_date, DateTime yqfy_date, string sktj, string ystk, string ysfs, string yfzffs, decimal yfje,
string shrxx, string shdz, string yq, string bqyq, string shwjyq, string other_yq, string gysdm, string xmjd, decimal Packing_net_weight, DateTime special_require,
DateTime Packaging_require, DateTime goods_require, DateTime goods_require_wl, DateTime check_require_zl, DateTime check_require_jy,
DateTime shipping_require, string Sorting_list,string zzgc, string domain_zzgc,string Serial_No,int Line_Code,string ddlxr,string ddlxphone, decimal ljdj_qad, 
string lable_type_Chrylser,string mdgc_Chrylser,string ENG_GM, string sbh_AAM,string sbh_HM_AAM,string sbh_NO_AAM,string ZGS_BBAC,string EQ_BBAC,string Part_Status_Daimler,string Part_Status_Daimler_ms,string Part_Change_Daimler,string DDL_DY,string CBL_DY,string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
      {
          new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Code",Code) ,
                        new SqlParameter("@CreateDate",CreateDate) ,
                        new SqlParameter("@Userid",Userid) ,
                        new SqlParameter("@UserName",UserName) ,
                        new SqlParameter("@UserName_AD",UserName_AD) ,
                        new SqlParameter("@dept",dept) ,
                        new SqlParameter("@managerid",managerid) ,
                        new SqlParameter("@manager",manager) ,
                        new SqlParameter("@manager_AD",manager_AD) ,
                        new SqlParameter("@Sales_engineer_id",Sales_engineer_id) ,
                        new SqlParameter("@Sales_engineer",Sales_engineer) ,
                        new SqlParameter("@Sales_engineer_AD",Sales_engineer_AD) ,
                        new SqlParameter("@Sales_engineer_job",Sales_engineer_job) ,
                        new SqlParameter("@product_engineer_id",product_engineer_id) ,
                        new SqlParameter("@product_engineer",product_engineer) ,
                        new SqlParameter("@product_engineer_AD",product_engineer_AD) ,
                        new SqlParameter("@product_engineer_job",product_engineer_job) ,
                        new SqlParameter("@quality_engineer_id",quality_engineer_id) ,
                        new SqlParameter("@quality_engineer",quality_engineer) ,
                        new SqlParameter("@quality_engineer_AD",quality_engineer_AD) ,
                        new SqlParameter("@quality_engineer_job",quality_engineer_job) ,
                        new SqlParameter("@project_engineer_id",project_engineer_id) ,
                        new SqlParameter("@project_engineer",project_engineer) ,
                        new SqlParameter("@project_engineer_AD",project_engineer_AD) ,
                        new SqlParameter("@project_engineer_job", project_engineer_job) ,
                        new SqlParameter("@Packaging_engineer_id",Packaging_engineer_id) ,
                        new SqlParameter("@Packaging_engineer",Packaging_engineer) ,
                        new SqlParameter("@Packaging_engineer_AD",Packaging_engineer_AD) ,
                        new SqlParameter("@Packaging_engineer_job",Packaging_engineer_job) ,
                        new SqlParameter("@planning_engineer_id",planning_engineer_id) ,
                        new SqlParameter("@planning_engineer",planning_engineer) ,
                        new SqlParameter("@planning_engineer_AD",planning_engineer_AD) ,
                        new SqlParameter("@planning_engineer_job",planning_engineer_job) ,
                        new SqlParameter("@warehouse_keeper_id",warehouse_keeper_id) ,
                        new SqlParameter("@warehouse_keeper",warehouse_keeper) ,
                        new SqlParameter("@warehouse_keeper_AD",warehouse_keeper_AD) ,
                        new SqlParameter("@warehouse_keeper_job",warehouse_keeper_job) ,
                        new SqlParameter("@checker_monitor_id",checker_monitor_id) ,
                        new SqlParameter("@checker_monitor",checker_monitor) ,
                        new SqlParameter("@checker_monitor_AD",checker_monitor_AD) ,
                        new SqlParameter("@checker_monitor_job",checker_monitor_job) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@sqgc",sqgc) ,
                        new SqlParameter("@domain",domain) ,
                        new SqlParameter("@gkddh",gkddh) ,
                        new SqlParameter("@sddd_date",sddd_date) ,
                        new SqlParameter("@xmh",xmh) ,
                        new SqlParameter("@ljh",ljh) ,
                        new SqlParameter("@ljmc",ljmc) ,
                        new SqlParameter("@ljzl",ljzl) ,
                        new SqlParameter("@ljdj",ljdj) ,
                        new SqlParameter("@fhz",fhz) ,
                        new SqlParameter("@hb",hb) ,
                        new SqlParameter("@gkdm",gkdm) ,
                        new SqlParameter("@gkmc",gkmc) ,
                        new SqlParameter("@ljzj",ljzj) ,
                        new SqlParameter("@yhsl",yhsl) ,
                        new SqlParameter("@kcl",kcl) ,
                        new SqlParameter("@kc_date",kc_date) ,
                        new SqlParameter("@tz_date",tz_date) ,
                        new SqlParameter("@yqdh_date",yqdh_date) ,
                        new SqlParameter("@yqfy_date",yqfy_date) ,
                        new SqlParameter("@sktj",sktj) ,
                        new SqlParameter("@ystk",ystk) ,
                        new SqlParameter("@ysfs",ysfs) ,
                        new SqlParameter("@yfzffs",yfzffs) ,
                        new SqlParameter("@yfje",yfje) ,
                        new SqlParameter("@shrxx",shrxx) ,
                        new SqlParameter("@shdz",shdz) ,
                        new SqlParameter("@yq",yq) ,
                        new SqlParameter("@bqyq", bqyq) ,
                        new SqlParameter("@shwjyq",shwjyq) ,
                        new SqlParameter("@other_yq",other_yq) ,
                        new SqlParameter("@gysdm", gysdm) ,
                        new SqlParameter("@xmjd",xmjd),
                        new SqlParameter("@Packing_net_weight",Packing_net_weight),
                        new SqlParameter("@special_require",special_require),
                        new SqlParameter("@Packaging_require",Packaging_require),
                        new SqlParameter("@goods_require",goods_require),
                        new SqlParameter("@goods_require_wl",goods_require_wl),
                        new SqlParameter("@check_require_zl",check_require_zl),
                        new SqlParameter("@check_require_jy",check_require_jy),
                        new SqlParameter("@shipping_require",shipping_require),
                        new SqlParameter("@Sorting_list",Sorting_list),
                        new SqlParameter("@zzgc",zzgc),
                        new SqlParameter("@domain_zzgc",domain_zzgc),
                        new SqlParameter("@Serial_No",Serial_No),
                        new SqlParameter("@Line_Code",Line_Code),
                        new SqlParameter("@ddlxr",ddlxr),
                        new SqlParameter("@ddlxphone",ddlxphone),
                        new SqlParameter("@ljdj_qad",ljdj_qad),
                        new SqlParameter("@lable_type_Chrylser",lable_type_Chrylser),
                        new SqlParameter("@mdgc_Chrylser",mdgc_Chrylser),
                        new SqlParameter("@ENG_GM",ENG_GM),
                        new SqlParameter("@sbh_AAM",sbh_AAM),
                        new SqlParameter("@sbh_HM_AAM",sbh_HM_AAM),
                        new SqlParameter("@sbh_NO_AAM",sbh_NO_AAM),
                        new SqlParameter("@ZGS_BBAC",ZGS_BBAC),
                        new SqlParameter("@EQ_BBAC",EQ_BBAC),
                        new SqlParameter("@Part_Status_Daimler",Part_Status_Daimler),
                        new SqlParameter("@Part_Status_Daimler_ms",Part_Status_Daimler_ms),
                        new SqlParameter("@Part_Change_Daimler",Part_Change_Daimler),
                         new SqlParameter("@DDL_DY",DDL_DY),
                         new SqlParameter("@CBL_DY",CBL_DY),
                        new SqlParameter("@Submit",Submit)

      };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Sales_Assistant_PRO1", param);
    }
    /// <summary>
    /// 销售助理【订舱】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="sqgc"></param>
    /// <param name="gkddh"></param>
    /// <param name="sddd_date"></param>
    /// <param name="xmh"></param>
    /// <param name="ljh"></param>
    /// <param name="ljmc"></param>
    /// <param name="ljzl"></param>
    /// <param name="ljdj"></param>
    /// <param name="fhz"></param>
    /// <param name="hb"></param>
    /// <param name="gkdm"></param>
    /// <param name="gkmc"></param>
    /// <param name="ljzj"></param>
    /// <param name="yhsl"></param>
    /// <param name="kcl"></param>
    /// <param name="kc_date"></param>
    /// <param name="tz_date"></param>
    /// <param name="yqdh_date"></param>
    /// <param name="yqfy_date"></param>
    /// <param name="sktj"></param>
    /// <param name="ystk"></param>
    /// <param name="ysfs"></param>
    /// <param name="yfzffs"></param>
    /// <param name="yfje"></param>
    /// <param name="shrxx"></param>
    /// <param name="shdz"></param>
    /// <param name="yq"></param>
    /// <param name="bqyq"></param>
    /// <param name="shwjyq"></param>
    /// <param name="other_yq"></param>
    /// <param name="gysdm"></param>
    /// <param name="xmjd"></param>
    /// <param name="tzfy_date"></param>
    /// <returns></returns>
    public int YJ_Sales_Assistant_PRO2(int requestId, string Update_user, string Update_content, DateTime tzfy_date, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
        { new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content", Update_content) ,
                        new SqlParameter("@tzfy_date", tzfy_date),
                        new SqlParameter("@Submit",Submit)
      };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Sales_Assistant_PRO2", param);
    }
    /// <summary>
    /// 销售助理【确认到货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="sqgc"></param>
    /// <param name="gkddh"></param>
    /// <param name="sddd_date"></param>
    /// <param name="xmh"></param>
    /// <param name="ljh"></param>
    /// <param name="ljmc"></param>
    /// <param name="ljzl"></param>
    /// <param name="ljdj"></param>
    /// <param name="fhz"></param>
    /// <param name="hb"></param>
    /// <param name="gkdm"></param>
    /// <param name="gkmc"></param>
    /// <param name="ljzj"></param>
    /// <param name="yhsl"></param>
    /// <param name="kcl"></param>
    /// <param name="kc_date"></param>
    /// <param name="tz_date"></param>
    /// <param name="yqdh_date"></param>
    /// <param name="yqfy_date"></param>
    /// <param name="sktj"></param>
    /// <param name="ystk"></param>
    /// <param name="ysfs"></param>
    /// <param name="yfzffs"></param>
    /// <param name="yfje"></param>
    /// <param name="shrxx"></param>
    /// <param name="shdz"></param>
    /// <param name="yq"></param>
    /// <param name="bqyq"></param>
    /// <param name="shwjyq"></param>
    /// <param name="other_yq"></param>
    /// <param name="gysdm"></param>
    /// <param name="xmjd"></param>
    /// <param name="tzfy_date"></param>
    /// <param name="dh_date"></param>
    /// <returns></returns>
    public int YJ_Sales_Assistant_PRO3(int requestId, string Update_user, string Update_content,  DateTime dh_date, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@dh_date",dh_date),
                        new SqlParameter("@Submit",Submit)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Sales_Assistant_PRO3", param);
    }

    /// <summary>
    /// 项目工程师 【订单确认】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="cp_status"></param>
    /// <param name="special_require"></param>
    /// <param name="special_yq"></param>
    /// <returns></returns>
    public int YJ_Project_Engineer_PRO1(int requestId, string Update_user, string Update_content, string cp_status, string special_yq, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
         {
                        new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content", Update_content) ,
                        new SqlParameter("@cp_status",cp_status) ,
                        new SqlParameter("@special_yq", special_yq),
                        new SqlParameter("@Submit",Submit)

         };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Project_Engineer_PRO1", param);
    }

    /// <summary>
    /// 项目工程师 【发货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="cp_status"></param>
    /// <param name="special_require"></param>
    /// <param name="special_yq"></param>
    /// <param name="customer_request"></param>
    /// <param name="customer_request_complete"></param>
    /// <returns></returns>
    public int YJ_Project_Engineer_PRO2(int requestId, string Update_user, string Update_content, string cp_status,
string special_yq, string customer_request, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
           {
                        new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@cp_status",cp_status) ,
                        new SqlParameter("@special_yq",special_yq) ,
                        new SqlParameter("@customer_request",customer_request),
                        new SqlParameter("@Submit",Submit)
           };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Project_Engineer_PRO2", param);
    }
    /// <summary>
    /// 包装工程师【包材备货及包装方案】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Packaging_scheme"></param>
    /// <param name="Box_weight_total"></param>
    /// <param name="Box_quantity"></param>
    /// <param name="Packing_net_weight"></param>
    /// <param name="Package_weight"></param>
    /// <param name="Box_specifications1"></param>
    /// <param name="Box_weight1"></param>
    /// <param name="Box_specifications2"></param>
    /// <param name="Box_weight2"></param>
    /// <param name="Other_box_dec1"></param>
    /// <param name="Other_box_dec2"></param>
    /// <returns></returns>
    public int YJ_Packaging_Engineer_PRO1(int requestId, string Update_user, string Update_content, string Packaging_scheme,
decimal Box_weight_total, int Box_quantity, decimal Packing_net_weight, decimal Package_weight, string Box_specifications1, decimal Box_weight1,
string Box_specifications2, decimal Box_weight2, string Other_box_dec1, string Other_box_dec2, int Per_Crate_Qty, int Per_Crate_Qty2, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {  new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content", Update_content) ,
                        new SqlParameter("@Packaging_scheme",Packaging_scheme) ,
                        new SqlParameter("@Box_weight_total",Box_weight_total) ,
                        new SqlParameter("@Box_quantity",Box_quantity) ,
                        new SqlParameter("@Packing_net_weight",Packing_net_weight) ,
                        new SqlParameter("@Package_weight", Package_weight) ,
                        new SqlParameter("@Box_specifications1",Box_specifications1) ,
                        new SqlParameter("@Box_weight1",Box_weight1) ,
                        new SqlParameter("@Box_specifications2",Box_specifications2) ,
                        new SqlParameter("@Box_weight2",Box_weight2) ,
                        new SqlParameter("@Other_box_dec1",Other_box_dec1) ,
                        new SqlParameter("@Other_box_dec2",Other_box_dec2),
                        new SqlParameter("@Per_Crate_Qty",Per_Crate_Qty),
                         new SqlParameter("@Per_Crate_Qty2",Per_Crate_Qty2),
                        new SqlParameter("@Submit",Submit)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Packaging_Engineer_PRO1", param);
    }
    /// <summary>
    /// 包装工程师 【包材备货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Packaging_scheme"></param>
    /// <param name="Box_weight_total"></param>
    /// <param name="Box_quantity"></param>
    /// <param name="Packing_net_weight"></param>
    /// <param name="Package_weight"></param>
    /// <param name="Box_specifications1"></param>
    /// <param name="Box_weight1"></param>
    /// <param name="Box_specifications2"></param>
    /// <param name="Box_weight2"></param>
    /// <param name="Other_box_dec1"></param>
    /// <param name="Other_box_dec2"></param>
    /// <returns></returns>
    public int YJ_Packaging_Engineer_PRO2(int requestId, string Update_user, string Update_content, string Packaging_scheme,
decimal Box_weight_total, int Box_quantity, decimal Packing_net_weight, decimal Package_weight, string Box_specifications1,
decimal Box_weight1, string Box_specifications2, decimal Box_weight2, string Other_box_dec1, string Other_box_dec2, string Packing_goods_Already, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
         {  new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@Packaging_scheme",Packaging_scheme) ,
                        new SqlParameter("@Box_weight_total", Box_weight_total) ,
                        new SqlParameter("@Box_quantity", Box_quantity) ,
                        new SqlParameter("@Packing_net_weight", Packing_net_weight) ,
                        new SqlParameter("@Package_weight", Package_weight) ,
                        new SqlParameter("@Box_specifications1",Box_specifications1) ,
                        new SqlParameter("@Box_weight1", Box_weight1) ,
                        new SqlParameter("@Box_specifications2",Box_specifications2) ,
                        new SqlParameter("@Box_weight2",Box_weight2) ,
                        new SqlParameter("@Other_box_dec1",Other_box_dec1) ,
                        new SqlParameter("@Other_box_dec2",Other_box_dec2),
                        new SqlParameter("@Packing_goods_Already",Packing_goods_Already),
                        new SqlParameter("@Submit",Submit)

         };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Packaging_Engineer_PRO2", param);
    }
    /// <summary>
    /// 仓库班长【检验送检】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Sorting_list"></param>
    /// <param name="Already_Check"></param>
    /// <param name="Warehouse_quantity"></param>
    /// <param name="shipping_require"></param>
    /// <param name="shipping_complete"></param>
    /// <returns></returns>
    public int YJ_Warehouse_Keeper_PRO1(int requestId, string Update_user, string Update_content, 
string Already_Check, int Warehouse_quantity, string warehouse_keeper_id, string warehouse_keeper, string warehouse_keeper_AD,
string warehouse_keeper_job, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
         {     new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                  
                        new SqlParameter("@Already_Check",Already_Check) ,
                        new SqlParameter("@Warehouse_quantity",Warehouse_quantity),
                        new SqlParameter("@warehouse_keeper_id",warehouse_keeper_id),
                        new SqlParameter("@warehouse_keeper",warehouse_keeper),
                        new SqlParameter("@warehouse_keeper_AD",warehouse_keeper_AD),
                        new SqlParameter("@warehouse_keeper_job",warehouse_keeper_job),
                        new SqlParameter("@Submit",Submit)
         };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Warehouse_Keeper_PRO1", param);
    }
    /// <summary>
    /// 仓库班长 【检验取货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Sorting_list"></param>
    /// <param name="Already_Check"></param>
    /// <param name="Warehouse_quantity"></param>
    /// <param name="shipping_require"></param>
    /// <param name="shipping_complete"></param>
    /// <returns></returns>
    public int YJ_Warehouse_Keeper_PRO2(int requestId, string Update_user, string Update_content, 
string Already_Check, int Warehouse_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
         {     new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                
                        new SqlParameter("@Already_Check",Already_Check) ,
                        new SqlParameter("@Warehouse_quantity",Warehouse_quantity),

                        new SqlParameter("@Submit",Submit)
         };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Warehouse_Keeper_PRO2", param);
    }
    /// <summary>
    /// 仓库班长 【发货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Sorting_list"></param>
    /// <param name="Already_Check"></param>
    /// <param name="Warehouse_quantity"></param>
    /// <param name="shipping_require"></param>
    /// <param name="shipping_complete"></param>
    /// <returns></returns>
    public int YJ_Warehouse_Keeper_PRO3(int requestId, string Update_user, string Update_content, 
string Already_Check, int Warehouse_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
         {     new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                     
                        new SqlParameter("@Already_Check",Already_Check) ,
                        new SqlParameter("@Warehouse_quantity",Warehouse_quantity),
                        new SqlParameter("@Submit",Submit)
         };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Warehouse_Keeper_PRO3", param);
    }
    public int YJ_Checker_Monitor_PRO1(int requestId, string Update_user, string Update_content, int Qualified_quantity,
int Unqualified_quantity, string Unqualified_description,
string checker_monitor_id, string checker_monitor, string checker_monitor_AD, string checker_monitor_job, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
           {  new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content", Update_content) ,
                        new SqlParameter("@Qualified_quantity",Qualified_quantity) ,
                        new SqlParameter("@Unqualified_quantity",Unqualified_quantity) ,
                        new SqlParameter("@Unqualified_description",Unqualified_description),
                        //new SqlParameter("@check_complete_jy",check_complete_jy),
                        new SqlParameter("@checker_monitor_id",checker_monitor_id),
                        new SqlParameter("@checker_monitor",checker_monitor),
                        new SqlParameter("@checker_monitor_AD",checker_monitor_AD),
                        new SqlParameter("@checker_monitor_job",checker_monitor_job),
                        new SqlParameter("@Submit",Submit)
           };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Checker_Monitor_PRO1", param);
    }

    public int YJ_Quality_Engineer_PRO1(int requestId, string Update_user, string Update_content, string is_check_qcc_zl,
string is_check_szb_zl, string is_check_wg_zl, string check_other_ms_zl, string check_lj_zl, string Already_zl, string is_check_jj_zl,
string Check_number_zl, string reference_number_zl, int Confirm_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {    new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc_zl", is_check_qcc_zl) ,
                        new SqlParameter("@is_check_szb_zl",is_check_szb_zl) ,
                        new SqlParameter("@is_check_wg_zl", is_check_wg_zl) ,
                        new SqlParameter("@check_other_ms_zl",check_other_ms_zl) ,
                        new SqlParameter("@check_lj_zl", check_lj_zl) ,
                        new SqlParameter("@Already_zl", Already_zl) ,
                        new SqlParameter("@is_check_jj_zl", is_check_jj_zl) ,
                        new SqlParameter("@Check_number_zl", Check_number_zl) ,
                        new SqlParameter("@reference_number_zl",reference_number_zl) ,
                        new SqlParameter("@Confirm_quantity",Confirm_quantity),
                        new SqlParameter("@Submit",Submit)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Quality_Engineer_PRO1", param);
    }

    public int YJ_Quality_Engineer_PRO2(int requestId, string Update_user, string Update_content, string is_check_qcc_zl,
string is_check_szb_zl, string is_check_wg_zl, string check_other_ms_zl, string check_lj_zl, string Already_zl, string is_check_jj_zl,
string Check_number_zl, string reference_number_zl, int Confirm_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {    new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc_zl", is_check_qcc_zl) ,
                        new SqlParameter("@is_check_szb_zl",is_check_szb_zl) ,
                        new SqlParameter("@is_check_wg_zl", is_check_wg_zl) ,
                        new SqlParameter("@check_other_ms_zl",check_other_ms_zl) ,
                        new SqlParameter("@check_lj_zl", check_lj_zl) ,
                        new SqlParameter("@Already_zl", Already_zl) ,
                        new SqlParameter("@is_check_jj_zl", is_check_jj_zl) ,
                        new SqlParameter("@Check_number_zl", Check_number_zl) ,
                        new SqlParameter("@reference_number_zl",reference_number_zl) ,
                        new SqlParameter("@Confirm_quantity",Confirm_quantity) ,
                        new SqlParameter("@Submit",Submit)
                        //new SqlParameter("@check_complete_zl", check_complete_zl)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Quality_Engineer_PRO2", param);
    }
    /// <summary>
    /// 质量工程师 【检验2】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="is_check_qcc"></param>
    /// <param name="is_check_szb"></param>
    /// <param name="is_check_wg"></param>
    /// <param name="check_other_ms"></param>
    /// <param name="check_lj"></param>
    /// <param name="check_fj"></param>
    /// <param name="is_check_jj"></param>
    /// <param name="Check_number"></param>
    /// <param name="reference_number"></param>
    /// <param name="Confirm_quantity"></param>
    /// <param name="check_require"></param>
    /// <param name="check_complete"></param>
    /// <returns></returns>
    public int YJ_Quality_Engineer_PRO3(int requestId, string Update_user, string Update_content, string is_check_qcc_zl,
string is_check_szb_zl, string is_check_wg_zl, string check_other_ms_zl, string check_lj_zl, string Already_zl, string is_check_jj_zl,
string Check_number_zl, string reference_number_zl, int Confirm_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {    new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc_zl", is_check_qcc_zl) ,
                        new SqlParameter("@is_check_szb_zl",is_check_szb_zl) ,
                        new SqlParameter("@is_check_wg_zl", is_check_wg_zl) ,
                        new SqlParameter("@check_other_ms_zl",check_other_ms_zl) ,
                        new SqlParameter("@check_lj_zl", check_lj_zl) ,
                        new SqlParameter("@Already_zl", Already_zl) ,
                        new SqlParameter("@is_check_jj_zl", is_check_jj_zl) ,
                        new SqlParameter("@Check_number_zl", Check_number_zl) ,
                        new SqlParameter("@reference_number_zl",reference_number_zl) ,
                        new SqlParameter("@Confirm_quantity",Confirm_quantity) ,
                        new SqlParameter("@Submit",Submit)
                        //new SqlParameter("@check_complete_zl", check_complete_zl)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Quality_Engineer_PRO3", param);
    }
    public int YJ_Quality_Engineer_PRO4(int requestId, string Update_user, string Update_content, string is_check_qcc_zl,
string is_check_szb_zl, string is_check_wg_zl, string check_other_ms_zl, string check_lj_zl, string Already_zl, string is_check_jj_zl,
string Check_number_zl, string reference_number_zl, int Confirm_quantity, int Unqualified_quantity_zl, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {    new SqlParameter("@requestId", requestId) ,
                        new SqlParameter("@Update_user", Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc_zl", is_check_qcc_zl) ,
                        new SqlParameter("@is_check_szb_zl",is_check_szb_zl) ,
                        new SqlParameter("@is_check_wg_zl", is_check_wg_zl) ,
                        new SqlParameter("@check_other_ms_zl",check_other_ms_zl) ,
                        new SqlParameter("@check_lj_zl", check_lj_zl) ,
                        new SqlParameter("@Already_zl", Already_zl) ,
                        new SqlParameter("@is_check_jj_zl", is_check_jj_zl) ,
                        new SqlParameter("@Check_number_zl", Check_number_zl) ,
                        new SqlParameter("@reference_number_zl",reference_number_zl) ,
                        new SqlParameter("@Confirm_quantity",Confirm_quantity),
                        new SqlParameter("@Unqualified_quantity_zl",Unqualified_quantity_zl),
                        new SqlParameter("@Submit",Submit)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Quality_Engineer_PRO4", param);
    }
    /// <summary>
    /// 项目工程师 【订单确认】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="is_check_qcc"></param>
    /// <param name="is_check_szb"></param>
    /// <param name="is_check_wg"></param>
    /// <param name="check_other_ms"></param>
    /// <param name="check_lj"></param>
    /// <param name="Check_number"></param>
    /// <param name="reference_number"></param>
    /// <param name="Stocking_quantity"></param>
    /// <param name="goods_require"></param>
    /// <param name="goods_complete"></param>
    /// <returns></returns>
    public int YJ_Product_Engineer_PRO1(int requestId, string Update_user, string Update_content, string is_check_qcc, string is_check_szb,
string is_check_wg, string is_check_jj, string check_other_ms, string check_lj, string Already_Product, string Check_number, string reference_number, int Stocking_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {          new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc",is_check_qcc) ,
                        new SqlParameter("@is_check_szb",is_check_szb) ,
                        new SqlParameter("@is_check_wg",is_check_wg) ,
                        new SqlParameter("@is_check_jj",is_check_jj) ,
                        new SqlParameter("@check_other_ms",check_other_ms) ,
                        new SqlParameter("@check_lj",check_lj) ,
                        new SqlParameter("@Already_Product",Already_Product) ,
                        new SqlParameter("@Check_number",Check_number) ,
                        new SqlParameter("@reference_number",reference_number) ,
                        new SqlParameter("@Stocking_quantity",Stocking_quantity),
                        new SqlParameter("@Submit",Submit)

          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Product_Engineer_PRO1", param);
    }
    /// <summary>
    /// 项目工程师【发货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="is_check_qcc"></param>
    /// <param name="is_check_szb"></param>
    /// <param name="is_check_wg"></param>
    /// <param name="check_other_ms"></param>
    /// <param name="check_lj"></param>
    /// <param name="Check_number"></param>
    /// <param name="reference_number"></param>
    /// <param name="Stocking_quantity"></param>
    /// <param name="goods_require"></param>
    /// <returns></returns>
    public int YJ_Product_Engineer_PRO2(int requestId, string Update_user, string Update_content, string is_check_qcc, string is_check_szb,
string is_check_wg, string is_check_jj, string check_other_ms, string check_lj, string Already_Product, string Check_number, string reference_number, int Stocking_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
          {             new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@is_check_qcc",is_check_qcc) ,
                        new SqlParameter("@is_check_szb",is_check_szb) ,
                        new SqlParameter("@is_check_wg",is_check_wg) ,
                        new SqlParameter("@is_check_jj",is_check_jj) ,
                        new SqlParameter("@check_other_ms",check_other_ms) ,
                        new SqlParameter("@check_lj",check_lj) ,
                        new SqlParameter("@Already_Product",Already_Product) ,
                        new SqlParameter("@Check_number",Check_number) ,
                        new SqlParameter("@reference_number",reference_number) ,
                        new SqlParameter("@Stocking_quantity",Stocking_quantity),
                        new SqlParameter("@Submit",Submit)
          };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Product_Engineer_PRO2", param);
    }
    /// <summary>
    /// 物流计划 【确认】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Already"></param>
    /// <param name="Current_inventory_quantity"></param>
    /// <param name="Current_inventory_date"></param>
    /// <param name="Stocking_quantity"></param>
    /// <param name="goods_complete"></param>
    /// <returns></returns>
    public int YJ_Planning_Engineer_PRO1(int requestId, string Update_user, string Update_content, string Already, int Current_inventory_quantity,
DateTime Current_inventory_date, int Stocking_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
            {          new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@Already",Already) ,
                        new SqlParameter("@Current_inventory_quantity",Current_inventory_quantity) ,
                        new SqlParameter("@Stocking_quantity",Stocking_quantity) ,
                        new SqlParameter("@Current_inventory_date",Current_inventory_date) ,
                        new SqlParameter("@Submit",Submit)
            };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Planning_Engineer_PRO1", param);
    }
    /// <summary>
    /// 物流计划【备货】
    /// </summary>
    /// <param name="requestId"></param>
    /// <param name="Update_user"></param>
    /// <param name="Update_content"></param>
    /// <param name="Already"></param>
    /// <param name="Current_inventory_quantity"></param>
    /// <param name="Current_inventory_date"></param>
    /// <param name="Stocking_quantity"></param>
    /// <returns></returns>
    public int YJ_Planning_Engineer_PRO2(int requestId, string Update_user, string Update_content, string Already, int Current_inventory_quantity,
DateTime Current_inventory_date, int Stocking_quantity, string Submit)
    {
        SqlParameter[] param = new SqlParameter[]
            {
                        new SqlParameter("@requestId",requestId) ,
                        new SqlParameter("@Update_user",Update_user) ,
                        new SqlParameter("@Update_content",Update_content) ,
                        new SqlParameter("@Already",Already) ,
                        new SqlParameter("@Current_inventory_quantity",Current_inventory_quantity) ,
                        new SqlParameter("@Stocking_quantity",Stocking_quantity) ,
                        new SqlParameter("@Current_inventory_date",Current_inventory_date),
                        new SqlParameter("@Submit",Submit)
            };
        return YangjianSQLHelper.ExecuteNonQuery("Form1_sale_YJ_Planning_Engineer_PRO2", param);
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="requestId"></param>
    /// <returns></returns>
    public DataTable Form1_YJ_Query_PRO(int requestId)
    {
        SqlParameter[] param = new SqlParameter[]
           {
               new SqlParameter("@requestId",requestId)
           };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("Form1_YJ_Query_PRO", param);
    }
   
}