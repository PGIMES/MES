using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using DevExpress.Web;
using System.Drawing;

public partial class SZ_report_dev_select : System.Web.UI.Page
{
    MaterialBase_CLASS MaterialBase_CLASS = new MaterialBase_CLASS();
    protected int widestData;
    protected int nid = 0;
    public string sdomain = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        nid = Convert.ToInt32(Request.QueryString["id"].ToString());
        sdomain = Request.QueryString["domain"].ToString();
        widestData = 0;
      
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {
            Get_ddl();//给相关人员Dropdownlist 绑定数据源
            txtBASE.Value = "修磨刀";
            txtValue.Value = "01ZT";
            //QueryASPxGridView();
            if (sdomain=="昆山工厂")
            {
                sdomain = "200";
            }
            else if (sdomain == "上海工厂")
            {
                sdomain = "100";

            }
            this.DDL_domain.SelectedValue = sdomain;
            this.DDL_domain.Enabled = false;
        }
        if (DDL_type.SelectedValue != "")
        {
            Get_style();
            //if (this.GV_PART.IsCallback)
            //{
                QueryASPxGridView();
           // }
            //QueryASPxGridView();
            //Get_ddl();
        }
    }
    public void Get_style()
    {
        //DDL_type.Items[0].Attributes.Add("style", "color:blue");
        //DDL_type.Items[2].Attributes.Add("style", "color:blue");
        //DDL_type.Items[3].Attributes.Add("style", "color:blue");
        //DDL_type.Items[6].Attributes.Add("style", "color:blue");
        //DDL_type.Items[5].Attributes.Add("style", "color:blue");
        //DDL_type.Items[13].Attributes.Add("style", "color:blue");
        //DDL_type.Items[14].Attributes.Add("style", "color:blue");
        //DDL_type.Items[15].Attributes.Add("style", "color:blue");
    }
    public void Get_ddl()
    {
        
        DDL_type.DataSource = MaterialBase_CLASS.MES_PT_BASE(1,"刀具类", "非生产","");
        DDL_type.DataValueField = "name2";
        DDL_type.DataTextField = "name";
        DDL_type.DataBind();
        //this.DDL_type.Items.Insert(0, new ListItem("01-钻头", "钻头"));

        Get_style();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //gv_pt.PageIndex = 0;
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
 
       
        DataTable dt = MaterialBase_CLASS.DJ_base(DDL_type.SelectedValue, txtBASE.Value, txtljh.Value, DDL_domain.SelectedValue);
        Pgi.Auto.Control.SetGrid(txtValue.Value,"", this.GV_PART, dt,1);
      
        //if (this.GV_PART.Selection.Count== 0)
        //{
        //    this.GV_PART.Selection.SelectRow(0);
        //}




    }

    protected void GV_PART_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.DataColumn.Caption == "序号")
        //{
        //    if (Convert.ToInt16(ViewState["i"]) == 0)
        //    {
        //        ViewState["i"] = 1;
        //    }
        //    int i = Convert.ToInt16(ViewState["i"]);
        //    e.Cell.Text = i.ToString();
        //    i++;
        //    ViewState["i"] = i;
        //}

             
    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType != GridViewRowType.Data) return;
        //string s = e.GetValue("xyph").ToString();
        //if (s == "死亡")
        //{
        //    e.Row.BackColor = Color.Silver;
        //}
        //if (txtBASE.Value == "非修磨刀")
        //{

        //}
        //else
        //{
        //    string c = (string)e.GetValue("colour");
        //    string type = (string)e.GetValue("type");
        //    DataTable dt = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "xmcs_sj");
        //    if (dt.Rows.Count > 0)
        //    {
        //        int list_order = Convert.ToInt32(dt.Rows[0]["list_order"]);

        //        if (c == "红色")
        //            //e.Row.BackColor = Color.Red;
        //            //e.Row.Cells[18].BackColor = Color.Red;
        //            e.Row.Cells[list_order].Style.Add("background-color", "red");
        //        if (c == "黄色")
        //            //e.Row.BackColor = Color.Yellow;
        //            //e.Row.Cells[20].BackColor = Color.Yellow;
        //            e.Row.Cells[list_order].Style.Add("background-color", "yellow");
        //    }

        //    //if (type=="钻头")
        //    //{
        //    //    if (c == "红色")
        //    //        //e.Row.BackColor = Color.Red;
        //    //        //e.Row.Cells[18].BackColor = Color.Red;
        //    //        e.Row.Cells[24].Style.Add("background-color", "red");
        //    //    if (c == "黄色")
        //    //        //e.Row.BackColor = Color.Yellow;
        //    //        //e.Row.Cells[20].BackColor = Color.Yellow;
        //    //        e.Row.Cells[24].Style.Add("background-color", "yellow");
        //    //}
        //    //else if(type == "倒角刀")
        //    //{
        //    //    if (c == "红色")
        //    //        e.Row.Cells[19].Style.Add("background-color", "red");
        //    //    if (c == "黄色")
        //    //        e.Row.Cells[19].Style.Add("background-color", "yellow");

        //    //}
        //    //else if (type == "拉刀")
        //    //{
        //    //    if (c == "红色")
        //    //        e.Row.Cells[21].Style.Add("background-color", "red");
        //    //    if (c == "黄色")
        //    //        e.Row.Cells[21].Style.Add("background-color", "yellow");

        //    //}
        //    //else if (type == "特殊刀具")
        //    //{
        //    //    if (c == "红色")
        //    //        e.Row.Cells[22].Style.Add("background-color", "red");
        //    //    if (c == "黄色")
        //    //        e.Row.Cells[22].Style.Add("background-color", "yellow");

        //    //}
        //}

        ////string wlh = (string)e.GetValue("wlh");
        ////string ljh = txtljh.Value;
        ////string site = DDL_domain.SelectedValue;
        ////((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).PropertiesHyperLinkEdit.NavigateUrlFormatString = "Forproducts.aspx?wlh=" + e.GetValue("wlh") + "&ljh=" + ljh + "&site=" + site + "";

        ////    string wlh = (string)e.GetValue("wlh");
        ////    string ljh = txtljh.Value;
        ////    string site = DDL_domain.SelectedValue;
        ////    LinkButton lb_update = (LinkButton)e.Row.Cells[16].FindControl("");  
        ////    string _jsEdit = "showModalDialog('Forproducts.aspx?wlh=" + wlh + "&ljh="+ljh+"&stie"+site+"',null,'dialogWidth:500px;dialogHeight:300px;center:yes;status:no;scroll:no;help:no;resizable:no')"; //javascipt 语句，我的这个是打开子页面的。


        ////lb_update.Attributes.Add("onclick", _jsEdit);


    }
    protected void GV_PART_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string wlh = (string)e.GetValue("wlh");
            if (this.GV_PART.DataColumns["wlh"] != null)
            {
                int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["wlh"]).VisibleIndex;
                e.Row.Cells[index].Text = wlh;
            }
                //string ljh = txtljh.Value;
                //string site = DDL_domain.SelectedValue;
            //    if (this.GV_PART.DataColumns["yycps"] != null)
            //{
            //    int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).VisibleIndex;
            //    int value = Convert.ToInt16(e.GetValue("yycps"));
            //    e.Row.Cells[index].Text = "<a href='Forproducts.aspx?wlh=" + e.GetValue("wlh") + "&ljh=" + ljh + "&site=" + site + "' target='_blank'>" + value.ToString() + "</a>";

            //    //  ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["yycps"]).PropertiesHyperLinkEdit.NavigateUrlFormatString = "Forproducts.aspx?wlh=" + e.GetValue("wlh") + "&ljh=" + ljh + "&site=" + site + "";
            //}
            //if (this.GV_PART.DataColumns["jg"] != null)
            //{                

            //}     
            //DevExpress.Web.ASPxSummaryItem jg_xd = new ASPxSummaryItem();
            //DevExpress.Web.aspx jg_xd = new GridViewDataColumn();
            //jg_xd.FieldName = "jg_xd";
            //jg_xd.DisplayFormat = "{0:N0}";
        }
        //if (e.RowType == DevExpress.Web.GridViewRowType.Footer)
        //{

        //    //string type = (string)e.GetValue("type");
        //    if (txtBASE.Value == "非修磨刀")
        //    {

        //    }
        //    else
        //    {
        //        string type = DDL_type.SelectedValue;
        //        DataTable dt = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "lycs_xd");
        //        DataTable dt_xmd = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "lycs_xmd");
        //        DataTable dt_xmcs_sj = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "xmcs_sj");
        //        DataTable dt_zpjyl_xd = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "zpjyl_xd");
        //        DataTable dt_zpjyl_xmd = MaterialBase_CLASS.PGI_BASE_PART_xmcs_sj_colour(type, "zpjyl_xmd");

        //        if (dt.Rows.Count > 0)
        //        {
        //            int list_order_lycs_xd = Convert.ToInt32(dt.Rows[0]["list_order"]);
        //            int list_order_lycs_xmd = Convert.ToInt32(dt_xmd.Rows[0]["list_order"]);
        //            int list_order_xmcs_sj = Convert.ToInt32(dt_xmcs_sj.Rows[0]["list_order"]);
        //            int list_order_zpjyl_xd = Convert.ToInt32(dt_zpjyl_xd.Rows[0]["list_order"]);
        //            int list_order_zpjyl_xmd = Convert.ToInt32(dt_zpjyl_xmd.Rows[0]["list_order"]);

        //            if (e.Row.Cells.Count > list_order_lycs_xd && e.Row.Cells.Count > list_order_lycs_xmd)
        //            {
        //                if (e.Row.Cells[list_order_lycs_xd].Text.ToString().Replace("&nbsp;", "") != "" && e.Row.Cells[list_order_lycs_xmd].Text.ToString().Replace("&nbsp;", "") != "")
        //                {
        //                    //实际修磨次数合计
        //                    Decimal lycs_xd = Math.Round(Convert.ToDecimal(e.Row.Cells[list_order_lycs_xd].Text.Replace(",", "").ToString()), 1);
        //                    Decimal lycs_xmd = Math.Round(Convert.ToDecimal(e.Row.Cells[list_order_lycs_xmd].Text.Replace(",", "").ToString()), 1);

        //                    e.Row.Cells[list_order_xmcs_sj].Text = Convert.ToString(Math.Round(lycs_xmd / lycs_xd, 1));
        //                    /// Convert.ToInt32(e.Row.Cells[34].Text.Replace(",", ""))));
        //                    //新刀周平均量合计
        //                    e.Row.Cells[list_order_zpjyl_xd].Text = Convert.ToString(Math.Round(lycs_xd / 52, 1));
        //                    //修磨刀周平均量合计
        //                    e.Row.Cells[list_order_zpjyl_xmd].Text = Convert.ToString(Math.Round(lycs_xmd / 52, 1));
        //                    dt.Clear();
        //                    dt_xmd.Clear();
        //                    dt_xmcs_sj.Clear();
        //                    dt_zpjyl_xd.Clear();
        //                    dt_zpjyl_xmd.Clear();
        //                }

        //            }

        //        }


        //    }

        //}
    }


    protected void DDL_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = MaterialBase_CLASS.MES_PT_BASE(2, "刀具类", "非生产", DDL_type.SelectedValue);
        txtBASE.Value = dt.Rows[0]["BASE"].ToString();
        txtValue.Value = dt.Rows[0]["Value"].ToString();
        this.GV_PART.Columns.Clear();

        QueryASPxGridView();


    }





  

    protected void GV_PART_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //string lswlh = "11";
        //string lsms = "22";
        //string lsjs = "";
        //string lsdjedsm = "";
        //string lspp = "";
        //string lsgys = "";

        //Response.Write("<script>alert('xxxx');</script>");

        // Response.Write("<script>parent.opener.form1.txt_xmh.value = '" + xmh + "';parent.opener.form1.txt_ljh.value = '" + ljh + "';parent.opener.form1.txt_gxh.value = '" + gxh + "'; window.close();</script>"); 
        // string temp = "<script>window.opener.setvalue_dj('" + nid.ToString() + "','" + lswlh + "','" + lsms + "','" + lsjs + "','" + lsdjedsm + "','" + lspp + "','" + lsgys + "');</script>";
        //  Response.Write(temp.Trim());
        
       
    }

    protected void GV_PART_SelectionChanged(object sender, EventArgs e)
    {
        //string lswlh = "11";
        //string lsms = "22";
        //string lsjs = "";
        //string lsdjedsm = "";
        //string lspp = "";
        //string lsgys = "";
        //string temp = "<script>window.opener.setvalue_dj('" + nid.ToString() + "','" + lswlh + "','" + lsms + "','" + lsjs + "','" + lsdjedsm + "','" + lspp + "','" + lsgys + "');</script>";
        //Response.Write(temp.Trim());
        
      
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        string lswlh = "11";
        string lsms = "22";
        string lsjs = "";
        string lsdjedsm = "";
        string lspp = "";
        string lsgys = "";
        string temp = "<script>window.opener.setvalue_dj('" + nid.ToString() + "','" + lswlh + "','" + lsms + "','" + lsjs + "','" + lsdjedsm + "','" + lspp + "','" + lsgys + "');</script>";
        Response.Write(temp.Trim());
        
    }
}