using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_pur_type : System.Web.UI.Page
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
        if (prtype == "费用服务类" || prtype == "合同类")
        {
            sql = @"select id,type typedesc,type2 typedesc2 from [dbo].[PUR_PR_Category_dtl] where class='" + prtype + "'";

            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            GV_PART2.Visible = true;
            GV_PART2.DataSource = dt;
            GV_PART2.DataBind();
        }
        else
        {
            if (prtype == "刀具类")
            {
                sql = @"select name as typedesc from [PGI_BASE_PART_DDL] where CP_Line='4010' order by  ID";
            }
            if (prtype == "非刀具辅料类")
            {
                sql = @"select * ,pl_prod_line+'-'+isnull(case when len(pl_desc)-len(replace(pl_desc,'-',''))=2 then SUBSTRING(pl_desc,dbo.fn_find('-',pl_desc,2)+1 ,LEN(pl_desc)-dbo.fn_find('-',pl_desc,1))
				                    when   len(pl_desc)-len(replace(pl_desc,'-',''))=1 then substring(pl_desc,charindex('-',pl_desc)+1,len(pl_desc)-charindex('-',pl_desc)) else pl_desc
				                    end,'') as typedesc  
                            from qad.dbo.qad_pl_mstr 
                            where pl_prod_line<>'4010'and  pl_prod_line like '4%' and pl_domain='{0}'";
            }
            if (prtype == "原材料")
            {
                sql = @"select * ,pl_prod_line+'-'+isnull(case when len(pl_desc)-len(replace(pl_desc,'-',''))=2 then SUBSTRING(pl_desc,dbo.fn_find('-',pl_desc,2)+1 ,LEN(pl_desc)-dbo.fn_find('-',pl_desc,1))
				                    when   len(pl_desc)-len(replace(pl_desc,'-',''))=1 then substring(pl_desc,charindex('-',pl_desc)+1,len(pl_desc)-charindex('-',pl_desc)) else pl_desc
				                    end,'') as typedesc  
                            from qad.dbo.qad_pl_mstr 
                            where pl_prod_line like '1%' and pl_domain='{0}'";
            }

            sql = string.Format(sql, Request.QueryString["domain"].ToString());

            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            GV_PART.Visible = true;
            GV_PART.DataSource = dt;
            GV_PART.DataBind();
        }
        
    }


}