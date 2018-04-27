﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPass.aspx.cs" Inherits="WebForm.Platform.UserInfo.EditPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body { overflow:hidden;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="97%" class="formtable">
    <tr>
        <th style="width: 100px;">
            旧密码：
        </th>
        <td>
            <input type="password" name="oldpass" id="oldpass" class="mytext" value="" validate="empty" style="width: 55%" />
        </td>
    </tr>
    <tr>
        <th>
            新密码：
        </th>
        <td>
            <input type="password" name="newpass" id="newpass" class="mytext" value="" validate="empty" style="width: 55%"/>
        </td>
    </tr>
    <tr>
        <th>
            确认新密码：
        </th>
        <td>
            <input type="password" name="newpass1" id="newpass1" class="mytext" value="" validate="equal" validate_equalfor="newpass" errmsg="与新密码输入不一致" style="width: 55%"/>
        </td>
    </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="确定保存" class="mybutton" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
    </div>
</form>
</body>
</html>
