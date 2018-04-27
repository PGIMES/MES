using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class TouLiaoTongJi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            BaseFun fun = new BaseFun();
             
            //初始化年份    
            int year=Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for(int i=0;i<5;i++)
            {
                dropYear.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }            

            //初始化月份
            dropMonth.Items.Add(new ListItem{ Value="", Text="" });
            for (int i = 1; i < 13; i++)
            {
                dropMonth.Items.Add(new ListItem( i.ToString()+"月" , i.ToString()  ));
            }   
           
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Function_TouLiao_RL RL = new Function_TouLiao_RL();
       
        DataTable tbl = RL.TouLiao_RL_TongJi("年",selHeJin.SelectedValue,"",this.dropYear.SelectedValue,dropMonth.SelectedValue );
        
        GridViewYear.DataSource = tbl;
        GridViewYear.DataBind();
      


    }
    protected void  Query_Click(object sender, EventArgs e)
    {
        Function_TouLiao_RL RL = new Function_TouLiao_RL();

        DataTable tbl = RL.TouLiao_RL_TongJi("年", selHeJin.SelectedValue, "", this.dropYear.SelectedValue, dropMonth.SelectedValue);

        GridViewYear.DataSource = tbl;
        GridViewYear.DataBind();
       // setHeader();


    }
    

  
    protected void GridViewYear_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[0].Width=200;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {               
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "btn" + i.ToString();
                lbtn.Text = e.Row.Cells[i].Text;
              //  lbtn.Click = Query_Click;               
                lbtn.Attributes.Add("href", @"javascript:__doPostBack('ctl00$MainContent$LinkBtn','')");
               // lbtn.OnClientClick = "return getMonth();";
                lbtn.Attributes.Add("name", "mon");
                e.Row.Cells[i].Controls.Add(lbtn);
            }
        }
        
    }
     
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        Function_TouLiao_RL RL = new Function_TouLiao_RL();
        DataTable tbl = RL.TouLiao_RL_TongJi("月", selHeJin.SelectedValue, "", this.dropYear.SelectedValue, txtmonth.Text.Substring(4));
        GridViewMonth.DataSource = tbl;
        GridViewMonth.DataBind();
      
    }
    
}