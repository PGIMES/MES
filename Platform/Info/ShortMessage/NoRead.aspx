<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoRead.aspx.cs" Inherits="WebForm.Platform.Info.ShortMessage.NoRead" %>

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
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    内容：<input type="text" class="mytext" id="Contents" name="Contents" runat="server" style="width:150px" />
                    发送人：<input type="text" class="mymember" id="SenderID" name="SenderID" runat="server" />
                    发送时间：<input type="text" class="mycalendar" runat="server" style="width:80px;" name="Date1" id="Date1"/> 至 <input type="text" style="width:80px;" runat="server" class="mycalendar" name="Date2" id="Date2"/>
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" Text="标记为已读" CssClass="mybutton" OnClientClick="return read();" OnClick="Button3_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="40px"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th>标题</th>
                <th>内容概要</th>
                <th>发送人</th>
                <th>发送时间</th>
            </tr>
        </thead>
        <tbody>
            <% 
                foreach (var li in MsgList)
                {    
            %>
            <tr>
                <td><input type="checkbox" value="<%=li.ID %>" name="checkbox_app"  /></td>
                <td><a href="javascript:show('<%=li.ID %>');" class="blue1"><%=li.Title %></a></td>
                <td><%=RoadFlow.Utility.Tools.RemoveHTML(li.Contents).CutString(100) %></td>
                <td><%=li.SendUserName %></td>
                <td><%=li.SendTime.ToDateTimeStringS() %></td>
            </tr>

            <%} %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="PagerText" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        function checkAll(checked)
        {
            $("input[name='checkbox_app']").prop("checked", checked);
        }

        function read()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert("您没有选择要标记的消息!");
                return false;
            }
            return confirm('您真的要将所选消息标记为已读吗?');
        }
        function show(id)
        {
            new RoadUI.Window().open({ url: RoadUI.Core.rooturl() + "/Platform/Info/ShortMessage/Show.aspx?id=" + id, width: 900, height: 500, title: "查看消息" });
        }
    </script>
</body>
</html>
