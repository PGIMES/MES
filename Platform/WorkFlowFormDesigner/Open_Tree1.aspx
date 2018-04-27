<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open_Tree1.aspx.cs" Inherits="WebForm.Platform.WorkFlowFormDesigner.Open_Tree1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <% 
        string rootid = new RoadFlow.Platform.Dictionary().GetIDByCode("FormTypes").ToString();    
    %>
    <form id="form1">
        <div id="tree"></div>
    </form>
    <script type="text/javascript">
        var AppID = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["iframeid"]%>';
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var roadTree = null;
        $(function ()
        {
            roadTree = new RoadUI.Tree({ id: "tree", path: "../Dictionary/Tree1.ashx?ischild=1&root=<%=rootid%>", refreshpath: "../Dictionary/TreeRefresh.ashx", onclick: openUrl });
        });
        function openUrl(json)
        {
            parent.frames[1].location = "Open_List1.aspx?typeid=" + json.id + "&appid=" + AppID + "&iframeid=" + iframeid + "&openerid=" + openerid;
        }
        function reLoad(id)
        {
            roadTree.refresh(id);
        }
    </script>
</body>
</html>
