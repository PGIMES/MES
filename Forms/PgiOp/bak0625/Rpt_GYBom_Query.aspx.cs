﻿using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


public partial class Forms_PgiOp_Rpt_GYBom_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();

    }

    public void QueryASPxGridView()
    {
      
        GYGS GYGS = new GYGS();
        gv_BOM.Visible = false;
        gv.Visible = false;
        DataTable dt = DbHelperSQL.Query("exec  Rpt_GYBom_Query '" + txt_part.Text + "','"+ddl_comp .SelectedValue+"','"+ddl_type.SelectedValue+"'").Tables[0];
        if (ddl_type.SelectedValue == "BOM")
        {
            gv_BOM.Visible = true;
            gv_BOM.DataSource = dt;
            gv_BOM.DataBind();
        }
        else
        {
            gv.Visible = true;
            gv.DataSource = dt;
            gv.DataBind();
        }

       
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ASPxGridViewExporter1.WriteXlsToResponse(ddl_type.SelectedValue+ System.DateTime.Now.ToShortDateString());//导出到Excel

    }
    protected void gv_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}