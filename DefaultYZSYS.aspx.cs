using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
public partial class DefaultYZSYS : System.Web.UI.Page
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
}
