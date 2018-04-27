<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Body.aspx.cs" Inherits="WebForm.Platform.Menu.Body" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
            <tr>
                <th style="width:80px;">应用名称：</th>
                <td><input type="text" id="Name" name="Name" class="mytext" value="" runat="server" validate="empty" style="width:75%" /></td>
            </tr>
            <tr>
                <th style="width:80px;">关联程序：</th>
                <td><select id="Type" name="Type" onchange="loadApp(this.value);" style="width:130px;" class="myselect" style="margin-right:5px"><option value=""></option><%=AppTypesOptions %></select>
                <select onclick="appidchange(this.value);" class="myselect" style="width:188px;" id="AppID" name="AppID"></select></td>
            </tr>
            <tr>
                <th style="width:80px;">相关参数：</th>
                <td><input type="text" id="Params" name="Params" value="" runat="server" class="mytext" style="width:75%"/></td>
            </tr>
           
        </table>
        <div class="buttondiv">
            <input type="button" value="添加子项" class="mybutton" onclick="window.location='AddApp.aspx'+'<%=Request.Url.Query%>';" />
            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="mybutton" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="Button1_Click" />
            <%if (!isRoot){ %>
                <asp:Button ID="Button2" runat="server" CssClass="mybutton" Text="删除" OnClientClick="return confirm('真的要删除该角色应用及其所有下级应用吗?');" OnClick="Button2_Click" />
                <input type="button" value="排序" class="mybutton" onclick="sort();" />
            <%}%>
        </div>
    </form>
    <script type="text/javascript">
        $(function ()
        {
            loadApp($("#Type").val());
        });
        function appidchange(value)
        {
            var options = $("#AppID option");
            for (var i = 0; i < options.length; i++)
            {
                if (value && options.eq(i).val() == value)
                {
                    $("#Name").val(options.eq(i).text());
                }
            }
        }
        function loadApp(value)
        {
            if (!value)
            {
                return false;
            }
            $.ajax({ url: "GetApps.ashx", type: "post", data: { "type": value, "value":"<%=appid%>" }, dataType: "text", async: false, cache: false, success: function (txt)
            {
                var $appid = $("#AppID");
                $("option", $appid).remove();
                $appid.append('<option value=""></option>'+txt);
            }
            });
        }
        function sort()
        {
            window.location = "Sort.aspx" + "<%=Request.Url.Query%>";
        }
    </script>
</body>
</html>
