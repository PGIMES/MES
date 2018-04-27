using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wuliu_SemiFinishedKanban : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["empid"] == null)
        {   // 给Session["empid"]  初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
        QueryASPxGridView(this.ddlSite2.SelectedValue.ToString(),
   this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());    
        }
        QueryASPxGridView(this.ddlSite2.SelectedValue.ToString(),
this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());
    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (this.txt_partno.Text == "")
        {
           Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('物料号栏位不能为空！'); $('#CKBZ').toggleClass('in');", true);
            return;
        }
        if (this.txt_planqty.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('排产夹具数量不能为空！'); $('#CKBZ').toggleClass('in');", true);
            return;
        }
        if (Convert.ToDouble(this.txt_fixtureqty.Text.ToString())- Convert.ToDouble(this.txt_planqty.Text.ToString())<0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('夹具排产数量必须小于等于夹具数量！'); $('#CKBZ').toggleClass('in');", true);
            return;
        }

        Wuliu WuliuSQLHelp = new Wuliu();
        int MaintainFixture = WuliuSQLHelp.Maintain_Fixture_Qty(this.ddl_site.SelectedValue.ToString(),
            this.txt_partno.Text.ToUpper().Trim().ToString(),this.txt_planqty.Text.Trim().ToString(), Session["empid"].ToString());
        if (MaintainFixture == -1)
        {
            //Response.Write("<script>alert('排产夹具数量维护失败，请联系IT!')</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('排产夹具数量维护失败，请联系IT!'); $('#CKBZ').toggleClass('in');", true);
            return;
        }
        else
        {
           //Response.Write("<script>alert('排产夹具数量维护成功!')</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('排产夹具数量维护成功!'); $('#CKBZ').toggleClass('in');", true);
          
        }
        //重置页面
        ResetPage();
        //刷新下面的查询
        QueryASPxGridView(this.ddlSite2.SelectedValue.ToString(),
        this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "$('#CKBZ').toggleClass('in');", true);

    }
    public  void ResetPage()
    {
        this.ddl_site.SelectedIndex = -1;
        this.txt_partno.Text = "";
        this.txt_partname.Text = "";
        this.txt_fixtureqty.Text = "";
        this.txt_planqty.Text = "";
    }
    /// <summary>
    /// 查询所有的信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView(this.ddlSite2.SelectedValue.ToString(),
 this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());
    }
    /// <summary>
    /// 输入物料号，焦点移动后，根据物料号自动带出物料名称
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txt_partno_TextChanged(object sender, EventArgs e)
    {
        Wuliu WuliuSQLHelp = new Wuliu();
        DataTable dt1 = WuliuSQLHelp.Get_SF_Partname_query(this.ddl_site.SelectedValue, this.txt_partno.Text.Trim().ToUpper());
        DataTable dt2 = WuliuSQLHelp.Get_SF_Fixture_Qty_query(this.ddl_site.SelectedValue, this.txt_partno.Text.Trim().ToUpper());
        if (dt1.Rows.Count > 0)
        {
            this.txt_partname.Text = dt1.Rows[0]["pt_desc1"].ToString();
            if (dt2.Rows.Count > 0)
            {
                this.txt_fixtureqty.Text = dt2.Rows[0]["FIXTURE_QTY"].ToString(); //夹具套数
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('夹具数量没有维护，请联系IE!'); $('#CKBZ').toggleClass('in');", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('您输入的物料号有误，请确认!'); $('#CKBZ').toggleClass('in');", true);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "$('#CKBZ').toggleClass('in');", true);
    
}

    protected void ASPxGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "半成品库库存可用班次")
        {
            string tJhdm = e.GetValue("机加是否生产").ToString();
            if (float.Parse(e.CellValue.ToString()) - 2 < 0 && tJhdm.Equals("生产中"))
            {
                e.Cell.BackColor = System.Drawing.Color.Red;

            }
            if (float.Parse(e.CellValue.ToString()) - 6 > 0)
            {
                e.Cell.BackColor = System.Drawing.Color.Yellow;
            }
        }
        if (e.DataColumn.FieldName == "差异")
        {
           
            if (float.Parse(e.CellValue.ToString())< 0)
            {
                e.Cell.BackColor = System.Drawing.Color.Red;

            }
           
        }
        if (e.DataColumn.FieldName == "物料号" || e.DataColumn.FieldName == "零件号")
        { }
        else
        {
            e.Cell.Style.Add("text-align", "center");
        }
    }
    public void QueryASPxGridView(string domain, string part_no, string part_name, string status)
    {
        Wuliu WuliuSQLHelp = new Wuliu();
        DataTable dt = WuliuSQLHelp.Get_SF_QAD_REORT_Query(domain, part_no, part_name, status);
        this.ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView(this.ddlSite2.SelectedValue.ToString(),
        this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());
    }
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        Wuliu WuliuQuery = new Wuliu();
        DataTable ldt = WuliuQuery.Get_SF_QAD_REORT_Query(this.ddlSite2.SelectedValue.ToString(),
        this.txtPartno2.Text.Trim().ToUpper().ToString(), this.txtPartname2.Text.ToString(), this.ddl_status.SelectedValue.ToString());
        string lsname = "半成品数据看板";
        WuliuQuery.DataTableToExcel(ldt, "xls", lsname, "1");
    }

}