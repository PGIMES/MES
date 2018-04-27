<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Applications.WeiXin.Agents.Default" %>

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
                    名称：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th>ID</th>
                <th>名称</th>
                
            </tr>
        </thead>
        <tbody>
            <%
                foreach(var agent in AgentList)
                {
            %>
            <tr>
                <td><%=agent.agentid %></td>
                <td><%=agent.name %></td>
                <!--
                <td style="width:100px;"><a class="editlink" href="javascript:void(0);" onclick="edit('<%=agent.agentid %>');return false;">设置</a></td>
                -->
            </tr>
            <%} %>
        </tbody>
    </table>
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["AppID"]%>';
        var dialog = top.mainDialog;
        function edit(id)
        {
            dialog.open({
                id: "window_" + appid.replaceAll('-', ''), title: "设置微信应用", width: 900, height: 550,
                url: '/Applications/WeiXin/Agents/Set.aspx?id=' + (id || "") + '<%=Query1%>'
            });
        }
    </script>
</body>
</html>
