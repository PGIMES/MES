using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class MoJu_Moju_BX_Detail : System.Web.UI.Page
{
    Moju Moju = new Moju();
    string dh = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["dh"] != null)
        {
            dh = Request["dh"].ToString();
        }
        if (!IsPostBack)
        {
            lb_dh.Text ="  --"+ dh;
            DataTable dt = Moju.Moju_BX_Query(1, dh, "", "", "", "", "", "", "", "", "", "", "");
            string sql = "select base_value from MES_Base_code where base_code='MOJU_BX_TYPE'";
            DataTable dt_type = SQLHelper.reDs(sql).Tables[0];
            this.ddl_gz_type.DataSource = dt_type;
            this.ddl_gz_type.DataTextField = "base_value";
            this.ddl_gz_type.DataValueField = "base_value";
            this.ddl_gz_type.DataBind();
            this.ddl_gz_type.Items.Insert(0, new ListItem("", ""));
            if (dt.Rows[0]["status"].ToString() == "报修完成")
            {
                Div_Qr.Style.Add("display", "none");
                Div_WX.Style.Add("display", "none");
            }
            else  if (dt.Rows[0]["status"].ToString() == "维修完成")
            {
                Div_Qr.Style.Add("display", "none");
                Div_WX.Style.Add("display", "block");
                txt_wx_cs.Value = dt.Rows[0]["wx_cs"].ToString();
                txt_wx_result.Value = dt.Rows[0]["wx_result"].ToString();
                txt_xm_cs.Value = dt.Rows[0]["mo_down_cs"].ToString();
                txt_wx_begindate.Value =dt.Rows[0]["wx_begin_date"].ToString();
                txt_wx_enddate.Value = dt.Rows[0]["wx_end_date"].ToString();
                txt_wxr.Value = dt.Rows[0]["wx_name"].ToString();
               
            }
           
            else if (dt.Rows[0]["status"].ToString() == "确认完成")
            {
                Div_Qr.Style.Add("display", "block");
                Div_WX.Style.Add("display", "block");
                txt_wx_cs.Value = dt.Rows[0]["wx_cs"].ToString();
                txt_wx_result.Value = dt.Rows[0]["wx_result"].ToString();
                txt_xm_cs.Value = dt.Rows[0]["mo_down_cs"].ToString();
                txt_wx_begindate.Value = dt.Rows[0]["wx_begin_date"].ToString();
                txt_wx_enddate.Value = dt.Rows[0]["wx_end_date"].ToString();
                txt_wxr.Value = dt.Rows[0]["wx_name"].ToString();

                txt_qr_gh.Value = dt.Rows[0]["qr_gh"].ToString();
                txt_qr_name.Value = dt.Rows[0]["qr_name"].ToString();
                txt_qr_date.Value = Convert.ToDateTime(dt.Rows[0]["qr_date"].ToString()).ToShortDateString();
                txt_qr_time.Value = Convert.ToDateTime(dt.Rows[0]["qr_date"].ToString()).ToLongTimeString();
                txt_qr_remark.Value = dt.Rows[0]["qr_remark"].ToString();
              
            }

            txt_moju_no.Value = dt.Rows[0]["bx_moju_no"].ToString();
            txt_sbname.Value = dt.Rows[0]["bx_sbname"].ToString();
            txt_moju_type.Value = dt.Rows[0]["bx_moju_type"].ToString();
            txt_part.Value = dt.Rows[0]["bx_part"].ToString();
            txt_bx_date.Value = Convert.ToDateTime(dt.Rows[0]["bx_date"].ToString()).ToShortDateString();
            txt_bx_time.Value = Convert.ToDateTime(dt.Rows[0]["bx_date"].ToString()).ToLongTimeString(); 
            txt_bxr.Value = dt.Rows[0]["bx_name"].ToString();
            txt_mo_no.Value = dt.Rows[0]["bx_mo_no"].ToString();
            string type = dt.Rows[0]["bx_gz_type"].ToString();
            ddl_gz_type.Text = dt.Rows[0]["bx_gz_type"].ToString();
            txt_gz_desc.Value = dt.Rows[0]["bx_gz_desc"].ToString();
            txt_banbie.Value = dt.Rows[0]["bx_banbie"].ToString();
            ddl_gz_type.CssClass = "form-control input-s-sm  ";
            ddl_gz_type.Enabled = false;

        }
    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //   // Response.Redirect("MojuBX_Query.aspx");
    //}
}