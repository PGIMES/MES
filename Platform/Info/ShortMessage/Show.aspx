<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="WebForm.Platform.Info.ShortMessage.Show" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="margin:10px"><%=message.Contents %></div>
    <div>
       <%=RoadFlow.Platform.Files.GetFilesShowString(message.Files) %>
    </div>
    </div>
    </form>
</body>
</html>
