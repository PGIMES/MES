using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Fin_Fin_RG_GZDetail : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        string year = Request.QueryString["year"];
        string comp = Request.QueryString["comp"];
        string mnth = Request.QueryString["month"];
        string company = comp == "200" ? "KS" : "SH";
        lblName.Text = year + "/" + mnth + " " + company + "人工过账明细";
        string strSQL = @"	select year as 年度,mnth As 月份,domain As 公司, postingVoucher as 发票号,fpcy as 发票价格差异 from Posting_FaPiao_Diff  
                       where item not in (select pt_part from qad.dbo.qad_pt_mstr)
                       and  year='" + year + "' and domain='" + comp + "' and mnth='" + mnth + "'  ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        gvDetail.DataSource = dt;
        gvDetail.DataBind();
    }
}