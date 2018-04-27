using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;

public partial class YangJian_YJ_Query_Report : System.Web.UI.Page
{
    decimal ntotal_zj = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.PageSize = 200;
        this.GridView2.PageSize = 200;
        this.GridView3.PageSize = 200;
        if (!IsPostBack)
        {
            SetddlStauts();//给订单状态  ddlStauts 绑定数据源
            Get_ddlxgry(); //给人员检索ddlxgry  Dropdownlist 绑定数据源
            Get_ddlDepart();//给部门检索ddlxgry  Dropdownlist 绑定数据源
        }
    }
    /// <summary>
    /// 抓取所有单据相关的人员清单
    /// </summary>
    public void Get_ddlxgry()
    {
        //给人员检索ddlxgry  Dropdownlist 绑定数据源
        BaseFun fun = new BaseFun();
        //改善程式抓取的效率 改从V_form1_GCS 获取相关人员  该view为 主表[dbo].[form1_Sale_YJ_MainTable] 中涉及的所有人员
        string strSQL = @"SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select  A.Update_user AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form1_Sale_YJ_LOG] a
	         JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
             UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流一部' AS dept_name
             UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流二部' AS dept_name
             UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流一部' AS dept_name
             UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流二部' AS dept_name
             UNION ALL
			 select a.DC_uid AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form2_Sale_DC_MainTable] a
	         JOIN HRM_EMP_MES B ON A.DC_uid = B.workcode 
			 ) AA
       WHERE 1 = 1  ";
        DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
        if (Updateusers.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddlxgry, Updateusers, "empid", "lastname");
        }
        ddlxgry.Items.Insert(0, new ListItem("", ""));
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
	  FROM V_form1_GCS A
	  JOIN HRM_EMP_MES B ON A.gcs=B.workcode 
	  UNION all
	  SELECT  b.dept_name as dept_name
	   FROM [dbo].[form1_Sale_YJ_MainTable] A ,
       [dbo].[form1_Sale_YJ_dt_Project_Engineer] C,
	   HRM_EMP_MES B
       WHERE C.cp_status IS NULL
       AND A.status_id=0
       AND A.requestId=C.requestId
       AND A.project_engineer_id=B.workcode
	    UNION all
		select  B.dept_name AS dept_name 
		from [dbo].[form1_Sale_YJ_LOG] a
	JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	) aa where 1=1 order by aa.dept_name ";
        DataTable departList = DbHelperSQL.Query(strSQL).Tables[0];
        if (departList.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddlDepart, departList, "dept_sname", "dept_name");
        }
        ddlDepart.Items.Insert(0, new ListItem("", ""));
    }


    /// <summary>
    /// 根据DropDownList  ddlDepart当前选中的值 给dropdownlist ddl_update_user 重新绑定值
    /// 即根据选择的部门绑定该部门的相关人员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_updateuser_bydepart(this.ddlDepart.SelectedValue.ToString());
    }
    public void Get_updateuser_bydepart(string deptname)
    {
        if (deptname == "")
        {
            Get_ddlxgry();
        }
        else
        {
            //给相关人员Dropdownlist 绑定数据源
            BaseFun fun = new BaseFun();
            string strSQL =
         @"SELECT DISTINCT AA.empid empid,
	   AA.lastname lastname
	   FROM (
	         select  A.Update_user AS empid,
			 B.lastname AS lastname,
             B.dept_name AS dept_name from [dbo].[form1_Sale_YJ_LOG] a
	         JOIN HRM_EMP_MES B ON A.Update_user = B.workcode 
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量一部' AS dept_name
	         UNION ALL SELECT '检验班长' AS empid,'检验班长' AS lastname,'质量二部' AS dept_name
             UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流一部' AS dept_name
             UNION ALL SELECT '仓库班长' AS empid,'仓库班长' AS lastname,'物流二部' AS dept_name
             UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流一部' AS dept_name
             UNION ALL SELECT '物流工程师' AS empid,'物流工程师' AS lastname,'物流二部' AS dept_name
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
                fun.initDropDownList(this.ddlxgry, Updateusers, "empid", "lastname");
            }
            ddlxgry.Items.Insert(0, new ListItem("", ""));
        }
    }
    public void SetddlStauts()
    {
        //给订单状态  ddlStauts 绑定数据源
        BaseFun fun = new BaseFun();
        string strSQL = @" SELECT a.status_id  status_id,a.status status FROM [dbo].[form1_Sale_YJ_Status_mstr] a
        where status_id not in (-1,-3,-4,-5,-6) ";
        DataTable Updateusers = DbHelperSQL.Query(strSQL).Tables[0];
        if (Updateusers.Rows.Count > 0)
        {
            fun.initDropDownList(this.ddlStauts, Updateusers, "status_id", "status");
        }
        ddlStauts.Items.Insert(0, new ListItem("", "-1"));
    }
    ///<summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.ddltype.SelectedItem.Text.ToString() == "")
        {
            Response.Write("<script>alert('请选择产品状态!')</script>");
            return;
        }
        if (this.ddltype.SelectedItem.Text.ToString()== "新产品")
        {
            this.GridView1.Visible = true;
            this.GridView1.DataSource = null;
            GridView1.DataBind();
            QueryASPxGridView1();
            GridView2.DataSource = null;
            GridView2.DataBind();
            this.GridView2.Visible = false;
            GridView3.DataSource= null;
            GridView3.DataBind();
            this.GridView3.Visible = false;
        }
        if (this.ddltype.SelectedItem.Text.ToString() == "量产件")
        {
            this.GridView2.Visible = true;
            this.GridView2.DataSource = null;
            GridView2.DataBind();
            QueryASPxGridView2();
            GridView1.DataSource = null;
            GridView1.DataBind();
            this.GridView1.Visible = false;
            GridView3.DataSource = null;
            GridView3.DataBind();
            this.GridView3.Visible = false;
        }
        if (this.ddltype.SelectedItem.Text.ToString() == "无产品状态")
        {
            this.GridView3.Visible = true;
            this.GridView3.DataSource = null;
            GridView3.DataBind();
            QueryASPxGridView3();
            GridView1.DataSource = null;
            GridView1.DataBind();
            this.GridView1.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            this.GridView2.Visible = false;
        }
       
    }
    /// <summary>
    /// 新产品
    /// </summary>
    public void QueryASPxGridView1()
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
             this.ddlxgry.SelectedValue.ToString(), this.ddlzzgc.SelectedItem.ToString(), this.ddlfhStatus.SelectedValue.ToString(),
             this.txt_shrxx.Text.ToString(),this.txt_shrdz.Text.ToString(),this.ddlDepart.SelectedItem.ToString(),this.ddl_iserp.SelectedValue.ToString(),
             this.ddl_IsDC.SelectedValue.ToString());
        this.Getsum(dt);
        this.GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
            this.ddlxgry.SelectedValue.ToString(),this.ddlzzgc.SelectedItem.ToString(),this.ddlfhStatus.SelectedValue.ToString(),
             this.txt_shrxx.Text.ToString(), this.txt_shrdz.Text.ToString(), this.ddlDepart.SelectedItem.ToString(),
             this.ddl_iserp.SelectedValue.ToString(), this.ddl_IsDC.SelectedValue.ToString());
        this.Getsum(dt);
        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
    /// <summary>
    /// 量产件
    /// </summary>
    public void QueryASPxGridView2()
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
             this.ddlxgry.SelectedValue.ToString(), this.ddlzzgc.SelectedItem.ToString(), this.ddlfhStatus.SelectedValue.ToString(),
              this.txt_shrxx.Text.ToString(), this.txt_shrdz.Text.ToString(),this.ddlDepart.SelectedItem.ToString(),
              this.ddl_iserp.SelectedValue.ToString(), this.ddl_IsDC.SelectedValue.ToString());
        this.Getsum(dt);
        this.GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
            this.ddlxgry.SelectedValue.ToString(), this.ddlzzgc.SelectedItem.ToString(), this.ddlfhStatus.SelectedValue.ToString(),
             this.txt_shrxx.Text.ToString(), this.txt_shrdz.Text.ToString(), this.ddlDepart.SelectedItem.ToString(),
             this.ddl_iserp.SelectedValue.ToString(), this.ddl_IsDC.SelectedValue.ToString());
        this.Getsum(dt);
        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }

        this.GridView2.DataSource = dv;
        this.GridView2.DataBind();
    }
    /// <summary>
    /// 项目工程师尚未维护 产品状态
    /// </summary>
    public void QueryASPxGridView3()
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_NoCPStatus_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
             this.ddlxgry.SelectedValue.ToString(), this.ddlzzgc.SelectedItem.ToString(), this.ddlfhStatus.SelectedValue.ToString(),
             this.txt_shrxx.Text.ToString(),this.txt_shrdz.Text.ToString(),this.ddlDepart.SelectedValue.ToString());
        this.Getsum(dt);
        this.GridView3.DataSource = dt;
        GridView3.DataBind();
    }
    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_NoCPStatus_Report(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(), this.ddltype.SelectedItem.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
              this.ddlxgry.SelectedValue.ToString(), this.ddlzzgc.SelectedItem.ToString(), this.ddlfhStatus.SelectedValue.ToString(),
               this.txt_shrxx.Text.ToString(), this.txt_shrdz.Text.ToString(),this.ddlDepart.SelectedValue.ToString());
        this.Getsum(dt);
        DataView dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }

        this.GridView3.DataSource = dv;
        this.GridView3.DataBind();
    }
    /// <summary>
    /// 点击返回按钮，重置界面所有信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>.
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.txt_gkdm.Text = "";
        this.txt_xmh.Text = "";
        this.txt_startdate.Text = "";
        this.txt_enddate.Text = "";
        this.ddltype.SelectedValue = "";
        this.ddlStauts.DataSource = null;
        SetddlStauts();
        this.txt_fhdh.Text = "";
        this.txt_gkmc.Text = "";
        this.txt_khljh.Text = "";
        this.txtstart_date21.Text = "";
        this.txtstart_date22.Text = "";
        this.txt_gkddh.Text = "";
        this.txt_gkmc.Text = "";
        this.txt_gkddh.Text = "";
        this.txtppap1.Text = "";
        this.txtppap2.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //重新绑定行号
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
        //隐藏列
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[35].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //给样件单号绑定链接
            if (((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Text != "")
            {
                ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/yangjian/Yangjian.aspx?requestid=" + e.Row.Cells[14].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");
            }
            if (((HyperLink)e.Row.Cells[34].FindControl("HyperLink2")).Text != "")
            {
                ((HyperLink)e.Row.Cells[34].FindControl("HyperLink2")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/Dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[35].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");
            }
               
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //处理所有状态的颜色显示
                for (int i = 16; i < 34;i++)
                {
                    //销售助理申请
                    if (e.Row.Cells[i].Text.ToString().Trim() != "&nbsp;")
                    {
                        if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "已完成")
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                        }
                        if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "进行中")
                        {
                            DateTime startTime = Convert.ToDateTime(e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.IndexOf(" ") + 1, e.Row.Cells[i].Text.LastIndexOf(" ") - e.Row.Cells[i].Text.IndexOf(" ") - 1));
                          //  TimeSpan ts = startTime - DateTime.Now;
                            int totaldays = (startTime - DateTime.Today).Days;//天数
                            if (totaldays < 0)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                            }
                            if (totaldays <=3 && totaldays >=0)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            }
                          
                            //if (i == 21) //销售助理确认发货的时间，与【要求发运日期】 做比较
                            //{
                            //    //requiredate
                            //    DateTime startTime = Convert.ToDateTime(e.Row.Cells[21].Text.Substring(e.Row.Cells[21].Text.IndexOf(" ") + 1, e.Row.Cells[21].Text.LastIndexOf(" ") - e.Row.Cells[21].Text.IndexOf(" ") - 1));
                            //  // DateTime endTime = Convert.ToDateTime(e.Row.Cells[10].Text.ToString());//要求发运日期

                            //    TimeSpan ts = startTime- DateTime.Now;
                            //    double totaldays = ts.Days;//天数
                            //  //  if (totaldays - 15 <= 0)
                            //   if (totaldays <= 0)
                            //    {
                            //        e.Row.Cells[21].BackColor = System.Drawing.Color.Red;
                            //    }
                            //    else
                            //    {
                            //        e.Row.Cells[21].BackColor = System.Drawing.Color.Yellow;
                            //    }
                            //}
                            //else //其他情况就是与当前日期比较
                            //{

                            //    if (Convert.ToDateTime(e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.IndexOf(" ") + 1, e.Row.Cells[i].Text.LastIndexOf(" ") - e.Row.Cells[i].Text.IndexOf(" ") - 1)) < DateTime.Now)
                            //    {
                            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                            //    }
                            //    else
                            //    {
                            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            //    }
                            //}
                        }
                    }
                }

                //如果该订单是取消状态，则整行显示灰色
                if (e.Row.Cells[13].Text.ToString().Trim() == "订单取消")
                {
                    e.Row.BackColor = System.Drawing.Color.Tan;
                 for (int i = 16; i < 34; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Tan;

                    }                       
                }
            }
        }
        //添加合计功能
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "合计";
            e.Row.Cells[9].Text = this.ntotal_zj.ToString("N0");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        QueryASPxGridView1();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        QueryASPxGridView2();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        QueryASPxGridView3();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[37].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //给样件单号绑定链接
            if (((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Text != "")
            {

                ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/yangjian/Yangjian.aspx?requestid=" + e.Row.Cells[14].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");

            }
            if (((HyperLink)e.Row.Cells[36].FindControl("HyperLink2")).Text != "")
            {

                ((HyperLink)e.Row.Cells[36].FindControl("HyperLink2")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/Dingchang/DC_Apply.aspx?requestid=" + e.Row.Cells[37].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 16; i < 37; i++)
                {
                    //销售助理申请
                    if (e.Row.Cells[i].Text.ToString().Trim() != "&nbsp;"&& e.Row.Cells[i].Text.ToString().Trim() != "")
                    {
                        if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "已完成")
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                        }
                        if (e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.LastIndexOf(" ")).Trim() == "进行中")
                        {
                            DateTime startTime = Convert.ToDateTime(e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.IndexOf(" ") + 1, e.Row.Cells[i].Text.LastIndexOf(" ") - e.Row.Cells[i].Text.IndexOf(" ") - 1));
                            //TimeSpan ts = startTime - DateTime.Now;
                            //double totaldays = ts.Days;//天数
                            int totaldays = (startTime - DateTime.Today).Days;//天数
                            if (totaldays < 0)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                            }
                            if (totaldays <= 3 && totaldays >= 0)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            }



                            //if (i == 22)
                            //{
                            //    DateTime startTime = Convert.ToDateTime(e.Row.Cells[22].Text.Substring(e.Row.Cells[22].Text.IndexOf(" ") + 1, e.Row.Cells[22].Text.LastIndexOf(" ") - e.Row.Cells[22].Text.IndexOf(" ") - 1));
                            //    //DateTime endTime = Convert.ToDateTime(e.Row.Cells[10].Text.ToString());
                            //    TimeSpan ts = startTime - DateTime.Now;
                            //    double totaldays = ts.Days;//天数
                            //   // if (totaldays - 15 <= 0)
                            //    if (totaldays <= 0)
                            //    {
                            //        e.Row.Cells[22].BackColor = System.Drawing.Color.Red;

                            //    }
                            //    else
                            //    {
                            //        e.Row.Cells[22].BackColor = System.Drawing.Color.Yellow;
                            //    }
                            //}
                            //else
                            //{
                            //    if (Convert.ToDateTime(e.Row.Cells[i].Text.Substring(e.Row.Cells[i].Text.IndexOf(" ") + 1, e.Row.Cells[i].Text.LastIndexOf(" ") - e.Row.Cells[i].Text.IndexOf(" ") - 1)) < DateTime.Now)
                            //    {
                            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                            //    }
                            //    else
                            //    {
                            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            //    }
                            //}
                        }
                    }
                }
       
                //如果该订单是取消状态，则整行显示灰色
                if (e.Row.Cells[13].Text.ToString().Trim() == "订单取消")
                {
                    e.Row.BackColor = System.Drawing.Color.Tan;
                    for (int i = 16; i < 37; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Tan;

                    }
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "合计";
            e.Row.Cells[9].Text = this.ntotal_zj.ToString("N0");
        }
    }
  
    /// <summary>
    /// For 无产品状态的查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[13].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //给样件单号绑定链接
            if (((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Text != "")
            {
                ((HyperLink)e.Row.Cells[1].FindControl("HyperLink1")).Attributes.Add("onclick", "window.open(encodeURI('http://172.16.5.26:8010/yangjian/Yangjian.aspx?requestid=" + e.Row.Cells[13].Text.ToString() + "'),'newwindow','toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,top=50,left=0,width=1100,height=600')");
            }
            if (e.Row.Cells[14].Text.ToString().Trim() != "&nbsp;" && e.Row.Cells[14].Text.ToString().Trim() != "")
            {
                if (Convert.ToDateTime(e.Row.Cells[14].Text.Substring(e.Row.Cells[14].Text.IndexOf(" ") + 1, e.Row.Cells[14].Text.LastIndexOf(" ") - e.Row.Cells[14].Text.IndexOf(" ") - 1)) < DateTime.Now)
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Yellow;
                }
            }
            //如果该订单是取消状态，则整行显示灰色
            if (e.Row.Cells[12].Text.ToString().Trim() == "订单取消")
            {
                e.Row.BackColor = System.Drawing.Color.Tan;
             
                e.Row.Cells[14].BackColor = System.Drawing.Color.Tan;

            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = "合计";
            e.Row.Cells[8].Text = this.ntotal_zj.ToString("N0");
        }

    }
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        DataTable dt = YangjianSQLHelp.Get_YJ_Query_Report_ToExcel(this.txt_gkdm.Text.ToString(), this.txt_gkmc.Text.ToString(),
            this.txt_xmh.Text.ToString(), this.txt_khljh.Text.ToString(),
            this.txt_startdate.Text.ToString(), this.txt_enddate.Text.ToString(), this.txtstart_date21.Text.ToString(),
            this.txtstart_date22.Text.ToString(), this.txtppap1.Text.ToString(), this.txtppap2.Text.ToString(),
            this.ddlStauts.SelectedValue.ToString(), this.txt_fhdh.Text.ToString(), this.txt_gkddh.Text.ToString(),
            this.txt_shrxx.Text.ToString(),this.txt_shrdz.Text.ToString(),this.ddlxgry.SelectedValue.ToString(),this.ddlDepart.SelectedValue.ToString());
        string lsname = "样件系统单据数据";
        YangjianSQLHelp.DataTableToExcel(dt, "xls", lsname, "1");
    }
    /// <summary>
    /// 计算要货数量总和
    /// </summary>
    /// <param name="ldt"></param>
    private void Getsum(DataTable ldt)
    {
        this.ntotal_zj = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["要货数量"].ToString() != "")
            {
                this.ntotal_zj = this.ntotal_zj + Convert.ToDecimal(ldt.Rows[i]["要货数量"].ToString());
            }
        }
    }
}