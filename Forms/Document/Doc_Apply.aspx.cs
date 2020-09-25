using DevExpress.Web;
using Maticsoft.DBUtility;
using Pgi.Auto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Forms_Document_Doc_Apply : System.Web.UI.Page
{

    public string ValidScript = "";
    public string DisplayModel;
    public string fieldStatus;

    public string SQ_StepID = "91AC2E10-9F5D-4DC0-B676-62FB8A165E16";
    public string UserId = "";

    string FlowID = "A";
    public string StepID = "A";
    string state = "";
    string m_sid = "";
    string Stepname = "";

   

    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Setddl_sel_Dept();
        Setddl_sel_User();
        if (ViewState["ApplyId_i"] == null) { ViewState["ApplyId_i"] = ""; }

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }

        if (Request.QueryString["flowid"] != null)
        {
            FlowID = Request.QueryString["flowid"];
        }

        if (Request.QueryString["stepid"] != null)
        {
            StepID = Request.QueryString["stepid"];
        }
        if (Request.QueryString["state"] != null)
        {
            state = Request.QueryString["state"];
        }

        if (Request.QueryString["stepid"] != null)
        {
            string sqlRole = @"select * from  RoadFlowWebForm.[dbo].[WorkFlowTask] a join(select InstanceID,flowid,max(sort)sort from  RoadFlowWebForm.[dbo].[WorkFlowTask] 
                                                 where FlowID='a46c47ad-1e1b-47c3-a7b2-6859ea45b7d7' AND InstanceID='" + Request.QueryString["instanceid"] +
                                                "'  group by FlowID,InstanceID )b on a.FlowID=b.FlowID  AND a.InstanceID=b.InstanceID  and a.Sort=b.sort where stepid='" + Request.QueryString["stepid"] + "' ";

            var loguser = DbHelperSQL.Query(sqlRole).Tables[0];
            if (loguser.Rows.Count > 0)
            {
                Stepname = loguser.Rows[0]["StepName"].ToString();
            }
        }

        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        Session["LogUser"] = LogUserModel;
        UserId = LogUserModel.UserId;
        

        if (!IsPostBack)
        {
            DataTable ldt_detail = null;
            File_lb.SelectedIndex = 0;
            string lssql = @"select a.* from [dbo].[PGI_File_Transceiver_dtl_Form] a";

            if (this.m_sid == "")
            {
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    ApplyDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    CreateId.Text = LogUserModel.UserId;
                    CreateName.Text = LogUserModel.UserName;
                    ApplyId.Text = LogUserModel.UserId;
                    ApplyName.Text = LogUserModel.UserName;
                    ApplyDeptName.Text = LogUserModel.DepartName;
                    ApplyTelephone.Text = LogUserModel.Telephone;
                }
                //修改申请
                if (Request.QueryString["formno"] != null && state == "edit")
                {
                    //----------------------------------------------------------------------------验证存在正在申请的项目:暂时不做，选择的时候，就剔除这些数据了
//                    string sql_prd = @"select null id,'' as formno,part,''type,deliver_dept,ApplyId,ApplyName,applytelephone,applydeptname,createid,createname,''domain,pn,remark,
//                                                       deliver_user, replace(convert(nvarchar(20),GETDATE(),120),'-','/') ApplyDate,pgino,
//                                                       numid,FileType,FileName,File_Number,File_Path_Orig,File_Name_Orig,File_Serialno,Verno,Customer_Verno
//                                                     from PGI_File_Transceiver_Main main join PGI_File_Transceiver_Dtl  dtl on main.formno=dtl.formno  where main.formno='" + Request.QueryString["formno"] + "'";
                    ViewState["ApplyId_i"] = "Y";
                    string sql_prd = "exec Getverno 1,'','" + Request.QueryString["formno"] + "'";
                    DataTable dt_prd = DbHelperSQL.Query(sql_prd).Tables[0];

                    string pgino = dt_prd.Rows[0]["part"].ToString();
                    string filename = dt_prd.Rows[0]["filename"].ToString();
                    string type = dt_prd.Rows[0]["type"].ToString();
                    string filetype = dt_prd.Rows[0]["filetype"].ToString();
                    File_lb.Text = dt_prd.Rows[0]["File_lb"].ToString();

                    string re_sql = @"select * from PGI_File_Transceiver_Main_form  main join PGI_File_Transceiver_Dtl_form  dtl on main.formno=dtl.formno  where  main.formno='" + Request.QueryString["formno"] +
                                                "'  and iscomplete is null and  pgino='"+pgino+"'  and filename='"+filename+"' and type='"+type+"' ";
                    DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];
       
                    if (re_dt.Rows.Count > 0    )
                    {
                        Pgi.Auto.Public.MsgBox(this, "alert", "项目号" + pgino + "申请类别" + type + " 文件类型" + filetype + "文件名称" + filename + "正在申请中，不能修改(单号:" + re_dt.Rows[0]["formno"].ToString()
                            + ",申请人:" + re_dt.Rows[0]["ApplyId"].ToString() + "-" + re_dt.Rows[0]["ApplyName"].ToString() + ")!");
                    }
                    else
                    {
                      //  string sql_dtl="select * from PGI_File_Transceiver_dtl_Form where formno='"+ Request.QueryString["formno"] +"'";
                        DataTable ldt_head = dt_prd;
                        ldt_detail = dt_prd;
                        ASPxDropDownEdit1.Text = dt_prd.Rows[0]["deliver_dept"].ToString();
                        ASPxDropDownEdit2.Text = dt_prd.Rows[0]["deliver_user"].ToString();
                        File_lb.Text = dt_prd.Rows[0]["File_lb"].ToString();
                        File_lb.Enabled = false;
                        Cb_caigou.Style.Add("disabled", "disabled");
                        SetControlValue("PGI_File_Transceiver_main_Form", "HEAD", this.Page, ldt_head.Rows[0], "ctl00$MainContent$");

                     
                    }

                }
                else//新增申请
                {
                    lssql += " where 1=0";
                    ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
                }
            }
            else
            {
                //表头赋值
                DataTable ldt = DbHelperSQL.Query("select * from PGI_File_Transceiver_Main_Form where formno='" + this.m_sid + "'").Tables[0];
                if (ldt.Rows.Count > 0)
                {
                    SetControlValue("PGI_File_Transceiver_main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");

                    if ((StepID.ToUpper() == SQ_StepID || StepID == "A") && Request.QueryString["display"] == null)//申请步骤是，为0的字段置空
                    {
                        
                    }

                    if (ldt.Rows[0]["deliver_dept"].ToString() != "")
                    {
                        ASPxDropDownEdit1.Text = ldt.Rows[0]["deliver_dept"].ToString();
                    }
                    if (ldt.Rows[0]["deliver_user"].ToString() != "")
                    {
                        ASPxDropDownEdit2.Text = ldt.Rows[0]["deliver_user"].ToString();
                    }
                    File_lb.Text = ldt.Rows[0]["File_lb"].ToString();
                    File_lb.Enabled = false;
                    Cb_caigou.Attributes.Add("disabled", "disabled");
                    if (ldt.Rows[0]["hq_caigou"].ToString() == "Y")
                    {
                        Cb_caigou.Checked = true;
                    }
                    //显示文件
                    if (ldt.Rows[0]["files"].ToString() != "")
                    {
                        this.ip_filelist_db.Value = ldt.Rows[0]["files"].ToString();
                        bindtab();
                    }
                }
                else
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "该单号" + this.m_sid + "不存在!");
                }

                lssql += " where formno='" + this.m_sid + "' order by a.numid";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            }
            bind_grid(ldt_detail);
        }
        //else
        //{
        //    bindtab(); 
        //}


        Settype(state);
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    public void Setddl_sel_Dept()
    {
        string strSQL = @"	select distinct dept_name from V_HRM_EMP_MES ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }

    public void Setddl_gv_Dept()
    {
        string strSQL = @"	select distinct dept_name from V_HRM_EMP_MES ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).TextField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit1.FindControl("listBox")).DataBind();
    }


    public void Setddl_sel_User()
    {
        string strSQL = @"	select workcode+'-'+lastname from V_HRM_EMP_MES ORDER BY workcode ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];

        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).TextField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).ValueField = dt.Columns[0].ColumnName;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataSource = dt;
        ((ASPxListBox)ASPxDropDownEdit2.FindControl("listBox2")).DataBind();
    }

    //public string jxfiles(string files)
    //{
    //    string files_new = files;
    //    string savepath_new = @"\" + savepath + @"\";
    //    string despath = MapPath("~") + savepath_new + @"\";

    //    string[] ls_files = files.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
    //    for (int i = 0; i < ls_files.Length; i++)
    //    {
    //        string[] ls_files_oth = ls_files[i].Split(',');
    //        string oripath = MapPath("~") + ls_files_oth[1].ToString();

    //        string resultExtension = Path.GetExtension(oripath);
    //        string resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
    //        string resultFilePath = despath + resultFileName;

    //        File.Copy(oripath, resultFilePath);
    //        files_new = files.Replace(ls_files_oth[1].ToString(), savepath_new + resultFileName);//替换路径
    //    }
    //    return files_new;
    //}



    public void Set_fbdept(DataTable dt)
    {
        string sql= "select value from PGI_File_Transceiver_BaseValue where type='{0}' ";
        DataTable ldt_bm = DbHelperSQL.Query(string.Format(sql, "发放部门")).Tables[0];
        DataTable ldt_bfl = DbHelperSQL.Query(string.Format(sql, "大分类")).Tables[0];
        DataTable ldt_mfl = DbHelperSQL.Query(string.Format(sql, "中分类")).Tables[0];
        DataTable ldt_sfl = DbHelperSQL.Query(string.Format(sql, "小分类")).Tables[0];
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            ASPxComboBox tb_bm = ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["Law_Dept"], "Law_Dept"));
            tb_bm.DataSource = ldt_bm;
            tb_bm.TextField = "value";
            tb_bm.ValueField = "value";
            tb_bm.DataBind();
            tb_bm.Value = dt.Rows[i]["Law_Dept"].ToString();

            ASPxComboBox tb_Bfl = ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["B_fenlei"], "B_fenlei"));
            tb_Bfl.DataSource = ldt_bfl;
            tb_Bfl.TextField = "value";
            tb_Bfl.ValueField = "value";
            tb_Bfl.DataBind();
            tb_Bfl.Value = dt.Rows[i]["B_fenlei"].ToString();

            ASPxComboBox tb_Mfl = ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["M_fenlei"], "M_fenlei"));
            tb_Mfl.DataSource = ldt_mfl;
            tb_Mfl.TextField = "value";
            tb_Mfl.ValueField = "value";
            tb_Mfl.DataBind();
            tb_Mfl.Value = dt.Rows[i]["M_fenlei"].ToString();

            ASPxComboBox tb_Sfl = ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (GridViewDataColumn)this.gv.Columns["S_fenlei"], "S_fenlei"));
            tb_Sfl.DataSource = ldt_sfl;
            tb_Sfl.TextField = "value";
            tb_Sfl.ValueField = "value";
            tb_Sfl.DataBind();
            tb_Sfl.Value = dt.Rows[i]["S_fenlei"].ToString();

        }
    
    
    }

    //绑定申请类别
    public void Settype(string state)
    {
        type.Columns.Clear();
        DataTable gv_date = Pgi.Auto.Control.AgvToDt(this.gv);
        string lssql = @"select [Code],[Name]
                        from (
	                        select '首次发放' [Code],'首次发放' [Name],0 rownum
	                        union 
	                        select '版本更新' [Code],'版本更新' [Name],1 rownum
	                        union 
	                        select '已发放文件废除' [Code],'已发放文件废除' [Name],2 rownum
	                       
	                        ) a
                        order by rownum";
        DataTable ldt = DbHelperSQL.Query(lssql).Tables[0];
        type.ValueField = "Name";
        type.Columns.Add("Name", "描述", 80);
        type.DataSource = ldt;
        type.DataBind();
        if (state == "edit")
        {
            type.Items.RemoveAt(0);
            
        }
        else   if (StepID.ToUpper() == "A" && m_sid == "")
        {
            type.SelectedIndex = 0;
            type.Enabled = false;
        }
    }


    public void bind_grid(DataTable dt)
    {
        ((GridViewEditDataColumn)gv.Columns["deliver_date"]).PropertiesEdit.DisplayFormatString = "yyyy-MM-dd";
        ((GridViewEditDataColumn)gv.Columns["material_date"]).PropertiesEdit.DisplayFormatString = "yyyy-MM-dd";
        this.gv.DataSource = dt;
        this.gv.DataBind();
        setGridIsRead(dt, type.Text);
       
    }

    public void setGridIsRead(DataTable ldt_detail, string type)
    {
        for (int i = 9; i < 20; i++)
        {
            if (File_lb.SelectedIndex == 0)
            {
                gv.Columns[i].Visible = false;
            }
            else
            {
                gv.Columns[i].Visible = true;
            }
        }
        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            if (state != "edit")
            {
                setread(i);
            }
            setread_grid(i);
        }
      
    }

    public void setread(int i)
    {
         Remark.ReadOnly = true; ASPxDropDownEdit1.Style.Add("disabled", "disabled");
        type.Enabled = false;
        //ViewState["ApplyId_i"] = "Y";
        ASPxDropDownEdit1.ClientEnabled = false;
        ASPxDropDownEdit2.ClientEnabled = false;
       // btnadd.Visible = true;
        //btnadd.Visible = false; btndel.Visible = false;

        if (i == 0)
        {
            gv.Columns[gv.VisibleColumns.Count - 1].Visible = false;
            gv.Columns[0].Visible = false;
        }
        //if (File_lb.SelectedIndex == 0)
        //{
        //    gv.Columns[4].Visible = false;
            //gv.Columns[8].Visible = false;
            //gv.Columns[9].Visible = false;
            //gv.Columns[11].Visible = false;
            //gv.Columns[12].Visible = false;
            //gv.Columns[13].Visible = false;
        //}

       
    }
    public void setread_grid(int i)
    {
        if (Stepname == "采购")
        {
            upload_fj.Style.Add("display", "");
        }
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["FileType"], "FileType")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["FileType"], "FileType")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["FileName"], "FileName")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["FileName"], "FileName")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Path_Orig"], "File_Path_Orig")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Path_Orig"], "File_Path_Orig")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Name_Orig"], "File_Name_Orig")).ReadOnly = true;
        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Name_Orig"], "File_Name_Orig")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Number"], "File_Number")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Number"], "File_Number")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Verno"], "Verno")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Verno"], "Verno")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Customer_Verno"], "Customer_Verno")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Customer_Verno"], "Customer_Verno")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["bz"], "bz")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["bz"], "bz")).BorderStyle = BorderStyle.None;
        if (File_lb.SelectedIndex == 1)
        {
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Language"], "File_Language")).ReadOnly = true;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Language"], "File_Language")).BorderStyle = BorderStyle.None;

            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Source"], "File_Source")).ReadOnly = true;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Source"], "File_Source")).BorderStyle = BorderStyle.None;

            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Law_List"], "Law_List")).ReadOnly = true;
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Law_List"], "Law_List")).BorderStyle = BorderStyle.None;

            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Law_Dept"], "Law_Dept")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Law_Dept"], "Law_Dept")).BackColor = Color.White;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Law_Dept"], "Law_Dept")).BorderStyle = BorderStyle.None;

            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["B_fenlei"], "B_fenlei")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["B_fenlei"], "B_fenlei")).BackColor = Color.White;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["B_fenlei"], "B_fenlei")).BorderStyle = BorderStyle.None;

            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["M_fenlei"], "M_fenlei")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["M_fenlei"], "M_fenlei")).BackColor = Color.White;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["M_fenlei"], "M_fenlei")).BorderStyle = BorderStyle.None;

            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["S_fenlei"], "S_fenlei")).Enabled = false;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["S_fenlei"], "S_fenlei")).BackColor = Color.Transparent;
            ((ASPxComboBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["S_fenlei"], "S_fenlei")).BorderStyle = BorderStyle.None;

           // ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["deliver_date"], "deliver_date")).Enabled = false;
           // ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["deliver_date"], "deliver_date")).pro


            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["deliver_date"], "deliver_date")).Attributes.Remove("onclick");
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["material_date"], "material_date")).Attributes.Remove("onclick");
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["collect_date"], "collect_date")).Attributes.Remove("onclick");
            ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["expiry_date"], "expiry_date")).Attributes.Remove("onclick");
      

       
       
        }
    }

    public void setread_first(int i)
    {

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Path_Orig"], "File_Path_Orig")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Path_Orig"], "File_Path_Orig")).BorderStyle = BorderStyle.None;

        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Name_Orig"], "File_Name_Orig")).ReadOnly = true;
        //((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["File_Name_Orig"], "File_Name_Orig")).BorderStyle = BorderStyle.None;

        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Verno"], "Verno")).ReadOnly = true;
        ((ASPxTextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["Verno"], "Verno")).BorderStyle = BorderStyle.None;
     //   upload_fj.Style.Add("display", "none");

       }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        add_row(1);
    }

    protected void add_row(int lnadd_rows)
    {
        //新增一行或一组
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            for (int j = 0; j < ldt.Columns.Count; j++)
            {

                if (ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = ldt.Rows.Count <= 0 ? 1 : (Convert.ToInt32(ldt.Rows[ldt.Rows.Count - 1]["numid"]) + 1);
                }
                else if (ldt.Columns[j].ColumnName == "Verno")
                {
                    ldr[ldt.Columns[j].ColumnName] =  "A0" ;
                }
                else
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.Add(ldr);
           
        }

      
        ldt.AcceptChanges();
        this.gv.DataSource = ldt;
        this.gv.DataBind();
        if (File_lb.SelectedIndex != 0)
        { Set_fbdept(ldt);}
        if (type.Text == "首次发放")
        {
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                setread_first(i);
            }
        }
    }

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
        gv.DataSource = ldt;
        gv.DataBind();
    }


    protected void gv_DataBound(object sender, EventArgs e)
    {
      //  ScriptManager.RegisterStartupScript(this, e.GetType(), "gridcolor", "gv_color();", true);//RefreshRow();
    }


    #region 将界面中控件值统计到List中

    /// <summary>
    /// 将界面中控件值统计到List中
    /// </summary>
    /// <param name="lsform_type">要显示字段大类</param>
    /// <param name="lsform_div">要显示字段小类</param>
    /// <param name="p">要统计的界面Page</param>
    /// <param name="lscontrol_format">界面控件中要套用的控件ID格式</param>
    /// <returns></returns>
    public static List<Pgi.Auto.Common> GetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, string lscontrol_format = "")
    {
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 " + lswhere + "",
            new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

        List<Pgi.Auto.Common> ls = new List<Common>();
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            string lscontrol_id = ldt.Rows[i]["control_id"].ToString().ToLower();
            if (lscontrol_format != "")
            {
                lscontrol_id = lscontrol_format.Replace("{0}", lscontrol_id);
            }
            if (p.FindControl(lscontrol_id) != null)
            {



                Pgi.Auto.Common com = new Common();
                com.Code = ldt.Rows[i]["control_id"].ToString().ToLower();
                string lstr = "0|0";
                string initlstr = "0|0";//初始lstr字符，以便忘记配置值而报错
                //-----------------------------------控件判断开始----------------------------------
                if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
                {
                    // ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Enabled = true;
                    // com.Value = ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Text;
                    com.Value = p.Request.Form[lscontrol_id].ToString();
                    ((TextBox)p.FindControl(lscontrol_id)).Text = p.Request.Form[lscontrol_id].ToString();
                    lstr = ((TextBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXTEXTBOX")
                {
                    com.Value = p.Request.Form[lscontrol_id].ToString();
                    ((ASPxTextBox)p.FindControl(lscontrol_id)).Text = p.Request.Form[lscontrol_id].ToString();
                    lstr = ((ASPxTextBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
                {
                    com.Value = ((DropDownList)p.FindControl(lscontrol_id)).SelectedValue;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DropDownList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXCOMBOBOX")
                {
                    //com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                    com.Value = "";
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }
                        if (com.Value == "")
                        {
                            com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "RadioButtonList")
                {
                    //com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                    com.Value = "";
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((RadioButtonList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDATEEDIT")
                {
                    com.Value = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).Text;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPxDropDownEdit")
                {
                    com.Value = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).Text;
                    if (com.Value == "")
                    {
                        if (p.Request.Form[lscontrol_id] != null)
                        {
                            com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                        }

                    }
                    lstr = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                    lstr = lstr == "" ? initlstr : lstr;
                }
                //-----------------------------------控件判断结束----------------------------------

                string[] ls1 = lstr.Split('|');
                com.Key = ls1[0];
                if (ls1[1] == "1" && com.Value == "")
                {
                    com.Value = ldt.Rows[i]["control_dest"].ToString();
                    com.Code = "";
                }
                ls.Add(com);
            }
        }
        return ls;
    }

    #endregion

    #region 将TableRow栏位值赋值给页面中的控件

    //把表中值初始化给控件 added by fish:
    /// <summary>
    /// 将TableRow栏位值赋值给页面中的控件
    /// </summary>
    /// <param name="lsform_type">页面识别参数</param>
    /// <param name="lsform_div">div显示范围识别参数</param>
    /// <param name="p">page</param>
    /// <param name="dr">a DataRow</param>
    public static void SetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, DataRow dr, string lscontrolformat = "")
    {
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 and isnull(control_id,'')<>''" + lswhere + "",
            new SqlParameter[]{
                                    new SqlParameter("@form_type",lsform_type)
                                    ,new SqlParameter("@form_div",lsform_div)}).Tables[0];


        foreach (DataRow row in ldt.Rows)
        {
            if (p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower()) != null)
            {
                var columnValue = dr[row["control_id"].ToString().ToLower()].ToString();
                if (row["control_type"].ToString() == "TEXTBOX")
                {
                    ((TextBox)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower())).Text = columnValue;
                }
                else if (row["control_type"].ToString() == "ASPXTEXTBOX")
                {
                    ((ASPxTextBox)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower())).Text = columnValue;
                }
                else if (row["control_type"].ToString() == "DROPDOWNLIST")
                {
                    var drop = (DropDownList)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {
                        ListItem item = drop.Items.FindByValue(columnValue);
                        if (item != null)
                        {
                            // drop.ClearSelection();
                            //  item.Selected = true;
                            drop.SelectedValue = columnValue;
                        }
                    }
                }
                else if (row["control_type"].ToString() == "ASPXCOMBOBOX")
                {
                    var drop = (DevExpress.Web.ASPxComboBox)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {

                        drop.Value = columnValue;

                    }
                }

                else if (row["control_type"].ToString() == "ASPxListBox")
                {
                    var drop = (DevExpress.Web.ASPxListBox)p.FindControl(lscontrolformat + row["control_id"].ToString());
                    if (drop != null)
                    {
                        drop.Value = columnValue;
                    }
                }
                else if (row["control_type"].ToString() == "FILEUPLOAD")
                {
                    var upload = (FileUpload)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower());
                    var link = (HyperLink)p.FindControl(lscontrolformat + "link_" + row["control_id"].ToString().ToLower());
                    if (link != null && columnValue != "")
                    {
                        link.NavigateUrl = columnValue;
                        var name = columnValue.Substring(columnValue.LastIndexOf(@"\") + 1);
                        link.Text = name;
                        link.Target = "_blank";
                    }
                }

            }
        }
        // return ls;
    }

    #endregion



    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Doc";
    protected void uploadcontrol_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string resultExtension = System.IO.Path.GetExtension(e.UploadedFile.FileName);
        string resultFileName = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), resultExtension);
        string resultFilePath = MapPath("~") + savepath + "\\" + resultFileName;
        e.UploadedFile.SaveAs(resultFilePath);

        string name = e.UploadedFile.FileName;
        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        string sizeText = sizeInKilobytes.ToString() + " KB";

        e.CallbackData = name + "|" + "\\" + savepath + "\\" + resultFileName + "|" + sizeText;

    }
    #endregion


    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
        bool flag = false;
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
        {
            flag = true;
        }
        else
        {
            flag = SaveData("save");
        }
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
        bool flag = false;
        if (StepID.ToUpper() != "A" && StepID.ToUpper() != SQ_StepID)
        {
            flag = true;
        }
        else
        {
            flag = SaveData("submit");
        }
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

    [WebMethod]
    public static string CheckData( string applyid)
    {
        string manager_flag = "", manager_id = "";
        DataTable dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + applyid + "')").Tables[0];
       
        manager_id = dt_manager.Rows[0]["manager_id"].ToString();
        if (manager_id == "")
        {
            manager_flag += "申请人(" + applyid + ")的部门经理不存在，不能提交!<br />";
        }

        string result = "[{\"manager_flag\":\"" + manager_flag + "\"}]";
        return result;

    }

    public static void CheckData_manager(string applyid, string file_type, string spec_flag, string pgino, out string manager_flag, out string manager_id, out string file_flag)
    {
        //------------------------------------------------------------------------------验证工程师对应主管是否为空
        manager_flag = ""; file_flag = "";

        DataTable dt_manager = DbHelperSQL.Query(@"select * from [fn_Get_Managers]('" + applyid + "')").Tables[0];

        DataTable dt_file = DbHelperSQL.Query(@"SELECT special_file FROM [PGI_File_Transceiver_FileType]  WHERE FILE_TYPE='" + file_type + "'").Tables[0];
        if (spec_flag == "")
        {
            if (pgino == "" && dt_file.Rows[0][0].ToString() != "Y")
            {
                file_flag += "项目号必须选择，不能提交!<br />";
            }
        }

        manager_id = dt_manager.Rows[0]["manager_id"].ToString();

        if (manager_id == "")
        {
            manager_flag += "申请人(" + applyid + ")的部门经理不存在，不能提交!<br />";
        }

    }




    private bool SaveData(string action)
    {
        bool bflag = false;
        string serialno="";
        int mc_index = 0, hz_index = 0;
        string dept = ASPxDropDownEdit1.Text;
        string tzuser = ASPxDropDownEdit2.Text;
        string filetype="";
        int special_index = 0;//判断项目号是否必须存在
        int fjbh_index = 0; //记录文件编号是否重复
        string fjbh_double = "";
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //---------------------------------------------------------------------------------------获取表头数据----------------------------------------------------------------------------------------
        List<Pgi.Auto.Common> ls = GetControlValue("PGI_File_Transceiver_Main_Form", "HEAD", this, "ctl00$MainContent${0}");

        string applyid = ApplyId.Text;
        string applyname = ApplyName.Text;
        string pgino = part.Text; string apply_type = type.Text; //string file_type = FileType.Text;

        string manager_flag = ""; string manager_id = ""; string file_flag = "";

        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);

        DataTable dt_file = DbHelperSQL.Query("  select dept from (select str2table as dept from[StrToTable]('" + dept + "') )A where dept not in (select dept_name from V_HRM_EMP_MES )").Tables[0];
        if (dept == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('发放部门不可为空!');", true);//$(#'" + ls[i].Code + "').focus();
            return false;
        }
        else  if (dt_file.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('发放部门选择有误!');", true);//$(#'" + ls[i].Code + "').focus();
            return false;
        }


        string str_sql = "   SELECT special_file FROM [dbo].[PGI_File_Transceiver_FileType]  where special_file is null and File_Type='{0}' ";
        string str_bh = "   SELECT File_Number FROM [dbo].[PGI_File_Transceiver_Dtl]  where File_Number='{0}'";
        for (int i = 0; i < ldt.Rows.Count;i++ )
        {
            filetype = ldt.Rows[i]["filetype"].ToString();
            string file_number = ldt.Rows[i]["file_number"].ToString();
            str_sql = string.Format(str_sql,filetype);
            DataTable dt_spec = DbHelperSQL.Query(str_sql).Tables[0];
            if (dt_spec.Rows.Count > 0)
            {
                special_index = 1;
            }
            if (type.Text == "首次发放")
            {
                str_bh = string.Format(str_bh, file_number);
                DataTable dt_bh = DbHelperSQL.Query(str_bh).Tables[0];
                if (dt_bh.Rows.Count > 0)
                {
                    fjbh_index = 1;
                    if (fjbh_double != "")
                    {
                        fjbh_double = fjbh_double + ";" + file_number;
                    }
                    else
                    {
                        fjbh_double =  file_number;
                    }
                }
            }
        }
        //DataRow[] found_type = ldt.Select("filetype  in ( SELECT file_type FROM [MES].[dbo].[PGI_File_Transceiver_FileType]  where special_file is null)  ");
        if (special_index > 0 && pgino == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('项目号不可为空!');", true);//$(#'" + ls[i].Code + "').focus();
            return false;
        }

        if (fjbh_index > 0 )
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('文件编号重复,重复编号为:"+fjbh_double+"!');", true);//$(#'" + ls[i].Code + "').focus();
            return false;
        }

        //定义一个Datatable 用于存放遍历后的文件
        DataTable dtFile = new DataTable();
        dtFile.Columns.Add("filename");
        dtFile.Columns.Add("filehz");
        DataRow dr = dtFile.NewRow();
        ValidateUser connect = new ValidateUser();
        string user = "it";
        string client_ip = "ks-server";
        string password = "pgi_1234";
        bool isImpersonated = false;

        try
        {
            if (connect.impersonateValidUser(user, client_ip, password))
            {
                isImpersonated = true;
               
                    string sourcePath = @"\\172.16.5.50\OA\"+UserId;
                    if (Directory.Exists(sourcePath))
                    {
                        DirectoryInfo folder = new DirectoryInfo(sourcePath);
                        foreach (FileInfo file in folder.GetFiles())
                        {
                            string file_name = Path.GetFileNameWithoutExtension(file.FullName);//取不带路径的文件名
                            string file_hz = Path.GetFileName(file.FullName);//取不带路径的文件名
                            dr["filename"] = file_name;
                            dr["filehz"] = file_hz;
                            dtFile.Rows.Add(dr.ItemArray);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('请联系孙毅在文件服务器OA下新建工号对应的文件夹');", true);//$(#'" + ls[i].Code + "').focus();
                        return false;
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


       
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                //循环取gridview 中的datatable
                DataRow[] foundRow = dtFile.Select("FileName ='" + ldt.Rows[i]["FileName"].ToString() + "'  ");
                if (foundRow.Length < 2)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('请确认" + ldt.Rows[i]["FileName"].ToString() + "文件的原档和PDF档都存在');", true);
                    return false;
                }
                DataRow[] hzRow = dtFile.Select("filehz ='" + ldt.Rows[i][3].ToString() + ".pdf'  ");
                if (hzRow.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('请确认" + ldt.Rows[i]["FileName"].ToString() + "的PDF档存在');", true);
                    return false;

                }
            }






            if (this.m_sid == "")// && FormNo.Text == "")
            {
                //没有单号，自动生成

                string lsid = "DCC" + System.DateTime.Now.ToString("yyMMdd");
                this.m_sid = Pgi.Auto.Public.GetNo("DCC", lsid, 0, 4);
                serialno = m_sid.Substring(3);
                CheckData_manager(applyid, "", "Y", pgino, out manager_flag, out  manager_id, out  file_flag);

                for (int i = 0; i < ls.Count; i++)
                {

                    if (ls[i].Code.ToLower() == "formno")
                    {
                        ls[i].Value = this.m_sid;
                        FormNo.Text = this.m_sid;
                    }
                    if (ls[i].Code.ToLower() == "deliver_dept")
                    {
                        ls[i].Value = dept;
                    }
                    if (ls[i].Code.ToLower() == "deliver_user")
                    {
                        ls[i].Value = tzuser;
                    }
                    if (ls[i].Code.ToLower() == "hq_caigou")
                    {
                        if (Cb_caigou.Checked == true)
                        {
                            ls[i].Value = "Y";
                            break;
                        }
                    }
                }
            }
            else
            {
                serialno = m_sid.Substring(3);
                CheckData_manager(applyid, "", "Y", pgino, out manager_flag, out  manager_id, out  file_flag);
            }


          


        //经理
        Pgi.Auto.Common lcmanager_id = new Pgi.Auto.Common();
        lcmanager_id.Code = "manager_id";
        lcmanager_id.Key = "";
        lcmanager_id.Value = "u_" + manager_id;
        ls.Add(lcmanager_id);

        //自定义，上传文件
        string filepath_upload = "";
        string[] ls_files = ip_filelist.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files)
        {
            string[] ls_files_2 = item.Split(',');

            FileInfo fi = new FileInfo(MapPath("~") + ls_files_2[1]);

            var sorpath = @"\" + savepath + @"\";
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

            filepath_upload += item.Replace(sorpath, sorpath + m_sid + @"\") + "|";

        }

        string[] ls_files_db = ip_filelist_db.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ls_files_db)
        {
            filepath_upload += item + "|";
        }
        if (filepath_upload != "") { filepath_upload = filepath_upload.Substring(0, filepath_upload.Length - 1); }

        // 增加上传文件列
        Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
        lcfile.Code = "files";
        lcfile.Key = "";
        lcfile.Value = filepath_upload;
        ls.Add(lcfile);

        if (Stepname == "采购" && filepath_upload=="")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('请上传附件!');", true);
            return false;
        }

        //---------------------------------------------------------------------------------------获取表体数据----------------------------------------------------------------------------------------
     
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["formno"] = m_sid;
            ldt.Rows[i]["numid"] = (i + 1);
            ldt.Rows[i]["pgino"] = pgino;
            ldt.Rows[i]["File_Serialno"] = serialno + "-"+(i+1).ToString().PadLeft(3,'0');
         
        }


        //--------------------------------------------------------------------------产生sql------------------------------------------------------------------------------------------------
        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_File_Transceiver_Main_Form"));


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
                ls_del.Sql = "delete from PGI_File_Transceiver_Dtl_Form where formno='" + m_sid + "' and id not in(" + dtl_ids + ")";    //删除数据库中的数据不在网页上暂时出来的        
            }
            else
            {
                ls_del.Sql = "delete from PGI_File_Transceiver_Dtl_Form where formno='" + m_sid + "'";//页面上没有数据库的id，也就是所有的都是新增的，需要根据表单单号清除数据库数据
            }
            ls_sum.Add(ls_del);

            //明细数据自动生成SQL，并增入SUM
            List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_File_Transceiver_Dtl_Form", "id", "Column1,flag");
            for (int i = 0; i < ls1.Count; i++)
            {
                ls_sum.Add(ls1[i]);
            }
        }

        //-----------------------------------------------------------需要即时验证是否存在正在申请的或者保存着的项目号

        //批量提交
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);

        if (ln > 0)
        {
            bflag = true;
            string title = "";
            var titletype ="文件收发申请";
            if (pgino != "")
            {
                title = titletype + "[" + this.m_sid + "][" + pgino + "][" + apply_type + "]";
            }
            else
            {
                title = titletype + "[" + this.m_sid + "][" + apply_type + "]";
            }

            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";

        }
        else
        {
            bflag = false;
        }

        return bflag;
    }
    protected void gv_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (type.Text != "首次发放")
        //{
            if (e.RowType == GridViewRowType.Data)
            {
                string part = Convert.ToString(e.GetValue("pgino"));
                string file_serialno = Convert.ToString(e.GetValue("File_Serialno"));
                string pdfFile = Convert.ToString(e.GetValue("FileName"));
     
                    ((HyperLink)this.gv.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["hl_url"], "hl_url")).NavigateUrl = "/oa/" + ApplyId.Text + "/" + e.GetValue("FileName") + ".pdf";
                    // e.Row.Cells[5].Text = "<a href='/oa/" + UserId + "/" + e.GetValue("FileName") + ".pdf' target='_blank'>" + pdfFile + "</a>";
               
            }
        //}
    }

 [WebMethod]
    public static string GetVerno_ByFile(string file_type, string file_name)
    {
        string result = "";
        var sql = "";
        sql = @" select case when cast((right(verno,1)+1) as int)>3 then char(left(verno,1)+1)+'1' else left(verno,1)+cast(cast(right(verno,1) as int)+1 as varchar) end as  Verno 
                         ,'\\\172.16.5.50\\'+File_Serialno+'\\'+FileName as oldpath,filename as oldname
                        from PGI_File_Transceiver_Dtl where FileType='{0}' AND FileName='{1}'";

        sql = string.Format(sql, file_type, file_name);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        var ver_no = dt.Rows.Count == 0 ? "" : dt.Rows[0][0].ToString();
        var oldpath = dt.Rows.Count == 0 ? "" : dt.Rows[0][1].ToString();
        var oldname = dt.Rows.Count == 0 ? "" : dt.Rows[0][2].ToString();
        result = "[{\"ver_no\":\"" + ver_no + "\",\"oldpath\":\"" + oldpath + "\",\"oldname\":\"" + oldname + "\"}]";
        return result;

    }
 //protected void type_SelectedIndexChanged(object sender, EventArgs e)
 //{
 //    string sql_prd = "exec Getverno '" + type.SelectedItem.Text + "','" + m_sid + "'";
 //    DataSet ds_head_con = DbHelperSQL.Query(sql_prd);
 //    DataTable ldt_head = ds_head_con.Tables[0];
 //    bind_grid(ldt_head);
 //}

 protected void gv_type_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
 {
     string formno = "";
     if (Request.QueryString["formno"] != null)
     {
         formno = Request.QueryString["formno"].ToString();
     }
     string sql_prd = "exec Getverno 1,'" + type.SelectedItem.Text + "','" + formno + "'";
     DataSet ds_head_con = DbHelperSQL.Query(sql_prd);
     DataTable ldt_head = ds_head_con.Tables[0];
     bind_grid(ldt_head);
     for (int i = 0; i < ldt_head.Rows.Count; i++)
     {
         setread_grid(i);
     }
    // ViewState["ApplyId_i"] = "Y";
 }



 void Btn_Click(object sender, EventArgs e)
 {
     //var btn = sender as Button;
     var btn = sender as LinkButton;
     int index = Convert.ToInt32(btn.ID.Substring(4));

     string filedb = ip_filelist_db.Value;
     string[] ls_files = filedb.Split('|');

     string files = "";
     for (int i = 0; i < ls_files.Length; i++)
     {
         if (i != index) { files += ls_files[i] + "|"; }
     }
     if (files != "") { files = files.Substring(0, files.Length - 1); }

     ip_filelist_db.Value = files;

     bindtab();
 }

 void bindtab()
 {
     bool is_del = true;
     DataTable ldt_flow = DbHelperSQL.Query(@"select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] 
                                        where cast(stepid as varchar(36))=cast('" + Request.QueryString["stepid"] + "' as varchar(36)) and cast(flowid as varchar(36))=cast('"
                                     + Request.QueryString["flowid"] + "' as varchar(36)) and instanceid='" + this.m_sid + "' and stepname='申请人'").Tables[0];

     if (ldt_flow.Rows.Count == 0)
     {
         is_del = false;
     }
     if (Request.QueryString["display"] != null)//未发送之前
     {
         is_del = false;
     }

     tab1.Rows.Clear();
     string[] ls_files = this.ip_filelist_db.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
     for (int i = 0; i < ls_files.Length; i++)
     {
         TableRow tempRow = new TableRow();
         string[] ls_files_2 = ls_files[i].Split(',');

         HyperLink hl = new HyperLink();
         Label lb = new Label();

         hl.Text = ls_files_2[0].ToString();
         hl.NavigateUrl = ls_files_2[1].ToString();
         hl.Target = "_blank";

         lb.Text = ls_files_2[2].ToString();

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
}