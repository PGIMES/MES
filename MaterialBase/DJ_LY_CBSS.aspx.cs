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

public partial class MaterialBase_DJ_LY_CBSS : System.Web.UI.Page
{

    decimal xmdlys = 0;
    decimal xdlys = 0;
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_ddl();
            BaseFun fun = new BaseFun();
            string sql = " select distinct substring(pl_desc,dbo.fn_find('-',pl_desc,'2')+1,len(pl_desc)-dbo.fn_find('-',pl_desc,'2')+1)dl from qad.dbo.qad_pl_mstr pl where pl_prod_line like '3%'";
            DataSet product = DbHelperSQL.Query(sql);
            fun.initDropDownList(dropdl, product.Tables[0], "dl", "dl");
            this.dropdl.Items.Insert(0, new ListItem("全部", ""));
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
        DataTable dt = DbHelperSQL.Query("exec Top_CBSS_query  '" + ddl_comp.SelectedValue + "','" + dropdl.SelectedValue + "','" + DDL_type.SelectedValue + "','" + dropnum.SelectedValue + "'").Tables[0];
        ViewState["Detail"] = dt;
        Pgi.Auto.Control.SetGrid("DJTOP", "", this.GV_PART, dt);
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

    public void Get_ddl()
    {

        DDL_type.DataSource = MaterialBase_CLASS.MES_PT_BASE(1, "刀具类", "非生产", "");
        DDL_type.DataValueField = "name2";
        DDL_type.DataTextField = "name";
        DDL_type.DataBind();
        this.DDL_type.Items.Insert(0, new ListItem("全部", ""));

        Get_style();
    }
    public void Get_style()
    {

    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType != GridViewRowType.Data) return;

        double qty_week = Convert.ToDouble(e.GetValue("qty_week").ToString());
        double qty_tweek = Convert.ToDouble(e.GetValue("qty_tweek").ToString());
        double qty_fweek = Convert.ToDouble(e.GetValue("qty_fweek").ToString());
        string status = e.GetValue("xyph").ToString();
        double avg_week = Convert.ToDouble(e.GetValue("qty_avgyear").ToString());
        double sjcb = string.IsNullOrEmpty(e.GetValue("sjdwcb").ToString()) ? 0 : Convert.ToDouble(e.GetValue("sjdwcb").ToString());
        double edcb = string.IsNullOrEmpty(e.GetValue("eddwcb").ToString()) ? 0 : Convert.ToDouble(e.GetValue("eddwcb").ToString());
        double sjsm = string.IsNullOrEmpty(e.GetValue("edsm_sj").ToString()) ? 0 : Convert.ToDouble(e.GetValue("edsm_sj").ToString());  
        double edsm = string.IsNullOrEmpty(e.GetValue("edsm").ToString()) ? 0 : Convert.ToDouble(e.GetValue("edsm").ToString());
        double edxmcs = string.IsNullOrEmpty(e.GetValue("edxmcs").ToString()) ? 0 : Convert.ToDouble(e.GetValue("edxmcs").ToString());
        double sjxmcs = Convert.ToDouble(e.GetValue("sjxmcs").ToString());
        double smtzxs = string.IsNullOrEmpty(e.GetValue("smtzxs").ToString()) ? 0 : Convert.ToDouble(e.GetValue("smtzxs").ToString());

        if (qty_week > 1 && qty_week > avg_week)
        {
            e.Row.Cells[6].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_week > avg_week * 2)
        {
            e.Row.Cells[6].Style.Add("background-color", "red");
        }
        //5周
        if (qty_week > 1 && qty_fweek > avg_week)
        {
            e.Row.Cells[7].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_fweek > avg_week * 2)
        {
            e.Row.Cells[7].Style.Add("background-color", "red");
        }

        //12周
        if (qty_week > 1 && qty_tweek > avg_week)
        {
            e.Row.Cells[8].Style.Add("background-color", "yellow");
        }
        if (qty_week > 1 && qty_tweek > avg_week * 2)
        {
            e.Row.Cells[8].Style.Add("background-color", "red");
        }
        if (status == "停用")
        {
            e.Row.Style.Add("background-color", "gray");
        }
        if (sjcb > edcb)
        {
            e.Row.Cells[17].Style.Add("background-color", "yellow");
        }
        if (sjcb > edcb * 1.3)
        {
            e.Row.Cells[17].Style.Add("background-color", "red");
        }
        //实际寿命
        if (sjsm / edsm < 0.5)
        {
            e.Row.Cells[19].Style.Add("background-color", "red");
        }
        else if (sjsm / edsm >= 0.5 && sjsm / edsm <= 1)
        {
            e.Row.Cells[19].Style.Add("background-color", "yellow");
        }
        //修模次数比较
        if (sjxmcs / edxmcs < 0.5)
        {
            e.Row.Cells[13].Style.Add("background-color", "red");
        }
        else if (sjxmcs / edxmcs >= 0.5 && sjxmcs / edxmcs <= 1)
        {
            e.Row.Cells[13].Style.Add("background-color", "yellow");
        }
        //寿命调整系数
        
        if (smtzxs > 1.5)
        {
            e.Row.Cells[21].Style.Add("background-color", "red");
        }
        else if (smtzxs >0 && smtzxs < 0.8)
        {
            e.Row.Cells[21].Style.Add("background-color", "yellow");
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
                //if (index == e.Row.Cells.Count - 4)
                //{
                    e.Row.Cells[index].Text = "<a href='Forproducts.aspx?wlh=" + wlh + "&site=" + site + "' target='_blank'>" + value.ToString() + "</a>";
                //}

            }
        }

        if (e.RowType == DevExpress.Web.GridViewRowType.Footer)
        {
            xmdlys = 0;
            xdlys = 0;

            DataTable dt = (DataTable)ViewState["Detail"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["xmdlycs"].ToString() != "")
                {
                    this.xmdlys = this.xmdlys + Convert.ToDecimal(dt.Rows[i]["xmdlycs"].ToString());
                }
                if (dt.Rows[i]["xdlycs"].ToString() != "")
                {
                    this.xdlys = this.xdlys + Convert.ToDecimal(dt.Rows[i]["xdlycs"].ToString());
                }


            }
            e.Row.Cells[13].Text =xdlys==0?"0": Convert.ToString(Math.Round(xmdlys / xdlys, 1)); 



        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    
}