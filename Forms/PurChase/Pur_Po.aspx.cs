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


        //加载表头控件
        //List<TableRow> ls = Pgi.Auto.Control.ShowControl("PUR_PO_Main_Form", "HEAD", 2, "", "", "form-control input-s-sm");
        List<TableRow> ls = ShowControl("PUR_PO_Main_Form", "HEAD", 2, "", "", "lineread", "linewrite");
        for (int i = 0; i < ls.Count; i++)
        {
            this.tblWLLeibie.Rows.Add(ls[i]);
        }

        //List<TableRow> ls_pay = Pgi.Auto.Control.ShowControl("PUR_PO_Main_Form", "HEAD_PAY", 1, "", "", "form-control input-s-sm");
        List<TableRow> ls_pay = ShowControl("PUR_PO_Main_Form", "HEAD_PAY", 1, "", "", "lineread", "linewrite");
        for (int i = 0; i < ls_pay.Count; i++)
        {
            this.tablePay.Rows.Add(ls_pay[i]);
        }



        if (!IsPostBack)
        {

            //获取每步骤栏位状态设定值，方便前端控制其可编辑性

            if (Request.QueryString["flowid"]!=null)
            {
                FlowID = Request.QueryString["flowid"];
            }
           
            if (Request.QueryString["stepid"]!=null)
            {
                 StepID = Request.QueryString["stepid"];
            }
           
          
           
            

            DataTable ldt_detail = null;
            DataTable ldt_pay = null;
            string lssql = "";
            //lssql = "select po.*,pr.wlType,pr.wlSubType,pr.wlh,pr.wlmc,pr.wlms,pr.usefor,pr.RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName";
            //lssql += ",pr.ApointVendorId,pr.unit,pr.historyPrice,pr.targetPrice,pr.deliveryDate,(pr.targetPrice*pr.qty) as targetTotalPrice,pr.attachments";
            //lssql += ",'查看' as attachments_name,qad_pt_mstr.pt_status";
            //lssql += " from PUR_PO_Dtl_Form po";
            //lssql += " left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid";
            //lssql += " left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno";
            //lssql += " inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain";
            lssql = @"select po.*,pr.wlType,pr.wlSubType,pr.wlh,pr.wlmc,pr.wlms,pr.usefor,pr.RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName
                        ,pr.ApointVendorId,pr.unit,pr.notax_historyPrice,pr.notax_targetPrice,pr.deliveryDate,(pr.notax_targetPrice*pr.qty) as notax_targetTotalPrice,pr.attachments
                        ,'查看' as attachments_name,qad_pt_mstr.pt_status
                    from PUR_PO_Dtl_Form po
                        left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid
                        left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno
                        inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain";
           
            if (this.m_sid == "")
            {
                //新增
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    ((TextBox)this.FindControl("ctl00$MainContent$CreateDate")).Text = System.DateTime.Now.ToString();
                    ((TextBox)this.FindControl("ctl00$MainContent$CreateByName")).Text = LogUserModel.UserName;
                    ((TextBox)this.FindControl("ctl00$MainContent$DeptName")).Text = LogUserModel.DepartName;
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$BuyerName")).Value = LogUserModel.UserId + "|" + LogUserModel.UserName;
                     ((TextBox)this.FindControl("ctl00$MainContent$IsToQAD")).Text = "是";
                }

                lssql += " where 1=0";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];

                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt_detail,2);
                ldt_pay = DbHelperSQL.Query("select * from PUR_PO_ContractPay_Form where 1=0").Tables[0];
                
                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PO_PAY", this.gv2, ldt_pay);

            }
            else
            {
                //编辑  
                //表头赋值
                DataTable ldt= DbHelperSQL.Query("select * from PUR_PO_Main_Form where PoNo='"+this.m_sid+"'").Tables[0];
                Pgi.Auto.Control.SetControlValue("PUR_PO_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                if (ldt.Rows[0]["attachments"].ToString()!="")
                {
                    //this.txtfile.NavigateUrl = ldt.Rows[0]["attachments"].ToString();
                    //this.txtfile.Visible = true;

                    this.ip_filelist_db.Value = ldt.Rows[0]["attachments"].ToString();
                    bindtab();
                }


                lssql += " where pono='" + this.m_sid + "'  order by po.rowid";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];


                //特殊处理
                ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Value = "";
                string lsrate = "0";
                if (ldt_detail.Rows.Count>0)
                {
                    //if (ldt_detail.Rows[0]["TaxRate"].ToString() == "16")
                    //{
                    //   lsrate = "17";
                    //}
                    //else if (ldt_detail.Rows[0]["TaxRate"].ToString() == "10")
                    //{
                    //    lsrate = "11";
                    //}
                    //else
                    //{
                    //  lsrate = ldt_detail.Rows[0]["TaxRate"].ToString();
                    //}

                    lsrate = ldt_detail.Rows[0]["TaxRate"].ToString();
                }
             

                ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Value = ldt.Rows[0]["PoVendorId"].ToString() + "|" + ldt.Rows[0]["PoVendorName"].ToString()+"|"+lsrate;

                ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).Value = ldt.Rows[0]["buyerid"].ToString() + "|" + ldt.Rows[0]["buyername"].ToString();

               

                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt_detail,2);
               

            }

            //增加事件
            for (int i = 0; i < ldt_detail.Rows.Count; i++)
            {

                ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).TextChanged += new EventHandler(txt_TextChanged);
                //特殊处理
                DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" + StepID + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + FlowID + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='采购负责人'").Tables[0];

                if (ldt_flow.Rows.Count == 0)
                {

                    ((DropDownList)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Enabled = false;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).ReadOnly = true;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["otherDesc"], "otherDesc")).ReadOnly = true;
                    ((DropDownList)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BorderStyle = BorderStyle.None;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Enabled = false;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["otherDesc"], "otherDesc")).BorderStyle = BorderStyle.None;
                    this.btndel.Visible = false;
                    this.btnadd.Visible = false;

                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Enabled = false;
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).Enabled = false;
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).Enabled = false;
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).Enabled = false;
                    //this.FileUpload1.Visible = false;
                    this.uploadcontrol.Visible = false;
                    this.btnflowSend.Text = "批准";

                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).CssClass = "lineread";
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).CssClass = "lineread";
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).CssClass = "lineread";
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).CssClass = "lineread";

                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).ControlStyle.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).ControlStyle.BackColor = System.Drawing.Color.FromName("#FFFFFF");

                }
                if(Request.QueryString["display"]!=null)
                {
                    ((DropDownList)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).Enabled = false;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).ReadOnly = true;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).ReadOnly = true;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["otherDesc"], "otherDesc")).ReadOnly = true;
                    ((DropDownList)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["currency"], "currency")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).BorderStyle = BorderStyle.None;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PriceDesc"], "PriceDesc")).BorderStyle = BorderStyle.None;
                    ((DevExpress.Web.ASPxDateEdit)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["PlanReceiveDate"], "PlanReceiveDate")).Enabled = false;
                    ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["otherDesc"], "otherDesc")).BorderStyle = BorderStyle.None;
                    this.btndel.Visible = false;
                    this.btnadd.Visible = false;
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Enabled = false;
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).Enabled = false;
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).Enabled = false;
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).Enabled = false;
                    // this.FileUpload1.Visible = false;
                    this.uploadcontrol.Visible = false;

                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).CssClass = "lineread";
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).CssClass = "lineread";
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).CssClass = "lineread";
                    ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).CssClass = "lineread";

                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).ControlStyle.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                    ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).ControlStyle.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                }
            }

        }
        else
        {

            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            this.gv.Columns.Clear();
           
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt,2);
            for (int i = 0; i < ldt.Rows.Count; i++)
            {

                ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["NoTaxPrice"], "NoTaxPrice")).TextChanged += new EventHandler(txt_TextChanged);

            }

            bindtab();
        }


       
        

        ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).AutoPostBack = true;
         ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).TextChanged += new EventHandler(PoDomain_TextChanged);

        //获取供应商信息
        SetPoVendor(((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).Text);

        // ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).AutoPostBack = true;
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).ClientSideEvents.ValueChanged = "function(s, e) {vendorid(s);}";

        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).ClientSideEvents.ValueChanged = "function(s, e) { buyername_potype_change();}";


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

    private void PoDomain_TextChanged(object sender, EventArgs e)
    {
        SetPoVendor(((DropDownList)sender).SelectedValue);
       
    }

    private void SetPoVendor(string lsdomain)
    {
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Columns.Clear();
        //string lssql = @"select distinct ad_addr,ad_name,vd_taxc,ad_addr+'|'+ad_name+'|'+vd_taxc as v  
        //                from qad_ad_mstr 
        //                    inner join qad_vd_mstr on ad_addr=vd_addr and ad_domain=vd_domain 
        //                where ad_type='supplier' and ad_domain='" + lsdomain+"'";
        string lssql = @"select distinct ad_addr,ad_name
                                ,case when isnull(vd_taxc,'')='17' then '16' when isnull(vd_taxc,'')='11' then '10' else '0' end vd_taxc
                                ,ad_addr+'|'+ad_name+'|'+case when isnull(vd_taxc,'')='17' then '16' when isnull(vd_taxc,'')='11' then '10' else '0' end as v  
                        from qad_ad_mstr 
                            inner join qad_vd_mstr on ad_addr=vd_addr and ad_domain=vd_domain 
                        where ad_type='supplier' and ad_domain='" + lsdomain + "'";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];

        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).ValueField = "v";
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Columns.Add("ad_addr","代码",40);
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Columns.Add("ad_name","名称",80);
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Columns.Add("vd_taxc","纳税级别",60);
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).TextFormatString = "{0}|{1}|{2}";
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).DataSource = ldt;
        ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).DataBind();
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
        string lsdomain = ((DropDownList)this.FindControl("ctl00$MainContent$PoDomain")).SelectedValue;
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        //获取表头
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("PUR_PO_Main_Form", "HEAD", this, "ctl00$MainContent${0}");
        string buyer = ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$buyername")).Value.ToString();
        string povendor = ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Value.ToString();

        //表体生成SQL
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        ldt.AcceptChanges();

        //特殊数据处理
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code == "podomain") { ls[i].Value = lsdomain; }

            if (ls[i].Code.ToLower() == "buyername")
            {
                //string[] lsstr = ls[i].Value.ToString().Split('|');
                string[] lsstr = buyer.Split('|');
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
                //string[] lsstr = ls[i].Value.ToString().Split('|');
                string[] lsstr = povendor.Split('|');
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
        string formno_main = "";
        for (int j = 0; j < ls.Count; j++)
        {
            if (ls[j].Code.ToLower() == "pono") { formno_main = ls[j].Value; }
        }

        decimal lntotalpay = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["pono"] = formno_main;
            if (ldt.Rows[i]["TotalPrice"].ToString() != "")
            {
                lntotalpay += Convert.ToDecimal(ldt.Rows[i]["TotalPrice"].ToString());
            }
        }

        //从明细表中合计采购总金额
        Pgi.Auto.Common lcTotalPay = new Pgi.Auto.Common();
        lcTotalPay.Code = "totalpay";
        lcTotalPay.Key = "";
        lcTotalPay.Value = lntotalpay.ToString();// ((LoginUser)Session["LogUser"]).UserId;
        ls.Add(lcTotalPay);

        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PUR_PO_Main_Form"));


        //明细数据自动生成SQL，并增入SUM
        List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PUR_PO_Dtl_Form", "id"
            , "flag,wlType,wlSubType,wlh,wlmc,wlms,notax_targetPrice,notax_targetTotalPrice,RecmdVendorName,notax_historyPrice,deliveryDate,RecmdVendorId,attachments,attachments_name,pt_status");
        for (int i = 0; i < ls1.Count; i++)
        {
            ls_sum.Add(ls1[i]);
        }

        //明细删除增加到list中
        if (Session["del"]!=null)
        {
            DataTable ldt_del = (DataTable)Session["del"];
            for (int i = 0; i < ldt_del.Rows.Count; i++)
            {
                Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
                ls_del.Sql = "delete from PUR_PO_Dtl_Form where id="+ldt_del.Rows[i]["id"].ToString()+"";
                ls_del.Sql += ";update PUR_PR_Dtl_Form set  Status='0' where prno='" + ldt_del.Rows[i]["prno"].ToString()+"' and rowid='"+ldt_del.Rows[i]["prrowid"].ToString()+"'";
                ls_sum.Add(ls_del);
            }
            Session["del"] = null;
        }

        //处理PR明细表状态
        if (ldt.Rows.Count>0)
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

    protected void Button1_Click(object sender, EventArgs e)
    {

        bool bflag = this.SaveData();
        if (bflag == true)
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存成功!");

        }
        else
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存失败!");
        }
    }

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        string[] lsrate = ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Text.Split('|');
        string lsrate1 = "0";
        if (lsrate.Length == 3)
        {
            lsrate1 = lsrate[2];
            //if (lsrate[2] == "17")
            //{
            //    lsrate1 = "16";
            //}
            //else if (lsrate[2] == "11")
            //{
            //    lsrate1 = "10";
            //}
            //else
            //{
            //    lsrate1 = lsrate[2];
            //}
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
        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt, 2);

    }


    protected void PoVendorName_TextChanged(object sender, EventArgs e) {

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
                ldr["wlType"] = ldt1.Rows[i]["wlType"].ToString();
                ldr["wlSubType"] = ldt1.Rows[i]["wlSubType"].ToString();
                ldr["wlh"] = ldt1.Rows[i]["wlh"].ToString();
                ldr["wlmc"] = ldt1.Rows[i]["wlmc"].ToString();
                ldr["wlms"] = ldt1.Rows[i]["wlms"].ToString();
                ldr["currency"] = ldt1.Rows[i]["currency"].ToString();
                ldr["notax_targetPrice"] = ldt1.Rows[i]["notax_targetPrice"].ToString();
                ldr["PurQty"] = ldt1.Rows[i]["qty"].ToString();
                ldr["RecmdVendorName"] = ldt1.Rows[i]["RecmdVendorName"].ToString();
                ldr["NoTaxPrice"] = ldt1.Rows[i]["notax_targetPrice"].ToString();
                ldr["pt_status"] = ldt1.Rows[i]["pt_status"].ToString();
                string[] lsrate = ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Text.Split('|');
                if (lsrate.Length == 3)
                {
                    //if (lsrate[2].ToString().Trim() == "17")
                    //{
                    //    ldr["taxrate"] = "16";
                    //}
                    //else if (lsrate[2].ToString().Trim() == "11")
                    //{
                    //    ldr["taxrate"] = "10";
                    //}
                    //else
                    //{
                    //    ldr["taxrate"] = lsrate[2].ToString().Trim();
                    //}

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
                ldr["notax_historyPrice"] = ldt1.Rows[i]["notax_historyPrice"].ToString();
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
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt, 2);
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
                lssql = @"select po.*,pr.wlType,pr.wlSubType,pr.wlh,pr.wlmc,pr.wlms,pr.usefor,pr.RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName
                            ,pr.ApointVendorId,pr.unit,pr.notax_historyPrice,pr.notax_targetPrice,pr.deliveryDate,(pr.notax_targetPrice*pr.qty) as notax_targetTotalPrice,pr.attachments
                            ,'查看' as attachments_name,qad_pt_mstr.pt_status
                        from PUR_PO_Dtl_Form po
                            left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid
                            left join PUR_PR_Main_Form pr_main on pr.prno=pr_main.prno
                            inner join qad_pt_mstr on pr.wlh=qad_pt_mstr.pt_part and pr_main.domain=qad_pt_mstr.pt_domain
                        where pono=(select pono from PUR_PO_Main_Form where pono='{0}' and PoType='{1}' and (buyerid+'|'+buyername)='{2}')  
                        order by po.rowid";
                lssql = string.Format(lssql, ((TextBox)this.FindControl("ctl00$MainContent$PoNo")).Text
                    , ((DropDownList)this.FindControl("ctl00$MainContent$PoType")).SelectedValue
                    , param);
                ldt = DbHelperSQL.Query(lssql).Tables[0];

                this.gv.Columns.Clear();
                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt, 2);
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



    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count-1; i >=0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString()=="1" && ldt.Rows[i]["id"].ToString()=="")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        if (ldt_del.Rows.Count>0)
        {
            if (Session["del"] !=null)
            {
                for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
                {
                    ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
                }
               
            }
            Session["del"] = ldt_del;
        }
        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL_New", this.gv, ldt,2);
        //Pgi.Auto.Public.MsgBox(this.Page,"alert","删除成功!");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      
        Pgi.Auto.Public.MsgBox(this.Page, "alert", ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId")).Text);
    }

    protected void gv_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
        {
            return;
        }
        int lncindex = 0; int prnoindex = 0;int RecmdVendorNameindex = 0;
        for (int i = 0; i < this.gv.DataColumns.Count; i++)
        {
            if (this.gv.DataColumns[i].FieldName== "notax_TotalPrice")
            {
                lncindex = i;
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
        decimal lmbzj = Convert.ToDecimal(e.GetValue("notax_targetTotalPrice"));
        decimal lzj = Convert.ToDecimal(e.GetValue("notax_TotalPrice").ToString() == "" ? "0" : e.GetValue("notax_TotalPrice").ToString());
        decimal ln = ((lzj - lmbzj) / lmbzj) *100;
        if (ln>0 && ln<=20)
        {
            e.Row.Cells[lncindex+1].Style.Add("background-color", "yellow");

        }
        else if (ln>20)
        {
            e.Row.Cells[lncindex + 1].Style.Add("color", "white");
            e.Row.Cells[lncindex+1].Style.Add("background-color", "red");
        }

        //add by heguiqin20180515 请购单号链接
        string PRNo = Convert.ToString(e.GetValue("PRNo"));
        //e.Row.Cells[prnoindex + 1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=62676129-f059-4c92-bd5c-86897f5b0d5&instanceid="
        //    + e.GetValue("PRNo") + "&mode=view' target='_blank'>" + PRNo.ToString() + "</a>";

        //mode=view 修改为 display=1
        e.Row.Cells[prnoindex + 1].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=62676129-f059-4c92-bd5c-86897f5b0d5&instanceid="
            + e.GetValue("PRNo") + "&display=1' target='_blank'>" + PRNo.ToString() + "</a>";

        //add by heguiqin20180515 采购供应商跟推荐供应商不一致，背景色黄色
        DevExpress.Web.ASPxComboBox acb_pvi = (DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$PoVendorId");
        string PoVendor = "";
        if (acb_pvi != null)
        {
            string[] pvarr = acb_pvi.Text.Split('|');
            if (pvarr.Length >= 2) { PoVendor = pvarr[0] + "_" + pvarr[1]; }
        }

        if (e.GetValue("RecmdVendorName").ToString() != PoVendor)
        {
            e.Row.Cells[RecmdVendorNameindex + 1].Style.Add("background-color", "yellow");
        }

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


