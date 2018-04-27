<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0;">
        <a href="javascript:void(0);" onclick="return1(); return false;">
            <span style="background:url(../../Images/ico/arrow_medium_left.png) no-repeat left center;">返回</span>
        </a>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="publish(); return false;">
            <span style="background:url(../../Images/ico/system-tick-alt-03.png) no-repeat left center;">发布</span>
        </a>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="openurl('Set_Attr.aspx'); return false;">
            <span style="background:url(../../Images/ico/module.gif) no-repeat left center;">属性</span>
        </a>
        <a href="javascript:void(0);" onclick="openurl('Set_ListField.aspx'); return false;">
            <span style="background:url(../../Images/ico/table.gif) no-repeat left center;">列表</span>
        </a>
        <a href="javascript:void(0);" onclick="openurl('Set_Query.aspx'); return false;">
            <span style="background:url(../../Images/ico/search.png) no-repeat left center;">查询</span>
        </a>
        <a href="javascript:void(0);" onclick="openurl('Set_Button.aspx'); return false;">
            <span style="background:url(../../Images/ico/select.gif) no-repeat left center;">按钮</span>
        </a>
        <a href="javascript:void(0);" onclick="openurl('Set_Validate.aspx'); return false;">
            <span style="background:url(../../Images/ico/right.gif) no-repeat left center;">验证</span>
        </a>
        <a href="javascript:void(0);" onclick="openurl('Set_Export.aspx'); return false;">
            <span style="background:url(../../Images/ico/arrow_medium_right.png) no-repeat left center;">导出</span>
        </a>
    </div>
    <div style="width:100%; overflow:auto; vertical-align:top;">
        <iframe id="iframe_base" border="0" src="Set_Attr.aspx?1=1<%=Query %>" style="height:500px; width:100%; border:none;margin:0; padding:0;"></iframe>
    </div>
    </form>
    <script type="text/javascript">
        $(function ()
        {
            var height = $(window).height() - 40;
            $("#iframe_base").height(height);
        });
        function return1()
        {
            window.location = "List.aspx?1=1<%=Query%>";
        }
        function openurl(url)
        {
            $("#iframe_base").attr("src", url + "?1=1<%=Query%>");
        }
        function publish()
        {
            $.ajax({
                url: "Publish.ashx<%=Request.Url.Query%>", async: false, cache: false, success: function (txt)
                {
                    alert(txt);
                }
            });
        }
    </script>
</body>
</html>
