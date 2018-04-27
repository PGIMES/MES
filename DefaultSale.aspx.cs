using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.Services;
public partial class DefaultSale : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //获取设备维修状态context.Request.QueryString["flag"] 
    [System.Web.Services.WebMethod()]//或[WebMethod(true)]
    public static string GetEquipStatus(string equipno)
    {
        string result = "";
        DataSet ds = DbHelperSQL.Query("SELECT isnull([equip_repair_status],'')status  FROM [dbo].[MES_Equipment] where [equip_no]='" + equipno + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = ds.Tables[0].Rows[0][0].ToString();
        }
        return result;
    }
    //--插入开关机记录
    [WebMethod(true)]//或[WebMethod(true)]
    public static string InsertEquipLogStatus(string equipno, string logaction,string actionmark,string actionreason)
    {
        MES.DAL.MES_EquipActionLog dal = new MES.DAL.MES_EquipActionLog();
        string result = dal.Add(new MES.Model.MES_EquipActionLogModel() {  equip_no=equipno, logaction=logaction, actionmark=actionmark, actionreason=actionreason});
         
        return result;
    }

    [WebMethod]
    public static string SayHello()
    {
        return "Hello guest:datetime is "+DateTime.Now;
    }
}
