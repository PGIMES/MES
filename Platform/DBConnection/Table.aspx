<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Table.aspx.cs" EnableViewState="false" EnableViewStateMac="false" Inherits="WebForm.Platform.DBConnection.Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:30px;">
        名称：<input type="text" class="mytext" id="Name1" name="Name1" runat="server" style="width:300px" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Button2_Click"><span style="background:url(../../Images/ico/zoom.png) no-repeat left center;">搜索表</span></asp:LinkButton>
        <span class="toolbarsplit">&nbsp;</span>
        <%if("Oracle"!=DBType){ %>
        <a href="javascript:void(0);" onclick="edit('<%=dbconnID %>','')">
            <span style="background:url(../../Images/ico/table_add.gif) no-repeat left center;">新建表</span>
        </a>
        <%} %>
        <a href="javascript:void(0);" onclick="queryTable('<%=dbconnID %>','');">
            <span style="background:url(../../Images/ico/search.png) no-repeat left center;">新建查询</span>
        </a>
        
    </div>
    <div style="margin-top:36px;">
        <table class="listtable" style="WORD-BREAK:break-all;WORD-WRAP:break-word">
            <thead>
                <tr>
                    <th style="width:60px; text-align:center;">序号</th>
                    <th>名称</th>
                    <th style="width:100px;">类型</th>
                    <th style="width:250px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <% 
                    int i = 1;
                    foreach (var table in List)
                    {
                        bool isSystemTable = SystemTables.Find(p => p.Equals(table.Item1, StringComparison.CurrentCultureIgnoreCase)) != null;
                %>
                <tr>
                    <td style="text-align:center;"><%=i++ %></td>
                    <td><%=table.Item1 %></td>
                    <td><%=table.Item2==0?isSystemTable?"系统表":"表":"视图" %></td>
                    <td>
                        <a class="viewlink" href="javascript:void(0);" onclick="queryTable('<%=dbconnID %>','<%=table.Item1 %>');">查询</a>
                        <%if (!isSystemTable && table.Item2 == 0 && "Oracle" != DBType)
                          { %>
                        <a class="editlink" href="javascript:void(0);" onclick="edit('<%=dbconnID %>','<%=table.Item1 %>');">修改</a>&nbsp;&nbsp;
                        <a class="deletelink" href="javascript:void(0);" onclick="del('<%=dbconnID %>','<%=table.Item1 %>');">删除</a>&nbsp;&nbsp;
                        <%} %>
                    </td>
                </tr>
                <%  } %>
            </tbody>
        </table>
        <br /><br /><br /><br /><br />
    </div>
    </form>
    <script type="text/javascript">
        var query = "<%=Query%>";
        var dbtype = "<%=DBType.ToLower()%>";
        function edit(connid, table)
        {
            var url = "";
            switch(dbtype)
            {
                case "sqlserver":
                    url = 'TableEdit_SqlServer.aspx?dbconnid=' + connid + "&tablename=" + table + query;
                    break;
                case "oracle":
                    url = 'TableEdit_Oracle.aspx?dbconnid=' + connid + "&tablename=" + table + query;
                    break;
                case "mysql":
                    url = 'TableEdit_MySql.aspx?dbconnid=' + connid + "&tablename=" + table + query;
                    break;
            }
            window.location = url;
        }
        function del(connid, table)
        {
            if (confirm('您真的要删除该表吗?'))
            {
                window.location = 'TableDelete.aspx?dbconnid=' + connid + "&tablename=" + table + query;
            }
        }
        function queryTable(connid, table)
        {
            window.location = 'TableQuery.aspx?dbconnid=' + connid + "&tablename=" + table + query;
        }
    </script>
</body>
</html>
