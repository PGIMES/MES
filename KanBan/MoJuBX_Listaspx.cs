using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

public partial class mojuBX_List : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
        showMoJuBX();

    }
    protected void showMoJuBX( )
    {
        this.GridView2.PageSize = 100;
        this.GridView2.Visible = true;        
        this.GridView2.DataSource =   MoJuBXQuery("", "", "", "", "", "", "", "","", "", "","");
        this.GridView2.DataBind();
    }

   
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    if (e.Row.RowIndex != -1)
        //    {
        //        int indexID = this.GridView2.PageIndex * this.GridView2.PageSize + e.Row.RowIndex + 1;
        //        e.Row.Cells[2].Text = indexID.ToString();
        //    }
        //    Image img = e.Row.FindControl("img") as Image;

        //    if (img != null)
        //    {

        //        img.Attributes.Add("onclick", "window.open(encodeURI('MoJu_BX_Repair.aspx?mainid=" + e.Row.Cells[1].Text.ToString() + "&mode=" + 2 + "'),'null','toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,top=50,left=300,width=900,height=450')");
        //    }

        //}
    }
    public static System.Data.DataSet MoJuBXQuery(string lsmojuno, string lspn, string lstype, string lsstatus, string lsweizhi, string lsyzj_no, string lsbx_date1, string lsbx_date2, string lsbx_user, string lsbx_id, string lsbx_result, string lsbx_remark)
    {
      //  string connString = ConfigurationSettings.AppSettings["connstringMoJu"];
       // static string connString = ConfigurationManager.AppSettings["connstringMoJu"];
        SqlParameter[] parms = GetMoJuBXQueryParameters();
        parms[0].Value = "%" + lsmojuno + "%";
        parms[1].Value = "%" + lspn + "%";
        parms[2].Value = lstype;
        parms[3].Value = lsstatus;
        parms[4].Value = lsweizhi;
        parms[5].Value = "%" + lsyzj_no + "%";
        parms[6].Value = lsbx_date1;

        if (lsbx_date2.ToString() != "")
        {
            parms[7].Value = Convert.ToDateTime(lsbx_date2).AddDays(1).ToShortDateString();
        }
        else
        {
            parms[7].Value = lsbx_date2;
        }
        parms[8].Value = lsbx_user;
        parms[9].Value = lsbx_id;
        parms[10].Value = lsbx_result;
        parms[11].Value = "%" + lsbx_remark + "%";

        // string connstring = ConfigurationSettings.AppSettings["connstringMoJu"];
        string lssql = "select moju_bx.*,round((cast(datediff(MINUTE,bx_date,getdate()) as float)/60),1) as wxsc,CONVERT(varchar(10), bx_date, 24) as bx_time";
        lssql += ",CONVERT(varchar(10), confirm_date, 24) as confirm_time,moju.mojuno,moju.pn,moju.mojuno_no,moju.type ";
        lssql += " ,(select top 1 by_user from moju_by where moju_id=moju_bx.moju_id and by_user is not null order by moju_by.by_id desc) as by_user";
        lssql += " from moju_bx left join moju on moju_bx.moju_id=moju.id where moju.flag=1  and confirm_user is null ";
        if (lsmojuno != "")
        {
            lssql += " and moju.mojuno like @mojuno";
        }
        if (lspn != "")
        {
            lssql += " and moju.pn like @pn";
        }
        if (lstype != "")
        {
            lssql += " and moju.type=@type";
        }
        if (lsstatus != "")
        {
            lssql += " and moju.status=@status";
        }
        if (lsweizhi != "")
        {
            lssql += " and moju.weizhi=@weizhi";
        }

        if (lsyzj_no != "")
        {
            lssql += " and moju_bx.yzj_no like @yzj_no";
        }
        if (lsbx_date1 != "" && lsbx_date2 != "")
        {
            lssql += " and moju_bx.bx_date>=convert(varchar(10),@bx_date1,21) and moju_bx.bx_date<convert(varchar(10),@bx_date2,21)";
        }
        if (lsbx_user != "")
        {
            lssql += " and moju_bx.bx_user=@bx_user";
        }
        if (lsbx_id != "")
        {
            lssql += " and moju_bx.bx_id=@bx_id";
        }

        //if (lsbx_result != "ALL" && lsbx_id == "")
        //{
        //    if (lsbx_result == "")
        //    {
        //        lssql += " and (moju_bx.bx_result is null or moju_bx.bx_result='')";
        //    }
        //    else
        //    {
        //        lssql += " and moju_bx.bx_result=@bx_result";
        //    }

        //}
        if (lsbx_remark != "")
        {
            lssql += " and moju_bx.bx_remark like @bx_remark";
        }

        lssql += " order by moju_bx.bx_date desc";
        
        DataSet ds = Query(lssql);
        return ds;
    }

    public static DataSet Query(string SQLString)
    {
         string connString = ConfigurationManager.AppSettings["connstringMoJu"];
         SqlConnection connection = new SqlConnection(connString);
         DataSet ds = new DataSet();
        try
        {
            connection.Open();
            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
            command.Fill(ds, "ds");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
         
        //using (SqlConnection connection = new SqlConnection(connString))
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        connection.Open();
        //        SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
        //        command.Fill(ds, "ds");
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return ds;
        //}
    }
    private static SqlParameter[] GetMoJuBXQueryParameters()
    {
        SqlParameter[] parms = new SqlParameter[] {   
											  

										    new SqlParameter("@mojuno", SqlDbType.VarChar ),
										   new SqlParameter("@pn", SqlDbType.VarChar),
										   new SqlParameter("@type", SqlDbType.VarChar),
                                           new SqlParameter("@status", SqlDbType.VarChar),
										   new SqlParameter("@weizhi", SqlDbType.VarChar),
                                            new SqlParameter("@yzj_no", SqlDbType.VarChar),
                                            new SqlParameter("@bx_date1", SqlDbType.VarChar),
                                             new SqlParameter("@bx_date2", SqlDbType.VarChar),
                                              new SqlParameter("@bx_user", SqlDbType.VarChar),
                                               new SqlParameter("@bx_id", SqlDbType.VarChar),
                                                new SqlParameter("@bx_result", SqlDbType.VarChar),
                                                new SqlParameter("@bx_remark", SqlDbType.VarChar),
			};


        return parms;

    }

}
