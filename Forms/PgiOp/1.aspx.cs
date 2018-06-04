using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PgiOp_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable ldt = DbHelperSQL.Query(@"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYGS_Dtl_Form] a where 1=0").Tables[0];

        DataTable dt_gx = null;
        dt_gx = DbHelperSQL.Query(@"select ro_op,ro_desc,ro_wkctr from [PGIHR].report.[dbo].qad_ro_det where ro_routing='P0322AA' order by ro_op").Tables[0];// and ro_domain='"+ lsdomain + "'
        for (int i = 0; i < dt_gx.Rows.Count; i++)
        {
            DataRow ldr = ldt.NewRow();
            ldr["typeno"] = "压铸";
            ldr["pgi_no"] = "P0322AA";
            ldr["op"] = "OP" + dt_gx.Rows[i]["ro_op"];
            ldr["numid"] = i;
            ldt.Rows.Add(ldr);
        }
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();
        op_ValueChanged(sender, e);
    }

    protected void gv_d_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Add")
        {
            GvAddRows(1, e.VisibleIndex);
        }
    }

    private void GvAddRows(int lnadd_rows, int lnindex)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);

        DataRow[] drs = ldt.Select("", "numid desc");
        DataTable dt_o = ldt.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt_o.Rows.Add(row.ItemArray);
        }

        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            for (int j = 0; j < ldt.Columns.Count; j++)
            {
                if (ldt.Columns[j].ColumnName == "typeno" || ldt.Columns[j].ColumnName == "pgi_no")
                {
                    ldr[ldt.Columns[j].ColumnName] = ldt.Rows[lnindex][ldt.Columns[j].ColumnName];
                }
                else if (ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = dt_o.Rows.Count <= 0 ? 0 : (Convert.ToInt32(dt_o.Rows[0]["numid"]) + 1);
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.InsertAt(ldr, lnindex + 1);

        }
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();

    }


    protected void op_ValueChanged(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["op"] = ldt.Rows[i]["op"] + "_ce";
        }
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();
    }
}