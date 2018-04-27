<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkGroup.aspx.cs" Inherits="WebForm.Platform.Members.WorkGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var win = new RoadUI.Window();
        var validate = new RoadUI.Validate();
    </script>
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:80px;">名称：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" validate="empty,minmax" runat="server" value="" max="100" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">成员：</th>
            <td><input type="text" id="Members" name="Members" class="mymember" validate="empty" runat="server" value="" style="width:65%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">人员：</th>
            <td><asp:Literal ID="UsersText" runat="server"></asp:Literal></td>
        </tr>
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="button" class="mybutton" value="设置菜单" onclick="setMenu();" />
        <asp:Button ID="Button1" runat="server" Text="保存" CssClass="mybutton" OnClientClick="return validate.validateForm(document.forms[0]);" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="删除" CssClass="mybutton" OnClientClick="return confirm('您真的要删除该工作组吗?');" OnClick="Button2_Click" />
    </div>
    </form>
    <script type="text/javascript">
        function setMenu()
        {
            var url = "SetMenu.aspx?prev=WorkGroup.aspx<%=query%>";
            //top.mainDialog.open({url: url,width:900,height:550,title:"设置菜单",opener:parent});
            window.location = url;
            return false;
        }
</script>
</body>
</html>
