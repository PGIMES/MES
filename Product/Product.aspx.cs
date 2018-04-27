using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.IO;
using System.Text;
using IBatisNet.Common.Transaction;

public partial class Product : System.Web.UI.Page
{
   
    Product_CLASS Product_CLASS = new Product_CLASS();
    public static string ctl = "ctl00$MainContent$";
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        this.gv_rz1.PageSize = 200;
        this.gv_rz2.PageSize = 100;
        this.gv_Product_version.PageSize = 100;
        this.gv_Product_Quantity.PageSize = 100;

        if (Session["empid"] == null || Session["job"] == null)
        {   // 给Session["empid"] & Session["job"] 初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Session["lv"] = "";
           
            ViewState["job"] = Session["job"];
            ViewState["empid"] = Session["empid"];

            if (Session["empid"] == null || Session["empid"] == "")
            {
                ViewState["empid"] = "01757";

            }
            else
            {
                ViewState["empid"] = Session["empid"];
            }
     
            DDL();

            if (ViewState["empid"] != null)
            {
                //当前登陆人员
                txt_update_user.Value = ViewState["empid"].ToString();
                DataTable dtemp = Product_CLASS.emp(ViewState["empid"].ToString());
                txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_update_user_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                txt_update_user_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                if (Request["requestid"] != null)//页面加载
                {
                  
                    Query();
               
                    bindrz_log(Request["requestid"], gv_rz1);
                    bindrz2_log(Request["requestid"], gv_rz2);
                    yanzheng();
                    txt_pc_date.Disabled = true;
                    txt_end_date.Disabled = true;
                    txt_dingdian_date.Disabled = true;
                }
                else//新建
                {
                    if (txt_update_user.Value == "01757" || txt_update_user_dept.Value == "IT部")
                    {
                        txt_Code.Value = "CP" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        this.txt_CreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        txt_create_by_empid.Value = ViewState["empid"].ToString();
                        //DataTable dtemp = Product_CLASS.emp(txt_create_by_empid.Value);
                        txt_create_by_name.Value = dtemp.Rows[0]["lastname"].ToString();
                        txt_create_by_ad.Value = dtemp.Rows[0]["ADAccount"].ToString();
                        txt_create_by_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                        txt_managerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                        txt_manager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                        txt_manager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();
                        txt_update_user.Value = ViewState["empid"].ToString();
                        txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
                        DDL_product_status.SelectedValue = "开发中";
                        //DDL_product_status.Enabled = false;
                        dtemp.Clear();

                        //产品明细
                        gv_Product_version_InitTable();
                        gv_Product_Quantity_InitTable();
                        txtupload.Visible = false;
                        btnImg.Visible = false;
                        BTN_Sales_submit.Text = "提交";
                        button_zhidu();
                        gv_Product_version_zhidu();
                        txt_delete_date.Disabled = true;
                        txt_pc_date.Disabled = true;
                        txt_end_date.Disabled = true;
                        txt_dingdian_date.Disabled = true;

                    }
                    else
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售部李平和张荣新有新增项目号权限！')", true);
                        //Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问


                        string script = "<script language='javascript'>alert('销售部张荣新有新增项目号权限');window.open('../index.aspx');" +
 "window.parent.opener = null;window.parent.open('','_self');window.parent.close(); window.close();</script>";
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "CloseWindow", script);

                    }
                }
            }
        }
    }
    //主表信息
    public void customer_project_all()
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            TextBox txt = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project");
            if (txt.Text != "")
            {
                if (lsvalue.Contains(txt.Text) == false)
                {
                    lsvalue += lsvalue == "" ? "" : "/";
                    lsvalue += txt.Text;
                }

            }
        }
        this.txt_customer_project.Value = lsvalue;
    }
    protected void txt_CP_ID_TextChanged(object sender, EventArgs e)
    {
        
        if (txt_CP_ID.Text == "")
        {

        }
        else
        {
            DataTable dt = Product_CLASS.Form3_Product_ljh(txt_baojia_no.Value, txt_CP_ID.Text, "ljh");
            txt_productcode.Value = dt.Rows[0]["ljh"].ToString();
            txt_productname.Value = dt.Rows[0]["lj_name"].ToString();
            txt_make_factory.Value = dt.Rows[0]["domain"].ToString();
            if (dt.Rows.Count > 0)
            {
                DDL_customer_name.SelectedValue = dt.Rows[0]["customer_name"].ToString();
                DDL_end_customer_name.SelectedValue = dt.Rows[0]["end_customer_name"].ToString();
            }
            //txt_customer_project.Value = dt.Rows[0]["customer_project"].ToString();
           
            txt_Sales_engineers.Value = dt.Rows[0]["sales"].ToString();
            DDL_sale.SelectedValue = dt.Rows[0]["sales"].ToString();
            if (dt.Rows[0]["dingdian_date"].ToString() != "")
            {
                txt_dingdian_date.Value = Convert.ToDateTime(dt.Rows[0]["dingdian_date"].ToString()).ToString("yyyy-MM-dd");

            }
            if (dt.Rows[0]["pc_date"].ToString() != "")
            {
                txt_pc_date.Value = Convert.ToDateTime(dt.Rows[0]["pc_date"].ToString()).ToString("yyyy-MM-dd");

            }
            DataTable ldt = Product_CLASS.Getmax().Tables[0];
            if(ldt.Rows.Count>0)
            {
                this.txt_pgino.Value = ldt.Rows[0]["pgino"].ToString();
            }
            else
            {
                DataTable ldt_hr = Product_CLASS.Getmax_hr().Tables[0];
                this.txt_pgino.Value = ldt_hr.Rows[0]["pgino"].ToString();
            }
          
                //for (int i = 0; i < gv_Product_version.Rows.Count; i++)
                //{
                    ((TextBox)this.gv_Product_version.Rows[0].FindControl("txt_pgino")).Text= this.txt_pgino.Value;
                    ((TextBox)this.gv_Product_version.Rows[0].FindControl("txt_productcode")).Text = this.txt_productcode.Value;
                    ((TextBox)this.gv_Product_version.Rows[0].FindControl("txt_version")).Text = "A";
                    ((TextBox)this.gv_Product_version.Rows[0].FindControl("txt_Desc_sm")).Text = "新增";
                //}

                DataTable dt_Quantity = Product_CLASS.Form3_Product_ljh(txt_baojia_no.Value, txt_CP_ID.Text, "agreement");
                gv_Product_Quantity.DataSource = dt_Quantity;
                gv_Product_Quantity.DataBind();
                DDL_gv_Product_Quantity();

                for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
                {
                    ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Text = txt_Sales_engineers.Value;
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_date")).Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_User")).Text = txt_update_user_name.Value;
                }

            customer_project_all();
            Sales_engineers_all();
        }
        ViewState["lv"] = "CPFZR";
        ViewState["lv"] = "cpyc";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
        button_yingchang();
        txtupload.Visible = true;
        btnImg.Visible = true;
    }
    public void DDL()
    {
        txt_update_user.Value = ViewState["empid"].ToString();

        //DDL_product_leibie.DataSource = Product_CLASS.DDL_base("qad_cpl", "DDL_product_leibie",txt_update_user.Value);
        //DDL_product_leibie.DataValueField = "material_id";
        //DDL_product_leibie.DataTextField = "material_id";
        //DDL_product_leibie.DataBind();
        //this.DDL_product_leibie.Items.Insert(0, new ListItem("", ""));

        DDL_product_leibie.DataSource = Product_CLASS.DDL_base("Product_BASE", "DDL_product_leibie", txt_update_user.Value);
        DDL_product_leibie.DataValueField = "CLASS_NAME";
        DDL_product_leibie.DataTextField = "CLASS_NAME";
        DDL_product_leibie.DataBind();
        this.DDL_product_leibie.Items.Insert(0, new ListItem("", ""));

        DDL_product_status.DataSource = Product_CLASS.DDL_base("Product_BASE", "DDL_product_status",txt_update_user.Value);
        DDL_product_status.DataValueField = "CLASS_NAME";
        DDL_product_status.DataTextField = "CLASS_NAME";
        DDL_product_status.DataBind();
        this.DDL_product_status.Items.Insert(0, new ListItem("", ""));

        DDL_end_customer_name.DataSource = Product_CLASS.BJ_base("end_customer", "Product");
        DDL_end_customer_name.DataValueField = "name";
        DDL_end_customer_name.DataTextField = "name";
        DDL_end_customer_name.DataBind();
        this.DDL_end_customer_name.Items.Insert(0, new ListItem("", ""));

        DDL_customer_name.DataSource = Product_CLASS.BJ_base("custumer", "Product");
        DDL_customer_name.DataValueField = "name";
        DDL_customer_name.DataTextField = "name";
        DDL_customer_name.DataBind();
        this.DDL_customer_name.Items.Insert(0, new ListItem("", ""));

        this.DDL_project_user.DataSource = Product_CLASS.DDL_base("engineer", "项目",txt_update_user.Value);
        this.DDL_project_user.DataTextField = "name";
        this.DDL_project_user.DataValueField = "name";
        this.DDL_project_user.DataBind();
        this.DDL_project_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_product_user.DataSource = Product_CLASS.DDL_base("engineer", "产品",txt_update_user.Value);
        this.DDL_product_user.DataTextField = "name";
        this.DDL_product_user.DataValueField = "name";
        this.DDL_product_user.DataBind();
        this.DDL_product_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_moju_user.DataSource = Product_CLASS.DDL_base("engineer", "模具",txt_update_user.Value);
        this.DDL_moju_user.DataTextField = "name";
        this.DDL_moju_user.DataValueField = "name";
        this.DDL_moju_user.DataBind();
        this.DDL_moju_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_yz_user.DataSource = Product_CLASS.DDL_base("engineer", "压铸工艺",txt_update_user.Value);
        this.DDL_yz_user.DataTextField = "name";
        this.DDL_yz_user.DataValueField = "name";
        this.DDL_yz_user.DataBind();
        this.DDL_yz_user.Items.Insert(0, new ListItem("", ""));


        this.DDL_jj_user.DataSource = Product_CLASS.DDL_base("engineer", "调试",txt_update_user.Value);
        this.DDL_jj_user.DataTextField = "name";
        this.DDL_jj_user.DataValueField = "name";
        this.DDL_jj_user.DataBind();
        this.DDL_jj_user.Items.Insert(0, new ListItem("", ""));


        this.DDL_zl_user.DataSource = Product_CLASS.DDL_base("engineer", "质量",txt_update_user.Value);
        this.DDL_zl_user.DataTextField = "name";
        this.DDL_zl_user.DataValueField = "name";
        this.DDL_zl_user.DataBind();
        this.DDL_zl_user.Items.Insert(0, new ListItem("", ""));


        this.DDL_bz_user.DataSource = Product_CLASS.DDL_base("engineer", "包装",txt_update_user.Value);
        this.DDL_bz_user.DataTextField = "name";
        this.DDL_bz_user.DataValueField = "name";
        this.DDL_bz_user.DataBind();
        this.DDL_bz_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_wl_user.DataSource = Product_CLASS.DDL_base("engineer", "计划",txt_update_user.Value);
        this.DDL_wl_user.DataTextField = "name";
        this.DDL_wl_user.DataValueField = "name";
        this.DDL_wl_user.DataBind();
        this.DDL_wl_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_zhiliangzhuguan_user.DataSource = Product_CLASS.DDL_base("engineer", "质量主管",txt_update_user.Value);
        this.DDL_zhiliangzhuguan_user.DataTextField = "name";
        this.DDL_zhiliangzhuguan_user.DataValueField = "name";
        this.DDL_zhiliangzhuguan_user.DataBind();
        this.DDL_zhiliangzhuguan_user.Items.Insert(0, new ListItem("", ""));

        this.DDL_sqe_user1.DataSource = Product_CLASS.DDL_base("engineer", "供应商质量",txt_update_user.Value);
        this.DDL_sqe_user1.DataTextField = "name";
        this.DDL_sqe_user1.DataValueField = "name";
        this.DDL_sqe_user1.DataBind();
        this.DDL_sqe_user1.Items.Insert(0, new ListItem("", ""));

        this.DDL_sqe_user2.DataSource = Product_CLASS.DDL_base("engineer", "物流工程师",txt_update_user.Value);
        this.DDL_sqe_user2.DataTextField = "name";
        this.DDL_sqe_user2.DataValueField = "name";
        this.DDL_sqe_user2.DataBind();
        this.DDL_sqe_user2.Items.Insert(0, new ListItem("", ""));

        selectwl.DataSource = Product_CLASS.DDL_base("engineer", "物流工程师", txt_update_user.Value);
        selectwl.DataValueField = "name";
        selectwl.DataTextField = "name";
        selectwl.DataBind();


        this.DDL_caigou.DataSource = Product_CLASS.DDL_base("engineer", "采购",txt_update_user.Value);
        this.DDL_caigou.DataTextField = "name";
        this.DDL_caigou.DataValueField = "name";
        this.DDL_caigou.DataBind();
        this.DDL_caigou.Items.Insert(0, new ListItem("", ""));


        if (Request["requestid"] != null)
        {
            this.DDL_sale.DataSource = Product_CLASS.DDL_base("engineer", "销售",txt_update_user.Value);
            this.DDL_sale.DataTextField = "name";
            this.DDL_sale.DataValueField = "name";
            this.DDL_sale.DataBind();
            this.DDL_sale.Items.Insert(0, new ListItem("", ""));
        }
        else
        {
            this.DDL_sale.DataSource = Product_CLASS.DDL_base("engineer", "销售", "00002");
            this.DDL_sale.DataTextField = "name";
            this.DDL_sale.DataValueField = "name";
            this.DDL_sale.DataBind();
            this.DDL_sale.Items.Insert(0, new ListItem("", ""));
        }
        

        this.DDL_jiaju_egnieer.DataSource = Product_CLASS.DDL_base("engineer", "夹具",txt_update_user.Value);
        this.DDL_jiaju_egnieer.DataTextField = "name";
        this.DDL_jiaju_egnieer.DataValueField = "name";
        this.DDL_jiaju_egnieer.DataBind();
        this.DDL_jiaju_egnieer.Items.Insert(0, new ListItem("", ""));

        this.DDL_daoju_egnieer.DataSource = Product_CLASS.DDL_base("engineer", "刀具",txt_update_user.Value);
        this.DDL_daoju_egnieer.DataTextField = "name";
        this.DDL_daoju_egnieer.DataValueField = "name";
        this.DDL_daoju_egnieer.DataBind();
        this.DDL_daoju_egnieer.Items.Insert(0, new ListItem("", ""));

        this.DDL_jianju_egnieer.DataSource = Product_CLASS.DDL_base("engineer", "检具",txt_update_user.Value);
        this.DDL_jianju_egnieer.DataTextField = "name";
        this.DDL_jianju_egnieer.DataValueField = "name";
        this.DDL_jianju_egnieer.DataBind();
        this.DDL_jianju_egnieer.Items.Insert(0, new ListItem("", ""));

        this.DDL_mojugl_egnieer.DataSource = Product_CLASS.DDL_base("engineer", "模具",txt_update_user.Value);
        this.DDL_mojugl_egnieer.DataTextField = "name";
        this.DDL_mojugl_egnieer.DataValueField = "name";
        this.DDL_mojugl_egnieer.DataBind();
        this.DDL_mojugl_egnieer.Items.Insert(0, new ListItem("", ""));

    }
    public void button_zhidu()
    {
        //BTN_product_update.Enabled = false;
        //BTN_Quantity_update.Enabled = false;
        //BTN_version_update.Enabled = false;
        //BTN_fzr_update.Enabled = false;
        BTN_product_update.Visible = false;
        BTN_Quantity_update.Visible = false;
        BTN_version_update.Visible = false;
        BTN_fzr_update.Visible = false;



    }
    public void mstr_zhidu()
    {
        DDL_product_leibie.Enabled = false;
        this.DDL_product_leibie.Style.Add("background-color", "#F0F0F0");
        DDL_customer_name.Enabled = false;
        this.DDL_customer_name.Style.Add("background-color", "#F0F0F0");
        DDL_end_customer_name.Enabled = false;
        this.DDL_end_customer_name.Style.Add("background-color", "#F0F0F0");
        txt_delete_date.Disabled = true;
        txt_customer_requestCN.Disabled = true;
        txt_customer_requestSM.Enabled = false;
        txt_productname.Disabled = true;
    }
    public void button_write()
    {
        BTN_product_update.Enabled = true;
        BTN_Quantity_update.Enabled = true;
        BTN_version_update.Enabled = true;
        BTN_fzr_update.Enabled = true;
        BTN_Sales_submit.Visible = false;

        DDL_product_leibie.Enabled = true;
        DDL_customer_name.Enabled = true;
        DDL_end_customer_name.Enabled = true;
        txt_delete_date.Disabled = false;
        txt_customer_requestCN.Disabled = false;
        txt_customer_requestSM.Enabled = true;
        txt_productname.Disabled = false;
    }
    public void button_yingchang()
    {
        BTN_product_update.Visible = false;
        BTN_Quantity_update.Visible = false;
        BTN_version_update.Visible = false;
        //BTN_fzr_update.Visible = false;

        //for (int i = 0; i < gv_Product_version.Rows.Count; i++)
        //{

        //    ((Button)this.gv_Product_version.FooterRow.FindControl("btnAdd")).Visible = false;
        //    ((Button)this.gv_Product_version.Rows[i].FindControl("btnDel")).Visible = false;

        //}
        //for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        //{

        //    ((Button)this.gv_Product_Quantity.FooterRow.FindControl("btnAddQTY")).Visible = false;
        //    ((Button)this.gv_Product_Quantity.Rows[i].FindControl("btnDelQTY")).Visible = false;

        //}
    }
    protected void BTN_product_update_Click(object sender, EventArgs e)
    {
        //string delete_date = "";
        //if (txt_delete_date.Value != "")
        //{
        //    delete_date = txt_delete_date.Value;
        //}
        //else
        //{
        //    delete_date = string.IsNullOrEmpty(txt_delete_date.Value) ? " " : txt_delete_date.Value;

        //}
        int update1= Product_CLASS.BTN_product_update(Convert.ToInt32(Request["requestid"]), DDL_product_leibie.SelectedValue, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, txt_end_date.Value, txt_update_user.Value, txt_pc_date.Value,DDL_product_status.SelectedValue, txt_delete_date.Value, txt_Sales_engineers.Value, txtproduct_img.Text,txt_dingdian_date.Value,txt_customer_requestCN.Value.Replace(",", ""), txt_customer_requestSM.Text, txt_productname.Value);
        if (update1 > 0)
        {
            //插入日志信息
            insert_form3_Sale_Product_LOG(Convert.ToInt32(Request["requestid"]),"修改", BTN_product_update.Text);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
            BTN_product_update.Enabled = false;
            pdyq();
            
        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改或联系管理员！！')", true);
        }
        
    }
    protected void BTN_fzr_update_Click(object sender, EventArgs e)
    {
        int type = 1;
        
        DataTable dtXMH = Product_CLASS.form3_Sale_V_Track(txt_pgino.Value);
       if(dtXMH.Rows.Count>0)
        {
            type = 2;
        }
       else
        {
            type = 1;
        }
        if (DDL_sqe_user22.Text != "")
        {
            DDL_sqe_user2.Items.Clear();
            DDL_sqe_user2.Items.Insert(0, new ListItem(DDL_sqe_user22.Text, DDL_sqe_user22.Text));
        }

        int update2 = Product_CLASS.BTN_fzr_update(Convert.ToInt32(Request["requestid"]),type,txt_update_user.Value, DDL_project_user.SelectedValue, DDL_product_user.SelectedValue, DDL_moju_user.SelectedValue,
                    DDL_yz_user.SelectedValue, DDL_jj_user.SelectedValue, DDL_zl_user.SelectedValue, DDL_bz_user.SelectedValue, DDL_wl_user.SelectedValue
                    , DDL_zhiliangzhuguan_user.SelectedValue, DDL_sqe_user1.SelectedValue, DDL_sqe_user2.SelectedValue, DDL_caigou.SelectedValue, DDL_sale.SelectedValue
                    , DDL_jiaju_egnieer.SelectedValue, DDL_daoju_egnieer.SelectedValue, DDL_jianju_egnieer.SelectedValue, DDL_mojugl_egnieer.SelectedValue);
        if (update2 > 0)
        {
            //插入日志信息
            insert_form3_Sale_Product_LOG(Convert.ToInt32(Request["requestid"]), "修改", BTN_fzr_update.Text);
            System.Web.UI.ScriptManager.RegisterStartupScript(Page,this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
            BTN_fzr_update.Enabled = false;
       
           
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改或联系管理员！')", true);
          
        }
    }
    public void yanzheng()
    {

        if (txt_update_user_dept.Value == "销售二部" || txt_update_user.Value == "00002" || txt_update_user_dept.Value == "IT部")
        //if (txt_update_user.Value == "01757" || txt_update_user.Value == "02149")
        {
            button_write();
            //gv_Product_version_write();
            for (int i = 0; i < gv_Product_version.Rows.Count; i++)
            {
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_pgino")).Enabled = false;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Enabled = false;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_version")).Enabled = false;
                ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_Desc_sm")).Enabled = false;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_date")).Enabled = false;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_User")).Enabled = false;
                //((Button)this.gv_Product_version.FooterRow.FindControl("btnAdd")).Visible = false;
                ((Button)this.gv_Product_version.Rows[i].FindControl("btnDel")).Visible = false;

            }
            if (txt_update_user.Value == "01757" || txt_update_user.Value == "02149")
            {
                gv_Product_Quantity_write();
                BTN_Quantity_update.Visible = true;
            }
            else
            {
                button_yingchang();
                gv_Product_Quantity_zhidu();
                gv_Product_version_zhidu();
                BTN_Quantity_update.Visible = false;
                BTN_Sales_submit.Visible = false;
                mstr_zhidu();
            }
         
            //加载预测信息中颜色及提示
            gvcolour();
        }
        else
        {
            button_yingchang();
            gv_Product_Quantity_zhidu();
            gv_Product_version_zhidu();
            BTN_Sales_submit.Visible = false;
            if(txt_update_user_job.Value.Contains("工程师"))
            {
                BTN_fzr_update.Visible = false;
                selectwl.Visible = false;
                selectwl.Disabled = false;
            }
            mstr_zhidu();
        }


    }
    protected void btnImg_Click(object sender, EventArgs e)
    {
        //if (this.productid == null)
        //{
        //    Response.Write("<script>javascript:alert('请先保存数据后，再上传图片!')</script>");
        //    return;
        //}
        string lshz = "";
        string lssserverpath = "";
        if (this.txtupload.FileName != "")
        {
            lshz = System.IO.Path.GetExtension(this.txtupload.PostedFile.FileName).ToUpper();
            if (lshz != ".JPG" && lshz != ".JPEG" && lshz != ".GIF")
            {
                Response.Write("<script>javascript:alert('产品图片格式不正确，请重新上传!')</script>");
                return;
            }

            //时间命名
            //string lstime = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            string lstime = txt_pgino.Value;
            if (this.txtproduct_img.Text != "")
            {
                if (System.IO.File.Exists(Server.MapPath(this.txtproduct_img.Text)))
                {
                    System.IO.File.Delete(Server.MapPath(this.txtproduct_img.Text));
                }
            }

            //后缀
            lshz = System.IO.Path.GetExtension(this.txtupload.PostedFile.FileName).ToUpper();
            lssserverpath = Server.MapPath("~/Product/upload/") + lstime + lshz;
            this.txtupload.PostedFile.SaveAs(lssserverpath);//将上传的文件另存为 
            this.txtproduct_img.Text = "../Product/upload/" + lstime + lshz;
            this.Image2.Visible = true;
            this.Image2.ImageUrl = this.txtproduct_img.Text;
            //Product_CLASS.ProductImgUpdate(this.txt_pgino.Value.ToString(), this.txtproduct_img.Text);

        }

        else
        {
            Response.Write("<script>javascript:alert('请选择图片!')</script>");
            return;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }
    //产品版本明细

    public void gv_Product_version_write()
    {
              for (int i = 0; i<gv_Product_version.Rows.Count; i++)
            {
            
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_pgino")).Enabled = true;
                ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Enabled = true;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_version")).Enabled = true;
                ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_Desc_sm")).Enabled = true;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_date")).Enabled = false;
                //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_User")).Enabled = false;
                ((Button)this.gv_Product_version.FooterRow.FindControl("btnAdd")).Visible = true;
                //((Button)this.gv_Product_version.Rows[i].FindControl("btnDel")).Visible = true;
     
            }
        }
    public void gv_Product_version_zhidu()
    {
        //gridview报价明细只读
        for (int i = 0; i < gv_Product_version.Rows.Count; i++)
        {
            //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_pgino")).Enabled = false;
            ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Enabled = true;
            if (Request["requestid"] != null)//页面加载
            {
                ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Enabled = false;
            }
                                             //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_version")).Enabled = false;
                ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_Desc_sm")).Enabled = false;
            //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_date")).Enabled = false;
            //((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_User")).Enabled = false;
            ((Button)this.gv_Product_version.FooterRow.FindControl("btnAdd")).Visible = false;
            ((Button)this.gv_Product_version.Rows[i].FindControl("btnDel")).Visible = false;

        }

    }
    private void gv_Product_version_InitTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("pgino"));
        dt.Columns.Add(new DataColumn("productcode"));
        dt.Columns.Add(new DataColumn("version"));
        dt.Columns.Add(new DataColumn("Desc_sm"));
        dt.Columns.Add(new DataColumn("update_date"));
        dt.Columns.Add(new DataColumn("update_User"));


        ViewState["dt"] = dt;
        int ln = gv_Product_version.Rows.Count;
        for (int i = ln; i < 1; i++)
        {
            DataRow ldr = dt.NewRow();
            //ldr["baojia_no"] = Request["requestid"];
            //ldr["turns"] = DDL_turns.SelectedValue;
            ldr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ldr["update_User"] = txt_update_user_name.Value;
            dt.Rows.Add(ldr);

      
        }
        this.gv_Product_version.DataSource = dt;
        this.gv_Product_version.DataBind();
        //gv_Product_version_write();
    }
    //字符转ASCII码：
    public static int Asc(string character)
    {
        if (character.Length == 1)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
            return (intAsciiCode);
        }
        else
        {
            throw new Exception("Character is not valid.");
        }
    }
    //ASCII码转字符：
    public static string Chr(int asciiCode)
    {
        if (asciiCode >= 0 && asciiCode <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)asciiCode };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        else
        {
            throw new Exception("ASCII Code is not valid.");
        }
    }

    protected void gv_Product_version_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dts = ViewState["dt"] as DataTable;
        DataTable dt = GetDataTableFromGridView1(gv_Product_version, dts);

        if (e.CommandName == "add")
        {
            DataRow ldr = dt.NewRow();
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).Parent.Parent);
            //dr["baojia_no"] = Request["requestid"];
            //dr["turns"] = DDL_turns.SelectedValue;
            ldr["pgino"] = txt_pgino.Value;
            int i = dt.Rows.Count-1;
            ldr["productcode"] = dt.Rows[i]["productcode"].ToString();
            string ZM = dt.Rows[i]["version"].ToString();
            //ldr["version"] = "B";
            ldr["version"] = Chr(Asc(ZM) + 1);
            ldr["Desc_sm"] = "修改";
            ldr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ldr["update_User"] = txt_update_user_name.Value;
            dt.Rows.Add(ldr);

      
            //gv_Product_version_write();

        }
        if (e.CommandName == "del")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            if (index == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少保留一行！')", true);
            }
            else
            {
                dt.Rows.RemoveAt(index);
            }
            ViewState["lv"] = "BJJD";
        }
        ViewState["dt"] = dt;
        this.gv_Product_version.DataSource = dt;
        this.gv_Product_version.DataBind();
   
    }
    //产品预测明细

    public void gv_Product_Quantity_zhidu()
    {
        //gv_Product_Quantity.Columns[1].Visible = false;//一开始隐藏
      
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pgino")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("ID")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("colour")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Enabled = false;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("Ship_from")).Enabled = false;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Enabled = false;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Enabled = false;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("Ship_to")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("khdm")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("dingdian_date")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj_QAD")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("quantity_year")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("price_year")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("psw_quantity")).Enabled = false;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Enabled = false;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2012")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2013")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2014")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2015")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2016")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2017")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2018")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2019")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2020")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2021")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2022")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2023")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2024")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2025")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2026")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2027")).ReadOnly = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2028")).ReadOnly = true;
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2012")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2013")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2014")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2015")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2016")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2017")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2018")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2019")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2020")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2021")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2022")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2023")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2024")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2025")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2026")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2027")).Style.Add("background-color", "#F0F0F0");
            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("QTY_2028")).Style.Add("background-color", "#F0F0F0");

            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_date")).Enabled = false;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_User")).Enabled = false;
            ((Button)this.gv_Product_Quantity.FooterRow.FindControl("btnAddQTY")).Visible = false;
            ((Button)this.gv_Product_Quantity.Rows[i].FindControl("btnDelQTY")).Visible = false;
            
        }

      
    }
    public void gv_Product_Quantity_write()
    {
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {

            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pgino")).Enabled = true;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Enabled = true;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("Ship_from")).Enabled = true;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Enabled = true;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("Ship_to")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("khdm")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("dingdian_date")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Enabled = true;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj_QAD")).Enabled = true;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("quantity_year")).Enabled = true;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("price_year")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("psw_quantity")).Enabled = true;
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2012")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2013")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2014")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2015")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2016")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2017")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2018")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2019")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2020")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2021")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2022")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2023")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2024")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2025")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2026")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2027")).Enabled = true;
            ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2028")).Enabled = true;

            for (int j = 2012; j <= 17; j++)
            {
                ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_A_" + j)).Enabled = false;
               // ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_RF_" + j)).Enabled = false;

            }
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_date")).Enabled = false;
            //((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_User")).Enabled = false;
            ((Button)this.gv_Product_Quantity.FooterRow.FindControl("btnAddQTY")).Visible = true;
            ((Button)this.gv_Product_Quantity.Rows[i].FindControl("btnDelQTY")).Visible = true;

        }
    }
    private void gv_Product_Quantity_InitTable()
    {
        //合同追踪页面加载
        DataTable dtht = new DataTable();
        //dtht.Columns.Add(new DataColumn("pgino"));
        dtht.Columns.Add(new DataColumn("ID"));
        dtht.Columns.Add(new DataColumn("colour"));
        dtht.Columns.Add(new DataColumn("product_status_dt"));
        dtht.Columns.Add(new DataColumn("Ship_from"));
        dtht.Columns.Add(new DataColumn("Ship_to"));
        dtht.Columns.Add(new DataColumn("customer_project"));
        dtht.Columns.Add(new DataColumn("khdm"));
        dtht.Columns.Add(new DataColumn("dingdian_date"));
        dtht.Columns.Add(new DataColumn("pc_date"));
        dtht.Columns.Add(new DataColumn("end_date"));
        dtht.Columns.Add(new DataColumn("pc_dj"));
        dtht.Columns.Add(new DataColumn("pc_dj_QAD"));
        dtht.Columns.Add(new DataColumn("quantity_year"));
        dtht.Columns.Add(new DataColumn("price_year"));
        dtht.Columns.Add(new DataColumn("psw_quantity"));
        dtht.Columns.Add(new DataColumn("Sales"));
        dtht.Columns.Add(new DataColumn("update_date"));
        dtht.Columns.Add(new DataColumn("update_User"));
        dtht.Columns.Add(new DataColumn("A_2012"));
        dtht.Columns.Add(new DataColumn("QTY_2012"));
        dtht.Columns.Add(new DataColumn("A_2013"));
        dtht.Columns.Add(new DataColumn("QTY_2013"));
        dtht.Columns.Add(new DataColumn("A_2014"));
        dtht.Columns.Add(new DataColumn("QTY_2014"));
        dtht.Columns.Add(new DataColumn("A_2015"));
        dtht.Columns.Add(new DataColumn("QTY_2015"));
        dtht.Columns.Add(new DataColumn("A_2016"));
        dtht.Columns.Add(new DataColumn("QTY_2016"));
        dtht.Columns.Add(new DataColumn("A_2017"));
        dtht.Columns.Add(new DataColumn("QTY_2017"));
        dtht.Columns.Add(new DataColumn("A_2018"));
        dtht.Columns.Add(new DataColumn("QTY_2018"));
        dtht.Columns.Add(new DataColumn("A_2019"));
        dtht.Columns.Add(new DataColumn("QTY_2019"));
        dtht.Columns.Add(new DataColumn("A_2020"));
        dtht.Columns.Add(new DataColumn("QTY_2020"));
        dtht.Columns.Add(new DataColumn("A_2021"));
        dtht.Columns.Add(new DataColumn("QTY_2021"));
        dtht.Columns.Add(new DataColumn("A_2022"));
        dtht.Columns.Add(new DataColumn("QTY_2022"));
        dtht.Columns.Add(new DataColumn("A_2023"));
        dtht.Columns.Add(new DataColumn("QTY_2023"));
        dtht.Columns.Add(new DataColumn("A_2024"));
        dtht.Columns.Add(new DataColumn("QTY_2024"));
        dtht.Columns.Add(new DataColumn("A_2025"));
        dtht.Columns.Add(new DataColumn("QTY_2025"));
        dtht.Columns.Add(new DataColumn("A_2026"));
        dtht.Columns.Add(new DataColumn("QTY_2026"));
        dtht.Columns.Add(new DataColumn("A_2027"));
        dtht.Columns.Add(new DataColumn("QTY_2027"));
        dtht.Columns.Add(new DataColumn("A_2028"));
        dtht.Columns.Add(new DataColumn("QTY_2028"));
       
        //for (int j = 2012; j <= 2028; j++)
        //{
        //    dtht.Columns.Add(new DataColumn("A_" + j));
        //    dtht.Columns.Add(new DataColumn("RF_" + j));
        //}
           

        ViewState["dtht"] = dtht;
        int lnht = gv_Product_Quantity.Rows.Count;
        for (int i = lnht; i < 1; i++)
        {
            DataRow ldr = dtht.NewRow();
            ldr["ID"] = 0;
            ldr["product_status_dt"] = "开发中";
            ldr["psw_quantity"] = 0;
            ldr["price_year"] = 0;
            ldr["quantity_year"] = 0;
            ldr["pc_dj_QAD"] = 0;
            //ldr["ID"] = 0;
            ldr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ldr["update_User"] = txt_update_user_name.Value;
            dtht.Rows.Add(ldr);
        }
        this.gv_Product_Quantity.DataSource = dtht;
        this.gv_Product_Quantity.DataBind();
        gv_Product_Quantity_write();
        DDL_gv_Product_Quantity();

    }
    protected void gv_Product_Quantity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtht = ViewState["dtht"] as DataTable;
        DataTable dtsht = GetDataTableFromGridView1(gv_Product_Quantity,dtht);
        if (e.CommandName == "addQTY")
        {
            DataRow dr = dtsht.NewRow();
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).Parent.Parent);
            dr["ID"] = 0;
            dr["product_status_dt"] = "开发中";
            dr["psw_quantity"] = 0;
            dr["price_year"] = 0;
            dr["quantity_year"] = 0;
            dr["pc_dj_QAD"] = 0;
            dr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["update_User"] = txt_update_user_name.Value;
            dr["colour"] = "green";
            //dr["A_2012"] = 0;
            //dr["A_2013"] = 0;
            //dr["A_2014"] = 0;
            //dr["A_2015"] = 0;
            //dr["A_2016"] = 0;
            //dr["A_2017"] = 0;
            //dr["A_2018"] = 0;
            //dr["A_2019"] = 0;
            //dr["A_2020"] = 0;
            //dr["A_2021"] = 0;
            //dr["A_2022"] = 0;
            //dr["A_2023"] = 0;
            //dr["A_2024"] = 0;
            //dr["A_2025"] = 0;
            //dr["A_2026"] = 0;
            //dr["A_2027"] = 0;
            //dr["A_2028"] = 0;
            

            dtsht.Rows.Add(dr);
            gvcolour();
        }
        if (e.CommandName == "delQTY")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            //if (index == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少保留一行！')", true);
            //}
            //获取实际数量之和
            int A_YEAR = 0;
            for (int i = 12; i < 29; i++)
            {
                if(((TextBox)gv_Product_Quantity.Rows[index].FindControl("QTY_A_20" + i)).Text=="")
                {
                    ((TextBox)gv_Product_Quantity.Rows[index].FindControl("QTY_A_20" + i)).Text = "0";
                }
                A_YEAR = A_YEAR + Convert.ToInt32(((TextBox)gv_Product_Quantity.Rows[index].FindControl("QTY_A_20"+i)).Text.Replace(",",""));
            }
            if (gv_Product_Quantity.Rows.Count == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少保留一行！')", true);
            }
            else if (A_YEAR > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('有实际出货数量，不可删除！')", true);
            }
            else
            {
                dtsht.Rows.RemoveAt(index);
            }
        
        }
        ViewState["dtht"] = dtsht;
        this.gv_Product_Quantity.DataSource = dtsht;
        this.gv_Product_Quantity.DataBind();
        DDL_gv_Product_Quantity();

        ViewState["lv"] = "cpyc";
        gvcolour();
    }
    public void DDL_gv_Product_Quantity()
    {
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {
            //DataTable dt_Sales = Product_CLASS.DDL_base("engineer", "销售工程师");
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).DataSource = Product_CLASS.DDL_base("engineer", "销售","00002");
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).DataTextField = "name";
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).DataValueField = "name";
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).DataBind();
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Items.Insert(0, new ListItem("", ""));
            //((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
            if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("txtSales")).Text != "")
            {
                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Text = ((TextBox)gv_Product_Quantity.Rows[i].FindControl("txtSales")).Text;
            }

            //SHIP_FROM
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).DataSource = Product_CLASS.DDL_base("Product_BASE", "ddl_Ship_from", txt_update_user.Value);
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).DataTextField = "CLASS_NAME";
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).DataValueField = "CLASS_NAME";
            ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).DataBind();
            ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Items.Insert(0, new ListItem("", ""));
            //((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
            if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("Ship_from")).Text != "")
            {
                //((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Text = ((TextBox)gv_Product_Quantity.Rows[i].FindControl("Ship_from")).Text;

                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Items.Insert(0, new ListItem(((TextBox)gv_Product_Quantity.Rows[i].FindControl("Ship_from")).Text));
            }
            if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("khdm")).Text != "")
            {
                string khdm = ((TextBox)gv_Product_Quantity.Rows[i].FindControl("khdm")).Text;
                //SHIP_to
                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).DataSource = Product_CLASS.Form3_Product_ship_to(khdm);
                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).DataTextField = "DebtorShipToName";
                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).DataValueField = "DebtorShipToName";
                ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).DataBind();
                ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Items.Insert(0, new ListItem("", ""));

                if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("Ship_to")).Text != "")
                {

                    ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Items.Insert(0, new ListItem(((TextBox)gv_Product_Quantity.Rows[i].FindControl("Ship_to")).Text));
                }
            }

        }

    }
    //保存按钮
    protected void BTN_version_update_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < gv_Product_version.Rows.Count; i++)
        {
            //防呆
            if (((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Text == "" || ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_Desc_sm")).Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件号和说明不能为空！')", true);
                return;
            }

        }
        using (TransactionScope ts = new TransactionScope())
        {

            int delete_gv = Product_CLASS.delete_gv(Convert.ToInt32(Request["requestid"]), "gv_Product_version");
            if (delete_gv > 0)
            {
                insert_gv_Product_version(Convert.ToInt32(Request["requestid"]));
                //插入日志信息
                insert_form3_Sale_Product_LOG(Convert.ToInt32(Request["requestid"]), "修改", BTN_version_update.Text);
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
                BTN_version_update.Enabled = false;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改或联系管理员！')", true);

            }
            ts.Complete();
        }


    }
    protected void BTN_Quantity_update_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {
            //防呆
            if (((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Text == "" || ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Text == "" || ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('ship_from/顾客项目/批产日期/停产日期/批产单价/销售工程师--不能为空！')", true);
                return;
            }


        }
        using (TransactionScope ts = new TransactionScope())
        {
            DataTable dt_ycxx = Product_CLASS.Getgv(Request["requestid"], "gv_Product_Quantity");
           if (dt_ycxx.Rows.Count > 0)
            {
                //如果有记录，删除记录
                int delete_gv = Product_CLASS.delete_gv(Convert.ToInt32(Request["requestid"]), "gv_Product_Quantity");
                if (delete_gv > 0)
                {

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改或联系管理员！')", true);
                    return;
                }
            }
            //开始存预测信息
            insert_gv_Product_Quantity(Convert.ToInt32(Request["requestid"]));

            //gvcolour_submit();
            //插入日志信息
            insert_form3_Sale_Product_LOG(Convert.ToInt32(Request["requestid"]), "修改", BTN_Quantity_update.Text);
            //修改基础信息中批产日期停产日期和产品状态
            Product_CLASS.BTN_product_update(Convert.ToInt32(Request["requestid"]), DDL_product_leibie.SelectedValue, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, txt_end_date.Value, txt_update_user.Value, txt_pc_date.Value, DDL_product_status.SelectedValue, txt_delete_date.Value, txt_Sales_engineers.Value, txtproduct_img.Text, txt_dingdian_date.Value, txt_customer_requestCN.Value.Replace(",", ""), txt_customer_requestSM.Text, txt_productname.Value);

            //验证批产日期和停产日期的年用量符合规则
            for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
            {
                int ID = Convert.ToInt32(((TextBox)gv_Product_Quantity.Rows[i].FindControl("ID")).Text);
                DataTable dt_ycxx_colour = Product_CLASS.Getgv_colour(ID);
                if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
                {
                    if (dt_ycxx_colour.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt_ycxx_colour.Rows.Count; j++)
                        {
                            string colour = dt_ycxx_colour.Rows[j]["colour"].ToString();
                            if (colour == "red_pc")
                            {
                                ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('小于批产日期包含年用量或批产年份年用量为0！')", true);

                                return;
                            }
                            if (colour == "red_end" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
                            {
                                ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('大于停产日期包含年用量或停产年份年用量为0！')", true);

                                return;
                            }
                            if (colour == "red" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
                            {
                                ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
                                ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('批产日期和停产日期之间不能有0年用量！')", true);
                                DataTable dt_ycxx1 = Product_CLASS.Getgv(Request["requestid"], "gv_Product_Quantity");

                                return;
                            }
                        }
                    }
                }
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
            //BTN_Quantity_update.Enabled = false;


            ts.Complete();
        }
        ViewState["lv"] = "cpyc";
    }
    public void insert_gv_Product_version(int requestid_sq)
    {
        for (int i = 0; i < gv_Product_version.Rows.Count; i++)
        {

            System.Text.StringBuilder strsql = new StringBuilder();
            strsql.Append("insert into form3_Sale_Product_DetailTable(requestId,pgino,productcode,version,Desc_sm,update_date,update_User)");
            strsql.Append("VALUES  ('" + requestid_sq + "', ");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_pgino")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_productcode")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_version")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_Desc_sm")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_date")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_Product_version.Rows[i].FindControl("txt_update_User")).Text + "' ");

            strsql.Append(")");

            DbHelperSQL.ExecuteSql(strsql.ToString());
        }
    }
  
    public void insert_gv_Product_Quantity(int requestid_sq)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
            {
                //相同的ship_to 顾客项目不能插入多笔
                String pgino = txt_pgino.Value;
                string ship_to = ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Text.Trim();
                //string customer_project= ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Text.Trim();
                //DataTable dt_ycxx_xt = Product_CLASS.Form3_Product_xmh_ship_to(pgino, ship_to, customer_project);
                DataTable dt_ycxx_xt = Product_CLASS.Form3_Product_xmh_ship_to(pgino, ship_to);
                if (dt_ycxx_xt.Rows.Count > 0)
                {
                    ((DropDownList)gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).BackColor = System.Drawing.Color.Red;
                    //((TextBox)gv_Product_Quantity.Rows[i].FindControl("customer_project")).BackColor = System.Drawing.Color.Red;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('相同的ship_to且相同客户项目不能插入！')", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('相同的ship_to不能插入！')", true);
                    return;
                }
                else
                {

                    System.Text.StringBuilder strsql = new StringBuilder();
                    strsql.Append("insert into form3_Sale_ProductQuantity_DetailTable(requestId,pgino,ship_from,ship_to,customer_project,khdm,dingdian_date,pc_date,end_date,pc_dj,pc_dj_QAD,quantity_year,price_year,psw_quantity,Sales,QTY_2012,QTY_2013,QTY_2014,");
                    strsql.Append("QTY_2015, QTY_2016, QTY_2017,QTY_2018,QTY_2019,QTY_2020,QTY_2021,QTY_2022,QTY_2023,QTY_2024,QTY_2025,QTY_2026,QTY_2027,QTY_2028,update_date,update_User ");
                    strsql.Append(") VALUES  ('" + requestid_sq + "', ");
                    strsql.Append("'" + txt_pgino.Value + "', ");
                    strsql.Append(" '" + ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_from")).Text.Trim() + "',");
                    strsql.Append(" '" + ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Ship_to")).Text.Trim() + "',");
                    //strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("ship_to")).Text.Trim() + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Text.Trim() + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("khdm")).Text.Trim() + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("dingdian_date")).Text + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date")).Text + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date")).Text + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Text.Replace(",", "") + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj_QAD")).Text.Replace(",", "") + "',");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("quantity_year")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("price_year")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("psw_quantity")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Text + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2012")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2013")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2014")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2015")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2016")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2017")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2018")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2019")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2020")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2021")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2022")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2023")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2024")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2025")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2026")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2027")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("QTY_2028")).Text.Replace(",", "") + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_date")).Text + "' ,");
                    strsql.Append(" '" + ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("update_User")).Text + "' ");
                    strsql.Append(")");

                    DbHelperSQL.ExecuteSql(strsql.ToString());
                }
            }
            DataTable dt_ycxx = Product_CLASS.Getgv(Request["requestid"], "gv_Product_Quantity");
            gv_Product_Quantity.DataSource = dt_ycxx;
            gv_Product_Quantity.DataBind();
            GetProduct_Status_dt();
            DDL_gv_Product_Quantity();
            gvcolour();

            ts.Complete();
        }
        ViewState["lv"] = "cpyc";
    }
    public void insert_form3_Sale_Product_LOG(int requestid_sq, string Update_LB,string Update_content)
    {
        Product_CLASS.insert_form3_Sale_Product_LOG(requestid_sq, txt_update_user_job.Value,txt_update_user.Value,txt_update_user_name.Value, Update_LB, Update_content);
    }
    public void update_baojia()
    {
        Product_CLASS.form3_Sale_update_baojia(txt_baojia_no.Value, txt_productcode.Value);
    }
    protected void dingdian_date_TextChanged(object sender, EventArgs e)
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            TextBox tb = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("dingdian_date");
            if (tb.Text != "")
            {
                if (lsvalue.ToString() == "")
                {
                    lsvalue = Convert.ToDateTime(tb.Text).ToString();
                }
                else
                {
                    if (Convert.ToDateTime(lsvalue) > Convert.ToDateTime(tb.Text))
                    {
                        lsvalue = Convert.ToDateTime(tb.Text).ToString();
                    }
                }
            }
        }
        if (lsvalue != "")
        {
            this.txt_dingdian_date.Value = Convert.ToDateTime(lsvalue).ToString("yyyy-MM-dd");
        }
        else
        {
            this.txt_dingdian_date.Value = "";
        }
        //GetProduct_Status();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }
    protected void pc_date_TextChanged(object sender, EventArgs e)
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            TextBox tb = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date");
            if (tb.Text != "")
            {
                if (lsvalue.ToString() == "")
                {
                    lsvalue = Convert.ToDateTime(tb.Text).ToString();
                }
                else
                {
                    if (Convert.ToDateTime(lsvalue) > Convert.ToDateTime(tb.Text))
                    {
                        lsvalue = Convert.ToDateTime(tb.Text).ToString();
                    }
                }
            }
        }
        if (lsvalue != "")
        {       
            this.txt_pc_date.Value = Convert.ToDateTime(lsvalue).ToString("yyyy-MM-dd");
        }
        else
        {
            this.txt_pc_date.Value = "";
        }
        GetProduct_Status();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }

    protected void end_date_TextChanged(object sender, EventArgs e)
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            TextBox tb = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date");
            if (tb.Text != "")
            {
                if (lsvalue.ToString() == "")
                {
                    lsvalue = Convert.ToDateTime(tb.Text).ToString();
                }
                else
                {
                    if (Convert.ToDateTime(lsvalue) < Convert.ToDateTime(tb.Text))
                    {
                        lsvalue = Convert.ToDateTime(tb.Text).ToString();
                    }
                }
            }
        }
        if (lsvalue != "")
        {
            this.txt_end_date.Value = Convert.ToDateTime(lsvalue).ToString("yyyy-MM-dd");
        }
        else
        {
            this.txt_end_date.Value = "";
        }
      
        GetProduct_Status();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }
    private void GetProduct_Status_dt()
    {
        string now_date = System.DateTime.Now.ToString("yyyy-MM-dd");

        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            TextBox pc_date = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date");
            TextBox end_date = (TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date");
            if (pc_date.Text != "")
            {
                if (DateTime.Compare(Convert.ToDateTime(now_date), Convert.ToDateTime(pc_date.Text)) < 0)
                {
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text = "开发中";
     
                }
            }
            if (end_date.Text != "")
            {
                if (DateTime.Compare(Convert.ToDateTime(end_date.Text), Convert.ToDateTime(now_date)) <= 0)
                {
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text = "停产";
                }
            }
            if (pc_date.Text != "" && end_date.Text != "")
            {
                if (DateTime.Compare(Convert.ToDateTime(pc_date.Text), Convert.ToDateTime(now_date)) <= 0 && DateTime.Compare(Convert.ToDateTime(end_date.Text), Convert.ToDateTime(now_date)) > 0)
                {
  
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text = "生产中";
                }
            }
            if (txt_delete_date.Value != "")
            {
                if (DateTime.Compare(Convert.ToDateTime(now_date), Convert.ToDateTime(txt_delete_date.Value)) >= 0)
                {
                    ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text = "项目取消";
                }
            }
        }
    }
    private void GetProduct_Status()
    {
        GetProduct_Status_dt();
        string now_date = System.DateTime.Now.ToString("yyyy-MM-dd");
      
        if (txt_pc_date.Value != "")
        {
            if (DateTime.Compare(Convert.ToDateTime(now_date), Convert.ToDateTime(txt_pc_date.Value)) < 0)
            {
                DDL_product_status.SelectedValue = "开发中";
            }
        }
        if (txt_end_date.Value != "")
        {
            if (DateTime.Compare(Convert.ToDateTime(txt_end_date.Value), Convert.ToDateTime(now_date)) <= 0)
            {
                DDL_product_status.SelectedValue = "停产";
            }
        }
        if (txt_pc_date.Value != "" && txt_end_date.Value != "")
        {
            if (DateTime.Compare(Convert.ToDateTime(txt_pc_date.Value), Convert.ToDateTime(now_date)) <= 0 && DateTime.Compare(Convert.ToDateTime(txt_end_date.Value), Convert.ToDateTime(now_date)) > 0)
            {
                DDL_product_status.SelectedValue = "生产中";
            }
        }
        if (txt_delete_date.Value != "")
        {
            if (DateTime.Compare(Convert.ToDateTime(now_date), Convert.ToDateTime(txt_delete_date.Value)) >= 0)
            {
                DDL_product_status.SelectedValue = "项目取消";
            }
        }

    }
    protected void BTN_Sales_sub_Click(object sender, EventArgs e)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            if (DateTime.Compare(Convert.ToDateTime(txt_end_date.Value), Convert.ToDateTime(txt_pc_date.Value)) < 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('停产日期不能小于批产日期！')", true);
                return;
            }
         
            if (gv_Product_Quantity.Rows.Count > 0)
            {
                for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
                {
                    //防呆
                    if (((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_date")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("customer_project")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("end_date")).Text == "" || ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Text == "" || ((DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales")).Text == "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('客户代码/顾客项目/批产日期/停产日期/批产单价/销售工程师--不能为空！')", true);
                        return;
                    }




                }
             
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少维护一项预测信息！')", true);
                return;
            }
            if (BTN_Sales_submit.Text == "提交")
            {
                //新增的表单产生序列号requestid
                string sql = "Select next value for  [dbo].[form3_product_requestid_asqc]";
                int requestid_sq = Convert.ToInt16(DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());
                int insert = -1;
                insert = Product_CLASS.BTN_Sales_sub(requestid_sq, txt_Code.Value, txt_CreateDate.Value, txt_create_by_empid.Value, txt_create_by_name.Value, txt_create_by_ad.Value, txt_create_by_dept.Value,
                    txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_Sales_engineers.Value, txt_baojia_no.Value,txt_pgino.Value, txt_productcode.Value, txt_productname.Value
                    , txt_make_factory.Value, DDL_product_leibie.SelectedValue, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue,
                    txt_customer_project.Value, txt_end_date.Value, txt_dingdian_date.Value,txt_delete_date.Value, DDL_product_status.SelectedValue, txtproduct_img.Text, txt_update_user.Value
                    , txt_pc_date.Value,txt_customer_requestCN.Value.Replace(",", ""), txt_customer_requestSM.Text, DDL_project_user.SelectedValue, DDL_product_user.SelectedValue, DDL_moju_user.SelectedValue,
                    DDL_yz_user.SelectedValue, DDL_jj_user.SelectedValue, DDL_zl_user.SelectedValue, DDL_bz_user.SelectedValue, DDL_wl_user.SelectedValue
                    , DDL_zhiliangzhuguan_user.SelectedValue, DDL_sqe_user1.SelectedValue, DDL_sqe_user2.SelectedValue, DDL_caigou.SelectedValue, DDL_sale.SelectedValue
                    , DDL_jiaju_egnieer.SelectedValue, DDL_daoju_egnieer.SelectedValue, DDL_jianju_egnieer.SelectedValue, DDL_mojugl_egnieer.SelectedValue
                    , BTN_Sales_submit.Text);
                if (insert > 0)
                {
                    //插入版本记录
                    insert_gv_Product_version(requestid_sq);
                    //插入预测信息
                    insert_gv_Product_Quantity(requestid_sq);
                    //插入日志信息
                    insert_form3_Sale_Product_LOG(requestid_sq,"提交","产品新增");
                    //修改报价中状态为已产生项目号（是）
                    update_baojia();
                    BTN_Sales_submit.Enabled = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交或联系管理员！')", true);
                }
            }

            ts.Complete();
        }

    }
    //页面加载
    public void gvbangding()
    {
        //加载版本明细
        DataTable dt_banben = Product_CLASS.Getgv(Request["requestid"], "gv_Product_version");
        gv_Product_version.DataSource = dt_banben;
        gv_Product_version.DataBind();
        ViewState["dt"] = dt_banben;


        //加载预测明细
        DataTable dt_ycxx = Product_CLASS.Getgv(Request["requestid"], "gv_Product_Quantity");
        gv_Product_Quantity.DataSource = dt_ycxx;
        gv_Product_Quantity.DataBind();
        ViewState["dtht"] = dt_ycxx;
        DDL_gv_Product_Quantity(); 
        if (gv_Product_Quantity.Rows.Count == 0)
        {
            gv_Product_Quantity_InitTable();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }
    public void gvcolour()
    {
        
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {
            int ID = Convert.ToInt32(((TextBox)gv_Product_Quantity.Rows[i].FindControl("ID")).Text);
            DataTable dt_ycxx_colour = Product_CLASS.Getgv_colour(ID);
            if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
            {
                if (dt_ycxx_colour.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_ycxx_colour.Rows.Count; j++)
                    {
                        string colour = dt_ycxx_colour.Rows[j]["colour"].ToString();
                        if (colour == "red_pc")
                        {
                            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('小于批产日期包含年用量或批产年份年用量为0！')", true);
                            //return;
                        }
                        if (colour == "red_end" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
                        {
                            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('大于停产日期包含年用量或停产年份年用量为0！')", true);
                            //return;
                        }
                        if (colour == "red" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
                        {
                            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
                            ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('批产日期和停产日期之间不能有0年用量！')", true);
                            //return;
                        }
                    }
                }
            }
          
            //    if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("colour")).Text == "red_pc" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
            //{
            //    ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('小于批产日期包含年用量或批产年份年用量为0！')", true);
            //    //return;
            //}
            //if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("colour")).Text == "red_end" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
            //{
            //    ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('大于停产日期包含年用量或停产年份年用量为0！')", true);
            //    //return;
            //}
            //if (((TextBox)gv_Product_Quantity.Rows[i].FindControl("colour")).Text == "red" && ((TextBox)gv_Product_Quantity.Rows[i].FindControl("product_status_dt")).Text != "项目取消")
            //{
            //    ((TextBox)gv_Product_Quantity.Rows[i].FindControl("pc_date")).BackColor = System.Drawing.Color.Red;
            //    ((TextBox)gv_Product_Quantity.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('批产日期和停产日期之间不能有0年用量！')", true);
            //    //return;
            //}
        }

    }


    public void Query()
    {
        select_ljh.Visible = false;
        txt_CP_ID.Visible = false;
        int requestid = Convert.ToInt32(Request["requestid"]);
        DataTable dt = Product_CLASS.form3_Sale_Product_Query_PRO(requestid);
        txt_CreateDate.Value = dt.Rows[0]["CreateDate"].ToString();
        txt_create_by_empid.Value = dt.Rows[0]["Userid"].ToString();
        txt_create_by_name.Value = dt.Rows[0]["UserName"].ToString();
        txt_create_by_ad.Value = dt.Rows[0]["UserName_AD"].ToString();
        txt_create_by_dept.Value = dt.Rows[0]["dept"].ToString();
        txt_managerid.Value = dt.Rows[0]["managerid"].ToString();
        txt_manager.Value = dt.Rows[0]["manager"].ToString();
        txt_manager_AD.Value = dt.Rows[0]["manager_AD"].ToString();
        txt_Code.Value = dt.Rows[0]["Code"].ToString();

        txt_Sales_engineers.Value = dt.Rows[0]["Sales_engineers"].ToString();
        txt_baojia_no.Value = dt.Rows[0]["baojia_no"].ToString();
        txt_pgino.Value = dt.Rows[0]["pgino"].ToString();
        txt_productcode.Value = dt.Rows[0]["productcode"].ToString();
        txt_productname.Value = dt.Rows[0]["productname"].ToString();
        txt_make_factory.Value = dt.Rows[0]["make_factory"].ToString();
        DDL_product_leibie.SelectedValue = dt.Rows[0]["product_leibie"].ToString();
        DDL_customer_name.SelectedValue = dt.Rows[0]["customer_name"].ToString();
        DDL_end_customer_name.SelectedValue = dt.Rows[0]["end_customer_name"].ToString();
        txt_customer_project.Value = dt.Rows[0]["customer_project"].ToString();
        
        if(dt.Rows[0]["customer_requestCN"].ToString()!="" && dt.Rows[0]["customer_requestCN"].ToString() != "0")
        {
            int sl = Convert.ToInt32(dt.Rows[0]["customer_requestCN"]);
            txt_customer_requestCN.Value = string.Format("{0:N0}", sl);
        }
       


        txt_customer_requestSM.Text = dt.Rows[0]["customer_requestSM"].ToString();
        if (dt.Rows[0]["dingdian_date"].ToString() != "")
        {
            txt_dingdian_date.Value = Convert.ToDateTime(dt.Rows[0]["dingdian_date"].ToString()).ToString("yyyy-MM-dd");

        }
        if (dt.Rows[0]["pc_date"].ToString() != "")
        {
            txt_pc_date.Value = Convert.ToDateTime(dt.Rows[0]["pc_date"].ToString()).ToString("yyyy-MM-dd");

        }

        if (dt.Rows[0]["end_date"].ToString() != "")
        {
            txt_end_date.Value = Convert.ToDateTime(dt.Rows[0]["end_date"].ToString()).ToString("yyyy-MM-dd");

        }

        if (dt.Rows[0]["delete_date"].ToString() != "")
        {
            txt_delete_date.Value = Convert.ToDateTime(dt.Rows[0]["delete_date"].ToString()).ToString("yyyy-MM-dd");

        }

        this.Image2.ImageUrl = dt.Rows[0]["product_img"].ToString();
        this.Image2.Visible = true;
        txtproduct_img.Text = dt.Rows[0]["product_img"].ToString();

        GetProduct_Status();
        DataTable dtversion = Product_CLASS.Product_version_new(Request["requestid"]);
        txt_productcode.Value = dtversion.Rows[0]["productcode"].ToString();
        txt_version.Value = dtversion.Rows[0]["version"].ToString();

        DataTable dtXMH = Product_CLASS.form3_Sale_V_Track(txt_pgino.Value);
        if (dtXMH.Rows.Count > 0)
        {
         
            DDL_project_user.SelectedValue = dtXMH.Rows[0]["project_user"].ToString();
            if (dtXMH.Rows[0]["project_user"].ToString() != "")
            {
                DDL_project_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["project_user"].ToString(), dtXMH.Rows[0]["project_user"].ToString()));
            }
            DDL_product_user.SelectedValue = dtXMH.Rows[0]["product_user"].ToString();
            if (dtXMH.Rows[0]["product_user"].ToString() != "")
            {
                DDL_product_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["product_user"].ToString(), dtXMH.Rows[0]["product_user"].ToString()));
            }
            DDL_moju_user.SelectedValue = dtXMH.Rows[0]["moju_user"].ToString();
            if (dtXMH.Rows[0]["moju_user"].ToString() != "")
            {
                DDL_moju_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["moju_user"].ToString(), dtXMH.Rows[0]["moju_user"].ToString()));
            }
            DDL_yz_user.SelectedValue = dtXMH.Rows[0]["yz_user"].ToString();
            if (dtXMH.Rows[0]["yz_user"].ToString() != "")
            {
                DDL_yz_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["yz_user"].ToString(), dtXMH.Rows[0]["yz_user"].ToString()));
            }
            DDL_jj_user.SelectedValue = dtXMH.Rows[0]["jj_user"].ToString();
            if (dtXMH.Rows[0]["jj_user"].ToString() != "")
            {
                DDL_jj_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["jj_user"].ToString(), dtXMH.Rows[0]["jj_user"].ToString()));
            }
            DDL_zl_user.SelectedValue = dtXMH.Rows[0]["zl_user"].ToString();
            if (dtXMH.Rows[0]["zl_user"].ToString() != "")
            {
                DDL_zl_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["zl_user"].ToString(), dtXMH.Rows[0]["zl_user"].ToString()));
            }
            DDL_bz_user.SelectedValue = dtXMH.Rows[0]["bz_user"].ToString();
            if (dtXMH.Rows[0]["bz_user"].ToString() != "")
            {
                DDL_bz_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["bz_user"].ToString(), dtXMH.Rows[0]["bz_user"].ToString()));           
            }
            DDL_wl_user.SelectedValue = dtXMH.Rows[0]["wl_user"].ToString();
            if (dtXMH.Rows[0]["wl_user"].ToString() != "")
            {
                DDL_wl_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["wl_user"].ToString(), dtXMH.Rows[0]["wl_user"].ToString()));
            }
            DDL_zhiliangzhuguan_user.SelectedValue = dtXMH.Rows[0]["zhiliangzhuguan_user"].ToString();
            if (dtXMH.Rows[0]["zhiliangzhuguan_user"].ToString() != "")
            {
                DDL_zhiliangzhuguan_user.Items.Insert(0, new ListItem(dtXMH.Rows[0]["zhiliangzhuguan_user"].ToString(), dtXMH.Rows[0]["zhiliangzhuguan_user"].ToString()));
            }
            DDL_sqe_user1.SelectedValue = dtXMH.Rows[0]["sqe_user1"].ToString();
            if (dtXMH.Rows[0]["sqe_user1"].ToString() != "")
            {
                DDL_sqe_user1.Items.Insert(0, new ListItem(dtXMH.Rows[0]["sqe_user1"].ToString(), dtXMH.Rows[0]["sqe_user1"].ToString()));
            }
            DDL_sqe_user2.SelectedValue = dtXMH.Rows[0]["sqe_user2"].ToString();
            if (dtXMH.Rows[0]["sqe_user2"].ToString() != "")
            {
                DDL_sqe_user2.Items.Insert(0, new ListItem(dtXMH.Rows[0]["sqe_user2"].ToString(), dtXMH.Rows[0]["sqe_user2"].ToString()));
            }
            DDL_caigou.SelectedValue = dtXMH.Rows[0]["caigou"].ToString();
            if (dtXMH.Rows[0]["caigou"].ToString() != "")
            {
                DDL_caigou.Items.Insert(0, new ListItem(dtXMH.Rows[0]["caigou"].ToString(), dtXMH.Rows[0]["caigou"].ToString()));
            }
            DDL_sale.SelectedValue = dtXMH.Rows[0]["sale"].ToString();
            if (dtXMH.Rows[0]["sale"].ToString() != "")
            {
                DDL_sale.Items.Insert(0, new ListItem(dtXMH.Rows[0]["sale"].ToString(), dtXMH.Rows[0]["sale"].ToString()));

            }
            DDL_jiaju_egnieer.SelectedValue = dtXMH.Rows[0]["jiaju_egnieer"].ToString();
            if (dtXMH.Rows[0]["jiaju_egnieer"].ToString() != "")
            {
                DDL_jiaju_egnieer.Items.Insert(0, new ListItem(dtXMH.Rows[0]["jiaju_egnieer"].ToString(), dtXMH.Rows[0]["jiaju_egnieer"].ToString()));
            }
            DDL_daoju_egnieer.SelectedValue = dtXMH.Rows[0]["daoju_egnieer"].ToString();
            if (dtXMH.Rows[0]["daoju_egnieer"].ToString() != "")
            {
                DDL_daoju_egnieer.Items.Insert(0, new ListItem(dtXMH.Rows[0]["daoju_egnieer"].ToString(), dtXMH.Rows[0]["daoju_egnieer"].ToString()));
            }
            DDL_jianju_egnieer.SelectedValue = dtXMH.Rows[0]["jianju_egnieer"].ToString();
            if (dtXMH.Rows[0]["jianju_egnieer"].ToString() != "")
            {
                DDL_jianju_egnieer.Items.Insert(0, new ListItem(dtXMH.Rows[0]["jianju_egnieer"].ToString(), dtXMH.Rows[0]["jianju_egnieer"].ToString()));
            }
            DDL_mojugl_egnieer.SelectedValue = dtXMH.Rows[0]["mojugl_egnieer"].ToString();
            if (dtXMH.Rows[0]["mojugl_egnieer"].ToString() != "")
            {
                DDL_mojugl_egnieer.Items.Insert(0, new ListItem(dtXMH.Rows[0]["mojugl_egnieer"].ToString(), dtXMH.Rows[0]["mojugl_egnieer"].ToString()));
            }
            //if (dtXMH.Rows[0]["status"].ToString()== "项目取消")
            //{
            //    DDL_product_status.SelectedValue = "项目取消";
            //}
        }
        else
        {
            DDL_project_user.SelectedValue = dt.Rows[0]["project_user"].ToString();
            if (dt.Rows[0]["project_user"].ToString() != "")
            {
                DDL_project_user.Items.Insert(0, new ListItem(dt.Rows[0]["project_user"].ToString(), dt.Rows[0]["project_user"].ToString()));
            }
            DDL_product_user.SelectedValue = dt.Rows[0]["product_user"].ToString();
            if (dt.Rows[0]["product_user"].ToString() != "")
            {
                DDL_product_user.Items.Insert(0, new ListItem(dt.Rows[0]["product_user"].ToString(), dt.Rows[0]["product_user"].ToString()));
            }
            DDL_moju_user.SelectedValue = dt.Rows[0]["moju_user"].ToString();
            if (dt.Rows[0]["product_user"].ToString() != "")
            {
                DDL_moju_user.Items.Insert(0, new ListItem(dt.Rows[0]["moju_user"].ToString(), dt.Rows[0]["moju_user"].ToString()));
            }
            DDL_yz_user.SelectedValue = dt.Rows[0]["yz_user"].ToString();
            if (dt.Rows[0]["yz_user"].ToString() != "")
            {
                DDL_yz_user.Items.Insert(0, new ListItem(dt.Rows[0]["yz_user"].ToString(), dt.Rows[0]["yz_user"].ToString()));
            }
            DDL_jj_user.SelectedValue = dt.Rows[0]["jj_user"].ToString();
            if (dt.Rows[0]["jj_user"].ToString() != "")
            {
                DDL_jj_user.Items.Insert(0, new ListItem(dt.Rows[0]["jj_user"].ToString(), dt.Rows[0]["jj_user"].ToString()));
            }
            DDL_zl_user.SelectedValue = dt.Rows[0]["zl_user"].ToString();
            if (dt.Rows[0]["zl_user"].ToString() != "")
            {
                DDL_zl_user.Items.Insert(0, new ListItem(dt.Rows[0]["zl_user"].ToString(), dt.Rows[0]["zl_user"].ToString()));
            }
            DDL_bz_user.SelectedValue = dt.Rows[0]["bz_user"].ToString();
            if (dt.Rows[0]["bz_user"].ToString() != "")
            {
                DDL_bz_user.Items.Insert(0, new ListItem(dt.Rows[0]["bz_user"].ToString(), dt.Rows[0]["bz_user"].ToString()));
            }
            DDL_wl_user.SelectedValue = dt.Rows[0]["wl_user"].ToString();
            if (dt.Rows[0]["wl_user"].ToString() != "")
            {
                DDL_wl_user.Items.Insert(0, new ListItem(dt.Rows[0]["wl_user"].ToString(), dt.Rows[0]["wl_user"].ToString()));
            }
            DDL_zhiliangzhuguan_user.SelectedValue = dt.Rows[0]["zhiliangzhuguan_user"].ToString();
            if (dt.Rows[0]["zhiliangzhuguan_user"].ToString() != "")
            {
                DDL_zhiliangzhuguan_user.Items.Insert(0, new ListItem(dt.Rows[0]["zhiliangzhuguan_user"].ToString(), dt.Rows[0]["zhiliangzhuguan_user"].ToString()));
            }
            DDL_sqe_user1.SelectedValue = dt.Rows[0]["sqe_user1"].ToString();
            if (dt.Rows[0]["sqe_user1"].ToString() != "")
            {
                DDL_sqe_user1.Items.Insert(0, new ListItem(dt.Rows[0]["sqe_user1"].ToString(), dt.Rows[0]["sqe_user1"].ToString()));
            }
            DDL_sqe_user2.SelectedValue = dt.Rows[0]["sqe_user2"].ToString();
            if (dt.Rows[0]["sqe_user2"].ToString() != "")
            {
                DDL_sqe_user2.Items.Insert(0, new ListItem(dt.Rows[0]["sqe_user2"].ToString(), dt.Rows[0]["sqe_user2"].ToString()));
            }

            DDL_caigou.SelectedValue = dt.Rows[0]["caigou"].ToString();
            if (dt.Rows[0]["caigou"].ToString() != "")
            {
                DDL_caigou.Items.Insert(0, new ListItem(dt.Rows[0]["caigou"].ToString(), dt.Rows[0]["caigou"].ToString()));
            }
            DDL_sale.SelectedValue = dt.Rows[0]["sale"].ToString();
            if (dt.Rows[0]["sale"].ToString() != "")
            {
                DDL_sale.Items.Insert(0, new ListItem(dt.Rows[0]["sale"].ToString(), dt.Rows[0]["sale"].ToString()));
            }
            DDL_jiaju_egnieer.SelectedValue = dt.Rows[0]["jiaju_egnieer"].ToString();
            if (dt.Rows[0]["jiaju_egnieer"].ToString() != "")
            {
                DDL_jiaju_egnieer.Items.Insert(0, new ListItem(dt.Rows[0]["jiaju_egnieer"].ToString(), dt.Rows[0]["jiaju_egnieer"].ToString()));
            }
            DDL_daoju_egnieer.SelectedValue = dt.Rows[0]["daoju_egnieer"].ToString();
            if (dt.Rows[0]["daoju_egnieer"].ToString() != "")
            {
                DDL_daoju_egnieer.Items.Insert(0, new ListItem(dt.Rows[0]["daoju_egnieer"].ToString(), dt.Rows[0]["daoju_egnieer"].ToString()));
            }
            DDL_jianju_egnieer.SelectedValue = dt.Rows[0]["jianju_egnieer"].ToString();
            if (dt.Rows[0]["jianju_egnieer"].ToString() != "")
            {
                DDL_jianju_egnieer.Items.Insert(0, new ListItem(dt.Rows[0]["jianju_egnieer"].ToString(), dt.Rows[0]["jianju_egnieer"].ToString()));
            }
            DDL_mojugl_egnieer.SelectedValue = dt.Rows[0]["mojugl_egnieer"].ToString();
            if (dt.Rows[0]["mojugl_egnieer"].ToString() != "")
            {
                DDL_mojugl_egnieer.Items.Insert(0, new ListItem(dt.Rows[0]["mojugl_egnieer"].ToString(), dt.Rows[0]["mojugl_egnieer"].ToString()));
            }
        }
      
        gvbangding();
        //判断客户要求产能和合计年用量比较
        pdyq();
        Sales_engineers_all();
        customer_project_all();
        GetProduct_Status_dt();
    }


    public void pdyq()
    {
        //判断客户要求产能和合计年用量比较
        DataTable dt_maxqty = Product_CLASS.form3_SUM_MAX(txt_pgino.Value, "QTY");
        if (dt_maxqty.Rows.Count > 0)
        {
            int max = Convert.ToInt32(dt_maxqty.Rows[0]["QTY_year"]) + Convert.ToInt32(Convert.ToInt32(dt_maxqty.Rows[0]["QTY_year"]) * 0.1);
            int min = Convert.ToInt32(dt_maxqty.Rows[0]["QTY_year"]) - Convert.ToInt32(Convert.ToInt32(dt_maxqty.Rows[0]["QTY_year"]) * 0.1);
            if (txt_customer_requestCN.Value != "")
            {
                int khyqsl = Convert.ToInt32(txt_customer_requestCN.Value.Replace(",", ""));
                if (khyqsl > max || khyqsl < min)
                {
                    this.txt_customer_requestCN.Style.Add("background-color", "red");
                }
                else
                {
                    this.txt_customer_requestCN.Style.Add("background-color", "white");
                }
                txt_customer_requestCN.Value = String.Format("{0:N0}", Convert.ToInt32(txt_customer_requestCN.Value.Replace(",", "")));
            }
            else
            {
                txt_customer_requestCN.Value = String.Format("{0:N0}", Convert.ToInt32(dt_maxqty.Rows[0]["QTY_year"]));

            }
        }
    
      

    }

    public void BindGridBjMX(DataTable dt, GridView gv)
    {
        gv_Product_version.DataSource = dt;
        gv_Product_version.DataBind();
        //for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        //{
        //    DataTable dtljh = Product_CLASS.BJ_ljh(Request["requestid"], "xz_ljh_dt");
        //    //dtqq.Rows.Add("");
        //    ((DropDownList)gv_Product_version.Rows[i].FindControl("ddl_ljh_dt")).DataSource = dtljh;
        //    ((DropDownList)gv_Product_version.Rows[i].FindControl("ddl_ljh_dt")).DataTextField = "xz_ljh";
        //    ((DropDownList)gv_Product_version.Rows[i].FindControl("ddl_ljh_dt")).DataValueField = "xz_ljh";
        //    ((DropDownList)this.gv_Product_version.Rows[i].FindControl("ddl_ljh_dt")).DataBind();
        //    ((DropDownList)gv_Product_version.Rows[i].FindControl("ddl_ljh_dt")).Items.Insert(0, new ListItem("", ""));
        //}
    }
    public static DataTable GetDataTableFromGridView1(GridView gv, DataTable dts)
    {
        DataTable dt = new DataTable();
        dt = dts.Clone();
        dt.Rows.Clear();
       GridViewRow hr = gv.HeaderRow;
        for (int j = 0; j < gv.Rows.Count; j++)
        {
            DataRow dr = dt.NewRow();
             
            for (int i = 0; i < gv.Columns.Count - 1; i++)
            {
                if (hr.Cells[i].Controls.Count > 1)
                {
                    string headValue = ((Label)hr.Cells[i].Controls[1]).Text;//年份
                    if (IsNumberic(headValue) && gv.Rows[j].Cells[i].Controls[1].GetType().Name == "TextBox")
                    {
                        if (((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text != "")
                        {
                            dr["A_" + headValue] = ((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();
                        }
                       // dr["RF_" + headValue] = ((TextBox)gv.Rows[j].Cells[i].Controls[3]).Text.Trim();
                        if (((TextBox)gv.Rows[j].Cells[i].Controls[3]).Text != "")
                        {
                            dr["QTY_" + headValue] = ((TextBox)gv.Rows[j].Cells[i].Controls[3]).Text.Trim();
                        }
                        
                    }
                    else 
                    {
                        dr[i] = ((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();
                    }         
                    
                }
                   

                else if (gv.Rows[j].Cells[i].Controls[1].GetType().Name == "TextBox")
                    {
                        if (((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text != "")
                        {
                            dr[i] = ((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();
                        }
                    }
                   
                    else if (gv.Rows[j].Cells[i].Controls[1].GetType().Name == "DropDownList")
                    {
                        dr[i ] = ((DropDownList)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();
                    }
                    else
                    {

                    }
                
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    private decimal num_quantity_year = 0;
    private decimal num_price_year = 0;
    private decimal num_psw_quantity = 0;
    private decimal num_QTY_2012 = 0;
    private decimal num_QTY_2013 = 0;
    private decimal num_QTY_2014 = 0;
    private decimal num_QTY_2015 = 0;
    private decimal num_QTY_2016 = 0;
    private decimal num_QTY_2017 = 0;
    private decimal num_QTY_2018 = 0;
    private decimal num_QTY_2019 = 0;
    private decimal num_QTY_2020 = 0;
    private decimal num_QTY_2021 = 0;
    private decimal num_QTY_2022 = 0;
    private decimal num_QTY_2023 = 0;
    private decimal num_QTY_2024 = 0;
    private decimal num_QTY_2025 = 0;
    private decimal num_QTY_2026 = 0;
    private decimal num_QTY_2027 = 0;
    private decimal num_QTY_2028 = 0;

    private decimal num_A_2012 = 0;
    private decimal num_A_2013 = 0;
    private decimal num_A_2014 = 0;
    private decimal num_A_2015 = 0;
    private decimal num_A_2016 = 0;
    private decimal num_A_2017 = 0;
    private decimal num_A_2018 = 0;
    private decimal num_A_2019 = 0;
    private decimal num_A_2020 = 0;
    private decimal num_A_2021 = 0;
    private decimal num_A_2022 = 0;
    private decimal num_A_2023 = 0;
    private decimal num_A_2024 = 0;
    private decimal num_A_2025 = 0;
    private decimal num_A_2026 = 0;
    private decimal num_A_2027 = 0;
    private decimal num_A_2028 = 0;
    private decimal num_RF_2012 = 0;
    private decimal num_RF_2013 = 0;
    private decimal num_RF_2014 = 0;
    private decimal num_RF_2015 = 0;
    private decimal num_RF_2016 = 0;
    private decimal num_RF_2017 = 0;
    private decimal num_RF_2018 = 0;
    private decimal num_RF_2019 = 0;
    private decimal num_RF_2020 = 0;
    private decimal num_RF_2021 = 0;
    private decimal num_RF_2022 = 0;
    private decimal num_RF_2023 = 0;
    private decimal num_RF_2024 = 0;
    private decimal num_RF_2025 = 0;
    private decimal num_RF_2026 = 0;
    private decimal num_RF_2027 = 0;
    private decimal num_RF_2028 = 0;
    protected void gv_Product_Quantity_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //int Curryear = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
        //if (e.Row.RowType == DataControlRowType.DataRow ||
        //    e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[2].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        //}
    }
    protected void gv_Product_Quantity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int Curryear = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
        //int Curryear = 2018;
        if (e.Row.RowType == DataControlRowType.DataRow )
        {

            DataRowView dr = e.Row.DataItem as DataRowView;

            //添加可点击Link 及前端识别属性：name
            for (int i = 14; i < e.Row.Cells.Count - 1; i++)
            {

                GridViewRow hr = gv_Product_Quantity.HeaderRow;

                if (hr.Cells[i].Controls.Count > 1)
                {
                    string headValue = ((Label)hr.Cells[i].Controls[1]).Text;//年份
                    if (IsNumberic(headValue))
                    {


                        TextBox txtbox = (TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue);
                        txtbox.Attributes.Add("QTY", "QTY");// 包含QTY属性的input
                        txtbox.Attributes.Add("rowvalue", dr.Row["ID"].ToString());
                        txtbox.Attributes.Add("headvalue", headValue);//列值

                        if (Convert.ToInt16(headValue) == Curryear)
                        {

                            ((TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue)).ReadOnly = true;
                            ((TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue)).Style.Add("background-color", "#CCCCCC");

                        }
                        else if (Convert.ToInt16(headValue) > Curryear)
                        {
                            ((TextBox)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Visible = false;
                            ((Label)hr.Cells[i].FindControl("lb_A_" + headValue)).Visible = false;
                            //((TextBox)e.Row.Cells[i].FindControl("QTY_RF_" + headValue)).Visible = true;
                            ((TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue)).Visible = true;

                        }
                        else if (Convert.ToInt16(headValue) < Curryear)
                        {
                            ((TextBox)e.Row.Cells[i].FindControl("QTY_A_" + headValue)).Visible = true;
                            ((TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue)).Visible = false;
                            ((Label)hr.Cells[i].FindControl("lb_F_" + headValue)).Visible = false;
                            //只显示当前年前两年的数据
                            if (Curryear - Convert.ToInt16(headValue) > 2)
                            {
                                gv_Product_Quantity.Columns[i].Visible = false;

                            }

                        }




                    }
                }

            }
            #region "Sum"
            //年用量
            if (dr.Row["quantity_year"].ToString() != "")
            {

                num_quantity_year += Convert.ToDecimal(dr.Row["quantity_year"].ToString().Replace(",", ""));

            }
            else
            {
                num_quantity_year = 0;
            }
            if (dr.Row["price_year"].ToString() != "")
            {
                num_price_year += Convert.ToDecimal(dr.Row["price_year"].ToString().Replace(",", ""));
            }
            else
            {
                num_price_year = 0;

            }
            if (dr.Row["psw_quantity"].ToString() != "")
            {
                num_psw_quantity += Convert.ToDecimal(dr.Row["psw_quantity"].ToString().Replace(",", ""));
            }
            else
            {
                num_psw_quantity = 0;

            }

            //for (int i = 2012; i <= 2018; i++)
            //{
            //if (dr.Row["A_" + i].ToString() != "")
            //{
            //    num_QTY_A_2017= Convert.ToDecimal(dr.Row["A_" + i].ToString().Replace(",", ""));
            //}

            //}
            if (dr.Row["QTY_2012"].ToString() != "")
            {
                num_QTY_2012 += Convert.ToDecimal(dr.Row["QTY_2012"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2012 = 0;

            }
            if (dr.Row["QTY_2013"].ToString() != "")
            {
                num_QTY_2013 += Convert.ToDecimal(dr.Row["QTY_2013"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2013 = 0;

            }
            if (dr.Row["QTY_2014"].ToString() != "")
            {
                num_QTY_2014 += Convert.ToDecimal(dr.Row["QTY_2014"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2014 = 0;

            }
            if (dr.Row["QTY_2015"].ToString() != "")
            {
                num_QTY_2015 += Convert.ToDecimal(dr.Row["QTY_2015"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2015 = 0;

            }
            if (dr.Row["QTY_2016"].ToString() != "")
            {
                num_QTY_2016 += Convert.ToDecimal(dr.Row["QTY_2016"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2016 = 0;

            }
            if (dr.Row["QTY_2017"].ToString() != "")
            {
                num_QTY_2017 += Convert.ToDecimal(dr.Row["QTY_2017"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2017 = 0;

            }
            if (dr.Row["QTY_2018"].ToString() != "")
            {
                num_QTY_2018 += Convert.ToDecimal(dr.Row["QTY_2018"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2018 = 0;

            }
            if (dr.Row["QTY_2019"].ToString() != "")
            {
                num_QTY_2019 += Convert.ToDecimal(dr.Row["QTY_2019"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2019 = 0;

            }
            if (dr.Row["QTY_2020"].ToString() != "")
            {
                num_QTY_2020 += Convert.ToDecimal(dr.Row["QTY_2020"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2020 = 0;

            }
            if (dr.Row["QTY_2021"].ToString() != "")
            {
                num_QTY_2021 += Convert.ToDecimal(dr.Row["QTY_2021"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2021 = 0;

            }
            if (dr.Row["QTY_2022"].ToString() != "")
            {
                num_QTY_2022 += Convert.ToDecimal(dr.Row["QTY_2022"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2022 = 0;

            }
            if (dr.Row["QTY_2023"].ToString() != "")
            {
                num_QTY_2023 += Convert.ToDecimal(dr.Row["QTY_2023"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2023 = 0;

            }
            if (dr.Row["QTY_2024"].ToString() != "")
            {
                num_QTY_2024 += Convert.ToDecimal(dr.Row["QTY_2024"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2024 = 0;

            }
            if (dr.Row["QTY_2025"].ToString() != "")
            {
                num_QTY_2025 += Convert.ToDecimal(dr.Row["QTY_2025"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2025 = 0;

            }
            if (dr.Row["QTY_2026"].ToString() != "")
            {
                num_QTY_2026 += Convert.ToDecimal(dr.Row["QTY_2026"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2026 = 0;

            }
            if (dr.Row["QTY_2027"].ToString() != "")
            {
                num_QTY_2027 += Convert.ToDecimal(dr.Row["QTY_2027"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2027 = 0;

            }
            if (dr.Row["QTY_2028"].ToString() != "")
            {
                num_QTY_2028 += Convert.ToDecimal(dr.Row["QTY_2028"].ToString().Replace(",", ""));
            }
            else
            {
                num_QTY_2028 = 0;

            }
            if (dr.Row["A_2012"].ToString() != "")
            {
                num_A_2012 += Convert.ToDecimal(dr.Row["A_2012"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2012 = 0;

            }
            if (dr.Row["A_2013"].ToString() != "")
            {
                num_A_2013 += Convert.ToDecimal(dr.Row["A_2013"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2013 = 0;

            }
            if (dr.Row["A_2014"].ToString() != "")
            {
                num_A_2014 += Convert.ToDecimal(dr.Row["A_2014"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2014 = 0;

            }
            if (dr.Row["A_2015"].ToString() != "")
            {
                num_A_2015 += Convert.ToDecimal(dr.Row["A_2015"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2015 = 0;

            }
            if (dr.Row["A_2016"].ToString() != "")
            {
                num_A_2016 += Convert.ToDecimal(dr.Row["A_2016"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2016 = 0;

            }
            if (dr.Row["A_2017"].ToString() != "")
            {
                num_A_2017 += Convert.ToDecimal(dr.Row["A_2017"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2017 = 0;

            }
            if (dr.Row["A_2018"].ToString() != "")
            {
                num_A_2018 += Convert.ToDecimal(dr.Row["A_2018"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2018 = 0;

            }
            if (dr.Row["A_2019"].ToString() != "")
            {
                num_A_2019 += Convert.ToDecimal(dr.Row["A_2019"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2019 = 0;

            }
            if (dr.Row["A_2020"].ToString() != "")
            {
                num_A_2020 += Convert.ToDecimal(dr.Row["A_2020"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2020 = 0;

            }
            if (dr.Row["A_2021"].ToString() != "")
            {
                num_A_2021 += Convert.ToDecimal(dr.Row["A_2021"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2021 = 0;

            }
            if (dr.Row["A_2022"].ToString() != "")
            {
                num_A_2022 += Convert.ToDecimal(dr.Row["A_2022"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2022 = 0;

            }
            if (dr.Row["A_2023"].ToString() != "")
            {
                num_A_2023 += Convert.ToDecimal(dr.Row["A_2023"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2023 = 0;

            }
            if (dr.Row["A_2024"].ToString() != "")
            {
                num_A_2024 += Convert.ToDecimal(dr.Row["A_2024"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2024 = 0;

            }
            if (dr.Row["A_2025"].ToString() != "")
            {
                num_A_2025 += Convert.ToDecimal(dr.Row["A_2025"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2025 = 0;

            }
            if (dr.Row["A_2026"].ToString() != "")
            {
                num_A_2026 += Convert.ToDecimal(dr.Row["A_2026"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2026 = 0;

            }
            if (dr.Row["A_2027"].ToString() != "")
            {
                num_A_2027 += Convert.ToDecimal(dr.Row["A_2027"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2027 = 0;

            }
            if (dr.Row["A_2028"].ToString() != "")
            {
                num_A_2028 += Convert.ToDecimal(dr.Row["A_2028"].ToString().Replace(",", ""));
            }
            else
            {
                num_A_2028 = 0;

            }


            
            #endregion

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox Lab_quantity_year_hj = e.Row.FindControl("Lab_quantity_year_hj") as TextBox;
            DataTable dt_maxqty = Product_CLASS.form3_SUM_MAX(txt_pgino.Value, "QTY");
            if (dt_maxqty.Rows.Count > 0)
            {
                Lab_quantity_year_hj.Visible = true;
                Lab_quantity_year_hj.Text = String.Format("{0:N0}", dt_maxqty.Rows[0]["QTY_year"]);
           
               
            }
            else
            {
                Lab_quantity_year_hj.Visible = false;
            }

            TextBox Lab_quantity_year = e.Row.FindControl("Lab_quantity_year") as TextBox;

            if (Lab_quantity_year != null)
            {
                if (num_quantity_year == 0)
                {
                    Lab_quantity_year.Visible = false;
                }
                else
                {
                    //Lab_quantity_year.Text += num_quantity_year.ToString();//"计算的总数
                    Lab_quantity_year.Text += String.Format("{0:N0}", num_quantity_year);
                    Lab_quantity_year.Visible = false;

                }
            }
            TextBox Lab_price_year = e.Row.FindControl("Lab_price_year") as TextBox;
            TextBox Lab_price_year_hj = e.Row.FindControl("Lab_price_year_hj") as TextBox;
            DataTable dt_maxprice = Product_CLASS.form3_SUM_MAX(txt_pgino.Value, "price");
            if (dt_maxprice.Rows.Count > 0)
            {
                Lab_price_year_hj.Visible = true;
                Lab_price_year_hj.Text = String.Format("{0:N0}", dt_maxprice.Rows[0]["price_year"]);

            }
            else
            {
                Lab_price_year_hj.Visible = false;
            }

            if (Lab_price_year != null)
            {
                if (num_price_year == 0)
                {
                    Lab_price_year.Visible = false;
                }
                else
                {
                    //Lab_price_year.Text += num_price_year.ToString();//"计算的总数
                    Lab_price_year.Text += String.Format("{0:N0}", num_price_year);
                    Lab_price_year.Visible = false;
                }
            }
            TextBox Lab_psw_quantity = e.Row.FindControl("Lab_psw_quantity") as TextBox;
            if (Lab_psw_quantity != null)
            {
                if (num_psw_quantity == 0)
                {
                    Lab_psw_quantity.Visible = false;
                }
                else
                {
                    //Lab_price_year.Text += num_price_year.ToString();//"计算的总数
                    Lab_psw_quantity.Text += String.Format("{0:N0}", num_psw_quantity);
                }
            }
            //年用量
            #region "赋值给Footer"
            TextBox Lab_QTY_2012 = e.Row.FindControl("Lab_QTY_2012") as TextBox;
            if (Lab_QTY_2012 != null)
            {
                Lab_QTY_2012.Text += String.Format("{0:N0}", num_QTY_2012);
            }
            TextBox Lab_QTY_A_2012 = e.Row.FindControl("Lab_QTY_A_2012") as TextBox;
            if (Lab_QTY_A_2012 != null)
            {
                Lab_QTY_A_2012.Text += String.Format("{0:N0}", num_A_2012);
            }
            TextBox Lab_QTY_A_2013 = e.Row.FindControl("Lab_QTY_A_2013") as TextBox;
            if (Lab_QTY_A_2013 != null)
            {
                Lab_QTY_A_2013.Text += String.Format("{0:N0}", num_A_2013);
            }
            TextBox Lab_QTY_2013 = e.Row.FindControl("Lab_QTY_2013") as TextBox;
            if (Lab_QTY_2013 != null)
            {
                Lab_QTY_2013.Text += String.Format("{0:N0}", num_QTY_2013);
            }
            TextBox Lab_QTY_A_2014 = e.Row.FindControl("Lab_QTY_A_2014") as TextBox;
            if (Lab_QTY_A_2014 != null)
            {
                Lab_QTY_A_2014.Text += String.Format("{0:N0}", num_A_2014);
            }
            TextBox Lab_QTY_2014 = e.Row.FindControl("Lab_QTY_2014") as TextBox;
            if (Lab_QTY_2014 != null)
            {
                Lab_QTY_2014.Text += String.Format("{0:N0}", num_QTY_2014);
            }
            TextBox Lab_QTY_A_2015 = e.Row.FindControl("Lab_QTY_A_2015") as TextBox;
            if (Lab_QTY_A_2015 != null)
            {
                Lab_QTY_A_2015.Text += String.Format("{0:N0}", num_A_2015);
            }
            TextBox Lab_QTY_2015 = e.Row.FindControl("Lab_QTY_2015") as TextBox;
            if (Lab_QTY_2015 != null)
            {
                Lab_QTY_2015.Text += String.Format("{0:N0}", num_QTY_2015);
            }
            TextBox Lab_QTY_A_2016 = e.Row.FindControl("Lab_QTY_A_2016") as TextBox;
            if (Lab_QTY_A_2016 != null)
            {
                Lab_QTY_A_2016.Text += String.Format("{0:N0}", num_A_2016);
            }
            TextBox Lab_QTY_2016 = e.Row.FindControl("Lab_QTY_2016") as TextBox;
            if (Lab_QTY_2016 != null)
            {
                Lab_QTY_2016.Text += String.Format("{0:N0}", num_QTY_2016);
            }
            TextBox Lab_QTY_A_2017 = e.Row.FindControl("Lab_QTY_A_2017") as TextBox;
            if (Lab_QTY_A_2017 != null)
            {
                Lab_QTY_A_2017.Text += String.Format("{0:N0}", num_A_2017);
            }
            TextBox Lab_QTY_2017 = e.Row.FindControl("Lab_QTY_2017") as TextBox;
            if (Lab_QTY_2017 != null)
            {
                Lab_QTY_2017.Text += String.Format("{0:N0}", num_QTY_2017);
            }
            TextBox Lab_QTY_2018 = e.Row.FindControl("Lab_QTY_2018") as TextBox;
            if (Lab_QTY_2018 != null)
            {
                Lab_QTY_2018.Text += String.Format("{0:N0}", num_QTY_2018);
            }
            TextBox Lab_QTY_A_2018 = e.Row.FindControl("Lab_QTY_A_2018") as TextBox;
            if (Lab_QTY_A_2018 != null)
            {
                Lab_QTY_A_2018.Text += String.Format("{0:N0}", num_A_2018);
            }
            TextBox Lab_QTY_2019 = e.Row.FindControl("Lab_QTY_2019") as TextBox;
            if (Lab_QTY_2019 != null)
            {
                Lab_QTY_2019.Text += String.Format("{0:N0}", num_QTY_2019);
            }
            TextBox Lab_QTY_A_2019 = e.Row.FindControl("Lab_QTY_A_2019") as TextBox;
            if (Lab_QTY_A_2019 != null)
            {
                Lab_QTY_A_2019.Text += String.Format("{0:N0}", num_A_2019);
            }
            TextBox Lab_QTY_A_2020 = e.Row.FindControl("Lab_QTY_A_2020") as TextBox;
            if (Lab_QTY_A_2020 != null)
            {
                Lab_QTY_A_2020.Text += String.Format("{0:N0}", num_A_2020);
            }
            TextBox Lab_QTY_2020 = e.Row.FindControl("Lab_QTY_2020") as TextBox;
            if (Lab_QTY_2020 != null)
            {
                Lab_QTY_2020.Text += String.Format("{0:N0}", num_QTY_2020);
            }
            TextBox Lab_QTY_A_2021 = e.Row.FindControl("Lab_QTY_A_2021") as TextBox;
            if (Lab_QTY_A_2021 != null)
            {
                Lab_QTY_A_2021.Text += String.Format("{0:N0}", num_A_2021);
            }
            TextBox Lab_QTY_2021 = e.Row.FindControl("Lab_QTY_2021") as TextBox;
            if (Lab_QTY_2021 != null)
            {
                Lab_QTY_2021.Text += String.Format("{0:N0}", num_QTY_2021);
            }
            TextBox Lab_QTY_A_2022 = e.Row.FindControl("Lab_QTY_A_2022") as TextBox;
            if (Lab_QTY_A_2022 != null)
            {
                Lab_QTY_A_2022.Text += String.Format("{0:N0}", num_A_2022);
            }
            TextBox Lab_QTY_2022 = e.Row.FindControl("Lab_QTY_2022") as TextBox;
            if (Lab_QTY_2022 != null)
            {
                Lab_QTY_2022.Text += String.Format("{0:N0}", num_QTY_2022);

            }
            TextBox Lab_QTY_A_2023 = e.Row.FindControl("Lab_QTY_A_2023") as TextBox;
            if (Lab_QTY_A_2023 != null)
            {
                Lab_QTY_A_2023.Text += String.Format("{0:N0}", num_A_2023);
            }
            TextBox Lab_QTY_2023 = e.Row.FindControl("Lab_QTY_2023") as TextBox;
            if (Lab_QTY_2023 != null)
            {
                Lab_QTY_2023.Text += String.Format("{0:N0}", num_QTY_2023);
            }
            TextBox Lab_QTY_A_2024 = e.Row.FindControl("Lab_QTY_A_2024") as TextBox;
            if (Lab_QTY_A_2024 != null)
            {
                Lab_QTY_A_2024.Text += String.Format("{0:N0}", num_A_2024);
            }
            TextBox Lab_QTY_2024 = e.Row.FindControl("Lab_QTY_2024") as TextBox;
            if (Lab_QTY_2024 != null)
            {
                Lab_QTY_2024.Text += String.Format("{0:N0}", num_QTY_2024);
            }
            TextBox Lab_QTY_A_2025 = e.Row.FindControl("Lab_QTY_A_2025") as TextBox;
            if (Lab_QTY_A_2025 != null)
            {
                Lab_QTY_A_2025.Text += String.Format("{0:N0}", num_A_2025);
            }
            TextBox Lab_QTY_2025 = e.Row.FindControl("Lab_QTY_2025") as TextBox;
            if (Lab_QTY_2025 != null)
            {
                Lab_QTY_2025.Text += String.Format("{0:N0}", num_QTY_2025);
            }
            TextBox Lab_QTY_A_2026 = e.Row.FindControl("Lab_QTY_A_2026") as TextBox;
            if (Lab_QTY_A_2026 != null)
            {
                Lab_QTY_A_2026.Text += String.Format("{0:N0}", num_A_2026);
            }
            TextBox Lab_QTY_2026 = e.Row.FindControl("Lab_QTY_2026") as TextBox;
            if (Lab_QTY_2026 != null)
            {
                Lab_QTY_2026.Text += String.Format("{0:N0}", num_QTY_2026);
            }
            TextBox Lab_QTY_A_2027 = e.Row.FindControl("Lab_QTY_A_2027") as TextBox;
            if (Lab_QTY_A_2027 != null)
            {
                Lab_QTY_A_2027.Text += String.Format("{0:N0}", num_A_2027);
            }
            TextBox Lab_QTY_2027 = e.Row.FindControl("Lab_QTY_2027") as TextBox;
            if (Lab_QTY_2027 != null)
            {
                Lab_QTY_2027.Text += String.Format("{0:N0}", num_QTY_2027);
            }

            TextBox Lab_QTY_A_2028 = e.Row.FindControl("Lab_QTY_A_2028") as TextBox;
            if (Lab_QTY_A_2028 != null)
            {
                Lab_QTY_A_2028.Text += String.Format("{0:N0}", num_A_2028);
            }
            TextBox Lab_QTY_2028 = e.Row.FindControl("Lab_QTY_2028") as TextBox;
            if (Lab_QTY_2028 != null)
            {
                Lab_QTY_2028.Text += String.Format("{0:N0}", num_QTY_2028);
            }
            TextBox Lab_QTY_RF_2012 = e.Row.FindControl("Lab_QTY_RF_2012") as TextBox;
            if (Lab_QTY_RF_2012 != null)
            {
                Lab_QTY_RF_2012.Text += String.Format("{0:N0}", num_RF_2012);
            }
            TextBox Lab_QTY_RF_2013 = e.Row.FindControl("Lab_QTY_RF_2013") as TextBox;
            if (Lab_QTY_RF_2013 != null)
            {
                Lab_QTY_RF_2013.Text += String.Format("{0:N0}", num_RF_2013);
            }

            TextBox Lab_QTY_RF_2014 = e.Row.FindControl("Lab_QTY_RF_2014") as TextBox;
            if (Lab_QTY_RF_2014 != null)
            {
                Lab_QTY_RF_2014.Text += String.Format("{0:N0}", num_RF_2014);
            }

            TextBox Lab_QTY_RF_2015 = e.Row.FindControl("Lab_QTY_RF_2015") as TextBox;
            if (Lab_QTY_RF_2015 != null)
            {
                Lab_QTY_RF_2015.Text += String.Format("{0:N0}", num_RF_2015);
            }

            TextBox Lab_QTY_RF_2016 = e.Row.FindControl("Lab_QTY_RF_2016") as TextBox;
            if (Lab_QTY_RF_2016 != null)
            {
                Lab_QTY_RF_2016.Text += String.Format("{0:N0}", num_RF_2016);
            }

            TextBox Lab_QTY_RF_2017 = e.Row.FindControl("Lab_QTY_RF_2017") as TextBox;
            if (Lab_QTY_RF_2017 != null)
            {
                Lab_QTY_RF_2017.Text += String.Format("{0:N0}", num_RF_2017);
            }
            TextBox Lab_QTY_RF_2018 = e.Row.FindControl("Lab_QTY_RF_2018") as TextBox;
            if (Lab_QTY_RF_2018 != null)
            {
                Lab_QTY_RF_2018.Text += String.Format("{0:N0}", num_RF_2018);

            }
            TextBox Lab_QTY_RF_2019 = e.Row.FindControl("Lab_QTY_RF_2019") as TextBox;
            if (Lab_QTY_RF_2019 != null)
            {              
                Lab_QTY_RF_2019.Text += String.Format("{0:N0}", num_RF_2019);
            }
            TextBox Lab_QTY_RF_2020 = e.Row.FindControl("Lab_QTY_RF_2020") as TextBox;
            if (Lab_QTY_RF_2020 != null)
            {
                Lab_QTY_RF_2020.Text += String.Format("{0:N0}", num_RF_2020);
            }

            TextBox Lab_QTY_RF_2021 = e.Row.FindControl("Lab_QTY_RF_2021") as TextBox;
            if (Lab_QTY_RF_2021 != null)
            {
                Lab_QTY_RF_2021.Text += String.Format("{0:N0}", num_RF_2021);
            }

            TextBox Lab_QTY_RF_2022 = e.Row.FindControl("Lab_QTY_RF_2022") as TextBox;
            if (Lab_QTY_RF_2022 != null)
            {
                Lab_QTY_RF_2022.Text += String.Format("{0:N0}", num_RF_2022);
            }

            TextBox Lab_QTY_RF_2023 = e.Row.FindControl("Lab_QTY_RF_2023") as TextBox;
            if (Lab_QTY_RF_2023 != null)
            {
                Lab_QTY_RF_2023.Text += String.Format("{0:N0}", num_RF_2023);
            }
            TextBox Lab_QTY_RF_2024 = e.Row.FindControl("Lab_QTY_RF_2024") as TextBox;
            if (Lab_QTY_RF_2024 != null)
            {
                Lab_QTY_RF_2024.Text += String.Format("{0:N0}", num_RF_2024);
            }

            TextBox Lab_QTY_RF_2025 = e.Row.FindControl("Lab_QTY_RF_2025") as TextBox;
            if (Lab_QTY_RF_2025 != null)
            {
                Lab_QTY_RF_2025.Text += String.Format("{0:N0}", num_RF_2025);
            }

            TextBox Lab_QTY_RF_2026 = e.Row.FindControl("Lab_QTY_RF_2026") as TextBox;
            if (Lab_QTY_RF_2026 != null)
            {
                Lab_QTY_RF_2026.Text += String.Format("{0:N0}", num_RF_2026);
            }

            TextBox Lab_QTY_RF_2027 = e.Row.FindControl("Lab_QTY_RF_2027") as TextBox;
            if (Lab_QTY_RF_2027 != null)
            {
                Lab_QTY_RF_2027.Text += String.Format("{0:N0}", num_RF_2027);
            }
            TextBox Lab_QTY_RF_2028 = e.Row.FindControl("Lab_QTY_RF_2028") as TextBox;
            if (Lab_QTY_RF_2028 != null)
            {
                Lab_QTY_RF_2028.Text += String.Format("{0:N0}", num_RF_2028);
            }
            #endregion
            //=========================
            string requestid = Request["requestid"];
            DataRowView datarow = e.Row.DataItem as DataRowView;
            GridViewRow headrow = gv_Product_Quantity.HeaderRow;


            for (int i = 14; i < e.Row.Cells.Count - 1; i++)
            {
                if (headrow.Cells[i].Controls.Count > 1)
                {
                    string headValue = ((Label)headrow.Cells[i].Controls[1]).Text;//年份
                    if (IsNumberic(headValue))
                    {
                        TextBox txtF_A = (TextBox)e.Row.Cells[i].FindControl("Lab_QTY_A_" + headValue);
                      //  TextBox txtF_RF = (TextBox)e.Row.Cells[i].FindControl("Lab_QTY_RF_" + headValue);
                        if (Convert.ToInt16(headValue) == Curryear)
                        {
                            //TextBox txtRF = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_RF_" + headValue));
                           // txtRF.Style.Add(txtRF.Text == "0" ? "visibility" : "display", txtRF.Text == "0" ? "hidden" : "");

                            TextBox txt = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_" + headValue));
                            txt.Style.Add(txt.Text == "0" ? "visibility" : "display", txt.Text == "0" ? "hidden" : "");

                            TextBox txtA = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_A_" + headValue));
                            txtA.Style.Add(txtA.Text == "0" ? "visibility" : "display", txtA.Text == "0" ? "hidden" : "");

                        }
                        else if (Convert.ToInt16(headValue) > Curryear)
                        {
                            ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_A_" + headValue)).Style.Add("display", "none");

                           // TextBox txtRF = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_RF_" + headValue));
                            //txtRF.Style.Add(txtRF.Text == "0" ? "visibility" : "display", txtRF.Text == "0" ? "hidden" : "");

                            TextBox txt = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_" + headValue));
                            txt.Style.Add(txt.Text == "0" ? "visibility" : "display", txt.Text == "0" ? "hidden" : "");
                        }
                        else if (Convert.ToInt16(headValue) < Curryear)
                        {
                            //((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_RF_" + headValue)).Style.Add("display", "none");
                            ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_" + headValue)).Style.Add("display", "none");
                            TextBox txtA = ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_A_" + headValue));
                            txtA.Style.Add(txtA.Text == "0" ? "visibility" : "display", txtA.Text == "0" ? "hidden" : "");

                        }
                    }
                    if (Request["requestid"] == null)
                    {
                        ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_A_" + headValue)).Style.Add("visibility", "hidden");
                       // ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_RF_" + headValue)).Style.Add("visibility", "hidden");
                        ((TextBox)e.Row.Cells[i].FindControl("Lab_QTY_" + headValue)).Style.Add("visibility", "hidden");
                    }
                }



            }

        }

    }

    protected void pc_dj_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_Product_Quantity.Rows.Count; i++)
        {
         
            if (((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Text != "" && ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("quantity_year")).Text != "")
            {
               
                ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj_QAD")).Text = ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj")).Text;
                decimal dj = Convert.ToDecimal(((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("pc_dj_QAD")).Text.Trim());
                int sl = Convert.ToInt32(((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("quantity_year")).Text.Replace(",", ""));
                ((TextBox)this.gv_Product_Quantity.Rows[i].FindControl("price_year")).Text = Convert.ToInt32(dj * sl).ToString();

            }
        }
        ViewState["lv"] = "cpyc";
    }

    protected void QTY_2012_TextChanged(object sender, EventArgs e)
    {
      
       
        decimal lnmaxnum = 0;
        string lnnum2012 = "";
        string lnnum2013 = "";
        string lnnum2014 = "";
        string lnnum2015 = "";
        string lnnum2016 = "";
        string lnnum2017 = "";
        string lnnum2018 = "";
        string lnnum2019 = "";
        string lnnum2020 = "";
        string lnnum2021 = "";
        string lnnum2022 = "";
        string lnnum2023 = "";
        string lnnum2024 = "";
        string lnnum2025 = "";
        string lnnum2026 = "";
        string lnnum2027 = "";
        string lnnum2028 = "";

        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;

        

        lnnum2012 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2012")).Text;
        if (lnnum2012 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2012")).Text = decimal.Parse(lnnum2012.Replace(",", "")).ToString("#,0");
        }
        lnnum2012 = (lnnum2012 == "" ? "0" : lnnum2012);
        if (lnnum2012 != "0")
        {
            lnmaxnum = Convert.ToDecimal(lnnum2012);
        }

        lnnum2013 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2013")).Text;
        if (lnnum2013 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2013")).Text = decimal.Parse(lnnum2013.Replace(",", "")).ToString("#,0");
        }
        lnnum2013 = (lnnum2013 == "" ? "0" : lnnum2013);
        if (Convert.ToDecimal(lnnum2013)> lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2013);
        }

        lnnum2014 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2014")).Text;
        if (lnnum2014 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2014")).Text = decimal.Parse(lnnum2014.Replace(",", "")).ToString("#,0");
        }
        lnnum2014 = (lnnum2014 == "" ? "0" : lnnum2014);
        if (Convert.ToDecimal(lnnum2014) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2014);
        }
        lnnum2015 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2015")).Text;
        if (lnnum2015 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2015")).Text = decimal.Parse(lnnum2015.Replace(",", "")).ToString("#,0");
        }
        lnnum2015 = (lnnum2015 == "" ? "0" : lnnum2015);
        if (Convert.ToDecimal(lnnum2015) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2015);
        }
        lnnum2016 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2016")).Text;
        if (lnnum2016 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2016")).Text = decimal.Parse(lnnum2016.Replace(",", "")).ToString("#,0");
        }
        lnnum2016 = (lnnum2016 == "" ? "0" : lnnum2016);
        if (Convert.ToDecimal(lnnum2016) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2016);
        }
        lnnum2017 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2017")).Text;
        if (lnnum2017 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2017")).Text = decimal.Parse(lnnum2017.Replace(",", "")).ToString("#,0");
        }
        lnnum2017 = (lnnum2017 == "" ? "0" : lnnum2017);
        if (Convert.ToDecimal(lnnum2017) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2017);
        }

        lnnum2018 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2018")).Text;
        if (lnnum2018 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2018")).Text = decimal.Parse(lnnum2018.Replace(",", "")).ToString("#,0");
        }
        lnnum2018 = (lnnum2018 == "" ? "0" : lnnum2018);
        if (Convert.ToDecimal(lnnum2018) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2018);
        }

        lnnum2019 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2019")).Text;
        if (lnnum2019 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2019")).Text = decimal.Parse(lnnum2019.Replace(",", "")).ToString("#,0");
        }
        lnnum2019 = (lnnum2019 == "" ? "0" : lnnum2019);
        if (Convert.ToDecimal(lnnum2019) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2019);
        }

        lnnum2020 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2020")).Text;
        if (lnnum2020 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2020")).Text = decimal.Parse(lnnum2020.Replace(",", "")).ToString("#,0");
        }
        lnnum2020 = (lnnum2020 == "" ? "0" : lnnum2020);
        if (Convert.ToDecimal(lnnum2020) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2020);
        }

        lnnum2021 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2021")).Text;
        if (lnnum2021 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2021")).Text = decimal.Parse(lnnum2021.Replace(",", "")).ToString("#,0");
        }
        lnnum2021 = (lnnum2021 == "" ? "0" : lnnum2021);
        if (Convert.ToDecimal(lnnum2021) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2021);
        }

        lnnum2022 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2022")).Text;
        if (lnnum2022 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2022")).Text = decimal.Parse(lnnum2022.Replace(",", "")).ToString("#,0");
        }
        lnnum2022 = (lnnum2022 == "" ? "0" : lnnum2022);
        if (Convert.ToDecimal(lnnum2022) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2022);
        }

        lnnum2023 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2023")).Text;
        if (lnnum2023 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2023")).Text = decimal.Parse(lnnum2023.Replace(",", "")).ToString("#,0");
        }
        lnnum2023 = (lnnum2023 == "" ? "0" : lnnum2023);
        if (Convert.ToDecimal(lnnum2023) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2023);
        }

        lnnum2024 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2024")).Text;
        if (lnnum2024 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2024")).Text = decimal.Parse(lnnum2024.Replace(",", "")).ToString("#,0");
        }
        lnnum2024 = (lnnum2024 == "" ? "0" : lnnum2024);
        if (Convert.ToDecimal(lnnum2024) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2024);
        }

        lnnum2025 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2025")).Text;
        if (lnnum2025 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2025")).Text = decimal.Parse(lnnum2025.Replace(",", "")).ToString("#,0");
        }
        lnnum2025 = (lnnum2025 == "" ? "0" : lnnum2025);
        if (Convert.ToDecimal(lnnum2025) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2025);
        }

        lnnum2026 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2026")).Text;
        if (lnnum2026 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2026")).Text = decimal.Parse(lnnum2026.Replace(",", "")).ToString("#,0");
        }
        lnnum2026 = (lnnum2026 == "" ? "0" : lnnum2026);
        if (Convert.ToDecimal(lnnum2026) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2026);
        }

        lnnum2027 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2027")).Text;
        if (lnnum2027 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2027")).Text = decimal.Parse(lnnum2027.Replace(",", "")).ToString("#,0");
        }
        lnnum2027 = (lnnum2027 == "" ? "0" : lnnum2027);
        if (Convert.ToDecimal(lnnum2027) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2027);
        }

        lnnum2028 = ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2028")).Text;
        if (lnnum2028 != "")
        {
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("QTY_2028")).Text = decimal.Parse(lnnum2028.Replace(",", "")).ToString("#,0");
        }
        lnnum2028 = (lnnum2028 == "" ? "0" : lnnum2028);
        if (Convert.ToDecimal(lnnum2028) > lnmaxnum)
        {
            lnmaxnum = Convert.ToDecimal(lnnum2028);
        }
        //lnmaxnum=Convert.ToDecimal( decimal.Parse(Convert.ToString(lnmaxnum).Replace(",", "")).ToString("#,0"));
        ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("quantity_year")).Text = decimal.Parse(Convert.ToString(lnmaxnum).Replace(",", "")).ToString("#,0");

        decimal dj = Convert.ToDecimal(((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("pc_dj_QAD")).Text.Trim());
        int sl = Convert.ToInt32(((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("quantity_year")).Text.Replace(",", ""));
        //((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("price_year")).Text = Convert.ToInt32(dj * sl).ToString();
        ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("price_year")).Text = decimal.Parse(Convert.ToString(dj * sl).Replace(",", "")).ToString("#,0");



        ViewState["lv"] = "cpyc";
    }

    protected void khdm_TextChanged(object sender, EventArgs e)
    {
      
        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        string khdm1=((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("khdm")).Text;
        DataTable dt = Product_CLASS.Form3_Product_qad_Debtor(khdm1);
        if(dt.Rows.Count>0)
        {
            if (((TextBox)gv_Product_Quantity.Rows[rowIndex].FindControl("khdm")).Text != "")
            {
                string khdm = ((TextBox)gv_Product_Quantity.Rows[rowIndex].FindControl("khdm")).Text;
                //SHIP_to
                ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).DataSource = Product_CLASS.Form3_Product_ship_to(khdm);
                ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).DataTextField = "DebtorShipToName";
                ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).DataValueField = "DebtorShipToName";
                ((DropDownList)this.gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).DataBind();


                    ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).Items.Insert(0, "");
               
            }

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('你输入的客户代码qad中不存在，请重新输入！')", true);
            ((TextBox)this.gv_Product_Quantity.Rows[rowIndex].FindControl("khdm")).Text = "";
            ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).Items.Clear();
            ((DropDownList)gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).Items.Insert(0, "");
            ((DropDownList)this.gv_Product_Quantity.Rows[rowIndex].FindControl("ddl_Ship_to")).Text = "";
        }
        ViewState["lv"] = "cpyc";
    }

    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM [MES].[dbo].[form3_Sale_Product_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }
    public void bindrz2_log(string requestid, GridView gv_rz2)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM [MES].[dbo].[form3_Sale_Product_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz2.DataSource = dt;
        gv_rz2.DataBind();
        gv_rz2.PageSize = 100;
    }





    protected void ddl_Sales_SelectedIndexChanged(object sender, EventArgs e)
    {
        Sales_engineers_all();
        ViewState["lv"] = "cpyc";
    }

    public void Sales_engineers_all()
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_Product_Quantity.Rows.Count; i++)
        {
            DropDownList ddl = (DropDownList)this.gv_Product_Quantity.Rows[i].FindControl("ddl_Sales");
            if (ddl.Text != "")
            {
                if (lsvalue.Contains(ddl.Text) == false)
                {
                    lsvalue += lsvalue == "" ? "" : "/";
                    lsvalue += ddl.Text;
                }

            }
        }
        this.txt_Sales_engineers_all.Value = lsvalue;
    }

    public static bool IsNumberic(string _string)
    {
        if (string.IsNullOrEmpty(_string))
            return false;
        int i = 0;
        return int.TryParse(_string, out i);
    }


    protected void txt_productcode_TextChanged(object sender, EventArgs e)
    {

        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        string ljh = ((TextBox)this.gv_Product_version.Rows[rowIndex].FindControl("txt_productcode")).Text;
        DataTable dt = Product_CLASS.DDL_base("Product_DetailTable", ljh, txt_update_user.Value);
        if (dt.Rows.Count > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('你输入的零件号已经存在，请重新输入！')", true);
            ((TextBox)this.gv_Product_version.Rows[rowIndex].FindControl("txt_productcode")).Text = "";

        }
        else
        {
          
        }
  
    }




 
}


