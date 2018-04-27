using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class shenhe_YZGX_XJ_Detail : System.Web.UI.Page
{
    SheHe_XJ SheHe_XJ = new SheHe_XJ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["jclb"] == "首件")
            {
                dropjclb.Items.Clear();
                dropjclb.Items.Add(new ListItem("首件", "首件"));
                dropjclb.CssClass = "form-control input-s-sm ";
                dropjclb.Enabled = false;

            }
          
            if (Request["isok"].TrimEnd() == "Y")
            {
                dropjclb.Items.Clear();
                dropjclb.Items.Add(new ListItem(Request["jclb"], Request["jclb"]));
                dropjclb.CssClass = "form-control input-s-sm ";
                dropjclb.Enabled = false;
                txtsave.Enabled = false;
                txtsave.CssClass = "btn btn-primary disabled";
            }
            ShowGrid();
        }
    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
     
         int lnindex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
         TextBox jcjg1 = (TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"));
         string sxvalue = gvDetail.Rows[lnindex].Cells[3].Text.ToString();
         string xxvalue = gvDetail.Rows[lnindex].Cells[4].Text.ToString();
         TextBox inputtype = (TextBox)(gvDetail.Rows[lnindex].FindControl("input_type"));
         float f = 0;
         float jcjg_1 = 0;
         float sx = 0;
         float xx = 0;
        if (dropjclb.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检测类别必须选择！')", true);
            ((TextBox)gvDetail.Rows[lnindex].FindControl("TextBox1")).Text = "";
            return;
        }

        if (inputtype.Text == "1-输入类型")
        {
            sx = float.Parse(sxvalue);
            xx = float.Parse(xxvalue); 
            if (float.TryParse(jcjg1.Text, out f) == false && jcjg1.Text != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('数据格式不正确！')", true);
                return;
            }
            else if (float.TryParse(jcjg1.Text, out f) == true)
            {
                jcjg_1 = float.Parse(jcjg1.Text);
                if (jcjg_1 >= xx && jcjg_1 <= sx)
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"))).BackColor = System.Drawing.Color.Green;

                }
                else if (jcjg1.Text != "")
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"))).BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"))).BackColor = System.Drawing.Color.Empty;
                }


            }
        }
        else
        {
            if (jcjg1.Text.ToUpper() != "OK")
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"))).BackColor = System.Drawing.Color.Red;
            }
            else
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox1"))).BackColor = System.Drawing.Color.Green;
            }
        }

    }

    protected void ShowGrid()
    {
        if (Request["jclb"] == "首件")
        {
            gvDetail.Columns[5].HeaderText = "首件  " + Request["timer"];
            dropjclb.Enabled = false;
        }
        DataTable dt = SheHe_XJ.GetXJList(3, "", "", "", "", "", "", "", "", dropjclb.SelectedItem.Text, "", "", "",int.Parse(Request["id"]) );
        gvDetail.DataSource = dt;
        gvDetail.DataBind();
        int[] cols = { 0, 1, 2, 3,4 };
        MergGridRow.MergeRow(gvDetail, cols);
       
    }
    protected void dropjclb_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDetail.Columns[7].HeaderText = dropjclb.SelectedItem.Text +"  "+ Request["timer"]; ;
        ShowGrid();
    }
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string inputtype = ((TextBox)e.Row.FindControl("input_type")).Text;
            string jgresult1 = e.Row.Cells[9].Text.ToString().Replace("&nbsp;", "");
            string jgresult2 = e.Row.Cells[10].Text.ToString().Replace("&nbsp;", "");
            string jgresult3 = e.Row.Cells[11].Text.ToString().Replace("&nbsp;", "");
            if(jgresult1!="") ((TextBox)e.Row.FindControl("TextBox1")).BackColor = jgresult1 == "OK" ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            if (jgresult2!= "") ((TextBox)e.Row.FindControl("TextBox2")).BackColor = jgresult2 == "OK" ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            if (jgresult3!= "") ((TextBox)e.Row.FindControl("TextBox3")).BackColor = jgresult3 == "OK" ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            if (inputtype == "0-判断类型" && Request["isok"].TrimEnd()=="N")
            {
                ((TextBox)e.Row.FindControl("TextBox1")).Text = "OK";
                ((TextBox)e.Row.FindControl("TextBox2")).Text = "OK";
                ((TextBox)e.Row.FindControl("TextBox3")).Text = "OK";
                if (e.Row.Cells[4].Text == e.Row.Cells[3].Text)
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[3].ColumnSpan = 2;
                    if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
                    {
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[2].ColumnSpan = 3;


                    }
                }
            }
            else if (inputtype == "0-判断类型")
            {
                if (e.Row.Cells[4].Text == e.Row.Cells[3].Text)
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[3].ColumnSpan = 2;
                    if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
                    {
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[2].ColumnSpan = 3;


                    }
                }
                //e.Row.Cells[2].ColumnSpan = 3;
                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
            }
            if (dropjclb.SelectedValue != "")
            {
                if (dropjclb.SelectedValue == "过程")
                {
                    ((TextBox)e.Row.FindControl("TextBox3")).Visible = false;
                }
            }
           
        }
       else if (e.Row.RowType == DataControlRowType.Header)
       {
           e.Row.Cells[2].ColumnSpan = 3;
           e.Row.Cells[3].Visible = false;
           e.Row.Cells[4].Visible = false;
       }
       
    }
   

    protected void txtsave_Click(object sender, EventArgs e)
    {
        
        string uid=Request["uid"];
        string jg1result = "";
        string jg2result = "";
        string jg3result = "";
        object obj1 = null;

        string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        for (int i = 0; i < gvDetail.Rows.Count; i++)
        {
            string id = Request["id"];
            string xuhao=gvDetail.Rows[i].Cells[0].Text.ToString();
            string jcxm = gvDetail.Rows[i].Cells[1].Text.ToString();
            string bzyq = gvDetail.Rows[i].Cells[2].Text.ToString();
            string pl = gvDetail.Rows[i].Cells[5].Text.ToString();
            string xh = gvDetail.Rows[i].Cells[6].Text.ToString();
            string sx = gvDetail.Rows[i].Cells[3].Text.ToString(); ;
            string xx = gvDetail.Rows[i].Cells[4].Text.ToString();
            string jg1 = ((TextBox)gvDetail.Rows[i].FindControl("TextBox1")).Text.ToString();
            string jg2 = ((TextBox)gvDetail.Rows[i].FindControl("TextBox2")).Text.ToString();
            string jg3 = ((TextBox)gvDetail.Rows[i].FindControl("TextBox3")).Text.ToString();
            string inputtype = ((TextBox)gvDetail.Rows[i].FindControl("input_type")).Text.ToString();
            if ((dropjclb.SelectedValue != "过程" && jg3 == "") || jg1=="" || jg2=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('有空白栏位未填写，请确认！')", true);
                return;
            }
            jg1result = ((TextBox)gvDetail.Rows[i].FindControl("TextBox1")).BackColor == System.Drawing.Color.Red ? "NG" : "OK";
            jg2result = ((TextBox)gvDetail.Rows[i].FindControl("TextBox2")).BackColor == System.Drawing.Color.Red ? "NG" : "OK";
            if (dropjclb.SelectedValue == "过程")
            {
                jg3 = "";
                jg3result = "";
            }
            else
            {
                jg3result = ((TextBox)gvDetail.Rows[i].FindControl("TextBox3")).BackColor == System.Drawing.Color.Red ? "NG" : "OK";
            }

            string sql1 = "insert into mes_xj_checkresult values('" + id + "','" + xuhao + "','" + jcxm + "','" + bzyq + "','" + sx + "','" + xx + "','" + pl + "','" + xh + "','" + jg1 + "','" + jg1result + "','" + jg2 + "','" + jg2result + "','" + jg3 + "','" + jg3result + "','" + dropjclb.SelectedValue + "','" + inputtype + "','" + uid + "','" + now + "','')";
             obj1 = DbHelperSQL.GetSingle(sql1.ToString(), new SqlParameter("", ""));
        }
        if (obj1 == null)
        {
            string sql = "update mes_xj_record set isok='Y' WHERE ID='" + Request["id"] + "' AND JCLB='" + dropjclb.SelectedValue + "'";
            DbHelperSQL.GetSingle(sql.ToString(), new SqlParameter("", ""));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('提交成功！')", true);
            txtsave.Enabled = false;
            txtsave.CssClass = "btn btn-primary disabled";
        }
       
       
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
        TextBox jcjg1 = (TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"));
        string sxvalue = gvDetail.Rows[lnindex].Cells[3].Text.ToString();
        string xxvalue = gvDetail.Rows[lnindex].Cells[4].Text.ToString();
        TextBox inputtype = (TextBox)(gvDetail.Rows[lnindex].FindControl("input_type"));
        float f = 0;
        float jcjg_1 = 0;
        float sx = 0;
        float xx = 0;
        if (dropjclb.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检测类别必须选择！')", true);
            ((TextBox)gvDetail.Rows[lnindex].FindControl("TextBox2")).Text = "";
            return;
        }

        if (inputtype.Text == "1-输入类型")
        {
            sx = float.Parse(sxvalue);
            xx = float.Parse(xxvalue);
            if (float.TryParse(jcjg1.Text, out f) == false && jcjg1.Text != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('数据格式不正确！')", true);
                return;
            }
            else if (float.TryParse(jcjg1.Text, out f) == true)
            {
                jcjg_1 = float.Parse(jcjg1.Text);
                if (jcjg_1 >= xx && jcjg_1 <= sx)
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"))).BackColor = System.Drawing.Color.Green;

                }
                else if (jcjg1.Text != "")
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"))).BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"))).BackColor = System.Drawing.Color.Empty;
                }


            }
        }
        else
        {
            if (jcjg1.Text.ToUpper() != "OK")
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"))).BackColor = System.Drawing.Color.Red;
            }
            else
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox2"))).BackColor = System.Drawing.Color.Green;
            }
        }
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        int lnindex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
        TextBox jcjg1 = (TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"));
        string sxvalue = gvDetail.Rows[lnindex].Cells[3].Text.ToString();
        string xxvalue = gvDetail.Rows[lnindex].Cells[4].Text.ToString();
        TextBox inputtype = (TextBox)(gvDetail.Rows[lnindex].FindControl("input_type"));
        float f = 0;
        float jcjg_1 = 0;
        float sx = 0;
        float xx = 0;
        if (dropjclb.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检测类别必须选择！')", true);
            ((TextBox)gvDetail.Rows[lnindex].FindControl("TextBox3")).Text = "";
            return;
        }

        if (inputtype.Text == "1-输入类型")
        {
            sx = float.Parse(sxvalue);
            xx = float.Parse(xxvalue);
            if (float.TryParse(jcjg1.Text, out f) == false && jcjg1.Text != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('数据格式不正确！')", true);
                return;
            }
            else if (float.TryParse(jcjg1.Text, out f) == true)
            {
                jcjg_1 = float.Parse(jcjg1.Text);
                if (jcjg_1 >= xx && jcjg_1 <= sx)
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"))).BackColor = System.Drawing.Color.Green;

                }
                else if (jcjg1.Text != "")
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"))).BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"))).BackColor = System.Drawing.Color.Empty;
                }


            }
        }
        else
        {
            if (jcjg1.Text.ToUpper() != "OK")
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"))).BackColor = System.Drawing.Color.Red;
            }
            else
            {
                ((TextBox)(gvDetail.Rows[lnindex].FindControl("TextBox3"))).BackColor = System.Drawing.Color.Green;
            }
        }
    }
}