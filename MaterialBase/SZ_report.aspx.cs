using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;

public partial class SZ_report : System.Web.UI.Page
{
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.gv_pt.PageSize = 200;
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Get_ddl();//给相关人员Dropdownlist 绑定数据源
             QueryASPxGridView();
        }
       
    }
    public void Get_ddl()
    {
        //给Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL_jgcz = @" SELECT distinct [被加工材质] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_jgcz = DbHelperSQL.Query(strSQL_jgcz).Tables[0];
        if (dt_jgcz.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_jgcz, dt_jgcz, "被加工材质", "被加工材质");
        }
        DDL_jgcz.Items.Insert(0, new ListItem("", ""));

        string strSQL_szlx = @" SELECT distinct [丝锥类型] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_szlx = DbHelperSQL.Query(strSQL_szlx).Tables[0];
        if (dt_szlx.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_szlx, dt_szlx, "丝锥类型", "丝锥类型");
        }
        DDL_szlx.Items.Insert(0, new ListItem("", ""));

        string strSQL_xx = @" SELECT distinct [旋向] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_xx = DbHelperSQL.Query(strSQL_xx).Tables[0];
        if (dt_xx.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_xx, dt_xx, "旋向", "旋向");
        }
        DDL_xx.Items.Insert(0, new ListItem("", ""));

        string strSQL = @" SELECT distinct [槽型] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_cx = DbHelperSQL.Query(strSQL).Tables[0];
        if (dt_cx.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_cx, dt_cx, "槽型", "槽型");
        }
        DDL_cx.Items.Insert(0, new ListItem("", ""));

        string strSQL_sccz = @" SELECT distinct [丝锥材质] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_sccz = DbHelperSQL.Query(strSQL_sccz).Tables[0];
        if (dt_sccz.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_sccz, dt_sccz, "丝锥材质", "丝锥材质");
        }
        DDL_sccz.Items.Insert(0, new ListItem("", ""));

        string strSQL_isnl = @" SELECT distinct [是否内冷] FROM [MES].[dbo].[MES_PT_PART]  ";
        DataTable dt_isnl = DbHelperSQL.Query(strSQL_isnl).Tables[0];
        if (dt_isnl.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_isnl, dt_isnl, "是否内冷", "是否内冷");
        }
        DDL_isnl.Items.Insert(0, new ListItem("", ""));


    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        gv_pt.PageIndex = 0;
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        gv_pt.DataSource = null;
        gv_pt.DataBind();
        //DataTable dt = MaterialBase_CLASS.SZ_base("SZ",txtwlh.Value, txtms.Value, txttc.Value, txtpp.Value, txtppms.Value, txtgys.Value, DDL_jgcz.SelectedValue, DDL_szlx.SelectedValue, DDL_xx.SelectedValue, DDL_cx.SelectedValue, DDL_sccz.SelectedValue, DDL_isnl.SelectedValue);

        //this.gv_pt.DataSource = dt;
        gv_pt.DataBind();

        //合并单元格
        int[] cols = { 0, 1, 2, 3, 4, 5, 6};
        MergGridRow.MergeRow(gv_pt, cols);

        int rowIndex = 1;
        for (int j = 0; j < gv_pt.Rows.Count; j++)
        {
            if (gv_pt.Rows[j].Cells[0].Visible == true)
            {
                gv_pt.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Customer_QAD.aspx?requestid=" + Server.HtmlDecode(gv_pt.Rows[j].Cells[6].Text).Trim()+"";
                link.Text = gv_pt.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                gv_pt.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;
            }
        }
    }
    protected void gv_pt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[6].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }
    }
    protected void gv_pt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_pt.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }

}