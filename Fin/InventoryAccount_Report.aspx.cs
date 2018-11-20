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

public partial class Fin_InventoryAccount_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Setddl_site();
        Setddl_p_leibie();

        if (!IsPostBack)
        {
            //StartDate.Text = System.DateTime.Now.ToShortDateString(); //System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToShortDateString();
            //EndDate.Text = System.DateTime.Now.ToShortDateString();
            QueryASPxGridView();
        }
        if (this.gv.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
            ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
        }
        if (this.ASPxGridView1.IsCallback == true) { QueryASPxGridView_1(); }
        if (this.ASPxGridView2.IsCallback == true) { QueryASPxGridView_2(); }
        if (this.ASPxGridView3.IsCallback == true) { QueryASPxGridView_3(); }
        if (this.ASPxGridView4.IsCallback == true) { QueryASPxGridView_4(); }
    }
    public void Setddl_site()
    {
        //string strSQL = @"	SELECT si_site,si_desc,si_site+'【'+si_desc+'】' as si_site_desc  from qad.[dbo].[qad_si_mstr] where si_domain='" + ddl_domain.SelectedValue + "' order by si_site";
        string strSQL = @"	SELECT si_site,si_desc,si_site+'【'+si_desc+'】' as si_site_desc  from [dbo].[qad_si_mstr] where si_domain='" + ddl_domain.SelectedValue + "' order by si_site";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).TextField = "si_site_desc";
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).ValueField = "si_site";
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataBind();
    }
    public void Setddl_p_leibie()
    {
        string strSQL = @"	SELECT pl_prod_line,pl_desc,pl_prod_line+'【'+pl_desc+'】' as pl_line_desc  from qad.[dbo].[qad_pl_mstr] where pl_domain='" + ddl_domain.SelectedValue + "' order by pl_prod_line";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = "pl_line_desc";
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = "pl_prod_line";
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }


    public void QueryASPxGridView()
    {
        ASPxPageControl1.TabPages[0].Text = ddl_typeno.SelectedItem.Text;
        if (ddl_typeno.SelectedValue == "A")
        {
            for (int i = 1; i <= 4; i++)
            {
                ASPxPageControl1.TabPages[i].Visible = false;
            }
        }
        else
        {
            for (int i = 1; i <= 4; i++)
            {
                ASPxPageControl1.TabPages[i].Visible = true;
            }
        }

        DataTable dt = null;
        if (StartDate.Text != "" && EndDate.Text != "")
        {
            dt = DbHelperSQL.Query("exec Report_InventoryA '" + ddl_typeno.SelectedValue + "','" + ddl_domain.SelectedValue + "','" + StartDate.Text + "','" + EndDate.Text
                + "','" + ddl_fuhao2.SelectedValue + "','" + ASPxDropDownEdit2.Text + "','" + ddl_fuhao1.SelectedValue + "','" + ASPxDropDownEdit1.Text + "'").Tables[0];
        }

        gv.Columns.Clear(); gv.TotalSummary.Clear();

        if (ddl_typeno.SelectedValue == "C")
        {
            SetGrid(gv, dt, 90);
        }
        else
        {
            Pgi.Auto.Control.SetGrid("Report_InventoryA", ddl_typeno.SelectedValue, gv, dt);
        }

        if (ddl_typeno.SelectedValue != "A")
        {
            QueryASPxGridView_1();
            QueryASPxGridView_2();
            QueryASPxGridView_3();
            QueryASPxGridView_4();
        }

    }


    public void QueryASPxGridView_1()
    {
        string sql = "";

        DataTable dt1 = null;
        sql = @"select [gl_acct] ,[gl_acct_desc],[gl_zzxs],case when [gl_type]='1' then '借方账号、贷方账号' when [gl_type]='2' then '借方账号' else '' end gl_type 
                    from [dbo].[Fin_GL_type]";
        dt1 = DbHelperSQL.Query(sql).Tables[0];
        ASPxGridView1.DataSource = dt1;
        ASPxGridView1.DataBind();
    }

    public void QueryASPxGridView_2()
    {
        string sql = "";
        DataTable dt2 = null;
        sql = @"select a.[si_site] ,b.[si_desc],b.[si_entity],b.[si_status],a.[si_type] 
                    from [dbo].[Fin_si_mstr_type] a
                        left join dbo.qad_si_mstr b on a.si_domain=b.si_domain and a.si_site=b.si_site
                    where a.si_domain='" + ddl_domain.SelectedValue + @"' order by a.[si_site]";
        dt2 = DbHelperSQL.Query(sql).Tables[0];
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }

    public void QueryASPxGridView_3()
    {
        DataTable dt_site = DbHelperSQL.Query("select si_site from dbo.qad_si_mstr where si_domain='" + ddl_domain.SelectedValue + "' order by si_site").Tables[0];
        ((GridViewDataComboBoxColumn)this.ASPxGridView3.Columns["loc_site"]).PropertiesComboBox.DataSource = dt_site;

        DataTable dt_loc = DbHelperSQL.Query(@"select distinct b.loc_loc 
                                                from qad.dbo.qad_loc_mstr b
                                                    left join [dbo].[Fin_loc_mstr_type] a on a.loc_domain = b.loc_domain and a.loc_site = b.loc_site and a.loc_loc = b.loc_loc
                                                where b.loc_domain = '" + ddl_domain.SelectedValue + @"'
                                                    and b.loc_loc  NOT LIKE('%[A-Z]%')
                                                    and(b.loc_status in('CUSTOMER', 'TRANSIT', 'OVERSEA', 'HOLD', 'NSP', 'SUB') or b.loc_loc like '4%')
                                                order by b.loc_loc").Tables[0];
        ((GridViewDataComboBoxColumn)this.ASPxGridView3.Columns["loc_loc"]).PropertiesComboBox.DataSource = dt_loc;

        string sql = "";
        DataTable dt3 = null;
        /*sql = @"select loc_site+loc_loc loc_site_loc,loc_site,loc_loc,loc_desc,loc_status
                    ,case when loc_loc like '4%' then '压铸线边库' 
                        when loc_status='CUSTOMER' then '寄售库' when loc_status='HOLD' then '中转库' when loc_status='NSP' then '内部交易' when loc_status='TRANSIT' then '在途库'
                         when loc_status='OVERSEA' then '中转库' when loc_status='SUB' then '外协加工库' else '' end loc_type						
                from qad.dbo.qad_loc_mstr
                where loc_status in('CUSTOMER','TRANSIT','OVERSEA','HOLD','NSP','SUB') and loc_domain='" + ddl_domain.SelectedValue + @"'";*/
        sql = @"select a.loc_site+'|'+a.loc_loc loc_site_loc,a.loc_domain,a.loc_site,a.loc_loc,b.loc_desc,b.loc_status,a.loc_type,a.part_is_r 
                    from [dbo].[Fin_loc_mstr_type] a
                        left join  qad.dbo.qad_loc_mstr b on a.loc_domain=b.loc_domain and a.loc_site=b.loc_site and a.loc_loc=b.loc_loc
                    where a.loc_domain='" + ddl_domain.SelectedValue + @"' order by a.part_is_r,a.loc_site,a.loc_loc";
        dt3 = DbHelperSQL.Query(sql).Tables[0];
        ASPxGridView3.DataSource = dt3;
        ASPxGridView3.DataBind();
    }

    public void QueryASPxGridView_4()
    {
        string sql = "";
        DataTable dt4 = null;
        sql = @"select da.pl_prod_line,db.pl_desc,da.pl_type 
					from Fin_pl_mstr_type da 
						left join qad.[dbo].[qad_pl_mstr] db on da.pl_domain=db.pl_domain and da.pl_prod_line=db.pl_prod_line
                    where da.pl_domain='" + ddl_domain.SelectedValue + @"' order by da.pl_prod_line";
        dt4 = DbHelperSQL.Query(sql).Tables[0];
        ASPxGridView4.DataSource = dt4;
        ASPxGridView4.DataBind();
    }

    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        gv_export.WriteXlsxToResponse("库存会计事务_" + ddl_typeno.SelectedItem.Text + "_" + ddl_domain.SelectedValue + "_" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    private static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw)
    {
        if (ldt_data == null)
        {
            return;
        }

        lgrid.AutoGenerateColumns = false;
        int lnwidth = 0; 
        lgrid.Columns.Clear();
        for (int i = 0; i < ldt_data.Columns.Count; i++)
        {
            string colname = ldt_data.Columns[i].ColumnName.ToString();
            int lnwidth_out = 0;
            GridViewDataTextColumn lcolumn = create_col(colname, i, lnwidth, out lnwidth_out, lnw);
            lnwidth = lnwidth_out;

            if (colname.Contains("|tr_qty_loc_2"))
            {
                GridViewBandColumn band_lcolumn = new GridViewBandColumn();
                band_lcolumn.Caption = colname.Substring(0, colname.IndexOf('|'));
                band_lcolumn.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                band_lcolumn.Columns.Add(lcolumn);

                string colname_2 = ldt_data.Columns[i + 1].ColumnName.ToString();
                int lnwidth_out_2 = 0;
                GridViewDataTextColumn lcolumn_2 = create_col(colname_2, i + 1, lnwidth, out lnwidth_out_2, lnw);
                lnwidth = lnwidth_out_2;

                band_lcolumn.Columns.Add(lcolumn_2);

                lgrid.Columns.Add(band_lcolumn);

                if (colname_2.Contains("|trgl_gl_amt_1"))
                {
                    ASPxSummaryItem sumcolumn = new ASPxSummaryItem();
                    sumcolumn.FieldName = colname_2;
                    sumcolumn.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    sumcolumn.DisplayFormat = "{0:N0}";
                    lgrid.TotalSummary.Add(sumcolumn);
                }

                i++;
            }
            else
            {
                lgrid.Columns.Add(lcolumn);
            }

            if (colname.Contains("|tr_qty_loc_2") || colname.Contains("|trgl_gl_amt_1") || colname == "sum_tr_qty_loc_2" || colname == "sum_trgl_gl_amt_1")
            {
                ASPxSummaryItem sumcolumn = new ASPxSummaryItem();
                sumcolumn.FieldName = colname;
                sumcolumn.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                sumcolumn.DisplayFormat = "{0:N0}";
                lgrid.TotalSummary.Add(sumcolumn);
            }
        }

        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();
    }

    public static GridViewDataTextColumn create_col(string colname,int i,int lnwidth, out int lnwidth_out, Int32 lnw)
    {

        GridViewDataTextColumn lcolumn = new GridViewDataTextColumn();
        lcolumn.Name = colname;
        if (colname.Contains("|tr_qty_loc_2"))
        {
            lcolumn.Caption = "求和项:库位数量更改修正2";
        }
        else if (colname.Contains("|trgl_gl_amt_1"))
        {
            lcolumn.Caption = "求和项:金额修正";
        }
        else if (colname == "sum_tr_qty_loc_2")
        {
            lcolumn.Caption = "求和项:库位数量更改修正2汇总";
        }
        else if (colname == "sum_trgl_gl_amt_1")
        {
            lcolumn.Caption = "求和项:金额修正汇总";
        }
        else
        {
            lcolumn.Caption = colname;
        }
        lcolumn.FieldName = colname;
        lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
        lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
        if (colname.Contains("|") || colname.Contains("sum_"))
        {
            lcolumn.PropertiesEdit.DisplayFormatString = "{0:N0}";
        }
        int lnwidth_emp = 0;
        if (colname == "产品类名称") { lnwidth_emp = 150; }
        if (colname == "物料描述") { lnwidth_emp = 150; }
        if (colname.Contains("|tr_qty_loc_2")) { lnwidth_emp = 100; }

        //if (colname == "地点分类" || colname == "地点" || colname == "库位代码" || colname == "库位描述" || colname == "产品类"
        //     || colname == "产品类名称" || colname == "产品类_物流归类" || colname == "物料号" || colname == "物料描述")
        //{
        //    lcolumn.GroupIndex = i;
        //}

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

        lnwidth_out = lnwidth;

        return lcolumn;

    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #region ASPxGridView3 编辑

    protected void ASPxGridView3_ToolbarItemClick(object sender, ASPxGridToolbarItemClickEventArgs e)
    {
        QueryASPxGridView_3();
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
        //ASPxGridView grid = (ASPxGridView)sender;
        //if (!grid.IsEditing || e.Column.FieldName != "loc_loc") return;
        //if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;
        //object val = grid.GetRowValuesByKeyValue(e.KeyValue, "loc_site");
        //if (val == DBNull.Value) return;
        //string site = (string)val;

        //ASPxComboBox combo = e.Editor as ASPxComboBox;
        //FillLocCombo(combo, site);

        //combo.Callback += new CallbackEventHandlerBase(cmbLoc_OnCallback);

        ASPxGridView grid = (ASPxGridView)sender;
        if (!grid.IsEditing) return;

        if (e.Column.FieldName == "loc_site")//地点
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;

            if (e.KeyValue != DBNull.Value && e.KeyValue != null)
            {
                //combo.Enabled = false;//编辑时，只读
                combo.ClientEnabled = false;
            }
        }
        if (e.Column.FieldName == "loc_loc")//库位
        {
            object val = grid.GetRowValuesByKeyValue(e.KeyValue, "loc_site");
            string site = (string)val;

            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillLocCombo(combo, site);

            combo.Callback += new CallbackEventHandlerBase(cmbLoc_OnCallback);

            if (e.KeyValue != DBNull.Value && e.KeyValue != null)
            {
                //combo.Enabled = false;//编辑时，只读
                combo.ClientEnabled = false;
            }
        }



    }

    void cmbLoc_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillLocCombo(source as ASPxComboBox, e.Parameter);
    }

    protected void FillLocCombo(ASPxComboBox cmb, string site)
    {
        if (string.IsNullOrEmpty(site)) return;

        cmb.Items.Clear();
        // a.loc_loc is null -->筛选出 不存在的库位
        string sql = @"select b.loc_loc 
                    from qad.dbo.qad_loc_mstr b
                        left join [dbo].[Fin_loc_mstr_type] a on a.loc_domain=b.loc_domain and a.loc_site=b.loc_site and a.loc_loc=b.loc_loc
                    where b.loc_domain='{0}' and b.loc_site like '%{1}%' 
                        and a.loc_loc is null
                        and b.loc_loc  NOT LIKE ('%[A-Z]%')
                        and (b.loc_status in('CUSTOMER','TRANSIT','OVERSEA','HOLD','NSP','SUB') or b.loc_loc like '4%') 
	                    order by b.loc_loc";
        sql = string.Format(sql, ddl_domain.SelectedValue, site);
        DataTable dt_loc = DbHelperSQL.Query(sql).Tables[0];

        foreach (DataRow dr in dt_loc.Rows)
            cmb.Items.Add(dr["loc_loc"].ToString());
    }

    protected void ASPxGridView3_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        foreach (GridViewColumn column in grid.Columns)
        {
            GridViewDataColumn dataColumn = column as GridViewDataColumn;
            if (dataColumn == null) continue;
            if (dataColumn.FieldName == "loc_desc" || dataColumn.FieldName == "loc_status") continue;
            if (e.NewValues[dataColumn.FieldName] == null)
            {
                e.Errors[dataColumn] = "Value can't be null.";
            }
        }
        if (e.Errors.Count > 0) e.RowError = "Please, fill all fields.";
    }

    protected void ASPxGridView3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string loc_site_loc = e.Keys["loc_site_loc"].ToString();
        string loc_site = e.NewValues["loc_site"].ToString();
        string loc_loc = e.NewValues["loc_loc"].ToString();
        string loc_type = e.NewValues["loc_type"].ToString();
        string part_is_r = e.NewValues["part_is_r"].ToString();

        string sql_update = @"update Fin_loc_mstr_type set [loc_type]='{3}',[part_is_r]='{4}' where loc_domain='{0}' and loc_site='{1}' and loc_loc='{2}'";
        sql_update = string.Format(sql_update, ddl_domain.SelectedValue, loc_site, loc_loc, loc_type, part_is_r);
        DbHelperSQL.ExecuteSql(sql_update);

        QueryASPxGridView_3();

        ASPxGridView3.CancelEdit();
        e.Cancel = true;


    }

    protected void ASPxGridView3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string loc_site = e.NewValues["loc_site"].ToString();
        string loc_loc = e.NewValues["loc_loc"].ToString();
        string loc_type = e.NewValues["loc_type"].ToString();
        string part_is_r = e.NewValues["part_is_r"].ToString();


        string sql_insert = @"insert into Fin_loc_mstr_type(loc_domain,loc_site,loc_loc,loc_type,part_is_r)
                                      values('{0}','{1}','{2}','{3}','{4}')";
        sql_insert = string.Format(sql_insert, ddl_domain.SelectedValue, loc_site, loc_loc, loc_type, part_is_r);
        DbHelperSQL.ExecuteSql(sql_insert);

        QueryASPxGridView_3();

        ASPxGridView3.CancelEdit();
        e.Cancel = true;

    }

    protected void ASPxGridView3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string loc_site_loc = e.Keys["loc_site_loc"].ToString();

        string loc_site = loc_site_loc.Substring(0, loc_site_loc.IndexOf('|'));
        string loc_loc = loc_site_loc.Substring(loc_site_loc.IndexOf('|') + 1);

        string sql_del = @"delete Fin_loc_mstr_type where loc_domain='{0}' and loc_site='{1}' and loc_loc='{2}'";
        sql_del = string.Format(sql_del, ddl_domain.SelectedValue, loc_site, loc_loc);
        DbHelperSQL.ExecuteSql(sql_del);

        QueryASPxGridView_3();

        ASPxGridView3.CancelEdit();
        e.Cancel = true;

    }

    #endregion


}