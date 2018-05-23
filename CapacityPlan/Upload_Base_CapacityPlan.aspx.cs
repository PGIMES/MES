using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CapacityPlan_Upload_Base_CapacityPlan : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryASPxGridView();
        }
        if (this.gv.IsCallback)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        Capacity Capacity = new Capacity();
        DataTable dt = Capacity.Upload_Base_CapacityPlan(txt_pgi_no.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
        SetGrid(this.gv, dt, 90);
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    private static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw)
    {
        if (ldt_data == null)
        {
            return;
        }

        lgrid.AutoGenerateColumns = false;
        int lnwidth = 0; int lnwidth_emp = 0;
        lgrid.Columns.Clear();
        for (int i = 0; i < ldt_data.Columns.Count; i++)
        {
            DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
            lcolumn.Name = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.Caption = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.FieldName = ldt_data.Columns[i].ColumnName.ToString();
            lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

            //if (ldt_data.Columns[i].MaxLength > 0)
            //{
            //    lcolumn.Width = ldt_data.Columns[i].MaxLength;
            //    lcolumn.ExportWidth = ldt_data.Columns[i].MaxLength;
            //}
            //else
            //{
            //    lcolumn.Width = lnw;
            //    lcolumn.ExportWidth = lnw;
            //}

            lnwidth_emp = 0;
            if (ldt_data.Columns[i].ColumnName.ToString() == "工厂") { lnwidth_emp = 80; }
            if (ldt_data.Columns[i].ColumnName.ToString() == "工艺代码") { lnwidth_emp = 100; }
            if (ldt_data.Columns[i].ColumnName.ToString() == "工作中心") { lnwidth_emp = 80; }
            if (ldt_data.Columns[i].ColumnName.ToString() == "周数") { lnwidth_emp = 120; }

            if (lnwidth_emp > 0)
            {
                lcolumn.Width = lnwidth_emp;
                lcolumn.ExportWidth = lnwidth_emp;
                lnwidth += Convert.ToInt32(lnwidth_emp);
            }
            else
            {
                lcolumn.Width = lnw;
                lcolumn.ExportWidth = lnw;
                lnwidth += Convert.ToInt32(lnw);
            }

            //设置查询
            lcolumn.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;

            lgrid.Columns.Add(lcolumn);
          

            //if (ldt_data.Columns[i].MaxLength > 0)
            //{
            //    lnwidth += ldt_data.Columns[i].MaxLength;
            //}
            //else
            //{
            //    lnwidth += Convert.ToInt32(lnw);
            //}

        }

        //lgrid.Columns[0].Width = 40;
        //lgrid.Columns[0].ExportWidth = 40;

        //lgrid.Width = (lnwidth + 40);
        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();

        //return lgrid;
    }


    //protected void btn_del_Click(object sender, EventArgs e)
    //{
        //List<attach_forms> List = (List<attach_forms>)Session["attach_forms"];

        //string[] fieldnames = new string[] { "domain", "pgi_no" };
        //List<object> lSelectValues = gv.GetSelectedFieldValues(fieldnames);

        //if (lSelectValues.Count <= 0)
        //{
        //    Pgi.Auto.Public.MsgBox(this, "alert", " 请选择需要删除的文件!");
        //    return;
        //}

        //string capacity_date = "";
        //for (int i = 3; i < gv.Columns.Count-1; i++)
        //{
        //    capacity_date = gv.Columns[i].Name;
        //}

        //for (int i = 0; i < lSelectValues.Count; i++)
        //{
        //    lSelectValues[i].ToString();
        //}
        //QueryASPxGridView();
    //}

    //protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    QueryASPxGridView();
    //}

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse(System.DateTime.Now.ToShortDateString());//导出到Excel
    }

}