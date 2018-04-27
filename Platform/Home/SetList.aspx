<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetList.aspx.cs" Inherits="WebForm.Platform.Home.SetList" %>

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
                    名称：<input type="text" class="mytext" id="Name1" name="Name1" runat="server" style="width:150px" />
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    类型：<select id="Type" name="Type" class="myselect"><option value=""></option><asp:Literal ID="TypeOptions" runat="server"></asp:Literal></select>
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;查&nbsp;询&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                    <input type="button" onclick="edit(); return false;" value="&nbsp;添&nbsp;加&nbsp;" class="mybutton" />
                    <asp:Button ID="Button1" runat="server" OnClientClick="return del();" CssClass="mybutton" Text="&nbsp;删&nbsp;除&nbsp;" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th><input type="checkbox" onclick="$('input[name=\'checkbox_app\']').prop('checked', this.checked);" style="vertical-align:middle;" /><label style="vertical-align:middle">模块名称</label></th>
                <th>显示标题</th>
                <th>类型</th>
                <th>数据来源</th>
                <th>使用对象</th>
                <th>备注</th>
                <th width="10%"></th>
            </tr>
        </thead>
        <tbody>
            <%foreach(var hi in HIList){ %>
            <tr>
                <td><input type="checkbox" value="<%=hi.ID %>" name="checkbox_app" id="app_<%=hi.ID %>" style="vertical-align:middle;"/><label for="app_<%=hi.ID %>" style="vertical-align:middle"><%=hi.Name %></label></td>
                <td>
                  <% 
                  if (!hi.Ico.IsNullOrEmpty())
                  {
                      if (hi.Ico.IsFontIco())
                      {
                          Response.Write("<i class='fa " + hi.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>");
                      }
                      else
                      {
                          Response.Write("<img src='" + hi.Ico + "' style='vertical-align:middle;margin-right:3px;'/>");
                      }
                  }  
                  %>
                    <%=hi.Title %></td>
                <td><%=BHI.GetTypeTitle(hi.Type) %></td>
                <td><%=BHI.GetDataSourceTitle(hi.DataSourceType) %></td>
                <td><%=BORG.GetNames(hi.UseOrganizes) %></td>
                <td><%=hi.Note %></td>
                <td>
                    <a class="editlink" href="javascript:void(0);" onclick="edit('<%=hi.ID %>');return false;">编辑</a>
                </td>
            </tr>
            <%} %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="PagerText" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        function edit(id)
        {
            window.location = 'SetAdd.aspx?id=' + (id || "") + "<%=Query1%>";
        }
        function del()
        {
            if ($(':checked[name=\'checkbox_app\']').size()==0)
            {
                alert("您没有选择要删除的项!");
                return false;
            }
            return confirm('真的要删除吗?');

        }
    </script>
</body>
</html>
