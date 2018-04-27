<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.Log.Default" %>

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
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" value="" runat="server" />
                    分类：<select class="myselect" id="Type" name="Type"><option value="">==全部==</option><asp:Literal ID="TypeOptions" runat="server"></asp:Literal></select>
                    人员：<input type="text" user="true" dept="false" value="" runat="server" station="false" unit="false" more="false" group="false" id="UserID" name="UserID" class="mymember" />
                    发生日期：<input type="text" class="mycalendar" id="Date1" name="Date1" style="width:90px;" value="" runat="server" /> 至 <input type="text" class="mycalendar" id="Date2" name="Date2" style="width:90px;" value="" runat="server" />
                    <asp:Button ID="Button1" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="45%">标题</th>
                <th width="10%">分类</th>
                <th width="15%">发生时间</th>
                <th width="10%">操作员</th>
                <th width="10%">发生IP</th>
                <th width="10%">详细</th>
            </tr>
        </thead>
        <tbody>
            <%foreach(System.Data.DataRow dr in Dt.Rows){ %>
            <tr>
                <td><%=dr["title"] %></td>
                <td><%=dr["Type"] %></td>
                <td><%=dr["WriteTime"].ToString().ToDateTimeStringS() %></td>
                <td><%=dr["UserName"] %></td>
                <td><%=dr["IPAddress"] %></td>
                <td><a class="viewlink" href="javascript:void(0);" onclick="detail('<%=dr["ID"] %>');return false;">查看</a></td>
            </tr>
            <%} %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>   
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        var query = '<%=Query%>';
        function detail(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "查看日志详细信息", width: 980, height: 550, url: "/Platform/Log/Detail.aspx?id=" + id + '<%=Query%>', opener:window, openerid: iframeid });
        }
    </script>
</body>
</html>
