using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class BaoJia_Baojia_history_UpdateBaojia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 200;
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Get_ddl_update_user();//给相关人员Dropdownlist 绑定数据源
             QueryASPxGridView();
        }
       
    }
    public void Get_ddl_update_user()
    {
        //给相关人员Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL = @" SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select  A.create_by_empid AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[Baojia_mst] a
	         JOIN HRM_EMP_MES B ON A.create_by_empid = B.workcode
			 where a.baojia_status='已报出'
			 ) AA
       WHERE 1 = 1  ";
        DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
        if (Updateusers.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_update_user, Updateusers, "empid", "lastname");
        }
        ddl_update_user.Items.Insert(0, new ListItem("", ""));
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
        DataTable dt = BaojiaSQLHelp.Get_Baojia_history_update_data(this.txtBaojia_no.Text.ToString(),
            this.txtCustomer_name.Text.ToString(), this.ddl_update_user.SelectedValue.ToString()
            );
        this.GridView1.DataSource = dt;
        GridView1.DataBind();

        //合并单元格
        int[] cols = { 0, 1, 2, 3, 4,5,9,10,11 };
        MergGridRow.MergeRow(GridView1, cols);

        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Baojia.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[11].Text).Trim()+"&update=1";
                link.Text = GridView1.Rows[j].Cells[1].Text;
                link.Target = "_blank";
                GridView1.Rows[j].Cells[1].Controls.Add(link);
                rowIndex++;
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }

}