using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class GongCheng_Product_GroupMatain : System.Web.UI.Page
{
    Function_DJ DJ = new Function_DJ();
    SQLHelper SQLHelper = new SQLHelper();
    BaseFun fun = new BaseFun();
    string strsql = "select distinct DJ_Group from Mes_DJ_ProGroup where IsDel='N'";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Session["empid"] == null)
            {
                InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
            }
            Getpro();
            DataTable dt_Group = DJ.GetDJ_Group_XM(1, ddl_site.SelectedValue, txt_ProGroup.Text, "");
            DataTable dt_xm = DJ.GetDJ_Group_XM(2, ddlcomp.SelectedValue, ddlproduct.SelectedValue, txt_xm.Value);
            if (dt_Group.Rows.Count == 0)
            {
                Panel1.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
                bind_Group();
            }
            if (dt_xm.Rows.Count == 0)
            {
                Panel2.Visible = false;
            }
            else
            {
                bind_xmh();
            }
        }
    }

    public int  Check_Sam_Product(string comp,string product)
    {
        int jg = 0;
        string sql = "select top 1 * from [Mes_DJ_ProGroup] where comp='" + comp + "' and DJ_Group='" + product + "' and IsDel='N' ";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        if (dt.Rows.Count > 0){  jg = 1;}
        return jg;
    }
    public int Check_Sam_XM(string comp,string product,string xmh)
    {
        int jg = 0;
        string sql = "select top 1 * from [MES_DJ_XMGroup] where comp='" + comp + "' and DJ_Group='" + product + "' and xmh='" + xmh + "'  and IsDel='N'";
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        if (dt.Rows.Count > 0) { jg = 1; }
        return jg;
    }
    public  void Getpro()
    {
        DataSet Product = DbHelperSQL.Query(strsql);
            fun.initDropDownList(ddlproduct, Product.Tables[0], "DJ_Group", "DJ_Group");
            this.ddlproduct.Items.Insert(0, new ListItem("", ""));
    }
    protected void btn_Group_confirm_Click(object sender, EventArgs e)
    {
        if (txt_ProGroup.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组不可为空！')", true);
            return;
        }
        string comp = ddl_site.SelectedValue;
        string uid="";
        string product = txt_ProGroup.Text;
        if (Session["empid"] != null)
        {
             uid = Session["empid"].ToString();
        }
       int jg= Check_Sam_Product(comp, product);
       if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组,请确认！')", true); return; }
        int result=0;
        result = DJ.DJ_Group_XM_Insert(1, comp, product, "", uid,0,"");
        if (result>0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组类别维护成功！')", true);
             bind_Group();
             Getpro();
        }
    }
    protected void gv_Group_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Group.EditIndex = e.NewEditIndex;
        bind_Group();
    }
    public void bind_Group()
    {
       // Panel1.Visible = true;
       // Panel2.Visible = false;
        DataTable dt_Group = DJ.GetDJ_Group_XM(1, ddl_site.SelectedValue, txt_ProGroup.Text, "");
        gv_Group.DataSource = dt_Group;
        gv_Group.DataKeyNames = new string[] { "id" };
        gv_Group.DataBind();
    }
    public void bind_xmh()
    {
        Panel2.Visible = true;
        DataTable dt_xm = DJ.GetDJ_Group_XM(2, ddlcomp.SelectedValue, ddlproduct.SelectedValue, txt_xm.Value);
        gv_xmh.DataSource = dt_xm;
        gv_xmh.DataKeyNames = new string[] { "id" };
        gv_xmh.DataBind();
        for (int i = 0; i < gv_xmh.Rows.Count; i++)
        {
            DataTable dt = DbHelperSQL.Query(strsql).Tables[0];
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataSource =dt;
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataTextField = "DJ_Group";
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataValueField = "DJ_Group";
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataBind();
            ((DropDownList)gv_xmh.Rows[i].FindControl("ddl_prod")).Items.Insert(0, new ListItem("", ""));
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Text = dt_xm.Rows[i]["DJ_Group"].ToString();
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).CssClass = "form-control input-s-sm";
            ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Enabled = false;
        }
    }
    protected void gv_Group_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string comp = ((TextBox)(gv_Group.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        string product = ((TextBox)(gv_Group.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        int id =Convert.ToInt16( gv_Group.DataKeys[e.RowIndex].Value.ToString());
        string uid = "";
        if (Session["empid"] != null)
        {
            uid = Session["empid"].ToString();
        }
        int jg = Check_Sam_Product(comp, product);
        if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组,请确认！')", true); return; }
       
        int result= 0;
        result = DJ.DJ_Group_XM_Insert(11, comp, product, "", uid, id,"");
        gv_Group.EditIndex = -1;
        bind_Group();
    }
    protected void gv_Group_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        int id = Convert.ToInt16(gv_Group.DataKeys[e.RowIndex].Value.ToString());
        string uid = "";
        if (Session["empid"] != null)
        {
            uid = Session["empid"].ToString();
        }
        int result = 0;
        result = DJ.DJ_Group_XM_Insert(21, "", "", "", uid, id,"");
        bind_Group();
    }
    protected void gv_Group_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_Group.EditIndex = -1;

        bind_Group();
    }
  
    protected void btn_xm_confirm_Click(object sender, EventArgs e)
    {
        if (txt_xm.Value == "" || ddlproduct.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组和项目号必须选择！')", true);
            return;
        }
        string comp = ddlcomp.SelectedValue;
        string uid = "";
        string product = ddlproduct.SelectedValue;
        string xmh = txt_xm.Value;
        string bzdj = txt_bzdj.Value;
        if (Session["empid"] != null)
        {
            uid = Session["empid"].ToString();
        }
        int jg = Check_Sam_XM(comp, product,xmh);
        if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组及项目,请确认！')", true); return; }
        int result = 0;
        result = DJ.DJ_Group_XM_Insert(2, comp, product, xmh, uid, 0,xmh);
        if (result > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组项目类别维护成功！')", true);
            bind_xmh();
        }

    }
    protected void gv_xmh_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_xmh.EditIndex = e.NewEditIndex;

        bind_xmh();
        int lnindex = e.NewEditIndex;
        int i = lnindex;
        ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).CssClass = "form-control input-s-sm  ";
        ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Enabled = true;
    }
    protected void gv_xmh_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string comp = ((TextBox)(gv_xmh.Rows[e.RowIndex].Cells[1].Controls[1])).Text.ToString().Trim();
        string xmh = ((TextBox)(gv_xmh.Rows[e.RowIndex].Cells[2].Controls[1])).Text.ToString().Trim();
        string product = ((DropDownList)gv_xmh.Rows[e.RowIndex].FindControl("ddl_prod")).SelectedValue;
        int id = Convert.ToInt16(gv_xmh.DataKeys[e.RowIndex].Value.ToString());
        string uid = "";
        if (Session["empid"] != null)
        {
            uid = Session["empid"].ToString();
        }
        int jg = Check_Sam_Product(comp, product);
        if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组及项目,请确认！')", true); return; }
        int result = 0;
        result = DJ.DJ_Group_XM_Insert(12, comp, product, xmh, uid, id,"");
        gv_xmh.EditIndex = -1;
        bind_xmh();
    }

    protected void gv_xmh_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_xmh.EditIndex = -1;

        bind_xmh();
    }
    protected void gv_xmh_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt16(gv_xmh.DataKeys[e.RowIndex].Value.ToString());
        string uid = "";
        if (Session["empid"] != null)
        {
            uid = Session["empid"].ToString();
        }
        int result = 0;
        result = DJ.DJ_Group_XM_Insert(22, "", "", "", uid, id,"");
        bind_xmh();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bind_xmh();
       
    }
    protected void btn_Group_query_Click(object sender, EventArgs e)
    {
        bind_Group();
    }
}