<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open_List1.aspx.cs" Inherits="WebForm.Platform.WorkFlowFormDesigner.Open_List1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="ueditor/ueditor.config.js"></script>
    <script src="ueditor/ueditor.all.min.js"></script>
    <script src="ueditor/lang/zh-cn/zh-cn.js"></script>
    <script src="ueditor/plugins/plugins.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="99%" align="center">
        <tr>
            <td align="left" height="35">
                名称：<input type="text" class="mytext" style="width:160px;" id="form_name" runat="server" name="form_name" />
                <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text=" 查 询 " OnClick="Button1_Click" />
                <input type="button" class="mybutton" onclick="newform();" value="新建表单" />
                <input type="button" class="mybutton" onclick="ImportForm();" value="导入表单" />
            </td>
        </tr>
    </table>

    <table class="listtable">
        <thead>
        <tr>
            <th width="40%">表单名称</th>
            <th width="12%">创建人</th>
            <th width="15%">创建时间</th>
            <th width="15%">修改时间</th>
            <th width="16%" sort="0"></th>
        </tr>
        </thead>
        <tbody>
        <% 
            foreach(var form in forms)
            {    
        %>
            <tr>
                <td title="表单ID：<%=form.ID %>"><%=form.Name %></td>
                <td><%=form.CreateUserName %></td>
                <td><%=form.CreateTime.ToDateTimeString() %></td>
                <td><%=form.LastModifyTime.ToDateTimeString() %></td>
                <td>
                    <a class="editlink" href="javascript:void(0);" onclick="openform('<%=form.ID %>','<%=form.Name %>');return false;">
                        <span style="vertical-align:middle;">编辑</span>
                    </a>
                    <a class="deletelink" href="javascript:void(0);" onclick="delform('<%=form.ID %>'); return false;">
                        <span style="vertical-align:middle;">删除</span>
                    </a>
                    <a href="javascript:void(0);" onclick="ExportForm('<%=form.ID %>'); return false;">
                        <span style="vertical-align:middle; background:url(../../Images/ico/arrow_medium_right.png) no-repeat;padding-left:18px;">导出</span>
                    </a>
                </td>
            </tr>
        <% 
            }    
        %>
        </tbody>
    </table>
    <div class="buttondiv">
        <asp:Literal ID="PaerText" runat="server"></asp:Literal>
    </div>
    </form>
    <script type="text/javascript">
        var iframeid = "<%=Request.QueryString["iframeid"]%>";
        function openform(id, formname)
        {
            top.openApp("/Platform/WorkFlowFormDesigner/Default.aspx?formid=" + id + "<%=query%>", 0, "编辑" + formname + "表单", id);
        }
        function newform()
        {
            top.openApp("/Platform/WorkFlowFormDesigner/Default.aspx?isnewform=1<%=query%>", 0, "新建表单", RoadUI.Core.newid(false));
        }
        function delform(id)
        {
            if (!confirm("您真的要删除该表单吗?"))
            {
                return false;
            }
            $.ajax({
                url: "ueditor/plugins/dialogs/delete.aspx?id="+id, async: false, cache: false, success: function (txt)
                {
                    if ("1" == txt)
                    {
                        alert("删除成功!");
                        window.location = window.location;
                    }
                }
            });
        }
        function ExportForm(formid)
        {
            var url = "Export.aspx?formid=" + formid + "<%=query%>";
            window.location = url;
        }

        function ImportForm()
        {
            var url = "/Platform/WorkFlowFormDesigner/Import.aspx?1=1<%=query%>";
            new RoadUI.Window().open({ title: "导入表单", width: 400, height: 200, url: url, opener: window, openerid: iframeid, resize: false });
        }
    </script>
</body>
</html>
