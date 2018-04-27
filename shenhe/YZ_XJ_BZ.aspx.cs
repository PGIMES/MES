using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.DBUtility;
using System.Data;
using System.Data.SqlClient;

public partial class shenhe_YZ_XJ_BZ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["isok"].TrimEnd() == "Y")
        {
            string SQL = "SELECT TOP 1 close_remark from mes_xj_checkresult where id='" + Request["id"] + "'";
            DataTable tbl = DbHelperSQL.Query(SQL).Tables[0];
            if (tbl.Rows.Count > 0)
            {
                txtRemarks.Text = tbl.Rows[0][0].ToString();
                btnSave.Enabled = false;
                btnSave.CssClass = "btn btn-primary disabled";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        object obj1=null;
        string id = Request["id"];
        string jclb = Request["jclb"];
        string uid=Request["uid"].TrimEnd();
        string now=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          string sql1 = "insert into mes_xj_checkresult select '"+id+"',xuhao,jcxm,bzyq,sx,xx,pl,xuhao,'NA','','NA','','NA','','"+jclb+"',input_type,'"+uid+"','"+now+"','"+txtRemarks.Text+"' from mes_xj_base";
          obj1 = DbHelperSQL.GetSingle(sql1.ToString(), new SqlParameter("", ""));
          string sql = "update mes_xj_record set isok='Y' WHERE ID='" + Request["id"] + "' AND JCLB='" + jclb.TrimEnd() + "'";
          DbHelperSQL.GetSingle(sql.ToString(), new SqlParameter("", ""));
          if (obj1 == null) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('保存成功！')", true); }
    }
}