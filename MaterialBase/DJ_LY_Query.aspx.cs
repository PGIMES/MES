using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using DevExpress.Web;
using System.Drawing;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;

public partial class MaterialBase_DJ_LY_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   //初始化日期
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
          
             txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
             QueryASPxGridView();
        }
        if (this.GV_PART.IsCallback)
        {
            QueryASPxGridView();
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        //DataTable dt =Query("exec DJ_LY_Query  '"+ddl_comp.SelectedValue+"','"+txtDateFrom.Text+"','"+txtDateTo.Text+"'").Tables[0];
        //Pgi.Auto.Control.SetGrid("DJLY", "", this.GV_PART, dt);
    }


    public void QueryASPxGridView()
    {
       //
        DataTable dt = DbHelperSQL.Query("exec DJ_LY_Querx  '" + ddl_comp.SelectedValue + "','" + txtDateFrom.Text + "','" + txtDateTo.Text + "','"+txtwlh.Text+ "','" + ddl_dept.SelectedValue + "'").Tables[0];
        Pgi.Auto.Control.SetGrid("DJLY", "", this.GV_PART, dt);
        

        //this.GV_PART.Settings.ShowFooter = true;
        //this.GV_PART.TotalSummary.Clear();
        //DevExpress.Web.ASPxSummaryItem i = new ASPxSummaryItem();
        //i.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        //i.FieldName = "sjxds";
        //i.DisplayFormat = "{0:N0}";
        //this.GV_PART.TotalSummary.Add(i);

        //DevExpress.Web.ASPxSummaryItem money = new ASPxSummaryItem();
        //money.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        //money.FieldName = "amt";
        ////  money.DisplayFormat = "RIGHT";
        //money.DisplayFormat = "{0:N0}";
       

       // this.GV_PART.TotalSummary.Add(money);
       

    }
 

    protected void GV_PART_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "序号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
        

             
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType != GridViewRowType.Data) return;

        double qty_week = Convert.ToDouble(e.GetValue("quantity_week").ToString());
        double qty_tweek = Convert.ToDouble(e.GetValue("quantity_tweek").ToString());
        double qty_fweek = Convert.ToDouble(e.GetValue("quantity_fweek").ToString());
        double sl = Convert.ToDouble(e.GetValue("sjxds").ToString());
        double kcs = Convert.ToDouble(e.GetValue("kcs").ToString());
       // double qty_fweeks = Convert.ToDouble(e.GetValue("lycs_fweek").ToString());
        double avg_week = Convert.ToDouble(e.GetValue("quantity_year").ToString()) / 52;

        if (qty_week > 1 && qty_week > avg_week)
        {
            e.Row.Cells[14].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_week > avg_week * 2)
        {
            e.Row.Cells[14].Style.Add("background-color", "red");
        }
        //5周
        if (qty_week > 1 && qty_fweek > avg_week)
        {
            e.Row.Cells[15].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_fweek > avg_week * 2)
        {
            e.Row.Cells[15].Style.Add("background-color", "red");
        }

        //12周
        if (qty_week > 1 && qty_tweek > avg_week)
        {
            e.Row.Cells[16].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_tweek > avg_week * 2)
        {
            e.Row.Cells[16].Style.Add("background-color", "red");
        }

        //库存数比较
        if (kcs < qty_week)
        {
            e.Row.Cells[20].Style.Add("background-color", "red");
        }
        if (kcs > qty_fweek * 5)
        {
            e.Row.Cells[20].Style.Add("background-color", "yellow");
        }
      
    }
    protected void GV_PART_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string wlh = (string)e.GetValue("tr_part");
            //string ljh = txtljh.Value;
            string site = ddl_comp.SelectedValue;
            wlh = wlh.Replace("X", "");
            if (this.GV_PART.DataColumns["yycps"] != null)
            {
                int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).VisibleIndex;
                int value = Convert.ToInt16(e.GetValue("yycps"));
                if (index==e.Row.Cells.Count-1)
                {
                    e.Row.Cells[index].Text = "<a href='Forproducts.aspx?wlh=" + wlh + "&site=" + site + "' target='_blank'>" + value.ToString() + "</a>";
                }
                    

              
             
                //  ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).PropertiesHyperLinkEdit.NavigateUrlFormatString = "Forproducts.aspx?wlh=" + e.GetValue("wlh") + "&ljh=" + ljh + "&site=" + site + "";
            }
        }
    }
}