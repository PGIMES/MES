using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Wuliu_tr_hist 的摘要说明
/// </summary>
public class Wuliu_tr_hist
{
    public Wuliu_tr_hist()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    SQLHelper SQLHelper = new SQLHelper();

    /// <summary>
    /// 根据物料号和公司别 
    /// </summary>
    /// <param name="domain">公司别</param>
    /// <param name="part_no">物料号</param>
    /// <returns></returns>
    public DataTable Get_tr_list_query(string flag,string domain, string site, string part_no_start, string curmonth)
    {
        SqlParameter[] param = new SqlParameter[]
      {
            new SqlParameter("@flag",flag),
           new SqlParameter("@domain",domain),
           new SqlParameter("@site",site),
           new SqlParameter("@part_no_start",part_no_start),
           new SqlParameter("@curmonth",curmonth)
      };
         
        return SQLHelper.GetDataTable("Report_tr_hist", param);

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