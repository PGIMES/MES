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
public class MaterialBase_CLASS
{
    public MaterialBase_CLASS()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper SQLHelper = new SQLHelper();
    //public DataTable SZ_base(string type,string wlh, string ms,string tc,string pp,string ppms,string gys,string jgcz,string szlx,string xx,string cx,string sccz,string isnl)
    //{
    //    SqlParameter[] param = new SqlParameter[]
    //   {
    //           new SqlParameter("@type",type),
    //           new SqlParameter("@wlh",wlh),
    //           new SqlParameter("@ms",ms),
    //           new SqlParameter("@tc",tc),
    //           new SqlParameter("@pp",pp),
    //           new SqlParameter("@ppms",ppms),
    //           new SqlParameter("@gys",gys),
    //           new SqlParameter("@jgcz",jgcz),
    //           new SqlParameter("@szlx",szlx),
    //           new SqlParameter("@xx",xx),
    //           new SqlParameter("@cx",cx),
    //           new SqlParameter("@sccz",sccz),
    //           new SqlParameter("@isnl",isnl),
    //   };
    //    DataTable dt = new DataTable();
    //    return SQLHelper.GetDataTable("MaterialBase", param);
    //}
   public DataTable DJ_base(string type,string BASE,string ljh, string domain)
    {
        SqlParameter[] param = new SqlParameter[]
       {
               new SqlParameter("@type",type),
                new SqlParameter("@BASE",BASE),
               new SqlParameter("@ljh",ljh),
                new SqlParameter("@domain",domain),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("PGI_BASE_PART_Report", param);
    }
    public DataTable PGI_BASE_PART_xmcs_sj_colour(string type,string list_fieldname)
    {
        SqlParameter[] param = new SqlParameter[]
       {
               new SqlParameter("@type",type),
                new SqlParameter("@list_fieldname",list_fieldname),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("PGI_BASE_PART_xmcs_sj_colour", param);
    }
    public DataTable Forproducts(string pt_part,string ljh,string site)
    {
        SqlParameter[] param = new SqlParameter[]
       {
               new SqlParameter("@pt_part",pt_part),
               new SqlParameter("@ljh",ljh),
               new SqlParameter("@site",site),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("PGI_BASE_PART_Forproducts", param);
    }


    public DataTable MES_PT_BASE(int QUERY, string type, string classify,string name)
    {
        SqlParameter[] param = new SqlParameter[]
       {
               new SqlParameter("@QUERY",QUERY),
               new SqlParameter("@type",type),
               new SqlParameter("@classify",classify),
               new SqlParameter("@name",name)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("PGI_BASE_PART_QUERY", param);
    }

    public DataTable ForproductsDay(string pt_part, string site,string isSchedule)
    {
        SqlParameter[] param = new SqlParameter[]
       {
               new SqlParameter("@pt_part",pt_part),
               new SqlParameter("@site",site),
               new SqlParameter("@isSchedule",isSchedule)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("Pur_Schedule", param);
    }

}

