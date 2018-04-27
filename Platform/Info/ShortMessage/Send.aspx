<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Send.aspx.cs" Inherits="WebForm.Platform.Info.ShortMessage.Send" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../../Scripts/Ueditor/ueditor.config.js"></script>
    <script src="../../../Scripts/Ueditor/ueditor.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="98%" class="formtable">
        <tr>
            <th style="width: 80px;">标题：</th>
            <td><input type="text" id="Title1" name="Title1" class="mytext" runat="server" style="width: 85%"/></td>
        </tr>
        <tr>
            <th>接收人：</th>
            <td><input type="text" id="ReceiveUserID" name="ReceiveUserID" class="mymember" runat="server" style="width: 55%" />
                <%if(RoadFlow.Platform.WeiXin.Config.IsUse){ %>
                <input type="checkbox" name="sendtoseixin" checked="checked" id="sendtoseixin" value="1" style="vertical-align:middle;"/><label for="sendtoseixin" style="vertical-align:middle;">同时发送到微信</label>
                <%} %>
            </td>
        </tr>
        <tr>
            <th>内容：</th>
            <td><textarea id="Contents" name="Contents" model="html" class="mytextarea" style="width:99%; height:350px;" runat="server"></textarea></td>
        </tr>
        <tr>
            <th>附件：</th>
            <td><input type="text" id="Files" name="Files" class="myfile" runat="server" style="width: 65%"/></td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text="确定发送" OnClientClick="return checkf();" CssClass="mybutton" OnClick="Button1_Click" />
    </div>
    </form>
    <script type="text/javascript">
        function checkf()
        {
            if($.trim($("#Title1").val()).length==0)
            {
                alert('标题不能为空!');
                $("#Title1").focus();
                return false;
            }
            if ($.trim($("#ReceiveUserID").val()).length == 0)
            {
                alert('接收人不能为空!');
                $("#ReceiveUserID").focus();
                return false;
            }
            var ue = UE.getEditor("Contents");
            if (!ue.hasContents())
            {
                alert('内容不能为空!');
                ue.focus(true);
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
