<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="WebForm.Platform.Members.Tree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-bottom:4px;">
        <input type="text" class="mytext" runat="server" id="searchtext" style="width:110px;" />
        <input type="button" class="mybutton" value=" 查 询 " style="margin-left:2px;" onclick="search();"/>
    </div>
    <div style="margin-bottom:4px;">
        <select onchange="treecng(this.value);" id="showtype" class="myselect" style="width:114px;">
            <option value="0">组织机构</option>
            <option value="1">角色组</option>
        </select>
        <input type="button" style="margin-left:2px;" class="mybutton" id="addWg" onclick="parent.frames[1].location = 'WorkGroupAdd.aspx' + '<%=Request.Url.Query%>'" value="添加角色组" style="display:none;" />
    </div>
    <div id="menu"></div>
    </form>
    <script type="text/javascript">
        var orgTree = null;
        var AppID = '<%=Request.QueryString["appid"]%>';
        $(function ()
        {
            treecng($("#showtype").val());
        });
        function treecng(val)
        {
            if (!val)
            {
                val = $("#showtype").val();
            }
            if ("1" == val)
            {
                $("#addWg").show();
                $("#addRole").hide();
            }
            else
            {
                $("#addWg").hide();
                $("#addRole").hide();
            }
            orgTree = new RoadUI.Tree({ id: "menu", path: "Tree1.ashx?showtype=" + val, refreshpath: "TreeRefresh.ashx?showtype=" + val, onclick: openurl });
            parent.frames[1].location = 'Empty.aspx?appid=' + AppID;
        }

        function openurl(json)
        {
            var query = "&appid=" + AppID + "&parentid=" + json.parentID + "&type=" + json.type;
            switch (parseInt(json.type))
            {
                case 1:
                case 2:
                case 3:
                    parent.frames[1].location = "Body.aspx?id=" + json.id + query;
                    break;
                case 4:
                    parent.frames[1].location = "User.aspx?id=" + json.id + query;
                    break;
                case 5:
                    parent.frames[1].location = "WorkGroup.aspx?id=" + json.id + query;
                    break;
            }
        }

        function reLoad(id)
        {
            orgTree.refresh(id);
        }

        function search()
        {
            var value=$.trim($("#searchtext").val());
            var val=$("#showtype").val();
            orgTree = new RoadUI.Tree({ id: "menu", path: "Tree1.ashx?showtype=" + val+"&searchword="+encodeURI(value), refreshpath: "TreeRefresh.ashx?showtype=" + val, onclick: openurl });
            parent.frames[1].location = 'Empty.aspx?appid=' + AppID;
        }
    </script>
</body>
</html>
