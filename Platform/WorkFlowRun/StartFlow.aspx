﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartFlow.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.StartFlow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    流程名称：<input type="text" class="mytext" id="FlowName" name="FlowName" runat="server" style="width:250px" />
                    <asp:Button ID="Button1" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button1_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="listtable" style="width:100%; margin:0 auto 0 auto;" >
            <thead>
                <tr>
                    <th>操作</th>
                    <th>流程名称</th>
                    <th>流程分类</th>
                    <th>创建日期</th>
                    <th>创建人</th>
                </tr>
            </thead>
            <tbody>
            <%foreach(var flow in StartFlows.OrderBy(p=>p.Name)){ %>
                <tr>
                    <td><a class="editlink" href="javascript:void(0);" onclick="start('<%=flow.ID %>','<%=flow.Name.Replace("'","") %>',<%=flow.OpenMode %>,<%=flow.WindowWidth %>,<%=flow.WindowHeight %>);">发起流程</a></td>
                    <td><%=flow.Name %></td>
                    <td><%=flow.Type %></td>
                    <td><%=flow.InstallDate %></td>
                    <td><%=flow.InstallUserName %></td>
                </tr>
            <%} %>
            </tbody>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        var appid = "<%=Request.QueryString["appid"]%>";
        function start(id, name, model, width, height)
        {
            var url = "/Platform/WorkFlowRun/Default.aspx?flowid=" + id + "&appid=" + appid;
            top.openApp(url, model, name, "tab_" + id.replaceAll('-', ''), width, height);
        }
    </script>
</body>
</html>
