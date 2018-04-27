using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Maticsoft.DBUtility;
namespace MES
{
   public class MoJu_BX
    {
        #region 模具报修查询
        public static string connString = ConfigurationSettings.AppSettings["connstringMoJu"].ToString();

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


        public static System.Data.DataSet MoJuBXQuery(string lsmojuno, string lspn, string lstype, string lsstatus, string lsweizhi, string lsyzj_no, string lsbx_date1, string lsbx_date2, string lsbx_user, string lsbx_id, string lsbx_result,string lsbx_remark)
        {


            SqlParameter[] parms = GetMoJuBXQueryParameters();
            parms[0].Value = "%"+lsmojuno+"%";
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
            string lssql = "select moju_bx.*,round((cast(datediff(MINUTE,bx_date,confirm_date) as float)/60),1) as wxsc,CONVERT(varchar(10), bx_date, 24) as bx_time";
            lssql += ",CONVERT(varchar(10), confirm_date, 24) as confirm_time,moju.mojuno,moju.pn,moju.mojuno_no,moju.type ";
            lssql+=" ,(select top 1 by_user from moju_by where moju_id=moju_bx.moju_id and by_user is not null order by moju_by.by_id desc) as by_user";
            lssql += " from moju_bx left join moju on moju_bx.moju_id=moju.id where moju.flag=1 ";
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

            if (lsbx_result != "ALL" && lsbx_id=="")
            {
                if (lsbx_result=="")
                {
                    lssql += " and (moju_bx.bx_result is null or moju_bx.bx_result='')";
                }
                else
                {
                    lssql += " and moju_bx.bx_result=@bx_result";
                }
                
            }
            if (lsbx_remark!="")
            {
                lssql += " and moju_bx.bx_remark like @bx_remark";
            }

            lssql += " order by moju_bx.bx_date desc";


            DataSet ds = Query(lssql);
           

             return ds;
            

        }

        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
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
            }
        }

        //public static System.Data.DataSet MoJuQuery(string lsmojuno, string lspn, string lstype, string lsstatus, string lsweizhi, string lsyzj_no, string lsbx_date1, string lsbx_date2, string lsbx_user, string lsbx_id,string lsbx_remark)
        //{


        //    SqlParameter[] parms = GetMoJuBXQueryParameters();
        //    parms[0].Value = "%" + lsmojuno + "%";
        //    parms[1].Value = "%" + lspn + "%";
        //    parms[2].Value = lstype;
        //    parms[3].Value = lsstatus;
        //    parms[4].Value = lsweizhi;
        //    parms[5].Value = "%" + lsyzj_no + "%";
        //    parms[6].Value = lsbx_date1;

        //    if (lsbx_date2.ToString() != "")
        //    {
        //        parms[7].Value = Convert.ToDateTime(lsbx_date2).AddDays(1).ToShortDateString();
        //    }
        //    else
        //    {
        //        parms[7].Value = lsbx_date2;
        //    }
        //    parms[8].Value = lsbx_user;
        //    parms[9].Value = lsbx_id;
        //    parms[10].Value = "";
        //    parms[11].Value = lsbx_remark;

        //    string connstring = ConfigurationSettings.AppSettings["connstring"];
        //    string lssql = "select * ";
        //    lssql += ",(select top 1 DATEDIFF(day,ly_date,GETDATE()) from MoJu_LY where moju_id=moju.id order by ly_id desc) as ly_day";
        //    lssql += " from moju  where flag=1";
        //    if (lsmojuno != "")
        //    {
        //        lssql += " and mojuno like @mojuno";
        //    }
        //    if (lspn != "")
        //    {
        //        lssql += " and pn like @pn";
        //    }
        //    if (parms[2].Value.ToString() != "")
        //    {
        //        lssql += " and type=@type";
        //    }
        //    if (parms[3].Value.ToString() != "")
        //    {
        //        lssql += " and status=@status";
        //    }
        //    if (parms[4].Value.ToString() != "")
        //    {
        //        lssql += " and weizhi=@weizhi";
        //    }

        //    if (lsyzj_no != "")
        //    {
        //        lssql += " and yzj_no like @yzj_no";
        //    }
        //    //if (parms[6].Value.ToString() != "" && parms[7].Value.ToString() != "")
        //    //{
        //    //    lssql += " and ly_date>=@ly_date1 and ly_date<@ly_date2";
        //    //}
        //    //if (parms[8].Value.ToString() != "")
        //    //{
        //    //    lssql += " and muju_ly.ly_user=@ly_user";
        //    //}
        //    //if (parms[9].Value.ToString() != "")
        //    //{
        //    //    lssql += " and muju_ly.ly_id=@ly_id";
        //    //}

        //    lssql += " order by yzj_no";
        //    using (DataSet ds = SQLHelper.ExecuteDataSet(connstring, CommandType.Text, lssql, parms))
        //    {

        //        return ds;
        //    }

        //}

        //#endregion


        //#region 模具报修新增

        //private static SqlParameter[] GetMoJuBXInsertParameters()
        //{
        //    SqlParameter[] parms = new SqlParameter[] {   
											  
										  
        //                                   new SqlParameter("@moju_id", SqlDbType.VarChar),
        //                                   new SqlParameter("@yzj_no", SqlDbType.VarChar),
        //                                   new SqlParameter("@bx_user", SqlDbType.VarChar),
        //                                    new SqlParameter("@bx_date", SqlDbType.VarChar),
        //                                    new SqlParameter("@bx_remark", SqlDbType.VarChar),
        //                                     new SqlParameter("@create_by", SqlDbType.VarChar),
        //                                      new SqlParameter("@create_date", SqlDbType.VarChar),
        //                                      new SqlParameter("@bx_no", SqlDbType.VarChar),
        //                                      new SqlParameter("@bx_moci", SqlDbType.VarChar),
        //                                      new SqlParameter("@bx_type", SqlDbType.VarChar),
                                           
        //    };


        //    return parms;

        //}



        //public static int MoJuBXInsert( string lsmoju_id, string lsyzj_no, string lsbx_user,string lsbx_date, string lsbx_remark, string lscreate_by,string lsbx_no,string lsbx_moci,string lsbx_type)
        //{
        //    int ln=0;
        //    SqlParameter[] parms = GetMoJuBXInsertParameters();
           
        //    parms[0].Value = lsmoju_id;
        //    parms[1].Value = lsyzj_no;
        //    parms[2].Value = lsbx_user;
        //    parms[3].Value = lsbx_date;
        //    parms[4].Value = lsbx_remark;
        //    parms[5].Value = lscreate_by;
        //    parms[6].Value = System.DateTime.Now.ToString();
        //    parms[7].Value = lsbx_no;
        //    parms[8].Value = lsbx_moci;
        //    parms[9].Value = lsbx_type;

        //    string lssql = "insert into moju_bx(moju_id,yzj_no,bx_user,bx_date,bx_remark,create_by,create_date,bx_no,bx_moci,bx_type) values(";
        //    lssql += "@moju_id,@yzj_no,@bx_user,@bx_date,@bx_remark,@create_by,@create_date,@bx_no,@bx_moci,@bx_type)";

        //    //string lssql1 = "update moju set weizhi='生产',kw=NULL,yzj_no=@yzj_no where id=@id";

        //    string connstring = ConfigurationSettings.AppSettings["connstring"];
        //    SqlConnection conn = new SqlConnection(connstring);
        //    conn.Open();
        //    SqlTransaction trans = conn.BeginTransaction();
        //    try
        //    {
        //       // SQLHelper.ExecuteNonQuery(trans, CommandType.Text, lssql1, new SqlParameter[] { new SqlParameter("@id", lsmoju_id), new SqlParameter("@yzj_no",lsyzj_no) });
        //        ln = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, lssql, parms);
        //        trans.Commit();

        //    }
        //    catch (Exception)
        //    {
        //        trans.Rollback();
        //        throw;
        //    }
        //    return ln;
        //}
        
        //#endregion




        //#region 模具报修修改1


        //private static SqlParameter[] GetMoJuBXUpdateParameters1()
        //{
        //    SqlParameter[] parms = new SqlParameter[] {   
											  
        //                                  new SqlParameter("@bx_id", SqlDbType.VarChar),
        //                                   new SqlParameter("@moju_id", SqlDbType.VarChar),
        //                                   new SqlParameter("@yzj_no", SqlDbType.VarChar),
        //                                   new SqlParameter("@bx_user", SqlDbType.VarChar),
        //                                    new SqlParameter("@bx_date", SqlDbType.VarChar),
        //                                    new SqlParameter("@bx_remark", SqlDbType.VarChar),
        //                                     new SqlParameter("@update_by", SqlDbType.VarChar),
        //                                      new SqlParameter("@update_date", SqlDbType.VarChar),
        //    };


        //    return parms;

        //}



        //public static int MoJuBXUpdate1(string lsbx_id,string lsmoju_id, string lsyzj_no, string lsbx_user, string lsbx_date, string lsbx_remark, string lsupdate_by)
        //{
        //    int ln = 0;
        //    SqlParameter[] parms = GetMoJuBXUpdateParameters1();

        //    parms[0].Value = lsbx_id;
        //    parms[1].Value = lsmoju_id;
        //    parms[2].Value = lsyzj_no;
        //    parms[3].Value = lsbx_user;
        //    parms[4].Value = lsbx_date;
        //    parms[5].Value = lsbx_remark;
        //    parms[6].Value = lsupdate_by;
        //    parms[7].Value = System.DateTime.Now.ToString();

        //    string lssql = "update moju_bx set moju_id=@moju_id,yzj_no=@yzj_no,bx_user=@bx_user,bx_date=@bx_date,bx_remark=@bx_remark,update_by=@update_by,update_date=@update_date";
        //    lssql += " where bx_id=@bx_id";
        //    try
        //    {
        //        string connstring = ConfigurationSettings.AppSettings["connstring"];
        //        SqlConnection conn = new SqlConnection(connstring);
        //        conn.Open();
        //        ln = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, lssql, parms);
        //                   }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //    return ln;
              
        //}
        //#endregion


        //#region 模具报修修改2


        //private static SqlParameter[] GetMoJuBXUpdateParameters2()
        //{
        //    SqlParameter[] parms = new SqlParameter[] {   
											  
        //                                  new SqlParameter("@bx_id", SqlDbType.VarChar),
        //                                   new SqlParameter("@bx_result", SqlDbType.VarChar),
        //                                   new SqlParameter("@result_remark", SqlDbType.VarChar),
        //                                   new SqlParameter("@result_yyfx", SqlDbType.VarChar),
        //                                   new SqlParameter("@repair_user", SqlDbType.VarChar),
        //                                     new SqlParameter("@update_by", SqlDbType.VarChar),
        //                                      new SqlParameter("@update_date", SqlDbType.VarChar),
        //                                       new SqlParameter("@finish_date", SqlDbType.VarChar),
        //    };


        //    return parms;

        //}



        //public static int MoJuBXUpdate2(string lsbx_id, string lsbx_result, string lsresult_remark,string lsresult_yyfx, string lsrepair_user, string lsupdate_by,string lsfinish_date)
        //{
        //    int ln = 0;
        //    SqlParameter[] parms = GetMoJuBXUpdateParameters2();

        //    parms[0].Value = lsbx_id;
        //    parms[1].Value =lsbx_result;
        //    parms[2].Value = lsresult_remark;
        //    parms[3].Value = lsresult_yyfx;
        //    parms[4].Value = lsrepair_user;
        //    parms[5].Value = lsupdate_by;
        //    parms[6].Value = System.DateTime.Now.ToString();
        //    if (lsfinish_date.ToString().Trim()!="")
        //    {
        //        parms[7].Value = lsfinish_date;
        //    }
        //    else
        //    {
        //        parms[7].Value = System.DateTime.Now.ToString();
        //    }
           

        //    string lssql = "update moju_bx set bx_result=@bx_result,result_remark=@result_remark,result_yyfx=@result_yyfx,repair_user=@repair_user,update_by=@update_by,update_date=@update_date,finish_date=@finish_date";
        //    lssql += " where bx_id=@bx_id";
        //    try
        //    {
        //        string connstring = ConfigurationSettings.AppSettings["connstring"];
        //        SqlConnection conn = new SqlConnection(connstring);
        //        conn.Open();
        //        ln = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, lssql, parms);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ln;

        //}



        //private static SqlParameter[] GetMoJuBXUpdateParameters3()
        //{
        //    SqlParameter[] parms = new SqlParameter[] {   
											  
        //                                  new SqlParameter("@bx_id", SqlDbType.VarChar),
        //                                     new SqlParameter("@confirm_date", SqlDbType.VarChar),
        //                                      new SqlParameter("@repair_confirm", SqlDbType.VarChar),
        //                                       new SqlParameter("@confirm_user", SqlDbType.VarChar),
        //    };


        //    return parms;

        //}

        //public static int MoJuBXUpdate3(string lsbx_id, string lsrepair_confirm,string lsconfirm_user)
        //{
        //    int ln = 0;
        //    SqlParameter[] parms = GetMoJuBXUpdateParameters3();

        //    parms[0].Value = lsbx_id;
        //    parms[1].Value = System.DateTime.Now.ToString();
        //    parms[2].Value = lsrepair_confirm;
        //    parms[3].Value = lsconfirm_user;

        //    string lssql = "update moju_bx set confirm_date=@confirm_date,repair_confirm=@repair_confirm,confirm_user=@confirm_user";
        //    lssql += " where bx_id=@bx_id";
        //    try
        //    {
        //        string connstring = ConfigurationSettings.AppSettings["connstring"];
        //        SqlConnection conn = new SqlConnection(connstring);
        //        conn.Open();
        //        ln = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, lssql, parms);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ln;

        //}
        //#endregion


        //#region 模具报修修改4

        //public static int MoJuBXUpdate4(string lsbx_id,string lsyzj_no,string lsbx_user,string lsbx_remark,string lsupdate_by,string lsbx_type)
        //{
        //    int ln = 0;
           

        //    string lssql = "update moju_bx set yzj_no=@yzj_no,bx_user=@bx_user,bx_remark=@bx_remark,update_by=@update_by,update_date=getdate(),bx_type=@bx_type";
        //    lssql += " where bx_id=@bx_id";
        //    try
        //    {
        //        string connstring = ConfigurationSettings.AppSettings["connstring"];
        //        SqlConnection conn = new SqlConnection(connstring);
        //        conn.Open();
        //        ln = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, lssql, 
        //            new SqlParameter[]{
                    
        //            new SqlParameter("@bx_id",lsbx_id)
        //            ,new SqlParameter("@yzj_no",lsyzj_no)
        //            ,new SqlParameter("@bx_user",lsbx_user)
        //           , new SqlParameter("@bx_remark",lsbx_remark)
        //          ,new SqlParameter("@update_by",lsupdate_by)
        //          ,new SqlParameter("@bx_type",lsbx_type)
        //            }
                    
        //            );
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ln;

        //}
        //#endregion


        //#region 模具报修删除

        //public static int MoJuBXDelete(string lsbx_id)
        //{
        //    int ln = 0;


        //    string lssql = "delete from moju_bx  where bx_id=@bx_id";
            
        //    try
        //    {
        //        string connstring = ConfigurationSettings.AppSettings["connstring"];
        //        SqlConnection conn = new SqlConnection(connstring);
        //        conn.Open();
        //        ln = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, lssql,new SqlParameter("@bx_id",lsbx_id));
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ln;

        //}

        #endregion
    }
      
}
