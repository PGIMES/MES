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
using System.Collections.Generic;


/// <summary>
///GetBase 的摘要说明
/// </summary>
public class GetBase
{
	public GetBase()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public DataTable Getzzr_Dept()
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("  select distinct dept_name from (select distinct file_zzr from [172.16.5.6].[report].dbo.track_file)a join (select PSNACCOUNT.EMPLOYEEID,PSNACCOUNT.TRUENAME,dept.UNITNAME as dept_name,cpcjobcode.names from [172.16.5.6].[ehr_db].dbo.PSNACCOUNT");
        strSQL.Append(" left join [172.16.5.6].[ehr_db].dbo.CPCPOSIITEM on PSNACCOUNT.GRADEID=CPCPOSIITEM.posiitemid left join [172.16.5.6].[ehr_db].dbo.cpcjobcode on PSNACCOUNT.JOBCODE=cpcjobcode.JOBCODEID ");
        strSQL.Append(" left join [172.16.5.6].[ehr_db].dbo.ORGSTDSTRUCT on PSNACCOUNT.BRANCHID=ORGSTDSTRUCT.UNITID left join [172.16.5.6].[ehr_db].dbo.PSNCONTACTINFO on PSNACCOUNT.PERSONID=PSNCONTACTINFO.PERSONID");
        strSQL.Append(" left join [172.16.5.6].[ehr_db].dbo.ORGSTDSTRUCT dept on left(ORGSTDSTRUCT.UNITCODE,8)=dept.UNITCODE and dept.ISTEMPUNIT=0 ) b on b.truename=file_zzr where b.dept_name is not null");

        DataSet ds = DbHelperSQL.Query(strSQL.ToString());
        return ds.Tables[0];
    }
    public DataSet GetPwResult(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from [dbo].[MES_PW_Clear] where cast(checkdate as date)=cast(getdate() as date) ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" and  " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }
    public DataSet GetPwsxResult(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from [dbo].[MES_PW_Clear] where datename(week,getdate())=datename(week,checkdate) ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" and  " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }

    public DataSet GetPwAdd(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("SELECT WL FROM MES_PW_Add WHERE add_date=(select max(add_date) from MES_PW_Add ) ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" and  " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }


}