using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using Maticsoft.DBUtility;

/// <summary>
/// Wuliu 的摘要说明
/// </summary>
public class Wuliu
{
    public Wuliu()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    Wuliu_SF_SQLHelp SQLHelper = new Wuliu_SF_SQLHelp();

    public DataTable Get_SF_Partname_query(string domain,string part_no)
    {
        SqlParameter[] param = new SqlParameter[]
       {
           new SqlParameter("@domain",domain),
           new SqlParameter("@part_no",part_no)
       };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("SF_PART_NAME_Query", param);
    }
    /// <summary>
    /// 根据物料号和公司别 获取IE维护的 夹具数量
    /// </summary>
    /// <param name="domain">公司别</param>
    /// <param name="part_no">物料号</param>
    /// <returns></returns>
    public DataTable Get_SF_Fixture_Qty_query(string domain, string part_no)
    {
        SqlParameter[] param = new SqlParameter[]
      {
           new SqlParameter("@domain",domain),
           new SqlParameter("@part_no",part_no)
      };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("SF_Fixture_Qty_query", param);

    }
    /// <summary>
    /// 返回值1 表示成功 -1 表示失败
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="part_no"></param>
    /// <returns></returns>
    public int Maintain_Fixture_Qty(string comp, string part,string @FIXTURE_QTY_ACT, string empid)
    {
        SqlParameter[] param = new SqlParameter[]
    {
           new SqlParameter("@comp",comp),
           new SqlParameter("@part",part),
           new SqlParameter("@FIXTURE_QTY_ACT",FIXTURE_QTY_ACT),
            new SqlParameter("@empid",empid)
    };
        return SQLHelper.ExecuteNonQuery("SF_Maintain_Fixture_Qty_PRO", param);
    }

    public DataTable Get_SF_QAD_REORT_Query(string domain,string part_no,string part_name,string status)
    {
        SqlParameter[] param = new SqlParameter[]
     {
           new SqlParameter("@domain",domain),
           new SqlParameter("@part_no",part_no),
           new SqlParameter("@part_name",part_name),
           new SqlParameter("@status",status)
     };
        DataTable dt = new DataTable();
        return SQLHelper.GetDataTable("SF_QAD_MONITOR_REPORT_PRO", param);

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