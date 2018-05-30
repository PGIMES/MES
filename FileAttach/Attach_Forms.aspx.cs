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


public partial class FileAttach_Attach_Forms : System.Web.UI.Page
{
    AttachUpload AttachUpload = new AttachUpload();
    //保存上传文件路径
    public static string savepath = "", formid = "", stepid = "", formno = "";

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

    public void QueryASPxGridView(int flag = 0)
    {
        if (flag == 1)
        {
            DataTable dt_o = (DataTable)Session["attach_forms"];
            bool bf = false;//变量为true，需要查询数据库的数据出来
            if (dt_o == null) { bf = true; }
            else
            {
                DataRow[] drs_session = dt_o.Select("formid='" + formid + "' and stepid='" + stepid + "' and wlh='" + formno + "'");
                if (drs_session.Length <= 0) { bf = true; }
            }

            if (bf)
            {
                DataSet ds = AttachUpload.AttachUpload_List("select_form", formid, stepid, formno);
                if (dt_o == null)
                {
                    dt_o = ds.Tables[0].Clone();
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    dt_o.Rows.Add(item.ItemArray);
                }
                Session["attach_forms"] = dt_o;
            }
           
        }

        DataTable dt_n = (DataTable)Session["attach_forms"];
        DataRow[] drs = dt_n.Select("flag<>'del' and formid='" + formid + "'and stepid='" + stepid + "' and wlh='" + formno + "'");

        DataTable dt = dt_n.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt.Rows.Add(row.ItemArray);
        }

        gv_list.DataSource = dt;
        gv_list.DataBind();
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["attach_forms"];

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

        DataRow dr = dt.NewRow();
        dr["rownum"] = dt.Rows.Count <= 0 ? 1 : Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["rownum"]) + 1;
        dr["id"] = 0;
        dr["formid"] = formid;
        dr["stepid"] = stepid;
        dr["wlh"] = formno;
        dr["OriginalFile"] = name;
        dr["FilePath"] = "\\" + savepath + "\\" + resultFileName;
        dr["FileType"] = resultExtension;
        dr["CreateDate"] = DateTime.Now;
        dr["CreateUser"] = ((LoginUser)Session["LogUser"]).UserId; ;
        dr["flag"] = "add";

        dt.Rows.Add(dr);
        Session["attach_forms"] = dt;

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
        DataTable dt = (DataTable)Session["attach_forms"];

        List<object> lSelectValues = gv_list.GetSelectedFieldValues("rownum");

        if (lSelectValues.Count <= 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", " 请选择需要删除的文件!");
            return;
        }

        for (int i = 0; i < lSelectValues.Count; i++)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["rownum"].ToString()== lSelectValues[i].ToString())
                {
                    dt.Rows[j]["flag"] = "del";
                }
            }
        }

        Session["attach_forms"] = dt;

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




}