<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" EnableViewStateMac="false" CodeBehind="TableQuery.aspx.cs" Inherits="WebForm.Platform.DBConnection.TableQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .sqltextarea { width:100%; height:200px; vertical-align:top; padding:3px; font-family:Verdana; font-size:14px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:8px auto 0 auto; width:98%;">
        <div style="margin:6px 0;">SQL:</div>
        <div>
            <asp:TextBox ID="sqltext" runat="server" TextMode="MultiLine" CssClass="sqltextarea"></asp:TextBox>
        </div>
        <div style="margin:6px 0; text-align:center;">
            <asp:Button CssClass="mybutton" ID="Button1" runat="server" Text=" 执行SQL " OnClick="Button1_Click" />
            <input type="button" class="mybutton" value=" 清空SQL " onclick="$('#sqltext').val('');" style="margin-left:8px;" />
            <input type="button" class="mybutton" value=" 返回前页 " onclick="window.location='Table.aspx<%=Request.Url.Query%>'" style="margin-left:8px;" />
        </div>
        
        <div style="margin:6px 0;" id="resultdiv" runat="server" visible="false">
            <div style="margin:6px 0;">执行结果<asp:Literal ID="LiteralResultCount" runat="server"></asp:Literal>:</div>
            <div id="tablediv" style="overflow:auto;">
                <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
        <br /><br />
    </form>
    <script type="text/javascript">
        $(function(){
            $("#tablediv").width($(window).width-100);
            
        });
    </script>
</body>
</html>
