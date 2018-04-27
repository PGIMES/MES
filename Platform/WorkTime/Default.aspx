<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" EnableViewStateMac="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkTime.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:8px 0;">
            年份：<asp:DropDownList ID="DropDownList_Year" CssClass="myselect" Width="100" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList_Year_SelectedIndexChanged"></asp:DropDownList>
            &nbsp;&nbsp;<input type="button" onclick="addTime();" value=" 添 加 " class="mybutton" />
        </div>
        <div>
            <table class="listtable">
                <thead>
                    <tr>
                        <th>开始日期</th>
                        <th>结束日期</th>
                        <th>上午上班时间</th>
                        <th>上午下班时间</th>
                        <th>下午上班时间</th>
                        <th>下午下班时间</th>
                        <th style="width:10%">操作</th>
                    </tr>
                </thead>
                <tbody>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                    <tr>
                        <td><%#Eval("Date1").ToString().ToDateTime().ToDateString() %></td>
                        <td><%#Eval("Date2").ToString().ToDateTime().ToDateString() %></td>
                        <td><%#Eval("AmTime1") %></td>
                        <td><%#Eval("AmTime2") %></td>
                        <td><%#Eval("PmTime1") %></td>
                        <td><%#Eval("PmTime2") %></td>
                        <td>
                            <a class="editlink" onclick="addTime('<%#Eval("ID") %>');" href="javascript:void(0);">修改</a>
                            <asp:LinkButton ID="LinkButton1" CssClass="deletelink" OnClientClick="return confirm('您真的要删除该设置吗?');" runat="server" CommandName="del" CommandArgument='<%#Eval("ID")%>'>删除</asp:LinkButton>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        var tabid="<%=Request.QueryString["tabid"]%>";
        function addTime(id)
        {
            var url = "/Platform/WorkTime/Add.aspx<%=Request.Url.Query%>&year=" + $("#DropDownList_Year").val() + "&id=" + (id || "");
            new RoadUI.Window().open({ url: url, title: "设置工作时间", width: 600, height: 320, openerid: tabid, opener: window });
        }
    </script>
</body>
</html>
