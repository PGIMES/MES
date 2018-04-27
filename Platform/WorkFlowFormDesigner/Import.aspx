<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="WebForm.Platform.WorkFlowFormDesigner.Import" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:50px auto 0 auto; width:96%;">
        <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="确定导入" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
