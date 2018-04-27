<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowComments.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:0;">
        <a href="javascript:void(0);" onclick="add();return false;"><span style="background-image:url(../../Images/ico/forum_add.gif);">添加意见</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" OnClientClick="return del();" runat="server" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/forum_del.gif);">删除所选</span></asp:LinkButton>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th width="36%">意见内容</th>
                <%if(!isOneSelf){%>
                <th width="30%">所属成员</th>
                <th width="10%">添加类型</th>
                <%}%>
                <th width="8%">排序</th>
                <th width="8%" sort="0">编辑</th>
            </tr>
        </thead>
        <tbody>
        <% 
            foreach (var comment in workFlowCommentList.OrderBy(p => p.Type).ThenBy(p => p.Sort)){ %>
            <tr>
                <td style="padding-left:4px;"><input type="checkbox" value="<%=comment.ID %>" name="checkbox_app" /></td>
                <td><%=comment.Comment %></td>
                <%if(!isOneSelf){ %>
                <td><%=comment.MemberID.IsNullOrEmpty() ? "所有人员" : borganize.GetNames(comment.MemberID) %></td>
                <td><%=comment.Type==0?"管理员":"个人" %></td>
                <%}%>
                <td><%=comment.Sort%></td>
                <td><a class="editlink" href="javascript:edit('<%=comment.ID%>');">编辑</a></td>
            </tr>
        <%}%>
        </tbody>
    </table>
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        function add()
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "添加意见", width: 700, height: 260, url: '/Platform/WorkFlowComments/Edit.aspx?1=1' + '<%=query1%>', opener:window, openerid: iframeid });
        }
        function edit(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑意见", width: 700, height: 260, url: '/Platform/WorkFlowComments/Edit.aspx?id=' + id + '<%=query1%>', opener: window, openerid: iframeid });
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
            if (!confirm('您真的要删除所选意见吗?'))
            {
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
