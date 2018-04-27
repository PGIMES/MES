using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class open_DetailByCondition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblName.Text = Request.QueryString["year"] + "-" + Request.QueryString["month"] + Request.QueryString["title"];
        ShowDetails(Request.QueryString["year"], Request.QueryString["month"], Request.QueryString["condition"],  Request.QueryString["type"]);
       
        List<int> list = new List<int>();
        for(int i=0;i<((DataTable)gvDetail.DataSource).Columns.Count;i++)
        {
            if (i != 6 && i != 7)//6:零件号  ;7:零件名称
            {
                list.Add(i);
            }
        }      
         
        // int[] cols = {0,1,2,3,4,5,8,9,10,11,12,13,14 }
         
        MergGridRow.MergeRow(gvDetail, list.ToArray());
        int rowIndex = 1;
        for(int i = 0; i <= gvDetail.Rows.Count - 1; i++)
        {
            if(gvDetail.Rows[i].Cells[0].Visible==true)
            {
                gvDetail.Rows[i].Cells[0].Text = rowIndex.ToString();
                rowIndex++;
            }
        }
    }
    //int rownum = 0;
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {                
           string requestid=Server.HtmlDecode(e.Row.Cells[0].Text.ToString()).Trim();           
           e.Row.Cells[1].Text = "<a href='baojia.aspx?requestid="+ requestid + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";
           e.Row.Cells[5].Style.Add("word-break", "break-all");
            e.Row.Cells[5].Style.Add("width", "100px");
        } 
        else if (e.Row.RowType == DataControlRowType.Header)
        {           
            e.Row.Cells[0].Text = "No.";
        }       
    }


    public void ShowDetails(string year, string month, string condition, string type)
    {       
        DataSet ds = DbHelperSQL.Query("exec Baojia_TJ_FenXi_ByCondition '" + year + "','" + month + "','" + condition + "','"+type+"'");
              
        gvDetail.DataSource = ds.Tables[0];
        gvDetail.DataBind();

    }
}