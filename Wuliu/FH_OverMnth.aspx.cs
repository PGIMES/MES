using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Aspose.Cells;
using System.IO;
using System.Reflection;

public partial class Wuliu_FH_OverMnth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();
            string strSQL = @"SELECT DISTINCT idh_site FROM QAD.DBO.qad_idh_hist   join qad.dbo.qad_ih_hist on idh_nbr=ih_nbr   and idh_inv_nbr=ih_inv_nbr  and idh_domain=ih_domain  where   ih_cust not in ('11801','30025','19999') and ih_sched=1";
            DataTable ship_from = DbHelperSQL.Query(strSQL).Tables[0];


            fun.initDropDownList(this.txt_shipfrom, ship_from, "idh_site", "idh_site");
            txt_shipfrom.Items.Insert(0, new ListItem("", ""));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = GetDS();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    protected DataSet GetDS()
    {
        DataSet ds = DbHelperSQL.Query("exec Wuliu_OverMnth_Query_modify '" + ddl_comp.SelectedValue + "','" + txt_part.Text + "','" + txt_desc.Text + "','" + txt_shipfrom.SelectedValue + "','" + txt_shipto.Text + "' ");
        return ds;
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
          
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].BackColor = e.Row.Cells[9].Text.ToString() == "Yellow" ? System.Drawing.Color.Yellow : System.Drawing.Color.Red;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[4].Style.Add("text-align", "right");
            e.Row.Cells[5].Style.Add("text-align", "right");
            e.Row.Cells[7].Style.Add("text-align", "right");

        }
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = GetDS();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        DataSet ds = GetDS();
        DataView dv = ds.Tables[0].DefaultView;
       
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }

    //按模板格式导出
    protected void Button2_Click(object sender, EventArgs e)
    {

        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        string lsname = "超一个月无交货报表";
        DataSet ds = GetDS();
        DataTable dt = ds.Tables[0];
        dt.Columns.Remove("status");
        YangjianSQLHelp.DataTableToExcel(dt, "xls", lsname, "1");

       // DeleteTempFiles();//先删除临时文件
       //System.Data.DataTable   dt = GetDS().Tables[0];
       // Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
       // app.Visible = false;
       // app.UserControl = true;
       // Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
       // Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Add(Server.MapPath("超一个月无交货报表.xlsx")); //加载模板
       // Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
       // Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);
       
       // for (int i = 1; i <= dt.Rows.Count; i++)
       // {

       //     int row_ = 1 + i;  //Excel模板上表头和标题行占了2行,根据实际模板需要修改;
       //     int dt_row = i - 1; //dataTable的行是从0开始的。 
          
       //     worksheet.Cells[row_, 1] = dt.Rows[dt_row]["地点"].ToString();
       //     worksheet.Cells[row_, 2] = dt.Rows[dt_row]["物料号"].ToString();
       //     worksheet.Cells[row_, 3] = dt.Rows[dt_row]["描述"].ToString();
       //     worksheet.Cells[row_, 4] = dt.Rows[dt_row]["物料状态"].ToString();
       //     worksheet.Cells[row_, 5] = dt.Rows[dt_row]["现有库存"].ToString();
       //     worksheet.Cells[row_, 6] = dt.Rows[dt_row]["有多久未发货(月)"].ToString();
       //     worksheet.Cells[row_, 7] = dt.Rows[dt_row]["最后一次发货时间"].ToString();
       //     worksheet.Cells[row_, 8] = dt.Rows[dt_row]["最后一次发货数量"].ToString();
       //     worksheet.Cells[row_, 9] = dt.Rows[dt_row]["Ship_To"].ToString();
       // }

       
       // string despath = "\\wuliu\\TempFiles\\";
       // string savaPath = MapPath("~") + despath + "超一个月无交货报表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
       // workbook.SaveAs(savaPath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        
       // app.Quit();
       // GC.Collect();


       // FileInfo DownloadFile = new FileInfo(savaPath);
       // //以字符流的形式下载文件  
       // //FileStream fs = new FileStream(savaPath, FileMode.Open);
       // FileStream fs = new FileStream(savaPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

       // byte[] bytes = new byte[(int)fs.Length];
       // fs.Read(bytes, 0, bytes.Length);
       // fs.Close();
       // Response.ContentType = "application/octet-stream";
       // //通知浏览器下载文件而不是打开  
       // //Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(savaPath, System.Text.Encoding.UTF8));
       // Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode("超一个月无交货报表.xls"))); //定义输出文件和文件名 
       // Response.BinaryWrite(bytes);
       // Response.Flush();
       // Response.End();
      
        //File.Delete(savaPath);


      



                                       
        
    }


    protected void DeleteTempFiles()
    {
        string strFolderPath = Server.MapPath("~") + "\\wuliu\\TempFiles\\";

        DirectoryInfo dyInfo = new DirectoryInfo(strFolderPath);
        FileInfo[] rgFiles = dyInfo.GetFiles("*.xlsx");

        foreach (FileInfo fi in rgFiles)
        {
            fi.Delete();
        }
    } 

}