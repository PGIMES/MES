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

public partial class Fin_Fin_WG2_Report : System.Web.UI.Page
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

    protected DataSet GetDS()
    {
        DataSet ds;
        ds = DbHelperSQL.Query("exec WG2_ReportQuery_detail '" + dropYear.SelectedValue + "','" + dropMnth.SelectedValue + "','" + dropcomp.SelectedValue + "'");
        return ds;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Div_fp.Style.Add("display", "none");
        DataSet ds = GetDS();
        GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView1.Visible = true;
    }
    protected void export_Click(object sender, EventArgs e)
    {
        YangjianSQLHelp YangjianSQLHelp = new YangjianSQLHelp();
        string lsname = "完工报表二数据";
        DataSet ds = GetDS();
        YangjianSQLHelp.DataTableToExcel(ds.Tables[0], "xls", lsname, "1");
    }
    public static string savepath = "\\UploadFile\\Fin";
    protected void Button2_Click(object sender, EventArgs e)
    {
        string path = "";
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
            path = MapDir + savepath + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录 
            }
            File1.PostedFile.SaveAs(path + "\\" + filename);
            path = MapDir + savepath + "\\" + filename;


        }




        Workbook workbook = new Workbook();
        workbook.Open(path);
        int rows = 0;
        Cells cells = workbook.Worksheets[0].Cells;
        string Sql ="";
        Sql="truncate table WG2_Import ";
        int delrows = DbHelperSQL.ExecuteSql(Sql.ToString());
       // System.Collections.Hashtable htCommand = new System.Collections.Hashtable();
        System.Collections.Generic.List<CommandInfo> list = new List<CommandInfo>();
        
        for (int i = 1; i < cells.MaxDataRow + 1; i++)
        {

            string year = cells[i, 0].StringValue.Trim();
            string mnth = cells[i, 1].StringValue.Trim();
            string comp = cells[i, 2].StringValue.Trim();
            string part = cells[i, 3].StringValue.Trim();
            float sj_dj = float.Parse(cells[i, 4].StringValue.Trim());
            float bz_dj = float.Parse(cells[i, 5].StringValue.Trim());
            float sj_moju = cells[i, 6].StringValue.Trim()=="" ? 0 : float.Parse(cells[i, 6].StringValue.Trim());
            float bz_moju = cells[i, 7].StringValue.Trim()=="" ? 0 : float.Parse(cells[i, 7].StringValue.Trim());

           

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WG2_Import(");
            strSql.Append("year,Mnth,Comp,Part,sj_dj,bz_dj,sj_moju,bz_moju)");
            strSql.Append(" values (");
            strSql.Append("@year,@Mnth,@Comp,@Part,@sj_dj,@bz_dj,@sj_moju,@bz_moju)");
           
            SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.NChar,4),
                    new SqlParameter("@mnth", SqlDbType.NChar,4),
                    new SqlParameter("@comp", SqlDbType.NChar,4),
                    new SqlParameter("@part", SqlDbType.NChar,10),
                    new SqlParameter("@sj_dj", SqlDbType.Float,20),
                    new SqlParameter("@bz_dj", SqlDbType.Float,20),
                    new SqlParameter("@sj_moju", SqlDbType.Float,20),
                    new SqlParameter("@bz_moju", SqlDbType.Float,20),
                    
                                            };
            parameters[0].Value = year;
            parameters[1].Value = mnth;
            parameters[2].Value = comp;
            parameters[3].Value = part;
            parameters[4].Value = sj_dj;
            parameters[5].Value = bz_dj;
            parameters[6].Value = sj_moju;
            parameters[7].Value = bz_moju;

            CommandInfo command = new CommandInfo();
            command.CommandText = strSql.ToString();
            command.Parameters = parameters;
            list.Add(command);
          //  htCommand.Add(strSql.ToString(), parameters);
            //rows = DbHelperSQL.ExecuteSqlTran(strSql.ToString(), parameters);
            
           
        }
        list.Add(new CommandInfo() { CommandText = "exec Update_WG2_Import", Parameters = null });
        rows= DbHelperSQL.ExecuteSqlTran(list);
       // DbHelperSQL.ExecuteSql("exec Update_WG2_Import");
        if (rows >=1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "layer.alert('上传成功！')", true);
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DataSet ds = GetDS();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
           
             e.Row.Cells[4].Wrap = false;
             e.Row.Cells[5].Wrap = false;
             GridView1.Style.Add("word-break", "keep-all");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Div_fp.Style.Add("display", "block");
        GridView1.Visible = false;
    }
}