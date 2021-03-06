﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Maticsoft.DBUtility;

public partial class selHisPrice : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //@windowid varchar(100) = 'emp', --开窗类别
            //@keywords varchar(50), --查询关键字
            //@pagerows int = 20, --每页行数
            //@currentpage int = 1, --当前页数
            //@total int out --总行数

            int total = 0;
            var _domain = Request["domain"] == null ? "" : Request["domain"].ToString();
            var _keyword = Request["keywords"];
            if (_keyword != null) txtKeywords.Text = _keyword;

            string sql = string.Format("exec z_SelectWindow '{0}','{1}','{2}','{3}',{4},{5}", Request["windowid"],this.txtKeywords.Text,_domain,"20","1", total );
            ViewState["sql"] = sql;
            GetData(sql);
         
        }
        if(Request["hiddenColoums"]!=null) setHiddenColunm(Request["hiddenColoums"]);
    }
    void setHiddenColunm(string columns)
    {   
        
    }
    protected string getSql(string opentype ,string sql,int currentPage)
    {
        var strB = new System.Text.StringBuilder();

        return strB.ToString();
    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        var _domain = Request["domain"] == null ? "" : Request["domain"].ToString();
        string sql = string.Format("exec z_SelectWindow '{0}','{1}','{2}','{3}',{4},{5}", Request["windowid"], this.txtKeywords.Text, _domain, "20", "1", 0);
        ViewState["sql"] = sql;
        
        GetData(ViewState["sql"].ToString());
    }
   
    private void GetData(string sql)
    {
      
        DataSet ds = new DataSet();
        ds = DbHelperSQL.Query(sql);
        var dtData = ds.Tables[0];
        var rowcounts = Convert.ToInt16(ds.Tables[1].Rows[0][0]);

        ViewState["rowcount"] = rowcounts;
        if (rowcounts <= 0)
        {
          //  lb_msg.Text = "No Data Found!";            
        }
        GridView1.DataSource = dtData;
        GridView1.DataBind();
        

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        GetData(ViewState["sql"].ToString());
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //隐藏前台抛转过来的栏位
        var columns = Request["hiddenColoums"];
        if (Request["hiddenColoums"] != null&& e.Row.RowType!=DataControlRowType.EmptyDataRow&& e.Row.RowType != DataControlRowType.Pager)
        {
            var arr = columns.Split(',');
            foreach (string col in arr)
            {
                e.Row.Cells[col.ToInt()].Visible = false;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DFE7DF';");
            e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor=currentcolor;");


        }

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        // 取得显示页数的那一列。
        GridViewRow pagerRow = GridView1.BottomPagerRow;
        if (pagerRow != null)
        {
            // 取得显示目前所在页数的 Label 控件。
            Label pageLabel = (Label)(pagerRow.Cells[0].FindControl("lblCurrentPage"));

            // 取得 第一页、上一页、下一页、最后页 的按钮。
            Button imgBtnFirst =(Button)(pagerRow.Cells[0].FindControl("imgBtnFirst"));
            Button imgBtnPrev = (Button)(pagerRow.Cells[0].FindControl("imgBtnPrev"));
            Button imgBtnNext = (Button)(pagerRow.Cells[0].FindControl("imgBtnNext"));
            Button imgBtnLast = (Button)(pagerRow.Cells[0].FindControl("imgBtnLast"));
            var num = ((TextBox)pagerRow.Cells[0].FindControl("txtPageNumber")).Text.Trim();
            var PageIndex = num==""? 0: Convert.ToInt16(num);
            // 设定何时应该显示 第一页、上一页、下一页、最后页 的按钮。
            var PageCount = Convert.ToInt16(ViewState["rowcount"]);
            if (PageIndex == 0)
            {
                imgBtnFirst.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                imgBtnPrev.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            } 
            else if (PageIndex == PageCount - 1)
            {
                imgBtnLast.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                imgBtnNext.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            }
            else if (PageCount <= 0)
            {
                imgBtnFirst.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                imgBtnPrev.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                imgBtnNext.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                imgBtnLast.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            }

            if (pageLabel != null)
            {
                // 计算目前所在的页数。
                int currentPage = PageIndex + 1;
                pageLabel.Text = currentPage.ToString() +  " / " + PageCount.ToString();
            }
        }
    }
   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var _needreturn = Request["needreturn"];
        if (_needreturn == "readonly" || _needreturn == "true")
        {
            Response.Write("<script> var index = parent.layer.getFrameIndex(window.name); parent.layer.close(index);</script>");
            return;
        };

        string ctrl0 = Request["ctrl0"];
        string ctrl1 = Request["ctrl1"];
        string ctrl2 = Request["ctrl2"];
        string ctrl3 = Request["ctrl3"];
        string ctrl4 = Request["ctrl4"];
        string keyValue0 = GridView1.SelectedRow.Cells[1].Text.Trim();
        string keyValue1=  GridView1.SelectedRow.Cells[2].Text.Trim();

        string keyValue2 = "";string js2 = "";
        if (GridView1.SelectedRow.Cells.Count > 3)
        {
            keyValue2 = GridView1.SelectedRow.Cells[3].Text.Trim();
            js2 = "parent.$(\"[id*='" + ctrl2 + "']\").val('" + keyValue2 + "');";
        }
        string keyValue3 = ""; string js3 = "";
        if (GridView1.SelectedRow.Cells.Count > 4)
        { 
            keyValue3 = GridView1.SelectedRow.Cells[4].Text.Trim();
            js3 = "parent.$(\"[id*='" + ctrl3 + "']\").val('" + keyValue3 + "');";
        }
        string keyValue4 = ""; string js4 = "";
        if (GridView1.SelectedRow.Cells.Count > 5)
        {
            keyValue4 = GridView1.SelectedRow.Cells[5].Text.Trim();
            js4 = "parent.$(\"[id*='" + ctrl4 + "']\").val('" + keyValue4 + "');";
        }

        var changeevent = "";//是否需做changes事件
        if (Request["changekey"] != null)
        {
            changeevent = "parent.$('[id*=" + Request["changekey"].ToString() + "]').change();";
        }
        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        // string temp = "<script>parent.layer.setvalue('"+ ctrl0 + "','" + keyValue0 + "','"+ ctrl1 + "','" + keyValue1 + "','"+ ctrl2 + "','" + keyValue2 + "');var index = layer.getFrameIndex(window.name);alert(index);</script>";
        string temp = "<script>"
            + "parent.$(\"[id*='" + ctrl0 + "']\").val('" + keyValue0 + "');"
            + "parent.$(\"[id*='" + ctrl1 + "']\").val('" + keyValue1 + "');"
            + js2
            + js3
            + js4
            + changeevent
            + "var index = parent.layer.getFrameIndex(window.name); parent.layer.close(index);</script>";
        Response.Write(temp.Trim());
    }

    protected void imgBtnFirst_Click(object sender, EventArgs e)
    {
        var pagerRow = GridView1.BottomPagerRow;
        var txtPageNumber = (TextBox)pagerRow.Cells[0].FindControl("txtPageNumber");
        txtPageNumber.Text = 1.ToString();
       // var PageIndex = Convert.ToInt16(txt.Text.Trim());        
       // var PageCount = Convert.ToInt16(ViewState["rowcount"]);
    }

    protected void imgBtnPrev_Click(object sender, EventArgs e)
    {

    }

    protected void imgBtnNext_Click(object sender, EventArgs e)
    {

    }

    protected void imgBtnLast_Click(object sender, EventArgs e)
    {
        var pagerRow = GridView1.BottomPagerRow;
        var txtPageNumber = (TextBox)pagerRow.Cells[0].FindControl("txtPageNumber");
        var rowcount = Convert.ToInt16(ViewState["rowcount"]);
        txtPageNumber.Text = Math.Floor(rowcount/20.0).ToString();

    }
}