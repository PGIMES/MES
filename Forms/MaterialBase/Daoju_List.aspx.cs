using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Web.Services;

public partial class Daoju_Daoju_List : System.Web.UI.Page
{
    Function_DJ DJ = new Function_DJ();
    SQLHelper SQLHelper = new SQLHelper();
    BaseFun fun = new BaseFun();
    string strsql = "select distinct DJ_Group from Mes_DJ_ProGroup where IsDel='N'";
    protected void Page_Load(object sender, EventArgs e)
    {
        ////  Page.MaintainScrollPositionOnPostBack = true;
        //ScriptManager1.RegisterAsyncPostBackControl(this.btnimport);
        ////ScriptManager1.RegisterAsyncPostBackControl(this.gve2);
        //ScriptManager1.RegisterAsyncPostBackControl(this.gv2);

        if (!IsPostBack)
        {
            DataTable ldt = DbHelperSQL.Query("select distinct  substring(product_user, charindex('-',product_user)+1,LEN(product_user)-charindex('-',product_user)) as product_user from form3_Sale_Product_MainTable where product_user<>''").Tables[0];
            this.txtproduct_user.DataValueField = "product_user";
            this.txtproduct_user.DataTextField = "product_user";
            this.txtproduct_user.DataSource = ldt;
            this.txtproduct_user.DataBind();
            this.txtproduct_user.Items.Insert(0, new ListItem("ALL", ""));
        }

        if (this.gv1.FocusedRowIndex <0)
        {
            this.SetData(0);
        }else
	   {
            this.SetData(this.gv1.FocusedRowIndex);
        }
       
    }

    private void SetData(int lnindex)
    {
        string lssql = "exec [Pgi_Base_DaoJu_Form_Query] '','"+this.txtpgi_no.Text+"','"+this.txtpn.Text+"','"+this.txtdept.Text+"','"+this.txtproduct_user.Text+"','"+this.txtver.Text+ "','" + this.txtdaoju_no1.Text.Trim() + "'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
       
       
        Pgi.Auto.Control.SetGrid("DAOJU", "DAOJU_1", this.gv1, ldt);
        if (this.gv1.VisibleRowCount>0)
        {
            this.gv1.FocusedRowIndex = lnindex;
            this.gv1.GetPreviewText(lnindex);
            if (this.gv1.VisibleRowCount <= lnindex)
            {
                lnindex = 0;
            }
            lblpgi_no.Text = "PGI编号:    " + this.gv1.GetRowValues(lnindex, "pgi_no").ToString();
            lblop.Text = "工序号:    " + this.gv1.GetRowValues(lnindex, "op").ToString();
            lblver.Text = "版本:    " + this.gv1.GetRowValues(lnindex, "ver").ToString();
            string lsdaoju_id= this.gv1.GetRowValues(lnindex, "id").ToString();
            DataTable ldt1 = DbHelperSQL.Query("exec Pgi_Base_DaoJu_Form_Dt_Query20180321 '" + lsdaoju_id + "','" + this.txtshow_type.SelectedValue + "'").Tables[0];
            Pgi.Auto.Control.SetGrid("DAOJU", "DAOJU_2", this.gv2, ldt1, 0);
           
            this.gv2.DataColumns[0].Settings.AllowCellMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gv2.DataColumns[1].Settings.AllowCellMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gv2.DataColumns[2].Settings.AllowCellMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gv2.Settings.VerticalScrollableHeight = (25 * this.gv2.VisibleRowCount);
        }
        else
        {
            this.gv2.DataSource = null;
            this.gv2.DataBind();
            //DevExpress.Web.ASPxSummaryItem li = new DevExpress.Web.ASPxSummaryItem();
            //li.FieldName = "eddjcb";
            //this.gv2.TotalSummary.Add(li);
            lblpgi_no.Text = "PGI编号:    " ;
            lblop.Text = "工序号:    "  ;
            lblver.Text = "版本:    " ;
        }

        ScriptManager.RegisterClientScriptBlock(this.p1, this.GetType(), "RegisterJs", "kz_cook();", true);
       



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
        //DataSet Product = DbHelperSQL.Query(strsql);
        //    fun.initDropDownList(ddlproduct, Product.Tables[0], "DJ_Group", "DJ_Group");
        //    this.ddlproduct.Items.Insert(0, new ListItem("", ""));
    }
    protected void btn_Group_confirm_Click(object sender, EventArgs e)
    {
       // if (txt_ProGroup.Text == "")
       // {
       //     Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组不可为空！')", true);
       //     return;
       // }
       // string comp = ddl_site.SelectedValue;
       // string uid="";
       // string product = txt_ProGroup.Text;
       // if (Session["empid"] != null)
       // {
       //      uid = Session["empid"].ToString();
       // }
       //int jg= Check_Sam_Product(comp, product);
       //if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组,请确认！')", true); return; }
       // int result=0;
       // result = DJ.DJ_Group_XM_Insert(1, comp, product, "", uid,0,"");
       // if (result>0)
       // {
       //     Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组类别维护成功！')", true);
       //      bind_Group();
       //      Getpro();
       // }
    }
    protected void gv_Group_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gv_Group.EditIndex = e.NewEditIndex;
        //bind_Group();
    }
    public void bind_Group()
    {
       // Panel1.Visible = true;
       // Panel2.Visible = false;
        //DataTable dt_Group = DJ.GetDJ_Group_XM(1, ddl_site.SelectedValue, txt_ProGroup.Text, "");
        //gv_Group.DataSource = dt_Group;
        //gv_Group.DataKeyNames = new string[] { "id" };
        //gv_Group.DataBind();
    }
    public void bind_xmh()
    {
        //Panel2.Visible = true;
        //DataTable dt_xm = DJ.GetDJ_Group_XM(2, ddlcomp.SelectedValue, ddlproduct.SelectedValue, txt_xm.Value);
        //gv_xmh.DataSource = dt_xm;
        //gv_xmh.DataKeyNames = new string[] { "id" };
        //gv_xmh.DataBind();
        //for (int i = 0; i < gv_xmh.Rows.Count; i++)
        //{
        //    DataTable dt = DbHelperSQL.Query(strsql).Tables[0];
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataSource =dt;
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataTextField = "DJ_Group";
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataValueField = "DJ_Group";
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).DataBind();
        //    ((DropDownList)gv_xmh.Rows[i].FindControl("ddl_prod")).Items.Insert(0, new ListItem("", ""));
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Text = dt_xm.Rows[i]["DJ_Group"].ToString();
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).CssClass = "form-control input-s-sm";
        //    ((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Enabled = false;
        //}
    }
    protected void gv_Group_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string comp = ((TextBox)(gv_Group.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        //string product = ((TextBox)(gv_Group.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        //int id =Convert.ToInt16( gv_Group.DataKeys[e.RowIndex].Value.ToString());
        //string uid = "";
        //if (Session["empid"] != null)
        //{
        //    uid = Session["empid"].ToString();
        //}
        //int jg = Check_Sam_Product(comp, product);
        //if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组,请确认！')", true); return; }
       
        //int result= 0;
        //result = DJ.DJ_Group_XM_Insert(11, comp, product, "", uid, id,"");
        //gv_Group.EditIndex = -1;
        //bind_Group();
    }
    protected void gv_Group_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        //int id = Convert.ToInt16(gv_Group.DataKeys[e.RowIndex].Value.ToString());
        //string uid = "";
        //if (Session["empid"] != null)
        //{
        //    uid = Session["empid"].ToString();
        //}
        //int result = 0;
        //result = DJ.DJ_Group_XM_Insert(21, "", "", "", uid, id,"");
        //bind_Group();
    }
    protected void gv_Group_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //gv_Group.EditIndex = -1;

        //bind_Group();
    }
  
    protected void btn_xm_confirm_Click(object sender, EventArgs e)
    {
        //if (txt_xm.Value == "" || ddlproduct.SelectedValue == "")
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组和项目号必须选择！')", true);
        //    return;
        //}
        //string comp = ddlcomp.SelectedValue;
        //string uid = "";
        //string product = ddlproduct.SelectedValue;
        //string xmh = txt_xm.Value;
        //string bzdj = txt_bzdj.Value;
        //if (Session["empid"] != null)
        //{
        //    uid = Session["empid"].ToString();
        //}
        //int jg = Check_Sam_XM(comp, product,xmh);
        //if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组及项目,请确认！')", true); return; }
        //int result = 0;
        //result = DJ.DJ_Group_XM_Insert(2, comp, product, xmh, uid, 0,xmh);
        //if (result > 0)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产品组项目类别维护成功！')", true);
        //    bind_xmh();
        //}

    }
    protected void gv_xmh_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gv_xmh.EditIndex = e.NewEditIndex;

        //bind_xmh();
        //int lnindex = e.NewEditIndex;
        //int i = lnindex;
        //((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).CssClass = "form-control input-s-sm  ";
        //((DropDownList)this.gv_xmh.Rows[i].FindControl("ddl_prod")).Enabled = true;
    }
    protected void gv_xmh_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string comp = ((TextBox)(gv_xmh.Rows[e.RowIndex].Cells[1].Controls[1])).Text.ToString().Trim();
        //string xmh = ((TextBox)(gv_xmh.Rows[e.RowIndex].Cells[2].Controls[1])).Text.ToString().Trim();
        //string product = ((DropDownList)gv_xmh.Rows[e.RowIndex].FindControl("ddl_prod")).SelectedValue;
        //int id = Convert.ToInt16(gv_xmh.DataKeys[e.RowIndex].Value.ToString());
        //string uid = "";
        //if (Session["empid"] != null)
        //{
        //    uid = Session["empid"].ToString();
        //}
        //int jg = Check_Sam_Product(comp, product);
        //if (jg == 1) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('系统中已维护该产品组及项目,请确认！')", true); return; }
        //int result = 0;
        //result = DJ.DJ_Group_XM_Insert(12, comp, product, xmh, uid, id,"");
        //gv_xmh.EditIndex = -1;
        //bind_xmh();
    }

    protected void gv_xmh_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //gv_xmh.EditIndex = -1;

        //bind_xmh();
    }
    protected void gv_xmh_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int id = Convert.ToInt16(gv_xmh.DataKeys[e.RowIndex].Value.ToString());
        //string uid = "";
        //if (Session["empid"] != null)
        //{
        //    uid = Session["empid"].ToString();
        //}
        //int result = 0;
        //result = DJ.DJ_Group_XM_Insert(22, "", "", "", uid, id,"");
        //bind_xmh();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bind_xmh();
       
    }
    protected void btn_Group_query_Click(object sender, EventArgs e)
    {
        bind_Group();
    }



    protected void gv1_FocusedRowChanged(object sender,EventArgs e)
    {
        //string lspgi_no = this.gv1.GetRowValues(this.gv1.FocusedRowIndex, "pgi_no").ToString();
        //string lsop = this.gv1.GetRowValues(this.gv1.FocusedRowIndex, "op").ToString();
        //string lsver = this.gv1.GetRowValues(this.gv1.FocusedRowIndex, "ver").ToString();

        //lblpgi_no.Text = "PGI编号:    " + lspgi_no;
        //lblop.Text = "工序号:    " + lsop;
        //lblver.Text = "版本:    " + lsver;

        //DataTable ldt1 = DbHelperSQL.Query("exec Pgi_Base_DaoJu_Form_Dt_Query '" + lspgi_no + "','" + lsop + "','" + lsver + "'").Tables[0];

        //Pgi.Auto.Control.SetGrid("DAOJU", "DAOJU_2", this.gv2, ldt1);
       
    }


    [WebMethod]
    public static void test()
    {
       

    }

    [WebMethod]
    public static string Getuser(string lsdept)
    {
        string result = "[";
        DataTable ldt = DbHelperSQL.Query("select distinct  substring(product_user, charindex('-',product_user)+1,LEN(product_user)-charindex('-',product_user)) as product_user from form3_Sale_Product_MainTable where product_user<>'' and  left( form3_Sale_Product_MainTable.product_user,5) in (select workcode from HRM_EMP_MES where (departmentname='" + lsdept + "' or dept_name='" + lsdept + "'))").Tables[0];
        if (ldt.Rows.Count > 0)
        {
            for (int i = 0; i <ldt.Rows.Count; i++)
            {
                result = result + "{\"value\":\"" + ldt.Rows[i][0].ToString() + "\"},";
            }
        }
        result = result.TrimEnd(',') + "]";
        return result;

    }




    //protected void txtdept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataTable ldt = DbHelperSQL.Query("select distinct  substring(product_user, charindex('-',product_user)+1,LEN(product_user)-charindex('-',product_user)) as product_user from form3_Sale_Product_MainTable where product_user<>'' and  left( form3_Sale_Product_MainTable.product_user,5) in (select workcode from HRM_EMP_MES where (departmentname='"+this.txtdept.SelectedValue+"' or dept_name='"+this.txtdept.SelectedValue+"'))").Tables[0];
    //    this.txtproduct_user.DataValueField = "product_user";
    //    this.txtproduct_user.DataTextField = "product_user";
    //    this.txtproduct_user.DataSource = ldt;
    //    this.txtproduct_user.DataBind();
    //    this.txtproduct_user.Items.Insert(0, new ListItem("ALL", ""));
    //}

   
    protected void gv1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
        string lsflag = e.GetValue("b_flag").ToString();
        string lsflow = e.GetValue("flow_status").ToString();
        if (lsflag=="无效" && lsflow=="流程结束")
        {
            e.Row.Style.Add("background-color", "LightGray");
            
            
        }
       
        }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        Session["flag"] = "EDIT";
        int ln = this.gv1.FocusedRowIndex;

        Response.Write("<script>window.open('daoju.aspx?id=" + this.gv1.GetRowValues(ln,"id").ToString() +"');</script>");
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Session["flag"] = "ADD";
        Response.Write("<script>window.open('daoju.aspx');</script>");
    }



    protected void btnimport_Click(object sender, EventArgs e)
    {
       

        string lsdaoju_id = this.gv1.GetRowValues(this.gv1.FocusedRowIndex, "id").ToString();
        DataTable ldt1 = DbHelperSQL.Query("exec Pgi_Base_DaoJu_Form_Dt_Query20180321 '" + lsdaoju_id + "','" + this.txtshow_type.SelectedValue + "'").Tables[0];
        Pgi.Auto.Control.SetGrid("DAOJU", "DAOJU_2", this.gv2, ldt1, 0);

       
       // this.p1.Update();
        this.gve2.WriteXlsToResponse(System.DateTime.Now.ToShortDateString());//导出到Excel
       
    }
}