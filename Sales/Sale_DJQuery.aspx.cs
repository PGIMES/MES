using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.Charts;
using System.Threading;

public partial class Sales_Sale_DJQuery : System.Web.UI.Page
{
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();
            string sql = " select distinct substring(pl_desc,dbo.fn_find('-',pl_desc,'2')+1,len(pl_desc)-dbo.fn_find('-',pl_desc,'2')+1)dl from qad.dbo.qad_pl_mstr pl where pl_prod_line like '3%'";
            DataSet product = DbHelperSQL.Query(sql);
            fun.initDropDownList(dropdl, product.Tables[0], "dl", "dl");
            this.dropdl.Items.Insert(0, new ListItem("全部", ""));


            DDL_type.DataSource = MaterialBase_CLASS.MES_PT_BASE(1, "刀具类", "非生产", "");
            DDL_type.DataValueField = "name2";
            DDL_type.DataTextField = "name";
            DDL_type.DataBind();
            this.DDL_type.Items.Insert(0, new ListItem("全部", ""));

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year);
            int chaYear = intYear - 2016+4;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            Yearlist[0]="近12日";
            Yearlist[1]="近12周";
            Yearlist[2]="近12月";
            for (int i = 3; i < chaYear; i++)
            {
                Yearlist[i] = (intYear-i+3).ToString();
            }
            txt_year.DataSource = Yearlist;
            txt_year.DataBind();
            txt_year.SelectedValue = Year;

            GetDetail();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
     
        GetDetail();
      
    }

    protected void GetDetail()
    {
        ChartA.Series.Clear();

        DataSet ds = DbHelperSQL.Query("exec MES_DJ_amt_ByPeriod  '" + txt_year.SelectedValue + "','" + ddl_comp.SelectedValue + "','" + DDL_type.SelectedValue + "','" + txt_gys.Text + "','" + dropdl.SelectedValue + "','" + drop_dept.SelectedValue + "'");
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ViewState["Detail"] = ds.Tables[0];
            //gv_month.DataSource = ds.Tables[0];
            //gv_month.DataBind();
            Pgi.Auto.Control.SetGrid(this.gv_month, ds.Tables[0],100);
           // Pgi.Auto.Control.SetGrid("DJMnth_QS", "", this.gv_month, ds.Tables[0]);
            this.gv_month.Columns[1].Caption=" ";
            this.gv_month.Columns[0].Visible = false;
            for (int i = 2; i < this.gv_month.DataColumns.Count ; i++)
            {

                this.gv_month.DataColumns[i].ToolTip = "判断规则:刀具占销售额比例为1.2-1.5%,单元格黄色;刀具占销售额比例大于1.5% 单元格红色";
              

            }
            CreateChart_month(ds.Tables[0]);
        }
      
    }

    private void CreateChart_month(DataTable dt)
    {
        #region Series
        //动态创建多个Series 图形的对象
        List<Series> list = new List<Series>();
        int j = 0;
        for (int i = 0; i <dt.Rows.Count-2; i++)
        {
            list.Add(CreateSeries(dt.Rows[i][1].ToString(), ViewType.Line, dt, j));
            j++;
        }
        #endregion
        this.ChartA.Series.AddRange(list.ToArray());
        this.ChartA.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
    }


    private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
    {
        Series series = new Series(caption, viewType);
        for (int i = 2; i < dt.Columns.Count - 2; i++)
        {
            int length = dt.Columns[i].ColumnName.IndexOf("<");
            string argument = length > 0 ?dt.Columns[i].ColumnName.Substring(0,length) : dt.Columns[i].ColumnName;
            //string argument = dt.Columns[i].ColumnName.Replace("<BR>","");//参数名称 
            decimal value = 0;
            if (dt.Rows[rowIndex][i].ToString() != null && dt.Rows[rowIndex][i].ToString() != "")
            {
                value = Convert.ToDecimal(dt.Rows[rowIndex][i].ToString());//参数值
            }
            series.Points.Add(new SeriesPoint(argument, value));
        }
        //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
        //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
        series.ArgumentScaleType = ScaleType.Qualitative;
        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
        return series;
    }

  
   
    protected void gv_month_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        

        

        if (e.VisibleIndex == 2)
        {

            for (int i = 2; i < this.gv_month.DataColumns.Count; i++)
            {
                if (e.GetValue(this.gv_month.DataColumns[i].FieldName) != System.DBNull.Value)
                {
                    if (Convert.ToDouble(e.GetValue(this.gv_month.DataColumns[i].FieldName)) > 1.2 && Convert.ToDouble(e.GetValue(this.gv_month.DataColumns[i].FieldName)) <= 1.5)
                    {
                        //  e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[i-1].Style.Add("background-color", "yellow");

                    }
                    else if (Convert.ToDouble(e.GetValue(this.gv_month.DataColumns[i].FieldName)) > 1.5)
                    {
                        // e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[i-1].Style.Add("background-color", "red");
                    }
                }
            }
           
        }
    }
    protected void gv_month_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        for (int i = 2; i < this.gv_month.DataColumns.Count; i++)
        {
            e.Row.Cells[i-1].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
        }
    }
}