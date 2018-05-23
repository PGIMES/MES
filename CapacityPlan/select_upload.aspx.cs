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

public partial class CapacityPlan_select_upload : System.Web.UI.Page
{
    //保存上传文件路径
    public static string savepath = @"UploadFile\CapacityPlan";
    

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = null;
        if (Session["empid"] == null)
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }
        Session["LogUser"] = LogUserModel;
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        //List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];
        //attach_forms af = new attach_forms();

        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + @"\" + resultFileName;
        if (!Directory.Exists(MapPath("~") + savepath + @"\"))
        {
            Directory.CreateDirectory(MapPath("~") + savepath + @"\");
        }
        e.UploadedFile.SaveAs(resultFilePath);

        string result = "", DesTableName = "Base_CapacityPlan_Temp";        
        DbHelperSQL.ExecuteSql("truncate table " + DesTableName);
        result = importdata(resultFilePath, DesTableName);
        File.Delete(resultFilePath);

        if (result != "")
        {
            e.CallbackData = result;
            return;
        }

        try
        {
            string sql = @"delete Base_CapacityPlan 
                        from  Base_CapacityPlan a 
	                        left join  Base_CapacityPlan_Temp b on a.domain=b.domain and a.pgi_no=b.pgi_no and a.Capacity_Date=b.Capacity_Date";
            DbHelperSQL.ExecuteSql(sql);
            sql = @"insert into Base_CapacityPlan select * from Base_CapacityPlan_Temp";
            DbHelperSQL.ExecuteSql(sql);

        }
        catch (Exception ex)
        {
            result = "error";
        }

        e.CallbackData = result;
        return;

        //string name = e.UploadedFile.FileName;
        //long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        //string sizeText = sizeInKilobytes.ToString() + " KB";

        //af.rownum = List.Count + 1;
        //af.id = 0;
        //af.OriginalFile = name;
        //af.FilePath = "\\" + savepath + "\\" + resultFileName;
        //af.FileExtension = resultExtension;
        //af.CreateUser = ((LoginUser)Session["LogUser"]).UserId;
        //af.CreateDate = DateTime.Now;
        //af.flag = "add";


        //List.Add(af);
        //Session["attach_forms"] = List;

        //e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }

    public string importdata(string fileName, string DesTableName)
    {
        try
        {
            DataTable dtExcel = GetExcelData_Table(fileName, 0);
            if (dtExcel == null || dtExcel.Rows.Count <= 0 || dtExcel.Columns.Count != 15)
            {
                return "No Data";
            }

            DataTable dt = new DataTable();
            DataColumn col_0 = new DataColumn("domain", typeof(string));
            DataColumn col_1 = new DataColumn("pgi_no", typeof(string));
            DataColumn col_2 = new DataColumn("capacity_date", typeof(DateTime));
            DataColumn col_3 = new DataColumn("capacity_qty", typeof(Int32));
            DataColumn col_4 = new DataColumn("gzzx", typeof(string));
            DataColumn col_5 = new DataColumn("CreateById", typeof(string));
            dt.Columns.Add(col_0); dt.Columns.Add(col_1); dt.Columns.Add(col_2); dt.Columns.Add(col_3); dt.Columns.Add(col_4); dt.Columns.Add(col_5);


            for (int k = 0; k < dtExcel.Rows.Count; k++)
            {
                DataRow dr = dtExcel.Rows[k];
                for (int i = 1; i <= dtExcel.Columns.Count - 2; i++)
                {
                    DataRow dt_r = dt.NewRow();
                    dt_r["domain"] = ddl_comp.SelectedValue;
                    dt_r["pgi_no"] = dr["产品编码"].ToString();
                    dt_r["capacity_date"] = Convert.ToDateTime(dtExcel.Columns[i].ColumnName);
                    dt_r["capacity_qty"] = dr[i].ToString().Replace(",", "").Trim() == "" ? "0" : dr[i].ToString().Replace(",", "").Trim();
                    dt_r["gzzx"] = dr["工作中心"].ToString();
                    dt_r["CreateById"] = ((LoginUser)Session["LogUser"]).UserId;
                    dt.Rows.Add(dt_r);
                }
            }

            bool bf = DataTableToSQLServer(dt, DesTableName);
            return bf == true ? "" : "error";
        }
        catch (Exception ex)
        {

            return "error";
        }
        
    }

    public DataTable GetExcelData_Table(string filePath, int sheetPoint)
    {
        Workbook book = new Workbook(filePath);
        Worksheet sheet = book.Worksheets[sheetPoint];
        Cells cells = sheet.Cells;
        DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);//获取excel中的数据保存到一个datatable中

        Row row = cells.GetRow(0);
        for (int i = 0; i < cells.MaxDataColumn + 1; i++)
        {
            dt_Import.Columns[i].ColumnName = row.GetCellOrNull(i).Value.ToString();
        }
        return dt_Import;

    }
    public bool DataTableToSQLServer(DataTable dt,string DesTableName)
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
                    bulkCopy.ColumnMappings.Add("domain", "domain");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("pgi_no", "pgi_no");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("capacity_date", "Capacity_Date");
                    bulkCopy.ColumnMappings.Add("capacity_qty", "Capacity_Qty");
                    bulkCopy.ColumnMappings.Add("gzzx", "gzzx");
                    bulkCopy.ColumnMappings.Add("CreateById", "CreateById");

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


    //protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    QueryASPxGridView();
    //}
}