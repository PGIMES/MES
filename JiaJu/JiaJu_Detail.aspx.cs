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
using System.Text;
using System.Web.Services;

public partial class JiaJu_JiaJu_Detail : System.Web.UI.Page
{

    string domain = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
            domain = LogUserModel.Domain;
            Drop_comp.SelectedValue = domain;
            //Setddl_value("夹具类型", Drop_type);//夹具类型下拉
            Setddl_value("夹具状态", Drop_status);//夹具状态下拉
            QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);
        }
        if (this.ASPxGridView3.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);
            ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight();", true);
        }
    }

    public void Setddl_value(string type, DropDownList drop)
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select * from Jiaju_Base where type='" + type + "' ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        fun.initDropDownList(drop, dt, "codevalue", "codevalue");
        drop.Items.Insert(0, new ListItem("--请选择--", ""));
    }
   

    protected void btn_search_Click(object sender, EventArgs e)
    {
       // string type = txt_type.Value;
        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView(string comp,string jiajuno,string type,string pn,string status,string customer,string loc)
    {
        //DataTable dt_type = DbHelperSQL.Query("select codevalue from Jiaju_Base where type='夹具类型'").Tables[0];
        //((GridViewDataComboBoxColumn)this.ASPxGridView3.Columns["type"]).PropertiesComboBox.DataSource = dt_type;

        DataTable dt_bel = DbHelperSQL.Query("select distinct isnull(zc_bel,'') as codevalue from jiaju_list order by isnull(zc_bel,'')").Tables[0];//select distinct zc_bel as codevalue from jiaju_list
        ((GridViewDataComboBoxColumn)this.ASPxGridView3.Columns["zc_bel"]).PropertiesComboBox.DataSource = dt_bel;

        DataTable dt_comp = DbHelperSQL.Query("select '100' codevalue union select '200'").Tables[0];
        ((GridViewDataComboBoxColumn)this.ASPxGridView3.Columns["comp"]).PropertiesComboBox.DataSource = dt_comp;

        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from Jiaju_List where 1=1");
        DataTable dt3 = null;
        if (jiajuno != "")
        {
            strSql.Append("and jiajuno like '%"+jiajuno+"%'");
        }
        if (comp != "")
        {
            strSql.Append("and comp like '%" + comp + "%'");
        }
        if (type != "")
        {
            strSql.Append("and type like '%" + type + "%'");
        }
        if (status != "")
        {
            strSql.Append("and status like '%" + status + "%'");
        }
        if (pn != "")
        {
            strSql.Append("and pn like '%" + pn + "%'");
        }
        if (customer != "")
        {
            strSql.Append("and customer like '%" + customer + "%'");
        }
        if (loc != "")
        {
            strSql.Append("and loc like '%" + loc + "%'");
        }
        dt3 = DbHelperSQL.Query(strSql.ToString()).Tables[0];
        ASPxGridView3.DataSource = dt3;
        ASPxGridView3.DataBind();
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);
        gv_export.WriteXlsxToResponse("夹具清单_"  + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    [WebMethod]
    public static string Get_Jiaju_no(string comp)
    {
        string sql = @"select '" + comp.Left(1) + "-TF'+CAST(CAST(SUBSTRING(jiajuno,5,LEN(jiajuno)-4) AS INT)+1 as varchar) from (select max(jiajuno)jiajuno from JiaJu_List where comp='" + comp + "')A";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        return dt.Rows[0][0].ToString();
    }


    private string Jiaju_no(string comp)
    {
        string sql = @"select '" + comp.Left(1) + "-TF'+CAST(CAST(SUBSTRING(jiajuno,5,LEN(jiajuno)-4) AS INT)+1 as varchar) from (select max(jiajuno)jiajuno from JiaJu_List where comp='" + comp + "')A";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        return dt.Rows[0][0].ToString();
    }



    protected void ASPxGridView3_ToolbarItemClick(object sender, ASPxGridToolbarItemClickEventArgs e)
    {
        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);
        ASPxGridView grid = (ASPxGridView)sender;
        switch (e.Item.Name)
        {
            case "CustomExportToXLS":
                grid.ExportXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
                break;
            case "CustomExportToXLSX":
                grid.ExportXlsxToResponse(new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                break;
            default:
                break;
        }
    }

    protected void ASPxGridView3_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        if (!grid.IsEditing) return;

        if (e.Column.FieldName == "jiajuno")//夹具号
        {
            ASPxTextBox TXT = e.Editor as ASPxTextBox;
            TXT.ClientEnabled = false;
        }
        if (e.Column.FieldName == "comp")
        {
            ASPxComboBox dropcomp = e.Editor as ASPxComboBox;
            if (e.KeyValue != DBNull.Value && e.KeyValue != null)
            {
                dropcomp.ClientEnabled = false;//编辑时，只读
            }
        }

    }

    protected void ASPxGridView3_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        foreach (GridViewColumn column in grid.Columns)
        {
            GridViewDataColumn dataColumn = column as GridViewDataColumn;
            if (dataColumn == null) continue;
            if (e.NewValues[dataColumn.FieldName] == null && (dataColumn.FieldName == "pn" || dataColumn.FieldName == "pn_name" || dataColumn.FieldName == "quantity" ||  dataColumn.FieldName == "comp"))
            {
                e.Errors[dataColumn] = "公司别/零件号/零件名称/数量不可为空.";
            }
           
        }
        if (e.Errors.Count > 0) e.RowError = "公司别/零件号/零件名称/库位/数量不可为空.";
    }

    protected void ASPxGridView3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string comp = e.NewValues["comp"] != null ? e.NewValues["comp"].ToString() : "";
        string type = e.NewValues["type"] != null ? e.NewValues["type"].ToString() : "";
        string jiajuno = e.NewValues["jiajuno"] != null ? e.NewValues["jiajuno"].ToString() : "";
        string pn = e.NewValues["pn"].ToString();
        string gxh = e.NewValues["gxh"] != null ? e.NewValues["gxh"].ToString() : "";
        string line = e.NewValues["line"] != null ? e.NewValues["line"].ToString() : "";
        string gongwei = e.NewValues["gongwei"] != null ? e.NewValues["gongwei"].ToString() : "";
        string pn_name = e.NewValues["pn_name"].ToString();
        string customer = e.NewValues["customer"] != null ? e.NewValues["customer"].ToString() : "";
        string supplier = e.NewValues["supplier"] != null ? e.NewValues["supplier"].ToString() : "";
        string zc_bel = e.NewValues["zc_bel"] != null ? e.NewValues["zc_bel"].ToString() : "";
        string loc = e.NewValues["loc"] != null ? e.NewValues["loc"].ToString() : "";
        string quantity = e.NewValues["quantity"].ToString();
        string now = DateTime.Now.ToString("yyyy/MM/dd");
        string status = e.NewValues["status"] != null ? e.NewValues["status"].ToString() : "";


        string sql_update = @"update JiaJu_List set comp='{0}',type='{2}',pn='{3}',pn_name='{4}',zc_bel='{5}',gxh='{6}',line='{7}',gongwei='{8}',customer='{9}',supplier='{10}',loc='{12}',quantity='{13}',update_date='{11}',status='{14}' where jiajuno='{1}'";
        sql_update = string.Format(sql_update, comp, jiajuno, type, pn, pn_name, zc_bel, gxh, line, gongwei, customer, supplier, now, loc, quantity, status);
        DbHelperSQL.ExecuteSql(sql_update);

        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);

        ASPxGridView3.CancelEdit();
        e.Cancel = true;


    }

    protected void ASPxGridView3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string comp = e.NewValues["comp"] != null ? e.NewValues["comp"].ToString() : "";
        string type = e.NewValues["type"] != null ? e.NewValues["type"].ToString() : "";
        string jiajuno = e.NewValues["jiajuno"] != null ? e.NewValues["jiajuno"].ToString() : "";
        string pn = e.NewValues["pn"].ToString() ;
        string gxh = e.NewValues["gxh"] != null ? e.NewValues["gxh"].ToString() : "";
        string line = e.NewValues["line"] != null ? e.NewValues["line"].ToString() : "";
        string gongwei = e.NewValues["gongwei"] != null ? e.NewValues["gongwei"].ToString() : "";
        string pn_name = e.NewValues["pn_name"].ToString();
        string customer =e.NewValues["customer"]!=null? e.NewValues["customer"].ToString():"";
        string supplier = e.NewValues["supplier"] != null ? e.NewValues["supplier"].ToString() : "";
        string zc_bel = e.NewValues["zc_bel"] != null ? e.NewValues["zc_bel"].ToString() : "";
        string loc = e.NewValues["loc"] != null ? e.NewValues["loc"].ToString() : "";
        string quantity = e.NewValues["quantity"].ToString();
        string now = DateTime.Now.ToString("yyyy/MM/dd");
        string status =e.NewValues["status"]!=null? e.NewValues["status"].ToString():"";


        string sql_insert = @"insert into JiaJu_List(comp,jiajuno,type,pn,pn_name,zc_bel,gxh,line,gongwei,customer,supplier,create_date,loc,quantity,status,update_date)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',NULL)";
        sql_insert = string.Format(sql_insert,comp, jiajuno, type, pn, pn_name, zc_bel, gxh,line,gongwei,customer,supplier,now, loc,quantity,status);
        DbHelperSQL.ExecuteSql(sql_insert);

        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);

        ASPxGridView3.CancelEdit();
        e.Cancel = true;

    }

    protected void ASPxGridView3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string jiajuno = e.Keys["jiajuno"].ToString();
        string sql_del = @"delete JiaJu_List where jiajuno='{0}' ";
        sql_del = string.Format(sql_del, jiajuno);
        DbHelperSQL.ExecuteSql(sql_del);

        QueryASPxGridView(Drop_comp.SelectedValue, txt_jiajuno.Value, txt_type.Value, txt_pn.Value, Drop_status.SelectedValue, txt_customer.Value, txt_loc.Value);

        ASPxGridView3.CancelEdit();
        e.Cancel = true;

    }


    
}