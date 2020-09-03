using Aspose.Cells;
using Maticsoft.DBUtility;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Forms_PurChase_PUR_PR_Upload_dj_x : System.Web.UI.Page
{
    //保存上传文件路径
    public static string savepath = @"UploadFile\Purchase\PR_DJ_X_UploadFile";

    protected void Page_Load(object sender, EventArgs e)
    {
        //lbl_domain.Text = Request.QueryString["domain"].ToString();
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

        string result = "", DesTableName = "PUR_PR_Dtl_Upload_dj_x_temp"; 
        DbHelperSQL.ExecuteSql("truncate table " + DesTableName);
        importdata(resultFilePath, DesTableName, resultFileName, e.UploadedFile.FileName, out result);

        if (result != "")
        {
            File.Delete(resultFilePath);
            Session["pr_dj_x_im"] = null;
            e.CallbackData = isSubmissionExpired + "|" + result;
            return;
        }

        try
        {
            string sql = @"exec [PUR_PR_Dtl_Upload_dj_x_import] '" + Request.QueryString["domain"].ToString() + "'";
            DataSet ds = DbHelperSQL.Query(sql);

            DataTable dt_flag = ds.Tables[0];//标记 成功还是失败
            DataTable ldt= ds.Tables[1];//失败的信息 或者是 成功的信息

            if (dt_flag.Rows[0][0].ToString() == "Y")
            {
                for (int i = 0; i < ldt.Rows.Count; i++)
                {
                    result = result + "【物料号】" + ldt.Rows[i]["wlh"].ToString() + " error:" + ldt.Rows[i]["errorMessage"].ToString() + "<br />";
                }
                Session["pr_dj_x_im"] = null;
                e.CallbackData = isSubmissionExpired + "|" + result;
                return;
            }
            Session["pr_dj_x_im"] = ldt;
        }
        catch (Exception ex)
        {
            result = ex.Message;
            Session["pr_dj_x_im"] = null;
            e.CallbackData = isSubmissionExpired + "|" + result;
            return;
        }

        isSubmissionExpired = "N";
        e.CallbackData = isSubmissionExpired + "|" + result;
        return;


    }

    public void importdata(string fileName, string DesTableName, string new_filename, string ori_filename, out string result)
    {
        result = ""; //ih_bill_name = ""; sumamount = 0;
        try
        {
            DataTable dtExcel = GetExcelData_Table(fileName, 0);
            if (dtExcel == null || dtExcel.Rows.Count <= 0 || dtExcel.Columns.Count != 5)
            {
                result = "No Data";
            }
            else
            {
                DataTable dt = new DataTable();
                DataColumn col_0 = new DataColumn("rowid", typeof(int));
                DataColumn col_1 = new DataColumn("wlh", typeof(string));
                DataColumn col_2 = new DataColumn("qty", typeof(int));
                DataColumn col_3 = new DataColumn("deliverydate", typeof(string));
                DataColumn col_4 = new DataColumn("recmdvendorid", typeof(string));
                DataColumn col_5 = new DataColumn("note", typeof(string));
                DataColumn col_6 = new DataColumn("ori_filename", typeof(string));
                DataColumn col_7 = new DataColumn("new_filename", typeof(string));
                
                dt.Columns.Add(col_0); dt.Columns.Add(col_1); dt.Columns.Add(col_2); dt.Columns.Add(col_3); 
                dt.Columns.Add(col_4); dt.Columns.Add(col_5); dt.Columns.Add(col_6); dt.Columns.Add(col_7);

                string yy_l = DateTime.Now.Year.ToString().Left(2);

                for (int k = 0; k < dtExcel.Rows.Count; k++)
                {
                    DataRow dr = dtExcel.Rows[k];

                    string wlh = dr["物料号"].ToString().Trim();
                    string qty = dr["数量"].ToString().Trim();
                    string deliverydate = dr["要求到货日期"].ToString().Trim();
                    string recmdvendorid = dr["推荐供应商"].ToString().Trim();
                    string note = dr["说明"].ToString().Trim();

                    //过滤key值为空行
                    if (wlh == "") { continue; }
                    else if (wlh.Right(1).ToUpper() != "X" && wlh.Right(1).ToUpper() != "H")
                    {
                        result = "【物料号】必须为修磨刀/换刀！" + "<br />";
                        break;
                    }

                    //验证数量
                    if (qty == "")
                    {
                        result = "物料号" + wlh + " 【数量】不可为空！" + "<br />";
                        break;
                    }
                    else if (qty.IsInt() == false)
                    {
                        result = "物料号" + wlh + " 【数量】必须为整数！" + "<br />";
                        break;
                    }
                    else if (Convert.ToInt32(qty) <=0)
                    {
                        result = "物料号" + wlh + " 【数量】必须为正整数！" + "<br />";
                        break;
                    }

                    //要求到货日期
                    if (deliverydate == "")
                    {
                        result = "物料号" + wlh + " 【要求到货日期】不可为空！" + "<br />";
                        break;
                    }
                    else if ((yy_l + deliverydate.Left(2) + "-" + deliverydate.Substring(2, 2) + "-" + deliverydate.Right(2)).IsDateTime() == false)
                    {
                        result = "物料号" + wlh + " 【要求到货日期】必须为yyMMdd！" + "<br />";
                        break;
                    }

                    //推荐供应商
                    if (recmdvendorid == "")
                    {
                        result = "物料号" + wlh + " 【推荐供应商】不可为空！" + "<br />";
                        break;
                    }

                    DataRow dt_r = dt.NewRow();
                    dt_r["rowid"] = dt.Rows.Count + 1;
                    dt_r["wlh"] = wlh;
                    dt_r["qty"] = qty;
                    dt_r["deliverydate"] = deliverydate;
                    dt_r["recmdvendorid"] = recmdvendorid;
                    dt_r["note"] = note;
                    dt_r["ori_filename"] = ori_filename;
                    dt_r["new_filename"] = new_filename;
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
            //sumamount = 0;
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
                    bulkCopy.ColumnMappings.Add("rowid", "rowid");//映射字段名 DataTable列名 ,数据库 对应的列名   
                    bulkCopy.ColumnMappings.Add("wlh", "wlh");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("qty", "qty");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("deliverydate", "deliverydate");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("recmdvendorid", "recmdvendorid");
                    bulkCopy.ColumnMappings.Add("note", "note");
                    bulkCopy.ColumnMappings.Add("ori_filename", "ori_filename");
                    bulkCopy.ColumnMappings.Add("new_filename", "new_filename");

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