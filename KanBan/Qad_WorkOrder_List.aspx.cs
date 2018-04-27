using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using IBatisNet.Common.Transaction;
using System.Web.Script.Services;
using System.Web.Services;
using MES.Model;
using MES.DAL;
using Maticsoft.Common;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Globalization;

public partial class KanBan_Qad_WorkOrder_List : System.Web.UI.Page
{
      
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";

        // this.gv_rz2.PageSize = 100;

        //                                              02128  00404      00076  01968  
        //LoginUser LogUserModel = InitUser.GetLoginUserInfo("02069", Request.ServerVariables["LOGON_USER"]);
        //Session["LogUser"] = LogUserModel;

        if (!IsPostBack)
        {

            this.GetData();

        }
        else
        {
        //    for (int i = 0; i < this.gv.Rows.Count; i++)
        //    {
        //        GridViewRow row = (GridViewRow)this.gv.Rows[i];
        //        for (int j = 1; j <= row.Cells.Count - 1; j++)
        //        {
        //            LinkButton lbtn = new LinkButton();
        //            lbtn.ID = "btnM" + j.ToString();
        //            lbtn.Text = this.gv.Rows[i].Cells[j].Text;
        //            lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
        //            lbtn.Attributes.Add("name", "C");
        //            string strName = this.gv.Columns[j].HeaderText;//
        //            string strType = row.RowIndex.ToString(); //
        //            lbtn.Attributes.Add("names", strName);
        //            lbtn.Attributes.Add("types", strType);
        //            this.gv.Rows[i].Cells[j].Controls.Add(lbtn);
        //            this.gv.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
        //        }
        //    }

        //    //图2
        //    DataTable ldt = DbHelperSQL6.Query("exec Pgi_Fgdkb '"+this.txtdomain.SelectedValue+"'").Tables[0];
        //    this.chart1.DataSource = ldt;
        //    this.chart1.DataBind();
        }

    }

    private void GetData()
    {

        //表1
        DataTable ldt_gd = DbHelperSQL6.Query("exec Pgi_Gdkb '" + this.txtdomain.SelectedValue + "','" + this.txtline.SelectedValue + "'").Tables[0];
        this.gv.DataSource = ldt_gd;
        this.gv.DataBind();

        for (int i = 0; i < this.gv.Rows.Count; i++)
        {
            GridViewRow row = (GridViewRow)this.gv.Rows[i];
            //for (int j = 1; j <= row.Cells.Count - 1; j++)
            //{
            //    LinkButton lbtn = new LinkButton();

            //    lbtn.ID = "btnM" + j.ToString();
            //    lbtn.Text = this.gv.Rows[i].Cells[j].Text;
            //    lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
            //    lbtn.Attributes.Add("name", "C");
            //    string strName = this.gv.Columns[j].HeaderText;//
            //    string strType = row.RowIndex.ToString(); //
            //    lbtn.Attributes.Add("names", strName);
            //    lbtn.Attributes.Add("types", strType);
            //    this.gv.Rows[i].Cells[j].Controls.Add(lbtn);
            //    this.gv.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            //}

            for (int j = 1; j < row.Cells.Count; j++)
            {
                HyperLink lhl = new HyperLink();
                lhl.Target = "blank";
                string lsday = "";
                if (row.RowIndex.ToString() == "0")
                {
                    lsday = "24";
                }
                else if (row.RowIndex.ToString() == "1")
                {
                    lsday = "18-24";
                }
                else
                {
                    lsday = "18";
                }
                string lsname = "";
                if (this.gv.Columns[j].HeaderText == "质量")
                {
                    lsname = "QA";
                }
                else if (this.gv.Columns[j].HeaderText == "生产")
                {
                    lsname = "PROD";
                }
                else
                {
                    lsname = "ALL";
                }
                lhl.Text = this.gv.Rows[i].Cells[j].Text;
                string lsopen = "http://172.16.5.6:8080/production/gd_list.aspx?kb_type=" + lsname + "&kb_day=" + lsday + "&domain=" + this.txtdomain.SelectedValue + "&line=" + this.txtline.SelectedValue;
                lhl.NavigateUrl = lsopen;
                this.gv.Rows[i].Cells[j].Controls.Add(lhl);

            }
        }

        //图2
        DataSet lds = DbHelperSQL6.Query("exec Pgi_Fgdkb '" + this.txtdomain.SelectedValue + "','"+this.txtline.Text+"'");
        this.chart1.DataSource = lds.Tables[0];
        this.chart1.DataBind();

        this.gv2.DataSource = lds.Tables[1];
        this.gv2.DataBind();
        for (int i = 0; i < this.gv2.Rows.Count; i++)
        {
            GridViewRow row = (GridViewRow)this.gv2.Rows[i];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (j!=0)
                {
                    continue;
                }
                string lskb_type = "";
                string lsday = GetWeekOfYear(System.DateTime.Now).ToString();
                if (j==0)
                {
                    lskb_type = "ALL";
                   
                }
                else if (j==1)
                {
                    lskb_type = "RK";
                }
                else if (j==2)
                {
                    lskb_type = "WWC";
                }
                HyperLink lhl = new HyperLink();
                lhl.Target = "blank";
               
                lhl.Text = this.gv2.Rows[i].Cells[j].Text;
                string lsopen = "http://172.16.5.6:8080/production/fgd_list.aspx?kb_type=" + lskb_type + "&kb_day=" + lsday + "&domain=" + this.txtdomain.SelectedValue + "&line=" + this.txtline.SelectedValue;
                lhl.NavigateUrl = lsopen;
                this.gv2.Rows[i].Cells[j].Controls.Add(lhl);
            }
        }

    }



    /// <summary>
    /// 获取指定日期，在为一年中为第几周
    /// </summary>
    /// <param name="dt">指定时间</param>
    /// <reutrn>返回第几周</reutrn>
    private static int GetWeekOfYear(DateTime dt)
    {
        GregorianCalendar gc = new GregorianCalendar();
        int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        return weekOfYear;
    }

    protected void LinkDtl_Click(object sender, EventArgs e)
    {

        string lsday= "";
        if (txtType.Text=="0")
        {
            lsday = "24";
        }
        else if (txtType.Text == "1")
        {
            lsday = "18-24";
        }
        else
        {
            lsday = "18";
        }
        string lsname = "";
        if (this.txtName.Text == "质量")
        {
            lsname = "QA";
        }
        else if (this.txtName.Text == "生产")
        {
            lsname = "PROD";
        } else
        {
            lsname = "ALL";
        }
        string lsopen = "http://172.16.5.6:8080/production/gd_list.aspx?kb_type=" + lsname + "&kb_day=" + lsday+"&domain="+this.txtdomain.SelectedValue+"&line="+this.txtline.SelectedValue;
        Response.Write("<script>window.open('"+lsopen+"');</script>");
      
    }









    protected void btnsearch_Click(object sender, EventArgs e)
    {
        this.GetData();
    }
}


