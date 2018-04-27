using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;

public partial class Customer_Update_list : System.Web.UI.Page
{
    Customer_CLASS Customer_CLASS = new Customer_CLASS();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 200;
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Get_ddl();//给相关人员Dropdownlist 绑定数据源
             QueryASPxGridView();
        }
       
    }
    public void Get_ddl()
    {
        //给相关人员Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL = @" SELECT distinct [Userid] ,[UserName] FROM [MES].[dbo].[form4_Customer_mstr]  where status_id=5 ";
        DataTable dt_UserName = DbHelperSQL.Query(strSQL).Tables[0];
        if (dt_UserName.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_UserName, dt_UserName, "UserName", "UserName");
        }
        DDL_UserName.Items.Insert(0, new ListItem("", ""));

        string strSQL_cmClassName = @" SELECT CmClassID,cmClassName FROM [MES].[dbo].[form4_Customer_mstr]  where status_id=5 ";
        DataTable dt_cmClassName = DbHelperSQL.Query(strSQL_cmClassName).Tables[0];
        if (dt_UserName.Rows.Count > 0)
        {
            fun.initDropDownList(this.DDL_cmClassName, dt_cmClassName, "cmClassName", "cmClassName");
        }
        DDL_cmClassName.Items.Insert(0, new ListItem("", ""));

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
        DataTable dt = Customer_CLASS.Get_Customer_Update_list(DDL_cmClassName.SelectedValue, txtBusinessRelationCode.Value, txtBusinessRelationName1.Value,DDL_UserName.SelectedValue);

        this.GridView1.DataSource = dt;
        GridView1.DataBind();

        ////合并单元格
        //int[] cols = { 0, 1, 2, 3, 4,5,9,10,11 };
        //MergGridRow.MergeRow(GridView1, cols);

        int rowIndex = 1;
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Visible == true)
            {
                GridView1.Rows[j].Cells[0].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Customer_QAD.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[6].Text).Trim()+"&update=1";
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
            e.Row.Cells[6].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }

}