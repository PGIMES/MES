using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using IBatisNet.Common.Transaction;
using System.Web.Script.Services;
using System.Web.Services;
using MES.Model;
using MES.DAL;
using Maticsoft.Common;
using System.Collections;
using System.Reflection;
using System.Linq;

public partial class Template : System.Web.UI.Page
{
      
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
       
       // this.gv_rz2.PageSize = 100;
       
        //                                              02128  00404      00076  01968  
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("02069", Request.ServerVariables["LOGON_USER"]);
        Session["LogUser"] = LogUserModel;
        
        if (!IsPostBack)
        { 
            if (LogUserModel != null)
            {
                //当前登陆人员
                txt_LogUserId.Value = LogUserModel.UserId;
                txt_LogUserName.Value = LogUserModel.UserName;
                txt_LogUserJob.Value = LogUserModel.JobTitleName;
                txt_LogUserDept.Value = LogUserModel.DepartName;


                List<TableRow> ls = Pgi.Auto.Control.ShowControl("TEST", "", 3, "rows", "column", "");
                for (int i = 0; i < ls.Count; i++)
                {
                    this.tblWLLeibie.Rows.Add(ls[i]);
                }

                if (Request["requestid"] != null)//页面加载
                {
                    string requestid = Request["requestid"];                     
                     
                    //日志加载                     
                    bindrz2_log(requestid, gv_rz2);
                }
                else//新建
                {
                             
                    this.txt_CreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txt_CreateById.Value = LogUserModel.UserId;
                    txt_CreateByName.Value = LogUserModel.UserName;
                    txt_CreateByAd.Value = LogUserModel.ADAccount;
                    txt_CreateByDept.Value = LogUserModel.DepartName; 
                    txt_managerid.Value = LogUserModel.ManagerId; 
                    txt_manager.Value = LogUserModel.ManagerName; 
                    txt_manager_AD.Value = LogUserModel.ManagerADAccount;
                    txt_LogUserId.Value = LogUserModel.UserId;
                    txt_LogUserName.Value = LogUserModel.UserName; 
                                       
                    
                }
            }
        }
    }

 
    [System.Web.Services.WebMethod()]//或[WebMethod(true)]
    public static string setDefaultValue(string id)
    {
        string result = "";
        DataSet ds = DbHelperSQL.Query(string.Format("SELECT distinct   DebtorCode,sqgc,cp_domain FROM [MES].[dbo].[V_form1_ljh] where pt_part='{0}' and DebtorCode<>'19999'" , id)  );
        string value1="";
        string value2="";
        if (ds.Tables[0].Rows.Count > 0)
        {
             value1 = ds.Tables[0].Rows[0]["cp_domain"].ToString();
             value2 = ds.Tables[0].Rows[0]["DebtorCode"].ToString();
        }
        DataSet ds2= DbHelperSQL.Query(string.Format("SELECT [Customer_DL] ,[Customer_DM] ,[Customer_MC] FROM [MES].[dbo].[Sale_CustID] where Customer_DM='{0}' ", value2));
        if (ds.Tables[0].Rows.Count > 0)
        {
            value2 = ds2.Tables[0].Rows[0]["Customer_DL"].ToString();
        }
         
        result = "{\'domain\':\'" + value1 + "\',\'cust\':\'" + value2 + "\'}";
        
        return result;
    }
   

    public DateTime addDaysExceptHoliday(DateTime startDate,int days)
    {
        DateTime dt=startDate;
        string sql = "select date from (SELECT top " + days + "  [DATE]  FROM [MES].[dbo].[WORK_HOLIDAYS_LIST] where   IS_HOLIDAY>1 and date>cast('"+ startDate + "' as date)  order by date asc)t order by date desc";
        dt = Convert.ToDateTime(DbHelperSQL.GetSingle(sql));
        return dt;
    }
    

   //加载问题主档
    public void loadProbByRequestId(int requestid)
    {
        MES.DAL.Q_Review_Prob dal = new MES.DAL.Q_Review_Prob();
        MES.Model.Q_Review_Prob m = dal.GetModel(requestid);
        txt_CreateById.Value = m.EmpId;
        txt_CreateByName.Value = m.EmpName;
        txt_CreateByDept.Value = m.Dept;
        txt_CreateDate.Value = string.Format("{0:yyyy-MM-dd}", m.CreateDate); 
                  
    }
   

    #region "日志"
    
    public void bindrz_log(string requestid, GridView gv_rz1)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM  [Q_ReView_LOG] ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz1.DataSource = dt;
        gv_rz1.DataBind();
        gv_rz1.PageSize = 100;
    }
    public void bindrz2_log(string requestid, GridView gv_rz2)
    {
        StringBuilder sql = new StringBuilder();//--baojia_no as 报价号, turns as 轮次,
        sql.Append("  SELECT * FROM Q_ReView_LOG ");
        sql.Append("    where requestid = '" + requestid + "'  order by id asc");
        DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
        gv_rz2.DataSource = dt;
        gv_rz2.DataBind();
        gv_rz2.PageSize = 100;
    }
  
    
   #endregion
    
    //问题提交
    protected void btnSaveProb_Click(object sender, EventArgs e)
    {
        #region "检查"
        string strErr = "";
        if (this.txt_CreateById.Value.Trim().Length == 0)
        {
            strErr += "登入人工号不能为空！\\n";
        }
        if (this.txt_CreateByName.Value.Trim().Length == 0)
        {
            strErr += "姓名不能为空！\\n";
        }
        if (this.txt_CreateByDept.Value.Trim().Length == 0)
        {
            strErr += "部门不能为空！\\n";
        }        
        
        #endregion

        if (strErr != "")
        {
            MessageBox.Show(this, strErr);
            return;
        }

         
        //Maticsoft.Common.MessageBox.Show(this.Page, "保存成功！");
       
    }
    
    public  void MergeRows(GridView gvw, int col, string controlNameo)
    {
        //for (int col = 0; col < colnum; col++) // 遍历每一列
        //{}
        string controlName = controlNameo;// + col.ToString(); // 获取当前列需要改变的Lable控件ID
            for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex--) //GridView中获取行数 并遍历每一行
            {
                GridViewRow row = gvw.Rows[rowIndex]; // 获取当前行
                GridViewRow previousRow = gvw.Rows[rowIndex + 1]; // 获取当前行 的上一行
                Label row_lbl = row.Cells[col].FindControl(controlName) as Label; //// 获取当前列当前行 的 Lable 控件ID 的文本
                Label previousRow_lbl = previousRow.Cells[col].FindControl(controlName) as Label; //// 获取当前列当前行 的上一行 的 Lable控件ID 的文本
                if (row_lbl != null && previousRow_lbl != null) // 如果当前行 和 上一行 要改动的 Lable 的ID 的文本不为空
                {
                    if (row_lbl.Text.Replace(" ","") == previousRow_lbl.Text.Replace(" ", "")) // 如果当前行 和 上一行 要改动的 Lable 的ID 的文本不为空 且相同
                    {
                        // 当前行的当前单元格（单元格跨越的行数。 默认值为 0 ） 与下一行的当前单元格的跨越行数相等且小于一 则 返回2 否则让上一行行的当前单元格的跨越行数+1
                        row.Cells[col].RowSpan = previousRow.Cells[col].RowSpan < 1 ? 2 : previousRow.Cells[col].RowSpan + 1;
                        //并让上一行的当前单元格不显示
                        previousRow.Cells[col].Visible = false;
                    }
                }
            }
        
    }
  
    //不同意关闭--暂时不使用此功能
    [WebMethod(true)]
    public static string DisClose(string requestid,string desc)
    {
        System.Text.StringBuilder result = new StringBuilder();
        result.Append(string.Format("update [dbo].[Q_Review_Prob] set [ActualState]='{0}'  where [RequestId]='{1}' ", "未解决", requestid));

        int rows = DbHelperSQL.ExecuteSql(result.ToString());
        result.Clear();
        if (rows == 0)
        {
            result.Append("0");//失败
        }
        else
        {
            result.Append("1"); //成功
        }

        //SendEmail To All
        
        DataTable dt = DbHelperSQL.Query("select distinct dutyEmp from Q_Review_ProbDuty where requestid='"+ requestid + "'").Tables[0];

        var mailgroup = "";
        var dutyemps = "";
        for(int row = 0; row < dt.Rows.Count; row++)
        {
            mailgroup = mailgroup + BaseFun.getMailByEmpId(dt.Rows[row]["DutyEmp"].ToString())+",";
            dutyemps = dutyemps + BaseFun.getEmpNameByEmpId(dt.Rows[row]["DutyEmp"].ToString()) + "，";
        }
        mailgroup = mailgroup.TrimEnd(',');
        dutyemps = dutyemps.TrimEnd(',');
        MES.DAL.Q_Review_Prob dal = new MES.DAL.Q_Review_Prob();
        MES.Model.Q_Review_Prob M = new MES.Model.Q_Review_Prob();
         
        M = dal.GetModel(Convert.ToInt16(requestid));
        if (mailgroup != "")
        {
            StringBuilder body = new StringBuilder();
            body.Append("Hi :");           

            string recipient = mailgroup;

            Maticsoft.Common.MailSender.Send(recipient, "问题不同意关闭,请重新评估改善措施.[问题系统]", body.ToString());
        }
        return result.ToString();
         
    }
    
  
}


