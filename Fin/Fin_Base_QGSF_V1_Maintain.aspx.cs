using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_Fin_Base_QGSF_V1_Maintain : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        SetWlh(); Setimmunity();
    }

    //物料号
    private void SetWlh()
    {
        wlh.Columns.Clear();

        //新增没有维护税率的 物料号
        string lssql = @"exec usp_Fin_Base_QGSF_maintain_init '',''";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        wlh.ValueField = "V";
        wlh.Columns.Add("comd_pgino", "物料号", 80);
        wlh.Columns.Add("com_domain", "域", 50);
        wlh.TextFormatString = "{0}|{1}";
        wlh.DataSource = ldt;
        wlh.DataBind();
    }

    //物料号
    private void Setimmunity()
    {
        cmb_immunity.Columns.Clear();

        string lssql = @"select 'Y' YN union select 'N' YN";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        cmb_immunity.ValueField = "YN";
        cmb_immunity.Columns.Add("YN", "是否豁免", 80);
        cmb_immunity.DataSource = ldt;
        cmb_immunity.DataBind();
    }

    [WebMethod]
    public static string GetData_ByWlh(string wlh_domain)
    {
        string re_domain="", re_hscode = "", re_comdesc = "", baserate = "", qgcode = "", qgrate = "", immunity="";

        string[] wlh_domain_arr = wlh_domain.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string lssql = @"exec usp_Fin_Base_QGSF_maintain_init '" + wlhno + "','" + domain + "'";
        DataSet ds = DbHelperSQL.Query(lssql);

        DataTable dt = ds.Tables[0];
        re_domain = dt.Rows[0]["com_domain"].ToString();
        re_hscode = dt.Rows[0]["com_comm_code"].ToString();
        re_comdesc = dt.Rows[0]["com_desc"].ToString();

        if (ds.Tables[1].Rows.Count > 0)
        {
            DataTable dt_2 = ds.Tables[1];

            baserate = dt_2.Rows[0]["BaseRate"].ToString();
            qgcode = dt_2.Rows[0]["301code"].ToString();
            qgrate = dt_2.Rows[0]["301Rate"].ToString();
            immunity = dt_2.Rows[0]["immunity"].ToString();
        }

        string result = "[{\"domain\":\"" + re_domain + "\",\"hscode\":\"" + re_hscode + "\",\"comdesc\":\"" + re_comdesc 
            + "\",\"baserate\":\"" + baserate + "\",\"qgcode\":\"" + qgcode + "\",\"qgrate\":\"" + qgrate
            + "\",\"immunity\":\"" + immunity + "\"}]";
        return result;

    }

    /*
    protected void GetGrid(DataTable DT)
    {
        DataTable ldt = DT;
        int index = gv.VisibleRowCount;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {

            DevExpress.Web.ASPxDateEdit tb1 = (DevExpress.Web.ASPxDateEdit)gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv.Columns["Effective_date"], "Effective_date");
            DevExpress.Web.ASPxDateEdit tb2 = (DevExpress.Web.ASPxDateEdit)gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv.Columns["End_date"], "End_date");
            if (ldt.Rows[i]["Effective_date"].ToString() != "")
            {
                tb1.Date = Convert.ToDateTime(ldt.Rows[i]["Effective_date"].ToString());
            }

            if (ldt.Rows[i]["End_date"].ToString() != "")
            {
                tb2.Date = Convert.ToDateTime(ldt.Rows[i]["End_date"].ToString());
            }
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        add_row(1);
    }
    protected void add_row(int lnadd_rows)
    {
        DataTable ldt_pay = Pgi.Auto.Control.AgvToDt(this.gv);
        int ln = 0;

        for (int i = 0; i < ldt_pay.Rows.Count; i++)
        {
            if (Convert.ToInt32(ldt_pay.Rows[i]["numid"].ToString()) > ln)
            {
                ln = Convert.ToInt32(ldt_pay.Rows[i]["numid"].ToString());
            }
        }

        DataRow ldr = ldt_pay.NewRow();
        ldr["numid"] = (ln + 1).ToString();
        ldt_pay.Rows.Add(ldr);

        this.gv.DataSource = ldt_pay;
        this.gv.DataBind();
        GetGrid(ldt_pay);
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1")
            {
                ldt.Rows[i].Delete();
            }
        }

        ldt.AcceptChanges();
        gv.DataSource = ldt;
        gv.DataBind();
        GetGrid(ldt);
    }

    */

    protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        if (param == "init")
        {
            gv_bind(param);
        }
    }

    public void gv_bind(string param)
    {
        string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string sql = @"select id,Effective_date,End_date from [dbo].[Fin_Base_QGSF] where wlh='" + wlhno + "' and domain='" + domain + "' order by Effective_date";
        DataTable dt  = DbHelperSQL.Query(sql).Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string err = "";

        //非空判断
        ASPxGridView grid = (ASPxGridView)sender;
        foreach (GridViewColumn column in grid.Columns)
        {
            GridViewDataColumn dataColumn = column as GridViewDataColumn;
            if (dataColumn == null) continue;
            if (dataColumn.FieldName == "id") continue;
            if (e.NewValues[dataColumn.FieldName] == null)
            {
                //e.Errors[dataColumn] = dataColumn.Caption + "不可为空.";
                err += dataColumn.Caption + "不可为空.<br/>";
            }
        }
        //数据验证
        if (err == "")
        {
            string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

            string Effective_date = e.NewValues["Effective_date"].ToString();
            string End_date = e.NewValues["End_date"].ToString();
            int id = 0;
            if (e.IsNewRow == false) { id = Convert.ToInt32(e.Keys["id"]); }

            if (Convert.ToDateTime(Effective_date) > Convert.ToDateTime(End_date))
            {
                err = "生效日期不可大于截止日期";
            }

            if (err == "")
            {
                string re_sql = @"exec usp_Fin_Base_QGSF_maintain_check '" + wlhno + "','" + domain + "','" + Effective_date + "','" + End_date + "'," + id + "";
                DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];
                string flag = re_dt.Rows[0][0].ToString();
                string msg = re_dt.Rows[0][1].ToString();
                if (flag == "Y") { err = msg; }
            }
        }

        //if (e.Errors.Count > 0) e.RowError = "Please, fill all fields.";
        if (err != "") e.RowError = err;
    }

    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string Effective_date = e.NewValues["Effective_date"].ToString();
        string End_date = e.NewValues["End_date"].ToString();
        int id = Convert.ToInt32(e.Keys["id"]);

        string sql_update = @"update Fin_Base_QGSF set [Effective_date]='{1}',[End_date]='{2}' where id={0}";
        sql_update = string.Format(sql_update, id, Effective_date, End_date);
        DbHelperSQL.ExecuteSql(sql_update);

        gv_bind("");

        gv.CancelEdit();
        e.Cancel = true;


    }

    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string Effective_date = e.NewValues["Effective_date"].ToString();
        string End_date = e.NewValues["End_date"].ToString();

        string sql = @"select id,Effective_date,End_date from [dbo].[Fin_Base_QGSF] where wlh='" + wlhno + "' and domain='" + domain + "' order by Effective_date";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        if (dt.Rows.Count == 1 && dt.Rows[0]["Effective_date"].ToString() == "")
        {
            string sql_update = @"update Fin_Base_QGSF set [Effective_date]='{1}',[End_date]='{2}',immunity='Y' where id={0}";
            sql_update = string.Format(sql_update, Convert.ToInt32(dt.Rows[0]["id"]), Effective_date, End_date);
            DbHelperSQL.ExecuteSql(sql_update);
        }
        else
        {
            string sql_insert = @"insert into Fin_Base_QGSF(domain, wlh, [301code], BaseRate, [301Rate], immunity
                                , Effective_date, End_date, UpdateId, UpdateName, UpdateTime)
                            values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}',getdate())";
            sql_insert = string.Format(sql_insert, domain, wlhno, txt_301code.Text, Convert.ToSingle(txt_BaseRate.Text), Convert.ToSingle(txt_301Rate.Text), cmb_immunity.Value
                                    , Effective_date, End_date, LogUserModel.UserId, LogUserModel.UserName);
            DbHelperSQL.ExecuteSql(sql_insert);
        }

        gv_bind("");

        gv.CancelEdit();
        e.Cancel = true;

    }

    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];
        int id = Convert.ToInt32(e.Keys["id"]);

        string sql_del = @"exec usp_Fin_Base_QGSF_maintain_del '" + wlhno + "','" + domain + "'," + id + "";
        DbHelperSQL.Query(sql_del);

        gv_bind("");

        gv.CancelEdit();
        e.Cancel = true;

        //string sql = @"select id,Effective_date,End_date,immunity from [dbo].[Fin_Base_QGSF] where wlh='" + wlhno + "' and domain='" + domain + "' order by Effective_date";
        //DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        //if (dt.Rows.Count == 1 && dt.Rows[0]["immunity"].ToString() == "N")
        //{
        //    string lsstr = "layer.alert('删除成功',function(index) {layer.close(index);parent.location.reload();})";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
        //}

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string[] wlh_domain_arr = wlh.Value.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

        string sql = @"exec [usp_Fin_Base_QGSF_maintain_save] '{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}'";
        sql = string.Format(sql, domain, wlhno, txt_301code.Text, Convert.ToSingle(txt_BaseRate.Text), Convert.ToSingle(txt_301Rate.Text), cmb_immunity.Value, LogUserModel.UserId, LogUserModel.UserName);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        string flag = dt.Rows[0][0].ToString();
        string msg = dt.Rows[0][1].ToString();

        string lsstr = "";
        if (flag == "N")
        {
            lsstr = "layer.alert('确认成功',function(index) {layer.close(index);load_grid();})";

        }
        else
        {
            lsstr = "layer.alert('" + msg + "')";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }


    [WebMethod]
    public static string del_data(string wlh_domain)
    {
        string re_flag = "";
        if (wlh_domain == "")
        {
            re_flag = "请选择要删除的物料号！";
        }
        else
        {
            string[] wlh_domain_arr = wlh_domain.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            string domain = wlh_domain_arr[1]; string wlhno = wlh_domain_arr[0];

            string sql = @"delete Fin_Base_QGSF where wlh='{1}' and domain='{0}'";
            sql = string.Format(sql, domain, wlhno);
            int ln = DbHelperSQL.ExecuteSql(sql);

            if (ln > 0)
            {
                re_flag = "删除成功！";
            }
            else
            {
                re_flag = "删除失败！";
            }
        }
        
        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;
    }

}