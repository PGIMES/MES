<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserInfo.aspx.cs" Inherits="WebForm.Platform.UserInfo.EditUserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_info" style="width:96%; margin:8px auto 0 auto;" >
            <table cellpadding="0" cellspacing="1" border="0" width="96%" class="formtable" style="margin-top:15px;">
            <tr>
                <th style="width: 100px;">办公电话：</th>
                <td>
                    <input type="text" name="Tel" id="Tel" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>手机：</th>
                <td>
                    <input type="text" name="MobilePhone" id="MobilePhone" class="mytext" runat="server" value="" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>传真：</th>
                <td>
                    <input type="text" name="Fax" id="Fax" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>邮箱：</th>
                <td>
                    <input type="text" name="Email" id="Email" validate="canempty,email" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>QQ：</th>
                <td>
                    <input type="text" name="QQ" id="QQ" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>微信号：</th>
                <td>
                    <input type="text" name="WeiXin" id="WeiXin" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>其他联系方式：</th>
                <td>
                    <input type="text" name="OtherTel" id="OtherTel" class="mytext" value="" runat="server" style="width: 90%" />
                </td>
            </tr>
            <tr>
                <th>说明：</th>
                <td>
                    <textarea class="mytextarea" name="Note" id="Note" style="width: 90%; height:90px;" runat="server"></textarea>
                </td>
            </tr>
        </table>
            <div class="buttondiv">
                <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="确定保存" OnClick="Button1_Click" />
                <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
            </div>
        </div>
    </form>
</body>
</html>
