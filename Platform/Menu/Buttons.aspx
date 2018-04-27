<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buttons.aspx.cs" Inherits="WebForm.Platform.Menu.Buttons" %>

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
                        名称：<input type="text" class="mytext" id="Title1" name="Title1" value="" runat="server" style="width:150px" />
                        <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" OnClick="Button1_Click" />
                        <input type="button" onclick="add(); return false;" value="添加按钮" class="mybutton" />
                        <asp:Button ID="Button2" runat="server" CssClass="mybutton" Text="删除所选" OnClientClick="return del();" OnClick="Button2_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <table class="listtable">
            <thead>
                <tr>
                    <th width="3%" style="text-align:center;"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                    <th width="20%">名称</th>
                    <th width="8%">图标</th>
                    <th width="30%">执行脚本</th>
                    <th width="20%">说明</th>
                    <th width="8%">排序</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <% 
                foreach (var but in ButtonList)
                { 
                %>
                    <tr>
                        <td style="text-align:center;"><input type="checkbox" name="checkbox_app" value="<%=but.ID %>" /></td>
                        <td><%=but.Name %></td>
                        <td><%=(but.Ico.IsNullOrEmpty()?"":but.Ico.IsFontIco() ? "<i class='fa " + but.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>" : "<img src=\"../.."+ but.Ico+"\" style=\"vertical-align:middle;\" />") %></td>
                        <td><%=but.Events %></td>
                        <td><%=but.Note %></td>
                        <td><%=but.Sort %></td>
                        <td>
                            <a class="editlink" href="javascript:void(0);" onclick="add('<%=but.ID %>');return false;">编辑</a>
                        </td>
                    </tr>
                 <%}%>
            </tbody>
        </table>
        <div class="buttondiv"><asp:Literal ID="PagerText" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        $(function ()
        {

        });
        function checkAll(checked)
        {
            $("input[name='checkbox_app']").prop("checked", checked);
        }
        function del()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert('您没有选择要删除的按钮!');
                return false;
            }
            return confirm('您真的要删除所选按钮吗?');
        }
        function add(id)
        {
            dialog.open({
                id: "window_" + appid.replaceAll('-', ''), title: "编辑按钮",
                width: 700, height: 450,
                url: '/Platform/Menu/ButtionsEdit.aspx?butid=' + (id || "") + '<%=Query1%>', opener: parent, openerid: iframeid
            });
        }

    </script>
</body>
</html>
