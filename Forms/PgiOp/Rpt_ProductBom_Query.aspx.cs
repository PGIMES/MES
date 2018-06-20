using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class Product_Rpt_ProductBom_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BaseFun fun = new BaseFun();
            string strSQL = @"select a.pt_part+'/'+b.productcode productcode from qad_pt_mstr a join [form3_Sale_Product_DetailTable] b on left(a.pt_part,6)=b.pgino+b.version where  pt_prod_line like '3%' and pt_status<>'Dead' order by pt_part";
            DataTable dtPgino = DbHelperSQL.Query(strSQL).Tables[0];

            if (dtPgino.Rows.Count > 0)
            {
                fun.initDropDownList(this.ddl_ljh, dtPgino, "productcode", "productcode");
            }
            ddl_ljh.Items.Insert(0, new ListItem("", ""));

            ddl_ljh.SelectedValue = Request["pgino_pn"].ToString();
            ddl_comp.SelectedValue= Request["domain"].ToString();

            Button1_Click(sender, e);

            hideTab();
        }
      
        if (Session["dt"]!= null)
        {
            this.treeList1.DataSource = Session["dt"];
            treeList1.KeyFieldName = "pt_part";
            treeList1.ParentFieldName = "PID";
            treeList1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        hideTab();
        Session["dt"] = null;
        var pgino = ddl_ljh.Text.Substring(0, 7);
        var domain = ddl_comp.SelectedValue;
        //初始化产品信息
        var sqlProdinf = @"select a.pgino, b.productcode,productname, pc_date,end_date,end_customer_name,customer_project,right(product_img,len(product_img)-2) product_img,(select qty_year from  [dbo].[V_form3_SUM_Quantity_MAX] where pgino=a.pgino )qty_year 
                        from [dbo].[form3_Sale_Product_MainTable] a 
                            inner join[dbo].[form3_Sale_Product_DetailTable] b on a.pgino = b.pgino
                        where b.pgino+b.version = '{0}'  ";//where ro_routing like  
       
        DataTable dtProdinf = DbHelperSQL.Query(string.Format(sqlProdinf, pgino.Substring(0, 6))).Tables[0];
        var strpgiver = ddl_ljh.Text.Substring(0, 7);
        if (dtProdinf.Rows.Count > 0)
        {
            DataRow dr = dtProdinf.Rows[0];
            txt_pgino.Value = dr["pgino"].ToString();
            txt_productcode.Value = dr["productcode"].ToString();
            txt_productname.Value = dr["productname"].ToString();
            txt_EndCustName.Value = dr["end_customer_name"].ToString();
            txt_CustProject.Value = dr["customer_project"].ToString();
            txt_PCDate.Value =Convert.ToDateTime( dr["pc_date"].ToString()).ToString("yyyy-MM-dd");
            txt_EndDate.Value = Convert.ToDateTime(dr["end_date"].ToString()).ToString("yyyy-MM-dd");
            txt_MaxYearuseCounts.Value = dr["qty_year"].ToString();
            this.Image2.ImageUrl = dr["product_img"].ToString();
            this.Image2.Visible = true;

        }
        //显示BOM列表
        var sqlBOM = "SELECT [ps_par] ,[ps_comp],convert(int,cast([ps_qty_per] as float) ) [ps_qty_per]  ,[ps_op],b.pt_desc1 物料描述,b.pt_net_wt 重量,pt_ord_mult 最小包装量  FROM [172.16.5.6].[Report].[dbo].[qad_ps_mstr] a join [dbo].[qad_pt_mstr] b on a.ps_comp=b.pt_part  where ps_par='{0}' ";

        DataTable dtBOM = DbHelperSQL.Query(string.Format(sqlBOM, strpgiver)).Tables[0];

        BindTreeData(pgino);
        var sqlOP = "  select ro_op 工序号,ro_desc 工序名称,cast([ro_men_mch] as float(2)) 单台需要人数,cast(ro_setup_men as float(2)) 本工序1人操作台数,ro_mch_op 总机台数,"
                        + "cast(ro_run as decimal(6,5)) [单台单件工时(时)], cast(cast(ro_run as float)*3600 as int) [单台单件工时(秒)], "
                        + "iif(ro_run=0 or ro_mch_op=0,0,cast((cast(12 as float)/ro_run)*0.83 as int)) [单台班产量(83%)],iif(ro_run=0 or ro_mch_op=0,0,cast((cast(12 as float)/ro_run)*ro_setup_men*0.83 as int)) [1人班产量(83%)],"
                        + "iif(ro_run=0 or ro_mch_op=0,0,cast((cast(12 as float)/ro_run)*ro_mch_op*0.83 as int))  [整线班产量(83%)]"//,iif(ro_run=0 or ro_mch_op=0,0,cast( (cast(12 as float)/ro_run)/ro_mch_op as int)) [per100],iif(ro_run=0 or ro_mch_op=0,0,cast(((cast(12 as float)/ro_run)/ro_mch_op)*0.85 as int)) [per85] "
                        + " from [qad].[dbo].[qad_ro_det] where ro_routing like  '{0}' order by  ro_op ";
        DataTable dtOP = DbHelperSQL.Query(string.Format(sqlOP, strpgiver)).Tables[0];
        ASPxGridView0.DataSource = dtOP;
        ASPxGridView0.DataBind();

        for (int i = 1; i <= dtBOM.Rows.Count; i++)
        {
            string sqlOPs = sqlOP;
            var dtOPs = DbHelperSQL.Query(string.Format(sqlOPs, dtBOM.Rows[i - 1]["ps_comp"].ToString()));
            if (dtOPs.Tables[0].Rows.Count > 0)
            {
                this.ASPxPageControl1.TabPages[i].Visible = true;
                this.ASPxPageControl1.TabPages[i].Text = dtBOM.Rows[i - 1]["ps_comp"].ToString() + "工艺路线";
                var gridctrl = ASPxPageControl1.TabPages[i].FindControl("ASPxGridView" + i.ToString()) as DevExpress.Web.ASPxGridView;
                gridctrl.DataSource = dtOPs;
                gridctrl.DataBind();
            }
            else
            {
                this.ASPxPageControl1.TabPages[i].Visible = false;
            }
            

        }
    }
    /// <summary>
    /// 默认隐藏tab不显示
    /// </summary>
    public void hideTab()
    {
        for (int i = 5; i > 0; i--)
        {
            ASPxPageControl1.TabPages[i].Visible = false;
        }
    }

    private void BindTreeData(string strpgitemp)
    {
        var sqlBOM_main = "select pt_part,pt_part 物料号,pt_desc1 as 物料描述,pt_desc2 as 描述2,''工序 ,                       '1' 数量 ,    '' PID,cast(pt_net_wt as float) 重量,pt_ord_mult 最小包装量 from [dbo].[qad_pt_mstr] where   pt_status<>'Dead' and( pt_domain='" + ddl_comp.SelectedValue + "' or  '" + ddl_comp.SelectedValue + "'='' ) and  pt_part = '{0}'  "; // "SELECT [ps_par] ,[ps_comp],convert(int,cast([ps_qty_per] as float) ) [ps_qty_per]  ,[ps_op],b.pt_desc1 物料描述  FROM [Report].[dbo].[qad_ps_mstr] a join [Report].[dbo].[qad_pt_mstr] b on a.ps_comp=b.pt_part  where ps_par='{0}'   order by [ps_comp];";
        var sqlBOM_child = "select pt_part,pt_part 物料号,pt_desc1 as 物料描述,pt_desc2 as 描述2,  [ps_op] as 工序 ,cast(cast(ps_qty_per as float(2)) as varchar(10))数量 ,ps_par PID,cast(pt_net_wt as float) 重量,pt_ord_mult 最小包装量 FROM [172.16.5.6].[Report].[dbo].[qad_ps_mstr] a join [dbo].[qad_pt_mstr] b on a.ps_comp=b.pt_part  and a.ps_domain=b.pt_domain    where ps_par like '{0}%' ";// "SELECT [ps_par] ,[ps_comp],convert(int,cast([ps_qty_per] as float) ) [ps_qty_per]  ,[ps_op],b.pt_desc1 物料描述  FROM [Report].[dbo].[qad_ps_mstr] a join [Report].[dbo].[qad_pt_mstr] b on a.ps_comp=b.pt_part  where ps_par='{0}'   order by [ps_comp];";
        DataTable dtmain = DbHelperSQL.Query(string.Format(sqlBOM_main, strpgitemp) + " union all " + string.Format(sqlBOM_child, strpgitemp)).Tables[0];
        // +" union  select pt_part,pt_part name,1,'',pt_desc1 ,pt_net_wt 重量,pt_ord_mult 最小包装量  from [172.16.5.26].mes.[dbo].[qad_pt_mstr] where  pt_status<>'Dead' and( pt_domain='" + domain + "' or  '" + domain + "'='' ) and  pt_part='" + pgino + "' ";
        Session["dt"] = dtmain;
        this.treeList1.DataSource = dtmain;
        treeList1.KeyFieldName = "pt_part";
        treeList1.ParentFieldName = "PID";
        treeList1.DataBind();
        treeList1.ExpandAll();
    }
}