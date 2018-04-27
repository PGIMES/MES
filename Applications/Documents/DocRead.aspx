<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocRead.aspx.cs" Inherits="WebForm.Applications.Documents.DocRead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%if("1"!=Request.QueryString["ismobile"]){ %>
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:99999; width:100%; margin-right:auto; height:26px;">
        <a href="javascript:void(0);" onclick="window.location='List.aspx<%=Request.Url.Query %>';return false;"><span style="background-image:url(../../Images/ico/arrow_medium_left.png);">返回列表</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <%if(IsEdit){ %>
        <a href="javascript:void(0);" onclick="window.location='DocAdd.aspx<%=Request.Url.Query %>';"><span style="background-image:url(../../Images/ico/topic_edit.gif);">编辑文档</span></a>
        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('您真的要删除该文档吗?');" runat="server" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/cancel.gif);">删除文档</span></asp:LinkButton>
        <%} %>
    </div>
    <%} %>
    <table cellpadding="0" cellspacing="0" border="0" width="98%;" class="formtable" style="<%="1"!=Request.QueryString["ismobile"]?"margin-top:40px;":""%>">
        <tr>
            <td style="height:28px; font-weight:bold; font-size:16px;">
                <asp:Literal ID="Title1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="height:28px; color:#666;">
                <span style="margin-right:12px;">栏目：<asp:Literal ID="DirectoryID" runat="server"></asp:Literal></span>
                <span style="margin-right:12px;">发布时间：<asp:Literal ID="WriteTime" runat="server"></asp:Literal></span>
                <span style="margin-right:12px;">发布人：<asp:Literal ID="WriteUserName" runat="server"></asp:Literal></span>
                <span style="margin-right:12px;">来源：<asp:Literal ID="Source" runat="server"></asp:Literal></span>
                <span style="margin-right:12px;"><asp:Literal ID="EditTime" runat="server"></asp:Literal></span>
                <span style="margin-right:12px;"><asp:Literal ID="EditUserName" runat="server"></asp:Literal></span>
                <span>阅读次数：<asp:Literal ID="ReadCount" runat="server"></asp:Literal></span>
            </td>
        </tr>
        <tr>
            <td style="border-bottom:1px solid #ccc;">
                
            </td>
        </tr>
        <tr>
            <td style="padding:8px 0;">
                <asp:Literal ID="Contents" runat="server"></asp:Literal>
            </td>
        </tr>
       
        <tr>
            <td style="height:28px;">
                <asp:Literal ID="Files" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
        <br />
    </form>
</body>
</html>
