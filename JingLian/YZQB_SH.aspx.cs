using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class JingLian_YZQB_SH : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            txt_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txt_time.Text = DateTime.Now.ToString("HH:ss:mm");

            if (Function_Jinglian.Emplogin_query(4, "", "").Rows[0][0].ToString() == "1")
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(3, "", "");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                txt_shift.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_banbie"].ToString();
                txt_name.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_name"].ToString();
                txt_banzu.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_banzhu"].ToString();

            }
            else
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(3, "", "");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                this.txt_gh.Items.Insert(0, new ListItem("", ""));
                txt_gh.BackColor = System.Drawing.Color.Yellow;
            }
            GetContent();
            
        }
        
    }
    private void GetContent()
    {
        string strSQL = "select jc_item,jc_remark from mes_jc_yzquestion where dl='CJ' ";
        DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
        this.GridView1.DataSource = tbl;
        this.GridView1.DataBind();
        GroupCol(GridView1, 0);
    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_shift.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(3, txt_gh.Text, "").Rows[0]["emp_banzhu"].ToString();
      
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {

    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (txt_gh.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择操作人工号！')", true);
            return;
        }
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)this.GridView1.Rows[i].FindControl("cb1")).Checked == false && ((CheckBox)this.GridView1.Rows[i].FindControl("cb2")).Checked==false)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检查项未全部勾选，请确认！')", true);
                return;
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('提交成功！')", true);
        
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {

        
        GetContent();
       
    }

    protected void cb1_CheckedChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

        CheckBox cbox1 = (CheckBox)GridView1.Rows[lnindex].FindControl("cb1");
        CheckBox cbox2 = (CheckBox)GridView1.Rows[lnindex].FindControl("cb2");

        if (cbox1.Checked == true)
        {
            cbox2.Checked = false;
        }
        else
        {
            cbox2.Checked = true;
        }
           
        

    }
    protected void cb2_CheckedChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;

        CheckBox cbox1 = (CheckBox)GridView1.Rows[lnindex].FindControl("cb1");
        CheckBox cbox2 = (CheckBox)GridView1.Rows[lnindex].FindControl("cb2");

        if (cbox2.Checked == true)
        {
            cbox1.Checked = false;
        }
        else
        {
            cbox1.Checked = true;
        }
       
    }

    public static void GroupCol(GridView gridView, int cols)
    {
        if (gridView.Rows.Count < 1 || cols > gridView.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc = gridView.Rows[0].Cells[cols];
        for (int i = 1; i < gridView.Rows.Count; i++)
        {
            TableCell tc = gridView.Rows[i].Cells[cols];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                }
                oldTc.RowSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                oldTc = tc;
            }
        }
    }

}