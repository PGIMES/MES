using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class Select_select_Pr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable ldt = DbHelperSQL.Query("select *,17 as TaxRate from PUR_PR_Dtl_Form ").Tables[0];
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PR_SELECT", this.gv, ldt,2);
        }
        else
        {

            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PR_SELECT", this.gv, ldt,2);
            //for (int i = 0; i < ldt.Rows.Count; i++)
            //{
            //    if (ldt.Rows[i]["SelectAll"].ToString() == "1")
            //    {
            //        ((CheckBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["SelectAll"], "txtcb")).Checked = true;
            //    }
            //}
        }
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = ldt.Rows.Count-1; i >=0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString()!="1")
            {
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        Session["pr_select"] = ldt;
        Response.Write("<script> var index = parent.layer.getFrameIndex(window.name);parent.grid.PerformCallback();parent.layer.close(index);</script>");

    }
}