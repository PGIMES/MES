<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Export.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Export" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%" style="text-align:center;"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th>字段名称</th>
                <th>列标题</th>
                <th>显示方式</th>
                <th>对齐方式</th>
                <th>显示宽度</th>
                <th>显示顺序</th>
                <th width="10%"></th>
            </tr>
        </thead>
        <tbody>
        <% 
            foreach(var filed in fieldList.OrderBy(p=>p.Sort))
            {    
        %>
            <tr>
                <td style="text-align:center;"><input type="checkbox" value="<%=filed.ID %>" name="checkbox_app" style="vertical-align:middle;"/></td>
                <td><%=filed.Field %></td>
                <td><%=filed.ShowTitle %></td>
                <td><%=BDict.GetTitle("programbuilder_showtype",filed.ShowType.ToString()) %></td>
                <td><%=filed.Align==0?"左对齐":filed.Align==1?"居中":"右对齐" %></td>
                <td><%=filed.Width %></td>
                <td><%=filed.Sort %></td>
                <td>
                    <a class="editlink" href="javascript:;" onclick="add('<%=filed.ID %>'); return false;">编辑</a>
                </td>
            </tr>
        <% 
            }    
        %>
        </tbody>
    </table>
    </div>
    <div class="buttondiv">
        <input type="button" class="mybutton" onclick="add('');" value="添加字段" />
        <asp:Button ID="Button1" runat="server" Text=" 删 除 " OnClientClick="return del();" CssClass="mybutton" OnClick="Button1_Click" />
    </div>
        
    </form>
    <script type="text/javascript">
        $(function ()
        {

        });
        function checkAll(checked)
        {
            $("input[name='checkbox_app']").prop("checked", checked);
        }
        function add(id)
        {
            window.location = "Set_Export_Add.aspx?fid=" + (id || "") + "<%=query%>";
        }
        function del()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert("您没有选择要删除的项!");
                return false;
            }
            return confirm('您真的要删除所选字段吗?');
        }
    </script>
</body>
</html>
