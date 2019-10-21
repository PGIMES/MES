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
using Aspose.Cells;

public partial class Forms_MaterialBase_FuLiao_Apply : System.Web.UI.Page
{
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";
    public string m_sid = "";
    string state = "";
    public string FlowID = "";
    string StepID = "";
    string wlh = "";
    string domainn = "";
    public static string fxsite = "";
    int id = 0;
    public string company = "";
    BaseFun fun = new BaseFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);//
        string id = Request["formno"];
        ViewState["fltype"] = "";
        ViewState["flname"] = "";
        ViewState["UidRole"] = "";
        Setpage(false);
        string IsAttach = "False";
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"];
        }
        if (Request.QueryString["state"] != null)
        {
            this.state = Request.QueryString["state"];
        }
        if (Request.QueryString["wlh"] != null)
        {
            this.wlh = Request.QueryString["wlh"];
        }
        if (Request.QueryString["domain"] != null)
        {
            this.domainn = Request.QueryString["domain"];
        }
        //登录人员权限
        if (Request.QueryString["stepid"] != null)
        {
            string sqlRole = @"select * from  RoadFlowWebForm.[dbo].[WorkFlowTask] a join(select InstanceID,flowid,max(sort)sort from  RoadFlowWebForm.[dbo].[WorkFlowTask] 
         where FlowID='9d591dd9-b615-4e8f-b2f8-d3a7161af952' AND InstanceID='" + Request.QueryString["instanceid"] + "'  group by FlowID,InstanceID )b on a.FlowID=b.FlowID  AND a.InstanceID=b.InstanceID  and a.Sort=b.sort where stepid='" + Request.QueryString["stepid"] + "' ";

            var loguser = DbHelperSQL.Query(sqlRole).Tables[0];
            if (loguser.Rows.Count > 0)
            {
                if (loguser.Rows[0]["StepName"].ToString().Contains("申请人") == true)
                { ViewState["UidRole"] = "Applyer"; }
                if (loguser.Rows[0]["StepName"].ToString().Contains("申请人确认") == true)
                { ViewState["UidRole"] = "Admin"; }
                if (loguser.Rows[0]["StepName"].ToString().Contains("申请人经理") == true)
                { ViewState["UidRole"] = "Manager"; }
                if (loguser.Rows[0]["StepName"].ToString().Contains("仓库") == true)
                {
                    ViewState["UidRole"] = "Ware";                 }
                if (loguser.Rows[0]["StepName"].ToString().Contains("采购") == true)
                { ViewState["UidRole"] = "Purchaser"; }

            }
        }

        
        #region "IsPostBack"
        if (!IsPostBack)
        {

            Session["gvdtl"] = null;
            if (LogUserModel != null)
            {
                //当前登陆人员
                //新增时表头基本信息
                CreateById.Text = LogUserModel.UserId;
                CreateByName.Text = LogUserModel.UserName;
                CreateByDept.Text = LogUserModel.DepartName;
                CreateDate.Text = System.DateTime.Now.ToString();
                var tbltype = DbHelperSQL.Query("select '' as value ,'-请选择-' as text,'' as val union all  select cp_line as value, name as text,cp_code as val from PGI_BASE_PART_ddl where type = '辅料类' and value<>'4010'   order by value").Tables[0];
                fun.initDropDownList(fltype, tbltype, "value", "text");
                var tbldomain = DbHelperSQL.Query(" select cateid,catevalue from pgi_base_data where cate='domain' ").Tables[0];
                fun.initCheckBoxList(ddldomain, tbldomain, "cateid", "catevalue");


                if (state == "edit")//编辑
                {
                    string re_sql = @"select top 1 a.InstanceID,b.createbyid,b.createbyname ,wlh,domain
                                                            from (select InstanceID from RoadFlowWebForm.dbo.WorkFlowTask where FlowID='9d591dd9-b615-4e8f-b2f8-d3a7161af952' and status in(0,1))  a
                                                                inner join PGI_FLMstr_DATA_Form b on a.InstanceID=b.formno 
                                                             where  b.wlh='" + Request.QueryString["wlh"] + "' and b.domain='" + domainn + "' ";
                    DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];
                    if (re_dt.Rows.Count > 0)
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "物料号正在申请中，不能修改(物料号:" + re_dt.Rows[0]["wlh"].ToString() + ")!");
                        btnflowSend.Visible = false;
                    }
                   
                    
                }
                

            }
        }


        #endregion

    
        var cpline = "";
        var cpname = "";
         IsAttach = IsAttachment.Checked.ToString();
       
        cpline = fltype.SelectedItem.Value;
        cpname = fltype.SelectedItem.Text;
        DataTable dtMst = new DataTable();
        
        if (m_sid != "" && state != "edit")
        {
            dtMst = DbHelperSQL.Query("select * from PGI_FLMstr_DATA_Form where formno='" + this.m_sid + "'").Tables[0];
        }
        var domain = "";
        var strType = "";
        if (dtMst != null && dtMst.Rows.Count > 0)
        {
            CreateById.Text = dtMst.Rows[0]["CreateById"].ToString();
            CreateByName.Text = dtMst.Rows[0]["CreateByName"].ToString();
            CreateByDept.Text = dtMst.Rows[0]["CreateByDept"].ToString();
            CreateDate.Text = dtMst.Rows[0]["CreateDate"].ToString();
            string buyerid = dtMst.Rows[0]["buyer_planner"].ToString();
            company = dtMst.Rows[0]["domain"].ToString();
            string sql = string.Format("select cp_line as value, name as text,cp_code as val from PGI_BASE_PART_ddl where type = '辅料类'and cp_line='{0}'", dtMst.Rows[0]["line"].ToString());
            strType = DbHelperSQL.Query(sql).Tables[0].Rows[0]["text"].ToString();
            fltype.SelectedItem.Text = strType;
            fltype.Enabled = false;
            fltype.CssClass = "form-control input-s-sm disabled ";
            fltype.BackColor = System.Drawing.Color.Empty;
            setCheckBoxListSelectValue(ddldomain, dtMst.Rows[0]["domain"].ToString(), ';', true);
            cpline = dtMst.Rows[0]["line"].ToString();
            cpname = dtMst.Rows[0]["lxmc"].ToString();
            IsAttach = dtMst.Rows[0]["IsAttach"].ToString();
            IsAttachment.Checked = Convert.ToBoolean(IsAttach);
          
            Setpage(Convert.ToBoolean(IsAttach));
            for (int i = 0; i < this.ddldomain.Items.Count; i++)
            {
                this.ddldomain.Items[i].Enabled = false;
            }
            IsAttachment.Enabled = false;
        }
        loadControl(IsAttach);
        RadioButtonList bclb = (RadioButtonList)this.FindControl("bclb") as RadioButtonList;
        ((RadioButtonList)this.FindControl("bclb")).Enabled = false;
        if (dtMst.Rows.Count > 0 && bclb!=null)
        { ((RadioButtonList)this.FindControl("bclb")).SelectedValue = dtMst.Rows[0]["bclb"].ToString(); }
       
     
   
        //发起【修改申请】初始化值给画面
        var wlh = Request["wlh"];
        var formno = Request["formno"];
        domain = Request["domain"];
        DataTable dtMstOld = new DataTable();
        if (formno != null)
        {
            dtMstOld = DbHelperSQL.Query(" select * from PGI_FLMstr_DATA where formno='" + formno + "'  and wlh='"+wlh+"'").Tables[0];

        }
        if (dtMstOld != null && dtMstOld.Rows.Count > 0)
        {
            if (dtMstOld.Rows[0]["line"].ToString()== "4010")
            {
                Pgi.Auto.Public.MsgBox(this, "alert", "刀具清单修改请至刀具查询页面修改!");
                btnflowSend.Visible = false;
                btnSave.Visible = false;
            }
            string strsql = string.Format("select cp_line as value, name as text,cp_code as val from PGI_BASE_PART_ddl where type = '辅料类'and cp_line='{0}'", dtMstOld.Rows[0]["line"].ToString());
            strType = DbHelperSQL.Query(strsql).Tables[0].Rows[0]["text"].ToString();
            fltype.SelectedItem.Text = strType;
            fltype.Enabled = false;
            fltype.CssClass = "form-control input-s-sm disabled ";
            IsAttachment.Enabled = false;
            fltype.BackColor = System.Drawing.Color.Empty;
            setCheckBoxListSelectValue(ddldomain, dtMstOld.Rows[0]["domain"].ToString(), ';', true);
            var jzdw = (DropDownList)this.FindControl("jzunit");
            var fydw = (DropDownList)this.FindControl("fyunit");
            company = dtMstOld.Rows[0]["domain"].ToString();
            cpline = dtMstOld.Rows[0]["line"].ToString();
            cpname = dtMstOld.Rows[0]["lxmc"].ToString();
            IsAttach = "False";// dtMstOld.Rows[0]["IsAttach"].ToString();
            IsAttachment.Checked = Convert.ToBoolean(IsAttach);
            //包材类别
            ((RadioButtonList)this.FindControl("bclb")).SelectedValue = dtMstOld.Rows[0]["bclb"].ToString();
            
            //取当前QAD安全库存量
            string str_kc = string.Format("select pt_sfty_stk from qad_pt_mstr where pt_part='{0}' and pt_domain='{1}'", wlh, company);
            string qad_aqkc = DbHelperSQL.Query(str_kc).Tables[0].Rows[0][0].ToString();
            ((TextBox)this.FindControl("qad_aqkc")).Text = qad_aqkc;
            Setpage(Convert.ToBoolean(IsAttach));
            string apply_status = dtMstOld.Rows[0]["pt_status"].ToString().ToUpper();
            var status = DbHelperSQL.Query("  select ''cateid,''catevalue union all select value as cateid,val as catevalue from pt_base_code where type='pt_status'").Tables[0];
            ((DropDownList)this.FindControl("pt_status")).DataSource = status;
            ((DropDownList)this.FindControl("pt_status")).DataTextField = "catevalue";
            ((DropDownList)this.FindControl("pt_status")).DataValueField = "cateid";
            ((DropDownList)this.FindControl("pt_status")).DataBind();
            if (((DropDownList)this.FindControl("pt_status")) != null)
            {
                ((DropDownList)this.FindControl("pt_status")).SelectedValue = apply_status;
                ((DropDownList)this.FindControl("pt_status")).CssClass = "form-control input-s-sm disabled ";
                ((DropDownList)this.FindControl("pt_status")).Enabled = false;
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
            DataColumn aqkc = new DataColumn()
            {
                DefaultValue = qad_aqkc,
                ColumnName = "qad_aqkc"
            };

            dtMstOld.Columns.Add(createbyid);
            dtMstOld.Columns.Add(createbyname);
            dtMstOld.Columns.Add(createdate);
            dtMstOld.Columns.Add(createbydept);
            dtMstOld.Columns.Add(aqkc);
            Pgi.Auto.Control.SetControlValue("FL_Main", "", this, dtMstOld.Rows[0]);
            if (((TextBox)this.FindControl("remark")) != null)
            {
                ((TextBox)this.FindControl("remark")).Text = "";
            }
        }
        ViewState["fltype"] = cpline;
        ViewState["flname"] = cpname;
        fltype.SelectedItem.Text = cpline == "" ? strType : cpname;
        
        //--== 第 步:装载控件========================================================================================================

        if (dtMst != null && dtMst.Rows.Count > 0)
        {

            Pgi.Auto.Control.SetControlValue("FL_Main", "", this, dtMst.Rows[0]);
            formstate.Text = dtMst.Rows[0]["formstate"] == null ? "" : dtMst.Rows[0]["formstate"].ToString();
            id = dtMst.Rows[0]["id"].ToString();
            //显示QAD当前实际库存
            if (dtMst != null && dtMst.Rows.Count > 0  && !string.IsNullOrEmpty (dtMst.Rows[0]["wlh"].ToString()))
            {
                string str_kc = string.Format("select pt_sfty_stk from qad_pt_mstr where pt_part='{0}' and pt_domain='{1}'", dtMst.Rows[0]["wlh"], company);
                ((TextBox)this.FindControl("qad_aqkc")).Text = DbHelperSQL.Query(str_kc).Tables[0].Rows.Count > 0 ? DbHelperSQL.Query(str_kc).Tables[0].Rows[0][0].ToString() : "0";

            }
        }

        //获取每步骤栏位状态设定值，方便前端控制其可编辑性（不需修改）
        string FlowID = Request.QueryString["flowid"];
        string StepID = Request.QueryString["stepid"];
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);



        
    }


    public void loadControl( string IsAttach)
    {
        //--== 第一步:装载控件========================================================================================================
        //物料属性
        if (IsAttach=="False")
        {
            tblWLShuXing.Rows.Clear();
           // tblWLSC.Rows.Clear();
            // List<TableRow> ls2 = Pgi.Auto.Control.ShowControl("Main", "Main", 3, "rows", "column", "form-control");
            List<TableRow> ls2 = ShowControl("FL_Main", "Main", 4, "", "", "line", "linewrite");

            for (int i = 0; i < ls2.Count; i++)
            {
                this.tblWLShuXing.Rows.Add(ls2[i]);
            }
            //物料主数据、计划数据
            tblWLZShuJu.Rows.Clear();
            List<TableRow> ls3 = ShowControl("FL_Main", "logistics", 4, "", "", "line", "linewrite");
            for (int i = 0; i < ls3.Count; i++)
            {
                this.tblWLZShuJu.Rows.Add(ls3[i]);
            }

           
        }
        else
        {
            tblWLShuXing.Rows.Clear();
            tblWLZShuJu.Rows.Clear();
            tblWLSC.Rows.Clear();
            List<TableRow> ls1 = ShowControl("FL_Main", "import", 4, "", "", "line", "linewrite");
            for (int i = 0; i < ls1.Count; i++)
            {
                this.tblWLSC.Rows.Add(ls1[i]);
            }
        }
       
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
                //if (lscontrol_style != "")
                //{
                //    ltxt.CssClass = lscontrol_style;
                //}

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

    protected bool SaveData()
    {   //保存数据是否成功标识
        bool flag = true;
        //验证
        string sformsate = "";
        string stat = formstate.Text;
        //获取表单页面数据
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("FL_Main", "", this);
      //RadioButtonList  bclb = ((RadioButtonList)this.FindControl("bclb")) as RadioButtonList;
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
            if (ls[i].Code.ToLower() == "bclb")
            {
                if (this.FindControl("bclb") is RadioButtonList)
                {
                    RadioButtonList bclb = ((RadioButtonList)this.FindControl("bclb")) as RadioButtonList;
                    if (bclb != null)
                        ls[i].Value = ((RadioButtonList)this.FindControl("bclb")).SelectedValue;
                }
                //if (bclb != null)
                //    ls[i].Value = ((RadioButtonList)this.FindControl("bclb")).SelectedValue;
            }
        }

            if ((Request["state"] == null || Request["state"] == "edit") && (Request["instanceid"] == null) && formstate.Text == "")//
            {
               
                string lsid = "FL" + System.DateTime.Now.Year.ToString().Substring(3, 1) + System.DateTime.Now.Month.ToString("00");
                this.m_sid = Pgi.Auto.Public.GetNo("FL", lsid, 0, 4);

                if (IsAttachment.Checked!=true)
                {

                    var txtWlh = ((TextBox)this.FindControl("wlh"));
                    var newwlh = txtWlh.Text;
                    newwlh = GetWLH((string)ViewState["fltype"]);
                }
                else
                {
                    var upfile = ((HyperLink)this.FindControl("link_upload")).NavigateUrl;
                    var lsupload = ls.Where(r => r.Code == "upload").ToList();
                    lsupload[0].Value = upfile;

                    Pgi.Auto.Common line_id= new Pgi.Auto.Common();
                    line_id.Code = "line";
                    line_id.Key = "";
                    line_id.Value = (string)ViewState["fltype"];
                    ls.Add(line_id);

                }

                for (int i = 0; i < ls.Count; i++)
                {

                    if (ls[i].Code.ToLower() == "formno")
                    {
                        ls[i].Value = this.m_sid;
                        ((TextBox)this.FindControl("Formno")).Text = this.m_sid;

                    }
                  

                }
            }

         
        string userID = ((TextBox)this.FindControl("CreateById")).Text.ToString();
        DataTable dt_manager = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select zg_workcode from V_HRM_EMP_MES where workcode='" + userID + "')").Tables[0];
        Pgi.Auto.Common lcmanager_id = new Pgi.Auto.Common();
        lcmanager_id.Code = "manager_id";
        lcmanager_id.Key = "";
        lcmanager_id.Value = dt_manager.Rows.Count > 0 ? "u_" + dt_manager.Rows[0][0].ToString() : "";
        ls.Add(lcmanager_id);

        string cpline = (string)ViewState["fltype"];
        DataTable dt_buyer = DbHelperSQL.Query(@"select id from RoadFlowWebForm.dbo.Users a where account=(select purchaser_id from PGI_BASE_PART_ddl where type = '辅料类' and CP_Line='"+cpline+"')").Tables[0];
        Pgi.Auto.Common buyer_id = new Pgi.Auto.Common();
        buyer_id.Code = "buyyer_id";
        buyer_id.Key = "";
        buyer_id.Value = dt_buyer.Rows.Count > 0 ? "u_" + dt_buyer.Rows[0][0].ToString() : "";
        ls.Add(buyer_id);

        Pgi.Auto.Common type_id = new Pgi.Auto.Common();
        type_id.Code = "lxmc";
        type_id.Key = "";
        type_id.Value = (string)ViewState["flname"];
        ls.Add(type_id);

        Pgi.Auto.Common Attach_id = new Pgi.Auto.Common();
        Attach_id.Code = "IsAttach";
        Attach_id.Key = "";
        Attach_id.Value = IsAttachment.Checked.ToString();
        ls.Add(Attach_id);


        if ((Request["state"] != null && Request["state"]!= "") )
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
        // instanceid = Pgi.Auto.Control.UpdateListValues(ls);
        try
        {
            instanceid = Pgi.Auto.Control.UpdateValues(ls, "PGI_FLMstr_DATA_Form");
        }
        catch (Exception e)
        {
            flag = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ok", "layer.alert('保存表单数据失败，请确认。ErrorMessage:" + e.Message.Replace("'", "").Replace("\r\n", "") + "');", true);

        }

        var fileup = (FileUpload)this.FindControl("upload");
        var filepath = "";
        if (fileup != null)
        {
            if (fileup.HasFile)
            {
                SaveFile(fileup, m_sid + "_" + getDomain(), out filepath);
                //更新文件目录
                string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                string sqlupdatefilecolum = string.Format("update PGI_FLMstr_DATA_Form set upload='{0}',uploadname='{2}' where formno='{1}'", filepath, m_sid,filename);
                DbHelperSQL.ExecuteSql(sqlupdatefilecolum);
                flag = true;
            }
        }

        //如果是签核或修改 取传递过来instanceid值
        if (Request["formno"] != null && Request["formno"] != "" && Request.Form["Formno"] != "")
        {
            instanceid = 1;
        }
    
        //执行流程相关事宜
        if (instanceid > 0)
        {
            var wlh = "";
            string cplline = (string)ViewState["fltype"];
            string cpname = (string)ViewState["flname"];
            if (IsAttachment.Checked!=true)
            {
                wlh = ((TextBox)this.FindControl("wlh")).Text.Trim();
            }
            else
            {
                wlh = cpname;
            }
            var titletype = sformsate.Left(4) == "edit" ? "辅料修改" : "辅料申请";
            string title = titletype + "[" + m_sid + "][" + wlh + "][" + cplline + "]"; //设定表单标题
            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";
        }
        else
        {
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

    protected void Setpage(bool Isattach)
    {
    
        if (Isattach!=true)//附件上传
        {
            var dl = fltype.SelectedValue; 
            ViewState["fltype"] = dl;
            var wlh = "";
            if (dl != "")
            {wlh = GetWLH(dl); }
            var txtWlh = (TextBox)this.FindControl("wlh");
            var txtline = (TextBox)this.FindControl("line");
            if (txtWlh != null)
            {
                txtWlh.Text = wlh;
            }
            if (txtline != null)
            {
                txtline.Text = dl;
            }
            div_base.Style.Add("display", "");
            div_main.Style.Add("display", "");
            divwarn.Style.Add("display", "none");
           div_sc.Style.Add("display", "none");
            div2.Style.Add("display", "none");
        }
        else
        {
           
           div_sc.Style.Add("display", "");
            div2.Style.Add("display", "");
            divwarn.Style.Add("display", "");
            div_base.Style.Add("display", "none");
            div_main.Style.Add("display", "none");
            gv.Visible = false;
            if (Request.QueryString["stepid"] == null || (string)ViewState["UidRole"].ToString() == "Ware" || (string)ViewState["UidRole"].ToString() == "Manager")
            {
                div2.Style.Add("display", "none");
            }
            else
            {
                div2.Style.Add("display", "");
                div_sc.Style.Add("display", "none");
                string strSql = " select  upload as File_lj,uploadname as File_name  from PGI_FLMstr_DATA_Form  where formno='" + m_sid + "'";
                DataSet ds = DbHelperSQL.Query(strSql);
                gvFile_ddfj.DataSource = ds;
                gvFile_ddfj.DataBind();

                DataTable dt = DbHelperSQL.Query(@"  select  ROW_NUMBER() OVER(ORDER BY ID) 序号,wlh as 物料号,domain as 申请工厂,wlmc as 描述一,ms as 描述二,line as 产品类, pt_status as 产品状态申请,jzweight as 净重,jzunit as 净重单位,aqkc as 安全库存,status as 产品状态财务,
                   buyer_planner as 采购员,dhperiod as 订货周期,quantity_min as 最小订单量,quantity_max as 最大订单量,ddbs as 订单倍数,ddsl as 订单数量,purchase_days as 采购提前期,cailiao1 as 材料一,cailiao2 as 材料二,bclb as 包材类别  from PGI_FLMstr_DATA_Form_Tmp 
                   where  formno='" + m_sid+"' order by wlh").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gv.Visible = true;
                    gv.DataSource = dt;
                    gv.DataBind();
                    Session["gvdtl"] = dt;
                    btn_import.Style.Add("display", "display");
                }
            }
            
        }
    }
    protected void fltype_SelectedIndexChanged(object sender, EventArgs e)
    {  
        var dl = fltype.SelectedValue; 
        var wlh = GetWLH(dl);
        ViewState["fltype"] = dl;
        if (IsAttachment.Checked == false)
        {
            var line = (TextBox)this.FindControl("line");
            line.Text = dl;
            var txtWlh = (TextBox)this.FindControl("wlh");
            var txtdhperiod = (TextBox)this.FindControl("dhperiod");
            ((TextBox)this.FindControl("QAD_aqkc")).Text = "0";//新增物料时默认为0
            txtdhperiod.Text = "7";
            if (dl != "4060")
            {
                ((TextBox)this.FindControl("cailiao1")).Style.Add("display", "none");
                ((TextBox)this.FindControl("cailiao2")).Style.Add("display", "none");
                ((Label)this.FindControl("lbl_FL_Main_Main_10")).Style.Add("display", "none");
                ((Label)this.FindControl("lbl_FL_Main_Main_11")).Style.Add("display", "none");

            }
            else
            {
                ((TextBox)this.FindControl("cailiao1")).Style.Add("display", "display");
                ((TextBox)this.FindControl("cailiao2")).Style.Add("display", "display");
                ((Label)this.FindControl("lbl_FL_Main_Main_10")).Style.Add("display", "display");
                ((Label)this.FindControl("lbl_FL_Main_Main_11")).Style.Add("display", "display");
            }
            if (txtWlh != null)
            {
                txtWlh.Text = wlh;
            }
            if (wlh == "")
            {
                var msg = "未维护物料编码规则。请联络IT设定";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('" + msg + "');", true);
            }
        }
        else
        {
            Setpage(true);
            loadControl("true");
        }
        
    }


    protected void btn_import_Click(object sender, EventArgs e)
    {
       DataTable dt = Session["gvdtl"] as DataTable;
       gv.DataSource = dt;
       gv.DataBind();
        ASPxGridViewExporter1.WriteXlsToResponse("新申请物料清单" + System.DateTime.Now.ToShortDateString());//导出到Excel
    }

    public static string GetWLH(string P1)
    {
        string result = "";

       // var sqlwlh = string.Format("select isnull(max(wlh),'')wlh from (select pt_part as wlh from qad_pt_mstr where pt_part<>'Z13000384' and left(pt_prod_line,4) like {0} and left(pt_part,1)='Z' and  len(pt_part)=9  union  select wlh from PGI_FLMstr_DATA_Form where line like {0} union    select wlh from PGI_FLMstr_DATA_Form_Tmp where line like {0} )t where right(wlh,1)<>'X'", P1);
        var sqlwlh = @"select isnull(max(wlh),'')wlh from (select pt_part as wlh from qad_pt_mstr where pt_part<>'Z13000384' and pt_part<>'Z09000002' and pt_part <>'Z09000001'
                                   and left(pt_prod_line,4) like '" + P1 + "%' and left(pt_part,1)='Z' and  len(pt_part)=9 union  select wlh from PGI_FLMstr_DATA_Form where line like '" + P1 + "%'  union    select wlh from PGI_FLMstr_DATA_Form_Tmp where line like '" + P1 + "%'  )t where right(wlh,1)<>'X'";

        var wlh = DbHelperSQL.GetSingle(sqlwlh).ToString();
        if (wlh.Length > 0)
        {
            var sn =wlh.Substring(3);
            sn = (Convert.ToInt32(sn) + 1).ToString().PadLeft(6, '0');
            wlh = wlh.Left(3) + sn;

        }

        result = wlh.ToString();
        return result;
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
    public string getDomain()
    {
        string value = "";
        foreach (ListItem item in ddldomain.Items)
        {
            if (item.Selected)
            {
                value = value + item.Value + ";";
            }
        }
        return value.TrimEnd(';');
    }
    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "\\UploadFile\\FL";
    public void SaveFile(FileUpload fileupload, string subpath, out string filepath)
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
            filename = fileupload.FileName;
            path = path + "\\" + filename;
            fileupload.SaveAs(path.Replace("&", "_").TrimStart(' '));
        }
        //return save path
        filepath =  savepath + "\\" + subpath + "\\" + filename.Replace("&", "_").TrimStart(' ');
    }
    #endregion


   
    protected void IsAttachment_CheckedChanged(object sender, EventArgs e)
    {
        if (IsAttachment.Checked == true)
        { Setpage(true);
        loadControl("true");
        HyperLink2.Visible = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请下载<span style=color:red>上传格式</span>后整理上传，此格式只针对物料批量新增，不可申请批量修改物料！')", true);
        }
        else 
        { Setpage(false);
        HyperLink2.Visible = false;
        }
    }
    protected void Btn_ddfj_Click(object sender, EventArgs e)
    {
        string FileName = "";
        string Pathvalue = "";
        if (upload_fj.Value != "")
        {
            FileUpload(m_sid, CreateById.Text, upload_fj, upload_fj.ID, gvFile_ddfj,out FileName,out Pathvalue);
            Setpage(true);
            ShowFile(m_sid, CreateById.Text, upload_fj.ID, gvFile_ddfj);
           
                GetUpload(m_sid, FileName, Pathvalue);
           

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择文件后再点击上传！')", true);
        }
    }

    public void GetUpload(string FormNo,string FileName,string Pathvalue)
    {
        bool flag = true;
        Workbook workbook = new Workbook();
        string strSql = " select * from form1_Sale_YJ_UPLOAD where Code='" + FormNo + "'  and id in (select max(id) from form1_Sale_YJ_UPLOAD where Code='" + FormNo + "' )";
        DataSet ds = DbHelperSQL.Query(strSql);
        string path = ds.Tables[0].Rows[0]["File_lj"].ToString();
        path = MapPath("~") + Pathvalue;
        workbook.Open(path);
        string Sql = "";
        string errmsg = "";
        string dropline = (string)ViewState["fltype"];
        string  UidRole = (string)ViewState["UidRole"];
        // 先删除临时表中的物料暂存数据      
        Sql = " update  PGI_FLMstr_DATA_Form set  upload='"+Pathvalue+"',uploadname='"+FileName+"' where formno='"+FormNo+"'";
        Sql += " delete from PGI_FLMstr_DATA_Form_Tmp where formno='" + FormNo + "'";
        int delrows = DbHelperSQL.ExecuteSql(Sql.ToString());
        Cells cells = workbook.Worksheets[0].Cells;
        for (int i = 1; i < cells.MaxDataRow + 1; i++)
        {
            string formno = Request.QueryString["instanceid"];
            string comp = company;
            string ms1 = cells[i, 0].StringValue.Trim();
            string ms2 = cells[i, 1].StringValue.Trim();
            string line = cells[i, 2].StringValue.Trim();
            string unit = cells[i, 3].StringValue.Trim();
            string apply_status = cells[i, 4].StringValue.Trim();
            string jz = cells[i, 5].StringValue.Trim();
            string jzunit = cells[i, 6].StringValue.Trim();
            string aqkc = cells[i, 7].StringValue.Trim();
            string dhperiod = cells[i, 8].StringValue.Trim();
            string quantity_min = cells[i, 9].StringValue.Trim();
            string quantity_max = cells[i, 10].StringValue.Trim();
            string ddbs = cells[i, 11].StringValue.Trim();
            string ddsl = cells[i, 12].StringValue.Trim();
            string purchase_days = cells[i, 13].StringValue.Trim();
            string cailiao1 = cells[i, 14].StringValue.Trim();
            string cailiao2 = cells[i, 15].StringValue.Trim();
            string bclb = cells[i, 16].StringValue.Trim() == null ? "" : cells[i, 16].StringValue.Trim();

            //  判断汇入的栏位格式是否正确
            DataTable dt = DbHelperSQL.Query("exec FL_Data_Import   0, '" + formno + "','" + comp + "','" + ms1 + "','" + ms2 + "','" + line + "','" + unit + "','" + apply_status + "','" + jz + "','" + jzunit + "','','" + aqkc + "','" + dhperiod + "','" + quantity_min + "','" + quantity_max + "','" + ddbs + "','" + ddsl + "','" + purchase_days + "','" + dropline + "','" + UidRole + "','" + cailiao1 + "','" + cailiao2 + "','" + bclb + "'").Tables[0];


            if (dt.Rows[0][0].ToString() != "")
            {
                flag = false;
                errmsg = errmsg + "第" + i + "行:" + dt.Rows[0][0].ToString();
            }
            else
            {
                // 将汇入的资料导入临时表

                int row = DbHelperSQL.ExecuteSql("exec FL_Data_Import   1, '" + formno + "','" + comp + "','" + ms1 + "','" + ms2 + "','" + line + "','" + unit + "','" + apply_status + "','" + jz + "','" + jzunit + "','','" + aqkc + "','" + dhperiod + "','" + quantity_min + "','" + quantity_max + "','" + ddbs + "','" + ddsl + "','" + purchase_days + "','" + dropline + "','','" + cailiao1 + "','" + cailiao2 + "','" + bclb + "'");
              
            }


        }
        if (errmsg != "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('" + errmsg + "')", true);
        }
        else
        {
            DataTable dt = DbHelperSQL.Query("exec FL_Data_Import   2, '" + m_sid + "','','','','','','','','','','','','','','','','','','','','',''").Tables[0];
            Pgi.Auto.Control.SetGrid(this.gv, dt, 70);
            gv.Visible = true;
            this.gv.Columns[0].Width = 50;
        }
    
    }
    public void FileUpload(string FormNo, string EmpID, System.Web.UI.HtmlControls.HtmlInputFile FileUpLoader, string File_mc, GridView gv, out string FileName,out string Pathvalue)
    {
        string filename = "";
        string path = "";
        if (FileUpLoader.PostedFile.FileName.Length != 0)
        {
             filename = FileUpLoader.PostedFile.FileName;
            if (filename.Contains("\\") == true)
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }

            string MapDir = MapPath("~");
             path = MapDir + savepath + "\\" + FormNo + "_" + getDomain();
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录 
            }
            FileUpLoader.PostedFile.SaveAs(path + "\\" + filename);
            FileSaveToDB(FormNo, EmpID, filename, File_mc, gv);
           
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择文件后再点击上传！')", true);
        }
        FileName = filename;
        Pathvalue = savepath + "\\" + FormNo + "_" + getDomain()+"\\"+filename;
    }

    public void FileSaveToDB(string FormNo, string EmpID, string filename, string File_mc, GridView gv)
    {
        string File_lj = savepath + "\\" + FormNo + "_" + getDomain() +"\\" + filename;
        string strSql = "insert into form1_Sale_YJ_UPLOAD(Code, UpLoad_user, File_name, File_lj,File_mc) values('" + FormNo + "','" + EmpID + "','" + filename + "','" + File_lj + "','" + File_mc + "')";
        DbHelperSQL.ExecuteSql(strSql);
        ShowFile(FormNo, EmpID, File_mc, gv);
    }

    public void ShowFile(string FormNo, string EmpID, string File_mc, GridView gv)
    {
        string strSql = " select * from form1_Sale_YJ_UPLOAD where Code='" + FormNo + "'  and id in (select max(id) from form1_Sale_YJ_UPLOAD where Code='" + FormNo + "' )";
        DataSet ds = DbHelperSQL.Query(strSql);
        gv.DataSource = ds;
        gv.DataBind();
    }
}