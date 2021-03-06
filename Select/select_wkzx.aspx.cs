﻿using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_wkzx : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }
        this.GridView1.PageSize = 20;
        if (!IsPostBack)
        {
            GetData();
        }
    }

    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        GetData();
    }

    private void GetData()
    {
        //string sql = @"select * from [172.16.5.6].[Report].[dbo].[qad_wkctr] 
        //            where domain='" + Request.QueryString["domain"] + "' and wkctr like '%" + txt_code.Text.Trim() + "%' and [desc] like '%" + txt_desc.Text.Trim() + "%' order by wkctr";
        //string sql = @"select * from [172.16.5.8].[ecology].[dbo].[qad_wc_mstr] 
        //            where wc_domain='" + Request.QueryString["domain"] + "' and wc_wkctr like '%" + txt_code.Text.Trim() + "%' and wc_desc like '%" + txt_desc.Text.Trim() + "%' order by wc_wkctr";

        //'5170','6170','7170'是财务专用，请设置工程师申请时不可见
        string sql = @"
                    select * 
                    from (
                        select a.* 
                        from [172.16.5.8].[ecology].[dbo].[qad_wc_mstr] a
	                        inner join (select * from [dbo].[PGI_GYLX_wc_relation] 
				                        where deptcode=(select deptcode from [172.16.5.6].[eHR_DB].[dbo].[View_CostCenter] where employeeid='{1}')
			                        )b on a.wc_dept=b.wc_dept
                        where wc_domain='{0}'
                        union
                        select a.* 
                        from [172.16.5.8].[ecology].[dbo].[qad_wc_mstr] a
	                        left join [dbo].[PGI_GYLX_wc_relation] b on a.wc_dept=b.wc_dept
                        where wc_domain='{0}' and b.wc_dept  is null
                        ) aa 
                    where wc_wkctr not in('5170','6170','7170') and wc_wkctr like '%{2}%' and wc_desc like '%{3}%' order by wc_wkctr";

        sql = string.Format(sql, Request.QueryString["domain"], LogUserModel.UserId, txt_code.Text.Trim(), txt_desc.Text.Trim());//, Request.QueryString["userid"]


        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GridView1.DataSource = dt;
        GridView1.DataBind();

      
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DFE7DF';");
            e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor=currentcolor;");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ls_gzzx = GridView1.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
        string ls_gzzx_desc = GridView1.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
        
        string temp = @"<script>parent.setvalue_wkzx('" + ls_gzzx + "','" + ls_gzzx_desc + "','" + Request.QueryString["vi"] + "','" + Request.QueryString["ty"] + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";
        Response.Write(temp.Trim());
    }
}