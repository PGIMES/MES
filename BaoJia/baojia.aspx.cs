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

public partial class baojia : System.Web.UI.Page
{
    BJ_CLASS BJ_CLASS = new BJ_CLASS();
    public static string ctl = "ctl00$MainContent$";
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        this.gv_rz1.PageSize = 200;
        this.gv_rz2.PageSize = 100;
        this.Gridview_hetong_log.PageSize = 100;
        this.gv_ljzt.PageSize = 100;

        //this.Gridview_hetong_log.ShowHeader = false;
        this.gv_bjmx.PageSize = 100;
        this.gv_bjjd.PageSize = 100;
        this.gv_htgz.PageSize = 100;

        if (Session["empid"] == null || Session["job"] == null)
        {   // 给Session["empid"] & Session["job"] 初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Session["lv"] = "";

            ViewState["job"] = Session["job"];
            ViewState["empid"] = Session["empid"];
            //测试数据
            //ViewState["empid"] = "00997";
            //ViewState["job"] = "仓库班长";

            //ViewState["empid"] = "01613";
            if (Session["empid"] == null || Session["empid"] == "")
            {
                ViewState["empid"] = "01920";
                //ViewState["empid"] = "02146";
            }
            else
            {
                ViewState["empid"] = Session["empid"];

            }
            DDL();
            if (ViewState["empid"] != null)
            {
                if (Request["requestid"] != null && Request["update"] != null)//创建新一轮报价
                {
                    //this.txt_create_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //txt_baojia_no.Value = "BJ" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    Query_update();
                    txt_baojia_no.Disabled = true;
                    DDL_turns.Enabled = false;
                    txt_baojia_start_date.Disabled = true;
                    txt_create_date.Disabled = true;
      
                    DDL_domain.Enabled = false;
                    Getstatus();
                    //gvbangding();
                    gvbangding_update();
                    button();
                    //报价明细

                    //BindGridBjMX((DataTable)ViewState["dt"], this.gv_bjmx);
                    if (txt_create_by_dept.Value != "销售二部")
                    {
                        BTN_Sales_sub.Visible = false;
                    }
                    else
                    {
                        gv_bjmx_csh();
                        BTN_Sales_sub.Visible = true;
                        BTN_Sales_sub.Text = "提交";
                    }

                    DDL_is_stop.Enabled = false;
                    DDL_liuchengyizhuan.Enabled = false;
                    ViewState["lv"] = "BJJD";

                }
                else if (Request["requestid"] != null && Request["update"] == null)//当前报价页面加载
                {
                    txt_update_user.Value = ViewState["empid"].ToString();
                    DataTable dtemp = BJ_CLASS.emp(ViewState["empid"].ToString());
                    txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
                    Query();

                    GeLOG();
                    Getstatus();
                    if (DDL_is_stop.SelectedValue == "是" || DDL_baojia_status.SelectedValue == "已报出")
                    {
                        gvbangding();
                        button();
                        button_zhidu();
                        DataTable dtagent = BJ_CLASS.GetAgent(txt_sales_empid.Value, ViewState["empid"].ToString());
                        //if (ViewState["empid"].ToString() == txt_sales_empid.Value && DDL_baojia_status.SelectedValue == "已报出")
                        if ((ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0) && DDL_baojia_status.SelectedValue == "已报出")
                        {
                            gvjz();
                            DDL_liuchengyizhuan.Visible = false;
                            BTN_Sales_yizhuan.Visible = false;
                        }
                        customer_project_all();
                        update_level();
                    }
                    else
                    {
                        int requestid = Convert.ToInt32(Request["requestid"]);
                        DataTable dt = BJ_CLASS.BJ_Query_PRO(requestid);
                        string isyizhuan = dt.Rows[0]["is_yizhuan"].ToString();
                        if (isyizhuan == "1")
                        {
                            InitTable_bjmx();
                            gvbangding_update();
                            button();
                            //报价明细
                            gv_bjmx_csh();
                            ViewState["lv"] = "BJJD";
                        }
                        else
                        {
                            gvbangding();
                            //所有按钮初始化只读
                            button();
                            button_zhidu();
                            gvjz();
                        }
                        //销售工程师可以修改
                        if (ViewState["empid"].ToString() == txt_sales_empid.Value && DDL_baojia_status.SelectedValue == "报价中")
                        {
                            //取消修改20171115
                            //BTN_Sales_sub_update.Visible = true;
                            //button_write();
                        }
                        //销售工程师在分析价格之后，还未报出的情况下，不在当前节点也可以修改价格
                        DataTable dt_BTN_Sales_5 = BJ_CLASS.BJ_BTN_Sales_5(requestid);
                        int COUNT = dt_BTN_Sales_5.Rows.Count;
                        if (ViewState["empid"].ToString() == txt_sales_empid.Value && DDL_baojia_status.SelectedValue == "报价中" && COUNT > 0)
                        {

                            BTN_Sales_5.Visible = true;

                        }
                        else
                        {

                            BTN_Sales_5.Visible = false;
                        }
                        customer_project_all();
                        //修改争取级别权限
                        update_level();
                    }

                }
                else//新建报价
                {
                    //this.txt_create_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                    this.txt_baojia_start_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.txt_create_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //txt_baojia_no.Value = "BJ" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    txt_baojia_no.Disabled = true;
                    DDL_turns.Enabled = false;
                    txt_baojia_start_date.Disabled = true;
                    txt_create_date.Disabled = true;
                    txt_create_by_empid.Value = ViewState["empid"].ToString();
                    DataTable dtemp = BJ_CLASS.emp(txt_create_by_empid.Value);
                    txt_create_by_name.Value = dtemp.Rows[0]["lastname"].ToString();
                    txt_create_by_ad.Value = dtemp.Rows[0]["ADAccount"].ToString();
                    txt_create_by_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                    txt_managerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                    txt_manager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                    txt_manager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();
                    // 销售负责人
                    txt_sales_empid.Value = ViewState["empid"].ToString();
                    txt_sales_name.Value = dtemp.Rows[0]["lastname"].ToString();
                    txt_sales_ad.Value = dtemp.Rows[0]["ADAccount"].ToString();
                    txt_ZG_empid.Value = dtemp.Rows[0]["zg_workcode"].ToString();
                    txt_status_id.Text = "100";
                    txt_status_name.Text = "报价中";

                    DDL_baojia_status.SelectedValue = "报价中";
                    DDL_is_stop.SelectedValue = "否";
                    txt_update_user.Value = ViewState["empid"].ToString();
                    txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
                    Session["fileurl"] = "";
                    dtemp.Clear();
                    //gridview

                    //报价明细
                    InitTable_bjmx();
                    //BindGridBjMX((DataTable)ViewState["dt"], this.gv_bjmx);

                    BTN_Sales_sub.Text = "提交";

                    BTN_Sales_2.Enabled = false;
                    BTN_Sales_4.Enabled = false;
                    txt_hetong_complet_date_qr.Disabled = true;
                    BTN_Sales_3.Visible = false;
                    BTN_Sales_tingzhi.Visible = false;
                    BTN_Sales_yizhuan.Visible = false;
                    DDL_is_stop.Enabled = false;
                    DDL_liuchengyizhuan.Enabled = false;


                }
            }
        }

        Panel1.GroupingText = "当前轮：第" + DDL_turns.SelectedValue + "轮--报价记录";
        Panel2.GroupingText = "当前轮：第" + DDL_turns.SelectedValue + "轮--报价进度控制(点击展开)";

        //string baojia_no = Request.QueryString["Baojia_no"];
        string baojia_no = this.txt_baojia_no.Value;
        bindMst(baojia_no, this.DataMst);
        bindMst_log(baojia_no, this.DataMst_log);
    }
    public void customer_project_all()
    {
        string lsvalue = "";
        for (int i = 0; i < this.gv_htgz.Rows.Count; i++)
        {
            TextBox txt = (TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project");
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
    public void update_level()
    {
        DataTable dtagent = BJ_CLASS.GetAgent(txt_sales_empid.Value, ViewState["empid"].ToString());
        if ((ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0) & Request["requestid"] != null)
        {
            DDL_project_level.Enabled = true;
            DDL_project_level.BackColor = System.Drawing.Color.White;
            txt_BTN_Sales_project_level.Visible = true;
            Lab_Sales_project_level.Visible = true;
            BTN_Sales_project_level.Visible = true;
        }
        else
        {
            DDL_project_level.Enabled = false;
            DDL_project_level.BackColor = System.Drawing.Color.WhiteSmoke;
            txt_BTN_Sales_project_level.Visible = false;
            Lab_Sales_project_level.Visible = false;
            BTN_Sales_project_level.Visible = false;
        }
    }
    public void button()
    {
        gv_bjmx_zhidu();
        gv_bjjd_zhidu();
        gv_htgz_zhidu();
        gv_ljztgz_zhidu();
        BTN_Sales_sub.Visible = false;
        BTN_Sales_2.Enabled = false;
        BTN_Sales_4.Enabled = false;
        txt_hetong_complet_date_qr.Disabled = true;
        BTN_Sales_3.Visible = false;
        BTN_Sales_tingzhi.Visible = false;
        BTN_Sales_yizhuan.Visible = false;
    }
    public void button_zhidu()
    {
        DDL_domain.Enabled = false;
        DDL_customer_name.Enabled = false;
        DDL_end_customer_name.Enabled = false;
        DDL_domain.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_customer_name.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_end_customer_name.BackColor = System.Drawing.Color.WhiteSmoke;
        txt_customer_project.Disabled = true;
        selectwl.Disabled = true;
        DDL_project_size.Enabled = false;
        DDL_project_level.Enabled = false;
        DDL_project_size.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_project_level.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_is_stop.Enabled = false;
        DDL_liuchengyizhuan.Enabled = false;
        DDL_sfxy_bjfx.Enabled = false;
        txt_baojia_start_date.Disabled = true;
        txt_create_date.Disabled = true;
        txt_baojia_end_date.Disabled = true;
        txt_baojia_no.Disabled = true;
        DDL_turns.Enabled = false;
        DDL_wl_tk.Enabled = false;
        DDL_wl_tk.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_bz_tk.Enabled = false;
        DDL_bz_tk.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_jijia_tk.Enabled = false;
        DDL_jijia_tk.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_yz_tk.Enabled = false;
        DDL_yz_tk.BackColor = System.Drawing.Color.WhiteSmoke;
        DDL_sfxj_cg.Enabled = false;
        DDL_sfxj_cg.BackColor = System.Drawing.Color.WhiteSmoke;
        txt_content.Disabled = true;
        txt_baojia_desc.Disabled = true;
        txt_cusRequestDate.Disabled = true;
        txt_Sj_baochuDate.Disabled = true;


    }
    public void button_write()
    {
        //DDL_domain.Enabled = true;
        DDL_customer_name.Enabled = true;
        DDL_end_customer_name.Enabled = true;
        //txt_customer_project.Disabled = false;
        //selectwl.Disabled = false;
        DDL_project_size.Enabled = true;
        DDL_project_level.Enabled = true;
        //DDL_is_stop.Enabled = true;
        //DDL_liuchengyizhuan.Enabled = true;
        //DDL_sfxy_bjfx.Enabled = true;
        //txt_baojia_start_date.Disabled = false;
        //txt_create_date.Disabled = false;
        //txt_baojia_no.Disabled = false;
        //DDL_turns.Enabled = true;
        //DDL_wl_tk.Enabled = true;
        //DDL_bz_tk.Enabled = true;
        //DDL_sfxj_cg.Enabled = true;
        txt_content.Disabled = false;
    }
    public void gv_bjmx_zhidu()
    {
        //gridview报价明细只读
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Enabled = false;

            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Enabled = false;
            ((Button)this.gv_bjmx.Rows[i].FindControl("btnDel")).Visible = false;
            // gv_bjmx.ShowFooter = false;
            ((Button)this.gv_bjmx.FooterRow.FindControl("btnAdd")).Visible = false;

        }
        //提交按钮只读
        BTN_Sales_sub.Visible = false;
        BTN_Sales_3.Visible = false;

    }
    public void gv_bjjd_zhidu()
    {
        //gridview报价进度只读
        for (int i = 0; i < gv_bjjd.Rows.Count; i++)
        {
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("require_date")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("sign_date")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("Operation_time")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).Enabled = false;
            ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Visible = false;
            ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Enabled = false;
            ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).Enabled = false;
        }
    }
    public void gv_htgz_write()
    {
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {


            ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Enabled = true;
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Enabled = true;
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Enabled = true;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Enabled = true;

        }
    }
    public void gv_htgz_zhidu()
    {
        gv_htgz.Columns[1].Visible = false;//一开始隐藏
        //gridview合同跟踪只读
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Enabled = false;
            //((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Visible = false;
            //((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Enabled = false;
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Enabled = false;
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Enabled = false;
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Enabled = false;
            ((TextBox)this.gv_htgz.Rows[i].FindControl("create_by_empid")).Enabled = false;
            //gv_htgz.ShowFooter = false;
            //((Button)this.gv_htgz.Rows[i].FindControl("btnAddht")).Visible = false;
            ((Button)this.gv_htgz.FooterRow.FindControl("btnAddht")).Visible = false;
            ((Button)this.gv_htgz.Rows[i].FindControl("btnDelht")).Visible = false;
        }
        BTN_Sales_2.Enabled = false;
        BTN_Sales_4.Enabled = false;
        txt_hetong_complet_date_qr.Disabled = true;

    }
    public void ddl_htgz()
    {
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            DataTable dttest = BJ_CLASS.BJ_BASE("agreement_types");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataSource = BJ_CLASS.BJ_BASE("agreement_types");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataBind();
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
            if (((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text != "")
            {
                ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text;
            }

            DataTable dt_currency = BJ_CLASS.BJ_BASE("currency");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataSource = BJ_CLASS.BJ_BASE("currency");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).DataBind();
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
            if (((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text != "")
            {
                ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text;
            }

            DataTable dtlj_status = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataSource = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataBind();

            if (((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text != "")
            {
                ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text;
            }
                ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).Items.Insert(0, new ListItem("争取", "争取"));
            //DataTable dtqq = BJ_CLASS.BJ_ljh(Request["requestid"]);
            DataTable dtqq = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "xz_ljh");
            //dtqq.Rows.Add("");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataSource = dtqq;
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataTextField = "xz_ljh";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataValueField = "id";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).DataBind();
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Items.Insert(0, new ListItem("", ""));

            if (((TextBox)gv_htgz.Rows[i].FindControl("txtljh")).Text != "")
            {
                ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtljh")).Text;
            }

            //DataTable dtship_to = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ship_to");
            //dtqq.Rows.Add("");
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataSource = dtship_to;
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataTextField = "ship_to";
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataValueField = "ship_to";
            //((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataBind();
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Items.Insert(0, new ListItem("", ""));

            //if (((TextBox)gv_htgz.Rows[i].FindControl("txtship_to")).Text != "")
            //{
            //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtship_to")).Text;
            //}

        }

    }
    public void ddl_bjmx()
    {
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            DataTable dtljh = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "xz_ljh_dt");
            //dtqq.Rows.Add("");
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataSource = dtljh;
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataTextField = "xz_ljh";
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataValueField = "xz_ljh";
            ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataBind();
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Items.Insert(0, new ListItem("", ""));

            //if (Request["update"] != null)
            //{

            //}
            //else
            //{
            //    if (((TextBox)gv_bjmx.Rows[i].FindControl("txtljh_dt")).Text != "")
            //    {
            //        ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text = ((TextBox)gv_bjmx.Rows[i].FindControl("txtljh_dt")).Text;
            //        ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Enabled = false;
            //    }
            //}


        }

    }

    public void ddl_bjjd()
    {
        //加载进度明细
        DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
        gv_bjjd.DataSource = dt_bjjd;
        gv_bjjd.DataBind();
        for (int i = 0; i < gv_bjjd.Rows.Count; i++)
        {
            //((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text = dt.Rows[i]["flow"].ToString();
            //DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
            string dept = dt_bjjd.Rows[i]["flow"].ToString();
            DataTable aa = BJ_CLASS.BJ_dept(dept);
            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept(dept);
            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
            ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
            if (((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text != "")
            {
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = ((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text;
            }

            string js = dt_bjjd.Rows[i]["empid"].ToString();
            if (js != "")
            {
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
            }
        }
    }
    public void gvbangding_update()
    {

        //加载报价明细
        DataTable dt_bjmx = BJ_CLASS.Getgv(Request["requestid"], "bjmx_update");
        //ViewState["dt"] = dt_bjmx;
        gv_bjmx.DataSource = dt_bjmx;
        gv_bjmx.DataBind();

        ////加载进度明细
        //DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
        //gv_bjjd.DataSource = dt_bjjd;
        //gv_bjjd.DataBind();

        //加载合同明细
        //DataTable dt_htgz = BJ_CLASS.Getgv_update(Request["requestid"], "htgz");
        DataTable dt_htgz = BJ_CLASS.Getgv(txt_baojia_no.Value, "htgz");
        ViewState["dtht"] = dt_htgz;
        gv_htgz.DataSource = dt_htgz;
        gv_htgz.DataBind();

        //下拉框
        ddl_htgz();
        ddl_ljztgz();
        ddl_bjmx();

        if (txt_create_by_dept.Value != "销售二部")
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售部才可申请报价流程！')", true);
            DDL_project_size.SelectedValue = "";
            BTN_Sales_sub.Visible = false;

        }
        else
        {
            DataTable dt = BJ_CLASS.BJ_GetSPR(DDL_project_size.Text, txt_sales_empid.Value, DDL_domain.SelectedValue, DDL_sfxy_bjfx.SelectedValue, DDL_customer_name.SelectedValue, DDL_wl_tk.SelectedValue, DDL_sfxj_cg.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, txt_ZG_empid.Value);
            ViewState["detail"] = dt;
            gv_bjjd.DataSource = dt;
            gv_bjjd.DataBind();
            Panel1.Visible = true;
            for (int i = 0; i < gv_bjjd.Rows.Count; i++)
            {
                //((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text = dt.Rows[i]["flow"].ToString();
                string dept = dt.Rows[i]["flow"].ToString();
                DataTable aa = BJ_CLASS.BJ_dept(dept);
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept(dept);
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
                ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
                if (((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text != "")
                {
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = ((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text;
                }
                //((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = dt.Rows[i]["empid"].ToString();
                string js = dt.Rows[i]["step"].ToString();
                string stepid = dt.Rows[i]["stepid"].ToString();
                if (js.Substring(js.Length - 3) == "工程师" || js.Substring(js.Length - 2) == "副总" || js.Substring(js.Length - 2) == "经理" || js.Substring(js.Length - 2) == "主管")
                {
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Style.Add("BackColor", "black");
                }
                if ((js == "(指派)机加经理" && stepid == "10") || (js == "(指派)压铸经理" && stepid == "10") )
                {
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = true;
                    //((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                    ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                }
                if (stepid == "10")
                {
                    if (js == "(指派)机加经理")
                    {
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept_step(dept, "工程经理");
                    }
                    if (js == "(指派)压铸经理")
                    {
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept_step(dept, "压铸工艺经理");
                    }
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
                    ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
                    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
                }

            }
            //gridview报价进度只读
            gv_bjjd_zhidu();
        }
    }
    public void gvbangding()
    {
        //加载报价明细
        DataTable dt_bjmx = BJ_CLASS.Getgv(Request["requestid"], "bjmx");
        gv_bjmx.DataSource = dt_bjmx;
        gv_bjmx.DataBind();

        //加载进度明细
        //DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
        //gv_bjjd.DataSource = dt_bjjd;
        //gv_bjjd.DataBind();

        //加载合同明细
        //DataTable dt_htgz = BJ_CLASS.Getgv(Request["requestid"], "htgz");
        DataTable dt_htgz = BJ_CLASS.Getgv(txt_baojia_no.Value, "htgz");
        ViewState["dtht"] = dt_htgz;
        gv_htgz.DataSource = dt_htgz;
        gv_htgz.DataBind();

        //加载零件状态明细  
        DataTable dt_ljztgz = BJ_CLASS.Getgv(txt_baojia_no.Value, "ljztgz");
        gv_ljztgz.DataSource = dt_ljztgz;
        gv_ljztgz.DataBind();
        //for (int i = 0; i < gv_htgz.Rows.Count; i++)
        //{
        //    DataTable dttest = BJ_CLASS.BJ_BASE("agreement_types");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataSource = BJ_CLASS.BJ_BASE("agreement_types");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataBind();
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text;
        //    }

        //    DataTable dt_currency = BJ_CLASS.BJ_BASE("currency");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataSource = BJ_CLASS.BJ_BASE("currency");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).DataBind();
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text;
        //    }


        //    DataTable dtlj_status = BJ_CLASS.BJ_BASE("lj_status");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataSource = BJ_CLASS.BJ_BASE("lj_status");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataBind();

        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text;
        //    }

        //    //DataTable dtqq = BJ_CLASS.BJ_ljh(Request["requestid"]);
        //    DataTable dtqq = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ljh");
        //    dtqq.Rows.Add("");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataSource = dtqq;
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataTextField = "ljh";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataValueField = "ljh";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).DataBind();
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Items.Insert(0, new ListItem("", ""));

        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtljh")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtljh")).Text;
        //    }

        //    DataTable dtship_to = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ship_to");
        //    dtqq.Rows.Add("");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataSource = dtship_to;
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataTextField = "ship_to";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataValueField = "ship_to";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataBind();
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Items.Insert(0, new ListItem("", ""));

        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtship_to")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtship_to")).Text;
        //    }

        //}
        ddl_htgz();
        ddl_bjjd();
        ddl_bjmx();
        ddl_ljztgz();
        //for (int i = 0; i < gv_bjjd.Rows.Count; i++)
        //{
        //    //((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text = dt.Rows[i]["flow"].ToString();
        //    //DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
        //    string dept = dt_bjjd.Rows[i]["flow"].ToString();
        //    DataTable aa = BJ_CLASS.BJ_dept(dept);
        //    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept(dept);
        //    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
        //    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
        //    ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
        //    ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
        //    if (((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text != "")
        //    {
        //        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = ((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text;
        //    }

        //    string js = dt_bjjd.Rows[i]["empid"].ToString();
        //    if (js != "")
        //    {
        //        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
        //    }
        //}

    }
    public void baojiamingxi()
    {
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Enabled = false;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Enabled = false;
            //((Button)this.gv_bjmx.Rows[i].FindControl("btnAdd")).Visible = false;
            //((Button)this.gv_bjmx.Rows[i].FindControl("btnDel")).Visible = false;
            ((Button)this.gv_bjmx.FooterRow.FindControl("btnAdd")).Visible = false;
            ((Button)this.gv_bjmx.Rows[i].FindControl("btnDel")).Visible = false;

            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Enabled = true;
        }
    }
    public void gvjz()
    {
        //销售300时维护零件价格等信息
        DataTable dtagent = BJ_CLASS.GetAgent(txt_sales_empid.Value, ViewState["empid"].ToString());
        
        if ((ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0 )&& txt_status_id.Text == "300")
        {
            baojiamingxi();
            //BTN_Sales_3.Visible = true;

        }
        //报价中根据待签核流程判断
        for (int i = 0; i < gv_bjjd.Rows.Count; i++)
        {

            if (DDL_baojia_status.SelectedValue == "报价中")
            {
                DataTable dtbtn = BJ_CLASS.GetBTN(Request["requestid"], ViewState["empid"].ToString());
                if (dtbtn == null)
                {

                }
                else
                {
                    //根据流程中接受时间但没有处理的，获取签核权限
                    if (dtbtn.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtbtn.Rows.Count; j++)
                        {
                            if (dtbtn.Rows[j]["empid"].ToString() == ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text && dtbtn.Rows[j]["flowid"].ToString() == ((TextBox)gv_bjjd.Rows[i].FindControl("flowid")).Text && dtbtn.Rows[j]["big_flowid"].ToString() == ((TextBox)gv_bjjd.Rows[i].FindControl("big_flowid")).Text && dtbtn.Rows[j]["stepid"].ToString() == ((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text)
                            {
                                ((Label)this.gv_bjjd.Rows[i].FindControl("Lab_yz")).Visible = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Visible = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Enabled = true;
                                ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).Enabled = true;
                                ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).BackColor = System.Drawing.Color.Yellow;
                                //gv_bjjd.Rows[i].BackColor = System.Drawing.Color.Yellow;
                                //baojiamingxi();

                            }
                            if (dtbtn.Rows[j]["stepid"].ToString() == "10" && dtbtn.Rows[j]["big_flowid"].ToString() == "200")
                            {
                                if (((TextBox)gv_bjjd.Rows[i].FindControl("flowid")).Text == dtbtn.Rows[j]["flowid"].ToString() && ((TextBox)gv_bjjd.Rows[i].FindControl("big_flowid")).Text == dtbtn.Rows[j]["big_flowid"].ToString() && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) < 40 && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) > 10)
                                {
                                    ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = true;
                                    // ((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                                    ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                                }
                                else
                                {
                                    ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
                                }

                            }
                        }
                        if (dtbtn.Rows[0]["flowid"].ToString() == "50" && (((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text == "包装" || ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text == "仓储" || ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text == "运输") && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) < 40 && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) > 10)
                        {
                            if (dtbtn.Rows[0]["step"].ToString() == "(指派)物流经理")
                            {
                                ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = true;
                                // ((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                                ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                            }
                        }
                        if (dtbtn.Rows[0]["flowid"].ToString() == "40" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text == "机加" && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) < 40 && Convert.ToInt32(((TextBox)gv_bjjd.Rows[i].FindControl("stepid")).Text) > 10)
                        {
                            if (dtbtn.Rows[0]["step"].ToString() == "(指派)机加经理")
                            {
                                ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("btn_tj")).Visible = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("btn_tj")).Enabled = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("bt_delete")).Visible = true;
                                ((Button)this.gv_bjjd.Rows[i].FindControl("bt_delete")).Enabled = true;
                                ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt")).Visible = true;
                                // ((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                                ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                            }
                        }
                        ViewState["lv"] = "BJJD";

                    }
                    else
                    {
                        ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Visible = false;
                        ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).Enabled = false;
                        ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
                    }
                }
            }
        }
        //销售维护合同跟踪
        if (gv_htgz.Rows.Count <= 0)//没维护过时
        {
            //DataTable dtagent = BJ_CLASS.GetAgent(txt_sales_empid.Value, ViewState["empid"].ToString());
            if ((ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0 )&& DDL_is_stop.SelectedValue == "否" && DDL_liuchengyizhuan.SelectedValue == "")
            {
                //合同追踪
                InitTable_htgz();
                BindGridhtgz((DataTable)ViewState["dtht"], this.gv_htgz);
                //gv_htgz.Columns[1].Visible = true;//销售维护时显示
                BTN_Sales_2.Enabled = true;
                //BTN_Sales_4.Enabled = true;
                txt_hetong_complet_date_qr.Disabled = false;
                //BTN_Sales_tingzhi.Visible = true;
                //BTN_Sales_yizhuan.Visible = true;
                DDL_is_stop.Enabled = true;
                //DDL_liuchengyizhuan.Enabled = true;
                gv_htgz_write();
            }
            else
            {
                gv_htgz_zhidu();
                BTN_Sales_2.Enabled = false;
                BTN_Sales_4.Enabled = false;
                gv_htgz.Columns[1].Visible = false;//其他隐藏
                txt_hetong_complet_date_qr.Disabled = true;
            }
        }
        else//已经维护过时
        {
            if (ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0)
            {
                //gv_htgz.Columns[1].Visible = true;//销售维护时显示
                BTN_Sales_2.Enabled = true;
                //BTN_Sales_4.Enabled = true;
                txt_hetong_complet_date_qr.Disabled = false;
                //BTN_Sales_tingzhi.Visible = true;
                //BTN_Sales_yizhuan.Visible = true;
                DDL_is_stop.Enabled = true;
                //DDL_liuchengyizhuan.Enabled = true;
                for (int i = 0; i < gv_htgz.Rows.Count; i++)
                {
                    //((Button)this.gv_htgz.FooterRow.FindControl("btnAddht")).Visible = true;
                    //((Button)this.gv_htgz.Rows[i].FindControl("btnDelht")).Visible = true;
                }
                gv_htgz_write();
            }
            else
            {

                gv_htgz_zhidu();
                BTN_Sales_2.Enabled = false;
                BTN_Sales_4.Enabled = false;
                txt_hetong_complet_date_qr.Disabled = true;
                gv_htgz.Columns[1].Visible = false;//其他隐藏
            }
        }
        if (ViewState["empid"].ToString() == txt_sales_empid.Value || dtagent.Rows.Count > 0)
        {
            gv_ljztgz_write();
        }
    }

    public void Query()
    {

        int requestid = Convert.ToInt32(Request["requestid"]);
        DataTable dt = BJ_CLASS.BJ_Query_PRO(requestid);
        txt_create_by_empid.Value = dt.Rows[0]["create_by_empid"].ToString();
        txt_create_by_name.Value = dt.Rows[0]["create_by_name"].ToString();
        txt_create_by_ad.Value = dt.Rows[0]["create_by_ad"].ToString();
        txt_create_by_dept.Value = dt.Rows[0]["create_by_dept"].ToString();
        txt_managerid.Value = dt.Rows[0]["managerid"].ToString();
        txt_manager.Value = dt.Rows[0]["manager"].ToString();
        txt_manager_AD.Value = dt.Rows[0]["manager_AD"].ToString();
        txt_baojia_no.Value = dt.Rows[0]["baojia_no"].ToString();
        txt_sales_empid.Value = dt.Rows[0]["sales_empid"].ToString();
        txt_sales_name.Value = dt.Rows[0]["sales_name"].ToString();
        txt_sales_ad.Value = dt.Rows[0]["sales_ad"].ToString();
        DDL_domain.SelectedValue = dt.Rows[0]["domain"].ToString();
        DDL_turns.SelectedValue = dt.Rows[0]["turns"].ToString();
        DDL_customer_name.SelectedValue = dt.Rows[0]["customer_name"].ToString();
        DDL_end_customer_name.SelectedValue = dt.Rows[0]["end_customer_name"].ToString();
        txt_customer_project.Value = dt.Rows[0]["customer_project"].ToString();
        //selectwl.Value = dt.Rows[0]["wl_tk"].ToString();
        txt_wl_tk.Text = dt.Rows[0]["wl_tk"].ToString();
        DDL_wl_tk.SelectedValue = dt.Rows[0]["wl_tk"].ToString();
        DDL_bz_tk.SelectedValue = dt.Rows[0]["bz_tk"].ToString();
        DDL_jijia_tk.SelectedValue = dt.Rows[0]["jijia_tk"].ToString();
        DDL_yz_tk.SelectedValue = dt.Rows[0]["yz_tk"].ToString();
        DDL_sfxj_cg.SelectedValue = dt.Rows[0]["sfxj_cg"].ToString();
        //txt_wl_tk.Visible = true;
        selectwl.Visible = false;
        DDL_project_size.SelectedValue = dt.Rows[0]["project_size"].ToString();
        DDL_project_level.SelectedValue = dt.Rows[0]["project_level"].ToString();
        DDL_baojia_status.SelectedValue = dt.Rows[0]["baojia_status"].ToString();
        DDL_is_stop.SelectedValue = dt.Rows[0]["is_stop"].ToString();
        if (DDL_is_stop.SelectedValue == "是")
        {
            txt_status_id.Text = "2000";
            txt_status_name.Text = "已停止";
        }
        txt_baojia_desc.Value = dt.Rows[0]["baojia_desc"].ToString();
        txt_baojia_file_path.Text = dt.Rows[0]["baojia_file_path"].ToString();
     
        //link_baojia_file_path.Text = "file"+dt.Rows[0]["baojia_file_path"].ToString();
        //Session["fileurl"] = "file:" + dt.Rows[0]["baojia_file_path"].ToString();

        txt_baojia_file_path.ReadOnly = true;
        //txt_baojia_file_path.Visible = false;
        txt_status_id.Text = dt.Rows[0]["Big_FlowID"].ToString();
        DDL_sfxy_bjfx.SelectedValue = dt.Rows[0]["sfxy_bjfx"].ToString();
        txt_status_name.Text = "报价中";
        if (txt_status_id.Text == "1000")
        {
            txt_status_name.Text = dt.Rows[0]["baojia_status"].ToString();
        }

        if (dt.Rows[0]["baojia_start_date"].ToString() != "")
        {
            txt_baojia_start_date.Value = Convert.ToDateTime(dt.Rows[0]["baojia_start_date"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["create_date"].ToString() != "")
        {
            txt_create_date.Value = Convert.ToDateTime(dt.Rows[0]["create_date"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["baojia_end_date"].ToString() != "")
        {
            txt_baojia_end_date.Value = Convert.ToDateTime(dt.Rows[0]["baojia_end_date"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["cusRequestDate"].ToString() != "")
        {
            txt_cusRequestDate.Value = Convert.ToDateTime(dt.Rows[0]["cusRequestDate"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["Sj_baochuDate"].ToString() != "")
        {
            txt_Sj_baochuDate.Value = Convert.ToDateTime(dt.Rows[0]["Sj_baochuDate"].ToString()).ToString("yyyy-MM-dd");
        }

        if (dt.Rows[0]["dingdian_date"].ToString() != "")
        {
            txt_dingdian_date.Value = Convert.ToDateTime(dt.Rows[0]["dingdian_date"].ToString()).ToString("yyyy-MM-dd");

        }
        if (dt.Rows[0]["hetong_complet_date"].ToString() != "")
        {
            txt_hetong_complet_date.Value = Convert.ToDateTime(dt.Rows[0]["hetong_complet_date"].ToString()).ToString("yyyy-MM-dd");
            txt_hetong_complet_date_qr.Value = Convert.ToDateTime(dt.Rows[0]["hetong_complet_date"].ToString()).ToString("yyyy-MM-dd");
        }
        DataTable dt_bjjd = BJ_CLASS.Getgv(Request["requestid"], "bjjd");
        txt_content.Value = dt_bjjd.Rows[0]["sign_desc"].ToString();
        yearhj();
    }
    public void yearhj()
    {
        DataTable dt_pc_date = BJ_CLASS.Get_year_heji(txt_baojia_no.Value, "pc_date");
        if (dt_pc_date.Rows.Count > 0)
        {
            if (dt_pc_date.Rows[0]["pc_date"].ToString() != "")
            {
                txt_pc_date.Value = Convert.ToDateTime(dt_pc_date.Rows[0]["pc_date"].ToString()).ToString("yyyy-MM-dd");
            }
        }
        DataTable dt_pc_quantity_year = BJ_CLASS.Get_year_heji(txt_baojia_no.Value, "quantity_year");
        if (dt_pc_quantity_year.Rows.Count > 0)
        {
            txt_total_quantity_year.Value = dt_pc_quantity_year.Rows[0]["quantity_year"].ToString();
            int sl = Convert.ToInt32(dt_pc_quantity_year.Rows[0]["quantity_year"]);
            txt_total_quantity_year.Value = string.Format("{0:N0}", sl);
            //txt_total_quantity_year.Value = String.Format("{0:N0}",dt_pc_quantity_year.Rows[0]["quantity_year"].ToString());

            //txt_total_price_year.Value = dt_pc_year.Rows[0]["price_year"].ToString();
        }
        DataTable dt_pc_price_year = BJ_CLASS.Get_year_heji(txt_baojia_no.Value, "price_year");
        if (dt_pc_price_year.Rows.Count > 0)
        {
            //txt_total_quantity_year.Value = dt_pc_year.Rows[0]["quantity_year"].ToString();
            //txt_total_price_year.Value = dt_pc_price_year.Rows[0]["price_year"].ToString();
            int sl = Convert.ToInt32(dt_pc_price_year.Rows[0]["price_year"]);
            txt_total_price_year.Value = string.Format("{0:N0}", sl);
        }
    }
    public void Query_update()
    {

        txt_create_by_empid.Value = ViewState["empid"].ToString();
        DataTable dtemp = BJ_CLASS.emp(txt_create_by_empid.Value);
        txt_create_by_name.Value = dtemp.Rows[0]["lastname"].ToString();
        txt_create_by_ad.Value = dtemp.Rows[0]["ADAccount"].ToString();
        txt_create_by_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
        txt_managerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
        txt_manager.Value = dtemp.Rows[0]["Manager_name"].ToString();
        txt_manager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();
        txt_ZG_empid.Value = dtemp.Rows[0]["zg_workcode"].ToString();
        // 销售负责人
        txt_sales_empid.Value = ViewState["empid"].ToString();
        txt_sales_name.Value = dtemp.Rows[0]["lastname"].ToString();
        txt_sales_ad.Value = dtemp.Rows[0]["ADAccount"].ToString();
        txt_status_id.Text = "100";
        txt_status_name.Text = "报价中";
        //DDL_turns.SelectedValue = "1";
        DDL_baojia_status.SelectedValue = "报价中";
        DDL_is_stop.SelectedValue = "否";
        txt_update_user.Value = ViewState["empid"].ToString();
        txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
        dtemp.Clear();
        //gridview

        //报价明细
        InitTable_bjmx();
        //BindGridBjMX((DataTable)ViewState["dt"], this.gv_bjmx);
        BTN_Sales_sub.Text = "提交";
        BTN_Sales_2.Enabled = false;
        txt_hetong_complet_date_qr.Disabled = true;
        BTN_Sales_4.Enabled = false;
        BTN_Sales_3.Visible = false;



        int requestid = Convert.ToInt32(Request["requestid"]);
        DataTable dt = BJ_CLASS.BJ_Query_PRO(requestid);
        txt_baojia_no.Value = dt.Rows[0]["baojia_no"].ToString();
        DDL_domain.SelectedValue = dt.Rows[0]["domain"].ToString();
        int turns = Convert.ToInt32(dt.Rows[0]["turns"].ToString()) + 1;
        DDL_turns.SelectedValue = turns.ToString();
        DDL_customer_name.SelectedValue = dt.Rows[0]["customer_name"].ToString();
        DDL_end_customer_name.SelectedValue = dt.Rows[0]["end_customer_name"].ToString();
        txt_customer_project.Value = dt.Rows[0]["customer_project"].ToString();

        //txt_wl_tk.Text = dt.Rows[0]["wl_tk"].ToString();
        //txt_wl_tk.Visible = true;
        //selectwl.Visible = false;
        //DDL_project_size.SelectedValue = dt.Rows[0]["project_size"].ToString();
        DDL_project_level.SelectedValue = dt.Rows[0]["project_level"].ToString();
        txt_baojia_desc.Value = dt.Rows[0]["baojia_desc"].ToString();
        txt_cusRequestDate.Value = dt.Rows[0]["cusRequestDate"].ToString();
        txt_Sj_baochuDate.Value = dt.Rows[0]["Sj_baochuDate"].ToString();
        txt_baojia_file_path.Text = dt.Rows[0]["baojia_file_path"].ToString();
        //Session["fileurl"] = "" ;
        if (dt.Rows[0]["baojia_start_date"].ToString() != "")
        {
            txt_baojia_start_date.Value = Convert.ToDateTime(dt.Rows[0]["baojia_start_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            txt_baojia_start_date.Disabled = true;
        }
        if (dt.Rows[0]["create_date"].ToString() != "")
        {
            //txt_create_date.Value = dt.Rows[0]["create_date"].ToString();
            txt_create_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        if (dt.Rows[0]["dingdian_date"].ToString() != "")
        {
            txt_dingdian_date.Value = Convert.ToDateTime(dt.Rows[0]["dingdian_date"].ToString()).ToString("yyyy-MM-dd");

        }
        if (dt.Rows[0]["hetong_complet_date"].ToString() != "")
        {
            txt_hetong_complet_date.Value = Convert.ToDateTime(dt.Rows[0]["hetong_complet_date"].ToString()).ToString("yyyy-MM-dd");
            txt_hetong_complet_date_qr.Value = Convert.ToDateTime(dt.Rows[0]["hetong_complet_date"].ToString()).ToString("yyyy-MM-dd");
        }

        yearhj();
    }
    public void DDL()
    {
        DDL_turns.DataSource = BJ_CLASS.BJ_base("turns", "BAOJIA");
        DDL_turns.DataValueField = "ID";
        DDL_turns.DataTextField = "name";
        DDL_turns.DataBind();
        this.DDL_turns.Items.Insert(0, new ListItem("", ""));

        DDL_end_customer_name.DataSource = BJ_CLASS.BJ_base("end_customer", "BAOJIA");
        DDL_end_customer_name.DataValueField = "name";
        DDL_end_customer_name.DataTextField = "name";
        DDL_end_customer_name.DataBind();
        this.DDL_end_customer_name.Items.Insert(0, new ListItem("", ""));

        DDL_customer_name.DataSource = BJ_CLASS.BJ_base("custumer", "BAOJIA");
        DDL_customer_name.DataValueField = "name";
        DDL_customer_name.DataTextField = "name";
        DDL_customer_name.DataBind();
        this.DDL_customer_name.Items.Insert(0, new ListItem("", ""));

        DDL_project_size.DataSource = BJ_CLASS.BJ_BASE("project_size");
        DDL_project_size.DataValueField = "lookup_desc";
        DDL_project_size.DataTextField = "lookup_desc";
        DDL_project_size.DataBind();
        this.DDL_project_size.Items.Insert(0, new ListItem("", ""));

        DDL_project_level.DataSource = BJ_CLASS.BJ_BASE("project_level");
        DDL_project_level.DataValueField = "lookup_desc";
        DDL_project_level.DataTextField = "lookup_desc";
        DDL_project_level.DataBind();
        this.DDL_project_level.Items.Insert(0, new ListItem("", ""));

        DDL_is_stop.DataSource = BJ_CLASS.BJ_BASE("Is_stop");
        DDL_is_stop.DataValueField = "lookup_desc";
        DDL_is_stop.DataTextField = "lookup_desc";
        DDL_is_stop.DataBind();
        this.DDL_is_stop.Items.Insert(0, new ListItem("", ""));

        DDL_baojia_status.DataSource = BJ_CLASS.BJ_BASE("baojia_status");
        DDL_baojia_status.DataValueField = "lookup_desc";
        DDL_baojia_status.DataTextField = "lookup_desc";
        DDL_baojia_status.DataBind();
        this.DDL_baojia_status.Items.Insert(0, new ListItem("", ""));

        selectwl.DataSource = BJ_CLASS.BJ_BASE("selectwl");
        selectwl.DataValueField = "lookup_desc";
        selectwl.DataTextField = "lookup_desc";
        selectwl.DataBind();
        //this.selectwl.Items.Insert(0, new ListItem("", ""));

        DDL_wl_tk.DataSource = BJ_CLASS.BJ_BASE("selectwl");
        DDL_wl_tk.DataValueField = "lookup_desc";
        DDL_wl_tk.DataTextField = "lookup_desc";
        DDL_wl_tk.DataBind();
        this.DDL_wl_tk.Items.Insert(0, new ListItem("", ""));

        //DDL_bz_tk.DataSource = BJ_CLASS.BJ_BASE("selectbz");
        //DDL_bz_tk.DataValueField = "lookup_desc";
        //DDL_bz_tk.DataTextField = "lookup_desc";
        //DDL_bz_tk.DataBind();
        //this.DDL_bz_tk.Items.Insert(0, new ListItem("", ""));


    }

    private void GeLOG()
    {
        if (txt_status_id.Text == "")
        {
        }
        else
        {
            DataTable dt = new DataTable();

            //dt = BJ_CLASS.Getlog(Request["requestid"]);
            ////dt = YJ_CLASS.Getlog("1002");
            //gv_rz1.DataSource = dt;
            //gv_rz1.DataBind();

            //DataTable dt_require = new DataTable();
            //dt_require = BJ_CLASS.Getlog(Request["requestid"]);
            //gv_rz3.DataSource = dt_require;
            //gv_rz3.DataBind();

        }

    }
    private void Getstatus()
    {
        if (txt_status_id.Text == "")
        {
        }
        else
        {
            DataTable dt = new DataTable();
            dt = BJ_CLASS.GetBaojia_Get_Agreement_data(Request["requestid"]);
            // DataTable dtbtn2 = YJ_CLASS.GetBTN2(Request["requestid"], "-3","");
            //dt = YJ_CLASS.Getlog("1002");
            gv_rz2.DataSource = dt;
            gv_rz2.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["状态"].ToString() == "争取" && dt.Rows[i]["完成日期"].ToString() != "")
                {
                    Lab_htzt.Text = "争取";
                }
                if (dt.Rows[i]["状态"].ToString() == "合同跟踪" && dt.Rows[i]["完成日期"].ToString() != "")
                {
                    Lab_htzt.Text = "合同跟踪";
                }
                if (dt.Rows[i]["状态"].ToString() == "合同已完成" && dt.Rows[i]["完成日期"].ToString() != "")

                {
                    Lab_htzt.Text = "合同已完成";
                }
                if (dt.Rows[i]["状态"].ToString() == "停止跟踪" && dt.Rows[i]["完成日期"].ToString() != "")
                {
                    Lab_htzt.Text = "停止跟踪";
                }
            }
        }

    }
    //protected void Image2_Click(object sender, ImageClickEventArgs e)
    //{
    //    //int rowIndex = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());
    //    int lnindex = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
    //    if (this.gv_rz3.Rows[lnindex].Cells[6].Text.ToString() != "&nbsp;")
    //    {
    //        ViewState["lv"] = gv_rz3.Rows[lnindex].Cells[6].Text.ToString();
    //        // Page.RegisterClientScriptBlock("showDiv", "<script>goTo()</script>");
    //    }
    //}
    //protected void Image1_Click(object sender, ImageClickEventArgs e)
    //{
    //    int lnindex = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

    //    if (this.gv_rz3.Rows[lnindex].Cells[6].Text.ToString() != "&nbsp;")
    //    {
    //        ViewState["lv"] = gv_rz3.Rows[lnindex].Cells[6].Text.ToString();
    //    }
    //}

    protected void BTN_Sales_sub_Click(object sender, EventArgs e)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            //if (string.IsNullOrEmpty(txt_content.Value.Trim()))
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('本轮报价原因不能为空！')", true);
            //}

            //验证报价明细是否填写完成
            if (gv_bjmx.Rows.Count > 0)
            {
                for (int i = 0; i < gv_bjmx.Rows.Count; i++)
                {
                    //第一行零件号都没有维护的情况下
                    if (((TextBox)this.gv_bjmx.Rows[0].FindControl("txt_ljh")).Text == "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件号不能为空！')", true);
                        return;
                    }
                    //维护了零件号没有维护其他信息的
                    if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text != "" && (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text == "" || ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text == "" || ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text == "" || ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text == ""))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请维护完整报价明细！')", true);
                        return;
                    }

                }
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少维护一项报价明细！')", true);
                return;
            }
            //验证签核中压铸经理和工程经理是否填写
            string empid_jj = "";
            string empid_yz = "";
            string empid_xszg = "";
            string empid_xsjl = "";
            for (int i = 0; i < gv_bjjd.Rows.Count; i++)
            {

                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "(指派)机加经理" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text == "10")
                {

                    empid_jj = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "(指派)压铸经理" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text == "10")
                {

                    empid_yz = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "销售主管")
                {

                    empid_xszg = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                //if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "销售经理")
                //{

                //    empid_xsjl = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                //}
            }
            //机加和压铸的至少有一个部门需要会签
            if (empid_jj == "" && empid_yz == "" && DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析" && DDL_jijia_tk.SelectedValue == "需要" && DDL_yz_tk.SelectedValue == "需要")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少维护一个压铸经理或机加经理')", true);
                ViewState["lv"] = "BJJD";
                return;

            }
            //销售主管和销售经理必须选择
            if ((empid_xszg == "") && (DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析" || DDL_sfxy_bjfx.SelectedValue == "仅需价格核实"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售主管和销售经理不能为空')", true);
                ViewState["lv"] = "BJJD";
                return;

            }
            if (BTN_Sales_sub.Text == "提交")
            {
                //新增一轮的时候报价号不需要变更
                if (Request["update"] != null)
                {

                }
                else
                {
                    if (DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析")
                    {
                        DataTable dtbaojia_no = BJ_CLASS.BJ_baojia_no(System.DateTime.Now.ToString("yyyy"));
                        if (dtbaojia_no.Rows.Count > 0)
                        {
                            string maxno = dtbaojia_no.Rows[0]["xh"].ToString();
                            int max_no = Convert.ToInt32(maxno) + 1;
                            txt_baojia_no.Value = System.DateTime.Now.ToString("yyyy") + "-" + max_no.ToString().PadLeft(4, '0');
                        }

                    }
                    else
                    {
                        DataTable dtbaojia_no_all = BJ_CLASS.BJ_baojia_no_all(txt_baojia_no.Value, DDL_turns.SelectedValue);
                        if (dtbaojia_no_all.Rows.Count > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('不能提交重复的报价号和轮次')", true);
                        }

                    }

                }

                //新增的表单产生序列号requestid
                string sql = "Select next value for  [dbo].[Baojia_requestid_sqc]";
                int requestid_sq = Convert.ToInt16(DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());
                int update = -1;
                update = BJ_CLASS.BTN_Sales_sub(requestid_sq, txt_baojia_no.Value, Convert.ToInt32(DDL_turns.SelectedValue), txt_sales_empid.Value, txt_sales_name.Value, txt_sales_ad.Value, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, DDL_project_size.SelectedValue, DDL_project_level.SelectedValue, DDL_is_stop.SelectedValue, txt_create_by_empid.Value, txt_create_by_name.Value, txt_create_by_ad.Value, txt_create_by_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_baojia_file_path.Text, txt_baojia_desc.Value, DDL_domain.SelectedValue, txt_baojia_start_date.Value, DDL_sfxy_bjfx.SelectedValue, txt_create_date.Value, DDL_wl_tk.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, DDL_sfxj_cg.SelectedValue,txt_cusRequestDate.Value, BTN_Sales_sub.Text);
                if (update > 0)
                {
                    string date = System.DateTime.Now.ToString();
                    //保存报价明细
                    for (int i = 0; i < gv_bjmx.Rows.Count; i++)
                    {
                        if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text != "")
                        {
                            //string strsql = " insert into Baojia_dtl(requestId,baojia_no,turns,ljh,lj_name,quantity_year,material,pc_date)  VALUES('" + requestid_sq + "', '" + txt_baojia_no.Value + "', '" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text + "','" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "')";
                            //DbHelperSQL.ExecuteSql(strsql);

                            System.Text.StringBuilder strsql = new StringBuilder();
                            strsql.Append("insert into Baojia_dtl(requestId,baojia_no,turns,ljh,xz_ljh,old_ljh,lj_name,customer_project,ship_from,ship_to,back_up,quantity_year,material,pc_date,pc_per_price,price_year,pc_mj_price,yj_per_price,yj_mj_price)");
                            strsql.Append("VALUES  ('" + requestid_sq + "', ");
                            strsql.Append(" '" + txt_baojia_no.Value + "',");
                            strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
                            //不变更零件号的，存现零件号
                            if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
                            {
                                strsql.Append(" '" + ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() + "',");
                            }
                            else
                            {
                                //strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
                                strsql.Append(" '',");
                            }
                            //不变更零件号的，存现零件号
                            if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
                            {
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            }
                            else
                            {
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            }
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Text.Trim().Replace("@[/n/r]", "") + "',");
                            string quantity_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
                            strsql.Append(quantity_year == "" ? "null" : quantity_year);

                            strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
                            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

                            string txt_pc_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
                            strsql.Append(txt_pc_per_price == "" ? "null" : txt_pc_per_price);

                            string txt_price_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
                            strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

                            string txt_pc_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
                            strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

                            string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
                            strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

                            string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
                            strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
                            strsql.Append(")");
                            //DbHelperSQL.ExecuteSql(strsql.ToString());
                            int updateaa = -1;
                            updateaa = DbHelperSQL.ExecuteSql(strsql.ToString());
                            if (update > 0)
                            {
                                string strsql_update_mst = " update Baojia_dtl set  price_year =quantity_year* pc_per_price  where requestid='" + requestid_sq + "'";
                                DbHelperSQL.ExecuteSql(strsql_update_mst);
                                BJ_CLASS.Baojia_Dtl2Agreement(requestid_sq);
                            }
                            else
                            {
                                //主表插入，明细表有执行不成功的，删除主表信息
                                string strsql_update_mst = " delete Baojia_mst  where requestid='" + requestid_sq + "'";
                                DbHelperSQL.ExecuteSql(strsql_update_mst);
                                string strsql_update_dtl = " delete Baojia_dtl  where requestid='" + requestid_sq + "'";
                                DbHelperSQL.ExecuteSql(strsql_update_dtl);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交或联系管理员！')", true);

                                return;
                            }


                        }


                    }
                    //保存报价进度
                    for (int i = 0; i < gv_bjjd.Rows.Count; i++)
                    {
                        //删除无人员选择
                        //if (((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        //{

                        //}
                        //else
                        //{
                        //    //string receive_date = System.DateTime.Now.ToString();
                        string strsql = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date)  VALUES('" + requestid_sq + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flowid")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("big_flowid")).Text + "','" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("Step")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text + "', '" + ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text + "', iif('" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Text + "'='',null,'" + date + "'), '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("require_date")).Text + "')";
                        DbHelperSQL.ExecuteSql(strsql);
                        //}
                    }
                    //插入申请人log
                    //string date = System.DateTime.Now.ToString();
                    string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + requestid_sq + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(申请)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','" + txt_content.Value + "')";
                    DbHelperSQL.ExecuteSql(strsql_xs);
                    //新增一轮的时候需要把第一轮的合同及状态复制过来
                    if (Request["update"] != null)
                    {
                        //UPDATE_Baojia_agreement_flow();
                        //    for (int i = 0; i < gv_htgz.Rows.Count; i++)
                        //    {
                        //    //string strsql = " insert into Baojia_agreement_flow(requestid,baojia_no,turns,description,ljh,dingdian_date,sdxx,create_by_empid)  VALUES('" + requestid_sq + "', '" + txt_baojia_no.Value + "', '" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "','" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text + "', '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "', '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "', '" + txt_update_user_name.Value + "')";
                        //    //DbHelperSQL.ExecuteSql(strsql);

                        //    System.Text.StringBuilder strsql = new StringBuilder();
                        //    strsql.Append("insert into Baojia_agreement_flow(requestId,baojia_no,turns,description,ljh,ship_from,ship_to,dingdian_date,sdxx,lj_status,create_by_empid,quantity_year,pc_per_price,price_year,pc_mj_price,currency,exchange_rate)");
                        //    strsql.Append("VALUES  ('" + requestid_sq + "', ");
                        //    strsql.Append(" '" + txt_baojia_no.Value + "',");
                        //    strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "',");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
                        //    strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "',");
                        //    strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
                        //    strsql.Append(" '" + txt_update_user_name.Value + "',");

                        //    string quantity_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
                        //    strsql.Append(quantity_year == "" ? "null" : quantity_year + ",");

                        //    //strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
                        //    //strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

                        //    string txt_pc_per_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
                        //    strsql.Append("," + txt_pc_per_price == "" ? "null" : txt_pc_per_price);

                        //    string txt_price_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
                        //    strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

                        //    string txt_pc_mj_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
                        //    strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

                        //    strsql.Append(", '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
                        //    strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "'");
                        //    //string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
                        //    //strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

                        //    //string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
                        //    //strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
                        //    strsql.Append(")");

                        //    DbHelperSQL.ExecuteSql(strsql.ToString());



                        //    //获取定点日期
                        //    string strsql_update_dingdian = " update Baojia_mst set hetong_status='合同跟踪', dingdian_date= '" + ((TextBox)this.gv_htgz.Rows[0].FindControl("dingdian_date")).Text + "' where requestid='" + requestid_sq + "'";
                        //        DbHelperSQL.ExecuteSql(strsql_update_dingdian);
                        //        ////获取合同完成日期
                        //        //if (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text == "已收到全部定点文件")
                        //        //{
                        //        //    string strsql_update = " update Baojia_mst set hetong_status='合同已完成', hetong_complet_date= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "' where requestid='" + requestid_sq + "'";
                        //        //    DbHelperSQL.ExecuteSql(strsql_update);
                        //        //}
                        //}

                    }
                    //删除未选择的机加或者压铸的人员行
                    int update_delete = -1;
                    update_delete = BJ_CLASS.Baojia_YZ_JJ_DELETE(requestid_sq);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                    BTN_Sales_sub.Enabled = false;
                    BJ_CLASS.SendMail(requestid_sq);

                    ValidateUser connect = new ValidateUser();
                    string user = "it";
                    string client_ip = "ks-server";
                    string password = "pgi_1234";
                    string year = txt_baojia_no.Value.Substring(0, 4);
                    bool isImpersonated = false;

                    try
                    {
                        if (connect.impersonateValidUser(user, client_ip, password))
                        {
                            isImpersonated = true;
                            if (DDL_turns.SelectedValue == "1")
                            {
                                string sourcePath = @"\\172.16.5.50\销售部\02 报价产品和开发\" + year + "报价产品和开发\\";
                                sourcePath = sourcePath + txt_baojia_no.Value;
                                if (!Directory.Exists(sourcePath))
                                {
                                    Directory.CreateDirectory(sourcePath);

                                }


                            }

                        }
                    }
                    catch
                    {
                        //    BLL.path_config.path_configUpdate(client_ip, "异常");

                    }
                    finally
                    {
                        if (isImpersonated)
                            connect.undoImpersonation();
                    }


                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后提交！')", true);
                    ViewState["lv"] = "BJJD";
                }

            }
            else if (BTN_Sales_sub.Text == "修改")
            {
                int update = -1;
                update = BJ_CLASS.BTN_Sales_sub(Convert.ToInt32(Request["requestid"]), txt_baojia_no.Value, Convert.ToInt32(DDL_turns.SelectedValue), txt_sales_empid.Value, txt_sales_name.Value, txt_sales_ad.Value, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, DDL_project_size.SelectedValue, DDL_project_level.SelectedValue, DDL_is_stop.SelectedValue, txt_create_by_empid.Value, txt_create_by_name.Value, txt_create_by_ad.Value, txt_create_by_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_baojia_file_path.Text, txt_baojia_desc.Value, DDL_domain.SelectedValue, txt_baojia_start_date.Value, DDL_sfxy_bjfx.SelectedValue, txt_create_date.Value, DDL_wl_tk.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, DDL_sfxj_cg.SelectedValue,txt_cusRequestDate.Value, BTN_Sales_sub.Text);
                if (update > 0)
                {
                    int update_Baojia_dtl = -1;
                    update_Baojia_dtl = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_dtl");
                    int update_Baojia_sign_flow = -1;
                    update_Baojia_sign_flow = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_sign_flow");
                    if (update_Baojia_dtl > 0)
                    {
                        //插入申请人log
                        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                        string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(修改)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','" + txt_content.Value + "')";
                        DbHelperSQL.ExecuteSql(strsql_xs);
                        //保存报价明细
                        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
                        {
                            if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text != "")
                            {
                                //string strsql = " insert into Baojia_dtl(requestId,baojia_no,turns,ljh,lj_name,quantity_year,material,pc_date)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "', '" + txt_baojia_no.Value + "', '" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text + "','" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "', '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "')";
                                //DbHelperSQL.ExecuteSql(strsql);

                                System.Text.StringBuilder strsql = new StringBuilder();
                                strsql.Append("insert into Baojia_dtl(requestId,baojia_no,turns,ljh,xz_ljh,old_ljh,lj_name,customer_project,ship_from,ship_to,back_up,quantity_year,material,pc_date,pc_per_price,price_year,pc_mj_price,yj_per_price,yj_mj_price)");
                                strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
                                strsql.Append(" '" + txt_baojia_no.Value + "',");
                                strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
                                //不变更零件号的，存现零件号
                                if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
                                {
                                    strsql.Append(" '" + ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() + "',");
                                }
                                else
                                {
                                    //strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
                                    strsql.Append(" '',");
                                }
                                //不变更零件号的，存现零件号
                                if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
                                {
                                    strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                }
                                else
                                {
                                    strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                }
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Text.Trim().Replace("@[/n/r]", "") + "',");
                                string quantity_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
                                strsql.Append(quantity_year == "" ? "null" : quantity_year);

                                strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
                                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

                                string txt_pc_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
                                strsql.Append(txt_pc_per_price == "" ? "null" : txt_pc_per_price);

                                string txt_price_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
                                strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

                                string txt_pc_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
                                strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

                                string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
                                strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

                                string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
                                strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
                                strsql.Append(")");
                                DbHelperSQL.ExecuteSql(strsql.ToString());
                            }

                        }
                        BJ_CLASS.Baojia_Dtl2Agreement(Convert.ToInt32(Request["requestid"]));

                        //保存报价进度
                        for (int i = 0; i < gv_bjjd.Rows.Count; i++)
                        {
                            //删除无人员选择
                            //if (((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text== "(指派)压铸经理")
                            //{

                            //}
                            //else
                            //{
                            //string receive_date = System.DateTime.Now.ToString();
                            string strsql = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flowid")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("big_flowid")).Text + "','" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("Step")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text + "', '" + ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text + "', iif('" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Text + "'='',null,'" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Text + "'), '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("require_date")).Text + "')";
                            DbHelperSQL.ExecuteSql(strsql);
                            //}

                        }
                        //移转取消
                        //string date = System.DateTime.Now.ToString();
                        string strsql_update = " update Baojia_mst set yizhuan_date= '" + date + "',yizhuan_empid='" + txt_update_user.Value + "',yizhuan_name='" + txt_update_user_name.Value + "',is_yizhuan='0' where requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
                        DbHelperSQL.ExecuteSql(strsql_update);
                        //删除未选择列表的人员
                        int update_delete = -1;
                        update_delete = BJ_CLASS.Baojia_YZ_JJ_DELETE(Convert.ToInt32(Request["requestid"]));


                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
                        BTN_Sales_sub.Enabled = false;
                        BJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]));
                        //BJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_baojia_no.Value);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改！')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改！')", true);
                }
            }
            ts.Complete();
        }
        ViewState["lv"] = "BJJD";
    }
    protected void gBindGridBjMXv_rz3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        Image Image1 = e.Row.FindControl("Image1") as Image;
        Image Image2 = e.Row.FindControl("Image2") as Image;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                //int indexID = this.GridView1.PageIndex * this.GridView1.PageSize + e.Row.RowIndex + 1;
                //e.Row.Cells[0].Text = indexID.ToString();
                if (Convert.ToInt32(e.Row.Cells[5].Text) > -1 & Convert.ToInt32(e.Row.Cells[5].Text) < 5 & e.Row.Cells[4].Text.ToString() != "" & e.Row.Cells[4].Text.ToString() != "&nbsp;")
                {
                    string t1 = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("yyyy-MM-dd");
                    string t2 = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("yyyy-MM-dd");


                    if (DateTime.Compare(Convert.ToDateTime(t1), Convert.ToDateTime(t2)) > 0)
                    //if (Convert.ToDateTime(e.Row.Cells[2].Text.ToString()) > Convert.ToDateTime(e.Row.Cells[4].Text.ToString()))
                    {

                        Image2.Visible = true;
                        Image1.Visible = false;

                    }
                    else
                    {
                        Image2.Visible = false;
                        Image1.Visible = true;
                    }
                }
                else
                {
                    Image2.Visible = false;
                    Image1.Visible = true;
                }
            }
        }
    }
    private void gv_bjmx_csh()
    {
        //DataTable dtbtn = BJ_CLASS.GetBTN(Request["requestid"], ViewState["empid"].ToString());
        if (ViewState["empid"].ToString() == txt_sales_empid.Value && txt_status_id.Text == "100")
        {
            for (int i = 0; i < gv_bjmx.Rows.Count; i++)
            {
                //((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Enabled = true;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Enabled = true;
                ((Button)this.gv_bjmx.FooterRow.FindControl("btnAdd")).Visible = true;
                ((Button)this.gv_bjmx.Rows[i].FindControl("btnDel")).Visible = true;

                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Enabled = false;
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Enabled = false;
                if (Request["requestid"] == null)
                {
                    //((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Visible = false;
                    gv_bjmx.Columns[0].Visible = false;
                }
                else
                {
                    //((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Visible = true;
                    gv_bjmx.Columns[0].Visible = true;
                }
            }
            if (Request["requestid"] != null && Request["update"] == null)
            {
                BTN_Sales_sub.Text = "修改";
                BTN_Sales_sub.Visible = true;
            }
        }
        txt_ljh_Text_change();

    }
    private void gv_bjmx_csh_bsp()
    {
        //DataTable dtbtn = BJ_CLASS.GetBTN(Request["requestid"], ViewState["empid"].ToString());

        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Enabled = true;
            ((Button)this.gv_bjmx.FooterRow.FindControl("btnAdd")).Visible = true;
            ((Button)this.gv_bjmx.Rows[i].FindControl("btnDel")).Visible = true;

            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Enabled = true;
            ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Enabled = false;

        }
        txt_ljh_Text_change();

    }
    public void BindGridBjMX(DataTable dt, GridView gv)
    {
        gv_bjmx.DataSource = dt;
        gv_bjmx.DataBind();
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            DataTable dtljh = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "xz_ljh_dt");
            //dtqq.Rows.Add("");
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataSource = dtljh;
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataTextField = "xz_ljh";
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataValueField = "xz_ljh";
            ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).DataBind();
            ((DropDownList)gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Items.Insert(0, new ListItem("", ""));
        }
    }
    public void BindGridhtgz(DataTable dt, GridView gv)
    {
        gv_htgz.DataSource = dt;
        gv_htgz.DataBind();
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            DataTable dttest = BJ_CLASS.BJ_BASE("agreement_types");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataSource = BJ_CLASS.BJ_BASE("agreement_types");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataBind();
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));

            DataTable dt_currency = BJ_CLASS.BJ_BASE("currency");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataSource = BJ_CLASS.BJ_BASE("currency");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).DataBind();
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).Items.Insert(0, new ListItem("", ""));

            DataTable dtlj_status = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataSource = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataTextField = "lookup_desc";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataBind();
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).Items.Insert(0, new ListItem("", ""));


            DataTable dtqq = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "xz_ljh");
            //dtqq.Rows.Add("");
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataSource = dtqq;
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataTextField = "xz_ljh";
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataValueField = "id";
            ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).DataBind();
            ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Items.Insert(0, new ListItem("", ""));

            //DataTable dtship_to = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ship_to");
            //dtqq.Rows.Add("");
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataSource = dtship_to;
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataTextField = "ship_to";
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataValueField = "ship_to";
            //((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataBind();
            //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Items.Insert(0, new ListItem("", ""));



            ((TextBox)gv_htgz.Rows[i].FindControl("dingdian_date")).Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            ((TextBox)gv_htgz.Rows[i].FindControl("create_by_empid")).Text = txt_update_user_name.Value;

        }
    }
    private void InitTable_bjmx()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add(new DataColumn("baojia_no"));
        //dt.Columns.Add(new DataColumn("turns"));
        dt.Columns.Add(new DataColumn("xz_ljh"));
        dt.Columns.Add(new DataColumn("ljh"));
        dt.Columns.Add(new DataColumn("lj_name"));
        dt.Columns.Add(new DataColumn("customer_project"));
        dt.Columns.Add(new DataColumn("ship_from"));
        dt.Columns.Add(new DataColumn("ship_to"));
        dt.Columns.Add(new DataColumn("quantity_year"));
        dt.Columns.Add(new DataColumn("material"));
        dt.Columns.Add(new DataColumn("pc_date"));
        dt.Columns.Add(new DataColumn("pc_per_price"));
        dt.Columns.Add(new DataColumn("price_year"));
        dt.Columns.Add(new DataColumn("pc_mj_price"));
        dt.Columns.Add(new DataColumn("yj_per_price"));
        dt.Columns.Add(new DataColumn("yj_mj_price"));
        dt.Columns.Add(new DataColumn("back_up"));
        dt.Columns.Add(new DataColumn("old_ljh"));
        //DataRow dr = dt.NewRow();
        //dr["baojia_no"] = "";
        //dr["turns"] = "";
        //dr["ljh"] = "";
        //dr["lj_name"] = "";
        //dr["quantity_year"] = "";
        //dr["material"] = "";
        //dr["pc_date"] = "";
        //dr["pc_per_price"] = "";
        //dr["price_year"] = "";
        //dr["pc_mj_price"] = "";
        //dr["yj_per_price"] = "";
        //dr["yj_mj_price"] = "";
        //dt.Rows.Add(dr);
        ViewState["dt"] = dt;
        int ln = gv_bjmx.Rows.Count;
        for (int i = ln; i < 15; i++)
        {
            DataRow ldr = dt.NewRow();
            //ldr["baojia_no"] = txt_baojia_no.Value;
            //ldr["turns"] = DDL_turns.SelectedValue;
            dt.Rows.Add(ldr);
        }
        this.gv_bjmx.DataSource = dt;
        this.gv_bjmx.DataBind();
        gv_bjmx_csh();
    }
    private void InitTable_htgz()
    {
        //合同追踪页面加载
        DataTable dtht = new DataTable();
        dtht.Columns.Add(new DataColumn("id"));
        dtht.Columns.Add(new DataColumn("xz_ljh"));
        dtht.Columns.Add(new DataColumn("ljh"));
        dtht.Columns.Add(new DataColumn("customer_project"));
        dtht.Columns.Add(new DataColumn("ship_from"));
        dtht.Columns.Add(new DataColumn("ship_to"));
        dtht.Columns.Add(new DataColumn("dingdian_date"));
        dtht.Columns.Add(new DataColumn("pc_date"));
        dtht.Columns.Add(new DataColumn("end_date"));
        dtht.Columns.Add(new DataColumn("quantity_year"));
        dtht.Columns.Add(new DataColumn("currency"));
        dtht.Columns.Add(new DataColumn("exchange_rate"));
        dtht.Columns.Add(new DataColumn("pc_per_price"));
        dtht.Columns.Add(new DataColumn("price_year"));
        dtht.Columns.Add(new DataColumn("pc_mj_price"));
        dtht.Columns.Add(new DataColumn("sdxx"));
        dtht.Columns.Add(new DataColumn("lj_status"));
        dtht.Columns.Add(new DataColumn("Description"));
        dtht.Columns.Add(new DataColumn("create_by_empid"));


        ViewState["dtht"] = dtht;
        int lnht = gv_htgz.Rows.Count;
        for (int i = lnht; i < 1; i++)
        {
            DataRow ldrht = dtht.NewRow();
            //ldrht["id"] = "";
            ldrht["dingdian_date"] = System.DateTime.Now.ToString("yyyy-MM-dd");
            ldrht["exchange_rate"] = 1;
            ldrht["lj_status"] = "已定点合同跟踪";
            ldrht["currency"] = "CNY";
            ldrht["quantity_year"] = 0;
            ldrht["pc_per_price"] = 0;
            ldrht["price_year"] = 0;
            ldrht["pc_mj_price"] = 0;
            ldrht["create_by_empid"] = txt_update_user_name.Value;
            dtht.Rows.Add(ldrht);
        }















    }
    public static DataTable GetDataTableFromGridView1(GridView gv, DataTable dts)
    {
        DataTable dt = new DataTable();
        dt = dts.Clone();
        dt.Rows.Clear();
        for (int j = 0; j < gv.Rows.Count; j++)
        {
            DataRow dr = dt.NewRow();
            for (int i = 0; i < gv.Columns.Count - 1; i++)
            {
                if (gv.Rows[j].Cells[i].Controls[1].GetType().Name == "TextBox")
                {
                    if (((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text != "")
                    {
                        dr[i] = ((TextBox)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();

                    }
                    else
                    {
                        //if (gv.Rows[j].Cells[i].Controls[1].ID == "txt_exchange_rate")
                        //{
                        //    dr[i] = 1;
                        //}
                        //else
                        //{
                        //dr[i] = 0;
                        //}
                    }
                }
                if (gv.Rows[j].Cells[i].Controls[1].GetType().Name == "DropDownList")
                {
                    dr[i] = ((DropDownList)gv.Rows[j].Cells[i].Controls[1]).Text.Trim();
                }
                else
                {

                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void gv_bjmx_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dts = ViewState["dt"] as DataTable;
        DataTable dt = GetDataTableFromGridView1(gv_bjmx, dts);

        if (e.CommandName == "add")
        {
            DataRow dr = dt.NewRow();
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).Parent.Parent);
            //dr["baojia_no"] = txt_baojia_no.Value;
            //dr["turns"] = DDL_turns.SelectedValue;
            //dr["ljh"] = (row.FindControl("txt_ljh") as TextBox).Text;
            //dr["lj_name"] = (row.FindControl("txtAdd_lj_name") as TextBox).Text;
            //dr["quantity_year"] = (row.FindControl("txtAdd_quantity_year") as TextBox).Text;
            //dr["material"] = (row.FindControl("txtAdd_material") as TextBox).Text;
            //dr["pc_date"] = (row.FindControl("txtAdd_pc_date") as TextBox).Text;
            //dr["pc_per_price"] = (row.FindControl("txtAdd_pc_per_price") as TextBox).Text;
            //dr["price_year"] = (row.FindControl("txtAdd_price_year") as TextBox).Text;
            //dr["pc_mj_price"] = (row.FindControl("txtAdd_pc_mj_price") as TextBox).Text;
            //dr["yj_per_price"] = (row.FindControl("txtAdd_yj_per_price") as TextBox).Text;
            //dr["yj_mj_price"] = (row.FindControl("txtAdd_yj_mj_price") as TextBox).Text;

            dt.Rows.Add(dr);

            gv_bjmx_csh();
            ViewState["lv"] = "BJJD";
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
        this.gv_bjmx.DataSource = dt;
        this.gv_bjmx.DataBind();
        ddl_bjmx();
        gv_bjmx_csh();
    }
    //protected void gv_bjmx_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //e.Row.Attributes.Add("onclick", "document.getElementById('txt_pc_date').value='" + e.Row.Cells[6].Text + "'");
    //        ((TextBox)e.Row.Cells[6].FindControl("txt_pc_date")).Attributes.Add("onclick", "document.getElementById('txt_pc_date').value='" + e.Row.Cells[6].Text + "'");
    //    }
    //}
    protected void DDL_project_size_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txt_create_by_dept.Value != "销售二部")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售部才可申请报价流程！')", true);
            DDL_project_size.SelectedValue = "";
            BTN_Sales_sub.Visible = false;
        }
        else
        {
            if (DDL_domain.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择申请工厂！')", true);
                DDL_project_size.SelectedValue = "";
                return;
            }
            if (DDL_customer_name.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择直接顾客！')", true);
                return;
            }
            if (DDL_wl_tk.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择物流条款！')", true);
                return;
            }
            if (DDL_bz_tk.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择包装条款！')", true);
                return;
            }
            if (DDL_jijia_tk.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择是否需要机加报价！')", true);
                return;
            }
            if (DDL_yz_tk.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择是否需要压铸报价！')", true);
                return;
            }
            if (DDL_sfxj_cg.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择是否需要采购询价！')", true);
                return;
            }
            if (DDL_wl_tk.SelectedValue == "本轮不需要" && DDL_bz_tk.SelectedValue == "不需要" && DDL_sfxj_cg.SelectedValue == "不需要" && DDL_jijia_tk.SelectedValue == "不需要" && DDL_yz_tk.SelectedValue == "不需要")
            {
                DDL_sfxy_bjfx.SelectedValue = "仅需价格核实";
            }
            else
            {
                DDL_sfxy_bjfx.SelectedValue = "需要详细价格分析";
            }

            //产生报价号
            DataTable dtbaojia_no = BJ_CLASS.BJ_baojia_no(System.DateTime.Now.ToString("yyyy"));
            if (txt_baojia_no.Value == "")
            {
                if (dtbaojia_no.Rows.Count > 0)
                {
                    string maxno = dtbaojia_no.Rows[0]["xh"].ToString();
                    int max_no = Convert.ToInt32(maxno) + 1;
                    txt_baojia_no.Value = System.DateTime.Now.ToString("yyyy") + "-" + max_no.ToString().PadLeft(4, '0');
                }
                else
                {
                    txt_baojia_no.Value = System.DateTime.Now.ToString("yyyy") + "-0001";
                }
            }

            if (DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析")
            {

                txt_baojia_no.Disabled = true;
                DDL_turns.Enabled = false;
                txt_baojia_start_date.Disabled = true;
                txt_create_date.Disabled = true;
                if (Request["update"] != null)
                {

                }
                else
                {
                    DDL_turns.SelectedValue = "1";
                    txt_content.Value = "首次报价";
                }

                gv_bjmx_csh();
            }
            else
            {
                txt_baojia_no.Disabled = true;
                DDL_turns.Enabled = false;
                txt_baojia_start_date.Disabled = true;
                txt_create_date.Disabled = true;
                if (Request["update"] != null)
                {
                    txt_baojia_no.Disabled = true;
                    DDL_turns.Enabled = false;
                    txt_baojia_start_date.Disabled = true;
                    txt_create_date.Disabled = true;
                }
                else
                {
                    DDL_turns.SelectedValue = "1";
                }

                gv_bjmx_csh_bsp();

                gv_bjjd.DataSource = null;
                gv_bjjd.DataBind();

            }


            //增加第一轮报价时自动带出报价路径2018/02/25
            if (DDL_turns.SelectedValue == "1")
            {
                string year = txt_baojia_no.Value.Substring(0, 4);
                string sourcePath = @"\\172.16.5.50\销售部\02 报价产品和开发\" + year + "报价产品和开发\\";
                txt_baojia_file_path.Text = sourcePath + txt_baojia_no.Value;
            }


            if (DDL_sfxy_bjfx.SelectedValue == "")
            {
                DDL_project_size.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择是否需要价格分析！')", true);
                return;
            }
            else if (DDL_sfxy_bjfx.SelectedValue == "不需要价格分析")
            {

            }
            else
            {
                if (DDL_project_size.SelectedValue != "")
                {

                    DataTable dt = BJ_CLASS.BJ_GetSPR(DDL_project_size.Text, txt_sales_empid.Value, DDL_domain.SelectedValue, DDL_sfxy_bjfx.SelectedValue, DDL_customer_name.SelectedValue, DDL_wl_tk.SelectedValue, DDL_sfxj_cg.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, txt_ZG_empid.Value);
                    ViewState["detail"] = dt;
                    gv_bjjd.DataSource = dt;
                    gv_bjjd.DataBind();
                    Panel1.Visible = true;
                    for (int i = 0; i < gv_bjjd.Rows.Count; i++)
                    {
                        //((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text = dt.Rows[i]["flow"].ToString();
                        string dept = dt.Rows[i]["flow"].ToString();
                        DataTable aa = BJ_CLASS.BJ_dept(dept);
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept(dept);
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
                        ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
                        ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text != "")
                        {
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = ((TextBox)gv_bjjd.Rows[i].FindControl("txt")).Text;
                        }
                        //((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text = dt.Rows[i]["empid"].ToString();
                        string js = dt.Rows[i]["step"].ToString();
                        string stepid = dt.Rows[i]["stepid"].ToString();
                        if (js == "销售工程师(实际报出)" || js.Substring(js.Length - 3) == "工程师" || js.Substring(js.Length - 2) == "副总" || js.Substring(js.Length - 2) == "经理" || js.Substring(js.Length - 2) == "主管")
                        {
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Style.Add("BackColor", "black");
                        }
                        if ((js == "(指派)机加经理" && stepid == "10") || (js == "(指派)压铸经理" && stepid == "10"))
                        {
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = true;
                            // ((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                            ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                        }
                        if ((js == "销售主管" && stepid == "20") || (js == "销售经理" && stepid == "30"))
                        {
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Enabled = false;
                            // ((TextBox)gv_bjjd.Rows[i].FindControl("step")).ForeColor = System.Drawing.Color.Red;
                            ((TextBox)gv_bjjd.Rows[i].FindControl("step")).Style.Add("background-color", "red");
                        }
                        if (stepid == "10")
                        {
                            if (js == "(指派)机加经理")
                            {
                                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept_step(dept, "工程经理");
                            }
                            if (js == "(指派)压铸经理")
                            {
                                ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataSource = BJ_CLASS.BJ_dept_step(dept, "压铸工艺经理");
                            }
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataTextField = "lastname";
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).DataValueField = "workcode";
                            ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).DataBind();
                            ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Items.Insert(0, new ListItem("", ""));
                        }
                            //gridview报价进度只读
                            ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("require_date")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("sign_date")).Enabled = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("Operation_time")).Enabled = false;
                        ((Button)this.gv_bjjd.Rows[i].FindControl("btncomit")).Visible = false;
                        ((TextBox)this.gv_bjjd.Rows[i].FindControl("txt_sign_desc")).Enabled = false;
                    }
                }

            }
        }
        ViewState["lv"] = "BJJD";
    }

    protected void gv_htgz_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtht = ViewState["dtht"] as DataTable;
        DataTable dtsht = GetDataTableFromGridView1(gv_htgz, dtht);
        if (e.CommandName == "addht")
        {
            DataRow dr = dtsht.NewRow();
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).Parent.Parent);
            //dr["ljh"] = (row.FindControl("ddl_ljh") as DropDownList).Text;
            dr["id"] = "0";
            dr["dingdian_date"] = System.DateTime.Now.ToString("yyyy-MM-dd");
            dr["exchange_rate"] = 1;
            dr["lj_status"] = "已定点合同跟踪";
            dr["quantity_year"] = 0;
            dr["pc_per_price"] = 0;
            dr["price_year"] = 0;
            dr["pc_mj_price"] = 0;

            //dr["sdxx"] = (row.FindControl("ddl_sdxx") as DropDownList).Text;
            //dr["Description"] = (row.FindControl("Description") as TextBox).Text;
            dr["create_by_empid"] = txt_update_user_name.Value;


            dtsht.Rows.Add(dr);
            //((TextBox)gv_htgz.Rows[i].FindControl("dingdian_date")).Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            //((TextBox)gv_htgz.Rows[i].FindControl("create_by_name")).Text = txt_update_user.Value;
        }
        if (e.CommandName == "delht")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            //if (index == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少保留一行！')", true);
            //}
            //else
            //{
            //    dtsht.Rows.RemoveAt(index);
            //}
            dtsht.Rows.RemoveAt(index);
        }
        ViewState["dtht"] = dtsht;
        this.gv_htgz.DataSource = dtsht;
        this.gv_htgz.DataBind();
        ddl_htgz();
        //for (int i = 0; i < gv_htgz.Rows.Count; i++)
        //{
        //    DataTable dttest = BJ_CLASS.BJ_BASE("agreement_types");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataSource = BJ_CLASS.BJ_BASE("agreement_types");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).DataBind();
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtsdxx")).Text;
        //    }

        //    DataTable dt_currency = BJ_CLASS.BJ_BASE("currency");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataSource = BJ_CLASS.BJ_BASE("currency");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).DataBind();
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).Items.Insert(0, new ListItem("", ""));
        //    //((DropDownList)gv_htgz.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_currency")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtcurrency")).Text;
        //    }

        //    DataTable dtlj_status = BJ_CLASS.BJ_BASE("lj_status");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataSource = BJ_CLASS.BJ_BASE("lj_status");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataTextField = "lookup_desc";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataValueField = "lookup_desc";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).DataBind();

        //    if (((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text = ((TextBox)gv_htgz.Rows[i].FindControl("txtlj_status")).Text;
        //    }


        //    DataTable dtqq = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ljh");
        //    dtqq.Rows.Add("");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataSource = dtqq;
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataTextField = "ljh";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).DataValueField = "ljh";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).DataBind();
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Items.Insert(0, new ListItem("", ""));
        //    string ljh = ((TextBox)gv_htgz.Rows[i].FindControl("txtljh")).Text;
        //    if( ljh!= "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ljh")).Text = ljh;
        //    }

        //    DataTable dtship_to = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, "ship_to");
        //    dtqq.Rows.Add("");
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataSource = dtship_to;
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataTextField = "ship_to";
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataValueField = "ship_to";
        //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).DataBind();
        //    ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Items.Insert(0, new ListItem("", ""));

        //    string ship_to = ((TextBox)gv_htgz.Rows[i].FindControl("txtship_to")).Text;
        //    if (ship_to != "")
        //    {
        //        ((DropDownList)gv_htgz.Rows[i].FindControl("ddl_ship_to")).Text = ship_to;
        //    }

        //}
        ViewState["lv"] = "htgz";

    }
    protected void gv_bjjd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "tj")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            if (((TextBox)gv_bjjd.Rows[index].FindControl("txt")).Text != "")
            {
                ((TextBox)gv_bjjd.Rows[index].FindControl("txt")).Text = ((TextBox)gv_bjjd.Rows[index].FindControl("txt")).Text + "," + ((DropDownList)gv_bjjd.Rows[index].FindControl("ddl_empid")).Text;
            }
            else
            {
                ((TextBox)gv_bjjd.Rows[index].FindControl("txt")).Text = ((DropDownList)gv_bjjd.Rows[index].FindControl("ddl_empid")).Text;
            }
            ViewState["lv"] = "BJJD";
        }
        if (e.CommandName == "delete_qk")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            ((TextBox)gv_bjjd.Rows[index].FindControl("txt")).Text = "";
            ViewState["lv"] = "BJJD";
        }
        if (e.CommandName == "comit")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            string JL = "";
            string JL_ID = "";
            string JL_ID_ZP = "";
            string GCS = "";
            string GCS_ID = "";
            string ZG = "";
            string ZG_ID = "";
            string WL = "";
            string WL_ID = "";
            JL_ID_ZP = ((TextBox)gv_bjjd.Rows[index].FindControl("ID")).Text;
            string sign_desc = ((TextBox)gv_bjjd.Rows[index].FindControl("txt_sign_desc")).Text;
            //if (txt_content.Value == "")
            //{
            //    ViewState["lv"] = "BJJD";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写提交说明！')", true);
            //    return;
            //}
            if (((TextBox)gv_bjjd.Rows[index].FindControl("txt_sign_desc")).Text == "")
            {
                ViewState["lv"] = "BJJD";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('提交说明不能为空！')", true);

                ((Label)gv_bjjd.Rows[index].FindControl("Lab_yz")).Visible = true;
                return;
            }
            if (((TextBox)gv_bjjd.Rows[index].FindControl("stepid")).Text == "10" && ((TextBox)gv_bjjd.Rows[index].FindControl("big_flowid")).Text == "200")
            {


                if (((DropDownList)gv_bjjd.Rows[index].FindControl("ddl_empid")).Text == "")
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('经理不能为空！')", true);
                    return;
                }
                else
                {

                    JL = ((DropDownList)gv_bjjd.Rows[index].FindControl("ddl_empid")).Text;

                    JL_ID = ((TextBox)gv_bjjd.Rows[index + 3].FindControl("ID")).Text;
                    //JL_ID_ZP = ((TextBox)gv_bjjd.Rows[index].FindControl("ID")).Text;

                }

                //物流部时
                if (((TextBox)gv_bjjd.Rows[index].FindControl("flow")).Text == "物流")
                {
                    for (int i = 0; i < gv_bjjd.Rows.Count; i++)
                    {
                        //包装工程师
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "包装工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择包装工程师！')", true);
                            return;
                        }

                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "包装工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }
                        //物流主管
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "物流主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择物流主管！')", true);
                            return;
                        }
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "物流主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }
                        //仓储工程师
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "仓储工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择仓储工程师！')", true);
                            return;
                        }
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "仓储工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }
                        //仓储主管
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "仓储主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择仓储主管！')", true);
                            return;
                        }
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "仓储主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }
                        //运输工程师
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "运输工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择运输工程师！')", true);
                            return;
                        }
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "运输工程师" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }
                        //运输主管
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "运输主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text == "")
                        {
                            ViewState["lv"] = "BJJD";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择运输主管！')", true);
                            return;
                        }
                        if (((TextBox)gv_bjjd.Rows[i].FindControl("step")).Text == "运输主管" && ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text != "")
                        {
                            WL = ((DropDownList)gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                            WL_ID = ((TextBox)gv_bjjd.Rows[i].FindControl("ID")).Text;
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + WL + "' where ID='" + WL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);
                        }

                    }



                }

                else
                {

                    if (((DropDownList)gv_bjjd.Rows[index + 1].FindControl("ddl_empid")).Text == "")
                    {
                        ViewState["lv"] = "BJJD";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择工程师！')", true);
                        return;
                    }
                    else
                    {
                        GCS = ((DropDownList)gv_bjjd.Rows[index + 1].FindControl("ddl_empid")).Text;
                        GCS_ID = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("ID")).Text;

                    }
                    if (((DropDownList)gv_bjjd.Rows[index + 2].FindControl("ddl_empid")).Text == "")
                    {
                        ViewState["lv"] = "BJJD";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择主管！')", true);
                        return;
                    }
                    else
                    {
                        ZG = ((DropDownList)gv_bjjd.Rows[index + 2].FindControl("ddl_empid")).Text;
                        ZG_ID = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("ID")).Text;

                    }

                    if (JL_ID != "" && GCS_ID != "" && ZG_ID != "")
                    {
                        if (DDL_project_size.SelectedValue == "小于1000万")
                        {

                        }
                        else
                        {
                            string strSqlJL = "update Baojia_sign_flow set empid='" + JL + "' where ID='" + JL_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlJL);
                        }
                        if (((TextBox)gv_bjjd.Rows[index].FindControl("flow")).Text == "机加" && ((TextBox)gv_bjjd.Rows[index].FindControl("step")).Text == "(指派)机加经理")
                        {
                            int requestid = Convert.ToInt32(Request["requestid"]);
                            if (((TextBox)gv_bjjd.Rows[index + 1].FindControl("txt")).Text == "")
                            {
                                ViewState["lv"] = "BJJD";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请点击添加工程师，可添加多人！')", true);
                                return;
                            }
                            if (((TextBox)gv_bjjd.Rows[index + 2].FindControl("txt")).Text == "")
                            {
                                ViewState["lv"] = "BJJD";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请点击添加主管，可添加多人！')", true);
                                return;
                            }
                            //遍历选择工程师
                            string GCS_2 = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("txt")).Text;//从文本框读取

                            string[] str = GCS_2.Split(',');//调用Split方法，定义string[]类型的变量
                            if (str.Length > 1)
                            {
                                //获取工程师对应flow
                                string Flow = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("flow")).Text;
                                string FlowID = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("flowid")).Text;
                                string Big_FlowID = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("big_flowid")).Text;
                                string Step = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("step")).Text;
                                string StepID = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("stepid")).Text;
                                string require_date = ((TextBox)gv_bjjd.Rows[index + 1].FindControl("require_date")).Text;
                                //插入工程师行
                                for (int i = 0; i < str.Length; i++)
                                {

                                    if (str[i] != "")//判断i遍历
                                    {
                                        string empid = str[i];
                                        //string strSqlGCS_2 = "insert into Baojia_sign_flow select * from Baojia_sign_flow where ID='" + GCS_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";       
                                        string strSqlGCS_2 = "INSERT INTO Baojia_sign_flow(requestid, baojia_no, turns, Flow, FlowID, Big_FlowID, Step, StepID, empid, require_date)VALUES('" + requestid + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "','" + Flow + "','" + FlowID + "','" + Big_FlowID + "','" + Step + "','" + StepID + "','" + empid + "','" + require_date + "') ";
                                        DbHelperSQL.ExecuteSql(strSqlGCS_2);
                                    }

                                }
                                //删除原来的工程师id行
                                string strSqlGCS = "delete Baojia_sign_flow  where ID='" + GCS_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSqlGCS);


                                //DataTable dtgcs = BJ_CLASS.baojia_flow_select(Convert.ToInt32(Request["requestid"]), "机加","20");
                                //if(dtgcs.Rows.Count>0)
                                //{
                                //    for (int i = 0; i < dtgcs.Rows.Count; i++)
                                //    {
                                //        string GCSid = dtgcs.Rows[i]["ID"].ToString();
                                //        for (int j = 0; j < str.Length; j++)
                                //        {

                                //            if (str[j] != "")//判断i遍历
                                //            {
                                //                string strSqlGCS_2 = "update Baojia_sign_flow set empid='" + str[j] + "' where ID='" + GCSid + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
                                //                DbHelperSQL.ExecuteSql(strSqlGCS_2);

                                //            }

                                //        }
                                //    }

                                //}


                            }
                            else
                            {
                                string strSqlGCS = "update Baojia_sign_flow set empid='" + GCS + "' where ID='" + GCS_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSqlGCS);
                            }
                            //遍历选择主管
                            string ZG_2 = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("txt")).Text;//从文本框读取
                            string[] str_zg = ZG_2.Split(',');//调用Split方法，定义string[]类型的变量
                            if (str_zg.Length > 1)
                            {
                                //获取主管对应flow
                                string Flow = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("flow")).Text;
                                string FlowID = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("flowid")).Text;
                                string Big_FlowID = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("big_flowid")).Text;
                                string Step = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("step")).Text;
                                string StepID = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("stepid")).Text;
                                string require_date = ((TextBox)gv_bjjd.Rows[index + 2].FindControl("require_date")).Text;
                                //插入主管行
                                for (int i = 0; i < str_zg.Length; i++)
                                {
                                    string empid = str_zg[i];
                                    if (str_zg[i] != "")//判断i遍历
                                    {
                                        //string strSqlZG_2 = "insert into Baojia_sign_flow select * from Baojia_sign_flow where ID='" + ZG_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                        //DbHelperSQL.ExecuteSql(strSqlZG_2);
                                        string strSqlZG_2 = "INSERT INTO Baojia_sign_flow(requestid, baojia_no, turns, Flow, FlowID, Big_FlowID, Step, StepID, empid, require_date)VALUES('" + requestid + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "','" + Flow + "','" + FlowID + "','" + Big_FlowID + "','" + Step + "','" + StepID + "','" + empid + "','" + require_date + "') ";
                                        DbHelperSQL.ExecuteSql(strSqlZG_2);

                                    }

                                }
                                //删除原来的主管id行
                                string strSqlZG = "delete Baojia_sign_flow  where ID='" + ZG_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSqlZG);

                                //DataTable dtZG = BJ_CLASS.baojia_flow_select(Convert.ToInt32(Request["requestid"]), "机加", "30");
                                //if (dtZG.Rows.Count > 0)
                                //{
                                //    for (int i = 0; i < dtZG.Rows.Count; i++)
                                //    {
                                //        string ZGid = dtZG.Rows[i]["ID"].ToString();
                                //        for (int j = 0; j < str.Length; j++)
                                //        {

                                //            if (str[j] != "")//判断i遍历
                                //            {
                                //                string strSqlGCS_2 = "update Baojia_sign_flow set empid='" + str[j] + "' where ID='" + ZGid + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
                                //                DbHelperSQL.ExecuteSql(strSqlGCS_2);

                                //            }

                                //        }
                                //    }

                                //}
                            }
                            else
                            {
                                string strSqlZG = "update Baojia_sign_flow set empid='" + ZG + "' where ID='" + ZG_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSqlZG);
                            }


                        }
                        else
                        {
                            string strSqlGCS = "update Baojia_sign_flow set empid='" + GCS + "' where ID='" + GCS_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlGCS);

                            string strSqlZG = "update Baojia_sign_flow set empid='" + ZG + "' where ID='" + ZG_ID + "' and requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                            DbHelperSQL.ExecuteSql(strSqlZG);
                        }
                    }

                }

            }
            //如果销售工程师填写时
            if (((TextBox)gv_bjjd.Rows[index].FindControl("flow")).Text == "销售" && ((TextBox)gv_bjjd.Rows[index].FindControl("step")).Text == "销售工程师")
            {
                BTN_Sales_3_Click(null, null);
                //验证报价明细是否填写完成
                for (int i = 0; i < gv_bjmx.Rows.Count; i++)
                {
                    if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text == "")
                    {
                        ViewState["lv"] = "BJJD";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少填写一样价格')", true);
                        return;
                    }

                }


            }
            int update = -1;
            update = BJ_CLASS.Baojia_Sign_Submit(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(JL_ID_ZP), sign_desc);
            if (update > 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('确认成功！')", true);
                ((Button)gv_bjjd.Rows[index].FindControl("btncomit")).Visible = false;
                BJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]));
                ViewState["lv"] = "BJJD";
            }
        }
    }

    public void insert_Baojia_agreement_flow()
    {
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            System.Text.StringBuilder strsql = new StringBuilder();
            strsql.Append("insert into Baojia_agreement_flow(requestId,baojia_no,turns,description,ljh,customer_project,ship_from,ship_to,dingdian_date,pc_date,end_date,sdxx,lj_status,create_by_empid,quantity_year,pc_per_price,price_year,pc_mj_price,currency,exchange_rate)");
            strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
            strsql.Append(" '" + txt_baojia_no.Value + "',");
            strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text + "',");
            strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "',");
            strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
            strsql.Append(" '" + txt_update_user_name.Value + "',");

            string quantity_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
            strsql.Append(quantity_year == "" ? "null" : quantity_year + ",");

            //strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

            string txt_pc_per_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
            strsql.Append("," + txt_pc_per_price == "" ? "null" : txt_pc_per_price);

            string txt_price_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
            strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

            string txt_pc_mj_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
            strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

            strsql.Append(", '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "'");
            //string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
            //strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

            //string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
            //strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
            strsql.Append(")");

            DbHelperSQL.ExecuteSql(strsql.ToString());
        }
    }
    public void UPDATE_Baojia_agreement_flow()
    {
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {

            System.Text.StringBuilder strsql = new StringBuilder();

            strsql.Append("update Baojia_agreement_flow set ");
            //string strSql = "
            strsql.Append("ljh='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
            strsql.Append("customer_project='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Text + "',");
            strsql.Append("ship_from='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
            strsql.Append("ship_to='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
            //strsql.Append("dingdian_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
            string txt_dingdian_date = ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text;
            txt_dingdian_date = txt_dingdian_date == "" ? "null" : "'" + txt_dingdian_date + "'";
            strsql.Append("dingdian_date= " + txt_dingdian_date + ",");
            //strsql.Append("pc_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Text + "',");
            string txt_pc_date = ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Text;
            txt_pc_date = txt_pc_date == "" ? "null" : "'" + txt_pc_date + "'";
            strsql.Append("pc_date= " + txt_pc_date + ",");
            //strsql.Append("end_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text + "',");
            string txt_end_date = ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text;
            txt_end_date = txt_end_date == "" ? "null" : "'" + txt_end_date + "'";
            strsql.Append("end_date= " + txt_end_date + ",");
            strsql.Append("quantity_year='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "") + "',");
            strsql.Append("pc_per_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "") + "',");
            strsql.Append("price_year= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "") + "',");
            strsql.Append("pc_mj_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "") + "',");
            strsql.Append("currency= '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
            strsql.Append("lj_status= '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
            strsql.Append("exchange_rate= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "',");
            strsql.Append("Description= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text.Replace(",", "") + "'");
            //strsql.Append(",lj_status= '已定点合同跟踪'");
            strsql.Append(" where id= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("id")).Text + "' ");


            DbHelperSQL.ExecuteSql(strsql.ToString());



            //System.Text.StringBuilder strsql = new StringBuilder();
            //strsql.Append("insert into Baojia_agreement_flow(requestId,baojia_no,turns,description,ljh,ship_from,ship_to,dingdian_date,sdxx,lj_status,create_by_empid,quantity_year,pc_per_price,price_year,pc_mj_price,currency,exchange_rate)");
            //strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
            //strsql.Append(" '" + txt_baojia_no.Value + "',");
            //strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
            //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "',");
            //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
            //strsql.Append(" '" + txt_update_user_name.Value + "',");

            //string quantity_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
            //strsql.Append(quantity_year == "" ? "null" : quantity_year + ",");
            //string txt_pc_per_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
            //strsql.Append("," + txt_pc_per_price == "" ? "null" : txt_pc_per_price);
            //string txt_price_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
            //strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));
            //string txt_pc_mj_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
            //strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

            //strsql.Append(", '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
            //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "'");

            //strsql.Append(")");

            //DbHelperSQL.ExecuteSql(strsql.ToString());
        }
    }
    //两个合同状态的写法
    //public void UPDATE_Baojia_agreement_flow()
    //{
    //    for (int i = 0; i < gv_htgz.Rows.Count; i++)
    //    {

    //        System.Text.StringBuilder strsql = new StringBuilder();
    //        if(((TextBox)this.gv_htgz.Rows[i].FindControl("txtlj_status")).Text=="争取")
    //        {
    //            strsql.Append("update Baojia_agreement_flow set ");
    //            //string strSql = "
    //            strsql.Append("ljh='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
    //            strsql.Append("customer_project='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Text + "',");
    //            strsql.Append("ship_from='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
    //            strsql.Append("ship_to='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
    //            strsql.Append("dingdian_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
    //            strsql.Append("pc_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Text + "',");
    //            strsql.Append("end_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text + "',");
    //            strsql.Append("quantity_year='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "") + "',");
    //            strsql.Append("pc_per_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "") + "',");
    //            strsql.Append("price_year= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "") + "',");
    //            strsql.Append("pc_mj_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "") + "',");
    //            strsql.Append("currency= '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
    //            strsql.Append("exchange_rate= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "',");
    //            strsql.Append("Description= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text.Replace(",", "") + "'");
    //            strsql.Append(",lj_status= '已定点合同跟踪'");
    //            strsql.Append("where id= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("id")).Text + "' ");
    //        }
    //        else
    //        {

    //            strsql.Append("update Baojia_agreement_flow set ");
    //            //string strSql = "
    //            strsql.Append("ljh='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
    //            strsql.Append("customer_project='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Text + "',");
    //            strsql.Append("ship_from='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
    //            strsql.Append("ship_to='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
    //            strsql.Append("dingdian_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
    //            strsql.Append("pc_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("pc_date")).Text + "',");
    //            strsql.Append("end_date='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text + "',");
    //            strsql.Append("quantity_year='" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "") + "',");
    //            strsql.Append("pc_per_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "") + "',");
    //            strsql.Append("price_year= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "") + "',");
    //            strsql.Append("pc_mj_price= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "") + "',");
    //            strsql.Append("currency= '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
    //            strsql.Append("exchange_rate= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "',");
    //            strsql.Append("Description= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text.Replace(",", "") + "'");
    //            //strsql.Append(",lj_status= '已定点合同跟踪'");
    //            strsql.Append("where id= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("id")).Text + "' ");
    //        }

    //        DbHelperSQL.ExecuteSql(strsql.ToString());



    //        //System.Text.StringBuilder strsql = new StringBuilder();
    //        //strsql.Append("insert into Baojia_agreement_flow(requestId,baojia_no,turns,description,ljh,ship_from,ship_to,dingdian_date,sdxx,lj_status,create_by_empid,quantity_year,pc_per_price,price_year,pc_mj_price,currency,exchange_rate)");
    //        //strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
    //        //strsql.Append(" '" + txt_baojia_no.Value + "',");
    //        //strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "',");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text + "',");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text + "',");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text + "',");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
    //        //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "',");
    //        //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
    //        //strsql.Append(" '" + txt_update_user_name.Value + "',");

    //        //string quantity_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
    //        //strsql.Append(quantity_year == "" ? "null" : quantity_year + ",");
    //        //string txt_pc_per_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
    //        //strsql.Append("," + txt_pc_per_price == "" ? "null" : txt_pc_per_price);
    //        //string txt_price_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
    //        //strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));
    //        //string txt_pc_mj_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
    //        //strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

    //        //strsql.Append(", '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
    //        //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "'");

    //        //strsql.Append(")");

    //        //DbHelperSQL.ExecuteSql(strsql.ToString());
    //    }
    //}
    protected void BTN_Sales_2_Click(object sender, EventArgs e)
    {

        //验证报价明细是否填写完成
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            //if (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text == "已收到全部定点文件")
            //{
            //    ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text = "";
            //}
            //else
            //{
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text == "" || ((TextBox)this.gv_htgz.Rows[i].FindControl("create_by_empid")).Text == "")
            {
                ViewState["lv"] = "htgz";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请维护完整合同信息！如行数多余请删除行')", true);
                return;
            }

            //增加保存时防呆
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text == "" &&
       (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "合同完成停止跟踪" ||
       ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "定点取消合同跟踪" ||
       ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "放弃跟踪"
       ))
            {
                ((TextBox)gv_htgz.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写该零件对应的结束日期！')", true);
                ViewState["lv"] = "htgz";
                return;
                //ViewState["lv"] = "htgz";
            }
            //if ((((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "已定点合同跟踪"))
            //{

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('（已定点合同跟踪）状态不需要维护，自动更新状态！')", true);
            //    ViewState["lv"] = "htgz";
            //    return;
            //    //ViewState["lv"] = "htgz";
            //}

            if (((TextBox)this.gv_htgz.Rows[i].FindControl("end_date")).Text != "" &&
              (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "争取" ||
              ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "争取(已收到样件订单)" ||
              ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "已定点合同跟踪"
              ))
            {
                ((TextBox)gv_htgz.Rows[i].FindControl("end_date")).BackColor = System.Drawing.Color.Red;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('（争取&已定点合同跟踪）这两种状态不可以维护结束日期！')", true);
                ViewState["lv"] = "htgz";
                return;
                //ViewState["lv"] = "htgz";
            }

            if (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text == "放弃跟踪")
            {

            }
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text == "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text = "0";
            }
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text == "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text = "1";
            }
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text == "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text = "0";
            }
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text == "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text = "0";
            }
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text == "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text = "0";
            }

            //}

        }

        //保存合同跟踪
        int update = -1;
        update = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_agreement_flow");
        if (update >= 0)
        {
            if (gv_htgz.Rows.Count <= 0)
            {
                string strSql = "update Baojia_agreement_flow set dingdian_date=NULL where baojia_no='" + txt_baojia_no.Value + "'";
                DbHelperSQL.ExecuteSql(strSql);
            }
            //插入合同明细
            //insert_Baojia_agreement_flow();
            //修改合同明细
            UPDATE_Baojia_agreement_flow();
            ////获取定点日期
            //string strsql_update_dingdian = " update Baojia_mst set hetong_status='合同跟踪',hetong_complet_date=NULL,dingdian_date= '" + ((TextBox)this.gv_htgz.Rows[0].FindControl("dingdian_date")).Text + "' where requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
            //DbHelperSQL.ExecuteSql(strsql_update_dingdian);
            //插入销售保存合同信息记录
            string date = System.DateTime.Now.ToString();
            string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(合同维护)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','保存合同记录')";
            DbHelperSQL.ExecuteSql(strsql_xs);
            for (int i = 0; i < gv_htgz.Rows.Count; i++)
            {
                //string strsql = " insert into Baojia_agreement_flow(requestid,baojia_no,turns,description,ljh,dingdian_date,sdxx,create_by_empid)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "', '" + txt_baojia_no.Value + "', '" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "','" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text + "', '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "', '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "', '" + txt_update_user_name.Value + "')";
                //DbHelperSQL.ExecuteSql(strsql);

                //System.Text.StringBuilder strsql = new StringBuilder();
                //strsql.Append("insert into Baojia_agreement_flow(requestId,baojia_no,turns,description,ljh,ship_to,dingdian_date,sdxx,lj_status,create_by_empid,quantity_year,pc_per_price,price_year,pc_mj_price,currency,exchange_rate)");
                //strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
                //strsql.Append(" '" + txt_baojia_no.Value + "',");
                //strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
                //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("Description")).Text + "',");
                //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text + "',");
                //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ship_to")).Text + "',");
                //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "',");
                //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text + "',");
                //strsql.Append(" '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_lj_status")).Text + "',");
                //strsql.Append(" '" + txt_update_user_name.Value + "',");

                //string quantity_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
                //strsql.Append(quantity_year == "" ? "null" : quantity_year +",");

                ////strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
                ////strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

                //string txt_pc_per_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
                //strsql.Append("," + txt_pc_per_price == "" ? "null" : txt_pc_per_price);

                //string txt_price_year = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
                //strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

                //string txt_pc_mj_price = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
                //strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

                //strsql.Append(", '" + ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_currency")).Text + "',");
                //strsql.Append(" '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text + "'");
                ////string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
                ////strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

                ////string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
                ////strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
                //strsql.Append(")");

                //DbHelperSQL.ExecuteSql(strsql.ToString());





                //获取合同完成日期
                //if (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_sdxx")).Text == "已收到全部定点文件")
                //{
                //    string strsql_update = " update Baojia_mst set hetong_status='合同已完成',hetong_complet_date= '" + ((TextBox)this.gv_htgz.Rows[i].FindControl("dingdian_date")).Text + "' where requestid='"+ Convert.ToInt32(Request["requestid"]) + "'";
                //    DbHelperSQL.ExecuteSql(strsql_update);
                //}
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合同信息维护成功！')", true);
            BTN_Sales_2.Enabled = false;
            //刷新以下零件状态
            //ddl_ljztgz();
            DataTable dt_ljztgz = BJ_CLASS.Getgv(txt_baojia_no.Value, "ljztgz");
            gv_ljztgz.DataSource = dt_ljztgz;
            gv_ljztgz.DataBind();
            ddl_ljztgz();
            //Response.Write("<script language=javascript>window.location.href=document.URL;</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('保存不成功，请检查或联系管理员！')", true);

        }
        ViewState["lv"] = "htgz";
    }
    public void insert_Baojia_dtl()
    {
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            System.Text.StringBuilder strsql = new StringBuilder();
            strsql.Append("insert into Baojia_dtl(requestId,baojia_no,turns,ljh,xz_ljh,old_ljh,lj_name,customer_project,ship_from,ship_to,back_up,quantity_year,material,pc_date,pc_per_price,price_year,pc_mj_price,yj_per_price,yj_mj_price)");
            strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
            strsql.Append(" '" + txt_baojia_no.Value + "',");
            strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
            //不变更零件号的，存现零件号
            if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
            {
                strsql.Append(" '" + ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() + "',");
            }
            else
            {
                //strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
                strsql.Append(" '',");
            }
            //不变更零件号的，存现零件号
            if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text.Trim() != "")
            {
                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
            }
            else
            {
                strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim().Replace("@[/n/r]", "") + "',");
            }
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text.Trim().Replace("@[/n/r]", "") + "',");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_customer_project")).Text.Trim().Replace("@[/n/r]", "") + "',");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Text.Trim().Replace("@[/n/r]", "") + "',");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Text.Trim().Replace("@[/n/r]", "") + "',");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_back_up")).Text.Trim().Replace("@[/n/r]", "") + "',");
            string quantity_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");
            strsql.Append(quantity_year == "" ? "null" : quantity_year);

            strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
            strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

            string txt_pc_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
            strsql.Append(txt_pc_per_price == "" ? "null" : txt_pc_per_price);

            string txt_price_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
            strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

            string txt_pc_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
            strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

            string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
            strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

            string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
            strsql.Append("," + (txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
            strsql.Append(")");
            DbHelperSQL.ExecuteSql(strsql.ToString());
        }

    }
    protected void BTN_Sales_3_Click(object sender, EventArgs e)
    {

        //验证报价明细是否填写完成
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少填写一样价格')", true);
                return;
            }

        }

        //报价明细保存
        int update = -1;
        update = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_dtl");
        if (update > 0)
        {
            //保存报价明细
            //for (int i = 0; i < gv_bjmx.Rows.Count; i++)
            //{
            //    System.Text.StringBuilder strsql = new StringBuilder();
            //    strsql.Append("insert into Baojia_dtl(requestId,baojia_no,turns,ljh,lj_name,ship_to,quantity_year,material,pc_date,pc_per_price,price_year,pc_mj_price,yj_per_price,yj_mj_price)");
            //    strsql.Append("VALUES  ('" + Convert.ToInt32(Request["requestid"]) + "', ");
            //    strsql.Append(" '" + txt_baojia_no.Value + "',");
            //    strsql.Append(" '" + DDL_turns.SelectedValue + "', ");
            //    strsql.Append( " '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text.Trim() + "',");
            //    strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_lj_name")).Text.Trim() + "',");
            //    strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Text.Trim() + "',");
            //    string quantity_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", "");                
            //    strsql.Append( quantity_year==""? "null": quantity_year);

            //    strsql.Append(", '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_material")).Text + "',");
            //    strsql.Append(" '" + ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_date")).Text + "',");

            //    string txt_pc_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(",", "");
            //    strsql.Append(txt_pc_per_price == "" ? "null" : txt_pc_per_price);

            //    string txt_price_year = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text.Replace(",", "");
            //    strsql.Append("," + (txt_price_year == "" ? "null" : txt_price_year));

            //    string txt_pc_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text.Replace(",", "");
            //    strsql.Append("," + (txt_pc_mj_price == "" ? "null" : txt_pc_mj_price));

            //    string txt_yj_per_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text.Replace(",", "");
            //    strsql.Append("," + (txt_yj_per_price == "" ? "null" : txt_yj_per_price));

            //    string txt_yj_mj_price = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text.Replace(",", "");
            //    strsql.Append(","+(txt_yj_mj_price == "" ? "null" : txt_yj_mj_price));
            //    strsql.Append(")");
            //    DbHelperSQL.ExecuteSql(strsql.ToString());
            //}

            //插入零件明细
            insert_Baojia_dtl();
            BJ_CLASS.Baojia_Dtl2Agreement(Convert.ToInt32(Request["requestid"]));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件信息维护成功！')", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('保存不成功，请检查或联系管理员！')", true);

        }
    }

    protected void BTN_Sales_tingzhi_Click(object sender, EventArgs e)
    {
        if (DDL_is_stop.SelectedValue == "否")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('需要停止报价请选择是！')", true);
        }
        else
        {
            string stop_date = System.DateTime.Now.ToString();
            string strsql_update = " update Baojia_mst set hetong_status='停止跟踪',stop_date= '" + stop_date + "',stop_empid='" + txt_update_user.Value + "',stop_name='" + txt_update_user_name.Value + "',is_stop='" + DDL_is_stop.SelectedValue + "' where requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
            DbHelperSQL.ExecuteSql(strsql_update);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('停止成功！')", true);
        }
    }

    protected void BTN_Sales_yizhuan_Click(object sender, EventArgs e)
    {
        if (DDL_liuchengyizhuan.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('需要移转请选择是！')", true);
        }
        else
        {
            int update = -1;
            update = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_sign_flow");
            if (update > 0)
            {
                string date = System.DateTime.Now.ToString();
                string strsql_update = " update Baojia_mst set yizhuan_date= '" + date + "',yizhuan_empid='" + txt_update_user.Value + "',yizhuan_name='" + txt_update_user_name.Value + "',is_yizhuan='1' where requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
                DbHelperSQL.ExecuteSql(strsql_update);


                //插入申请人log

                string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(修改)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','')";
                DbHelperSQL.ExecuteSql(strsql_xs);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('移转成功！')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('移转不成功。请联系管理员！')", true);
            }


        }
    }
    #region BindHistory
    //public void bindMst(string mstid, DataList gvMst)
    //{
    //    StringBuilder sql = new StringBuilder();

    //    sql.Append(" declare @turns int;  ");
    //    sql.Append(" select @turns = isnull(max(turns), 0) from[dbo].[Baojia_dtl] where baojia_no = '" + mstid + "';  ");
    //    sql.Append("    select * from [Baojia_mst] where baojia_no='" + mstid + "' and baojia_status='已报出'");//turns<@turns 不获取最后一轮
    //    if (Request["update"] == null)
    //    {
    //        sql.Append("  and requestid not in (select max(requestid) from [Baojia_mst] where baojia_no='" + mstid + "') ");
    //    }
    //    sql.Append("   order by turns ");
    //    // sql.Append( "select  m.requestid,m.baojia_no,m.turns		,m.customer_name		,m.end_customer_name		,m.customer_project		,m.create_date		,d.ljh		,d.lj_name		,d.quantity_year		,d.material		,d.pc_date		,d.pc_per_price		,d.pc_mj_price		,d.price_year		,m.project_size		,m.project_level		,m.hetong_status		,m.baojia_status  	,m.dingdian_date		,m.hetong_complet_date	from [dbo].[Baojia_mst]   m left join[dbo].[Baojia_dtl]  d on m.baojia_no=d.baojia_no and m.turns=d.turns where m.baojia_no= '"+ mstid + "'");

    //    DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
    //    if (dt.Rows.Count == 0) { lblHisMsg.Text = "暂无历史报价记录"; } else { lblHisMsg.Text = ""; }//告知一下历史记录
    //    gvMst.DataSource = dt;
    //    gvMst.DataBind();
    //}
    public void bindMst(string mstid, DataList gvMst)
    {
        StringBuilder sql = new StringBuilder();

        sql.Append(" declare @turns int;  ");
        sql.Append(" select @turns = isnull(max(turns), 0) from[dbo].[Baojia_dtl] where baojia_no = '" + mstid + "';  ");
        sql.Append("    select * from [V_Baojia_mst_desc] where baojia_no='" + mstid + "' and baojia_status='已报出'");//turns<@turns 不获取最后一轮
        if (Request["update"] == null)
        {
            sql.Append("  and requestid not in (select max(requestid) from [Baojia_mst] where baojia_no='" + mstid + "') ");
        }
        sql.Append("   order by turns ");
        // sql.Append( "select  m.requestid,m.baojia_no,m.turns		,m.customer_name		,m.end_customer_name		,m.customer_project		,m.create_date		,d.ljh		,d.lj_name		,d.quantity_year		,d.material		,d.pc_date		,d.pc_per_price		,d.pc_mj_price		,d.price_year		,m.project_size		,m.project_level		,m.hetong_status		,m.baojia_status  	,m.dingdian_date		,m.hetong_complet_date	from [dbo].[Baojia_mst]   m left join[dbo].[Baojia_dtl]  d on m.baojia_no=d.baojia_no and m.turns=d.turns where m.baojia_no= '"+ mstid + "'");

        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        if (dt.Rows.Count == 0) { lblHisMsg.Text = "暂无历史报价记录"; } else { lblHisMsg.Text = ""; }//告知一下历史记录
        gvMst.DataSource = dt;
        gvMst.DataBind();
    }
    public void bindDetail(string requestid, GridView gvdetail)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("    select  ljh as 零件号, lj_name as 零件名称,customer_project as 顾客项目,ship_from as ship_from, Ship_to as Ship_to, CAST([quantity_year] AS int) as 年用量, material as 材料, CONVERT(varchar(100), pc_date, 23) as 批产日期,");
        sql.Append("    CAST([pc_per_price] AS decimal(18, 2)) as 批产单价, CAST([price_year] AS int) as 年销售额, CAST([pc_mj_price] AS decimal(18, 2)) as 批产模具价格, CAST([yj_per_price] AS decimal(18, 2)) as 样件单价, CAST([yj_mj_price] AS decimal(18, 2)) as 样件模具价格          ");
        sql.Append("    from [Baojia_dtl] where requestid='" + requestid + "'");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gvdetail.DataSource = dt;
        gvdetail.DataBind();
    }

    protected void DataMst_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvdetail = (GridView)e.Item.FindControl("GridViewD");

            DataRowView item = (DataRowView)e.Item.DataItem;
            bindDetail(item["requestid"].ToString(), gvdetail);
        }
    }
    #endregion
    #region Bindzhuangtai
    public void bindMst_log(string mstid, DataList gvMst)
    {
        StringBuilder sql = new StringBuilder();

        sql.Append(" declare @turns int;  ");
        sql.Append(" select @turns = isnull(max(turns), 0) from[dbo].[Baojia_dtl] where baojia_no = '" + mstid + "';  ");
        sql.Append("select * from [Baojia_mst] where baojia_no='" + mstid + "' order by turns /*desc*/");

        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        if (dt.Rows.Count == 0) { lblHisMsg_log.Text = "暂无历史签核记录"; } else { lblHisMsg_log.Text = ""; }//告知一下历史记录
        gvMst.DataSource = dt;
        gvMst.DataBind();

    }
    public void bindDetail_log(string requestid, GridView gvdetail)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("   select a.sign_date ,b.lastname,a.flow,a.step,a.require_date,a.sign_desc,a.Operation_time from Baojia_sign_flow a join HRM_EMP_MES b on a.empid = b.workcode ");
        sql.Append("    where requestid = '" + requestid + "' and sign_date is not null order by sign_date ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gvdetail.DataSource = dt;
        gvdetail.DataBind();
        gvdetail.PageSize = 100;
    }

    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("   select a.sign_date ,b.lastname,a.flow,a.step,a.require_date,a.receive_date,a.sign_desc,a.Operation_time from Baojia_sign_flow a join HRM_EMP_MES b on a.empid = b.workcode ");
        sql.Append("    where requestid = '" + requestid + "' and sign_date is not null order by sign_date ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }
    protected void GridViewD_log_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string t1 = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("yyyy-MM-dd");
            string t2 = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(t1), Convert.ToDateTime(t2)) > 0) //早于要求时间
            {
                e.Row.Cells[0].Style.Add("color", "red");
            }
            else
            {

            }
        }
    }


    protected void DataMst_log_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvdetail = (GridView)e.Item.FindControl("GridViewD_log");

            DataRowView item = (DataRowView)e.Item.DataItem;
            bindDetail_log(item["requestid"].ToString(), gvdetail);
            bindrz_log(Request["requestid"], gv_rz1);
            bindDetail_Hetong(item["requestid"].ToString());
            bind_gv_ljzt(txt_baojia_no.Value);
        }
    }
    /// <summary>
    /// 给合同追踪的明细绑定 数据
    /// </summary>
    /// <param name="requestid"></param>
    /// <param name="gvdetail"></param>
    public void bindDetail_Hetong(string requestid)
    {
        StringBuilder sql = new StringBuilder();//

        sql.Append("     select aa.baojia_no,aa.ljh,aa.sdxx,aa.create_by_empid,aa.dingdian_date");
        sql.Append("     from (select top 100 1 as id, aa.baojia_no, aa.ljh, sdxx, aa.create_by_empid, min(aa.dingdian_date) as dingdian_date ");
        sql.Append("  from(select * from[dbo].[Baojia_agreement_flow]) aa where aa.requestid =  '" + requestid + "' ");
        sql.Append("    group by aa.baojia_no, aa.ljh, sdxx, aa.create_by_empid  union ");
        sql.Append("    select  top 100 2 as id, baojia_no, '' as ljh, '合同已完成' as sdxx, sales_name, hetong_complet_date as dingdian_date  from baojia_mst ");
        sql.Append("   where requestid =  '" + requestid + "'  and hetong_status='合同已完成' ");
        sql.Append("   order by dingdian_date,id )aa  where 1=1 ");

        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        Gridview_hetong_log.DataSource = dt;
        Gridview_hetong_log.DataBind();
    }
    //零件状态

    public void bind_gv_ljzt(string txt_baojia_no)
    {
        DataSet ds = DbHelperSQL.Query("exec [dbo].[Baojia_Get_Lingjian_detail]  '" + txt_baojia_no + "' ");

        DataTable dt = ds.Tables[0];
        gv_ljzt.DataSource = dt;
        gv_ljzt.DataBind();

    }



    #endregion

    private decimal num_quantity_year = 0;
    private decimal num_price_year = 0;
    protected void gv_bjmx_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
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
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
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

                }
            }
            TextBox Lab_price_year = e.Row.FindControl("Lab_price_year") as TextBox;
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
                }
            }

        }
    }

    private decimal num_quantity_year_ht = 0;
    private decimal num_price_year_ht = 0;
    private decimal num_pc_mj_price_ht = 0;

    protected void gv_htgz_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            if (dr.Row["quantity_year"].ToString() != "")
            {

                num_quantity_year_ht += Convert.ToDecimal(dr.Row["quantity_year"].ToString().Replace(",", ""));

            }
            else
            {
                num_quantity_year_ht = 0;
            }
            if (dr.Row["price_year"].ToString() != "")
            {
                num_price_year_ht += Convert.ToDecimal(dr.Row["price_year"].ToString().Replace(",", ""));
            }
            else
            {
                num_price_year_ht = 0;

            }
            if (dr.Row["pc_mj_price"].ToString() != "")
            {
                decimal yb = Convert.ToDecimal(dr.Row["pc_mj_price"].ToString().Replace(",", ""));
                decimal hl = Convert.ToDecimal(dr.Row["exchange_rate"].ToString().Replace(",", ""));
                num_pc_mj_price_ht += Convert.ToDecimal(yb * hl);
            }
            else
            {
                num_pc_mj_price_ht = 0;

            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox Lab_quantity_year = e.Row.FindControl("Lab_quantity_year") as TextBox;
            if (Lab_quantity_year != null)
            {
                if (num_quantity_year_ht == 0)
                {
                    Lab_quantity_year.Visible = false;
                }
                else
                {
                    //Lab_quantity_year.Text += num_quantity_year.ToString();//"计算的总数
                    Lab_quantity_year.Text += String.Format("{0:N0}", num_quantity_year_ht);

                }
            }
            TextBox Lab_price_year = e.Row.FindControl("Lab_price_year") as TextBox;
            if (Lab_price_year != null)
            {
                if (num_price_year_ht == 0)
                {
                    Lab_price_year.Visible = false;
                }
                else
                {
                    //Lab_price_year.Text += num_price_year.ToString();//"计算的总数
                    Lab_price_year.Text += String.Format("{0:N0}", num_price_year_ht);
                }
            }
            TextBox Lab_pc_mj_price = e.Row.FindControl("Lab_pc_mj_price") as TextBox;
            if (Lab_pc_mj_price != null)
            {
                if (num_pc_mj_price_ht == 0)
                {
                    Lab_pc_mj_price.Visible = false;
                }
                else
                {
                    //Lab_price_year.Text += num_price_year.ToString();//"计算的总数
                    Lab_pc_mj_price.Text += String.Format("{0:N0}", num_pc_mj_price_ht);
                }
            }
        }
    }



    private int num_quantity_year_ls = 0;
    private int num_price_year_ls = 0;
    protected void GridViewD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;

            if (dr.Row["年用量"].ToString() != "")
            {

                num_quantity_year_ls += Convert.ToInt32(dr["年用量"].ToString());
            }
            else
            {
                num_quantity_year_ls = 0;
            }
            if (dr.Row["年销售额"].ToString() != "")
            {
                num_price_year_ls += Convert.ToInt32(dr["年销售额"].ToString());
            }
            else
            {
                if (num_price_year_ls > 0)
                {
                    num_price_year_ls += 0;
                }
                else
                {
                    num_price_year_ls = 0;
                }
            }


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计:";
            e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            if (num_quantity_year_ls == 0)
            {

            }
            else
            {
                e.Row.Cells[5].Text = String.Format("{0:N0}", num_quantity_year_ls);
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
            if (num_price_year_ls == 0)
            {

            }
            else
            {
                e.Row.Cells[9].Text = String.Format("{0:N0}", num_price_year_ls);
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            }

            num_quantity_year_ls = 0;
            num_price_year_ls = 0;


        }
    }

    protected void txt_pc_per_price_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text != "")
            {
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先填写零件号！')", true);
            }
            else
            {
                if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text != "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text != "")
                {
                    decimal dj = Convert.ToDecimal(((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text);
                    int sl = Convert.ToInt32(((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", ""));
                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text = Convert.ToInt32(dj * sl).ToString();


                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text.Replace(" ", "");
                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(" ", "");
                }
                else if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text != "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text == "")
                {
                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text = "";
                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_price_year")).Text = "";
                }
                else if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_quantity_year")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text != "")
                {
                    ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先填写年用量！')", true);

                }
            }

        }
        ViewState["lv"] = "BJJD";
    }
    protected void txt_pc_per_price_TextChanged1(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            if (((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text == "" && ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text != "")
            {
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先填写零件号！')", true);
            }
            else
            {
                if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text != "" && ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text != "" && ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text != "")
                {
                    decimal dj = Convert.ToDecimal(((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text);
                    int sl = Convert.ToInt32(((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(",", ""));
                    decimal hl = Convert.ToDecimal(((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text);
                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text = Convert.ToInt32(dj * sl * hl).ToString();


                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text.Replace(" ", "");
                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text = ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text.Replace(" ", "");
                }
                else if (((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text != "" && ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text == "")
                {
                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text = "";
                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_price_year")).Text = "";
                }
                else if ((((TextBox)this.gv_htgz.Rows[i].FindControl("txt_quantity_year")).Text == "" || ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_exchange_rate")).Text == "") && ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text != "")
                {
                    ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_pc_per_price")).Text = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先填写年用量和汇率！')", true);
                    ViewState["lv"] = "htgz";
                }
            }

        }
        ViewState["lv"] = "htgz";
    }


    //protected void txt_baojia_file_path_TextChanged(object sender, EventArgs e)
    //{
    //    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
    //    //info.WorkingDirectory = Application.StartupPath;
    //    info.FileName = @"\\172.16.5.50\销售部\02 报价产品和开发\2017报价产品和开发\2017-038 ML3E 6019 AA FA NR3E 6019 AA 5.0L front cover";
    //    info.Arguments = "";
    //    try
    //    {
    //        System.Diagnostics.Process.Start(info);
    //    }
    //    catch (System.ComponentModel.Win32Exception we)
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", we.Message, true);
    //        return;
    //    }
    //}

    protected void BTN_Sales_sub_update_Click(object sender, EventArgs e)
    {
        //销售在报出前任何阶段都可以修改

        DataTable dt = BJ_CLASS.BJ_Query_PRO(Convert.ToInt32(Request["requestid"]));
        string project_size = dt.Rows[0]["project_size"].ToString();
        int update = -1;
        if (DDL_project_size.SelectedValue == project_size)
        {
            update = BJ_CLASS.BTN_Sales_sub(Convert.ToInt32(Request["requestid"]), txt_baojia_no.Value, Convert.ToInt32(DDL_turns.SelectedValue), txt_sales_empid.Value, txt_sales_name.Value, txt_sales_ad.Value, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, DDL_project_size.SelectedValue, DDL_project_level.SelectedValue, DDL_is_stop.SelectedValue, txt_create_by_empid.Value, txt_create_by_name.Value, txt_create_by_ad.Value, txt_create_by_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_baojia_file_path.Text, txt_baojia_desc.Value, DDL_domain.SelectedValue, txt_baojia_start_date.Value, DDL_sfxy_bjfx.SelectedValue, txt_create_date.Value, DDL_wl_tk.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, DDL_sfxj_cg.SelectedValue,txt_cusRequestDate.Value,  BTN_Sales_sub.Text);
            if (update > 0)
            {
                //插入申请人log
                string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(修改)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','" + txt_content.Value + "')";
                DbHelperSQL.ExecuteSql(strsql_xs);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
                BTN_Sales_sub_update.Enabled = false;
            }
        }
        else
        {
            //验证签核中压铸经理和工程经理是否填写
            string empid_jj = "";
            string empid_yz = "";
            string empid_xszg = "";
            string empid_xsjl = "";
            for (int i = 0; i < gv_bjjd.Rows.Count; i++)
            {

                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "(指派)机加经理" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text == "10")
                {

                    empid_jj = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "(指派)压铸经理" && ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text == "10")
                {

                    empid_yz = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "销售主管")
                {

                    empid_xszg = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
                if (((TextBox)this.gv_bjjd.Rows[i].FindControl("step")).Text == "销售经理")
                {

                    empid_xsjl = ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text;
                }
            }
            //机加和压铸的至少有一个部门需要会签
            if (empid_jj == "" && empid_yz == "" && DDL_jijia_tk.SelectedValue == "需要" && DDL_yz_tk.SelectedValue == "需要")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少维护一个压铸经理或机加经理')", true);
                ViewState["lv"] = "BJJD";
                return;

            }
            //销售主管和销售经理必须选择
            if (empid_xszg == "" || empid_xsjl == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售主管和销售经理不能为空')", true);
                ViewState["lv"] = "BJJD";
                return;
            }
            update = BJ_CLASS.BTN_Sales_sub(Convert.ToInt32(Request["requestid"]), txt_baojia_no.Value, Convert.ToInt32(DDL_turns.SelectedValue), txt_sales_empid.Value, txt_sales_name.Value, txt_sales_ad.Value, DDL_customer_name.SelectedValue, DDL_end_customer_name.SelectedValue, txt_customer_project.Value, DDL_project_size.SelectedValue, DDL_project_level.SelectedValue, DDL_is_stop.SelectedValue, txt_create_by_empid.Value, txt_create_by_name.Value, txt_create_by_ad.Value, txt_create_by_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_baojia_file_path.Text, txt_baojia_desc.Value, DDL_domain.SelectedValue, txt_baojia_start_date.Value, DDL_sfxy_bjfx.SelectedValue, txt_create_date.Value, DDL_wl_tk.SelectedValue, DDL_bz_tk.SelectedValue, DDL_jijia_tk.SelectedValue, DDL_yz_tk.SelectedValue, DDL_sfxj_cg.SelectedValue,txt_cusRequestDate.Value, BTN_Sales_sub.Text);

            if (update > 0)
            {

                int update_Baojia_sign_flow = -1;
                update_Baojia_sign_flow = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_sign_flow");
                if (update_Baojia_sign_flow > 0)
                {

                    //保存报价进度
                    for (int i = 0; i < gv_bjjd.Rows.Count; i++)
                    {
                        string strsql = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flow")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("flowid")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("big_flowid")).Text + "','" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("Step")).Text + "', '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("stepid")).Text + "', '" + ((DropDownList)this.gv_bjjd.Rows[i].FindControl("ddl_empid")).Text + "', iif('" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Text + "'='',null,'" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("receive_date")).Text + "'), '" + ((TextBox)this.gv_bjjd.Rows[i].FindControl("require_date")).Text + "')";
                        DbHelperSQL.ExecuteSql(strsql);
                    }

                    //删除未选择列表的人员
                    int update_delete = -1;
                    update_delete = BJ_CLASS.Baojia_YZ_JJ_DELETE(Convert.ToInt32(Request["requestid"]));

                    //插入申请人log
                    string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                    string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(修改)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','" + txt_content.Value + "')";
                    DbHelperSQL.ExecuteSql(strsql_xs);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
                    BTN_Sales_sub_update.Enabled = false;
                    BJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]));
                    //BJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_baojia_no.Value);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改！')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再修改！')", true);
            }
        }




        ViewState["lv"] = "BJJD";
    }
    protected void DDL_sfxy_bjfx_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDL_project_size.SelectedValue = "";
        if (DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析")
        {
            Label3.Text = "需要会签各部门审批";
        }
        else if (DDL_sfxy_bjfx.SelectedValue == "仅需价格核实")
        {
            Label3.Text = "需要内部审批";
        }
        else
        {
            Label3.Text = "无需审批,只需存档";
        }
        //DataTable dtbaojia_no = BJ_CLASS.BJ_baojia_no(System.DateTime.Now.ToString("yyyy"));
        //if (txt_baojia_no.Value == "")
        //{
        //    if (dtbaojia_no.Rows.Count > 0)
        //    {
        //        string maxno = dtbaojia_no.Rows[0]["xh"].ToString();
        //        int max_no = Convert.ToInt32(maxno) + 1;
        //        txt_baojia_no.Value = System.DateTime.Now.ToString("yyyy") + "-" + max_no.ToString().PadLeft(4, '0');
        //    }
        //    else
        //    {
        //        txt_baojia_no.Value = System.DateTime.Now.ToString("yyyy") + "-0001";
        //    }
        //}

        //if (DDL_sfxy_bjfx.SelectedValue == "需要详细价格分析")
        //{

        //    txt_baojia_no.Disabled = true;
        //    DDL_turns.Enabled = false;
        //    txt_baojia_start_date.Disabled = true;
        //    txt_create_date.Disabled = true;
        //    if(Request["update"] != null)
        //    {

        //    }
        //    else
        //    {
        //        DDL_turns.SelectedValue = "1";
        //        txt_content.Value = "首次报价";
        //    }

        //    gv_bjmx_csh();
        //}
        //else
        //{

        //    //txt_baojia_no.Disabled = false;
        //    //DDL_turns.Enabled = true;
        //    //txt_baojia_start_date.Disabled = false;
        //    //txt_create_date.Disabled = false;
        //    txt_baojia_no.Disabled = true;
        //    DDL_turns.Enabled = false;
        //    txt_baojia_start_date.Disabled = true;
        //    txt_create_date.Disabled = true;
        //    if (Request["update"] != null)
        //    {
        //        txt_baojia_no.Disabled = true;
        //        DDL_turns.Enabled = false;
        //        txt_baojia_start_date.Disabled = true;
        //        txt_create_date.Disabled = true;
        //    }
        //    else
        //    {
        //        DDL_turns.SelectedValue = "1";
        //    }

        //    gv_bjmx_csh_bsp();
        //    //DDL_project_size.SelectedValue = "";
        //    gv_bjjd.DataSource = null;
        //    gv_bjjd.DataBind();
        //    //foreach (GridViewRow row in gv_bjjd.Rows)
        //    //{
        //    //    row.Cells.Clear();

        //    //}
        //}
    }



    protected void Link_baojia_file_path_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert(" + txt_baojia_file_path.Text + ")", true);
        //string path = txt_baojia_file_path.Text.Replace(@"\\", @"\"); ;
        //string v_OpenFolderPath = @path;
        string path = txt_baojia_file_path.Text;
        string v_OpenFolderPath = @path;

        System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);

    }

    protected void BTN_Sales_4_Click(object sender, EventArgs e)
    {
        if (txt_hetong_complet_date_qr.Value != "")
        {
            if (gv_htgz.Rows.Count > 0)
            {
                ////获取合同完成日期
                //string strsql_update = " update Baojia_mst set hetong_status='合同已完成',hetong_complet_date= '" + txt_hetong_complet_date_qr.Value + "' where requestid='" + Convert.ToInt32(Request["requestid"]) + "'";
                //DbHelperSQL.ExecuteSql(strsql_update);

                //插入申请人log
                string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(合同维护)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','保存合同记录')";
                DbHelperSQL.ExecuteSql(strsql_xs);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜你，已经维护成功！')", true);
                BTN_Sales_4.Enabled = false;

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少维护一笔定点！')", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写合同完成日期！')", true);

        }
        ViewState["lv"] = "htgz";
    }



    protected void ddl_ljh_SelectedIndexChanged(object sender, EventArgs e)
    {
        string xz_ljh = "";

        for (int i = 0; i < gv_htgz.Rows.Count; i++)
        {
            if (((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text == "")
            {

            }
            else
            {
                xz_ljh = ((DropDownList)this.gv_htgz.Rows[i].FindControl("ddl_ljh")).Text;
                DataTable dtljh = BJ_CLASS.BJ_ljh(txt_baojia_no.Value, xz_ljh);
                ((TextBox)this.gv_htgz.Rows[i].FindControl("ljh")).Text = dtljh.Rows[0]["ljh"].ToString();
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txt_customer_project")).Text = dtljh.Rows[0]["customer_project"].ToString();
                ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_from")).Text = dtljh.Rows[0]["ship_from"].ToString();
                ((TextBox)this.gv_htgz.Rows[i].FindControl("ship_to")).Text = dtljh.Rows[0]["ship_to"].ToString();
                ((TextBox)this.gv_htgz.Rows[i].FindControl("txtlj_status")).Text = dtljh.Rows[0]["lj_status"].ToString();
                ((TextBox)this.gv_htgz.Rows[i].FindControl("id")).Text = dtljh.Rows[0]["id"].ToString();
            }
        }
        ViewState["lv"] = "htgz";
    }

    protected void ddl_ljh_dt_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ljh = "";

        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            if (((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text == "")
            {

            }
            else
            {
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text = ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text;
                ljh = ((DropDownList)this.gv_bjmx.Rows[i].FindControl("ddl_ljh_dt")).Text;
                DataTable dtljh = BJ_CLASS.BJ_ljh_old_LJH(txt_baojia_no.Value, ljh);
                ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_old_ljh")).Text = dtljh.Rows[0]["old_ljh"].ToString();

            }
        }
        ViewState["lv"] = "htgz";
    }

    public void txt_ljh_Text_change()
    {
        if (Request["update"] != null)
        {
            string ljh = "";
            for (int i = 0; i < gv_bjmx.Rows.Count; i++)
            {
                if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text == "")
                {

                }
                else
                {
                    ljh = ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ljh")).Text;
                    DataTable dtljh = BJ_CLASS.baojia_ljh_ship(txt_baojia_no.Value, ljh);
                    if (dtljh.Rows.Count > 0)
                    {
                        ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = false;
                        ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = false;
                    }
                    else
                    {
                        ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_from")).Enabled = true;
                        ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_ship_to")).Enabled = true;
                    }

                }
            }
        }
    }

    protected void txt_ljh_TextChanged(object sender, EventArgs e)
    {
        txt_ljh_Text_change();
    }
    public void gv_ljztgz_zhidu()
    {
        for (int i = 0; i < gv_ljztgz.Rows.Count; i++)
        {
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Enabled = false;//结束日期
            ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Enabled = false;//零件状态
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_ztgz_description")).Enabled = false;//状态说明
            ((Button)this.gv_ljztgz.Rows[i].FindControl("btncommit")).Visible = false;
        }
    }
    public void gv_ljztgz_write()
    {
        for (int i = 0; i < gv_ljztgz.Rows.Count; i++)
        {
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Enabled = true;//结束日期
            ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Enabled = true;//零件状态
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_ztgz_description")).Enabled = true;//状态说明
            ((Button)this.gv_ljztgz.Rows[i].FindControl("btncommit")).Visible = true;
        }
    }
    public void ddl_ljztgz()
    {
        for (int i = 0; i < gv_ljztgz.Rows.Count; i++)
        {
            DataTable dtlj_status = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).DataSource = BJ_CLASS.BJ_BASE("lj_status");
            ((DropDownList)gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).DataTextField = "lookup_desc";
            ((DropDownList)gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).DataValueField = "lookup_desc";
            ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).DataBind();

            if (((TextBox)gv_ljztgz.Rows[i].FindControl("txtlj_status")).Text != "")
            {
                ((DropDownList)gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text = ((TextBox)gv_ljztgz.Rows[i].FindControl("txtlj_status")).Text;
            }
            ((DropDownList)gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Items.Insert(0, new ListItem("争取", "争取"));
        }
    }
    /// <summary>
    /// add by lilian 2017/08/15 设定零件结束状态跟踪的textbox 的可编辑
    /// </summary>
    public void gv_ljztgz_edit()
    {
        for (int i = 0; i < gv_ljztgz.Rows.Count; i++)
        {
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Enabled = true;//结束日期
            ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Enabled = true;//零件状态
            ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_ztgz_description")).Enabled = true;//状态说明
            ((Button)this.gv_ljztgz.Rows[i].FindControl("btncommit")).Enabled = true;//保存按钮
        }
    }




    protected void gv_ljztgz_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //add by lilian 
        if (e.CommandName == "ztgz_commit")
        {
            //获取保存按钮发生的行号
            int i = Convert.ToInt32(e.CommandArgument);
            if (((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Text == "" &&
                (((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "合同完成停止跟踪" ||
                ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "定点取消合同跟踪" ||
                ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "放弃跟踪"
                ))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写该零件对应的结束日期！')", true);
                ViewState["lv"] = "htgz";
                return;
                //ViewState["lv"] = "htgz";
            }
            if ((((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "已定点合同跟踪"))
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('（已定点合同跟踪）状态不需要维护，自动更新状态！')", true);
                ViewState["lv"] = "htgz";
                return;
                //ViewState["lv"] = "htgz";
            }

            if (((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Text != "" &&
              (((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "争取" ||
              ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).Text == "已定点合同跟踪"
              ))
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('（争取&已定点合同跟踪）这两种状态不可以维护结束日期！')", true);
                ViewState["lv"] = "htgz";
                return;
                //ViewState["lv"] = "htgz";
            }
            //更新表Baojia_agreement_flow 并插入Baojia_agreement_flow_log
            DbHelperSQL.ExecuteSql("exec Baojia_ljztgz_update  '" + ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_id")).Text + "', '" + ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_jieshu_date")).Text + "', '" + ((DropDownList)this.gv_ljztgz.Rows[i].FindControl("ddl_lj_status")).SelectedValue + "','" + ((TextBox)this.gv_ljztgz.Rows[i].FindControl("txt_ztgz_description")).Text + "','" + this.txt_update_user_name.Value + "' ");
            //插入销售保存合同信息记录
            string date = System.DateTime.Now.ToString();
            string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(零件状态维护)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','保存零件结束状态记录')";
            DbHelperSQL.ExecuteSql(strsql_xs);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('保存成功！')", true);
        }
        ViewState["lv"] = "htgz";
    }

    protected void DDL_wl_tk_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDL_project_size.SelectedValue = "";
    }




    protected void BTN_Sales_project_level_Click(object sender, EventArgs e)
    {
        int update = -1;
        if (DDL_project_level.SelectedValue != "")
        {
            update = BJ_CLASS.BTN_Sales_project_level(Convert.ToInt32(Request["requestid"]), DDL_project_level.SelectedValue, "修改");
            if (update > 0)
            {
                //插入申请人log
                string date = System.DateTime.Now.ToString();
                string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(修改)', '10', '" + txt_update_user.Value + "', '" + date + "', '" + date + "','" + date + "','0','" + txt_BTN_Sales_project_level.Text + "')";
                DbHelperSQL.ExecuteSql(strsql_xs);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 修改成功！')", true);
                BTN_Sales_project_level.Enabled = false;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('争取级别不能为空！')", true);
        }
    }

    protected void BTN_Sales_5_Click(object sender, EventArgs e)
    {
        //验证报价明细是否填写完成
        for (int i = 0; i < gv_bjmx.Rows.Count; i++)
        {
            if (((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_pc_mj_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_per_price")).Text == "" && ((TextBox)this.gv_bjmx.Rows[i].FindControl("txt_yj_mj_price")).Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少填写一样价格')", true);
                return;
            }

        }

        using (TransactionScope ts = new TransactionScope())
        {
            //报价明细保存
            int update = -1;
            update = BJ_CLASS.Baojia_Reback_Modify_Flow(Convert.ToInt32(Request["requestid"]), "Baojia_dtl");
            if (update > 0)
            {
                //插入零件明细
                insert_Baojia_dtl();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件信息维护成功！')", true);
                //插入销售保存合同信息记录
                string date = System.DateTime.Now.ToString();
                string strsql_xs = " insert into Baojia_sign_flow(requestId,baojia_no,turns,Flow,FlowID,Big_FlowID,Step,StepID,empid,receive_date,require_date,sign_date,Operation_time,sign_desc)  VALUES('" + Convert.ToInt32(Request["requestid"]) + "','" + txt_baojia_no.Value + "','" + DDL_turns.SelectedValue + "', '销售', '10', '100','销售工程师(报价修改)', '10', '" + txt_sales_empid.Value + "', '" + date + "', '" + date + "','" + date + "','0','报价记录修改')";
                DbHelperSQL.ExecuteSql(strsql_xs);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('保存不成功，请检查或联系管理员！')", true);

            }
            ts.Complete();
        }

    }
}


