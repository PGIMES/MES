<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sort.aspx.cs" Inherits="WebForm.Platform.Menu.Sort" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div style="padding-left:7px; width:83%; margin:0 auto; height:auto;" id="sortdiv">
            <%foreach (var app in menuList){  %>
            <ul class="sortul">
                <input type="hidden" value="<%=app.ID %>" name="sortapp" />
                <%=app.Title %>
            </ul>
            <% }%>
        </div>
        <div class="buttondiv">
            <asp:Button ID="Button1" runat="server" Text="保存排序" CssClass="mybutton" OnClick="Button1_Click" />
            <input type="button" class="mybutton" value="返回" onclick="re();" />
        </div>
    </form>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(function ()
        {
            new RoadUI.DragSort($("#sortdiv"));
        });
        function re()
        {
            window.location = "Body.aspx" + "<%=Request.Url.Query%>";
        }
    </script>
</body>
</html>
