using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_PUR_PO_Dtl_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

        QueryASPxGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    public void QueryASPxGridView()
    {
        string sql = "select * from PUR_PO_CLASS order by ms_code,id";

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART2.Visible = true;
        GV_PART2.DataSource = dt;
        GV_PART2.DataBind();
    }


}