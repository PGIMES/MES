using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class JingLian_GP_CS : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    Function_GP gp = new Function_GP();
    //string sourcePath = @"\\172.16.9.22\d$\Spectro Smart Studio\Sample Results\";
    //string bakPath = @"\\172.16.9.22\d$\Spectro Smart Studio\Sample Results Report\";
    string sourcePath = @"\\172.16.9.22\dd$\Sample Results\";
    string bakPath = @"\\172.16.9.22\dd$\Sample Results Report\";
    string destPath = @"D:\GPdata\guangpuDoc\";//D:\MES\MES\guangpuDoc\
    string serverPath_bak = @"D:\GPdata\bak_guangpuDoc\";//D:\MES\MES\guangpuDoc\

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            ini_default();
            txt_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txt_time.Text = DateTime.Now.ToString("HH:ss:mm");
            if (Function_Jinglian.Emplogin_query(8, "", "").Rows[0][0].ToString() == "1")
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(7, "", "");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                txt_shift.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_banbie"].ToString();
                txt_name.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_name"].ToString();
                txt_banzu.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_banzhu"].ToString();


            }
            else
            {
                this.txt_gh.DataSource = Function_Jinglian.Emplogin_query(7, "", "");
                this.txt_gh.DataTextField = "emp_no";
                this.txt_gh.DataValueField = "emp_no";
                this.txt_gh.DataBind();
                this.txt_gh.Items.Insert(0, new ListItem("", ""));
                txt_gh.BackColor = System.Drawing.Color.Yellow;
            }
            ddl_source.BackColor = System.Drawing.Color.Yellow;
            ddl_dh.CssClass = "form-control input-s-sm  ";
            ddl_dh.Enabled = false;
            btn_confirm.Enabled = false;
            btn_confirm.CssClass = "btn btn-large btn-primary disabled";
           

        }
    }

    public void ini_default()
    {

        btn_begin.Enabled = true;
        btn_confirm.Enabled = false;
        btn_begin.CssClass = "btn btn-large btn-primary ";
        btn_confirm.CssClass = "btn btn-large btn-primary  disabled";
        lb_start.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        //ddl_source.Text = "";
        ddl_gys.Text = "";
        txt_bihao.Text = "";
        DataTable dt = Function_Jinglian.GPList_Query(3, "", "", "", "", "", "", "", "","","");
        this.ddl_dh.DataSource = dt;
        this.ddl_dh.DataTextField = "dh";
        this.ddl_dh.DataValueField = "dh";
        this.ddl_dh.DataBind();
        this.ddl_dh.Items.Insert(0, new ListItem("", ""));
        ddl_dh.BackColor = txt_name.BackColor;
        ddl_hejin.BackColor = txt_name.BackColor;
        ddl_sbno.BackColor = txt_name.BackColor;
        txt_bihao.BackColor = txt_name.BackColor;
        ddl_gys.BackColor = txt_name.BackColor;

        ddl_luhao.BackColor = txt_name.BackColor;
        this.ddl_luhao.DataSource = Function_Jinglian.ZybClear_Content_Query("6");
        this.ddl_luhao.DataTextField = "equip_name";
        this.ddl_luhao.DataValueField = "equip_name";
        this.ddl_luhao.DataBind();
        this.ddl_luhao.Items.Insert(0, new ListItem("", ""));

        this.ddl_sbno.DataSource = Function_Jinglian.ZybClear_Content_Query("5");
        this.ddl_sbno.DataTextField = "equip_no";
        this.ddl_sbno.DataValueField = "equip_no";
        this.ddl_sbno.DataBind();
        this.ddl_sbno.Items.Insert(0, new ListItem("", ""));

        this.ddl_hejin.DataSource = Function_Jinglian.Hydrogen_Query(4, "", "", "", "", "", "", "");
        this.ddl_hejin.DataTextField = "base_value";
        this.ddl_hejin.DataValueField = "base_value";
        this.ddl_hejin.DataBind();
        this.ddl_hejin.Items.Insert(0, new ListItem("", ""));
        ddl_dh.Enabled = false;
        ddl_gys.Enabled = false;
        txt_bihao.ReadOnly = true;
        ddl_sbno.Enabled = false;
        ddl_hejin.Enabled = false;
        ddl_luhao.Enabled = false;
        ddl_luhao.CssClass = "form-control input-s-sm  ";
        ddl_gys.CssClass = "form-control input-s-sm  ";
        ddl_hejin.CssClass = "form-control input-s-sm  ";
        ddl_sbno.CssClass = "form-control input-s-sm  ";
        DIV2.Style.Add("display", "none");
    }
    protected void txt_gh_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_shift.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_banbie"].ToString();
        txt_name.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_name"].ToString();
        txt_banzu.Text = Function_Jinglian.Emplogin_query(7, txt_gh.Text, "").Rows[0]["emp_banzhu"].ToString();
    }
    protected void btn_begin_Click(object sender, EventArgs e)
    {
        int del = gp.GP_Detail_insert(1, "", "", "", "", "", "", "", "", "", "", "", "","","");
        if (txt_gh.Text == "" || ddl_source.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择员工和样件来源！')", true);
            return;
        }
        if (ddl_source.Text == "精炼机")
        {
            if (ddl_dh.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择样件序列号！')", true);
                return;
            }
        }
        else if (ddl_source.Text == "保温炉")
        {
            if (ddl_sbno.Text=="" || ddl_hejin.Text=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择设备号和合金！')", true);
                return;
            }
        }
        else if (ddl_source.Text == "进货")
        {
            if (ddl_gys.Text == "" || ddl_hejin.Text == "" || txt_bihao.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择供应商，合金和原材料批号！')", true);
                return;
            }
        }
       
       // btn_begin.Enabled = false;
        btn_confirm.Enabled = true;
       // btn_begin.CssClass = "btn btn-large btn-primary ";
        btn_confirm.CssClass = "btn btn-large btn-primary ";
        lb_start.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        Copy_data("192.168.2.87", destPath, sourcePath, bakPath, "pgi", "pgi");
        
        DataTable dt = GetTemp();
        DIV2.Style.Add("display", "block");
        this.ddl_file.DataSource = dt;
        this.ddl_file.DataTextField = "filename";
        this.ddl_file.DataValueField = "filename";
        this.ddl_file.DataBind();

        string path = destPath + ddl_file.SelectedValue;
       //
       

        read1(path, "", "");
        GridView1.DataSource = gp.GP_Temp_query(ddl_dh.Text);
        GridView1.DataBind();
    }
    private DataTable GetTemp()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("filename", Type.GetType("System.String"));
        dt.Columns.Add("createtime", Type.GetType("System.DateTime"));
        DirectoryInfo di = new DirectoryInfo(destPath);
        FileInfo[] rgFiles = di.GetFiles("*.xml");
        foreach (FileInfo fi in rgFiles)
        {
            
                if (File.Exists(fi.FullName))
                {
                    string filename =  fi.Name;
                    string createtime = fi.LastWriteTime.ToString();
                    DataRow row = dt.NewRow();
                    row["filename"] = filename;
                    row["createtime"] = createtime;

                    dt.Rows.Add(row);
                   
                }
               
            
        }
        DataView ldv_file = dt.DefaultView;
        ldv_file.Sort = "createtime desc";
        dt = ldv_file.ToTable();
        return dt;
    }

    private void read1(string path, string no_exename, string name)
    {


        //string value_si = "";
        string si ="";
        string fe ="";
        string cu ="";
        string mg ="";
        string mn="";
        string cr ="";
        string ni ="";
        string zn= "";
        string ti="";
        string pb ="";
        string sn ="";
        string al ="";
        string sr="";
        string sf = "";

        if (System.IO.File.Exists(path))//判断指定路径的文件是否存在
        {
            XmlDocument xmlDoc = new XmlDocument();
            //  xmlDoc.Load(@"E:\pgi project\光谱分析\DATA\Results_8328.xml");
            xmlDoc.Load(path);
            XmlNode node_MeasurementReplicates = xmlDoc.SelectSingleNode("//MeasurementReplicates");
            int count1 = int.Parse(node_MeasurementReplicates.Attributes["Count"].Value);

            ///////////////////////////////////////////////////////////avage///////////////////////
            XmlNode node_avg = xmlDoc.SelectNodes("//Elements").Item(count1);
            XmlNodeList nodeList_avg = node_avg.ChildNodes;//Element
            //  XmlNodeList nodeList = xmlDoc.SelectSingleNode("//Elements").ChildNodes;//获取bookstore节点的所有子节点
            string ElementName = "";
            string ElementResult_status = "";
            float ResultValue = 0;

            foreach (XmlNode xni in nodeList_avg)
            {

                // x = x+string.Format("\n<TreeNode type=\"{0}\" id=\"{1}\" name=\"{2}\" security=\"{3}\" property=\"{4}\">", xni.Attributes["type"].Value, xni.Attributes["id"].Value, xni.Attributes["name"].Value, xni.Attributes["security"].Value, xni.Attributes["property"].Value);
                //   x = x + string.Format("\n<TreeNode type=\"{0}\" >", xni.Attributes["LineName"].Value);
                XmlElement xe = (XmlElement)xni;


                //  x = x + xe.Name + "   " + xe.Attributes[0].Value + "; ";
                ElementName = xe.Attributes[0].Value;
                XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点  //ElementResult

                foreach (XmlNode xn1 in nls)//遍历
                {
                    XmlElement xe2 = (XmlElement)xn1;//转换类型
                  
                    if (xe2.Name == "ElementResult")
                    {
                        if (xe2.Attributes["Type"].Value == "ConcTypeCorr" && xe2.Attributes["StatType"].Value == "Reported")
                       //if (xe2.Attributes["Type"].Value == "Conc" && xe2.Attributes["StatType"].Value == "Reported")
                        {
                            ElementResult_status = xe2.Attributes["Status"].Value;
                            XmlNodeList nls1 = xe2.ChildNodes;  //ResultValue

                            foreach (XmlNode xn2 in nls1)//遍历
                            {
                                XmlElement xe3 = (XmlElement)xn2;

                                if (xe3.Name == "ResultValue")
                                {
                                    // x = x + xe3.Name + "值;" + xe3.InnerText + ";";
                                    //  ResultValue = float.Parse(xe3.InnerText);
                                    //  DAL.record.RecordInsert_qa(name, bianhao, material, MeasureDateTime, ElementName, ElementResult_status, ResultValue, "", 0, banci, baohao, "xuyong");

                                    if (ElementName == "Si")
                                    {
                                        si = xe3.InnerText;
                                    }
                                    else if (ElementName == "Fe")
                                    {
                                        fe = xe3.InnerText;
                                    }
                                    else if (ElementName == "Cu")
                                    {
                                        cu = xe3.InnerText;
                                    }
                                    else if (ElementName == "Mn")
                                    {
                                        mn = xe3.InnerText;
                                    }
                                    else if (ElementName == "Mg")
                                    {
                                        mg = xe3.InnerText;
                                    }
                                    else if (ElementName == "Fe")
                                    {
                                        fe = xe3.InnerText;
                                    }
                                    else if (ElementName == "Cr")
                                    {
                                        cr = xe3.InnerText;
                                    }
                                    else if (ElementName == "Ni")
                                    {
                                        ni = xe3.InnerText;
                                    }
                                    else if (ElementName == "Zn")
                                    {
                                        zn = xe3.InnerText;
                                    }
                                    else if (ElementName == "Ti")
                                    {
                                        ti = xe3.InnerText;
                                    }
                                    else if (ElementName == "Pb")
                                    {
                                        pb = xe3.InnerText;
                                    }
                                    else if (ElementName == "Sn")
                                    {
                                        sn = xe3.InnerText;
                                    }
                                    else if (ElementName == "Al")
                                    {
                                        al = xe3.InnerText;
                                    }
                                    else if (ElementName == "Sr")
                                    {
                                        sr = xe3.InnerText;
                                    }

                                    else if (ElementName == "S.F")
                                    {
                                        sf = xe3.InnerText;
                                    }

                                }
                            }
                        }
                        else if (xe2.Attributes["Type"].Value == "Other" && xe2.Attributes["StatType"].Value == "Reported")
                        {
                            ElementResult_status = xe2.Attributes["Status"].Value;
                            XmlNodeList nls1 = xe2.ChildNodes;  //ResultValue

                            foreach (XmlNode xn2 in nls1)//遍历
                            {
                                XmlElement xe3 = (XmlElement)xn2;

                                if (xe3.Name == "ResultValue")
                                {
                                    // x = x + xe3.Name + "值;" + xe3.InnerText + ";";
                                    //  ResultValue = float.Parse(xe3.InnerText);
                                    //  DAL.record.RecordInsert_qa(name, bianhao, material, MeasureDateTime, ElementName, ElementResult_status, ResultValue, "", 0, banci, baohao, "xuyong");

                                   

                                    if (ElementName == "S.F")
                                    {
                                        sf = xe3.InnerText;
                                    }

                                }
                            }
                        }
                    }


                }
                // break;



            }


            //////////INSERT DATA
            string dh = ddl_dh.Text;
            string hejin = ddl_hejin.Text;
            gp.GP_Temp_insert( dh,  hejin,  si,  fe,  cu,  mg,  mn,  cr,  ni,  zn,  ti,  pb,  sn,  al,  sr,  sf);
           // File.Move(path, oqc_ok_path + name);
        }


    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = Function_Jinglian.GPList_Query(5, "", "", "", "", "", "", "","","","");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex == 2)
            {
                if (ddl_hejin.SelectedValue == "A356-T6")
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (dt.Rows[3][6 + i].ToString().ToUpper() == "GREEN")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Green;
                        }
                        if (dt.Rows[3][6 + i].ToString().ToUpper() == "YELLOW")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Yellow;
                        }
                        if (dt.Rows[3][6 + i].ToString().ToUpper() == "RED")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 14; i++)
                    {
                       // string XX = dt.Rows[5][6 + i].ToString();
                        if (dt.Rows[5][6 + i].ToString().ToUpper() == "GREEN")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Green;
                        }
                        if (dt.Rows[5][6 + i].ToString().ToUpper() == "YELLOW")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Yellow;
                        }
                        if (dt.Rows[5][6 + i].ToString().ToUpper() == "RED")
                        {
                            e.Row.Cells[1 + i].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            
        }

        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    private void Copy_data(string client_ip, string destPath, string sourcePath, string bakPath, string user, string password)
    {
        ValidateUser connect = new ValidateUser();

        bool isImpersonated = false;
        try
        {
            if (connect.impersonateValidUser(user, client_ip, password))
            {
                isImpersonated = true;
                //do what you want now, as the special user                
                DirectoryInfo di = new DirectoryInfo(sourcePath);
                FileInfo[] rgFiles = di.GetFiles("*.xml");
                foreach (FileInfo fi in rgFiles)
                {
                    if (Convert.ToInt32(fi.CreationTime.ToString("yyyyMMdd")) >= Convert.ToInt32(DateTime.Now.AddDays(0).ToString("yyyyMMdd")))
                    {
                        if (File.Exists(fi.FullName))
                        {
                            string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + fi.Name;
                            File.Copy(fi.FullName, destPath + filename);
                            //bakPath = bakPath + filename;//fi.Name.Substring(0, fi.Name.LastIndexOf("."))+ ".xml"
                            File.Move(fi.FullName, bakPath + filename);
                            
                        }

                    }
                }
            }
        }
        catch
        {
            //    BLL.path_config.path_configUpdate(client_ip, "异常");

        }
        finally
        {
            if (isImpersonated)
                connect.undoImpersonation();
        }



    }
    protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_source.Text == "精炼机")
        {
            ini_default();
            ddl_dh.BackColor = System.Drawing.Color.Yellow;
            ddl_dh.Enabled = true;

        }
        else if (ddl_source.Text == "保温炉")
        {
            ini_default();
            ddl_hejin.BackColor = System.Drawing.Color.Yellow;
            ddl_sbno.BackColor = System.Drawing.Color.Yellow;
            ddl_hejin.Enabled = true;
            ddl_sbno.Enabled = true;

        }
        else if (ddl_source.Text == "进货")
        {
            ini_default();
            ddl_gys.BackColor = System.Drawing.Color.Yellow;
            ddl_gys.Enabled = true;
            txt_bihao.ReadOnly = false;
            txt_bihao.BackColor = System.Drawing.Color.Yellow;
            ddl_hejin.BackColor = System.Drawing.Color.Yellow;
            ddl_hejin.Enabled = true;
        }
    }
    protected void ddl_dh_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Function_Jinglian.GPList_Query(4, "", "", "", "", ddl_dh.Text, "", "","","","");
        this.ddl_hejin.DataSource = dt;
        this.ddl_hejin.DataTextField = "hejin";
        this.ddl_hejin.DataValueField = "hejin";
        this.ddl_hejin.DataBind();

        this.ddl_sbno.DataSource = dt;
        this.ddl_sbno.DataTextField = "sbno";
        this.ddl_sbno.DataValueField = "sbno";
        this.ddl_sbno.DataBind();

        this.ddl_luhao.DataSource = dt;
        this.ddl_luhao.DataTextField = "luhao";
        this.ddl_luhao.DataValueField = "luhao";
        this.ddl_luhao.DataBind();

        
    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        lb_end.Text = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        int result = gp.GP_Detail_insert(2, ddl_source.Text, ddl_dh.Text, ddl_hejin.Text, ddl_luhao.Text, ddl_sbno.Text, lb_start.Text, lb_end.Text, ddl_file.Text, txt_gh.Text, txt_name.Text, txt_shift.Text, txt_banzu.Text,ddl_gys.Text,txt_bihao.Text);
        if (result >= 1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('测量完成！')", true);

            ///move源文件至备份文件夹
            DirectoryInfo di = new DirectoryInfo(destPath);
            FileInfo[] rgFiles = di.GetFiles("*.xml");
            foreach (FileInfo fi in rgFiles)
            {

                if (fi.Name== ddl_file.Text)
                    {
                        string filename =  fi.Name;
                        //bakPath = ;//fi.Name.Substring(0, fi.Name.LastIndexOf("."))+ ".xml"
                        File.Move(fi.FullName, serverPath_bak + filename);
                        break;
                    }

                
            }
            ini_default();
            ddl_source.Text = "";
            
        }
    }



    protected void ddl_sbno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_sbno.Text != "")
        {
            DataTable dt = Function_Jinglian.GPList_Query(6, "", "", "", "", "", "", "", ddl_sbno.Text,"","");
            this.ddl_dh.DataSource = dt;
            this.ddl_dh.DataTextField = "dh";
            this.ddl_dh.DataValueField = "dh";
            this.ddl_dh.DataBind();
          
           
        }
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DefaultYZSYS.aspx");
    }
    protected void ddl_gys_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_gys.Text!= "")
        {
            DataTable dt = Function_Jinglian.GPList_Query(6, "", "", "", "", "", "", "", "", "", ddl_gys.Text);
            this.ddl_dh.DataSource = dt;
            this.ddl_dh.DataTextField = "dh";
            this.ddl_dh.DataValueField = "dh";
            this.ddl_dh.DataBind();


        }
    }
}