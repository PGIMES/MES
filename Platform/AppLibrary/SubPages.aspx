<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubPages.aspx.cs" Inherits="WebForm.Platform.AppLibrary.SubPages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="toolbar" style="margin-top:0; border-top:0;">
        <a href="javascript:void(0);" onclick="add('');return false;"><span style="background-image:url(../../Images/ico/application_osx_add.png);">添加页面</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return del();" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/application_osx_remove.png);">删除页面</span></asp:LinkButton>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="new RoadUI.Window().close();return false;"><span style="background-image:url(../../Images/ico/cancel.gif);">关闭窗口</span></a>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%" style="text-align:center;"><input type="checkbox" onclick="$('input[name=\'subpagesbox\']').prop('checked', this.checked);" style="vertical-align:middle;" /></th>
                <th width="25%">名称</th>
                <th width="25%">地址</th>
                <th width="25%">按钮</th>
                <th width="10%"></th>
            </tr>
        </thead>
        <tbody>
            <% 
            var subpages = new RoadFlow.Platform.AppLibrarySubPages().GetAllByAppID(Request.QueryString["id"].ToGuid());
            foreach(var page in subpages)
            {
            %>
            <tr>
                <td style="text-align:center;"><input type="checkbox" name="subpagesbox" value="<%=page.ID %>" /></td>
                <td>
                    <span style="vertical-align:middle;"><%=page.Name %></span>
                </td>
                <td><%=page.Address %></td>
                <td></td>
                <td><a class="editlink" href="javascript:;" onclick="add('<%=page.ID %>');">编辑</a></td>
            </tr>
            <%}%>
        </tbody>
    </table>
    </form>
    <script type="text/javascript">
        function add(id)
        {
            window.location = "SubPageEdit.aspx?subid=" + (id || "") + "<%=query%>";
        }
        function del()
        {
            if ($(":checked[name='subpagesbox']").size() == 0)
            {
                alert('您没有选择要删除的子页面!');
                return false;
            }
            return confirm('真的要删除所选子页面吗?');
        }
    </script>
</body>
</html>
