<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetMenu.aspx.cs" Inherits="WebForm.Platform.Members.SetMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto;">
        <a href="javascript:void(0);" onclick="window.location='<%=(Request.QueryString["prev"]) %>';return false;"><span style="background-image:url(../../Images/ico/back.gif);">返回</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return checkform(this);" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/save.gif);">保存设置</span></asp:LinkButton>
    </div>
     <table id="treeTable1" style="width:100%; margin-top:37px;" class="listtable">
        <thead>
            <tr>
                <th style="width:26%;">标题</th>
                <th style="width:7%;">图标</th>
                <th style="width:7%; text-align:center"><input type="checkbox" id="checkall" onclick="$('input[type=\'checkbox\']').prop('checked', this.checked);" /></th>
                <th style="width:40%;">按钮</th>
                <th style="width:20%">参数</th>
            </tr>
        </thead>
        <tbody>
            <%=menuhtml %>
        </tbody>
    </table>
</form>
<script type="text/javascript">
    $(function ()
    {
        new RoadUI.TreeTable().init({ id: "treeTable1" });
        if ($(":checked[type='checkbox'][name='menuid']").size() > 0)
        {
            $("#checkall").prop('checked', true);
        }
    });
    function checkform(but)
    {
        return true;
    }
    function appboxclick(box)
    {
        if (!box.checked)
        {
            $(box).parent().next().find("input[type='checkbox']").prop('checked', box.checked);
        }
        else
        {
            var treetable = new RoadUI.TreeTable();
            treetable.parents = [];
            treetable.getAllParents($(box).parent().parent());
            for (var i = 0; i < treetable.parents.length; i++)
            {
                $("input[type='checkbox'][name='menuid']", treetable.parents[i]).prop('checked', true);
            }
        }
    }
    function buttonboxclick(box)
    {
        if ($(":checked", $(box).parent()).size() > 0)
        {
            $(box).parent().prev().find("input[type='checkbox']").prop('checked', true);
        }
        else
        {
            //$(box).parent().prev().find("input[type='checkbox']").prop('checked', false);
        }
    }
</script>
</body>
</html>
