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
        prtype = Server.UrlDecode(Request.QueryString["prtype"].ToString());

        if (!IsPostBack)
        {
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
        if (prtype == "刀具类")
        {
            // select a.wlh,a.wlmc,a.ms,b.pt_status,a.type
            sql = @"select * 
                    from (
                        select a.wlh,b.pt_desc1 wlmc,b.pt_desc2 ms,b.pt_status,a.type
	                        , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.domain and [pod_sched]=1 and [pod_part]=a.wlh  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                        from dbo.PGI_BASE_PART_DATA a 
	                        left join qad.dbo.qad_pt_mstr b on a.domain=b.pt_domain and a.wlh=b.pt_part
                        where a.domain='{0}' and (b.pt_status<>'DEAD' and b.pt_status<>'OBS') and RIGHT(a.wlh,1)<>'X'
                        ) aa where ispodsched=0
                    order by aa.wlh";
        }
        if (prtype == "非刀具辅料类")
        {
            sql = @"select * 
                    from (
                        select a.pt_part wlh,a.pt_desc1 wlmc,a.pt_desc2 ms,a.pt_status,a.pt_prod_line type
                             , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.pt_domain and [pod_sched]=1 and [pod_part]=a.pt_part  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                        from qad.dbo.qad_pt_mstr a
                         where a.pt_pm_code = 'P' and a.pt_part like 'Z%' and a.pt_prod_line <> '4010' and(a.pt_status <> 'DEAD' and a.pt_status <> 'OBS')                         
                             and a.pt_domain = '{0}'
                        ) aa where ispodsched=0
                    order by aa.wlh";
        }
        if (prtype == "原材料")
        {
            sql = @"select * 
                    from (
                        select a.pt_part wlh,a.pt_desc1 wlmc,a.pt_desc2 ms,a.pt_status,a.pt_prod_line type
                            , (SELECT  count(1)  FROM [qad].[dbo].[qad_pod_det] where [pod_domain]=a.pt_domain and [pod_sched]=1 and [pod_part]=a.pt_part  and getdate()<=isnull( [pod_end_eff[1]]] , getdate() )    )ispodsched 
                        from qad.dbo.qad_pt_mstr a
                            where (
                                    (a.pt_pm_code = 'P' and a.pt_part like 'P%' and a.pt_prod_line like '1%' and(a.pt_status <> 'DEAD' and a.pt_status <> 'OBS') )                                      
                                 or (a.pt_part='P0170AA')  or (a.pt_part='P0739AA-01') or (a.pt_part='P0738AA-01')  
                                )                   
                                and a.pt_domain = '{0}' 
                         ) aa where ispodsched=0
                    order by aa.wlh";
        }
        sql = string.Format(sql, Request.QueryString["domain"].ToString());

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        DataRow dr = dt.NewRow();
        dr["wlh"] = "无"; 
        dt.Rows.InsertAt(dr, 0);

        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }


}