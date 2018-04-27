<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Applications.Documents.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="querybar">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    发布时间：<input type="text" class="mycalendar" id="Date1" name="Date1" runat="server" style="width:80px" />
                    至 <input type="text" class="mycalendar" id="Date2" name="Date2" runat="server" style="width:80px" />
                    <asp:Button ID="Button_Query" runat="server" Text="&nbsp;查询&nbsp;" CssClass="mybutton" OnClick="Button_Query_Click" />
                    <%if(HasPublish){ %>
                    <input type="button" class="mybutton" onclick="window.location = 'DocAdd.aspx?1=1<%=Query%>';" value="添加文档" />
                    <asp:Button ID="Button1" runat="server" Text="&nbsp;删除文档&nbsp;" OnClientClick="return checkdel();" CssClass="mybutton" OnClick="Button1_Click"/>
                    <%} %>
                    <%if(HasManage){ %>
                    <input type="button" class="mybutton" value="编辑栏目" onclick="window.location='DirAdd.aspx?currentdirid=<%=DirID%><%=Query%>';"/>
                    <input type="button" class="mybutton" value="添加子栏目" onclick="window.location='DirAdd.aspx?1=1<%=Query%>';" />
                    <%} %>
                </td>
            </tr>
        </table>
        </div>
        <table class="listtable">
            <thead>
                <tr>
                    <th width="5%" style="text-align:center;">序号</th>
                    <th width="40%">标题</th>
                    <th width="15%">栏目</th>
                    <th width="10%">发布人</th>
                    <th width="15%">发布时间</th>
                    <th width="7%">阅读次数</th>
                   
                </tr>
            </thead>
            <tbody>
                <% 
                    int i=1;
                    foreach(System.Data.DataRow dr in DocDt.Rows)  
                    {
                %>
                <tr>
                    <td style="text-align:center;"><%=i++ %></td>
                    <td><a class="blue" href="javascript:;" onclick="showDoc('<%=dr["ID"] %>','<%=dr["Title"].ToString().Replace1("'","") %>');return false;"><%=dr["Title"] %></a>
                        <%if ("0" == dr["IsRead"].ToString()){ %>
                        <span><img src="../../Images/loading/new.png" style="border:0 none; vertical-align:middle;" /></span>
                        <%} %>
                    </td>
                    <td><%=dr["DirectoryName"] %></td>
                    <td><%=dr["WriteUserName"] %></td>
                    <td><%=dr["WriteTime"].ToString().ToDateTimeString() %></td>
                    <td><%=dr["ReadCount"] %></td>
                   
                </tr>
                <%} %>
            </tbody>
        </table>
        <div class="buttondiv">
            <asp:Literal ID="PagerText" runat="server"></asp:Literal>
        </div>
    </form>
    <script type="text/javascript">
        function showDoc(id, title)
        {
            window.location='DocRead.aspx?docid=' + id + "<%=Query1%>";
        }
        function checkdel()
        {
            if($(":checked[name='docbox']").size()==0)
            {
                alert('您没有选择要删除的文档!');
                return false;
            }
            return confirm('您真的删除所选的文档吗?');
        }
    </script>
</body>
</html>
