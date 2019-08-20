using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web.ASPxTreeList;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public partial class Bom_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var dt = GetDataTable();
        QueryASPxGridView(dt);        
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        var dt = GetDataTable();
        QueryASPxGridView(dt);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        var file = MapPath("/") + "bom" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
        TableToExcel(GetDataTable(),file );
    }
    public DataTable GetDataTable()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(" select  d.aplno, id, (case when pid is null then d.pt_part+'【'+d.bomver+'】' else d.pt_part end) pt_part, d.pt_desc1, d.pt_desc2, drawno, pt.pt_net_wt, ps_qty_per, (case when isnull(unit,'')<>'' or pid is null then unit when  cast(ps_qty_per as float)  in(1.0,2.0,3.0,4.0) then 'EA' else 'KG' end)unit, material, vendor, ps_op, note, pid,d.domain,pt_status,p.product_user,p.bz_user from eng_bom_dtl d     ");
        //sb.Append(" left join form3_Sale_Product_MainTable p on left(d.pt_part,5)=p.pgino and d.pid is null ");
        //sb.Append(" left join qad_pt_mstr pt on d.pt_part=pt.pt_part and d.domain=pt.pt_domain ");//and d.pt_part not like 'R%'
        //sb.Append(" join  (select aplno,bomver from eng_bom_main_Form where (domain = '" + domain.SelectedValue + "' or '" + domain.SelectedValue + "' = '') and(pgino like '%" + pgino.Text.Trim() + "%' or '" + pgino.Text.Trim() + "' = ''))t on d.aplno = t.aplno order by d.pt_part ");

        sb.Append("exec Eng_Bom_Query '" + domain.SelectedValue + "','" + pgino.Text.Trim() + "','" + IsLatest.SelectedValue + "'");

        DataTable dt = DbHelperSQL.Query(sb.ToString()).Tables[0];
        return dt;
    }
    public void QueryASPxGridView(DataTable dt)
    {
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(" select  d.aplno, id, (case when pid is null then d.pt_part+'【'+d.bomver+'】' else d.pt_part end) pt_part, d.pt_desc1, d.pt_desc2, drawno, pt.pt_net_wt, ps_qty_per, (case when isnull(unit,'')<>'' or pid is null then unit when  cast(ps_qty_per as float)  in(1.0,2.0,3.0,4.0) then 'EA' else 'KG' end)unit, material, vendor, ps_op, note, pid,d.domain,pt_status,p.product_user,p.bz_user from eng_bom_dtl d     ");
        //sb.Append(" left join form3_Sale_Product_MainTable p on left(d.pt_part,5)=p.pgino and d.pid is null ");
        //sb.Append(" left join qad_pt_mstr pt on d.pt_part=pt.pt_part and d.domain=pt.pt_domain ");//and d.pt_part not like 'R%'
        //sb.Append(" join  (select aplno,bomver from eng_bom_main_Form where (domain = '"+domain.SelectedValue+"' or '"+ domain.SelectedValue + "' = '') and(pgino like '%"+pgino.Text.Trim()+"%' or '"+pgino.Text.Trim()+ "' = ''))t on d.aplno = t.aplno order by d.pt_part ");

        ////sb.Append("  select    d.aplno, id, (case when pid is null then d.pt_part+'【'+d.bomver+'】' else d.pt_part end) pt_part, pt_desc1, pt_desc2, drawno, pt_net_wt, ps_qty_per, unit, material, vendor, ps_op, note, pid from eng_bom_dtl d left join  ");
        ////sb.Append("  (select aplno,bomver from eng_bom_main_Form where (domain = '" + domain.SelectedValue + "' or '" + domain.SelectedValue + "' = '') and(pgino like '%" + pgino.Text.Trim() + "%' or '" + pgino.Text.Trim() + "' = '')) t on d.aplno = t.aplno order by d.pt_part");


        //DataTable dt = DbHelperSQL.Query(sb.ToString()).Tables[0];

        bomtree.DataSource = dt;
        bomtree.DataBind();

    }
   
    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        var dt = GetDataTable();
        QueryASPxGridView(dt);
      //  ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }
    protected void bomtree_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        TreeListNode node = this.bomtree.FindNodeByKeyValue(e.NodeKey.ToString());
        if (Object.Equals(e.GetValue("pid"), ""))
        {
            // e.Cell.Font.Bold = true;
            e.Cell.Text= "";
        }

    }


    protected void bomtree_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        TreeListNode node = this.bomtree.FindNodeByKeyValue(e.NodeKey.ToString());
        if (Object.Equals(node["pid"].ToString(), ""))
        {
            // e.Cell.Font.Bold = true;
            e.Row.Cells[1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=4A901BC7-EA83-43B1-80B6-5B14708DEDE9&instanceid="+ node["aplno"].ToString() + "&display=1' target='_blank'>" + node["pt_part"].ToString() + "</a>";
        }
        //e.Row.Cells[1].Text = "<a href=''>" + node["pt_part"].ToString() + "</a>";
    }

    public string getHeadName(string[] E,string[] C,string colname)
    {
        var strName = "";
        int index = E.ToList().IndexOf(colname);
        if (index >= 0)
        {
            if (C.Length >= index)
            {
                strName = C[index].ToString();
            }
        }
        return strName;
    }
    public void TableToExcel(DataTable dt, string file)
    {
        dt.Columns.Remove(dt.Columns["aplno"]);
        dt.Columns.Remove(dt.Columns["id"]);
        string[] headnameE = { "aplno", "id", "pt_part", "pt_desc1", "pt_desc2", "drawno", "pt_net_wt", "ps_qty_per", "unit", "material", "vendor", "ps_op", "note", "pid", "domain", "pt_status", "product_user", "bz_user" };
        string[] headnameC = { ""      , "" , "物料号" , "描述(零件号)", "描述2"  , "图纸", "单件重量", "单件用量"  , "单位",   "材料"  , "供应商", "消耗工序","备注", "pid" , "工厂" , "状态" , "产品负责人", "包装负责人" };

        IWorkbook workbook;
        string fileExt = Path.GetExtension(file).ToLower();
        if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
        if (workbook == null) { return; }
        ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

        //表头  
        IRow row = sheet.CreateRow(0);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ICell cell = row.CreateCell(i);
            cell.SetCellValue(getHeadName(headnameE,headnameC, dt.Columns[i].ColumnName));
            //cell.SetCellValue(dt.Columns[i].ColumnName);
        }

        //数据  
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row1 = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                cell.SetCellValue(dt.Rows[i][j].ToString());
            }
        }
        
        ////转为字节数组  
        //MemoryStream stream = new MemoryStream();
        //workbook.Write(stream);
        //var buf = stream.ToArray();

        ////保存为Excel文件  
        //using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        //{
        //    fs.Write(buf, 0, buf.Length);
        //    fs.Flush();
        //}

        var context = HttpContext.Current;
        context.Response.Clear();
        context.Response.ContentType = "application/vnd.ms-excel";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + "bom"+DateTime.Now.ToString("yyMMddHHmmss")+".xls");
        using (var ms = new MemoryStream())
        {
            workbook.Write(ms);

            long fileSize = ms.Length;
            //加上设置大小下载下来的.xlsx文件打开时才不会报“Excel 已完成文件级验证和修复。此工作簿的某些部分可能已被修复或丢弃”
            context.Response.AddHeader("Content-Length", fileSize.ToString());

            context.Response.BinaryWrite(ms.GetBuffer());
            context.ApplicationInstance.CompleteRequest();
        }

    }

    /// <summary>
    ///     将datatable数据写入excel并下载
    /// </summary>
    /// <param name="dt">datatable </param>
    /// <param name="excelName">文件名</param>
    /// <param name="templatePath">模板路径</param>
    /// <returns></returns>
    public static void DataTableToExcelAndDownload(DataTable dt, string excelName, string templatePath)
    {
        IWorkbook workbook = null;
        FileStream fs = null;
        IRow row = null;
        ISheet sheet = null;
        ICell cell = null;
        ICellStyle cellStyle = null;

        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                var rowCount = dt.Rows.Count; //行数
                var columnCount = dt.Columns.Count; //列数

                using (fs = File.OpenRead(templatePath))
                {
                    //大批量数据导出的时候，需要注意这样的一个问题，Excel2003格式一个sheet只支持65536行，excel 2007 就比较多，是1048576
                    //workbook = new HSSFWorkbook(fs);//2003版本.xls
                    workbook = new XSSFWorkbook(fs); // 2007版本.xlsx
                }

                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0); //读取第一个sheet

                    //设置每行每列的单元格,
                    for (var i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (var j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);
                            var value = dt.Rows[i][j];
                            var bdType = value.GetType().ToString();
                            switch (bdType)
                            {
                                case "System.String":
                                    cell.SetCellValue(value.ToString());
                                    break;
                                case "System.DateTime": //日期类型 
                                    cell.SetCellValue(
                                        Convert.ToDateTime(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                                    break;
                                case "System.Int16": //整型   
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    var intV = 0;
                                    int.TryParse(value.ToString(), out intV);
                                    cell.SetCellValue(intV);
                                    break;
                                case "System.Decimal": //浮点型   
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(value.ToString(), out doubV);//格式化值
                                    cellStyle = workbook.CreateCellStyle();
                                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                                    cell.SetCellValue(doubV);
                                    cell.CellStyle = cellStyle;
                                    break;
                                default:
                                    cell.SetCellValue(value.ToString());
                                    break;
                            }
                        }
                    }

                    var context = HttpContext.Current;
                    context.Response.Clear();
                    context.Response.ContentType = "application/vnd.ms-excel";
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + excelName);
                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);

                        long fileSize = ms.Length;
                        //加上设置大小下载下来的.xlsx文件打开时才不会报“Excel 已完成文件级验证和修复。此工作簿的某些部分可能已被修复或丢弃”
                        context.Response.AddHeader("Content-Length", fileSize.ToString());

                        context.Response.BinaryWrite(ms.GetBuffer());
                        context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (fs != null)
            {
                fs.Close();
            }
           // ExceptionHandling.ExceptionHandler.HandleException(ex);
        }
    }
}