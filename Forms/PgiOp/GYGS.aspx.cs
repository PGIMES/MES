﻿using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PgiOp_GYGS : System.Web.UI.Page
{
    public string ValidScript = "";
    string m_sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
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
        List<TableRow> ls = ShowControl("PGI_GYGS_Main_Form", "HEAD", 3, "", "", "lineread", "linewrite");//Pgi.Auto.Control.form-control input-s-sm
        for (int i = 0; i < ls.Count; i++)
        {
            this.tblCPXX.Rows.Add(ls[i]);
        }

        if (Request.QueryString["Option"] == null)
        {
            ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).Items.Add("压铸");
            ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).Items.Add("机加&质量");
        }
        else
        {
            ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).Items.Add("压铸");
            ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).Items.Add("机加");
            ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).Items.Add("质量");
        }


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
            string lssql = @"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYGS_Dtl_Form] a";

            if (this.m_sid == "")
            {
                //新增
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    txt_CreateById.Value = LogUserModel.UserId;
                    txt_CreateByName.Value = LogUserModel.UserName;
                    txt_CreateByDept.Value= LogUserModel.DepartName;
                    txt_CreateDate.Value = System.DateTime.Now.ToString();
                }

                lssql += " where 1=0";

            }
            else
            {
                lssql += " where GYGSNo='" + this.m_sid + "'  order by a.typeno,op";
            }
            ldt_detail = DbHelperSQL.Query(lssql).Tables[0];
            this.gv_d.DataSource = ldt_detail;
            this.gv_d.DataBind();

        }
        else
        {

            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
            this.gv_d.DataSource = ldt;
            this.gv_d.DataBind();

        }

        /*JgNum_ValueChanged(sender, e);*/
    }


    private bool SaveData()
    {
        return true;
    }


    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() == "")
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
        if (ldt_del.Rows.Count > 0)
        {
            if (Session["del"] != null)
            {
                for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
                {
                    ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
                }

            }
            Session["del"] = ldt_del;
        }
        gv_d.DataSource = ldt;
        gv_d.DataBind();
    }

    private void GvAddRows(int lnadd_rows, int lnindex)
    {
        //新增一行或一组
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
                if (ldt.Columns[j].ColumnName == "typeno" || ldt.Columns[j].ColumnName == "pgi_no")
                {
                    ldr[ldt.Columns[j].ColumnName] = ldt.Rows[lnindex][ldt.Columns[j].ColumnName];
                }
                else if(ldt.Columns[j].ColumnName == "numid")
                {
                    ldr[ldt.Columns[j].ColumnName] = dt_o.Rows.Count <= 0 ? 0 : (Convert.ToInt32(dt_o.Rows[0]["numid"]) + 1);
                }
                else if(ldt.Columns[j].ColumnName == "isbg")
                {
                    ldr[ldt.Columns[j].ColumnName] = "Y";
                }
                else 
                {
                    ldr[ldt.Columns[j].ColumnName] = DBNull.Value;
                }

            }
            ldt.Rows.InsertAt(ldr, lnindex + 1);

        }
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();

    }

    protected void gv_d_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Add")
        {
            GvAddRows(1, e.VisibleIndex);
        }
    }

    protected void gv_d_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        SetGvRow();
    }

    public void SetGvRow()
    {

        string lstypeno = ((RadioButtonList)this.FindControl("ctl00$MainContent$rbltypeno")).SelectedValue;
        string lspgi_no = ((TextBox)this.FindControl("ctl00$MainContent$pgi_no")).Text;
        string lsdomain = txt_domain.Text; //((TextBox)this.FindControl("ctl00$MainContent$domain")).Text;
        if (lstypeno == "" || lspgi_no == "")
        {
            return;
        }

        DataTable ldt = DbHelperSQL.Query(@"select a.*,ROW_NUMBER() OVER (ORDER BY UpdateDate) numid from [dbo].[PGI_GYGS_Dtl_Form] a where 1=0").Tables[0];
        if (lstypeno == "压铸")
        {
            DataTable dt_gx = null;
            dt_gx = DbHelperSQL.Query(@"select ro_op,ro_desc,ro_wkctr from [PGIHR].report.[dbo].qad_ro_det where ro_routing='" + lspgi_no + "' and ro_domain='" + lsdomain + "' order by ro_op").Tables[0];
            for (int i = 0; i < dt_gx.Rows.Count; i++)
            {
                DataRow ldr = ldt.NewRow();
                ldr["typeno"] = lstypeno;
                ldr["pgi_no"] = lspgi_no;
                ldr["op"] = "OP" + dt_gx.Rows[i]["ro_op"];
                ldr["op_desc"] = dt_gx.Rows[i]["ro_desc"];
                ldr["op_remark"] = dt_gx.Rows[i]["ro_desc"];
                //ldr["gzzx"] = dt_gx.Rows[i]["ro_wkctr"];
                ldr["isbg"] = "Y";
                ldr["numid"] = i;
                ldt.Rows.Add(ldr);
            }
        }
        else
        {
            for (int i = 1; i <= 7; i++)
            {
                DataRow ldr = ldt.NewRow();
                ldr["typeno"] = "机加";
                ldr["pgi_no"] = lspgi_no;
                ldr["op"] = "OP1" + i.ToString() + "0";
                ldr["isbg"] = "Y";
                ldr["numid"] = i;

                ldt.Rows.Add(ldr);
            }

            DataRow ldr_z1 = ldt.NewRow();
            ldr_z1["typeno"] = "质量";
            ldr_z1["pgi_no"] = lspgi_no;
            ldr_z1["op"] = "OP600";
            ldr_z1["isbg"] = "Y";
            ldr_z1["numid"] = 8;
            ldt.Rows.Add(ldr_z1);

            DataRow ldr_z2 = ldt.NewRow();
            ldr_z2["typeno"] = "质量";
            ldr_z2["pgi_no"] = lspgi_no;
            ldr_z2["op"] = "OP700";
            ldr_z2["isbg"] = "Y";
            ldr_z2["numid"] = 9;
            ldt.Rows.Add(ldr_z2);



            #region 赋值常量1
            if (lspgi_no == "P0322AA")
            {
                ldt.Rows.Clear();
                for (int i = 1; i <= 7; i++)
                {
                    DataRow ldr = ldt.NewRow();
                    ldr["typeno"] = "机加";
                    ldr["pgi_no"] = lspgi_no;
                    ldr["op"] = "OP1" + i.ToString() + "0";
                    ldr["numid"] = i;

                    if (i == 1)
                    {
                        ldr["op_desc"] = "CNC机加"; ldr["op_remark"] = "铣面"; ldr["gzzx_desc"] = "KIWA加工中心"; ldr["gzzx"] = "6110";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "4"; ldr["JgSec"] = "86"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "10"; ldr["JtNum"] = "2";
                        ldr["TjOpSec"] = "24.00"; ldr["JSec"] = "12.00"; ldr["JHour"] = "0.00666667";
                        ldr["col1"] = "0.5"; ldr["col2"] = "2"; ldr["col3"] = "1530"; ldr["col4"] = "3060"; ldr["col5"] = "3060";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }

                    if (i == 2)
                    {
                        ldr["op_desc"] = "CNC机加"; ldr["op_remark"] = "锪面、钻孔"; ldr["gzzx_desc"] = "创胜加工中心"; ldr["gzzx"] = "6100";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "16"; ldr["JgSec"] = "256"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "128"; ldr["JtNum"] = "2";
                        ldr["TjOpSec"] = "24.00"; ldr["JSec"] = "12.00"; ldr["JHour"] = "0.00666667";
                        ldr["col1"] = "0.5"; ldr["col2"] = "2"; ldr["col3"] = "1530"; ldr["col4"] = "3060"; ldr["col5"] = "3060";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 3)
                    {
                        ldr["op_desc"] = "CNC机加"; ldr["op_remark"] = "铣面、扩孔、镗半圆"; ldr["gzzx_desc"] = "创胜加工中心"; ldr["gzzx"] = "6110";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "12"; ldr["JgSec"] = "566"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "10"; ldr["JtNum"] = "4";
                        ldr["TjOpSec"] = "48.00"; ldr["JSec"] = "12.00"; ldr["JHour"] = "0.01333333";
                        ldr["col1"] = "0.5"; ldr["col2"] = "2"; ldr["col3"] = "765"; ldr["col4"] = "1530"; ldr["col5"] = "3060";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 4)
                    {
                        ldr["op_desc"] = "分选检测"; ldr["op_remark"] = "分选测量去毛刺"; ldr["gzzx_desc"] = "去毛刺测量专机"; ldr["gzzx"] = "6070";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "1"; ldr["JgSec"] = "10"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "0"; ldr["JtNum"] = "1";
                        ldr["TjOpSec"] = "9.60"; ldr["JSec"] = "9.60"; ldr["JHour"] = "0.00266667";
                        ldr["col1"] = "0"; ldr["col2"] = "1"; ldr["col3"] = "3825"; ldr["col4"] = "3825"; ldr["col5"] = "3825";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 5)
                    {
                        ldr["op_desc"] = "清洗"; ldr["op_remark"] = "清洗"; ldr["gzzx_desc"] = "清洗机"; ldr["gzzx"] = "6080";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "1"; ldr["JgSec"] = "8"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "0"; ldr["JtNum"] = "1";
                        ldr["TjOpSec"] = "7.68"; ldr["JSec"] = "7.68"; ldr["JHour"] = "0.00213333";
                        ldr["col1"] = "0"; ldr["col2"] = "1"; ldr["col3"] = "4871"; ldr["col4"] = "4871"; ldr["col5"] = "4871";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 6)
                    {
                        ldr["op_desc"] = "外观检"; ldr["op_remark"] = "外观检"; ldr["gzzx_desc"] = "铁件检验中心"; ldr["gzzx"] = "6160";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "1"; ldr["JgSec"] = "8"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "0"; ldr["JtNum"] = "1";
                        ldr["TjOpSec"] = "7.68"; ldr["JSec"] = "7.68"; ldr["JHour"] = "0.00213333";
                        ldr["col1"] = "1"; ldr["col2"] = "1"; ldr["col3"] = "4871"; ldr["col4"] = "4871"; ldr["col5"] = "4871";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 7)
                    {
                        ldr["op_desc"] = "装箱"; ldr["op_remark"] = "装箱"; ldr["gzzx_desc"] = "人工（铁）"; ldr["gzzx"] = "6170";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "1"; ldr["JgSec"] = "8"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "0"; ldr["JtNum"] = "1";
                        ldr["TjOpSec"] = "7.68"; ldr["JSec"] = "7.68"; ldr["JHour"] = "0.00213333";
                        ldr["col1"] = "1"; ldr["col2"] = "1"; ldr["col3"] = "4871"; ldr["col4"] = "4871"; ldr["col5"] = "4871";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    ldt.Rows.Add(ldr);
                }

                DataRow ldr_z1_032aa = ldt.NewRow();
                ldr_z1_032aa["typeno"] = "质量";
                ldr_z1_032aa["pgi_no"] = lspgi_no;
                ldr_z1_032aa["op"] = "OP600";
                ldr_z1_032aa["numid"] = 8;
                ldt.Rows.Add(ldr_z1_032aa);

                DataRow ldr_z2_032aa = ldt.NewRow();
                ldr_z2_032aa["typeno"] = "质量";
                ldr_z2_032aa["pgi_no"] = lspgi_no;
                ldr_z2_032aa["op"] = "OP700";
                ldr_z2_032aa["numid"] = 9;
                ldt.Rows.Add(ldr_z2_032aa);
            }
            #endregion

            #region 赋值常量2
            if (lspgi_no == "P0656AA")
            {
                ldt.Rows.Clear();

                DataRow ld = ldt.NewRow();
                ld["typeno"] = "机加";
                ld["pgi_no"] = lspgi_no;
                ld["op"] = "OP105";
                ld["numid"] = 0;

                ld["op_desc"] = "激光打码"; ld["op_remark"] = "激光打码"; ld["gzzx_desc"] = "打标机"; ld["gzzx"] = "5140";
                ld["IsBg"] = "Y"; ld["JgNum"] = "2"; ld["JgSec"] = "24"; ld["WaitSec"] = "53"; ld["ZjSecc"] = "16"; ld["JtNum"] = "2";
                ld["TjOpSec"] = "46.50"; ld["JSec"] = "23.25"; ld["JHour"] = "0.012916667";
                ld["col1"] = "0.33"; ld["col2"] = "1"; ld["col3"] = "790"; ld["col4"] = "790"; ld["col5"] = "1579";
                //ld["FinishHour"] = Convert.ToDecimal(ld["JHour"]) * Convert.ToDecimal(ld["col1"]) * Convert.ToDecimal(ld["col4"]);
                ldt.Rows.Add(ld);

                for (int i = 1; i <= 5; i++)
                {
                    DataRow ldr = ldt.NewRow();
                    ldr["typeno"] = "机加";
                    ldr["pgi_no"] = lspgi_no;
                    ldr["op"] = "OP1" + i.ToString() + "0";
                    ldr["numid"] = i;

                    if (i == 1)
                    {
                        ldr["op_desc"] = "铣面、扩孔"; ldr["op_remark"] = "铣面、扩孔"; ldr["gzzx_desc"] = "brother+4TH"; ldr["gzzx"] = "5020";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "2"; ldr["JgSec"] = "154"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "32"; ldr["JtNum"] = "4";
                        ldr["TjOpSec"] = "93.00"; ldr["JSec"] = "23.25"; ldr["JHour"] = "0.025833333";
                        ldr["col1"] = "0.17"; ldr["col2"] = "2"; ldr["col3"] = "395"; ldr["col4"] = "790"; ldr["col5"] = "1579";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }

                    if (i == 2)
                    {
                        ldr["op_desc"] = "铣面、扩孔、攻丝"; ldr["op_remark"] = "铣面、扩孔、攻丝"; ldr["gzzx_desc"] = "brother+4TH"; ldr["gzzx"] = "5020";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "2"; ldr["JgSec"] = "154"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "32"; ldr["JtNum"] = "4";
                        ldr["TjOpSec"] = "93.00"; ldr["JSec"] = "23.25"; ldr["JHour"] = "0.025833333";
                        ldr["col1"] = "0.17"; ldr["col2"] = "2"; ldr["col3"] = "395"; ldr["col4"] = "790"; ldr["col5"] = "1579";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 3)
                    {
                        ldr["op_desc"] = "清洗"; ldr["op_remark"] = "清洗"; ldr["gzzx_desc"] = "清洗机"; ldr["gzzx"] = "5041";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "24"; ldr["JgSec"] = "240"; ldr["WaitSec"] = "0"; ldr["ZjSecc"] = "0"; ldr["JtNum"] = "1";
                        ldr["TjOpSec"] = "10.00"; ldr["JSec"] = "10.00"; ldr["JHour"] = "0.002777778";
                        ldr["col1"] = "2"; ldr["col2"] = "0.5"; ldr["col3"] = "3672"; ldr["col4"] = "1836"; ldr["col5"] = "3672";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 4)
                    {
                        ldr["op_desc"] = "压装"; ldr["op_remark"] = "压装"; ldr["gzzx_desc"] = "压装机"; ldr["gzzx"] = "5032";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "1"; ldr["JgSec"] = "20"; ldr["WaitSec"] = "16.5"; ldr["ZjSecc"] = "10"; ldr["JtNum"] = "2";
                        ldr["TjOpSec"] = "46.50"; ldr["JSec"] = "23.25"; ldr["JHour"] = "0.012916667";
                        ldr["col1"] = "0.25"; ldr["col2"] = "2"; ldr["col3"] = "790"; ldr["col4"] = "1580"; ldr["col5"] = "1579";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }
                    if (i == 5)
                    {
                        ldr["op_desc"] = "测漏"; ldr["op_remark"] = "测漏"; ldr["gzzx_desc"] = "测漏机"; ldr["gzzx"] = "5051";
                        ldr["IsBg"] = "Y"; ldr["JgNum"] = "2"; ldr["JgSec"] = "45"; ldr["WaitSec"] = "28"; ldr["ZjSecc"] = "20"; ldr["JtNum"] = "2";
                        ldr["TjOpSec"] = "46.50"; ldr["JSec"] = "23.25"; ldr["JHour"] = "0.012916667";
                        ldr["col1"] = "0.25"; ldr["col2"] = "2"; ldr["col3"] = "790"; ldr["col4"] = "1580"; ldr["col5"] = "1579";
                        //ldr["FinishHour"] = Convert.ToDecimal(ldr["JHour"]) * Convert.ToDecimal(ldr["col1"]) * Convert.ToDecimal(ldr["col4"]);
                    }



                    ldt.Rows.Add(ldr);
                }

                DataRow ldr_z = ldt.NewRow();
                ldr_z["typeno"] = "质量";
                ldr_z["pgi_no"] = lspgi_no;
                ldr_z["op"] = "OP600";
                ldr_z["numid"] = 8;

                ldr_z["op_desc"] = "最终检验"; ldr_z["op_remark"] = "外观检验"; ldr_z["gzzx_desc"] = "检验台"; ldr_z["gzzx"] = "5060";
                ldr_z["IsBg"] = "Y"; ldr_z["JgNum"] = "1"; ldr_z["JgSec"] = "15"; ldr_z["WaitSec"] = "0"; ldr_z["ZjSecc"] = "0"; ldr_z["JtNum"] = "1";
                ldr_z["TjOpSec"] = "15.00"; ldr_z["JSec"] = "15.00"; ldr_z["JHour"] = "0.004166667";
                ldr_z["col1"] = "1"; ldr_z["col2"] = "1"; ldr_z["col3"] = "2448"; ldr_z["col4"] = "2448"; ldr_z["col5"] = "2448";
                //ldr_z["FinishHour"] = Convert.ToDecimal(ldr_z["JHour"]) * Convert.ToDecimal(ldr_z["col1"]) * Convert.ToDecimal(ldr_z["col4"]);

                ldt.Rows.Add(ldr_z);

                DataRow ldr_z22 = ldt.NewRow();
                ldr_z22["typeno"] = "质量";
                ldr_z22["pgi_no"] = lspgi_no;
                ldr_z22["op"] = "OP700";
                ldr_z22["numid"] = 9;
                ldt.Rows.Add(ldr_z22);

                ldr_z22["op_desc"] = " GP12检验"; ldr_z22["op_remark"] = "替代客户端检验"; ldr_z22["gzzx_desc"] = "检验台"; ldr_z22["gzzx"] = "5060";
                ldr_z22["IsBg"] = "Y"; ldr_z22["JgNum"] = "1"; ldr_z22["JgSec"] = "15"; ldr_z22["WaitSec"] = "0"; ldr_z22["ZjSecc"] = "0"; ldr_z22["JtNum"] = "1";
                ldr_z22["TjOpSec"] = "15.00"; ldr_z22["JSec"] = "15.00"; ldr_z22["JHour"] = "0.004166667";
                ldr_z22["col1"] = "1"; ldr_z22["col2"] = "1"; ldr_z22["col3"] = "2448"; ldr_z22["col4"] = "2448"; ldr_z22["col5"] = "2448";
                //ldr_z22["FinishHour"] = Convert.ToDecimal(ldr_z22["JHour"]) * Convert.ToDecimal(ldr_z22["col1"]) * Convert.ToDecimal(ldr_z22["col4"]);
            }
            #endregion



        }

        gv_d.DataSource = ldt;
        gv_d.DataBind();
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
        bool flag = SaveData();
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

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
                if (lscontrol_style != "")
                {
                    ltxt.CssClass = lscontrol_style;
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

            //RadioButtonList控件
            else if (ldt.Rows[i]["control_type"].ToString() == "RadioButtonList")
            {
                #region "CheckBoxList"
                RadioButtonList ltxt = new RadioButtonList();
                HiddenField htxt = new HiddenField();
                htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                ltxt.AutoPostBack = false;
                ltxt.ID = "rbl" + ldt.Rows[i]["control_id"].ToString().ToLower();
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

    /*
    //测试数据

    protected void JgNum_ValueChanged(object sender, EventArgs e)
    {
        update_gv_d();
    }

    public void update_gv_d()
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            string jgnum = ldt.Rows[i]["jgnum"].ToString().Trim(); if (jgnum == "") { jgnum = "0"; }//每次加工数量
            string JgSec = ldt.Rows[i]["JgSec"].ToString().Trim(); if (JgSec == "") { JgSec = "0"; }
            string WaitSec = ldt.Rows[i]["WaitSec"].ToString().Trim(); if (WaitSec == "") { WaitSec = "0"; }
            string ZjSecc = ldt.Rows[i]["ZjSecc"].ToString().Trim(); if (ZjSecc == "") { ZjSecc = "0"; }
            string JtNum = ldt.Rows[i]["JtNum"].ToString().Trim(); if (JtNum == "") { JtNum = "0"; }

            if (jgnum == "0") { ldt.Rows[i]["TjOpSec"] = Math.Round(0.00, 2); }//单台单件工序工时(秒)TjOpSec
            else
            {
                ldt.Rows[i]["TjOpSec"] = Math.Round((Convert.ToDecimal(JgSec) + Convert.ToDecimal(WaitSec) + Convert.ToDecimal(ZjSecc)) / Convert.ToDecimal(jgnum), 2);
            }

            if (JtNum == "0") { ldt.Rows[i]["JSec"] = Math.Round(0.00, 2); }//单件工时(秒)
            else
            {
                ldt.Rows[i]["JSec"] = Math.Round(Convert.ToDecimal(ldt.Rows[i]["TjOpSec"]) / Convert.ToDecimal(JtNum), 2);
            }
            ldt.Rows[i]["JHour"] = Math.Round(Convert.ToDecimal(ldt.Rows[i]["TjOpSec"]) / Convert.ToDecimal(3600), 5);//单件工时(时)


            if (Convert.ToDecimal(ldt.Rows[i]["TjOpSec"].ToString()) == 0) { ldt.Rows[i]["col3"] = 0; }//单台85%产量
            else
            {
                ldt.Rows[i]["col3"] = Math.Round((12 * 60 * 60) / Convert.ToDecimal(ldt.Rows[i]["TjOpSec"]) * Convert.ToDecimal(0.85), 0);
            }

            if (ldt.Rows[i]["col2"].ToString() == "") { ldt.Rows[i]["col4"] = 0; }//一人85%产量
            else
            {
                ldt.Rows[i]["col4"] = Math.Round(Convert.ToDecimal(ldt.Rows[i]["col2"]) * Convert.ToDecimal(ldt.Rows[i]["col3"]), 0);
            }

            if (Convert.ToDecimal(ldt.Rows[i]["JSec"].ToString()) == 0) { ldt.Rows[i]["col5"] = 0; }//整线班产量
            else
            {
                ldt.Rows[i]["col5"] = Math.Round((12 * 60 * 60) / Convert.ToDecimal(ldt.Rows[i]["JSec"]) * Convert.ToDecimal(0.85), 0);
            }

            //if (ldt.Rows[i]["col1"].ToString() == "") { ldt.Rows[i]["FinishHour"] = Math.Round(0.00, 2); }
            //else
            //{
            //    ldt.Rows[i]["FinishHour"] = Math.Round(Convert.ToDecimal(ldt.Rows[i]["JHour"]) * Convert.ToDecimal(ldt.Rows[i]["col1"]) * Convert.ToDecimal(ldt.Rows[i]["col4"]), 2);//完工工时
            //}
        }
        ldt.AcceptChanges();
        this.gv_d.DataSource = ldt;
        this.gv_d.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //表体生成SQL
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv_d);
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

        }
    }
    */
}