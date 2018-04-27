using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using BarCodeManagerLib;
using BarcodeLib;
using MESDataSetTableAdapters;
using System.IO;
public partial  class PrintDaimler_DomesticLabel : System.Web.UI.Page
{  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // ObjectDataSource1.SelectParameters[0].DefaultValue = Request["requestid"];
           
            MESDataSetTableAdapters.print_YJ_Daimler_DomesticLabelTableAdapter ta = new print_YJ_Daimler_DomesticLabelTableAdapter();
            MESDataSet.print_YJ_Daimler_DomesticLabelDataTable dt = new MESDataSet.print_YJ_Daimler_DomesticLabelDataTable();
            DataTable dt2 = (DataTable)dt;
            ta.Fill(dt, Request["requestid"]);
                                   
            foreach (MESDataSet.print_YJ_Daimler_DomesticLabelRow row in dt.Rows)
            {
                System.Drawing.Image image;
                int width = 250, height = 50;
                byte[] buffer = GetBarcode(height, width, BarcodeLib.TYPE.CODE128, row.Part_NO, out image);
                row.PartNoBarCode = buffer;
                
                //Package_Type
                width = 90; height = 25;
                buffer = GetBarcode(height, width, BarcodeLib.TYPE.CODE128, row.Package_Type, out image);
                row.PackageTypeBarCode = buffer;
            }
            Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetDaimler_DomesticLabel",dt2);
            ObjectDataSource1.DataObjectTypeName = "MESDataSetTableAdapters.print_YJ_Daimler_DomesticLabelTableAdapter";
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(rptDataSource);

        }
    }
    public   byte[] GetBarcode(int height, int width, BarcodeLib.TYPE type,string code, out System.Drawing.Image image)
    {
        image = null;
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        b.BackColor = System.Drawing.Color.White;
        b.ForeColor = System.Drawing.Color.Black;
        b.IncludeLabel = false;
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
        b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
        b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
        System.Drawing.Font font = new System.Drawing.Font("verdana", 10f);
        b.LabelFont = font;

        b.Height = height;
        b.Width = width;

        image = b.Encode(type, code);
       // b.SaveImage( Server.MapPath("~/abc.jpg"), SaveTypes.JPG);
        byte[] buffer = b.GetImageData(SaveTypes.JPG);
        return buffer;
    }
    
    protected void Page_Unload(object sender, EventArgs e)
    {
       // ReportDocument sReport = new ReportDocument();
     
    } 
}