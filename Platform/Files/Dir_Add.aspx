<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dir_Add.aspx.cs" Inherits="WebForm.Platform.Files.Dir_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto;">
        <a href="javascript:void(0);" onclick="window.location='List.aspx<%=Request.Url.Query %>';return false;"><i class='fa fa-mail-reply (alias)' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>返回</label></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="LinkButton1_Click"><i class='fa fa-save (alias)' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>保存</label></asp:LinkButton>
    </div>
    <div style="margin-top:100px; margin-left:30px;">
       文件夹名称：<input type="text" class="mytext" validate="dirname" style="width:300px" id="DirName" name="DirName" runat="server" />
    </div>
    </form>
</body>
</html>
