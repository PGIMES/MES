<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="WebForm.Platform.Menu.Tree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="menu"></div>
    </form>
    <script type="text/javascript">
        var AppID = '<%=Request.QueryString["appid"]%>';
        var roadTree = null;
        $(function ()
        {
            loadTree();
        });
        function loadTree()
        {
            $("#menu").html('');
            roadTree = new RoadUI.Tree({ id: "menu", path: "Tree1.ashx", refreshpath: "TreeRefresh.ashx", onclick: openUrl });
        }
        function reLoad(id)
        {
            if (roadTree != null)
            {
                roadTree.refresh(id);
            }
        }
        function openUrl(json)
        {
            parent.frames[1].location = "Body.aspx?id=" + json.id + "&appid=" + AppID;
        }
</script>
</body>
</html>
