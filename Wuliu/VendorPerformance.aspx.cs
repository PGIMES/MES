using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class VendorPerformance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["prems"] = "";
            int year = DateTime.Now.Year;
            for(int i = year; i > year -5; i--)
            {
                dropYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });                
            }

        }
    }
    protected DataSet GetDS()
    {
        DataSet ds = DbHelperSQL.Query("exec QAD.dbo.SUP_VendorPerformance '"+dropcomp.SelectedValue+"','"+txtNbr.Text.Replace("'","").Trim()+"','"+txtCharger.Text.Trim().Replace("'","")+"','"+weeks.Text.Trim()+"','"+dropYear.SelectedValue.ToString()+"'");
        return ds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = GetDS();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = GetDS();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        //int[] arr = { 0, 1, 2 };
        //MergGridRow.MergeRow(GridView1,arr);
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    string lsname = "发运记录";
    //    DataSet ds = GetDS();
    //    DataTableToExcel(ds.Tables[0], "xls", lsname, "1");

    //}

    public void DataTableToExcel(DataTable dt, string FileType, string FileName, string b_head)
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Charset = "UTF-8";
        System.Web.HttpContext.Current.Response.Buffer = true;
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
        System.Web.HttpContext.Current.Response.ContentType = FileType;
        string colHeaders = string.Empty;



        string ls_item = string.Empty;
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        if (b_head == "1")
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ls_item += dt.Columns[j].ColumnName + "\t";
            }
        }
        ls_item += "\n";
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    ls_item += row[i].ToString() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString() + "\t";
                }
            }
            System.Web.HttpContext.Current.Response.Output.Write(ls_item);
            ls_item = string.Empty;
        }

        System.Web.HttpContext.Current.Response.Output.Flush();
        System.Web.HttpContext.Current.Response.End();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            var strMS = row["供应商"].ToString();
            if (strMS != ViewState["prems"].ToString())
            {
                e.Row.Style.Add("border-top","2px solid ");
                ViewState["prems"] = strMS ;
            }             
            //序号为2的给个底色
            if (row["No"].ToString()=="2")
            {
                e.Row.Style.Add("background-color", "lightcyan");     
            }
            //序号为3 欠交的给加粗
            if (row["No"].ToString() == "3")
            {
                e.Row.Style.Add("font-weight", "bolder");

            }
            //合并单元格
            if (row["No"].ToString() == "1")
            {
                e.Row.Cells[0].RowSpan = 3;
                e.Row.Cells[1].RowSpan = 3;
                e.Row.Cells[2].RowSpan = 3;
            }
            else
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            //格式化 从第六列数据开始
            for(int i=6;i<e.Row.Cells.Count;i++)
            {
                var value = Server.HtmlDecode(e.Row.Cells[i].Text).Trim();
                e.Row.Cells[i].Text =value=="-1"?"NA": String.Format("{0:N0}", Convert.ToSingle(value==""?"0": value));
                //删除最后一周超欠交数据
                if (i == e.Row.Cells.Count-1 && (row["No"].ToString()== "3"|| row["No"].ToString()=="2"))
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text == "0" ? "": e.Row.Cells[i].Text;
                }
            }
            // 添加链接
            if (row["No"].ToString() == "1")
            {
                //添加可点击Link 及前端识别属性：name
                for (int i = 6; i < e.Row.Cells.Count ; i++)
                {

                    if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
                    {
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtnA1" + e.Row.Cells[i - 1].Text;
                        lbtn.Text = e.Row.Cells[i].Text;
                        lbtn.Attributes.Add("href", @"javascript:void(0)','')");

                        var vendor = e.Row.Cells[1].Text.Trim();
                        vendor = vendor.Substring(vendor.IndexOf(".") + 1);
                        lbtn.Attributes.Add("name", "vendor");
                        lbtn.Attributes.Add("vendor", vendor);//vendor
                        lbtn.Attributes.Add("week", GridView1.HeaderRow.Cells[i].Text);//week
                        
                        //lbtn.Attributes.Add("type", "A" + e.Row.Cells[0].Text); //1:报价次数-- 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
                        e.Row.Cells[i].Controls.Add(lbtn);
                        e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                    }

                }
            }


            e.Row.HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;           
           // e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left; 3,4,列隐藏就不设定了
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
        }
        //隐藏序号列
        if (e.Row.RowType != DataControlRowType.Pager&&e.Row.RowType!=DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[3].Visible = false;//HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].Visible = false;
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        
    }
}