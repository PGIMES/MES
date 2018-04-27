<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtionsEdit.aspx.cs" Inherits="WebForm.Platform.Menu.ButtionsEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
    <tr>
        <th style="width: 80px;">名称：</th>
        <td><input type="text" name="Name" id="Name" class="mytext" value="" runat="server" validate="empty" style="width: 75%"/></td>
    </tr>
    <tr>
        <th>执行脚本：</th>
        <td><textarea class="mytext" name="Events" id="Events" cols="1" rows="1" validate="empty" runat="server" style="width: 75%; height: 80px;"></textarea></td>
    </tr>
    <tr>
        <th style="width: 80px;">图标：</th>
        <td><input type="text" name="Ico" id="Ico" class="myico" source="/Images/ico" value="" runat="server" style="width: 69%"/></td>
    </tr>
    <tr>
        <th>备注说明：</th>
        <td><textarea class="mytext" name="Note" id="Note" runat="server" cols="1" rows="1" style="width: 95%; height: 50px;"></textarea></td>
    </tr>
    <tr>
        <th style="width: 80px;">排序：</th>
        <td><input type="text" name="Sort" id="Sort" class="mytext" value="" runat="server" validate="empty"/></td>
    </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text="确定保存" CssClass="mybutton" OnClientClick="return checkform(this);" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
    </div>
</form>
<script type="text/javascript">
    $(function ()
    {

    });
    function checkform(but)
    {
        var f = document.forms[0];
        var flag = new RoadUI.Validate().validateForm(f);
        return flag;
    }
</script>
</body>
</html>
