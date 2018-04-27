<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Default1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:0; overflow:hidden;">
    <% 
        string query = "appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];    
    %>
    <table cellpadding="0" cellspacing="1" border="0" width="100%">
        <tr>
            <td style="width:170px; vertical-align:top; padding:5px 5px 0 5px;">
                <iframe id="Iframe1" frameborder="0" scrolling="auto" src="Open_Tree1.aspx?<%=query %>&iframeid=Iframe1" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
            <td class="organizesplit" style="padding:0;">
                <iframe id="WorkFLowManage_Iframe2" frameborder="0" scrolling="auto" src="Open_List1.aspx?<%=query %>&iframeid=WorkFLowManage_Iframe2" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function ()
        {
            var height = $(window).height();
            $('#Iframe1').attr('height', height - 10);
            $('#WorkFLowManage_Iframe2').attr('height', height - 10);
        });
    </script>
</body>
</html>
