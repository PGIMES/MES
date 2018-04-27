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

public partial class Daoju_Daoju : System.Web.UI.Page
{
    string m_sid = "";
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        string FlowID = "A";
        string StepID = "A";

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }
        if (this.m_sid=="")
        {
            if (Request.QueryString["id"] != null)
            {
                this.m_sid = Request.QueryString["id"].ToString();
            }
           
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
            string lsid = "0";
            if (this.m_sid!="")
            {
                lsid = this.m_sid;
            }
            

            //特殊处理
          

            DataTable ldt_flow = DbHelperSQL.Query("select * from [RoadFlowWebForm].[dbo].[WorkFlowTask] where cast(stepid as varchar(36))=cast('" + StepID + "' as varchar(36)) and cast(flowid as varchar(36))=cast('" + FlowID + "' as varchar(36)) and instanceid='" + lsid+"'").Tables[0];
         
            if (ldt_flow.Rows.Count==1)
            {
                if (ldt_flow.Rows[0]["stepname"].ToString()!="申请")
                {
                   // this.gv.Enabled = false;
                }
                else
                {
                   // this.gv.Enabled = true;
                }
            }
           
            if (LogUserModel != null)
            {
                //当前登陆人员
                txt_LogUserId.Value = LogUserModel.UserId;
                txt_LogUserName.Value = LogUserModel.UserName;
                txt_LogUserJob.Value = LogUserModel.JobTitleName;
                txt_LogUserDept.Value = LogUserModel.DepartName;

            }
        }

        if (Session["flag"] == null)
        {
            if (this.m_sid == "")
            {
                Session["flag"] = "ADD";
            }
            else
            {
                Session["flag"] = "EDIT";
            }
        }
        DataTable ldt_detail = null;
        if (this.m_sid == "")
        {
           

            List<TableRow> ls = Pgi.Auto.Control.ShowControl("", "DAOJU_1", 3, "", "", "form-control input-s-sm");
            for (int i = 0; i < ls.Count; i++)
            {
                this.tblWLLeibie.Rows.Add(ls[i]);
            }
            //if (Session["flag"].ToString() == "ADD")
            //{
            //    GvAddRows(4, 0);
            //}
            Session["flag"] = "ROWEDIT";
            ldt_detail = this.GvToDt();
        }
        else
        {
            txt_LogUserId.Value = LogUserModel.UserId;
            txt_LogUserName.Value = LogUserModel.UserName;

            //修改
            DataTable ldt_head = DbHelperSQL.Query("select * from PGI_BASE_DAOJU_FORM where id=" + this.m_sid + "").Tables[0];
            List<TableRow> ls = Pgi.Auto.Control.ShowControl("", "DAOJU_1", 3, "", "", "form-control input-s-sm", ldt_head);
            for (int i = 0; i < ls.Count; i++)
            {
                this.tblWLLeibie.Rows.Add(ls[i]);
            }
            //特殊处理
            //((DropDownList)this.FindControl("ctl00$MainContent$op")).Items.Clear();
            //((DropDownList)this.FindControl("ctl00$MainContent$op")).SelectedValue = "";
            DataTable ldt_op = DbHelperSQL.Query("select ro_op,ro_desc from [PGIHR].report.[dbo].qad_ro_det where ro_routing='" + ((TextBox)this.FindControl("ctl00$MainContent$pgi_no")).Text + "' ").Tables[0];
            for (int i = 0; i < ldt_op.Rows.Count; i++)
            {
                ((DropDownList)this.FindControl("ctl00$MainContent$op")).Items.Add(new ListItem(ldt_op.Rows[i]["ro_op"].ToString(), ldt_op.Rows[i]["ro_op"].ToString()));
            }
                ((DropDownList)this.FindControl("ctl00$MainContent$op")).Text = ldt_head.Rows[0]["op"].ToString().Replace("OP", "");

           

            if (this.gv.VisibleRowCount == 0)
            {
                ldt_detail = DbHelperSQL.Query("select *,0 as xh from PGI_BASE_DAOJU_FORM_DT where daoju_id=" + this.m_sid + " order by daoju_no").Tables[0];
                //if (ldt_detail.Rows.Count == 0)
                //{
                //    ldt_detail = DbHelperSQL.Query("select *,0 as xh from PGI_BASE_DAOJU_FORM_DT where pgi_no='" + ldt_head.Rows[0]["pgi_no"].ToString() + "' and op='" + ldt_head.Rows[0]["op"].ToString() + "' and ver='" + ldt_head.Rows[0]["ver"].ToString() + "' order by daoju_no").Tables[0];
                //}
                if (ldt_detail.Rows.Count > 0)
                {
                    string lslen = " ";
                    string lslen1 = "";
                    string lsdaoju_no = ldt_detail.Rows[0]["daoju_no"].ToString();
                    string lsdaoju_no1 = ldt_detail.Rows[0]["daoju_no"].ToString();
                    if (ldt_detail.Rows[0]["length"].ToString().Trim() == "")
                    {
                        ldt_detail.Rows[0]["length"] = lslen;
                    }
                    for (int i = 1; i < ldt_detail.Rows.Count; i++)
                    {
                        if (ldt_detail.Rows[i]["length"].ToString().Trim() == "")
                        {
                            if (ldt_detail.Rows[i]["daoju_no"].ToString() != lsdaoju_no)
                            {
                                lsdaoju_no = ldt_detail.Rows[i]["daoju_no"].ToString();
                                lslen += " ";
                                ldt_detail.Rows[i]["length"] = lslen;
                            }
                            ldt_detail.Rows[i]["length"] = lslen;
                        }

                        //处理刀具描述，未来可以和长度合并（优化）
                        if (ldt_detail.Rows[i]["daoju_no"].ToString() != lsdaoju_no1)
                        {
                            lslen1 += " ";
                            lsdaoju_no1 = ldt_detail.Rows[i]["daoju_no"].ToString();
                        }
                        ldt_detail.Rows[i]["daoju_desc"] = ldt_detail.Rows[i]["daoju_desc"].ToString() + lslen1;

                    }
                }
            }
            else
            {
                ldt_detail = this.GvToDt();
            }


        }

        for (int i = 0; i < ldt_detail.Rows.Count; i++)
        {
            float lnedsm = 0;  //额定寿命
            float lndyl = 0;  //每把刀用量
            float lnlength = 0;  //加工长度
            float lnsmtzxs = 0;  //调整系数
            if (ldt_detail.Rows[i]["edsm"].ToString().Trim() != "" && ldt_detail.Rows[i]["dyl"].ToString().Trim() != "" && ldt_detail.Rows[i]["length"].ToString().Trim() != "" && ldt_detail.Rows[i]["smtzxs"].ToString().Trim() != "")
            {
                lnedsm = float.Parse(ldt_detail.Rows[i]["edsm"].ToString().Trim());
                lndyl = float.Parse(ldt_detail.Rows[i]["dyl"].ToString().Trim());
                lnlength = float.Parse(ldt_detail.Rows[i]["length"].ToString().Trim());
                lnsmtzxs = float.Parse(ldt_detail.Rows[i]["smtzxs"].ToString().Trim());
                ldt_detail.Rows[i]["djedsm"] = Math.Round(((lnedsm * 1000 * lndyl)) / (lnlength * lnsmtzxs), 0);
            }

        }
        if (ldt_detail.Rows.Count == 0)
        {
            GvAddRows(4, 0);
        }

        else
        {
            this.gv.DataSource = ldt_detail;
            this.gv.DataBind();
        }

       


        //复制
        if (Request.QueryString["type"]!=null)
        {
            if (Request.QueryString["type"].ToString().ToUpper()=="COPY")
            {
                 this.m_sid = "";
                ((TextBox)this.FindControl("ctl00$MainContent$ver")).Text = GetVer(((TextBox)this.FindControl("ctl00$MainContent$ver")).Text);
                ((TextBox)this.FindControl("ctl00$MainContent$form_no")).Text ="";
            }
        }
        if (this.m_sid=="")
        {
            //新建
            this.txt_CreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txt_CreateById.Value = LogUserModel.UserId;
            txt_CreateByName.Value = LogUserModel.UserName;
            txt_CreateByAd.Value = LogUserModel.ADAccount;
            txt_CreateByDept.Value = LogUserModel.DepartName;
            txt_managerid.Value = LogUserModel.ManagerId;
            txt_manager.Value = LogUserModel.ManagerName;
            txt_manager_AD.Value = LogUserModel.ManagerADAccount;
            txt_LogUserId.Value = LogUserModel.UserId;
            txt_LogUserName.Value = LogUserModel.UserName;
        }
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }

    private DataTable GvToDt()
    {
        DataTable ldt = DbHelperSQL.Query("select * from PGI_BASE_DAOJU_FORM_DT where 1=0").Tables[0];
        ldt.Columns.Add("xh");
        for (int i = 0; i < this.gv.VisibleRowCount; i++)
        {

            DataRow ldr = ldt.NewRow();

            for (int j = 0; j < ldt.Columns.Count; j++)
            {

                DevExpress.Web.GridViewDataColumn t = this.gv.Columns[ldt.Columns[j].ColumnName] as DevExpress.Web.GridViewDataColumn;
                DevExpress.Web.ASPxTextBox tb1 = (DevExpress.Web.ASPxTextBox)this.gv.FindRowCellTemplateControl(i, t, ldt.Columns[j].ColumnName);
                if (tb1 != null)
                {
                    if (tb1.Text != "")
                    {
                        ldr[ldt.Columns[j].ColumnName] = tb1.Text;
                    }
                    else
                    {
                        ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                    }

                }
                else
                {
                    if (this.gv.GetRowValues(i, ldt.Columns[j].ColumnName) != null)
                    {
                        if (this.gv.GetRowValues(i, ldt.Columns[j].ColumnName).ToString()=="")
                        {
                            ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                        }
                        else
                        {
                            ldr[ldt.Columns[j].ColumnName] = this.gv.GetRowValues(i, ldt.Columns[j].ColumnName);
                        }
                       
                    }

                }
            }
            ldt.Rows.Add(ldr);
        }

        //处理合并单元格数据
        string lsdaoju_no = "";
        string lsdaoju_desc = "";
        string lslength = "";
        if (ldt.Rows.Count > 0)
        {
            lsdaoju_no = ldt.Rows[0]["daoju_no"].ToString().Trim();
            lsdaoju_desc = ldt.Rows[0]["daoju_desc"].ToString();
            lslength = ldt.Rows[0]["length"].ToString();
        }
        for (int i = 1; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["daoju_no"] = ldt.Rows[i]["daoju_no"].ToString().Trim();
            if (ldt.Rows[i]["daoju_no"].ToString().Trim() == lsdaoju_no || ldt.Rows[i]["daoju_desc"].ToString() == lsdaoju_desc || ldt.Rows[i]["daoju_no"].ToString().Trim() == "")
            {
                ldt.Rows[i]["daoju_no"] = lsdaoju_no;
                ldt.Rows[i]["daoju_desc"] = lsdaoju_desc;
                ldt.Rows[i]["length"] = lslength;

            }
            else
            {
                lsdaoju_no = ldt.Rows[i]["daoju_no"].ToString().Trim();
                lsdaoju_desc = ldt.Rows[i]["daoju_desc"].ToString();
                lslength = ldt.Rows[i]["length"].ToString();
            }



        }


        return ldt;
    }

    private void GvAddRows(int lnadd_rows, int lnindex)
    {
        //新增一行或一组
        DataTable ldt = this.GvToDt();
        for (int i = 0; i < lnadd_rows; i++)
        {
            DataRow ldr = ldt.NewRow();
            string lsempty = "";
            for (int j = 0; j < ldt.Columns.Count; j++)
            {

                //判断合并单元格
                if (ldt.Columns[j].ColumnName == "daoju_no" || ldt.Columns[j].ColumnName == "daoju_desc" || ldt.Columns[j].ColumnName == "length")
                {
                    if (lnadd_rows == 1)
                    {
                        ldr[ldt.Columns[j].ColumnName] = ldt.Rows[lnindex][ldt.Columns[j].ColumnName];
                    }
                    else
                    {
                        ldr[ldt.Columns[j].ColumnName] = lsempty;
                    }
                }
                else
                {
                    //if (ldt.Columns[j].DataType.Name == "String")
                    //{
                    //    ldr[ldt.Columns[j].ColumnName] = lsempty;
                    //}
                    //else
                    //{
                        ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                    //}

                }

            }
            if (lnadd_rows == 1)
            {
                ldt.Rows.InsertAt(ldr, lnindex + 1);
            }
            else
            {
                ldt.Rows.Add(ldr);
            }

        }




        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ldt.Rows[i]["xh"] = i;
        }

        this.gv.DataSource = ldt;
        this.gv.DataBind();

    }





    [WebMethod]
    public static string GetGx(string lspgi_no)
    {
        string result = "[";
        DataSet ds = DbHelperSQL.Query("select ro_op,ro_desc from [PGIHR].report.[dbo].qad_ro_det where ro_routing='" + lspgi_no + "' ");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                result = result + "{\"value\":\"" + ds.Tables[0].Rows[i][0].ToString() + "\",";
                result = result + "\"text\":\"" + ds.Tables[0].Rows[i][1].ToString() + "\"},";
            }
        }
        result = result.TrimEnd(',') + "]";
        return result;

    }

    [WebMethod]
    public static string GetGxms(string lspgi_no, string lsop)
    {
        string result = "";
        DataSet ds = DbHelperSQL.Query("select ro_op,ro_desc from [PGIHR].report.[dbo].qad_ro_det where ro_routing='" + lspgi_no + "' and ro_op='" + lsop + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    result = result + "{\"value\":\"" + ds.Tables[0].Rows[i][1].ToString() + "\",";

            //}
            result = ds.Tables[0].Rows[0][1].ToString();
        }
        // result = result.TrimEnd(',') + "]";
        return result;

    }




    #region "日志"

    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM  [Q_ReView_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }
    public void bindrz2_log(string requestid, GridView gv_rz2)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM Q_ReView_LOG ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz2.DataSource = dt;
        gv_rz2.DataBind();
        gv_rz2.PageSize = 100;
    }


    #endregion



    public void MergeRows(GridView gvw, int col, string controlNameo)
    {
        //for (int col = 0; col < colnum; col++) // 遍历每一列
        //{}
        string controlName = controlNameo;// + col.ToString(); // 获取当前列需要改变的Lable控件ID
        for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex--) //GridView中获取行数 并遍历每一行
        {
            GridViewRow row = gvw.Rows[rowIndex]; // 获取当前行
            GridViewRow previousRow = gvw.Rows[rowIndex + 1]; // 获取当前行 的上一行
            Label row_lbl = row.Cells[col].FindControl(controlName) as Label; //// 获取当前列当前行 的 Lable 控件ID 的文本
            Label previousRow_lbl = previousRow.Cells[col].FindControl(controlName) as Label; //// 获取当前列当前行 的上一行 的 Lable控件ID 的文本
            if (row_lbl != null && previousRow_lbl != null) // 如果当前行 和 上一行 要改动的 Lable 的ID 的文本不为空
            {
                if (row_lbl.Text.Replace(" ", "") == previousRow_lbl.Text.Replace(" ", "")) // 如果当前行 和 上一行 要改动的 Lable 的ID 的文本不为空 且相同
                {
                    // 当前行的当前单元格（单元格跨越的行数。 默认值为 0 ） 与下一行的当前单元格的跨越行数相等且小于一 则 返回2 否则让上一行行的当前单元格的跨越行数+1
                    row.Cells[col].RowSpan = previousRow.Cells[col].RowSpan < 1 ? 2 : previousRow.Cells[col].RowSpan + 1;
                    //并让上一行的当前单元格不显示
                    previousRow.Cells[col].Visible = false;
                }
            }
        }

    }


    protected void gv_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {

        if (e.CommandArgs.CommandName == "Add")
        {
            //新增一行
            //DevExpress.Web.GridViewDataColumn t = this.gv.Columns[0] as DevExpress.Web.GridViewDataColumn;
            //DevExpress.Web.ASPxTextBox tb1 = (DevExpress.Web.ASPxTextBox)this.gv.FindRowCellTemplateControl(0, t, "daoju_no");
            GvAddRows(1, e.VisibleIndex);
            Session["flag"] = "ROWEDIT";

        }
        else if (e.CommandArgs.CommandName == "Add1")
        {
            //新增一组
            GvAddRows(4, 0);
        }
        else if (e.CommandArgs.CommandName == "JS")
        {
            Response.Write("xxxxxxx");
            Session["flag"] = "ROWEDIT";
        }
    }
    private bool SaveData()
    {
        bool bflag = false;
        string lspgi_no = "";
        string lsop = "";
        string lsdomain = "";
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        //获取表头
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("", "DAOJU_1", this, "ctl00$MainContent${0}");
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

        //特殊数据处理
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code=="domain")
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
           
        }

        //没有主键手动添加
        Pgi.Auto.Common ls_key = new Pgi.Auto.Common();
        ls_key.Code = "id";
        string lsno = "";


        //获取PGI编号和工序
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code == "pgi_no")
            {
                lspgi_no = ls[i].Value;
            }
            if (ls[i].Code == "op")
            {
                lsop = ls[i].Value;
            }
            if (ls[i].Code == "form_no")
            {
                lsno = ls[i].Value;
            }
        }

        if (this.m_sid == "")
        {
            //判断是否还有进行中的流程
            string lssql_flow = "select * from PGI_BASE_DAOJU_FORM where pgi_no='" + lspgi_no + "' and op='" + lsop + "' and domain='" + lsdomain + "' and flow_status='流程中'";
            DataTable ldt_flow = DbHelperSQL.Query(lssql_flow).Tables[0];
           
            if (ldt_flow.Rows.Count>0)
            {
                Pgi.Auto.Public.MsgBox(this.Page, "alert", "此项目还有未签核完，请勿新建！");
                return false;
            }
            //新增时，生成ID和单号
            ls_key.Value = Pgi.Auto.Public.GetNo("ALL", "");
            
            //没有单号，自动生成
             lsno = Pgi.Auto.Public.GetNo("DJ", "DJ");
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code == "form_no")
                {
                    ls[i].Value = lsno;
                    ((TextBox)this.FindControl("ctl00$MainContent$form_no")).Text = lsno;
                }

            }

            //增加主表信息
            Pgi.Auto.Common ls_create_by = new Pgi.Auto.Common();
            ls_create_by.Code = "create_by";
            ls_create_by.Key = "";
            ls_create_by.Value = txt_CreateById.Value;
            ls.Add(ls_create_by);

            Pgi.Auto.Common ls_create_date = new Pgi.Auto.Common();
            ls_create_date.Code = "create_date";
            ls_create_date.Key = "";
            ls_create_date.Value = System.DateTime.Now.ToString();
            ls.Add(ls_create_date);
        }
        else
        {
            LoginUser LogUserModel = (LoginUser)Session["LogUser"];

            //修改主表信息
            Pgi.Auto.Common ls_update_by = new Pgi.Auto.Common();
            ls_update_by.Code = "update_by";
            ls_update_by.Key = "";
            ls_update_by.Value = LogUserModel.UserId;
            ls.Add(ls_update_by);

            Pgi.Auto.Common ls_update_date = new Pgi.Auto.Common();
            ls_update_date.Code = "update_date";
            ls_update_by.Key = "";
            ls_update_date.Value = System.DateTime.Now.ToString();
            ls.Add(ls_update_date);

            ls_key.Value = this.m_sid;
        }

        ls_key.Key = "1";
        ls.Add(ls_key);

        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PGI_BASE_DAOJU_FORM"));

        //表体生成SQL
        DataTable ldt = this.GvToDt();
        //判断daoju_desc1是否为空，为空则删除
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["id"].ToString() == "" && ldt.Rows[i]["daoju_no1"].ToString() == "")
            {
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();


        //主表相关字段赋值到明细表
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            //判断组号是否为空，如果为空新增
            if (ldt.Rows[i]["pt_group_part"].ToString().Trim() == "")
            {
                ldt.Rows[i]["pt_group_part"] = lspgi_no.Substring(0, 6) + "OP" + lsop + ldt.Rows[i]["daoju_no"].ToString().Trim();
            }
            for (int j = 0; j < ls.Count; j++)
            {
                if (ls[j].Code == "pgi_no")
                {
                    ldt.Rows[i]["pgi_no"] = ls[j].Value;
                }
                if (ls[j].Code == "op")
                {
                    ldt.Rows[i]["op"] = ls[j].Value;
                }
                if (ls[j].Code == "ver")
                {
                    ldt.Rows[i]["ver"] = ls[j].Value;
                }
                if (ls[j].Code == "domain")
                {
                    ldt.Rows[i]["domain"] = ls[j].Value;
                }
                ldt.Rows[i]["daoju_id"] = ls_key.Value;
               
            }
            if (this.m_sid == "")
            {
                ldt.Rows[i]["id"] = System.DBNull.Value;
            }
        }


        //判断已有明细是否需要删除
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["id"].ToString() != "" && ldt.Rows[i]["daoju_no1"].ToString() == "")
            {
                for (int j = 0; j < ldt.Columns.Count; j++)
                {
                    if (ldt.Columns[j].ColumnName != "id")
                    {
                        ldt.Rows[i][ldt.Columns[j].ColumnName] = System.DBNull.Value;
                    }

                }
            }
        }

        //明细数据自动生成SQL，并增入SUM
        List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PGI_BASE_DAOJU_FORM_DT", "id", "xh");
        for (int i = 0; i < ls1.Count; i++)
        {
            ls_sum.Add(ls1[i]);
        }

        //批量提交
       
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        Session["flag"] = null;
        if (ln > 0)
        {
            bflag = true;
           // string instanceid = ln.ToString();
            string title = "刀具清单申请" +lspgi_no+"--"+ lsno;
            script = "$('#instanceid',parent.document).val('" + ls_key.Value + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";
          
        }
        else
        {
            bflag = false;
        }
        //if (ln > 0)
        //{
        //    bflag = true;
        //    Pgi.Auto.Public.MsgBox(Page, "alert", "保存成功!", 1);

        //}
        //else
        //{
        //    bflag = false;
        //    Pgi.Auto.Public.MsgBox(Page, "alert", "保存失败!", 1);
        //}
        return bflag;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        bool bflag = this.SaveData();
        if (bflag == true)
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存成功!", 1);

        }
        else
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存失败!", 1);
        }
    }

    protected void length_TextChanged(object sender, EventArgs e)
    {
        Session["flag"] = "ROWEDIT";
        DataTable ldt = this.GvToDt();
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            float lnedsm = 0;  //额定寿命
            float lndyl = 0;  //每把刀用量
            float lnlength = 0;  //加工长度
            float lnsmtzxs = 0;  //调整系数
            if (ldt.Rows[i]["edsm"].ToString() != "" && ldt.Rows[i]["dyl"].ToString() != "" && ldt.Rows[i]["length"].ToString() != "" && ldt.Rows[i]["smtzxs"].ToString() != "")
            {
                lnedsm = float.Parse(ldt.Rows[i]["edsm"].ToString());
                lndyl = float.Parse(ldt.Rows[i]["dyl"].ToString());
                lnlength = float.Parse(ldt.Rows[i]["length"].ToString());
                lnsmtzxs = float.Parse(ldt.Rows[i]["smtzxs"].ToString());
                ldt.Rows[i]["djedsm"] = Math.Round(((lnedsm * 1000 * lndyl)) / (lnlength * lnsmtzxs), 0);
            }

        }
        this.gv.DataSource = ldt;
        this.gv.DataBind();
    }
    private string GetVer(string lsver)
    {
        string lsver_new = "";
        String[] ls = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        for (int i = 0; i < ls.Length; i++)
        {
            if (ls[i]==lsver)
            {
                lsver_new = ls[i + 1];
                continue;
            }
        }
        return lsver_new;
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
}


