<%@ Page Language="C#" %>

<% 
    string tablename = @"AppLibrary,AppLibraryButtons,AppLibraryButtons1,AppLibrarySubPages,Dictionary,
DocumentDirectory,Documents,DocumentsReadUsers,HomeItems,Menu,MenuUser,Organize,ProgramBuilder,
ProgramBuilderButtons,ProgramBuilderFields,ProgramBuilderQuerys,ProgramBuilderValidates,UserShortcut,
SMSLog,Users,UsersInfo,UsersRelation,WorkCalendar,WorkFlow,WorkFlowArchives,WorkFlowButtons,WorkFlowComment,
WorkFlowDelegation,WorkFlowForm,WorkFlowTask,WorkGroup";

    foreach (string table in tablename.Split(','))
    {
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnection"].ConnectionString);
        conn.Open();
        System.Data.SqlClient.SqlDataAdapter dap1 = new System.Data.SqlClient.SqlDataAdapter("select * from " + table, conn);

        System.Data.DataTable dt = new System.Data.DataTable();
        dap1.Fill(dt);
        dap1.Dispose();


        Oracle.ManagedDataAccess.Client.OracleConnection conn1 = new Oracle.ManagedDataAccess.Client.OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnectionOracle"].ConnectionString);

        conn1.Open();
        Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("truncate table " + table, conn1);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        
        Oracle.ManagedDataAccess.Client.OracleDataAdapter dap = new Oracle.ManagedDataAccess.Client.OracleDataAdapter("select * from " + table, conn1);

        System.Data.DataSet ds = new System.Data.DataSet();
        dap.Fill(ds);
        ds.Tables[0].Clear();
        foreach (System.Data.DataRow dr in dt.Rows)
        {
            System.Data.DataRow dr1 = ds.Tables[0].NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr1[i] = dr[i];
            }
            ds.Tables[0].Rows.Add(dr1);
        }
        Oracle.ManagedDataAccess.Client.OracleCommandBuilder ocb = new Oracle.ManagedDataAccess.Client.OracleCommandBuilder(dap);
        //ds.AcceptChanges();
        dap.Update(ds);
        dap.Dispose();
        ocb.Dispose();
        conn1.Close();

        conn.Close();
    }

    Response.Write("完成"); 
    
%>