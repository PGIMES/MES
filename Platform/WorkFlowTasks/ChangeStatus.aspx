<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeStatus.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.ChangeStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; padding-top:50px;">
            <div>
                将状态改变为：
                <select id="Status" name="Status" class="myselect" validate="empty">
                    <option value=""></option>
                    <%=BTask.GetStatusListItems(Status) %>
                </select>
                <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="确&nbsp;定" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="Button1_Click" />
                <span type="msg"></span>
            </div>
        </div>
    </form>
</body>
</html>
