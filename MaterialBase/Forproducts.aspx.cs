using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;

public partial class Forproducts : System.Web.UI.Page
{
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected int widestData;
    protected void Page_Load(object sender, EventArgs e)
    {

        widestData = 0;
        //this.gv_pt.PageSize = 200;
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            //Get_ddl();//给相关人员Dropdownlist 绑定数据源
          
        }
     
            QueryASPxGridView();
        
    }


    public void QueryASPxGridView()
    {
        if (Request["wlh"] != null)
        {
            string pt_part = Request["wlh"];
            string ljh = Request["ljh"];
            string site = Request["site"];
            DataTable dt = MaterialBase_CLASS.Forproducts(pt_part,ljh,site);
            this.gv_pt.DataSource = dt;
            gv_pt.DataBind();

        }

        
    }



    protected void gv_pt_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        //string product_no = "";


        //if (e.Column.VisibleIndex == 3)
        //{
        //    string title = e.GetFieldValue("title").ToString();
        //    double taobao_price = Convert.ToDouble(e.GetFieldValue("price").ToString());
        //    product_no = product_no_str(title);
        //    string str_select_net_price = "select top 1  price2 from tbl_product_store where product_no='" + product_no + "'";
        //    SqlDataReader dr = co.storereadershop(str_select_net_price);
        //    if (dr.Read())
        //    {
        //        e.DisplayText = dr[0].ToString();
        //        double taoqu_price = Convert.ToDouble(dr[0].ToString());
        //        if (taobao_price != taoqu_price)
        //        {
        //            e.DisplayText = "<p style=\"color: #FF0000\">" + dr[0] + "</p>";
        //            tb_more_product_no.Text += product_no + Environment.NewLine;
        //        }
        //    }

        //}
    }
}