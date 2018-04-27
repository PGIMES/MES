<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Platform.WorkFlowArchives.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
        <table cellpadding="0" cellspacing="1" border="0" width="100%">
            <tr>
                <td>
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:180px" />
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="mybutton" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="50%">标题</th>
                <th width="15%">流程</th>
                <th width="15%">步骤</th>
                <th width="20%">归档时间</th>
            </tr>
        </thead>
        <tbody>
        <% 
        foreach (System.Data.DataRow dr in Dt.Rows)
        {%>
        
            <tr>
                <td><a href="javascript:;" onclick="show('<%=dr["ID"] %>');return false;" class="blue"><%=dr["Title"] %></a></td>
                <td><%=dr["FlowName"] %></td>
                <td><%=dr["StepName"] %></td>
                <td><%=dr["WriteTime"].ToString().ToDateTimeStringS() %></td>
            </tr>
        <% } %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
    </form>

    <script type="text/javascript">
        var appid = '<%=appid%>';
        var iframeid = '<%=tabid%>';
        var typeid = '<%=typeid%>';
        var dialog = new RoadUI.Window();
       
        function show(id)
        {
            var url = RoadUI.Core.rooturl() + '/Platform/WorkFlowArchives/Show.aspx?id=' + id + '<%=query1%>';
            RoadUI.Core.open(url, 980, 500, "查看归档内容");
            //dialog.open({
            //    id: "window_" + appid.replaceAll('-', ''), title: "查看归档内容", width: 980, height: 500,
             //   url: , openerid: iframeid
            //});
        }
    </script>
</body>
</html>
