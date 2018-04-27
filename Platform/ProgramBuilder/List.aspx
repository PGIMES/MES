<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.List" %>

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
                    应用名称：<input type="text" class="mytext" id="Name1" name="Name1" runat="server" style="width:150px" />
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                    <input type="button" onclick="edit(); return false;" value=" 添 加 " class="mybutton" />
                    <asp:Button ID="Button3" runat="server" Text=" 删 除 " CssClass="mybutton" OnClientClick="return del();" OnClick="Button3_Click" />
                    <asp:Button ID="Button1" runat="server" Text=" 发 布 " CssClass="mybutton" OnClientClick="return publish();" OnClick="Button1_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /><label style="vertical-align:middle">应用名称</label></th>
                <th>应用分类</th>
                <th>创建日期</th>
                <th>状态</th>
                <th width="10%">操作</th>
            </tr>
        </thead>
        <tbody>
        <% 
        foreach(var pb in pbList)
        {    
        %>
            <tr>
                <td><input type="checkbox" value="<%=pb.ID %>" name="checkbox_app" id="app_<%=pb.ID %>" style="vertical-align:middle;"/><label for="app_<%=pb.ID %>" style="vertical-align:middle"><%=pb.Name %></label></td>
                <td><%=BDict.GetTitle(pb.Type) %></td>
                <td><%=pb.CreateTime.ToDateTimeStringS() %></td>
                <td><%=pb.Status==0?"设计中":pb.Status==1?"已发布":"已作废" %></td>
                <td>
                    <a class="editlink" href="javascript:;" onclick="edit('<%=pb.ID %>');return false;">编辑</a>
                </td>
            </tr>
        <% 
        }   
        %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="PagerText" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
      
        function edit(id)
        {
            window.location = "Add.aspx?pid=" + (id || "")+"<%=Query1%>";
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
            return confirm('您真的要删除所选应用程序设计吗?');
        }
        function publish()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert("您没有选择要发布的项!");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
