using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_PurChase_PO_Contract_Modify : System.Web.UI.Page
{
    protected string domain = "";
    protected string nbr = "";
    //protected int line = 0;

    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        nbr = Request.QueryString["nbr"].ToString();
        domain = Request.QueryString["domain"].ToString();
        //line = Convert.ToInt32(Request.QueryString["line"].ToString());

        DateTime? signdate = null;
        if (Request.QueryString["signdate"].ToString() != "") { signdate = Convert.ToDateTime(Request.QueryString["signdate"]); }

        if (!IsPostBack)
        {
            txt_domain.Text = domain;
            SysContractNo.Text = nbr;
            SignDate.Value = signdate;
            TotalPay.Text = Convert.ToDouble(Request.QueryString["ori_total_amount"].ToString()).ToString();

            QueryASPxGridView();
        }
        if (this.gv_his.IsCallback == true)//页面搜索条件使用
        {
            QueryASPxGridView();
        }
    }

    public void QueryASPxGridView()
    {
        DataSet ds = DbHelperSQL.Query(" exec [Report_PO_Contract_Modify] '" + SysContractNo.Text + "','" + txt_domain.Text + "'");
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        GetGrid(ds.Tables[0]);

        DataTable dt_his = ds.Tables[1];
        gv_his.DataSource = dt_his;
        gv_his.DataBind();


        DateTime? signdate = null;string ori_total_amount = null;
        if (ds.Tables[2].Rows.Count > 0)
        {
            if (ds.Tables[2].Rows[0]["signdate"].ToString() != "") { signdate = Convert.ToDateTime(ds.Tables[2].Rows[0]["signdate"]); }
            ori_total_amount = Convert.ToDouble(ds.Tables[2].Rows[0]["ori_total_amount"].ToString()).ToString();
        }
        else
        {
            SysContractNo.Text = "";
        }
        SignDate.Value = signdate;
        TotalPay.Text = ori_total_amount;
        
    }

    protected void gv_his_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        Pgi.Auto.Common ls_update = new Pgi.Auto.Common();
        string sql_update = @"update PUR_PO_ContractPay_Plan_His set b_flag=0,UpdateId='{2}',UpdateName='{3}',UpdateTime=getdate() 
                            where domain='{0}' and SysContractNo='{1}' and ContractLine is null and b_flag=1";
        sql_update = string.Format(sql_update, txt_domain.Text, SysContractNo.Text, LogUserModel.UserId, LogUserModel.UserName);
        ls_update.Sql = sql_update;
        ls_sum.Add(ls_update);

        Pgi.Auto.Common ls_insert = new Pgi.Auto.Common();
        string sql_insert = @"insert into PUR_PO_ContractPay_Plan_His(domain,SysContractNo, SignDate,CreateId,CreateName,CreateTime,b_flag)
                       select '{0}','{1}','{2}','{3}','{4}',getdate(),'1'";
        sql_insert = string.Format(sql_insert, txt_domain.Text, SysContractNo.Text, SignDate.Value, LogUserModel.UserId, LogUserModel.UserName);
        ls_insert.Sql = sql_insert;
        ls_sum.Add(ls_insert);

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        string msg = "";
        if (ln > 0)
        {
            msg = "确认成功！";
        }
        else
        {
            msg = "确认失败！";
        }
        //string lsstr = "layer.alert('" + msg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        string lsstr = "layer.alert('" + msg + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }

    protected void GetGrid(DataTable DT)
    {
        DataTable ldt = DT;
        int index = gv.VisibleRowCount;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {

            DevExpress.Web.ASPxDateEdit tb1 = (DevExpress.Web.ASPxDateEdit)gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv.Columns["PlanPayDate"], "PlanPayDate");
            DevExpress.Web.ASPxDateEdit tb2 = (DevExpress.Web.ASPxDateEdit)gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)gv.Columns["FPDate"], "FPDate");
            if (ldt.Rows[i]["PlanPayDate"].ToString() != "")
            {
                tb1.Date = Convert.ToDateTime(ldt.Rows[i]["PlanPayDate"].ToString());
            }

            if (ldt.Rows[i]["FPDate"].ToString() != "")
            {
                tb2.Date = Convert.ToDateTime(ldt.Rows[i]["FPDate"].ToString());
            }


            DevExpress.Web.ASPxComboBox tb_PayClause = (DevExpress.Web.ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["PayClause"], "PayClause");
            DevExpress.Web.ASPxComboBox tb_PayFunc = (DevExpress.Web.ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["PayFunc"], "PayFunc");
            DevExpress.Web.ASPxComboBox tb_PayFile = (DevExpress.Web.ASPxComboBox)gv.FindRowCellTemplateControl(i, gv.DataColumns["PayFile"], "PayFile");
            DataTable ldt_PayClause = DbHelperSQL.Query(@"select '预付款' value union select '发货款' value union select '验收款' value union select '尾款' value").Tables[0];
            DataTable ldt_PayFunc = DbHelperSQL.Query(@"select '电汇TT' value union select '信用证' value union select '承兑汇票' value union select '质量保函' value").Tables[0];
            DataTable ldt_PayFile = DbHelperSQL.Query(@"select '合同' value union select '预验收报告' value union select '终验收报告' value union select '运行情况报告' value").Tables[0];

            tb_PayClause.DataSource = ldt_PayClause;
            tb_PayClause.TextField = "value";
            tb_PayClause.ValueField = "value";
            tb_PayClause.DataBind();
            tb_PayClause.Value = ldt.Rows[i]["PayClause"].ToString();

            tb_PayFunc.DataSource = ldt_PayFunc;
            tb_PayFunc.TextField = "value";
            tb_PayFunc.ValueField = "value";
            tb_PayFunc.DataBind();
            tb_PayFunc.Value = ldt.Rows[i]["PayFunc"].ToString();

            tb_PayFile.DataSource = ldt_PayFile;
            tb_PayFile.TextField = "value";
            tb_PayFile.ValueField = "value";
            tb_PayFile.DataBind();
            tb_PayFile.Value = ldt.Rows[i]["PayFile"].ToString();
        }
    }
    

    protected void btnadd_Click(object sender, EventArgs e)
    {
        add_row(1);
    }
    protected void add_row(int lnadd_rows)
    {
        DataTable ldt_pay = Pgi.Auto.Control.AgvToDt(this.gv);
        int ln = 0;

        for (int i = 0; i < ldt_pay.Rows.Count; i++)
        {
            if (Convert.ToInt32(ldt_pay.Rows[i]["ContractLine"].ToString()) > ln)
            {
                ln = Convert.ToInt32(ldt_pay.Rows[i]["ContractLine"].ToString());
            }
        }

        DataRow ldr = ldt_pay.NewRow();
        ldr["ContractLine"] = (ln + 10).ToString();
        ldt_pay.Rows.Add(ldr);

        this.gv.DataSource = ldt_pay;
        this.gv.DataBind();
        GetGrid(ldt_pay);
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = ldt.Rows.Count - 1; i >= 0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString() == "1")
            {
                string fkamt = ldt.Rows[i]["fkamt"].ToString() == "" ? "0" : ldt.Rows[i]["fkamt"].ToString();
                if (Convert.ToDouble(fkamt) > 0)
                {
                    Pgi.Auto.Public.MsgBox(this, "alert", "行号" + ldt.Rows[i]["ContractLine"].ToString() + "已付款，不能删除!");
                }
                else
                {
                    ldt.Rows[i].Delete();
                }
            }
        }

        ldt.AcceptChanges();
        gv.DataSource = ldt;
        gv.DataBind();
        GetGrid(ldt);
    }

    protected void PayRate_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt_2 = Pgi.Auto.Control.AgvToDt(this.gv);
        double paytotal = Convert.ToDouble(TotalPay.Text);

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
        ldt_2.AcceptChanges();
        gv.DataSource = ldt_2;
        gv.DataBind();
        GetGrid(ldt_2);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        if (ldt.Rows.Count <= 0)
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "计划付款信息不可为空!");
            return;
        }
        if (SysContractNo.Text == "")
        {
            Pgi.Auto.Public.MsgBox(this, "alert", "系统合同号不可为空!");
            return;
        }

        double paytotal = Convert.ToDouble(TotalPay.Text);

        string msg="";
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["PayRate"].ToString() == "" || ldt.Rows[i]["PayMoney"].ToString() == "")
            {
                msg += "行号" + ldt.Rows[i]["ContractLine"].ToString() + "，计划付款比例不可为空!<br />";
            }

            if (ldt.Rows[i]["FPDate"].ToString() == "" && ldt.Rows[i]["FPAmount"].ToString() != "" && ldt.Rows[i]["FPAmount"].ToString() != "0")
            {
                msg += "行号" + ldt.Rows[i]["ContractLine"].ToString() + "，发票日期为空，发票金额必须也为空!<br />";
            }
            if (ldt.Rows[i]["FPDate"].ToString() != "" && ldt.Rows[i]["FPAmount"].ToString() == "")
            {
                msg += "行号" + ldt.Rows[i]["ContractLine"].ToString() + "，发票日期不为空，发票金额不可为空!<br />";
            }
        }

        if (msg != "")
        {
            Pgi.Auto.Public.MsgBox(this, "alert", msg);
            return;
        }

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        Pgi.Auto.Common ls_update = new Pgi.Auto.Common();
        string sql_update = @"update PUR_PO_ContractPay_Plan_His set b_flag=0,UpdateId='{2}',UpdateName='{3}',UpdateTime=getdate() 
                            where domain='{0}' and SysContractNo='{1}' and ContractLine is not null and b_flag=1";
        sql_update = string.Format(sql_update, txt_domain.Text, SysContractNo.Text, LogUserModel.UserId, LogUserModel.UserName);
        ls_update.Sql = sql_update;
        ls_sum.Add(ls_update);

        //明细数据自动生成SQL，并增入SUM
       
        foreach (DataRow item in ldt.Rows)
        {
            Pgi.Auto.Common ls_insert = new Pgi.Auto.Common();
            string sql = "";

            if (item["FPDate"].ToString() != "" && item["FPAmount"].ToString() != "")
            {
                string sql_insert = @"insert into PUR_PO_ContractPay_Plan_His(domain,SysContractNo,ContractLine,PlanPayDate,PayRate,PayMoney
                                            ,PayClause,PayFunc,PayFile,FPDate,FPAmount,Remark,CreateId,CreateName
                                            ,CreateTime,b_flag)
                       select '{0}','{1}','{2}','{3}','{4}','{5}'
                            ,'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}'
                            ,getdate(),'1'";
                sql = string.Format(sql_insert, txt_domain.Text, SysContractNo.Text, item["ContractLine"], item["PlanPayDate"], item["PayRate"], item["PayMoney"]
                                , item["PayClause"], item["PayFunc"], item["PayFile"], item["FPDate"], item["FPAmount"], item["Remark"], LogUserModel.UserId, LogUserModel.UserName
                                );
            }
            else
            {
                string sql_insert = @"insert into PUR_PO_ContractPay_Plan_His(domain,SysContractNo,ContractLine,PlanPayDate,PayRate,PayMoney
                                            ,PayClause,PayFunc,PayFile,Remark,CreateId,CreateName
                                            ,CreateTime,b_flag)
                       select '{0}','{1}','{2}','{3}','{4}','{5}'
                            ,'{6}','{7}','{8}','{9}','{10}','{11}'
                            ,getdate(),'1'";
                sql = string.Format(sql_insert, txt_domain.Text, SysContractNo.Text, item["ContractLine"], item["PlanPayDate"], item["PayRate"], item["PayMoney"]
                          , item["PayClause"], item["PayFunc"], item["PayFile"], item["Remark"], LogUserModel.UserId, LogUserModel.UserName
                           );
            }

            ls_insert.Sql = sql;
            ls_sum.Add(ls_insert);
        }

        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        string remsg = "";
        if (ln > 0)
        {
            remsg = "确认成功！";
        }
        else
        {
            remsg = "确认失败！";
        }
        //string lsstr = "layer.alert('" + remsg + "',function(index) {parent.layer.close(index);parent.location.reload();})";
        string lsstr = "layer.alert('" + remsg + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", lsstr, true);
    }


    [WebMethod]
    public static string check_data(string nbr, string domain)
    {
        string re_flag = "";

        DataTable dt = DbHelperSQL.Query(@"select * from PUR_PO_Contract_Status where domain='" + domain + "' and SysContractNo='" + nbr + "'").Tables[0];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                re_flag = dt.Rows[0]["Status"].ToString();
            }
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


    protected void SysContractNo_TextChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
}