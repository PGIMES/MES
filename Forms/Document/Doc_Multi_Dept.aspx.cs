using DevExpress.Web;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Forms_Document_Doc_Multi_Dept : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strSQL = @"	select distinct dept_name,''per_num from V_HRM_EMP_MES ";
            DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
            bind_grid(dt);
        }
    }

    public void bind_grid(DataTable dt)
    {
        this.gv.DataSource = dt;
        this.gv.DataBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string ffbm = "";
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        DataRow[] foundRow = ldt.Select("per_num >0 ");
        if (foundRow.Length == 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "layer.alert('请维护发放份数');", true);
        }
        else
        {
            foreach (DataRow row in foundRow)
            {
                ffbm += row["dept_name"].ToString() + " " + row["per_num"].ToString() + "份" + ";";
            }
            string temp = @"<script>parent.setvalue_dept('" + Request.QueryString["vi"].ToString() + "','" + ffbm + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";
            Response.Write(temp.Trim());
        }
        
    }
}