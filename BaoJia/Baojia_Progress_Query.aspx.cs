using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class BaoJia_Baojia_Progress_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 500;
        if (Session["empid"] == null)
        {
            //Session["empid"] = "02088";
            // 给Session["empid"] & Session["job"] 初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {

            QueryASPxGridView();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        QueryASPxGridView();
    }
    public void QueryASPxGridView()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        Baojia_Report_sql BaojiaSQLHelp = new Baojia_Report_sql();
        DataTable dt = BaojiaSQLHelp.Get_Baojia_Process_Query_Data(this.txtBaojia_no.Text.ToString(), this.txtCustomer_name.Text.ToString(), this.txtRenyuan.Text.ToString(),
        this.ddlBaojia_status.SelectedValue.ToString());
        this.GridView1.DataSource = dt;
        GridView1.DataBind();
        int[] cols = { 0, 1, 2, 3,4, 8,  9, 10, 11, 12, 13, 14,15,17,18 };
        MergGridRow.MergeRow(GridView1, cols);
        //获取最新的requestid
        int visbleRow = 0;
        for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                visbleRow = j;
            }
            else
            {
                GridView1.Rows[visbleRow].Cells[16].Text = GridView1.Rows[j].Cells[16].Text;
            }
        }
        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Baojia.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[16].Text).Trim();
                link.Text = GridView1.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //隐藏requestid   
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[16].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[16].Style.Add("display", "none");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //处理所有状态的颜色显示
            for (int i = 8; i < 15; i++)
            {
                //销售助理申请
                if (e.Row.Cells[i].Text.ToString().Trim() != "&nbsp;")
                {
                    if (e.Row.Cells[i].Text.Trim() == "NA")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    }
                    else  if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "已完成")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    }
                   else if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "进行中")
                    {
                        DateTime startTime = Convert.ToDateTime(e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.IndexOf(" ") + 1, e.Row.Cells[i].Text.LastIndexOf(" ") - e.Row.Cells[i].Text.IndexOf(" ") - 1));

                        int totaldays = (startTime - DateTime.Today).Days;//天数
                        if (totaldays < 0)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                        }
                        if (totaldays <= 1 && totaldays >= 0)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                    else if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "未开始")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                    else if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "未指派")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                    }




                }
            }
        }
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }
}