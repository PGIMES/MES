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
using MES.DAL;
using MES.Model;
public partial class Customer_QAD : System.Web.UI.Page
{

    Customer_CLASS Customer_CLASS = new Customer_CLASS();
    Mail Mail = new Mail();
    public static string ctl = "ctl00$MainContent$";
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        this.gv_rz1.PageSize = 200;
        this.gv_rz2.PageSize = 100;
        this.gv_DebtorShipTo.PageSize = 100;

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
                ViewState["empid"] = "00002";

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
                DataTable dtemp = Customer_CLASS.emp(ViewState["empid"].ToString());
                txt_update_user_name.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_update_user_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                txt_update_user_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                txt_ZG_empid.Value = dtemp.Rows[0]["zg_workcode"].ToString();
                txt_ZG.Value = dtemp.Rows[0]["zg_name"].ToString();
                if (Request["requestid"] != null )//页面加载   更新客户
                {
                    int requestid = Convert.ToInt32(Request["requestid"]);
                    //获取数据
                    ShowInfo_mstr(requestid);
                    //获取当前轮数
                    bindrz_turns(requestid);
                    int turns = Convert.ToInt32(Lab_turns.Text);
                    //验证客户类别
                    yanzheng();           
                    //获取发往明细
                    Query_gv(requestid, turns);
        
                    yq_zhankai();
                    //页面主表数据只读
                    mstr_zhidu();
                    if (Request["update"] == null)//页面加载
                    {

                        int Status_id = Convert.ToInt32(Lab_Status_id.Text);

                        //获取签核记录
                        gv_log(requestid, turns);                       
                        approve_yanzheng(Status_id, requestid, txt_update_user.Value, Convert.ToString(turns));
                        //获取明细只读
                        gv_DebtorShipTo_zhidu();
                        gvcolour();
                        bindrz_status(Status_id);
                        bindrz_log(Request["requestid"], gv_rz1);
                        bindrz2_log(Request["requestid"], gv_rz2);
                    }
                    else//更新客户
                    {
                        mstr_write();
                        //重新获取申请人员
                        this.txtCreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        txtUserid.Value = ViewState["empid"].ToString();
                        txtUserName.Value = dtemp.Rows[0]["lastname"].ToString();
                        txtUserName_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                        txtdept.Value = dtemp.Rows[0]["dept_name"].ToString();
                        txtmanagerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                        txtmanager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                        txtmanager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();

                        dtemp.Clear();
                        //初始话状态
                        Lab_Status_id.Text = "0";
                        Lab_Status.Text = "销售工程师";
                       
                        //获取审批流程
                        approve();
                        approve_yanzheng(0, 0, txt_update_user.Value, Lab_turns.Text);
                        gv_DebtorShipTo_update_zhidu();
                        if (gv_DebtorShipTo.Rows.Count <= 0)
                        {
                            //初始化发往明细一行
                            gv_DebtorShipTo_InitTable();
                        }
                    }
                
                }
                else//新建
                {
                    //获取个人基本信息
                    txtCode.Value = "Customer" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    this.txtCreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    txtUserid.Value = ViewState["empid"].ToString();
                    txtUserName.Value = dtemp.Rows[0]["lastname"].ToString();
                    txtUserName_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                    txtdept.Value = dtemp.Rows[0]["dept_name"].ToString();
                    txtmanagerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                    txtmanager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                    txtmanager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();

                    dtemp.Clear();
                    //默认字段
                    txtAddressTypeCode.Value = "HEADOFFICE";
                    Lab_Status_id.Text = "0";
                    Lab_Status.Text = "销售工程师";
                    Lab_turns.Text = "1";
                    //收拢展开
                    yq_zhankai();
                    //新建时默认为已有客户大类
                    customer_isold();
                    //初始化发往明细一行
                    gv_DebtorShipTo_InitTable();
                    //获取审批流程
                    approve();
                    approve_yanzheng(0, 0, txt_update_user.Value, Lab_turns.Text);


                }
                
            }
        }
    }
    //日志信息
    public void bindrz_status(int status_id)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("  SELECT * FROM [MES].[dbo].[form4_Customer_Status_mstr] ");
        sql.Append("    where status_id = '" + status_id + "'  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        Lab_Status.Text = dt.Rows[0]["status"].ToString();
    }
    public void bindrz_turns(int requestid)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("   SELECT requestid, max(Update_LB)  AS turns FROM[MES].[dbo].[form4_Sale_Customer_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  GROUP BY requestid  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
    
       if(Request["update"] == null)
        {
            Lab_turns.Text = dt.Rows[0]["turns"].ToString();
        }
       else
        {
            Lab_turns.Text = Convert.ToString(Convert.ToUInt32(dt.Rows[0]["turns"].ToString()) +1) ;
        }
    }
   
    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("  SELECT * FROM [MES].[dbo].[form4_Customer_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }


    public void bindrz2_log(string requestid, GridView gv_rz2)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("  SELECT * FROM [MES].[dbo].[form4_Sale_Customer_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz2.DataSource = dt;
        gv_rz2.DataBind();
        gv_rz2.PageSize = 100;
    }
    public void DDL()
    {

        DDL_cmClassName.DataSource = Customer_CLASS.BJ_base("Customer", "Customer");
        DDL_cmClassName.DataValueField = "name";
        DDL_cmClassName.DataTextField = "name";
        DDL_cmClassName.DataBind();
        this.DDL_cmClassName.Items.Insert(0, new ListItem("", ""));

        DDLtry_country.DataSource = Customer_CLASS.form4_Customer_base_PRO("country");
        DDLtry_country.DataValueField = "CountryCode";
        DDLtry_country.DataTextField = "CountryDescription";
        DDLtry_country.DataBind();
        this.DDLtry_country.Items.Insert(0, new ListItem("", ""));

        DDLLedgerDays.DataSource = Customer_CLASS.form4_Customer_base_PRO("PaymentCondition");
        DDLLedgerDays.DataValueField = "PaymentConditionCode";
        DDLLedgerDays.DataTextField = "PaymentConditionCode";
        DDLLedgerDays.DataBind();
        this.DDLLedgerDays.Items.Insert(0, new ListItem("", ""));

        DDLConditionCode.DataSource = Customer_CLASS.form4_Customer_base_PRO("PaymentCondition");
        DDLConditionCode.DataValueField = "PaymentConditionCode";
        DDLConditionCode.DataTextField = "PaymentConditionCode";
        DDLConditionCode.DataBind();
        this.DDLConditionCode.Items.Insert(0, new ListItem("", ""));

        DDLCurrencyCode.DataSource = Customer_CLASS.form4_Customer_base_PRO("Currency");
        DDLCurrencyCode.DataValueField = "CurrencyCode";
        DDLCurrencyCode.DataTextField = "CurrencyCode";
        DDLCurrencyCode.DataBind();
        this.DDLCurrencyCode.Items.Insert(0, new ListItem("", ""));


    }

    public void yq_zhankai()
    {

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#HWFW').removeClass('collapse');$('#approve').removeClass('collapse');$('#YWGX').removeClass('collapse');", true);

    }

    public void yanzheng()
    {
        if (DDL_Class.SelectedValue == "新增客户大类")
        {
            customer_isnew();

            if (Lab_turns.Text == "1")
            {
                Lab_update.Text = DDL_Class.SelectedValue + "并且为" + "新增客户";
            }
            else
            {
                Lab_update.Text = DDL_Class.SelectedValue + "并且为" + "更新客户";
            }
        }
        else
        {
            customer_isold();

            if (Lab_turns.Text == "1")
            {
                Lab_update.Text = "新增客户";
            }
            else
            {
                Lab_update.Text = "更新客户";
            }

        }
    }
    public void gv_approve_comit_yingchang()
    {
        for (int i = 0; i < gv_approve.Rows.Count; i++)
        {
            ((Button)this.gv_approve.Rows[i].FindControl("btncomit")).Visible = false;
            ((Button)this.gv_approve.Rows[i].FindControl("btncomit")).Enabled = false;
            ((TextBox)this.gv_approve.Rows[i].FindControl("Update_content")).Enabled = false;
        }
    }
    public void approve_yanzheng(int status_id, int requestid, string empid,string turns)
    {
        //按钮失效
        gv_approve_comit_yingchang();
        for (int i = 0; i < gv_approve.Rows.Count; i++)
        {
            
            //申请时按钮启用
            if (status_id == 0)
            {
                ((Label)this.gv_approve.Rows[0].FindControl("Lab_yz")).Visible = true;
                ((Button)this.gv_approve.Rows[0].FindControl("btncomit")).Visible = true;
                ((Button)this.gv_approve.Rows[0].FindControl("btncomit")).Enabled = true;
                ((TextBox)this.gv_approve.Rows[0].FindControl("Update_content")).Enabled = true;
                ((TextBox)this.gv_approve.Rows[0].FindControl("Update_content")).BackColor = System.Drawing.Color.Yellow;
               
            }
            //审批时按钮启用
            else
            {
                DataTable dtbtn = Customer_CLASS.GetBTN(status_id, requestid, empid, turns);
                if (dtbtn == null)
                {
                    
                        gv_approve_comit_yingchang();
                    
                }
                else
                {
                    if (dtbtn.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtbtn.Rows.Count; j++)
                        {
                            if (dtbtn.Rows[j]["Update_user"].ToString() == ((TextBox)gv_approve.Rows[i].FindControl("Update_user")).Text && dtbtn.Rows[j]["status_id"].ToString() == ((TextBox)gv_approve.Rows[i].FindControl("status_id")).Text)
                            {
                                ((Label)this.gv_approve.Rows[i].FindControl("Lab_yz")).Visible = true;
                                ((Button)this.gv_approve.Rows[i].FindControl("btncomit")).Visible = true;
                                ((Button)this.gv_approve.Rows[i].FindControl("btncomit")).Enabled = true;
                                ((TextBox)this.gv_approve.Rows[i].FindControl("Update_content")).Enabled = true;
                                ((TextBox)this.gv_approve.Rows[i].FindControl("Update_content")).BackColor = System.Drawing.Color.Yellow;
                                //财务可编辑财务信息
                                if (dtbtn.Rows[j]["Update_user"].ToString() == ((TextBox)gv_approve.Rows[i].FindControl("Update_user")).Text && dtbtn.Rows[j]["status_id"].ToString()=="2")
                                {
                                    FIN_write();
                                    ((Button)this.gv_approve.Rows[i].FindControl("btncomit")).ValidationGroup = "FIN";
                                    ViewState["lv"] = "CWSJ";
                                }
                            }
                           
                        }
                        
                    }
                  
                }
            }
        }
    }
    public void mstr_write()
    {
        this.CBL_cm_domain.Enabled = true;
        DDL_DebtorTypeCode.Enabled = true;
        this.DDL_DebtorTypeCode.Style.Add("background-color", "#FF,FF,FF");
        this.txtBusinessRelationName1.Disabled = false;
        this.txtAddressSearchName.Disabled = false;
        this.txtAddressSearchName.Disabled = false;
        this.txtAddressTypeCode.Disabled = false;
        this.txtAddressStreet1.Disabled = false;
        this.DDLcm_lang.Enabled = true;
        this.DDLcm_lang.Style.Add("background-color", "#FF,FF,FF");
        this.DDLtry_country.Enabled = true;
        this.DDLtry_country.Style.Add("background-color", "#FF,FF,FF");
        this.txtcm_region.Disabled = false;
        this.txtAddressZip.Disabled = false;
        this.txtAddressCity.Disabled = false;
        this.txtAddressState.Disabled = false;
        this.txtad_fax.Disabled = false;
        this.txtad_phone.Disabled = false;
        this.txtAddressEMail.Disabled = false;
        this.txtContactName.Disabled = false;
        this.DDLContactGender.Enabled = true;
        this.DDLContactGender.Style.Add("background-color", "#FF,FF,FF");
        this.txtContactTelephone.Disabled = false;
        this.txtContactEmail.Disabled = false;
        this.CBAddressIsTaxable.Enabled = true;
        this.CBAddressIsTaxIncluded.Enabled = true;
        this.CBAddressIsTaxInCity.Enabled = true;
        this.DDLTxzTaxZone.Enabled = true;
        this.DDLTxzTaxZone.Style.Add("background-color", "#FF,FF,FF");
        this.DDLTxclTaxCls.Enabled = true;
        this.DDLTxclTaxCls.Style.Add("background-color", "#FF,FF,FF");
        this.DDLLedgerDays.Enabled = true;
        this.DDLLedgerDays.Style.Add("background-color", "#FF,FF,FF");
        this.DDLcm_slspsn.Enabled = true;
        this.DDLcm_slspsn.Style.Add("background-color", "#FF,FF,FF");
        this.CBcm_fix_pr.Enabled = true;
    }



    public void mstr_zhidu()
    {
        this.CBL_cm_domain.Enabled = false;
        this.DDL_Class.Enabled = false;
        this.DDL_Class.Style.Add("background-color", "#F0F0F0");
        this.txtCmClassID.Disabled = true;
        this.DDL_cmClassName.Enabled = false;
        this.DDL_cmClassName.Style.Add("background-color", "#F0F0F0");
        //this.CBL_cm_domain.Enabled = cm_domain;        
        this.txtBusinessRelationCode.Disabled = true;
        this.txtBusinessRelationName1.Disabled = true;
        this.txtAddressSearchName.Disabled = true;
        this.txtAddressTypeCode.Disabled = true;
        this.txtAddressStreet1.Disabled = true;
        this.DDLcm_lang.Enabled = false;
        this.DDLcm_lang.Style.Add("background-color", "#F0F0F0");
        this.DDLtry_country.Enabled = false;
        this.DDLtry_country.Style.Add("background-color", "#F0F0F0");
        this.txtcm_region.Disabled = true;
        this.txtAddressZip.Disabled = true;
        this.txtAddressCity.Disabled = true;
        this.txtAddressState.Disabled = true;
        this.txtad_fax.Disabled = true;
        this.txtad_phone.Disabled = true;
        this.txtAddressEMail.Disabled = true;
        this.txtContactName.Disabled = true;
        this.DDLContactGender.Enabled = false;
        this.DDLContactGender.Style.Add("background-color", "#F0F0F0");
        this.txtContactTelephone.Disabled = true;
        this.txtContactEmail.Disabled = true;
        this.CBAddressIsTaxable.Enabled = false;
        this.CBAddressIsTaxIncluded.Enabled = false;
        this.CBAddressIsTaxInCity.Enabled = false;
        this.DDLTxzTaxZone.Enabled = false;
        this.DDLTxzTaxZone.Style.Add("background-color", "#F0F0F0");
        this.DDLTxclTaxCls.Enabled = false;
        this.DDLTxclTaxCls.Style.Add("background-color", "#F0F0F0");
        this.DDLLedgerDays.Enabled = false;
        this.DDLLedgerDays.Style.Add("background-color", "#F0F0F0");
        this.DDLcm_slspsn.Enabled = false;
        this.DDLcm_slspsn.Style.Add("background-color", "#F0F0F0");
        this.txtcm_pr_list.Disabled = true;
        this.CBcm_fix_pr.Enabled = false;
        this.DDLBankNumberIsActive.Enabled = false;
        this.DDLBankNumberIsActive.Style.Add("background-color", "#F0F0F0");
        this.txtInvControlGLProfileCode.Disabled = true;
        this.txtCnControlGLProfileCode.Disabled = true;
        this.txtPrepayControlGLProfileCode.Disabled = true;
        this.txtSalesAccountGLProfileCode.Disabled = true;
        this.DDLReasonCode.Enabled = false;
        this.DDLReasonCode.Style.Add("background-color", "#F0F0F0");
        this.DDLConditionCode.Enabled = false;
        this.DDLConditionCode.Style.Add("background-color", "#F0F0F0");
        this.DDLCurrencyCode.Enabled = false;
        this.DDLCurrencyCode.Style.Add("background-color", "#F0F0F0");
        this.txtBankAccFormatCode.Disabled = true;
        this.txtBankNumberFormatted.Disabled = true;
        this.txtOwnBankNumber.Disabled = true;

        this.DDL_DebtorTypeCode.Enabled = false;
        this.DDL_DebtorTypeCode.Style.Add("background-color", "#F0F0F0");
        this.txt_ExistsClass.Disabled = true;


    }
    public void FIN_write()
    {
        //this.DDLBankNumberIsActive.Enabled = true;
        this.txtInvControlGLProfileCode.Disabled = false;
        this.txtCnControlGLProfileCode.Disabled = false;
        this.txtPrepayControlGLProfileCode.Disabled = false;
        this.txtSalesAccountGLProfileCode.Disabled = false;
        this.DDLReasonCode.Enabled = true;
        this.DDLReasonCode.Style.Add("background-color", "White");
        this.DDLConditionCode.Enabled = true;
        this.DDLConditionCode.Style.Add("background-color", "White");
        this.DDLCurrencyCode.Enabled = true;
        this.DDLCurrencyCode.Style.Add("background-color", "White");
        this.txtBankAccFormatCode.Disabled = false;
        this.txtBankNumberFormatted.Disabled = false;
        this.txtOwnBankNumber.Disabled = false;
    }

    public void gv_DebtorShipTo_zhidu()
    {
        for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
        {

            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToCode")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToName")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("cm_addr")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressZip")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressCity")).Enabled = false;
            ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("cm_region")).Enabled = false;
            ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Enabled = false;
            ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Enabled = false;
            
            ((CheckBoxList)this.gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Enabled = false;
            ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressStreet1")).Enabled = false;
            ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Enabled = false;
            ((Button)this.gv_DebtorShipTo.FooterRow.FindControl("btnAddQTY")).Visible = false;
            ((Button)this.gv_DebtorShipTo.Rows[i].FindControl("btnDelQTY")).Visible = false;
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Style.Add("background-color", "#F0F0F0");
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Style.Add("background-color", "#F0F0F0");
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Style.Add("background-color", "#F0F0F0");
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Style.Add("background-color", "#F0F0F0");
        }


    }
    public void gv_DebtorShipTo_update_zhidu()
    {
        for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
        {

            ((Button)this.gv_DebtorShipTo.Rows[i].FindControl("btnDelQTY")).Visible = false;
   
        }


    }

    private void approve()
    {
        if (txtdept.Value == "IT部" || txtdept.Value == "销售二部" || txt_update_user.Value == "00002")
        {
            DataTable dt = Customer_CLASS.GetSPR(txt_ZG_empid.Value, txt_ZG.Value, txtUserid.Value, txtUserName.Value, txtCreateDate.Value, Lab_turns.Text);
            ViewState["detail"] = dt;
            gv_approve.DataSource = dt;
            gv_approve.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售部才可新增客户！')", true);
            //BTN_Sales_submit.Visible = false;
        }
    }
    private void gv_DebtorShipTo_InitTable()
    {
        //合同追踪页面加载
        DataTable dtht = new DataTable();
        dtht.Columns.Add(new DataColumn("turns"));
        dtht.Columns.Add(new DataColumn("ID"));
        dtht.Columns.Add(new DataColumn("BusinessRelationCode"));
        dtht.Columns.Add(new DataColumn("ShipToNum"));
        dtht.Columns.Add(new DataColumn("AddressTypeCode"));
        dtht.Columns.Add(new DataColumn("DebtorShipToCode"));
        dtht.Columns.Add(new DataColumn("DebtorShipToName"));
        dtht.Columns.Add(new DataColumn("cm_addr"));        
        dtht.Columns.Add(new DataColumn("AddressZip"));
        dtht.Columns.Add(new DataColumn("AddressCity"));
        dtht.Columns.Add(new DataColumn("ctry_country"));
        dtht.Columns.Add(new DataColumn("cm_region"));
        dtht.Columns.Add(new DataColumn("LngCode"));
        dtht.Columns.Add(new DataColumn("Debtor_Domain"));
        dtht.Columns.Add(new DataColumn("AddressStreet1"));
        dtht.Columns.Add(new DataColumn("IsEffective"));
        dtht.Columns.Add(new DataColumn("UpdateDate"));
        dtht.Columns.Add(new DataColumn("UpdateById"));
        dtht.Columns.Add(new DataColumn("UpdateByName"));

        ViewState["dtht"] = dtht;

        int lnht = gv_DebtorShipTo.Rows.Count;
        for (int i = lnht; i < 1; i++)
        {
            DataRow ldr = dtht.NewRow();
            ldr["ID"] = 0;
            ldr["ShipToNum"] = 0;
            ldr["UpdateDate"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //ldr["AddressTypeCode"] = "SHIP-TO";
            //ldr["IsEffective"] = "有效";
            ldr["UpdateById"] = txt_update_user.Value;
            ldr["UpdateByName"] = txt_update_user_name.Value;
            dtht.Rows.Add(ldr);
        }
        this.gv_DebtorShipTo.DataSource = dtht;
        this.gv_DebtorShipTo.DataBind();
        //gv_DebtorShipTo_write();
        DDL_gv_DebtorShipTo();

    }
    protected void gv_DebtorShipTo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtht = ViewState["dtht"] as DataTable;
        DataTable dtsht = GetDataTableFromGridView1(gv_DebtorShipTo, dtht);
        if (e.CommandName == "addQTY")
        {
            DataRow dr = dtsht.NewRow();
            GridViewRow row = ((GridViewRow)((Control)e.CommandSource).Parent.Parent);
            dr["ID"] = 0;
            dr["ShipToNum"] = 0;
            dr["UpdateDate"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //dr["AddressTypeCode"] = "SHIP-TO";
            //ldr["IsEffective"] = "有效";
            dr["UpdateById"] = txt_update_user.Value;
            dr["UpdateByName"] = txt_update_user_name.Value;


            dtsht.Rows.Add(dr);
    

        }
        if (e.CommandName == "delQTY")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            if (index == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('至少保留一行！')", true);
            }
            else
            {
                dtsht.Rows.RemoveAt(index);
            }

        }
        ViewState["dtht"] = dtsht;
        this.gv_DebtorShipTo.DataSource = dtsht;
        this.gv_DebtorShipTo.DataBind();
        DDL_gv_DebtorShipTo();
        yq_zhankai();
        if (Request["update"] != null)
        {
            gv_DebtorShipTo_update_zhidu();
        }
        //ViewState["lv"] = "HWFW";
    }
    public void DDL_gv_DebtorShipTo()
    {
        DataTable dt_country = Customer_CLASS.form4_Customer_base_PRO("country");
        for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
        {

            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).DataSource = dt_country;
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).DataTextField = "CountryDescription";
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).DataValueField = "CountryCode";
            ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).DataBind();
            ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Items.Insert(0, new ListItem("", ""));
            //((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_sdxx")).SelectedValue = dt_htgz.Rows[i]["sdxx"].ToString();
            if (((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_ctry_country")).Text != "")
            {
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Text = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_ctry_country")).Text;
            }

            if (((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressTypeCode")).Text != "")
            {
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Text = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressTypeCode")).Text;
            }

            if (((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_LngCode")).Text != "")
            {
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Text = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_LngCode")).Text;
            }

            if (((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_IsEffective")).Text != "")
            {
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Text = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_IsEffective")).Text;
            }

            //if (((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_Debtor_Domain")).Text != "")
            //{
            //    int count = ((CheckBoxList)gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items.Count;
            //    string Debtor_Domain = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("txt_Debtor_Domain")).Text;
            //    string[] str = Debtor_Domain.Split(';');//调用Split方法，定义string[]类型的变量
            //    for (int k = 0; k < str.Length; k++)
            //    {
            //        for (int j = 0; j < count; j++)//循环遍历在页面中CheckBoxList里的所有项
            //        {
            //            if (((CheckBoxList)gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items[j].Value == str[k])//判断i遍历后的id与cbx当中的值是否有相同
            //            {
            //                ((CheckBoxList)gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items[j].Selected = true; //相同就选中
            //            }
            //        }
            //    }

            //}



        }

    }
    public void insert_mstr(int requestid_sq)
    {
        int requestid = requestid_sq;
        string Code = this.txtCode.Value;
        DateTime CreateDate = DateTime.Parse(this.txtCreateDate.Value);
        string Userid = this.txtUserid.Value;
        string UserName = this.txtUserName.Value;
        string UserName_AD = this.txtUserName_AD.Value;
        string dept = this.txtdept.Value;
        string managerid = this.txtmanagerid.Value;
        string manager = this.txtmanager.Value;
        string manager_AD = this.txtmanager_AD.Value;
        string Class = this.DDL_Class.SelectedValue;
        string CmClassID = this.txtCmClassID.Value;
        string cmClassName = this.DDL_cmClassName.SelectedValue;
        string cm_domain = "";
        for (int i = 0; i < CBL_cm_domain.Items.Count; i++)
        {
            if (CBL_cm_domain.Items[i].Selected)
            {
                cm_domain += ";" + CBL_cm_domain.Items[i].Value;
            }
        }

        string BusinessRelationCode = this.txtBusinessRelationCode.Value;
        string BusinessRelationName1 = this.txtBusinessRelationName1.Value;
        string AddressSearchName = this.txtAddressSearchName.Value;
        string AddressTypeCode = this.txtAddressTypeCode.Value;
        string AddressStreet1 = this.txtAddressStreet1.Value;
        string cm_lang = this.DDLcm_lang.SelectedValue;
        string ctry_country = this.DDLtry_country.SelectedValue;
        string cm_region = this.txtcm_region.Value;
        string AddressZip = this.txtAddressZip.Value;
        string AddressCity = this.txtAddressCity.Value;
        string AddressState = this.txtAddressState.Value;
        string ad_fax = this.txtad_fax.Value;
        string ad_phone = this.txtad_phone.Value;
        string AddressEMail = this.txtAddressEMail.Value;
        string ContactName = this.txtContactName.Value;
        string ContactGender = this.DDLContactGender.SelectedValue;
        string ContactTelephone = this.txtContactTelephone.Value;
        string ContactEmail = this.txtContactEmail.Value;
        string AddressIsTaxable = this.CBAddressIsTaxable.Text;
        if (CBAddressIsTaxable.Checked == true)
        {
            AddressIsTaxable = "1";
        }
        else
        {
            AddressIsTaxable = "0";
        }
        string AddressIsTaxIncluded = this.CBAddressIsTaxIncluded.Text;
        if (CBAddressIsTaxIncluded.Checked == true)
        {
            AddressIsTaxIncluded = "1";
        }
        else
        {
            AddressIsTaxIncluded = "0";
        }
        string AddressIsTaxInCity = this.CBAddressIsTaxInCity.Text;
        if (CBAddressIsTaxInCity.Checked == true)
        {
            AddressIsTaxInCity = "1";
        }
        else
        {
            AddressIsTaxInCity = "0";
        }

        string TxzTaxZone = this.DDLTxzTaxZone.SelectedValue;
        string TxclTaxCls = this.DDLTxclTaxCls.Text;
        string LedgerDays = this.DDLLedgerDays.Text;
        string cm_slspsn = this.DDLcm_slspsn.SelectedValue;
        string cm_pr_list = this.txtcm_pr_list.Value;
        string cm_fix_pr = this.CBcm_fix_pr.Text;
        if (CBcm_fix_pr.Checked == true)
        {
            cm_fix_pr = "1";
        }
        else
        {
            cm_fix_pr = "0";
        }
        //财务数据
        string BankNumberIsActive = this.DDLBankNumberIsActive.SelectedValue;
        string InvControlGLProfileCode = this.txtInvControlGLProfileCode.Value;
        string CnControlGLProfileCode = this.txtCnControlGLProfileCode.Value;
        string PrepayControlGLProfileCode = this.txtPrepayControlGLProfileCode.Value;
        string SalesAccountGLProfileCode = this.txtSalesAccountGLProfileCode.Value;
        string ReasonCode = this.DDLReasonCode.SelectedValue;
        string NormalPaymentConditionCode = this.DDLConditionCode.SelectedValue;
        string CurrencyCode = this.DDLCurrencyCode.SelectedValue;
        string BankAccFormatCode = this.txtBankAccFormatCode.Value;
        string BankNumberFormatted = this.txtBankNumberFormatted.Value;
        string OwnBankNumber = this.txtOwnBankNumber.Value;
        string DebtorTypeCode = this.DDL_DebtorTypeCode.SelectedValue;

        //主表数据新增
        string ExistsClass = this.txt_ExistsClass.Value;
        string Status_id = this.Lab_Status_id.Text;
        //根据当前状态修改下一阶段状态。
        if (this.Lab_Status_id.Text == "0")
        {
            Status_id = "1";
        }
        else if (this.Lab_Status_id.Text == "1")
        {
            Status_id = "2";
        }
        else if (this.Lab_Status_id.Text == "2")
        {
            Status_id = "3";
        }
        else if (this.Lab_Status_id.Text == "3")
        {
            Status_id = "4";
        }
        else if (this.Lab_Status_id.Text == "4")
        {
            Status_id = "5";
        }

        MES.Model.form4_Customer_mstr model = new MES.Model.form4_Customer_mstr();
        model.requestid = requestid;
        model.Code = Code;
        model.CreateDate = CreateDate;
        model.Userid = Userid;
        model.UserName = UserName;
        model.UserName_AD = UserName_AD;
        model.dept = dept;
        model.managerid = managerid;
        model.manager = manager;
        model.manager_AD = manager_AD;
        model.Class = Class;
        model.CmClassID = CmClassID;
        model.cmClassName = cmClassName;
        model.cm_domain = cm_domain;
        model.BusinessRelationCode = BusinessRelationCode;
        model.BusinessRelationName1 = BusinessRelationName1;
        model.AddressSearchName = AddressSearchName;
        model.AddressTypeCode = AddressTypeCode;
        model.AddressStreet1 = AddressStreet1;
        model.cm_lang = cm_lang;
        model.ctry_country = ctry_country;
        model.cm_region = cm_region;
        model.AddressZip = AddressZip;
        model.AddressCity = AddressCity;
        model.AddressState = AddressState;
        model.ad_fax = ad_fax;
        model.ad_phone = ad_phone;
        model.AddressEMail = AddressEMail;
        model.ContactName = ContactName;
        model.ContactGender = ContactGender;
        model.ContactTelephone = ContactTelephone;
        model.ContactEmail = ContactEmail;
        model.AddressIsTaxable = AddressIsTaxable;
        model.AddressIsTaxIncluded = AddressIsTaxIncluded;
        model.AddressIsTaxInCity = AddressIsTaxInCity;
        model.TxzTaxZone = TxzTaxZone;
        model.TxclTaxCls = TxclTaxCls;
        model.LedgerDays = LedgerDays;
        model.cm_slspsn = cm_slspsn;
        model.cm_pr_list = cm_pr_list;
        model.cm_fix_pr = cm_fix_pr;
        model.BankNumberIsActive = BankNumberIsActive;
        model.InvControlGLProfileCode = InvControlGLProfileCode;
        model.CnControlGLProfileCode = CnControlGLProfileCode;
        model.PrepayControlGLProfileCode = PrepayControlGLProfileCode;
        model.SalesAccountGLProfileCode = SalesAccountGLProfileCode;
        model.ReasonCode = ReasonCode;
        model.NormalPaymentConditionCode = NormalPaymentConditionCode;
        model.CurrencyCode = CurrencyCode;
        model.BankAccFormatCode = BankAccFormatCode;
        model.BankNumberFormatted = BankNumberFormatted;
        model.OwnBankNumber = OwnBankNumber;
        model.DebtorTypeCode = DebtorTypeCode;
        model.ExistsClass = ExistsClass;
        model.Status_id = Status_id;
        MES.DAL.form4_Customer_mstr dal = new MES.DAL.form4_Customer_mstr();

        if (Request["requestid"] != null)
        {
            dal.Update(model);
        }
        else
        {
            dal.Add(model);
        }
    }

    public void insert_mstr_log(int requestid_sq)
    {       
            System.Text.StringBuilder strsql = new StringBuilder();
            strsql.Append("insert into form4_Customer_mstr_log select * from form4_Customer_mstr  where requestid='"+ requestid_sq + "'");
            DbHelperSQL.ExecuteSql(strsql.ToString());        
    }
    public void insert_gv_DebtorShipTo_log(int requestid_sq)
    {
        System.Text.StringBuilder strsql = new StringBuilder();
        strsql.Append("insert into form4_Customer_DebtorShipTo_log select * from form4_Customer_DebtorShipTo  where requestid='" + requestid_sq + "'");
        DbHelperSQL.ExecuteSql(strsql.ToString());
    }
    public void insert_gv_DebtorShipTo(int requestid_sq)
    {
        for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
        {
            int requestid = requestid_sq;
            string BusinessRelationCode = this.txtBusinessRelationCode.Value;
            if (Request["requestid"] == null && ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text=="")
            {
                ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text = Lab_turns.Text;
               
            }
            else if(((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text!="")
            {
                ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text.Trim();
            }
            if (Request["requestid"] != null && ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text == "")
            {
                ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text = Lab_turns.Text;

            }
            string turns = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("turns")).Text.Trim();
            int ID = int.Parse(((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("ID")).Text.Trim());
            if (((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("ShipToNum")).Text == "0")
            {
                int row = gv_DebtorShipTo.Rows[i].RowIndex + 1;
                ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("ShipToNum")).Text = BusinessRelationCode + row.ToString().PadLeft(3, '0');
            }
            int ShipToNum = int.Parse(((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("ShipToNum")).Text.Trim());
            string DebtorShipToCode = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToCode")).Text.Trim();
            string DebtorShipToName = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToName")).Text.Trim();
            string cm_addr = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("cm_addr")).Text.Trim();
            //string AddressTypeCode = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressTypeCode")).Text.Trim();
            string AddressTypeCode = ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Text.Trim();
            string AddressStreet1 = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressStreet1")).Text.Trim();
            string AddressZip = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressZip")).Text.Trim();
            string AddressCity = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressCity")).Text.Trim();
            string cm_region = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("cm_region")).Text.Trim();
            string ctry_country = ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).SelectedItem.Text.Trim();
            string Debtor_Domain = "";
            for (int j = 0; j < ((CheckBoxList)this.gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items.Count; j++)
            {
                if (((CheckBoxList)this.gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items[j].Selected)
                {
                    Debtor_Domain += ";" + ((CheckBoxList)this.gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Items[j].Value;
                }
            }
            string IsEffective = ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Text.Trim();
            DateTime UpdateDate = DateTime.Parse(((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("UpdateDate")).Text.Trim());
            string UpdateById = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("UpdateById")).Text.Trim();
            string UpdateByName = ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("UpdateByName")).Text.Trim();
            string LngCode = ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Text.Trim();

            MES.Model.form4_Customer_DebtorShipTo model = new MES.Model.form4_Customer_DebtorShipTo();
            model.ID = ID;
            model.requestid = requestid;
            model.BusinessRelationCode = BusinessRelationCode;
            model.ShipToNum = ShipToNum;
            model.DebtorShipToCode = DebtorShipToCode;
            model.DebtorShipToName = DebtorShipToName;
            model.cm_addr = cm_addr;
            model.AddressTypeCode = AddressTypeCode;
            model.AddressStreet1 = AddressStreet1;
            model.AddressZip = AddressZip;
            model.AddressCity = AddressCity;
            model.cm_region = cm_region;
            model.ctry_country = ctry_country;
            model.Debtor_Domain = Debtor_Domain;
            model.IsEffective = IsEffective;
            model.UpdateDate = UpdateDate;
            model.UpdateById = UpdateById;
            model.UpdateByName = UpdateByName;
            model.LngCode = LngCode;
            model.turns = turns;
           

            MES.DAL.form4_Customer_DebtorShipTo dal = new MES.DAL.form4_Customer_DebtorShipTo();


            if (((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("ID")).Text == "0")
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }



        }
        //DataTable dt_ycxx = Customer_CLASS.Getgv(Request["requestid"], "gv_DebtorShipTo");
        //gv_DebtorShipTo.DataSource = dt_ycxx;
        //gv_DebtorShipTo.DataBind();

        DDL_gv_DebtorShipTo();

    }
    public void insert_gv_gv_approve(int requestid_sq)
    {
        for (int i = 0; i < gv_approve.Rows.Count; i++)
        {
            int requestid = requestid_sq;

            int ID = Convert.ToInt32(((TextBox)this.gv_approve.Rows[i].FindControl("ID")).Text.Trim());
            string status_id = ((TextBox)this.gv_approve.Rows[i].FindControl("status_id")).Text.Trim();
            string status_ms = ((TextBox)this.gv_approve.Rows[i].FindControl("status_ms")).Text.Trim();
            string dept = ((TextBox)this.gv_approve.Rows[i].FindControl("dept")).Text.Trim();
            string Update_Engineer = ((TextBox)this.gv_approve.Rows[i].FindControl("Update_Engineer")).Text.Trim();
            string Update_user = ((TextBox)this.gv_approve.Rows[i].FindControl("Update_user")).Text.Trim();
            string Update_username = ((TextBox)this.gv_approve.Rows[i].FindControl("Update_username")).Text.Trim();

            System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
            //DateTime Receive_time = DateTime.Parse(string.IsNullOrEmpty(((TextBox)this.gv_approve.Rows[i].FindControl("Receive_time")).Text) ? null : ((TextBox)this.gv_approve.Rows[i].FindControl("Receive_time")).Text);
            DateTime? Receive_time = (DateTime?)nullableDateTime.ConvertFromString(((TextBox)this.gv_approve.Rows[i].FindControl("Receive_time")).Text);
            DateTime? Commit_time = (DateTime?)nullableDateTime.ConvertFromString(((TextBox)this.gv_approve.Rows[i].FindControl("Commit_time")).Text);
            // DateTime Receive_time = DateTime.Parse(Receive_time1);
            //DateTime? Commit_time = DateTime.Parse(string.IsNullOrEmpty(((TextBox)this.gv_approve.Rows[i].FindControl("Commit_time")).Text) ? null : ((TextBox)this.gv_approve.Rows[i].FindControl("Commit_time")).Text);
            // DateTime Commit_time = DateTime.Parse(null);// DateTime.Parse(Commit_time1);
            string Update_content = ((TextBox)this.gv_approve.Rows[i].FindControl("Update_content")).Text.Trim();
            string Update_LB = ((TextBox)this.gv_approve.Rows[i].FindControl("Update_LB")).Text.Trim();
        
            MES.Model.form4_Sale_Customer_LOG model = new MES.Model.form4_Sale_Customer_LOG();
            model.ID = ID;
            model.requestid = requestid;
            model.status_id = status_id;
            model.status_ms = status_ms;
            model.dept = dept;
            model.Update_Engineer = Update_Engineer;
            model.Update_user = Update_user;
            model.Update_username = Update_username;
            model.Receive_time = Receive_time;
            model.Commit_time = Commit_time;
            model.Update_content = Update_content;
            model.Update_LB = Update_LB;

            
            MES.DAL.form4_Sale_Customer_LOG dal = new MES.DAL.form4_Sale_Customer_LOG();

            if (((TextBox)this.gv_approve.Rows[i].FindControl("ID")).Text == "0")
            {
                dal.Add(model);
            }
            //当前节点赋值时间和内容  && 下一节点赋值接收时间
            else if(status_id == Lab_Status_id.Text || Convert.ToInt32(status_id) == Convert.ToInt32(Lab_Status_id.Text)+1)
            {
                dal.Update(model);
            }
            //dal.Add(model);

        }

    }
    protected void BTN_Sales_sub_Click(object sender, EventArgs e)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            //验证申请工厂
            string cm_domain = "";
            for (int i = 0; i < CBL_cm_domain.Items.Count; i++)
            {
                if (CBL_cm_domain.Items[i].Selected)
                {
                    cm_domain += ";" + CBL_cm_domain.Items[i].Value;
                }
            }
            if(cm_domain=="")
            {
                yq_zhankai();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('必须选择至少一个申请工厂！')", true);
                return;
            }
            //自动获取流程有问题时验证
            if (gv_approve.Rows.Count < 1)
            {
                yq_zhankai();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('流程设定有异常，请联系管理员！')", true);
                return;
            }
                if (gv_DebtorShipTo.Rows.Count > 0)
            {
                for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
                {
                    //防呆
                    if (((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToCode")).Text == ""
                        || ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToName")).Text == ""
                        || ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("cm_addr")).Text == ""
                        || ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressCity")).Text == ""
                        || ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Text == ""
                        || ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Text == ""
                        || ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Text == ""
                        || ((CheckBoxList)this.gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).Text == ""
                        || ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("AddressStreet1")).Text == ""
                        //|| ((TextBox)this.gv_DebtorShipTo.Rows[i].FindControl("txt_Debtor_Domain")).Text == ""
                        || ((DropDownList)this.gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Text == "")
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                        yq_zhankai();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写完整客户货物发往信息！')", true);
                        return;
                    }

                }

            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
                yq_zhankai();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请至少维护一项客户货物发往工厂！')", true);
                return;
         
            }
            if (BTN_Sales_submit.Text == "提交")
            {
                int requestid_sq = 0;
                if (Request["requestid"] == null)
                {
                    //新增的表单产生序列号requestid
                    string sql = "Select next value for  [dbo].[form4_requestid_sqc]";
                    requestid_sq = Convert.ToInt32(DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    requestid_sq = Convert.ToInt32(Request["requestid"]);
                }

                if (requestid_sq != 0)
                {
                    //插入主表信息
                    //最后一笔主表数据插入log表
                    insert_mstr(requestid_sq);
                    insert_mstr_log(requestid_sq);
                    //插入发往信息
                    //最后一笔发往数据插入log表
                    insert_gv_DebtorShipTo(requestid_sq);
                    insert_gv_DebtorShipTo_log(requestid_sq);
                    //插入签核信息
                    insert_gv_gv_approve(requestid_sq);
                    //插入日志
                    insert_form3_Sale_Product_LOG(requestid_sq, "提交", "客户新增");
                    yq_zhankai();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                    gv_approve_comit_yingchang();
                    //发送邮件
                    SendMail(requestid_sq);
                }
                else
                {
                    yq_zhankai();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交或联系管理员！')", true);
                }
            }

            ts.Complete();
        }

    }
    //页面加载
    public void gvbangding(int requestid,int turns)
    {
        //加载发往工厂
        DataTable dt_fwgc = Customer_CLASS.Getgv(requestid, "form4_Customer_DebtorShipTo",turns);
        gv_DebtorShipTo.DataSource = dt_fwgc;
        gv_DebtorShipTo.DataBind();
        ViewState["dtht"] = dt_fwgc;
        DDL_gv_DebtorShipTo();
    }
    public void gvcolour()
    {
        for (int i = 0; i < gv_DebtorShipTo.Rows.Count; i++)
        {
            string turns = ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("turns")).Text;
            string IsEffective = ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Text;
            if (turns == Lab_turns.Text)
            {
                //gv_DebtorShipTo.Rows[i].Cells[5].BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("BusinessRelationCode")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("ShipToNum")).BackColor = System.Drawing.Color.Red;
                //((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressTypeCode")).BackColor = System.Drawing.Color.Red;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToCode")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToName")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("cm_addr")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressZip")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressCity")).BackColor = System.Drawing.Color.Red;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("cm_region")).BackColor = System.Drawing.Color.Red;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).BackColor = System.Drawing.Color.Red;
                ((CheckBoxList)gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressStreet1")).BackColor = System.Drawing.Color.Red;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("UpdateDate")).BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("UpdateByName")).BackColor = System.Drawing.Color.Red;

                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Style.Add("background-color", "#Red");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Style.Add("background-color", "#Red");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Style.Add("background-color", "#Red");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Style.Add("background-color", "#Red");
            }
            if (IsEffective == "无效")
            {
                //gv_DebtorShipTo.Rows[i].Cells[5].BackColor = System.Drawing.Color.Red;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("BusinessRelationCode")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("ShipToNum")).BackColor = System.Drawing.Color.Gray;
                //((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressTypeCode")).BackColor = System.Drawing.Color.Gray;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToCode")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("DebtorShipToName")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("cm_addr")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressZip")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressCity")).BackColor = System.Drawing.Color.Gray;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("cm_region")).BackColor = System.Drawing.Color.Gray;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).BackColor = System.Drawing.Color.Gray;
                ((CheckBoxList)gv_DebtorShipTo.Rows[i].FindControl("CBL_Debtor_Domain")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("AddressStreet1")).BackColor = System.Drawing.Color.Gray;
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("UpdateDate")).BackColor = System.Drawing.Color.Gray;
                ((TextBox)gv_DebtorShipTo.Rows[i].FindControl("UpdateByName")).BackColor = System.Drawing.Color.Gray;

                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_Lctry_country")).Style.Add("background-color", "#Gray");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_LngCode")).Style.Add("background-color", "#Gray");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_AddressTypeCode")).Style.Add("background-color", "#Gray");
                ((DropDownList)gv_DebtorShipTo.Rows[i].FindControl("ddl_IsEffective")).Style.Add("background-color", "#Gray");
            }
        }
    }
    

public void gv_log(int requestid,int turns)
    {
        //加载log
        DataTable dt_log = Customer_CLASS.Getgv(requestid, "form4_Sale_Customer_LOG", turns);
        gv_approve.DataSource = dt_log;
        gv_approve.DataBind();
        gv_approve.PageSize = 100;
    }
    public void Query_gv(int requestid,int truns)
    {
        gvbangding(requestid,truns);
    
    }
    public static string Get_value_CheckBoxList(CheckBoxList chkl)
    {
        string value = "";
        for (int i = 0; i < chkl.Items.Count; i++)
        {
            if (chkl.Items[i].Selected)
            {
                value += ";" + chkl.Items[i].Value;
            }
        }
        return value;
    }
    private void ShowInfo_mstr(int requestid)
    {
        MES.DAL.form4_Customer_mstr dal = new MES.DAL.form4_Customer_mstr();
        MES.Model.form4_Customer_mstr model = dal.GetModel(requestid);

        this.txtCode.Value = model.Code;
        this.txtCreateDate.Value = model.CreateDate.ToString();
        this.txtUserid.Value = model.Userid;
        this.txtUserName.Value = model.UserName;
        this.txtUserName_AD.Value = model.UserName_AD;
        this.txtdept.Value = model.dept;
        this.txtmanagerid.Value = model.managerid;
        this.txtmanager.Value = model.manager;
        this.txtmanager_AD.Value = model.manager_AD;
        this.DDL_Class.SelectedValue = model.Class;
        this.txtCmClassID.Value = model.CmClassID;
        this.DDL_cmClassName.Text = model.cmClassName;
        //this.CBL_cm_domain.Text = model.cm_domain;

        //获取工厂清单
        string cm_domain = model.cm_domain;//从数据库读取值
        string[] str = cm_domain.Split(';');//调用Split方法，定义string[]类型的变量
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = 0; j < CBL_cm_domain.Items.Count; j++)//循环遍历在页面中CheckBoxList里的所有项
            {
                if (this.CBL_cm_domain.Items[j].Value == str[i])//判断i遍历后的id与cbx当中的值是否有相同
                {
                    this.CBL_cm_domain.Items[j].Selected = true; //相同就选中
                }
            }
        }
        this.txtBusinessRelationCode.Value = model.BusinessRelationCode;
        this.txtBusinessRelationName1.Value = model.BusinessRelationName1;
        this.txtAddressSearchName.Value = model.AddressSearchName;
        this.txtAddressTypeCode.Value = model.AddressTypeCode;
        this.txtAddressStreet1.Value = model.AddressStreet1;
        this.DDLcm_lang.SelectedValue = model.cm_lang;
        this.DDLtry_country.Text = model.cm_region;
        this.txtcm_region.Value = model.cm_region;
        this.txtAddressZip.Value = model.AddressZip;
        this.txtAddressCity.Value = model.AddressCity;
        this.txtAddressState.Value = model.AddressState;
        this.txtad_fax.Value = model.ad_fax;
        this.txtad_phone.Value = model.ad_phone;
        this.txtAddressEMail.Value = model.AddressEMail;
        this.txtContactName.Value = model.ContactName;
        this.DDLContactGender.SelectedValue = model.ContactGender;
        this.txtContactTelephone.Value = model.ContactTelephone;
        this.txtContactEmail.Value = model.ContactEmail;
        //this.txtAddressIsTaxable.Text = model.AddressIsTaxable;
        if (model.AddressIsTaxable == "1")
        {
            CBAddressIsTaxable.Checked = true;
        }
        else
        {
            CBAddressIsTaxable.Checked = false;
        }

        //this.txtAddressIsTaxIncluded.Text = model.AddressIsTaxIncluded;
        if (model.AddressIsTaxIncluded == "1")
        {
            CBAddressIsTaxIncluded.Checked = true;
        }
        else
        {
            CBAddressIsTaxIncluded.Checked = false;
        }

        //this.txtAddressIsTaxInCity.Text = model.AddressIsTaxInCity;
        if (model.AddressIsTaxInCity == "1")
        {
            CBAddressIsTaxInCity.Checked = true;
        }
        else
        {
            CBAddressIsTaxInCity.Checked = false;
        }
        this.DDLTxzTaxZone.SelectedValue = model.TxzTaxZone;
        this.DDLTxclTaxCls.SelectedValue = model.TxclTaxCls;
        this.DDLLedgerDays.SelectedValue = model.LedgerDays;
        this.DDLcm_slspsn.SelectedValue = model.cm_slspsn;

        this.txtcm_pr_list.Value = model.cm_pr_list;
        //this.txtcm_fix_pr.Text = model.cm_fix_pr;
        if (model.cm_fix_pr == "1")
        {
            CBcm_fix_pr.Checked = true;
        }
        else
        {
            CBcm_fix_pr.Checked = false;
        }

        this.DDLBankNumberIsActive.Text = model.BankNumberIsActive;
        this.txtInvControlGLProfileCode.Value = model.InvControlGLProfileCode;
        this.txtCnControlGLProfileCode.Value = model.CnControlGLProfileCode;
        this.txtPrepayControlGLProfileCode.Value = model.PrepayControlGLProfileCode;
        this.txtSalesAccountGLProfileCode.Value = model.SalesAccountGLProfileCode;
        this.DDLReasonCode.SelectedValue = model.ReasonCode;
        this.DDLConditionCode.SelectedValue = model.NormalPaymentConditionCode;
        this.DDLCurrencyCode.SelectedValue = model.CurrencyCode;
        this.txtBankAccFormatCode.Value = model.BankAccFormatCode;
        StringBuilder sql = new StringBuilder();
        sql.Append("  SELECT  BankAccFormatDescription  FROM  qad_BankAccFormat_all where BankAccFormatCode='"+ txtBankAccFormatCode.Value + "' ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.LabBankAccFormatCode.Text = dt.Rows[0]["BankAccFormatDescription"].ToString();
        }
        this.txtBankNumberFormatted.Value = model.BankNumberFormatted;
        this.txtOwnBankNumber.Value = model.OwnBankNumber;
        //新增客户类型
        this.DDL_DebtorTypeCode.SelectedValue = model.DebtorTypeCode;
        this.txt_ExistsClass.Value = model.ExistsClass;
        this.Lab_Status_id.Text = model.Status_id;

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
                if (gv.Rows[j].Cells[i].Controls[1].GetType().Name == "CheckBoxList")
                {
                    CheckBoxList chkl = (CheckBoxList)gv.Rows[j].Cells[i].Controls[1];
                    dr[i] = Get_value_CheckBoxList(chkl);
                }
                else
                {

                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void gv_DebtorShipTo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView dr = e.Row.DataItem as DataRowView;
            //添加可点击Link 及前端识别属性：name
            for (int i = 10; i < e.Row.Cells.Count - 2; i++)
            {

                GridViewRow hr = gv_DebtorShipTo.HeaderRow;
                string headValue = hr.Cells[i].Text;
                if (IsNumberic(headValue))
                {
                    TextBox txtbox = (TextBox)e.Row.Cells[i].FindControl("QTY_" + headValue);
                    txtbox.Attributes.Add("QTY", "QTY");// 包含QTY属性的input
                    txtbox.Attributes.Add("rowvalue", dr.Row["ID"].ToString());
                    txtbox.Attributes.Add("headvalue", headValue);//列值

                }


            }
            //复选框添加或者删除行时重新绑定
            if (((TextBox)e.Row.FindControl("txt_Debtor_Domain")).Text != "")
            {
                int count = ((CheckBoxList)e.Row.FindControl("CBL_Debtor_Domain")).Items.Count;
                string Debtor_Domain = ((TextBox)e.Row.FindControl("txt_Debtor_Domain")).Text;
                string[] str = Debtor_Domain.Split(';');//调用Split方法，定义string[]类型的变量
                for (int k = 0; k < str.Length; k++)
                {
                    for (int j = 0; j < count; j++)//循环遍历在页面中CheckBoxList里的所有项
                    {
                        if (((CheckBoxList)e.Row.FindControl("CBL_Debtor_Domain")).Items[j].Value == str[k])//判断i遍历后的id与cbx当中的值是否有相同
                        {
                            ((CheckBoxList)e.Row.FindControl("CBL_Debtor_Domain")).Items[j].Selected = true; //相同就选中
                        }
                    }
                }

            }





        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {



        }
    }
    public static bool IsNumberic(string _string)
    {
        if (string.IsNullOrEmpty(_string))
            return false;
        int i = 0;
        return int.TryParse(_string, out i);
    }
    public void customer_isnew()
    {
        lab_custumer_name_new.Visible = true;
        txt_ExistsClass.Visible = true;
        txt_ExistsClass.Disabled = false;
        //this.txt_ExistsClass.Style.Add("background-color", "#FF,00,00");
        DDL_cmClassName.Enabled = false;
        DDL_cmClassName.Visible = false;
        lab_custumer_name.Visible = false;
    }
    public void customer_isold()
    {
        lab_custumer_name_new.Visible = false;
        txt_ExistsClass.Visible = false;
        DDL_cmClassName.Visible = true;
        DDL_cmClassName.Enabled = true;
        lab_custumer_name.Visible = true;
    }

    protected void DDL_Class_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDL_Class.SelectedValue == "新增客户大类")
        {
            customer_isnew();
            DDL_cmClassName.SelectedValue = "";
            StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
            sql.Append("  SELECT   MAX(Value) + 1 AS maxdm  FROM      Baojia_base ");
            sql.Append("  WHERE   (classify = 'Customer') AND (TYPE = 'Customer') AND (Value NOT LIKE '3%') ");
            DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
            txtCmClassID.Value = dt.Rows[0]["maxdm"].ToString();
            txtBusinessRelationCode.Value = txtCmClassID.Value + "01";
            txtcm_pr_list.Value = txtBusinessRelationCode.Value;


        }
        else
        {
            customer_isold();
            txt_ExistsClass.Value = "";
            txtCmClassID.Value = "";
            txtBusinessRelationCode.Value = "";
            txtcm_pr_list.Value = "";



        }
        yq_zhankai();
    }

    protected void DDL_custumer_name_SelectedIndexChanged(object sender, EventArgs e)
    {


        StringBuilder sqldm = new StringBuilder();//,
        sqldm.Append("  SELECT * FROM [MES].[dbo].[Baojia_base] where type='Customer' and  classify='Customer' AND isyx='Y'   ");
        sqldm.Append("  and    name ='" + DDL_cmClassName.SelectedValue + "'  ");
        DataTable dtdm = DbHelperSQL.Query(sqldm.ToString()).Tables[0];
        txtCmClassID.Value = dtdm.Rows[0]["Value"].ToString();

        StringBuilder sqlxh = new StringBuilder();//
        sqlxh.Append("  SELECT DM,MAX(XH) AS XH FROM [MES].[dbo].[V_form4_Customer_XH]   ");
        sqlxh.Append("  WHERE   DM='" + txtCmClassID.Value + "' GROUP BY DM ");
        DataTable dtxh = DbHelperSQL.Query(sqlxh.ToString()).Tables[0];
        String DM = txtCmClassID.Value;
        String XH = dtxh.Rows[0]["XH"].ToString();
        txtBusinessRelationCode.Value = Convert.ToString(Convert.ToInt32(DM + XH) + 1);
        txtcm_pr_list.Value = txtBusinessRelationCode.Value;

        yq_zhankai();
    }




    protected void DDL_try_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtcm_region.Value = DDLtry_country.SelectedValue;
        yq_zhankai();
    }

    protected void ddl_Lctry_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList t = (DropDownList)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        string value = ((DropDownList)this.gv_DebtorShipTo.Rows[rowIndex].FindControl("ddl_Lctry_country")).SelectedValue;
        ((TextBox)this.gv_DebtorShipTo.Rows[rowIndex].FindControl("cm_region")).Text = value;
        yq_zhankai();
    }



    public void insert_form3_Sale_Product_LOG(int requestid, string Update_LB, string Update_content)
    {
        Customer_CLASS.insert_form_LOG(requestid, txt_update_user_job.Value, txt_update_user.Value, txt_update_user_name.Value, Update_LB, Update_content);
    }

    protected void gv_approve_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "comit")
        {
            object obj = e.CommandArgument;
            int index = Convert.ToInt32(e.CommandArgument);
            string sign_desc = ((TextBox)gv_approve.Rows[index].FindControl("Update_content")).Text;

            if (sign_desc == "")
            {
                ViewState["lv"] = "approve";
                yq_zhankai();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('提交说明不能为空！')", true);

                ((Label)gv_approve.Rows[index].FindControl("Lab_yz")).Visible = true;
                return;
                
            }
            else
            {
                int requestid = Convert.ToInt32(Request["requestid"]);
                //申请时执行
                if (Request["requestid"] == null )
                {
                    BTN_Sales_sub_Click(null, null);
           
                }
                //申请时执行
                else if (Request["requestid"] != null &&  Request["update"] != null)
                {
                    BTN_Sales_sub_Click(null, null);
             
                }
                //审批时执行
                else
                {
                    //如果为流程中但不是最后一个节点
                    if (Convert.ToInt32(this.Lab_Status_id.Text) < 4 && Convert.ToInt32(this.Lab_Status_id.Text) >0)
                    {
                        ((TextBox)gv_approve.Rows[index].FindControl("Commit_time")).Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ((TextBox)gv_approve.Rows[index + 1].FindControl("Receive_time")).Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //最后一个节点
                    else
                    {
                        ((TextBox)gv_approve.Rows[index].FindControl("Commit_time")).Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //插入新增客户大类到类别中
                        if(DDL_Class.SelectedValue== "新增客户大类" && txt_ExistsClass.Value!="" && txtCmClassID.Value !="")
                        {
                            //获取客户大类最大ID
                            StringBuilder sql = new StringBuilder();
                            sql.Append("  SELECT max(ID) AS ID FROM [MES].[dbo].[Baojia_base]  where classify='Customer' and type='Customer' ");
                            DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
                            int ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString())+1;
                            //插入大类
                            Customer_CLASS.insert_ExistsClass(ID, txt_ExistsClass.Value, txtCmClassID.Value);
                        }
                     
                    }
                    //插入主表信息
                    insert_mstr(requestid);
                    insert_mstr_log(requestid);
                    //插入签核信息
                    insert_gv_gv_approve(requestid);
                    //插入日志
                    insert_form3_Sale_Product_LOG(Convert.ToInt32(Request["requestid"]), "审批", "审批新增客户");
                    yq_zhankai();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                    gv_approve_comit_yingchang();
                    //发送邮件
                    SendMail(requestid);

                }
        
            }

        }
        yq_zhankai();
    }



    public void SendMail(int requestid)
    {
        string status_id = Convert.ToString(Convert.ToInt32(Lab_Status_id.Text) + 1);
        //string status_id = Convert.ToString(Convert.ToInt32(Lab_Status_id.Text) );
        StringBuilder sql = new StringBuilder();
        sql.Append("  SELECT * FROM [MES].[dbo].[form4_Sale_Customer_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  and Update_LB=" + Lab_turns.Text + " and status_id=" + status_id + " ");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            //工号
            string Update_user = dt.Rows[0]["Update_user"].ToString();
            DataTable dtemp = Customer_CLASS.emp(Update_user);
            //邮件地址
            string usermail = dtemp.Rows[0]["email"].ToString();
            //string usermail = "angela.xu@pgi.cn";
            //姓名
            string username = dt.Rows[0]["Update_username"].ToString();
            //操作事项
            string work = dt.Rows[0]["status_ms"].ToString();
            //角色
            string job = dt.Rows[0]["Update_Engineer"].ToString();
            //接收时间
            string Receive_time = dt.Rows[0]["Receive_time"].ToString();
            //主题
            string lstitle = "客户管理系统待签核邮件通知--由" + txtUserName.Value + "申请";
            //链接code
            string BusinessRelationCode = txtBusinessRelationCode.Value;
            string LINKCode = "<a href='http://172.16.5.26:8010/sales/Customer_QAD.aspx?requestid=" + requestid + "'>" + BusinessRelationCode + "</a>";
            //链接名称
            string LINKName = txtBusinessRelationName1.Value;
            //第一行
            string code = LabBusinessRelationCode.Text;
            string name = LabBusinessRelationName1.Text;

            //邮件发送
            string body = "客户管理系统：未完成事项请尽快处理!";
            Mail.SendMail(usermail, lstitle, LINKCode, LINKName, work, job, username, Receive_time, code, name, body);

            //邮件存log
            int requestid_1 = requestid;
            string code_1 = txtCode.Value;
            Mail.insert_A_MES_Send_mail_log(requestid_1, code_1, usermail, username, Receive_time,work, job,"即时发送");
        }
    }




 
}


