<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Refresh.aspx.cs" Inherits="WebForm.Platform.Menu.Refresh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        var url = "Refresh1.ashx";
        RoadUI.Core.showWait("正在更新...");
        $.ajax({
            url: url, type: "Post", dataType: "text", cache: false, async: true,
            success: function (txt)
            {
                alert(txt);
                RoadUI.Core.closeWait();
                top.mainTab.closeTab();
            }
        });
    </script>
</body>
</html>
