<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.DBConnection.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:0;">
        <a href="javascript:void(0);" onclick="add();return false;"><span style="background-image:url(../../Images/ico/folder_classic_stuffed_add.png);">添加连接</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return del();" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/folder_classic_stuffed_remove.png);">删除所选</span></asp:LinkButton>
    </div>
    <table class="listtable" style="table-layout:fixed;WORD-BREAK:break-all;WORD-WRAP:break-word">
        <thead>
            <tr>
                <th width="3%"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th width="15%">连接名称</th>
                <th width="10%">连接类型</th>
                <th width="42%">连接字符串</th>
                <th width="15%">备注</th>
                <th width="15%">操作</th>
            </tr>
        </thead>
        <tbody>
        <% 
        foreach (var conn in ConnList)
        {%>
            <tr>
                <td><input type="checkbox" value="<%=conn.ID %>" name="checkbox_app" /></td>
                <td><%=conn.Name %></td>
                <td><%=conn.Type %></td>
                <td style="word-break:break-all; word-wrap:break-word;"><%=conn.ConnectionString %></td>
                <td><%=conn.Note %></td>
                <td>
                    <a class="editlink" href="javascript:edit('<%=conn.ID %>');">编辑</a>
                    <a onclick="test('<%=conn.ID %>');" style="background:url(../../Images/ico/hammer_screwdriver.png) no-repeat left center; padding-left:18px; margin-left:5px;" href="javascript:void(0);">测试</a>
                    <a onclick="table('<%=conn.ID %>','<%=conn.Name %>');" style="background:url(../../Images/ico/topic_search.gif) no-repeat left center; padding-left:18px; margin-left:5px;" href="javascript:void(0);">管理表</a>
                </td>
            </tr>
        <% }%>
        </tbody>
    </table>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["iframeid1"].IsNullOrEmpty()?Request.QueryString["tabid"]:Request.QueryString["iframeid1"]%>';
        var dialog = top.mainDialog;
        function table(connid, name)
        {
            var url = '/Platform/DBConnection/Table.aspx?connid=' + connid + '<%=Query1 %>';
            top.mainTab.addTab({ id: "tab_" + connid.replaceAll('-', ''), title: "管理表-" + name, src: url });
        }
        function add()
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "添加连接", width: 700, height: 320, url: '/Platform/DBConnection/Edit.aspx?1=1' + '<%=Query1%>', opener:window, openerid: iframeid });
        }
        function edit(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑连接", width: 700, height: 320, url: '/Platform/DBConnection/Edit.aspx?id=' + id + '<%=Query1%>', opener: window, openerid: iframeid });
        }
        function test(id)
        {
            $.ajax({
                url: "Test.ashx?id=" + id + "&appid=" + appid, cache: false, async: false, success: function (txt)
                {
                    alert(txt);
                }
            });
        }
        function checkAll(checked)
        {
            $("input[name='checkbox_app']").prop("checked", checked);
        }
        function del()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert("您没有选择要删除的项!");
                return false;
            }
            if (!confirm('您真的要删除所选连接吗?'))
            {
                return false;
            }
            return true;
        }
    </script>
</form>
</body>
</html>
