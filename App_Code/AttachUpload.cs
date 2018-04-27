using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// AttachUpload 的摘要说明
/// </summary>
public class AttachUpload
{
    public AttachUpload()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    SQLHelper SQLHelper = new SQLHelper();
    //public DataSet AttachUpload_List(string action,int id,
    //     string formid,string stepid, string wlh, string originalname, string filepath, string filetype, string filesource,
    //     string empid)
    //{
    //    SqlParameter[] param = new SqlParameter[]
    //     {
    //           new SqlParameter("@action",action),
    //           new SqlParameter("@id", id),
    //           new SqlParameter("@formid", formid),
    //           new SqlParameter("@stepid", stepid),
    //           new SqlParameter("@wlh", wlh),
    //           new SqlParameter("@originalname", originalname),
    //           new SqlParameter("@filepath",filepath),
    //           new SqlParameter("@filetype", filetype),
    //           new SqlParameter("@filesource",filesource),
    //           new SqlParameter("@empid", empid)
    //     };

    //    return SQLHelper.GetDataSet("MES_AttachUplad_List", param);
    //}

    public DataSet AttachUpload_List(string action, string formid, string stepid, string wlh)
    {
        SqlParameter[] param = new SqlParameter[]
         {
               new SqlParameter("@action",action),
               new SqlParameter("@formid", formid),
               new SqlParameter("@stepid", stepid),
               new SqlParameter("@wlh", wlh)
         };

        return SQLHelper.GetDataSet("MES_AttachUplad_List", param);
    }

    public int AttachUpload_Edit(string action,
         string formid, string stepid, string wlh, string originalname, string filepath, string filetype, string filesource,
         string empid)
    {
        SqlParameter[] param = new SqlParameter[]
         {
               new SqlParameter("@action",action),
               new SqlParameter("@formid", formid),
               new SqlParameter("@stepid", stepid),
               new SqlParameter("@wlh", wlh),
               new SqlParameter("@originalname", originalname),
               new SqlParameter("@filepath",filepath),
               new SqlParameter("@filetype", filetype),
               new SqlParameter("@filesource",filesource),
               new SqlParameter("@empid", empid)
         };

        return SQLHelper.ExecuteNonQuery("MES_AttachUplad_List", param);
    }

    public int AttachUpload_delete(string action, string id)
    {
        SqlParameter[] param = new SqlParameter[]
         {
               new SqlParameter("@action",action),
               new SqlParameter("@id", id),
         };

        return SQLHelper.ExecuteNonQuery("MES_AttachUplad_List", param);
    }
}