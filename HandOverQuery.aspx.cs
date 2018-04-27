using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class HandOverQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            BaseFun fun = new BaseFun();
            //车间初始化
            fun.initDropDownList(dropCheJian, DbHelperSQL.Query("select * from MES_BasType where type='chejian'").Tables[0], "value1", "value1");
            dropCheJian.Items.Insert(0,"");
            if (dropCheJian.Items.FindByValue("压铸")!=null)
            {
                dropCheJian.SelectedValue = "压铸";
            }
            //岗位,初始化
           // fun.initDropDownList(dropGangWei, DbHelperSQL.Query("select distinct rtrim(gongwei) as value1 from MES_Equipment where inuse!='N'").Tables[0], "value1", "value1");
            //区域
            fun.initDropDownList(dropGangWei, DbHelperSQL.Query("select distinct rtrim(equip_station_desc) as equip_station_desc , equip_station  from MES_Equipment where inuse!='N'").Tables[0], "equip_station_desc", "equip_station");
            dropGangWei.Items.Insert(0,"");
            //设备   
            fun.initDropDownList(dropShebei, DbHelperSQL.Query(@"select  rtrim(equip_no) as equip_no,equip_name from MES_Equipment where inuse!='N' and equip_station_desc='" + Request["quyu"] + "'").Tables[0], "equip_no", "equip_name");
            dropShebei.Items.Insert(0, "");
            //初始化年份    
            int year=Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for(int i=0;i<5;i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }
            //区域
            if (dropGangWei.Items.FindByValue(Request["quyu"]) != null)
            {
                dropGangWei.SelectedValue = Request["quyu"];
            }
            //工位           
            //if (dropGangWei.Items.FindByValue(Request["gongwei"]) != null)
            //{
            //    dropGangWei.SelectedValue = Request["gongwei"];
            //}
            
            
            //初始化月份
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem( i.ToString()+"月" , i.ToString()  ));
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            //初始化日期
            txtDateFrom.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime dt=DateTime.Now;            
            for(int k = 1; k <= 7; k++) 
            {                 
                string day=dt.DayOfWeek.ToString();
                if (day == "Monday")
                {   
                    txtDateFrom.Text=dt.ToString("yyyy/MM/dd");
                    return;
                }
                else 
                { dt = dt.Date.AddDays(-k); }

            }
            
            //txtDateFrom.Text=
            //初始岗位
            //string strSql = "select distinct gongwei from [dbo].[MES_Equipment]";
            //DataTable tbl = DbHelperSQL.Query(strSql).Tables[0];
            //fun.initDropDownList(dropGangWei, tbl, "gongwei", "gongwei");
            //dropGangWei.Items.Insert(0, new ListItem("-请选择", "0"));

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strWhere = GetstrWhere();


        MES.BLL.MES_EmpLogin bll=new MES.BLL.MES_EmpLogin();
        DataTable tbl = bll.GetListByPage(strWhere,"logindate desc",1,200 ).Tables[0];
        //DataTable tbl = bll.GetList(10,0, strWhere).Tables[0];
        
        GridView1.DataSource = tbl;
        
        GridView1.DataBind();
  


    }
    public string GetstrWhere()
    {
        string strWhere = " 1=1 and T.emp_shebei=E.equip_no";
        if (txtDateTo.Text.Trim() != "")
        {
            strWhere = strWhere + " and logindate<convert(datetime,'" + txtDateTo.Text.Trim() + " 23:59:59')";
        }  
        if (txtEmpName.Text.Trim() != "")
        {
            strWhere = strWhere + " and emp_no+emp_name like '%" + txtEmpName.Text.Trim() + "%' ";
        }
        if (dropGangWei.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and E.equip_station_Desc = '" + dropGangWei.Text.Trim() + "'";
        }
        if (dropBanBie.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and emp_banbie = '" + dropBanBie.SelectedValue.Trim() + "'";
        }
        if (dropShebei.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and T.emp_shebei = '" + dropShebei.SelectedValue.Trim() + "'";
        }
        if (this.dropCheJian.SelectedItem.Value.Trim() != "")
        {
            strWhere = strWhere + " and emp_chejian like '%" + dropCheJian.SelectedItem.Value.Trim() + "%'";
        }
        if (dropYear.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and convert(varchar(4),logindate,111)=" + dropYear.SelectedValue.Trim() + "";
        }
        if (dropMonth.SelectedValue.Trim() != "")
        {
            strWhere = strWhere + " and month(logindate)=" + dropMonth.SelectedValue.Trim() + "";
        }
        if (txtDateFrom.Text.Trim() != "")
        {
            strWhere = strWhere + " and logindate > convert(datetime,'" + txtDateFrom.Text.Trim() + " 00:00:00')";
        }
 
        
        return strWhere;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        string strWhere = GetstrWhere();
        MES.BLL.MES_EmpLogin bll = new MES.BLL.MES_EmpLogin();
        DataTable tbl = bll.GetListByPage(strWhere, "logindate desc", 1, 200).Tables[0];
        
        GridView1.DataSource = tbl;
        GridView1.DataBind();
    }
    protected void dropGangWei_SelectedIndexChanged(object sender, EventArgs e)
    {
        BaseFun fun = new BaseFun();
        //设备   
        fun.initDropDownList(dropShebei, DbHelperSQL.Query(@"select  rtrim(equip_no) as equip_no,equip_name from MES_Equipment where inuse!='N' and equip_station_desc='" + dropGangWei.SelectedValue.Trim() + "'").Tables[0], "equip_no", "equip_name");
        dropShebei.Items.Insert(0, "");
    }
}