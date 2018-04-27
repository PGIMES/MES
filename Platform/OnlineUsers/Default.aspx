<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.OnlineUsers.Default" %>

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
            <td style="width:60%">
                姓名：<input type="text" class="mytext" id="Name" name="Name" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="清除所有用户" OnClientClick="return clearAll();" CssClass="mybutton" OnClick="Button2_Click"/>
                <asp:Button ID="Button3" runat="server" Text="清除选择用户" OnClientClick="return clearSelect();" CssClass="mybutton" OnClick="Button3_Click"/>
            </td>
            <td align="right" style="padding-right:15px;">
                <span>当前共有 <asp:Literal ID="Count" runat="server"></asp:Literal> 人在线</span>
            </td>
        </tr>
    </table>
</div>
<table class="listtable">
    <thead>
        <tr>
            <th width="3%" sort="0"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
            <th width="10%">姓名</th>
            <th width="30%">所在机构</th>
            <th width="15%">登录时间</th>
            <th width="10%">登录IP</th>
            <th width="32%">客户端信息</th>
        </tr>
    </thead>
    <tbody>
    <%
    foreach (var user in UserList)
    { %>
        <tr>
            <td><input type="checkbox" value="<%=user.ID %>" name="checkbox_app" /></td>
            <td><%=user.UserName %></td>
            <td><%=user.OrgName %></td>
            <td><%=user.LoginTime.ToDateTimeStringS() %></td>
            <td><%=user.IP %></td>
            <td><%=user.ClientInfo %></td>
        </tr>
    <%}%>
    </tbody>
</table>
<div class="buttondiv"></div>
</form>
<script type="text/javascript">
    function checkAll(checked)
    {
        $("input[name='checkbox_app']").prop("checked", checked);
    }
    function clearAll()
    {
        return confirm("您真的要清除所有在线用户吗?");
    }
    function clearSelect()
    {
        if ($(":checked[name='checkbox_app']").size() == 0)
        {
            alert("您没有选择要清除的用户!");
            return false;
        }
        return confirm('您真的要清除所选用户吗?');
    }
</script>
</body>
</html>
