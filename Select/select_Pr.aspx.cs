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
        string lsdomain = "200";
        if (Request.QueryString["domain"] != null)
        {
            lsdomain = Request.QueryString["domain"];
        }
        if (!IsPostBack)
        {


            //string lssql = "select pr.*,'0' as taxrate,pt_status from PUR_PR_Dtl_Form pr left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno";
            //lssql += " inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain";
            //lssql += " where domain='" + lsdomain + "' and pr.status=0 and pr_main.iscomplete='1' and (pt_status<>'OBS' and pt_status<>'DEAD')";
            //lssql += " order by pr.prno,pr.rowid";
            DataTable ldt = GetData(lsdomain);
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PR_SELECT", this.gv, ldt,2);
        }
        else
        {

            //string lssql = "select pr.*,'0' as taxrate,pt_status from PUR_PR_Dtl_Form pr left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno";
            //lssql += " inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain";
            //lssql += " where domain='" + lsdomain + "' and pr.status=0 and pr_main.iscomplete='1' and (pt_status<>'OBS' and pt_status<>'DEAD')";
            //lssql += " order by pr.prno,pr.rowid";
            DataTable ldt = GetData(lsdomain);
            this.gv.DataSource = ldt;
            this.gv.DataBind();

           // Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PR_SELECT", this.gv, ldt,2);
            //for (int i = 0; i < ldt.Rows.Count; i++)
            //{
            //    if (ldt.Rows[i]["SelectAll"].ToString() == "1")
            //    {
            //        ((CheckBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["SelectAll"], "txtcb")).Checked = true;
            //    }
            //}
        }
    }


    private DataTable GetData(string lsdomain)
    {
        string lssql = "select pr.*,'0' as taxrate,pt_status from PUR_PR_Dtl_Form pr left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno";
        lssql += " inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain";
        lssql += " where domain='" + lsdomain + "' and pr.status=0 and pr_main.iscomplete='1' and (pt_status<>'OBS' and pt_status<>'DEAD')";
        lssql += " order by pr.prno,pr.rowid";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
        return ldt;
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