using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class BaoJia_Baojia_Task_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 200;
        if (Session["empid"] == null || Session["job"] == null)
        {
            //Session["empid"] = "02088";
            // 给Session["empid"] & Session["job"] 初始化
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Get_ddl_update_user();//给相关人员Dropdownlist 绑定数据源
            Get_ddlDepart(); //给相关部门Dropdownlist 绑定数据源
            Get_updateuser_bydepart(this.ddl_depart.SelectedValue.ToString());//根据部门号重新绑定人员
            //年份
            string sqlyear = "select distinct year(receive_date) year from Baojia_sign_flow where receive_date is not null";
            BaseFun fun = new BaseFun();
            fun.initDropDownList(ddlyear, DbHelperSQL.Query(sqlyear).Tables[0], "year", "year");
            BaseFun.setDropSelectValue(ddlyear, DateTime.Now.Year.ToString(), "");
            
        }
    }
    public void Get_ddl_update_user()
    {
        //给相关人员Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL = @" SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select  A.empid AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[Baojia_sign_flow] a
	         JOIN HRM_EMP_MES B ON A.empid = B.workcode
			 where a.receive_date is not null
			 ) AA
       WHERE 1 = 1  ";
        DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
        if (Updateusers.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_update_user, Updateusers, "empid", "lastname");
        }
        ddl_update_user.Items.Insert(0, new ListItem("", ""));
        //进入界面直接默认显示该人员
      //  this.ddl_update_user.SelectedValue = Session["empid"].ToString();
    }
    /// <summary>
    /// 抓取所有单据相关的部门 LIST
    /// </summary>
    public void Get_ddlDepart()
    {
        //给部门检索ddlxgry  Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL = @" 
	 	  
	    select distinct dept_name as dept_sname,dept_name 
	   from(
	  SELECT b.dept_name as dept_name
	  FROM [dbo].[Baojia_sign_flow]  A
	  JOIN HRM_EMP_MES B ON A.EMPID=B.workcode 
	  AND a.receive_date is not null
	) aa where 1=1 order by aa.dept_name ";
        DataTable departList = DbHelperSQL.Query(strSQL).Tables[0];
        if (departList.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_depart, departList, "dept_sname", "dept_name");
        }
        ddl_depart.Items.Insert(0, new ListItem("", ""));

        //根据当前进入的人员所在部门，直接默认选择该部门
        //根据人员工号，获取该人员所属部门名称
        string getdepat_name = @"  select distinct dept_name  from HRM_EMP_MES
	    where workcode='" + Session["empid"].ToString() + "'";
        if (DbHelperSQL.Query(getdepat_name).Tables[0].Rows.Count > 0)
        {
            this.ddl_depart.SelectedValue = DbHelperSQL.Query(getdepat_name).Tables[0].Rows[0][0].ToString();
        }
    }

    /// <summary>
    /// 根据DropDownList  ddlDepart当前选中的值 给dropdownlist ddl_update_user 重新绑定值
    /// 即根据选择的部门绑定该部门的相关人员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_updateuser_bydepart(this.ddl_depart.SelectedValue.ToString());
    }

    public void Get_updateuser_bydepart(string deptname)
    {
        if (deptname == "")
        {
            Get_ddl_update_user();
        }
        else
        {
            //给相关人员Dropdownlist 绑定数据源
            BaseFun fun = new BaseFun();
            string strSQL =
         @"SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select  A.EMPID AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[Baojia_sign_flow]  a
	         JOIN HRM_EMP_MES B ON A.EMPID = B.workcode 
			 AND  a.receive_date is not null
			 ) AA
       WHERE AA.dept_name='" + deptname + "'  ";
            DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
            if (Updateusers.Rows.Count > 0)
            {
                fun.initDropDownList(this.ddl_update_user, Updateusers, "empid", "lastname");
            }
            ddl_update_user.Items.Insert(0, new ListItem("", ""));
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
        DataTable dt = BaojiaSQLHelp.Get_Baojia_Task_Query_Data(this.ddlyear.SelectedValue.ToString(), this.txt_startdate.Text, this.txt_enddate.Text,
            this.ddlBaojiaStatus.SelectedValue.ToString(), this.ddlsign_status.SelectedValue.ToString(), this.txt_date.Value, this.ddlzzgc.SelectedValue.ToString(),
            this.ddl_depart.SelectedValue.ToString(), this.ddl_update_user.SelectedValue.ToString(), this.ddl_ISDelay.SelectedValue.ToString()
            );
        this.GridView1.DataSource = dt;
        GridView1.DataBind();
        int[] cols = { 0, 1, 2, 3,4,5,6,7, 8, 9, 10, 11, 12, 13, 17,18 };
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
                GridView1.Rows[visbleRow].Cells[18].Text = GridView1.Rows[j].Cells[18].Text;
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
                link.NavigateUrl = "Baojia.aspx?requestid=" + Server.HtmlDecode(GridView1.Rows[j].Cells[18].Text).Trim();
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
            e.Row.Cells[18].Visible = false; //如果想使第2列不可见,则将它的可见性设为false
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            if (e.Row.Cells[17].Text == "未完成(逾时)")
            {
                e.Row.Cells[17].BackColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[17].Text == "未完成(未逾时)")
            {
                DateTime startTime = Convert.ToDateTime(e.Row.Cells[12].Text.ToString());
                DateTime endTime = DateTime.Today;
                //TimeSpan ts = endTime - startTime;
                double totaldays = (endTime - startTime).Days;//天数
                if (totaldays == 0)
                {
                    e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }
}
   