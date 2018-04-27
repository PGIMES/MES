using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.IO;
public partial class BaoJia_BaoJia_Remark_flow : System.Web.UI.Page
{
    public static string savepath = "UploadFile\\BaoJia";
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["empid"]="02069";
        
        if (Session["empid"] == null )
        {   // 给Session["empid"]  
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!Page.IsPostBack)
        { 
            BaseFun fun = new BaseFun();
            DataTable dt = fun.getEmpInfo(Session["empid"].ToString());
            this.txtXingMing.Value = dt.Rows[0]["empname"].ToString();
            this.txtEmpId.Value = dt.Rows[0]["EMPLOYEEID"].ToString();
            this.txtRiQi.Value = DateTime.Now.ToShortDateString();
            this.txtBaoJia_no.Value = Request.QueryString["Baojia_no"];
            this.txtTurns.Value = Request.QueryString["turns"];

            //历史记录
            ShowFile();
        }
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[7].Style.Add("display","none");
        e.Row.Cells[2].Style.Add("display", "none");
        if (e.Row.RowType==DataControlRowType.DataRow)
        {   //显示行号
            e.Row.Cells[0].Text = (e.Row.DataItemIndex).ToString();
            if (e.Row.DataItemIndex == 0)
            {
                ((Label)e.Row.FindControl("lblRemarks")).Visible = false;
            }
            else if (e.Row.DataItemIndex == 1)
            {
                DateTime startTime = Convert.ToDateTime(e.Row.Cells[1].Text);//["create_date"]
                int totaldays = (startTime - DateTime.Now).Days;//天数
                if (totaldays < 0)
                {
                    ////判断第二行的时间是否为当天，如果是当天，则 第二行也为可编辑
                    ((TextBox)e.Row.FindControl("txtValue")).Visible = false;
                    ((FileUpload)e.Row.FindControl("txtFile")).Visible = false;
                }
                else
                {
                    ((Label)e.Row.FindControl("lblRemarks")).Visible = false;
                }
            }
            else
            {
                ((TextBox)e.Row.FindControl("txtValue")).Visible = false;
                ((FileUpload)e.Row.FindControl("txtFile")).Visible = false;
            }
        }
    }
    /// <summary>
    /// 点击保存按钮时，须遍历gridview最上面两行，看说明栏位是否为空，如果为空，则提示销售 需要输入状态描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //判断第2行的状态描述是否为空，如果为空 对第一行不做任何动作
        if (GridView1.Rows.Count >= 2)
        { 
            if (Server.HtmlDecode(((TextBox)GridView1.Rows[1].FindControl("txtValue")).Text).Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请输入第1行要修改的状态描述！')", true);
                return;
            }
        }

        FileSaveToDB(Request.QueryString["baojia_no"]);

    }
    public void FileUpload(string BJNo, System.Web.UI.WebControls.FileUpload  FileUpLoader,out string lj,out string filname)//System.Web.UI.HtmlControls.HtmlInputFile
    {
        string filename="";
        string yjpath = "";
        if (FileUpLoader.PostedFile.FileName.Length != 0)
        {
            filename = FileUpLoader.PostedFile.FileName;
            if (filename.Contains("\\") == true)
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }

            string MapDir = MapPath("~");
            yjpath = MapDir + savepath + "\\" + BJNo;
            if (!System.IO.Directory.Exists(yjpath))
            {
                System.IO.Directory.CreateDirectory(yjpath);//不存在就创建目录 
            }
            FileUpLoader.PostedFile.SaveAs(yjpath + "\\" + filename);
            
           // FileSaveToDB(BJNo,  filename);
        }
         
        lj = filename == "" ? "" : (yjpath + "\\" +  filename);
        filname = filename;
       // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.msg('文件保存成功！')", true);

    }
    /// <summary>
    /// 保存的时候将第一行的值塞入DB 如果上传的附件有值，则上传对应的文档
    ///           将第二行的值update入DB
    /// </summary>
    /// <param name="BJNo"></param>
    /// <param name="filename"></param>
    public void FileSaveToDB(string BJNo)
    {
        string sqlInsert = "";
        string sqlUpdate = "";
        string File_lj = "";
        string File_name = "";
        //insert File_name
        FileUpload fileUp0 = (FileUpload)GridView1.Rows[0].FindControl("txtFile");
        FileUpload(BJNo, fileUp0,out File_lj,out File_name);
       // File_lj =filename ==""?"":( savepath + "\\" + BJNo + "\\" + filename);
        if (((TextBox)GridView1.Rows[0].FindControl("txtValue")).Text.Replace("'", " ").Trim() != "")
        {
            string remark = ((TextBox)GridView1.Rows[0].FindControl("txtValue")).Text.Trim().Replace("'", " ");
            sqlInsert = "insert into [dbo].[Baojia_remarks_flow]( baojia_no, requestid, turns, remarks, file_path, file_name, create_by_empid, create_by_name, create_date) values('"
           + Request.QueryString["baojia_no"] + "','" + Request.QueryString["requestid"] + "','" + Request.QueryString["turns"] + "','"+ remark + "','" + File_lj + "','" + File_name + "','"+this.txtEmpId.Value+"','" + this.txtXingMing.Value + "',getdate());";

        }


        //update
        
        if (GridView1.Rows.Count >= 2)
        { 
            File_lj = "";
            File_name = "";
            FileUpload fileUp1 = (FileUpload)GridView1.Rows[1].FindControl("txtFile");
            if ( GridView1.Rows[1].FindControl("txtValue") !=null && fileUp1.Visible==true)
            {
                FileUpload(BJNo, fileUp1, out File_lj, out File_name);
                string remark = ((TextBox)GridView1.Rows[1].FindControl("txtValue")).Text.Trim().Replace("'"," ");
                string id = GridView1.Rows[1].Cells[7].Text;
                string str = "";
                if (File_lj != "")
                { //如果有重新传文件则更新文件
                    str = ",file_path='"+ File_lj + "', file_name='"+ File_name + "'";
                }
                sqlUpdate = "update [dbo].[Baojia_remarks_flow] set remarks='"+remark+"' "+ str + "   where id='"+id+"';";
         
            }
        }
        if (sqlInsert != "" || sqlUpdate != "")
        { DbHelperSQL.ExecuteSql(sqlInsert + sqlUpdate); }

        ShowFile();
    }
    //显示历史记录
    public void ShowFile()
    {

        string strSQL = @"  select * from ( select 100000000000 as id,''  baojia_no, '' requestid, '' turns, cast('' as varchar(1000)) as remarks, cast('' as varchar(1000)) as file_path, "
                    + "cast('' as varchar(1000)) as file_name,'"+ this.txtEmpId.Value + "' as create_by_empid, '"+this.txtXingMing.Value+"' as create_by_name,getdate() as create_date from[Baojia_remarks_flow] "
                    +" union select * from[Baojia_remarks_flow] where baojia_no = '" + Request.QueryString["baojia_no"] + "' "
                    +@" ) aa 
                    order by id desc ";
        DataTable  dt = DbHelperSQL.Query(strSQL).Tables[0];   
       
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //隐藏第一行新增的行号  不显示
        GridView1.Rows[0].Cells[0].Text = "";
         
    }


    //public void FileDelete()
    //{
    //    string strSql = "delete from Baojia_remarks_flow where requestid=" + Request.QueryString["requestid"];
    //    DbHelperSQL.ExecuteSql(strSql);
    //  //  FileDirDelete(filepath);
    //    ShowFile();
    //}
    //public void FileDirDelete(string filedir)
    //{
    //    if (System.IO.File.Exists(filedir))
    //    {
    //        System.IO.File.Delete(filedir);
    //    }
    //}

   
   


}