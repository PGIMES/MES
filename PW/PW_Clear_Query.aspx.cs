using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class PW_PW_Clear_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();

            //设备   
            fun.initDropDownList(dropShebei, DbHelperSQL.Query(@"select  rtrim(equip_name) as equip_no,equip_name from MES_Equipment where inuse!='N' and equip_station_desc='pw'").Tables[0], "equip_no", "equip_name");
            dropShebei.Items.Insert(0, "");
            //初始化年份    
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }
            //初始化月份
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化日期
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txtDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime dt = DateTime.Now;



        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strWhere = GetstrWhere();

        DataTable tbl = SQLHelper.reDs(strWhere).Tables[0];

        GridView1.DataSource = tbl;

        GridView1.DataBind();
    }
    public string GetstrWhere()
    {
        string strWhere = "select * from MES_PW_Clear where 1=1";
        if (txtDateTo.Text.Trim() != "")
        {
            strWhere = strWhere + " and checkdate<convert(datetime,'" + txtDateTo.Text.Trim() + " 23:59:59')";
        }
       
        if (txtEmpName.Text.Trim() != "")
        {
            strWhere = strWhere + " and emp_no+emp_name like '%" + txtEmpName.Text.Trim() + "%' ";
        }

        if (dropBanBie.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and emp_banbie = '" + dropBanBie.SelectedValue.Trim() + "'";
        }
        if (dropShebei.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and equip_name = '" + dropShebei.SelectedValue.Trim() + "'";
        }

        if (dropYear.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and convert(varchar(4),checkdate,111)=" + dropYear.SelectedValue.Trim() + "";
        }
        if (txtDateFrom.Text.Trim() != "")
        {
            strWhere = strWhere + " and checkdate > convert(datetime,'" + txtDateFrom.Text.Trim() + " 00:00:00')";
        }
        strWhere = strWhere + " order by checkdate desc";

        return strWhere;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        string strWhere = GetstrWhere();

        DataTable tbl = SQLHelper.reDs(strWhere).Tables[0];
        GridView1.DataSource = tbl;
        GridView1.DataBind();
    }
}