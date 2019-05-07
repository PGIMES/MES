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
            sql = @"select * 
                    from (
                        select a.pt_part,a.pt_desc1,a.pt_desc2,a.pt_status,a.pt_prod_line,a.pt_domain
                             , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.pt_domain and [pod_sched]=1 and [pod_part]=a.pt_part  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                        from qad_pt_mstr a
                         where a.pt_pm_code = 'P' and a.pt_part like 'Z%' and a.pt_prod_line <> '4010' and(a.pt_status <> 'DEAD' and a.pt_status <> 'OBS')                         
                             and a.pt_domain = '{0}' and a.pt_part like '%{1}%' and a.pt_desc1 like '%{2}%'
                        ) aa where ispodsched=0
                    order by aa.pt_part";//and (a.pt_prod_line='4090' or a.pt_prod_line='4060')
        }
        if (prtype == "存货(原材料及前期样件)")
        {
            sql = @"select * 
                    from (
                        select a.pt_part,a.pt_desc1,a.pt_desc2,a.pt_status,a.pt_prod_line,a.pt_domain
                            , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.pt_domain and [pod_sched]=1 and [pod_part]=a.pt_part  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                        from qad_pt_mstr a
                            where (
                                    (a.pt_pm_code = 'P' and a.pt_part like 'P%' and a.pt_prod_line like '1%' and(a.pt_status <> 'DEAD' and a.pt_status <> 'OBS') )                                      
                                 or (a.pt_part='P0170AA')  
                                )                   
                                and a.pt_domain = '{0}' and a.pt_part like '%{1}%' and a.pt_desc1 like '%{2}%'
                         ) aa where ispodsched=0
                    order by aa.pt_part";
        }


        sql = string.Format(sql, sdomain, this.txtwlh.Value.Trim(), this.txtljh.Value.Trim());

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }


}