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
        string lsdomain = "200"; string lspotype = ""; string lsbuyername = "";
        if (Request.QueryString["domain"] != null)
        {
            lsdomain = Request.QueryString["domain"];
        }
        if (Request.QueryString["potype"] != null)
        {
            lspotype = Request.QueryString["potype"];
        }
        if (Request.QueryString["buyername"] != null)
        {
            lsbuyername = Request.QueryString["buyername"];
            if (lsbuyername.IndexOf('|')>0)
            {
                lsbuyername = lsbuyername.Substring(0, lsbuyername.IndexOf('|'));
            }
        }
        if (!IsPostBack)
        {
            DataTable ldt = GetData(lsdomain, lspotype, lsbuyername);
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PR_SELECT_New_all", this.gv, ldt,2);
        }
        else
        {
            DataTable ldt = GetData(lsdomain, lspotype, lsbuyername);
            this.gv.DataSource = ldt;
            this.gv.DataBind();
        }
    }


    private DataTable GetData(string lsdomain, string lspotype, string lsbuyername)
    {
        string lssql = "";
        lssql = @"exec Pur_Po_GetPrListByPerson '{0}','{1}','{2}'";
        lssql = string.Format(lssql, lsdomain, lspotype, lsbuyername);

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