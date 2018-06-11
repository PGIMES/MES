using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PgiOp_GYGS_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Getuser(string lsdept)
    {
        string sql = @"select distinct product_user--right(product_user,len(product_user)-charindex('-',product_user)) as product_user 
                        from form3_Sale_Product_MainTable a
                        where a.product_user<>'' and  left(a.product_user,5) in (select workcode from HRM_EMP_MES where (departmentname='" + lsdept + "' or dept_name='" + lsdept + "'))";
        string result = "[";
        DataTable ldt = DbHelperSQL.Query(sql).Tables[0];
        if (ldt.Rows.Count > 0)
        {
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                result = result + "{\"value\":\"" + ldt.Rows[i][0].ToString() + "\"},";
            }
        }
        result = result.TrimEnd(',') + "]";
        return result;

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        txt_pgi_no.Text = "A1";

    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        txt_pgi_no.Text = "A";

    }
}