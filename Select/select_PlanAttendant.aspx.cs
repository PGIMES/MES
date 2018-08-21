using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Maticsoft.DBUtility;

public partial class select_PlanAttendant : System.Web.UI.Page
{
    Function_Base Function_Base = new Function_Base();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }

    }
    protected void BtnStartSearch_Click(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        GetData();
    }
   
    private void GetData()
    {
        string sql = @"select workcode,lastname,dept_name,ROW_NUMBER() OVER (ORDER BY workcode) numid  
                    from [dbo].[HRM_EMP_MES] 
                    where workcode + lastname+ dept_name like '%{0}%' and workcode<>'{1}'";
        sql = string.Format(sql, this.txtKeywords.Text, Request["ApplyId"]);
        DataTable dt = new DataTable();
        dt = DbHelperSQL.Query(sql).Tables[0];
        if (dt == null || dt.Rows.Count <= 0)
        {
            lb_msg.Text = "No Data Found!";
        }
        gv.DataSource = dt;
        gv.DataBind();

        SelectionState();
    }

    //设置选中
    private void SelectionState()
    {
        string PlanAttendant = Request["PA"];
        string[] sArray = PlanAttendant.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);

        int count = 0;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            DataRowView row = gv.GetRow(i) as DataRowView;
            for (int j = 0; j < sArray.Length; j++)
            {
                if ((row["workcode"].ToString()+"("+ row["lastname"].ToString() + ")")== sArray[j])
                {
                    gv.Selection.SetSelection(i, true);
                    count++;
                }               
            }

            if (count == sArray.Length) { break; }//已经全部设置好
        }
    }
   
}