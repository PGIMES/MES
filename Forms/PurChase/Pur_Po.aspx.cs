using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using IBatisNet.Common.Transaction;
using System.Web.Script.Services;
using System.Web.Services;
using MES.Model;
using MES.DAL;
using Maticsoft.Common;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;

public partial class Pur_Po : System.Web.UI.Page
{
    string m_sid = "";
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        string FlowID = "A";
        string StepID = "A";

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }


        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        Session["LogUser"] = LogUserModel;

        SetBuyerName();
        SetWgPayCon();

        SetContractType();

        if (!IsPostBack)
        {

            //获取每步骤栏位状态设定值，方便前端控制其可编辑性

            if (Request.QueryString["flowid"] != null)
            {
                FlowID = Request.QueryString["flowid"];
            }

            if (Request.QueryString["stepid"] != null)
            {
                StepID = Request.QueryString["stepid"];
            }

            DataTable ldt_detail = null;
            DataTable ldt_pay = null;
            string lssql = "";

            /*
            ,case when left(pr.wlType,4)='4010' then '['+pr.wlh+']'+right(pr.wlType,LEN(pr.wlType)-5) +'/'+pr.wlSubType 
		                        else (case when isnull(pr.wlh,'')<>'' then '['+pr.wlh+']'+ right(pr.wlType,LEN(pr.wlType)-5) else pr.wlType end ) end wlType
            ,case when CHARINDEX('刀具',pr_main.PRType)>0 or CHARINDEX('辅料',pr_main.PRType)>0 or CHARINDEX('原材料',pr_main.PRType)>0 then '' else pr.note end note2
            */
            lssql = @"select po.*
                        ,ISNULL(pr.wlh,'无') wlType
                        ,pr.wlSubType,pr.wlh,pr.wlmc+'['+pr.wlms+']' wldesc,pr.usefor
                        ,replace(right(pr.RecmdVendorName,LEN(pr.RecmdVendorName)-CHARINDEX('_',pr.RecmdVendorName)),'有限公司','') RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName
                        ,pr.ApointVendorId,pr.unit,isnull(cast(pr.notax_historyPrice as nvarchar(max)),'新单价') notax_historyPrice,pr.notax_targetPrice,pr.deliveryDate
                        ,(pr.notax_targetPrice*pr.qty) as notax_targetTotalPrice,pr.attachments
                        ,case when CHARINDEX('刀具',pr_main.PRType)>0 or CHARINDEX('辅料',pr_main.PRType)>0 or CHARINDEX('原材料',pr_main.PRType)>0 then isnull(pr.wlSubType,pr.wlType) else pr.note end note2
                        ,'查看' as attachments_name
                    from PUR_PO_Dtl_Form po
                        left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid
                        left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno";
            if (this.m_sid == "")
            {
                //新增
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    CreateDate.Text = System.DateTime.Now.ToString();
                    CreateByName.Text = LogUserModel.UserName;
                    DeptName.Text = LogUserModel.DepartName;
                    BuyerName.Value = LogUserModel.UserId + "|" + LogUserModel.UserName;
                }

                lssql += " where 1=0";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
                loadControl(ldt_detail);

                ldt_pay = DbHelperSQL.Query("select * from PUR_PO_ContractPay_Form where 1=0").Tables[0];
                loadControl_Pay(ldt_pay);

            }
            else
            {
                //编辑  
                //表头赋值
                DataTable ldt = DbHelperSQL.Query("select * from PUR_PO_Main_Form where PoNo='" + this.m_sid + "'").Tables[0];
                Pgi.Auto.Control.SetControlValue("PUR_PO_Main_Form", "HEAD_New_2", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                if (ldt.Rows[0]["attachments"].ToString() != "")
                {
                    //this.txtfile.NavigateUrl = ldt.Rows[0]["attachments"].ToString();
                    //this.txtfile.Visible = true;

                    this.ip_filelist_db.Value = ldt.Rows[0]["attachments"].ToString();
                    bindtab();
                }

                Pgi.Auto.Control.SetControlValue("PUR_PO_Main_Form", "HEAD_PAY", this.Page, ldt.Rows[0], "ctl00$MainContent$");

                lssql += " where pono='" + this.m_sid + "'  order by po.NoTaxPrice-ISNULL(pr.notax_historyPrice,0) desc,po.rowid";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];


                //特殊处理
                PoVendorId.Value = "";
                string lsrate = "0";
                if (ldt_detail.Rows.Count > 0)
                {
                    lsrate = ldt_detail.Rows[0]["TaxRate"].ToString();
                }


                PoVendorId.Value = ldt.Rows[0]["PoVendorId"].ToString() + "|" + ldt.Rows[0]["PoVendorName"].ToString() + "|" + lsrate;
                BuyerName.Value = ldt.Rows[0]["buyerid"].ToString() + "|" + ldt.Rows[0]["buyername"].ToString();
                //if (ldt.Rows[0]["PoVendorId"].ToString() == "31567" && ldt.Rows[0]["PoVendorName"].ToString() == "网购")
                if (ldt.Rows[0]["PoVendorId"].ToString()=="31567")
                {
                    WgPayCon.Value = ldt.Rows[0]["WgPayCon"].ToString() + "|" + ldt.Rows[0]["WgPayConDesc"].ToString();
                }
                ContractType.Value = ldt.Rows[0]["contracttype"].ToString() + "|" + ldt.Rows[0]["contracttypedesc"].ToString();

                loadControl(ldt_detail);

                ldt_pay = DbHelperSQL.Query("select * from PUR_PO_ContractPay_Form where pono='" + this.m_sid + "' order by rowid").Tables[0];
                loadControl_Pay(ldt_pay);
            }

            #region 设置样式 ldt_detail

            //特殊处理
            string sql_flow = @"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                            where cast(stepid as varchar(36))=cast('{0}' as varchar(36)) and cast(flowid as varchar(36))=cast('{1}' as varchar(36)) 
                                and instanceid='{2}' and stepname='{3}'";
            sql_flow = string.Format(sql_flow, StepID, FlowID, this.m_sid, "采购负责人");
            DataTable ldt_flow = DbHelperSQL.Query(sql_flow).Tables[0];

            //增加事件
            for (int i = 0; i < ldt_detail.Rows.Count; i++)
            {
                ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).TextChanged += new EventHandler(txt_TextChanged);

                if (ldt_flow.Rows.Count == 0)
                {
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Enabled = false;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Width = Unit.Pixel(30);
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).Enabled = false;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).Width = Unit.Pixel(80);
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).Style.Add("text-align", "right");
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BackColor = System.Drawing.Color.Transparent;

                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BackColor = System.Drawing.Color.Transparent;

                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Enabled = false;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Width = Unit.Pixel(72);
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    this.btndel.Visible = false;
                    this.btnadd.Visible = false;

                    PoVendorId.Enabled = false;
                    BuyerName.Enabled = false;
                    PoType.Enabled = false;
                    PoDomain.Enabled = false;
                    AssetsAtt.Enabled = false;
                    WgPayCon.Enabled = false;

                    //this.FileUpload1.Visible = false;
                    this.uploadcontrol.Visible = false;
                    this.btnflowSend.Text = "批准";

                    PoDomain.CssClass = "lineread";
                    AssetsAtt.CssClass = "lineread";

                    wgvendor.BackColor = System.Drawing.Color.Transparent;

                    //签核的时候，加上%
                    if (PoVendorId.Value.ToString().Right(1) != "%")
                    {
                        PoVendorId.Value = PoVendorId.Value + "%";
                    }
                }
                if (Request.QueryString["display"] != null)
                {
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Enabled = false;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Width = Unit.Pixel(28);
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).Enabled = false;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).Width = Unit.Pixel(80);
                    ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).Style.Add("text-align", "right");
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BackColor = System.Drawing.Color.Transparent;

                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BackColor = System.Drawing.Color.Transparent;

                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Enabled = false;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Width = Unit.Pixel(72);
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    this.btndel.Visible = false;
                    this.btnadd.Visible = false;

                    PoVendorId.Enabled = false;
                    BuyerName.Enabled = false;
                    PoType.Enabled = false;
                    PoDomain.Enabled = false;
                    AssetsAtt.Enabled = false;
                    WgPayCon.Enabled = false;

                    // this.FileUpload1.Visible = false;
                    this.uploadcontrol.Visible = false;

                    PoDomain.CssClass = "lineread";
                    AssetsAtt.CssClass = "lineread";

                    wgvendor.BackColor = System.Drawing.Color.Transparent;

                    //签核的时候，加上%
                    if (PoVendorId.Value.ToString().Right(1) != "%")
                    {
                        PoVendorId.Value = PoVendorId.Value + "%";
                    }
                }
            }

            for (int i = 0; i < ldt_pay.Rows.Count; i++)
            {
                //AutoPostBack = true 已经在 数据库配置好，list_type_ref设定
                ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).TextChanged += new EventHandler(txt_pay_TextChanged);

                if (ldt_flow.Rows.Count == 0)
                {
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).Enabled = false;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BackColor = System.Drawing.Color.Transparent;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).Style.Add("text-align", "right");

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).Width = Unit.Pixel(150);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).Width = Unit.Pixel(150);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).Width = Unit.Pixel(180);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).Enabled = false;
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).Width = Unit.Pixel(80);
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    this.btnadd_contract.Visible = false;
                    this.btndel_contract.Visible = false;

                    PayType.Enabled = false;
                    PayType.CssClass = "lineread";

                    contractname.BackColor = System.Drawing.Color.Transparent;
                    actualcontractno.BackColor = System.Drawing.Color.Transparent;

                    ContractType.Enabled = false;
                }
                if (Request.QueryString["display"] != null)
                {
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).Enabled = false;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BackColor = System.Drawing.Color.Transparent;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).Style.Add("text-align", "right");

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).Width = Unit.Pixel(150);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).Width = Unit.Pixel(150);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).Enabled = false;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).Width = Unit.Pixel(180);
                    ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).Enabled = false;
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).Width = Unit.Pixel(80);
                    ((ASPxDateEdit)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PlanPayDate"], "PlanPayDate")).DisabledStyle.BackColor = System.Drawing.Color.Transparent;

                    this.btnadd_contract.Visible = false;
                    this.btndel_contract.Visible = false;

                    PayType.Enabled = false;
                    PayType.CssClass = "lineread";

                    contractname.BackColor = System.Drawing.Color.Transparent;
                    actualcontractno.BackColor = System.Drawing.Color.Transparent;

                    ContractType.Enabled = false;
                    
                }
            }

            #endregion

        }
        else
        {

            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            this.gv.Columns.Clear();

            loadControl(ldt);
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                ((TextBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).TextChanged += new EventHandler(txt_TextChanged);
            }
            bindtab();
        }

        //获取采购类别
        SetPoType(BuyerName.Value.ToString());

        //获取供应商信息
        SetPoVendor(PoDomain.Text);

        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    void bindtab()
    {
        bool is_del = true;
        DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" + Request.QueryString["stepid"] + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + Request.QueryString["flowid"] + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='采购负责人'").Tables[0];

        if (ldt_flow.Rows.Count == 0)
        {
            is_del = false;
        }
        if (Request.QueryString["display"] != null)//未发送之前
        {
            is_del = false;
        }

        tab1.Rows.Clear();
        string[] ls_files = this.ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ls_files.Length; i++)
        {
            TableRow tempRow = new TableRow();
            string[] ls_files_2 = ls_files[i].Split(',');

            HyperLink hl = new HyperLink();
            Label lb = new Label();

            if (ls_files_2.Length == 3)
            {                
                hl.Text = ls_files_2[0].ToString();
                hl.NavigateUrl = ls_files_2[1].ToString();
                hl.Target = "_blank";
              
                lb.Text = ls_files_2[2].ToString();
            }
            else//之前的文件，只有一个路径
            {
               
                hl.Text = "文件浏览";
                hl.NavigateUrl = ls_files_2[0].ToString();
                hl.Target = "_blank";

                lb.Text = "";               
            }

            TableCell td1 = new TableCell(); td1.Controls.Add(hl); td1.Width = Unit.Pixel(400);
            tempRow.Cells.Add(td1);

            TableCell td2 = new TableCell(); td2.Controls.Add(lb); td2.Width = Unit.Pixel(60);
            tempRow.Cells.Add(td2);

            if (is_del)
            {
                //Button Btn = new Button(); 
                LinkButton Btn = new LinkButton();
                Btn.Text = "删除"; Btn.ID = "btn_" + i.ToString(); Btn.Click += new EventHandler(Btn_Click);

                TableCell td3 = new TableCell(); td3.Controls.Add(Btn);
                tempRow.Cells.Add(td3);
            }
            tab1.Rows.Add(tempRow);
        }
    }

    void Btn_Click(object sender, EventArgs e)
    {
        //var btn = sender as Button;
        var btn = sender as LinkButton;
        int index = Convert.ToInt32(btn.ID.Substring(4));

        string filedb = ip_filelist_db.Value;
        string[] ls_files = filedb.Split(';');

        string files = "";
        for (int i = 0; i < ls_files.Length; i++)
        {
            if (i != index) { files += ls_files[i] + ";"; }
        }
        if (files != "") { files = files.Substring(0, files.Length - 1); }

        ip_filelist_db.Value = files;

        bindtab();
    }

    public void loadControl(DataTable ldt_detail)
    {
        /*string potype = "";
        if (((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).Value != null)
        {
            potype = ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).Value.ToString();
        }

        this.gv.Columns.Clear();

        string formdiv = "";
        if (potype.Contains("存货")) { formdiv = "DETAIL_New_2"; }
        else { formdiv = "DETAIL_New_3"; }

        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", formdiv, this.gv, ldt_detail, 2);*/

        string potype = "";
        if (PoType.Value != null)
        {
            potype = PoType.Value.ToString();
        }

        this.gv.Columns.Clear();
        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_all", this.gv, ldt_detail, 2);
        GetGrid(ldt_detail, potype);

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype")).Width = Unit.Pixel(80);
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["currency"], "currency")).Width = Unit.Pixel(48);
        }

        //更新付款明细表
        DataTable ldt_2 = Pgi.Auto.Control.AgvToDt(this.gv2);
        double paytotal = Convert.ToDouble(gv.GetTotalSummaryValue(gv.TotalSummary["TotalPrice"]));
        if (paytotal > 0)
        {
            for (int i = 0; i < ldt_2.Rows.Count; i++)
            {
                if (ldt_2.Rows[i]["PayRate"].ToString() != "")
                {
                    ldt_2.Rows[i]["PayMoney"] = paytotal * (Convert.ToDouble(ldt_2.Rows[i]["PayRate"]) / 100);
                }
            }
        }
        else
        {
            ldt_2.Rows.Clear();
            PayType.SelectedValue = "";
        }
        this.gv2.Columns.Clear();
        loadControl_Pay(ldt_2);
    }

    public void PoDomain_TextChanged(object sender, EventArgs e)
    {
        SetPoVendor(((DropDownList)sender).SelectedValue);
       
    }

    //绑定采购负责人
    public void SetBuyerName()
    {
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).Columns.Clear();
        string lssql = @"select workcode,lastname,workcode+'|'+lastname as v from HRM_EMP_MES  where dept_name='采购二部' and jobtitlename<>'采购经理'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).ValueField = "v";
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).Columns.Add("workcode", "采购人工号", 80);
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).Columns.Add("lastname", "采购人姓名", 80);
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).TextFormatString = "{0}|{1}";
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).DataSource = ldt;
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).DataBind();
    }

    //绑定采购类别
    public void SetPoType(string buyername)
    {
        string[] buyername_arry = buyername.Split('|');
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).Columns.Clear();
        //string lssql = @"exec Pur_GetClassByPerson '" + buyername_arry[0] + "','','PO'";
        string lssql = @"select 'PO' type union select '合同' type";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).ValueField = "type";
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).Columns.Add("type", "采购类别", 80);
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).TextFormatString = "{0}";
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).DataSource = ldt;
        ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).DataBind();
    }

    //采购供应商
    private void SetPoVendor(string lsdomain)
    {
        PoVendorId.Columns.Clear();
        string lssql = @"select distinct ad_addr,ad_name
                                ,case when isnull(vd_taxc,'')='17' then '13' when isnull(vd_taxc,'')='11' then '9' else vd_taxc end vd_taxc
                                ,ad_addr+'|'+ad_name+'|'+case when isnull(vd_taxc,'')='17' then '13' when isnull(vd_taxc,'')='11' then '9' else vd_taxc end as v   
                        from qad_ad_mstr 
                            inner join qad_vd_mstr on ad_addr=vd_addr and ad_domain=vd_domain 
                        where ad_type='supplier' and vd_taxc<>'' and ad_domain='" + lsdomain + "'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        PoVendorId.ValueField = "v";
        PoVendorId.Columns.Add("ad_addr","代码",40);
        PoVendorId.Columns.Add("ad_name","名称",80);
        PoVendorId.Columns.Add("vd_taxc","纳税级别",60);
        PoVendorId.TextFormatString = "{0}|{1}|{2}";
        PoVendorId.DataSource = ldt;
        PoVendorId.DataBind();
    }

    //绑定支付方式
    public void SetWgPayCon()
    {
        WgPayCon.Columns.Clear();
        //string lssql = @"select [PaymentConditionCode],[PaymentConditionDescript],PaymentConditionCode+'|'+PaymentConditionDescript as v from [qad].[dbo].[qad_paymentcondition]  
        //                where [PaymentConditionIsActive]=1";
        string lssql = @"select [PaymentConditionCode],[PaymentConditionDescript],v
                        from (
	                        select '预付' [PaymentConditionCode],'预付' [PaymentConditionDescript],'预付|预付' v,0 rownum
	                        union 
	                        select [PaymentConditionCode],[PaymentConditionDescript],PaymentConditionCode+'|'+PaymentConditionDescript as v 
		                        ,ROW_NUMBER() over(order by [PaymentConditionDaysMonths]) as rownum
	                        from [qad].[dbo].[qad_paymentcondition]  
	                        where [PaymentConditionIsActive]=1 
	                        ) a
                        order by rownum";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        WgPayCon.ValueField = "v";
        WgPayCon.Columns.Add("PaymentConditionCode", "代码", 80);
        WgPayCon.Columns.Add("PaymentConditionDescript", "描述", 80);
        WgPayCon.TextFormatString = "{0}|{1}";
        WgPayCon.DataSource = ldt;
        WgPayCon.DataBind();
    }

    //合同类别
    public void SetContractType()
    {
        ContractType.Columns.Clear();
        string lssql = @"select code_value,code_cmmt,code_value+'|'+code_cmmt as v from [qad].[dbo].[qad_code_mstr] where code_fldname='AP-CONTRACT'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        ContractType.ValueField = "v";
        ContractType.Columns.Add("code_value", "代码", 80);
        ContractType.Columns.Add("code_cmmt", "名称", 80);
        ContractType.TextFormatString = "{0}|{1}";
        ContractType.DataSource = ldt;
        ContractType.DataBind();
    }

    protected void gv_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {

        if (e.CommandArgs.CommandName == "Add")
        {
            //新增一行
            //DevExpress.Web.GridViewDataColumn t = this.gv.Columns[0] as DevExpress.Web.GridViewDataColumn;
            //DevExpress.Web.ASPxTextBox tb1 = (DevExpress.Web.ASPxTextBox)this.gv.FindRowCellTemplateControl(0, t, "daoju_no");
            

        }
        else if (e.CommandArgs.CommandName == "Add1")
        {
           
        }
        else if (e.CommandArgs.CommandName == "JS")
        {
            
        }
    }
       
    private bool SaveData()
    {
        bool bflag = false;
        string lspgi_no = "";
        string lsdomain = PoDomain.SelectedValue;
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        //获取表头
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("PUR_PO_Main_Form", "HEAD_New_2", this, "ctl00$MainContent${0}");
        List<Pgi.Auto.Common> ls_head_pay = Pgi.Auto.Control.GetControlValue("PUR_PO_Main_Form", "HEAD_PAY", this, "ctl00$MainContent${0}");

        string cggysId = "";string cggysName = "";
        string wgfkfsId = ""; string wgfkfsDesc = "";

        string contracttype = "";
        string potype = PoType.Value.ToString();//采购类型
       
        //var potype_con_sql = string.Format("select check_accept from [dbo].[PUR_Permission_category] where type='{0}' or type2='{0}'", potype);
        //var potype_con_obj = DbHelperSQL.GetSingle(potype_con_sql);
        //string potype_con = potype_con_obj == null ? "" : potype_con_obj.ToString();

        //表体生成SQL
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        DataTable ldt_pay = Pgi.Auto.Control.AgvToDt(this.gv2);

        ldt.AcceptChanges();

        if (ldt.Rows.Count == 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "采购清单不能为空!");
            return false;
        }

        string sql_pr_exist = "";//验证 一起选择数据的 问题，重复采购
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["PlanReceiveDate"].ToString() == "")
            {
                Pgi.Auto.Public.MsgBox(this, "alert", "第" + (i + 1).ToString() + "行计划到货日期不能为空!");
                return false;
            }
            if (sql_pr_exist != "")
            {
                sql_pr_exist += " union";
            }
            sql_pr_exist += @" select PONo, rowid, PRNo, PRRowId," + (i + 1).ToString()+" as line" 
                            + @" from PUR_PO_Dtl_Form where PRNo = '" + ldt.Rows[i]["prno"].ToString() + "' and PRRowId = '" + ldt.Rows[i]["prrowid"].ToString() + "'";
        }
        if (sql_pr_exist != "")
        {
            sql_pr_exist = "select PONo, rowid, PRNo, PRRowId, line from (" + sql_pr_exist + ") a";
            if (this.m_sid != "")
            {
                sql_pr_exist = sql_pr_exist + " where pono<>'" + this.m_sid + "'";
            }
            DataTable ldt_pr_exist = DbHelperSQL.Query(sql_pr_exist).Tables[0];
            string msg_pr_exist = "";
            for (int i = 0; i < ldt_pr_exist.Rows.Count; i++)
            {
                msg_pr_exist = "第" + ldt_pr_exist.Rows[i]["line"].ToString()
                    + "行，请购单号【" + ldt_pr_exist.Rows[i]["PRNo"].ToString() + "】请购行号【" + ldt_pr_exist.Rows[i]["PRRowId"].ToString()
                    + "】已经采购，不能重复采购！<br />采购单号【" + ldt_pr_exist.Rows[i]["PONo"].ToString() + "】采购行号【" + ldt_pr_exist.Rows[i]["rowid"].ToString() + "】";
            }
            if (msg_pr_exist != "")
            {
                Pgi.Auto.Public.MsgBox(this, "alert", msg_pr_exist);
                return false;
            }
        }
      

        if (potype=="合同")//(potype_con == "合同模块")
        {
            if (ldt_pay.Rows.Count == 0)
            {
                Pgi.Auto.Public.MsgBox(this, "alert", "付款信息不能为空!");
                return false;
            }

            contracttype = ContractType.Value.ToString();
        }
        
        //特殊数据处理
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code == "podomain") { ls[i].Value = lsdomain; }
            if (ls[i].Code == "potype") { ls[i].Value = potype; }

            if (ls[i].Code.ToLower() == "buyername")
            {
                string[] lsstr = BuyerName.Enabled == true ? ls[i].Value.ToString().Split('|') : BuyerName.Value.ToString().Split('|');

                if (lsstr.Length == 2)
                {
                    ls[i].Value = lsstr[1];

                    //增加采购负责人ID
                    Pgi.Auto.Common lcbuyerid = new Pgi.Auto.Common();
                    lcbuyerid.Code = "buyerid";
                    lcbuyerid.Key = "";
                    lcbuyerid.Value = lsstr[0];
                    ls.Add(lcbuyerid);
                }

            }

            if (ls[i].Code.ToLower() == "povendorid")
            {
                string[] lsstr = PoVendorId.Enabled == true ? ls[i].Value.ToString().Split('|') : PoVendorId.Value.ToString().Split('|');

                if (lsstr.Length == 3)
                {
                    ls[i].Value = lsstr[0]; cggysId = lsstr[0];

                    //增加供应商名称
                    Pgi.Auto.Common lcpovendname = new Pgi.Auto.Common();
                    lcpovendname.Code = "povendorname";
                    lcpovendname.Key = "";
                    lcpovendname.Value = lsstr[1];
                    ls.Add(lcpovendname);

                    cggysName = lsstr[1];
                }
            }
        }

        //以上采购供应商值定下来之后，才能确定 网购供应商及 网购支付方式
        for (int i = 0; i < ls.Count; i++)
        {

            if (ls[i].Code.ToLower() == "wgpaycon")
            {
                //if (cggysId != "31567" || cggysName != "网购")
                if (cggysId != "31567")
                {
                    wgfkfsId = "";
                    wgfkfsDesc = "";
                }
                else
                {
                    string[] lsstr = WgPayCon.Enabled == true ? ls[i].Value.ToString().Split('|') : WgPayCon.Value.ToString().Split('|');

                    if (lsstr.Length == 2)
                    {
                        wgfkfsId = lsstr[0];
                        wgfkfsDesc = lsstr[1];
                    }
                }

                ls[i].Value = wgfkfsId;

                //增加采购负责人ID
                Pgi.Auto.Common lcwgpaycon = new Pgi.Auto.Common();
                lcwgpaycon.Code = "wgpaycondesc";
                lcwgpaycon.Key = "";
                lcwgpaycon.Value = wgfkfsDesc;
                ls.Add(lcwgpaycon);
            }
        }       
      
        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "K";
            if (lsdomain == "100")
            {
                lsid = "S";
            }
            lsid += System.DateTime.Now.Year.ToString().Substring(3, 1)+System.DateTime.Now.Month.ToString("00");
             this.m_sid = Pgi.Auto.Public.GetNo("PO", lsid,0,4);
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "pono")
                {
                    ls[i].Value = this.m_sid;
                    PoNo.Text = this.m_sid;
                }

            }

            //新增时增加创建人ID
            Pgi.Auto.Common lccreate_byid = new Pgi.Auto.Common();
            lccreate_byid.Code = "createbyid";
            lccreate_byid.Key = "";
            lccreate_byid.Value = ((LoginUser)Session["LogUser"]).UserId;
            ls.Add(lccreate_byid);
        }


        //自定义，上传文件
        //var filepath = "";
        // if (this.FileUpload1.HasFile)
        // {
        //     SaveFile(this.FileUpload1, this.m_sid, out filepath, "123.txt", "123.txt");
        // //增加上传文件列
        // Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        // lcfile.Code = "attachments";
        // lcfile.Key = "";
        // lcfile.Value = filepath;
        // ls.Add(lcfile);

        //}
        string filepath = "";//string filepath=this.UploadFiles(this.uploadcontrol);
        string[] ls_files = ip_filelist.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_2 = item.Split(',');
            if (ls_files_2.Length == 3)//挪动路径到po单号下面
            {
                FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

                var sorpath = @"\UploadFile\Purchase\";
                var despath = MapPath("~") + sorpath + @"\" + m_sid + @"\";
                if (!System.IO.Directory.Exists(despath))
                {
                    System.IO.Directory.CreateDirectory(despath);
                }
                string tmp = despath + ls_files_2[1].Replace(sorpath, "");
                if (File.Exists(tmp))
                {
                    File.Delete(tmp);
                }
                fi.MoveTo(tmp);

                filepath += item.Replace(@"\UploadFile\Purchase\", @"\UploadFile\Purchase\" + m_sid + @"\") + ";";
            }
            else
            {
                filepath += item + ";";
            }
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            filepath += item + ";";
        }
        if (filepath != "") { filepath = filepath.Substring(0, filepath.Length - 1); }


        // 增加上传文件列
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "attachments";
        lcfile.Key = "";
        lcfile.Value = filepath;
        ls.Add(lcfile);

        //---------------------------------------------------------------------------合同 主表 列新增----------------------------------------------------------------------
        string paytype = ""; string contracttype_y = "";string contracttypedesc_y = "";
        string contractname = "";string actualcontractno = "";

        if (potype == "合同")
        {
            for (int i = 0; i < ls_head_pay.Count; i++)
            {
                if (ls_head_pay[i].Code.ToLower() == "paytype")//付款类型
                {
                    paytype= ls_head_pay[i].Value;
                }
                if (ls_head_pay[i].Code.ToLower() == "contracttype")//合同类别
                {
                    string[] lsstr = ContractType.Enabled == true ? ls_head_pay[i].Value.ToString().Split('|') : contracttype.Split('|');

                    if (lsstr.Length == 2)
                    {
                        contracttype_y = lsstr[0];
                        contracttypedesc_y = lsstr[1];
                    }
                }
                if (ls_head_pay[i].Code.ToLower() == "contractname")//合同名称
                {
                    contractname= ls_head_pay[i].Value;
                }
                if (ls_head_pay[i].Code.ToLower() == "actualcontractno")//实际合同号
                {
                    //actualcontractno = this.m_sid;
                    actualcontractno = ls_head_pay[i].Value.ToString() == "" ? this.m_sid : ls_head_pay[i].Value.ToString();
                }
            }
        }

            
        Pgi.Auto.Common lcpaytype = new Pgi.Auto.Common();
        lcpaytype.Code = "paytype";
        lcpaytype.Key = "";
        lcpaytype.Value = paytype;
        ls.Add(lcpaytype);
           
        Pgi.Auto.Common lccontracttype = new Pgi.Auto.Common();
        lccontracttype.Code = "contracttype";
        lccontracttype.Key = "";
        lccontracttype.Value = contracttype_y;
        ls.Add(lccontracttype);

        Pgi.Auto.Common lccontracttypedesc = new Pgi.Auto.Common();
        lccontracttypedesc.Code = "contracttypedesc";
        lccontracttypedesc.Key = "";
        lccontracttypedesc.Value = contracttypedesc_y;
        ls.Add(lccontracttypedesc);
                        
        Pgi.Auto.Common lccontractname = new Pgi.Auto.Common();
        lccontractname.Code = "contractname";
        lccontractname.Key = "";
        lccontractname.Value = contractname;
        ls.Add(lccontractname);
           
        Pgi.Auto.Common lcactualcontractno = new Pgi.Auto.Common();
        lcactualcontractno.Code = "actualcontractno";
        lcactualcontractno.Key = "";
        lcactualcontractno.Value = actualcontractno;
        ls.Add(lcactualcontractno);

        //----------------------------------------------------------------------------------------主表相关字段赋值到明细表
        string formno_main = "";
        for (int j = 0; j < ls.Count; j++)
        {
            if (ls[j].Code.ToLower() == "pono") { formno_main = ls[j].Value; }
        }

        DataTable dt_po_class = DbHelperSQL.Query("select * from PUR_PO_CLASS").Tables[0];

        decimal lntotalpay = 0;
        string pricetype = ""; decimal notax_historyPrice = 0, NoTaxPrice = 0, notax_historyPrice_num = 0, NoTaxPrice_num = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["pono"] = formno_main;
            if (ldt.Rows[i]["TotalPrice"].ToString() != "")
            {
                lntotalpay += Convert.ToDecimal(ldt.Rows[i]["TotalPrice"].ToString());
            }

            notax_historyPrice = 0; NoTaxPrice = 0;
            notax_historyPrice = Convert.ToDecimal(ldt.Rows[i]["notax_historyPrice"].ToString() == "新单价" ? "0" : ldt.Rows[i]["notax_historyPrice"].ToString() == "" ? "0" : ldt.Rows[i]["notax_historyPrice"].ToString());
            NoTaxPrice = Convert.ToDecimal(ldt.Rows[i]["NoTaxPrice"].ToString());

            if (NoTaxPrice <= notax_historyPrice) { notax_historyPrice_num++; }
            else if (NoTaxPrice > notax_historyPrice) { NoTaxPrice_num++; }

            //赋值flag_qad
            DataRow[] dr_class = dt_po_class.Select("LB='" + ldt.Rows[i]["po_wltype"].ToString() + "'");
            if (dr_class.Length == 1)
            {
                ldt.Rows[i]["flag_qad"] = dr_class[0]["MS_CODE"].ToString() == "1" ? "是" : "否";
            }
            else
            {
                ldt.Rows[i]["flag_qad"] = "否";
            }
        }

        for (int i = 0; i < ldt_pay.Rows.Count; i++)
        {
            ldt_pay.Rows[i]["pono"] = formno_main;
        }


        //----------------------------------------------------------------------------------------从明细表中合计采购总金额
        Pgi.Auto.Common lcTotalPay = new Pgi.Auto.Common();
        lcTotalPay.Code = "totalpay";
        lcTotalPay.Key = "";
        lcTotalPay.Value = lntotalpay.ToString();// ((LoginUser)Session["LogUser"]).UserId;
        ls.Add(lcTotalPay);

        //----------------------------------------------------------------------------------------从明细表中得出 PriceType
        if (notax_historyPrice_num == ldt.Rows.Count) { pricetype = "0"; }
        else if (NoTaxPrice_num == ldt.Rows.Count) { pricetype = "1"; }
        else { pricetype = "2"; }

        Pgi.Auto.Common lcPriceType = new Pgi.Auto.Common();
        lcPriceType.Code = "pricetype";
        lcPriceType.Key = "";
        lcPriceType.Value = pricetype;
        ls.Add(lcPriceType);


        //----------------------------------------------------------------------------------------获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PUR_PO_Main_Form"));


        //----------------------------------------------------------------------------------------付款信息
        if (potype == "合同" && ldt_pay.Rows.Count > 0) //(potype_con == "合同模块" && ldt_pay.Rows.Count > 0)
        {
            Pgi.Auto.Common ls_del_pay = new Pgi.Auto.Common();
            string dtl_pay_ids = "";
            for (int i = 0; i < ldt_pay.Rows.Count; i++)
            {
                if (ldt_pay.Rows[i]["id"].ToString() != "") { dtl_pay_ids = dtl_pay_ids + ldt_pay.Rows[i]["id"].ToString() + ","; }
            }
            if (dtl_pay_ids != "")
            {
                dtl_pay_ids = dtl_pay_ids.Substring(0, dtl_pay_ids.Length - 1);
                ls_del_pay.Sql = "delete from PUR_PO_ContractPay_Form where pono='" + m_sid + "' and id not in(" + dtl_pay_ids + ")";    //删除数据库中的数据不在网页上展示出来的        
            }
            else
            {
                ls_del_pay.Sql = "delete from PUR_PO_ContractPay_Form where pono='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del_pay);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls_pay = Pgi.Auto.Control.GetList(ldt_pay, "PUR_PO_ContractPay_Form", "id", "flag");
            for (int i = 0; i < ls_pay.Count; i++)
            {
                ls_sum.Add(ls_pay[i]);
            }

            //更新系统合同号
            Pgi.Auto.Common ls_sysconctractno = new Pgi.Auto.Common();
            ls_sysconctractno.Sql = @"update PUR_PO_Main_Form set SysContractNo=a.SysContractNo
                                    from (select case when MAX(SysContractNo) is null then 
				                                        (select  '{0}'+ right('000000' + cast(isnull(MAX(a.xxcontract_nbr),0)+1 as varchar),6)  
				                                        from qad.[dbo].[qad_xxcontract_mstr] a
				                                        where cast(a.[xxcontract_charfld[2]]] as nvarchar)='{0}' and a.xxcontract_domain='{1}')
			                                        else '{0}'+ right('000000' + cast(isnull(MAX(SysContractNo),0)+1 as varchar),6) 
			                                        end SysContractNo
	                                        from PUR_PO_Main_Form where ContractType='{0}' and PoDomain='{1}' and PoNo<>'{2}'
	                                        ) a
                                    where  pono='{2}'";
            ls_sysconctractno.Sql = string.Format(ls_sysconctractno.Sql, contracttype_y,lsdomain, m_sid);
            ls_sum.Add(ls_sysconctractno);
        }
        else
        {
            Pgi.Auto.Common ls_del_pay = new Pgi.Auto.Common();
            ls_del_pay.Sql = "delete from PUR_PO_ContractPay_Form where pono='" + m_sid + "'";
            ls_sum.Add(ls_del_pay);

            Pgi.Auto.Common ls_sysconctractno = new Pgi.Auto.Common();
            ls_sysconctractno.Sql = @"update PUR_PO_Main_Form set SysContractNo=null where  pono='{0}'";
            ls_sysconctractno.Sql = string.Format(ls_sysconctractno.Sql, m_sid);
            ls_sum.Add(ls_sysconctractno);
        }

        //----------------------------------------------------------------------------------------明细数据
        if (ldt.Rows.Count > 0)
        {
            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            string dtl_ids = "";
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["id"].ToString() != "") { dtl_ids = dtl_ids + ldt.Rows[i]["id"].ToString() + ","; }
            }
            if (dtl_ids != "")
            {
                dtl_ids = dtl_ids.Substring(0, dtl_ids.Length - 1);
                ls_del.Sql = "delete from PUR_PO_Dtl_Form where pono='" + m_sid + "' and id not in(" + dtl_ids + ");";    //删除数据库中的数据不在网页上展示出来的 

                DataTable dt_del = DbHelperSQL.Query("select * from PUR_PO_Dtl_Form where pono='" + m_sid + "' and id not in(" + dtl_ids + ")").Tables[0];
                for (int i = 0; i < dt_del.Rows.Count; i++)
                {
                    ls_del.Sql += "update PUR_PR_Dtl_Form set Status='0' where prno='" + dt_del.Rows[i]["prno"].ToString() + "' and rowid='" + dt_del.Rows[i]["prrowid"].ToString() + "'";
                    if (i > 0)
                    {
                        ls_del.Sql += ";";
                    }
                }
                ls_sum.Add(ls_del);
            }
            else
            {
                ls_del.Sql = "delete from PUR_PO_Dtl_Form where pono='" + m_sid + "';";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据

                DataTable ldt_del = DbHelperSQL.Query("select * from PUR_PO_Dtl_Form where pono='" + m_sid + "'").Tables[0];
                for (int i = 0; i < ldt_del.Rows.Count; i++)
                {
                    ls_del.Sql += "update PUR_PR_Dtl_Form set Status='0' where prno='" + ldt_del.Rows[i]["prno"].ToString() + "' and rowid='" + ldt_del.Rows[i]["prrowid"].ToString() + "'";
                    if (i > 0)
                    {
                        ls_del.Sql += ";";
                    }
                }
                ls_sum.Add(ls_del);
            }

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PUR_PO_Dtl_Form", "id"
                , "flag,wlType,wlSubType,wlh,wlmc,wlms,wldesc,notax_targetPrice,notax_targetTotalPrice,RecmdVendorName,notax_historyPrice,deliveryDate,RecmdVendorId,attachments,attachments_name,pt_status,note2");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }

            //处理PR明细表状态
            if (ldt.Rows.Count > 0)
            {
                Pgi.Auto.Common ls_status = new Pgi.Auto.Common();
                for (int i = 0; i < ldt.Rows.Count; i++)
                {
                    ls_status.Sql += "update PUR_PR_Dtl_Form set status=2 where prno='" + ldt.Rows[i]["prno"].ToString() + "' and rowid='" + ldt.Rows[i]["prrowid"].ToString() + "'";
                    if (i > 0)
                    {
                        ls_status.Sql += ";";
                    }
                }
                ls_sum.Add(ls_status);
            }
        }
        else
        {
            Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
            ls_del.Sql = "delete from PUR_PO_Dtl_Form where pono='" + m_sid + "'";
            ls_sum.Add(ls_del);
        }

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
       
        if (ln > 0)
        {
            bflag = true;
           // string instanceid = ln.ToString();
            string title = "PO采购单申请" +lspgi_no+"--"+ this.m_sid;
            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";
          
        }
        else
        {
            bflag = false;
        }
       
        return bflag;
    }

    /*private bool SaveData()
    {
        bool bflag = false;
        string lspgi_no = "";
        string lsdomain = "";
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        //获取表头
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("PUR_PO_Main_Form", "HEAD", this, "ctl00$MainContent${0}");
        for (int i = 0; i < ls.Count; i++)
        {
            Pgi.Auto.Common com = new Pgi.Auto.Common();
            com = ls[i];
            if (ls[i].Code == "")
            {

                Pgi.Auto.Public.MsgBox(this, "alert", ls[i].Value + " 不能为空!");
                //Response.Write(ls[i].Value + "不能为空!");
                return false;
            }

        }

        //表体生成SQL
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        ldt.AcceptChanges();
        if (ldt.Rows.Count == 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "采购清单不能为空!");
            return false;
        }
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["PlanReceiveDate"].ToString() == "")
            {
                Pgi.Auto.Public.MsgBox(this, "alert", "第" + (i + 1).ToString() + "行计划到货日期不能为空!");
                return false;
            }
        }

        //特殊数据处理
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code == "podomain")
            {
                if (ls[i].Value == "昆山工厂")
                {
                    ls[i].Value = "200";
                    lsdomain = "200";
                }
                else if (ls[i].Value == "上海工厂")
                {
                    ls[i].Value = "100";
                    lsdomain = "100";
                }
                else
                {
                    lsdomain = ls[i].Value;
                }
            }

            if (ls[i].Code.ToLower() == "buyername")
            {
                string[] lsstr = ls[i].Value.ToString().Split('|');
                if (lsstr.Length == 2)
                {
                    ls[i].Value = lsstr[1];


                    //增加采购负责人ID
                    Pgi.Auto.Common lcbuyerid = new Pgi.Auto.Common();
                    lcbuyerid.Code = "buyerid";
                    lcbuyerid.Key = "";
                    lcbuyerid.Value = lsstr[0];
                    ls.Add(lcbuyerid);
                }

            }

            if (ls[i].Code.ToLower() == "povendorid")
            {
                string[] lsstr = ls[i].Value.ToString().Split('|');
                if (lsstr.Length == 3)
                {
                    ls[i].Value = lsstr[0];


                    //增加供应商名称
                    Pgi.Auto.Common lcpovendname = new Pgi.Auto.Common();
                    lcpovendname.Code = "povendorname";
                    lcpovendname.Key = "";
                    lcpovendname.Value = lsstr[1];
                    ls.Add(lcpovendname);
                }


            }

        }



        if (this.m_sid == "")
        {
            //没有单号，自动生成
            string lsid = "K";
            if (((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).SelectedValue == "100")
            {
                lsid = "S";
            }
            lsid += System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
            this.m_sid = Pgi.Auto.Public.GetNo("PO", lsid, 0, 4);
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "pono")
                {
                    ls[i].Value = this.m_sid;
                    ((TextBox)this.FindControl("ctl00$MainContent$PoNo")).Text = this.m_sid;
                }

            }

            //新增时增加创建人ID
            Pgi.Auto.Common lccreate_byid = new Pgi.Auto.Common();
            lccreate_byid.Code = "createbyid";
            lccreate_byid.Key = "";
            lccreate_byid.Value = ((LoginUser)Session["LogUser"]).UserId;
            ls.Add(lccreate_byid);
        }


        //自定义，上传文件
        //var filepath = "";
        // if (this.FileUpload1.HasFile)
        // {
        //     SaveFile(this.FileUpload1, this.m_sid, out filepath, "123.txt", "123.txt");
        // //增加上传文件列
        // Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        // lcfile.Code = "attachments";
        // lcfile.Key = "";
        // lcfile.Value = filepath;
        // ls.Add(lcfile);

        //}
        string filepath = "";//string filepath=this.UploadFiles(this.uploadcontrol);
        string[] ls_files = ip_filelist.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_2 = item.Split(',');
            if (ls_files_2.Length == 3)//挪动路径到po单号下面
            {
                FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

                var sorpath = @"\UploadFile\Purchase\";
                var despath = MapPath("~") + sorpath + @"\" + m_sid + @"\";
                if (!System.IO.Directory.Exists(despath))
                {
                    System.IO.Directory.CreateDirectory(despath);
                }
                string tmp = despath + ls_files_2[1].Replace(sorpath, "");
                if (File.Exists(tmp))
                {
                    File.Delete(tmp);
                }
                fi.MoveTo(tmp);

                filepath += item.Replace(@"\UploadFile\Purchase\", @"\UploadFile\Purchase\" + m_sid + @"\") + ";";
            }
            else
            {
                filepath += item + ";";
            }
        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            filepath += item + ";";
        }
        if (filepath != "") { filepath = filepath.Substring(0, filepath.Length - 1); }


        // 增加上传文件列
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "attachments";
        lcfile.Key = "";
        lcfile.Value = filepath;
        ls.Add(lcfile);











        //主表相关字段赋值到明细表
        decimal lntotalpay = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            for (int j = 0; j < ls.Count; j++)
            {
                if (ls[j].Code.ToLower() == "pono")
                {
                    ldt.Rows[i]["pono"] = ls[j].Value;
                }
                if (ldt.Rows[i]["TotalPrice"].ToString() != "")
                {
                    lntotalpay += Convert.ToDecimal(ldt.Rows[i]["TotalPrice"].ToString());
                }
            }
        }

        //从明细表中合计采购总金额
        Pgi.Auto.Common lcTotalPay = new Pgi.Auto.Common();
        lcTotalPay.Code = "totalpay";
        lcTotalPay.Key = "";
        lcTotalPay.Value = ((LoginUser)Session["LogUser"]).UserId;
        ls.Add(lcTotalPay);


        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PUR_PO_Main_Form"));


        //明细数据自动生成SQL，并增入SUM
        List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PUR_PO_Dtl_Form", "id", "flag,wlType,wlSubType,wlh,wlmc,wlms,targetPrice,targetTotalPrice,RecmdVendorName,historyPrice,deliveryDate,RecmdVendorId,attachments,attachments_name,pt_status");
        for (int i = 0; i < ls1.Count; i++)
        {
            ls_sum.Add(ls1[i]);
        }

        //明细删除增加到list中
        if (Session["del"] != null)
        {
            DataTable ldt_del = (DataTable)Session["del"];
            for (int i = 0; i < ldt_del.Rows.Count; i++)
            {
                Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
                ls_del.Sql = "delete from PUR_PO_Dtl_Form where id=" + ldt_del.Rows[i]["id"].ToString() + "";
                ls_del.Sql += ";update PUR_PR_Dtl_Form set  Status='0' where prno='" + ldt_del.Rows[i]["prno"].ToString() + "' and rowid='" + ldt_del.Rows[i]["prrowid"].ToString() + "'";
                ls_sum.Add(ls_del);
            }
            Session["del"] = null;
        }

        //处理PR明细表状态
        if (ldt.Rows.Count > 0)
        {
            Pgi.Auto.Common ls_status = new Pgi.Auto.Common();
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                ls_status.Sql += "update PUR_PR_Dtl_Form set status=2 where prno='" + ldt.Rows[i]["prno"].ToString() + "' and rowid='" + ldt.Rows[i]["prrowid"].ToString() + "'";
                if (i > 0)
                {
                    ls_status.Sql += ";";
                }
            }
            ls_sum.Add(ls_status);
        }



        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        if (ln > 0)
        {
            bflag = true;
            // string instanceid = ln.ToString();
            string title = "PO采购单申请" + lspgi_no + "--" + this.m_sid;
            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";

        }
        else
        {
            bflag = false;
        }

        return bflag;
    }*/


    protected void txt_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        string[] lsrate = PoVendorId.Text.Split('|');
        string lsrate1 = "0";
        if (lsrate.Length == 3)
        {
            lsrate1 = lsrate[2];
        }

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["taxrate"] = lsrate1;

            if (ldt.Rows[i]["notaxprice"].ToString() != "" && ldt.Rows[i]["purqty"].ToString() != "")
            {
                ldt.Rows[i]["notax_totalprice"] =(Convert.ToDecimal(ldt.Rows[i]["notaxprice"].ToString()) * Convert.ToDecimal(ldt.Rows[i]["purqty"].ToString())).ToString("0.0000");
            }

            if (ldt.Rows[i]["notaxprice"].ToString() != "" && ldt.Rows[i]["taxrate"].ToString() != "")
            {
                ldt.Rows[i]["taxprice"] = (Convert.ToDecimal(ldt.Rows[i]["notaxprice"].ToString()) * (1 + Convert.ToDecimal(ldt.Rows[i]["taxrate"].ToString().Replace("%", "")) / 100)).ToString("0.0000");
            }

            if (ldt.Rows[i]["taxprice"].ToString() != "" && ldt.Rows[i]["purqty"].ToString() != "")
            {

                ldt.Rows[i]["totalprice"] = (Convert.ToDecimal(ldt.Rows[i]["taxprice"].ToString()) * Convert.ToDecimal(ldt.Rows[i]["purqty"].ToString())).ToString("0.0000");

            }
        }
        this.gv.Columns.Clear();
        loadControl(ldt);

    }


    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
         bool flag = SaveData();
        //保存当前流程
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
         bool flag= SaveData();
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

  

    protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (Session["pr_select"] != null)
        {
            DataTable ldt1 = (DataTable)Session["pr_select"];
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            int ln = 0;

            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (Convert.ToInt32(ldt.Rows[i]["rowid"].ToString()) > ln)
                {
                    ln = Convert.ToInt32(ldt.Rows[i]["rowid"].ToString());
                }
            }
            for (int i = 0; i < ldt1.Rows.Count; i++)
            {
                DataRow ldr = ldt.NewRow(); ;
                if (ldt.Select("prno='" + ldt1.Rows[i]["prno"].ToString() + "' and prrowid='" + ldt1.Rows[i]["rowid"].ToString() + "'").Length > 0)
                {
                    continue;
                }

                ldr["rowid"] = (ln + 1).ToString();
                ldr["PRNo"] = ldt1.Rows[i]["prno"].ToString();
                ldr["PRRowId"] = ldt1.Rows[i]["rowid"].ToString();
                //ldr["wlType"] = ldt1.Rows[i]["wlType"].ToString();
                //ldr["wlSubType"] = ldt1.Rows[i]["wlSubType"].ToString();
                //ldr["wlh"] = ldt1.Rows[i]["wlh"].ToString();
                //ldr["wlmc"] = ldt1.Rows[i]["wlmc"].ToString();
                //ldr["wlms"] = ldt1.Rows[i]["wlms"].ToString();

                //if (ldt1.Rows[i]["wlType"].ToString().StartsWith("4010"))//ldt1.Rows[i]["wlType"].ToString().Substring(0, 4) == "4010"
                //{
                //    ldr["wlType"] = "[" + ldt1.Rows[i]["wlh"].ToString() + "]" + ldt1.Rows[i]["wlType"].ToString().Substring(5) + "/" + ldt1.Rows[i]["wlSubType"].ToString();
                //}
                //else
                //{
                //    if (ldt1.Rows[i]["wlh"].ToString() != "无")//ldt1.Rows[i]["wlh"].ToString() != ""
                //    {
                //        ldr["wlType"] = "[" + ldt1.Rows[i]["wlh"].ToString() + "]" + ldt1.Rows[i]["wlType"].ToString().Substring(5);
                //    }
                //    else
                //    {
                //        ldr["wlType"] = ldt1.Rows[i]["wlType"].ToString();//费用 合同类
                //    }
                //}


                //if (ldt1.Rows[i]["wlType"].ToString().StartsWith("4") == true || ldt1.Rows[i]["wlType"].ToString().StartsWith("1"))
                //{
                //    ldr["wlType"] = "[" + ldt1.Rows[i]["wlh"].ToString() + "]" + ldt1.Rows[i]["wlType"].ToString().Substring(5);

                //    if (ldt1.Rows[i]["wlType"].ToString().StartsWith("4010"))
                //    {
                //        ldr["wlType"] = ldr["wlType"] + "/" + ldt1.Rows[i]["wlSubType"].ToString();
                //    }
                //}
                //else
                //{
                //    if (ldt1.Rows[i]["wlh"].ToString() != "")
                //    {
                //        ldr["wlType"] = "[" + ldt1.Rows[i]["wlh"].ToString() + "]";
                //    }
                //    ldr["wlType"] = ldr["wlType"] + ldt1.Rows[i]["wlType"].ToString();//费用 合同类
                //}
                ldr["wlType"] = ldt1.Rows[i]["wlh"].ToString() == "" ? "无" : ldt1.Rows[i]["wlh"].ToString();

                ldr["wldesc"] = ldt1.Rows[i]["wlmc"].ToString() + "[" + ldt1.Rows[i]["wlms"].ToString() + "]";
                ldr["note2"] = ldt1.Rows[i]["note2"].ToString();
                ldr["currency"] = ldt1.Rows[i]["currency"].ToString();
                ldr["notax_targetPrice"] = ldt1.Rows[i]["notax_targetPrice"].ToString();
                ldr["PurQty"] = ldt1.Rows[i]["qty"].ToString();
                ldr["RecmdVendorName"] = ldt1.Rows[i]["RecmdVendorName"].ToString().Substring(ldt1.Rows[i]["RecmdVendorName"].ToString().IndexOf('_') + 1).Replace("有限公司", "");
                ldr["NoTaxPrice"] = ldt1.Rows[i]["notax_targetPrice"].ToString();
                //ldr["pt_status"] = ldt1.Rows[i]["pt_status"].ToString();
                string[] lsrate = ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Text.Split('|');
                if (lsrate.Length == 3)
                {
                    ldr["taxrate"] = lsrate[2].ToString().Trim();

                    if (ldt1.Rows[i]["notax_targetPrice"].ToString() != "")
                    {
                        ldr["TaxPrice"] = (Convert.ToDecimal(ldt1.Rows[i]["notax_targetPrice"].ToString()) * (1 + Convert.ToDecimal(ldr["taxrate"]) / 100)).ToString("0.0000");
                    }

                }



                if (ldt1.Rows[i]["deliveryDate"].ToString() != "")
                {
                    ldr["deliveryDate"] = Convert.ToDateTime(ldt1.Rows[i]["deliveryDate"].ToString()).ToShortDateString();
                }
                if (ldt1.Rows[i]["notax_targetPrice"].ToString() != "" && ldt1.Rows[i]["qty"].ToString() != "")
                {
                    ldr["notax_targetTotalPrice"] = (Convert.ToDecimal(ldt1.Rows[i]["notax_targetPrice"].ToString()) * Convert.ToDecimal(ldt1.Rows[i]["qty"].ToString())).ToString("0.0000");
                    ldr["notax_totalprice"] = ldr["notax_targetTotalPrice"];
                }
                if (ldr["TaxPrice"].ToString() != "" && ldt1.Rows[i]["qty"].ToString() != "")
                {
                    ldr["TotalPrice"] = (Convert.ToDecimal(ldr["TaxPrice"]) * Convert.ToDecimal(ldt1.Rows[i]["qty"].ToString())).ToString("0.0000");
                }

                ldr["notax_historyPrice"] = ldt1.Rows[i]["notax_historyPrice"].ToString() == "" ? "新单价" : ldt1.Rows[i]["notax_historyPrice"].ToString();

                ldr["attachments"] = ldt1.Rows[i]["attachments"].ToString();
                ldr["attachments_name"] = "查看";

                ln += 1;
                ldt.Rows.Add(ldr);
            }

            //未保存时，重新更改序号
            if (this.m_sid == "")
            {
                for (int i = 0; i < ldt.Rows.Count; i++)
                {
                    ldt.Rows[i]["rowid"] = (i + 1).ToString();
                }
            }

            this.gv.Columns.Clear();
            loadControl(ldt);//Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt, 2);
            Session["pr_select"] = null;
        }
        else
        {
            string param = e.Parameters.Trim();
            int i = param.Length - param.Replace("|", @"").Length;
            if (i < 2)//采购负责人、采购类别  下拉动态改变
            {
                DataTable ldt = null;
                string lssql = "";
                //where pono = (select pono from PUR_PO_Main_Form where pono = '{0}' and PoType = '{1}' and(buyerid + '|' + buyername) = '{2}') 
                lssql = @"
                        select po.*
                            ,ISNULL(pr.wlh,'无') wlType
                            ,pr.wlSubType,pr.wlh,pr.wlmc+'['+pr.wlms+']' wldesc,pr.usefor
                            ,replace(right(pr.RecmdVendorName,LEN(pr.RecmdVendorName)-CHARINDEX('_',pr.RecmdVendorName)),'有限公司','') RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName
                            ,pr.ApointVendorId,pr.unit,isnull(cast(pr.notax_historyPrice as nvarchar(max)),'新单价') notax_historyPrice,pr.notax_targetPrice,pr.deliveryDate
                            ,(pr.notax_targetPrice*pr.qty) as notax_targetTotalPrice,pr.attachments
                            ,case when CHARINDEX('刀具',pr_main.PRType)>0 or CHARINDEX('辅料',pr_main.PRType)>0 or CHARINDEX('原材料',pr_main.PRType)>0 then isnull(pr.wlSubType,pr.wlType) else pr.note end note2
                            ,'查看' as attachments_name
                        from PUR_PO_Dtl_Form po
                            left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid
                            left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno
                        where pono='{0}' 
                        order by po.NoTaxPrice-ISNULL(pr.notax_historyPrice,0) desc,po.rowid";

                lssql = string.Format(lssql, ((TextBox)this.FindControl("ctl00$MainContent$PoNo")).Text
                    , ((ASPxComboBox)this.FindControl("ctl00$MainContent$PoType")).Value.ToString()
                    , param);

                ldt = DbHelperSQL.Query(lssql).Tables[0];

                this.gv.Columns.Clear();
                loadControl(ldt);
            }
            else//i== 2//采购供应商  下拉动态改变
            {
                txt_TextChanged(sender, e);
            }
        }

        
    }


    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Purchase";
    public void SaveFile(FileUpload fileupload, string subpath, out string filepath, string oldName, string newName)
    {
        var path = MapPath("~") + savepath + "\\" + subpath;
        //Create directory
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        //save file
        var filename = "";
        if (fileupload.HasFile)
        {
            filename = fileupload.FileName.Replace(oldName, newName);
            path = path + "\\" + filename;
            fileupload.SaveAs(path.Replace("&", "_").TrimStart(' '));
        }
        //return save path
        filepath = "\\" + savepath + "\\" + subpath + "\\" + filename.Replace("&", "_").TrimStart(' ');
    }


    public string UploadFiles(DevExpress.Web.ASPxUploadControl uc)
    {
       DevExpress.Web.UploadedFile[] files = uc.UploadedFiles;//获得上传的所有文件  
        //string filenames = "";
        //string filename = "";
        //string savepath = "";
        if (files.Length != 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].FileName != "")
                {
                    // filename = files[i].FileName;
                    if (savepath!= "UploadFile\\Purchase")
                    {
                        savepath += ";";
                    }
                     savepath = Server.MapPath(savepath) + files[i].FileName;
                    files[i].SaveAs(savepath);
                   // filenames += filename + "-";
                }
            }
            //filenames = filenames.Substring(0, filenames.Length - 1);
           
        }
        return savepath;
    }
    #endregion

    //protected void btndel_Click(object sender, EventArgs e)
    //{
    //    DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
    //    DataTable ldt_del = ldt.Clone();
    //    for (int i = ldt.Rows.Count-1; i >=0; i--)
    //    {
    //        if (ldt.Rows[i]["flag"].ToString()=="1" && ldt.Rows[i]["id"].ToString()=="")
    //        {
    //            ldt.Rows[i].Delete();
    //        }
    //        else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
    //        {
    //            ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
    //            ldt.Rows[i].Delete();
    //        }
    //    }
    //    ldt.AcceptChanges();
    //    if (ldt_del.Rows.Count>0)
    //    {
    //        if (Session["del"] !=null)
    //        {
    //            for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
    //            {
    //                ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
    //            }

    //        }
    //        Session["del"] = ldt_del;
    //    }
    //    loadControl(ldt);//Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt,2);
    //    //Pgi.Auto.Public.MsgBox(this.Page,"alert","删除成功!");
    //}

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        loadControl(ldt);
    }

    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
        {
            return;
        }
        string potype = "";
        if (PoType is ASPxComboBox)
        {
            if (PoType.Value != null)
            {
                potype = PoType.Value.ToString();
            }
        }
        else
        {
            return;
        }

        int lncindex = 0; int prnoindex = 0; int RecmdVendorNameindex = 0;
        for (int i = 0; i < this.gv.DataColumns.Count; i++)
        {
            if (this.gv.DataColumns[i].FieldName == "notax_TotalPrice")
            {
                lncindex = i;//采购总价(未税)
            }
            if (this.gv.DataColumns[i].FieldName == "PRNo")
            {
                prnoindex = i;
            }
            if (this.gv.DataColumns[i].FieldName == "RecmdVendorName")
            {
                RecmdVendorNameindex = i;
            }
        }
        //int lncindex_cell= lncindex - 1; int prnoindex_cell = prnoindex + 1; int RecmdVendorNameindex_cell = RecmdVendorNameindex - 2;// RecmdVendorNameindex - 1;
        //if (potype != "存货")
        //{
        //    lncindex_cell = lncindex + 1;
        //}

        int lncindex_cell = lncindex - 2; int prnoindex_cell = prnoindex + 1; int RecmdVendorNameindex_cell = RecmdVendorNameindex - 2;


        decimal lmbzj = Convert.ToDecimal(e.GetValue("notax_targetTotalPrice"));//目标总价(未税)
        decimal lzj = Convert.ToDecimal(e.GetValue("notax_TotalPrice").ToString() == "" ? "0" : e.GetValue("notax_TotalPrice").ToString());//采购总价(未税)
        //decimal ln = ((lzj - lmbzj) / lmbzj) *100;
        decimal ln = 0;
        if (lmbzj != 0)
        {
            ln = ((lzj - lmbzj) / lmbzj) * 100;
        }

        if (ln > 0 && ln <= 20)
        {
            e.Row.Cells[lncindex_cell].Style.Add("background-color", "#EEEE00");
        }
        else if (ln > 20)
        {
            e.Row.Cells[lncindex_cell].Style.Add("color", "white");
            e.Row.Cells[lncindex_cell].Style.Add("background-color", "red");
        }

        string PRNo = Convert.ToString(e.GetValue("PRNo"));
        e.Row.Cells[prnoindex_cell].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=62676129-f059-4c92-bd5c-86897f5b0d5&instanceid="
            + PRNo + "&display=1' target='_blank'>" + PRNo.ToString() + "</a>";

        //add by heguiqin20180515 采购供应商跟推荐供应商不一致，背景色黄色
        string PoVendor = "";
        if (PoVendorId != null)
        {
            string[] pvarr = PoVendorId.Text.Split('|');
            if (pvarr.Length >= 2)
            {
                //PoVendor = pvarr[0] + "_" + pvarr[1];
                PoVendor = pvarr[1];
            }
        }

        if (e.GetValue("RecmdVendorName").ToString() != PoVendor.Replace("有限公司", "") && e.GetValue("RecmdVendorName").ToString() !="")
        {
            e.Row.Cells[RecmdVendorNameindex_cell].Style.Add("background-color", "#EEEE00");
        }

        //新增价格 背景色 
        #region color
        if (this.PoNo is Control)
        {
            string pono = PoNo.Text;
            if (pono != "")
            {

                string stepname_po = DbHelperSQL.Query("select top 1 stepname from RoadFlowWebForm.dbo.WorkFlowTask where flowid='ce701853-e13b-4c39-9cd6-b97e18656d31' and InstanceID='"
                            + pono + "' order by sort desc").Tables[0].Rows[0][0].ToString();
                if (stepname_po != "采购负责人")//申请步骤
                {
                    string pricetype = DbHelperSQL.Query("select pricetype from PUR_PO_Main_Form where pono='" + pono + "'").Tables[0].Rows[0][0].ToString();
                    if (pricetype == "2")
                    {
                        decimal notax_historyPrice = Convert.ToDecimal(e.GetValue("notax_historyPrice").ToString() == "新单价" ? "0" : e.GetValue("notax_historyPrice").ToString());
                        decimal NoTaxPrice = Convert.ToDecimal(e.GetValue("NoTaxPrice").ToString());
                        //if (NoTaxPrice > notax_historyPrice)
                        //{
                        //    e.Row.Style.Add("background-color", "#FFE7BA");
                        //}
                        //else
                        //{
                        //    e.Row.Style.Add("background-color", "#FFFFFF");
                        //}

                        if (NoTaxPrice > notax_historyPrice)
                        {
                            //e.Row.Style.Add("background-color", "#FFFFFF");
                        }
                        else
                        {
                            e.Row.Style.Add("background-color", "#FFFFFF");
                            e.Row.Style.Add("font-style", "italic");
                            e.Row.Style.Add("color", "#969696");

                            e.Row.Cells[prnoindex_cell].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=62676129-f059-4c92-bd5c-86897f5b0d5&instanceid="
                + PRNo + "&display=1' target='_blank' style='color:#969696'>" + PRNo.ToString() + "</a>";

                            //((Label)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["wlType"], "wlType")).Style.Add("color", "#CDC5BF");
                            //((Label)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["RecmdVendorName"], "RecmdVendorName")).Style.Add("color", "#CDC5BF");
                            //((Label)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["notax_historyPrice"], "notax_historyPrice")).Style.Add("color", "#CDC5BF");
                            ((HyperLink)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["attachments_name"], "attachments_name")).Style.Add("color", "#969696");

                            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.ForeColor = System.Drawing.Color.FromName("#969696");
                            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).DisabledStyle.Font.Italic = true;

                            ((TextBox)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).Style.Add("color", "#969696");
                            ((TextBox)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).Style.Add("font-style", "italic");

                            ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.ForeColor = System.Drawing.Color.FromName("#969696");
                            ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).DisabledStyle.Font.Italic = true;

                            e.Row.Cells[RecmdVendorNameindex_cell].Style.Remove("background-color");
                            e.Row.Cells[lncindex_cell].Style.Add("color", "#969696");
                            e.Row.Cells[lncindex_cell].Style.Remove("background-color");


                        }
                    }
                }
            }
        }

        #endregion

    }

    protected void gv_DataBound(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, e.GetType(), "gridkey", "init_pay();change_paytype();appendSearch();", true);
    }

    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        /*
        string UploadDirectory = "~/UploadFile/Purchase/";

        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFileUrl = UploadDirectory + resultFileName;
        string resultFilePath = MapPath(resultFileUrl);
        e.UploadedFile.SaveAs(resultFilePath);

        //UploadingUtils.RemoveFileWithDelay(resultFileName, resultFilePath, 5);

        string name = e.UploadedFile.FileName;
        string url = ResolveClientUrl(resultFileUrl);
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";
        e.CallbackData = name + "|" + url + "|" + sizeText;
        */

        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName; 
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }


    protected void gv2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        if (param == "change")//采购类别改变
        {
            string paytype_page = PayType.SelectedValue;//当前付款类型
            DataTable ldt_pay = DbHelperSQL.Query("select * from PUR_PO_ContractPay_Form where 1=0").Tables[0];

            string sql = @"select * from PUR_PO_ContractPay_Form where pono='" + PoNo.Text + "' order by rowid";
            DataTable ldt_pay_db = DbHelperSQL.Query(sql).Tables[0];

            double paytotal = Convert.ToDouble(gv.GetTotalSummaryValue(gv.TotalSummary["TotalPrice"]));

            int flag = 0;
            if (ldt_pay_db != null)
            {
                if (ldt_pay_db.Rows.Count > 1)
                {
                    flag = 1;//数据库有多笔数据
                }
                if (ldt_pay_db.Rows.Count == 1)
                {
                    if (ldt_pay_db.Rows[0]["PayRate"].ToString() == "100")
                    {
                        flag = 2;//数据库有一笔数据 且 比例是100%
                    }
                    
                }
            }

            if (paytype_page == "")
            {
                this.gv2.Columns.Clear();
                loadControl_Pay(ldt_pay);
            }
            else if (paytype_page.Contains("一次性"))
            {
                if (flag == 2)
                {
                    this.gv2.Columns.Clear();
                    loadControl_Pay(ldt_pay_db);
                }
                else
                {
                    DataRow ldr = ldt_pay.NewRow();
                    ldr["rowid"] = "10";
                    ldr["PayRate"] = 100;
                    ldr["PayMoney"] = paytotal;
                    ldt_pay.Rows.Add(ldr);

                    this.gv2.Columns.Clear();
                    loadControl_Pay(ldt_pay);
                }

            }
            else//合同
            {
                if (flag == 1)
                {
                    this.gv2.Columns.Clear();
                    loadControl_Pay(ldt_pay_db);
                }
                else
                {
                    DataRow ldr = ldt_pay.NewRow();
                    ldr["rowid"] = "10";
                    ldt_pay.Rows.Add(ldr);

                    this.gv2.Columns.Clear();
                    loadControl_Pay(ldt_pay);
                }

            }

        }

        if (param == "load")//新增，修改采购供应商 gv 回传后，调用
        {
            DataTable ldt_2 = Pgi.Auto.Control.AgvToDt(this.gv2);
            double paytotal = Convert.ToDouble(gv.GetTotalSummaryValue(gv.TotalSummary["TotalPrice"]));
            if (paytotal > 0)
            {
                for (int i = 0; i < ldt_2.Rows.Count; i++)
                {
                    if (ldt_2.Rows[i]["PayRate"].ToString() != "")
                    {
                        ldt_2.Rows[i]["PayMoney"] = paytotal * (Convert.ToDouble(ldt_2.Rows[i]["PayRate"]) / 100);
                    }
                }
            }
            else
            {
                ldt_2.Rows.Clear();
                PayType.SelectedValue = "";
            }
            
            this.gv2.Columns.Clear();
            loadControl_Pay(ldt_2);
        }
    }

    public void loadControl_Pay(DataTable ldt_pay)
    {
        string paytype_page = PayType.SelectedValue;//当前付款类型

        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PO_PAY", this.gv2, ldt_pay, 2);

        for (int i = 0; i < ldt_pay.Rows.Count; i++)
        {
            ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayClause"], "PayClause")).Width = Unit.Pixel(120);
            ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFunc"], "PayFunc")).Width = Unit.Pixel(120);
            ((ASPxComboBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayFile"], "PayFile")).Width = Unit.Pixel(150);

            //AutoPostBack = true 已经在 数据库配置好，list_type_ref设定
            ((TextBox)this.gv2.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).TextChanged += new EventHandler(txt_pay_TextChanged);
        }

        if (ldt_pay.Rows.Count == 1 && paytype_page.Contains("一次性"))
        {
            ((TextBox)this.gv2.FindRowCellTemplateControl(0, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).Enabled = false;
            ((TextBox)this.gv2.FindRowCellTemplateControl(0, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BackColor = System.Drawing.Color.Transparent;
            ((TextBox)this.gv2.FindRowCellTemplateControl(0, (GridViewDataColumn)this.gv2.Columns["PayRate"], "PayRate")).BorderStyle = BorderStyle.None;
        }
    }

    protected void txt_pay_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt_2 = Pgi.Auto.Control.AgvToDt(this.gv2);
        double paytotal = Convert.ToDouble(gv.GetTotalSummaryValue(gv.TotalSummary["TotalPrice"]));

        for (int i = 0; i < ldt_2.Rows.Count; i++)
        {
            if (ldt_2.Rows[i]["PayRate"].ToString() != "")
            {
                ldt_2.Rows[i]["PayMoney"] = paytotal * (Convert.ToDouble(ldt_2.Rows[i]["PayRate"]) / 100);
            }
            else
            {
                ldt_2.Rows[i]["PayMoney"] = null;
            }
        }
        this.gv2.Columns.Clear();
        loadControl_Pay(ldt_2);

    }
    
    protected void btnadd_contract_Click(object sender, EventArgs e)
    {
        //double paytotal = Convert.ToDouble(gv.GetTotalSummaryValue(gv.TotalSummary["TotalPrice"]));
        //if (paytotal <= 0)
        //{
        //    Pgi.Auto.Public.MsgBox(this, "alert", "采购总价(含税)不可为0!");
        //    return;
        //}
        DataTable ldt_pay = Pgi.Auto.Control.AgvToDt(this.gv2);
        int ln = 0;

        for (int i = 0; i < ldt_pay.Rows.Count; i++)
        {
            if (Convert.ToInt32(ldt_pay.Rows[i]["rowid"].ToString()) > ln)
            {
                ln = Convert.ToInt32(ldt_pay.Rows[i]["rowid"].ToString());
            }
        }

        DataRow ldr = ldt_pay.NewRow();
        ldr["rowid"] = (ln + 10).ToString();
        ldt_pay.Rows.Add(ldr);

        this.gv2.Columns.Clear();
        loadControl_Pay(ldt_pay);
    }

    protected void btndel_contract_Click(object sender, EventArgs e)
    {
        DataTable ldt_pay = Pgi.Auto.Control.AgvToDt(this.gv2);
        for (int i = ldt_pay.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt_pay.Rows[i]["flag"].ToString() == "1" && ldt_pay.Rows[i]["id"].ToString() == "")
            {
                ldt_pay.Rows[i].Delete();
            }
            else if (ldt_pay.Rows[i]["flag"].ToString() == "1" && ldt_pay.Rows[i]["id"].ToString() != "")
            {
                ldt_pay.Rows[i].Delete();
            }
        }
        ldt_pay.AcceptChanges();

        this.gv2.Columns.Clear();
        loadControl_Pay(ldt_pay);
    }

    protected void GetGrid(DataTable DT, string potype)
    {
        //用于产品/项目
        string sql_po_wltype = @"";
        if (potype == "PO")
        {
            sql_po_wltype = @"select LB as value from PUR_PO_CLASS where ms_code<>'3'";
        }
        else
        {
            sql_po_wltype = @"select LB as value from PUR_PO_CLASS";
        }
        DataTable ldt_po_wltype = DbHelperSQL.Query(sql_po_wltype).Tables[0];

        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            ASPxComboBox tb1 = ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["po_wltype"], "po_wltype"));
            tb1.DataSource = ldt_po_wltype;
            tb1.TextField = "value";
            tb1.ValueField = "value";
            tb1.DataBind();

            //复制时候判断，存在与否
            if (ldt_po_wltype.Select("value='" + DT.Rows[i]["po_wltype"].ToString() + "'").Length <= 0)
            {
                tb1.Value = "";
            }
            else
            {
                tb1.Value = DT.Rows[i]["po_wltype"].ToString();
            }
            
        }


    }

    //#region "WebMethod"

    ////获取 是否是 合同模块
    //[WebMethod]
    //public static string getType_Contain(string potype)
    //{
    //    var sql = string.Format("select check_accept from [dbo].[PUR_Permission_category] where type='{0}' or type2='{0}'", potype);
    //    var obj = DbHelperSQL.GetSingle(sql);
    //    return obj == null ? "" : obj.ToString();
    //}

    //pdf按钮是否显示
    [WebMethod]
    public static string get_iscomplete(string PoNo)
    {
        var sql = string.Format("select isnull(iscomplete,'') iscomplete from [dbo].[PUR_PO_Main_Form] where PoNo='{0}'", PoNo);
        var obj = DbHelperSQL.GetSingle(sql);
        return obj == null ? "" : obj.ToString();
    }

    //#endregion

    #region 在页面中显示要显示的字段
    /// <summary>
    /// 在页面中显示要显示的字段
    /// </summary>
    /// <param name="lsform_type">要显示字段大类</param>
    /// <param name="lsform_div">要显示字段小类</param>
    /// <param name="lncolumn">设置每行显示列的数量</param>
    /// <param name="lsrow_style">设置行样式</param>
    /// <param name="lscolumn_style">设置列样式</param>
    /// <param name="lscontrol_style">设置显示控件样式（默认统一）</param>
    /// <param name="ldt_head">赋值数据源（可选参数，默认抓取Datatable中第一行）</param>
    /// <returns></returns>
    public static List<TableRow> ShowControl(string lsform_type, string lsform_div, int lncolumn, string lsrow_style, string lscolumn_style, string lscontrol_style_read, string lscontrol_style_write, DataTable ldt_head = null)
    {
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where (control_id<>'' or control_id is null) " + lswhere + " order by control_order",
            new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

        List<TableRow> ls = new List<TableRow>();
        int ln = 1;
        TableRow lrow = null;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            if ((i % lncolumn) == 0)
            {
                lrow = new TableRow();
                if (lsrow_style != "")
                {
                    lrow.CssClass = lsrow_style;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {

                ls.Add(lrow);
                lrow = new TableRow();
                //行样式
                if (lsrow_style != "")
                {
                    lrow.CssClass = lsrow_style;
                }
            }

            //列样式
            TableCell lcellHead = new TableCell();
            if (lscolumn_style != "")
            {
                lcellHead.CssClass = lscolumn_style;
            }
            TableCell lcellContent = new TableCell();
            if (lscolumn_style != "")
            {
                lcellContent.CssClass = lscolumn_style;
            }
            Label lbl = new Label();
            lbl.ID = "lbl_" + lsform_type + "_" + lsform_div + "_" + ln.ToString();
            lbl.Text = ldt.Rows[i]["control_dest"].ToString();

            //-----------------------------------控件判断开始----------------------------------------
            //文本控件
            if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
            {
                #region "TextBox"
                TextBox ltxt = new TextBox();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                {
                    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                }
                else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.ReadOnly = true;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.CssClass = lscontrol_style_read;
                }
                else
                {
                    ltxt.CssClass = lscontrol_style_write;
                }
                
                if (ldt_head != null)
                {
                    ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                }



                lcellContent.Controls.Add(ltxt);
                #endregion
            }
            //下拉控件
            else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
            {
                #region "DropDownList"
                DropDownList ltxt = new DropDownList();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式                
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.CssClass = lscontrol_style_read;
                }
                else
                {

                    ltxt.CssClass = lscontrol_style_write;
                }
                
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        if (ls1.Length == ls2.Length)
                        {
                            for (int j = 0; j < ls1.Length; j++)
                            {
                                ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                            }
                        }

                    }
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    for (int j = 0; j < ldt_source.Rows.Count; j++)
                    {
                        ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    }
                }

                if (ldt_head != null)
                {
                    if (ltxt.Items.Count > 0)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }

                }

                lcellContent.Controls.Add(ltxt);
                #endregion

            }

            //CheckBoxList控件
            else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOXLIST")
            {
                #region "CheckBoxList"
                CheckBoxList ltxt = new CheckBoxList();
                HiddenField htxt = new HiddenField();
                htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                ltxt.AutoPostBack = false;
                ltxt.ID = "chk" + ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                ltxt.RepeatLayout = RepeatLayout.Flow;
                ltxt.RepeatColumns = 3;
                //事件
                var script = "var val='';$(\"input[id*='" + ldt.Rows[i]["control_id"].ToString().ToLower() + "']\").each(function(){   val+=$(this).val();   }); ";
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onclick", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }
                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    ltxt.CssClass = lscontrol_style;
                //}
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        if (ls1.Length == ls2.Length)
                        {
                            for (int j = 0; j < ls1.Length; j++)
                            {
                                ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                            }
                        }

                    }
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    for (int j = 0; j < ldt_source.Rows.Count; j++)
                    {
                        ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    }
                }

                if (ldt_head != null)
                {
                    if (ltxt.Items.Count > 0)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }

                }

                lcellContent.Controls.Add(ltxt);
                #endregion

            }

            else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOX")
            {
                #region "CheckBox"
                CheckBox ltxt = new CheckBox();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                ////是否服务器运行
                //ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }
                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                    // ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    ltxt.CssClass = lscontrol_style;
                //}
                if (ldt.Rows[i]["control_type_value"].ToString() != "")
                {
                    ltxt.Checked = true;
                }

                if (ldt_head != null)
                {
                    if (ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString() == "1" || ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString().ToUpper() == "TRUE")
                    {
                        ltxt.Checked = true;
                    }
                }

                lcellContent.Controls.Add(ltxt);
                #endregion
            }
            else if (ldt.Rows[i]["control_type"].ToString() == "FILEUPLOAD")
            {
                #region "FileUpLoad"
                FileUpload ltxt = new FileUpload();
                HyperLink lnk = new HyperLink();
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                lnk.ID = "link_" + ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                ////是否服务器运行
                //ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                //if (ldt.Rows[i]["control_event"].ToString() != "")
                //{
                //    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                //}
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }
                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                    // ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    ltxt.CssClass = lscontrol_style;
                //}



                lcellContent.Controls.Add(ltxt);
                lcellContent.Controls.Add(lnk);
                #endregion
            }
            else if (ldt.Rows[i]["control_type"].ToString() == "ASPXGRIDLOOKUP")
            {
                #region ASPxGridLookup
                DevExpress.Web.ASPxGridLookup ltxt = new DevExpress.Web.ASPxGridLookup();
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                // ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                //if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                //{
                //    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                //}
                //else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                //{
                //    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                //}
                if (ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.ClientSideEvents.QueryCloseUp = "function(s, e) {" + ldt.Rows[i]["control_event"].ToString() + "}";
                }

                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    ltxt.CssClass = lscontrol_style;
                //}
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值
                    //if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                    //{
                    //    string[] ls_column= ldt.Rows[i]["control_type_text"].ToString().Split(',');
                    //    string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                    //    string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                    //    if (ls1.Length == ls2.Length)
                    //    {
                    //        for (int j = 0; j < ls1.Length; j++)
                    //        {
                    //            ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                    //        }
                    //    }

                    //}
                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    string lspara = "";
                    for (int j = 0; j < ldt_source.Columns.Count; j++)
                    {
                        if (lspara != "")
                        {
                            lspara += " ";
                        }
                        lspara += "{" + j + "}";
                        DevExpress.Web.GridViewDataTextColumn lcom = new DevExpress.Web.GridViewDataTextColumn();
                        lcom.Name = ldt_source.Columns[j].ColumnName;
                        lcom.FieldName = ldt_source.Columns[j].ColumnName; ;
                        ltxt.Columns.Add(lcom);
                    }
                    if (ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        ltxt.KeyFieldName = ldt.Rows[i]["control_type_value"].ToString();
                    }
                    else
                    {
                        ltxt.KeyFieldName = ldt_source.Columns[0].ColumnName;
                    }

                    if (ldt.Rows[i]["control_type_text"].ToString() != "")
                    {
                        ltxt.TextFormatString = ldt.Rows[i]["control_type_text"].ToString();
                    }
                    else
                    {
                        ltxt.TextFormatString = lspara;
                    }
                    ltxt.GridViewProperties.SettingsBehavior.AllowFocusedRow = true;
                    ltxt.GridViewProperties.SettingsBehavior.AllowSelectSingleRowOnly = true;
                    ltxt.GridViewProperties.SettingsBehavior.AllowDragDrop = false;
                    ltxt.GridViewProperties.SettingsBehavior.EnableRowHotTrack = false;
                    ltxt.GridViewProperties.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;
                    ltxt.GridViewProperties.Settings.ShowColumnHeaders = false;


                    ltxt.DataSource = ldt_source;
                    ltxt.DataBind();
                    //for (int j = 0; j < ldt_source.Rows.Count; j++)
                    //{
                    //    ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    //}
                }

                if (ldt_head != null)
                {

                    ltxt.Value = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();


                }

                lcellContent.Controls.Add(ltxt);

                #endregion
            }
            else if (ldt.Rows[i]["control_type"].ToString() == "ASPXCOMBOBOX")
            {
                #region ASPxComboBox
                DevExpress.Web.ASPxComboBox ltxt = new DevExpress.Web.ASPxComboBox();
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();

                if (ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.ClientSideEvents.QueryCloseUp = "function(s, e) {" + ldt.Rows[i]["control_event"].ToString() + "}";
                }

                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.Enabled = false;
                }
                //样式
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.CssClass = lscontrol_style_read;
                }
                else
                {

                    ltxt.CssClass = lscontrol_style_write;
                    ltxt.ControlStyle.BackColor = System.Drawing.Color.FromName("#FDF7D9");
                }
                //赋值
                if (ldt.Rows[i]["control_type_source"].ToString() == "")
                {
                    //直接给定值

                }

                else
                {
                    //通过数据源获取
                    DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                        , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                    string lspara = "";
                    for (int j = 0; j < ldt_source.Columns.Count; j++)
                    {
                        if (lspara != "")
                        {
                            lspara += " ";
                        }
                        lspara += "{" + j + "}";
                        DevExpress.Web.ListBoxColumn lcom = new DevExpress.Web.ListBoxColumn();
                        lcom.Name = ldt_source.Columns[j].ColumnName;
                        lcom.FieldName = ldt_source.Columns[j].ColumnName; ;
                        ltxt.Columns.Add(lcom);
                    }


                    if (ldt.Rows[i]["control_type_text"].ToString() != "")
                    {
                        ltxt.TextFormatString = ldt.Rows[i]["control_type_text"].ToString();
                    }
                    else
                    {
                        ltxt.TextFormatString = lspara;
                    }



                    ltxt.DataSource = ldt_source;
                    ltxt.DataBind();
                    //for (int j = 0; j < ldt_source.Rows.Count; j++)
                    //{
                    //    ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                    //}
                }

                if (ldt_head != null)
                {

                    ltxt.Value = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();


                }

                lcellContent.Controls.Add(ltxt);

                #endregion
            }
            else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDATEEDIT")
            {
                #region "ASPXDATEEDIT"
                DevExpress.Web.ASPxDateEdit ltxt = new DevExpress.Web.ASPxDateEdit();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToopTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //事件
                if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                {
                    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                }
                else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                {
                    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                }
                //宽度
                if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                {
                    ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                }
                //高度
                if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                {
                    ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                }
                //字体
                if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                {
                    ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                }

                //只读
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.ReadOnly = true;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                {
                    ltxt.CssClass = lscontrol_style_read;
                }
                else
                {

                    ltxt.CssClass = lscontrol_style_write;
                }
                if (ldt_head != null)
                {
                    ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                }



                lcellContent.Controls.Add(ltxt);
                #endregion
            }
            //-----------------------------------控件判断结束----------------------------------------
            //判断下个字段是否独立
            if (i + 1 < ldt.Rows.Count)
            {
                if (ldt.Rows[i + 1]["control_onlyrow"].ToString() == "1")
                {
                    int lnspan = i % lncolumn + 1;
                    lcellContent.ColumnSpan = lnspan * 2;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {
                lcellContent.ColumnSpan = lncolumn * 2 - 1;
            }
            lcellHead.Controls.Add(lbl);
            lrow.Cells.Add(lcellHead);
            lrow.Cells.Add(lcellContent);


            if ((i % lncolumn) == 0 || ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {
                ls.Add(lrow);
            }
            ln += 1;
        }


        return ls;
    }
    #endregion

}


