using DevExpress.Data;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Forms_PurChase_PO_Deal_Form : System.Web.UI.Page
{
    public string UserId = "";
    public string UserName = "";
    public string DeptName = "";
    LoginUser LogUserModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        UserId = LogUserModel.UserId;
        UserName = LogUserModel.UserName;
        DeptName = LogUserModel.DepartName;

        if (!IsPostBack)
        {
            StartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            EndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            txt_pur_user.Text = UserName + "(" + UserId + ")";
        }
        QueryASPxGridView();
    }


    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec Pur_PO_Deal '{0}','{1}','{2}'";
        sql = string.Format(sql, UserName, StartDate.Text, EndDate.Text);
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        GV_PART_DK.DataSource = dt;
        GV_PART_DK.DataBind();
    }

    protected void GV_PART_DK_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }

    protected void GV_PART_DK_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        if (e.GetValue("po_status").ToString() == "作废")//删除 灰色背景色 
        {
            e.Row.Style.Add("background-color", "#FFFFFF");
            e.Row.Style.Add("font-style", "italic");
            e.Row.Style.Add("color", "#969696");
        }

    }

    [WebMethod]
    public static string deal(string ids, string ponos)
    {
        string re_flag = "";

        //判断条件
        string re_sql = @"exec Pur_PO_Deal_check '{0}'";
        re_sql = string.Format(re_sql, ids);
        DataTable re_dt = DbHelperSQL.Query(re_sql).Tables[0];

        for (int i = 0; i < re_dt.Rows.Count; i++)
        {
            re_flag = "【采购单号】" + re_dt.Rows[0]["pono"].ToString() + "【行号】" + re_dt.Rows[0]["rowid"].ToString();
            if (re_dt.Rows[0]["ms_code"].ToString() == "1")
            {
                re_flag += "【QAD订单号】" + re_dt.Rows[0]["qad_pono"].ToString();
            }
            re_flag += ":" + re_dt.Rows[0]["descs"].ToString();
        }

        if (re_flag != "")
        {
            string result_msg = "[{\"re_flag\":\"" + re_flag + "\"}]";
            return result_msg;
        }

        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        //更新PO状态为作废
        Pgi.Auto.Common ls_update_po = new Pgi.Auto.Common();
        string sql_update_po = @"update PUR_PO_Dtl_Form set status=1,zf_date=getdate() where id in({0})";
        sql_update_po = string.Format(sql_update_po, ids);
        ls_update_po.Sql = sql_update_po;
        ls_sum.Add(ls_update_po);

        //恢复PR状态
        Pgi.Auto.Common ls_update_pr = new Pgi.Auto.Common();
        string sql_update_pr = @"update PUR_PR_Dtl_Form set status=0
                                from (select * from PUR_PO_Dtl_Form where id in({0})) a 
                                where PUR_PR_Dtl_Form.prno=a.prno and PUR_PR_Dtl_Form.rowid=a.prrowid";
        sql_update_pr = string.Format(sql_update_pr, ids);
        ls_update_pr.Sql = sql_update_pr;
        ls_sum.Add(ls_update_pr);

        //更新含税总价
        Pgi.Auto.Common ls_update_po_main = new Pgi.Auto.Common();
        string sql_update_po_main = @"update PUR_PO_Main_Form set TotalPay=a.TotalPrice
                                    from (select pono,sum(TotalPrice) TotalPrice from PUR_PO_Dtl_Form where pono in({0}) and id not in({1}) group by pono) a 
                                    where PUR_PO_Main_Form.pono=a.pono";
        sql_update_po_main = string.Format(sql_update_po_main, ponos, ids);
        ls_update_po_main.Sql = sql_update_po_main;
        ls_sum.Add(ls_update_po_main);

        //若包含合同，更新合同付款比例：先修改历史修改最新记录表里，不存在这表里的，再去修改表单的表
        Pgi.Auto.Common ls_update_ht_his = new Pgi.Auto.Common();
        string sql_update_ht_his = @"update PUR_PO_ContractPay_Plan_His set PayMoney=a.TotalPrice*(PayRate/100)
                                    from (select dtl.pono,main.SysContractNo,TotalPrice 
                                        from (select pono,sum(TotalPrice) TotalPrice from PUR_PO_Dtl_Form where pono in({0}) and id not in({1}) group by pono)dtl
	                                        inner join pur_po_main_form main on dtl.PONo=main.PoNo
                                        where main.SysContractNo is not null and main.PoType='合同'
                                        ) a 
                                    where ContractLine is not null and b_flag=1 and PUR_PO_ContractPay_Plan_His.SysContractNo=a.SysContractNo";
        sql_update_ht_his = string.Format(sql_update_ht_his, ponos, ids);
        ls_update_ht_his.Sql = sql_update_ht_his;
        ls_sum.Add(ls_update_ht_his);

        Pgi.Auto.Common ls_update_ht = new Pgi.Auto.Common();
        string sql_update_ht = @"update PUR_PO_ContractPay_Form set PayMoney=a.TotalPrice*(PayRate/100)
                                from (select dtl.pono,main.SysContractNo,TotalPrice 
                                    from (select pono,sum(TotalPrice) TotalPrice from PUR_PO_Dtl_Form where pono in({0}) and id not in({1}) group by pono)dtl
	                                    inner join pur_po_main_form main on dtl.PONo=main.PoNo
                                    where main.SysContractNo is not null and main.PoType='合同'
                                        and main.SysContractNo not in(select SysContractNo from PUR_PO_ContractPay_Plan_His where ContractLine is not null and b_flag=1)
                                    ) a 
                                where PUR_PO_ContractPay_Form.PONo=a.PONo";
        sql_update_ht = string.Format(sql_update_ht, ponos, ids);
        ls_update_ht.Sql = sql_update_ht;
        ls_sum.Add(ls_update_ht);


        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        if (ln > 0)
        {
            re_flag = "作废成功！";
        }
        else
        {
            re_flag = "作废失败！";
        }

        string result = "[{\"re_flag\":\"" + re_flag + "\"}]";
        return result;

    }


}