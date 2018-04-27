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
public class Function_Base
{
	public Function_Base()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();
    public DataTable Jiance_Base(int flag)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Jiance_BaseMatain", param);


    }

    public DataTable BaseData_query(string xmh,string ljh,string gxh,string sjlb,string item,string sys)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@item",item),
           new SqlParameter("@sys",sys)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_BaseData_query", param);


    }
    public int Base_ADD(string xmh, string ljh, string gxh, string sjlb, string dh, string item, string dlth, string jcyq, string sx, string xx, string jcsd,string jcsd_desc, string sys,string yksx,string ykxx,string filepath)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@dh",dh),
           new SqlParameter("@item",item),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@jcyq",jcyq),
           new SqlParameter("@sx",sx),
           new SqlParameter("@xx",xx),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@jcsd_desc",jcsd_desc),
           new SqlParameter("@sys",sys),
           new SqlParameter("@yksx",yksx),
           new SqlParameter("@ykxx",ykxx),
           new SqlParameter("@filepath",filepath)
           
       };
        return SQLHelper.ExecuteNonQuery("usp_Base_ADD", param);
    }

    public int Base_Update(string xmh, string ljh, string gxh, string sjlb, string dh, string item, string dlth, string jcyq, string sx, string xx, string jcsd, string jcsd_desc, string sys, string yksx, string ykxx, string filepath)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@dh",dh),
           new SqlParameter("@item",item),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@jcyq",jcyq),
           new SqlParameter("@sx",sx),
           new SqlParameter("@xx",xx),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@jcsd_desc",jcsd_desc),
           new SqlParameter("@sys",sys),
           new SqlParameter("@yksx",yksx),
           new SqlParameter("@ykxx",ykxx),
           new SqlParameter("@filepath",filepath)
           
       };
        return SQLHelper.ExecuteNonQuery("usp_Base_Update", param);
    }
   /// <summary>
   /// 基础数据--员工工号
   /// </summary>

    public DataTable Apply_base(int flag,string uid,string xmh,string sjlb,string gxh)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@uid",uid),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@gxh",gxh)
       
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_apply_detail", param);
    }

    public DataSet xmh_query(int flag, string uid, string xmh, string sjlb, string gxh)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@uid",uid),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@gxh",gxh)
       
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("usp_apply_detail", param);
    }
    public int DJ_Apply(int flag,string sjdate,string sjtime,string wtr,string wtrname,string xmh,string ljh,string wtdept,
        string sjlb,int synum,string gxh,string yqwcsj,string remark,string sjitem,string dlth,string jcyq_image,string jcyq_text,string sx,string xx,string yksx,string ykxx,string jcsd,string jcjg_1
    , string jcjg_2, string jcjg_3, string jcjg_4, string jcjg_5, string jcjg_6, string jcjg_7, string jcjg_8, string jcjg_9, string jcjg_10, string result, string no, string sys, string jcnr, string oldno, string cjchecked, string beizhu, string fileupload,string sbno)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@sjdate",sjdate),
           new SqlParameter("@sjtime",sjtime),
           new SqlParameter("@wtr",wtr),
           new SqlParameter("@wtrname",wtrname),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@wtdept",wtdept),
           new SqlParameter("@sjlb",sjlb),
           new SqlParameter("@synum",synum),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@yqwcsj",yqwcsj),
           new SqlParameter("@remark",remark),
           new SqlParameter("@sjitem",sjitem),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@jcyq_image",jcyq_image),
           new SqlParameter("@jcyq_text",jcyq_text),
           new SqlParameter("@sx",sx),
           new SqlParameter("@xx",xx),
           new SqlParameter("@yksx",yksx),
           new SqlParameter("@ykxx",ykxx),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@jcjg_1",jcjg_1),
           new SqlParameter("@jcjg_2",jcjg_2),
           new SqlParameter("@jcjg_3",jcjg_3),
           new SqlParameter("@jcjg_4",jcjg_4),
           new SqlParameter("@jcjg_5",jcjg_5),
           new SqlParameter("@jcjg_6",jcjg_6),
           new SqlParameter("@jcjg_7",jcjg_7),
           new SqlParameter("@jcjg_8",jcjg_8),
           new SqlParameter("@jcjg_9",jcjg_9),
           new SqlParameter("@jcjg_10",jcjg_10),
           new SqlParameter("@result",result),
           new SqlParameter("@no",no),
           new SqlParameter("@sys",sys),
           new SqlParameter("@jcnr",jcnr),
           new SqlParameter("@oldno",oldno),
           new SqlParameter("@cjchecked",cjchecked),
           new SqlParameter("@beizhu",beizhu),
           new SqlParameter("@fileupload",fileupload),
           new SqlParameter("@sbno",sbno)
       
       };
       // DataTable dt = new DataTable();
        return SQLHelper.ExecuteNonQuery("usp_DJ_apply", param);
    }

    public DataTable query_dh( int flag)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@flag",flag),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_query_dh", param);
    }

    public DataTable Cal_sysProgress(string dh)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@dh",dh),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Cal_sysProgress", param);
    }

    public DataTable DJLIST_Query(int flag,string xmh, string dh, string gxh, string sjlb, string jcxm, string sys)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@flag",flag),
              new SqlParameter("@xmh",xmh),
              new SqlParameter("@dh",dh),
              new SqlParameter("@gxh",gxh),
              new SqlParameter("@sjlb",sjlb),
              new SqlParameter("@jcxm",jcxm),
              new SqlParameter("@sys",sys),

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("DJlIST_Query", param);
    }

    public int GetWCSJ(int flag,string dh,string uid)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@uid",uid)
          
          
       };
        return SQLHelper.ExecuteNonQuery("usp_GetWCSJ", param);
    }

    public DataTable YJLIST_Query(int flag, string xmh, string dh, string gxh, string sjlb, string jcxm, string sys,string ljh,string wtr)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@flag",flag),
              new SqlParameter("@xmh",xmh),
              new SqlParameter("@dh",dh),
              new SqlParameter("@gxh",gxh),
              new SqlParameter("@sjlb",sjlb),
              new SqlParameter("@jcxm",jcxm),
              new SqlParameter("@sys",sys),
              new SqlParameter("@ljh",ljh),
              new SqlParameter("@wtr",wtr),


       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("YJlIST_Query", param);
    }

   


    public DataTable YJLIST_Query_detail(int flag, string xmh, string dh, string gxh, string sjlb, string jcxm, string sys, string ljh, string wtr,string applydate)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@flag",flag),
              new SqlParameter("@xmh",xmh),
              new SqlParameter("@dh",dh),
              new SqlParameter("@gxh",gxh),
              new SqlParameter("@sjlb",sjlb),
              new SqlParameter("@jcxm",jcxm),
              new SqlParameter("@sys",sys),
              new SqlParameter("@ljh",ljh),
              new SqlParameter("@wtr",wtr),
              new SqlParameter("@applydate",applydate)


       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("YJlIST_Query_XG", param);
    }


    public DataTable Get_SB(int flag, string item)
    {
        SqlParameter[] param = new SqlParameter[]
       {
              new SqlParameter("@flag",flag),
              new SqlParameter("@jcitem",item)
             


       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Get_SB", param);
    }

    public int Get_JYTime(int flag, string dh, string dlth,string jg1,string jg2,string jg3,string jg4,string jg5,string jg6,string jg7,string jg8,string jg9,string jg10,string sbno,string wd,string sd,string hwsj,
        string csr,string sx,string xx,string jcsd,string jcyq,string result,string bz,string id)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@jg1",jg1),
           new SqlParameter("@jg2",jg2),
           new SqlParameter("@jg3",jg3),
           new SqlParameter("@jg4",jg4),
           new SqlParameter("@jg5",jg5),
           new SqlParameter("@jg6",jg6),
           new SqlParameter("@jg7",jg7),
           new SqlParameter("@jg8",jg8),
           new SqlParameter("@jg9",jg9),
           new SqlParameter("@jg10",jg10),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@wd",wd),
           new SqlParameter("@sd",sd),
           new SqlParameter("@hwsj",hwsj),
           new SqlParameter("@csr",csr),
           new SqlParameter("@sx",sx),
           new SqlParameter("@xx",xx),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@jcyq",jcyq),
           new SqlParameter("@result",result),
           new SqlParameter("@bz",bz),
            new SqlParameter("@id",id)

          
          
       };
        return SQLHelper.ExecuteNonQuery("usp_Get_JYTime_XG", param);
    }
    public int DJ_Upload_Update(int flag, string dh, string dlth, string sbno, string jg1, string jg2, string jg3, string jg4,
        string jg5, string jg6, string jg7, string jg8, string jg9, string jg10, string csrid, string csrname, string wd, string sd, string hwsj)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@jg1",jg1),
           new SqlParameter("@jg2",jg2),
           new SqlParameter("@jg3",jg3),
           new SqlParameter("@jg4",jg4),
           new SqlParameter("@jg5",jg5),
           new SqlParameter("@jg6",jg6),
           new SqlParameter("@jg7",jg3),
           new SqlParameter("@jg8",jg4),
           new SqlParameter("@jg9",jg5),
           new SqlParameter("@jg10",jg6),
           new SqlParameter("@csrid",csrid),
           new SqlParameter("@csrname",csrname),
           new SqlParameter("@wd",wd),
           new SqlParameter("@sd",sd),
           new SqlParameter("@hwsj",hwsj)

          
          
       };
        return SQLHelper.ExecuteNonQuery("usp_DJ_Upload", param);
    }

    public int GS_Add(int flag, string xmh, string ljh,string gxh,string jcxm,string gs)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@jcxm",jcxm),
           new SqlParameter("@gs",gs)
          
          
          
       };
        return SQLHelper.ExecuteNonQuery("usp_gs_add", param);
    }


    public DataTable GS_Query(int flag, string xmh, string ljh, string gxh, string jcxm)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@jcxm",jcxm),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_GS_Query", param);
    }

    public DataTable Report_Query(int flag, string dh)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh)
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Report_query", param);
    }

    public int Update_CJStatus(int flag, string dh, string dlth,string beizhu)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@beizhu",beizhu)
          
       };
        return SQLHelper.ExecuteNonQuery("usp_Update_cjstauts", param);
    }
    public DataTable Hydron_query(int flag, string sbno, string guige, string hejin, string luhao, string date, string banci, int zysxh, string jilian_start, string jilian_end, string is_gj, string wd, string kq_zl, string water_zl, string midu, string result, string czgid, string czgname)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@guige",guige),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@date",date),
           new SqlParameter("@banci",banci),
           new SqlParameter("@zysxh",zysxh),
           new SqlParameter("@jilian_start",jilian_start),
           new SqlParameter("@jilian_end",jilian_end),
           new SqlParameter("@is_ganjing",is_gj),
           new SqlParameter("@wd",wd),
           new SqlParameter("@kq_zl",kq_zl),
           new SqlParameter("@water_zl",water_zl),
           new SqlParameter("@midu",midu),
           new SqlParameter("@result",result),
           new SqlParameter("@czgid",czgid),
           new SqlParameter("@czgname",czgname)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Hydron_query", param);
    }



    public int Hydron_insert(int flag, string Hd_Date, string Hd_zyno, string Hd_banci, string Hd_banzu, string Hd_shift, string Hd_gh, string Hd_name, string Hd_luhao, string Hd_Hejin, string Hd_zybno, string Hd_zybpz, string Hd_mz, string Hd_jz, string Hd_kongqi, string Hd_water, string Hd_mdyq, string Hd_result, string Hd_sjmd, string Hd_sjlwd,
        string Hd_ejlwd, string Hd_sjltime, string Hd_ejltime)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@Hd_Date",Hd_Date),
           new SqlParameter("@Hd_zyno",Hd_zyno),
           new SqlParameter("@Hd_banci",Hd_banci),
           new SqlParameter("@Hd_banzu",Hd_banzu),
           new SqlParameter("@Hd_shift",Hd_shift),
           new SqlParameter("@Hd_gh",Hd_gh),
           new SqlParameter("@Hd_name",Hd_name),
           new SqlParameter("@Hd_luhao",Hd_luhao),
           new SqlParameter("@Hd_Hejin",Hd_Hejin),
           new SqlParameter("@Hd_zybno",Hd_zybno),
           new SqlParameter("@Hd_zybpz",Hd_zybpz),
           new SqlParameter("@Hd_mz",Hd_mz),
           new SqlParameter("@Hd_jz",Hd_jz),
           new SqlParameter("@Hd_kongqi",Hd_kongqi),
           new SqlParameter("@Hd_water",Hd_water),
           new SqlParameter("@Hd_mdyq",Hd_mdyq),
           new SqlParameter("@Hd_sjmd",Hd_sjmd),
           new SqlParameter("@Hd_result",Hd_result),
           new SqlParameter("@Hd_sjlwd",Hd_sjlwd),
           new SqlParameter("@Hd_ejlwd",Hd_ejlwd),
           new SqlParameter("@Hd_sjltime",Hd_sjltime),
           new SqlParameter("@Hd_ejltime",Hd_ejltime)

          
          
       };
        return SQLHelper.ExecuteNonQuery("usp_Hydron_insert", param);

    }

    public DataTable Hydron_detail(string sbno, string hejin, string start_date, string end_date, string banci, string luhao,string czg)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@banci",banci),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@czg",czg)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Hydron_detail", param);
    }

    public DataTable DJ_Upload(int flag, string dh, string dlth, string sbno, string jg1, string jg2,string jg3,string jg4,
        string jg5, string jg6, string jg7, string jg8, string jg9, string jg10, string csrid, string csrname, string wd, string sd, string hwsj)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@jg1",jg1),
           new SqlParameter("@jg2",jg2),
           new SqlParameter("@jg3",jg3),
           new SqlParameter("@jg4",jg4),
           new SqlParameter("@jg5",jg5),
           new SqlParameter("@jg6",jg6),
           new SqlParameter("@jg7",jg7),
           new SqlParameter("@jg8",jg8),
           new SqlParameter("@jg9",jg9),
           new SqlParameter("@jg10",jg10),
           new SqlParameter("@csrid",csrid),
           new SqlParameter("@csrname",csrname),
           new SqlParameter("@wd",wd),
           new SqlParameter("@sd",sd),
           new SqlParameter("@hwsj",hwsj),



          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_DJ_Upload", param);
    }

    public DataTable Getxmh(int flag,string xmh)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_xmh_select", param);


    }
    public int link_add(int flag, string dh, string filelink,string remark,string sl)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@filelink",filelink),
           new SqlParameter("@remark",remark),
           new SqlParameter("@scsl",sl)
          
       };
        return SQLHelper.ExecuteNonQuery("usp_link_add", param);
    }


    public DataTable Get_Hydron_Report(string year,string mnth,string shift)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@year",year),
           new SqlParameter("@month",mnth),
           new SqlParameter("@banzu",shift),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_hydron_report", param);


    }
    public DataTable Get_md_query(string year, string mnth, string shift)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@year",year),
           new SqlParameter("@month",mnth),
           new SqlParameter("@banzu",shift),
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_md_query", param);


    }

    public DataTable Get_Status(int flag,string dh,string jcsd,string path,string csr,string sbno)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@filepath",path),
           new SqlParameter("@csr",csr),
           new SqlParameter("@sbno",sbno)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_DJ_Query", param);


    }

    public int Get_Status_insert(int flag, string dh, string jcsd,string path,string csr,string sbno)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@dh",dh),
           new SqlParameter("@jcsd",jcsd),
           new SqlParameter("@filepath",path),
           new SqlParameter("@csr",csr),
           new SqlParameter("@sbno",sbno)
       };

        return SQLHelper.ExecuteNonQuery("usp_DJ_Query", param);


    }


    public int Base_Del(int flag, string xmh, string dlth,string gxh,string sjlb)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@dlth",dlth),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@sjlb",sjlb)
       };

        return SQLHelper.ExecuteNonQuery("usp_base_Del_xg", param);


    }

    public DataTable JC_GS_Query(int flag, string xmh, string ljh, string gxh,string jcxm,string status)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@jcxm",jcxm),
           new SqlParameter("@status",status)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_JC_GS", param);


    }

    public DataTable Copy_xmh(int flag, string xmh, string sjlb_source, string gxh, string sjlb_desc)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@sjlb_souce",sjlb_source),
           new SqlParameter("@gxh",gxh),
           new SqlParameter("@sjlb_des",sjlb_desc)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_xmh_Copy", param);


    }

}