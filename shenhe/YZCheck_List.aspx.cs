using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class shenhe_YZCheck_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "select pic_filepath,pic_id from YZJ_Check where type='B'";
        DataTable tbl = DbHelperSQL.Query(sql).Tables[0];
        GridView1.DataSource = tbl;
        GridView1.DataBind();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Write("<script>window.open('YZ_Check.aspx');</script>");
    }
}