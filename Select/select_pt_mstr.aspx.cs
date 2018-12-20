using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_pt_mstr : System.Web.UI.Page
{
    protected int nid = 0;
    public string sdomain = "";
    public string prtype = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        nid = Convert.ToInt32(Request.QueryString["id"].ToString());
        sdomain = Request.QueryString["domain"].ToString();
        prtype = Server.UrlDecode(Request.QueryString["prtype"].ToString());

        if (!IsPostBack)
        {
            if (sdomain == "昆山工厂") { sdomain = "200"; }
            else if (sdomain == "上海工厂") { sdomain = "100"; }
            this.DDL_domain.SelectedValue = sdomain;
            this.DDL_domain.Enabled = false;
        }

        QueryASPxGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }
    public void QueryASPxGridView()
    {
        string sql = "";
        if (prtype == "存货(其他辅料类)")
        {
            sql = @"select aa.pt_part,aa.pt_desc1,aa.pt_desc2,aa.pt_status,aa.pt_prod_line,aa.pt_domain
                    from qad_pt_mstr aa
                     where aa.pt_pm_code = 'P' and aa.pt_part like 'Z%' and aa.pt_prod_line <> '4010' and(aa.pt_status <> 'DEAD' and aa.pt_status <> 'OBS')                         
                         and aa.pt_domain = '{0}' and aa.pt_part like '%{1}%' and aa.pt_desc1 like '%{2}%'
                     order by aa.pt_part";//and (aa.pt_prod_line='4090' or aa.pt_prod_line='4060')
        }
        if (prtype == "存货(原材料及前期样件)")
        {
            sql = @"select aa.pt_part,aa.pt_desc1,aa.pt_desc2,aa.pt_status,aa.pt_prod_line,aa.pt_domain
                    from qad_pt_mstr aa
                     where aa.pt_pm_code = 'P' and aa.pt_part like 'P%' and aa.pt_prod_line like '1%' and(aa.pt_status <> 'DEAD' and aa.pt_status <> 'OBS')                         
                         and aa.pt_domain = '{0}' and aa.pt_part like '%{1}%' and aa.pt_desc1 like '%{2}%'
                     order by aa.pt_part";
        }

        sql = string.Format(sql, sdomain, this.txtwlh.Value.Trim(), this.txtljh.Value.Trim());

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }


}