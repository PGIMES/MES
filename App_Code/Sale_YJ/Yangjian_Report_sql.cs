using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// YangjianSQLHelp 的摘要说明
/// </summary>
public class YangjianSQLHelp
{
    public YangjianSQLHelp()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SQLHelper YangjianSQLHelper = new SQLHelper();
    public DataTable Get_YJ_Tracking_Report(string year, string yqdh_date_start, string yqdh_date_end,string yqfy_date_start,
        string yqfy_date_end,string status_id,string Update_name,string zzgc,string sign_status,string ISDelay,string depart,string RequireDate,string iserp,string czsx)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@year",year),
           new SqlParameter("@yqdh_date_start",yqdh_date_start),
           new SqlParameter("@yqdh_date_end",yqdh_date_end),
           new SqlParameter("@yqfy_date_start",yqfy_date_start),
           new SqlParameter("@yqfy_date_end",yqfy_date_end),
           new SqlParameter("@status_id",status_id),
           new SqlParameter("@Update_name",Update_name),
           new SqlParameter("@zzgc",zzgc),
           new SqlParameter("@sign_status",sign_status),
           new SqlParameter("@ISDelay",ISDelay),
           new SqlParameter("@depart",depart),
           new SqlParameter("@RequireDate",RequireDate),
           new SqlParameter("@iserp",iserp),
           new SqlParameter("@czsx",czsx)
     };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("rpt_Form1_sale_YJ_Tracking_Progress", param);

    }

    public DataTable Get_YJ_Query_Report( string gkdm,string gkmc,string xmh,string ljh,string cp_status,
        string yqfy_date_start, string yqfy_date_end, string yqdh_date_start, string yqdh_date_end,
        string tzfy_date_start, string tzfy_date_end, string Ticket_status, string code, string gkddh,
        string xgry,string zzgc,string fhstatus,string shrxx, string shdz,string depart,string iserp,string isDC)
    {
        SqlParameter[] param = new SqlParameter[]
     {
          new SqlParameter("@gkdm",gkdm),
          new SqlParameter("@gkmc",gkmc),
          new SqlParameter("@xmh",xmh),
          new SqlParameter("@ljh",ljh),
          new SqlParameter("@cp_status",cp_status),
          new SqlParameter("@yqfy_date_start",yqfy_date_start),
          new SqlParameter("@yqfy_date_end",yqfy_date_end),
          new SqlParameter("@yqdh_date_start",yqdh_date_start),
          new SqlParameter("@yqdh_date_end",yqdh_date_end),
          new SqlParameter("@tzfy_date_start",tzfy_date_start),
          new SqlParameter("@tzfy_date_end",tzfy_date_end),
          new SqlParameter("@Ticket_status",Ticket_status),
          new SqlParameter("@code",code),
          new SqlParameter("@gkddh",gkddh),
          new SqlParameter("@xgry",xgry),
          new SqlParameter("@zzgc",zzgc),
          new SqlParameter("@fhstatus",fhstatus),
          new SqlParameter("@shrxx",shrxx),
          new SqlParameter("@shdz",shdz),
          new SqlParameter("@depart",depart),
          new SqlParameter("@iserp",iserp),
          new SqlParameter("@isDC",isDC)

     };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("rpt_Form1_sale_YJ_Query_Report", param);

    }

    public DataTable Get_YJ_Query_NoCPStatus_Report(string gkdm, string gkmc, string xmh, string ljh, string cp_status,
       string yqfy_date_start, string yqfy_date_end, string yqdh_date_start, string yqdh_date_end,
       string tzfy_date_start, string tzfy_date_end, string Ticket_status, string code, string gkddh,
        string xgry, string zzgc, string fhstatus,string shrxx,string shdz, string depart)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@gkdm",gkdm),
           new SqlParameter("@gkmc",gkmc),
           new SqlParameter("@xmh",xmh),
           new SqlParameter("@ljh",ljh),
           new SqlParameter("@cp_status",cp_status),
          new SqlParameter("@yqfy_date_start",yqfy_date_start),
          new SqlParameter("@yqfy_date_end",yqfy_date_end),
          new SqlParameter("@yqdh_date_start",yqdh_date_start),
          new SqlParameter("@yqdh_date_end",yqdh_date_end),
          new SqlParameter("@tzfy_date_start",tzfy_date_start),
          new SqlParameter("@tzfy_date_end",tzfy_date_end),
          new SqlParameter("@Ticket_status",Ticket_status),
          new SqlParameter("@code",code),
          new SqlParameter("@gkddh",gkddh),
          new SqlParameter("@xgry",xgry),
          new SqlParameter("@zzgc",zzgc),
          new SqlParameter("@fhstatus",fhstatus),
          new SqlParameter("@shrxx",shrxx),
          new SqlParameter("@shdz",shdz),
            new SqlParameter("@depart",depart)
     };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("rpt_Form1_sale_YJ_Query_NoCPStatus", param);

    }
    public DataTable Get_YJ_Query_Report_ToExcel(string gkdm, string gkmc, string xmh, string ljh,
      string yqfy_date_start, string yqfy_date_end, string yqdh_date_start, string yqdh_date_end,
      string tzfy_date_start, string tzfy_date_end, string Ticket_status, string code, string gkddh,
      string shrxx,string shdz,string xgry, string depart)
    {
        SqlParameter[] param = new SqlParameter[]
     {
          new SqlParameter("@gkdm",gkdm),
          new SqlParameter("@gkmc",gkmc),
          new SqlParameter("@xmh",xmh),
          new SqlParameter("@ljh",ljh),
          new SqlParameter("@yqfy_date_start",yqfy_date_start),
          new SqlParameter("@yqfy_date_end",yqfy_date_end),
          new SqlParameter("@yqdh_date_start",yqdh_date_start),
          new SqlParameter("@yqdh_date_end",yqdh_date_end),
          new SqlParameter("@tzfy_date_start",tzfy_date_start),
          new SqlParameter("@tzfy_date_end",tzfy_date_end),
          new SqlParameter("@Ticket_status",Ticket_status),
          new SqlParameter("@code",code),
          new SqlParameter("@gkddh",gkddh),
          new SqlParameter("@shrxx",shrxx),
          new SqlParameter("@shdz",shdz),
          new SqlParameter("@xgry",xgry),
          new SqlParameter("@depart",depart)
     };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("rpt_Form1_sale_YJ_Query_ToExcel", param);

    }
    /// <summary>
    /// 调用PRO rpt_Form1_sale_YJ_DelayShip_details 根据年，月，日获取 未按时发货明细
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <param name="day">日</param>
    /// <returns></returns>
    public DataTable Get_YJ_DelayShip_Details(string year, string month, string day,string type)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@year",year),
           new SqlParameter("@month",month),
           new SqlParameter("@day",day),
           new SqlParameter("@type",type)
     };
        DataTable dt = new DataTable();
        return YangjianSQLHelper.GetDataTable("rpt_Form1_sale_YJ_DelayShip_details", param);
    }
    /// <summary>
    /// DataTable中的数据导出到Excel并下载
    /// </summary>
    /// <param name="dt">要导出的DataTable</param>
    /// <param name="FileType">类型</param>
    /// <param name="FileName">Excel的文件名</param>
    public void DataTableToExcel(DataTable dt, string FileType, string FileName, string b_head)
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Charset = "UTF-8";
        System.Web.HttpContext.Current.Response.Buffer = true;
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
        System.Web.HttpContext.Current.Response.ContentType = FileType;
        string colHeaders = string.Empty;
        string ls_item = string.Empty;
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        if (b_head == "1")
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ls_item += dt.Columns[j].ColumnName + "\t";
            }
        }
        ls_item += "\n";
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    ls_item += row[i].ToString() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString() + "\t";
                }
            }
            System.Web.HttpContext.Current.Response.Output.Write(ls_item);
            ls_item = string.Empty;
        }
        System.Web.HttpContext.Current.Response.Output.Flush();
        System.Web.HttpContext.Current.Response.End();
    }
}