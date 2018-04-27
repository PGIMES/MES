using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class shenhe_YZGX_XJ : System.Web.UI.Page
{
    SheHe_XJ SheHe_XJ = new SheHe_XJ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BaseFun fun = new BaseFun();
            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");

            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            if (tbl.Rows.Count > 0)
            {
                fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
                txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString();
                txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString();
                txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString();
            }

            string strSQL2 = "select * from MES_Equipment where equip_no='" + Request["deviceid"] + "' and equip_type='压铸机'";
            DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
            if (tbl2.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl2.Rows)
                {
                    txtSheBeiHao.Value = dr["equip_no"].ToString();//.Field["equip_no"];
                    txtSheBeiJianCheng.Value = dr["equip_name"].ToString();


                }
            }
            string strSQL1 = "select distinct part from [dbo].[MES_YZ_MoJu] where  equip_no='" + Request["deviceid"] + "' and status=0 ";
            DataTable tbl1 = DbHelperSQL.Query(strSQL1).Tables[0];
            if (tbl1.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl1.Rows)
                {
                    txtljh.Value = dr["part"].ToString();

                }
            }

            string SQL = "select moju_no from [dbo].[MES_YZ_MoJu] where   equip_no='" + Request["deviceid"] + "' and status=0 ";
            DataTable tbl3 = DbHelperSQL.Query(SQL).Tables[0];

            if (tbl3.Rows.Count > 0)
            {
                fun.initDropDownList(dropmoju_no, tbl3, "moju_no", "moju_no");
            }

            ShowGrid();
        }
    }
    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ShowGrid()
    {
        string sql = "select * from [dbo].MES_XJ_Record where cast(jcdatetime as date)='" + txtRiQi.Value + "' ORDER BY jcdatetime";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btn_shoujian_Click(object sender, EventArgs e)
    {
        //首件记录生成后，生成一系列的点巡检记录
        DataTable dt = SheHe_XJ.GetXJList(1, dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, txtljh.Value, dropmoju_no.SelectedValue,"首件", txtRiQi.Value, txtShiJian.Value, "",0);
        if (dt.Rows[0][0].ToString() == "Y")
        {
            string str = "layer.confirm('当天首件已生成，是否重新执行首件作业？', {  btn: ['是','否'] }, function(){ $('#MainContent_btnNext').click(); }, function(){  });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", str, true);
        }
        else
        {
            btnNext_Click(sender, e);
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DataTable dt = SheHe_XJ.GetXJList(2, dropGongHao.SelectedValue, txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, txtljh.Value, dropmoju_no.SelectedValue, "首件", txtRiQi.Value, txtShiJian.Value, "",0);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btn_check_Click(object sender, EventArgs e)
    {

        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string mojuno = GridView1.Rows[lnindex].Cells[3].Text.ToString();
        string jclb = GridView1.Rows[lnindex].Cells[1].Text.ToString();
        string id = GridView1.Rows[lnindex].Cells[6].Text.ToString();
        string isok = GridView1.Rows[lnindex].Cells[7].Text.ToString();
        string time = GridView1.Rows[lnindex].Cells[0].Text.ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", " layer.open({    type: 2,    title: '点检',    shadeClose: true,    shade: 0.8,    area: ['900px', '650px']," +
                                                                                                                "  content: 'YZGX_XJ_Detail.aspx?mojuno=" + mojuno + "&id=" + id + "&jclb=" + jclb + "&uid=" + dropGongHao.SelectedValue + "&isok=" + isok + "&timer=" + time + "'" +
                                                                                                              "})", true);
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string mojuno = GridView1.Rows[lnindex].Cells[3].Text.ToString();
        string jclb = GridView1.Rows[lnindex].Cells[1].Text.ToString();
        string id = GridView1.Rows[lnindex].Cells[6].Text.ToString();
        string isok = GridView1.Rows[lnindex].Cells[7].Text.ToString();
        string time = GridView1.Rows[lnindex].Cells[0].Text.ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", " layer.open({    type: 2,    title: '点检',    shadeClose: true,    shade: 0.8,    area: ['900px', '650px']," +
                                                                                                                "  content: 'YZ_XJ_BZ.aspx?mojuno=" + mojuno + "&id=" + id + "&jclb=" + jclb + "&uid=" + dropGongHao.SelectedValue + "&isok=" + isok + "&timer=" + time + "'" +
                                                                                                              "})", true);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = e.Row.Cells[7].Text.ToString().TrimEnd();
            DateTime jcdatetime =Convert.ToDateTime( e.Row.Cells[0].Text.ToString());
            if (status == "Y")
            {
                e.Row.BackColor = System.Drawing.Color.Gray;
            }
            if (jcdatetime < System.DateTime.Now  && status!="Y")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
           
        }
    }
}