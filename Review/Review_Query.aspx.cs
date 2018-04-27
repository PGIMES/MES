using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.Services;

public partial class Review_Query : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 1000;
        this.gvMore.PageSize = 1000;
        if (!IsPostBack)
        {   //初始化日期
            this.txtcreate_date1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");//DateTime.Now.Year.ToString() + "/01/01"
            this.txtcreate_date2.Text = DateTime.Now.ToShortDateString();

            LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
            if(LogUserModel != null)
            {
                BaseFun.setDropSelectValue(ddlDept, LogUserModel.DepartName, "");
                if (LogUserModel.JobTitleName.Contains("经理") == false)
                {
                    txtzzr.Text = LogUserModel.UserName;
                }
            }
            //客户
            GetCust();
         
            
        }
    }

    public void GetCust()
    {
        string strSQL = @"select distinct customer_name from [dbo].[form3_Sale_Product_MainTable]";
        DataTable dt = DbHelperSQL.Query(strSQL).Tables[0];
        this.ddlcustomer.DataTextField = dt.Columns[0].ColumnName;
        this.ddlcustomer.DataValueField = dt.Columns[0].ColumnName;
        this.ddlcustomer.DataSource = dt;
        this.ddlcustomer.DataBind();
        this.ddlcustomer.Items.Insert(0, new ListItem("ALL",""));
    }


    private void GetData()
    {
       
        System.Text.StringBuilder lsbSQL = new System.Text.StringBuilder();
        lsbSQL.Append("exec [Q_Review_Query] ");
        lsbSQL.Append("'" + this.ddlCompany.Text.Trim() + "',");  //公司
        
        lsbSQL.Append("'" + this.ddlDept.Text + "',");  //责任部门
        lsbSQL.Append("'" + this.txtzzr.Text.Trim() + "',");  //责任人
        lsbSQL.Append("'" + this.txttcr.Text.Trim() + "',");  //提出人
        lsbSQL.Append("'" + this.ddlcustomer.Text.Trim() + "',");  //客户
        lsbSQL.Append("'" + this.ddlstatus.Text.Trim() + "',");  //问题状态
        lsbSQL.Append("'" + this.ddlsource.Text.Trim() + "',");  //问题来源
        //提出日期
        if (this.txtcreate_date1.Text.Trim()!="" && this.txtcreate_date2.Text.Trim()!="")
        {
            lsbSQL.Append("'" + this.txtcreate_date1.Text.Trim() + "',");
            lsbSQL.Append("'" + Convert.ToDateTime(this.txtcreate_date2.Text.Trim()).AddDays(1).ToShortDateString() + "',");
        }
        else
        {
            lsbSQL.Append("'',");
            lsbSQL.Append("'',");
        }
        lsbSQL.Append("'" + this.txtProddesc.Text.Trim() + "',");  //问题描述
        lsbSQL.Append("'" + this.txtProduct.Text.Trim() + "',");  //产品、项目
        lsbSQL.Append("'',");  //类型
        lsbSQL.Append("'"+this.txtstatus1.SelectedValue+"'");  //措施状态
        DataTable ldt = DbHelperSQL.Query(lsbSQL.ToString()).Tables[0];
        this.GridView1.DataSource = ldt;
        this.GridView1.DataBind();         
        gvMore.Visible = false;
        GridView1.Visible = true;

        int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
        MergGridRow.MergeRow(GridView1, cols);

       

        int rowIndex = 1;
        for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
        {
            if (GridView1.Rows[j].Cells[1].Visible == true)
            {

                GridView1.Rows[j].Cells[1].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Review.aspx?requestid=" + GridView1.Rows[j].Cells[0].Text;
                link.Text = GridView1.Rows[j].Cells[4].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[4].Controls.Add(link);
                rowIndex++;
            }

           
            
        }
    }
    private void GetDataMore(GridView grid)
    {

        System.Text.StringBuilder lsbSQL = new System.Text.StringBuilder();
        lsbSQL.Append("exec [Q_Review_Query_tt] ");
        lsbSQL.Append("'" + this.ddlCompany.Text.Trim() + "',");  //公司

        lsbSQL.Append("'" + this.ddlDept.Text + "',");  //责任部门
        lsbSQL.Append("'" + this.txtzzr.Text.Trim() + "',");  //责任人
        lsbSQL.Append("'" + this.txttcr.Text.Trim() + "',");  //提出人
        lsbSQL.Append("'" + this.ddlcustomer.Text.Trim() + "',");  //客户
        lsbSQL.Append("'" + this.ddlstatus.Text.Trim() + "',");  //问题状态
        lsbSQL.Append("'" + this.ddlsource.Text.Trim() + "',");  //问题来源
        //提出日期
        if (this.txtcreate_date1.Text.Trim() != "" && this.txtcreate_date2.Text.Trim() != "")
        {
            lsbSQL.Append("'" + this.txtcreate_date1.Text.Trim() + "',");
            lsbSQL.Append("'" + Convert.ToDateTime(this.txtcreate_date2.Text.Trim()).AddDays(1).ToShortDateString() + "',");
        }
        else
        {
            lsbSQL.Append(",");
            lsbSQL.Append(",");
        }
        lsbSQL.Append("'" + this.txtProddesc.Text.Trim() + "',");  //问题描述
        lsbSQL.Append("'" + this.txtProduct.Text.Trim() + "',");  //产品、项目
        lsbSQL.Append("''");  //类型
        DataTable ldt = DbHelperSQL.Query(lsbSQL.ToString()).Tables[0];
        grid.DataSource = ldt;
        grid.DataBind();
        grid.Visible = true;
        GridView1.Visible = false;
        int[] cols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 13, 14, 15 };
        MergGridRow.MergeRow(grid, cols);



        int rowIndex = 1;
        for (int j = 0; j <= grid.Rows.Count - 1; j++)
        {
            if (grid.Rows[j].Cells[0].Visible == true)
            {

                grid.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Review.aspx?requestid=" + grid.Rows[j].Cells[19].Text;
                link.Text = grid.Rows[j].Cells[3].Text;
                link.Target = "_blank";
                grid.Rows[j].Cells[3].Controls.Add(link);
                rowIndex++;
            }


            //if (grid.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "") != "")
            //{
            //    if (Convert.ToInt32(grid.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) >= 30 && Convert.ToInt32(grid.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) < 60)
            //    {
            //        //超30天黄色
            //        grid.Rows[j].BackColor = System.Drawing.Color.Yellow;
            //    }
            //    else if (Convert.ToInt32(grid.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) >= 60)
            //    {
            //        //超60天红色
            //        grid.Rows[j].BackColor = System.Drawing.Color.Red;
            //    }
            //}
            //if (grid.Rows[j].Cells[18].Text.Trim().Replace("&nbsp;", "") == "已关闭")
            //{
            //    grid.Rows[j].BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            //}
        }
    }

    ///<summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.GetData();
        gvMore.DataSource = null;
        gvMore.DataBind();
        
    }

    protected void btnMore_Click(object sender, EventArgs e)
    {
        GetDataMore(gvMore);
        GridView1.DataSource = null;
        GridView1.DataBind();     
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //重新绑定行号
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // e.Row.Cells[16].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[16].Style.Add("display", "none");
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (drv["s"].ToString().Replace("&nbsp;", "") == "已关闭")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            }

            var warningcolor = drv["warningcolor"].ToString().Replace("&nbsp;", "");
            e.Row.Style.Add("background-color", warningcolor);//.BackColor = System.Drawing.Color.Yellow;

            if (drv["ConfirmStatus"].ToString() == "通过")
            {
                e.Row.Style.Remove("background-color");
                //e.Row.Style.Add("background-color", "");
                e.Row.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            }
        }


    }
    protected void gvMore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //重新绑定行号
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // e.Row.Cells[19].Style.Add("display", "none");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {            
           // e.Row.Cells[19].Style.Add("display", "none");

            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (drv["s"].ToString().Replace("&nbsp;", "") == "已关闭")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            }


            //var d = drv["d"].ToString().Replace("&nbsp;", "");
            //if (d != "")
            //{
            //    if (Convert.ToInt32(d) >= 30 && Convert.ToInt32(d) < 60)
            //    {
            //        //超30天黄色
            //        e.Row.BackColor = System.Drawing.Color.Yellow;
            //    }
            //    else if (Convert.ToInt32(d) >= 60)
            //    {
            //        //超60天红色
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //    }
            //}

            var warningcolor = drv["warningcolor"].ToString().Replace("&nbsp;", "");
            e.Row.Style.Add("background-color",warningcolor);//.BackColor = System.Drawing.Color.Yellow;
               

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.GetData();
    }        
    protected void gvMore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMore.PageIndex = e.NewPageIndex;
        GetDataMore(gvMore);
    }
}

