<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hasten.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.Hasten" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width: 80px;">被催办人员：</th>
            <td>
                <asp:Literal ID="HastenUsersText" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>催办方式：</th>
            <td>
                <asp:Literal ID="HastenTypeText" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>催办内容：</th>
            <td>
                <textarea class="mytextarea" id="Contents" name="Contents" validate="empty" style="width:99%; height:120px;" runat="server"></textarea>
            </td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text="确认催办" CssClass="mybutton" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="关闭窗口" onclick="new RoadUI.Window().close();" />
    </div>
        
    </form>
</body>
</html>
