using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YaSheTou_YST_Record : System.Web.UI.Page
{
    LoginUser LogUserModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);

        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();

            this.txtRiQi.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.txtShiJian.Value = DateTime.Now.ToString("HH:mm:ss");
            //初始话下拉登入此台设备人员
            string strSQL = "select * from MES_EmpLogin where emp_shebei='" + Request["deviceid"] + "' and status=1 and emp_no in (select empid from Emp_Tiaoshi) ";
            DataTable tbl = DbHelperSQL.Query(strSQL).Tables[0];
            if (tbl.Rows.Count > 0)
            {
                fun.initDropDownList(dropGongHao, tbl, "emp_no", "emp_no");
                txtXingMing.Value = tbl.Rows[0]["emp_name"].ToString().Trim();
                txtBanBie.Value = tbl.Rows[0]["emp_banbie"].ToString().Trim();
                txtBanZu.Value = tbl.Rows[0]["emp_banzhu"].ToString().Trim();
            }
            //初始设备信息
            string strSQL2 = "select * from MES_Equipment where equip_no='" + Request["deviceid"] + "' and equip_type='压铸机'";
            DataTable tbl2 = DbHelperSQL.Query(strSQL2).Tables[0];
            if (tbl2.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl2.Rows)
                {
                    txtSheBeiHao.Value = dr["equip_no"].ToString().Trim();//.Field["equip_no"];
                    txtSheBeiJianCheng.Value = dr["equip_name"].ToString().Trim();


                }
            }
            ini_default();

        }
    }
    public void ini_default()
    {
        divXiaMo.Visible = false;
        divShangMo.Visible = false;

        btn_Save.Enabled = false;
        btn_Save.CssClass = "btn btn-large btn-primary  disabled";

    }
    protected void dropGongHao_SelectedIndexChanged(object sender, EventArgs e)
    {
        Function_Jinglian jl = new Function_Jinglian();

        txtXingMing.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue, "").Rows[0]["emp_name"].ToString().Trim();
        txtBanBie.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue, "").Rows[0]["emp_banbie"].ToString().Trim();
        txtBanZu.Value = jl.Emplogin_query(9, dropGongHao.SelectedValue, "").Rows[0]["emp_banzhu"].ToString().Trim();

        DataTable dtpihao = new DataTable();


    }

    public void Bind_code(string equip_no, string code)
    {
        if (code == "")//绑定赋值
        {

            DataTable dt = DbHelperSQL.Query(@"select a.*,case when a.gys='A' then '铸泰' when a.gys='B' then '宜龙' else '' end gys_name,b.lj_mc start_mc
                                            from [dbo].[MES_YaSheTou_Status] a
                                                 left join MES_YaSheTou_Base b on a.code=b.code
                                            where a.equip_no='" + equip_no + "' and a.enddate is null").Tables[0];
            ddl_code.DataSource = dt;
            ddl_code.DataValueField = "code";
            ddl_code.DataTextField = "code";
            ddl_code.DataBind();

            DataRow[] dr = dt.Select("code='" + ddl_code.SelectedValue + "'");
            if (dr.Length == 1)
            {
                txt_mc.Text = dr[0]["mc"].ToString();
                txt_gys_Name.Text = dr[0]["gys_name"].ToString();
                txt_gys.Text = dr[0]["gys"].ToString();
                txt_zj.Text = dr[0]["zj"].ToString();
                txt_kaishi.Text = dr[0]["start_mc"].ToString();
            }
        }
        else//change事件
        {
            DataTable dt = DbHelperSQL.Query(@"select a.*,case when a.gys='A' then '铸泰' when a.gys='B' then '宜龙' else '' end gys_name,b.lj_mc start_mc
                                            from [dbo].[MES_YaSheTou_Status] a
                                                 left join MES_YaSheTou_Base b on a.code=b.code
                                            where a.equip_no='" + equip_no + "' and a.code='" + code + "' and a.enddate is null").Tables[0];
            if (dt.Rows.Count == 1)
            {
                txt_mc.Text = dt.Rows[0]["mc"].ToString();
                txt_gys_Name.Text = dt.Rows[0]["gys_name"].ToString();
                txt_gys.Text = dt.Rows[0]["gys"].ToString();
                txt_zj.Text = dt.Rows[0]["zj"].ToString();
                txt_kaishi.Text = dt.Rows[0]["start_mc"].ToString();
            }
        }

    }

    public void Bind_code_S(string code)
    {
        if (code == "")//绑定
        {
            string sql = @"select *,case when gys='A' then '铸泰' when gys='B' then '宜龙' else '' end gys_name
                        from [dbo].[MES_YaSheTou_Base] a 
                        where code not in(select code from [dbo].[MES_YaSheTou_Status] where enddate is null)";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            ddl_code_S.DataSource = dt;
            ddl_code_S.DataValueField = "code";
            ddl_code_S.DataTextField = "code";
            ddl_code_S.DataBind();

            DataRow[] dr = dt.Select("code='" + ddl_code_S.SelectedValue + "'");
            if (dr.Length == 1)
            {
                txt_mc_S.Text = dr[0]["mc"].ToString();
                txt_gys_Name_S.Text = dr[0]["gys_name"].ToString();
                txt_gys_S.Text = dr[0]["gys"].ToString();
                txt_zj_S.Text = dr[0]["zj"].ToString();
                txt_start_mc.Text = dr[0]["lj_mc"].ToString();
            }
        }
        else//change
        {
            string sql = @"select *,case when gys='A' then '铸泰' when gys='B' then '宜龙' else '' end gys_name
                        from [dbo].[MES_YaSheTou_Base] a 
                        where code='" + code + "'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count == 1)
            {
                txt_mc_S.Text = dt.Rows[0]["mc"].ToString();
                txt_gys_Name_S.Text = dt.Rows[0]["gys_name"].ToString();
                txt_gys_S.Text = dt.Rows[0]["gys"].ToString();
                txt_zj_S.Text = dt.Rows[0]["zj"].ToString();
                txt_start_mc.Text = dt.Rows[0]["lj_mc"].ToString();
            }
        }

    }

    public void clear()
    {
        txt_mc.Text = "";
        txt_gys_Name.Text = "";
        txt_gys.Text = "";
        txt_zj.Text = "";
        txt_remark.Text = "";
        txt_deal_mc.Text = "";
        txt_end_mc.Text = "";
        txt_kaishi.Text = "";
    }

    public void clear_S()
    {
        txt_mc_S.Text = "";
        txt_gys_Name_S.Text = "";
        txt_gys_S.Text = "";
        txt_zj_S.Text = "";
        txt_remark_S.Text = "";
        txt_start_mc.Text = "";
    }

    protected void ddl_change_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_code.Items.Clear();
        clear();

        ddl_code_S.Items.Clear();
        clear_S();

        if (ddl_change_type.SelectedValue == "仅上")
        {
            divXiaMo.Visible = false;
            divShangMo.Visible = true;

            btn_Save.Enabled = true;
            btn_Save.CssClass = "btn btn-large btn-primary ";

            Bind_code_S("");
        }
        else if (ddl_change_type.SelectedValue == "仅下")
        {
            divShangMo.Visible = false;
            divXiaMo.Visible = true; lbl_xs.Text = "下";

            btn_Save.Enabled = true;
            btn_Save.CssClass = "btn btn-large btn-primary ";

            Bind_code(Request["deviceid"], "");

        }
        else if (ddl_change_type.SelectedValue == "先下再上")
        {
            divXiaMo.Visible = true;
            divShangMo.Visible = true;

            btn_Save.Enabled = true;
            btn_Save.CssClass = "btn btn-large btn-primary ";

            Bind_code(Request["deviceid"], "");
            Bind_code_S("");
        }
        else if (ddl_change_type.SelectedValue == "不更换")
        {
            divShangMo.Visible = false;
            divXiaMo.Visible = true; lbl_xs.Text = "不更换";

            btn_Save.Enabled = true;
            btn_Save.CssClass = "btn btn-large btn-primary ";

            Bind_code(Request["deviceid"], "");

        }
        else
        {
            divXiaMo.Visible = false;
            divShangMo.Visible = false;

            btn_Save.Enabled = false;
            btn_Save.CssClass = "btn btn-large btn-primary  disabled";
        }
    }

    protected void ddl_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        Bind_code(Request["deviceid"], ddl_code.SelectedValue);
    }

    protected void ddl_code_S_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear_S();
        Bind_code_S(ddl_code_S.SelectedValue);
    }
    protected void txt_deal_mc_TextChanged(object sender, EventArgs e)
    {
        Regex numRegex = new Regex(@"^[1-9]+[0-9]*$");
        if (numRegex.IsMatch(txt_deal_mc.Text) == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【本次使用模次】请输入正整数')", true);
            txt_end_mc.Text = "";
            return;
        }
        txt_end_mc.Text = (Convert.ToInt32(txt_kaishi.Text) + Convert.ToInt32(txt_deal_mc.Text)).ToString();

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (dropGongHao.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【工号】不可为空！')", true);
            return;
        }

        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        Regex numRegex = new Regex(@"^[1-9]+[0-9]*$");

        string changetype = ddl_change_type.SelectedValue;
        if (changetype == "仅上")
        {
            if (ddl_code_S.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【压射头编码】不可为空！')", true);
                return;
            }
            ls_sum = sql_S(changetype);
        }
        else if (changetype == "仅下")
        {
            if (ddl_code.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【压射头编码】不可为空！')", true);
                return;
            }
            if (numRegex.IsMatch(txt_deal_mc.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【本次使用模次】请输入正整数')", true);
                txt_end_mc.Text = "";
                return;
            }

            ls_sum = sql(changetype);
        }
        else if (ddl_change_type.SelectedValue == "先下再上")
        {
            if (ddl_code.SelectedValue == "" || ddl_code_S.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【压射头编码】不可为空！')", true);
                return;
            }
            if (numRegex.IsMatch(txt_deal_mc.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【本次使用模次】请输入正整数')", true);
                txt_end_mc.Text = "";
                return;
            }
            ls_sum = sql(changetype);

            List<Pgi.Auto.Common> ls_sum_2 = sql_S(changetype);
            foreach (Pgi.Auto.Common item in ls_sum_2)
            {
                ls_sum.Add(item);
            }
        }
        else if (ddl_change_type.SelectedValue == "不更换")
        {
            if (ddl_code.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【压射头编码】不可为空！')", true);
                return;
            }
            if (numRegex.IsMatch(txt_deal_mc.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('【本次使用模次】请输入正整数')", true);
                txt_end_mc.Text = "";
                return;
            }

            ls_sum = sql(changetype);
        }

        int ln = 0;
        try
        {
            ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
        }
        catch (Exception ex)
        {
            ln = 0;
        }

        if (ln > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('确认成功！')", true);

            btn_Save.Enabled = false;
            btn_Save.CssClass = "btn btn-large btn-primary  disabled";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "layer.alert('确认失败！')", true);
        }
    }

    public List<Pgi.Auto.Common> sql_S(string changetype)
    {
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();

        Pgi.Auto.Common ls_status_insert = new Pgi.Auto.Common();
        string sql_status_insert = @"insert into MES_YaSheTou_Status(equip_no, equip_name, code, mc, gys, zj, startdate)
                           select '{0}','{1}','{2}','{3}','{4}','{5}',getdate()";
        sql_status_insert = string.Format(sql_status_insert, txtSheBeiHao.Value, txtSheBeiJianCheng.Value, ddl_code_S.SelectedValue, txt_mc_S.Text, txt_gys_S.Text, txt_zj_S.Text);
        ls_status_insert.Sql = sql_status_insert;
        ls_sum.Add(ls_status_insert);

        Pgi.Auto.Common ls_Record_insert = new Pgi.Auto.Common();
        string sql_Record_insert = @"insert into MES_YaSheTou_Record(emp_no, emp_name, emp_banbie, emp_banzhu, equip_no, equip_name
                                                , change_type, code, mc, gys, zj, yzt_status, start_mc
                                                , remark, CreateId, CreateName, CreateTime)
                           select '{0}','{1}','{2}','{3}','{4}','{5}'
                                 ,'{6}','{7}','{8}','{9}','{10}','{11}','{12}'
                                 ,'{13}','{14}','{15}',getdate()";
        sql_Record_insert = string.Format(sql_Record_insert, dropGongHao.SelectedValue.Trim(), txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value
                                        , changetype, ddl_code_S.SelectedValue, txt_mc_S.Text, txt_gys_S.Text, txt_zj_S.Text, ddl_status_S.SelectedValue, txt_start_mc.Text
                                        , txt_remark_S.Text, LogUserModel.UserId, LogUserModel.UserName);
        ls_Record_insert.Sql = sql_Record_insert;
        ls_sum.Add(ls_Record_insert);

        return ls_sum;
    }

    public List<Pgi.Auto.Common> sql(string changetype)
    {
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
       
        if (changetype == "仅下")
        {
            Pgi.Auto.Common ls_status_update = new Pgi.Auto.Common();
            string sql_status_update = @"update MES_YaSheTou_Status set enddate=getdate() where equip_no='{0}' and code='{1}' and enddate is null";
            sql_status_update = string.Format(sql_status_update, txtSheBeiHao.Value, ddl_code.SelectedValue);
            ls_status_update.Sql = sql_status_update;
            ls_sum.Add(ls_status_update);
        }

        Pgi.Auto.Common ls_Record_insert = new Pgi.Auto.Common();
        string sql_Record_insert = @"insert into MES_YaSheTou_Record(emp_no, emp_name, emp_banbie, emp_banzhu, equip_no, equip_name
                                                , change_type, code, mc, gys, zj, yzt_status, deal_mc
                                                , end_mc, remark, CreateId, CreateName, CreateTime)
                           select '{0}','{1}','{2}','{3}','{4}','{5}'
                                 ,'{6}','{7}','{8}','{9}','{10}','{11}','{12}'
                                 ,'{13}','{14}','{15}','{16}',getdate()";
        sql_Record_insert = string.Format(sql_Record_insert, dropGongHao.SelectedValue.Trim(), txtXingMing.Value, txtBanBie.Value, txtBanZu.Value, txtSheBeiHao.Value, txtSheBeiJianCheng.Value
                                        , changetype, ddl_code.SelectedValue, txt_mc.Text, txt_gys.Text, txt_zj.Text, ddl_status.SelectedValue, txt_deal_mc.Text
                                        , txt_end_mc.Text, txt_remark.Text, LogUserModel.UserId, LogUserModel.UserName);
        ls_Record_insert.Sql = sql_Record_insert;
        ls_sum.Add(ls_Record_insert);

        Pgi.Auto.Common ls_base_update = new Pgi.Auto.Common();
        string sql_base_update = @"update MES_YaSheTou_Base set lj_mc=lj_mc+" + Convert.ToInt32(txt_deal_mc.Text) + " where code='"+ ddl_code.SelectedValue + "'";
        ls_base_update.Sql = sql_base_update;
        ls_sum.Add(ls_base_update);

        return ls_sum;
    }


}