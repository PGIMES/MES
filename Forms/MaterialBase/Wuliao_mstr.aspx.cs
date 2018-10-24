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
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;

public partial class Forms_MaterialBase_Wuliao_mstr : System.Web.UI.Page
{
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";
    public string m_sid = "";
    string state = "";
    public string FlowID = "";
    public string StepID = "";
    string bz = "";
    string gc = "";
    public static string fxsite="";
    int id = 0;
    BaseFun fun = new BaseFun();

    protected void Page_Load(object sender, EventArgs e)
    {
        

        Page.MaintainScrollPositionOnPostBack = true;
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);//
        Session["LogUser"] = LogUserModel;
        Session["UserAD"] = LogUserModel.ADAccount;
        Session["UserId"] = LogUserModel.UserId;
        ViewState["TB"] = null;
        ViewState["UidRole"] = "";
        ViewState["line"] = "";
        Session["lv"] = "";
        if (ViewState["instanceid"] == null) { ViewState["instanceid"] = ""; }

        if (this.gv_d.IsCallback == true )
        {
            ScriptManager.RegisterStartupScript(this, e.GetType(), "", "SetGridbtn()", true);
        }


        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"];
        }

        string id = Request["formid"]; // get instanceid
        if (Request.QueryString["flowid"] != null)
        {
            this.FlowID = Request["flowid"];
        }
        if (Request.QueryString["stepid"] != null)
        {
            this.StepID = Request["stepid"];
        }
        if (Request.QueryString["state"] != null)
        {
            this.state = Request.QueryString["state"];
        }
        if (Request.QueryString["bz"] != null)
        {
            this.bz = Request.QueryString["bz"];
        }
        if (Request.QueryString["gc"] != null)
        {
            this.gc = Request.QueryString["gc"];
        }
        //登录人员权限
        if (Request.QueryString["stepid"] != null )
        {
            string sqlRole = @"select * from  RoadFlowWebForm.[dbo].[WorkFlowTask] a join(select InstanceID,flowid,max(sort)sort from  RoadFlowWebForm.[dbo].[WorkFlowTask] 
         where FlowID='d9cb9476-13f9-48ec-a87e-5b84ca0790b0' AND InstanceID='" + Request.QueryString["instanceid"] + "'  group by FlowID,InstanceID )b on a.FlowID=b.FlowID  AND a.InstanceID=b.InstanceID  and a.Sort=b.sort where stepid='" + Request.QueryString["stepid"] + "' ";

            var loguser = DbHelperSQL.Query(sqlRole).Tables[0];
            if (loguser.Rows.Count > 0)
            {
                if (loguser.Rows[0]["StepName"].ToString().Contains("计划") == true)
                { ViewState["UidRole"] = "jihua"; }
                else if (loguser.Rows[0]["StepName"].ToString().Contains("包装") == true)
                { ViewState["UidRole"] = "bz"; }
                else if (loguser.Rows[0]["StepName"].ToString().Contains("采购") == true)
                { ViewState["UidRole"] = "caigou"; Session["lv"] = "caigou"; }
                else if (loguser.Rows[0]["StepName"].ToString().Contains("产品工程师") == true)
                { ViewState["UidRole"] = "product"; Session["lv"] = "product"; }
                else if (loguser.Rows[0]["StepName"].ToString().Contains("会计") == true)
                { ViewState["UidRole"] = "Fin"; Session["lv"] = "Fin"; }
                role.Text = (string)ViewState["UidRole"];
            }
        }


        #region "IsPostBack"
        if (!IsPostBack)
        {
   
            if (LogUserModel != null)
            {
                //当前登陆人员
                //新增时表头基本信息
                CreateById.Text = LogUserModel.UserId;
                CreateByName.Text = LogUserModel.UserName;
                CreateByDept.Text = LogUserModel.DepartName;
                CreateDate.Text = System.DateTime.Now.ToString();
                string lssql = "";
//                lssql = @"select null id, formno,buyer_planner,isfx,a.site,fxcode,dhff,ddsl,dhperiod,pt_pm_code,make_days,domain,
//                                 purchase_days,quantity_min,quantity_max,ddbs,aqtj_wuliu ,ROW_NUMBER() OVER(ORDER BY Updatetime) numid from PGI_PartDtl_DATA_Form a";
                lssql = "if exists(select *from PGI_PartDtl_DATA_Form where site in ('100','200') and formno='"+m_sid+"')";
                lssql+="  select null id, formno,buyer_planner,isfx,a.site,fxcode,dhff,ddsl,dhperiod,pt_pm_code,make_days,domain,";
                lssql += "  purchase_days,quantity_min,quantity_max,ddbs,aqtj_wuliu ,ROW_NUMBER() OVER(ORDER BY Updatetime) numid from PGI_PartDtl_DATA_Form a where formno='" + m_sid + "'";
                 lssql+="   else select null id, formno,buyer_planner,'N'as isfx,a.site,'' as fxcode,dhff,0 AS ddsl, 7 AS dhperiod,pt_pm_code,0 AS make_days,comp as domain,";
                 lssql += " 0 AS purchase_days,0 as quantity_min,0 as quantity_max,0 as ddbs,0 AS aqtj_wuliu ,ROW_NUMBER() OVER(ORDER BY ID) numid from PGI_Partmstr_DATA_Form a where  formno='" + m_sid + "'";
                    applytype.Text = type.SelectedValue;

                    if (state == "edit" )//编辑
                    {

                        lssql = @"select null id, formno,buyer_planner,isfx,a.site,fxcode,dhff,ddsl,dhperiod,pt_pm_code,make_days,domain,
                                 purchase_days,quantity_min,quantity_max,ddbs,aqtj_wuliu ,ROW_NUMBER() OVER(ORDER BY id) numid from PGI_PartDtl_DATA a where formno='" + id + "' order by pt_pm_code desc";

                        string re_sql = @"select top 1 a.InstanceID,b.createbyid,b.createbyname ,part_no
                                                            from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='d9cb9476-13f9-48ec-a87e-5b84ca0790b0' and status in(0,1))  a
                                                                inner join PGI_PartMstr_DATA_Form b on a.InstanceID=b.formno 
                                                             where b.formno='"+id+"'";
                        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

                        if (re_dt.Rows.Count > 0)
                        {
                            Pgi.Auto.Public.MsgBox(this, "alert",  "物料号正在申请中，不能修改(物料号:" + re_dt.Rows[0]["part_no"].ToString() + ")!");
                        }

                        type.Items.Remove("新增"); 
                        this.type.Items.Insert(0, new ListItem("", ""));//edit时默认新增一空白列
                        this.type.SelectedIndex = 0;
                        //lssql += " where formno='" + id + "'";

                    }
                    else if (m_sid != "" && state != "edit")
                    {
                  
                        DataTable dttype = DbHelperSQL.Query("select * from PGI_PartMstr_DATA_Form where formno='" + this.m_sid + "'").Tables[0];
                        if (dttype != null && dttype.Rows.Count > 0)
                        {
                             //Pgi.Auto.Public.MsgBox(this, "alert", "测试ok");
                            var item = type.Items.FindByText(dttype.Rows[0]["applytype"].ToString());
                            if (item != null)
                            {
                                type.ClearSelection();
                                type.Enabled = false;
                                item.Selected = true;
                            }
                        }
                       // lssql += " where a.formno='" + this.m_sid + "'";
                    }
                    else
                    {
                     //   lssql += " where 1=0";
                        type.Enabled = false;
                        type.SelectedIndex = 0;
                    }


                    var tblopinion = DbHelperSQL.Query(" select value as cateid,text catevalue from pt_base_code where type='pt_opinion' ").Tables[0];
                    fun.initCheckBoxList(ddlopinion, tblopinion, "cateid", "catevalue");
               
                DataTable ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
                this.gv_d.DataSource = ldt_detail;
                ViewState["TB"] = ldt_detail;
                ViewState["comp"] =ldt_detail.Rows.Count>0? ldt_detail.Rows[0]["domain"]:"";
                this.gv_d.DataBind();
                setGridIsRead(ldt_detail);
                GetGrid(ldt_detail);

                }
        }

      
        #endregion
        loadControl();
        var jzunit = (DropDownList)this.FindControl("jzunit");
        var fyunit = (DropDownList)this.FindControl("fyunit");
        jzunit.SelectedItem.Text = "KG";
        fyunit.SelectedItem.Text = "KG";

        DataTable dtMst = new DataTable();
        if (m_sid != "" && state!="edit")
        {
            dtMst = DbHelperSQL.Query("select * from PGI_PartMstr_DATA_Form where formno='" + this.m_sid + "'").Tables[0];
        }
        var domain = "";
        if (dtMst != null && dtMst.Rows.Count > 0)
        {
            CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
            CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
            CreateByDept.Text = dtMst.Rows[0]["CreateByDept"].ToString();
            CreateDate.Text = dtMst.Rows[0]["CreateDate"].ToString();
            comp.Text = dtMst.Rows[0]["comp"].ToString();
            applytype.Text = dtMst.Rows[0]["applytype"].ToString();
            if (comp.Text == "200")
            { Checkbox1.Checked = true; }
            else if (comp.Text == "100")
            { Checkbox2.Checked = true; }
            ViewState["comp"] = comp.Text;
            setCheckBoxListSelectValue(ddlopinion, dtMst.Rows[0]["opinion"].ToString(), ';', true);
            string line = dtMst.Rows[0]["line"].ToString();
           // ViewState["line"] = line.Substring(0, 1);
            string pt_pm_code = dtMst.Rows[0]["pt_pm_code"].ToString();
            var tbltype = DbHelperSQL.Query("  select distinct   pl_prod_line as value,  pl_desc+'('+pl_prod_line+')' as text from qad.dbo.qad_pl_mstr  where (pl_desc like '%" + dtMst.Rows[0]["dl"].ToString() + "%' or  pl_prod_line='1090')").Tables[0];
            ((DropDownList)this.FindControl("line")).DataSource = tbltype;
            ((DropDownList)this.FindControl("line")).DataTextField = "text";
            ((DropDownList)this.FindControl("line")).DataValueField = "value";
            ((DropDownList)this.FindControl("line")).DataBind();

            var group = DbHelperSQL.Query("  select value as cateid,value as text from pt_base_code where type='pt_pm_code' and value='" + pt_pm_code + "'").Tables[0];
            ((DropDownList)this.FindControl("pt_pm_code")).DataSource = group;
            ((DropDownList)this.FindControl("pt_pm_code")).DataTextField = "text";
            ((DropDownList)this.FindControl("pt_pm_code")).DataValueField = "cateid";
            ((DropDownList)this.FindControl("pt_pm_code")).DataBind();

            string buyerid = dtMst.Rows[0]["buyer_planner"].ToString();
            var buyer = DbHelperSQL.Query(" select value as cateid,val as text from pt_base_code where type='pt_buyer' and value='" + buyerid + "'").Tables[0];
            ((DropDownList)this.FindControl("buyer_planner")).DataSource = buyer;
            ((DropDownList)this.FindControl("buyer_planner")).DataTextField = "text";
            ((DropDownList)this.FindControl("buyer_planner")).DataValueField = "cateid";
            ((DropDownList)this.FindControl("buyer_planner")).DataBind();

            if (((DropDownList)this.FindControl("line")) != null)
            {
                ((DropDownList)this.FindControl("line")).SelectedValue = line;
            }
            var jzdw= (DropDownList)this.FindControl("jzunit");
            var fydw= (DropDownList)this.FindControl("fyunit");
            if (string.IsNullOrEmpty(dtMst.Rows[0]["jzunit"].ToString()))
            {
                jzdw.SelectedItem.Text = "KG";
            }
            if (string.IsNullOrEmpty(dtMst.Rows[0]["fyunit"].ToString()))
            {
                fydw.SelectedItem.Text = "KG";
            }
            
            
        }
        //else
        //{
        //    btnAdd.Style.Add("display", "none");
        //    btndel.Style.Add("display", "none");
        //}
   

        
     
        //发起【修改申请】初始化值给画面
        var wlh = Request["wlh"];
        var formid = Request["formid"];
        domain = Request["domain"];
        DataTable dtMstOld = new DataTable();
        if (formid != null)
        {
            dtMstOld = DbHelperSQL.Query(" select * from PGI_PartMstr_DATA where formno='" + formid + "' and  wlh='" + wlh.ToString() + "' and comp='" + domain.ToString() + "' and gc_version='" + gc.ToString() + "' and bz_version='" + bz + "'").Tables[0];

        }
        if (dtMstOld != null && dtMstOld.Rows.Count > 0)
        {
          //  ((TextBox)this.FindControl("wlh")).Attributes.Remove("ondblclick");
            comp.Text = dtMstOld.Rows[0]["comp"].ToString();
            ViewState["comp"] = comp.Text;
            if (comp.Text == "200")
            { Checkbox1.Checked = true; }
            else if (comp.Text == "100")
            { Checkbox2.Checked = true; }
            setCheckBoxListSelectValue(ddlopinion, dtMstOld.Rows[0]["opinion"].ToString(), ';', true);
            if (!string.IsNullOrEmpty(dtMstOld.Rows[0]["site"].ToString()))
            {
                var site = dtMstOld.Rows[0]["site"].ToString();
                var sql = string.Format("  select  text,value from  pt_base_code  where type='fxcode'   AND value='{0}'  ", site);
                formsite.Text = DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString() + "/" + DbHelperSQL.Query(sql).Tables[0].Rows[0][1].ToString();
            }
            string pt_pm_code = dtMstOld.Rows[0]["pt_pm_code"].ToString();
            string line = dtMstOld.Rows[0]["line"].ToString();
            var tbltype = DbHelperSQL.Query("  select distinct   pl_prod_line as value,  pl_desc+'('+pl_prod_line+')'  as text from qad.dbo.qad_pl_mstr  where (pl_desc like '%" + dtMstOld.Rows[0]["dl"].ToString() + "%' or  pl_prod_line='1090')").Tables[0];
            ((DropDownList)this.FindControl("line")).DataSource = tbltype;
            ((DropDownList)this.FindControl("line")).DataTextField = "text";
            ((DropDownList)this.FindControl("line")).DataValueField = "value";
            ((DropDownList)this.FindControl("line")).DataBind();

            var group = DbHelperSQL.Query("  select value as cateid,value as text from pt_base_code where type='pt_pm_code' and value='" + pt_pm_code + "'").Tables[0];
            ((DropDownList)this.FindControl("pt_pm_code")).DataSource = group;
            ((DropDownList)this.FindControl("pt_pm_code")).DataTextField = "text";
            ((DropDownList)this.FindControl("pt_pm_code")).DataValueField = "cateid";
            ((DropDownList)this.FindControl("pt_pm_code")).DataBind();

            string buyerid = dtMstOld.Rows[0]["buyer_planner"].ToString();
            var buyer = DbHelperSQL.Query(" select value as cateid,val as text from pt_base_code where type='pt_buyer' and value='" + buyerid + "'").Tables[0];
            ((DropDownList)this.FindControl("buyer_planner")).DataSource = buyer;
            ((DropDownList)this.FindControl("buyer_planner")).DataTextField = "text";
            ((DropDownList)this.FindControl("buyer_planner")).DataValueField = "cateid";
            ((DropDownList)this.FindControl("buyer_planner")).DataBind();
            if (((DropDownList)this.FindControl("line")) != null)
            {
                ((DropDownList)this.FindControl("line")).SelectedValue = line;
            }
            var jzdw = (DropDownList)this.FindControl("jzunit");
            var fydw= (DropDownList)this.FindControl("fyunit");
            if (string.IsNullOrEmpty(dtMstOld.Rows[0]["jzunit"].ToString()))
            {
                jzdw.SelectedItem.Text = "KG";
            }
            if (string.IsNullOrEmpty(dtMstOld.Rows[0]["fyunit"].ToString()))
            {
                fydw.SelectedItem.Text = "KG";
            }

            DataColumn createbyid = new DataColumn()
            {
                DefaultValue = CreateById.Text,
                ColumnName = "createbyid"
            };
            DataColumn createbyname = new DataColumn()
            {
                DefaultValue = CreateByName.Text,
                ColumnName = "createbyname"
            };
            DataColumn createdate = new DataColumn()
            {
                DefaultValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ColumnName = "createdate"
            };
            DataColumn createbydept = new DataColumn()
            {
                DefaultValue = CreateByDept.Text,
                ColumnName = "createbydept"
            };
            //DataColumn formno = new DataColumn()
            //{
            //    DefaultValue = Formno.Text,
            //    ColumnName = "formno"
            //};
            dtMstOld.Columns.Add(createbyid);
            dtMstOld.Columns.Add(createbyname);
            dtMstOld.Columns.Add(createdate);
            dtMstOld.Columns.Add(createbydept);
          //  dtMstOld.Columns.Add(formno);
        
            Pgi.Auto.Control.SetControlValue("main", "", this, dtMstOld.Rows[0]);
     
        }
        //--== 第 步:装载控件========================================================================================================
   
        if (dtMst != null && dtMst.Rows.Count > 0)
        {
            Checkbox1.Disabled = true;
            Checkbox2.Disabled = true;
            //line.CssClass = "form-control disabled";
            //line.Enabled = false;  
            Pgi.Auto.Control.SetControlValue("main", "", this, dtMst.Rows[0]);
            domain = dtMst.Rows[0]["comp"].ToString();
            formstate.Text = dtMst.Rows[0]["formstate"] == null ? "" : dtMst.Rows[0]["formstate"].ToString();
          
            if( !string.IsNullOrEmpty( dtMst.Rows[0]["site"].ToString()))
            {
                var site = dtMst.Rows[0]["site"].ToString();
           var sql =  string.Format("  select  text,value from  pt_base_code  where type='fxcode'   AND value='{0}'  ", site);
           formsite.Text = DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString() + "/" + DbHelperSQL.Query(sql).Tables[0].Rows[0][1].ToString();
            }

            id = dtMst.Rows[0]["id"].ToString();

        }

        //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
        string FlowID = Request.QueryString["flowid"];
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);



        if (Request.QueryString["display"] != null)
        {
            ((DropDownList)this.FindControl("line")).Enabled = false;
            ((TextBox)this.FindControl("part_no")).CssClass = "lineread";
            ((TextBox)this.FindControl("wlh")).CssClass = "lineread";
            ((TextBox)this.FindControl("wlmc")).CssClass = "lineread";
            ((TextBox)this.FindControl("ms")).CssClass = "lineread";
            ((TextBox)this.FindControl("ms")).CssClass = "lineread";
            ((TextBox)this.FindControl("picversion")).CssClass = "lineread";
            ((TextBox)this.FindControl("remark")).CssClass = "lineread";
            ((TextBox)this.FindControl("jzweight")).CssClass = "lineread";
            ((TextBox)this.FindControl("lilun_jzweight")).CssClass = "lineread";
            ((DropDownList)this.FindControl("cpgroup")).CssClass = "lineread";
            ((DropDownList)this.FindControl("line")).CssClass = "lineread";
            ((DropDownList)this.FindControl("pt_status")).CssClass = "lineread";
            ((DropDownList)this.FindControl("gc_version")).CssClass = "lineread";
            ((DropDownList)this.FindControl("bz_version")).CssClass = "lineread";
            ((DropDownList)this.FindControl("jzunit")).CssClass = "lineread";

            ((DropDownList)this.FindControl("site")).Enabled = false;
            ((DropDownList)this.FindControl("dhff")).Enabled = false;
            ((DropDownList)this.FindControl("pt_pm_code")).Enabled = false;
            ((DropDownList)this.FindControl("gc_version")).Enabled = false;
            ((DropDownList)this.FindControl("bz_version")).Enabled = false;
            ((DropDownList)this.FindControl("parthz")).Enabled = false;
            ((DropDownList)this.FindControl("jzunit")).Enabled = false;
            ((DropDownList)this.FindControl("cpgroup")).Enabled = false;
            ((DropDownList)this.FindControl("pt_status")).Enabled = false;


            Checkbox1.Disabled = true;
            Checkbox2.Disabled = true;
            type.Enabled = false;
            gv_d.Columns[gv_d.VisibleColumns.Count - 1].Visible = false;

            if ((string)ViewState["UidRole"] != "jihua")
            {
                ((DropDownList)this.FindControl("buyer_planner")).Enabled = false;
                DataTable dt = (DataTable)ViewState["TB"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["buyer_planner"], "buyer_planner")).Enabled = false;
                    ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["site"], "site")).Enabled = false;
                    ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["dhff"], "dhff")).Enabled = false;
             
                }
            }
            if ((string)ViewState["UidRole"] != "Fin")
            {
                ((DropDownList)this.FindControl("status")).Enabled = false;
            }
        }
        
        
    }

    public void setGridIsRead(DataTable ldt_detail)
    {
        
        string no = "";
        if (!string.IsNullOrEmpty(Request["Instanceid"]) ) { no = Request["Instanceid"].ToString(); }
        if (!string.IsNullOrEmpty(Request["formid"]) ) { no = Request["formid"].ToString(); }
        string sql = "select top 1 * from PGI_PartMstr_DATA_Form where formno='" + no + "'";
        DataTable dtMst = DbHelperSQL.Query(sql).Tables[0];
        ViewState["line"] =dtMst.Rows.Count>0? dtMst.Rows[0]["line"].ToString().Substring(0, 1):"";
 
        if (ldt_detail.Rows.Count == 0  && (string)ViewState["line"]=="1")
        {
            div_FX.Style.Add("display", "none");
        }
        else if ((string)ViewState["UidRole"] == "jihua" )
        {
            div_FX.Style.Add("display", "display");
            if (ldt_detail.Rows.Count > 0) { btnAdd.Style.Add("display", "none"); }
        }
        else if (Request["state"] != "edit")
        {
            //gv_d.Columns[gv_d.VisibleColumns.Count - 1].Visible = false;
            div_FX.Style.Add("display", "display");
            btnAdd.Style.Add("display", "none");
            btndel.Style.Add("display", "none");
        }
        else
        {
            if (ldt_detail.Rows.Count > 0)
            {
                div_FX.Style.Add("display", "display");
                btnAdd.Style.Add("display", "none");
                btndel.Style.Add("display", "display");
                for (int i = 0; i < ldt_detail.Rows.Count; i++)
                {
                    ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddbs"], "ddbs")).ReadOnly = true;
                    ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddbs"], "ddbs")).BackColor = System.Drawing.Color.LightGray;
                }
            }
        }


        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {

            if ((string)ViewState["UidRole"] == "jihua")
            {
            
                ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["buyer_planner"], "buyer_planner")).Enabled = true;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddbs"], "ddbs")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddbs"], "ddbs")).BackColor = System.Drawing.Color.LightGray;

            }
            else if ( (string)ViewState["UidRole"] == "bz")
            {
                gv_d.Columns[0].Visible = false;
                
                ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["buyer_planner"], "buyer_planner")).Enabled = false;
                ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["site"], "site")).Enabled = false;
                ((ASPxComboBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["dhff"], "dhff")).Enabled = false;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["fxcode"], "fxcode")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["fxcode"], "fxcode")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["dhperiod"], "dhperiod")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["dhperiod"], "dhperiod")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["aqtj_wuliu"], "aqtj_wuliu")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["aqtj_wuliu"], "aqtj_wuliu")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["make_days"], "make_days")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["make_days"], "make_days")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["quantity_min"], "quantity_min")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["quantity_min"], "quantity_min")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["quantity_max"], "quantity_max")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["quantity_max"], "quantity_max")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddsl"], "ddsl")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddsl"], "ddsl")).BackColor = System.Drawing.Color.LightGray;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["ddbs"], "ddbs")).ReadOnly = false;

                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["purchase_days"], "purchase_days")).ReadOnly = true;
                ((ASPxTextBox)this.gv_d.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv_d.Columns["purchase_days"], "purchase_days")).BackColor = System.Drawing.Color.LightGray;
            }
            //else
            //{
            //    gv_d.Columns[gv_d.VisibleColumns.Count - 1].Visible = false;
            //    gv_d.Columns[0].Visible = false;
            //    btnAdd.Visible = false;
            //    btndel.Visible = false;
            //}
       
        }
    }

    //获取分销网代码
    [System.Web.Services.WebMethod()]
    public static string GetFxCode(string P1,string P2)
    {
        string result = "";
        var sql = "";
        if (P2 == "Y")
        {
            sql = string.Format("  select text,value from  pt_base_code  where type='fxcode'   AND domain='{0}'  ", P1);
        }
        else
        {
            sql = string.Format("  select text,value from  pt_base_code  where type='pt_site'   AND value='{0}'  ", P1);
        }
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }

  [System.Web.Services.WebMethod()]
    public static string CheckData(string xmh, string cpleibie, string gc, string bz, string comp, string formno,
      string part_no, string state, string status, string aprover, string createuid, string pt_pm_code)
    {
        string check_flag = "";
      string SQL="";
      if(pt_pm_code=="P")
      {
          SQL = @"Select LEFT(bz_user,5)bz_user,left(caigou,5)caigou,left(project_user,5)project_user  from form3_Sale_Product_MainTable 
          where pgino='" + xmh + "'  AND   LEFT(bz_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES ) AND   LEFT(caigou,5) IN (SELECT workcode FROM V_HRM_EMP_MES )  AND   LEFT(project_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES )";
      }
      else
      {
          SQL = @"Select LEFT(bz_user,5)bz_user,left(product_user,5) product_user,left(project_user,5)project_user  from form3_Sale_Product_MainTable 
          where pgino='" + xmh + "'  AND   LEFT(bz_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES ) AND   LEFT(product_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES )  AND   LEFT(project_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES )";
      }
        DataTable dt = DbHelperSQL.Query(SQL).Tables[0];
      if (dt.Rows.Count==0 && cpleibie == "1" && status!="数据管理员修改")
        {
            check_flag = "产品清单中(" + xmh + ")采购工程师、包装工程师、或项目工程师不存在或离职，不能提交!";
            
        }

      if ((dt.Rows.Count == 0) && cpleibie != "1" && status != "数据管理员修改")
        {
            check_flag = "产品清单中(" + xmh + ")包装工程师、产品工程师、或项目工程师不存在或离职，不能提交!";
        }
        DataTable dt_manager = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select workcode from V_HRM_EMP_MES where workcode='" + createuid + "')").Tables[0];
        if (dt_manager.Rows[0][0].ToString() == "")
        {
            check_flag = "工程师对应部门主管不存在，不能提交!";
           
        }
        string wlh = part_no;
        string re_sql = @"select top 1 a.InstanceID,b.createbyid,b.createbyname 
                                                            from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='d9cb9476-13f9-48ec-a87e-5b84ca0790b0' and status in(0,1))  a
                                                                inner join PGI_PartMstr_DATA_Form b on a.InstanceID=b.formno 
                                                             where b.part_no='" + wlh + "' and b.comp='" + comp + "'";
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

      
       
        string sql1 = @"select * from PGI_PartMstr_DATA
                                                             where part_no='"+wlh+"' and comp='" + comp + "'";
        DataTable dt1 = DbHelperSQL.Query(sql1).Tables[0];

        if (dt1.Rows.Count > 0 && state != "edit" && status == "新增" && aprover == "" && formno!=dt1.Rows[0]["formno"].ToString())
        {
            check_flag = "物料号: " + wlh+" 已存在,请至查询页面编辑后送签！";
        }
        if (re_dt.Rows.Count > 0 && formno == "")
        {
            check_flag = "物料号: " + wlh + ";正在申请中,单号:" + re_dt.Rows[0]["InstanceID"] + "不能提交";
        }
        string result = "[{\"check_flag\":\"" + check_flag + "\"}]";
        return result;

    }

  //获取计划员
  [System.Web.Services.WebMethod()]
  public static string Getbuyer(string P1, string P2)
  {
      string result = "";
      var sql = "";

      sql = string.Format("     select ''value,''text union all select value as value,val as text from pt_base_code where type='pt_buyer'  and text='"+P2+"'   and domain like '" + P1 + "%'  ");
   //   sql = string.Format("    select value as value,val as text from pt_base_code where type='pt_buyer' and substring(val,2,1)='0' and text='" + P2 + "'   and domain like '" + P1 + "%'  ");

      var value = DbHelperSQL.Query(sql).Tables[0];
      if (value.Rows.Count > 0)
      { result = value.ToJsonString(); }
      return result;
  }

    //获取产品大类
    [System.Web.Services.WebMethod()]
    public static string GetDL(string P1,string P2)
    {
        string result = "";
        var sql = "";

        sql = string.Format("  select distinct   pl_desc+'('+pl_prod_line+')'  as text,pl_prod_line as value from qad.dbo.qad_pl_mstr where (pl_desc like '%{0}%'  or  pl_prod_line='1090') and pl_domain='{1}'  ", P1, P2);

        
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }

    //获取分销点提前期
    [System.Web.Services.WebMethod()]

    public static string GetPeriod(string fxcode, string comp)
    {
        string result = "";
        var sql = "";

        sql = string.Format("  select ssd_leadtime from qad.dbo.qad_ssd_det where ssd_domain='{0}' and ssd_network='{1}'   ", comp, fxcode);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        var aqtqvalue = dt.Rows.Count==0?"":dt.Rows[0][0].ToString();
        result = "[{\"aqtqvalue\":\"" + aqtqvalue + "\"}]";
        return result;

    }

    protected void gv_d_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.Trim();
        //DataTable ldt = (DataTable)ViewState["TB"];
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        //GetGrid(ldt);
        ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["buyer_planner"].ToString() == "" || ldt.Rows[i]["site"].ToString() == "" || ldt.Rows[i]["dhperiod"]=="")
            {
                ldt.Rows[i].Delete();
            }
        }
        gv_d.DataSource = ldt;

        gv_d.DataBind();
        GetGrid(ldt);
    }

    protected void gv_d_DataBound(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, e.GetType(), "gridkey", "SetGridbtn();", true);
    }
    public void setCheckBoxListSelectValue(CheckBoxList checkboxlist, string checkedValue, char splitChar, bool enabled)
    {
        var list = checkedValue.Split(splitChar);
        foreach (var value in list)
        {
            ListItem item = checkboxlist.Items.FindByValue(value);
            if (item != null)
            {
                item.Selected = true;
                item.Enabled = enabled;
            }
        }
        checkboxlist.Enabled = enabled;
    }

    public void loadControl()
    {
        //--== 第一步:装载控件========================================================================================================
        //物料属性
        tblWLShuXing.Rows.Clear();
       // List<TableRow> ls2 = Pgi.Auto.Control.ShowControl("Main", "Main", 3, "rows", "column", "form-control");
        List<TableRow> ls2 = ShowControl("Main", "Main", 4, "", "", "line", "linewrite");
       
        for (int i = 0; i < ls2.Count; i++)
        {
            this.tblWLShuXing.Rows.Add(ls2[i]);
        }
        //物料主数据、计划数据
        tblWLZShuJu.Rows.Clear();
        List<TableRow> ls3 = ShowControl("Main", "logistics", 4, "", "", "line", "linewrite");
        for (int i = 0; i < ls3.Count; i++)
        {
            this.tblWLZShuJu.Rows.Add(ls3[i]);
        }
    }

    public static void CheckData_uid(string xmh, string cpl, out string flag)
    {
        //------------------------------------------------------------------------------验证采购和包装是否有维护
        flag = "";

        DataTable dt = DbHelperSQL.Query(@"select LEFT(bz_user,5)bz_user,left(caigou,5)caigou,left(product_user,5) product_user from form3_Sale_Product_MainTable where pgino='" + xmh + "'  AND   LEFT(bz_user,5) IN (SELECT workcode FROM V_HRM_EMP_MES ) AND   LEFT(caigou,5) IN (SELECT workcode FROM V_HRM_EMP_MES )").Tables[0];
        if (dt.Rows[0][0].ToString() == "" && cpl == "1")
        {
            flag = "产品清单中(" + xmh + ")采购工程师不存在或离职，不能提交!<br />";
        }
        if ((dt.Rows[0][1].ToString() == "" || dt.Rows[0][2].ToString() == "") && cpl != "1")
        {
            flag = "产品清单中(" + xmh + ")包装工程师或产品工程师不存在或离职，不能提交!<br />";
        }

    }
    /// <summary>
    /// 保存Data
    /// </summary>
    /// <param name="formtype">表单对应的设定</param>
    /// <param name="flag">输出参数是否保存成功</param>
    protected bool SaveData()
    {   //保存数据是否成功标识
       bool flag = true;
        //验证
        //获取表单页面数据
       List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("Main", "", this);
       List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
       string sformsate = "";
       string stat = formstate.Text;
       //string xmh = ((TextBox)this.FindControl("wlh")).Text.Trim();
       //string cpl = ((DropDownList)this.FindControl("line")).SelectedValue;
       //string check_flag = "";
        //从Auto_Form 获取值 验证
 
        for (int i = 0; i < ls.Count; i++)
        {
            Pgi.Auto.Common com = new Pgi.Auto.Common();
            com = ls[i];
            if (ls[i].Code == "")
            {
                var msg = ls[i].Value + "不能为空!";
                flag = false; //
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('" + msg + "');", true);//$(#'" + ls[i].Code + "').focus();
                return flag;
            }
        }
       // CheckData_uid( xmh, cpl,out  check_flag);
        if (m_sid == "" || Request.QueryString["state"]=="edit"  )
        {
            //applytype.Text = type.SelectedValue;
            string lsid = "P" + System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
            this.m_sid = Pgi.Auto.Public.GetNo("P", lsid, 0, 4);
            string partno = ((TextBox)this.FindControl("wlh")).Text + ((DropDownList)this.FindControl("gc_version")).SelectedValue+((DropDownList)this.FindControl("bz_version")).SelectedValue;
            //ViewState["instance"] = m_sid;
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "comp" && comp.Text != "")
                {
                    ls[i].Value = comp.Text;
                }
             
                if (ls[i].Code.ToLower() == "formno")
                {
                    ls[i].Value = this.m_sid;
                    ((TextBox)this.FindControl("Formno")).Text = this.m_sid;
                
                }
                if (ls[i].Code.ToLower() == "applytype")
                {
                    ls[i].Value = type.SelectedValue;
                 
                }
                //if (ls[i].Code.ToLower() == "partno")
                //{
                //    ls[i].Value = partno;
                //    break;
                //}
            }
        }
        string wlh = ((TextBox)this.FindControl("wlh")).Text.Trim();
        DataTable dt = DbHelperSQL.Query(@"select left(caigou,5)caigou,left(bz_user,5)bz_user,left(product_user,5) product_user, left(project_user,5) project_user from form3_Sale_Product_MainTable where pgino='" + wlh + "' ").Tables[0];
        string caigou = dt.Rows[0][0].ToString();
        string bz = dt.Rows[0][1].ToString();
        string product_user = dt.Rows[0][2].ToString();
        string project_user = dt.Rows[0][3].ToString();

        DataTable dt_person = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select workcode from V_HRM_EMP_MES where workcode='" + bz + "')").Tables[0];
            Pgi.Auto.Common lcbz_id = new Pgi.Auto.Common();
            lcbz_id.Code = "bz_id";
            lcbz_id.Key = "";
            lcbz_id.Value = "u_" + dt_person.Rows[0][0].ToString();
            ls.Add(lcbz_id);
     

        DataTable dt_caigou = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select workcode from V_HRM_EMP_MES where workcode='" + caigou + "')").Tables[0];
            Pgi.Auto.Common lccaigou_id = new Pgi.Auto.Common();
            lccaigou_id.Code = "caigou_id";
            lccaigou_id.Key = "";
            lccaigou_id.Value =dt_caigou.Rows.Count>0? "u_" + dt_caigou.Rows[0][0].ToString():"";
            ls.Add(lccaigou_id);


            DataTable dt_product = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select workcode from V_HRM_EMP_MES where workcode='" + product_user + "')").Tables[0];
            Pgi.Auto.Common lcproduct_id = new Pgi.Auto.Common();
            lcproduct_id.Code = "product_id";
            lcproduct_id.Key = "";
            lcproduct_id.Value = dt_product.Rows.Count > 0 ? "u_" + dt_product.Rows[0][0].ToString() : "";
            ls.Add(lcproduct_id);

            DataTable dt_project = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select workcode from V_HRM_EMP_MES where workcode='" + project_user + "')").Tables[0];
            Pgi.Auto.Common lcproject_id = new Pgi.Auto.Common();
            lcproject_id.Code = "project_id";
            lcproject_id.Key = "";
            lcproject_id.Value = dt_project.Rows.Count > 0 ? "u_" + dt_project.Rows[0][0].ToString() : "";
            ls.Add(lcproject_id);

            string userID = ((TextBox)this.FindControl("CreateById")).Text.ToString();
           //dt_manager = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select zg_workcode from V_HRM_EMP_MES where workcode='" + appuserid + "')").Tables[0];
            DataTable dt_manager = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select zg_workcode from V_HRM_EMP_MES where workcode='" + userID + "')").Tables[0];
            Pgi.Auto.Common lcmanager_id = new Pgi.Auto.Common();
            lcmanager_id.Code = "manager_id";
            lcmanager_id.Key = "";
            lcmanager_id.Value = dt_manager.Rows.Count > 0 ? "u_" + dt_manager.Rows[0][0].ToString() : "";
            ls.Add(lcmanager_id);

            if ((Request["state"] != null || Request["state"] != "") && (Request["formno"] != null))//
        {

            var lskey = ls.Where(r => r.Key == "1").ToList();
            foreach (var item in lskey)
            {
                item.Key = "0";
            }
            sformsate = formstate.Text == "" ? "edit" + DateTime.Now.ToString("yyyyMMddHHmmss") : formstate.Text;
            script += "$('#formstate').val('" + sformsate + "');";

            Pgi.Auto.Common comformstate = new Pgi.Auto.Common() { Code = "formstate", Key = "1", Value = sformsate };
            ls.Add(comformstate);




        }
        else
        {
            sformsate = formstate.Text == "" ? "new" + DateTime.Now.ToString("yyyyMMddHHmmss") : formstate.Text;
            script += "$('#formstate').val('" + sformsate + "');";

            Pgi.Auto.Common comformstate = new Pgi.Auto.Common() { Code = "formstate", Key = "1", Value = sformsate };
            ls.Add(comformstate);
        }
        //保存主数据1.插入的返回实例Id, 2.更新的返回受影响行数，需取Request["instanceid"]
        int instanceid = 0;
        //gridview中数据
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        //明细数据自动生成SQL，并增入SUM


        //主表相关字段赋值到明细表
        string formno_main = "";
        for (int j = 0; j < ls.Count; j++)
        {
            if (ls[j].Code.ToLower() == "formno") { formno_main = ls[j].Value; }
        }

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["formno"] = formno_main;
            ldt.Rows[i]["isfx"] = "Y";
            ldt.Rows[i]["domain"] = comp.Text;
           
            
        }
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
                ls_del.Sql = "delete from PGI_PartDtl_DATA_Form where formno='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_PartDtl_DATA_Form where formno='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_PartDtl_DATA_Form", "id", "Column1,numid,flag");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }
        }

        //List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_PartDtl_DATA_Form", "id", "Column1,numid,flag");
        //for (int i = 0; i < ls1.Count; i++)
        //{
        //    ls_sum.Add(ls1[i]);
        //}
        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        // instanceid = Pgi.Auto.Control.UpdateListValues(ls);
        try
        {
            instanceid = Pgi.Auto.Control.UpdateValues(ls, "PGI_PartMstr_DATA_Form");
        }
        catch (Exception e)
        {
            flag = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", "layer.alert('保存表单数据失败，请确认。ErrorMessage:" + e.Message.Replace("'", "").Replace("\r\n", "") + "');", true);
           
        }

        //如果是签核或修改 取传递过来instanceid值
        if (Request["formno"] != null && Request["formno"] != "" && Request.Form["Formno"] != "")
        {
            instanceid = 1;
        }

        //执行流程相关事宜
        if (instanceid > 0)
        {
            var code = ((DropDownList)this.FindControl("type")).Text.Trim();
            var xmh = ((TextBox)this.FindControl("part_no")).Text.Trim();
            var titletype = code == "新增" ? "物料申请" : "物料修改";
            string title = titletype + "[" + m_sid + "][" + xmh+ "][" + Request.Form["wlmc"] + "]"; //设定表单标题
            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";
            //将实例id,表单标题给流程script
            //script += "$('#instanceid',parent.document).val('" + instanceid.ToString() + "');" +
            //     "$('#customformtitle',parent.document).val('" + title + "');" +
            //     "if($('#txtInstanceID').val()==''){$('#txtInstanceID').val('" + instanceid.ToString() + "');}";
            //保存自定义（保存...）
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok",
            //     "$('#instanceid',parent.document).val('" + instanceid + "');" +
            //     "$('#customformtitle',parent.document).val('" + title + ")');parent.flowSaveIframe(true);",
            //     true);
        }
        else
        {
            //Response.Write("保存失败!");
            flag = false;

        }
        return flag;
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
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + " parent.flowSave(true);", true);// 先隐藏
           

        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = SaveData();
        
        //发送
        if (flag == true)
        {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", script + " parent.flowSend(true);", true);
            
        }
    }
    #endregion


    

    

    public bool Check()
    {
        bool result = true;

        // if()

        return result;
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
    public static List<TableRow> ShowControl(string lsform_type, string lsform_div, int lncolumn, string lsrow_style, string lscolumn_style, string lscontrol_style, string lscontrol_style2, DataTable ldt_head = null)
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


        int k = lncolumn;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (k == lncolumn)//(i % lncolumn) == 0
            {
                lrow = new TableRow();
                if (lsrow_style != "")
                {
                    lrow.CssClass = lsrow_style;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {

                ls.Add(lrow); k = lncolumn;
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
                    //设置只读 刷新数据会丢掉数据
                    ltxt.ReadOnly = true;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                if (lscontrol_style != "")
                {
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    else
                    {

                        ltxt.CssClass = lscontrol_style2;
                    }

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
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;
                    //ltxt.CssClass = "form-control input-s-sm";
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    else
                    {

                        ltxt.CssClass = lscontrol_style2;
                    }
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
                if (lscontrol_style != "")
                {
                    ltxt.CssClass = lscontrol_style;
                }



                lcellContent.Controls.Add(ltxt);
                lcellContent.Controls.Add(lnk);
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
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower(); ;// "chk" + ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //ltxt.RepeatLayout = RepeatLayout.Flow;
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
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;
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

            //CheckBox控件
            else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOX")
            {
                #region CHECKBOX
                CheckBox ltxt = new CheckBox();
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
                    //设置只读 刷新数据会丢掉数据
                    ltxt.Enabled = false;
                    ltxt.Attributes.Add("contenteditable", "false");
                }
                //样式
                //if (lscontrol_style != "")
                //{
                //    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                //    {
                //        ltxt.CssClass = lscontrol_style;
                //    }
                //    else
                //    {

                //        ltxt.CssClass = lscontrol_style2;
                //    }

                //}
                if (ldt_head != null)
                {
                    ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                }



                lcellContent.Controls.Add(ltxt);
                #endregion
            }

            //RadioButtonList控件
            else if (ldt.Rows[i]["control_type"].ToString() == "RadioButtonList")
            {
                #region "RadioButtonList"
                RadioButtonList ltxt = new RadioButtonList();
                HiddenField htxt = new HiddenField();
                htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                ltxt.AutoPostBack = false;
                ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                //ToolTip
                ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                //是否服务器运行
                ltxt.Attributes.Add("AutoPostBack", "false");
                //ltxt.RepeatLayout = RepeatLayout.Flow;
                ltxt.RepeatDirection = RepeatDirection.Horizontal;
                ltxt.RepeatColumns = 5;
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
                if (lscontrol_style != "")
                {
                    //ltxt.CssClass = lscontrol_style;

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
            //-----------------------------------控件判断结束----------------------------------------
            //判断下个字段是否独立
            if (i + 1 < ldt.Rows.Count)
            {
                if (ldt.Rows[i + 1]["control_onlyrow"].ToString() == "1")
                {
                    //int lnspan = i % lncolumn + 1;
                    //lcellContent.ColumnSpan = lnspan * 2;
                }
            }
            if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
            {
                lcellContent.ColumnSpan = lncolumn * 2 - 1;
            }
            lcellHead.Controls.Add(lbl);
            lrow.Cells.Add(lcellHead);
            lrow.Cells.Add(lcellContent);

            k--;

            if (k == 0 || i == ldt.Rows.Count - 1 || ldt.Rows[i]["control_onlyrow"].ToString() == "1")//(i % lncolumn) == 0
            {
                ls.Add(lrow); k = lncolumn;
            }
            ln += 1;
        }


        return ls;
    }
    #endregion
    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        //DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" )
            {
                if (ldt.Rows[i]["site"].ToString() == "100" || ldt.Rows[i]["site"].ToString() == "200")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('主地点数据不可删除,请知悉!');", true);
                    return;
                }
                else
                {
                    ldt.Rows[i].Delete();
                }
            }

        }
        ldt.AcceptChanges();
        
        gv_d.DataSource = ldt;
        gv_d.DataBind();
        setGridIsRead(ldt);
        GetGrid(ldt);
    }

    protected void GetGrid(DataTable DT)
    {
        DataTable ldt = DT;
        int index = gv_d.VisibleRowCount;
      //  string comp =DT.Rows.Count>0? DT.Rows[0]["domain"].ToString():"";
        string comp = (string)ViewState["comp"];
        for (int i = 0; i < gv_d.VisibleRowCount; i++)
        {
         
            DevExpress.Web.ASPxComboBox tb1 = (DevExpress.Web.ASPxComboBox)gv_d.FindRowCellTemplateControl(i, gv_d.DataColumns[0], gv_d.DataColumns[0].FieldName);
            DevExpress.Web.ASPxComboBox tb2 = (DevExpress.Web.ASPxComboBox)gv_d.FindRowCellTemplateControl(i, gv_d.DataColumns[2], gv_d.DataColumns[2].FieldName);
            DataTable ldt_ps = DbHelperSQL.Query(@"select ''cateid,''catevalue union all select value as cateid,val as catevalue from pt_base_code where type='pt_buyer' and TEXT='L' and domain like '" + comp + "%'   ").Tables[0];
            DataTable ldt_site = DbHelperSQL.Query(@"select ''cateid,''catevalue union all select value as cateid,value as catevalue from pt_base_code where type='fxcode'  and domain like '" + comp + "%'  ").Tables[0];
            tb1.DataSource = ldt_ps;
            tb1.TextField = "catevalue";
            tb1.ValueField = "cateid";
            tb1.DataBind();
            tb1.Value = ldt.Rows[i]["buyer_planner"].ToString();

            tb2.DataSource = ldt_site;
            tb2.TextField = "catevalue";
            tb2.ValueField = "cateid";
            tb2.DataBind();
            tb2.Value = ldt.Rows[i]["site"].ToString();
        }
    }

    protected void gv_d_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Add")
        {
            GvAddRows(1, e.VisibleIndex);
        }
    }
    private void GvAddRows(int lnadd_rows, int lnindex)
    {
        //新增一行
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);

        DataRow[] drs = ldt.Select("", "numid desc");
        DataTable dt_o = ldt.Clone();
        foreach (DataRow row in drs)  // 将查询的结果添加到dt中； 
        {
            dt_o.Rows.Add(row.ItemArray);
        }

        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            for (int j = 0; j < ldt.Columns.Count; j++)
            {
                if (ldt.Columns[j].ColumnName =="isfx")
                {
                  ldr[ldt.Columns[j].ColumnName] = "Y";
                }
               else if (ldt.Columns[j].ColumnName == "pt_pm_code")
                {
                    ldr[ldt.Columns[j].ColumnName] = "D";
                }
                else if (ldt.Columns[j].ColumnName == "purchase_days")
                {
                    ldr[ldt.Columns[j].ColumnName] = "0";
                }
                else if (ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = dt_o.Rows.Count <= 0 ? 0 : (Convert.ToInt32(dt_o.Rows[0]["numid"]) + 1);
                }
                else if (ldt.Columns[j].ColumnName.ToLower() == "formno")
                {
                    ldr[ldt.Columns[j].ColumnName] = ((TextBox)this.FindControl("formno")).Text;
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.InsertAt(ldr, lnindex + 1);

        }
        ViewState["TB"] = ldt;
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();
        setGridIsRead(ldt);
        GetGrid(ldt);

        

    }

    public void SetGvRow()
    {
  
        string wlh = ((TextBox)this.FindControl("wlh")).Text;
        string gc = ((DropDownList)this.FindControl("gc_version")).Text;
        string bz = ((DropDownList)this.FindControl("bz_version")).Text;


        //先查询数据库时候有数据
        string lsformno = Request["formid"];
        string lssql = @"select a.*,ROW_NUMBER() OVER (ORDER BY Updatetime) numid from PGI_PartDtl_DATA_Form a  WHERE formno='"+lsformno+"'";
        DataTable ldt_db = DbHelperSQL.Query(lssql).Tables[0];
        if (ldt_db != null)
        {
            if (ldt_db.Rows.Count > 0)
            {
                gv_d.DataSource = ldt_db;
                gv_d.DataBind();
                return;
            }
        }

        //首次
        DataTable ldt = DbHelperSQL.Query(@"select a.*,ROW_NUMBER() OVER (ORDER BY Updatetime) numid from [dbo].[PGI_PartDtl_DATA_Form] a where 1=0").Tables[0];


        DataRow ldr = ldt.NewRow();
        ldr["pt_pm_code"] = "D";
        ldr["numid"] =1;
        ldt.Rows.Add(ldr);
        


        gv_d.DataSource = ldt;
        gv_d.DataBind();
        setGridIsRead(ldt);
     
        //}

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetGvRow();
    }
    protected void gv_d_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        string lsformno = Request["formid"];
        string comp = (string)ViewState["comp"];
        string lssql = @"select a.* from [dbo].[PGI_PartDtl_DATA_Form] a 
                        where formno='" + lsformno + "' ";
        DataTable ldt =((DataTable)ViewState["TB"] )!=null?(DataTable)ViewState["TB"] : DbHelperSQL.Query(lssql).Tables[0];
        DevExpress.Web.GridViewDataColumn t = this.gv_d.Columns[1] as DevExpress.Web.GridViewDataColumn;
        DevExpress.Web.GridViewDataColumn t2 = this.gv_d.Columns[3] as DevExpress.Web.GridViewDataColumn;

        DevExpress.Web.ASPxComboBox tb1 = (DevExpress.Web.ASPxComboBox)this.gv_d.FindRowCellTemplateControl(e.VisibleIndex, t, "buyer_planner");
        DevExpress.Web.ASPxComboBox tb2 = (DevExpress.Web.ASPxComboBox)this.gv_d.FindRowCellTemplateControl(e.VisibleIndex, t2, "site");

        DataTable ldt_ps = DbHelperSQL.Query(@"select ''cateid,''catevalue union all select value as cateid,val as catevalue from pt_base_code where type='pt_buyer' and TEXT='L' and domain like '" + comp + "%' ").Tables[0];
        DataTable ldt_site = DbHelperSQL.Query(@"select ''cateid,''catevalue union all select value as cateid,value as catevalue from pt_base_code where type='fxcode'  and domain like '" + comp + "%'  ").Tables[0];

            if (tb1 != null)
            {
                tb1.DataSource = ldt_ps;
                tb1.TextField = "catevalue";
                tb1.ValueField = "cateid";
                tb1.DataBind();
                tb1.Value = ldt.Rows.Count > 0 ? ldt.Rows[e.VisibleIndex]["buyer_planner"].ToString() : "";
            }
            //地点下拉
            if (tb2 != null)
            {
                tb2.DataSource = ldt_site;
                tb2.TextField = "catevalue";
                tb2.ValueField = "cateid";
                tb2.DataBind();
                tb2.Value = ldt.Rows.Count > 0 ? ldt.Rows[e.VisibleIndex]["site"].ToString() : "";

            }

      
    }



}