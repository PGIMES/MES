using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Aspose.Cells;
using System.Text;

public partial class Fin_Fin_WG1_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BaseFun fun = new BaseFun();
            //初始化年份    

            //int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            //for (int i = 0; i < 5; i++)
            //{
            //    dropYear.Items.Add(new ListItem((year + i).ToString(), (year + i).ToString()));
            //}
            for (int i = 1; i < 13; i++)
            {
                dropMnth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            dropMnth.SelectedValue = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 2;
            int chaYear = intYear - 2017;
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (2017 + i).ToString();
            }
            dropYear.DataSource = Yearlist;
            dropYear.DataBind();
            dropYear.SelectedValue = Year;

            Div_fp.Style.Add("display", "none");
            
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Div_fp.Style.Add("display", "none");
        DataSet ds = GetDS();
        GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView1.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView1.Style.Add("word-break", "keep-all");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GridViewRow hr = GridView1.HeaderRow;
           
                for (int i = 7; i <= e.Row.Cells.Count-1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                if (Server.HtmlDecode(e.Row.Cells[5].Text).Trim() == "非物料号的发票差异")
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnA" ;
                    lbtn.Text = e.Row.Cells[5].Text;
                    lbtn.Attributes.Add("href", @"javascript:void(0)','')");
                    lbtn.Attributes.Add("name", "A");
                    lbtn.Attributes.Add("value", dropMnth.SelectedValue);//月分
                    lbtn.Attributes.Add("comp", dropcomp.SelectedValue); //1:报价次数-- 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率
                    e.Row.Cells[5].Controls.Add(lbtn);
                    e.Row.Cells[5].Attributes.Add("allowClick", "true");//添加特殊属性表示可以点击
                }
            
        }
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Div_fp.Style.Add("display", "block");
        GridView1.Visible = false;
    }
    protected DataSet GetDS()
    {
        DataSet ds;
        ds = DbHelperSQL.Query("exec WG1_ReportQuery '" + dropYear.SelectedValue + "','" + dropMnth.SelectedValue + "','" + dropcomp.SelectedValue + "'");
        return ds;
    }
    public static string savepath = "\\UploadFile\\Fin";
    protected void Button2_Click(object sender, EventArgs e)
    {
        string path = "";
        string year = "";
        string mnth = "";
         var fileName = this.File1.PostedFile.FileName;
        if (string.IsNullOrWhiteSpace(fileName))  
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('请选择Excel文件上传！')", true);
            return;
        }

       else
        {
            string filename = File1.PostedFile.FileName;
            if (filename.Contains("\\") == true)
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            
            string MapDir = MapPath("~");
             path = MapDir + savepath + "\\" ;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录 
            }
            File1.PostedFile.SaveAs(path + "\\" + filename);
            path = MapDir + savepath + "\\" + filename;
            
           
        }
       


      
            Workbook workbook = new Workbook();
            workbook.Open(path);
            int rows=0;
            string Sql = "";
            Sql = "truncate table Fin_WG_JKFY_FPJG_ByMnth ";
            int delrows = DbHelperSQL.ExecuteSql(Sql.ToString());
            Cells cells = workbook.Worksheets[0].Cells;
            System.Collections.Generic.List<CommandInfo> list = new List<CommandInfo>();
            for (int i = 1; i < cells.MaxDataRow + 1; i++)
            {

                 year = cells[i, 0].StringValue.Trim();
                 mnth = cells[i, 1].StringValue.Trim();
                string comp = cells[i, 2].StringValue.Trim();
                string part = cells[i, 3].StringValue.Trim();
                float jkfy = float.Parse(cells[i, 4].StringValue.Trim());
                float Fpjg = float.Parse(cells[i, 5].StringValue.Trim());



                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Fin_WG_JKFY_FPJG_ByMnth(");
                strSql.Append("year,Mnth,Comp,Part,jkfy,fpjg)");
                strSql.Append(" values (");
                strSql.Append("@year,@Mnth,@Comp,@Part,@jkfy,@Fpjg)");
                SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.NChar,4),
                    new SqlParameter("@mnth", SqlDbType.NChar,4),
                    new SqlParameter("@comp", SqlDbType.NChar,4),
                    new SqlParameter("@part", SqlDbType.NChar,10),
                    new SqlParameter("@jkfy", SqlDbType.Float,20),
                    new SqlParameter("@Fpjg", SqlDbType.Float,20),
                                            };
                parameters[0].Value =year;
                parameters[1].Value = mnth;
                parameters[2].Value = comp;
                parameters[3].Value = part;
                parameters[4].Value = jkfy;
                parameters[5].Value = Fpjg;
                CommandInfo command = new CommandInfo();
                command.CommandText = strSql.ToString();
                command.Parameters = parameters;
                list.Add(command);
            }




            list.Add(new CommandInfo() { CommandText = "exec Update_WG1_Import", Parameters = null });
            rows = DbHelperSQL.ExecuteSqlTran(list);
            if (rows >= 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('上传成功！')", true);
            }
        
        
    }
    protected void export_Click(object sender, EventArgs e)
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        string lsname = "完工报表一数据";
        DataSet ds = GetDS();
        YangjianSQLHelp.DataTableToExcel(ds.Tables[0], "xls", lsname, "1");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = GetDS();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }
}