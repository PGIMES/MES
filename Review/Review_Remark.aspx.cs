using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.IO;
using System.Web.Services;
using System.Text;

public partial class Review_Remark : System.Web.UI.Page
{
    public static string savepath = "\\UploadFile\\Review";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["LogUser"] == null || Session["LogUser"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "reset", "$('#content').html('登入账号失效，请关闭重新登入.').addClass('label label-warning').css('font-size','15px').css('text-align','center').css('display','block');", true);
            return;
        }

        if (!Page.IsPostBack)
        { 
            BaseFun fun = new BaseFun();
            LoginUser logUser = (LoginUser)Session["LogUser"];
            
            this.txtXingMing.Value = logUser.UserName;
            this.txtEmpId.Value = logUser.UserId;
            this.txtRiQi.Value = DateTime.Now.ToShortDateString();
           // this.txtBaoJia_no.Value = Request.QueryString["Baojia_no"];
           // this.txtTurns.Value = Request.QueryString["turns"];

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
            //如果本单拥有者，可进行评价
            if (Convert.ToBoolean(Session["IsOwner"]) == true)
            {
                ((Label)e.Row.FindControl("lblAssessments")).Visible = true;
            }
            else
            {
                ((Label)e.Row.FindControl("lblAssessments")).Visible = false;
            }
            //格式化第一，二行显示格式
            if (e.Row.DataItemIndex == 0)
            {
                ((Label)e.Row.FindControl("lblRemarks")).Visible = false; 
                ((Label)e.Row.FindControl("lblAssessments")).Visible = false;
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
         string newfilename = DateTime.Now.ToString("yyMMddHHmmss") ;
        if (FileUpLoader.PostedFile.FileName.Length != 0)
        {
            filename = FileUpLoader.PostedFile.FileName;
            string fileExtension = System.IO.Path.GetExtension(FileUpLoader.FileName).ToLower();
            

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
            FileUpLoader.PostedFile.SaveAs(yjpath + "\\" + newfilename + filename.Replace('&', '_'));//替换敏感字符
            
           // FileSaveToDB(BJNo,  filename);
        }
         
        lj = filename == "" ? "" : (savepath + "\\" + BJNo + "\\" + newfilename + filename.Replace('&','_')  );//替换敏感字符
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
        string slnid = Request.QueryString["slnid"];
        //insert File_name
        FileUpload fileUp0 = (FileUpload)GridView1.Rows[0].FindControl("txtFile");
        FileUpload(BJNo, fileUp0,out File_lj,out File_name);

       // File_lj =filename ==""?"":( savepath + "\\" + BJNo + "\\" + filename);
        if (((TextBox)GridView1.Rows[0].FindControl("txtValue")).Text.Replace("'", " ").Trim() != "")
        {
            string remark = ((TextBox)GridView1.Rows[0].FindControl("txtValue")).Text.Trim().Replace("'", " ");
            
            sqlInsert = "insert into [dbo].[Q_Review_Remarks]( slnid,  remarks, file_path, file_name,Assessments, create_by_empid, create_by_empname, create_date) values('"
           + slnid + "','"+ remark + "','" + File_lj + "','" + File_name + "','','"+this.txtEmpId.Value+"','" + this.txtXingMing.Value + "',getdate());";
            sqlInsert += "update Q_Review_solution set ResultDesc='"+remark+"' where slnid='"+slnid+"';";
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
                sqlUpdate = "update [dbo].[Q_Review_Remarks] set remarks='" + remark+"' "+ str + "   where id='"+id+"';";
                sqlUpdate += "update Q_Review_solution set ResultDesc='" + remark + "' where slnid='" + slnid + "';";
            }
        }
        if (sqlInsert != "" || sqlUpdate != "")
        { DbHelperSQL.ExecuteSql(sqlInsert + sqlUpdate); }

        ShowFile();
    }
    //显示历史记录
    public void ShowFile()
    {
        string slnid = Request.QueryString["slnid"];
        string strSQL = @"  select * from ( select "+slnid+" as slnid, 100000000000 as id, cast('' as varchar(1000)) as remarks, cast('' as varchar(1000)) as file_path, "
                    + "cast('' as varchar(1000)) as file_name,'' as Assessments,'"+ this.txtEmpId.Value + "' as create_by_empid, '"+this.txtXingMing.Value+ "' as create_by_empname,getdate() as create_date  "
                    + " union select * from [Q_Review_Remarks] where slnid = '" + Request.QueryString["slnid"] + "' "
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

    /// <summary>
    /// 否决措施
    /// </summary>
    /// <param name="year"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod(true)]
    public static string Evaluate(string id, string assessments)
    {
        System.Text.StringBuilder result = new StringBuilder();
        result.Append(string.Format("update [dbo].[Q_Review_Remarks] set [Assessments]='{0}' where [Id]='{1}' ", assessments, id));
        result.Append(string.Format(";update [dbo].[Q_Review_solution] set [ConfirmDesc]='{0}' where slnid=(select slnid from Q_Review_Remarks where [Id]='{1}') ", assessments, id));
        int rows = DbHelperSQL.ExecuteSql(result.ToString());
        result.Clear();
        if (rows == 0)
        {
            result.Append("0");//失败
        }
        else
        {
            result.Append("1"); //成功
        }

        return result.ToString();
    }



}