using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace WebForm.Common
{
    public class CustomFormSave
    {
        public static string QianShi(RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            string t = System.Web.HttpContext.Current.Request["TempTest.Title"];
            RoadFlow.Platform.Log.Add("获取值测试", t + "--");
            return "";
            //string title = System.Web.HttpContext.Current.Request.Form["Title"];
            //string Contents = System.Web.HttpContext.Current.Request.Form["Contents"];

            //if (eventParams.InstanceID.IsInt())
            //{
            //    string sql = "UPDATE TempTest_CustomForm SET Title=@Title,Contents=@Contents WHERE ID=@ID";
            //    SqlParameter[] parArray = { 
            //                 new SqlParameter("@Title", title),
            //                 new SqlParameter("@Contents", Contents),
            //                 new SqlParameter("@ID", eventParams.InstanceID.ToString())
            //                 };
            //    new RoadFlow.Data.MSSQL.DBHelper().Execute(sql, parArray);
            //    return eventParams.InstanceID.ToString();
            //}
            //else
            //{
            //    string sql = "INSERT INTO TempTest_CustomForm(Title,Contents,FlowCompleted) VALUES(@Title,@Contents,@FlowCompleted);SELECT SCOPE_IDENTITY();";
            //    SqlParameter[] parArray = { 
            //                 new SqlParameter("@Title", title),
            //                 new SqlParameter("@Contents", Contents),
            //                 new SqlParameter("@FlowCompleted", "0")
            //                 };
            //    return new RoadFlow.Data.MSSQL.DBHelper().ExecuteScalar(sql, parArray);
            //}
        }

        /// <summary>
        /// 子流程激活前事件（示例）
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static RoadFlow.Data.Model.WorkFlowExecute.Execute SubFlowActivationBefore(RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            RoadFlow.Data.Model.WorkFlowExecute.Execute execute = new RoadFlow.Data.Model.WorkFlowExecute.Execute();

            //在这里添加插入子流程业务数据代码
            RoadFlow.Data.MSSQL.DBHelper dbHelper = new RoadFlow.Data.MSSQL.DBHelper();
            DataTable mainDt = dbHelper.GetDataTable("select * from TepTest_GoOut where id=" + eventParams.InstanceID);
            if (mainDt.Rows.Count > 0)
            {
                string flwoTitle = "费用申请";
                string sql = "insert into TempTest_Free(title,note,squser,sqdate) values(@title,@note,@squser,@sqdate)";
                SqlParameter[] pars = new SqlParameter[] { 
                    new SqlParameter("@title",flwoTitle),
                    new SqlParameter("@note",""),
                    new SqlParameter("@squser",mainDt.Rows[0]["SqUser"].ToString()),
                    new SqlParameter("@sqdate",mainDt.Rows[0]["SqDate"].ToString().ToDateTime())
                };
                int instanceID = dbHelper.Execute(sql, pars, true);
                execute.Title = flwoTitle;
                execute.InstanceID = instanceID.ToString();
            }

            RoadFlow.Platform.Log.Add("执行了子流程激活前事件", "", RoadFlow.Platform.Log.Types.其它分类);

            return execute;
        }

        /// <summary>
        /// 子流程结束后事件（示例）
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static void SubFlowCompletedBefore(RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams)
        {

            //在这里添加子流程结束后代码

            RoadFlow.Platform.Log.Add("执行了子流程结束后事件", "", RoadFlow.Platform.Log.Types.其它分类);
        }


        /// <summary>
        /// 将资料从流程区抛到正式区
        /// </summary>
        /// <param name="id">RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams 流程实例id</param>
        /// <returns>int; 值=0 失败；>0 成功</returns>
        //[System.Web.Services.WebMethod()]
        public static int PaoZiLiao(RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            var result = 0;
            int i = DbHelperSQL.ExecuteSql(string.Format(" insert into [dbo].[PGI_BASE_PART_DATA]( class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh, wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5) " +
                                                                                         " select  class, coping_lb, type, typeDesc, ISYX, Domain, factory, djlx, pp, pt_status, wlh, wlmc, ms, gysdm, gys, jg, edsm, edxmcs, lycs, lycs_week, dwcb, zpjyl, xyph, djjs, zongchang, otherms, cpl, aqkc, UPLOAD, dhzq, quantity_min, quantity_max, ddbs, buyer_planner, purchase_make, make_days, purchase_days, lycs_fweek, lycs_tweek, 分割, var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, float1, float2, float3, float4, float5 " +
                                                        " from[dbo].[rf_PGI_BASE_PART_DATA]  where id = '{0}'", eventParams.InstanceID));

            return result;
        }
    }
}