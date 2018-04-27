using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Web.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MES.Model;
using MES.BLL;
using MES.DAL;
using Maticsoft.DBUtility;

public partial class DC_DC_Apply : System.Web.UI.Page
{
    YJ_CLASS YJ_CLASS = new YJ_CLASS();
    Function_DC DC = new Function_DC();
    control control = new control();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["lv"] = "";
            BaseFun fun = new BaseFun();
            Panel1.Visible = false;
            btn_wuliu.Visible = false;
            btn_ck.Visible = false;
            string sql = "select value,CLASS_NAME from [dbo].[form1_Sale_YJ_BASE] where BASE_ID='11'";
            DataSet wuliu = DbHelperSQL.Query(sql);
            fun.initDropDownList(DropDC_Uid, wuliu.Tables[0], "value", "CLASS_NAME");
            this.DropDC_Uid.Items.Insert(0, new ListItem("", ""));

            if (Session["empid"] == null )
            {   
                InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
            }


          // Session["empid"] = "01901";
            if (Request["requestid"] != null)
            {
                if (Session["empid"] != null)
                {
                    txt_update_user.Value = Session["empid"].ToString();
                    Query();
                    GetStatus(Request["requestid"]);
                    Fileload();

                }
               
            }
            else
            {


                if (Session["empid"] != null)
                {

                    txt_update_user.Value = Session["empid"].ToString();
                    this.txt_CreateDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                    txt_Code.Value = "DC" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    txt_huoyun_no.Value = "DC" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    txt_Userid.Value = Session["empid"].ToString();
                    DataTable dtemp = YJ_CLASS.YJ_emp(txt_Userid.Value);
                    txt_UserName.Value = dtemp.Rows[0]["lastname"].ToString();
                    txt_UserName_AD.Value = dtemp.Rows[0]["ADAccount"].ToString();
                    txt_dept.Value = dtemp.Rows[0]["dept_name"].ToString();
                    txt_managerid.Value = dtemp.Rows[0]["Manager_workcode"].ToString();
                    txt_manager.Value = dtemp.Rows[0]["Manager_name"].ToString();
                    txt_manager_AD.Value = dtemp.Rows[0]["Manager_AD_ACCOUNT"].ToString();
                    dtemp.Clear();
                   
                }

            }


        }
    }
    public void Fileload()
    {

        ShowFileload(txt_Code.Value, "txt_ddfj", gvFile_ddfj);
        ShowFileload(txt_Code.Value, "txt_xsfp_fj", gvFile_xsfp_fj);
        ShowFileload(txt_Code.Value, "txt_ch_fj", gv_chuo);
        ShowFileload(txt_Code.Value, "txt_zkck_fj", gvFile_zkck_fj);
   
    }
    protected void GetStatus(string requestid)
    {
        DataTable dt = DC.GetStatus(requestid);
        string status = dt.Rows[0]["status_id"].ToString();
        txt_status_id.Text = status;
        switch (status)
        {
            case "0":
               txt_status_id.Text = "已申请";
                lb_ddqr.Text = dt.Rows[0]["tjdate_sale"].ToString();
                lb_bh.Text = dt.Rows[0]["tjdate_wuliu"].ToString();
                break;
            case "1":
                txt_status_id.Text = "已订舱";
                lb_ddqr.Text = dt.Rows[0]["tjdate_sale"].ToString();
                lb_bh.Text = dt.Rows[0]["tjdate_wuliu"].ToString();
                lb_jy.Text = dt.Rows[0]["tjdate_ware"].ToString();
                break;
            //case "2": txt_status_id.Text = "已发货";
            //    lb_ddqr.Text = dt.Rows[0]["tjdate_sale"].ToString();
            //    lb_bh.Text = dt.Rows[0]["tjdate_wuliu"].ToString();
            //    lb_jy.Text = dt.Rows[0]["tjdate_ware"].ToString();
            //    break;
            default:
                break;

        }
    }
    public void Query()
    {
        string requestid = Request["requestid"];

        //显示销售申请明细
        DataTable dt = DC.DC_GetSale_Detail(requestid);
        DataTable ddmx = DC.DC_Getddmx(requestid);


        if (ddmx.Rows.Count>0) Panel1.Visible=true;

        gv_ddmx.DataSource = ddmx;
        gv_ddmx.DataBind();
        if (Convert.ToInt16 (dt.Rows[0]["status_id"].ToString())>=0)
        {
            (gv_ddmx).Columns[13].Visible = false;
            (gvFile_ddfj).Columns[2].Visible = false;
            (gvFile_zkck_fj).Columns[2].Visible = false;
            (gvFile_xsfp_fj).Columns[2].Visible = false;
            (gv_chuo).Columns[2].Visible = false;

            (gvFile_ddfj).Columns[0].Visible = false;
            (gvFile_zkck_fj).Columns[0].Visible = false;
            (gvFile_xsfp_fj).Columns[0].Visible = false;
            (gv_chuo).Columns[0].Visible = false;

            txt_Userid.Value = dt.Rows[0]["Userid"].ToString();
            txt_UserName.Value = dt.Rows[0]["UserName"].ToString();
            txt_UserName_AD.Value = dt.Rows[0]["UserName_AD"].ToString();
            txt_dept.Value = dt.Rows[0]["dept"].ToString();
            txt_managerid.Value = dt.Rows[0]["managerid"].ToString();
            txt_manager.Value = dt.Rows[0]["manager"].ToString();
            txt_manager_AD.Value = dt.Rows[0]["manager_AD"].ToString();
            txt_Code.Value = dt.Rows[0]["Code"].ToString();
            //txt_huoyun_no.Value = dt.Rows[0]["Code"].ToString();
            if (dt.Rows[0]["CreateDate"].ToString() != "")
            {
                txt_CreateDate.Value = Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString()).ToString("yyyy-MM-dd");
            }
            if (dt.Rows[0]["yjthrq_wuliu"].ToString() != "")
            {
                txt_yjthdate.Value = Convert.ToDateTime(dt.Rows[0]["yjthrq_wuliu"].ToString()).ToString("yyyy-MM-dd");
            }
            if (dt.Rows[0]["jjdhrq_wuliu"].ToString() != "")
            {
                txt_dhdate.Value = Convert.ToDateTime(dt.Rows[0]["jjdhrq_wuliu"].ToString()).ToString("yyyy-MM-dd");
                if (txt_dhdate.Value == "1900-01-01")
                {
                    txt_dhdate.Value = "";
                }
            }
            if (dt.Rows[0]["qrch_ware"].ToString() != "")
            {
                txt_chuodate.Value = Convert.ToDateTime(dt.Rows[0]["qrch_ware"].ToString()).ToString("yyyy-MM-dd");
            }
            if (dt.Rows[0]["tihuodate_ware"].ToString() != "")
            {
                txt_thdate.Value = Convert.ToDateTime(dt.Rows[0]["tihuodate_ware"].ToString()).ToString("yyyy-MM-dd");
            }
            txt_fayun_date.Text = Convert.ToDateTime(dt.Rows[0]["fyrq_sale"].ToString()).ToString("yyyy-MM-dd");
            txt_shdz.Value = dt.Rows[0]["shdz_sale"].ToString();
            txtdesc_sale.Value= dt.Rows[0]["tjdesc_sale"].ToString();
            DropDC_Uid.SelectedValue = dt.Rows[0]["dc_uid"].ToString();
            if (dt.Rows[0]["hyno_wuliu"].ToString() == "")
            {
                txt_huoyun_no.Value = dt.Rows[0]["Code"].ToString();
            }
            else
            {
                txt_huoyun_no.Value = dt.Rows[0]["hyno_wuliu"].ToString();
            }
            txt_hydl.Value = dt.Rows[0]["hydl_wuliu"].ToString();
            tjdesc_wuliu.Value = dt.Rows[0]["tidesc_wuliu"].ToString();
            tjdesc_ware.Value = dt.Rows[0]["tjdesc_ware"].ToString();
            
        }

       //判断登录者是否为之前操作的销售人员
        if (Session["empid"].ToString() == dt.Rows[0]["Userid"].ToString())
        {
            btn_sale.Visible = true;
            (gv_ddmx).Columns[12].Visible = true;
            (gvFile_ddfj).Columns[2].Visible = true;
            (gvFile_ddfj).Columns[0].Visible = false;
            btn_sale.Text = "修改";


        }
        else
        {
            btn_sale.Visible = false; ;
        }
        //判断登录者是否为物流订舱人员
        
        if (Session["empid"].ToString() == DropDC_Uid.SelectedValue )
        {
            if (DC.DC_Getstatus(requestid, 1).Rows.Count > 0 || DC.DC_Getstatus(requestid, 2).Rows.Count > 0)
            {
                btn_wuliu.Text = "修改";
                btn_sale.Visible = false;
                (gvFile_zkck_fj).Columns[0].Visible = false;
                (gvFile_xsfp_fj).Columns[0].Visible = false;
            }
            btn_wuliu.Visible = true;
            (gvFile_zkck_fj).Columns[2].Visible = true;
            (gvFile_xsfp_fj).Columns[2].Visible = true;
            Session["lv"] = "wuliu";
            if (txt_huoyun_no.Value == txt_Code.Value)
            {
                txt_huoyun_no.Value = "";
            }
        }

        //判断登录者是否为仓库主管
        if (Session["empid"].ToString() == DC.GetWare().Rows[0][0].ToString())
        {
            (gv_chuo).Columns[2].Visible = true;
            if (DC.DC_Getstatus(requestid, 2).Rows.Count > 0)
            {
                btn_ck.Visible = true;
                btn_ck.Text = "修改";
                Session["lv"] = "ware";
            }
            if (DC.DC_Getstatus(requestid, 1).Rows.Count > 0)
            {
                btn_ck.Visible = true;
                Session["lv"] = "ware";
            }

        }
    }
    protected void Btn_ddfj_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_xsfp_fj_Click(object sender, EventArgs e)
    {
        if (txt_xsfp_fj.Value != "")
        {
            FileUpload(txt_Code.Value, Session["empid"].ToString(), txt_xsfp_fj, txt_xsfp_fj.ID, gvFile_xsfp_fj);
        }
    }
    protected void Btn_zkck_fj_Click(object sender, EventArgs e)
    {
        if (txt_zkck_fj.Value != "")
        {
            FileUpload(txt_Code.Value, Session["empid"].ToString(), txt_zkck_fj, txt_zkck_fj.ID, gvFile_zkck_fj);
        }
    }
    protected void gvFile_ddfj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_ddfj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, Session["empid"].ToString(), txt_ddfj.ID, gvFile_ddfj);
    }

    protected void Btn_ddfj_Click1(object sender, EventArgs e)
    {
        if (txt_ddfj.Value != "")
        {
            FileUpload(txt_Code.Value, Session["empid"].ToString(), txt_ddfj, txt_ddfj.ID, gvFile_ddfj);
        }
    }
    public static string savepath = "\\UploadFile\\DC";
    //public void FileUpload(string YJNo, string Engineer, System.Web.UI.HtmlControls.HtmlInputFile FileUpLoader, string File_mc, GridView gv)
    //{
    //    //if (FileUpLoader.PostedFile != null)
    //    if (FileUpLoader.PostedFile.FileName.Length != 0)
    //    {
    //        string filename = FileUpLoader.PostedFile.FileName;
    //        string MapDir = MapPath("~");
    //        string yjpath = MapDir + savepath + "\\" + YJNo;
    //        if (!System.IO.Directory.Exists(yjpath))
    //        {
    //            System.IO.Directory.CreateDirectory(yjpath);//不存在就创建目录 
    //        }
    //        FileUpLoader.PostedFile.SaveAs(yjpath + "\\" + filename);
    //        FileSaveToDB(YJNo, Engineer, filename, File_mc, gv);
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择文件后再点击上传！')", true);
    //    }

    //}
    public void FileUpload(string YJNo, string Engineer, System.Web.UI.HtmlControls.HtmlInputFile FileUpLoader, string File_mc, GridView gv)
    {
        //if (FileUpLoader.PostedFile != null)
        if (FileUpLoader.PostedFile.FileName.Length != 0)
        {
            string filename = FileUpLoader.PostedFile.FileName;
            if (filename.Contains("\\") == true)
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }

            string MapDir = MapPath("~");
            string yjpath = MapDir + savepath + "\\" + YJNo;
            if (!System.IO.Directory.Exists(yjpath))
            {
                System.IO.Directory.CreateDirectory(yjpath);//不存在就创建目录 
            }
            FileUpLoader.PostedFile.SaveAs(yjpath + "\\" + filename);
            FileSaveToDB(YJNo, Engineer, filename, File_mc, gv);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('选择文件后再点击上传！')", true);
        }

    }
    public void FileSaveToDB(string YJNo, string Engineer, string filename, string File_mc, GridView gv)
    {
        string File_lj = savepath + "\\" + YJNo + "\\" + filename;
        string strSql = "insert into form1_Sale_YJ_UPLOAD(Code, UpLoad_user, File_name, File_lj,File_mc) values('" + YJNo + "','" + Engineer + "','" + filename + "','" + File_lj + "','" + File_mc + "')";
        DbHelperSQL.ExecuteSql(strSql);
        ShowFile(YJNo, Engineer, File_mc, gv);
    }
    public void FileDelete(string id, string filepath, string YJNo, string Engineer, string File_mc, GridView gv)
    {
        string strSql = "delete from form1_Sale_YJ_UPLOAD where id=" + id;
        DbHelperSQL.ExecuteSql(strSql);
        FileDirDelete(filepath);
        ShowFile(YJNo, Engineer, File_mc, gv);
    }
    public void FileDirDelete(string filedir)
    {
        if (System.IO.File.Exists(filedir))
        {
            System.IO.File.Delete(filedir);
        }

    }
    public void ShowFile(string YJNo, string Engineer, string File_mc, GridView gv)
    {
        string strSql = "select * from form1_Sale_YJ_UPLOAD where Code='" + YJNo + "' and File_mc='" + File_mc + "'";
        DataSet ds = DbHelperSQL.Query(strSql);
        gv.DataSource = ds;
        gv.DataBind();
    }
    public void ShowFileload(string YJNo, string File_mc, GridView gv)
    {
        string strSql = "select * from form1_Sale_YJ_UPLOAD where Code='" + YJNo + "' and File_mc='" + File_mc + "'";
        DataSet ds = DbHelperSQL.Query(strSql);
        gv.DataSource = ds;
        gv.DataBind();
    }
    protected void btn_sale_Click(object sender, EventArgs e)
    {
        int result_Main = 0;
        if (gv_ddmx.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('订单明细无内容，请重新选择发运日期和送货地址！')", true);
            return;
        }
        for (int i = 0; i < gv_ddmx.Rows.Count; i++)
        {
            if (((TextBox)this.gv_ddmx.Rows[i].FindControl("box_quantity")).Text == "" || ((TextBox)this.gv_ddmx.Rows[i].FindControl("zxfa")).Text == "" || ((TextBox)this.gv_ddmx.Rows[i].FindControl("mz")).Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请维护箱子数量&装箱方案&毛重！')", true);
                return;
            }
            if (this.gv_ddmx.Rows[i].Cells[2].ToString() == "" )
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('QAD单号为空,请确认！')", true);
                return;
            }
        }
        //if (gvFile_ddfj.Rows.Count <= 0)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传订单信息附件！')", true);
        //    return;
        //}
        string sql = "select isnull(max(requestid),1000)+1 from form2_Sale_DC_MainTable";
        int no =Convert.ToInt16( DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());
        if (btn_sale.Text == "提交")//首次提交时insert
        {
            result_Main = DC.DC_Insert(1, no, txt_Code.Value, txt_CreateDate.Value, txt_Userid.Value, txt_UserName.Value, txt_UserName_AD.Value, txt_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, 0, DropDC_Uid.SelectedValue, DropDC_Uid.SelectedItem.Text, txt_fayun_date.Text, txt_shdz.Value, txtdesc_sale.Value, "", "", "", "", "", "", "", "", btn_sale.Text);
            if (result_Main > 0)
            {
                for (int i = 0; i < gv_ddmx.Rows.Count; i++)
                {
                    string strsql = " insert into form2_Sale_DC_DetailTable(requestId,xmh,customer_ddh,customer_mc,customer_ljh,yhsl,xzsl,zxfa,mz,ysdesc,dhdate,requestId_YJ,ISok, modify_date) VALUES('" + no + "','" + gv_ddmx.Rows[i].Cells[3].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[4].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[5].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[6].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[7].Text.ToString().Replace("&nbsp;", "") + "','" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("box_quantity")).Text + "','" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("zxfa")).Text + "','" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("mz")).Text + "','" + gv_ddmx.Rows[i].Cells[11].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[12].Text.ToString().Replace("&nbsp;", "") + "','" + gv_ddmx.Rows[i].Cells[0].Text.ToString().Replace("&nbsp;", "") + "','Y',convert(varchar(20),getdate(),120))";
                    DbHelperSQL.ExecuteSql(strsql);
                }
                if (result_Main > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "setcss", @"layer.alert('销售申请提交成功！');sendmail('" + no + "','0');", true);
                    btn_sale.Enabled = false;
                    btn_sale.CssClass = "btn btn-primary disabled";
                }
            }
             
        }
        else//否则只是更新栏位
        {
            result_Main = DC.DC_Insert(1,Convert.ToInt16( Request["requestid"].ToString()), txt_Code.Value, txt_CreateDate.Value, txt_Userid.Value, txt_UserName.Value, txt_UserName_AD.Value, txt_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, 0, DropDC_Uid.SelectedValue, DropDC_Uid.SelectedItem.Text, txt_fayun_date.Text, txt_shdz.Value, txtdesc_sale.Value, "", "", "", "", "", "", "", "", btn_sale.Text);
            for (int i = 0; i < gv_ddmx.Rows.Count; i++)
            {
                string strsql = " update  form2_Sale_DC_DetailTable set xzsl='" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("box_quantity")).Text + "',zxfa='" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("zxfa")).Text + "',mz='" + ((TextBox)this.gv_ddmx.Rows[i].FindControl("mz")).Text + "' where requestid='" + Request["requestid"] + "' and requestid_YJ='" + gv_ddmx.Rows[i].Cells[0].Text.ToString() + "'";
                DbHelperSQL.ExecuteSql(strsql);
            }
           // if (result_Main > 0)
           // {
                btn_sale.Text = "修改";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('销售申请修改成功！')", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setcss", @"layer.alert('销售申请修改成功！');sendmail('" + no + "','0');", true);
           // }
        }
            
            
    }
    protected void txt_shdz_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_CP_ID_TextChanged(object sender, EventArgs e)
    {
        if (txt_fayun_date.Text != "")
        {
            DataTable dt = DC.DC_GetXMH(txt_fayun_date.Text, txt_CP_ID.Text);
            ViewState["detail"] = dt;
            gv_ddmx.DataSource = dt;
            gv_ddmx.DataBind();
            Panel1.Visible = true;
            if (dt.Rows.Count>0)
            txt_shdz.Value = dt.Rows[0]["shdz"].ToString();
            GetSite();
        }
    }
    protected void gv_ddmx_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        DataTable dt;
        if (Request["requestid"] != null)
        {
            dt =DC.DC_Getddmx(Request["requestid"]);
            string strsql = "update form2_Sale_DC_DetailTable set isok='N',modify_date=convert(varchar(20),getdate(),120) WHERE requestid_YJ='" + id + "' AND requestid='"+Request["requestid"]+"'";
            DbHelperSQL.ExecuteSql(strsql);
        }
        else
        {
             dt = (DataTable)ViewState["detail"];
        }
        DataRow[] foundRow;
        foundRow = dt.Select("requestid='" + id + "'"); 
        foreach (DataRow row in foundRow)
        { dt.Rows.Remove(row); }

        gv_ddmx.DataSource = dt;
        gv_ddmx.DataBind();
    }
    protected void btn_wuliu_Click(object sender, EventArgs e)
    {
        if (gvFile_zkck_fj.Rows.Count <= 0 || gvFile_xsfp_fj.Rows.Count<=0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传装箱出库单附件和形式发票附件！')", true);
            return;
        }
        int result_Main = DC.DC_Insert(2, Convert.ToInt16(Request["requestid"]), txt_Code.Value, txt_CreateDate.Value, txt_Userid.Value, txt_UserName.Value, txt_UserName_AD.Value, txt_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, 0, DropDC_Uid.SelectedValue, DropDC_Uid.SelectedItem.Text, txt_fayun_date.Text, txt_shdz.Value, txtdesc_sale.Value, txt_huoyun_no.Value, txt_yjthdate.Value, txt_dhdate.Value, txt_hydl.Value, tjdesc_wuliu.Value, "", "", "","");
        if (result_Main > 0 && btn_wuliu.Text == "提交")
        {
            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('物流订舱提交成功！')", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "setcss", @"layer.alert('物流订舱提交成功！');sendmail('" + Request["requestid"] + "','1');", true);
            btn_wuliu.Text = "修改";
        }
        else
        {   
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('物流订舱修改成功！')", true);
        }
    }
    protected void btn_ck_Click(object sender, EventArgs e)
    {
        if (gv_chuo.Rows.Count<=0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请上传出货照片！')", true);
            return;
        }
        int result_Main = DC.DC_Insert(3, Convert.ToInt16(Request["requestid"]), txt_Code.Value, txt_CreateDate.Value, txt_Userid.Value, txt_UserName.Value, txt_UserName_AD.Value, txt_dept.Value, txt_managerid.Value, txt_manager.Value, txt_manager_AD.Value, 0, DropDC_Uid.SelectedValue, DropDC_Uid.SelectedItem.Text, txt_fayun_date.Text, txt_shdz.Value, txtdesc_sale.Value, txt_huoyun_no.Value, txt_yjthdate.Value, txt_dhdate.Value, txt_hydl.Value, tjdesc_wuliu.Value, txt_chuodate.Value, txt_thdate.Value, tjdesc_ware.Value,"");
        if (result_Main > 0 && btn_ck.Text == "提交")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "setcss", @"layer.alert('仓库已发货！');sendmail('" + Request["requestid"] + "','2');", true);
            btn_ck.Text = "修改";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('仓库已发货！')", true);
        }
    }
    protected void gvFile_zkck_fj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_zkck_fj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, Session["empid"].ToString(), txt_zkck_fj.ID, gvFile_zkck_fj);
    }
    protected void gvFile_xsfp_fj_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gvFile_xsfp_fj.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, Session["empid"].ToString(), txt_xsfp_fj.ID, gvFile_xsfp_fj);
    }
    protected void gv_chuo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string file_lj = ((HyperLink)gv_chuo.Rows[e.RowIndex].FindControl("HyperLink1")).NavigateUrl;
        string filepath = MapPath("~") + file_lj;
        FileDelete(id, filepath, txt_Code.Value, Session["empid"].ToString(), txt_ch_fj.ID, gv_chuo);
    }
    protected void txt_fayun_date_TextChanged(object sender, EventArgs e)
    {
        if (txt_shdz.Value != "" && Request["requestid"] == null)
        {
            DataTable dt = DC.DC_GetXMH(txt_fayun_date.Text, txt_CP_ID.Text);
            txt_shdz.Value = dt.Rows[0]["shdz"].ToString();
            ViewState["detail"] = dt;
            gv_ddmx.DataSource = dt;
            gv_ddmx.DataBind();
            Panel1.Visible = true;
        }
        else if (Request["requestid"] != null)
        {
            DataTable mx = DC.DC_Getddmx(Request["requestid"].ToString());
            gv_ddmx.DataSource = mx;
            gv_ddmx.DataBind();
            Panel1.Visible = true;
        }
        GetSite();
    }
    protected void GetSite()
    {
        if (gv_ddmx.Rows.Count > 0)
        {

            string comp = gv_ddmx.Rows[0].Cells[14].Text.ToString();
            

            if (comp == "200")
            {
                DropDC_Uid.SelectedItem.Text = "王敏";
                DropDC_Uid.SelectedItem.Value = "02167";
            }
            else if (comp == "100")
            {
                DropDC_Uid.SelectedItem.Text = "郁利亚";
                DropDC_Uid.SelectedItem.Value = "00490";
            }
        }
        
    }

    protected void gv_ddmx_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HyperLink)e.Row.Cells[0].FindControl("HyperLink1")).Text != "")
            {
                HyperLink HyperLink = e.Row.FindControl("HyperLink1") as HyperLink;
                HyperLink.Attributes.Add("OnClick", "if(window.open(encodeURI('../YangJian/Yangjian.aspx?requestid=" + e.Row.Cells[0].Text.ToString() + "'))) return false; ");
            }
        }
    }
    protected void Btn_ch_fj_Click(object sender, EventArgs e)
    {
        if (txt_ch_fj.Value != "")
        {
            FileUpload(txt_Code.Value, Session["empid"].ToString(), txt_ch_fj, txt_ch_fj.ID, gv_chuo);
        }
    }
    [WebMethod()]//或[WebMethod(true)]
    public static string SendEmail(string requestid,string status)
    {
        DbHelperSQL.ExecuteSql("EXEC DC_Sendmail_Submit_Tracking '" + requestid + "','" + status + "'");
        return requestid ;
    }

}