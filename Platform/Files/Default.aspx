﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.Files.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:0; overflow:hidden;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width:<%="1"==Request.QueryString["isselect"]?"160px":"300px"%>; vertical-align:top; padding:5px 5px 0 5px;">
                <iframe id="Iframe1" frameborder="0" scrolling="auto" src="Tree.aspx<%=Request.Url.Query %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
            <td class="organizesplit" style="padding:0;">
                <iframe id="Iframe2_FilesList" frameborder="0" scrolling="auto" src="List.aspx<%=Request.Url.Query %>&id=<%=new RoadFlow.Platform.Files().GetUserRootPath(CurrentUserID).DesEncrypt() %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(window).load(function ()
        {
            var height = $(window).height();
            if (height == 0)
            {
                height = $(parent).height();
            }
            $('#Iframe1').attr('height', height - 10);
            $('#Iframe2_FilesList').attr('height', height - 10);
        });
    </script>
    </form>
</body>
</html>
