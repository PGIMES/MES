<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirAdd.aspx.cs" Inherits="WebForm.Applications.Documents.DirAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:26px;">
        <a href="javascript:void(0);" onclick="window.location='List.aspx<%=Request.Url.Query %>';return false;"><span style="background-image:url(../../Images/ico/arrow_medium_left.png);">返回列表</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" runat="server" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/save.gif);">保存栏目</span></asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('删除栏目将删除所有子栏目及其文档,真的删除吗?');" runat="server" OnClick="LinkButton2_Click"><span style="background-image:url(../../Images/ico/cancel.gif);">删除栏目</span></asp:LinkButton>
    </div>
    
    <table cellpadding="0" cellspacing="1" border="0" width="90%;" class="formtable" style="margin-top:36px;">
        <tr>
            <th style="width: 100px;">上级栏目：</th>
            <td>
                <input type="hidden" name="DirectoryID" id="DirectoryID" value="<%=DirID %>" />
                <%=DocDir.GetName(DirID.ToGuid()) %>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">栏目名称：</th>
            <td><input type="text" id="Name" name="Name" validate="empty" class="mytext" runat="server"  style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 100px;">文档发布人员:</th>
            <td><input type="text" id="PublishUsers" name="PublishUsers" class="mymember" runat="server"  style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 100px;">文档阅读人员:</th>
            <td><input type="text" id="ReadUsers" name="ReadUsers" class="mymember" runat="server"  style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 100px;">管理人员:</th>
            <td><input type="text" id="ManageUsers" name="ManageUsers" class="mymember" runat="server"  style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 100px;">排序：</th>
            <td><input type="text" id="Sort" name="Sort" class="mytext" runat="server"  style="width: 85%"/></td>
        </tr>
    </table>
    </form>
</body>
</html>
