using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

            //包材合同,邮件CC给财务 吴燕蓝
            if (syscontractno.StartsWith("108"))
            {
                string body = "Dear all:<br />【包材合同】,合同号" + syscontractno + ",采购订单号" + pono + "采购行号" + rowid + ",到货确认完毕，请知悉.";
                string to_add = "yanlan.wu@pgi.cn";

                SmtpClient mail = new SmtpClient();
                mail.DeliveryMethod = SmtpDeliveryMethod.Network; //发送方式           
                mail.Host = "mail.pgi.cn"; //smtp服务器                                
                mail.Credentials = new System.Net.NetworkCredential("oa@pgi.cn", "pgi_1234");//用户名凭证  

                MailMessage message = new MailMessage();//邮件信息            
                message.From = new MailAddress("oa@pgi.cn");//发件人           
                message.To.Add(to_add); //收件人
                //message.CC.Add(to_cc);//抄送收件人
                message.Bcc.Add("guiqin.he@pgi.cn");//,angela.xu@pgi.cn

                message.Subject = "【包材合同】,合同号" + syscontractno + ",到货确认通知";//主题            
                message.Body = body;//内容           
                message.BodyEncoding = System.Text.Encoding.UTF8; //正文编码            
                message.IsBodyHtml = true;//设置为HTML格式            
                message.Priority = MailPriority.Normal;//优先级
                message.Attachments.Clear();

                //Attachment attach = new Attachment(resultFilePath);
                //message.Attachments.Add(attach);

                mail.Send(message);

            }
        }
        else
        {
            msg = "确认失败！";
        }
        string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);

    }
}