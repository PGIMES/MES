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
///Function_Jinglian 的摘要说明
/// </summary>
public class Function_Jinglian
{
	public Function_Jinglian()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    SQLHelper SQLHelper = new SQLHelper();


    public DataTable Emplogin_query(int flag, string emp_no,string sbno)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@gh",emp_no),
            new SqlParameter("@sbno",sbno)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Emplogin_query", param);


    }

    public DataTable zybno_query(int flag, string jl_date, string jl_time, string banbie, string banzhu, string luhao, string hejin)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@jilian_date",jl_date),
           new SqlParameter("@jilian_time",jl_time),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@banzu",banzhu),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Get_ZYB", param);


    }

    public int jinglian_insert_1(int flag, string jl_date,string start, string jl_time, string banbie,string gh,string name, string banzhu,string sbno,string zybno, string luhao, string hejin,string bf_wd,string bf_time,string af_wd,string af_time,string baohao,string pz,string mz,string jz,string zl_time)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@jinglian_date",jl_date),
           new SqlParameter("@start_time",start),
           new SqlParameter("@jinglian_time",jl_time),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@gh",gh),
           new SqlParameter("@name",name),
           new SqlParameter("@banzhu",banzhu),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@bf_wd",bf_wd),
           new SqlParameter("@bf_time",bf_time),
           new SqlParameter("@af_wd",af_wd),
           new SqlParameter("@af_time",af_time),
           new SqlParameter("@baohao",baohao),
           new SqlParameter("@pz",pz),
           new SqlParameter("@mz",mz),
           new SqlParameter("@jz",jz),
           new SqlParameter("@zl_time",zl_time)

       };
        return SQLHelper.ExecuteNonQuery("usp_jinglian_end_insert_1", param);

    }

    public int jinglian_insert(int flag, string jl_date, string start, string jl_time, string banbie, string gh, string name, string banzhu, string sbno, string zybno, string luhao, string hejin, string bf_wd, string bf_time, string af_wd, string af_time)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@jinglian_date",jl_date),
           new SqlParameter("@start_time",start),
           new SqlParameter("@jinglian_time",jl_time),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@gh",gh),
           new SqlParameter("@name",name),
           new SqlParameter("@banzhu",banzhu),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@bf_wd",bf_wd),
           new SqlParameter("@bf_time",bf_time),
           new SqlParameter("@af_wd",af_wd),
           new SqlParameter("@af_time",af_time)

       };
        return SQLHelper.ExecuteNonQuery("usp_jinglian_end_insert", param);

    }

    public int Hydrogen_insert(int flag,string zybno,string date,string banbie,string banzhu,string gh,string name,string water,string kq,string sjmd,string bmzt )
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@Hd_date",date),
           new SqlParameter("@Hd_banbie",banbie),
           new SqlParameter("@Hd_banzhu",banzhu),
           new SqlParameter("@Hd_gh",gh),
           new SqlParameter("@Hd_name",name),
           new SqlParameter("@water",water),
           new SqlParameter("@kq",kq),
           new SqlParameter("@sjmd",sjmd), 
           new SqlParameter("@bmzt",bmzt),
        

       };
        return SQLHelper.ExecuteNonQuery("usp_Hydrogen_insert", param);

    }

    public int Hydrogen_insert_Bytemperature(int flag, string zybno, string date, string banbie, string banzhu, string gh, string name, string water, string kq, string sjmd, string bmzt, string water_wendu, string water_value)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@Hd_date",date),
           new SqlParameter("@Hd_banbie",banbie),
           new SqlParameter("@Hd_banzhu",banzhu),
           new SqlParameter("@Hd_gh",gh),
           new SqlParameter("@Hd_name",name),
           new SqlParameter("@water",water),
           new SqlParameter("@kq",kq),
           new SqlParameter("@sjmd",sjmd), 
           new SqlParameter("@bmzt",bmzt),
           new SqlParameter("@water_wendu",water_wendu), 
           new SqlParameter("@water_value",water_value)
        

       };
        return SQLHelper.ExecuteNonQuery("usp_Hydrogen_insert", param);

    }

    public DataTable Hydrogen_Query(int flag,string zybno, string hejin, string start_date, string end_date, string banci, string luhao, string czg)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@banci",banci),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@czg",czg)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Hydrogen_query", param);
    }

    public DataTable Hydrogen_Query_ByGW(int flag, string zybno, string hejin, string start_date, string end_date, string banci, string luhao, string czg, string gongwei)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@banci",banci),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@czg",czg),
          new SqlParameter("@gongwei",gongwei)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_Hydrogen_query_ByGW", param);
    }


    public DataTable ZybClear_zyno_Query(string flag, string date, string shift)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@date",date),
           new SqlParameter("@shift",shift)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_zybclear_zynoquery", param);


    }

    public DataTable ZybClear_Content_Query(string flag)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_zybcontent_query", param);


    }

    public int ZYBClear_insert(int flag, string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no,string equip_name,string equip_type,string check1,string check2,string check3,string check4,string weight,string demo1,string demo2,string demo3,string demo4)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu), 
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@equip_type",equip_type),
           new SqlParameter("@check1",check1),
           new SqlParameter("@check2",check2),
           new SqlParameter("@check3",check3),
           new SqlParameter("@check4",check4),
           new SqlParameter("@weight",weight),
           new SqlParameter("@check1demo",demo1),
           new SqlParameter("@check2demo",demo2),
           new SqlParameter("@check3demo",demo3),
           new SqlParameter("@check4demo",demo4)
        

       };
        return SQLHelper.ExecuteNonQuery("usp_ZYBClear_Insert", param);

    }

    public DataTable ZybClear_Query(int flag,string year,string mnth,string start_date,string end_date,string banbie,string zyb)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",mnth),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@zyb",zyb)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_ZYBClear_query", param);


    }


    public int ZYB_JY_Insert(int flag, string emp_no, string emp_name, string emp_banzhu, string banbie, string equip_name,string zybno,string hejing,string zyb)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banzhu",emp_banzhu),
           new SqlParameter("@emp_banbie",banbie),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@hejing",hejing),
           new SqlParameter("@zyb",zyb)
        

       };
        return SQLHelper.ExecuteNonQuery("usp_zybjy_insert", param);

    }

    public DataTable ZYB_JY_query(int flag, string emp_name, string equip_name, string zybno, string hejing, string zyb,string start_date,string end_date,string year,string month)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@hejing",hejing),
           new SqlParameter("@zyb",zyb),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_zybjy_query", param);

    }

    public int jldz_insert(int flag, string emp_no, string emp_name, string emp_banbie, string emp_banzhu, string equip_no, string equip_name, string bf_wd, string jlj_use, string czj_use, string check, string af_wd, string begin_date, string end_date)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@emp_no",emp_no),
           new SqlParameter("@emp_name",emp_name),
           new SqlParameter("@emp_banbie",emp_banbie),
           new SqlParameter("@emp_banzhu",emp_banzhu), 
           new SqlParameter("@equip_no",equip_no),
           new SqlParameter("@equip_name",equip_name),
           new SqlParameter("@bf_wendu",bf_wd),
           new SqlParameter("@jlj_use",jlj_use),
           new SqlParameter("@czj_use",czj_use),
           new SqlParameter("@check",check),
           new SqlParameter("@af_wendu",af_wd),
           new SqlParameter("@begin_date",begin_date),
           new SqlParameter("@end_date",end_date)
        

       };
        return SQLHelper.ExecuteNonQuery("usp_jldz_insert", param);

    }

    public DataTable JLDZ_Query(int flag, string year, string month, string start_date, string end_date, string banbie, string luhao,string czr )
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@czr",czr)

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_jldz_query", param);

    }

    public DataTable hydrogen_check(int flag, string zyb)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@zyb",zyb),
         

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_hydrogen_check", param);

    }

    public DataTable GPList_Query(int flag, string year, string month, string start_date, string end_date, string dh, string source, string element,string luhao,string hejin,string gys)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@dh",dh),
           new SqlParameter("@source",source),
           new SqlParameter("@element",element),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@gys",gys)
         

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_GP_Query_1", param);
    }

    public DataTable GPQuery(int flag, string year, string month, string start_date, string end_date, string dh, string source, string element, string luhao, string hejin, string gys,string banbie )
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@dh",dh),
           new SqlParameter("@source",source),
           new SqlParameter("@element",element),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@gys",gys),
           new SqlParameter("@banbie",banbie)
         

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_GP_DetailQuery", param);
    }
    public DataTable GPDetail_Query(int flag, string year, string month, string start_date, string end_date, string dh, string source, string element, string luhao, string hejin, string gys,string type)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@dh",dh),
           new SqlParameter("@source",source),
           new SqlParameter("@element",element),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@gys",gys),
           new SqlParameter("@type",type)
         

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_GPDetail_Query_1", param);
    }


    public DataTable sbcolor_query(int flag,string sbno, string hejin)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@hejin",hejin),
         

       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_sbcolor_query", param);

    }

    //换模查询
    public DataTable ChangeMo_Query(int flag, string year, string month, string start_date, string end_date, string sbmc, string ljmc, string reason,string type)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@start_date",start_date),
           new SqlParameter("@end_date",end_date),
           new SqlParameter("@sbjc",sbmc),
           new SqlParameter("@ljmc",ljmc),
           new SqlParameter("@reason",reason),
           new SqlParameter("@type",type)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("usp_ChangeMO_Query_1", param);
    }

    public DataSet Jinglian_TJ_Report(int flag, string year, string month,  string banzhu,string date)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@banzhu",banzhu),
           new SqlParameter("@date",date)

          
          
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataSet("MES_JingLian_TJ", param);
    }

 public int jinglian_insert_2(int flag, string jl_date, string start, string jl_time, string banbie, string gh, string name, string banzhu, string sbno, string zybno, string luhao, string hejin, string bf_wd, string bf_time, string af_wd, string af_time, string baohao, string pz, string mz, string jz, string zl_time, string gp, string bf_wd_2, string cy_yn)
    {

        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@flag",flag),
           new SqlParameter("@jinglian_date",jl_date),
           new SqlParameter("@start_time",start),
           new SqlParameter("@jinglian_time",jl_time),
           new SqlParameter("@banbie",banbie),
           new SqlParameter("@gh",gh),
           new SqlParameter("@name",name),
           new SqlParameter("@banzhu",banzhu),
           new SqlParameter("@sbno",sbno),
           new SqlParameter("@zybno",zybno),
           new SqlParameter("@luhao",luhao),
           new SqlParameter("@hejin",hejin),
           new SqlParameter("@bf_wd",bf_wd),
           new SqlParameter("@bf_time",bf_time),
           new SqlParameter("@af_wd",af_wd),
           new SqlParameter("@af_time",af_time),
           new SqlParameter("@baohao",baohao),
           new SqlParameter("@pz",pz),
           new SqlParameter("@mz",mz),
           new SqlParameter("@jz",jz),
           new SqlParameter("@zl_time",zl_time),
           new SqlParameter("@gp_flag",gp),
           new SqlParameter("@bf_wd_2",bf_wd_2),
           new SqlParameter("@cy_yn",cy_yn)

       };
        return SQLHelper.ExecuteNonQuery("usp_jinglian_end_insert_2", param);

    }
}