using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using IBatisNet.Common.Transaction;
using System.Web.Script.Services;
using System.Web.Services;
using MES.Model;
using MES.DAL;
using Maticsoft.Common;
using System.Collections;
using System.Reflection;
using System.Linq;

public partial class Pur_Po : System.Web.UI.Page
{
    string m_sid = "";
    public string fieldStatus;
    public string DisplayModel;
    public string ValidScript = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
       // Page.MaintainScrollPositionOnPostBack = true;
        ViewState["lv"] = "";
        string FlowID = "A";
        string StepID = "A";

        //接收
        if (Request.QueryString["instanceid"] != null)
        {
            this.m_sid = Request.QueryString["instanceid"].ToString();
        }
        

        LoginUser LogUserModel = null;
        if (Request.ServerVariables["LOGON_USER"].ToString() == "")
        {
            LogUserModel = InitUser.GetLoginUserInfo("02274", Request.ServerVariables["LOGON_USER"]);
        }
        else
        {
            LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        }

        Session["LogUser"] = LogUserModel;


        //加载表头控件
        List<TableRow> ls = Pgi.Auto.Control.ShowControl("PUR_PO_Main_Form", "HEAD", 2, "", "", "form-control input-s-sm");
        for (int i = 0; i < ls.Count; i++)
        {
            this.tblWLLeibie.Rows.Add(ls[i]);
        }

        List<TableRow> ls_pay = Pgi.Auto.Control.ShowControl("PUR_PO_Main_Form", "HEAD_PAY", 1, "", "", "form-control input-s-sm");
        for (int i = 0; i < ls_pay.Count; i++)
        {
            this.tablePay.Rows.Add(ls_pay[i]);
        }



        if (!IsPostBack)
        {
            //获取每步骤栏位状态设定值，方便前端控制其可编辑性
           
            if (Request.QueryString["flowid"]!=null)
            {
                FlowID = Request.QueryString["flowid"];
            }
           
            if (Request.QueryString["stepid"]!=null)
            {
                 StepID = Request.QueryString["stepid"];
            }
           
          
           
            

            DataTable ldt_detail = null;
            DataTable ldt_pay = null;
            string lssql = "";
            lssql = "select po.*,pr.wlType,pr.wlSubType,pr.wlh,pr.wlmc,pr.wlms,pr.usefor,pr.RecmdVendorName,pr.RecmdVendorId,pr.ApointVendorName";
            lssql += ",pr.ApointVendorId,pr.unit,pr.historyPrice,pr.targetPrice,pr.deliveryDate,(pr.targetPrice*pr.qty) as targetTotalPrice";
            lssql += " from PUR_PO_Dtl_Form po";
            lssql += " left join PUR_PR_Dtl_Form pr on po.prno=pr.prno and po.PRRowId=pr.rowid";
            if (this.m_sid == "")
            {
                //新增
                if (LogUserModel != null)
                {
                    //新增时表头基本信息
                    ((TextBox)this.FindControl("ctl00$MainContent$CreateDate")).Text = System.DateTime.Now.ToString();
                    ((TextBox)this.FindControl("ctl00$MainContent$CreateByName")).Text = LogUserModel.UserName;
                    ((TextBox)this.FindControl("ctl00$MainContent$DeptName")).Text = LogUserModel.DepartName;
                    ((TextBox)this.FindControl("ctl00$MainContent$PoDomain")).Text = LogUserModel.DomainName;
                    ((TextBox)this.FindControl("ctl00$MainContent$IsToQAD")).Text = "是";
                }

                lssql += " where 1=0";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];

                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt_detail,2);
                ldt_pay = DbHelperSQL.Query("select * from PUR_PO_ContractPay_Form where 1=0").Tables[0];
                
                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "PO_PAY", this.gv2, ldt_pay);

            }
            else
            {
                //编辑  
                //表头赋值
                DataTable ldt= DbHelperSQL.Query("select * from PUR_PO_Main_Form where PoNo='"+this.m_sid+"'").Tables[0];
                Pgi.Auto.Control.SetControlValue("PUR_PO_Main_Form", "HEAD", this.Page, ldt.Rows[0], "ctl00$MainContent$");
                if (ldt.Rows[0]["attachments"].ToString()!="")
                {
                    this.txtfile.NavigateUrl = ldt.Rows[0]["attachments"].ToString();
                    this.txtfile.Visible = true;
                }
               
                
                //特殊处理
                ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Value = "";
                ((DevExpress.Web.ASPxComboBox)this.FindControl("ctl00$MainContent$povendorid")).Value = ldt.Rows[0]["PoVendorId"].ToString() + "," + ldt.Rows[0]["PoVendorName"].ToString();
                lssql += " where pono='"+this.m_sid+"'";
                ldt_detail = DbHelperSQL.Query(lssql).Tables[0];

                Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt_detail,2);
               

            }

            //增加事件
            for (int i = 0; i < ldt_detail.Rows.Count; i++)
            {

                ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["TaxPrice"], "TaxPrice")).TextChanged += new EventHandler(txt_TextChanged);

            }


        }
        else
        {
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            this.gv.Columns.Clear();
           
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt,2);
            for (int i = 0; i < ldt.Rows.Count; i++)
            {

                ((TextBox)this.gv.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)this.gv.Columns["TaxPrice"], "TaxPrice")).TextChanged += new EventHandler(txt_TextChanged);

            }
        }


      
        DisplayModel = Request.QueryString["display"] ?? "0";
        RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
        fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
    }


    protected void gv_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {

        if (e.CommandArgs.CommandName == "Add")
        {
            //新增一行
            //DevExpress.Web.GridViewDataColumn t = this.gv.Columns[0] as DevExpress.Web.GridViewDataColumn;
            //DevExpress.Web.ASPxTextBox tb1 = (DevExpress.Web.ASPxTextBox)this.gv.FindRowCellTemplateControl(0, t, "daoju_no");
            

        }
        else if (e.CommandArgs.CommandName == "Add1")
        {
           
        }
        else if (e.CommandArgs.CommandName == "JS")
        {
            
        }
    }
    private bool SaveData()
    {
        bool bflag = false;
        string lspgi_no = "";
        string lsdomain = "";
        //定义总SQL LIST
        List<Pgi.Auto.Common> ls_sum = new List<Pgi.Auto.Common>();
        //获取表头
        List<Pgi.Auto.Common> ls = Pgi.Auto.Control.GetControlValue("PUR_PO_Main_Form", "HEAD", this, "ctl00$MainContent${0}");
        for (int i = 0; i < ls.Count; i++)
        {
            Pgi.Auto.Common com = new Pgi.Auto.Common();
            com = ls[i];
            if (ls[i].Code == "")
            {

                Pgi.Auto.Public.MsgBox(this, "alert", ls[i].Value + " 不能为空!");
                //Response.Write(ls[i].Value + "不能为空!");
                return false;
            }

        }

        //特殊数据处理
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].Code=="podomain")
            {
                if (ls[i].Value == "昆山工厂")
                {
                    ls[i].Value = "200";
                    lsdomain = "200";
                }
                else if (ls[i].Value == "上海工厂")
                {
                    ls[i].Value = "100";
                    lsdomain = "100";
                }
                else
                {
                    lsdomain = ls[i].Value;
                }
            }

            if (ls[i].Code.ToLower()== "povendorid")
            {
                string[] lsstr = ls[i].Value.ToString().Split(',');
                ls[i].Value = lsstr[0];


                //增加供应商名称
                Pgi.Auto.Common lcpovendname = new Pgi.Auto.Common();
                lcpovendname.Code = "povendorname";
                lcpovendname.Key = "";
                lcpovendname.Value = lsstr[1];
                ls.Add(lcpovendname);

            }
           
        }

       
      
        if (this.m_sid == "")
        {
            //没有单号，自动生成
             this.m_sid = Pgi.Auto.Public.GetNo("PO", "PO");
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Code.ToLower() == "pono")
                {
                    ls[i].Value = this.m_sid;
                    ((TextBox)this.FindControl("ctl00$MainContent$PoNo")).Text = this.m_sid;
                }

            }

            //新增时增加创建人ID
            Pgi.Auto.Common lccreate_byid = new Pgi.Auto.Common();
            lccreate_byid.Code = "createbyid";
            lccreate_byid.Key = "";
            lccreate_byid.Value = ((LoginUser)Session["LogUser"]).UserId;
            ls.Add(lccreate_byid);
        }


           //自定义，上传文件
           var filepath = "";
            if (this.FileUpload1.HasFile)
            {
                SaveFile(this.FileUpload1, this.m_sid, out filepath, "123.txt", "123.txt");
            //增加上传文件列
            Pgi.Auto.Common lcfile = new Pgi.Auto.Common();
            lcfile.Code = "attachments";
            lcfile.Key = "";
            lcfile.Value = filepath;
            ls.Add(lcfile);

           }






        //表体生成SQL
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        ldt.AcceptChanges();


        //主表相关字段赋值到明细表
        decimal lntotalpay = 0;
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
           
            for (int j = 0; j < ls.Count; j++)
            {
                if (ls[j].Code.ToLower() == "pono")
                {
                    ldt.Rows[i]["pono"] = ls[j].Value;
                }
                if (ldt.Rows[i]["TotalPrice"].ToString()!="")
                {
                    lntotalpay += Convert.ToDecimal(ldt.Rows[i]["TotalPrice"].ToString());
                }
            } 
        }

        //从明细表中合计采购总金额
        Pgi.Auto.Common lcTotalPay = new Pgi.Auto.Common();
        lcTotalPay.Code = "totalpay";
        lcTotalPay.Key = "";
        lcTotalPay.Value = ((LoginUser)Session["LogUser"]).UserId;
        ls.Add(lcTotalPay);


        //获取的表头信息，自动生成SQL，增加到SUM中
        ls_sum.Add(Pgi.Auto.Control.GetList(ls, "PUR_PO_Main_Form"));


        //明细数据自动生成SQL，并增入SUM
        List<Pgi.Auto.Common> ls1 = Pgi.Auto.Control.GetList(ldt, "PUR_PO_Dtl_Form", "id", "flag,wlType,wlSubType,wlh,wlmc,wlms,targetPrice,targetTotalPrice,RecmdVendorName,historyPrice,deliveryDate,RecmdVendorId");
        for (int i = 0; i < ls1.Count; i++)
        {
            ls_sum.Add(ls1[i]);
        }

        //明细删除增加到list中
        if (Session["del"]!=null)
        {
            DataTable ldt_del = (DataTable)Session["del"];
            for (int i = 0; i < ldt_del.Rows.Count; i++)
            {
                Pgi.Auto.Common ls_del = new Pgi.Auto.Common();
                ls_del.Sql = "delete from PUR_PO_Dtl_Form where id="+ldt_del.Rows[i]["id"].ToString()+"";
                ls_sum.Add(ls_del);
            }
            Session["del"] = null;
        }

        //批量提交
       
        int ln = Pgi.Auto.Control.UpdateListValues(ls_sum);
       
        if (ln > 0)
        {
            bflag = true;
           // string instanceid = ln.ToString();
            string title = "PO采购单申请" +lspgi_no+"--"+ this.m_sid;
            script = "$('#instanceid',parent.document).val('" + this.m_sid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + "');";
          
        }
        else
        {
            bflag = false;
        }
       
        return bflag;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        bool bflag = this.SaveData();
        if (bflag == true)
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存成功!" );

        }
        else
        {
            Pgi.Auto.Public.MsgBox(Page, "alert", "保存失败!");
        }
    }

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        for (int i = 0; i < ldt.Rows.Count; i++)
        {
            if (ldt.Rows[i]["TaxPrice"].ToString() != "" && ldt.Rows[i]["TaxRate"].ToString() != "")
            {
                ldt.Rows[i]["NoTaxPrice"] = Math.Round(Convert.ToDecimal(ldt.Rows[i]["TaxPrice"].ToString()) / (1 + Convert.ToDecimal(ldt.Rows[i]["TaxRate"].ToString().Replace("%", "")) / 100), 2);
            }
         
            if (ldt.Rows[i]["TaxPrice"].ToString() != "" && ldt.Rows[i]["PurQty"].ToString() != "")
            {
               
                ldt.Rows[i]["TotalPrice"] = Convert.ToDecimal(ldt.Rows[i]["TaxPrice"].ToString()) * Convert.ToDecimal(ldt.Rows[i]["PurQty"].ToString());

            }
        }
        this.gv.Columns.Clear();
        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt,2);
    }

  

   
   

    #region "保存，发送流程固定用法，不可随意变更"
    string script = "";//全局前端控制Script
    //临时保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存数据
         bool flag = SaveData();
        //保存当前流程
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSave(true);", true);
        }

    }
    //发送按钮
    protected void btnflowSend_Click(object sender, EventArgs e)
    {
        //保存数据
         bool flag= SaveData();
        //发送
        if (flag == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", script + " parent.flowSend(true);", true);
        }
    }
    #endregion

  

    protected void gv_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (Session["pr_select"]!=null)
        {
            DataTable ldt1 = (DataTable)Session["pr_select"];
            DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
            int ln = 0;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                if (Convert.ToInt32(ldt.Rows[i]["rowid"].ToString())>ln)
                {
                    ln = Convert.ToInt32(ldt.Rows[i]["rowid"].ToString());
                }
            }
            for (int i = 0; i < ldt1.Rows.Count; i++)
            {
                DataRow ldr = ldt.NewRow(); ;
                if (ldt.Select("prno='"+ldt1.Rows[i]["prno"].ToString()+"' and prrowid='"+ldt1.Rows[i]["rowid"].ToString()+"'").Length>0)
                {
                    continue;
                }
               
                ldr["rowid"] = (ln+1).ToString("00");
                ldr["PRNo"] = ldt1.Rows[i]["prno"].ToString();
                ldr["PRRowId"] = ldt1.Rows[i]["rowid"].ToString();
                ldr["wlType"] = ldt1.Rows[i]["wlType"].ToString();
                ldr["wlSubType"] = ldt1.Rows[i]["wlSubType"].ToString();
                ldr["wlh"] = ldt1.Rows[i]["wlh"].ToString();
                ldr["wlmc"] = ldt1.Rows[i]["wlmc"].ToString();
                ldr["wlms"] = ldt1.Rows[i]["wlms"].ToString();
                ldr["currency"] = ldt1.Rows[i]["currency"].ToString();
                ldr["targetPrice"] = ldt1.Rows[i]["targetPrice"].ToString();
                ldr["PurQty"] = ldt1.Rows[i]["qty"].ToString();
                ldr["RecmdVendorName"] = ldt1.Rows[i]["RecmdVendorName"].ToString();
                ldr["TaxPrice"] = ldt1.Rows[i]["targetPrice"].ToString();
                if (ldt1.Rows[i]["targetPrice"].ToString()!="" && ldt1.Rows[i]["TaxRate"].ToString()!="")
                {
                    ldr["NoTaxPrice"] = Math.Round(Convert.ToDecimal(ldt1.Rows[i]["targetPrice"].ToString()) / (1 + Convert.ToDecimal(ldt1.Rows[i]["TaxRate"].ToString().Replace("%","")) /100),2);
                }
                ldr["TaxRate"] = ldt1.Rows[i]["TaxRate"].ToString();
                ldr["deliveryDate"] = ldt1.Rows[i]["deliveryDate"].ToString();
                if (ldt1.Rows[i]["targetPrice"].ToString()!="" && ldt1.Rows[i]["qty"].ToString()!="")
                {
                    ldr["targetTotalPrice"] = Convert.ToDecimal(ldt1.Rows[i]["targetPrice"].ToString()) * Convert.ToDecimal(ldt1.Rows[i]["qty"].ToString());
                    ldr["TotalPrice"] = Convert.ToDecimal(ldt1.Rows[i]["targetPrice"].ToString()) * Convert.ToDecimal(ldt1.Rows[i]["qty"].ToString());

                }
                ldr["historyPrice"]=ldt1.Rows[i]["historyPrice"].ToString();
                ln += 1;
                ldt.Rows.Add(ldr);
            }
            this.gv.Columns.Clear();
            Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt,2);
            Session["pr_select"] = null;
        }
       
    }




    #region "上传文件"

    //保存上传文件路径
    public static string savepath = "UploadFile\\Purchase";
    public void SaveFile(FileUpload fileupload, string subpath, out string filepath, string oldName, string newName)
    {
        var path = MapPath("~") + savepath + "\\" + subpath;
        //Create directory
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        //save file
        var filename = "";
        if (fileupload.HasFile)
        {
            filename = fileupload.FileName.Replace(oldName, newName);
            path = path + "\\" + filename;
            fileupload.SaveAs(path.Replace("&", "_").TrimStart(' '));
        }
        //return save path
        filepath = "\\" + savepath + "\\" + subpath + "\\" + filename.Replace("&", "_").TrimStart(' ');
    }
    #endregion



    protected void btndel_Click(object sender, EventArgs e)
    {
        DataTable ldt = Pgi.Auto.Control.AgvToDt(this.gv);
        DataTable ldt_del = ldt.Clone();
        for (int i = ldt.Rows.Count-1; i >=0; i--)
        {
            if (ldt.Rows[i]["flag"].ToString()=="1" && ldt.Rows[i]["id"].ToString()=="")
            {
                ldt.Rows[i].Delete();
            }
            else if (ldt.Rows[i]["flag"].ToString() == "1" && ldt.Rows[i]["id"].ToString() != "")
            {
                ldt_del.Rows.Add(ldt.Rows[i].ItemArray);
                ldt.Rows[i].Delete();
            }
        }
        ldt.AcceptChanges();
        if (ldt_del.Rows.Count>0)
        {
            if (Session["del"] !=null)
            {
                for (int i = 0; i < ((DataTable)Session["del"]).Rows.Count; i++)
                {
                    ldt_del.Rows.Add(((DataTable)Session["del"]).Rows[i].ItemArray);
                }
               
            }
            Session["del"] = ldt_del;
        }
        Pgi.Auto.Control.SetGrid("PUR_PO_Main_Form", "DETAIL", this.gv, ldt,2);
        Pgi.Auto.Public.MsgBox(this.Page,"alert","删除成功!");
    }
}


