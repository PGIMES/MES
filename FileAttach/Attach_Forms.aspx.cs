using DevExpress.Web;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region 文件model
public class attach_forms
{
    public attach_forms()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //       

    }
    /// <summary>
    /// 序号，记载数据session中key
    /// </summary>
    public Int64 rownum { get; set; }

    /// <summary>
    /// id，数据库id字段
    /// </summary>
    public Int64 id { get; set; }

    /// <summary>
    /// 原本文件名称
    /// </summary>
    public string OriginalFile { get; set; }

    /// <summary>
    /// 文件在服务器上的相对路径\UploadFile\表单名称\表单编号\新文件名称
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    // 文件后缀名
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    /// 上传文件人
    /// </summary>
    public string CreateUser { get; set; }

    /// <summary>
    /// 上传文件时间
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 标记,新增add,删除del,已存在 空
    /// </summary>
    public string flag { get; set; }

}
#endregion

#region Datatable与list转换
public static class Extensetion
{
    /// <summary>    
    /// 将集合类转换成DataTable    
    /// </summary>    
    /// <param name="list">集合</param>    
    /// <returns></returns>    
    public static DataTable ToDataTableTow(IList list)
    {
        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                result.Columns.Add(pi.Name, pi.PropertyType);
            }
            foreach (object t in list)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(t, null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

    /// <summary>    
    /// DataTable 转换为List 集合    
    /// </summary>    
    /// <typeparam name="TResult">类型</typeparam>    
    /// <param name="dt">DataTable</param>    
    /// <returns></returns>    
    public static List<T> ToList<T>(this DataTable dt) where T : class, new()
    {
        //创建一个属性的列表    
        List<PropertyInfo> prlist = new List<PropertyInfo>();
        //获取TResult的类型实例  反射的入口    

        Type t = typeof(T);

        //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
        Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

        //创建返回的集合    

        List<T> oblist = new List<T>();

        foreach (DataRow row in dt.Rows)
        {
            //创建TResult的实例    
            T ob = new T();
            //找到对应的数据  并赋值    
            prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
            //放入到返回的集合中.    
            oblist.Add(ob);
        }
        return oblist;
    }
}
#endregion

public partial class FileAttach_Attach_Forms : System.Web.UI.Page
{
    AttachUpload AttachUpload = new AttachUpload();
    //保存上传文件路径
    public static string savepath = "", formid = "", stepid = "", formno = "", filesource = "fileupload";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["lpname"] != null) { savepath = @"UploadFile\" + Request["lpname"].ToString(); } else { savepath = "UploadFile"; }
            if (Request.QueryString["formid"] != null) { formid = Request["formid"].ToString(); } else { formid = ""; }
            if (Request.QueryString["stepid"] != null) { stepid = Request["stepid"].ToString(); } else { stepid = ""; }
            if (Request.QueryString["formno"] != null) { formno = Request["formno"].ToString(); } else { formno = ""; }
            if (Request.QueryString["option"] != null)
            {
                if (Request.QueryString["option"].ToString() != "edit")
                {
                    uploadcontrol.Visible = false; btn_del.Visible = false;
                }
            }

            LoginUser LogUserModel = null;
            if (Request.ServerVariables["LOGON_USER"].ToString() == "")
            {
                LogUserModel = InitUser.GetLoginUserInfo("02432", Request.ServerVariables["LOGON_USER"]);
            }
            else
            {
                LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
            }

            Session["LogUser"] = LogUserModel;

            QueryASPxGridView(1);
        }
        if (gv_list.IsCallback)
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView(int flag=0)
    {
        if (flag == 1)
        {
            DataSet ds = AttachUpload.AttachUpload_List("select_form", formid, stepid, formno);
            List<attach_forms> Listw = Extensetion.ToList<attach_forms>(ds.Tables[0]);
            Session["attach_forms"] = Listw;
        }
        List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];
        var List_tp = List.Where(item => item.flag != "del").ToList();

        gv_list.DataSource = List_tp;
        gv_list.DataBind();
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];
        attach_forms af = new attach_forms();

        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        if (!System.IO.Directory.Exists(MapPath("~") + savepath + "\\"))
        {
            System.IO.Directory.CreateDirectory(MapPath("~") + savepath + "\\");
        }
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        af.rownum = List.Count + 1;
        af.id = 0;
        af.OriginalFile = name;
        af.FilePath = "\\" + savepath + "\\" + resultFileName;
        af.FileExtension = resultExtension;
        af.CreateUser= ((LoginUser)Session["LogUser"]).UserId;
        af.CreateDate = DateTime.Now;
        af.flag = "add";


        List.Add(af);
        Session["attach_forms"] = List; 

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }

    protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_list_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "序号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
    }


    protected void btn_del_Click(object sender, EventArgs e)
    {
        List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];

        List<object> lSelectValues = gv_list.GetSelectedFieldValues("rownum");

        if (lSelectValues.Count <= 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", " 请选择需要删除的文件!");
            return;
        }

        for (int i = 0; i < lSelectValues.Count; i++)
        {
            var List_tp = List.Where(item => item.rownum == (Int64)lSelectValues[i]).ToList();
            foreach (var tp in List_tp)
            {
                tp.flag = "del";
            }
        }
        QueryASPxGridView();
    }

    //protected string GetDelID()
    //{
    //    string delId = "";
    //    //获取选中的记录Id   
    //    List<object> lSelectValues = gv_list.GetSelectedFieldValues("rownum");
    //    for (int i = 0; i < lSelectValues.Count; i++)
    //    {
    //        delId += lSelectValues[i] + ",";
    //    }
    //    delId = delId.Substring(0, delId.LastIndexOf(','));
    //    return delId;
    //}


    /// <summary>
    /// 文件记录保存到数据库
    /// </summary>

    public void UpdateToDB(string formid_save, string stepid_save, string formno_save, bool is_movefileToFormno)
    {
        List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];
        foreach (attach_forms af in List)
        {
            if (af.flag == "") { continue; }

            string rfp = MapPath("~") + af.FilePath;

            if (af.flag == "del")//id=0只有文件，没有记录，而且文件没有移动到formno下面；id>0既有文件，又有记录，而且文件在formno下面
            {
                File.Delete(rfp);//删除文件
                if (af.id > 0)
                {
                    AttachUpload.AttachUpload_delete("delete", af.id.ToString());
                }
            }
            if (af.flag == "add")
            {
                if (is_movefileToFormno)
                {
                    string filepath = rfp.Substring(0, rfp.LastIndexOf(@"\")) + @"\" + formno + @"\";

                    string despath = MapPath("~") + filepath;
                    if (!System.IO.Directory.Exists(despath))
                    {
                        System.IO.Directory.CreateDirectory(despath);
                    }
                    File.Move(rfp, despath + Path.GetFileName(rfp));

                    af.FilePath = filepath + Path.GetFileName(rfp);//移动成功之后修改路径
                }
                AttachUpload.AttachUpload_Edit("insert_form", formid, stepid
                                   , formno, af.OriginalFile, af.FilePath
                                   , af.FileExtension, filesource, af.CreateUser);
            }
        }
        Session["attach_forms"] = null;//保存后置空
    }


}