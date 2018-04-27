<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowButtons.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
    <table cellpadding="0" cellspacing="1" border="0" width="100%">
        <tr>
            <td>
                名称：<input type="text" class="mytext" style="width:190px;" id="Name" name="Name" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button1_Click" />
                <input type="button" onclick="add(); return false;" value="添加按钮" class="mybutton" />
                <asp:Button ID="Button2" runat="server" Text="删除所选" CssClass="mybutton" OnClientClick="return del();" OnClick="Button2_Click"/>
            </td>
        </tr>
    </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="30"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th>按钮名称</th>
                <th>按钮图标</th>
                <th>按钮说明</th>
                <th>操作</th>
            </tr>
        </thead>
        <tbody>
        <% 
        foreach (var button in workFlowButtonsList.OrderBy(p => p.Title))
        {%>
            <tr>
                <td><input type="checkbox" value="<%=button.ID %>" name="checkbox_app" style="vertical-align:middle;" /></td>
                <td><%=button.Title %></td>
                <td align="center">
                    <%
                    if (!button.Ico.IsNullOrEmpty())
                    {
                        if (button.Ico.IsFontIco())
                        {
                            Response.Write("<i class='fa " + button.Ico + "' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i>");
                        }
                        else
                        {
                            Response.Write("<img src='" + WebForm.Common.Tools.BaseUrl + button.Ico + "' style='vertical-align:middle;margin-right:3px;'/>");
                        }
                    }                
                    %>
                </td>
                <td><%=button.Note %></td>
                <td>
                    <a class="editlink" href="javascript:edit('<%=button.ID %>');">编辑</a>
                </td>
            </tr>
        <% }%>
        </tbody>
    </table>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        
        function add()
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "添加按钮", width: 700, height: 420, url: '/Platform/WorkFlowButtons/Edit.aspx?1=1' + '<%=Query1%>', opener:window, openerid: iframeid });
        }
        function edit(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑按钮", width: 700, height: 420, url: '/Platform/WorkFlowButtons/Edit.aspx?id=' + id + '<%=Query1%>', opener: window, openerid: iframeid });
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
            return confirm('您真的要删除所选按钮吗?');
        }
    </script>
    </form>
</body>
</html>
