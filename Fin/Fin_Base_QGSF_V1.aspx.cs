using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fin_Fin_Base_QGSF_V1 : System.Web.UI.Page
{
    string lMergeFileds = "com_comm_code;com_desc;productcode;BaseRate;301code;301Rate;immunity;domain;wlh;";//预定义需要合并的列名集合
    string sMergeByKey = "wlh";//根据哪列进行单元格合并，在这里定义，比如我根据业务编号分组合并，即相同业务编号内合并

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


        }
        QueryASPxGridView();
    }

    protected void Bt_select_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "setHeight();", true);
    }

    public void QueryASPxGridView()
    {
        string sql = @"exec usp_Fin_Base_QGSF_select '{0}'";
        sql = string.Format(sql, txt_wlh.Text.Trim());
        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        GV_PART.DataSource = dt;
        GV_PART.DataBind();
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("产品税率_" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel
    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "merge", "clear();setHeight();", true);
    }



    protected void GV_PART_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
    {
        /*
        *在这种情况下，可以处理CustomCellMerge事件来手动实现单元格合并。该事件对列中的每对相邻单元格激发。
        *事件参数属性提供有关已处理列（列）、包含已处理单元格的行的可见索引（RowVisibleIndex1和RowVisibleIndex2）及其值（Value1和Value2）的信息。
        * 若要提供自定义合并逻辑，请将Handled属性设置为true，并使用Merge属性指定是否应合并当前处理的单元格。如           果合并单元格，则结果单元格值等于Value1。
             
        *注意：行合并后，会无法选定焦点行，焦点行失效，不出现焦点行，也无法选定当前行上屏修改数据
        */
        string sFiledName = "";//当前单元格所在列的列名定义
        sFiledName = e.Column.FieldName;// ((GridViewEditDataColumn)e.Column).FieldName;//由于e.Column继承GridViewEditDataColumn父类，所以强转成父类然后调用FieldName即可获取列名

        if (lMergeFileds.Contains(sFiledName) == true || sFiledName == "")//lMergeFileds:List集合,即需要合并列的列名集合，sFiledName：当前单元格所在列名
        {
            int iFirst_Row = e.RowVisibleIndex1;//当前行的行号
            int iSecond_Row = e.RowVisibleIndex2;//下一行的行号
            object oFirst_Value = e.Value1;//当前行单元格的值
            object oSecond_Value = e.Value2;//下一行单元格的值
            object oYwbh_First = GV_PART.GetRowValues(iFirst_Row, sMergeByKey);//获取当前行关键列的单元格的值，注:关键列是指依据哪列进行合并的列名（字符型)
            object oYwbh_Second = GV_PART.GetRowValues(iSecond_Row, sMergeByKey);//获取第二行关键列的单元格的值，注:关键列是指依据哪列进行合并的列名（字符型)

            if (oYwbh_First.Equals(oYwbh_Second))//当第一行业务编号与第二行业务编号相同时
            {
                if (oFirst_Value.Equals(oSecond_Value))//当第一行单元格的值与第二行单元格的值相同时
                {
                    e.Merge = true;//合并
                }
                else//当第一行单元格的值与第二行单元格的值不相同时
                {
                    e.Merge = false;//不合并
                }
            }
            else//当第一行业务编号与第二行业务编号不相同时
            {
                e.Merge = false;
            }

        }

        e.Handled = true;//关键代码：此句负责执行上面的合并，刷新客户端的表格中的合并情况
    }

}