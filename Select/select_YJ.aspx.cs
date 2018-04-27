using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Select_select_XMH : System.Web.UI.Page
{
    YJ_CLASS YJ_CLASS = new YJ_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request["ljh"] != null & Request["domain"] != null)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Lab_ljh.Text = Request["ljh"];
            Lab_Domain.Text = Request["domain"];
            if (!IsPostBack)
            {                // GetData();
                Get_PGIXMH(Lab_ljh.Text.Substring(0, 5) , Lab_Domain.Text);
            }
        }
        else
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        Panel2.Visible = true;
        GetData();
       
    }
    public void Get_PGIXMH(string ld_part2, string ld_domain)
    {
        DataTable dtkcl = YJ_CLASS.Getkcl("", ld_part2, ld_domain, 4, "");
        if (dtkcl.Rows.Count != 0)
        {
            Rab_list.DataSource = YJ_CLASS.Getkcl("",ld_part2, ld_domain, 4, "");
            Rab_list.DataValueField = "pt_part";
            Rab_list.DataTextField = "KCL2";
            Rab_list.DataBind();
        }
        else
        {


        }

    }
    private void GetData()
    {
        if (txt_xmh.Text == ""&& txt_fhz.Text=="")
        {
           
            lb_msg.Text = "物料号和发货至不能都为空！";
        }
        else
        {
            DataTable dt = new DataTable();
            if (DDL.SelectedValue == "1")
            {
                dt = YJ_CLASS.Getxmh(txt_xmh.Text, "", txt_fhz.Text, "");
            }
            if (DDL.SelectedValue == "2")
            {
                dt = YJ_CLASS.Getxmh("", txt_xmh.Text, txt_fhz.Text, "");
            }
            if (dt == null || dt.Rows.Count <= 0)
            {
                lb_msg.Text = "No Data Found!";
                //return;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DFE7DF';");
            e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor=currentcolor;");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string xmh = GridView1.SelectedRow.Cells[0].Text.Trim();
        //string ljh = GridView1.SelectedRow.Cells[1].Text.Trim();
        //string gxh = GridView1.SelectedRow.Cells[3].Text.Trim();
        string CP_ID = GridView1.SelectedRow.Cells[0].Text.Trim();


        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        string temp = "<script>window.opener.XMH_setvalue('form1','" + CP_ID + "');</script>";
        Response.Write(temp.Trim());
    }

    protected void Btnqx(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>window.opener=null;window.close();</script>");
    }

    protected void Btnqr_Click(object sender, EventArgs e)
    {
        if (Rab_list.SelectedValue == "")
        {
            Lab_ts.Text = "请选择产品版本！";
        }
        else
        {
            if (Rab_list.SelectedValue == Lab_ljh.Text)
            {
                Lab_ts.Text = "你选择的是一样的产品版本状态！";

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>window.opener=null;window.close();</script>");
            }
            else
            {
                Lab_ts.Text = "";
                Panel2.Visible = true;
                txt_xmh.Text = Rab_list.SelectedValue;
            }
        }
    }

    
}