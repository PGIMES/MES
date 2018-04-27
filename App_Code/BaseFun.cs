using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Maticsoft.DBUtility;
using System.Web.UI;
using System.Web.UI.WebControls;
using MES.Model;
using System.Collections;
using System.Reflection;

/// <summary>
///BaseFun 的摘要说明
/// </summary>
public class BaseFun
{
	public BaseFun()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 简单SQL查询
    /// </summary>
    /// <param name="SQLString"></param>
    /// <returns></returns>
    public DataTable GetList(string SQLString)
    {        
        return DbHelperSQL.Query(SQLString).Tables[0];
    }    
    /// <summary>
    /// 初始化DropDownList
    /// </summary>
    /// <param name="drop"></param>
    /// <param name="tbl"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public void initDropDownList(DropDownList drop, DataTable tbl,string value,string text)
    {
        drop.DataSource = tbl;
        drop.DataValueField = value;
        drop.DataTextField = text;
        drop.DataBind();
    }
    /// <summary>
    /// 初始化CheckBoxList
    /// </summary>
    /// <param name="chkL"></param>
    /// <param name="tbl"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public void initCheckBoxList(CheckBoxList chkL, DataTable tbl, string value, string text)
    {
        chkL.DataSource = tbl;
        chkL.DataValueField = value;
        chkL.DataTextField = text;
        chkL.DataBind();
    }
     /// <summary>
     /// 初始化合金
     /// </summary>
     /// <param name="drop"></param>
    public void initHeJin(DropDownList drop)
    {
        MES_HeJin model = new MES_HeJin();
        List<MES_HeJin> list = model.MES_HeJins();
        drop.DataSource = list;
        drop.DataValueField = "id";
        drop.DataTextField = "name";
        drop.DataBind();
    }

    public void initEquipment(CheckBoxList chkId,string sqlWhere)
    {
        MES.DAL.MES_Equipment eq = new MES.DAL.MES_Equipment();
        DataTable tbl = eq.GetList(sqlWhere).Tables[0];
        chkId.DataSource = tbl;
        chkId.DataValueField = "equip_no";
        chkId.DataTextField = "equip_name";
        chkId.DataBind();
    }

    /// <summary>
    /// 根据工号取设备人员记录
    /// </summary>
    /// <param name="empno"></param>
    /// <returns></returns>
    public DataTable getSheBeiEmpInfo(string empno)
    {   
        StringBuilder strSQL = new StringBuilder();    
        strSQL.Append(" select dept.UNITNAME as dept_name,PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME,ORGSTDSTRUCT.UNITNAME as zz ");
        strSQL.Append(" from [172.16.5.6].[ehr_db].dbo.psnaccount left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID ");
        strSQL.Append(" left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0 ");
        strSQL.Append(" where PSNACCOUNT.accessionstate in(1,2,6) and dept.UNITNAME in('设备一部','设备二部')");
        if(empno!="")
        {
            strSQL.Append(" and PSNACCOUNT.EMPLOYEEID='"+empno +"'");
        }
         strSQL.Append("  order by  PSNACCOUNT.EMPLOYEEID  ");
        DataSet ds=DbHelperSQL.Query(strSQL.ToString());
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据工号取模修人员记录
    /// </summary>
    /// <param name="empno"></param>
    /// <returns></returns>
    public DataTable getMoXiuEmpInfo(string empno)
    {   
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select EMPLOYEEID,EMPLOYEEID+'_'+TRUENAME EMPLOYEENAME ,TRUENAME from  [172.16.5.6].[ehr_db].dbo.psnaccount ");
        strSQL.Append(" where BRANCHID in( select UNITID from  [172.16.5.6].[ehr_db].dbo.ORGSTDSTRUCT where unitcode like '0201020401%' and ISTEMPUNIT=0 ) ");
        
        if(empno!="")
        {
            strSQL.Append(" and EMPLOYEEID='" + empno +"'");
        }
         strSQL.Append("  order by  EMPLOYEEID  ");
        DataSet ds=DbHelperSQL.Query(strSQL.ToString());
        return ds.Tables[0];
    }
    /// <summary>
    /// 根据工号取人员信息
    /// </summary>
    /// <param name="empno"></param>
    /// <returns>DataTable:dept_name,EMPLOYEEID,EmpName,UNITNAME as zz</returns>
    public DataTable getEmpInfo(string empno)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select dept.UNITNAME as dept_name,PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME EmpName,ORGSTDSTRUCT.UNITNAME as zz ");
        strSQL.Append(" from [172.16.5.6].[ehr_db].dbo.psnaccount left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID ");
        strSQL.Append(" left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0 ");
        strSQL.Append(" where PSNACCOUNT.accessionstate in(1,2,6) ");
        if (empno != "")
        {
            strSQL.Append(" and PSNACCOUNT.EMPLOYEEID='" + empno + "'");
        }
        strSQL.Append("  order by  PSNACCOUNT.EMPLOYEEID  ");
        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        return ds.Tables[0];
    }
    /// <summary>
    /// 根据工号取姓名
    /// </summary>
    /// <param name="empno"></param>
    /// <returns>string</returns>
    public  string getEmpNameById(string empno)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select dept.UNITNAME as dept_name,PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME EmpName,ORGSTDSTRUCT.UNITNAME as zz ");
        strSQL.Append(" from [172.16.5.6].[ehr_db].dbo.psnaccount left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID ");
        strSQL.Append(" left join [172.16.5.6].[eHR_DB].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0 ");
        strSQL.Append(" where PSNACCOUNT.accessionstate in(1,2,6) ");
        if (empno != "")
        {
            strSQL.Append(" and PSNACCOUNT.EMPLOYEEID='" + empno + "'");
        }        
        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        { return ds.Tables[0].Rows[0]["EmpName"].ToString(); }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 获取员工姓名 by 工号
    /// </summary>
    /// <param name="EmpId"></param>
    /// <returns></returns>
    public static  string getEmpNameByEmpId(string EmpId)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("  select lastname as EmpName from HRM_EMP_MES where workcode='"+ EmpId +"' ");       
        
        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["EmpName"].ToString(); }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 获取员工邮件 by 工号
    /// </summary>
    /// <param name="EmpId"></param>
    /// <returns></returns>
    public static string getMailByEmpId(string EmpId)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("  select email  from HRM_EMP_MES where workcode='" + EmpId + "' ");

        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["email"].ToString();
        }
        else
        {
            return "";
        }
    }
    
    /// <summary>
    /// 获取员工主管邮件
    /// </summary>
    /// <param name="EmpId"></param>
    /// <returns></returns>
    public static string getDirManagerMailByEmpId(string EmpId)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("  select *   from HRM_EMP_MES where workcode='" + EmpId + "' ");

        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {             
            return ds.Tables[0].Rows[0]["manager_email"].ToString();
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 初始化班别
    /// </summary>
    /// <returns></returns>
    public string GetBanBie()
    {
        string banbie = "";
        string time = DateTime.Now.ToString("HHmm");
        if (string.Compare(time, "0730") > 0 && string.Compare(time, "1730") < 0)
        {
            banbie = "白班";
        }
        else
        { banbie = "晚班"; }
        return banbie;
    }
    /// <summary>
    /// 初始化客户大类
    /// </summary>
    /// <returns></returns> 
    public static void loadCustClass(DropDownList ddl, string value, string text)
    {
        string strSql = "SELECT '' as value,'' as text union all SELECT name as value,name as text  FROM [dbo].[Baojia_base] where type='End_Customer' and  classify='baojia' AND isyx='Y' ORDER BY value";
        DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
        ddl.DataSource = dt;
        ddl.DataValueField = "value";
        ddl.DataTextField = "text";
        ddl.DataBind();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#CPFZR').removeClass('collapse');$('#cpyc').removeClass('collapse');", true);
    }
    /// <summary>
    /// 初始化部门下拉
    /// </summary>
    public static  void loadDepartment(DropDownList drop,string domain)
    {
        string sql = "select distinct dept_name value,dept_name text from [dbo].[HRM_EMP_MES] where domain='"+domain+"' or ''=''";
        DataSet ds = DbHelperSQL.Query(sql);
        drop.DataSource = ds;
        drop.DataTextField = "text";
        drop.DataValueField = "value";
        drop.DataBind();
    }
    /// <summary>
    /// 根据部门id 带出该部门在职人员
    /// </summary>
    /// <param name="DepartId"></param>
    public static void loadEmpByDepartId(string domain,string DepartId,DropDownList drop)
    {
        string sql = "select workcode value,lastname text from [dbo].[HRM_EMP_MES] where (domain='"+domain+"' or ''='') and ( dept_name='"+ DepartId + "')";
        DataSet ds = DbHelperSQL.Query(sql);
        drop.DataSource = ds;
        drop.DataTextField = "text";
        drop.DataValueField = "value";
        drop.DataBind();

    }
    /// <summary>
    /// 设定DropDownList选定值
    /// </summary>
    /// <param name="drop"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public static void setDropSelectValue(DropDownList drop, string value, string text)
    {
        drop.SelectedIndex = -1;
        if (value != "")
        {
            if (drop.Items.FindByValue(value) != null)
            {
                drop.SelectedValue = value;

            }
        }
        else if (text != "")
        {
            ListItem item = drop.Items.FindByText(text);
            if (item != null)
            {
                item.Selected = true;
            }
        }
        else
            drop.SelectedIndex = -1;

    }



    // 获取维修记录
    public DataTable Get_WX_Record(string dh)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select * from MES_SB_WX  ");
        strSQL.Append(" where wx_dh='"+dh+"'");
        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        return ds.Tables[0];
    }
    

}
/// <summary>
/// Convert DataTable to List
/// </summary>
/// <typeparam name="T"></typeparam>
public class ModelConvertHelper<T> where T : new()  // 此处一定要加上new()
{

    public static IList<T> ConvertToModel(DataTable dt)
    {

        IList<T> ts = new List<T>();// 定义集合
        Type type = typeof(T); // 获得此模型的类型
        string tempName = "";
        foreach (DataRow dr in dt.Rows)
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;
                if (dt.Columns.Contains(tempName))
                {
                    if (!pi.CanWrite) continue;
                    object value = dr[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
            }
            ts.Add(t);
        }
        return ts;
    }
    public static void BindGrid(GridView grid, List<T> list)
    {
        //处理查无资料结果显示效果：添加一行显示友好一点
        bool isEmpty = false;
        if (list.Count == 0)
        {
            isEmpty = true;
            list.Add(new T());
        }

        grid.DataSource = list;
        grid.DataBind();
        if (isEmpty == true)
        {
            int columnCount = grid.Columns.Count;
            grid.Rows[0].Cells.Clear();
            grid.Rows[0].Cells.Add(new TableCell());
            grid.Rows[0].Cells[0].ColumnSpan = columnCount;
            grid.Rows[0].Cells[0].Text = "没有记录";
            grid.Rows[0].Cells[0].Style.Add("text-align", "center");
        };
    }
}