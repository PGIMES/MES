using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using System.Text;

public partial class JiaJu_Jiaju_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            txt_date.Value = DateTime.Now.Date.ToString("yyyy/MM/dd");
            Setddl_value("夹具类型", Drop_type);//夹具类型下拉
            Setddl_value("资产所属", Drop_zc);//资产所属下拉
            Setddl_value("夹具状态", Drop_status);//夹具状态下拉
        }
    }

    public void Setddl_value(string type, DropDownList drop)
    {
        BaseFun fun = new BaseFun();
        string strSQL = @"select * from Jiaju_Base where type='"+type+"' ";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        fun.initDropDownList(drop, dt, "codevalue", "codevalue");
        drop.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    protected void btn_save_click(object sender, EventArgs e)
    {
        //判断夹具号是否已存在
        var sql = string.Format("  select jiajuno from JiaJu_List  where  jiajuno='{0}'", txt_jiajuno.Value);
        if (DbHelperSQL.Query(sql).Tables[0].Rows.Count > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('已存在夹具号'"+txt_jiajuno.Value+"'！,请确认')", true);
        }
        StringBuilder strSql = new StringBuilder();
        strSql.Append(" insert into JiaJu_List(");
        strSql.Append("  jiajuno,type,pn,pn_name,zc_bel,customer,date,loc,status)");
        strSql.Append(" values (");
        strSql.Append("  '" + txt_jiajuno.Value+ "','"+Drop_type.SelectedValue+"','"+txt_pn.Value+"','"+txt_pnname.Value+"','"+Drop_zc.SelectedValue+"','"+txt_Customer.Value+"','"+txt_date.Value+"','"+txt_loc.Value+"','"+Drop_status.SelectedValue+"');");
        int obj = DbHelperSQL.ExecuteSql(strSql.ToString());
        if (obj >0)
        {
          ScriptManager.RegisterStartupScript(btn_save, this.GetType(), "clos", "layer.alert('保存成功. ');document.getElementById('btnBack').click()", true);
        }
    
    }

    
}