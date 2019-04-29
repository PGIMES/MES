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

public partial class CapacityPlan_select_plan_upload : System.Web.UI.Page
{
    //保存上传文件路径
    public static string savepath = @"UploadFile\Planning";


    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = null;
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        Session["LogUser"] = LogUserModel;
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + @"\" + resultFileName;
        if (!Directory.Exists(MapPath("~") + savepath + @"\"))
        {
            Directory.CreateDirectory(MapPath("~") + savepath + @"\");
        }
        e.UploadedFile.SaveAs(resultFilePath);

        string result = "", DesTableName = "Planning_base_upload_temp";        
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
            string sql = @"delete Planning_base_upload 
                        from  Planning_base_upload a 
	                        inner join  Planning_base_upload_temp b on a.domain=b.domain and a.dept=b.dept and a.years=b.years and a.weeks=b.weeks";
            DbHelperSQL.ExecuteSql(sql);
            sql = @"insert into Planning_base_upload select * from Planning_base_upload_temp";
            DbHelperSQL.ExecuteSql(sql);

        }
        catch (Exception ex)
        {
            result = "error";
        }

        e.CallbackData = result;
        return;


    }

    public string importdata(string fileName, string DesTableName)
    {
        try
        {
            DataTable dtExcel = GetExcelData_Table(fileName, 0);
            if (dtExcel == null || dtExcel.Rows.Count <= 0 || dtExcel.Columns.Count != 4)
            {
                return "No Data";
            }

            DataTable dt = new DataTable();
            DataColumn col_0 = new DataColumn("domain", typeof(string));
            DataColumn col_1 = new DataColumn("dept", typeof(string));
            DataColumn col_2 = new DataColumn("years", typeof(int));
            DataColumn col_3 = new DataColumn("weeks", typeof(string));
            DataColumn col_4 = new DataColumn("Qty", typeof(int));
            DataColumn col_5 = new DataColumn("CreateById", typeof(string));
            dt.Columns.Add(col_0); dt.Columns.Add(col_1); dt.Columns.Add(col_2); dt.Columns.Add(col_3); dt.Columns.Add(col_4); dt.Columns.Add(col_5);


            for (int k = 0; k < dtExcel.Rows.Count; k++)
            {
                DataRow dr = dtExcel.Rows[k];

                DataRow dt_r = dt.NewRow();
                dt_r["domain"] = dr["部门"].ToString() == "生产1部" ? "100" : "200";
                if (dr["部门"].ToString() == "生产1部") { dt_r["dept"] = "一车间"; }
                if (dr["部门"].ToString() == "生产2部") { dt_r["dept"] = "二车间"; }
                if (dr["部门"].ToString() == "生产3部") { dt_r["dept"] = "三车间"; }
                if (dr["部门"].ToString() == "生产4部") { dt_r["dept"] = "四车间"; }

                dt_r["years"] = dr["年份"].ToString();
                dt_r["weeks"] = dr["周数"].ToString();
                dt_r["Qty"] = dr["数量"].ToString().Replace(",", "").Trim() == "" ? "0" : dr["数量"].ToString().Replace(",", "").Trim();
                dt_r["CreateById"] = ((LoginUser)Session["LogUser"]).UserId;
                dt.Rows.Add(dt_r);

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
                    bulkCopy.ColumnMappings.Add("dept", "dept");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("years", "years");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("weeks", "weeks");//映射字段名 DataTable列名 ,数据库 对应的列名  
                    bulkCopy.ColumnMappings.Add("Qty", "Qty");
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
}