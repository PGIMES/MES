using DevExpress.Export;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Forms_PurChase_PO_KPI_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }
        Session["LogUser"] = LogUserModel;

        if (((LoginUser)Session["LogUser"]).UserId != "02089" && ((LoginUser)Session["LogUser"]).DepartName != "IT部")
        {
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "checkAuth();", true);
        }
        else
        {
            if (!IsPostBack)
            {
                Set_buyname();
                QueryASPxGridView();
            }
            if (this.gv.IsCallback == true)//页面搜索条件使用
            {
                QueryASPxGridView();
                ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
            }
        }

    }
    public void Set_buyname()
    {
        string strSQL = @"select '' workcode,'All' lastname union select workcode,lastname from HRM_EMP_MES  where dept_name='采购二部'";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ddl_buyname.DataValueField = "workcode";
        ddl_buyname.DataTextField = "lastname";
        ddl_buyname.DataSource = dt;
        ddl_buyname.DataBind();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (((LoginUser)Session["LogUser"]).UserId != "02089" && ((LoginUser)Session["LogUser"]).DepartName != "IT部")
        {
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "checkAuth();", true);
        }
        else
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec [Report_PO_KPI] '" + ddl_domain.SelectedValue + "','" + ddl_buyname.SelectedValue + "'").Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv.ExportXlsxToResponse("应付类合同执行进度查询_" + ddl_domain.SelectedItem.Text + "_" + System.DateTime.Now.ToShortDateString()
            , new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void gv_EmpYear_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        string[] param_arr = param.Split('|');
        string workcode = param_arr[0]; string year = param_arr[1];

        DataTable dt = DbHelperSQL.Query("exec [Report_PO_KPI_By_EmpYear] '" + workcode + "','" + year + "'").Tables[0];
        gv_EmpYear.DataSource = dt;
        gv_EmpYear.DataBind();
    }

    protected void gv_EmpYear_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString() == "及时率")
            {
                for (int i = 1; i < 13; i++)
                {
                    if (e.GetValue(i.ToString()) != DBNull.Value)
                    {
                        e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(i.ToString())) * 100) + "%";
                    }
                }
            }
        }
    }

    protected void gv_EmpMonth_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        string[] param_arr = param.Split('|');
        string workcode = param_arr[0]; string year = param_arr[1]; string month = param_arr[2];

        DataTable dt = DbHelperSQL.Query("exec [Report_PO_KPI_By_EmpMonth] '" + workcode + "','" + year + "','" + month + "'").Tables[0];
        gv_EmpMonth.DataSource = dt;
        gv_EmpMonth.DataBind();
        gv_EmpMonth.Columns["统计明细项"].Width = Unit.Pixel(100);
    }


    protected void gv_EmpMonth_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.KeyValue.ToString() == "及时率")
            {
                DataTable ldt = Pgi.Auto.Control.AgvToDt(gv_EmpMonth);
                for (int i = 0; i < ldt.Columns.Count; i++)
                {
                    if (ldt.Columns[i].ColumnName != "统计明细项" && ldt.Columns[i].ColumnName != "flag")
                    {
                        if (e.GetValue(ldt.Columns[i].ColumnName) != DBNull.Value)
                        {
                            e.Row.Cells[i].Text = Convert.ToString(Convert.ToDouble(e.GetValue(ldt.Columns[i].ColumnName)) * 100) + "%";
                        }
                    }
                }
            }
        }
    }
}

