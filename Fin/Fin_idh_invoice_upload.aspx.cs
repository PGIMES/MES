using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Aspose.Cells;
using System.Data.SqlClient;
using System.Configuration;
using Maticsoft.DBUtility;
using System.Net.Mail;

public partial class Fin_Fin_idh_invoice_upload : System.Web.UI.Page
{
    //保存上传文件路径
    public static string savepath = @"UploadFile\Fin\invoice";

    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        Session["LogUser"] = LogUserModel;

        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv_his.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string isSubmissionExpired = "Y";

        string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
        //string resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
        string resultFileName = Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss")+ resultExtension;

        string resultFilePath = MapPath("~") + savepath + @"\" + resultFileName;
        if (!Directory.Exists(MapPath("~") + savepath + @"\"))
        {
            Directory.CreateDirectory(MapPath("~") + savepath + @"\");
        }
        e.UploadedFile.SaveAs(resultFilePath);

        string result = "", DesTableName = "idh_invoice_upload_temp"; decimal sumamount = 0;string ih_bill_name = "";
        DbHelperSQL.ExecuteSql("truncate table " + DesTableName);
        importdata(resultFilePath, DesTableName, resultFileName, e.UploadedFile.FileName,out result, out ih_bill_name, out sumamount);

        if (result != "")
        {
            File.Delete(resultFilePath);
            e.CallbackData = isSubmissionExpired + "|" + result;
            return;
        }

        try
        {
            //string sql = @"insert into idh_invoice_upload select * from idh_invoice_upload_temp";
            //DbHelperSQL.ExecuteSql(sql);
            string sql = @"exec [Report_idh_invoice_upload]";
            DataTable dt_flag = DbHelperSQL.Query(sql).Tables[0];

            if (dt_flag.Rows[0][0].ToString() == "Y")
            {
                DataTable dt_error = DbHelperSQL.Query(@"select * from idh_invoice_upload_temp where ISNULL(errorMessage,'')<>''").Tables[0];
                for (int i = 0; i < dt_error.Rows.Count; i++)
                {
                    result = result + "发票号" + dt_error.Rows[i]["ih_inv_nbr"].ToString() + " 发货至" + dt_error.Rows[i]["ih_ship"].ToString()
                        + " 物料号" + dt_error.Rows[i]["idh_part"].ToString() + " error:" + dt_error.Rows[i]["errorMessage"].ToString()
                        + "<br />";
                }
                e.CallbackData = isSubmissionExpired + "|" + result;
                return;
            }

            //发邮件给上传人、蔡红玲 <hongling.cai@pgi.cn>，Cc '徐殿青'<edward.xu@pgi.cn>徐镇jim.xu <jim.xu@pgi.cn>
            DataTable dt = DbHelperSQL.Query("SELECT  * FROM V_HRM_EMP_MES where workcode='" + ((LoginUser)Session["LogUser"]).UserId + "'").Tables[0];
            string to_add = dt.Rows[0]["email"].ToString();

            SmtpClient mail = new SmtpClient();
            mail.DeliveryMethod = SmtpDeliveryMethod.Network; //发送方式           
            mail.Host = "mail.pgi.cn"; //smtp服务器                                
            mail.Credentials = new System.Net.NetworkCredential("oa@pgi.cn", "pgi_1234");//用户名凭证  

            MailMessage message = new MailMessage();//邮件信息            
            message.From = new MailAddress("oa@pgi.cn");//发件人           
            message.To.Add(to_add + ",hongling.cai@pgi.cn"); //收件人            
            //message.CC.Add("guiqin.he@pgi.cn,guiqin.he@pgi.cn");//抄送收件人edward.xu@pgi.cn,jim.xu@pgi.cn
            message.Bcc.Add("guiqin.he@pgi.cn,angela.xu@pgi.cn");

            message.Subject = "【开票通知单】上传成功";//主题            
            message.Body = "Dear all:<br />票据开往名称：" + ih_bill_name + ",总金额：" + sumamount.ToString();//内容           
            message.BodyEncoding = System.Text.Encoding.UTF8; //正文编码            
            message.IsBodyHtml = true;//设置为HTML格式            
            message.Priority = MailPriority.Normal;//优先级
            message.Attachments.Clear();

            Attachment attach = new Attachment(resultFilePath);
            message.Attachments.Add(attach);

            mail.Send(message);
        }
        catch (Exception ex)
        {
            result = ex.Message;
            e.CallbackData = isSubmissionExpired + "|" + result;
            return;
        }

        isSubmissionExpired = "N";
        e.CallbackData = isSubmissionExpired + "|" + result;
        return;


    }

    public void importdata(string fileName, string DesTableName, string new_filename, string ori_filename, out string result, out string ih_bill_name, out decimal sumamount)
    {
        result = ""; ih_bill_name = ""; sumamount = 0;
        try
        {
            DataTable dtExcel = GetExcelData_Table(fileName, 0);
            if (dtExcel == null || dtExcel.Rows.Count <= 0 || dtExcel.Columns.Count != 19)
            {
                result = "No Data";
            }
            else
            {
                DataTable dt = new DataTable();
                DataColumn col_0 = new DataColumn("idh_part", typeof(string));
                DataColumn col_1 = new DataColumn("ih_inv_nbr", typeof(string));
                DataColumn col_2 = new DataColumn("ih_ship", typeof(string));
                DataColumn col_3 = new DataColumn("idh_qty_inv", typeof(decimal));
                DataColumn col_4 = new DataColumn("idh_price_inv", typeof(decimal));
                DataColumn col_5 = new DataColumn("idh_taxc_new", typeof(decimal));
                DataColumn col_6 = new DataColumn("ori_filename", typeof(string));
                DataColumn col_7 = new DataColumn("new_filename", typeof(string));
                DataColumn col_8 = new DataColumn("ih_inv_date", typeof(DateTime));
                DataColumn col_9 = new DataColumn("isdel", typeof(string));
                DataColumn col_10 = new DataColumn("CreateById", typeof(string));
                DataColumn col_11 = new DataColumn("status", typeof(string));
                dt.Columns.Add(col_0); dt.Columns.Add(col_1); dt.Columns.Add(col_2); dt.Columns.Add(col_3); dt.Columns.Add(col_4); dt.Columns.Add(col_5);
                dt.Columns.Add(col_6); dt.Columns.Add(col_7); dt.Columns.Add(col_8); dt.Columns.Add(col_9); dt.Columns.Add(col_10);
                dt.Columns.Add(col_11);

                for (int k = 0; k < dtExcel.Rows.Count; k++)
                {
                    DataRow dr = dtExcel.Rows[k];
                    if (dr["物料号"].ToString().Trim() == "" || dr["发票号"].ToString().Trim() == "" || dr["发货至"].ToString().Trim() == "")
                    {
                        continue;
                    }
                    if (dr["本次开票数量"].ToString().Trim() == "")
                    {
                        result = "发票号" + dr["发票号"].ToString().Trim() + " 发货至" + dr["发货至"].ToString().Trim()
                            + " 物料号" + dr["物料号"].ToString().Trim() + " 【本次开票数量】不可为空！" + "<br />";
                        break;
                    }
                    if (dr["开票价格"].ToString().Trim() == "")
                    {
                        result = "发票号" + dr["发票号"].ToString().Trim() + " 发货至" + dr["发货至"].ToString().Trim()
                           + " 物料号" + dr["物料号"].ToString().Trim() + " 【开票价格】不可为空！" + "<br />";
                        break;
                    }
                    if (dr["税率"].ToString().Trim() == "")
                    {
                        result = "发票号" + dr["发票号"].ToString().Trim() + " 发货至" + dr["发货至"].ToString().Trim()
                           + " 物料号" + dr["物料号"].ToString().Trim() + " 【税率】不可为空！" + "<br />";
                        break;
                    }

                    DataRow dt_r = dt.NewRow();
                    dt_r["idh_part"] = dr["物料号"].ToString();
                    dt_r["ih_inv_nbr"] = dr["发票号"].ToString();
                    dt_r["ih_ship"] = dr["发货至"].ToString();
                    dt_r["idh_qty_inv"] = dr["本次开票数量"].ToString().Trim();
                    dt_r["idh_price_inv"] = dr["开票价格"].ToString().Trim();
                    dt_r["idh_taxc_new"] = dr["税率"].ToString().Trim();
                    dt_r["ori_filename"] = ori_filename;
                    dt_r["new_filename"] = new_filename;
                    dt_r["ih_inv_date"] = DateTime.Now;
                    dt_r["isdel"] = "N";
                    dt_r["CreateById"] = ((LoginUser)Session["LogUser"]).UserId;
                    dt_r["status"] = "待开票";
                    dt.Rows.Add(dt_r);

                    if (k == 0) { ih_bill_name = dr["票据开往名称"].ToString(); }
                    sumamount = sumamount + Convert.ToDecimal(dr["税款合计new"].ToString().Replace(",", ""));
                }

                if (result == "")
                {
                    bool bf = DataTableToSQLServer(dt, DesTableName);
                    result = bf == true ? "" : "excel导入临时表失败";
                }

            }
        }
        catch (Exception ex)
        {
            result = "读取excel异常：" + ex.Message;
            sumamount = 0;
        }

    }

    public DataTable GetExcelData_Table(string filePath, int sheetPoint)
    {
        Workbook book = new Workbook(filePath);
        Worksheet sheet = book.Worksheets[sheetPoint];
        Cells cells = sheet.Cells;
        book.CalculateFormula();
        //DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);//获取excel中的数据保存到一个datatable中
        DataTable dt_Import = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);//获取excel中的数据保存到一个datatable中

        //Row row = cells.GetRow(0);
        Row row = cells.GetRow(1);
        for (int i = 0; i < cells.MaxDataColumn + 1; i++)
        {
            dt_Import.Columns[i].ColumnName = row.GetCellOrNull(i).Value.ToString();
        }
        return dt_Import;

    }
    public bool DataTableToSQLServer(DataTable dt, string DesTableName)
    {
        bool bf = true;
        string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        using (SqlConnection destinationConnection = new SqlConnection(connectionString))
        {
            destinationConnection.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
            {
                try
                {

                    bulkCopy.DestinationTableName = DesTableName;//要插入的表的表名
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.ColumnMappings.Add("idh_part", "idh_part");//映射字段名 DataTable列名 ,数据库 对应的列名   
                    bulkCopy.ColumnMappings.Add("ih_inv_nbr", "ih_inv_nbr");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("ih_ship", "ih_ship");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("idh_qty_inv", "idh_qty_inv");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("idh_price_inv", "idh_price_inv");
                    bulkCopy.ColumnMappings.Add("idh_taxc_new", "idh_taxc_new");
                    bulkCopy.ColumnMappings.Add("ori_filename", "ori_filename");
                    bulkCopy.ColumnMappings.Add("new_filename", "new_filename");
                    bulkCopy.ColumnMappings.Add("ih_inv_date", "ih_inv_date");
                    bulkCopy.ColumnMappings.Add("isdel", "isdel");
                    bulkCopy.ColumnMappings.Add("CreateById", "CreateById");
                    bulkCopy.ColumnMappings.Add("status", "status");

                    bulkCopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    bf = false;
                }
            }

            return bf;
        }

    }
    
    public void QueryASPxGridView()
    {
        string sql = @"select distinct ori_filename,new_filename,createbyid 
                    from idh_invoice_upload where status='待开票' and isdel='N'
                        and new_filename not in(select distinct new_filename from idh_invoice_upload where status='已开票' and isdel='N')";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        gv_his.DataSource = dt;
        gv_his.DataBind();
    }
    protected void gv_his_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void gv_his_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        QueryASPxGridView();
    }

    protected void gv_his_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        string new_filename = e.Values["new_filename"].ToString();
        string ori_filename = e.Values["ori_filename"].ToString();
        string createbyid = e.Values["createbyid"].ToString();

        string sql = @"update idh_invoice_upload set isdel='Y' where new_filename='"+ new_filename + "'";
        DbHelperSQL.ExecuteSql(sql);

        string resultFilePath = MapPath("~") + savepath + @"\" + new_filename;
        //File.Delete(resultFilePath);

        //发邮件给上传人、蔡红玲 <hongling.cai@pgi.cn>，Cc '徐殿青'<edward.xu@pgi.cn>徐镇jim.xu <jim.xu@pgi.cn>
        DataTable dt = DbHelperSQL.Query("SELECT  * FROM V_HRM_EMP_MES where workcode='" + ((LoginUser)Session["LogUser"]).UserId + "' or workcode='" + createbyid + "'").Tables[0];
        string to_add = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            to_add = to_add + dt.Rows[0]["email"].ToString() + ",";
        }
        to_add = to_add.Substring(0, to_add.Length - 1);

        SmtpClient mail = new SmtpClient();
        mail.DeliveryMethod = SmtpDeliveryMethod.Network; //发送方式           
        mail.Host = "mail.pgi.cn"; //smtp服务器                                
        mail.Credentials = new System.Net.NetworkCredential("oa@pgi.cn", "pgi_1234");//用户名凭证  

        MailMessage message = new MailMessage();//邮件信息            
        message.From = new MailAddress("oa@pgi.cn");//发件人           
        message.To.Add(to_add + ",hongling.cai@pgi.cn"); //收件人            
        //message.CC.Add("guiqin.he@pgi.cn,guiqin.he@pgi.cn");//抄送收件人edward.xu@pgi.cn,jim.xu@pgi.cn
        message.Bcc.Add("guiqin.he@pgi.cn,angela.xu@pgi.cn");

        message.Subject = "【开票通知单】删除成功";//主题            
        //message.Body = "Dear all:<br />票据开往名称：" + ih_bill_name + ",总金额：" + sumamount.ToString();//内容           
        message.BodyEncoding = System.Text.Encoding.UTF8; //正文编码            
        message.IsBodyHtml = true;//设置为HTML格式            
        message.Priority = MailPriority.Normal;//优先级
        message.Attachments.Clear();

        Attachment attach = new Attachment(resultFilePath);
        message.Attachments.Add(attach);

        mail.Send(message);

        QueryASPxGridView();
    }

}