using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class Review_TJ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
           
           // BaseFun fun = new BaseFun();         
            //初始化年份    
            ddlyear.DataSource = DbHelperSQL.Query("select distinct year(ProbDate) as y from Q_Review_Prob").Tables[0];
            ddlyear.DataTextField = "y";
            ddlyear.DataValueField = "y";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("ALL", ""));
            //初始化月份
            for(int i=1;i<=12;i++)
            {
                ddlMonth.Items.Add(i.ToString());
            }
            ddlMonth.Items.Insert(0, new ListItem("ALL", ""));

            //问题来源 
            selprobfrom.DataSource= DbHelperSQL.Query("select distinct probfrom from Q_Review_Prob").Tables[0];
            selprobfrom.DataTextField = "probfrom";
            selprobfrom.DataValueField = "probfrom";
            selprobfrom.DataBind();

            //提出人
            ddlempname.DataSource = DbHelperSQL.Query("select distinct empname from Q_Review_Prob").Tables[0];
            ddlempname.DataTextField = "empname";
            ddlempname.DataValueField = "empname";
            ddlempname.DataBind();
            ddlempname.Items.Insert(0, new ListItem("ALL", ""));

            //客户
            ddlcustomer.DataSource= DbHelperSQL.Query("select distinct CustClass from Q_Review_Prob").Tables[0];
            ddlcustomer.DataTextField = "CustClass";
            ddlcustomer.DataValueField = "CustClass";
            ddlcustomer.DataBind();
            ddlcustomer.Items.Insert(0, new ListItem("ALL", ""));

            // 初始化部分标签
            this.lblB.Text = System.DateTime.Now.Year.ToString();
            this.lblC.Text= System.DateTime.Now.Year.ToString();
            this.lblD.Text= System.DateTime.Now.Year.ToString();

            Getdate();

        }      

    }

    private void Getdate()
    {
        //string lswhere = "";
        //string lswhere1 = "";
        //if (this.ddlyear.Text.Trim() != "")
        //{
        //    lswhere += " and year(probdate)=" + this.ddlyear.Text.Trim() + "";
        //    lswhere1 += " and year(probdate)=" + this.ddlyear.Text.Trim() + "";
        //}
        //else
        //{
        //    lswhere += " and year(probdate)=" + System.DateTime.Now.Year.ToString() + "";
        //    lswhere1 += " and year(probdate)=" + System.DateTime.Now.Year.ToString() + "";
        //}
        ////问题来源
        //if (this.txtprobfrom.Text.Trim() != "")
        //{
        //    string[] ls = this.txtprobfrom.Text.Trim().Split(',');
        //    lswhere += " and ( probfrom in (";
        //    lswhere1 += " and ( probfrom in (";
        //    for (int i = 0; i < ls.Length; i++)
        //    {
        //        lswhere += "''" + ls[i] + "''";
        //        lswhere1 += "''" + ls[i] + "''";
        //        if (i < ls.Length - 1)
        //        {
        //            lswhere += ",";
        //            lswhere1 += ",";
        //        }
        //    }
        //    lswhere += "))";
        //    lswhere1 += "))";
        //}
        ////责任部门

        //if (this.txtdutydept.Text.Trim() != "")
        //{
        //    string[] ls = this.txtdutydept.Text.Trim().Split(',');
        //    lswhere += " and RequestId in (select RequestId from Q_Review_ProbDuty where dutydept in (";
        //    lswhere1 += " and dept_name in (";
        //    for (int i = 0; i < ls.Length; i++)
        //    {
        //        lswhere += "''" + ls[i].Trim() + "''";
        //        lswhere1 += "''" + ls[i].Trim() + "''";
        //        if (i < ls.Length - 1)
        //        {
        //            lswhere += ",";
        //            lswhere1 += ",";
        //        }
        //    }
        //    lswhere += "))";
        //    lswhere1 += ")";
        //}
        ////客户
        //if (this.ddlcustomer.Text.Trim() != "")
        //{
        //    lswhere += " and CustClass=''" + this.ddlcustomer.Text.Trim() + "''";
        //    lswhere1 += " and CustClass=''" + this.ddlcustomer.Text.Trim() + "''";
        //}
        ////产品
        //if (this.txtProduct.Text.Trim() != "")
        //{
        //    lswhere += " and ProdProject like ''%" + this.txtProduct.Text.Trim() + "%''";
        //    lswhere1 += " and ProdProject like ''%" + this.txtProduct.Text.Trim() + "%''";
        //}

        string lssql = "exec [Q_Review_Tj20180412] '"+this.txtprobfrom.Text.Trim()+"','" + this.ddlyear.Text.Trim() + "','" + this.txtdutydept.Text.Trim() + "','" + this.ddlcustomer.Text.Trim() + "','" + this.txtProduct.Text.Trim() + "','" + this.ddlempname.Text.Trim() + "' ,'" + this.txtProdDesc.Text.Trim() + "','"+ddlMonth.SelectedValue+"','"+this.txtdate_type.SelectedValue+"'";
        DataSet lds = DbHelperSQL.Query(lssql);
        QueryA(lds);//       
        QueryB(lds);//
        QueryC(lds);//
        QueryD(lds);//

        //设置标签
        if (this.ddlyear.Text.Trim()=="")
        {
            this.lblB.Text = System.DateTime.Now.Year.ToString();
            this.lblC.Text = System.DateTime.Now.Year.ToString();
            this.lblD.Text = System.DateTime.Now.Year.ToString();
        }
        else
        {
            this.lblB.Text = this.ddlyear.Text;
            this.lblC.Text = this.ddlyear.Text;
            this.lblD.Text = this.ddlyear.Text;

        }
        this.gv1.DataSource = null;
        this.gv1.DataBind();
       
    }
    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.Getdate();
 
    }
    public void QueryA(DataSet lds)
    {

       // string lssql = "exec [Q_Review_Tj20171226] 'ChartA','" + lswhere+ "','" + lswhere1 + "'";
        DataTable ldt = lds.Tables[0];

       
      

        //行转列
        DataTable ldt1 = new DataTable();
        ldt1.Columns.Add("类型");
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["y"].ToString()=="")
            {
                ldt1.Columns.Add(" ");
            }
            else
            {
                ldt1.Columns.Add(ldt.Rows[i]["y"].ToString());
            }
            
        }
      
            
            for (int j = 1; j < ldt.Columns.Count; j++)
            {
            DataRow ldr = ldt1.NewRow();
            ldr["类型"] = ldt.Columns[j].ColumnName;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["y"].ToString()=="")
                {
                    ldt.Rows[i]["y"] = " ";
                }
                ldr[ldt.Rows[i]["y"].ToString()] = ldt.Rows[i][ldt.Columns[j].ToString()].ToString();       
            }
            ldt1.Rows.Add(ldr);
        }

        //合计
        ldt1 = GetSum(ldt1);
        bindChartA(ldt);
        GridViewA.DataSource = ldt1;
        GridViewA.DataBind();
        SetGridViewALink();
    }


    private void SetGridViewALink()
    {
        //添加链接
        GridViewRow dr = this.GridViewA.HeaderRow;
        for (int i = 0; i < 3; i++)
        {
            GridViewRow row = (GridViewRow)this.GridViewA.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM" + j.ToString();
                lbtn.Text = this.GridViewA.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
                lbtn.Attributes.Add("name", "C");
                string strName = dr.Cells[j].Text;//获取月份
                string strType = Server.HtmlEncode(row.Cells[0].Text); //获取类别
                lbtn.Attributes.Add("names", strName);
                lbtn.Attributes.Add("types", "年份_" + strType);
                this.GridViewA.Rows[i].Cells[j].Controls.Add(lbtn);
                this.GridViewA.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }

    public void QueryB(DataSet lds)
    {
        
       // string lssql = "exec [Q_Review_Tj20171226] 'ChartB','" + lswhere + "','" + lswhere1 + "'";
        DataTable ldt =lds.Tables[1];



        //行转列
        DataTable ldt1 = new DataTable();
        ldt1.Columns.Add("类型");
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["m"].ToString() == "")
            {
                ldt1.Columns.Add(" ");
            }
            else
            {
                ldt1.Columns.Add(ldt.Rows[i]["m"].ToString());
            }

        }


        for (int j = 1; j < ldt.Columns.Count; j++)
        {
            DataRow ldr = ldt1.NewRow();
            ldr["类型"] = ldt.Columns[j].ColumnName;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["m"].ToString() == "")
                {
                    ldt.Rows[i]["m"] = " ";
                }
                ldr[ldt.Rows[i]["m"].ToString()] = ldt.Rows[i][ldt.Columns[j].ToString()].ToString();
            }
            ldt1.Rows.Add(ldr);
        }
        //合计
        ldt1 = GetSum(ldt1);


        bindChartB(ldt);
        GridViewB.DataSource = ldt1;
        GridViewB.DataBind();
        SetGridViewBLink();
    }

    private void SetGridViewBLink()
    {
        //添加链接
        GridViewRow dr = this.GridViewB.HeaderRow;
        for (int i = 0; i < 3; i++)
        {
            GridViewRow row = (GridViewRow)this.GridViewB.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM" + j.ToString();
                lbtn.Text = this.GridViewB.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
                lbtn.Attributes.Add("name", "C");
                string strName = dr.Cells[j].Text;//获取月份
                string strType = Server.HtmlEncode(row.Cells[0].Text); //获取类别
                lbtn.Attributes.Add("names", strName);
                lbtn.Attributes.Add("types", "月份_" + strType);
                this.GridViewB.Rows[i].Cells[j].Controls.Add(lbtn);
                this.GridViewB.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }

    public void QueryC(DataSet lds )
    {
        
       // string lssql = "exec [Q_Review_Tj20171226] 'ChartC','" + lswhere + "','" + lswhere1 + "'";
        DataTable ldt = lds.Tables[2];

        //行转列
        DataTable ldt1 = new DataTable();
        ldt1.Columns.Add("类型");
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["dept"].ToString()=="")
            {
                ldt1.Columns.Add(" ");
            }
            else
            {
                ldt1.Columns.Add(ldt.Rows[i]["dept"].ToString());
            }
           
        }
    
       
           
            for (int j = 1; j < 9; j++)
            {
                DataRow ldr = ldt1.NewRow();
            //更改类型
            string lscolumn = "";
            if (ldt.Columns[j].ColumnName == "未关闭1")
            {
                lscolumn = "未关闭(逾时)";
            }
            else if (ldt.Columns[j].ColumnName== "未关闭2")
            {
                lscolumn = "未关闭(未逾时)";
            }
            else if (ldt.Columns[j].ColumnName == "已关闭1")
            {
                lscolumn = "已关闭(逾时)";
            }
            else if (ldt.Columns[j].ColumnName == "已关闭2")
            {
                lscolumn = "已关闭(未逾时)";
            }
            else
            {
                lscolumn = ldt.Columns[j].ColumnName;
            }

            ldr["类型"] = lscolumn;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["dept"].ToString()=="")
                {
                    ldt.Rows[i]["dept"] = " ";
                }
                ldr[ldt.Rows[i]["dept"].ToString()] = ldt.Rows[i][ldt.Columns[j].ToString()].ToString(); 
            }
            ldt1.Rows.Add(ldr);
        }
        bindChartC(ldt);
        //合计
        ldt = GetSum3(ldt1);
        
        GridViewC.DataSource = ldt1;
        GridViewC.DataBind();
        if (this.GridViewC.HeaderRow.Cells.Count > 0)
        {
            this.GridViewC.Width = 80 * this.GridViewC.HeaderRow.Cells.Count + 20;
            this.GridViewC.HeaderRow.Cells[0].Width = 100;
        }
        this.SetGridViewCLink();

      
    }

    private void SetGridViewCLink()
    {
        //添加链接
        GridViewRow dr = this.GridViewC.HeaderRow;
        for (int i = 0; i < 4; i++)
        {
            GridViewRow row = (GridViewRow)this.GridViewC.Rows[i];
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM" + j.ToString();
                lbtn.Text = this.GridViewC.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
                lbtn.Attributes.Add("name", "C");
                string strName = dr.Cells[j].Text;//获取部门或人名
                string strType = Server.HtmlEncode(row.Cells[0].Text); //获取类别
                //if (strType.Substring(0,3)=="已关闭")
                //{
                //    strType = "CD";
                //}
                lbtn.Attributes.Add("names", strName);
                lbtn.Attributes.Add("types", "部门_"+strType);
                this.GridViewC.Rows[i].Cells[j].Controls.Add(lbtn);
                this.GridViewC.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
    }

    protected void LinkDtl_Click(object sender, EventArgs e)
    {
        string lstype = txtType.Text;
        //lstype = lstype.Replace("(未逾时)", "");
        //lstype = lstype.Replace("(逾时)", "");
        string lsname = txtName.Text; //获取人名
        System.Text.StringBuilder lsbSQL = new System.Text.StringBuilder();
        string[] ls = lstype.Split('_');
        lsbSQL.Append("exec [Q_Review_Query] ");
        lsbSQL.Append("'',");  //公司
      
        if (ls[0].Trim()=="部门" && lsname!="合计")
        {
            lsbSQL.Append("'"+lsname+"',");  //责任部门
            lsbSQL.Append("'',");  //责任人
        }
        else if(ls[0].Trim() == "人员")
        {
            lsbSQL.Append("'"+ this.txtdutydept.Text.Trim() + "',");  //责任部门
            lsbSQL.Append("'"+ lsname + "',");  //责任人
        }
        else
        {
            lsbSQL.Append("'" + this.txtdutydept.Text.Trim() + "',");  //责任部门
            lsbSQL.Append("'',");  //责任人
        }
       
        lsbSQL.Append("'"+this.ddlempname.Text.Trim()+"',");  //提出人
        lsbSQL.Append("'',");  //客户
       
            lsbSQL.Append("'" + ls[1].Trim() + "',");  //问题状态

      
       
        lsbSQL.Append("'"+this.txtprobfrom.Text+"',");  //问题来源
        //提出日期
        if (this.ddlyear.Text.Trim()!="")
        {
            if (ls[0].Trim()=="月份" && lsname!="合计")
            {
                DateTime ldate1 = Convert.ToDateTime(this.ddlyear.Text.Trim() + "-" + lsname + "-01");
                lsbSQL.Append("'" + ldate1.ToString() + "',");
                DateTime ldate2= ldate1.AddDays(1 - ldate1.Day).AddMonths(1).AddDays(-1);
                lsbSQL.Append("'"+ldate2.ToString()+"',");
               
              
            }
            else
            {
                if (this.ddlMonth.Text!="")
                {
                    lsbSQL.Append("'" + this.ddlyear.Text.Trim() + "-"+this.ddlMonth.Text+"-01"+"',");
                    DateTime ldate2 =Convert.ToDateTime(this.ddlyear.Text.Trim() + "-" +(Convert.ToInt32(this.ddlMonth.Text)+1).ToString() + "-01").AddDays(-1);
                    lsbSQL.Append("'" + ldate2 + "',");
                }
                else
                {
                    lsbSQL.Append("'" + this.ddlyear.Text.Trim() + "-01-01',");
                    lsbSQL.Append("'" + this.ddlyear.Text.Trim() + "-12-31',");
                }
               
            }
           
        }
        else
        {
            string lsyear = System.DateTime.Now.Year.ToString();

            if (ls[0].Trim() == "月份" && lsname != "合计")
            {
                
                if (this.ddlyear.Text.Trim()!="")
                {
                    lsyear = this.ddlyear.Text.Trim();
                }
                DateTime ldate1 = Convert.ToDateTime(lsyear + "-" + lsname.ToString() + "-01");
                lsbSQL.Append("'" + ldate1.ToString() + "',");
               // DateTime ldate2 = ldate1.AddDays(1 - ldate1.Day).AddMonths(1).AddDays(-1);
                DateTime ldate2 = Convert.ToDateTime(lsyear + "-" + (Convert.ToInt32(lsname)+1).ToString() + "-01");
                lsbSQL.Append("'" + ldate2.ToString() + "',");


            }else if (ls[0].Trim() == "年份" && lsname != "合计")
            {
                lsbSQL.Append("'" + lsname + "-01-01" + "',");
                lsbSQL.Append("'" + (Convert.ToInt32(lsname) + 1).ToString() + "-01-01" + "',");
            }
            else if (ls[0].Trim() == "年份" && lsname == "合计" && ls[1].Trim()== "合计")
            {
                lsbSQL.Append("'',");
                lsbSQL.Append("'',");
            }
            else
            {
                lsbSQL.Append("'"+lsyear+"-01-01"+"',");
                lsbSQL.Append("'" + (Convert.ToInt32(lsyear)+1).ToString() + "-01-01" + "',");
            }
          
        }
       
        lsbSQL.Append("'"+this.txtProdDesc.Text.Trim()+"',");  //问题描述
        lsbSQL.Append("'',");  //产品、项目
        lsbSQL.Append("'',");  //类型
       
            lsbSQL.Append("''");
        
        
        DataTable ldt = DbHelperSQL.Query(lsbSQL.ToString()).Tables[0];
        this.gv1.DataSource = ldt;
        this.gv1.DataBind();
        int[] cols = { 0, 1, 2, 3, 4, 5, 6,7,8,9,10,11 };
        MergGridRow.MergeRow(this.gv1, cols);
        int rowIndex = 1;
        for (int j = 0; j <= this.gv1.Rows.Count - 1; j++)
        {
            if (this.gv1.Rows[j].Cells[1].Visible == true)
            {

                this.gv1.Rows[j].Cells[1].Text = rowIndex.ToString();
                HyperLink link = new HyperLink();
                link.ID = "link" + rowIndex.ToString();
                link.NavigateUrl = "Review.aspx?requestid=" + this.gv1.Rows[j].Cells[0].Text;
                link.Text = this.gv1.Rows[j].Cells[4].Text;
                link.Target = "_blank";
                this.gv1.Rows[j].Cells[4].Controls.Add(link);
                rowIndex++;
            }


            //if (this.gv1.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "") != "")
            //{
            //    if (Convert.ToInt32(this.gv1.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) >= 30 && Convert.ToInt32(this.gv1.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) < 60)
            //    {
            //        //超30天黄色
            //        this.gv1.Rows[j].BackColor = System.Drawing.Color.Yellow;
            //    }
            //    else if (Convert.ToInt32(this.gv1.Rows[j].Cells[16].Text.Trim().Replace("&nbsp;", "")) >= 60)
            //    {
            //        //超60天红色
            //        this.gv1.Rows[j].BackColor = System.Drawing.Color.Red;
            //    }
            //}
            //if (this.gv1.Rows[j].Cells[17].Text.Trim().Replace("&nbsp;", "") == "已关闭")
            //{
            //    this.gv1.Rows[j].BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            //}
        }

        this.SetGridViewCLink();

    }

    public void QueryD(DataSet lds)
    {

       
        DataTable ldt = lds.Tables[3];

        //行转列
        DataTable ldt1 = new DataTable();
        ldt1.Columns.Add("类型");
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["empname"].ToString() == "")
            {
                ldt1.Columns.Add(" ");
            }
            else
            {
                ldt1.Columns.Add(ldt.Rows[i]["empname"].ToString());
            }

        }
        for (int j = 1; j < 9; j++)
        {
            DataRow ldr = ldt1.NewRow();
            //更改类型
            string lscolumn = "";
            if (ldt.Columns[j].ColumnName == "未关闭1")
            {
                lscolumn = "未关闭(逾时)";
            }
            else if (ldt.Columns[j].ColumnName == "未关闭2")
            {
                lscolumn = "未关闭(未逾时)";
            }
            else if (ldt.Columns[j].ColumnName == "已关闭1")
            {
                lscolumn = "已关闭(逾时)";
            }
            else if (ldt.Columns[j].ColumnName == "已关闭2")
            {
                lscolumn = "已关闭(未逾时)";
            }
            else
            {
                lscolumn = ldt.Columns[j].ColumnName;
            }

            ldr["类型"] = lscolumn;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (ldt.Rows[i]["empname"].ToString() == "")
                {
                    ldt.Rows[i]["empname"] = " ";
                }
                ldr[ldt.Rows[i]["empname"].ToString()] = ldt.Rows[i][ldt.Columns[j].ToString()].ToString();
            }
            ldt1.Rows.Add(ldr);
        }
        bindChartD(ldt);
        //合计
        ldt = GetSum3(ldt1);

        GridViewD.DataSource = ldt1;
        GridViewD.DataBind();
        if (this.GridViewD.HeaderRow.Cells.Count>0)
        {
            this.GridViewD.Width = 60 * this.GridViewD.HeaderRow.Cells.Count + 40;
            this.GridViewD.HeaderRow.Cells[0].Width = 100;
        }
       
        //this.GridViewD.HeaderRow.Cells[0].CssClass ="d_width";
        this.SetGridViewDLink();



    }

    private void SetGridViewDLink()
    {
        //添加链接
       
        
        GridViewRow dr = this.GridViewD.HeaderRow;
        for (int i = 0; i < 4; i++)
        {
            GridViewRow row = (GridViewRow)this.GridViewD.Rows[i];
          
            for (int j = 1; j <= row.Cells.Count - 1; j++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btnM" + j.ToString();
                lbtn.Text = this.GridViewD.Rows[i].Cells[j].Text;
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkDtl','')");
                lbtn.Attributes.Add("name", "C");
                string strName = dr.Cells[j].Text;//获取部门或人名
                string strType = Server.HtmlEncode(row.Cells[0].Text); //获取类别
                //if (strType.Substring(0,3) == "已关闭")
                //{
                //    strType = "CD";
                //}
                lbtn.Attributes.Add("names", strName);
                lbtn.Attributes.Add("types", "人员_"+strType);
                this.GridViewD.Rows[i].Cells[j].Controls.Add(lbtn);
                this.GridViewD.Rows[i].Cells[j].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
            }
        }
       
    }


    public void bindChartA(DataTable ldt)
    {
        ChartA.DataSource = ldt;
        ChartA.Series["A1"].XValueMember = "y";
        ChartA.Series["A1"].YValueMembers = "已关闭";
        ChartA.Series["A1"].ToolTip = "xxxx";
        ChartA.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        ChartA.Series["A1"].Color = System.Drawing.Color.FromArgb(113, 148, 149);
        ChartA.Series["A2"].Color = System.Drawing.Color.FromArgb(190, 85, 61);

        ChartA.Series["A2"].XValueMember = "y";
        ChartA.Series["A2"].YValueMembers = "未关闭";
        ChartA.DataBind();
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ChartA.Series["A1"].Points[i].ToolTip = ldt.Rows[i]["y"].ToString()+ "(已关闭):" + ldt.Rows[i]["已关闭"].ToString();
            ChartA.Series["A2"].Points[i].ToolTip = ldt.Rows[i]["y"].ToString() + "(未关闭):" + ldt.Rows[i]["未关闭"].ToString();
        }
       
    }
    public void bindChartB(DataTable ldt)
    {   //批次
        ChartB.DataSource = ldt;               
        ChartB.Series["B1"].XValueMember = "m";
        ChartB.Series["B1"].YValueMembers = "已关闭";
        ChartB.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        ChartB.Series["B2"].XValueMember = "m";
        ChartB.Series["B2"].YValueMembers = "未关闭";
        ChartB.DataBind();

        ChartB.Series["B1"].Color = System.Drawing.Color.FromArgb(113, 148, 149);
        ChartB.Series["B2"].Color = System.Drawing.Color.FromArgb(190, 85, 61); 

        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            ChartB.Series["B1"].Points[i].ToolTip = ldt.Rows[i]["m"].ToString() + "(已关闭):" + ldt.Rows[i]["已关闭"].ToString();
            ChartB.Series["B2"].Points[i].ToolTip = ldt.Rows[i]["m"].ToString() + "(未关闭):" + ldt.Rows[i]["未关闭"].ToString();
        }

    }
    public void bindChartC(DataTable ldt)
    {
        ChartC.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        ChartC.ChartAreas[0].AxisY.Interval = 5;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            DataTable ldt1 = new DataTable();
            ldt1.Columns.Add("Type");
            ldt1.Columns.Add("Ncount");
            for (int j = 10; j < ldt.Columns.Count; j++)
            {
                if (Convert.ToInt32(ldt.Rows[i][ldt.Columns[j].ColumnName].ToString()) > 0)
                {
                    DataRow ldr = ldt1.NewRow();
                    ldr["Type"] = ldt.Columns[j].ColumnName;
                    ldr["Ncount"] = ldt.Rows[i][ldt.Columns[j].ColumnName];
                    ldt1.Rows.Add(ldr);
                }

            }
            DataView ldv = ldt1.DefaultView;
            ldv.Sort = "Ncount,Type Desc";
            DataTable ldt2 = ldv.ToTable();



            if (ldt2.Rows.Count > 5)
            {
                Int32 lncount = 0;
                for (int l = 5; l < ldt2.Rows.Count; l++)
                {
                    lncount += Convert.ToInt32(ldt2.Rows[l]["Ncount"].ToString());
                }
                for (int l = ldt2.Rows.Count - 1; l >= 5; l--)
                {
                    ldt2.Rows[l].Delete();
                }
                ldt2.AcceptChanges();
                DataRow ldr = ldt2.NewRow();
                ldr["Type"] = "其他";
                ldr["Ncount"] = lncount;
                ldt2.Rows.Add(ldr);
            }
            else
            {
                for (int m = ldt2.Rows.Count; m < 6; m++)
                {
                    DataRow ldr = ldt2.NewRow();
                    ldr["Type"] = " ";
                    ldr["Ncount"] = 0;
                    ldt2.Rows.Add(ldr);
                }
            }

            for (int k = 0; k < ldt2.Rows.Count; k++)
            {
                System.Web.UI.DataVisualization.Charting.DataPoint ldp = new System.Web.UI.DataVisualization.Charting.DataPoint();
                ldp.ToolTip = ldt.Rows[i]["dept"].ToString() + "(" + ldt2.Rows[k]["Type"].ToString() + "):" + ldt2.Rows[k]["Ncount"].ToString();
                ldp.Color = this.SetColor(ldt2.Rows[k]["Type"].ToString());
                string lsdept = ldt.Rows[i]["dept"].ToString();
                if (ldt.Rows[i]["dept"].ToString().Trim().Length>4)
                {
                    lsdept = ldt.Rows[i]["dept"].ToString().Trim().Substring(0, 4);
                }
                ldp.SetValueXY(lsdept, new object[] { Convert.ToInt32(ldt2.Rows[k]["Ncount"].ToString()) });
               

                string lsc = "C" + (k + 1).ToString();
                ChartC.Series[lsc].Points.Add(ldp);


            }


        }

        ////测试
        //for (int i = 1; i < 7; i++)
        //{
        //    for (int j = 1; j < 7; j++)
        //    {
        //        System.Web.UI.DataVisualization.Charting.DataPoint ldp = new System.Web.UI.DataVisualization.Charting.DataPoint();
        //        ldp.SetValueXY(i.ToString(), new object[] {j });
        //        ChartC.Series[j-1].Points.Add(ldp);
        //    }
        //}

    }
    private System.Drawing.Color SetColor(string lsType)
    {
        System.Drawing.Color lc= System.Drawing.Color.FromArgb(102, 68, 102);
        if (lsType=="客户投诉")
        {
            lc = System.Drawing.Color.FromArgb(102,153,170);
        }
        else if (lsType == "产品审核")
        {
            lc =  System.Drawing.Color.FromArgb(123, 157, 158);
        }
        else if (lsType == "过程审核")
        {
            lc = System.Drawing.Color.FromArgb(190, 85, 61);
        }
        else if (lsType == "内审")
        {
            lc = System.Drawing.Color.FromArgb(116, 102, 93);
        }
        else if (lsType == "管理评审")
        {
            lc = System.Drawing.Color.FromArgb(34, 153, 136);
        }
        else if (lsType == "客户审核")
        {
            lc = System.Drawing.Color.FromArgb(68, 119, 102);
        }
        else if (lsType == "第三方审核")
        {
            lc = System.Drawing.Color.FromArgb(176, 155, 0);
        }
        else if (lsType == "S检查")
        {
            lc = System.Drawing.Color.FromArgb(85, 170, 238);
        }
        else if (lsType == "EHS检查")
        {
            lc = System.Drawing.Color.FromArgb(68, 51, 170);
        }
        else if (lsType == "持续改进")
        {
            lc = System.Drawing.Color.FromArgb(255, 104, 102);
        }
        else if (lsType == "月会问题跟踪")
        {
            lc = System.Drawing.Color.FromArgb(153, 221, 102);
        }
        else if (lsType == "周会问题跟踪")
        {
            lc = System.Drawing.Color.FromArgb(153, 85, 170);
        }
        else
        {
            lc = System.Drawing.Color.FromArgb(34, 204, 136);
        }
        return lc;
    }
    public void bindChartD(DataTable ldt)
    {
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (i>14)
            {
                continue;
            }
            DataTable ldt1 = new DataTable();
            ldt1.Columns.Add("Type");
            ldt1.Columns.Add("Ncount");
            for (int j = 10; j < ldt.Columns.Count; j++)
            {
                if (Convert.ToInt32(ldt.Rows[i][ldt.Columns[j].ColumnName].ToString()) > 0)
                {
                    DataRow ldr = ldt1.NewRow();
                    ldr["Type"] = ldt.Columns[j].ColumnName;
                    ldr["Ncount"] = ldt.Rows[i][ldt.Columns[j].ColumnName];
                    ldt1.Rows.Add(ldr);
                }

            }
            DataView ldv = ldt1.DefaultView;
            ldv.Sort = "Ncount,Type Desc";
            DataTable ldt2 = ldv.ToTable();

            ChartD.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            ChartD.ChartAreas[0].AxisY.Interval = 1;

            if (ldt2.Rows.Count > 5)
            {
                Int32 lncount = 0;
                for (int l = 5; l < ldt2.Rows.Count; l++)
                {
                    lncount += Convert.ToInt32(ldt2.Rows[l]["Ncount"].ToString());
                }
                for (int l = ldt2.Rows.Count - 1; l >= 5; l--)
                {
                    ldt2.Rows[l].Delete();
                }
                ldt2.AcceptChanges();
                DataRow ldr = ldt2.NewRow();
                ldr["Type"] = "其他";
                ldr["Ncount"] = lncount;
                ldt2.Rows.Add(ldr);
            }
            else
            {
                for (int m = ldt2.Rows.Count; m < 6; m++)
                {
                    DataRow ldr = ldt2.NewRow();
                    ldr["Type"] = " ";
                    ldr["Ncount"] = 0;
                    ldt2.Rows.Add(ldr);
                }
            }

            for (int k = 0; k < ldt2.Rows.Count; k++)
            {
                
                System.Web.UI.DataVisualization.Charting.DataPoint ldp = new System.Web.UI.DataVisualization.Charting.DataPoint();
                ldp.SetValueXY(ldt.Rows[i]["EmpName"].ToString(), new object[] { Convert.ToInt32(ldt2.Rows[k]["Ncount"].ToString()) });
                ldp.ToolTip = ldt.Rows[i]["EmpName"].ToString() +"("+ ldt2.Rows[k]["Type"].ToString()+"):" + ldt2.Rows[k]["Ncount"].ToString();
                ldp.Color = this.SetColor(ldt2.Rows[k]["Type"].ToString());

                string lsd = "D" + (k + 1).ToString();
                ChartD.Series[lsd].Points.Add(ldp);


            }

        }


    }
    //public void setChartType(System.Web.UI.DataVisualization.Charting.Series   chartSeries, Boolean isC )
    //{
    //    //Boolean bln = this.dropCondition.SelectedValue == "M" ? true:false; //按月份:显示折线图
    //    //if (bln)
    //    //{
    //    //    chartSeries.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
    //    //}
    //    //else
    //    //{
    //    //    chartSeries.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
    //    //}
    //}

    //合计
    private DataTable GetSum(DataTable ldt)
    {
        ldt.Columns.Add("合计");
        for (int i = 0; i < 7; i++)
        {
            int lncount = 0;
            for (int j = 1; j < ldt.Columns.Count - 1; j++)
            {
                lncount += Convert.ToInt32(ldt.Rows[i][ldt.Columns[j].ColumnName].ToString().Replace("%",""));
            }
            ldt.Rows[i]["合计"] = lncount;
        }
        ldt.Rows[4]["合计"] = Math.Round(Convert.ToDecimal(ldt.Rows[1]["合计"].ToString()) / (Convert.ToDecimal(ldt.Rows[2]["合计"].ToString()) == 0 ? 1 : Convert.ToDecimal(ldt.Rows[2]["合计"].ToString())) * 100, 0).ToString() + "%";
        ldt.Rows[5]["合计"] = Math.Round(Convert.ToDecimal(ldt.Rows[3]["合计"].ToString()) / (Convert.ToDecimal(ldt.Rows[6]["合计"].ToString()) == 0 ? 1 : Convert.ToDecimal(ldt.Rows[6]["合计"].ToString())) * 100, 0).ToString() + "%";
        ldt.Rows[3].Delete();
        ldt.Rows[5].Delete();
        ldt.AcceptChanges();
        return ldt;
    }
    private DataTable GetSum3(DataTable ldt)
    {
        ldt.Columns.Add("合计");
        for (int i = 0; i < 8; i++)
        {
            int lncount = 0;
            for (int j = 1; j < ldt.Columns.Count - 1; j++)
            {
                lncount += Convert.ToInt32(ldt.Rows[i][ldt.Columns[j].ColumnName].ToString().Replace("%",""));
            }
            ldt.Rows[i]["合计"] = lncount;
        }
        ldt.Rows[6]["合计"] = Math.Round((Convert.ToDecimal(ldt.Rows[2]["合计"].ToString())+ Convert.ToDecimal(ldt.Rows[3]["合计"].ToString())) / (Convert.ToDecimal(ldt.Rows[4]["合计"].ToString()) == 0 ? 1 : Convert.ToDecimal(ldt.Rows[4]["合计"].ToString())) * 100, 0).ToString() + "%";
        ldt.Rows[7]["合计"] = Math.Round(Convert.ToDecimal(ldt.Rows[5]["合计"].ToString()) / (Convert.ToDecimal(ldt.Rows[4]["合计"].ToString()) == 0 ? 1 : Convert.ToDecimal(ldt.Rows[4]["合计"].ToString())) * 100, 0).ToString() + "%";
        ldt.Rows[5].Delete();
        //ldt.Rows[8].Delete();
        ldt.AcceptChanges();
        return ldt;
    }
    //设定Gridview Header
    //protected void GridViewA_RowCreated(object sender, GridViewRowEventArgs e)
    //{        
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Text = ""; 
    //        for (int i = 2; i < e.Row.Cells.Count; i++)
    //        {
    //            if (e.Row.Cells[i].Text.ToUpper() == "100")
    //            {
    //                e.Row.Cells[i].Text = "合计";
    //            }
    //            else if (e.Row.Cells[i].Text == "AVG")
    //            {
    //                e.Row.Cells[i].Text = "平均";
    //            }
    //            //else
    //            //    e.Row.Cells[i].Controls.Add(lbtn);

    //        }

    //    }
    //    else
    //    {
    //        e.Row.Cells[0].Wrap = false;
    //        for (int i = 2; i < e.Row.Cells.Count; i++)
    //        {
    //            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
    //        }
    //    }
    //}
    ///// <summary>
    ///// C: 报价成功率
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewC_RowCreated(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Text = "";
    //        //e.Row.Cells[0].Width=200;
    //        for (int i = 2; i < e.Row.Cells.Count; i++)
    //        {

    //            if (e.Row.Cells[i].Text.ToUpper() == "100")
    //            {
    //                e.Row.Cells[i].Text = "合计";
    //            }
    //            else if (e.Row.Cells[i].Text == "AVG")
    //            {
    //                e.Row.Cells[i].Text = "平均";
    //            }
    //            //else
    //            //    e.Row.Cells[i].Controls.Add(lbtn);

    //        }

    //    }
    //    else
    //    {
    //            e.Row.Cells[0].Wrap = false;
    //            for (int i = 2; i < e.Row.Cells.Count; i++)
    //            {
    //                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
    //            }
    //    }
    //}

    ///// <summary>
    ///// 报价及时率
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewB_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    GridViewB.HeaderStyle.Wrap = false;
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {            
    //        e.Row.Cells[0].Text = "";
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            if (e.Row.Cells[i].Text == "100")
    //                e.Row.Cells[i].Text = "合计";
    //        }            
    //    }
    //    else
    //    {           
    //        e.Row.Cells[0].Wrap = false;
    //        for (int i = 2; i < e.Row.Cells.Count; i++)
    //        {               
    //            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;;
    //        }            
    //    }
    //}

    //protected void GridViewD_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Text = "";
    //        e.Row.Cells[1].Wrap=false;
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            if (e.Row.Cells[i].Text == "100")
    //                e.Row.Cells[i].Text = "合计";
    //        }

    //    }
    //    else
    //    {
    //        e.Row.Cells[0].Wrap = false;
    //        for (int i = 2; i < e.Row.Cells.Count; i++)
    //        {
    //            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right; ;
    //        }
    //    }
    //}
    ///// <summary>
    ///// A:报价零件数 
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewA_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
    //    //{
    //    //    e.Row.Cells[1].Style.Add("padding-left", "20px");
    //    //}

    //    //if (this.dropCondition.SelectedValue == "M")
    //    //{
    //    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    //    {

    //    //        if ( e.Row.Cells[0].Text == "1")//报价次数
    //    //        {
    //    //            //添加可点击Link 及前端识别属性：name
    //    //            for (int i = 2; i < e.Row.Cells.Count-1; i++)
    //    //            {

    //    //                if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim()!="")
    //    //                {
    //    //                    LinkButton lbtn = new LinkButton();
    //    //                    lbtn.ID = "lbtnA1" + e.Row.Cells[i-1].Text;
    //    //                    lbtn.Text = e.Row.Cells[i].Text;
    //    //                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
    //    //                    lbtn.Attributes.Add("name", "A");
    //    //                    lbtn.Attributes.Add("value", (i-1).ToString());//月分
    //    //                    lbtn.Attributes.Add("type", "A"+e.Row.Cells[0].Text); //1:报价次数-- 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
    //    //                    e.Row.Cells[i].Controls.Add(lbtn);
    //    //                    e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
    //    //                }

    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}
    ///// <summary>
    ///// B:报价及时率
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewB_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //if (e.Row.RowType == DataControlRowType.DataRow)
    //    //{
    //    //    if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
    //    //    {
    //    //        e.Row.Cells[1].Style.Add("padding-left", "20px");
    //    //    }
    //    //    if (this.dropCondition.SelectedValue == "M")//月份才加链接
    //    //    {
    //    //        if (e.Row.Cells[0].Text == "1")
    //    //        {
    //    //            //添加可点击Link 及前端识别属性：name
    //    //            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
    //    //            {
    //    //                if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
    //    //                {
    //    //                    LinkButton lbtn = new LinkButton();
    //    //                    lbtn.ID = "lbtnB1" + e.Row.Cells[i - 1].Text;
    //    //                    lbtn.Text = e.Row.Cells[i].Text;
    //    //                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
    //    //                    lbtn.Attributes.Add("name", "B");
    //    //                    lbtn.Attributes.Add("value", (i - 1).ToString());//月
    //    //                    lbtn.Attributes.Add("type", "B" + e.Row.Cells[0].Text); //1:报出次数 2:及时报出次数 
    //    //                    e.Row.Cells[i].Controls.Add(lbtn);
    //    //                    e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击变色
    //    //                }

    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}
    ///// <summary>
    ///// C: 报价成功率 GridViewC_RowDataBound
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewC_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //if (this.dropCondition.SelectedValue == "M")
    //    //{
    //    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    //    {
    //    //        if (e.Row.Cells[0].Text == "1"||e.Row.Cells[0].Text == "3")//1:报价项目数,3:定点项目数
    //    //        {
    //    //            //添加可点击Link 及前端识别属性：name
    //    //            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
    //    //            {
    //    //                if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
    //    //                {
    //    //                    LinkButton lbtn = new LinkButton();
    //    //                    lbtn.ID = "lbtnC1" + e.Row.Cells[i - 1].Text;
    //    //                    lbtn.Text = e.Row.Cells[i].Text;
    //    //                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
    //    //                    lbtn.Attributes.Add("name", "C" );
    //    //                    lbtn.Attributes.Add("value", (i - 1).ToString());//月分
    //    //                    lbtn.Attributes.Add("type", "C"+e.Row.Cells[0].Text); //1:报价项目数;2:  ;3:定点项目数 
    //    //                    e.Row.Cells[i].Controls.Add(lbtn);
    //    //                    e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
    //    //                }

    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}
    ///// <summary>
    ///// D: 报价天数
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GridViewD_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.Cells[0].Text == "2" || e.Row.Cells[0].Text == "3")//左边留空表示子项
    //    {
    //        e.Row.Cells[1].Style.Add("padding-left", "20px");
    //    }

    //    //if (this.dropCondition.SelectedValue == "M")
    //    //{
    //        //if (e.Row.RowType == DataControlRowType.DataRow)
    //        //{
    //        //    if(e.Row.Cells[0].Text == "1" || e.Row.Cells[0].Text=="2"|| e.Row.Cells[0].Text == "3")
    //        //    {

    //        //        for (int i = 2; i < e.Row.Cells.Count - 1; i++)
    //        //        {
    //        //            if (Server.HtmlDecode(e.Row.Cells[i].Text).Trim() != "")
    //        //            {
    //        //                LinkButton lbtn = new LinkButton();
    //        //                lbtn.ID = "lbtnD1" + e.Row.Cells[i - 1].Text;
    //        //                lbtn.Text = e.Row.Cells[i].Text;
    //        //                lbtn.Attributes.Add("href", @"javascript:void(0)','')");
    //        //                lbtn.Attributes.Add("name", "D");
    //        //                lbtn.Attributes.Add("value", (i - 1).ToString());//日

    //        //                lbtn.Attributes.Add("type", "D"+e.Row.Cells[0].Text); //2:报价中(逾时) 3:报价中(未逾时)                  

    //        //                e.Row.Cells[i].Controls.Add(lbtn);
    //        //                e.Row.Cells[i].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击

    //        //            }
    //        //        }
    //        //    }


    //        //}


    //   // }
    // }



    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
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
               
                e.Row.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            }
        }

    }
}