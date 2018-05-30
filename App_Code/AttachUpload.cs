using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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


    //返回文件新增修改SQL
    public List<Pgi.Auto.Common> GetAttachSql(DataTable dt_o, string dirpath, string formid, string stepid, string formno, string formno_ori, bool is_movefileToFormno,out DataRow[] drs, string filesource = "fileupload")
    {
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //DataTable dt_o = (DataTable)Session["attach_forms"];
        if (formno_ori != "")
        {
            drs = dt_o.Select("formid='" + formid + "'and stepid='" + stepid + "' and wlh='" + formno + "'");
        }
        else
        {
            drs = dt_o.Select("formid='" + formid + "'and stepid='" + stepid + "'");
        }
        

        DataTable dt = dt_o.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt.Rows.Add(row.ItemArray);
        }

        foreach (DataRow item in dt.Rows)
        {
            if (item["flag"].ToString() == "") { continue; }

            //string rfp = MapPath("~") + item["FilePath"].ToString();
            string rfp = dirpath + item["FilePath"].ToString();

            if (item["flag"].ToString() == "del")//id=0只有文件，没有记录，而且文件没有移动到formno下面；id>0既有文件，又有记录，而且文件在formno下面
            {
                if (item["id"].ToString() != "0")
                {
                    Pgi.Auto.Common ls = new Pgi.Auto.Common();
                    ls.Sql = @"delete from MES_AttachUpload where id=" + item["id"].ToString();
                    ls_sum.Add(ls);
                }
            }
            if (item["flag"].ToString() == "add")
            {
                if (is_movefileToFormno)//移动成功之后修改路径
                {
                    string filepath = item["FilePath"].ToString().Substring(0, item["FilePath"].ToString().LastIndexOf(@"\")) + @"\" + formno + @"\";
                    item["FilePath"] = filepath + Path.GetFileName(rfp);
                }
                Pgi.Auto.Common ls = new Pgi.Auto.Common();
                ls.Sql = @"insert into MES_AttachUpload(FormId, StepId, Wlh, OriginalFile, FilePath
                                        , FileType, FileSource, CreateUser, CreateDate
                                    )
                            select '" + formid + "','" + stepid + "','" + formno + "','" + item["OriginalFile"].ToString() + "','" + item["FilePath"].ToString()
                                + "','" + item["FileType"].ToString() + "','" + filesource + "','" + item["CreateUser"].ToString() + "',getdate()";
                ls_sum.Add(ls);
            }
        }

        return ls_sum;
    }

    //处理文件去向
    public string DealAttachFile(DataRow[] drs, string dirpath, string formid, string stepid, string formno, bool is_movefileToFormno)
    {
        string msg = "";
        try
        {
            foreach (DataRow item in drs)
            {
                if (item["flag"].ToString() == "") { continue; }
                //string rfp = MapPath("~") + item["FilePath"].ToString();
                string rfp = dirpath + item["FilePath"].ToString();

                if (item["flag"].ToString() == "del")
                {
                    File.Delete(rfp);//删除文件
                }

                if (item["flag"].ToString() == "add")
                {
                    if (is_movefileToFormno)
                    {
                        string despath = rfp.Substring(0, rfp.LastIndexOf(@"\")) + @"\" + formno + @"\";
                        if (!System.IO.Directory.Exists(despath))
                        {
                            System.IO.Directory.CreateDirectory(despath);
                        }
                        File.Move(rfp, despath + Path.GetFileName(rfp));
                    }
                }
            }
            //Session["attach_forms"] = null;//保存后置空
        }
        catch (Exception ex)
        {
            msg = "error";
        }
        return msg;
    }

}