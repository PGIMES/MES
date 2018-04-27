using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileAttach_AttachUpload : System.Web.UI.Page
{
    string rootPath = HttpContext.Current.Request.PhysicalApplicationPath;
    public static string filepath = @"UploadFile\Attach";
    AttachUpload AttachUpload = new AttachUpload();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Directory.Exists(rootPath + filepath))
        {
            Directory.CreateDirectory(rootPath + filepath);
        }

        //if (Session["empid"] == null)
        //{
        //    InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        //}
        if (!IsPostBack)
        {
            //ViewState["empid"] = Session["empid"];
            ViewState["empid"] = "02432";
            BindData();
        }

    }

    public void BindData()
    {
        DataSet ds = AttachUpload.AttachUpload_List("select", Request["formid"], Request["stepid"], Request["wlh"]);
        gv_AttachList.DataSource = ds;
        gv_AttachList.Columns[0].Visible = true; gv_AttachList.Columns[1].Visible = true;
        gv_AttachList.DataBind();
        gv_AttachList.Columns[0].Visible = false; gv_AttachList.Columns[1].Visible = false;
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        HttpFileCollection files = Request.Files;
        if (files.Count <= 0 || txt_file.Value.Trim()=="")
        {
            //System.Web.HttpContext.Current.Response.Write("选择文件后再点击上传!");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('选择文件后再点击上传！')", true);
        }

        try
        {
            for (int iFile = 0; iFile < files.Count; iFile++)
            {
                //检查文件扩展名
                HttpPostedFile postFile = files[iFile];

                string originalname = string.Empty; string fileExtension = string.Empty; string fileName = string.Empty;

                originalname = System.IO.Path.GetFileName(postFile.FileName);
                int pos = originalname.LastIndexOf('.');
                if (pos > 0)
                {
                    fileExtension = originalname.Substring(pos + 1);
                    fileName = @"\" + filepath + @"\" + System.Guid.NewGuid().ToString() + "." + fileExtension;
                }

                if (fileName != "")
                {
                    string postPath = rootPath + fileName;
                    postFile.SaveAs(postPath);
                    
                    AttachUpload.AttachUpload_Edit("insert", Request["formid"], Request["stepid"]
                                    , Request["wlh"], originalname, fileName
                                    , fileExtension, Request["filesource"], ViewState["empid"].ToString());
                }
            }

            BindData();
        }
        catch (Exception ex)
        {
            //System.Web.HttpContext.Current.Response.Write(ex.Message);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('上传失败：" + ex.Message + "')", true);
        }
    }


    protected void btn_copy_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_file_copy.Value.Trim() == "")
            {
                //System.Web.HttpContext.Current.Response.Write("文件路径不可为空!");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('文件路径不可为空')", true);
                return;
            }

            AttachUpload.AttachUpload_Edit("insert", Request["formid"], Request["stepid"]
                                  , Request["wlh"], txt_file_copy.Value.Trim(), txt_file_copy.Value.Trim()
                                  , "", Request["filesource"], ViewState["empid"].ToString());

            BindData();
        }
        catch (Exception ex)
        {
            //System.Web.HttpContext.Current.Response.Write("文件上传复制失败!");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('文件上传复制失败')", true);
        }
    }


    //protected void gv_AttachList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string id = e.Keys[0].ToString();
    //    string file_attach = ((HyperLink)gv_AttachList.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
    //    string filepath = MapPath("~") + file_attach;

    //    AttachUpload.AttachUpload_delete("delete", id);

    //    if (System.IO.File.Exists(filepath))
    //    {
    //        System.IO.File.Delete(filepath);
    //    }
    //    BindData();
    //}

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < this.gv_AttachList.Rows.Count; i++)
        {
            int id = Convert.ToInt32(this.gv_AttachList.DataKeys[i].Value);
            if ((this.gv_AttachList.Rows[i].Cells[0].FindControl("cbk_select") as CheckBox).Checked == true)
            {
                ids = ids + "'" + id.ToString() + "',";
                if (gv_AttachList.Rows[i].Cells[1].Text == "fileupload")
                {
                    string file_attach = ((HyperLink)gv_AttachList.Rows[i].FindControl("HyperLink1")).NavigateUrl;
                    string filepath = MapPath("~") + file_attach;

                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                AttachUpload.AttachUpload_delete("delete", id.ToString());
            }
        }

        if (ids == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请选择需要删除的记录！')", true);
            return;
        }

        //ids = ids.Substring(0, ids.Length - 1);
        //AttachUpload.AttachUpload_delete("delete", ids);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除成功！')", true);
        BindData();
    }

    protected void gv_AttachList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_AttachList.SelectedIndex = -1;
        gv_AttachList.PageIndex = e.NewPageIndex;

        BindData();
    }
}