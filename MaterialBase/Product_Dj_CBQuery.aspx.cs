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

public partial class MaterialBase_Product_Dj_CBQuery : System.Web.UI.Page
{
    decimal dj = 0;
    decimal eddjcb = 0;
    Product_CLASS Product_CLASS = new Product_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Get_ddl();
            BaseFun fun = new BaseFun();
            string sql = " select distinct substring(pl_desc,dbo.fn_find('-',pl_desc,'2')+1,len(pl_desc)-dbo.fn_find('-',pl_desc,'2')+1)dl from qad.dbo.qad_pl_mstr pl where pl_prod_line like '3%'";
            DataSet product = DbHelperSQL.Query(sql);
            fun.initDropDownList(dropdl, product.Tables[0], "dl", "dl");
            this.dropdl.Items.Insert(0, new ListItem("全部", ""));
          

            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 1;
            int chaYear = intYear - 2016;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (2016 + i).ToString();
            }
            drop_year.DataSource = Yearlist;
            drop_year.DataBind();
            drop_year.SelectedValue = Year;

            QueryASPxGridView();
        }
        if (this.GV_PART.IsCallback)
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        //
        
        DataTable dt = DbHelperSQL.Query("exec MES_GetDJCB_Byproduct_add '" + drop_year.SelectedValue + "' , '" + ddl_comp.SelectedValue + "','" + dropdl.SelectedValue + "','" + dropnum.SelectedValue + "'").Tables[0];
        ViewState["Detail"] = dt;
        Pgi.Auto.Control.SetGrid("DJCB", "", this.GV_PART, dt);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            int requestid = (int)e.GetValue("requestId");

           
            if (this.GV_PART.DataColumns["pgino"] != null)
            {
            
                string value = Convert.ToString(e.GetValue("pgino"));
                e.Row.Cells[1].Text = "<a href='http://172.16.5.26:8010/product/product.aspx?requestid=" + e.GetValue("requestId") + "' target='_blank'>" + value.ToString() + "</a>";
              
            }
        }
        if (e.RowType == DevExpress.Web.GridViewRowType.Footer)
        {
            dj = 0;
            eddjcb = 0;

            DataTable dt = (DataTable)ViewState["Detail"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["pc_dj_QAD"].ToString() != "")
                {
                    this.dj = this.dj + Convert.ToDecimal(dt.Rows[i]["pc_dj_QAD"].ToString());
                }
                if (dt.Rows[i]["eddjcb"].ToString() != "")
                {
                    this.eddjcb = this.eddjcb + Convert.ToDecimal(dt.Rows[i]["eddjcb"].ToString());
                }

            }
           // e.Row.Cells[11].Text = Convert.ToString(Math.Round(eddjcb / dj, 5));
           if(dj!=0)
           {
            e.Row.Cells[11].Text = string.Format("{0:P2}", Convert.ToDecimal(eddjcb / dj));
            }


        }
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        //销售单价比
        if (e.RowType != GridViewRowType.Data) return;
     
            string c = (string)e.GetValue("color");

            if (c == "red")

                e.Row.Cells[11].Style.Add("background-color", "red");
            if (c == "Yellow")

                e.Row.Cells[11].Style.Add("background-color", "yellow");


        //额定年成本损失颜色
            string d = (string)e.GetValue("edcolor");

            if (d == "red")

                e.Row.Cells[17].Style.Add("background-color", "red");
            if (d == "Yellow")

                e.Row.Cells[17].Style.Add("background-color", "yellow");

        //实际单件成本颜色
            string f= (string)e.GetValue("edcolor");

            if (f == "red")

                e.Row.Cells[12].Style.Add("background-color", "red");
            if (f == "Yellow")

                e.Row.Cells[12].Style.Add("background-color", "yellow");
            string status = (string)e.GetValue("product_status");
        //产品状态
            if (status == "停产")
            {
                e.Row.Style.Add("background-color", "gray");
            }
        
    }
}