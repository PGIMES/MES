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

public partial class Fin_WLYF_upload_multi : System.Web.UI.Page
{
    //保存上传文件路径
    public static string savepath = @"UploadFile\wuliu\wlfy";

    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        Session["LogUser"] = LogUserModel;

    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string isSubmissionExpired = "Y";

        string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
        //string resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
        string resultFileName = Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss") + resultExtension;

        string resultFilePath = MapPath("~") + savepath + @"\" + resultFileName;
        if (!Directory.Exists(MapPath("~") + savepath + @"\"))
        {
            Directory.CreateDirectory(MapPath("~") + savepath + @"\");
        }
        e.UploadedFile.SaveAs(resultFilePath);

        string result = "", DesTableName = "WLYF_upload_temp";
        DbHelperSQL.ExecuteSql("truncate table " + DesTableName);
        importdata(resultFilePath, DesTableName, resultFileName, e.UploadedFile.FileName, out result);

        if (result != "")
        {
            File.Delete(resultFilePath);
            e.CallbackData = isSubmissionExpired + "|" + result;
            return;
        }

        try
        {
            string sql = @"exec [Report_WLYF_upload]";
            DataTable dt_flag = DbHelperSQL.Query(sql).Tables[0];

            if (dt_flag.Rows[0][0].ToString() == "Y")
            {
                DataTable dt_error = DbHelperSQL.Query(@"select * from WLYF_upload_temp where ISNULL(errorMessage,'')<>''").Tables[0];
                for (int i = 0; i < dt_error.Rows.Count; i++)
                {
                    result = result + "第" + dt_error.Rows[i]["line"].ToString() + "行:" + dt_error.Rows[i]["errorMessage"].ToString() + "<br />";
                }
                e.CallbackData = isSubmissionExpired + "|" + result;
                return;
            }

          
            string body = "Dear all:";
            body = body + "<br/>【请款通知】上传成功,见附件.";//内容       
            body = body + "<br/><br/><a href='http://172.16.5.26:8010/wuliu/WLYF.aspx'>请点击查看</a>";//内容         

            //发邮件给上传人、吴燕蓝 <yanlan.wu@pgi.cn>
            DataTable dt = DbHelperSQL.Query("SELECT  * FROM V_HRM_EMP_MES where workcode='" + ((LoginUser)Session["LogUser"]).UserId + "'").Tables[0];
            string to_add = dt.Rows[0]["email"].ToString();
            to_add = to_add + ",yanlan.wu@pgi.cn"; //收件人:上传人,yanlan.wu@pgi.cn

            SmtpClient mail = new SmtpClient();
            mail.DeliveryMethod = SmtpDeliveryMethod.Network; //发送方式           
            mail.Host = "mail.pgi.cn"; //smtp服务器                                
            mail.Credentials = new System.Net.NetworkCredential("oa@pgi.cn", "pgi_1234");//用户名凭证  

            MailMessage message = new MailMessage();//邮件信息            
            message.From = new MailAddress("oa@pgi.cn");//发件人           
            message.To.Add(to_add); //收件人
            message.Bcc.Add("guiqin.he@pgi.cn,angela.xu@pgi.cn");

            message.Subject = "物流运费【请款通知】上传成功";//主题            
            message.Body = body;//内容           
            message.BodyEncoding = System.Text.Encoding.UTF8; //正文编码            
            message.IsBodyHtml = true;//设置为HTML格式            
            message.Priority = MailPriority.Normal;//优先级
            message.Attachments.Clear();

            Attachment attach = new Attachment(resultFilePath);
            message.Attachments.Add(attach);

            mail.Send(message);

            //临时表 导入正式表
            string sql_prd = @"delete WLYF_upload
                                from WLYF_upload_temp a
                                where WLYF_upload.status<>'已付款'
                                        and convert(datetime,WLYF_upload.fyrq)=convert(datetime,a.fyrq) and WLYF_upload.jzx=a.jzx and WLYF_upload.site=a.site 
		                                and WLYF_upload.hyd=a.hyd and WLYF_upload.gh=a.gh and WLYF_upload.shipto=a.shipto;                              
                                insert into WLYF_upload 
                                  (fyrq, jzx, site, hyd, gh, shipto, USD_fixed_amount, CNY_fixed_amount, USD_no_fixed_amount, CNY_no_fixed_amount, ori_filename, new_filename, uploadtime, CreateById, status, line)
                                 select fyrq, jzx, site, hyd, gh, shipto, USD_fixed_amount, CNY_fixed_amount, USD_no_fixed_amount, CNY_no_fixed_amount, ori_filename, new_filename, uploadtime, CreateById, status, line
                                 from WLYF_upload_temp";
            int j = DbHelperSQL.ExecuteSql(sql_prd);

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

    public void importdata(string fileName, string DesTableName, string new_filename, string ori_filename, out string result)
    {
        result = ""; 
        try
        {
            DataTable dtExcel = GetExcelData_Table(fileName, 0);
            if (dtExcel == null || dtExcel.Rows.Count <= 0 || dtExcel.Columns.Count != 10)
            {
                result = "No Data";
            }
            else
            {
                DataTable dt = new DataTable();
                DataColumn col_0 = new DataColumn("fyrq", typeof(string));
                DataColumn col_1 = new DataColumn("jzx", typeof(string));
                DataColumn col_2 = new DataColumn("site", typeof(string));
                DataColumn col_3 = new DataColumn("hyd", typeof(string));
                DataColumn col_4 = new DataColumn("gh", typeof(string));
                DataColumn col_5 = new DataColumn("shipto", typeof(string));
                DataColumn col_6 = new DataColumn("USD_fixed_amount", typeof(decimal));
                DataColumn col_7 = new DataColumn("CNY_fixed_amount", typeof(decimal));
                DataColumn col_8 = new DataColumn("USD_no_fixed_amount", typeof(decimal));
                DataColumn col_9 = new DataColumn("CNY_no_fixed_amount", typeof(decimal));
                DataColumn col_10 = new DataColumn("ori_filename", typeof(string));
                DataColumn col_11 = new DataColumn("new_filename", typeof(string));
                DataColumn col_12 = new DataColumn("uploadtime", typeof(DateTime));
                DataColumn col_13 = new DataColumn("CreateById", typeof(string));
                DataColumn col_14 = new DataColumn("status", typeof(string));
                DataColumn col_15 = new DataColumn("line", typeof(int));
                dt.Columns.Add(col_0); dt.Columns.Add(col_1); dt.Columns.Add(col_2); dt.Columns.Add(col_3); dt.Columns.Add(col_4); dt.Columns.Add(col_5);
                dt.Columns.Add(col_6); dt.Columns.Add(col_7); dt.Columns.Add(col_8); dt.Columns.Add(col_9); dt.Columns.Add(col_10);
                dt.Columns.Add(col_11); dt.Columns.Add(col_12); dt.Columns.Add(col_13); dt.Columns.Add(col_14); dt.Columns.Add(col_15);

                for (int k = 0; k < dtExcel.Rows.Count; k++)
                {
                    DataRow dr = dtExcel.Rows[k];

                    string fyrq = dr["发运日期"].ToString().Trim();
                    string jzx = dr["集装箱"].ToString().Trim();
                    string site = dr["地点"].ToString().Trim();
                    string hyd = dr["货运单"].ToString().Trim();
                    string gh = dr["柜号"].ToString().Trim();
                    string shipto = dr["ship_to"].ToString().Trim();
                    //decimal USD_fixed_amount = Convert.ToDecimal(dr["USD固定金额"].ToString().Trim() == "" ? "0" : dr["USD固定金额"].ToString().Trim());
                    //decimal CNY_fixed_amount = Convert.ToDecimal(dr["CNY固定金额"].ToString().Trim() == "" ? "0" : dr["CNY固定金额"].ToString().Trim());
                    //decimal USD_no_fixed_amount = Convert.ToDecimal(dr["USD不固定金额"].ToString().Trim() == "" ? "0" : dr["USD不固定金额"].ToString().Trim());
                    //decimal CNY_no_fixed_amount = Convert.ToDecimal(dr["CNY不固定金额"].ToString().Trim() == "" ? "0" : dr["CNY不固定金额"].ToString().Trim());

                    //过滤key值为空行
                    if (fyrq == "" || jzx == "" || site == "" || hyd == "" || gh == "" || shipto == "")
                    {
                        continue;
                    }
                    if (fyrq.IsDateTime()==false)
                    {
                        result = "第" + (k + 1).ToString() + "行【发运日期】不是日期格式yyyy/MM/dd！" + "<br />";
                        break;
                    }
                    if (dr["USD固定金额"].ToString().Trim() == "")
                    {
                        result = "第" + (k + 1).ToString() + "行【USD固定金额】不可为空！" + "<br />";
                        break;
                    }
                    if (dr["CNY固定金额"].ToString().Trim() == "")
                    {
                        result = "第" + (k + 1).ToString() + "行【CNY固定金额】不可为空！" + "<br />";
                        break;
                    }
                    if (dr["USD不固定金额"].ToString().Trim() == "")
                    {
                        result = "第" + (k + 1).ToString() + "行【USD不固定金额】不可为空！" + "<br />";
                        break;
                    }
                    if (dr["CNY不固定金额"].ToString().Trim() == "")
                    {
                        result = "第" + (k + 1).ToString() + "行【CNY不固定金额】不可为空！" + "<br />";
                        break;
                    }

                    decimal USD_fixed_amount = Convert.ToDecimal(dr["USD固定金额"].ToString().Trim());
                    decimal CNY_fixed_amount = Convert.ToDecimal(dr["CNY固定金额"].ToString().Trim());
                    decimal USD_no_fixed_amount = Convert.ToDecimal(dr["USD不固定金额"].ToString().Trim());
                    decimal CNY_no_fixed_amount = Convert.ToDecimal(dr["CNY不固定金额"].ToString().Trim());

                    DataRow dt_r = dt.NewRow();
                    dt_r["fyrq"] = fyrq;
                    dt_r["jzx"] = jzx;
                    dt_r["site"] = site;
                    dt_r["hyd"] = hyd;
                    dt_r["gh"] = gh;
                    dt_r["shipto"] = shipto;
                    dt_r["USD_fixed_amount"] = USD_fixed_amount;
                    dt_r["CNY_fixed_amount"] = CNY_fixed_amount;
                    dt_r["USD_no_fixed_amount"] = USD_no_fixed_amount;
                    dt_r["CNY_no_fixed_amount"] = CNY_no_fixed_amount;
                    dt_r["ori_filename"] = ori_filename;
                    dt_r["new_filename"] = new_filename;
                    dt_r["uploadtime"] = DateTime.Now;
                    dt_r["CreateById"] = ((LoginUser)Session["LogUser"]).UserId;
                    dt_r["status"] = "已请款";
                    dt_r["line"] = (k + 1);

                    dt.Rows.Add(dt_r);

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
        }

    }

    public DataTable GetExcelData_Table(string filePath, int sheetPoint)
    {
        Workbook book = new Workbook(filePath);
        Worksheet sheet = book.Worksheets[sheetPoint];
        Cells cells = sheet.Cells;
        book.CalculateFormula();
        DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);//获取excel中的数据保存到一个datatable中

        Row row = cells.GetRow(0);
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
                    bulkCopy.ColumnMappings.Add("fyrq", "fyrq");//映射字段名 DataTable列名 ,数据库 对应的列名   
                    bulkCopy.ColumnMappings.Add("jzx", "jzx");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("site", "site");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("hyd", "hyd");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("gh", "gh");
                    bulkCopy.ColumnMappings.Add("shipto", "shipto");
                    bulkCopy.ColumnMappings.Add("USD_fixed_amount", "USD_fixed_amount");
                    bulkCopy.ColumnMappings.Add("CNY_fixed_amount", "CNY_fixed_amount");
                    bulkCopy.ColumnMappings.Add("USD_no_fixed_amount", "USD_no_fixed_amount");
                    bulkCopy.ColumnMappings.Add("CNY_no_fixed_amount", "CNY_no_fixed_amount");
                    bulkCopy.ColumnMappings.Add("ori_filename", "ori_filename");
                    bulkCopy.ColumnMappings.Add("new_filename", "new_filename");
                    bulkCopy.ColumnMappings.Add("uploadtime", "uploadtime");
                    bulkCopy.ColumnMappings.Add("CreateById", "CreateById");
                    bulkCopy.ColumnMappings.Add("status", "status");
                    bulkCopy.ColumnMappings.Add("line", "line");

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
    

}