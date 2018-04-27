using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class shenhe_YZCheck_Detail : System.Web.UI.Page
{

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request["id"].ToString();
            string sql = "select distinct question_neirong from YZJ_Check where question_id='" + id + "'";
            DataTable tbl = DbHelperSQL.Query(sql).Tables[0];
            gvDetail.DataSource = tbl;
            gvDetail.DataBind();

            string sql_solve = "select distinct solve_neirong from YZJ_Check where question_id='" + id + "'";
            DataTable tbl_solve = DbHelperSQL.Query(sql_solve).Tables[0];
            gv_solve.DataSource = tbl_solve;
            gv_solve.DataBind();



            string sql_pic = "select distinct pic_filepath from YZJ_Check where question_id='" + id + "'";
            DataTable tbl_pic = DbHelperSQL.Query(sql_pic).Tables[0];
            gv_pic.DataSource = tbl_pic;
            gv_pic.DataBind();
        }
    }
    protected void ck_OK_CheckedChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

        CheckBox cbox1 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_OK");
        CheckBox cbox2 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NG");
        CheckBox cbox3 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NA");

        if (cbox1.Checked == true)
        {
            cbox2.Checked = false;
            cbox3.Checked = false;
        }

    }
    protected void ck_NG_CheckedChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

        CheckBox cbox1 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_OK");
        CheckBox cbox2 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NG");
        CheckBox cbox3 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NA");

        if (cbox2.Checked == true)
        {
            cbox1.Checked = false;
            cbox3.Checked = false;
        }

    }
    protected void ck_NA_CheckedChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

        CheckBox cbox1 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_OK");
        CheckBox cbox2 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NG");
        CheckBox cbox3 = (CheckBox)gvDetail.Rows[lnindex].FindControl("ck_NA");

        if (cbox3.Checked == true)
        {
            cbox1.Checked = false;
            cbox2.Checked = false;
        }
    }
}