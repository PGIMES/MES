using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class YangJian_YJ_Tracking_Process_Report : System.Web.UI.Page
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
            string sqlyear = "select distinct year(yqfy_date) year from [dbo].[form1_Sale_YJ_dt_Sales_Assistant] where yqfy_date is not null";
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
	         select  A.Update_user AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form1_Sale_YJ_LOG] a
	         JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
             UNION ALL
			 select a.DC_uid AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form2_Sale_DC_MainTable] a
	         JOIN HRM_EMP_MES B ON A.DC_uid = B.workcode 
			 ) AA
       WHERE 1 = 1  ";

    //    string strSQL = @" SELECT DISTINCT AA.empid empid,
	   //AA.lastname lastname
	   //FROM (
	   //      select  A.Update_user AS empid,
			 //B.lastname AS lastname,
    //         B.dept_name AS dept_name from [dbo].[form1_Sale_YJ_LOG] a
	   //      JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	   //      UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	   //      UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
    //         UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流一部' AS dept_name
    //         UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流二部' AS dept_name
    //         UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流一部' AS dept_name
    //         UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流二部' AS dept_name
    //         UNION ALL
			 //select a.DC_uid AS empid,
			 //B.lastname AS lastname,
    //         B.dept_name AS dept_name from [dbo].[form2_Sale_DC_MainTable] a
	   //      JOIN HRM_EMP_MES B ON A.DC_uid = B.workcode 
			 //) AA
    //   WHERE 1 = 1  ";
        DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
        if (Updateusers.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_update_user, Updateusers, "empid", "lastname");
        }
        ddl_update_user.Items.Insert(0, new ListItem("", ""));
        //取消原先的进入界面直接默认显示该人员
        //ddl_update_user.Items.Insert(0, new ListItem("", "-1"));
        //if (Session["job"].ToString() == "仓库班长" || Session["job"].ToString() == "检验班长")
        //{
        //    this.ddl_update_user.SelectedValue = Session["job"].ToString();
        //}
        //else
        //{
        //    this.ddl_update_user.SelectedValue = Session["empid"].ToString();
        //}
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
	  FROM form1_Sale_YJ_LOG A
	  JOIN HRM_EMP_MES B ON A.Update_user=B.workcode 
	  UNION all
	  SELECT  b.dept_name as dept_name
	   FROM [dbo].[form1_Sale_YJ_MainTable] A ,
       [dbo].[form1_Sale_YJ_dt_Project_Engineer] C,
	   HRM_EMP_MES B
       WHERE C.cp_status IS NULL
       AND A.status_id=0
       AND A.requestId=C.requestId
       AND A.project_engineer_id=B.workcode
	 
	) aa where 1=1 order by aa.dept_name ";

 //       string strSQL = @" 
	// 	   select distinct dept_name as dept_sname,dept_name 
	//   from(
	//  SELECT b.dept_name as dept_name
	//  FROM V_form1_GCS A
	//  JOIN HRM_EMP_MES B ON A.gcs=B.workcode 
	//  UNION all
	//  SELECT  b.dept_name as dept_name
	//   FROM [dbo].[form1_Sale_YJ_MainTable] A ,
 //      [dbo].[form1_Sale_YJ_dt_Project_Engineer] C,
	//   HRM_EMP_MES B
 //      WHERE C.cp_status IS NULL
 //      AND A.status_id=0
 //      AND A.requestId=C.requestId
 //      AND A.project_engineer_id=B.workcode
	//    UNION all
	//	select  B.dept_name AS dept_name 
	//	from [dbo].[form1_Sale_YJ_LOG] a
	//JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	//) aa where 1=1 order by aa.dept_name ";
        DataTable departList = DbHelperSQL.Query(strSQL).Tables[0];
        if (departList.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddl_depart, departList, "dept_sname", "dept_name");
        }
        ddl_depart.Items.Insert(0, new ListItem("", ""));

        //根据当前进入的人员所在部门，直接默认选择该部门
        //根据人员工号，获取该人员所属部门名称
        string getdepat_name = @"  select distinct dept_name  from HRM_EMP_MES
	    where workcode='"+Session["empid"].ToString()+"'";
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
    //        string strSQL =
    //     @"	SELECT DISTINCT AA.empid empid,
	   //AA.lastname lastname
	   //FROM (
	   //      select  A.Update_user AS empid,
			 //B.lastname AS lastname,
    //         B.dept_name AS dept_name from [dbo].[form1_Sale_YJ_LOG] a
	   //      JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	   //      UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	   //      UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
    //         UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流一部' AS dept_name
    //         UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流二部' AS dept_name
    //         UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流一部' AS dept_name
    //         UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流二部' AS dept_name
    //         UNION ALL
			 //select a.DC_uid AS empid,
			 //B.lastname AS lastname,
    //         B.dept_name AS dept_name from [dbo].[form2_Sale_DC_MainTable] a
	   //      JOIN HRM_EMP_MES B ON A.DC_uid = B.workcode 
			 //) AA
    //   WHERE AA.dept_name='" + deptname + "'  ";

            string strSQL =
@"	SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select A.Update_user AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from form1_Sale_YJ_LOG a
	         JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
             UNION ALL
			 select a.DC_uid AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form2_Sale_DC_MainTable] a
	         JOIN HRM_EMP_MES B ON A.DC_uid = B.workcode 
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
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Tracking_Report(this.ddlyear.SelectedItem.Text.ToString(),
            this.txt_startdate.Text.ToString(),this.txt_enddate.Text.ToString(),this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(),this.ddlStauts.SelectedValue.ToString(),
            this.ddl_update_user.SelectedItem.Text.ToString(), this.ddlzzgc.SelectedItem.ToString(),
            this.ddlsign_status.SelectedValue.ToString(),this.ddl_ISDelay.SelectedValue.ToString(),
            this.ddl_depart.SelectedItem.ToString(), this.txt_date.Value.ToString(),this.ddl_iserp.SelectedValue.ToString(),
            this.ddl_czsx.SelectedItem.ToString()
            );
        this.GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////重新绑定行号
        //if (e.Row.RowIndex > -1)
        //{
        //    e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        //}
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
       {
            e.Row.Cells[2].Visible = false; //如果想使第2列不可见,则将它的可见性设为false
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Text != "")
            {
                if (e.Row.Cells[15].Text.ToString() == "订舱申请")
                {
                    if (e.Row.Cells[3].Text.ToString() == "" || e.Row.Cells[3].Text.ToString().Trim() == "&nbsp;")
                    {
                        ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/dingchang/DC_Apply.aspx'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");

                    }
                    else
                    {
                        ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[3].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");

                    }

                }
                else if (e.Row.Cells[15].Text.ToString() == "订舱处理")
                {
                    if (e.Row.Cells[3].Text.ToString() == "" || e.Row.Cells[3].Text.ToString().Trim() == "&nbsp;")
                    {

                    }
                    else
                    {
                        ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[3].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");

                    }
                }
                else
                {

                    ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/yangjian/Yangjian.aspx?requestid=" + e.Row.Cells[2].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");
                }
            }
        
                if (e.Row.Cells[20].Text == "未完成(逾时)")
                {
                    e.Row.Cells[20].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[20].Text == "未完成(未逾时)")
                {
                  if (e.Row.Cells[16].Text.ToString()!= "&nbsp;")
                  {
                    DateTime startTime = Convert.ToDateTime(e.Row.Cells[16].Text.ToString());
                    DateTime endTime = DateTime.Today;
                    //TimeSpan ts = endTime - startTime;
                    double totaldays = (endTime - startTime).Days;//天数
                    if (totaldays>=0&& totaldays<=3)
                    {
                        e.Row.Cells[20].BackColor = System.Drawing.Color.Yellow;
                    }
                   }
                }
        }
      }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        this.txt_startdate.Text = "";
        this.txt_enddate.Text = "";
        ddlStauts.SelectedIndex = -1;
        ddl_update_user.SelectedIndex=-1;
    }
    /// <summary>
    /// 给 计划完成时间 栏位 添加Sorting功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "desc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "desc")
            {
                ViewState["sortdirection"] = "asc";
            }
            else
            {
                ViewState["sortdirection"] = "desc";
            }
        }
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Tracking_Report(this.ddlyear.SelectedItem.Text.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.ddlStauts.SelectedValue.ToString(),
            this.ddl_update_user.SelectedItem.Text.ToString(), this.ddlzzgc.SelectedItem.ToString(),
            this.ddlsign_status.SelectedValue.ToString(), this.ddl_ISDelay.SelectedValue.ToString(),
             this.ddl_depart.SelectedItem.ToString(),this.txt_date.Value.ToString(), this.ddl_iserp.SelectedValue.ToString(), this.ddl_czsx.SelectedItem.ToString()
            );
        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }

        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
}