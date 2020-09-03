using System;
using System.Web.UI;
using System.Data;
using Maticsoft.DBUtility;
using DevExpress.Web;

public partial class Forms_PurChase_PO_Report_Receive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化日期           
            //txtDateFrom.Text = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            //txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        QueryASPxGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        //DataTable dt = DbHelperSQL.Query("exec Pur_PO_Receive '" + ddl_domain.SelectedValue + "','" + txtDateFrom.Text + "','" + txtDateTo.Text + "'").Tables[0];
        DataTable dt = DbHelperSQL.Query("exec Pur_PO_Receive '" + ddl_domain.SelectedValue + "'").Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("合同类未收货查询_" + ddl_domain.SelectedItem.Text + "_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }

        string SysContractNo = Convert.ToString(e.GetValue("SysContractNo"));
        if (SysContractNo.Contains("_QAD"))
        {
            return;
        }

        int pono_index = 0;
        for (int i = 0; i < this.GV_PART.DataColumns.Count; i++)
        {
            if (this.GV_PART.DataColumns[i].FieldName == "PoNo")
            {
                pono_index = i;
            }
        }

        string PoNo = Convert.ToString(e.GetValue("PoNo"));
        string groupid = Convert.ToString(e.GetValue("GroupID"));
        e.Row.Cells[pono_index + 1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&instanceid="
                    + e.GetValue("PoNo") + "&groupid=" + groupid + "&display=1' target='_blank'>" + PoNo.ToString() + "</a>";
    }
}