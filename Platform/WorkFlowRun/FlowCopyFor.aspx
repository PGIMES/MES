<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowCopyFor.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.FlowCopyFor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br /><br />
    <div style="text-align:center;">
        接收人：<input type="text" id="user" name="user" validate="empty" class="mymember" style="width:200px;" />
    </div><br />
    <div style="text-align:center;">
        <asp:Button ID="Button1" runat="server" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" CssClass="mybutton" Text="&nbsp;确&nbsp;定&nbsp;" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="&nbsp;取&nbsp;消&nbsp;" onclick="new RoadUI.Window().close();" />
    </div>
   
    </form>
</body>
</html>
