using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Drawing;

/// <summary>
/// Summary description for control
/// </summary>
public class control
{
	public control()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //ListBox
    public void ListBox_Bind(System.Web.UI.WebControls.ListBox lb, DataSet ds, int value, int text)
    {
      
        lb.Items.Clear();//清空他的值
        if (ds == null)
        {
            return;
        }
        if (ds.Tables.Count == 0)
        {
            return;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ListItem litem = new ListItem();
            litem.Text = ds.Tables[0].Rows[i][text].ToString().Trim();
            litem.Value = ds.Tables[0].Rows[i][value].ToString().Trim();
            lb.Items.Add(litem);
        }

    

    }


    //CheckBoxList
    public void CheckBoxList_Bind(System.Web.UI.WebControls.CheckBoxList cklist,DataSet ds,int value,int text)
    {
       

        cklist.Items.Clear();//清空他的值
        if (ds == null)
        {
            return;
        }
        if (ds.Tables.Count == 0)
        {
            return;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ListItem litem = new ListItem();
            litem.Text = ds.Tables[0].Rows[i][text].ToString().Trim();
            litem.Value = ds.Tables[0].Rows[i][value].ToString().Trim();
            cklist.Items.Add(litem);
        }
     

    }

    //RadioButtonList
    public void RadioButtonList_Bind(System.Web.UI.WebControls.RadioButtonList rblist, DataSet ds, int value, int text)
    {
        rblist.Items.Clear();//清空他的值
        if (ds == null)
        {
            return;
        }
        if (ds.Tables.Count == 0)
        {
            return;
        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ListItem litem = new ListItem();
            litem.Text = ds.Tables[0].Rows[i][text].ToString().Trim();
            litem.Value = ds.Tables[0].Rows[i][value].ToString().Trim();
            rblist.Items.Add(litem);

        }
        

    }


    //GridView
    public void GridView_Bind(System.Web.UI.WebControls.GridView gv, DataSet ds)
    {
        if (ds == null)
        {
            return;
        }
        if (ds.Tables.Count == 0)
        {
            return;
        }

        gv.DataSource = ds.Tables[0].DefaultView;
        gv.DataBind();
       

    }
    
    //DropDownList
   // 'dropdwonlist 綁定
    public void  Dropdownlist_Bind(System .Web .UI .WebControls .DropDownList ddr,DataTable dt,string value,string text,int type)
    {

        ddr.Items.Clear();
        if (type == 1)
        {
            ddr.Items.Add("");
        }
        
        if (dt.Rows.Count == 0)
        {
            return;
        }

      //  string msg  = "";
        for (int i=0;i<dt.Rows .Count ;i++)
        {
            ListItem item  = new ListItem();

            item.Value = "";
            if (!(dt.Rows[i][value] is DBNull ))
             {
                 item.Value = dt.Rows[i][value].ToString().Trim();
             }

            item.Text = "";
            if (!(dt.Rows[i][text] is DBNull))
              {
                  item.Text =item.Value+"--"+ dt.Rows[i][text].ToString().Trim();
              }
             
             ddr.Items.Add(item);

         }
        

    }

    //DropDownList 顯示兩個欄位值
    // 'dropdwonlist 綁定 
    public void Dropdownlist_TwoBind(System.Web.UI.WebControls.DropDownList ddr, DataSet ds, string value, string text, int type, string text2)
    {

        ddr.Items.Clear();
        if (type == 1)
        {
            ddr.Items.Add("");
        }
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return;
        }
        
        //  string msg  = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ListItem item = new ListItem();

            item.Value = "";
            if (!(ds.Tables[0].Rows[i][value] is DBNull))
            {
                item.Value = ds.Tables[0].Rows[i][value].ToString().Trim();
            }
           
            item.Text = "";

            if (!(ds.Tables[0].Rows[i][text2] is DBNull))
            {
                item.Text = ds.Tables[0].Rows[i][text2].ToString().Trim();
            }
            if (!(ds.Tables[0].Rows[i][text] is DBNull))
            {
                item.Text = item.Text + "--" + ds.Tables[0].Rows[i][text].ToString().Trim();
            }

           
            ddr.Items.Add(item);

        }
        

    }


    //設置GridView 的外觀樣式
    public void setGridViewStyle(int flag,System.Web.UI.WebControls.GridView gv,
        int fontsize, int pagesize, bool apage, bool asort)
    {
        //gviewWorker.AutoGenerateColumns = false;
        ////設置具有唯一性
        //string[] Keynames = new string[] { "w_no" };
        //gviewWorker.DataKeyNames = Keynames;
        //設置GridView 屬性

        gv.AllowPaging = apage;
        gv.AllowSorting = asort;
        gv.Font.Size = fontsize;
        gv.PageSize = pagesize;

        if (flag == 1)
        {
             //gv.GridLines = GridLines.Both;
             //gv.BackColor = Color.White;
             //gv.BorderColor = Color.LightGreen;
             //gv.BorderStyle= "None";
             //gv.BorderWidth = 1;
             //gv.CellPadding = 1;
             //gv.CellSpacing = 0;

            //分頁位置
            //gv.PagerSettings.Position = PagerPosition.TopAndBottom;
            //gv.PagerStyle.BackColor = Color.LightGray;

            //分頁對齊
            //gv.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
            
            //外觀顏色

            //header
            //gv.HeaderRow.Font.Size = 10;
            //gv.HeaderRow.Font.Bold = true;
            //gv.HeaderStyle.BackColor = Color.LightGray;
            //gv.HeaderStyle.ForeColor = Color.Blue;
          
            //row
            //gv.RowStyle.BackColor = Color.White; ;
            //gv.RowStyle.ForeColor = Color.Black;

            //alter row
            //gv.AlternatingRowStyle.BackColor = Color.Gainsboro;
            
           //select row
            //gv.SelectedRowStyle.BackColor = Color.LightBlue;
            //gv.SelectedRow.Font.Bold = true;
            //gv.SelectedRowStyle.ForeColor = Color.White;
        }

        if (flag == 2)
        {
            //gv.GridLines = GridLines.Both;
          
            //分頁位置
            //gv.PagerSettings.Position = PagerPosition.TopAndBottom;
            //gv.PagerStyle.BackColor = Color.Goldenrod;

            //分頁對齊
            //gv.PagerStyle.HorizontalAlign = HorizontalAlign.Center;


            //gv.HeaderStyle.BackColor = Color.Tan;
            //gv.RowStyle.BackColor = Color.LightGoldenrodYellow;
            //gv.AlternatingRowStyle.BackColor = Color.PaleGoldenrod;
            //gv.HeaderStyle.ForeColor = Color.Black;
            //gv.SelectedRowStyle.BackColor = Color.LightBlue;
        }

    }


}
