using DevExpress.Export;
using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class JiaJu_Jiaju_LY : System.Web.UI.Page
{
    public string ValidScript = "";
    public string uid = "";
    public string domain = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        Session["UserId"] = LogUserModel.UserId;
        uid = LogUserModel.UserId;
        domain = LogUserModel.Domain;
        if(!IsPostBack)
        {
            Setddl_value("领用类型", Drop_lytype);//夹具类型下拉
            Setddl_value("入库类型", Drop_rktype);//夹具类型下拉
            //Setsbno_value(Drop_lysbno);//夹具类型下拉
         //   SelJiajuno_value(Drop_Jiajuno);
          
        }
    }

    public void Setddl_value(string type, DropDownList drop)
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select * from Jiaju_Base where type='" + type + "' ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        fun.initDropDownList(drop, dt, "codevalue", "codevalue");
        drop.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    //public void Setsbno_value(DropDownList drop)//机台号
    //{
    //    BaseFun fun = new BaseFun();
    //    string strSQL = @"select * from [172.16.5.6].report.dbo.mo_code  where mo_code_key<>'' order by mo_code_key ";
    //    DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
    //    fun.initDropDownList(drop, dt, "mo_code_key", "mo_code_key");
    //    drop.Items.Insert(0, new ListItem("--请选择--", ""));
    //}

    //public void SelJiajuno_value(DropDownList drop)//夹具号
    //{
    //    BaseFun fun = new BaseFun();

    //    string strSQL = string.Format("  select  * from Jiaju_LY where isrk='N' AND substring(ck_uid,1,CHARINDEX('|',ck_uid)-1)='{0}'  ", Session["UserId"]);
    //    DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
    //    fun.initDropDownList(drop, dt, "jiajuno", "jiajuno");
    //    drop.Items.Insert(0, new ListItem("--请选择--", ""));
    //}

    //获取领用人员及姓名
    [System.Web.Services.WebMethod()]
    public static string GetUid(string uid)
    {
        string result = "";
        var sql = "";
        sql = string.Format("  select employeeid as text,employeeid+'|'+truename as value from [172.16.5.6].ehr_db.dbo.psnaccount where  (employeeid='{0}' or truename='{0}' ) ", uid);
       
        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.Rows[0][1].ToString(); }
         result = "[{\"UserId\":\"" + result + "\"}]";
        return result;
    }

    //获取零件号及名称
    [System.Web.Services.WebMethod()]
    public static string GetPN(string jiajuno)
    {
        string pn = "";
        string pnname = "";
        string result = "";
        var sql = "";
        sql = string.Format("  select pn,pn_name from JiaJu_List where jiajuno='{0}'  ", jiajuno);

        var value = DbHelperSQL.Query(sql).Tables[0];
        if (value.Rows.Count > 0)
        { pn = value.Rows[0][0].ToString();
        pnname = value.Rows[0][1].ToString(); 
        }
        result = "[{\"pn\":\"" + pn + "\",\"pnname\":\"" + pnname + "\"}]";
        return result;
    }
    protected void btn_Start_Click(object sender, EventArgs e)
    {
        bool flag = true;
        if (Rd_lytype.SelectedValue == "0")
        {
            string[] strjiajuno = txt_lyjiajuno.Text.Split(';');
            int index = strjiajuno.Length;
            string[] strpn = txt_lypn.Text.Split(';');
            string[] strpnname = txt_lyname.Text.Split(';');
            for (int i = 0; i < index-1; i++)
            {
                string sql_insert = @"insert into Jiaju_LY(jiajuno,pn,pn_name,ck_type,ck_date,ck_uid,ly_sbno,ck_desc,ISRK)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','N')";
                sql_insert += "update JiaJu_List set loc='{6}' where jiajuno='{0}'";
                sql_insert = string.Format(sql_insert, strjiajuno[i], strpn[i], strpnname[i], Drop_lytype.SelectedValue, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), txt_lyuid.Text, txt_lysbno.Text, txt_lyremark.Text);
                int result = DbHelperSQL.ExecuteSql(sql_insert);
                if (result <= 0)
                {
                    flag = false;
                }
            }
           if (flag)
           {
               Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('领用成功！')", true);
           }
        }
        else if (Rd_lytype.SelectedValue == "1")
        {
            string[] strjiajuno = txt_rkjiajuno.Text.Split(';');
            int index = strjiajuno.Length;
            string[] strpn = txt_rkpn.Text.Split(';');
            string[] strpnname = txt_rkname.Text.Split(';');
            string date = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            for (int i = 0; i < index - 1; i++)
            {
                string sql_insert = @"insert into Jiaju_RK(jiajuno,pn,pn_name,rk_type,rk_loc,rk_date,rk_uid,rk_desc)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                sql_insert += " if not exists( select top 1 * from JiaJu_List where jiajuno='{0} ') insert into JiaJu_List(jiajuno,pn,pn_name,loc,quantity,update_date) values('{0}','{1}','{2}','{4}',1,'{5}')";
                sql_insert += "  update Jiaju_LY set ISRK='Y' , rk_date='{5}' WHERE jiajuno='{0} ' and  ISRK='N'";
                sql_insert += "update JiaJu_List set loc='{4}' where jiajuno='{0}'";
                sql_insert = string.Format(sql_insert, strjiajuno[i], strpn[i], strpnname[i], Drop_rktype.SelectedValue, txt_rkloc.Text, date, txt_rkuid.Text, txt_rkremark.Text);
                int result = DbHelperSQL.ExecuteSql(sql_insert);
                if (result <= 0)
                {
                    flag = false;
                }
            }
            if (txt_rkjiajuno.Text != "" && txt_rkjiajuno.Text.IndexOf(";")<0)
            {
                string sql_insert = " if not exists( select top 1 * from JiaJu_List where jiajuno='{0} ') insert into JiaJu_List(comp,jiajuno,pn,pn_name,loc,quantity,update_date) values('{8}','{0}','{1}','{2}','{3}',1,'{4}')";
                sql_insert+="insert into Jiaju_RK(jiajuno,pn,pn_name,rk_loc,rk_date,rk_type,rk_uid,rk_desc) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
               sql_insert = string.Format(sql_insert, txt_rkjiajuno.Text,txt_rkpn.Text,txt_rkname.Text, txt_rkloc.Text, date,Drop_rktype.SelectedValue,txt_rkuid.Text,txt_rkremark.Text,domain);
               int result = DbHelperSQL.ExecuteSql(sql_insert);
               if (result <= 0)
               {
                   flag = false;
               }
            }
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('入库成功！')", true);
            }
        }
        else
        {
            string[] strjiajuno = txt_lyjiajuno.Text.Split(';');
            int lyindex = strjiajuno.Length;
            string[] strpn = txt_lypn.Text.Split(';');
            string[] strpnname = txt_lyname.Text.Split(';');

            string[] strrkjiajuno = txt_rkjiajuno.Text.Split(';');
            int rkindex = strrkjiajuno.Length;
            string[] strrkpn = txt_rkpn.Text.Split(';');
            string[] strrkpnname = txt_rkname.Text.Split(';');

            for (int i = 0; i < lyindex-1; i++)
            {
                string sql_LYinsert = @"insert into Jiaju_LY(jiajuno,pn,pn_name,ck_type,ck_date,ck_uid,ly_sbno,ck_desc,ISRK,rk_date)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','N',NULL)";
                sql_LYinsert += "update JiaJu_List set loc='{6}' where jiajuno='{0}'";
                sql_LYinsert = string.Format(sql_LYinsert, strjiajuno[i], strpn[i], strpnname[i], Drop_lytype.SelectedValue, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), txt_lyuid.Text, txt_lysbno.Text, txt_lyremark.Text);
                int ly_result = DbHelperSQL.ExecuteSql(sql_LYinsert);
                if (ly_result <= 0)
                {
                    flag = false;
                }
            }
            for (int i = 0; i < rkindex; i++)
            {
                string date = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                string sql_RKinsert = @"insert into Jiaju_RK(jiajuno,pn,pn_name,rk_type,rk_loc,rk_date,rk_uid,rk_desc)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

                sql_RKinsert += "  update Jiaju_LY set ISRK='Y',rk_date='{5}'  WHERE jiajuno='{0} ' and  ISRK='N'";
                sql_RKinsert += "update JiaJu_List set loc='{4}' where jiajuno='{0}'";
                sql_RKinsert = string.Format(sql_RKinsert, strrkjiajuno[i], strrkpn[i], strrkpnname[i], Drop_rktype.SelectedValue, txt_rkloc.Text, date, txt_rkuid.Text, txt_rkremark.Text);
                int rk_result = DbHelperSQL.ExecuteSql(sql_RKinsert);
                  if (rk_result<= 0)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('夹具号"+txt_lyjiajuno.Text+"已领用！')", true);
            }
        }
        btn_Start.CssClass = "btn btn-primary disabled";
       
       
    }

   
    //获取入库夹具号
    [System.Web.Services.WebMethod()]
    public static string GetRK_Jiaju(string uid)
    {
        string result = "";
        string strSQL = string.Format("  select '--请选择--' as text,''as value union all select   jiajuno as text,jiajuno as value from Jiaju_LY where isrk='N' AND substring(ck_uid,1,CHARINDEX('|',ck_uid)-1)='{0}'  ", uid);
        var value = DbHelperSQL.Query(strSQL).Tables[0];
        if (value.Rows.Count > 0)
        { result = value.ToJsonString(); }
        return result;
    }
}