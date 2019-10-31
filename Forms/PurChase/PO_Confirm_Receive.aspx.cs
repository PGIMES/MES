using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_pt_mstr : System.Web.UI.Page
{
    protected string domain = "";
    protected string syscontractno = "";
    protected string pono = "";
    public string rowid = "";
    public int purqty = 0;
    public int qty_dsh = 0;
    protected string contractname = "";

    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        domain = Request.QueryString["domain"].ToString();
        syscontractno = Request.QueryString["syscontractno"].ToString();
        pono = Request.QueryString["pono"].ToString();
        rowid = Request.QueryString["rowid"].ToString();
        purqty = Convert.ToInt32(Request.QueryString["purqty"].ToString());
        qty_dsh = Convert.ToInt32(Request.QueryString["qty_dsh"].ToString());

        if (!IsPostBack)
        {
            txt_SysContractNo.Text = syscontractno;
            txt_domain.Text = domain;
            txt_PoNo.Text = pono;
            txt_rowid.Text = rowid;
            txt_PurQty.Text = purqty.ToString();
            txt_qty_dsh.Text = qty_dsh.ToString();

            //包材合同，收货数量默认等于待收货数量，也就是总数量，一次性验收
            if (syscontractno.StartsWith("108")) { txt_qty.Text = qty_dsh.ToString(); }

            ActualReceiveDate.MaxDate = DateTime.Now;
            ActualReceiveDate.Value = DateTime.Now;
        }

    }

    //保存上传文件路径
    public static string savepath = @"\UploadFile\Purchase\Receive";
    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {

        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }



    protected void btn_save_Click(object sender, EventArgs e)
    {
        int i = 0;
        try
        {
            //上传文件路径
            string filepath = "";//string filepath=this.UploadFiles(this.uploadcontrol);
            string[] ls_files = ip_filelist.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in ls_files)
            {
                string[] ls_files_2 = item.Split(',');
                if (ls_files_2.Length == 3)//挪动路径到po单号下面
                {
                    FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

                    var despath = MapPath("~") + savepath + @"\" + syscontractno + @"\";
                    if (!System.IO.Directory.Exists(despath))
                    {
                        System.IO.Directory.CreateDirectory(despath);
                    }
                    string tmp = despath + ls_files_2[1].Replace(savepath, "");
                    if (File.Exists(tmp))
                    {
                        File.Delete(tmp);
                    }
                    fi.MoveTo(tmp);

                    filepath += item.Replace(@"\UploadFile\Purchase\Receive\", @"\UploadFile\Purchase\Receive\" + syscontractno + @"\") + ";";
                }
                else
                {
                    filepath += item + ";";
                }
            }

            if (filepath != "") { filepath = filepath.Substring(0, filepath.Length - 1); }

            string sql = "";
            if (syscontractno.EndsWith("_QAD"))
            {
                sql = @"insert into PUR_PO_Contract_ActaulReceive(domain,SysContractNo,qty,ActualReceiveDate,Attachments,UpdateId,UpdateTime)
                       select '{0}','{1}','{2}','{3}','{4}','{5}',getdate()";
            }
            else
            {
                sql = @"insert into PUR_PO_Contract_ActaulReceive(domain,SysContractNo,qty,ActualReceiveDate,Attachments,UpdateId,UpdateTime,pono,rowid)
                       select '{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}','{7}'";
            }
            sql = string.Format(sql, domain, syscontractno, txt_qty.Text, ActualReceiveDate.Text, filepath, LogUserModel.UserId, pono, rowid);
            i = DbHelperSQL.ExecuteSql(sql);
        }
        catch (Exception ex)
        {
            i = 0;
        }

        string msg = "";
        if (i > 0)
        {
            msg = "确认成功！";
        }
        else
        {
            msg = "确认失败！";
        }
        string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);

    }
}