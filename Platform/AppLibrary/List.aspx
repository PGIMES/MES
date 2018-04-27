<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Platform.AppLibrary.List" %>

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
                    应用名称：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    应用地址：<input type="text" class="mytext" id="Address" name="Address" runat="server" style="width:220px" />
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                    <input type="button" onclick="edit(); return false;" value="添加应用" class="mybutton" />
                    <asp:Button ID="Button3" runat="server" Text="删除所选" CssClass="mybutton" OnClientClick="return del();" OnClick="Button3_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th width="20%">应用名称</th>
                <th width="47%">应用地址</th>
                <th width="15%">应用分类</th>
                <th width="15%">操作</th>
            </tr>
        </thead>
        <tbody>
        <%
        RoadFlow.Platform.Dictionary bdict = new RoadFlow.Platform.Dictionary();
        foreach (var app in AppList)
        {
        %>
            <tr>
                <td><input type="checkbox" value="<%=app.ID %>" name="checkbox_app"  /></td>
                <td>
                    <% 
                    if (!app.Ico.IsNullOrEmpty())
                    {
                        if (app.Ico.IsFontIco())
                        {
                            Response.Write("<i class='fa " + app.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>");
                        }
                        else
                        {
                            Response.Write("<img src='../.." + app.Ico + "' style='vertical-align:middle;margin-right:3px;'/>");
                        }
                    }
                    %>
                    <%=app.Title %></td>
                <td><%=app.Address %></td>
                <td><%=bdict.GetTitle(app.Type) %></td>
                <td><a class="editlink" href="javascript:void(0);" onclick="edit('<%=app.ID %>');return false;">编辑</a>
                    <a class="editlink" href="javascript:void(0);" onclick="editsubpage('<%=app.ID %>');return false;">子页面</a>
                </td>
            </tr>

        <%}%>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["AppID"]%>';
        var iframeid = '<%=Request.QueryString["TabID"]%>';
        var typeid = '<%=Request.QueryString["TypeID"]%>';
        var dialog = top.mainDialog;
       
        function edit(id)
        {
            dialog.open({
                id: "window_" + appid.replaceAll('-', ''), title: (id ? "编辑" : "添加") + "应用程序", width: 900, height: 450,
                url: '/Platform/AppLibrary/Edit.aspx?id=' + (id || "") + '<%=Query1%>', opener: parent, openerid: iframeid
            });
        }
        function editsubpage(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑子页面", width: 900, height: 450, url: '/Platform/AppLibrary/SubPages.aspx?id=' + id + '<%=Query1%>', opener: parent, openerid: iframeid });
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
            return confirm('您真的要删除所选应用吗?');
        }
    </script>
</body>
</html>
