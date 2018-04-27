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

public partial class YangJian : System.Web.UI.Page
{
    YJ_CLASS YJ_CLASS = new YJ_CLASS();
    public static string ctl = "ctl00$MainContent$";
    YJ_Confirm_Action_SQL YJ_Confirm_Action_SQL = new YJ_Confirm_Action_SQL();
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        this.GridView1.PageSize = 200;
        this.GridView2.PageSize = 100;
        this.GridView3.PageSize = 100;
        this.GridView3.ShowHeader = false;
        if (Session["empid"] == null || Session["job"] == null|| Session["empid"] == "" || Session["job"] == "")
        {   // 给Session["empid"] & Session["job"] 初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {

            ViewState["V_cpgcs"] = "";
            ViewState["V_wljh"] = "";

            ViewState["empid"] = Session["empid"];
            ViewState["job"] = Session["job"];
            //测试数据


            //ViewState["empid"] = "00698";
            //ViewState["job"] = "检验班长";
            //ViewState["empid"] = "00997";
            //ViewState["job"] = "仓库班长";
           
            if (Session["empid"] == null || Session["empid"] == "")
            {
                ViewState["empid"] = "01746";

            }
            else
            {
                ViewState["empid"] = Session["empid"];
  

            }
            DDL();
            button();

            if (Request["requestid"] != null)
            {
                if (ViewState["empid"] != null)
                {
                    txt_update_user.Value = ViewState["empid"].ToString();
                    Query();                  
                    XS_BQ();
                    Fileload();
                    GeLOG();
                    Getstatus();
                    if (Convert.ToInt32(txt_status_id.Text) < 5)
                    {
                        Get_btn();
                        Get_Process_intervention();
                    }
                    if (txt_iscsdd.Text == "0")
                    {
                        txt_iscsdd_sm.Text = "未触发";
                    }
                    else
                    {
                        txt_iscsdd_sm.Text = "已触发";
                        BTN_Sales_Assistant2_CS.Text = "QAD订单已生成";
                        BTN_Sales_Assistant2_CS.Enabled = false;
                    }

                    //测试
                    //LB_Process_intervention.Visible = true;
                    //txt_Process_intervention.Visible = true;
                    //txt_Process_intervention.Visible = true;
                    //BTN_Process_intervention.Visible = true;
                    //txt_gysm.Visible = true;



                    //Getstatus_job();

                }
                //BTN_Warehouse_Keeper_DY.Visible = true;
                //gy_zl.Visible = true;
                //BTN_Process_intervention_zl.Visible = true;
            }
            else
            {


                if (ViewState["empid"] != null)
                {
                    button_Assistant_01();
                    BTN_Sales_Assistant1.Text = "提交";
                    this.txt_CreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                    txt_Code.Value = "YJ" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    txt_Sorting_list.Value = txt_Code.Value;
                    txt_Userid.Value = ViewState["empid"].ToString();
                    DataTable dtemp = YJ_CLASS.YJ_emp(txt_Userid.Value);
                    txt_UserName.Value = dtemp.Rows[0]["lastname"].ToString();
                    txt_UserName_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                    txt_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                    txt_managerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                    txt_manager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                    txt_manager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();
                    txt_status_id.Text = "-1";
                    txt_status_name.Text = "订单输入";
                    dtemp.Clear();
                    txt_iscsdd_sm.Text = "未触发";
                }

            }




        }
    }

    public void Sales_Assistant_fj()
    {
        txt_ddfj.Visible = true;
        Btn_ddfj.Visible = true;
        gvFile_ddfj.Columns[2].Visible = true;
        txt_yqfj.Visible = true;
        Btn_yqfj.Visible = true;
        gvFile_yqfj.Columns[2].Visible = false;
        txt_bqfj.Visible = true;
        Btn_bqfj.Visible = true;
        gvFile_bqfj.Columns[2].Visible = true;

    }
    public void Project_Engineer_fj()
    {
        //--项目
        txt_special_fj.Visible = true;
        Btn_special_fj.Visible = true;
        gvFile_special_fj.Columns[2].Visible = true;
    }
    public void Warehouse_Keeper_fj()
    {
        //--仓库
        txt_Package_photo.Visible = true;
        Btn_Package_photo.Visible = true;
        gvFile_Package_photo.Columns[2].Visible = true;
        txt_Shipping_photos.Visible = true;
        Btn_Shipping_photos.Visible = true;
        gvFile_Shipping_photos.Columns[2].Visible = true;
    }
    public void Quality_Engineer_fj()
    {

        //--质量
        txt_check_fj.Visible = true;
        Btn_check_fj.Visible = true;
        gvFile_check_fj.Columns[2].Visible = true;
    }
    public void Fileload()
    {

        ShowFileload(txt_Code.Value, "txt_ddfj", gvFile_ddfj);
        ShowFileload(txt_Code.Value, "txt_yqfj", gvFile_yqfj);
        ShowFileload(txt_Code.Value, "txt_bqfj", gvFile_bqfj);
        ShowFileload(txt_Code.Value, "txt_special_fj", gvFile_special_fj);
        ShowFileload(txt_Code.Value, "txt_Package_photo", gvFile_Package_photo);
        ShowFileload(txt_Code.Value, "txt_Shipping_photos", gvFile_Shipping_photos);
        ShowFileload(txt_Code.Value, "txt_check_fj", gvFile_check_fj);
    }
    public void Query()
    {
        int requestid = Convert.ToInt32(Request["requestid"]);
        DataTable dt = YJ_Confirm_Action_SQL.Form1_YJ_Query_PRO(requestid);
        txt_Userid.Value = dt.Rows[0]["Userid"].ToString();
        txt_UserName.Value = dt.Rows[0]["UserName"].ToString();
        txt_UserName_AD.Value = dt.Rows[0]["UserName_AD"].ToString();
        txt_dept.Value = dt.Rows[0]["dept"].ToString();
        txt_managerid.Value = dt.Rows[0]["managerid"].ToString();
        txt_manager.Value = dt.Rows[0]["manager"].ToString();
        txt_manager_AD.Value = dt.Rows[0]["manager_AD"].ToString();
        txt_Code.Value = dt.Rows[0]["Code"].ToString();
        if (dt.Rows[0]["CreateDate"].ToString() != "")
        {
            txt_CreateDate.Value = Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString()).ToString("yyyy-MM-dd");
        }
        txt_domain.Value = dt.Rows[0]["domain"].ToString();
        txt_sqgc.Value = dt.Rows[0]["sqgc"].ToString();
        txt_zzgc.Value = dt.Rows[0]["zzgc"].ToString();
        txt_domain_zzgc.Value = dt.Rows[0]["domain_zzgc"].ToString();
        txt_gkddh.Value = dt.Rows[0]["gkddh"].ToString();

        txt_Line_Code.Value = dt.Rows[0]["Line_Code"].ToString();
        txt_ddlxr.Value = dt.Rows[0]["ddlxr"].ToString();
        txt_ddlxphone.Value = dt.Rows[0]["ddlxphone"].ToString();
        txt_Serial_No.SelectedValue = dt.Rows[0]["Serial_No"].ToString();
        txt_Per_Crate_Qty.Value = dt.Rows[0]["Per_Crate_Qty"].ToString();
        txt_Per_Crate_Qty2.Value = dt.Rows[0]["Per_Crate_Qty2"].ToString();

        if (dt.Rows[0]["sddd_date"].ToString() != "")
        {
            txt_sddd_date.Value = Convert.ToDateTime(dt.Rows[0]["sddd_date"].ToString()).ToString("yyyy-MM-dd");
        }

        //txt_ddfj.Value = dt.Rows[0]["ddfj"].ToString();
        //txt_CP_ID.Text = dt.Rows[0]["CP_ID"].ToString();
        txt_xmh.Value = dt.Rows[0]["xmh"].ToString();
        txt_ljh.Value = dt.Rows[0]["ljh"].ToString();
        txt_ljmc.Value = dt.Rows[0]["ljmc"].ToString();
        txt_ljzl.Value = dt.Rows[0]["ljzl"].ToString();
        txt_fhz.Value = dt.Rows[0]["fhz"].ToString();
        txt_yhsl.Text = dt.Rows[0]["yhsl"].ToString();
        txt_kcl.Value = dt.Rows[0]["kcl"].ToString();
        if (dt.Rows[0]["kc_date"].ToString() != "")
        {
            txt_kc_date.Value = Convert.ToDateTime(dt.Rows[0]["kc_date"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["tz_date"].ToString() != "")
        {
            txt_tz_date.Value = Convert.ToDateTime(dt.Rows[0]["tz_date"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["yqdh_date"].ToString() != "")
        {
            txt_yqdh_date.Value = Convert.ToDateTime(dt.Rows[0]["yqdh_date"].ToString()).ToString("yyyy-MM-dd");
        }

        txt_gysdm.Value = dt.Rows[0]["gysdm"].ToString();
        txt_xmjd.Value = dt.Rows[0]["xmjd"].ToString();
        txt_gkdm.Value = dt.Rows[0]["gkdm"].ToString();
        txt_gkmc.Value = dt.Rows[0]["gkmc"].ToString();
        txt_hb.Value = dt.Rows[0]["hb"].ToString();
        txt_ljdj.Text = dt.Rows[0]["ljdj"].ToString();
        txt_ljdj_qad.Text = dt.Rows[0]["ljdj_qad"].ToString();

        if (txt_ljdj_qad.Text != txt_ljdj.Text)
        {
            txt_ljdj.Style.Add("color", "Red");
        }
        txt_ljzj.Value = dt.Rows[0]["ljzj"].ToString();
        txt_sktj.SelectedValue = dt.Rows[0]["sktj"].ToString();
        txt_ysfs.SelectedValue = dt.Rows[0]["ysfs"].ToString();
        if (dt.Rows[0]["yqfy_date"].ToString() != "")
        {
            txt_yqfy_date.Text = Convert.ToDateTime(dt.Rows[0]["yqfy_date"].ToString()).ToString("yyyy-MM-dd");
        }
        txt_yfzffs.SelectedValue = dt.Rows[0]["yfzffs"].ToString();
        txt_yfje.Value = dt.Rows[0]["yfje"].ToString();
        txt_ystk.SelectedValue = dt.Rows[0]["ystk"].ToString();
        txt_shrxx.Value = dt.Rows[0]["shrxx"].ToString();
        txt_shdz.Value = dt.Rows[0]["shdz"].ToString();
        txt_yq.Value = dt.Rows[0]["yq"].ToString();
        //txt_yqfj.Value = dt.Rows[0]["yqfj"].ToString();
        txt_bqyq.Value = dt.Rows[0]["bqyq"].ToString();
        //txt_bqfj.Value = dt.Rows[0]["bqfj"].ToString();
        txt_shwjyq.Value = dt.Rows[0]["shwjyq"].ToString();
        txt_other_yq.Value = dt.Rows[0]["other_yq"].ToString();
        txt_Sales_engineer_job.Value = dt.Rows[0]["Sales_engineer_job"].ToString();
        txt_Sales_engineer_id.Value = dt.Rows[0]["Sales_engineer_id"].ToString();
        txt_Sales_engineer.Value = dt.Rows[0]["Sales_engineer"].ToString();
        txt_Sales_engineer_AD.Value = dt.Rows[0]["Sales_engineer_AD"].ToString();
        txt_project_engineer_job.Value = dt.Rows[0]["project_engineer_job"].ToString();
        txt_project_engineer_id.Value = dt.Rows[0]["project_engineer_id"].ToString();
        txt_project_engineer.Value = dt.Rows[0]["project_engineer"].ToString();
        txt_project_engineer_AD.Value = dt.Rows[0]["project_engineer_AD"].ToString();
        txt_product_engineer_job.Value = dt.Rows[0]["product_engineer_job"].ToString();
        txt_product_engineer_id.Value = dt.Rows[0]["product_engineer_id"].ToString();
        txt_product_engineer.Value = dt.Rows[0]["product_engineer"].ToString();
        txt_product_engineer_AD.Value = dt.Rows[0]["product_engineer_AD"].ToString();
        txt_quality_engineer_job.Value = dt.Rows[0]["quality_engineer_job"].ToString();
        txt_quality_engineer_id.Value = dt.Rows[0]["quality_engineer_id"].ToString();
        txt_quality_engineer.Value = dt.Rows[0]["quality_engineer"].ToString();
        txt_quality_engineer_AD.Value = dt.Rows[0]["quality_engineer_AD"].ToString();
        txt_checker_monitor_job.Value = dt.Rows[0]["checker_monitor_job"].ToString();
        txt_checker_monitor_id.Value = dt.Rows[0]["checker_monitor_id"].ToString();
        txt_checker_monitor.Value = dt.Rows[0]["checker_monitor"].ToString();
        txt_checker_monitor_AD.Value = dt.Rows[0]["checker_monitor_AD"].ToString();

        txt_Packaging_engineer_job.Value = dt.Rows[0]["Packaging_engineer_job"].ToString();
        txt_Packaging_engineer_id.Value = dt.Rows[0]["Packaging_engineer_id"].ToString();
        txt_Packaging_engineer.Value = dt.Rows[0]["Packaging_engineer"].ToString();
        txt_Packaging_engineer_AD.Value = dt.Rows[0]["Packaging_engineer_AD"].ToString();
        txt_planning_engineer_job.Value = dt.Rows[0]["planning_engineer_job"].ToString();
        txt_planning_engineer_id.Value = dt.Rows[0]["planning_engineer_id"].ToString();
        txt_planning_engineer.Value = dt.Rows[0]["planning_engineer"].ToString();
        txt_planning_engineer_AD.Value = dt.Rows[0]["planning_engineer_AD"].ToString();

        txt_warehouse_keeper_job.Value = dt.Rows[0]["warehouse_keeper_job"].ToString();
        txt_warehouse_keeper_id.Value = dt.Rows[0]["warehouse_keeper_id"].ToString();
        txt_warehouse_keeper.Value = dt.Rows[0]["warehouse_keeper"].ToString();
        txt_warehouse_keeper_AD.Value = dt.Rows[0]["warehouse_keeper_AD"].ToString();

        if (dt.Rows[0]["check_require_jy"].ToString() != "")
        {
            txt_check_require_jy.Value = Convert.ToDateTime(dt.Rows[0]["check_require_jy"].ToString()).ToString("yyyy-MM-dd");

        }
        if (dt.Rows[0]["check_complete_jy"].ToString() != "")
        {
            txt_check_complete_jy.Value = Convert.ToDateTime(dt.Rows[0]["check_complete_jy"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_check_require_jy.Value), Convert.ToDateTime(txt_check_complete_jy.Value)) < 0)
            {

                txt_check_complete_jy.Style.Add("color", "Red");
            }
        }
        if (dt.Rows[0]["goods_require_wl"].ToString() != "")
        {
            txt_goods_require_wl.Value = Convert.ToDateTime(dt.Rows[0]["goods_require_wl"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["goods_complete_wl"].ToString() != "")
        {
            txt_goods_complete_wl.Value = Convert.ToDateTime(dt.Rows[0]["goods_complete_wl"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_goods_require_wl.Value), Convert.ToDateTime(txt_goods_complete_wl.Value)) < 0)
            {

                txt_goods_complete_wl.Style.Add("color", "Red");
            }
        }

        if (dt.Rows[0]["Packaging_require"].ToString() != "")
        {
            txt_Packaging_require.Value = Convert.ToDateTime(dt.Rows[0]["Packaging_require"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["Packaging_complete"].ToString() != "")
        {
            txt_Packaging_complete.Value = Convert.ToDateTime(dt.Rows[0]["Packaging_complete"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_Packaging_require.Value), Convert.ToDateTime(txt_Packaging_complete.Value)) < 0)
            {

                txt_Packaging_complete.Style.Add("color", "Red");
            }
        }
        if (dt.Rows[0]["goods_require"].ToString() != "")
        {
            txt_goods_require.Value = Convert.ToDateTime(dt.Rows[0]["goods_require"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["goods_complete"].ToString() != "")
        {
            txt_goods_complete.Value = Convert.ToDateTime(dt.Rows[0]["goods_complete"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_goods_require.Value), Convert.ToDateTime(txt_goods_complete.Value)) < 0)
            {

                txt_goods_complete.Style.Add("color", "Red");
            }
        }
        if (dt.Rows[0]["check_require_zl"].ToString() != "")
        {
            txt_check_require.Value = Convert.ToDateTime(dt.Rows[0]["check_require_zl"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["check_complete_zl"].ToString() != "")
        {
            txt_check_complete.Value = Convert.ToDateTime(dt.Rows[0]["check_complete_zl"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_check_require.Value), Convert.ToDateTime(txt_check_complete.Value)) < 0)
            {

                txt_check_complete.Style.Add("color", "Red");
            }
        }
        if (dt.Rows[0]["special_require"].ToString() != "")
        {
            txt_special_require.Value = Convert.ToDateTime(dt.Rows[0]["special_require"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["customer_request_complete"].ToString() != "")
        {
            txt_customer_request_complete.Value = Convert.ToDateTime(dt.Rows[0]["customer_request_complete"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_special_require.Value), Convert.ToDateTime(txt_customer_request_complete.Value)) < 0)
            {

                txt_customer_request_complete.Style.Add("color", "Red");
            }
        }
        if (dt.Rows[0]["shipping_require"].ToString() != "")
        {
            txt_shipping_require.Value = Convert.ToDateTime(dt.Rows[0]["shipping_require"].ToString()).ToString("yyyy-MM-dd");
        }
        if (dt.Rows[0]["shipping_complete"].ToString() != "")
        {
            txt_shipping_complete.Value = Convert.ToDateTime(dt.Rows[0]["shipping_complete"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_shipping_require.Value), Convert.ToDateTime(txt_shipping_complete.Value)) < 0)
            {

                txt_shipping_complete.Style.Add("color", "Red");
            }
        }



        if (dt.Rows[0]["tzfy_date"].ToString() != "")
        {
            txt_tzfy_date.Value = Convert.ToDateTime(dt.Rows[0]["tzfy_date"].ToString()).ToString("yyyy-MM-dd");
        }



        txt_cp_status.SelectedValue = dt.Rows[0]["cp_status"].ToString();
        txt_special_yq.Value = dt.Rows[0]["special_yq"].ToString();
        //txt_special_fj.Value = dt.Rows[0]["special_fj"].ToString();
        txt_customer_request.SelectedValue = dt.Rows[0]["customer_request"].ToString();
        txt_is_check_qcc.SelectedValue = dt.Rows[0]["is_check_qcc"].ToString();
        txt_is_check_szb.SelectedValue = dt.Rows[0]["is_check_szb"].ToString();
        txt_is_check_wg.SelectedValue = dt.Rows[0]["is_check_wg"].ToString();
        txt_is_check_jj.SelectedValue = dt.Rows[0]["is_check_jj"].ToString();
        txt_check_other_ms.Value = dt.Rows[0]["check_other_ms"].ToString();
        txt_check_lj.SelectedValue = dt.Rows[0]["check_lj"].ToString();
        txt_Already_Product.SelectedValue = dt.Rows[0]["Already_Product"].ToString();
        Lb_txt_reference_number.Text = dt.Rows[0]["reference_number"].ToString();
        txt_Stocking_quantity.Value = dt.Rows[0]["Stocking_quantity"].ToString();
        txt_Check_number.Text = dt.Rows[0]["Check_number"].ToString();
        txt_Box_specifications1.SelectedValue = dt.Rows[0]["Box_specifications1"].ToString();
        txt_Other_box_dec1.Value = dt.Rows[0]["Other_box_dec1"].ToString();
        txt_Box_weight1.Value = dt.Rows[0]["Box_weight1"].ToString();
        txt_Box_specifications2.SelectedValue = dt.Rows[0]["Box_specifications2"].ToString();
        txt_Other_box_dec2.Value = dt.Rows[0]["Other_box_dec2"].ToString();
        txt_Box_weight2.Value = dt.Rows[0]["Box_weight2"].ToString();
        txt_Box_weight_total.Value = dt.Rows[0]["Box_weight_total"].ToString();
        txt_Box_quantity.Value = dt.Rows[0]["Box_quantity"].ToString();
        txt_Packing_net_weight.Value = dt.Rows[0]["Packing_net_weight"].ToString();
        txt_Package_weight.Value = dt.Rows[0]["Package_weight"].ToString();
        txt_Packaging_scheme.Value = dt.Rows[0]["Packaging_scheme"].ToString();
        txt_Packing_goods_Already.SelectedValue = dt.Rows[0]["Packing_goods_Already"].ToString();
        txt_Already_Planning.SelectedValue = dt.Rows[0]["Already_Planning"].ToString();
        txt_Current_inventory_quantity.Value = dt.Rows[0]["Current_inventory_quantity"].ToString();
        if (dt.Rows[0]["Current_inventory_date"].ToString() != "")
        {
            txt_Current_inventory_date.Value = Convert.ToDateTime(dt.Rows[0]["Current_inventory_date"].ToString()).ToString("yyyy-MM-dd");
        }
        txt_Stocking_quantity_wl.Value = dt.Rows[0]["Stocking_quantity_wl"].ToString();
        txt_Sorting_list.Value = dt.Rows[0]["Sorting_list"].ToString();
        txt_Already_Check.SelectedValue = dt.Rows[0]["Already_Check"].ToString();
        txt_Warehouse_quantity.Value = dt.Rows[0]["Warehouse_quantity"].ToString();
        //txt_Package_photo.Value = dt.Rows[0]["Package_photo"].ToString();
        //txt_Shipping_photos.Value = dt.Rows[0]["Shipping_photos"].ToString();
        txt_Qualified_quantity.Value = dt.Rows[0]["Qualified_quantity"].ToString();
        txt_Unqualified_quantity.Value = dt.Rows[0]["Unqualified_quantity"].ToString();
        txt_Unqualified_description.Value = dt.Rows[0]["Unqualified_description"].ToString();
        txt_is_check_qcc_zl.SelectedValue = dt.Rows[0]["is_check_qcc_zl"].ToString();
        txt_is_check_szb_zl.SelectedValue = dt.Rows[0]["is_check_szb_zl"].ToString();
        txt_is_check_wg_zl.SelectedValue = dt.Rows[0]["is_check_wg_zl"].ToString();
        txt_is_check_jj_zl.SelectedValue = dt.Rows[0]["is_check_jj_zl"].ToString();
        txt_check_other_ms_zl.Value = dt.Rows[0]["check_other_ms_zl"].ToString();
        txt_check_lj_zl.SelectedValue = dt.Rows[0]["check_lj_zl"].ToString();
        txt_Already_zl.SelectedValue = dt.Rows[0]["Already_zl"].ToString();
        Lb_txt_reference_number_zl.Text = dt.Rows[0]["reference_number_zl"].ToString();
        txt_Check_number_zl.Text = dt.Rows[0]["Check_number_zl"].ToString();
        //txt_check_fj.Value = dt.Rows[0]["check_fj"].ToString();
        txt_Confirm_quantity.Value = dt.Rows[0]["Confirm_quantity"].ToString();
        txt_Unqualified_quantity_zl.Value = dt.Rows[0]["Unqualified_quantity_zl"].ToString();
        txt_status_id.Text = dt.Rows[0]["status_id"].ToString();
        txt_status_name.Text = dt.Rows[0]["status"].ToString();
        txt_iscsdd.Text = dt.Rows[0]["iserp"].ToString();
        txt_qaddh.Value = dt.Rows[0]["QADSO"].ToString();
        DDL_lable_type.SelectedValue = dt.Rows[0]["lable_type_Chrylser"].ToString();
        txt_mdgc_Chrylser.Value = dt.Rows[0]["mdgc_Chrylser"].ToString();
        txt_ENG_GM.Value = dt.Rows[0]["ENG_GM"].ToString();
        DDL_sbh_AAM.SelectedValue = dt.Rows[0]["sbh_AAM"].ToString();
        txt_sbh_HM_AAM.Value = dt.Rows[0]["sbh_HM_AAM"].ToString();
        txt_sbh_NO_AAM.Value = dt.Rows[0]["sbh_NO_AAM"].ToString();
        txt_ZGS_BBAC.Value = dt.Rows[0]["ZGS_BBAC"].ToString();
        txt_EQ_BBAC.Value = dt.Rows[0]["EQ_BBAC"].ToString();
        DDL_Part_Status_Daimler.SelectedValue = dt.Rows[0]["Part_Status_Daimler"].ToString();
        txt_Part_Status_Daimler_ms.Value = dt.Rows[0]["Part_Status_Daimler_ms"].ToString();
        DDL_Part_Change_Daimler.SelectedValue = dt.Rows[0]["Part_Change_Daimler"].ToString();
        DDL_DY.SelectedValue = dt.Rows[0]["DDL_DY"].ToString();

        //
        string BASE_ID = "";
        BASE_ID = DDL_DY.SelectedValue;
        CBL_DY.DataSource = YJ_CLASS.YJ_BASE(BASE_ID);
        CBL_DY.DataValueField = "ID";
        CBL_DY.DataTextField = "CLASS_NAME";
        CBL_DY.DataBind();

        //获取打印标签清单
        string ICBCLevel = dt.Rows[0]["CBL_DY"].ToString();//从数据库读取值
        string[] str = ICBCLevel.Split(';');//调用Split方法，定义string[]类型的变量
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = 0; j < CBL_DY.Items.Count; j++)//循环遍历在页面中CheckBoxList里的所有项
            {
                if (this.CBL_DY.Items[j].Value == str[i])//判断i遍历后的id与cbx当中的值是否有相同
                {
                    this.CBL_DY.Items[j].Selected = true; //相同就选中
                }
            }
        }

        //获取仓库打印标签清单
        if (DDL_DY.SelectedValue == "")
        {
            BASE_dy();
        }
        else
        {
            for (int i = 0; i < this.CBL_DY.Items.Count; i++)
            {
                if (this.CBL_DY.Items[i].Selected)
                {
                    // 列出选定的项
                    DataTable dtDY = YJ_CLASS.YJ_BASE3(this.CBL_DY.Items[i].Value);
                    this.txt_bqdy.Items.Add(new ListItem(this.CBL_DY.Items[i].Text, dtDY.Rows[0]["VALUE"].ToString()));
                }
            }
            this.txt_bqdy.Items.Insert(0, new ListItem("装箱出库单-所有客户", "PrintPackingList.aspx"));
        }




        //CBL_DY.SelectedValue = dt.Rows[0]["CBL_DY"].ToString();
        //确认阶段要求时间显示

        txt_Assistant_job.Value = "销售助理";
        txt_Assistant_id.Value = dt.Rows[0]["Userid"].ToString();
        txt_Assistant.Value = dt.Rows[0]["UserName"].ToString();
        txt_Assistant_AD.Value = dt.Rows[0]["UserName_AD"].ToString();
        //销售助理
        //发货时间确认
        DataTable dt_QR_Assistant = YJ_CLASS.qr_date(requestid, txt_Assistant_id.Value, 0);
        if (dt_QR_Assistant.Rows.Count > 0)
        {
            txt_Assistant_require.Value = Convert.ToDateTime(dt_QR_Assistant.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_Assistant_complete.Value = Convert.ToDateTime(dt_QR_Assistant.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");


            if (DateTime.Compare(Convert.ToDateTime(txt_Assistant_require.Value), Convert.ToDateTime(txt_Assistant_complete.Value)) < 0)
            {
                txt_Assistant_complete.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_Assistant = YJ_CLASS.qr_date_yq(requestid, txt_Assistant_id.Value, 0);
            txt_Assistant_require.Value = Convert.ToDateTime(dt_QR_Assistant.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }
        //订舱申请时间确认
        DataTable dt_QR_Assistant_dcsq = YJ_CLASS.qr_date(requestid, txt_Assistant_id.Value, -4);
        if (dt_QR_Assistant_dcsq.Rows.Count > 0)
        {
            txt_dcsq_require.Value = Convert.ToDateTime(dt_QR_Assistant_dcsq.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_dcsq_complete.Value = Convert.ToDateTime(dt_QR_Assistant_dcsq.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");


            if (DateTime.Compare(Convert.ToDateTime(txt_dcsq_require.Value), Convert.ToDateTime(txt_dcsq_complete.Value)) < 0)
            {
                txt_dcsq_complete.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_Assistant_dcsq = YJ_CLASS.qr_date_yq(requestid, txt_Assistant_id.Value, -4);
            txt_dcsq_require.Value = Convert.ToDateTime(dt_QR_Assistant.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");

        }
        //QAD对接时间确认
        DataTable dt_QR_Assistant_qad = YJ_CLASS.qr_date(requestid, txt_Assistant_id.Value, -6);
        if (dt_QR_Assistant_qad.Rows.Count > 0)
        {
            txt_qad_require.Value = Convert.ToDateTime(dt_QR_Assistant_qad.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_qad_complete.Value = Convert.ToDateTime(dt_QR_Assistant_qad.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");


            if (DateTime.Compare(Convert.ToDateTime(txt_qad_require.Value), Convert.ToDateTime(txt_qad_complete.Value)) < 0)
            {
                txt_qad_complete.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_Assistant_qad = YJ_CLASS.qr_date_yq(requestid, txt_Assistant_id.Value, -6);
            txt_qad_require.Value = Convert.ToDateTime(dt_QR_Assistant.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }
        //销售助理到货日期
        if (dt.Rows[0]["yqdh_date"].ToString() != "")
        {

            txt_dh_date_require.Value = Convert.ToDateTime(dt.Rows[0]["yqdh_date"].ToString()).ToString("yyyy-MM-dd");
        }


        DataTable dt_QR_Assistant3 = YJ_CLASS.qr_date(requestid, txt_Assistant_id.Value, 4);

        if (dt_QR_Assistant3.Rows.Count > 0)
        {
            txt_dh_date_require.Value = Convert.ToDateTime(dt_QR_Assistant3.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_dh_date_complete.Value = Convert.ToDateTime(dt_QR_Assistant3.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_dh_date_require.Value), Convert.ToDateTime(txt_dh_date_complete.Value)) < 0)
            {
                txt_dh_date_complete.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_Assistant3 = YJ_CLASS.qr_date_yq(requestid, txt_Assistant_id.Value, 4);
            txt_dh_date_require.Value = Convert.ToDateTime(dt_QR_Assistant3.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }
        //物流工程师订舱
        //订舱信息

        DataTable dt_dc = YJ_CLASS.DC_PRO(requestid);
        if (dt_dc.Rows.Count > 0)
        {
            if (dt_dc.Rows[0]["fyrq_sale"].ToString() != "")
            {
                txt_dcfy_date.Value = Convert.ToDateTime(dt_dc.Rows[0]["fyrq_sale"].ToString()).ToString("yyyy-MM-dd");
            }
            if (dt_dc.Rows[0]["tjdate_wuliu"].ToString() != "")
            {
                txt_dccl_date.Value = Convert.ToDateTime(dt_dc.Rows[0]["tjdate_wuliu"].ToString()).ToString("yyyy-MM-dd");
                txt_dch.Value = dt_dc.Rows[0]["hyno_wuliu"].ToString();
                txt_dcr.Value = dt_dc.Rows[0]["DC_uname"].ToString();
                txt_Logistics_job.Value = "物流工程师";
                txt_Logistics_id.Value = dt_dc.Rows[0]["DC_uid"].ToString();
                txt_Logistics.Value = dt_dc.Rows[0]["DC_uname"].ToString();
            }
        }
        //订舱申请时间确认
        DataTable dt_QR_Logistics_dccl = YJ_CLASS.qr_date(requestid, txt_Logistics_id.Value, -5);
        if (dt_QR_Logistics_dccl.Rows.Count > 0)
        {
            txt_dccl_require.Value = Convert.ToDateTime(dt_QR_Logistics_dccl.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_dccl_complete.Value = Convert.ToDateTime(dt_QR_Logistics_dccl.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");

            if (DateTime.Compare(Convert.ToDateTime(txt_dccl_require.Value), Convert.ToDateTime(txt_dccl_complete.Value)) < 0)
            {
                txt_dccl_complete.Style.Add("color", "Red");
            }
        }
        else
        {
            if (dt_QR_Logistics_dccl.Rows.Count > 0)
            {
                dt_QR_Logistics_dccl = YJ_CLASS.qr_date_yq(requestid, txt_Logistics_id.Value, -5);
                txt_dccl_require.Value = Convert.ToDateTime(dt_QR_Logistics_dccl.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            }

        }
        //项目工程师
        DataTable dt_QR_project_engineer = YJ_CLASS.qr_date(requestid, txt_project_engineer_id.Value, 0);
        if (dt_QR_project_engineer.Rows.Count > 0)
        {
            txt_special_require0.Value = Convert.ToDateTime(dt_QR_project_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_customer_request_complete0.Value = Convert.ToDateTime(dt_QR_project_engineer.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_special_require0.Value), Convert.ToDateTime(txt_customer_request_complete0.Value)) < 0)
            {

                txt_customer_request_complete0.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_project_engineer = YJ_CLASS.qr_date_yq(requestid, txt_project_engineer_id.Value, 0);
            txt_special_require0.Value = Convert.ToDateTime(dt_QR_project_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");

        }
        //包装工程师
        DataTable dt_QR_Packaging_engineer = YJ_CLASS.qr_date(requestid, txt_Packaging_engineer_id.Value, 0);
        if (dt_QR_Packaging_engineer.Rows.Count > 0)
        {
            txt_Packaging_require0.Value = Convert.ToDateTime(dt_QR_Packaging_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_Packaging_complete0.Value = Convert.ToDateTime(dt_QR_Packaging_engineer.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_Packaging_require0.Value), Convert.ToDateTime(txt_Packaging_complete0.Value)) < 0)
            {

                txt_Packaging_complete0.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_Packaging_engineer = YJ_CLASS.qr_date_yq(requestid, txt_Packaging_engineer_id.Value, 0);
            txt_Packaging_require0.Value = Convert.ToDateTime(dt_QR_Packaging_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");

        }
        //产品工程师
        DataTable dt_QR_product_engineer = YJ_CLASS.qr_date(requestid, txt_product_engineer_id.Value, 0);
        if (dt_QR_product_engineer.Rows.Count > 0)
        {
            txt_goods_require0.Value = Convert.ToDateTime(dt_QR_product_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_goods_complete0.Value = Convert.ToDateTime(dt_QR_product_engineer.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_goods_require0.Value), Convert.ToDateTime(txt_goods_complete0.Value)) < 0)
            {

                txt_goods_complete0.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_product_engineer = YJ_CLASS.qr_date_yq(requestid, txt_product_engineer_id.Value, 0);
            txt_goods_require0.Value = Convert.ToDateTime(dt_QR_product_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }

        //物流计划
        DataTable dt_QR_planning_engineer = YJ_CLASS.qr_date(requestid, txt_planning_engineer_id.Value, 0);
        if (dt_QR_planning_engineer.Rows.Count > 0)
        {
            txt_goods_require_wl0.Value = Convert.ToDateTime(dt_QR_planning_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_goods_complete_wl0.Value = Convert.ToDateTime(dt_QR_planning_engineer.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_goods_require_wl0.Value), Convert.ToDateTime(txt_goods_complete_wl0.Value)) < 0)
            {

                txt_goods_complete_wl0.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_planning_engineer = YJ_CLASS.qr_date_yq(requestid, txt_planning_engineer_id.Value, 0);
            txt_goods_require_wl0.Value = Convert.ToDateTime(dt_QR_planning_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }
        //质量工程师
        DataTable dt_QR_quality_engineer = YJ_CLASS.qr_date(requestid, txt_quality_engineer_id.Value, 0);
        if (dt_QR_quality_engineer.Rows.Count > 0)
        {
            txt_check_require0.Value = Convert.ToDateTime(dt_QR_quality_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
            txt_check_complete0.Value = Convert.ToDateTime(dt_QR_quality_engineer.Rows[0]["Commit_time"].ToString()).ToString("yyyy-MM-dd");
            if (DateTime.Compare(Convert.ToDateTime(txt_check_require0.Value), Convert.ToDateTime(txt_check_complete0.Value)) < 0)
            {

                txt_check_complete0.Style.Add("color", "Red");
            }
        }
        else
        {
            dt_QR_quality_engineer = YJ_CLASS.qr_date_yq(requestid, txt_quality_engineer_id.Value, 0);
            txt_check_require0.Value = Convert.ToDateTime(dt_QR_quality_engineer.Rows[0]["RequireDate"].ToString()).ToString("yyyy-MM-dd");
        }

        //新产品和量产件区分显示
        if (txt_cp_status.SelectedValue != "")
        {
            if (txt_cp_status.SelectedValue == "新产品")
            {

                wljh.Visible = false;
                cpgcs.Visible = true;
                //   V_cpgcs.Visible = true;
                //wljh.Attributes.Add("display", "none");
                //  cpgcs.Attributes.Add("display", "");
                ViewState["V_cpgcs"] = "";
                ViewState["V_wljh"] = "none";
                V_zlgcs1.Visible = false;
                V_zlgcs3.Visible = false;
            }
            else
            {
                wljh.Visible = true;
                cpgcs.Visible = false;
                //V_cpgcs.Attributes.Add("display", "none");
                //  V_cpgcs.Visible = false;
                ViewState["V_cpgcs"] = "none";
                ViewState["V_wljh"] = "";
            }
        }
        warehouse_keeper();
    }

    public void warehouse_keeper()
    {
        if (txt_domain_zzgc.Value == "200")
        {
            txt_warehouse_keeper_id_sj.Value = "01222";
        }
        if (txt_domain_zzgc.Value == "100")
        {
            txt_warehouse_keeper_id_sj.Value = "00082";
        }
        //仓库发货
        if (txt_domain.Value == "200")
        {
            txt_warehouse_keeper_id.Value = "01222";
            txt_Logistics_id.Value = "02167";
        }
        if (txt_domain.Value == "100")
        {
            txt_warehouse_keeper_id.Value = "00082";
            txt_Logistics_id.Value = "00490";
        }
        if (YJ_CLASS.YJ_emp(txt_warehouse_keeper_id.Value).Rows.Count > 0)
        {

            DataTable dtemp = YJ_CLASS.YJ_emp(txt_warehouse_keeper_id.Value);
            //姓名
            txt_warehouse_keeper.Value = dtemp.Rows[0]["lastname"].ToString();
            //域账号
            txt_warehouse_keeper_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
            //职位
            txt_warehouse_keeper_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
            dtemp.Clear();
        }
        if (YJ_CLASS.YJ_emp(txt_warehouse_keeper_id_sj.Value).Rows.Count > 0)
        {

            DataTable dtemp = YJ_CLASS.YJ_emp(txt_warehouse_keeper_id_sj.Value);
            //姓名
            txt_warehouse_keeper_sj.Value = dtemp.Rows[0]["lastname"].ToString();
            //域账号
            txt_warehouse_keeper_AD_sj.Value = dtemp.Rows[0]["ADAccount"].ToString();
            //职位
            txt_warehouse_keeper_job_sj.Value = dtemp.Rows[0]["jobtitlename"].ToString();
            dtemp.Clear();
        }
        if (YJ_CLASS.YJ_emp(txt_Logistics_id.Value).Rows.Count > 0)
        {

            DataTable dtemp = YJ_CLASS.YJ_emp(txt_Logistics_id.Value);
            //姓名
            txt_Logistics.Value = dtemp.Rows[0]["lastname"].ToString();
            ////域账号
            //txt_warehouse_keeper_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
            //职位
            txt_Logistics_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
            dtemp.Clear();
        }
    }


    public void DDL()
    {
        txt_sktj.DataSource = YJ_CLASS.YJ_BASE("1");
        txt_sktj.DataValueField = "CLASS_NAME";
        txt_sktj.DataTextField = "CLASS_NAME";
        txt_sktj.DataBind();
        this.txt_sktj.Items.Insert(0, new ListItem("", ""));

        txt_ysfs.DataSource = YJ_CLASS.YJ_BASE("3");
        txt_ysfs.DataValueField = "CLASS_NAME";
        txt_ysfs.DataTextField = "CLASS_NAME";
        txt_ysfs.DataBind();
        this.txt_ysfs.Items.Insert(0, new ListItem("", ""));


        txt_ystk.DataSource = YJ_CLASS.YJ_BASE("4");
        txt_ystk.DataValueField = "CLASS_NAME";
        txt_ystk.DataTextField = "CLASS_NAME";
        txt_ystk.DataBind();
        this.txt_ystk.Items.Insert(0, new ListItem("", ""));


        txt_yfzffs.DataSource = YJ_CLASS.YJ_BASE("2");
        txt_yfzffs.DataValueField = "CLASS_NAME";
        txt_yfzffs.DataTextField = "CLASS_NAME";
        txt_yfzffs.DataBind();
        this.txt_yfzffs.Items.Insert(0, new ListItem("", ""));

        txt_is_check_qcc.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_qcc.DataValueField = "CLASS_NAME";
        txt_is_check_qcc.DataTextField = "CLASS_NAME";
        txt_is_check_qcc.DataBind();
        this.txt_is_check_qcc.Items.Insert(0, new ListItem("", ""));

        txt_is_check_szb.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_szb.DataValueField = "CLASS_NAME";
        txt_is_check_szb.DataTextField = "CLASS_NAME";
        txt_is_check_szb.DataBind();
        this.txt_is_check_szb.Items.Insert(0, new ListItem("", ""));

        txt_is_check_wg.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_wg.DataValueField = "CLASS_NAME";
        txt_is_check_wg.DataTextField = "CLASS_NAME";
        txt_is_check_wg.DataBind();
        this.txt_is_check_wg.Items.Insert(0, new ListItem("", ""));

        txt_is_check_jj.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_jj.DataValueField = "CLASS_NAME";
        txt_is_check_jj.DataTextField = "CLASS_NAME";
        txt_is_check_jj.DataBind();
        this.txt_is_check_jj.Items.Insert(0, new ListItem("", ""));

        txt_is_check_qcc_zl.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_qcc_zl.DataValueField = "CLASS_NAME";
        txt_is_check_qcc_zl.DataTextField = "CLASS_NAME";
        txt_is_check_qcc_zl.DataBind();
        this.txt_is_check_qcc_zl.Items.Insert(0, new ListItem("", ""));

        txt_is_check_szb_zl.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_szb_zl.DataValueField = "CLASS_NAME";
        txt_is_check_szb_zl.DataTextField = "CLASS_NAME";
        txt_is_check_szb_zl.DataBind();
        this.txt_is_check_szb_zl.Items.Insert(0, new ListItem("", ""));

        txt_is_check_wg_zl.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_wg_zl.DataValueField = "CLASS_NAME";
        txt_is_check_wg_zl.DataTextField = "CLASS_NAME";
        txt_is_check_wg_zl.DataBind();
        this.txt_is_check_wg_zl.Items.Insert(0, new ListItem("", ""));

        txt_is_check_jj_zl.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_is_check_jj_zl.DataValueField = "CLASS_NAME";
        txt_is_check_jj_zl.DataTextField = "CLASS_NAME";
        txt_is_check_jj_zl.DataBind();
        this.txt_is_check_jj_zl.Items.Insert(0, new ListItem("", ""));

        txt_check_lj.DataSource = YJ_CLASS.YJ_BASE("6");
        txt_check_lj.DataValueField = "CLASS_NAME";
        txt_check_lj.DataTextField = "CLASS_NAME";
        txt_check_lj.DataBind();
        this.txt_check_lj.Items.Insert(0, new ListItem("", ""));


        txt_check_lj_zl.DataSource = YJ_CLASS.YJ_BASE("6");
        txt_check_lj_zl.DataValueField = "CLASS_NAME";
        txt_check_lj_zl.DataTextField = "CLASS_NAME";
        txt_check_lj_zl.DataBind();
        this.txt_check_lj_zl.Items.Insert(0, new ListItem("", ""));

        txt_Box_specifications1.DataSource = YJ_CLASS.YJ_BASE("7");
        txt_Box_specifications1.DataValueField = "CLASS_NAME";
        txt_Box_specifications1.DataTextField = "CLASS_NAME";
        txt_Box_specifications1.DataBind();
        this.txt_Box_specifications1.Items.Insert(0, new ListItem("", ""));

        txt_Box_specifications2.DataSource = YJ_CLASS.YJ_BASE("7");
        txt_Box_specifications2.DataValueField = "CLASS_NAME";
        txt_Box_specifications2.DataTextField = "CLASS_NAME";
        txt_Box_specifications2.DataBind();
        this.txt_Box_specifications2.Items.Insert(0, new ListItem("", ""));

        txt_cp_status.DataSource = YJ_CLASS.YJ_BASE("8");
        txt_cp_status.DataValueField = "CLASS_NAME";
        txt_cp_status.DataTextField = "CLASS_NAME";
        txt_cp_status.DataBind();
        this.txt_cp_status.Items.Insert(0, new ListItem("", ""));

        txt_customer_request.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_customer_request.DataValueField = "CLASS_NAME";
        txt_customer_request.DataTextField = "CLASS_NAME";
        txt_customer_request.DataBind();
        this.txt_customer_request.Items.Insert(0, new ListItem("", ""));

        txt_Already_Product.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_Already_Product.DataValueField = "CLASS_NAME";
        txt_Already_Product.DataTextField = "CLASS_NAME";
        txt_Already_Product.DataBind();
        this.txt_Already_Product.Items.Insert(0, new ListItem("", ""));

        txt_Packing_goods_Already.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_Packing_goods_Already.DataValueField = "CLASS_NAME";
        txt_Packing_goods_Already.DataTextField = "CLASS_NAME";
        txt_Packing_goods_Already.DataBind();
        this.txt_Packing_goods_Already.Items.Insert(0, new ListItem("", ""));

        txt_Already_Planning.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_Already_Planning.DataValueField = "CLASS_NAME";
        txt_Already_Planning.DataTextField = "CLASS_NAME";
        txt_Already_Planning.DataBind();
        this.txt_Already_Planning.Items.Insert(0, new ListItem("", ""));

        txt_Already_Check.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_Already_Check.DataValueField = "CLASS_NAME";
        txt_Already_Check.DataTextField = "CLASS_NAME";
        txt_Already_Check.DataBind();
        this.txt_Already_Check.Items.Insert(0, new ListItem("", ""));

        txt_Already_zl.DataSource = YJ_CLASS.YJ_BASE("9");
        txt_Already_zl.DataValueField = "CLASS_NAME";
        txt_Already_zl.DataTextField = "CLASS_NAME";
        txt_Already_zl.DataBind();
        this.txt_Already_zl.Items.Insert(0, new ListItem("", ""));

        txt_Process_intervention.DataSource = YJ_CLASS.YJ_BASE("10");
        txt_Process_intervention.DataValueField = "CLASS_NAME";
        txt_Process_intervention.DataTextField = "CLASS_NAME";
        txt_Process_intervention.DataBind();
        this.txt_Process_intervention.Items.Insert(0, new ListItem("", ""));

        txt_Process_intervention_zl.DataSource = YJ_CLASS.YJ_BASE("13");
        txt_Process_intervention_zl.DataValueField = "CLASS_NAME";
        txt_Process_intervention_zl.DataTextField = "CLASS_NAME";
        txt_Process_intervention_zl.DataBind();
        this.txt_Process_intervention_zl.Items.Insert(0, new ListItem("", ""));

        txt_Serial_No.DataSource = YJ_CLASS.YJ_BASE("5");
        txt_Serial_No.DataValueField = "CLASS_NAME";
        txt_Serial_No.DataTextField = "CLASS_NAME";
        txt_Serial_No.DataBind();
        this.txt_Serial_No.Items.Insert(0, new ListItem("", ""));

        DDL_lable_type.DataSource = YJ_CLASS.YJ_BASE("18");
        DDL_lable_type.DataValueField = "VALUE";
        DDL_lable_type.DataTextField = "CLASS_NAME";
        DDL_lable_type.DataBind();
        this.DDL_lable_type.Items.Insert(0, new ListItem("", ""));

        DDL_sbh_AAM.DataSource = YJ_CLASS.YJ_BASE("16");
        DDL_sbh_AAM.DataValueField = "CLASS_NAME";
        DDL_sbh_AAM.DataTextField = "CLASS_NAME";
        DDL_sbh_AAM.DataBind();
        this.DDL_sbh_AAM.Items.Insert(0, new ListItem("", ""));

        DDL_Part_Status_Daimler.DataSource = YJ_CLASS.YJ_BASE("17");
        DDL_Part_Status_Daimler.DataValueField = "CLASS_NAME";
        DDL_Part_Status_Daimler.DataTextField = "CLASS_NAME";
        DDL_Part_Status_Daimler.DataBind();
        this.DDL_Part_Status_Daimler.Items.Insert(0, new ListItem("", ""));

        DDL_Part_Change_Daimler.DataSource = YJ_CLASS.YJ_BASE("5");
        DDL_Part_Change_Daimler.DataValueField = "CLASS_NAME";
        DDL_Part_Change_Daimler.DataTextField = "CLASS_NAME";
        DDL_Part_Change_Daimler.DataBind();
        this.DDL_Part_Change_Daimler.Items.Insert(0, new ListItem("", ""));
       
        this.DDL_DY.Items.Insert(0, new ListItem("", ""));

   
        

    }
    protected void DDL_DY_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BASE_ID = "";
        BASE_ID = DDL_DY.SelectedValue;
        CBL_DY.DataSource = YJ_CLASS.YJ_BASE(BASE_ID);
        CBL_DY.DataValueField = "ID";
        CBL_DY.DataTextField = "CLASS_NAME";
        CBL_DY.DataBind();

        //DataTable dt= YJ_CLASS.YJ_BASE(BASE_ID);
        //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //{ 
        //    CBL_DY.Items.Add(dt.Rows[i][0].ToString());
        //}
        XS_BQ();
    }
    public void BASE_dy()
    {
        string BASE_ID = "";
        if (txt_gkdm.Value != "")
        {
            BASE_ID = txt_gkdm.Value.Substring(0, 3);

            txt_bqdy.DataSource = YJ_CLASS.YJ_BASE(BASE_ID);
            txt_bqdy.DataValueField = "VALUE";
            txt_bqdy.DataTextField = "CLASS_NAME";
            txt_bqdy.DataBind();
            this.txt_bqdy.Items.Insert(0, new ListItem("装箱出库单-所有客户", "PrintPackingList.aspx"));


     


        }

    }
    public static string savepath = "\\UploadFile\\YangJian";

    protected void Btn_ddfj_Click(object sender, EventArgs e)
    {
        //ViewState["gridview"] = gvFile_ddfj;
        //if (txt_ddfj.Value != "")
        //{
        FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_ddfj, txt_ddfj.ID, gvFile_ddfj);
        //}

    }
    protected void gvFile_ddfj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_ddfj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_ddfj.ID, gvFile_ddfj);//補充兩參數 樣件單號，工程師
    }
    protected void Btn_yqfj_Click(object sender, EventArgs e)
    {
        if (txt_yqfj.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_yqfj, txt_yqfj.ID, gvFile_yqfj);
        }
    }
    protected void gvFile_yqfj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_yqfj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_yqfj.ID, gvFile_yqfj);
    }
    protected void Btn_bqfj_Click(object sender, EventArgs e)
    {
        if (txt_bqfj.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_bqfj, txt_bqfj.ID, gvFile_bqfj);
        }
    }
    protected void gvFile_bqfj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_bqfj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_bqfj.ID, gvFile_bqfj);
    }

    protected void Btn_special_fj_Click(object sender, EventArgs e)
    {
        if (txt_special_fj.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_special_fj, txt_special_fj.ID, gvFile_special_fj);
        }
        ViewState["lv"] = "XMGCS";

    }
    protected void gvFile_special_fj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_special_fj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_special_fj.ID, gvFile_special_fj);
    }
    protected void Btn_Package_photo_Click(object sender, EventArgs e)
    {
        if (txt_Package_photo.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_Package_photo, txt_Package_photo.ID, gvFile_Package_photo);
        }
        ViewState["lv"] = "CKBZ";
    }

    protected void gvFile_Package_photo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_Package_photo.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_Package_photo.ID, gvFile_Package_photo);
    }

    protected void Btn_Shipping_photos_Click(object sender, EventArgs e)
    {
        if (txt_Shipping_photos.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_Shipping_photos, txt_Shipping_photos.ID, gvFile_Shipping_photos);
        }
        ViewState["lv"] = "CKBZ";
    }

    protected void gvFile_Shipping_photos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_Shipping_photos.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_Shipping_photos.ID, gvFile_Shipping_photos);
    }

    protected void Btn_check_fj_Click(object sender, EventArgs e)
    {
        if (txt_check_fj.Value != "")
        {
            FileUpload(txt_Code.Value, ViewState["empid"].ToString(), txt_check_fj, txt_check_fj.ID, gvFile_check_fj);
        }
        ViewState["lv"] = "ZLGCS";

    }

    protected void gvFile_check_fj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_check_fj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, ViewState["empid"].ToString(), txt_check_fj.ID, gvFile_check_fj);
    }
    public void FileUpload(string YJNo, string Engineer, System.Web.UI.HtmlControls.HtmlInputFile FileUpLoader, string File_mc, GridView gv)
    {
        //if (FileUpLoader.PostedFile != null)
        if (FileUpLoader.PostedFile.FileName.Length != 0)
        {
            string filename = FileUpLoader.PostedFile.FileName;
            if (filename.Contains("\\") == true)
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }

            string MapDir = MapPath("~");
            string yjpath = MapDir + savepath + "\\" + YJNo;
            if (!System.IO.Directory.Exists(yjpath))
            {
                System.IO.Directory.CreateDirectory(yjpath);//不存在就创建目录 
            }
            FileUpLoader.PostedFile.SaveAs(yjpath + "\\" + filename);
            FileSaveToDB(YJNo, Engineer, filename, File_mc, gv);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择文件后再点击上传！')", true);
        }

    }
    public void FileSaveToDB(string YJNo, string Engineer, string filename, string File_mc, GridView gv)
    {
        string File_lj = savepath + "\\" + YJNo + "\\" + filename;
        string strSql = "insert into form1_Sale_YJ_UPLOAD(Code, UpLoad_user, File_name, File_lj,File_mc) values('" + YJNo + "','" + Engineer + "','" + filename + "','" + File_lj + "','" + File_mc + "')";
        DbHelperSQL.ExecuteSql(strSql);
        ShowFile(YJNo, Engineer, File_mc, gv);
    }
    public void FileDelete(string id, string filepath, string YJNo, string Engineer, string File_mc, GridView gv)
    {
        string strSql = "delete from form1_Sale_YJ_UPLOAD where id=" + id;
        DbHelperSQL.ExecuteSql(strSql);
        FileDirDelete(filepath);
        ShowFile(YJNo, Engineer, File_mc, gv);
    }
    public void FileDirDelete(string filedir)
    {
        if (System.IO.File.Exists(filedir))
        {
            System.IO.File.Delete(filedir);
        }

    }

    public void ShowFile(string YJNo, string Engineer, string File_mc, GridView gv)
    {
        string strSql = "select * from form1_Sale_YJ_UPLOAD where Code='" + YJNo + "' and File_mc='" + File_mc + "'";
        DataSet ds = DbHelperSQL.Query(strSql);
        gv.DataSource = ds;
        gv.DataBind();
    }
    public void ShowFileload(string YJNo, string File_mc, GridView gv)
    {
        string strSql = "select * from form1_Sale_YJ_UPLOAD where Code='" + YJNo + "' and File_mc='" + File_mc + "'";
        DataSet ds = DbHelperSQL.Query(strSql);
        gv.DataSource = ds;
        gv.DataBind();
    }



    protected void txt_CP_ID_TextChanged(object sender, EventArgs e)
    {

        if (txt_CP_ID.Text == "")
        {

        }
        else
        {
            DataTable dtxmh = YJ_CLASS.Getxmh("", "", "", txt_CP_ID.Text);
            txt_gkdm.Value = dtxmh.Rows[0]["DebtorCode"].ToString();
            txt_gkdm.Value = dtxmh.Rows[0]["DebtorCode"].ToString();
            txt_gkmc.Value = dtxmh.Rows[0]["DebtorShipToName"].ToString();
            txt_ljmc.Value = dtxmh.Rows[0]["ljmc"].ToString();
            // txt_ljzl.Value = dtxmh.Rows[0]["pt_ship_wt"].ToString();
            txt_ljzl.Value = dtxmh.Rows[0]["pt_net_wt"].ToString();
            txt_fhz.Value = dtxmh.Rows[0]["cp_cust"].ToString();
            txt_ljh.Value = dtxmh.Rows[0]["cp_cust_part"].ToString();
            txt_xmh.Value = dtxmh.Rows[0]["pt_part"].ToString();
            txt_sqgc.Value = dtxmh.Rows[0]["sqgc"].ToString();
            txt_domain.Value = dtxmh.Rows[0]["cp_domain"].ToString();
            //各相关工程师
            txt_Sales_engineer_id.Value = dtxmh.Rows[0]["sale"].ToString();
            txt_project_engineer_id.Value = dtxmh.Rows[0]["project_user"].ToString();
            txt_product_engineer_id.Value = dtxmh.Rows[0]["product_user"].ToString();
            txt_quality_engineer_id.Value = dtxmh.Rows[0]["zl_user"].ToString();
            txt_Packaging_engineer_id.Value = dtxmh.Rows[0]["bz_user"].ToString();

            //其他相关人员
            txt_planning_engineer_id.Value = dtxmh.Rows[0]["plan_user"].ToString();
            txt_checker_monitor_id.Value = dtxmh.Rows[0]["checker_user"].ToString();
            txt_warehouse_keeper_id.Value = dtxmh.Rows[0]["warehouse_user"].ToString();
            dtxmh.Clear();

            //获取工程师姓名；域账号；职称等
            if (YJ_CLASS.YJ_emp(txt_Userid.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_Userid.Value);
                txt_Assistant_id.Value = txt_Userid.Value;
                txt_Assistant.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_Assistant_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_Assistant_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_Sales_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_Sales_engineer_id.Value);
                txt_Sales_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_Sales_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_Sales_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_project_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_project_engineer_id.Value);
                txt_project_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_project_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_project_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_product_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_product_engineer_id.Value);
                txt_product_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_product_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_product_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                txt_zzgc.Value = dtemp.Rows[0]["gc"].ToString();
                txt_domain_zzgc.Value = dtemp.Rows[0]["domain"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_quality_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_quality_engineer_id.Value);
                txt_quality_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_quality_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_quality_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_Packaging_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_Packaging_engineer_id.Value);
                txt_Packaging_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_Packaging_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_Packaging_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_checker_monitor_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_checker_monitor_id.Value);
                txt_checker_monitor.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_checker_monitor_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_checker_monitor_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            if (YJ_CLASS.YJ_emp(txt_planning_engineer_id.Value).Rows.Count > 0)
            {
                DataTable dtemp = YJ_CLASS.YJ_emp(txt_planning_engineer_id.Value);
                txt_planning_engineer.Value = dtemp.Rows[0]["lastname"].ToString();
                txt_planning_engineer_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                txt_planning_engineer_job.Value = dtemp.Rows[0]["jobtitlename"].ToString();
                dtemp.Clear();
            }
            warehouse_keeper();
           
            Get_Barcode();
            XS_BQ();
            //获取库存量
            Getkcl(txt_xmh.Value, txt_domain_zzgc.Value);
            //获取价格
            Getjg(txt_xmh.Value, txt_gkdm.Value, txt_domain.Value);

        }

    }
    public void Get_Barcode()
    {
        string path = @"\\172.16.5.26\barcode$\";
        path = path + txt_ljh.Value + "\\1.JPG";

        if (!(File.Exists(path)))

        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('条码标签图片不存在，请增加后再选择！')", true);
            //BTN_Sales_Assistant1.Visible = false;
            // Directory.CreateDirectory(path);  //创建目录 
        }
        else
        {
        }
    }
    public void Get_jgbj()
    {
        //价格截至日期小于发运日期
        if (txt_yqfy_date.Text != "")
        {
            if (txt_ljdj_rq.Text != "")
            {
                if (DateTime.Compare(Convert.ToDateTime(txt_ljdj_rq.Text), Convert.ToDateTime(txt_yqfy_date.Text)) < 0)
                {

                    txt_ljdj_rq.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txt_ljdj_rq.ForeColor = System.Drawing.Color.Green;
                }
            }
        }


    }
    public void Getjg(string pi_part_code, string ad_id, string pi_domain)
    {
        DataTable dtjg = YJ_CLASS.Getjg(pi_part_code, ad_id, pi_domain);
        if (dtjg.Rows.Count != 0)
        {
            txt_hb.Value = dtjg.Rows[0]["pi_curr"].ToString();
            txt_ljdj_rq.Text = Convert.ToDateTime(dtjg.Rows[0]["pi_expire"].ToString()).ToString("yyyy-MM-dd");
            txt_ljdj_rq.Visible = true;
            Lab_ljdj_rq.Visible = true;
            Get_jgbj();
            //第一次申请时两个价格，后面打开时只加载第qad价格
            if (txt_status_id.Text == "-1")
            {
                txt_ljdj.Text = dtjg.Rows[0]["dj"].ToString();
                txt_ljdj_qad.Text = dtjg.Rows[0]["dj"].ToString();
            }
            else
            {
                txt_ljdj_qad.Text = dtjg.Rows[0]["dj"].ToString();
            }
            txt_ljdj_qad.Text = dtjg.Rows[0]["dj"].ToString();
            if (txt_ljdj_qad.Text != txt_ljdj.Text)
            {
                txt_ljdj.Style.Add("color", "Red");
            }
            else
            {
                txt_ljdj.Style.Add("color", "black");
            }
            dtjg.Clear();
            if (txt_yhsl.Text != "" && txt_ljdj.Text != "" && txt_ljzl.Value != "")
            {
                txt_ljzj.Value = Convert.ToString(Convert.ToInt32(txt_yhsl.Text) * Convert.ToDecimal(txt_ljdj.Text));
                txt_Packing_net_weight.Value = Convert.ToString(Convert.ToInt32(txt_yhsl.Text) * Convert.ToDecimal(txt_ljzl.Value));
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('价格和数量和零件重量都需要填写！')", true);
                BTN_Sales_Assistant1.Visible = true;
            }
            BTN_Sales_Assistant1.Visible = true;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请在QAD系统中维护样件价格后再申请！')", true);
            BTN_Sales_Assistant1.Visible = false;
        }
    }
    public void Getkcl(string ld_part, string ld_domain)
    {
        this.txt_kc_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
        txt_Current_inventory_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
        if (YJ_CLASS.Getkcl(ld_part, "", ld_domain, 1, "").Rows.Count != 0)
        {
            txt_kcl.Value = YJ_CLASS.Getkcl(ld_part, "", ld_domain, 1, "").Rows[0]["kcl"].ToString();
            txt_Current_inventory_quantity.Value = YJ_CLASS.Getkcl(ld_part, "", ld_domain, 1, "").Rows[0]["kcl"].ToString();
        }
        else
        {

            txt_kcl.Value = '0'.ToString();
            txt_Current_inventory_quantity.Value = '0'.ToString();
        }

        if (YJ_CLASS.Getkcl(ld_part, "", ld_domain, 2, "").Rows.Count != 0)
        {
            //产品工程师参考号
            txt_reference_number.DataSource = YJ_CLASS.Getkcl(ld_part, "", ld_domain, 2, "");
            txt_reference_number.DataValueField = "ld_ref";
            txt_reference_number.DataTextField = "ld_ref";
            txt_reference_number.DataBind();
            this.txt_reference_number.Items.Insert(0, new ListItem("", ""));
            //质量工程师参考号
            txt_reference_number_zl.DataSource = YJ_CLASS.Getkcl(ld_part, "", ld_domain, 2, "");
            txt_reference_number_zl.DataValueField = "ld_ref";
            txt_reference_number_zl.DataTextField = "ld_ref";
            txt_reference_number_zl.DataBind();
            this.txt_reference_number_zl.Items.Insert(0, new ListItem("", ""));

            txt_Stocking_quantity.Value = txt_yhsl.Text;
            txt_Stocking_quantity_wl.Value = txt_yhsl.Text;

        }
        else
        {

            txt_kcl.Value = '0'.ToString();
            txt_Current_inventory_quantity.Value = '0'.ToString();
            txt_Stocking_quantity.Value = txt_yhsl.Text;
            txt_Stocking_quantity_wl.Value = txt_yhsl.Text;
        }
    }

    protected void txt_ysfs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_yqdh_date.Value == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先填写要求到货日期！')", true);
        }
        else
        {
            if (txt_ysfs.SelectedValue == "国际快递" || txt_ysfs.SelectedValue == "空运")
            {
                txt_yqfy_date.Text = DateTime.ParseExact(txt_yqdh_date.Value, "yyyy-MM-dd", null).AddDays(-14).ToString("yyyy-MM-dd");

            }
            else if (txt_ysfs.SelectedValue == "货车" || txt_ysfs.SelectedValue == "国内快递")
            {

                txt_yqfy_date.Text = DateTime.ParseExact(txt_yqdh_date.Value, "yyyy-MM-dd", null).AddDays(-7).ToString("yyyy-MM-dd");
            }
            else
            {
                txt_yqfy_date.Text = DateTime.ParseExact(txt_yqdh_date.Value, "yyyy-MM-dd", null).AddDays(-49).ToString("yyyy-MM-dd");
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('ctl00$MainContent$txt_yqfy_date').change()", true);
        }

        txt_yqfy_date_TextChanged(sender, e);
    }

    protected void txt_yqfy_date_TextChanged(object sender, EventArgs e)
    {
        if (txt_yqfy_date.Text != "")
        {
            //if(txt_ljdj_rq.Text!="")
            //{
            //    if (DateTime.Compare(Convert.ToDateTime(txt_ljdj_rq.Text), Convert.ToDateTime(txt_yqfy_date.Text)) < 0)
            //    {

            //        txt_ljdj_rq.ForeColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        txt_ljdj_rq.ForeColor = System.Drawing.Color.Green;
            //    }
            //}
            Get_jgbj();
            if (DateTime.Compare(Convert.ToDateTime(txt_yqfy_date.Text), DateTime.Now) < 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('要求发运日期小于今天日期！')", true);
                txt_yqfy_date.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txt_yqfy_date.ForeColor = System.Drawing.Color.Green;
            }
            if (txt_yqfy_date.Text != "")
            {
                txt_shipping_require.Value = txt_yqfy_date.Text;
                txt_special_require.Value = txt_yqfy_date.Text;
                txt_check_require.Value = DateTime.ParseExact(txt_yqfy_date.Text, "yyyy-MM-dd", null).AddDays(-2).ToString("yyyy-MM-dd");
                txt_check_require_jy.Value = DateTime.ParseExact(txt_yqfy_date.Text, "yyyy-MM-dd", null).AddDays(-2).ToString("yyyy-MM-dd");
                txt_goods_require.Value = DateTime.ParseExact(txt_yqfy_date.Text, "yyyy-MM-dd", null).AddDays(-5).ToString("yyyy-MM-dd");
                txt_goods_require_wl.Value = DateTime.ParseExact(txt_yqfy_date.Text, "yyyy-MM-dd", null).AddDays(-5).ToString("yyyy-MM-dd");
                txt_Packaging_require.Value = DateTime.ParseExact(txt_yqfy_date.Text, "yyyy-MM-dd", null).AddDays(-6).ToString("yyyy-MM-dd");

                //if(txt_check_require.Value)
                //如果计算结果要求日期小于当天，即为当天
                if (DateTime.Compare(Convert.ToDateTime(txt_check_require.Value), DateTime.Now) < 0)
                {
                    txt_check_require.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (DateTime.Compare(Convert.ToDateTime(txt_check_require_jy.Value), DateTime.Now) < 0)
                {
                    txt_check_require_jy.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (DateTime.Compare(Convert.ToDateTime(txt_goods_require.Value), DateTime.Now) < 0)
                {
                    txt_goods_require.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (DateTime.Compare(Convert.ToDateTime(txt_goods_require_wl.Value), DateTime.Now) < 0)
                {
                    txt_goods_require_wl.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (DateTime.Compare(Convert.ToDateTime(txt_Packaging_require.Value), DateTime.Now) < 0)
                {
                    txt_Packaging_require.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }

                txt_shipping_require.Disabled = false;
                txt_special_require.Disabled = false;
                txt_check_require.Disabled = false;
                txt_check_require_jy.Disabled = false;
                txt_goods_require.Disabled = false;
                txt_goods_require_wl.Disabled = false;
                txt_Packaging_require.Disabled = false;

            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('要求发运日期不能为空！')", true);
        }
    }

    private void GeLOG()
    {
        if (txt_status_id.Text == "")
        {
        }
        else
        {
            DataTable dt = new DataTable();

            dt = YJ_CLASS.Getlog(Request["requestid"]);
            //dt = YJ_CLASS.Getlog("1002");
            GridView1.DataSource = dt;
            GridView1.DataBind();

            DataTable dt_require = new DataTable();
            dt_require = YJ_CLASS.Getlog_require(Request["requestid"]);
            GridView3.DataSource = dt_require;
            GridView3.DataBind();

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
            dt = YJ_CLASS.GetCommit_time(Request["requestid"]);
            // DataTable dtbtn2 = YJ_CLASS.GetBTN2(Request["requestid"], "-3","");
            //dt = YJ_CLASS.Getlog("1002");
            GridView2.DataSource = dt;
            GridView2.DataBind();

        }

    }

    public void button_Assistant_01()
    {
        //上传附件按钮
        //--销售
        txt_ddfj.Visible = true;
        Btn_ddfj.Visible = true;
        gvFile_ddfj.Columns[2].Visible = true;
        txt_yqfj.Visible = true;
        Btn_yqfj.Visible = true;
        gvFile_yqfj.Columns[2].Visible = true;
        txt_bqfj.Visible = true;
        Btn_bqfj.Visible = true;
        gvFile_bqfj.Columns[2].Visible = true;
        //提交按钮
        BTN_Sales_Assistant1.Visible = true;
        BTN_Sales_Assistant1.Text = "修改";

        //获取价格
        if (txt_xmh.Value != "")
        {
            Getjg(txt_xmh.Value, txt_gkdm.Value, txt_domain.Value);
        }

    }
    public void button_Project_01()
    {
        txt_special_fj.Visible = true;
        Btn_special_fj.Visible = true;
        gvFile_special_fj.Columns[2].Visible = true;
        BTN_Project_Engineer1.Visible = true;
        ViewState["lv"] = "XMGCS";
    }

    public void button_Warehouse_03()
    {
        txt_Package_photo.Visible = true;
        Btn_Package_photo.Visible = true;
        gvFile_Package_photo.Columns[2].Visible = true;
        txt_Shipping_photos.Visible = true;
        Btn_Shipping_photos.Visible = true;
        gvFile_Shipping_photos.Columns[2].Visible = true;
    }

    public void button_Quality_04()
    {
        txt_check_fj.Visible = true;
        Btn_check_fj.Visible = true;
        gvFile_check_fj.Columns[2].Visible = true;
    }
    public void button()
    {
        //上传附件按钮
        //--销售
        txt_ddfj.Visible = false;
        Btn_ddfj.Visible = false;
        gvFile_ddfj.Columns[2].Visible = false;
        gvFile_ddfj.Columns[0].Visible = false;
        txt_yqfj.Visible = false;
        Btn_yqfj.Visible = false;
        gvFile_yqfj.Columns[2].Visible = false;
        gvFile_yqfj.Columns[0].Visible = false;
        txt_bqfj.Visible = false;
        Btn_bqfj.Visible = false;
        gvFile_bqfj.Columns[2].Visible = false;
        gvFile_bqfj.Columns[0].Visible = false;
        //--项目
        txt_special_fj.Visible = false;
        Btn_special_fj.Visible = false;
        gvFile_special_fj.Columns[2].Visible = false;
        gvFile_special_fj.Columns[0].Visible = false;
        //--仓库
        txt_Package_photo.Visible = false;
        Btn_Package_photo.Visible = false;
        gvFile_Package_photo.Columns[2].Visible = false;
        gvFile_Package_photo.Columns[0].Visible = false;
        txt_Shipping_photos.Visible = false;
        Btn_Shipping_photos.Visible = false;
        gvFile_Shipping_photos.Columns[2].Visible = false;
        gvFile_Shipping_photos.Columns[0].Visible = false;
        BTN_Warehouse_Keeper_DY.Visible = false;
        //--质量
        txt_check_fj.Visible = false;
        Btn_check_fj.Visible = false;
        gvFile_check_fj.Columns[2].Visible = false;
        gvFile_check_fj.Columns[0].Visible = false;
        //提交按钮
        BTN_Sales_Assistant1.Visible = false;
        BTN_Sales_Assistant2.Visible = false;
        BTN_Sales_Assistant3.Visible = false;
        BTN_Project_Engineer1.Visible = false;
        BTN_Project_Engineer2.Visible = false;
        BTN_Product_Engineer1.Visible = false;
        BTN_Product_Engineer2.Visible = false;
        BTN_Packaging_Engineer1.Visible = false;
        BTN_Packaging_Engineer2.Visible = false;
        BTN_Planning_Engineer1.Visible = false;
        BTN_Planning_Engineer2.Visible = false;
        BTN_Warehouse_Keeper1.Visible = false;
        BTN_Warehouse_Keeper2.Visible = false;
        BTN_Warehouse_Keeper3.Visible = false;
        BTN_Checker_Monitor.Visible = false;
        BTN_Quality_Engineer1.Visible = false;
        BTN_Quality_Engineer2.Visible = false;
        BTN_Quality_Engineer3.Visible = false;
        BTN_Quality_Engineer4.Visible = false;
        LB_Process_intervention.Visible = false;
        txt_Process_intervention.Visible = false;
        BTN_Process_intervention.Visible = false;
        txt_gysm.Visible = false;
        BTN_Process_intervention_zl.Visible = false;
        gy_zl.Visible = false;
        BTN_Sales_Assistant2_CS.Visible = false;

        //标签栏位
        FORD.Visible = false;
        Chrylser.Visible = false;
        GM.Visible = false;
        AAM.Visible = false;
        AAM2.Visible = false;
        BBAC.Visible = false;
        Daimler.Visible = false;
        Daimler2.Visible = false;
    }
    public void Get_Process_intervention()
    {

        if (Convert.ToInt32(txt_status_id.Text) < 4 && Convert.ToInt32(txt_status_id.Text) >= 0 && ViewState["empid"].ToString() == txt_Userid.Value)
        {
            LB_Process_intervention.Visible = true;
            txt_Process_intervention.Visible = true;
            txt_Process_intervention.Visible = true;
            BTN_Process_intervention.Visible = true;
            txt_gysm.Visible = true;
            BTN_Sales_Assistant2_CS.Visible = true;
            button_Assistant_01();
            if (Convert.ToInt32(txt_status_id.Text) < 4 && Convert.ToInt32(txt_status_id.Text) > 0 && ViewState["empid"].ToString() == txt_Userid.Value)
            {
                BTN_Sales_Assistant2.Visible = true;
                BTN_Sales_Assistant2.Text = "修改";
            }
        }
    }


    public void Get_btn()
    {
        //BTN_Sales_Assistant2.Visible = true;
        //申请提交后还未定产品状态
        if (txt_cp_status.SelectedValue == "")
        {
            if (ViewState["empid"].ToString() == txt_project_engineer_id.Value)
            {
                button_Project_01();
            }
            if (ViewState["empid"].ToString() == txt_Userid.Value)
            {
                button_Assistant_01();
            }
        }
        //已有产品状态后可根据节点判断
        if (txt_cp_status.SelectedValue != "")
        {
            DataTable dtbtn = YJ_CLASS.GetBTN(Request["requestid"], txt_cp_status.SelectedValue, txt_status_id.Text, ViewState["empid"].ToString());
            DataTable dtbtn2 = YJ_CLASS.GetBTN2(Request["requestid"], txt_status_id.Text, ViewState["empid"].ToString());
            DataTable dtemp = YJ_CLASS.YJ_emp(ViewState["empid"].ToString());
            if (dtbtn.Rows.Count > 0)
            {
                for (int i = 0; i < dtbtn.Rows.Count; i++)
                {

                    Button btn = null;
                    //获取当前登入人员是否为当前节点人员
                    string AA = ctl + dtbtn.Rows[i]["BTN"].ToString();
                    btn = ((Button)Page.FindControl(AA));
                    if (btn != null)
                    {
                        btn.Visible = true;
                        ViewState["lv"] = dtbtn.Rows[i]["lv"].ToString();

                        //获取当前登入人员是否已经处理过
                        if (dtbtn.Rows[i]["Update_user"].ToString() != "")
                        {
                            btn.Text = "修改";
                            if(ViewState["empid"].ToString() == txt_warehouse_keeper_id_sj.Value && txt_status_id.Text == "2")
                            {
                                if(txt_Already_Check.SelectedValue !="" && txt_Warehouse_quantity.Value == "" )
                                {
                                    BTN_Warehouse_Keeper1.Text = "修改";
                                    BTN_Warehouse_Keeper2.Text = "提交";
                                }
                            }
                        }
                        //如果被干预的流程，获取当前登入人员是否已经处理过
                        if (dtbtn2.Rows.Count > 0)
                        {

                            btn.Text = "修改";
                            if (ViewState["empid"].ToString() == txt_warehouse_keeper_id_sj.Value && txt_status_id.Text == "2")
                            {
                                if (txt_Already_Check.SelectedValue != "" && txt_Warehouse_quantity.Value == "")
                                {
                                    BTN_Warehouse_Keeper1.Text = "修改";
                                    BTN_Warehouse_Keeper2.Text = "提交";
                                }
                            }

                        }
                    }

                }
                if (ViewState["empid"].ToString() == txt_Userid.Value && txt_status_id.Text == "0")
                {
                    //修改附件
                    button_Assistant_01();

                    DataTable dt = new DataTable();
                    dt = YJ_CLASS.Getlog_group(Request["requestid"]);
                    //确认发货阶段打开时                 
                    DataTable dtkcl = YJ_CLASS.Getkcl("", txt_xmh.Value.Substring(0, 5), txt_domain.Value, 4, "");
                    if (dtkcl.Rows.Count > 1)
                    {
                        lab_bb.Text = "此零件号有多个版本，请确认发货版本";

                        lab_bb.BackColor = System.Drawing.Color.DarkOrange;
                    }
                   else
                    {
                        lab_bb.Text = "单个版本";
                        lab_bb.BackColor = System.Drawing.Color.Green;
                    }

                    if (txt_cp_status.SelectedValue == "新产品")
                    {
                        if (dt.Rows.Count < 5)
                        {
                            BTN_Sales_Assistant1.Visible = true;
                            BTN_Sales_Assistant1.Text = "修改";
                            BTN_Sales_Assistant2.Visible = false;

                        }
                        else if (dt.Rows.Count == 5)
                        {

                            BTN_Sales_Assistant1.Visible = true;
                            BTN_Sales_Assistant1.Text = "修改";
                            BTN_Sales_Assistant2.Visible = true;
                            txt_tzfy_date.Value = txt_yqfy_date.Text;

                        }

                    }
                    if (txt_cp_status.SelectedValue == "量产件")
                    {
                        if (dt.Rows.Count < 6)
                        {

                            BTN_Sales_Assistant1.Visible = true;
                            BTN_Sales_Assistant1.Text = "修改";
                            BTN_Sales_Assistant2.Visible = false;
                        }
                        else if (dt.Rows.Count == 6)
                        {

                            BTN_Sales_Assistant1.Visible = true;
                            BTN_Sales_Assistant1.Text = "修改";
                            BTN_Sales_Assistant2.Visible = true;
                            txt_tzfy_date.Value = txt_yqfy_date.Text;
                        }

                    }

                }
                //销售到货确认
                if (ViewState["empid"].ToString() == txt_Userid.Value && txt_status_id.Text == "4")
                {
                    txt_dh_date.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                //检验阶段质量填写参考号
                if (ViewState["empid"].ToString() == txt_quality_engineer_id.Value && txt_status_id.Text == "2")
                {
                    Getkcl(txt_xmh.Value, txt_domain_zzgc.Value);
                    button_Quality_04();
                }
                //备货阶段产品或计划填写参考号和数量
                if ((ViewState["empid"].ToString() == txt_product_engineer_id.Value || ViewState["empid"].ToString() == txt_planning_engineer_id.Value) && txt_status_id.Text == "1")
                {
                    Getkcl(txt_xmh.Value, txt_domain_zzgc.Value);
                }
                //仓库班长分工厂处理-- - 检验阶段
                if (ViewState["empid"].ToString() == txt_warehouse_keeper_id_sj.Value && txt_status_id.Text == "2")
                {
                    BTN_Warehouse_Keeper1.Visible = true;
                    BTN_Warehouse_Keeper2.Visible = true;
                    ViewState["lv"] = "CKBZ";
                    BTN_Warehouse_Keeper_DY.Visible = true;
                    //button_Warehouse_03();
                }
                if (ViewState["empid"].ToString() == txt_warehouse_keeper_id.Value && txt_status_id.Text == "3")
                {
                    BTN_Warehouse_Keeper3.Visible = true;
                    ViewState["lv"] = "CKBZ";
                    button_Warehouse_03();

                }

            }
            if (dtbtn.Rows.Count == 0)
            {
                //仓库班长分工厂处理-- - 检验阶段
                if (ViewState["empid"].ToString() == txt_warehouse_keeper_id_sj.Value && txt_status_id.Text == "2")
                {
                    BTN_Warehouse_Keeper1.Visible = true;
                    BTN_Warehouse_Keeper2.Visible = true;
                    ViewState["lv"] = "CKBZ";
                    BTN_Warehouse_Keeper_DY.Visible = true;
                    //button_Warehouse_03();
                }
                if (ViewState["empid"].ToString() == txt_warehouse_keeper_id.Value && txt_status_id.Text == "3")
                {
                    BTN_Warehouse_Keeper3.Visible = true;
                    ViewState["lv"] = "CKBZ";
                    button_Warehouse_03();

                }
            }
            //仓库班长和检验班长无固定人员分工厂处理---检验阶段
            if (dtbtn.Rows.Count == 0 && txt_status_id.Text == "2")
                {

                //if (ViewState["job"].ToString() == "仓库班长")
             
                if (ViewState["job"].ToString() == "检验班长")
                {
                    BTN_Checker_Monitor.Visible = true;

                    ViewState["lv"] = "JYBZ";
                }
                //if (txt_sqgc.Value == "上海工厂")
                //{
                //    if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流一部")
                //    {
                //        BTN_Warehouse_Keeper1.Visible = true;
                //        BTN_Warehouse_Keeper2.Visible = true;
                //        ViewState["lv"] = "CKBZ";
                //        BTN_Warehouse_Keeper_DY.Visible = true;
                //        //button_Warehouse_03();
                //    }
                //    if (ViewState["job"].ToString() == "检验班长" && dtemp.Rows[0]["dept_name"].ToString() == "质量一部")
                //    {
                //        BTN_Checker_Monitor.Visible = true;

                //        ViewState["lv"] = "JYBZ";
                //    }
                //}
                //if (txt_sqgc.Value == "昆山工厂")
                //{
                //    if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流二部")
                //    {
                //        BTN_Warehouse_Keeper1.Visible = true;
                //        BTN_Warehouse_Keeper2.Visible = true;
                //        ViewState["lv"] = "CKBZ";
                //        BTN_Warehouse_Keeper_DY.Visible = true;
                //        //button_Warehouse_03();
                //    }
                //    if (ViewState["job"].ToString() == "检验班长" && dtemp.Rows[0]["dept_name"].ToString() == "质量二部")
                //    {
                //        BTN_Checker_Monitor.Visible = true;
                //        ViewState["lv"] = "JYBZ";
                //    }
                //}
            }
            //仓库班长和检验班长无固定人员分工厂处理---发货阶段
            //if (ViewState["job"].ToString() == "仓库班长" && txt_status_id.Text == "3")
            if (dtbtn.Rows.Count == 0 && txt_status_id.Text == "3")
            {
               
                //if (txt_sqgc.Value == "上海工厂")
                //{
                //    if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流一部")
                //    {
                //        BTN_Warehouse_Keeper3.Visible = true;
                //        ViewState["lv"] = "CKBZ";
                //        button_Warehouse_03();
                //    }

                //}
                //else if (txt_sqgc.Value == "昆山工厂")
                //{
                //    if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流二部")
                //    {
                //        BTN_Warehouse_Keeper3.Visible = true;
                //        ViewState["lv"] = "CKBZ";
                //        button_Warehouse_03();
                //    }

                //}
            }



        }



    }
    //public void Getstatus_job()
    //{
    //    if (txt_status_id.Value == "")
    //    {
    //        BTN_Sales_Assistant1.Visible = true;

    //    }
    //    else if (txt_status_id.Value != "")
    //    {
    //        DataTable dtemp = YJ_CLASS.YJ_emp(ViewState["empid"].ToString());
    //        //订单确认阶段
    //        if (txt_status_id.Value == "0")
    //        {
    //            //还未分出产品状态
    //            if (txt_cp_status.SelectedValue == "")
    //            {
    //                if (ViewState["empid"].ToString() == txt_project_engineer_id.Value)
    //                {

    //                    BTN_Project_Engineer1.Visible = true;
    //                    ViewState["lv"] = "XMGCS";
    //                }
    //            }
    //            //分出产品状态后
    //            else if (txt_cp_status.SelectedValue != "")
    //            {
    //                BTN_Sales_Assistant1.Visible = true;
    //                if (txt_cp_status.SelectedValue == "新产品")
    //                {
    //                    if (ViewState["empid"].ToString() == txt_product_engineer_id.Value)
    //                    {
    //                        BTN_Product_Engineer1.Visible = true;
    //                        ViewState["lv"] = "CPGCS";
    //                    }
    //                    if (ViewState["empid"].ToString() == txt_Packaging_engineer_id.Value)
    //                    {
    //                        BTN_Packaging_Engineer1.Visible = true;
    //                        ViewState["lv"] = "BZGCS";
    //                    }
    //                    if (ViewState["empid"].ToString() == txt_quality_engineer_id.Value)
    //                    {
    //                        BTN_Quality_Engineer2.Visible = true;
    //                        ViewState["lv"] = "ZLGCS";

    //                    }

    //                    if (ViewState["empid"].ToString() == txt_Userid.Value)
    //                    {

    //                        //获取前面处理人员个数，判断是否可以订舱
    //                        DataTable dt = new DataTable();
    //                        dt = YJ_CLASS.Getlog_group(Request["requestid"]);
    //                        if (dt.Rows.Count > 5)
    //                        {
    //                            BTN_Sales_Assistant2.Visible = true;
    //                        }
    //                        //获取价格
    //                        Getjg(txt_xmh.Value, txt_gkdm.Value, txt_domain.Value);
    //                    }



    //                }
    //                else if (txt_cp_status.SelectedValue == "量产件")
    //                {
    //                    if (ViewState["empid"].ToString() == txt_planning_engineer_id.Value)
    //                    {
    //                        BTN_Planning_Engineer1.Visible = true;
    //                        ViewState["lv"] = "WLJH";
    //                    }
    //                    if (ViewState["empid"].ToString() == txt_Packaging_engineer_id.Value)
    //                    {
    //                        BTN_Packaging_Engineer1.Visible = true;
    //                        ViewState["lv"] = "BZGCS";
    //                    }
    //                    if (ViewState["empid"].ToString() == txt_quality_engineer_id.Value)
    //                    {

    //                        BTN_Quality_Engineer1.Visible = true;
    //                        BTN_Quality_Engineer2.Visible = true;
    //                        ViewState["lv"] = "ZLGCS";
    //                    }
    //                    if (ViewState["empid"].ToString() == txt_Userid.Value)
    //                    {

    //                        //获取前面处理人员个数，判断是否可以订舱
    //                        DataTable dt = new DataTable();
    //                        dt = YJ_CLASS.Getlog_group(Request["requestid"]);
    //                        if (dt.Rows.Count > 6)
    //                        {
    //                            BTN_Sales_Assistant2.Visible = true;
    //                        }
    //                        //获取价格
    //                        Getjg(txt_xmh.Value, txt_gkdm.Value, txt_domain.Value);
    //                    }
    //                }


    //            }

    //        }
    //        //订单备货阶段
    //        else if (txt_status_id.Value == "1")
    //        {
    //            if (ViewState["empid"].ToString() == txt_Packaging_engineer_id.Value)
    //            {
    //                BTN_Packaging_Engineer2.Visible = true;
    //                ViewState["lv"] = "BZGCS";
    //            }
    //            if (txt_cp_status.SelectedValue == "新产品")
    //            {
    //                if (ViewState["empid"].ToString() == txt_product_engineer_id.Value)
    //                {
    //                    BTN_Product_Engineer2.Visible = true;
    //                    ViewState["lv"] = "CPGCS";
    //                    //获取库存量
    //                    Getkcl(txt_xmh.Value, txt_domain.Value);
    //                }

    //            }
    //            else if (txt_cp_status.SelectedValue == "量产件")
    //            {
    //                if (ViewState["empid"].ToString() == txt_planning_engineer_id.Value)
    //                {
    //                    BTN_Planning_Engineer2.Visible = true;
    //                    ViewState["lv"] = "WLJH";
    //                    //获取库存量
    //                    Getkcl(txt_xmh.Value, txt_domain.Value);
    //                }

    //            }

    //        }
    //        //检验阶段
    //        else if (txt_status_id.Value == "2")
    //        {

    //            if (ViewState["empid"].ToString() == txt_quality_engineer_id.Value)
    //            {
    //                BTN_Quality_Engineer3.Visible = true;
    //                BTN_Quality_Engineer4.Visible = true;
    //                ViewState["lv"] = "ZLGCS";
    //                //获取库存量
    //                Getkcl(txt_xmh.Value, txt_domain.Value);
    //            }
    //            if (txt_sqgc.Value == "上海工厂")
    //            {

    //                if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流一部")
    //                {
    //                    BTN_Warehouse_Keeper1.Visible = true;
    //                    BTN_Warehouse_Keeper2.Visible = true;
    //                    ViewState["lv"] = "CKBZ";
    //                }
    //                if (ViewState["job"].ToString() == "检验班长" && dtemp.Rows[0]["dept_name"].ToString() == "质量一部")
    //                {
    //                    BTN_Checker_Monitor.Visible = true;

    //                    ViewState["lv"] = "JYBZ";
    //                }
    //            }
    //            else if (txt_sqgc.Value == "昆山工厂")
    //            {
    //                if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流二部")
    //                {
    //                    BTN_Warehouse_Keeper1.Visible = true;
    //                    BTN_Warehouse_Keeper2.Visible = true;
    //                    ViewState["lv"] = "CKBZ";
    //                }
    //                if (ViewState["job"].ToString() == "检验班长" && dtemp.Rows[0]["dept_name"].ToString() == "质量二部")
    //                {
    //                    BTN_Checker_Monitor.Visible = true;
    //                    ViewState["lv"] = "JYBZ";
    //                }
    //            }
    //        }
    //        //发货阶段
    //        else if (txt_status_id.Value == "3")
    //        {
    //            if (ViewState["empid"].ToString() == txt_project_engineer_id.Value)
    //            {
    //                BTN_Project_Engineer2.Visible = true;
    //                ViewState["lv"] = "XMGCS";
    //            }
    //            if (txt_sqgc.Value == "上海工厂")
    //            {
    //                if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流一部")
    //                {
    //                    BTN_Warehouse_Keeper3.Visible = true;
    //                    ViewState["lv"] = "CKBZ";
    //                }

    //            }
    //            else if (txt_sqgc.Value == "昆山工厂")
    //            {
    //                if (ViewState["job"].ToString() == "仓库班长" && dtemp.Rows[0]["dept_name"].ToString() == "物流二部")
    //                {
    //                    BTN_Warehouse_Keeper3.Visible = true;
    //                    ViewState["lv"] = "CKBZ";

    //                }

    //            }
    //        }
    //        //确认到货
    //        else if (txt_status_id.Value == "4")
    //        {
    //            if (ViewState["empid"].ToString() == txt_Userid.Value)
    //            {

    //                BTN_Sales_Assistant3.Visible = true;

    //            }
    //        }
    //        dtemp.Clear();
    //        }
    //}




    protected void txt_reference_number_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_reference_number.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请先选择参考号！')", true);
        }
        else
        {
            DataTable dtkcl = YJ_CLASS.Getkcl(txt_xmh.Value, "", txt_domain_zzgc.Value, 3, txt_reference_number.SelectedValue);
            if (dtkcl.Rows.Count != 0)
            {
                //txt_Stocking_quantity.Value = dtkcl.Rows[0]["ld_qty_oh"].ToString();
                if (Convert.ToInt32(dtkcl.Rows[0]["ld_qty_oh"].ToString()) < Convert.ToInt32(txt_Stocking_quantity.Value))
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('库存量不能小于备货数量！')", true);
                    BTN_Product_Engineer2.Visible = false;
                }
                else
                {
                    BTN_Product_Engineer2.Visible = true;
                }
            }
            dtkcl.Clear();
        }
        ViewState["lv"] = "CPGCS";
    }
    protected void txt_yhsl_TextChanged(object sender, EventArgs e)
    {
        if (txt_yhsl.Text != "" && txt_ljdj.Text != "" && txt_ljzl.Value != "")
        {
            txt_ljzj.Value = Convert.ToString(Convert.ToInt32(txt_yhsl.Text) * Convert.ToDecimal(txt_ljdj.Text));
            txt_Packing_net_weight.Value = Convert.ToString(Convert.ToInt32(txt_yhsl.Text) * Convert.ToDecimal(txt_ljzl.Value));

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('价格和数量和零件重量都需要填写！')", true);
        }
    }

    protected void txt_Box_specifications1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtbox = YJ_CLASS.YJ_BASE2("7", txt_Box_specifications1.SelectedValue);
        if (txt_Box_specifications1.SelectedValue == "")
        {
            txt_Box_weight1.Value = "";
        }
        else
        {
            txt_Box_weight1.Value = dtbox.Rows[0]["VALUE"].ToString();
        }
        dtbox.Clear();
        ViewState["lv"] = "BZGCS";
        if (txt_Box_weight1.Value != "" && txt_Box_weight2.Value != "")
        {
            txt_Box_weight_total.Value = Convert.ToString(Convert.ToDecimal(txt_Box_weight1.Value) + Convert.ToDecimal(txt_Box_weight2.Value));
            txt_Box_quantity.Value = Convert.ToString(2);
        }
        else
        {
            txt_Box_weight_total.Value = txt_Box_weight1.Value;
            txt_Box_quantity.Value = Convert.ToString(1);
        }
        if (txt_Packing_net_weight.Value != "" && txt_Box_weight_total.Value != "")
        {
            txt_Package_weight.Value = Convert.ToString(Convert.ToDecimal(txt_Packing_net_weight.Value) + Convert.ToDecimal(txt_Box_weight_total.Value));

        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('包装净重和样件箱总量不能为空！')", true);
        }
        if (txt_Box_specifications1.SelectedValue == "其他")
        {
            txt_Box_weight_total.Disabled = false;
            txt_Package_weight.Disabled = false;
        }

    }

    protected void txt_Box_specifications2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtbox = YJ_CLASS.YJ_BASE2("7", txt_Box_specifications2.SelectedValue);
        if (txt_Box_specifications2.SelectedValue == "")
        {
            txt_Box_weight2.Value = "";
        }
        else
        {
            txt_Box_weight2.Value = dtbox.Rows[0]["VALUE"].ToString();
        }

        dtbox.Clear();
        ViewState["lv"] = "BZGCS";
        if (txt_Box_weight1.Value != "" && txt_Box_weight2.Value != "")
        {
            txt_Box_weight_total.Value = Convert.ToString(Convert.ToDecimal(txt_Box_weight1.Value) + Convert.ToDecimal(txt_Box_weight2.Value));
            txt_Box_quantity.Value = Convert.ToString(2);
        }
        else
        {
            txt_Box_weight_total.Value = txt_Box_weight2.Value;
            txt_Box_quantity.Value = Convert.ToString(1);
        }
        if (txt_Packing_net_weight.Value != "" && txt_Box_weight_total.Value != "")
        {
            txt_Package_weight.Value = Convert.ToString(Convert.ToDecimal(txt_Packing_net_weight.Value) + Convert.ToDecimal(txt_Box_weight_total.Value));

        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('包装净重和样件箱总量不能为空！')", true);
        }
    }
    public void XS_BQ()
    {
        //if (txt_gkdm.Value != "")
        if (DDL_DY.SelectedValue != "")
        {
            //if (txt_gkdm.Value.Substring(0, 3) == "101")
            if (DDL_DY.SelectedValue == "101")
            {
                FORD.Visible = true;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;
            }
            //if (txt_gkdm.Value.Substring(0, 3) == "102")
                if (DDL_DY.SelectedValue == "102")
                {
                FORD.Visible = false;
                Chrylser.Visible = true;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            //if (txt_gkdm.Value.Substring(0, 3) == "117")
                if (DDL_DY.SelectedValue == "117")
                {

                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = true;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;
            }
            //if (txt_gkdm.Value.Substring(0, 3) == "115")
                if (DDL_DY.SelectedValue == "115")
                {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = true;
                AAM2.Visible = true;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            //if (txt_gkdm.Value.Substring(0, 3) == "109")
            if (DDL_DY.SelectedValue == "109")
            {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = true;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            //if (txt_gkdm.Value.Substring(0, 3) == "119")
                if (DDL_DY.SelectedValue == "119")
                {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = true;
                Daimler2.Visible = true;

            }
        }
        else
        {
            if (txt_gkdm.Value.Substring(0, 3) == "101")
            //if (DDL_DY.SelectedValue == "101")
            {
                FORD.Visible = true;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;
            }
            if (txt_gkdm.Value.Substring(0, 3) == "102")
                //if (DDL_DY.SelectedValue == "102")
            {
                FORD.Visible = false;
                Chrylser.Visible = true;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            if (txt_gkdm.Value.Substring(0, 3) == "117")
                //if (DDL_DY.SelectedValue == "117")
                {

                    FORD.Visible = false;
                    Chrylser.Visible = false;
                    GM.Visible = true;
                    AAM.Visible = false;
                    AAM2.Visible = false;
                    BBAC.Visible = false;
                    Daimler.Visible = false;
                    Daimler2.Visible = false;
                }
            if (txt_gkdm.Value.Substring(0, 3) == "115")
            //if (DDL_DY.SelectedValue == "115")
            {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = true;
                AAM2.Visible = true;
                BBAC.Visible = false;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            if (txt_gkdm.Value.Substring(0, 3) == "109")
                //if (DDL_DY.SelectedValue == "109")
            {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = true;
                Daimler.Visible = false;
                Daimler2.Visible = false;

            }
            if (txt_gkdm.Value.Substring(0, 3) == "119")
                //if (DDL_DY.SelectedValue == "119")
            {
                FORD.Visible = false;
                Chrylser.Visible = false;
                GM.Visible = false;
                AAM.Visible = false;
                AAM2.Visible = false;
                BBAC.Visible = false;
                Daimler.Visible = true;
                Daimler2.Visible = true;

            }

        }
    }
    protected void BTN_Sales_Assistant1_Click(object sender, EventArgs e)
    {
     
        //福特标签
        if (txt_gkdm.Value == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('客户不能为空！')", true);
        }
        else
        {
            if (txt_gkdm.Value.Substring(0, 3) == "101" && txt_Serial_No.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('是否需要Serial_No不能为空！')", true);

            }
            //else if (txt_gkdm.Value.Substring(0, 3) == "102" && DDL_lable_type.SelectedValue == "" && txt_mdgc_Chrylser.Value == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('lable_type_Chrylser不能为空！')", true);

            //}
            else if (txt_gkdm.Value.Substring(0, 3) == "117" && txt_ENG_GM.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('ENG. DESIGN RECORD CHG LVL不能为空！')", true);

            }
            else if (txt_gkdm.Value.Substring(0, 3) == "115" && DDL_sbh_AAM.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('产生的识别号不能为空！')", true);

            }
            else if (txt_gkdm.Value.Substring(0, 3) == "109" && txt_ZGS_BBAC.Value == "" && txt_EQ_BBAC.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件级别不能为空！')", true);

            }
            else if (txt_gkdm.Value.Substring(0, 3) == "119" && DDL_Part_Status_Daimler.SelectedValue == "" && DDL_Part_Change_Daimler.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('零件状态不能为空！')", true);
            }
            else
            {
                //获取打印标签内容
                string oth = "";
                if (DDL_DY.SelectedValue != "")
                {
                    
                    for (int i = 0; i < CBL_DY.Items.Count; i++)
                    {
                        if (CBL_DY.Items[i].Selected)
                        {
                            oth += ";" + CBL_DY.Items[i].Value;
                        }
                    }
                }

                if (gvFile_ddfj.Rows.Count > 0)
                {
                    int insert = -1;
                    int request = 0;

                    if (BTN_Sales_Assistant1.Text == "修改")
                    {
                        request = Convert.ToInt32(Request["requestid"]);
                    }
                    if (txt_Line_Code.Value == "")
                    {
                        txt_Line_Code.Value = "1";
                    }
                    if (txt_Serial_No.SelectedValue == "")
                    {
                        txt_Serial_No.SelectedValue = "否";
                    }

                    insert = YJ_Confirm_Action_SQL.YJ_Sales_Assistant_PRO1(request, txt_Code.Value, Convert.ToDateTime(txt_CreateDate.Value), txt_Userid.Value,
            txt_UserName.Value, txt_UserName_AD.Value, txt_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, txt_Sales_engineer_id.Value,
            txt_Sales_engineer.Value, txt_Sales_engineer_AD.Value, txt_Sales_engineer_job.Value, txt_product_engineer_id.Value, txt_product_engineer.Value,
            txt_product_engineer_AD.Value, txt_product_engineer_job.Value, txt_quality_engineer_id.Value, txt_quality_engineer.Value, txt_quality_engineer_AD.Value,
            txt_quality_engineer_job.Value, txt_project_engineer_id.Value, txt_project_engineer.Value, txt_project_engineer_AD.Value, txt_project_engineer_job.Value,
            txt_Packaging_engineer_id.Value, txt_Packaging_engineer.Value, txt_Packaging_engineer_AD.Value, txt_Packaging_engineer_job.Value, txt_planning_engineer_id.Value,
            txt_planning_engineer.Value, txt_planning_engineer_AD.Value, txt_planning_engineer_job.Value, txt_warehouse_keeper_id.Value, txt_warehouse_keeper.Value,
            txt_warehouse_keeper_AD.Value, txt_warehouse_keeper_job.Value, txt_checker_monitor_id.Value, txt_checker_monitor.Value, txt_checker_monitor_AD.Value,
            txt_checker_monitor_job.Value, txt_Userid.Value, txt_content_Sales_Assistant.Value, txt_sqgc.Value, Convert.ToInt32(txt_domain.Value), txt_gkddh.Value, Convert.ToDateTime(txt_sddd_date.Value), txt_xmh.Value,
            txt_ljh.Value, txt_ljmc.Value, Convert.ToDecimal(txt_ljzl.Value), Convert.ToDecimal(txt_ljdj.Text), txt_fhz.Value, txt_hb.Value, txt_gkdm.Value, txt_gkmc.Value, Convert.ToDecimal(txt_ljzj.Value), Convert.ToInt32(txt_yhsl.Text), Convert.ToInt32(txt_kcl.Value),
            Convert.ToDateTime(txt_kc_date.Value), Convert.ToDateTime(txt_tz_date.Value == "" ? DateTime.Now.ToString("yyyy/MM/dd") : txt_tz_date.Value), Convert.ToDateTime(txt_yqdh_date.Value), Convert.ToDateTime(txt_yqfy_date.Text), txt_sktj.SelectedValue, txt_ystk.SelectedValue, txt_ysfs.SelectedValue, txt_yfzffs.SelectedValue, Convert.ToDecimal(txt_yfje.Value),
            txt_shrxx.Value, txt_shdz.Value, txt_yq.Value, txt_bqyq.Value, txt_shwjyq.Value, txt_other_yq.Value, txt_gysdm.Value, txt_xmjd.Value, Convert.ToDecimal(txt_Packing_net_weight.Value), Convert.ToDateTime(txt_special_require.Value),
            Convert.ToDateTime(txt_Packaging_require.Value), Convert.ToDateTime(txt_goods_require.Value), Convert.ToDateTime(txt_goods_require_wl.Value), Convert.ToDateTime(txt_check_require.Value), Convert.ToDateTime(txt_check_require_jy.Value),
            Convert.ToDateTime(txt_shipping_require.Value), txt_Sorting_list.Value, txt_zzgc.Value, txt_domain_zzgc.Value, txt_Serial_No.SelectedValue, Convert.ToInt32(txt_Line_Code.Value), txt_ddlxr.Value, txt_ddlxphone.Value, Convert.ToDecimal(txt_ljdj_qad.Text),
            DDL_lable_type.SelectedValue, txt_mdgc_Chrylser.Value, txt_ENG_GM.Value, DDL_sbh_AAM.SelectedValue, txt_sbh_HM_AAM.Value, txt_sbh_NO_AAM.Value, txt_ZGS_BBAC.Value, txt_EQ_BBAC.Value, DDL_Part_Status_Daimler.SelectedValue, txt_Part_Status_Daimler_ms.Value, DDL_Part_Change_Daimler.SelectedValue, DDL_DY.SelectedValue, oth, BTN_Sales_Assistant1.Text);
                    if (insert > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                        BTN_Sales_Assistant1.Enabled = false;
                        //txt_tzfy_date.Value = txt_yqfy_date.Text;
                        YJ_CLASS.SendMail(request, Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                        //备货完以后，修改要货数量同步修改备货数量
                        if (Convert.ToInt32(txt_status_id.Text) > 1)
                        {
                            if (txt_Stocking_quantity.Value != txt_yhsl.Text && txt_Stocking_quantity.Value != "" && txt_cp_status.SelectedValue == "新产品")
                            {
                                string strSql = "update form1_Sale_YJ_dt_Product_Engineer set Stocking_quantity='" + txt_yhsl.Text + "' where requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSql);
                            }
                            if (txt_Stocking_quantity_wl.Value != txt_yhsl.Text && txt_Stocking_quantity_wl.Value != "" && txt_cp_status.SelectedValue == "量产件")
                            {
                                string strSql = "update form1_Sale_YJ_dt_Planning_Engineer set Stocking_quantity_wl='" + txt_yhsl.Text + "' where requestid='" + Convert.ToInt32(Request["requestid"]) + "' ";
                                DbHelperSQL.ExecuteSql(strSql);
                            }
                        }


                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传订单附件！')", true);
                }
            }
        }
    }

    protected void BTN_Sales_Assistant2_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Sales_Assistant_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Sales_Assistant.Value, Convert.ToDateTime(txt_tzfy_date.Value), BTN_Sales_Assistant2.Text);
        if (update > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Sales_Assistant2.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后提交！')", true);
        }
    }

    protected void BTN_Sales_Assistant3_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Sales_Assistant_PRO3(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Sales_Assistant.Value, Convert.ToDateTime(txt_dh_date.Value), BTN_Sales_Assistant3.Text);
        if (update > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Sales_Assistant3.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
    }
    protected void BTN_Project_Engineer1_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Project_Engineer_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Project_Engineer.Value, txt_cp_status.SelectedValue, txt_special_yq.Value, BTN_Project_Engineer1.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Project_Engineer1.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "XMGCS";

    }

    protected void BTN_Project_Engineer2_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Project_Engineer_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Project_Engineer.Value, txt_cp_status.SelectedValue,
txt_special_yq.Value, txt_customer_request.SelectedValue, BTN_Project_Engineer2.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Project_Engineer2.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "XMGCS";
    }

    protected void BTN_Product_Engineer1_Click(object sender, EventArgs e)
    {

        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Product_Engineer_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Product_Engineer.Value, txt_is_check_qcc.SelectedValue, txt_is_check_szb.SelectedValue,
txt_is_check_wg.SelectedValue, txt_is_check_jj.SelectedValue, txt_check_other_ms.Value, txt_check_lj.SelectedValue, txt_Already_Product.SelectedValue, txt_Check_number.Text, txt_reference_number.SelectedValue, Convert.ToInt32("0" + txt_Stocking_quantity.Value), BTN_Product_Engineer1.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Product_Engineer1.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "CPGCS";
    }

    protected void BTN_Product_Engineer2_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_Stocking_quantity.Value) >= Convert.ToInt32(txt_yhsl.Text))
        {
            int update = -1;
            update = YJ_Confirm_Action_SQL.YJ_Product_Engineer_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Product_Engineer.Value, txt_is_check_qcc.SelectedValue, txt_is_check_szb.SelectedValue,
    txt_is_check_wg.SelectedValue, txt_is_check_jj.SelectedValue, txt_check_other_ms.Value, txt_check_lj.SelectedValue, txt_Already_Product.SelectedValue, txt_Check_number.Text, txt_reference_number.SelectedValue, Convert.ToInt32("0" + txt_Stocking_quantity.Value), BTN_Product_Engineer2.Text);
            if (update > 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                BTN_Product_Engineer2.Enabled = false;
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('备货数量必须大于要货数量！')", true);

        }
        ViewState["lv"] = "CPGCS";
    }

    protected void BTN_Packaging_Engineer1_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Packaging_Engineer_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Packaging_Engineer.Value, txt_Packaging_scheme.Value,
Convert.ToDecimal(txt_Box_weight_total.Value), Convert.ToInt32(txt_Box_quantity.Value), Convert.ToDecimal(txt_Packing_net_weight.Value), Convert.ToDecimal(txt_Package_weight.Value), txt_Box_specifications1.SelectedValue, Convert.ToDecimal("0" + txt_Box_weight1.Value),
txt_Box_specifications2.SelectedValue, Convert.ToDecimal("0" + txt_Box_weight2.Value), txt_Other_box_dec1.Value, txt_Other_box_dec2.Value, Convert.ToInt32("0" + txt_Per_Crate_Qty.Value), Convert.ToInt32("0" + txt_Per_Crate_Qty2.Value), BTN_Packaging_Engineer1.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Packaging_Engineer1.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "BZGCS";
    }

    protected void BTN_Packaging_Engineer2_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Packaging_Engineer_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Packaging_Engineer.Value, txt_Packaging_scheme.Value,
Convert.ToDecimal(txt_Box_weight_total.Value), Convert.ToInt32(txt_Box_quantity.Value), Convert.ToDecimal(txt_Packing_net_weight.Value), Convert.ToDecimal(txt_Package_weight.Value), txt_Box_specifications1.SelectedValue, Convert.ToDecimal(txt_Box_weight1.Value),
txt_Box_specifications2.SelectedValue, Convert.ToDecimal(txt_Box_weight2.Value), txt_Other_box_dec1.Value, txt_Other_box_dec2.Value, txt_Packing_goods_Already.SelectedValue, BTN_Packaging_Engineer2.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Packaging_Engineer2.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "BZGCS";
    }

    protected void BTN_Planning_Engineer1_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Planning_Engineer_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Planning_Engineer.Value, txt_Already_Planning.SelectedValue, Convert.ToInt32("0" + txt_Current_inventory_quantity.Value),
Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToInt32("0" + txt_Stocking_quantity_wl.Value), BTN_Planning_Engineer1.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Planning_Engineer1.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "WLJH";
    }

    protected void BTN_Planning_Engineer2_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_Current_inventory_quantity.Value) >= Convert.ToInt32(txt_yhsl.Text))
        {
            int update = -1;
            update = YJ_Confirm_Action_SQL.YJ_Planning_Engineer_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Planning_Engineer.Value, txt_Already_Planning.SelectedValue, Convert.ToInt32("0" + txt_Current_inventory_quantity.Value),
    Convert.ToDateTime(txt_Current_inventory_date.Value), Convert.ToInt32("0" + txt_Stocking_quantity_wl.Value), BTN_Planning_Engineer2.Text);
            if (update > 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                BTN_Planning_Engineer2.Enabled = false;
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('库存数量必须大于要货数量！')", true);

        }
        ViewState["lv"] = "WLJH";
    }

    protected void BTN_Warehouse_Keeper1_Click(object sender, EventArgs e)
    {
        if (txt_isdy.Text == "")
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(BTN_Warehouse_Keeper1, this.GetType(), "alert", "layer.alert('请先打印在库分选单后提交！')", true);
        }
        else
        {
            //获取当前检验班长是谁处理
            DataTable dtemp = YJ_CLASS.YJ_emp(txt_update_user.Value);
            string gh = txt_update_user.Value;
            string name = dtemp.Rows[0]["lastname"].ToString();
            string ad = dtemp.Rows[0]["ADAccount"].ToString();
            string job = dtemp.Rows[0]["jobtitlename"].ToString();
            dtemp.Clear();

            int update = -1;
            update = YJ_Confirm_Action_SQL.YJ_Warehouse_Keeper_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Warehouse_Keeper.Value,
    txt_Already_Check.SelectedValue, Convert.ToInt32("0" + txt_Warehouse_quantity.Value), gh, name, ad, job, BTN_Warehouse_Keeper1.Text);
            if (update > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                BTN_Warehouse_Keeper1.Enabled = false;
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
            }
        }
        ViewState["lv"] = "CKBZ";
    }

    protected void BTN_Warehouse_Keeper2_Click(object sender, EventArgs e)
    {


        if (txt_Already_Check.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请送检确认再做取回！')", true);
        }
        else if (txt_Qualified_quantity.Value == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请检验组检验完才可取回确认！')", true);
        }
        else
        {
            //量产件
            if (txt_cp_status.SelectedValue == "量产件")
            {
                if (Convert.ToInt32(txt_Warehouse_quantity.Value) != Convert.ToInt32(txt_Stocking_quantity_wl.Value))
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('取回数量必须等于备货数量！')", true);
                }
                else
                {
                    int update = -1;
                    update = YJ_Confirm_Action_SQL.YJ_Warehouse_Keeper_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Warehouse_Keeper.Value,
            txt_Already_Check.SelectedValue, Convert.ToInt32("0" + txt_Warehouse_quantity.Value), BTN_Warehouse_Keeper2.Text);
                    if (update > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                        BTN_Warehouse_Keeper2.Enabled = false;
                        YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
                    }
                }
            }
            if (txt_cp_status.SelectedValue == "新产品")
            {
                if (Convert.ToInt32(txt_Warehouse_quantity.Value) != Convert.ToInt32(txt_Stocking_quantity.Value))
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('取回数量必须等于备货数量！')", true);
                }
                else
                {
                    int update = -1;
                    update = YJ_Confirm_Action_SQL.YJ_Warehouse_Keeper_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Warehouse_Keeper.Value,
            txt_Already_Check.SelectedValue, Convert.ToInt32("0" + txt_Warehouse_quantity.Value), BTN_Warehouse_Keeper2.Text);
                    if (update > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                        BTN_Warehouse_Keeper2.Enabled = false;
                        YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
                    }
                }
            }
        }
        ViewState["lv"] = "CKBZ";
    }

    protected void BTN_Warehouse_Keeper3_Click(object sender, EventArgs e)
    {
        if (gvFile_Package_photo.Rows.Count > 0 && gvFile_Shipping_photos.Rows.Count > 0)
        {
            int update = -1;
            update = YJ_Confirm_Action_SQL.YJ_Warehouse_Keeper_PRO3(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Warehouse_Keeper.Value,
    txt_Already_Check.SelectedValue, Convert.ToInt32("0" + txt_Warehouse_quantity.Value), BTN_Warehouse_Keeper3.Text);
            if (update > 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                BTN_Warehouse_Keeper3.Enabled = false;
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('必须上传包装和发货附件！')", true);

        }
        ViewState["lv"] = "CKBZ";
    }

    protected void BTN_Checker_Monitor_Click(object sender, EventArgs e)
    {
        int quantity = Convert.ToInt32(txt_Qualified_quantity.Value) + Convert.ToInt32(txt_Unqualified_quantity.Value);
        int Stocking_quantity = 0;
        if (txt_cp_status.SelectedValue == "新产品")
        {

            Stocking_quantity = Convert.ToInt32(txt_Stocking_quantity.Value);
        }
        else
        {
            Stocking_quantity = Convert.ToInt32(txt_Stocking_quantity_wl.Value);
        }


        if (quantity == Stocking_quantity)
        {

            //获取当前检验班长是谁处理
            DataTable dtemp = YJ_CLASS.YJ_emp(txt_update_user.Value);
            string gh = txt_update_user.Value;
            string name = dtemp.Rows[0]["lastname"].ToString();
            string ad = dtemp.Rows[0]["ADAccount"].ToString();
            string job = dtemp.Rows[0]["jobtitlename"].ToString();
            //ad = "检验班长";
            dtemp.Clear();
            int update = -1;
            update = YJ_Confirm_Action_SQL.YJ_Checker_Monitor_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Checker_Monitor.Value, Convert.ToInt32(txt_Qualified_quantity.Value),
    Convert.ToInt32(txt_Unqualified_quantity.Value), txt_Unqualified_description.Value, gh, name, ad, job, BTN_Checker_Monitor.Text);
            if (update > 0)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                BTN_Checker_Monitor.Enabled = false;
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                //检验完增加质量工程师通知邮件
                //工号

                DataTable dtuser = YJ_CLASS.YJ_emp(txt_quality_engineer_id.Value);
                //邮件地址
                string usermail = dtuser.Rows[0]["email"].ToString();
                //string usermail = "angela.xu@pgi.cn";
                //姓名
                string username = dtuser.Rows[0]["lastname"].ToString();
                //操作事项
                string work = "质量检验结果确认";
                //角色
                string job_ZL = "质量工程师";
                //接收时间
                string Receive_time = System.DateTime.Now.ToString();
                //主题
                string lstitle = "样件管理系统待签核邮件通知";
                //链接code
                string Code = txt_Code.Value;
                int requestid = Convert.ToInt32(Request["requestid"]);
                string LINKCode = "<a href='http://172.16.5.26:8010/Yangjian/Yangjian.aspx?requestid=" + requestid + "'>" + Code + "</a>";
                //链接名称
                string LINKName = txt_xmh.Value;
                //第一行
                string code = "样件单号";
                string name_mail = "项目号";

                //邮件发送
                string body = "检验员检验完成：请尽快确认检验结果!";
                Mail.SendMail(usermail, lstitle, LINKCode, LINKName, work, job_ZL, username, Receive_time, code, name_mail, body);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检验数量合计必须等于备货数量！')", true);
        }
        ViewState["lv"] = "JYBZ";
    }

    protected void BTN_Quality_Engineer1_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Quality_Engineer_PRO1(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Quality_Engineer.Value, txt_is_check_qcc_zl.SelectedValue,
txt_is_check_szb_zl.SelectedValue, txt_is_check_wg_zl.SelectedValue, txt_check_other_ms_zl.Value, txt_check_lj_zl.SelectedValue, txt_Already_zl.SelectedValue, txt_is_check_jj_zl.SelectedValue,
txt_Check_number_zl.Text, txt_reference_number_zl.SelectedValue, Convert.ToInt32("0" + txt_Confirm_quantity.Value), BTN_Quality_Engineer1.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Quality_Engineer1.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "ZLGCS";
    }

    protected void BTN_Quality_Engineer2_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Quality_Engineer_PRO2(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Quality_Engineer.Value, txt_is_check_qcc_zl.SelectedValue,
txt_is_check_szb_zl.SelectedValue, txt_is_check_wg_zl.SelectedValue, txt_check_other_ms_zl.Value, txt_check_lj_zl.SelectedValue, txt_Already_zl.SelectedValue, txt_is_check_jj_zl.SelectedValue,
txt_Check_number_zl.Text, txt_reference_number_zl.SelectedValue, Convert.ToInt32("0" + txt_Confirm_quantity.Value), BTN_Quality_Engineer2.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Quality_Engineer2.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "ZLGCS";
    }

    protected void BTN_Quality_Engineer3_Click(object sender, EventArgs e)
    {
        int update = -1;
        update = YJ_Confirm_Action_SQL.YJ_Quality_Engineer_PRO3(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Quality_Engineer.Value, txt_is_check_qcc_zl.SelectedValue,
txt_is_check_szb_zl.SelectedValue, txt_is_check_wg_zl.SelectedValue, txt_check_other_ms_zl.Value, txt_check_lj_zl.SelectedValue, txt_Already_zl.SelectedValue, txt_is_check_jj_zl.SelectedValue,
txt_Check_number_zl.Text, txt_reference_number_zl.SelectedValue, Convert.ToInt32("0" + txt_Confirm_quantity.Value), BTN_Quality_Engineer3.Text);
        if (update > 0)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
            BTN_Quality_Engineer3.Enabled = false;
            YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
        }
        ViewState["lv"] = "ZLGCS";
    }

    protected void BTN_Quality_Engineer4_Click(object sender, EventArgs e)
    {
        if (txt_Qualified_quantity.Value == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('检验完成后才可提交确认，请联系检验组检验！')", true);
        }
        else
        {
            //合格数量小于要货数量，流程干预功能开启
            int sl = Convert.ToInt32(txt_Confirm_quantity.Value) + Convert.ToInt32(txt_Unqualified_quantity_zl.Value);
            int yhsl = Convert.ToInt32(txt_yhsl.Text);
            int hgsl = Convert.ToInt32(txt_Confirm_quantity.Value);
            if (hgsl < yhsl)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合格数量必须大于要货数量，否则请干预流程至销售，重新确认！')", true);
                gy_zl.Visible = true;
                BTN_Process_intervention_zl.Visible = true;
            }
            else
            {
                gy_zl.Visible = false;
                BTN_Process_intervention_zl.Visible = false;
                //合计数量必须等于备货数量
                if (sl == Convert.ToInt32("0" + txt_Stocking_quantity.Value) || sl == Convert.ToInt32("0" + txt_Stocking_quantity_wl.Value))
                {
                    //量产件
                    if (txt_cp_status.SelectedValue == "量产件")
                    {
                        if (gvFile_check_fj.Rows.Count <= 0 && (txt_check_lj_zl.SelectedValue != "无需检验" && txt_check_lj_zl.SelectedValue != "检验组-结束"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传检验报告！')", true);
                        }
                        else
                        {
                            int update = -1;
                            update = YJ_Confirm_Action_SQL.YJ_Quality_Engineer_PRO4(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Quality_Engineer.Value, txt_is_check_qcc_zl.SelectedValue,
                    txt_is_check_szb_zl.SelectedValue, txt_is_check_wg_zl.SelectedValue, txt_check_other_ms_zl.Value, txt_check_lj_zl.SelectedValue, txt_Already_zl.SelectedValue, txt_is_check_jj_zl.SelectedValue,
                    txt_Check_number_zl.Text, txt_reference_number_zl.SelectedValue, Convert.ToInt32("0" + txt_Confirm_quantity.Value), Convert.ToInt32("0" + txt_Unqualified_quantity_zl.Value), BTN_Quality_Engineer4.Text);
                            if (update > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                                BTN_Quality_Engineer4.Enabled = false;
                                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
                            }

                        }

                    }

                    //新产品
                    if (txt_cp_status.SelectedValue == "新产品")
                    {
                        if (gvFile_check_fj.Rows.Count <= 0 && (txt_check_lj.SelectedValue != "无需检验" && txt_check_lj.SelectedValue != "检验组-结束"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传检验报告！')", true);
                        }
                        else
                        {
                            int update = -1;
                            update = YJ_Confirm_Action_SQL.YJ_Quality_Engineer_PRO4(Convert.ToInt32(Request["requestid"]), txt_update_user.Value, txt_content_Quality_Engineer.Value, txt_is_check_qcc_zl.SelectedValue,
                         txt_is_check_szb_zl.SelectedValue, txt_is_check_wg_zl.SelectedValue, txt_check_other_ms_zl.Value, txt_check_lj_zl.SelectedValue, txt_Already_zl.SelectedValue, txt_is_check_jj_zl.SelectedValue,
                         txt_Check_number_zl.Text, txt_reference_number_zl.SelectedValue, Convert.ToInt32("0" + txt_Confirm_quantity.Value), Convert.ToInt32("0" + txt_Unqualified_quantity_zl.Value), BTN_Quality_Engineer4.Text);
                            if (update > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 提交成功！')", true);
                                BTN_Quality_Engineer4.Enabled = false;
                                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请核对后再提交！')", true);
                            }

                        }

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('合计数量必须等于备货数量！')", true);

                }
            }
        }
        ViewState["lv"] = "ZLGCS";
    }



    protected void BTN_Warehouse_Keeper_DY_Click(object sender, EventArgs e)
    {

        DataTable dtemp = YJ_CLASS.YJ_emp(txt_update_user.Value);
        string name = dtemp.Rows[0]["lastname"].ToString();
        if (txt_cp_status.SelectedValue == "新产品")
        {
            if (Lb_txt_reference_number.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请提醒产品工程师填写参考号！')", true);
            }
            else
            {
                txt_isdy.Text = "1";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "openwind('" + txt_Sorting_list.Value + "', '" + name + "')", true);

                //Response.Write("<script>javascript:window.open('PrintFenJianDan.aspx?Code=" + txt_Sorting_list.Value + "&FLR=" + name + "','_top')</script>");
            }
        }
        else
        {
            if (Lb_txt_reference_number_zl.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请提醒质量工程师填写参考号！')", true);
            }
            else
            {
                txt_isdy.Text = "1";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "openwind('" + txt_Sorting_list.Value + "', '" + name + "')", true);
                //Response.Write("<script>javascript:window.open('PrintFenJianDan.aspx?Code=" + txt_Sorting_list.Value + "&FLR=" + name + "','_blank')</script>");
            }
        }
        dtemp.Clear();
        ViewState["lv"] = "CKBZ";
    }

    protected void txt_Check_number_zl_TextChanged(object sender, EventArgs e)
    {
        if (txt_domain_zzgc.Value == "100")
        {
            if (txt_check_lj_zl.SelectedValue == "无需检验" || txt_check_lj_zl.SelectedValue == "检验组-结束")
            {
                txt_Check_number_zl.Text = "无";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('上海工厂请填写纸挡检验单流水号！')", true);
            }
        }
        else
        {
            if (txt_check_lj_zl.SelectedValue == "无需检验" || txt_check_lj_zl.SelectedValue == "检验组-结束")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('根据检验要求无需送检请填写无！')", true);
                txt_Check_number_zl.Text = "无";
            }
            else
            {
                if (txt_Check_number_zl.Text != "")
                {
                    DataTable dtbtn = YJ_CLASS.GetJCApply_Master(txt_xmh.Value, txt_Check_number_zl.Text);
                    if (dtbtn.Rows.Count <= 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写正确委托检验系统中单号！')", true);
                        txt_Check_number_zl.Text = "";
                    }
                    dtbtn.Clear();

                }
            }
        }
        ViewState["lv"] = "ZLGCS";
    }

    protected void txt_Check_number_TextChanged(object sender, EventArgs e)
    {
        if (txt_domain_zzgc.Value == "100")
        {
            if (txt_check_lj.SelectedValue == "无需检验" || txt_check_lj.SelectedValue == "检验组-结束")
            {
                txt_Check_number.Text = "无";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('制造工厂为上海工厂请填写纸挡检验单流水号！')", true);
            }
        }
        else
        {
            if (txt_check_lj.SelectedValue == "无需检验" || txt_check_lj.SelectedValue == "检验组-结束")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('根据检验要求无需送检请填写无！')", true);
                txt_Check_number.Text = "无";
            }
            else
            {
                if (txt_Check_number.Text != "")
                {
                    DataTable dtbtn = YJ_CLASS.GetJCApply_Master(txt_xmh.Value, txt_Check_number.Text);
                    if (dtbtn.Rows.Count <= 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请填写正确委托检验系统中单号！')", true);
                        txt_Check_number.Text = "";
                    }
                    dtbtn.Clear();

                }
            }
        }
        ViewState["lv"] = "CPGCS";
    }
    //销售取消订单和干预流程
    protected void BTN_Process_intervention_Click(object sender, EventArgs e)
    {
        if (txt_Process_intervention.SelectedValue == "干预回确认状态")
        {
            int update = -1;
            update = YJ_CLASS.Process_intervention(Convert.ToInt32(Request["requestid"]));
            if (update > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 干预成功！')", true);
                BTN_Process_intervention.Enabled = false;
                string strSql = "insert into form1_Sale_YJ_LOG(requestid, Update_Engineer, Update_user, Update_Status,Receive_time,Commit_time,Operation_time,Update_content) values('" + Convert.ToInt32(Request["requestid"]) + "','Assistant-01','" + txt_update_user.Value + "',-3,'" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','0','" + txt_gysm.Value + "')";
                DbHelperSQL.ExecuteSql(strSql);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('没有干预成功！')", true);
            }

        }
        else if (txt_Process_intervention.SelectedValue == "订单取消")
        {
            if (txt_status_id.Text =="-2")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('订单当前为取消状态，请勿重复取消订单！')", true);
                BTN_Process_intervention.Enabled = false;
            }
            else
            {
                int update = -1;
                update = YJ_CLASS.Process_intervention_delete(Convert.ToInt32(Request["requestid"]));
                if (update > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 订单取消成功！')", true);
                    BTN_Process_intervention.Enabled = false;
                    string strSql = "insert into form1_Sale_YJ_LOG(requestid, Update_Engineer, Update_user, Update_Status,Receive_time,Commit_time,Operation_time,Update_content) values('" + Convert.ToInt32(Request["requestid"]) + "','Assistant-01','" + txt_update_user.Value + "',-2,'" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','0','" + txt_gysm.Value + "')";
                    DbHelperSQL.ExecuteSql(strSql);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('订单没有取消成功！')", true);
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择操作！')", true);
        }
    }
    //质量干预流程
    protected void BTN_Process_intervention_zl_Click(object sender, EventArgs e)
    {
        if (txt_Process_intervention_zl.SelectedValue != "")
        {
            int update = -1;
            update = YJ_CLASS.Process_intervention(Convert.ToInt32(Request["requestid"]));
            if (update > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 干预成功！')", true);
                BTN_Process_intervention.Enabled = false;
                string strSql = "insert into form1_Sale_YJ_LOG(requestid, Update_Engineer, Update_user, Update_Status,Receive_time,Commit_time,Operation_time,Update_content) values('" + Convert.ToInt32(Request["requestid"]) + "','Assistant-01','" + txt_update_user.Value + "',-3,'" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','0','" + txt_gysm_zl.Value + "')";
                DbHelperSQL.ExecuteSql(strSql);
                YJ_CLASS.SendMail(Convert.ToInt32(Request["requestid"]), Convert.ToInt32(txt_status_id.Text), txt_Code.Value);

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('没有干预成功！')", true);
            }

        }
    }
    protected void txt_ljdj_TextChanged(object sender, EventArgs e)
    {
        txt_ljzj.Value = Convert.ToString(Convert.ToDecimal(txt_ljdj.Text) * Convert.ToInt32(txt_yhsl.Text));
    }



    protected void txt_check_lj_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtcheck_lj = YJ_CLASS.YJ_BASE2("6", txt_check_lj.SelectedValue);
        if (txt_check_lj.SelectedValue == "")
        {
            lb_ljsm_cp.Text = "";
        }
        else
        {
            lb_ljsm_cp.Text = dtcheck_lj.Rows[0]["VALUE"].ToString();
        }
        ViewState["lv"] = "CPGCS";
    }

    protected void txt_check_lj_zl_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtcheck_lj = YJ_CLASS.YJ_BASE2("6", txt_check_lj_zl.SelectedValue);
        if (txt_check_lj_zl.SelectedValue == "")
        {
            lb_ljsm_zl.Text = "";
        }
        else
        {
            lb_ljsm_zl.Text = dtcheck_lj.Rows[0]["VALUE"].ToString();
        }
        ViewState["lv"] = "ZLGCS";
    }


    private static void moveFiles(string srcFolder, string destFolder)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(srcFolder);
        FileInfo[] files = directoryInfo.GetFiles();

        foreach (FileInfo file in files) // Directory.GetFiles(srcFolder)
        {
            if (file.Extension == ".txt")
            {
                file.MoveTo(Path.Combine(destFolder, file.Name));
            }
        }
    }
    public static void filecreateSO(DataSet ds, string name, string QADSO)
    {


        FileStream fs = new FileStream("D:\\QAD_INPORT_SO\\" + name + ".txt", FileMode.Append, FileAccess.Write);


        //StreamWriter sw = new StreamWriter(fs);
        StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
        if (ds != null && ds.Tables.Count > 0)
        {
            for (int q = 0; q < ds.Tables[0].Rows.Count; q++)
            {

                sw.Write(ds.Tables[0].Rows[q]["domain"].ToString());
                sw.Write(",");
                sw.Write("SO");
                sw.Write(",");
                sw.Write(QADSO);
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["gkdm"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["fhz"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["gkddh"].ToString());
                sw.Write(",");
                sw.Write("102");
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["hb"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["domain"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["ljh"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["yhsl"].ToString());
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["ljdj"].ToString());
                sw.Write(",");
                sw.Write("EA");
                sw.Write(",");
                sw.Write(ds.Tables[0].Rows[q]["pod_DUE_date"].ToString());
                sw.Write("\r\n");
                sw.Flush();

            }
        }

        sw.Close();
        fs.Close();
        SFTP.SFTPHelper sftp = new SFTP.SFTPHelper("172.16.5.12", "qad", "qad");
        sftp.Connect();


        sftp.Put("D:\\QAD_INPORT_SO\\" + name + ".txt", "/apps/OA/" + name + ".txt");
        //sftp.Put("D:\\QAD_INPORT_SO\\" + name + ".txt", "/apps/OA/test/" + name + ".txt");


    }
    protected void BTN_Sales_Assistant2_CS_Click(object sender, EventArgs e)
    {
        if (txt_iscsdd.Text == "1")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请勿重复触发订单！')", true);
        }
        else
        {

            int requestid = Convert.ToInt32(Request["requestid"]);
            DataTable dtall = YJ_Confirm_Action_SQL.Form1_YJ_Query_PRO(requestid);
            //txt_Userid.Value = dt.Rows[0]["Userid"].ToString();

            if (dtall.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dtall);

                string name = "";
                string QADSO = "";
                name = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (ds.Tables[0].Rows[0]["domain"].ToString() == "100")
                {
                    name = "100SO" + name;
                    QADSO = "SS20" + ds.Tables[0].Rows[0]["requestid"].ToString();
                }
                else
                {
                    name = "200SO" + name;
                    QADSO = "KS20" + ds.Tables[0].Rows[0]["requestid"].ToString();
                }

                filecreateSO(ds, name, QADSO);
                string TXT = "D:\\QAD_INPORT_SO";
                string BAK = "D:\\QAD_INPORT_SO\\BAKUP";

                moveFiles(TXT, BAK);

                int update = -1;

                update = YJ_CLASS.iserp(Convert.ToInt32(Request["requestid"]), QADSO);
                if (update > 0)
                {
                    txt_iscsdd.Text = "1";
                    txt_iscsdd_sm.Text = "已触发";
                    BTN_Sales_Assistant2_CS.Text = "QAD订单已生成";
                    BTN_Sales_Assistant2_CS.Enabled = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('恭喜您(⊙o⊙) 已触发对接！')", true);
                    string strSql = "insert into form1_Sale_YJ_LOG(requestid, Update_Engineer, Update_user, Update_Status,Receive_time,Commit_time,Operation_time,Update_content) values('" + Convert.ToInt32(Request["requestid"]) + "','Assistant-01','" + txt_update_user.Value + "','-6','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','0','生成QAD订单')";
                    DbHelperSQL.ExecuteSql(strSql);

                }
                //Environment.Exit(0);
            }

        }




    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (Convert.ToInt32(e.Row.Cells[5].Text) != -1 & Convert.ToInt32(e.Row.Cells[5].Text) < 5 & e.Row.Cells[4].Text.ToString() != "" & e.Row.Cells[4].Text.ToString() != "&nbsp;")
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






    protected void Image2_Click(object sender, ImageClickEventArgs e)
    {
        //int rowIndex = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());
        int lnindex = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

        if (this.GridView3.Rows[lnindex].Cells[6].Text.ToString() != "&nbsp;")
        {

            ViewState["lv"] = GridView3.Rows[lnindex].Cells[6].Text.ToString();
            // Page.RegisterClientScriptBlock("showDiv", "<script>goTo()</script>");

        }


    }

    protected void Image1_Click(object sender, ImageClickEventArgs e)
    {
        int lnindex = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

        if (this.GridView3.Rows[lnindex].Cells[6].Text.ToString() != "&nbsp;")
        {

            ViewState["lv"] = GridView3.Rows[lnindex].Cells[6].Text.ToString();
        }

    }



    protected void Btn_dybq_Click(object sender, EventArgs e)
    {
        if (txt_bqdy.SelectedValue == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Btn_dybq, this.GetType(), "alert", "layer.alert('请选择打印标签类别！')", true);

        }
        else
        {
            String dyym = txt_bqdy.SelectedValue;
            if (dyym == "PrintFordTag1.aspx")
            {
                if (txt_dysl.Value == "" && txt_Serial_No.SelectedValue == "否")
                {
                    txt_dysl.Visible = true;
                    Lab_dyslms.Visible = true;
                    System.Web.UI.ScriptManager.RegisterStartupScript(Btn_dybq, this.GetType(), "alert", "layer.alert('请填写打印数量！')", true);
                }
                else
                {

                    if (txt_gkdm.Value.Substring(0, 3) == "101")
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Btn_dybq, this.GetType(), "open", "openwindFord('" + Convert.ToInt32(Request["requestid"]) + "','" + dyym + "', '" + txt_dysl.Value + "')", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Btn_dybq, this.GetType(), "alert", "layer.alert('FORD特殊标签，其他客户不能打印！')", true);
                    }

                }
            }
            else
            {
                txt_dysl.Visible = false;
                Lab_dyslms.Visible = false;
                System.Web.UI.ScriptManager.RegisterStartupScript(Btn_dybq, this.GetType(), "open", "openwindother('" + Convert.ToInt32(Request["requestid"]) + "', '" + dyym + "')", true);

            }
        }
        ViewState["lv"] = "CKBZ";
    }

    //protected void BTN_Sales_Assistant2_qr_Click(object sender, EventArgs e)
    //{
    //    //再次获取零件清单（查看是否有需要修改产品号）
    //    Response.Write("<script>javascript:window.open('../Select/select_YJ.aspx?ljh=" + txt_xmh.Value + "&domain=" + txt_domain_zzgc.Value + "','_blank')</script>");
    //    //Response.Write("<script>javascript:window.showModelessDialog('../Select/select_YJ.aspx?ljh=" + txt_xmh.Value + "&domain=" + txt_domain_zzgc.Value + "','_blank')</script>");

    //}









    
}
